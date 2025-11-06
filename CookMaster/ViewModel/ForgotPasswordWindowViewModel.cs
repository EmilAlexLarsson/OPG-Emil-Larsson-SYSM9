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
    public class ForgotPasswordWindowViewModel : ViewModelBase
    {
        public UserManager UserManager { get; }

        public ForgotPasswordWindowViewModel(UserManager userManager)
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
                Question = UserManager.Question(Username) ?? string.Empty;
            }
        }

        private string _newPassword;
        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
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

        private string _question;
        public string Question
        {
            get { return _question; }
            set
            {
                _question = value;
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
        
        public RelayCommand ResetPasswordCommand => new RelayCommand(execute => ResetPassword(), canExecute => NewPassword != null && ConfirmPassword != null && QuestionAnswer != null);
        public bool ResetPassword()
        {
            try
            {
                if (UserManager.ForgotPassword(Username, QuestionAnswer, NewPassword, ConfirmPassword, out string error))
                {
                    MessageBox.Show("Password has been updated!");
                    
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window is ForgotPasswordWindow)
                        {
                            window.Close();
                            break;
                        }
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show(error);
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while trying to reset password: " + e.Message);
                return false;
            }
            
        }
    }
}
