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
        public RecipeManager RecipeManager { get; }
        public ObservableCollection<Recipe>? Recipes { get; }
        public ObservableCollection<Recipe>? VisibleRecipes { get; }

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
        public bool IsCopy { get; set; } = false;

        public RecipeDetailWindowViewModel(UserManager userManager, RecipeManager recipeManager, Recipe selectedRecipe)
        {
            UserManager = userManager;
            RecipeManager = recipeManager;
            SelectedRecipe = selectedRecipe;
            Recipes = RecipeManager.Recipes;
            
            



            if (IsCopy == true)
            {
                Edit = true;
            }
            if(SelectedRecipe != null)
            {
                Title = SelectedRecipe.Title;
                Ingredients = SelectedRecipe.Ingredients;
                Instructions = SelectedRecipe.Instructions;
                Category = SelectedRecipe.Category;
                Date = SelectedRecipe.Date;
            }

        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        private string _ingredients;
        public string Ingredients
        {
            get { return _ingredients; }
            set
            {
                _ingredients = value;
                OnPropertyChanged();
            }
        }
        private string _instructions;
        public string Instructions
        {
            get { return _instructions; }
            set
            {
                _instructions = value;
                OnPropertyChanged();
            }
        }
        private string _category;
        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged();
            }
        }
        private DateTime _date = DateTime.Now;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand EditCommand => new RelayCommand(execute => EditRecipe(), canExecute => !Edit);
        public RelayCommand SaveCommand => new RelayCommand(execute => SaveRecipe());
        public RelayCommand CopyCommand => new RelayCommand(execute => CopyRecipe(), canExecute => !IsCopy);
        public void EditRecipe()
        {
            Edit = true;
        }
        
        public void SaveRecipe()
        {
            try
            {
                if (RecipeManager == null)
                {
                    MessageBox.Show("Cannot find RecipeManager");
                    return;
                }
                if (SelectedRecipe == null)
                {
                    MessageBox.Show("No recipe selected to save.");
                    return;
                }

                if (IsCopy)
                {
                    SelectedRecipe.Date = DateTime.Now;
                    RecipeManager.Recipes?.Add(SelectedRecipe);
                    
                    MessageBox.Show("Recipe copied and saved!");
                }
                else
                {
                    SelectedRecipe.Title = Title;
                    SelectedRecipe.Ingredients = Ingredients;
                    SelectedRecipe.Instructions = Instructions;
                    SelectedRecipe.Category = Category;
                    SelectedRecipe.Date = DateTime.Now;

                    MessageBox.Show("Recipe saved!");
                    
                }
                SelectedRecipe.Date = DateTime.Now;
                RecipeListWindow recipeListWindow = new RecipeListWindow();
                recipeListWindow.Show();
                foreach (Window window in Application.Current.Windows)
                {
                    if (window != recipeListWindow)
                    {
                        window.Close();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while saving recipe: " + e.Message);
            }
        }
        
        public void CopyRecipe()
        {
            try
            {
                if (SelectedRecipe == null)
                {
                    MessageBox.Show("No recipe selected to copy.");
                    return;
                }

                IsCopy = true;
                Edit = true;

                SelectedRecipe = new Recipe
                {
                    Title = SelectedRecipe.Title + " (Copied recipe!)",
                    Ingredients = SelectedRecipe.Ingredients,
                    Instructions = SelectedRecipe.Instructions,
                    Category = SelectedRecipe.Category,
                    Date = DateTime.Now,
                    CreatedBy = UserManager.LoggedIn
                };
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while copying recipe: " + e.Message);
            }


        }

    }
}
