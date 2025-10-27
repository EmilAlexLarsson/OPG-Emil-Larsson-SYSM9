using CookMaster.Manager;
using CookMaster.Model;
using CookMaster.ViewModel;
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

namespace CookMaster.View
{
    /// <summary>
    /// Interaction logic for RecipeDetailWindow.xaml
    /// </summary>
    public partial class RecipeDetailWindow : Window
    {
        public RecipeDetailWindow(RecipeManager recipeManager, Recipe selectedRecipe)
        {
            InitializeComponent();
            UserManager userManager = (UserManager)Application.Current.Resources["UserManager"];
            //RecipeManager recipeManager = (RecipeManager)Application.Current.Resources["RecipeManager"];
            RecipeDetailWindowViewModel recipeDetailViewModel = new RecipeDetailWindowViewModel(userManager, recipeManager, selectedRecipe);
            DataContext = recipeDetailViewModel;
        }
    }
}
