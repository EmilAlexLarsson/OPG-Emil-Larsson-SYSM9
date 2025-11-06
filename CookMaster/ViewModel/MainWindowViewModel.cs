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
        public RelayCommand LogInCommand => new RelayCommand(execute => LogIn(), canExecute => CanLogIn());
        public RelayCommand RegisterCommand => new RelayCommand(execute => OpenRegister());
        public RelayCommand ForgotPasswordCommand => new RelayCommand(execute => OpenForgotPassword());

        public void LogIn()
        {
            try
            {
                if (UserManager == null)
                {
                    MessageBox.Show("Cannot find UserManager");
                    return;
                }

                if (UserManager.LogIn(Username, Password, out string error))
                {
                    //RecipeManager recipeManager = new RecipeManager(UserManager);
                    RecipeListWindow recipeListWindow = new RecipeListWindow();
                    recipeListWindow.Show();
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window != recipeListWindow)
                        {
                            window.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show(error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error when logging in" + e.Message);
            }

        }
        private bool CanLogIn()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }
        public void OpenRegister()
        {
            
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
            

        }
        public void OpenForgotPassword()
        {
            ForgotPasswordWindow forgotPasswordWindow = new ForgotPasswordWindow();
            forgotPasswordWindow.ShowDialog();
            
        }
    }
}
