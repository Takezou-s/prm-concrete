using Promax.Business;
using Promax.Core;
using Promax.Entities;
using System.Windows;

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
        public IRecipeManager RecipeManager { get => Infrastructure.Main.RecipeManager; }
        public IBeeMapper Mapper { get => Infrastructure.Main.Mapper; }
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
                    RecipeManager.Update(Recipe);
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
