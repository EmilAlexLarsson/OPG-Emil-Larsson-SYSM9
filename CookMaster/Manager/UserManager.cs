using CookMaster.Model;
using CookMaster.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CookMaster.Manager
{
    public class UserManager : ViewModelBase
    {
        private ObservableCollection<User> _users;
        public ObservableCollection <User> Users
        {
            get
            {
                return _users;
            }
        }
        private User _loggedIn;
        public User LoggedIn
        {
            get
            {
                return _loggedIn;
            }
            set
            {
                _loggedIn = value;
                OnPropertyChanged();
            }
        }

        public UserManager()
        {
            _users = new ObservableCollection<User>();
            DefaultUsers();
        }

        private void DefaultUsers()
        {
            Users.Add(new User
            {
                Username = "admin",
                Password = "password"
            });
            Users.Add(new User
            {
                Username = "user",
                Password = "password"
            });
        }

        public bool LogIn(string username, string password)
        {
            
            foreach (User user in Users)
            {
                if (user.Username == username && user.Password == password)
                {
                    LoggedIn = user;
                    return true;
                }
            }
            return false;
        }
        public void Register(string username, string password, string country)
        {
            Users.Add(new User
            {
                Username = username,
                Password = password,
                Country = country
            });
        }
        public User FindUser(string name)
        {
            foreach (User user in Users)
            {
                if (user.Username == name)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
