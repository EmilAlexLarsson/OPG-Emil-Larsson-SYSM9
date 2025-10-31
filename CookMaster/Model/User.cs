using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Model
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public ObservableCollection<Recipe> Recipes { get; set; } = new ObservableCollection<Recipe>();
        public string Question { get; set; }
        public string QuestionAnswer { get; set; }

        public virtual bool ViewAllRecipes()
        {
            return false;
        }

    }
}
