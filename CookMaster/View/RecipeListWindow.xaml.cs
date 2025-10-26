using CookMaster.Manager;
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
    /// Interaction logic for RecipeListWindow.xaml
    /// </summary>
    public partial class RecipeListWindow : Window
    {
        public RecipeListWindow(RecipeManager recipeManager)
        {
            InitializeComponent();
            UserManager userManager = (UserManager)Application.Current.Resources["UserManager"];
            //RecipeManager recipeManager = (RecipeManager)Application.Current.Resources["RecipeManager"];
            RecipeListWindowViewModel recipeListViewModel = new RecipeListWindowViewModel(userManager, recipeManager);
            DataContext = recipeListViewModel;
        }
    }
}
