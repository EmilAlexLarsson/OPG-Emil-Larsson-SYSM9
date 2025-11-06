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
                
            }
        }


        public RecipeManager()
        {
            UserManager = (UserManager)Application.Current.Resources["UserManager"];
            if (UserManager == null)
            {
                MessageBox.Show("Cannot find UserManager");
                return;
            }

            User? defaultUser = null;
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

            

        }
        
        public bool AddRecipe(string title, string ingredients, string instructions, string category, User createdBy)
        {
            try
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

                if (UserManager.LoggedIn is AdminUser admin)
                {
                    if (admin.Recipes == null)
                    {
                        admin.Recipes ??= new ObservableCollection<Recipe>();
                    }
                    foreach (Recipe existingRecipe in admin.Recipes)
                    {
                        if (existingRecipe.Title == title)
                        {
                            return false;
                        }
                    }
                    admin.Recipes.Add(newRecipe);
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
                //ShowAllUserRecipe();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while adding recipe: " + e.Message);
                return false;
            }

        }
        

        public void RemoveRecipe(Recipe recipe)
        {
            try
            {
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
                if (ShowAllRecipes != null && ShowAllRecipes.Contains(recipe))
                {
                    ShowAllRecipes.Remove(recipe);
                }
                ShowAllUserRecipe();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while removing recipe: " + e.Message);
            }
        }
        
        public Recipe? CopyRecipe(Recipe recipe)
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
            try
            {
                ShowAllRecipes.Clear();

                foreach (User user in UserManager.Users)
                {
                    if (user.Recipes != null)
                    {
                        foreach (Recipe recipe in user.Recipes)
                        {
                            ShowAllRecipes?.Add(recipe);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }
        public ObservableCollection<Recipe> SortByNewest(ObservableCollection<Recipe> visibileRecipes)
        {
            try
            {
                if (visibileRecipes == null || visibileRecipes.Count <= 1)
                {
                    return visibileRecipes ?? new ObservableCollection<Recipe>();
                }
                List<Recipe> sortedList = new List<Recipe>(visibileRecipes);
                sortedList.Sort((recipe1, recipe2) => recipe2.Date.CompareTo(recipe1.Date)); //om 2 är nyare, lägg de före 1
                return new ObservableCollection<Recipe>(sortedList);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while sorting recipes: " + e.Message);
                return visibileRecipes ?? new ObservableCollection<Recipe>();
            }
        }
        public ObservableCollection<Recipe> FilterRecipes(string category, ObservableCollection<Recipe> allRecipes)
        {
            try
            {
                if (allRecipes == null)
                {
                    return new ObservableCollection<Recipe>();
                }
                if (string.IsNullOrWhiteSpace(category))
                {
                    return new ObservableCollection<Recipe>(allRecipes);
                }
                ObservableCollection<Recipe> filteredList = new ObservableCollection<Recipe>();
                foreach (Recipe recipe in allRecipes)
                {
                    if (recipe.Category != null && recipe.Category.ToLower().Contains(category.ToLower()))
                    {
                        filteredList.Add(recipe);
                    }
                }
                return filteredList;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while filtering recipes: " + e.Message);
                return new ObservableCollection<Recipe>();
            }
        }


    }
}
