using CookMaster.Model;
using CookMaster.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CookMaster.Manager
{
    public class RecipeManager : ViewModelBase
    {
        public UserManager UserManager { get; }
        
        public ObservableCollection<Recipe> Recipes { get; private set; }


        public RecipeManager(UserManager userManager)
        {
            UserManager = userManager;
            Recipes = new ObservableCollection<Recipe>();

            DefaultRecipes();
        }

        public void DefaultRecipes()
        {


            Recipes.Add(new Recipe
            {
                Title = "Köttbullar med makaroner",
                Ingredients = "Köttbullar och makaroner",
                Instructions = "Stek köttbullarna och koka makaronerna.",
                Category = "Huvudrätt",
                Date = DateTime.Now,
                CreatedBy = UserManager.LoggedIn
            });
            UserManager.LoggedIn.Recipes = Recipes;
        }
        //public void testUser()
        //{
        //    if (UserManager.LoggedIn != null)
        //    {
        //        Console.WriteLine("Logged in user: " + UserManager.LoggedIn.Username);
        //    }
        //    else
        //    {
        //        Console.WriteLine("No user is currently logged in.");
        //    }
        //}

        public void AddRecipe(Recipe recipe)
        {
            UserManager.LoggedIn?.Recipes.Add(recipe);
        }

        public void RemoveRecipe(Recipe recipe)
        {
            UserManager.LoggedIn?.Recipes.Remove(recipe);
        }

        public void UpdateRecipe (Recipe recipe)
        {
            
        }
        
    }
}
