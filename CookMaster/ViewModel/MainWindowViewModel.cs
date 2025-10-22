using CookMaster.Manager;
using CookMaster.MVVM;
using CookMaster.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CookMaster.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public UserManager UserManager { get; }
        public MainWindowViewModel(UserManager userManager)
        {
            UserManager = userManager;

            //for (int i = 0; i < Application.Current.Windows.Count; i++)
            //{
            //    var window = Application.Current.Windows[i];
            //    MessageBox.Show($"{i}");
            //}
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
        public RelayCommand LogInCommand => new RelayCommand(execute => LogIn());
        public RelayCommand RegisterCommand => new RelayCommand(execute => OpenRegister());

        public void LogIn()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Username or password can't be empty");
                return;
            }

            RecipeListWindow recipeListWindow = new RecipeListWindow();
            if (UserManager.LogIn(Username, Password))
            {
                recipeListWindow.Show();
                Application.Current.Windows[0].Close();
            }
            else
            {
                MessageBox.Show("Wrong Username or password");
            }
        }
        public void OpenRegister()
        {
            
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            Application.Current.Windows[0].Close();
            //Stänger fönsta fönstert i listan, alltså mainwidow, då det öppnades först

        }
    }
}
