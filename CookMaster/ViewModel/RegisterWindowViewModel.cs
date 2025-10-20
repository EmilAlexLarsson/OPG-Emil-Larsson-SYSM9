using CookMaster.Manager;
using CookMaster.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.ViewModel
{
    public class RegisterWindowViewModel : ViewModelBase
    {
        public UserManager UserManager { get; }
        public RegisterWindowViewModel(UserManager userManager)
        {
            UserManager = userManager;
        }
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public List<string> Countries { get; set; } = new List<string>
        {
            "Sweden",
            "Norway",
            "Denmark",
            "Finland",
            "Iceland"
        };
        private string _selectedCountry;
        public string SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                _selectedCountry = value;
                OnPropertyChanged();
            }
        }
    }
}
