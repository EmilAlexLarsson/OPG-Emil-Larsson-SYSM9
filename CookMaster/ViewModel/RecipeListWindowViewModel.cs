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
        private ObservableCollection<Recipe> _recipes;
        public ObservableCollection<Recipe> Recipes
        {
            get { return UserManager.LoggedIn.Recipes; }
            set
            {
                _recipes = value;
                OnPropertyChanged();
            }
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
        //public RelayCommand LogInCommand => new RelayCommand(execute => );
        public RelayCommand OpenAddRecipeCommand => new RelayCommand(execute => AddRecipe());

        public void AddRecipe()
        {
            
            AddRecipeWindow addRecipeWindow = new AddRecipeWindow();
            addRecipeWindow.Show();
        }

        public void OpenUser()
        {
            //Öppnar userdetailwindow
        }

        public void Details ()
        {
            //Öppnar details fönster
        }
        public void RemoveRecipe()
        {
            //Ta bort markerat recept från listan
        }

        public void SignOut()
        {

        }
    }
}
