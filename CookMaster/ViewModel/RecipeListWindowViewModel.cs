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
        
        //private ObservableCollection<Recipe> _visibleRecipes;
        //public ObservableCollection<Recipe> VisibleRecipes
        //{
        //    get
        //    {
        //        return _visibleRecipes;
        //    }
        //    set
        //    {
        //        _visibleRecipes = value;
        //        OnPropertyChanged();
        //    }
        //}


        public RecipeListWindowViewModel(UserManager userManager, RecipeManager recipeManager)
        {
            UserManager = userManager;
            RecipeManager = recipeManager;
            //recipeManager = new RecipeManager(userManager);
            //MessageBox.Show(UserManager.LoggedIn);
            AllRecipes = new ObservableCollection<Recipe>(RecipeManager.Recipes);
            VisibleRecipes = new ObservableCollection<Recipe>(AllRecipes);
            
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
                //if(string.IsNullOrWhiteSpace(_search))
                //{
                //    RecipeManager.CategoryFilter("");
                //    OnPropertyChanged(nameof(Recipes));
                //}
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
        public RelayCommand FilterCommand => new RelayCommand(execute => FilterList());
        public RelayCommand ResetFilterCommand => new RelayCommand(execute => ResetFilter());


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
            if(AllRecipes.Contains(SelectedRecipe))
            {
                AllRecipes.Remove(SelectedRecipe);
            }
            if(VisibleRecipes.Contains(SelectedRecipe))
            {
                VisibleRecipes.Remove(SelectedRecipe);
            }

            OnPropertyChanged(nameof(VisibleRecipes));
        }
        public void Info()
        {
            MessageBox.Show("CookMaster is a platform for your recipes");
        }
        
        public void SortByNewest()
        {
            if(VisibleRecipes == null || VisibleRecipes.Count == 0)
            {
                return;
            }
            List<Recipe> sortedList = new List<Recipe>(VisibleRecipes);
            sortedList.Sort((x, y) => y.Date.CompareTo(x.Date)); //om 2 är nyare, lägg de före 1
            VisibleRecipes.Clear();
            foreach (var recipe in sortedList)
            {
                VisibleRecipes.Add(recipe);
            }
        }
        public void FilterList()
        {
            if (string.IsNullOrWhiteSpace(Search))
            {
                VisibleRecipes.Clear();
                foreach (var recipe in AllRecipes)
                {
                    VisibleRecipes.Add(recipe);
                }
                return;
            }
            string filter = Search.ToLower();
            VisibleRecipes.Clear();
            foreach (var recipe in AllRecipes)
            {
                if (recipe.Category != null && recipe.Category.ToLower().Contains(filter))
                {
                    VisibleRecipes.Add(recipe);
                }
            }
            
        }
        public void ResetFilter()
        {
            Search = string.Empty;
            FilterList();
            
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
