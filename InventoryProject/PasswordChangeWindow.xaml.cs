using InventoryProject.Models.Users;
using InventoryProject.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InventoryProject
{
    /// <summary>
    /// Interaction logic for PasswordChangeWindow.xaml
    /// </summary>
    public partial class PasswordChangeWindow : Window
    {
        Boolean cancel = true;
        public String Password = "";
        public Int32 UserID = 0;
        ItemDataContext dataCon = new ItemDataContext();
        UserRepository repo = new UserRepository();
        public PasswordChangeWindow(Int32 userid)
        {
            InitializeComponent();
            newpassword.Focus();
            passwordStrength.Foreground = Brushes.Red;
            errorfield.Foreground = Brushes.Red;
            UserID = userid;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            LoginWindow subWindow = new LoginWindow();
            subWindow.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = this.cancel;
        }

        private void newpassword_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            passwordStrength.Content = PasswordAdvisor.CheckStrength(newpassword.Password.ToString());

            switch (PasswordAdvisor.CheckStrength(newpassword.Password.ToString()))
            {
                case PasswordScore.Strong:
                    passwordStrength.Foreground = Brushes.Green;
                    button.IsEnabled = true;
                    break;
                case PasswordScore.VeryStrong:
                    passwordStrength.Foreground = Brushes.DarkGreen;
                    button.IsEnabled = true;
                    break;
                default:
                    passwordStrength.Foreground = Brushes.Red;
                    button.IsEnabled = false;
                    break;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            NewPassword();
        }

        private void confirmpassword_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            NewPassword();
        }

        private void NewPassword()
        {
            if (newpassword.Password.ToString() != confirmpassword.Password.ToString())
            {
                errorfield.Content = "Confirm Password does not Match!!!";
            }
            else
            {
                this.dataCon.one = new UserParam();
                byte[] ba = Encoding.Default.GetBytes(Encryption.Encrypt(newpassword.Password.ToString()));
                var hexString = BitConverter.ToString(ba);

                this.dataCon.one.UserID = UserID;
                this.dataCon.one.Password = hexString;

                if (this.repo.UpdateUserInfo(this.dataCon.one))
                {
                    MessageBox.Show("Successfully Changed!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                
                this.Password = newpassword.Password.ToString();
                this.cancel = false;               
            }
            this.Close();
        }

        public enum PasswordScore
        {
            Blank = 0,
            VeryWeak = 1,
            Weak = 2,
            Medium = 3,
            Strong = 4,
            VeryStrong = 5
        }

        public class PasswordAdvisor
        {
            public static PasswordScore CheckStrength(string password)
            {
                int score = 0;

                if (password.Length < 1)
                    return PasswordScore.Blank;
                if (password.Length < 8)
                    return PasswordScore.VeryWeak;

                if (password.Length >= 8)
                    score++;


                string pattern = @"[A-Z]";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(password);

                if (match.Value.Length > 0)
                {
                    score++;
                }
                pattern = @"[a-z]";
                regex = new Regex(pattern);
                match = regex.Match(password);

                if (match.Value.Length > 0)
                {
                    score++;
                }
                pattern = @"[0-9]";
                regex = new Regex(pattern);
                match = regex.Match(password);

                if (match.Value.Length > 0)
                {
                    score++;
                }
                pattern = @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/";
                regex = new Regex(pattern);
                match = regex.Match(password);

                if (match.Value.Length > 0)
                {
                    score++;
                }


                if (password.Length >= 12)
                    score++;


                return (PasswordScore)score;
            }
        }

        private void GridDrag_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {

            }
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public class ItemDataContext : INotifyPropertyChanged
        {

            List<UserParam> _userparam;
            public List<UserParam> userparam
            {
                get { return _userparam; }
                set
                {
                    _userparam = value;
                    NotifyPropertyChanged("userparam");
                }
            }

            UserParam _one;
            public UserParam one
            {
                get { return _one; }
                set
                {
                    _one = value;
                    NotifyPropertyChanged("one");
                }
            }


            public event PropertyChangedEventHandler PropertyChanged;
            private void NotifyPropertyChanged(String info)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(info));
                }
            }
        }
    }
}

