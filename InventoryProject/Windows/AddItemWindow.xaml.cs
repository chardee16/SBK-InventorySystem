using InventoryProject.Models;
using InventoryProject.Models.ItemModule;
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
    /// Interaction logic for AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {

        BackgroundWorker worker = new BackgroundWorker();
        ItemRepository repo = new ItemRepository();
        ItemDataContext dataCon = new ItemDataContext();

        public AddItemWindow()
        {
            InitializeComponent();
            InitializeWorkers();

            try
            {
                worker.RunWorkerAsync();
            }
            catch (Exception ex)
            {

            }


        }


        private void InitializeWorkers()
        {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;

        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                this.dataCon.itemCategories = repo.GetCategoryList();
                this.dataCon.unitList = repo.GetUnitList();
                break;
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.DataContext = this.dataCon;
            this.txt_ItemName.Focus();
            //this.cmb_ClientStatus.ItemsSource = this.dataCon.clientStatusList;
 
        }





        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                item.ItemName = this.dataCon.ItemName;
                item.ItemDescription = this.dataCon.ItemDescription;
                item.CategoryID = this.dataCon.CategoryID;
                item.Price = this.dataCon.Price;
                item.Barcode = this.dataCon.Barcode;
                item.Stock = this.dataCon.Stock;
                item.UnitID = this.dataCon.UnitID;

                if (this.repo.InsertItem(item))
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
                MessageBox.Show("Some data is missing!.","Error",MessageBoxButton.OK,MessageBoxImage.Error);
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
            else if (this.dataCon.CategoryID <= 0)
            {
                return false;
            }
            else if (this.dataCon.Price <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }





    }









    public class ItemDataContext : INotifyPropertyChanged
    {

        List<ItemCategoryClass> _itemCategories;
        public List<ItemCategoryClass> itemCategories
        {
            get { return _itemCategories; }
            set
            {
                _itemCategories = value;
                NotifyPropertyChanged("itemCategories");
            }
        }


        List<UnitClass> _unitList;
        public List<UnitClass> unitList
        {
            get { return _unitList; }
            set
            {
                _unitList = value;
                NotifyPropertyChanged("unitList");
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


        Int64 _Stock;
        public Int64 Stock
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
