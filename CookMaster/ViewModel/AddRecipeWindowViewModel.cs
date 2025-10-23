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
        public RecipeManager RecipeManager { get; set; }
        public AddRecipeWindowViewModel(UserManager userManager, RecipeManager recipeManager)
        {
            UserManager = userManager;
            
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

        


        public RelayCommand AddRecipeCommand => new RelayCommand(execute => AddRecipe());
        public void AddRecipe()
        {
            if(!string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Ingredients) && !string.IsNullOrWhiteSpace(Instructions) && !string.IsNullOrWhiteSpace(Category) )
            {
                UserManager.LoggedIn.Recipes.Add(new Recipe
                {
                   Title = Title,
                   Ingredients = Ingredients, 
                   Instructions = Instructions,
                   Category = Category,
                   Date = DateTime.Now,
                   CreatedBy = UserManager.LoggedIn

                });
                if (Application.Current.Windows.Count > 1)
                {
                    Application.Current.Windows[1].Close();
                }
                else
                {
                    RecipeListWindow recipeListWindow = new RecipeListWindow();
                    recipeListWindow.Show();
                    Application.Current.Windows[0].Close();


                }
            }
            else
            {
                MessageBox.Show("Fill in the empty sections");
                return;
            }
            //Om RecipeListWindow stängs innan AddRecipeWindow stängs så kraschar det
            
            //if( Application.Current.Windows.Count > 1)
            //{
            //    Application.Current.Windows[1].Close();
            //}
            //else
            //{
            //    RecipeListWindow recipeListWindow = new RecipeListWindow();
            //    recipeListWindow.Show();
            //    Application.Current.Windows[0].Close();
                
            //}
        }
    }
}
