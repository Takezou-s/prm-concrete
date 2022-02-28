using System;
using System.ComponentModel;
using Extensions;

namespace Utility.Binding
{
    public class BindingHandler
    {
        private Action _whenTargetPropertyChanged, _whenSourcePropertyChanged;

        public bool Deactivated { get; private set; }

        private Func<bool> _predicate;
        private Func<bool> _ignoreChangedPredicate;
        private bool _mapToTarget, _mapToSource;
        private bool _initialMapped;

        public MyBindingMode BindingMode { get; private set; } = MyBindingMode.TwoWay;
        public MyBindingBehaviour BindingBehaviour { get; private set; } = MyBindingBehaviour.MapNInvoke;
        public MyBindingType BindingType { get; private set; } = MyBindingType.Automatic;
        public bool IgnoreChangedWhileMapping { get; private set; }
        private bool IgnoreChangedPredicate
        {
            get
            {
                bool result = false;
                _ignoreChangedPredicate.Do(x => result = x());
                return result;
            }
        }
        public object SourceObject { get; private set; }
        public string SourcePropertyName { get; private set; }
        public object TargetObject { get; private set; }
        public string TargetPropertyName { get; private set; }
        public IBeeValueConverter Converter { get; private set; }
        public bool Map()
        {
            if (Deactivated)
                return false;
            bool result = false;
            MyBinding.ExceptionHandler.Log(() =>
            {
                _mapToTarget.DoIfReturn(x => x || IgnoreChangedPredicate || IgnoreChangedWhileMapping, x => { result = true; SourcePropertyChangedImp(new PropertyChangedEventArgs(SourcePropertyName)); }).Do(x => _mapToTarget = false);
                _mapToSource.DoIfReturn(x => x || IgnoreChangedPredicate || IgnoreChangedWhileMapping, x => { result = true; TargetPropertyChangedImp(new PropertyChangedEventArgs(TargetPropertyName)); }).Do(x => _mapToSource = false);
            });
            return result;
        }
        public bool MapIgnoreChanged()
        {
            IgnoreChangedWhileMapping = true;
            bool result = Map();
            IgnoreChangedWhileMapping = false;
            return result;
        }
        public void InitialMapping()
        {
            if (Deactivated)
                return;
            MyBinding.ExceptionHandler.Log(() =>
            {
                if (_initialMapped)
                    return;
                SourcePropertyChangedImp(new PropertyChangedEventArgs(SourcePropertyName));
                TargetPropertyChangedImp(new PropertyChangedEventArgs(TargetPropertyName));
                _initialMapped = true;
            });
        }
        #region PropInit
        public void Deactivate()
        {
            if (SourceObject != null && SourceObject is INotifyPropertyChanged)
                (SourceObject as INotifyPropertyChanged).PropertyChanged -= SourcePropertyChanged;
            if (TargetObject != null && TargetObject is INotifyPropertyChanged)
                (TargetObject as INotifyPropertyChanged).PropertyChanged -= TargetPropertyChanged;
            SourceObject = null;
            TargetObject = null;
            _ignoreChangedPredicate = null;
            _predicate = null;
            _whenSourcePropertyChanged = null;
            _whenTargetPropertyChanged = null;
            Deactivated = true;
        }
        public BindingHandler IgnoreChanged(bool ignore)
        {
            IgnoreChangedWhileMapping = ignore;
            return this;
        }
        public BindingHandler IgnoreChangedWhen(Func<bool> predicate)
        {
            _ignoreChangedPredicate = predicate;
            return this;
        }
        public BindingHandler Type(MyBindingType type)
        {
            BindingType = type;
            return this;
        }
        public BindingHandler Convert(IBeeValueConverter converter)
        {
            Converter = converter;
            return this;
        }
        public BindingHandler Behaviour(MyBindingBehaviour behaviour)
        {
            BindingBehaviour = behaviour;
            return this;
        }
        public BindingHandler Source(object obj)
        {
            SourceObject = obj;
            if (obj == null)
                return this;
            if (obj is INotifyPropertyChanged)
            {
                (obj as INotifyPropertyChanged).PropertyChanged += SourcePropertyChanged;
            }
            return this;
        }
        public BindingHandler UpdateWhile(Func<bool> predicate)
        {
            _predicate = predicate;
            return this;
        }
        public BindingHandler WhenTargetPropertyChanged(Action action)
        {
            _whenTargetPropertyChanged = action;
            return this;
        }
        public BindingHandler WhenSourcePropertyChanged(Action action)
        {
            _whenSourcePropertyChanged = action;
            return this;
        }

        public BindingHandler SourceProperty(string propertyName)
        {
            SourcePropertyName = propertyName;
            return this;
        }
        public BindingHandler Target(object obj)
        {
            TargetObject = obj;
            if (obj == null)
                return this;
            if (obj is INotifyPropertyChanged)
            {
                (obj as INotifyPropertyChanged).PropertyChanged += TargetPropertyChanged;
            }
            return this;
        }
        public BindingHandler TargetProperty(string propertyName)
        {
            TargetPropertyName = propertyName;
            return this;
        }
        public BindingHandler Mode(MyBindingMode bindingMode)
        {
            BindingMode = bindingMode;
            return this;
        }
        #endregion

