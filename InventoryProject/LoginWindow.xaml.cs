using InventoryProject.Models.Users;
using InventoryProject.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace InventoryProject
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public DateTime CurrentDateAndTime { get; set; }
        ItemDataContext dataCon = new ItemDataContext();
        UserRepository repo = new UserRepository();
        public LoginWindow()
        {
            InitializeComponent();
            DispatcherTimer dayTimer = new DispatcherTimer();
            dayTimer.Interval = TimeSpan.FromMilliseconds(500);
            dayTimer.Tick += new EventHandler(timer_Tick);
            dayTimer.Start();
            lblDateTime.Content = "Date:" + " " + DateTime.Now.ToShortDateString();
            txt_username.Focus();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            RequiredLogin();
           
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

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = "Time:" + " " + DateTime.Now.ToLongTimeString();

        }

        private void RequiredLogin()
        {
            if (txt_username.Text == "" || string.IsNullOrEmpty(txt_username.Text) || txt_Password.Password == "" || string.IsNullOrEmpty(txt_Password.Password))
            {
                MessageBox.Show(this, "LogIn Failed!", "Invalid Username and Password...", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {

                byte[] ba = Encoding.Default.GetBytes(Encryption.Encrypt(txt_Password.Password));
                var hexString = BitConverter.ToString(ba);
                this.dataCon.one = new UserParam();
               // this.dataCon.userparam = this.repo.GetUserList(txt_username.Text, hexString);
                this.dataCon.userparam = this.repo.GetUserList(txt_username.Text, txt_Password.Password);
                foreach (var item in this.dataCon.userparam)
                {
                    this.dataCon.one.Firstname = item.Firstname;
                    this.dataCon.one.IsActive = item.IsActive;
                    this.dataCon.one.IsAdmin = item.IsAdmin;
                    this.dataCon.one.IsReset = item.IsReset;
                    this.dataCon.one.JobPositionID = item.JobPositionID;
                    this.dataCon.one.Lastname = item.Lastname;
                    this.dataCon.one.Middlename = item.Middlename;
                    this.dataCon.one.Password = item.Password;
                    this.dataCon.one.UserID = item.UserID;
                    this.dataCon.one.Id = item.Id;
                    this.dataCon.one.Username = item.Username;
                }


                if (this.dataCon.one.IsReset)
                {
                    PasswordChangeWindow main = new PasswordChangeWindow(this.dataCon.one.UserID);
                    this.Hide();
                    main.ShowDialog();
                }
                else
                {
                    if (this.dataCon.userparam.Count > 0)
                    {
                        MainWindow subWindow = new MainWindow(this.dataCon.one.UserID, this.dataCon.one.Username);
                        this.Hide();
                        subWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show(this, "LogIn Failed!", "Invalid Username and Password...", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                  

                }
           
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RequiredLogin();
            }
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

}
