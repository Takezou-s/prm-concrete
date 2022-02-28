using Promax.Business.Abstract;
using Promax.Business.Mappers;
using Promax.Core;
using Promax.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Promax.UI.Windows
{
    /// <summary>
    /// ReçeteKartı.xaml etkileşim mantığı
    /// </summary>
    public partial class ReçeteKartı : Window
    {
        public static ReçeteKartı CreateNew()
        {
            return new ReçeteKartı() { Recipe = new Recipe() };
        }

        public static ReçeteKartı Edit(Recipe recipe)
        {
            var a = new ReçeteKartı();
            a.Recipe = a.Mapper.Map<Recipe>(a.Mapper.Map<RecipeDTO>(recipe));
            a.oldRecipe = recipe;
            a.Editing = true;
            return a;
        }
        private Recipe oldRecipe;
        private bool Editing { get; set; }
        public Recipe Recipe { get; set; }
        public IComplexRecipeManager RecipeManager { get => Infrastructure.Main.GetRecipeManager(); }
        public IBeeMapper Mapper { get => Infrastructure.Main.GetMapper(); }
        private ReçeteKartı()
        {
            InitializeComponent();
        }

        private void TamamButon_Click(object sender, RoutedEventArgs e)
        {
            Infrastructure.Main.HandleExceptions(() =>
            {
                if (Editing)
                {
                    RecipeManager.Update(Recipe, oldRecipe);
                }
                else
                {
                    RecipeManager.Add(Recipe);
                }
                this.Close();
            });
        }

        private void İptalButon_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
