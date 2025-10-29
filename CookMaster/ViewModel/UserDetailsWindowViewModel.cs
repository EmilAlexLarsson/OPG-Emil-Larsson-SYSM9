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
        public RecipeManager RecipeManager { get; set; }
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
        public RelayCommand SaveCommand => new RelayCommand(execute => Save());

        public void Save()
        {
            if(UserManager.UpdateUserDetails(Username, NewPassword, ConfirmPassword, SelectedCountry, out string error))
            {
                MessageBox.Show("User details updated!");
                RecipeListWindow recipeListWindow = new RecipeListWindow(RecipeManager);
                recipeListWindow.Show();

                foreach (Window window in Application.Current.Windows)
                {
                    if (window != recipeListWindow)
                    {
                        window.Close(); //kolla på annan variant
                    }
                }

            }
            else
            {
                MessageBox.Show (error);
            }
            //if (Username.Length < 3)
            //{
            //    MessageBox.Show("Username must be at least 3 characters long.");
            //    return;
            //}
            //if(string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(ConfirmPassword))
            //{
            //    MessageBox.Show("Password cannot be empty!");
            //    return;
            //}
            //if (NewPassword != ConfirmPassword)
            //{
            //    MessageBox.Show("Passwords do not match!");
            //    return;
            //}

            //UserManager.LoggedIn.Username = Username;
            //UserManager.LoggedIn.Password = NewPassword;
            //UserManager.LoggedIn.Country = SelectedCountry;

            //MessageBox.Show("New User details saved!");
            //RecipeListWindow recipeListWindow = new RecipeListWindow(RecipeManager);
            //recipeListWindow.Show();
            //foreach (Window window in Application.Current.Windows)
            //{
            //    if (window != recipeListWindow)
            //    {
            //        window.Close(); //kolla på annan variant
            //    }
            //}

        }
        public void Cancel()
        {
            RecipeListWindow recipeListWindow = new RecipeListWindow(RecipeManager);
            recipeListWindow.Show();
            
            foreach (Window window in Application.Current.Windows)
            {
                if (window != recipeListWindow)
                {
                    window.Close(); //kolla på annan variant
                }
            }
        }

    }
}
