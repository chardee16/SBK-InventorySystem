using InventoryProject.Models;
using InventoryProject.Models.ClientDiscountDeliveryModule;
using InventoryProject.Models.SalesModule;
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
        public DeliveryWindow(Int64 clientid, string firstname, string middlename, string lastname)
        {
            InitializeComponent();
            btn_Edit.IsEnabled = false;
            this.DataContext = this.dataCon;
            this.dataCon.id = clientid;
            string fullname =
                    (firstname ?? "") + " " +
                    (middlename ?? "") + " " +
                    (lastname ?? "");
            this.dataCon.ClientName = fullname.Trim();
            LoadDelivery();
        }

        public void LoadDelivery()
        {
            try
            {
                this.dataCon.ProductList = repo.GetClientProductList(this.dataCon.id);
            }
            catch (Exception)
            {

            }

        }

        public void ClearValues()
        {
            try
            {
                this.dataCon.ItemName = "";
                this.dataCon.ItemCode = 0;
                this.dataCon.Price = 0;
                this.dataCon.DiscountPrice = 0;
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
                txt_DiscountPrice.IsEnabled = true;
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
                txt_DiscountPrice.IsEnabled = false;
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
                ItemClass selected = (ItemClass)DG_Product.SelectedItem;
                this.dataCon.ItemCode = selected.ItemCode ?? 0;
                this.dataCon.ItemName = selected.ItemName;
                this.dataCon.Price = selected.Price ?? 0;
                this.dataCon.DiscountPrice = selected.DiscountPrice;

                txt_DiscountPrice.IsEnabled = false;
                btn_Save.IsEnabled = false;
                btn_Edit.IsEnabled = true;

                Dispatcher.BeginInvoke(new Action(() =>
                {
                    txt_DiscountPrice.Focus();
                    Keyboard.Focus(txt_DiscountPrice);
                }));

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

                if (this.dataCon.ItemCode > 0)
                {
                    addup = "update";
                }
                else
                {
                    addup = "add";
                    MessageBox.Show("No Item selected!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure you want to {addup} this discount?", "CONFIRMATION", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    if (!String.IsNullOrEmpty(this.dataCon.ItemName))
                    {
                        this.dataCon.item = new ItemClass();

                        this.dataCon.item.ItemCode = this.dataCon.ItemCode;
                        this.dataCon.item.ItemName = this.dataCon.ItemName;

                        if (this.repo.InsertForClientDiscount(this.dataCon.id, this.dataCon.item.ItemCode ?? 0, Convert.ToDecimal(txt_DiscountPrice.Text)))
                        {
                            if (this.dataCon.DeliveryID > 0)
                            {
                                MessageBox.Show("Discount Successfully Updated!", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show("Discount Successfully Saved.", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            LoadDelivery();
                            ClearValues();
                            txt_DiscountPrice.IsEnabled = false;
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

            List<ItemClass> _ProductList;
            public List<ItemClass> ProductList
            {
                get { return _ProductList; }
                set
                {
                    _ProductList = value;
                    NotifyPropertyChanged("ProductList");
                }
            }


            ItemClass _item;
            public ItemClass item
            {
                get { return _item; }
                set
                {
                    _item = value;
                    NotifyPropertyChanged("item");
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

            Int64 _id;
            public Int64 id
            {
                get { return _id; }
                set
                {
                    _id = value;
                    NotifyPropertyChanged("id");
                }
            }

            String _ClientName;
            public String ClientName
            {
                get
                {
                    return _ClientName;
                }
                set
                {
                    if (value != _ClientName)
                    {
                        _ClientName = value;
                        NotifyPropertyChanged("ClientName");
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

            Int64 _ItemCode;
            public Int64 ItemCode
            {
                get
                {
                    return _ItemCode;
                }
                set
                {
                    if (value != _ItemCode)
                    {
                        _ItemCode = value;
                        NotifyPropertyChanged("ItemCode");
                    }
                }
            }

            Decimal _DiscountPrice;
            public Decimal DiscountPrice
            {
                get
                {
                    return _DiscountPrice;
                }
                set
                {
                    if (value != _DiscountPrice)
                    {
                        _DiscountPrice = value;
                        NotifyPropertyChanged("DiscountPrice");
                    }
                }
            }

            Decimal _Price;
            public Decimal Price
            {
                get
                {
                    return _Price;
                }
                set
                {
                    if (value != _Price)
                    {
                        _Price = value;
                        NotifyPropertyChanged("Price");
                    }
                }
            }


            String _ItemName;
            public String ItemName
            {
                get
                {
                    return _ItemName;
                }
                set
                {
                    if (value != _ItemName)
                    {
                        _ItemName = value;
                        NotifyPropertyChanged("ItemName");
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
