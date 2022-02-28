using Extensions;
using Promax.Core;
using Promax.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Binding;

namespace Promax.Business
{
    public class RecipeController : INotifyPropertyChanged
    {
        private IVariables _recipeScope;
        private IRecipeContentManager _recipeContentManager;
        private MyBinding _variableBindings;
        private MyBinding _saveBindings;
        private MyBinding _internalBindings;

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region FieldsByAutoPropCreator
        private Recipe _selectedRecipe;
        #endregion
        #region PropertiesByAutoPropCreator
        public Recipe SelectedRecipe
        {
            get
            {
                return _selectedRecipe;
            }
            private set
            {
                bool changed = false;
                if (!_selectedRecipe.IsEqual(value))
                    changed = true;
                _selectedRecipe = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(SelectedRecipe));
                }
            }
        }
        #endregion

        public RecipeController(IVariables recipeScope, IRecipeContentManager recipeContentManager)
        {
            _recipeScope = recipeScope;
            _recipeContentManager = recipeContentManager;
            _internalBindings = new MyBinding();
            _internalBindings.CreateBinding().Behaviour(MyBindingBehaviour.Invoke).Source(this).SourceProperty(nameof(SelectedRecipe)).WhenSourcePropertyChanged(() => AdjustSaveBinding()).Mode(MyBindingMode.OneWay);
        }
        public void SelectRecipe(Recipe recipe)
        {
            SelectedRecipe = recipe;
        }
        public void SendRecipe(Recipe recipe)
        {
            AdjustVariablesBinding(recipe);
            _variableBindings.Do(x => x.ClearBindings());
            _variableBindings = null;
        }
        public void SendSelectedRecipe()
        {
            SelectedRecipe.Do(x => SendRecipe(x));
        }
        public void SelectNSendRecipe(Recipe recipe)
        {
            SelectRecipe(recipe);
            SendRecipe(recipe);
        }

        private void AdjustSaveBinding()
        {
            SelectedRecipe.Do(x =>
            {
                _saveBindings = new MyBinding();
                foreach (var recipeContent in x.RecipeContents)
                {
                    _saveBindings.CreateBinding().Behaviour(MyBindingBehaviour.Invoke).Source(recipeContent).SourceProperty(nameof(RecipeContent.Quantity)).WhenSourcePropertyChanged(() =>
                    {
                        _recipeContentManager.Update(recipeContent);
                    });
                }
            }, () => _saveBindings.DoReturn(y => y.ClearBindings()).Do(y => _saveBindings = null));
        }
        private void AdjustVariablesBinding(Recipe recipe)
        {
            //_variableBindings = new MyBinding();
            //foreach (var recipeContent in recipe.RecipeContents)
            //{
            //    _recipeScope.GetVariable(recipeContent.Silo.Name, recipe.SystemType.ToString() + "İstenen").Do(x =>
            //    {
            //        _variableBindings.CreateBinding().Source(recipeContent).SourceProperty(nameof(RecipeContent.Quantity)).
            //        Target(x).TargetProperty(nameof(IRemoteVariable.WriteValue)).
            //        Mode(MyBindingMode.OneWay).WhenSourcePropertyChanged(() =>
            //        {
            //            x.Write();
            //        });
            //    });
            //}
            //_variableBindings.InitialMapping();
        }
    }
}
