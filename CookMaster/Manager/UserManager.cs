using CookMaster.Model;
using CookMaster.MVVM;
using Microsoft.VisualBasic;
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
        private User? _loggedIn;
        public User? LoggedIn
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
        
        public List<string> Countries { get; set; } = new List<string>
        {
            "Sweden",
            "Norway",
            "Denmark",
            "Finland",
            "Iceland"
        };
        public List<string> SecurityQuestion { get; set; } = new List<string>
        {
            "What is your favorite color?",
            "What is your Lucky number?",
            "What is your favorite sports team?"
        };
        private Random random = new Random();

        public UserManager()
        {
            _users = new ObservableCollection<User>();
            DefaultUsers();
        }

        private void DefaultUsers()
        {
            Users.Add(new AdminUser //AdminUser
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

        public bool LogIn(string username, string password, out string error)
        {
            error = string.Empty;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                error = "Username or password cannot be empty";
                return false;
            }
            var user = FindUser(username);
            if (user == null)
            {
                error = "User does not exist!";
                return false;
            }
            if (user.Password != password)
            {
                error = "Wrong password!";
                return false;
            }

            //string verificationCode = random.Next(100000, 999999).ToString();
            //MessageBox.Show("Your 2FA code: \n" + verificationCode);
            //string codeInput = Interaction.InputBox("Enter 2FA code: ", "Verification code", "");

            //if (string.IsNullOrEmpty(codeInput))
            //{
            //    error = "Please enter code";
            //    return false;
            //}
            //if (codeInput != verificationCode)
            //{
            //    error = "Incorrect code";
            //}

            LoggedIn = user;

            return true;
        }
        public bool Register(string username, string password,string confirmPassword, string country,string question, string questionAnswer, out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(question) || string.IsNullOrWhiteSpace(questionAnswer))
            {
                error = "Please fill in username, password, country, answer and pick a question";
                return false;
            }

            if (password != confirmPassword)
            {
                error = "Passwords do not match!";
                return false;
            }

            if (FindUser(username) != null)
            {
                error = "User already exists!";
                return false;
            }

            if (!ValidatePassword(password, out error))
            {
                return false;
            }

            Users.Add(new User
            {
                Username = username,
                Password = password,
                Country = country,
                Question = question,
                QuestionAnswer = questionAnswer
            });
            return true;
        }
        public User FindUser(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            foreach (User user in Users)
            {
                if (user.Username == name)
                {
                    return user;
                }
            }
            return null;
        }
        public bool UpdateUserDetails(string username, string newPassword, string confirmPassword, string country, out string error)
        {
            error = string.Empty;

            if(string.IsNullOrWhiteSpace(username)|| username.Length < 3)
            {
                error = "Username must be more than 3 characters!";
                return false;
            }

            if(string.IsNullOrWhiteSpace(newPassword)|| string.IsNullOrWhiteSpace(confirmPassword))
            {
                error = "Password cannot be empty!";
                return false;
            }

            if (LoggedIn == null)
            {
                error = "Cannot find logged in user!";
                return false;
            }

            if (newPassword != confirmPassword)
            {
                error = "Passwords do not match!";
                return false;
            }
            
            if (!ValidatePassword(newPassword, out error))
            {
                return false;
            }
            LoggedIn.Username = username;
            LoggedIn.Password = newPassword;
            LoggedIn.Country = country;
            return true;
        }
        public bool ForgotPassword(string username, string questionAnswer, string newPassword, string confirmPassword, out string error)
        {
            error = string.Empty;
            var user = FindUser(username);
            if (user == null)
            {
                error = "Cannot find user!";
                return false;
            }

            if(!string.Equals(user.QuestionAnswer, questionAnswer, StringComparison.OrdinalIgnoreCase))
            {
                error = "Incorrect answer!";
                return false;
            }

            if(string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                error = "Password cannot be empty!";
                return false;
            }

            if(newPassword != confirmPassword)
            {
                error = "Passwords do not match!";
                return false;
            }

            if (!ValidatePassword(newPassword, out error))
            {
                return false;
            }
            user.Password = newPassword;
            return true;
        }
        public bool ValidatePassword(string password, out string error)
        {
            error = string.Empty;
            if (password.Length < 8)
            {
                error = "Password must be at least 8 characters!";
                return false;
            }
            if (!password.Any(char.IsDigit))
            {
                error = "Password must contain at least one digit!";
                return false;
            }
            bool specialChar = false;
            foreach (char c in password)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    specialChar = true;
                    break;
                }
            }
            if (!specialChar)
            {
                error = "Password must contain at least one special character!";
                return false;
            }
            return true;
        }
        public string Question(string username)
        {
            var user = FindUser(username);
            if (user != null)
            {
                return user.Question;
            }
            return null;
        }

    }
}
