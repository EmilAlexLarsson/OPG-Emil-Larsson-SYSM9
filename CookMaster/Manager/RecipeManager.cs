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
        
        public ObservableCollection<Recipe>? Recipes
        {
            get { return UserManager.LoggedIn?.Recipes; }
        }


        public RecipeManager(UserManager userManager)
        {
            UserManager = userManager;
            

            DefaultRecipes();
        }

        public void DefaultRecipes()
        {

            if (UserManager.LoggedIn == null)
            {
                return;
            }

            if (UserManager.LoggedIn.Recipes == null)
            {
                UserManager.LoggedIn.Recipes = new ObservableCollection<Recipe>();
            }
            if (UserManager.LoggedIn.Recipes.Count ==0)
            {
                UserManager.LoggedIn.Recipes.Add(new Recipe
                {
                    Title = "Köttbullar med makaroner",
                    Ingredients = "Köttbullar och makaroner",
                    Instructions = "Stek köttbullarna och koka makaronerna.",
                    Category = "Huvudrätt",
                    Date = new DateTime(2025, 10, 31),
                    CreatedBy = new User { Username = "CookMaster" }
                });
                UserManager.LoggedIn.Recipes.Add(new Recipe
                {
                    Title = "Spaghetti bolognese",
                    Ingredients = "Spaghetti, köttfärs, tomatsås",
                    Instructions = "Koka spaghetti och blanda med köttfärs och tomatsås.",
                    Category = "Huvudrätt",
                    Date = new DateTime(2025, 10, 31),
                    CreatedBy = new User { Username = "CookMaster" }
                });
                UserManager.LoggedIn.Recipes.Add(new Recipe
                {
                    Title = "Äppelpaj",
                    Ingredients = "Äpplen, socker, kanel, pajdeg",
                    Instructions = "Skala och skiva äpplena. Blanda med socker och kanel. Fyll pajdegen med äppelfyllningen och grädda.",
                    Category = "Efterrätt",
                    Date = new DateTime(2025, 10, 31),
                    CreatedBy = new User { Username = "CookMaster" }
                });
            }
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

        public bool AddRecipe(string title, string ingredients, string instructions, string category, User createdBy)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(ingredients) || string.IsNullOrEmpty(instructions) || string.IsNullOrEmpty(category))
            {
                return false;
            }

            Recipe newRecipe = new Recipe
            {
                Title = title,
                Ingredients = ingredients,
                Instructions = instructions,
                Category = category,
                Date = DateTime.Now,
                CreatedBy = createdBy
            };

            Recipes?.Add(newRecipe);
            return true;
        }
        //public void AddRecipe(Recipe recipe)
        //{
        //    Recipes?.Add(recipe);
        //}

        public void RemoveRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                return;
            }
            Recipes?.Remove(recipe);
        }
        public Recipe CopyRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                return null;
            }
            Recipe copiedRecipe = new Recipe
            {
                Title = recipe.Title,
                Ingredients = recipe.Ingredients,
                Instructions = recipe.Instructions,
                Category = recipe.Category,
                Date = DateTime.Now,
                CreatedBy = UserManager.LoggedIn
            };
            Recipes?.Add(copiedRecipe);
            return copiedRecipe;
        }

        public void UpdateRecipe (Recipe recipe)
        {
            if (recipe != null)
            {
                
            }
        }
        
    }
}
