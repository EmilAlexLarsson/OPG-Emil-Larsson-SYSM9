using CookMaster.Manager;
using CookMaster.Model;
using CookMaster.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CookMaster.ViewModel
{
    public class RegisterWindowViewModel : ViewModelBase
    {
        //Application.Current.Windows[1].Close();
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
        public RelayCommand CreateUserCommand => new RelayCommand(execute => CreateUser());
        public void CreateUser()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(SelectedCountry))
            {
                MessageBox.Show("Username or password can't be empty");
                return;
            }
            if (UserManager.FindUser(Username) != null)
            {
                MessageBox.Show("Username already exists");
                return;
            }
            else
            {
                UserManager.Register(Username, Password, SelectedCountry);

                MessageBox.Show("New user created!");


                Application.Current.Windows[1].Close();

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                //funkar inte
            }


        }
    }
}
