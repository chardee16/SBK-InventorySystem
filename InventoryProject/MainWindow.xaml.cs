using InventoryProject.Models.Users;
using InventoryProject.Pages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ItemDataContext dataCon = new ItemDataContext();
        UserRepository repo = new UserRepository();
        int id;
        string username = "";
        public MainWindow(int userid, string user)
        {
            InitializeComponent();

            MainContent.Content = new HomePage();
            txt_home.Foreground = Brushes.White;
            txt_Inventory.Foreground = Brushes.White;
            txt_POS.Foreground = Brushes.White;
            txt_InventoryManagement.Foreground = Brushes.White;
            txt_SalesReport.Foreground = Brushes.White;
            txt_Client.Foreground = Brushes.White;
            txt_UserManagement.Foreground = Brushes.White;
            txt_ClientDiscount.Foreground = Brushes.White;
            id = userid;
            username = user;
            this.dataCon.privilege = this.repo.GetUserLogin(id);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            GridCursor.Margin = new Thickness(10 + (150 * index), 90, 0, 0);

            switch (index)
            {
                case 0:
                    MainContent.Content = new HomePage();
                    txt_home.Foreground = Brushes.Black;
                    txt_Inventory.Foreground = Brushes.White;
                    txt_POS.Foreground = Brushes.White;
                    txt_InventoryManagement.Foreground = Brushes.White;
                    txt_SalesReport.Foreground = Brushes.White;
                    txt_Client.Foreground = Brushes.White;
                    txt_UserManagement.Foreground = Brushes.White;
                    txt_ClientDiscount.Foreground = Brushes.White;
                    break;
                case 1:
                    UserPrivileges a = this.dataCon.privilege.Find(x => x.PrivilegeID == index);

                    if (a != null || a.IsAllowed)
                    {
                        MainContent.Content = new InventoryPage();
                        txt_home.Foreground = Brushes.White;
                        txt_Inventory.Foreground = Brushes.Black;
                        txt_POS.Foreground = Brushes.White;
                        txt_InventoryManagement.Foreground = Brushes.White;
                        txt_SalesReport.Foreground = Brushes.White;
                        txt_Client.Foreground = Brushes.White;
                        txt_UserManagement.Foreground = Brushes.White;
                        txt_ClientDiscount.Foreground = Brushes.White;
                    }
                    else
                    {
                        MessageBox.Show("You do not have permission to access this module!");
                    }

                 
                    break;
                case 2:
                    UserPrivileges b = this.dataCon.privilege.Find(x => x.PrivilegeID == index);

                    if (b != null)
                    {
                        MainContent.Content = new SalesPage(username);
                        txt_home.Foreground = Brushes.White;
                        txt_Inventory.Foreground = Brushes.White;
                        txt_POS.Foreground = Brushes.Black;
                        txt_InventoryManagement.Foreground = Brushes.White;
                        txt_SalesReport.Foreground = Brushes.White;
                        txt_Client.Foreground = Brushes.White;
                        txt_UserManagement.Foreground = Brushes.White;
                        txt_ClientDiscount.Foreground = Brushes.White;
                    }
                    else
                    {
                        MessageBox.Show("You do not have permission to access this module!");
                    }
               
                    break;
                case 3:
                    UserPrivileges c = this.dataCon.privilege.Find(x => x.PrivilegeID == index);

                    if (c != null)
                    {
                        MainContent.Content = new InventoryManagementPage();
                        txt_home.Foreground = Brushes.White;
                        txt_Inventory.Foreground = Brushes.White;
                        txt_POS.Foreground = Brushes.White;
                        txt_InventoryManagement.Foreground = Brushes.Black;
                        txt_SalesReport.Foreground = Brushes.White;
                        txt_Client.Foreground = Brushes.White;
                        txt_UserManagement.Foreground = Brushes.White;
                        txt_ClientDiscount.Foreground = Brushes.White;
                    }
                    else
                    {
                        MessageBox.Show("You do not have permission to access this module!");
                    }                  
                    break;
                case 4:
                    UserPrivileges d = this.dataCon.privilege.Find(x => x.PrivilegeID == index);

                    if (d != null)
                    {
                        MainContent.Content = new SalesReportPage();
                        txt_home.Foreground = Brushes.White;
                        txt_Inventory.Foreground = Brushes.White;
                        txt_POS.Foreground = Brushes.White;
                        txt_InventoryManagement.Foreground = Brushes.White;
                        txt_SalesReport.Foreground = Brushes.Black;
                        txt_Client.Foreground = Brushes.White;
                        txt_UserManagement.Foreground = Brushes.White;
                        txt_ClientDiscount.Foreground = Brushes.White;
                    }
                    else
                    {
                        MessageBox.Show("You do not have permission to access this module!");
                    }
                    break;
                case 5:
                    UserPrivileges f = this.dataCon.privilege.Find(x => x.PrivilegeID == index);

                    if (f != null)
                    {
                        MainContent.Content = new ClientInformationPage();
                        txt_home.Foreground = Brushes.White;
                        txt_Inventory.Foreground = Brushes.White;
                        txt_POS.Foreground = Brushes.White;
                        txt_InventoryManagement.Foreground = Brushes.White;
                        txt_SalesReport.Foreground = Brushes.White;
                        txt_Client.Foreground = Brushes.Black;
                        txt_UserManagement.Foreground = Brushes.White;
                        txt_ClientDiscount.Foreground = Brushes.White;
                    }
                    else
                    {
                        MessageBox.Show("You do not have permission to access this module!");
                    }
                    break;
                case 6:

                    UserPrivileges h = this.dataCon.privilege.Find(x => x.PrivilegeID == index);

                    if (h != null)
                    {
                        MainContent.Content = new UserManagementPage();
                        txt_home.Foreground = Brushes.White;
                        txt_Inventory.Foreground = Brushes.White;
                        txt_POS.Foreground = Brushes.White;
                        txt_InventoryManagement.Foreground = Brushes.White;
                        txt_SalesReport.Foreground = Brushes.White;
                        txt_Client.Foreground = Brushes.White;
                        txt_UserManagement.Foreground = Brushes.Black;
                        txt_ClientDiscount.Foreground = Brushes.White;
                    }
                    else
                    {
                        MessageBox.Show("You do not have permission to access this module!");
                    }
                   
                    break;
                case 7:
                    UserPrivileges g = this.dataCon.privilege.Find(x => x.PrivilegeID == index);

                    if (g != null)
                    {
                        //MainContent.Content = new ClientInformationPage();
                        txt_home.Foreground = Brushes.White;
                        txt_Inventory.Foreground = Brushes.White;
                        txt_POS.Foreground = Brushes.White;
                        txt_InventoryManagement.Foreground = Brushes.White;
                        txt_SalesReport.Foreground = Brushes.White;
                        txt_Client.Foreground = Brushes.White;
                        txt_UserManagement.Foreground = Brushes.White;
                        txt_ClientDiscount.Foreground = Brushes.Black;
                    }
                    else
                    {
                        MessageBox.Show("You do not have permission to access this module!");
                    }
                    break;
            }
        }

        

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MessageBox.Show(this, "Logout Successful!", "Log Out", MessageBoxButton.OK, MessageBoxImage.Information);
            LoginWindow subWindow = new LoginWindow();
            subWindow.ShowDialog();
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

            List<UserPrivileges> _privilege;
            public List<UserPrivileges> privilege
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