        private void TargetPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Deactivated)
                return;
            MyBinding.ExceptionHandler.Log(() =>
            {
                if (e.PropertyName == TargetPropertyName)
                {
                    if (BindingType == MyBindingType.Automatic)
                        TargetPropertyChangedImp(e);
                    else if (BindingMode == MyBindingMode.OneWayToSource || BindingMode == MyBindingMode.TwoWay)
                        _mapToSource = true;
                }
            });
        }

        private void TargetPropertyChangedImp(PropertyChangedEventArgs e)
        {
            if (Deactivated)
                return;
            MyBinding.ExceptionHandler.Log(() =>
            {
                bool update = true;
                _predicate.DoIf(x => !x(), x => update = false);
                if (!update)
                    return;
                if (BindingMode == MyBindingMode.TwoWay || BindingMode == MyBindingMode.OneWayToSource)
                {
                    if (e.PropertyName != TargetPropertyName)
                        return;

                    BindingBehaviour.DoIf(x => x == MyBindingBehaviour.Map || x == MyBindingBehaviour.MapNInvoke, x => MapToSource());
                    BindingBehaviour.DoIf(x => x == MyBindingBehaviour.Invoke || x == MyBindingBehaviour.MapNInvoke, x => _whenTargetPropertyChanged?.Invoke());
                }
            });
        }

        private void MapToSource()
        {
            if (Deactivated)
                return;
            try
            {
                ToSource();
            }
            catch (Exception exp1)
            {
                try
                {
                    ToTarget();
                }
                catch (Exception exp2)
                {
                    MyBinding.ExceptionHandler.Log(() =>
                    {
                        throw exp2;
                    });
                }
                MyBinding.ExceptionHandler.Log(() =>
                {
                    throw exp1;
                });
            }
        }

        private void SourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Deactivated)
                return;
            MyBinding.ExceptionHandler.Log(() =>
            {
                if (e.PropertyName == SourcePropertyName)
                {
                    if (BindingType == MyBindingType.Automatic)
                    {
                        SourcePropertyChangedImp(e);
                    }
                    else if (BindingMode == MyBindingMode.TwoWay || BindingMode == MyBindingMode.OneWay)
                        _mapToTarget = true;
                }
            });
        }

        private void SourcePropertyChangedImp(PropertyChangedEventArgs e)
        {
            if (Deactivated)
                return;
            MyBinding.ExceptionHandler.Log(() =>
            {
                bool update = true;
                _predicate.DoIf(x => !x(), x => update = false);
                if (!update)
                    return;
                if (BindingMode == MyBindingMode.OneWayToSource)
                    return;
                if (e.PropertyName != SourcePropertyName)
                    return;
                BindingBehaviour.DoIf(x => x == MyBindingBehaviour.Map || x == MyBindingBehaviour.MapNInvoke, x => MapToTarget());
                BindingBehaviour.DoIf(x => x == MyBindingBehaviour.Invoke || x == MyBindingBehaviour.MapNInvoke, x => _whenSourcePropertyChanged?.Invoke());
            });
        }

        private void MapToTarget()
        {
            if (Deactivated)
                return;
            try
            {
                ToTarget();
            }
            catch (Exception exp1)
            {
                try
                {
                    ToSource();
                }
                catch (Exception exp2)
                {
                    MyBinding.ExceptionHandler.Log(() =>
                    {
                        throw exp2;
                    });
                }
                MyBinding.ExceptionHandler.Log(() =>
                {
                    throw exp1;
                });
            }
        }

        private void ToTarget()
        {
            if (Deactivated)
                return;
            MyBinding.ExceptionHandler.Log(() =>
            {
                if (SourceObject == null || TargetObject == null)
                    return;
                object value = ReflectionController.GetPropertyValue(SourceObject, SourcePropertyName);
                object targetValue = ReflectionController.GetPropertyValue(TargetObject, TargetPropertyName);
                Converter.Do(x => value = Converter.Convert(value, null, null, null));
                if (value.IsEqual(targetValue) && !IgnoreChangedWhileMapping && !IgnoreChangedPredicate)
                    return;
                ReflectionController.SetPropertyValue(TargetObject, TargetPropertyName, value);
            });
        }

        private void ToSource()
        {
            if (Deactivated)
                return;
            MyBinding.ExceptionHandler.Log(() =>
            {
                if (SourceObject == null || TargetObject == null)
                    return;
                object value = ReflectionController.GetPropertyValue(TargetObject, TargetPropertyName);
                object sourceValue = ReflectionController.GetPropertyValue(SourceObject, SourcePropertyName);
                Converter.Do(x => value = Converter.ConvertBack(value, null, null, null));
                if (value.IsEqual(sourceValue) && !IgnoreChangedWhileMapping && !IgnoreChangedPredicate)
                    return;
                ReflectionController.SetPropertyValue(SourceObject, SourcePropertyName, value);
            });
        }
        ~BindingHandler()
        {
            Deactivate();
        }
    }
}
