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
    public class RecipeDetailWindowViewModel :ViewModelBase
    {
        public UserManager UserManager { get; }
        public RecipeManager RecipeManager { get; set; }
        public ObservableCollection<Recipe>? Recipes { get; }

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
        private bool _edit = false;
        public bool Edit
        {
            get { return _edit; }
            set
            {
                _edit = value;
                OnPropertyChanged();
            }
        }
        public RecipeDetailWindowViewModel(UserManager userManager, RecipeManager recipeManager, Recipe selectedRecipe)
        {
            UserManager = userManager;
            RecipeManager = recipeManager;
            Recipes = RecipeManager.Recipes;
            SelectedRecipe = selectedRecipe;

        }
        //private string _title;
        //public string Title
        //{
        //    get { return _title; }
        //    set
        //    {
        //        _title = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private string _ingredients;
        //public string Ingredients
        //{
        //    get { return _ingredients; }
        //    set
        //    {
        //        _ingredients = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private string _instructions;
        //public string Instructions
        //{
        //    get { return _instructions; }
        //    set
        //    {
        //        _instructions = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private string _category;
        //public string Category
        //{
        //    get { return _category; }
        //    set
        //    {
        //        _category = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private DateTime _date = DateTime.Now;
        //public DateTime Date
        //{
        //    get { return _date; }
        //    set
        //    {
        //        _date = value;
        //        OnPropertyChanged();
        //    }
        //}
        public RelayCommand EditCommand => new RelayCommand(execute => EditRecipe());
        public RelayCommand SaveCommand => new RelayCommand(execute => SaveRecipe());
        public void EditRecipe()
        {
            Edit = true;
        }
        public void SaveRecipe()
        {
            MessageBox.Show("Recipe saved!");
            SelectedRecipe.Date = DateTime.Now;
            RecipeListWindow recipeListWindow = new RecipeListWindow(RecipeManager);
            recipeListWindow.Show();
            foreach (Window window in Application.Current.Windows)
            {
                if (window != recipeListWindow)
                {
                    window.Close();
                }
            }
        }

    }
}
