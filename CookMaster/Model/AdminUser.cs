using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Model
{
    public class AdminUser : User
    {
        public void RemoveAnyRecipe()
        {

        }
        public override bool ViewAllRecipes()
        {
            return true;
        }
    }
}
