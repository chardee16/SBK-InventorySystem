using InventoryProject.Models.ClientModule;
using InventoryProject.Models.Users;
using InventoryProject.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InventoryProject.Pages
{
    /// <summary>
    /// Interaction logic for ClientInformationPage.xaml
    /// </summary>
    public partial class UserManagementPage : Page
    {
        ItemDataContext dataCon = new ItemDataContext();
        UserRepository repo = new UserRepository();
        public bool _ispassed = false;
        String message = "";
        bool _isactived;
        public UserManagementPage()
        {
            InitializeComponent();
            this.DataContext = this.dataCon;
            Disable();
            LoadAllUsers();       
        }

        private void LoadAllUsers()
        {
            this.dataCon.userparam = this.repo.GetAllUsers();
            this.dataCon.privilege = this.repo.GetAllPrivilege();
        }

        private void ClearValues()
        {
            this.txt_UserID.Text = "";
            this.txt_FirstName.Text = String.Empty;
            this.txt_MiddleName.Text = String.Empty;
            this.txt_LastName.Text = String.Empty;
            this.txt_UserName.Text = String.Empty;
            txt_Password.Password = String.Empty;
            txt_ConfirmPass.Password = String.Empty;
            this.chck_isAdmin.IsChecked = false;
          
        }

        private void Disable()
        {
            this.txt_FirstName.IsReadOnly = true;
            this.txt_LastName.IsReadOnly = true;
            this.txt_MiddleName.IsReadOnly = true;
            this.txt_UserName.IsReadOnly = true;
            this.txt_Password.IsEnabled = false;
            this.txt_ConfirmPass.IsEnabled = false;
            this.btn_Activate.IsEnabled = false;
            lb_userprivilege.IsEnabled = false;
            btn_Reset.IsEnabled = false;
            btn_Activate.IsEnabled = false;
        }

        private void Enable()
        {
            this.txt_FirstName.IsReadOnly = false;
            this.txt_LastName.IsReadOnly = false;
            this.txt_MiddleName.IsReadOnly = false;
            this.txt_UserName.IsReadOnly = false;
            this.txt_Password.IsEnabled = true;
            this.txt_ConfirmPass.IsEnabled = true;
            this.btn_Activate.IsEnabled = true;
            lb_userprivilege.IsEnabled = true;
            btn_Activate.IsEnabled = true;
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

            List<Privilege> _privilege;
            public List<Privilege> privilege
            {
                get { return _privilege; }
                set
                {
                    _privilege = value;
                    NotifyPropertyChanged("privilege");
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

            string _Lastname;
            public string Lastname
            {
                get { return _Lastname; }
                set
                {
                    _Lastname = value;
                    NotifyPropertyChanged("Lastname");
                }
            }

            string _Firstname;
            public string Firstname
            {
                get { return _Firstname; }
                set
                {
                    _Firstname = value;
                    NotifyPropertyChanged("Firstname");
                }
            }

            string _Middlename;
            public string Middlename
            {
                get { return _Middlename; }
                set
                {
                    _Middlename = value;
                    NotifyPropertyChanged("Middlename");
                }
            }

            string _UserName;
            public string UserName
            {
                get { return _UserName; }
                set
                {
                    _UserName = value;
                    NotifyPropertyChanged("UserName");
                }
            }


            string _UserID;
            public string UserID
            {
                get { return _UserID; }
                set
                {
                    _UserID = value;
                    NotifyPropertyChanged("UserID");
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

        private void btn_New_Click(object sender, RoutedEventArgs e)
        {
            ClearValues();
            Enable();
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            _ispassed = true;
            Enable();
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveUser();
        }

        private void DG_Client_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {

                UserParam selected = (UserParam)DG_Clients.SelectedItem;
                this.dataCon.UserID = selected.UserID.ToString();
                this.dataCon.Firstname = selected.Firstname;
                this.dataCon.Middlename = selected.Middlename;
                this.dataCon.Lastname = selected.Lastname;
                this.dataCon.UserName = selected.Username;
                _isactived = selected.IsActive;
                if (selected.IsActive)
                {
                    btn_Activate.Content = "DISABLE";

                }
                else
                {
                    btn_Activate.Content = "ENABLE";
                }

                Disable();

            }
            catch (Exception ex)
            {
            }
        }


        private void SaveUser()
        {
            if (_ispassed)
            {
                message = "Are you sure you want to update this user?";
            }
            else
            {
                message = "Are you sure you want to save this user?";
            }

            MessageBoxResult messageBoxResult = MessageBox.Show(message, "CONFIRMATION", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
            if (messageBoxResult == MessageBoxResult.Yes)
            {

                byte[] ba = Encoding.Default.GetBytes(Encryption.Encrypt(txt_Password.Password));
                var hexString = BitConverter.ToString(ba);

                UserParam user = new UserParam();
                user.Lastname = txt_LastName.Text;
                user.Firstname = txt_FirstName.Text;
                user.Middlename = txt_MiddleName.Text;
                user.Username = txt_UserName.Text;
                user.Password = txt_Password.Password;
                user.IsAdmin = chck_isAdmin.IsChecked ?? false;

                user.privil = new List<UserPrivileges>();

                if (_ispassed)
                {
                    user.UserID = Convert.ToInt32(txt_UserID.Text);

                    if (lb_userprivilege.SelectedItems.Count > 0)
                    {
                        foreach (Privilege item in lb_userprivilege.SelectedItems)
                        {
                            user.privil.Add(new UserPrivileges()
                            {
                                UserID = String.IsNullOrWhiteSpace(txt_UserID.Text) ? 0 : Convert.ToInt32(txt_UserID.Text),
                                IsAllowed = true,
                                PrivilegeID = item.ID,
                             });
                        }
                    }


                    if (this.repo.UpdateUserInformation(user))
                    {
                        MessageBox.Show("User Successfully Updated.", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearValues();
                        LoadAllUsers();
                        _ispassed = false;
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong.\nPlease Contact Administrator!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {

                    //user.UserID = Convert.ToInt32(txt_UserID.Text);
                    if (lb_userprivilege.SelectedItems.Count > 0)
                    {
                        foreach (Privilege item in lb_userprivilege.SelectedItems)
                        {
                            user.privil.Add(new UserPrivileges()
                            {
                                UserID = String.IsNullOrWhiteSpace(txt_UserID.Text) ? 0 : Convert.ToInt32(txt_UserID.Text),
                                IsAllowed = true,
                                PrivilegeID = item.ID,
                            });
                        }
                    }

                    if (this.repo.InsertUserInfo(user))
                    {
                        MessageBox.Show("User Successfully Saved.", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearValues();
                        LoadAllUsers();
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong.\nPlease Contact Administrator!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }

            }

        }

        private void confirmpassword_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (txt_Password.Password.ToString() == txt_ConfirmPass.Password.ToString())
            {
                btn_Reset.IsEnabled = true;
            }
        }

        private void btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            if (txt_Password.Password.ToString() == txt_ConfirmPass.Password.ToString())
            {
                this.dataCon.one = new UserParam();
                byte[] ba = Encoding.Default.GetBytes(Encryption.Encrypt(txt_Password.Password));
                var hexString = BitConverter.ToString(ba);

                this.dataCon.one.UserID = Convert.ToInt32(txt_UserID.Text);
                this.dataCon.one.Password = hexString;

                if (this.repo.ResetUserInfo(this.dataCon.one))
                {
                    MessageBox.Show("Successfully Changed!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    txt_Password.Password = String.Empty;
                    txt_ConfirmPass.Password = String.Empty;
                }
            }
        }

        private void btn_Activate_Click(object sender, RoutedEventArgs e)
        {
            if (txt_UserID.Text != "" || !String.IsNullOrEmpty(txt_UserID.Text))
            {
                this.dataCon.one = new UserParam();
                this.dataCon.one.UserID = Convert.ToInt32(txt_UserID.Text);

                if (_isactived)
                {

                    this.dataCon.one.IsActive = false;
                    if (this.repo.ActivateUserInfo(this.dataCon.one))
                    {
                        MessageBox.Show("Successfully Deactivated!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadAllUsers();
                    }
                }
                else
                {
                    this.dataCon.one.IsActive = true;
                    if (this.repo.ActivateUserInfo(this.dataCon.one))
                    {
                        MessageBox.Show("Successfully Activated!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadAllUsers();
                    }

                }

            }

        }
    }
}
