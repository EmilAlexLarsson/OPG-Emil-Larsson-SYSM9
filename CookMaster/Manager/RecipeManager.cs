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
        public ObservableCollection<Recipe> ShowAllRecipes { get; set; } = new ObservableCollection<Recipe>();
        public ObservableCollection<Recipe>? Recipes
        {
            get 
            {
                if (UserManager.LoggedIn is AdminUser)
                {
                    return ShowAllRecipes;
                }
                return UserManager.LoggedIn?.Recipes;
                //if(UserManager.LoggedIn is AdminUser)
                //{
                //    var showAllRecipes = new ObservableCollection<Recipe>();

                    //    foreach(var user in UserManager.Users)
                    //    {
                    //        foreach(Recipe recipe in user.Recipes)
                    //        {
                    //            showAllRecipes.Add(recipe);
                    //        }
                    //    }
                    //    return showAllRecipes;
                    //}
                    //return UserManager.LoggedIn?.Recipes; 
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
            ShowAllUserRecipe();


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
                Date = new DateTime(2024, 10, 31),
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
            //if (Recipes == null)
            //{
            //    return false;
            //}
            //foreach (Recipe existingRecipe in Recipes)
            //{
            //    if(existingRecipe.Title == title)
            //    {
            //        return false;
            //    }
            //}

            Recipe newRecipe = new Recipe
            {
                Title = title,
                Ingredients = ingredients,
                Instructions = instructions,
                Category = category,
                Date = DateTime.Now,
                CreatedBy = createdBy
            };

            //Recipes?.Add(newRecipe);
            //return true;

            if (UserManager.LoggedIn is AdminUser admin)
            {
                if (admin.Recipes == null)
                {
                    admin.Recipes = new ObservableCollection<Recipe>();
                }
                foreach (Recipe existingRecipe in admin.Recipes)
                {
                    if (existingRecipe.Title == title)
                    {
                        return false;
                    }
                }
                admin.Recipes.Add (newRecipe);
                //return true;
            }
            else if (UserManager.LoggedIn != null)
            {
                if (UserManager.LoggedIn.Recipes == null)
                {
                    UserManager.LoggedIn.Recipes = new ObservableCollection<Recipe>();
                }
                foreach (Recipe existingRecipe in UserManager.LoggedIn.Recipes)
                {
                    if (existingRecipe.Title == title)
                    {
                        return false;
                    }
                }
                UserManager.LoggedIn.Recipes.Add(newRecipe);
                
            }
            else
            {
                return false;
            }
            if (ShowAllRecipes == null)
            {
                ShowAllRecipes = new ObservableCollection<Recipe>();
            }
            foreach (Recipe existingRecipe in ShowAllRecipes)
            {
                if (existingRecipe.Title == title)
                {
                    return false;
                }
            }
            ShowAllRecipes.Add(newRecipe);
            ShowAllUserRecipe();
            return true;
        }
        

        public void RemoveRecipe(Recipe recipe)
        {
            //if (recipe == null)
            //{
            //    return;
            //}
            //if(UserManager.LoggedIn is AdminUser)
            //{
            //    //Recipes.Remove(recipe);
            //    //showAllRecipes.Remove(recipe);

            //    foreach (var user in UserManager.Users)
            //    {
            //        if (user.Recipes.Contains(recipe))
            //        {
            //            user.Recipes.Remove(recipe);
            //            //showAllRecipes.Remove(recipe);
            //            return;
            //        }
            //    }
            //    return;
            //}
            //Recipes?.Remove(recipe);
            //UserManager.LoggedIn.Recipes.Remove(recipe);
            if (recipe == null)
            {
                return;
            }
            foreach (User user in UserManager.Users)
            {
                if (user.Recipes != null && user.Recipes.Contains(recipe))
                {
                    user.Recipes.Remove(recipe);
                    break;
                }
            }
            if(ShowAllRecipes != null && ShowAllRecipes.Contains(recipe))
            {
                ShowAllRecipes.Remove(recipe);
            }
            ShowAllUserRecipe();
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
        public void ShowAllUserRecipe()
        {
            ShowAllRecipes.Clear();

            foreach (User user in UserManager.Users)
            {
                if (user.Recipes != null)
                {
                    foreach(Recipe recipe in user.Recipes)
                    {
                        ShowAllRecipes?.Add(recipe);
                    }
                }
            }
        }
        public void SortByNewest()
        {
            ObservableCollection<Recipe> recipeList;

            if (UserManager.LoggedIn is AdminUser)
            {
                recipeList = ShowAllRecipes;
            }
            else
            {
                recipeList = UserManager.LoggedIn?.Recipes;
            }
            if (recipeList == null)
            {
                return;
            }

            List<Recipe> sortRecipes = new List<Recipe>(recipeList);
            sortRecipes.Sort((recipe1, recipe2) => recipe2.Date.CompareTo(recipe1.Date)); //om 2 är nyare, lägg de före 1
            recipeList.Clear();

            foreach(Recipe recipe in sortRecipes)
            {
                recipeList.Add(recipe);
            }

        }
        public void CategoryFilter(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return;
            }
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
