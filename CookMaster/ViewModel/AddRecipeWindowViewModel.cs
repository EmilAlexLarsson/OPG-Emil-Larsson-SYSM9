using CookMaster.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.ViewModel
{
    public class AddRecipeWindowViewModel
    {
        public UserManager UserManager { get; set; }
        public RecipeManager RecipeManager { get; set; }
        public AddRecipeWindowViewModel(UserManager userManager, RecipeManager recipeManager)
        {
            UserManager = userManager;
            RecipeManager = recipeManager;
        }
    }
}
