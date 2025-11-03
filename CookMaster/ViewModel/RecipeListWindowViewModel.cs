using CookMaster.Manager;
using CookMaster.Model;
using CookMaster.MVVM;
using CookMaster.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CookMaster.ViewModel
{
    public class RecipeListWindowViewModel : ViewModelBase
    {

        public RecipeManager RecipeManager { get; }
        public UserManager UserManager { get; }


        public RecipeListWindowViewModel(UserManager userManager, RecipeManager recipeManager)
        {
            UserManager = userManager;
            RecipeManager = recipeManager;
            //recipeManager = new RecipeManager(userManager);
            //MessageBox.Show(UserManager.LoggedIn);
            
        }
        public ObservableCollection<Recipe> Recipes
        {
            get { return RecipeManager.Recipes; }
        }
        private Recipe _selectedRecipe;
        public Recipe SelectedRecipe
        {
            get { return _selectedRecipe; }
            set
            {
                _selectedRecipe = value;
                OnPropertyChanged();
            }
        }
        private string _search;
        public string Search
        {
            get
            {
                return _search;
            }
            set
            {
                _search = value;
                OnPropertyChanged();
            }
        }

        
        //public RelayCommand LogInCommand => new RelayCommand(execute => );
        public RelayCommand OpenAddRecipeCommand => new RelayCommand(execute => AddRecipe());
        public RelayCommand SignOutCommand => new RelayCommand(execute => SignOut());
        public RelayCommand RemoveCommand => new RelayCommand(execute => RemoveRecipe());
        public RelayCommand DetailsCommand => new RelayCommand(execute => Details());
        public RelayCommand OpenUserCommand => new RelayCommand(execute => OpenUser());
        public RelayCommand InfoCommand => new RelayCommand(execute => Info());
        public RelayCommand SortCommand => new RelayCommand(execute => SortByNewest());


        //public void ViewAllRecipes()
        //{
        //    if(UserManager.LoggedIn is AdminUser)
        //    {
        //        //adminuser kan se alla recepten

        //    }
        //}
        public void AddRecipe()
        {

            AddRecipeWindow addRecipeWindow = new AddRecipeWindow(RecipeManager);
            addRecipeWindow.Show();
        }

        public void OpenUser()
        {
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow(RecipeManager);
            userDetailsWindow.Show();
        }

        public void Details ()
        {
            if (SelectedRecipe == null)
            {
                MessageBox.Show("You have to select a recipe to see details!");
            }
            else
            {
                RecipeDetailWindow recipeDetailWindow = new RecipeDetailWindow(RecipeManager, SelectedRecipe);
                recipeDetailWindow.Show();
            }
        }
        public void RemoveRecipe()
        {
            if (SelectedRecipe == null)
            {
                MessageBox.Show("You have to select a recipe to remove!");
                return;
            }
            
            RecipeManager.RemoveRecipe(SelectedRecipe);
            OnPropertyChanged(nameof(Recipes));
        }
        public void Info()
        {
            MessageBox.Show("CookMaster is a platform for your recipes");
        }
        public void SortByNewest()
        {
            RecipeManager.SortByNewest();
            OnPropertyChanged(nameof(Recipes));

        }

        public void SignOut()
        {
            UserManager.LoggedIn = null;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            foreach (Window window in Application.Current.Windows)
            {
                if (window != mainWindow)
                {
                    window.Close();
                }
            }
            
        }
    }
}
