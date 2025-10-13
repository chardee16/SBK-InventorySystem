using InventoryProject.Models;
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

namespace InventoryProject.Windows
{
    /// <summary>
    /// Interaction logic for AddStockWindow.xaml
    /// </summary>
    public partial class AddStockWindow : Window
    {
        AddStockDataContext dataCon;
        ItemRepository repo = new ItemRepository();
        public AddStockWindow(ItemClass item)
        {
            InitializeComponent();
            this.dataCon = new AddStockDataContext();
            this.DataContext = this.dataCon;

            this.dataCon.ItemCode = item.ItemCode;
            this.dataCon.ItemName = item.ItemName;
            this.dataCon.ItemDescription = item.ItemDescription;
            this.dataCon.CategoryID = item.CategoryID;
            this.dataCon.CategoryDescription = item.CategoryDescription;
            this.dataCon.Price = item.Price;
            this.dataCon.Barcode = item.Barcode;

        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            AddFunction();
        }



        private void AddFunction()
        {
            if (Validator())
            {
                ItemClass item = new ItemClass();
                item.ItemCode = this.dataCon.ItemCode;
                item.Stock = this.dataCon.Stock;
                item.Price = this.dataCon.Price;
                item.ExpiryDate = this.dataCon.ExpiryDate;

                if (this.repo.AddStock(item))
                {
                    MessageBox.Show("Item successfully added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Item failed to add.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }
            else
            {
                MessageBox.Show("Some data is missing!.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private Boolean Validator()
        {
            if (String.IsNullOrEmpty(this.dataCon.ItemName))
            {
                return false;
            }
            else if (String.IsNullOrEmpty(this.dataCon.ItemDescription))
            {
                return false;
            }
            else if (this.dataCon.ItemCode == 0)
            {
                return false;
            }
            else if (this.dataCon.Stock <= 0)
            {
                return false;
            }
            //else if (Convert.ToDateTime(this.dataCon.ExpiryDate) < DateTime.Now)
            //{
            //    MessageBox.Show("Product already Expired.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return false;
            //}
            else
            {
                return true;
            }
        }


        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }







    public class AddStockDataContext : INotifyPropertyChanged
    {

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



        String _ItemDescription;
        public String ItemDescription
        {
            get
            {
                return _ItemDescription;
            }
            set
            {
                if (value != _ItemDescription)
                {
                    _ItemDescription = value;
                    NotifyPropertyChanged("ItemDescription");
                }
            }
        }



        Int32 _CategoryID;
        public Int32 CategoryID
        {
            get
            {
                return _CategoryID;
            }
            set
            {
                if (value != _CategoryID)
                {
                    _CategoryID = value;
                    NotifyPropertyChanged("CategoryID");
                }
            }
        }


        String _CategoryDescription;
        public String CategoryDescription
        {
            get
            {
                return _CategoryDescription;
            }
            set
            {
                if (value != _CategoryDescription)
                {
                    _CategoryDescription = value;
                    NotifyPropertyChanged("CategoryDescription");
                }
            }
        }



        Int32 _UnitID;
        public Int32 UnitID
        {
            get
            {
                return _UnitID;
            }
            set
            {
                if (value != _UnitID)
                {
                    _UnitID = value;
                    NotifyPropertyChanged("UnitID");
                }
            }
        }



        String _UnitAbbr;
        public String UnitAbbr
        {
            get
            {
                return _UnitAbbr;
            }
            set
            {
                if (value != _UnitAbbr)
                {
                    _UnitAbbr = value;
                    NotifyPropertyChanged("UnitAbbr");
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



        String _Barcode;
        public String Barcode
        {
            get
            {
                return _Barcode;
            }
            set
            {
                if (value != _Barcode)
                {
                    _Barcode = value;
                    NotifyPropertyChanged("Barcode");
                }
            }
        }


        Decimal _Stock;
        public Decimal Stock
        {
            get
            {
                return _Stock;
            }
            set
            {
                if (value != _Stock)
                {
                    _Stock = value;
                    NotifyPropertyChanged("Stock");
                }
            }
        }


        String _ExpiryDate;
        public String ExpiryDate
        {
            get
            {
                return _ExpiryDate;
            }
            set
            {
                if (value != _ExpiryDate)
                {
                    _ExpiryDate = value;
                    NotifyPropertyChanged("ExpiryDate");
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
