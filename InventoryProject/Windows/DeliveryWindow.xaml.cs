using InventoryProject.Models;
using InventoryProject.Models.ClientDiscountDeliveryModule;
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
using static InventoryProject.Windows.CategoryWindow;

namespace InventoryProject.Windows
{
    /// <summary>
    /// Interaction logic for DeliveryWindow.xaml
    /// </summary>
    public partial class DeliveryWindow : Window
    {
        DiscountDeliveryRepository repo = new DiscountDeliveryRepository();
        DeliveryDataContext dataCon = new DeliveryDataContext();
        public DeliveryWindow()
        {
            InitializeComponent();
            btn_Edit.IsEnabled = false;
            this.DataContext = this.dataCon;
            LoadDelivery();
        }

        public void LoadDelivery()
        {
            try
            {
                this.dataCon.DeliveryClass = this.repo.GetDeliveryList();
            }
            catch (Exception)
            {

            }

        }

        public void ClearValues()
        {
            try
            {
                this.dataCon.DeliveryDescription = "";
                this.dataCon.DeliveryID = 0;
            }
            catch (Exception)
            {

            }
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Save();
            }
            catch (Exception)
            {
            }
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btn_Edit.IsEnabled = false;
                btn_Save.IsEnabled = true;
            }
            catch (Exception)
            {
            }
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btn_Edit.IsEnabled = false;
                btn_Save.IsEnabled = true;
                ClearValues();
            }
            catch (Exception)
            {
            }
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dg_category_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ItemDeliveryClass selected = (ItemDeliveryClass)dg_delivery.SelectedItem;
                this.dataCon.DeliveryID = selected.DeliveryID;
                this.dataCon.DeliveryDescription = selected.DeliveryDescription;

                btn_Save.IsEnabled = false;
                btn_Edit.IsEnabled = true;

            }
            catch (Exception)
            {

            }
        }



        public void Save()
        {
            try
            {

                string addup = "";

                if (this.dataCon.DeliveryID > 0)
                {
                    addup = "update";
                }
                else
                {
                    addup = "add";
                }

                    MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure you want to {addup} this delivery?", "CONFIRMATION", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    if (!String.IsNullOrEmpty(this.dataCon.DeliveryDescription))
                    {
                        this.dataCon.delivery = new ItemDeliveryClass();

                        this.dataCon.delivery.DeliveryDescription = this.dataCon.DeliveryDescription;
                        this.dataCon.delivery.DeliveryID = this.dataCon.DeliveryID;

                        if (this.repo.InsertDelivery(this.dataCon.delivery))
                        {
                            if (this.dataCon.DeliveryID > 0)
                            {
                                MessageBox.Show("Delivery Successfully Updated!", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show("Delivery Successfully Saved.", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            LoadDelivery();
                            ClearValues();

                        }
                        else
                        {
                            MessageBox.Show("Something went wrong.\nPlease Contact Administrator!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }


            }
            catch (Exception)
            {

            }
        }

        public class DeliveryDataContext : INotifyPropertyChanged
        {

            List<ItemDeliveryClass> _DeliveryClass;
            public List<ItemDeliveryClass> DeliveryClass
            {
                get { return _DeliveryClass; }
                set
                {
                    _DeliveryClass = value;
                    NotifyPropertyChanged("DeliveryClass");
                }
            }


            ItemDeliveryClass _delivery;
            public ItemDeliveryClass delivery
            {
                get { return _delivery; }
                set
                {
                    _delivery = value;
                    NotifyPropertyChanged("delivery");
                }
            }

            Int32 _DeliveryID;
            public Int32 DeliveryID
            {
                get
                {
                    return _DeliveryID;
                }
                set
                {
                    if (value != _DeliveryID)
                    {
                        _DeliveryID = value;
                        NotifyPropertyChanged("DeliveryID");
                    }
                }
            }



            String _DeliveryDescription;
            public String DeliveryDescription
            {
                get
                {
                    return _DeliveryDescription;
                }
                set
                {
                    if (value != _DeliveryDescription)
                    {
                        _DeliveryDescription = value;
                        NotifyPropertyChanged("DeliveryDescription");
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
    }
}
