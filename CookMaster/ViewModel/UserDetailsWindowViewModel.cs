using CookMaster.Manager;
using CookMaster.MVVM;
using CookMaster.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CookMaster.ViewModel
{
    public class UserDetailsWindowViewModel : ViewModelBase
    {
        public UserManager UserManager { get; }
        public RecipeManager RecipeManager { get;  }
        public List<string> Countries { get; }


        public UserDetailsWindowViewModel(UserManager userManager, RecipeManager recipeManager)
        {
            UserManager = userManager;
            RecipeManager = recipeManager;
            Countries = UserManager.Countries;
        }

        public string Username
        {
            get { return UserManager.LoggedIn?.Username ?? string.Empty; }
            set
            {
                if (UserManager.LoggedIn?.Username != value && UserManager.LoggedIn != null)
                {
                    UserManager.LoggedIn.Username = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _newPassword;
        public string NewPassword
        {
            get
            {
                return _newPassword;
            }
            set
            {
                _newPassword = value;
                OnPropertyChanged();
            }
        }
        private string _confirmPassword;
        public string ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
            }
        }
        private string _selectedCountry;
        public string SelectedCountry
        {
            get { return UserManager.LoggedIn?.Country ?? string.Empty; }
            set
            {
                if (UserManager.LoggedIn?.Country != value && UserManager.LoggedIn != null)
                {
                    UserManager.LoggedIn.Country = value;
                    OnPropertyChanged();
                }
            }
        }


        public RelayCommand CancelCommand => new RelayCommand(execute => Cancel());
        public RelayCommand SaveCommand => new RelayCommand(execute => Save(), canExecute => !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(NewPassword) && !string.IsNullOrWhiteSpace(ConfirmPassword) && !string.IsNullOrWhiteSpace(SelectedCountry));

        public void Save()
        {
            try
            {
                if (UserManager.LoggedIn == null || UserManager == null)
                {
                    MessageBox.Show("Cannot find UserManager or LoggedIn user.");
                    return;
                }
                if (UserManager.UpdateUserDetails(Username, NewPassword, ConfirmPassword, SelectedCountry, out string error))
                {
                    MessageBox.Show("User details updated!");
                    RecipeListWindow recipeListWindow = new RecipeListWindow(RecipeManager);
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
                MessageBox.Show("Error while trying to save " + e.Message);
            }



        }
        public void Cancel()
        {
            RecipeListWindow recipeListWindow = new RecipeListWindow(RecipeManager);
            recipeListWindow.Show();
            
            foreach (Window window in Application.Current.Windows)
            {
                if (window != recipeListWindow)
                {
                    window.Close();
                }
            }
        }

    }
}
