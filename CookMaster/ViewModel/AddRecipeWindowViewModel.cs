using CookMaster.Manager;
using CookMaster.Model;
using CookMaster.MVVM;
using CookMaster.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CookMaster.ViewModel
{
    public class AddRecipeWindowViewModel : ViewModelBase
    {
        public UserManager UserManager { get; }
        public RecipeManager RecipeManager { get; }
        public AddRecipeWindowViewModel(UserManager userManager, RecipeManager recipeManager)
        {
            UserManager = (UserManager)Application.Current.Resources["UserManager"];

            RecipeManager = recipeManager;
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
        public DateTime Date { get; set; } = DateTime.Now;

        


        public RelayCommand AddRecipeCommand => new RelayCommand(execute => AddRecipe(), canExecute => !string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Ingredients) && !string.IsNullOrWhiteSpace(Instructions) && !string.IsNullOrWhiteSpace(Category));
        public void AddRecipe()
        {
            try
            {
                if (RecipeManager.AddRecipe(Title, Ingredients, Instructions, Category, UserManager?.LoggedIn))
                {
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
                else
                {
                    MessageBox.Show("Fill in the empty sections or recipe title might already exist");
                    return;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while adding recipe: " + e.Message);
            }
            
        }
    }
}
