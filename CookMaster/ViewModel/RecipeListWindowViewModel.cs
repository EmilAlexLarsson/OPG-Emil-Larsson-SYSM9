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
        public ObservableCollection<Recipe> AllRecipes { get; set; }
        public ObservableCollection<Recipe> VisibleRecipes { get; set; }




        public RecipeListWindowViewModel(UserManager userManager, RecipeManager recipeManager)
        {
            UserManager = userManager;
            RecipeManager = recipeManager;

            //AllRecipes = new ObservableCollection<Recipe>(RecipeManager?.Recipes);
            //VisibleRecipes = new ObservableCollection<Recipe>(AllRecipes);
            AllRecipes = RecipeManager?.Recipes ?? new ObservableCollection<Recipe>();
            VisibleRecipes = AllRecipes;

        }
        public ObservableCollection<Recipe> Recipes
        {
            get { return RecipeManager?.Recipes; }
        }
        private Recipe? _selectedRecipe;
        public Recipe? SelectedRecipe
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
        public RelayCommand OpenAddRecipeCommand => new RelayCommand(execute => AddRecipe());
        public RelayCommand SignOutCommand => new RelayCommand(execute => SignOut());
        public RelayCommand RemoveCommand => new RelayCommand(execute => RemoveRecipe());
        public RelayCommand DetailsCommand => new RelayCommand(execute => Details());
        public RelayCommand OpenUserCommand => new RelayCommand(execute => OpenUser());
        public RelayCommand InfoCommand => new RelayCommand(execute => Info());
        public RelayCommand SortCommand => new RelayCommand(execute => SortByNewest());
        public RelayCommand FilterCommand => new RelayCommand(execute => FilterList());
        public RelayCommand ResetFilterCommand => new RelayCommand(execute => ResetFilter());



        public void AddRecipe()
        {

            AddRecipeWindow addRecipeWindow = new AddRecipeWindow();
            addRecipeWindow.ShowDialog();
        }

        public void OpenUser()
        {
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow();
            userDetailsWindow.ShowDialog();
        }

        public void Details ()
        {
            if (SelectedRecipe == null)
            {
                MessageBox.Show("You have to select a recipe to see details!");
                return;
            }

            RecipeDetailWindow recipeDetailWindow = new RecipeDetailWindow(SelectedRecipe);
            recipeDetailWindow.ShowDialog();
            
        }
        public void RemoveRecipe()
        {
            if (SelectedRecipe == null)
            {
                MessageBox.Show("You have to select a recipe to remove!");
                return;
            }
            try
            {
                RecipeManager?.RemoveRecipe(SelectedRecipe);
                if (AllRecipes.Contains(SelectedRecipe))
                {
                    AllRecipes.Remove(SelectedRecipe);
                }
                if (VisibleRecipes.Contains(SelectedRecipe))
                {
                    VisibleRecipes.Remove(SelectedRecipe);
                }

                OnPropertyChanged(nameof(VisibleRecipes));
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while removing recipe: " + e.Message);
            }
        }
        public void Info()
        {
            MessageBox.Show("CookMaster is a platform for your recipes");
        }

        public void SortByNewest()
        {

            VisibleRecipes = RecipeManager.SortByNewest(VisibleRecipes);
            OnPropertyChanged(nameof(VisibleRecipes));
        }
        public void FilterList()
        {

            VisibleRecipes = RecipeManager.FilterRecipes(Search, AllRecipes);
            OnPropertyChanged(nameof(VisibleRecipes));

        }
        public void ResetFilter()
        {
            Search = string.Empty;
            FilterList();

        }

        public void SignOut()
        {
            try
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
            catch (Exception e)
            {
                MessageBox.Show("Error while signing out: " + e.Message);
            }

        }
    }
}
