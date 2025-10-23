using CookMaster.Model;
using CookMaster.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Manager
{
    public class RecipeManager : ViewModelBase
    {
        public UserManager UserManager { get; }
        private ObservableCollection<Recipe> _recipes;
        public ObservableCollection<Recipe> Recipes
        {
            get { return _recipes; }
            set
            {
                _recipes = value;
                OnPropertyChanged();
            }
        }

        public RecipeManager(UserManager userManager)
        {
            UserManager = userManager;
            _recipes = new ObservableCollection<Recipe>();

            
        }

        public void DefaultRecipes()
        {
            
            //UserManager.LoggedIn.Recipes.Add(new Recipe
            //{
            //    Title = "Pasta med tomatsås",
            //    Ingredients = "Pasta, tomatsås, olivolja, vitlök, salt, peppar",
            //    Instructions = "Koka pastan enligt anvisningarna på förpackningen. Värm tomatsåsen i en kastrull med olivolja och vitlök. Blanda pastan med tomatsåsen och krydda med salt och peppar.",
            //    Category = "Huvudrätt",
            //    Date = DateTime.Now,
            //    CreatedBy = UserManager.LoggedIn
            //});
            //Recipes.Add(new Recipe
            //{
            //    Title = "Köttbullar med makaroner",
            //    Ingredients = "Köttbullar och makaroner",
            //    Instructions = "Stek köttbullarna och koka makaronerna.",
            //    Category = "Huvudrätt",
            //    Date = DateTime.Now,
            //    CreatedBy = UserManager.LoggedIn
            //});
            //UserManager.LoggedIn.Recipes = Recipes;
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
            Recipes.Add(recipe);
        }

        public void RemoveRecipe(Recipe recipe)
        {
            Recipes.Remove(recipe);
        }

        public void UpdateRecipe (Recipe recipe)
        {
            
        }
        
    }
}
