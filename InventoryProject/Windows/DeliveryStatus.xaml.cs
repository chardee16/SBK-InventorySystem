using InventoryProject.Models;
using InventoryProject.Models.ClientDiscountDeliveryModule;
using InventoryProject.Models.SalesModule;
using InventoryProject.Models.Users;
using InventoryProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static InventoryProject.Pages.ClientDiscountDelivery;
using static InventoryProject.Windows.DeliveryWindow;

namespace InventoryProject.Windows
{
    /// <summary>
    /// Interaction logic for DeliveryStatus.xaml
    /// </summary>
    public partial class DeliveryStatus : Window
    {
        DeliveryDataContext dataCon = new DeliveryDataContext();
        DiscountDeliveryRepository repo = new DiscountDeliveryRepository();
        UserRepository repouser = new UserRepository();
        public DeliveryStatus()
        {
            InitializeComponent();
            InitializeWorkers();
            this.PreviewKeyDown += new KeyEventHandler(HandleKeysEvent);
            this.DataContext = this.dataCon;
        }

        private void HandleKeysEvent(object sender, KeyEventArgs e)
        {

        }
        private void InitializeWorkers()
        {

            this.dataCon.userparam = this.repouser.GetAllUsers().Where(u => u.IsDelivery ?? false).ToList();
            this.dataCon.Date = DateTime.Now.ToString("MM/dd/yyyy");
           
        }


        public class DeliveryDataContext : INotifyPropertyChanged
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

            List<DeliveryStatusClass> _deliverystatus;
            public List<DeliveryStatusClass> deliverystatus
            {
                get { return _deliverystatus; }
                set
                {
                    _deliverystatus = value;
                    NotifyPropertyChanged("deliverystatus");
                }
            }

            Int32 _UserID;
            public Int32 UserID
            {
                get
                {
                    return _UserID;
                }
                set
                {
                    if (value != _UserID)
                    {
                        _UserID = value;
                        NotifyPropertyChanged("UserID");
                    }
                }
            }


            String _Date;
            public String Date
            {
                get
                {
                    return _Date;
                }
                set
                {
                    if (value != _Date)
                    {
                        _Date = value;
                        NotifyPropertyChanged("Date");
                    }
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

        private void BTN_Load_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void BTN_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {        
            var button = sender as Button;
            if (button == null) return;

            var rowData = button.DataContext;

            dynamic delivery = rowData;

            string deliveryNo = delivery.DeliveryNo;
            string customer = delivery.CustomerName;

            MessageBox.Show($"Returning remaining items for Delivery No: {deliveryNo}, Customer: {customer}");
        }
    }
}
