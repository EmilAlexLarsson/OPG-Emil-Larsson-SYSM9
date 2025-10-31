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
        
        public UserManager UserManager { get; }
        public List<string> Countries { get; }
        public List<string> SecurityQuestion { get; }
        public RegisterWindowViewModel(UserManager userManager)
        {
            UserManager = userManager;
            Countries = UserManager.Countries;
            SecurityQuestion = UserManager.SecurityQuestion;
            //for (int i = 0; i < Application.Current.Windows.Count; i++)
            //{
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
        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
            }
        }
        private string _selectedQuestion;
        public string SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                _selectedQuestion = value;
                OnPropertyChanged();
            }
        }
        private string _questionAnswer;
        public string QuestionAnswer
        {
            get { return _questionAnswer; }
            set
            {
                _questionAnswer = value;
                OnPropertyChanged();
            }
        }


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
            if (UserManager.Register(Username, Password, ConfirmPassword, SelectedCountry, SelectedQuestion,QuestionAnswer, out string error))
            {
                MessageBox.Show("New user created!");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Application.Current.Windows[0].Close();
            }
            else
            {
                MessageBox.Show(error);
            }
            //MainWindow mainWindow = new MainWindow();
            //var result = mainWindow.ShowDialog();

            //if(result != true)
            //{
            //    Application.Current.Shutdown();
            //}

            //MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();
            //Application.Current.Windows[0].Close();
            //Stänger första fönstert i listan (index 0), alltså registerwindow, då det är det ända öppna fönstert
        }
    }
}
