using CookMaster.Manager;
using CookMaster.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.ViewModel
{
    public class RecipeListWindowViewModel : ViewModelBase
    {
        public RecipeManager recipeManager {  get; }


        public RecipeListWindowViewModel()
        {
            recipeManager = new RecipeManager();
        }
        //public RelayCommand LogInCommand => new RelayCommand(execute => );

        public void AddRecipe()
        {
            //öppnar addrecipe window
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
