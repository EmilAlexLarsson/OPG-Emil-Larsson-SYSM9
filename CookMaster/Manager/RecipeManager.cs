using CookMaster.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Manager
{
    public class RecipeManager
    {
        public UserManager UserManager { get; }
        public ObservableCollection<Recipe> Recipes { get; }

        public RecipeManager()
        {
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
                //CreatedBy = UserManager.LoggedIn
            });

        }

        public void AddRecipe(Recipe recipe)
        {
            Recipes.Add(recipe);
        }

        public void RemoveRecipe(Recipe recipe)
        {
            Recipes.Remove(recipe);
        }

        public void UpdateRecipe (Recipe recipe)
        {
            
        }
        //Currentuser.recipe???
    }
}
