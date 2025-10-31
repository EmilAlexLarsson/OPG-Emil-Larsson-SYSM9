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
        //public ObservableCollection<Recipe> showAllRecipes { get; set; }
        public ObservableCollection<Recipe>? Recipes
        {
            get 
            { 
                if(UserManager.LoggedIn is AdminUser)
                {
                    var showAllRecipes = new ObservableCollection<Recipe>();

                    foreach(var user in UserManager.Users)
                    {
                        foreach(Recipe recipe in user.Recipes)
                        {
                            showAllRecipes.Add(recipe);
                        }
                    }
                    return showAllRecipes;
                }
                return UserManager.LoggedIn?.Recipes; 
            }
        }


        public RecipeManager(UserManager userManager)
        {
            UserManager = userManager;
            
            User defaultUser = null;
            foreach(User user in UserManager.Users)
            {
                if(user.Username == "user")
                {
                    defaultUser = user;
                    break;
                }
            }
            if(defaultUser != null)
            {
                if(defaultUser.Recipes == null || defaultUser.Recipes.Count == 0)
                {
                    DefaultRecipes(defaultUser);
                }
            }
            
        }

        public void DefaultRecipes(User defaultUser)
        {
            if (defaultUser == null)
            {
                return;
            }
            if(defaultUser.Recipes == null)
            {
                defaultUser.Recipes = new ObservableCollection<Recipe>();
            }
            if(defaultUser.Recipes.Count > 0)
            {
                return;
            }
            defaultUser.Recipes.Add(new Recipe
            {
                Title = "Köttbullar med makaroner",
                Ingredients = "Köttbullar och makaroner",
                Instructions = "Stek köttbullarna och koka makaronerna.",
                Category = "Huvudrätt",
                Date = new DateTime(2025, 10, 31),
                CreatedBy = defaultUser
            });
            defaultUser.Recipes.Add(new Recipe
            {
                Title = "Äppelpaj",
                Ingredients = "Äpplen, socker, kanel, pajdeg",
                Instructions = "Skala och skiva äpplena. Blanda med socker och kanel. Fyll pajdegen med äppelfyllningen och grädda.",
                Category = "Efterrätt",
                Date = new DateTime(2025, 10, 31),
                CreatedBy = defaultUser
            });

            //User CookMaster = new User { Username = "Cookmaster" };
            //if (UserManager.LoggedIn == null)
            //{
            //    return;
            //}

            //if (UserManager.LoggedIn.Recipes == null)
            //{
            //    UserManager.LoggedIn.Recipes = new ObservableCollection<Recipe>();
            //}
            //if (UserManager.LoggedIn.Recipes.Any())
            //{
            //    return;
            //}

            //AddRecipe(
            //    "Köttbullar med makaroner",
            //    "Köttbullar och makaroner",
            //    "Stek köttbullarna och koka makaronerna.",
            //    "Huvudrätt",
            //    CookMaster
            //    );
            //AddRecipe(
            //    "Spaghetti bolognese",
            //    "Spaghetti, köttfärs, tomatsås",
            //    "Koka spaghetti och blanda med köttfärs och tomatsås.",
            //    "Huvudrätt",
            //    CookMaster
            //);
            //AddRecipe(
            //    "Äppelpaj",
            //    "Äpplen, socker, kanel, pajdeg",
            //    "Skala och skiva äpplena. Blanda med socker och kanel. Fyll pajdegen med äppelfyllningen och grädda.",
            //    "Efterrätt",
            //    CookMaster
            //);






            //Recipes.Add(new Recipe
            //{
            //    Title = "Köttbullar med makaroner",
            //    Ingredients = "Köttbullar och makaroner",
            //    Instructions = "Stek köttbullarna och koka makaronerna.",
            //    Category = "Huvudrätt",
            //    Date = new DateTime(2025, 10, 31),
            //    CreatedBy = new User { Username = "CookMaster" }
            //});
            //Recipes.Add(new Recipe
            //{
            //    Title = "Spaghetti bolognese",
            //    Ingredients = "Spaghetti, köttfärs, tomatsås",
            //    Instructions = "Koka spaghetti och blanda med köttfärs och tomatsås.",
            //    Category = "Huvudrätt",
            //    Date = new DateTime(2025, 10, 31),
            //    CreatedBy = new User { Username = "CookMaster" }
            //});
            //Recipes.Add(new Recipe
            //{
            //    Title = "Äppelpaj",
            //    Ingredients = "Äpplen, socker, kanel, pajdeg",
            //    Instructions = "Skala och skiva äpplena. Blanda med socker och kanel. Fyll pajdegen med äppelfyllningen och grädda.",
            //    Category = "Efterrätt",
            //    Date = new DateTime(2025, 10, 31),
            //    CreatedBy = new User { Username = "CookMaster" }
            //});

        }
        
        public bool AddRecipe(string title, string ingredients, string instructions, string category, User createdBy)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(ingredients) || string.IsNullOrEmpty(instructions) || string.IsNullOrEmpty(category))
            {
                return false;
            }
            if (Recipes == null)
            {
                return false;
            }
            foreach (Recipe existingRecipe in Recipes)
            {
                if(existingRecipe.Title == title)
                {
                    return false;
                }
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
            if(UserManager.LoggedIn is AdminUser)
            {
                //Recipes.Remove(recipe);
                //showAllRecipes.Remove(recipe);

                foreach (var user in UserManager.Users)
                {
                    if (user.Recipes.Contains(recipe))
                    {
                        user.Recipes.Remove(recipe);
                        //showAllRecipes.Remove(recipe);
                        return;
                    }
                }
                return;
            }
            Recipes?.Remove(recipe);
            //UserManager.LoggedIn.Recipes.Remove(recipe);
        }
        //public ObservableCollection<Recipe> GetAllRecipes()
        //{
            
        //        if (Recipes == null)
        //        {
        //            return null;
        //        }
        //        return new ObservableCollection<Recipe>(Recipes);
            
        //}
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

        //public void UpdateRecipe (Recipe updateRecipe)
        //{
        //    if (updateRecipe == null)
        //    {
        //        return;
        //    }
        //    if(UserManager.LoggedIn is AdminUser)
        //    {
        //        foreach (var user in UserManager.Users)
        //        {
        //            foreach(var recipe in user.Recipes)
        //            {
        //                if(recipe.Title == updateRecipe.Title)
        //                {
        //                    recipe.Ingredients = updateRecipe.Ingredients;
        //                    recipe.Instructions = updateRecipe.Instructions;
        //                    recipe.Category = updateRecipe.Category;
        //                    recipe.Date = DateTime.Now;
        //                    return;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        foreach(var recipe in UserManager.LoggedIn.Recipes)
        //        {
        //            if (recipe.Title == updateRecipe.Title)
        //            {
        //                recipe.Ingredients = updateRecipe.Ingredients;
        //                recipe.Instructions = updateRecipe.Instructions;
        //                recipe.Category = updateRecipe.Category;
        //                recipe.Date = DateTime.Now;
        //                return;
        //            }
        //        }
        //    }
        //}
        
    }
}
