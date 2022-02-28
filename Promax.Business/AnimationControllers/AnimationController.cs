using Extensions;
using Promax.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Utility.Binding;

namespace Promax.Business
{
    public class AnimationController : IDesignModeController, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        private IDesignModeController _designModeController;
        private IAnimationSerializer _animationSerializer;
        private MyBinding _entityBindings = new MyBinding();


        public AnimationObjectContainer AnimationObjectContainer { get; private set; }
        public EntitiesWithStringKeyContainer Entities { get; private set; }

        public AnimationController(IDesignModeController designModeController, IAnimationSerializer animationSerializer, AnimationObjectContainer animationObjectContainer, EntitiesWithStringKeyContainer entitiesToBind)
        {
            _designModeController = designModeController;
            _animationSerializer = animationSerializer;
            AnimationObjectContainer = animationObjectContainer;
            Entities = entitiesToBind;
            Entities.ObjectRegistered += Entities_ObjectRegistered;
            AnimationObjectContainer.ObjectRegistered += AnimationObjectContainer_ObjectRegistered;
            Bind();
            _animationSerializer.Deserialize(AnimationObjectContainer.Objects.ToArray());
        }

        private void Entities_ObjectRegistered(object container, string registeredT1, IMyEntity registeredT2)
        {
            IReadOnlyDictionary<string, IMyEntity> sourcesDic = new Dictionary<string, IMyEntity>().DoReturn(x => x.Add(registeredT1, registeredT2));
            foreach (var animationObject in AnimationObjectContainer.Objects)
            {
                BindAnimationObject(sourcesDic, animationObject);
            }
            _entityBindings?.InitialMapping();
        }

        private void AnimationObjectContainer_ObjectRegistered(object container, IAnimationObject registeredObject)
        {
            EntitiesWithStringKeyContainer entityContainer = Entities;
            var sourcesDic = entityContainer.Objects;
            BindAnimationObject(sourcesDic, registeredObject);
            _entityBindings?.InitialMapping();
        }

        #region IDesignModeController
        public bool InDesignMode => _designModeController.InDesignMode;

        public void EnterDesignMode()
        {
            _designModeController.EnterDesignMode();
            OnPropertyChanged(nameof(InDesignMode));
        }

        public void ExitDesignMode()
        {
            _designModeController.ExitDesignMode();
            OnPropertyChanged(nameof(InDesignMode));
        }
        #endregion
        public void SaveAnimations()
        {
            _animationSerializer.Serialize(AnimationObjectContainer.Objects.ToArray());
        }
        public void LoadAnimations()
        {
            _animationSerializer.Deserialize(AnimationObjectContainer.Objects.ToArray());
        }
        private void Bind()
        {
            _entityBindings.ClearBindings();
            EntitiesWithStringKeyContainer container = Entities;
            var sourcesDic = container.Objects;
            foreach (var animationObject in AnimationObjectContainer.Objects)
            {
                BindAnimationObject(sourcesDic, animationObject);
            }
            _entityBindings.InitialMapping();
        }

        private void BindAnimationObject(IReadOnlyDictionary<string, IMyEntity> sourcesDic, IAnimationObject animationObject)
        {
            animationObject.DoIf(x => !string.IsNullOrEmpty(x.AnimationObjectName) && sourcesDic.ContainsKey(x.AnimationObjectName.RemoveAfter("_")), animObj =>
            {
                sourcesDic[animObj.AnimationObjectName.RemoveAfter("_")].Do(entity =>
                {
                    BindAnimationObjectToEntity(entity, animObj);
                });
            });
        }
        private void BindAnimationObjectToEntity(IMyEntity entity, IAnimationObject animationObject)
        {
            var sourceProps = entity.EntityProperties;
            var targetProps = animationObject.EntityBindableProperties;
            foreach (var targetProp in targetProps)
            {
                foreach (var sourceProp in sourceProps)
                {
                    if (sourceProp.IsEqual(targetProp))
                    {
                        _entityBindings.CreateBinding().Source(entity).SourceProperty(sourceProp).Target(animationObject).TargetProperty(targetProp).Mode(MyBindingMode.OneWay);
                        break;
                    }
                }
            }
            if (animationObject is IDirectedPropertyOwner)
            {
                var obj = animationObject as IDirectedPropertyOwner;
                foreach (var property in obj.DirectedProperties)
                {
                    foreach (var sourceProp in sourceProps)
                    {
                        if (property.Key.IsEqual(sourceProp))
                        {
                            foreach (var directedProperty in property.Value)
                            {
                                _entityBindings.CreateBinding().Source(entity).SourceProperty(sourceProp).Target(animationObject).TargetProperty(directedProperty).Mode(MyBindingMode.OneWay);
                            }
                        }
                    }
                }
            }
        }
    }
}
