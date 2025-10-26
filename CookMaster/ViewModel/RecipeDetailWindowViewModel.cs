using CookMaster.Manager;
using CookMaster.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.ViewModel
{
    public class RecipeDetailWindowViewModel :ViewModelBase
    {
        public UserManager UserManager { get; }
        public RecipeManager RecipeManager { get; set; }
        public RecipeDetailWindowViewModel(UserManager userManager, RecipeManager recipeManager)
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
        public void EditRecipe()
        {
            
        }

    }
}
