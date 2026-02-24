using InventoryProject.Models;
using InventoryProject.Models.InventoryManagementModule;
using InventoryProject.Models.ItemModule;
using InventoryProject.Repository;
using InventoryProject.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
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

namespace InventoryProject.Pages
{
    /// <summary>
    /// Interaction logic for InventoryManagementPage.xaml
    /// </summary>
    public partial class InventoryManagementPage : Page
    {

        BackgroundWorker worker = new BackgroundWorker();
        BackgroundWorker workerProduct = new BackgroundWorker();
        InventoryDataContext dataCon = new InventoryDataContext();
        InventoryManagementRepository repo = new InventoryManagementRepository();
        ObservableCollection<ItemClass> filter;
        ItemRepository repo2 = new ItemRepository();
        Decimal totalstocks = 0;
        Decimal sumtotalstocks = 0;
        private ICollectionView MyData;
        string SearchText = string.Empty;
        public string barcodeGlobal = "";

        public InventoryManagementPage()
        {
            InitializeComponent();
            InitializeWorkers();


            try
            {
                txt_Search.Focus();
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

            workerProduct.DoWork += workerProduct_DoWork;
            workerProduct.RunWorkerCompleted += workerProduct_RunWorkerCompleted;

        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                this.dataCon.genericMedList = repo.GetGenericMedicine();



                this.dataCon.itemCategories = repo.GetCategoryList();
                foreach (var item in this.dataCon.itemCategories)
                {

                    item.CategoryID = item.id;
                }


                this.dataCon.shelveList = repo.GetShelveList();
                foreach (var item in this.dataCon.shelveList)
                {

                    item.ShelfID = item.id;
                }


                this.dataCon.supplierList = repo.GetSupplierList();



                this.dataCon.unitList = repo2.GetUnitList();


                break;
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DisableFunction();
            this.DataContext = this.dataCon;
            this.dataCon.MarkupValue = Convert.ToDecimal(14.00);
            this.dataCon.IsEditMode = false;


            try
            {
                workerProduct.RunWorkerAsync();
            }
            catch (Exception ex)
            {

            }

        }


        private void workerProduct_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                this.dataCon.ProductList = repo.GetProductList();
            

                break;
            }
        }

        private void workerProduct_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            filter = new ObservableCollection<ItemClass>(this.dataCon.ProductList);
            DG_Products.ItemsSource = filter;
            MyData = CollectionViewSource.GetDefaultView(filter);
        }
        

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (SaveChecking())
            {
                if (this.dataCon.IsEditMode)
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("Do you want edit this product?", "CONFIRMATION", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        Editfunction();
                        btn_Edit.Opacity = 1;
                    }
                }
                else
                {
                    if (this.dataCon.ItemCode == 0)
                    {
                        MessageBoxResult messageBoxResult = MessageBox.Show("Do you want add this product?", "CONFIRMATION", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            Savefunction();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Item already on the list.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
              
            }
            
        }


        private Boolean SaveChecking()
        {
            if (String.IsNullOrEmpty(this.dataCon.ItemName))
            {
                MessageBox.Show("Item name must have a value.","ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
                return false;
            }
            else if (String.IsNullOrEmpty(this.dataCon.ItemDescription))
            {
                MessageBox.Show("Item description must have a value.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (this.dataCon.GenericID == 0)
            {
                MessageBox.Show("Must select generic name.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (this.dataCon.CategoryID == 0)
            {
                MessageBox.Show("Must select category.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (this.dataCon.ShelfID == 0)
            {
                MessageBox.Show("Must select shelf.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (this.dataCon.SupplierID == 0)
            {
                MessageBox.Show("Must select a supplier.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (this.dataCon.PurchasePrice <= 0)
            {
                MessageBox.Show("Purchase price must above zero.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (this.dataCon.PurchasePrice < 0)
            {
                MessageBox.Show("Markup value must be greater than or equal to zero.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (this.dataCon.Price <= 0)
            {
                MessageBox.Show("Sellingprice must above zero.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
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


        private void Savefunction()
        {
            try
            {
                

                ItemClass item = new ItemClass();

                item.ItemName = this.dataCon.ItemName;
                item.ItemDescription = this.dataCon.ItemDescription;
                item.GenericID = this.dataCon.GenericID;
                item.CategoryID = this.dataCon.CategoryID;
                item.ShelfID = this.dataCon.ShelfID;
                item.SupplierID = this.dataCon.SupplierID;
                item.Barcode = this.dataCon.Barcode;
                item.PurchasePrice = this.dataCon.PurchasePrice;
                item.MarkupValue = this.dataCon.MarkupValue;
                item.Price = this.dataCon.Price;
                item.ExpiryDate = this.dataCon.ExpiryDate;
                item.Stock = this.dataCon.Stock;
                item.SideEffect = this.dataCon.SideEffect;
                item.UnitID = this.dataCon.UnitID;
                item.Value = this.dataCon.Value;

                if (this.repo.InsertItem(item))
                {
                    MessageBox.Show("Item successfully added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    try
                    {
                        workerProduct.RunWorkerAsync();
                    }
                    catch (Exception ex)
                    {

                    }

                    clearFunction();
                    DisableFunction();
                }
                else
                {
                    MessageBox.Show("Item failed to add.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception)
            {

            }
        }


        private void Editfunction()
        {

            if (totalstocks > this.dataCon.Stock)
            {
                sumtotalstocks = totalstocks - this.dataCon.Stock;
                sumtotalstocks = -sumtotalstocks;

                try
                {
                    ItemClass item2 = new ItemClass();
                    item2.ItemCode = this.dataCon.ItemCode;
                    item2.Stock = sumtotalstocks;
                    item2.Price = this.dataCon.Price;

                    if (this.dataCon.ExpiryDate == "No value")
                    {
                        this.dataCon.ExpiryDate = "1900-01-01";
                    }


                    item2.ExpiryDate = this.dataCon.ExpiryDate;

                    if (this.repo2.AddStock(item2))
                    { }
                    else
                    { }
                }
                catch (Exception)
                {
                    
                }
             
            }
            else
            {
                sumtotalstocks = this.dataCon.Stock - totalstocks;

                try
                {
                    ItemClass item3 = new ItemClass();
                    item3.ItemCode = this.dataCon.ItemCode;
                    item3.Stock = sumtotalstocks;
                    item3.Price = this.dataCon.Price;

                    if (this.dataCon.ExpiryDate == "No value")
                    {
                        this.dataCon.ExpiryDate = "1900-01-01";
                    }

                    item3.ExpiryDate = this.dataCon.ExpiryDate;

                    if (this.repo2.AddStock(item3))
                    { }
                    else
                    { }
                }
                catch (Exception)
                {

                }

            }

            ItemClass item = new ItemClass();
            item.ItemCode = this.dataCon.ItemCode;
            item.ItemName = this.dataCon.ItemName;
            item.ItemDescription = this.dataCon.ItemDescription;
            item.GenericID = this.dataCon.GenericID;
            item.CategoryID = this.dataCon.CategoryID;
            item.ShelfID = this.dataCon.ShelfID;
            item.SupplierID = this.dataCon.SupplierID;
            item.Barcode = this.dataCon.Barcode;
            item.PurchasePrice = this.dataCon.PurchasePrice;
            item.MarkupValue = this.dataCon.MarkupValue;
            item.Price = this.dataCon.Price;
            item.ExpiryDate = this.dataCon.ExpiryDate;
            item.Stock = this.dataCon.Stock;
            item.SideEffect = this.dataCon.SideEffect;
            item.UnitID = this.dataCon.UnitID;
            item.Value = this.dataCon.Value;
            item.ExpiryDate = this.dataCon.ExpiryDate;

            if (this.repo.UpdateItem(item))
            {
                MessageBox.Show("Item successfully updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                try
                {
                    workerProduct.RunWorkerAsync();
                }
                catch (Exception ex)
                {

                }

                clearFunction();
                DisableFunction();
            }
            else
            {
                MessageBox.Show("Item failed to add.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }



        private void clearFunction()
        {
            this.dataCon.ItemCode = 0;
            this.dataCon.ItemName = String.Empty;
            this.dataCon.ItemDescription = String.Empty;
            this.dataCon.GenericID = 0;
            this.dataCon.CategoryID = 0;
            this.dataCon.ShelfID = 0;
            this.dataCon.SupplierID = 0;
            this.dataCon.Barcode = String.Empty;
            this.dataCon.PurchasePrice = 0;
            this.dataCon.MarkupValue = 14;
            this.dataCon.Price = 0;
            this.dataCon.ExpiryDate = DateTime.Now.ToString("MM/dd/yyyy");
            this.dataCon.Stock = 0;
            this.dataCon.SideEffect = String.Empty;
            this.dataCon.IsEditMode = false;
            this.dataCon.UnitID = 0;
            this.dataCon.Value = 0;
        }

        private void DisableFunction()
        {
            this.txt_ItemName.IsReadOnly = true;
            this.txt_ItemDescription.IsReadOnly = true;
            this.cmb_GenericMedicine.IsHitTestVisible = false;
            this.cmb_Category.IsHitTestVisible = false;
            this.cmb_Shelves.IsHitTestVisible = false;
            this.cmb_Supplier.IsHitTestVisible = false;
            this.txt_Barcode.IsReadOnly = true;
            this.myCurrencyTextBox.IsReadOnly = true;
            this.txt_MarkupValue.IsReadOnly = true;
            this.txt_Price.IsReadOnly = true;
            this.dtp_ExpiryDate.IsEnabled = false;
            this.txt_Stock.IsReadOnly = true;
            this.txt_SideEffect.IsReadOnly = true;
            this.dataCon.IsEditMode = false;
            this.cmb_UnitMeasure.IsHitTestVisible = false;
            this.txt_value.IsReadOnly = true;
        }

        private void EnableFunction()
        {
            this.txt_ItemName.IsReadOnly = false;
            this.txt_ItemDescription.IsReadOnly = false;
            this.cmb_GenericMedicine.IsHitTestVisible = true;
            this.cmb_Category.IsHitTestVisible = true;
            this.cmb_Shelves.IsHitTestVisible = true;
            this.cmb_Supplier.IsHitTestVisible = true;
            this.txt_Barcode.IsReadOnly = false;
            this.myCurrencyTextBox.IsReadOnly = false;
            this.txt_MarkupValue.IsReadOnly = false;
            this.txt_Price.IsReadOnly = false;
            this.dtp_ExpiryDate.IsEnabled = true;
            this.cmb_UnitMeasure.IsHitTestVisible = true;
            this.txt_value.IsReadOnly = false;

            //if (this.dataCon.IsEditMode)
            //{
            //    this.txt_Stock.IsReadOnly = true;
            //}
            //else
            //{
                this.txt_Stock.IsReadOnly = false;
            //}

            this.txt_SideEffect.IsReadOnly = false;
        }






        private void myCurrencyTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.dataCon.Price = this.dataCon.PurchasePrice + (this.dataCon.PurchasePrice * (this.dataCon.MarkupValue/100));
            }
        }


        private void btn_New_Click(object sender, RoutedEventArgs e)
        {
            clearFunction();
            btn_Edit.IsEnabled = false;
            btn_Save.IsEnabled = true;
            btn_Save.Opacity = 1;
            EnableFunction();
        }

        private void DG_Products_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DoubleClick();
            }
            catch
            {

            }
        }

        public void DoubleClick()
        {
            try
            {
                ItemClass selected = (ItemClass)DG_Products.SelectedItem;
                this.dataCon.ItemCode = selected.ItemCode ?? 0;
                this.dataCon.ItemName = selected.ItemName;
                this.dataCon.ItemDescription = selected.ItemDescription;
                this.dataCon.GenericID = selected.GenericID ?? 0;
                this.dataCon.CategoryID = selected.CategoryID ?? 0;
                this.dataCon.ShelfID = selected.ShelfID ?? 0;
                this.dataCon.SupplierID = selected.SupplierID ?? 0;
                this.dataCon.Barcode = selected.Barcode;
                this.dataCon.PurchasePrice = selected.PurchasePrice ?? 0;
                this.dataCon.MarkupValue = selected.MarkupValue ?? 0;
                this.dataCon.Price = selected.Price ?? 0;
                this.dataCon.ExpiryDate = selected.ExpiryDate;
                this.dataCon.Stock = selected.Stock ?? 0;
                this.dataCon.SideEffect = selected.SideEffect;
                this.dataCon.UnitID = selected.UnitID ?? 0;
                this.dataCon.Value = selected.Value ?? 0;
                this.dataCon.CategoryDesc = selected.CategoryDesc;
                this.dataCon.SupplierDescription = selected.SupplierDescription;



                this.dataCon.Value = this.dataCon.PurchasePrice * this.dataCon.Stock;
                totalstocks = selected.Stock ?? 0;
                btn_Save.IsEnabled = false;
                btn_Save.Opacity = 0.5;
                btn_Edit.IsEnabled = true;
                btn_Edit.Opacity = 1;
                DisableFunction();
            }
            catch
            {

            }


        }


        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (this.dataCon.ItemCode != 0)
            {
                this.dataCon.IsEditMode = true;
                EnableFunction();
                btn_Edit.Opacity = 0.5;
                btn_Edit.IsEnabled = false;
                btn_Save.IsEnabled = true;
                btn_Save.Opacity = 1;
            }
            
        }




        private void btn_AddStock_Click(object sender, RoutedEventArgs e)
        {
            if (this.dataCon.ItemCode != 0)
            {
                ItemClass item = new ItemClass();
                item.ItemCode = this.dataCon.ItemCode;
                item.ItemName = this.dataCon.ItemName;
                item.ItemDescription = this.dataCon.ItemDescription;
                item.CategoryID = this.dataCon.CategoryID;
                item.Price = this.dataCon.Price;
                item.Stock = this.dataCon.Stock;
                item.Barcode = this.dataCon.Barcode;
                item.UnitID = this.dataCon.UnitID;

                AddStockWindow addStock = new AddStockWindow(item);
                addStock.ShowDialog();
                clearFunction();
                try
                {
                    workerProduct.RunWorkerAsync();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                MessageBox.Show("Please select product.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HandleKeys(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:

                    if (btn_Save.IsEnabled == true && btn_Edit.IsEnabled == false)
                    {
                        if (this.repo.GetDuplicateBarCode(this.dataCon.Barcode) == true)
                        {
                            MessageBox.Show("Barcode is already exist!", "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
                            clearFunction();
                            DisableFunction();
                        }
                    }
                    else
                    {
                        DoubleClick();
                        txt_Search.SelectAll();
                        txt_Search.Focus();
                    }
                                     
                   
                    e.Handled = true;
                    break;
            }
        }


        public class InventoryDataContext : INotifyPropertyChanged
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

            List<GenericMedicineClass> _genericMedList;
            public List<GenericMedicineClass> genericMedList
            {
                get { return _genericMedList; }
                set
                {
                    _genericMedList = value;
                    NotifyPropertyChanged("genericMedList");
                }
            }



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


            List<ShelveClass> _shelveList;
            public List<ShelveClass> shelveList
            {
                get { return _shelveList; }
                set
                {
                    _shelveList = value;
                    NotifyPropertyChanged("shelveList");
                }
            }

            String _SupplierDescription;
            public String SupplierDescription
            {
                get
                {
                    return _SupplierDescription;
                }
                set
                {
                    if (value != _SupplierDescription)
                    {
                        _SupplierDescription = value;
                        NotifyPropertyChanged("SupplierDescription");
                    }
                }
            }

            Int32 _id;
            public Int32 id
            {
                get
                {
                    return _id;
                }
                set
                {
                    if (value != _id)
                    {
                        _id = value;
                        NotifyPropertyChanged("id");
                    }
                }
            }


            String _CategoryDesc;
            public String CategoryDesc
            {
                get
                {
                    return _CategoryDesc;
                }
                set
                {
                    if (value != _CategoryDesc)
                    {
                        _CategoryDesc = value;
                        NotifyPropertyChanged("CategoryDesc");
                    }
                }
            }


            List<SupplierClass> _supplierList;
            public List<SupplierClass> supplierList
            {
                get { return _supplierList; }
                set
                {
                    _supplierList = value;
                    NotifyPropertyChanged("supplierList");
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



            Int32 _GenericID;
            public Int32 GenericID
            {
                get
                {
                    return _GenericID;
                }
                set
                {
                    if (value != _GenericID)
                    {
                        _GenericID = value;
                        NotifyPropertyChanged("GenericID");
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



            Int32 _ShelfID;
            public Int32 ShelfID
            {
                get
                {
                    return _ShelfID;
                }
                set
                {
                    if (value != _ShelfID)
                    {
                        _ShelfID = value;
                        NotifyPropertyChanged("ShelfID");
                    }
                }
            }


            Int32 _SupplierID;
            public Int32 SupplierID
            {
                get
                {
                    return _SupplierID;
                }
                set
                {
                    if (value != _SupplierID)
                    {
                        _SupplierID = value;
                        NotifyPropertyChanged("SupplierID");
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


            Decimal _PurchasePrice;
            public Decimal PurchasePrice
            {
                get
                {
                    return _PurchasePrice;
                }
                set
                {
                    if (value != _PurchasePrice)
                    {
                        _PurchasePrice = value;
                        NotifyPropertyChanged("PurchasePrice");
                    }
                }
            }


            Decimal _Value;
            public Decimal Value
            {
                get
                {
                    return _Value;
                }
                set
                {
                    if (value != _Value)
                    {
                        _Value = value;
                        NotifyPropertyChanged("Value");
                    }
                }
            }



            Decimal _MarkupValue;
            public Decimal MarkupValue
            {
                get
                {
                    return _MarkupValue;
                }
                set
                {
                    if (value != _MarkupValue)
                    {
                        _MarkupValue = value;
                        NotifyPropertyChanged("MarkupValue");
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



            String _SideEffect;
            public String SideEffect
            {
                get
                {
                    return _SideEffect;
                }
                set
                {
                    if (value != _SideEffect)
                    {
                        _SideEffect = value;
                        NotifyPropertyChanged("SideEffect");
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



            Boolean _IsEditMode;
            public Boolean IsEditMode
            {
                get
                {
                    return _IsEditMode;
                }
                set
                {
                    if (value != _IsEditMode)
                    {
                        _IsEditMode = value;
                        NotifyPropertyChanged("IsEditMode");
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

        //private void txt_Search_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    var filtered = filter.Where(s => s.ItemDescription.ToLower().Contains(txt_Search.Text.ToLower()));

        //    DG_Products.ItemsSource = filtered.ToList();
        //}

        //private bool FilterData(object item)
        //{
        //    var value = (ItemClass)item;
        //    if (value == null || value.ItemCode <= 0)
        //        return false;
        //    return value.Barcode.ToLower().Contains(SearchText.ToLower())
        //    || value.ItemDescription.ToLower().Contains(SearchText.ToLower())
        //    || value.ItemName.ToLower().Contains(SearchText.ToLower())
        //    || value.CategoryDescription.ToLower().Contains(SearchText.ToLower());
        //}



        private void txt_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = sender as TextBox;
            SearchText = t.Text.ToString();
            MyData.Filter = FilterData;

            DG_Products.SelectedIndex = 0;
        }

        private bool FilterData(object item)
        {
            var value = (ItemClass)item;
            if (value == null || value.ItemCode <= 0)
                return false;
            return Convert.ToString(value.ItemCode).Contains(SearchText.ToLower()) || value.Barcode.ToLower().Contains(SearchText.ToLower()) || value.ItemDescription.ToLower().Contains(SearchText.ToLower()) || value.ItemName.ToLower().Contains(SearchText.ToLower());
        }

        private void btn_AddCategory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CategoryWindow subWindow = new CategoryWindow();
                subWindow.ShowInTaskbar = false;
                subWindow.ShowDialog();
                this.dataCon.itemCategories = repo.GetCategoryList();
            }
            catch (Exception)
            {

            }
        }

        private void btn_AddStoreShelve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StoreShelveWindow subWindow = new StoreShelveWindow();
                subWindow.ShowInTaskbar = false;
                subWindow.ShowDialog();
                this.dataCon.shelveList = repo.GetShelveList();
            }
            catch (Exception)
            {

            }
        }

        private void btn_Supplier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SupplierWindow subWindow = new SupplierWindow();
                subWindow.ShowInTaskbar = false;
                subWindow.ShowDialog();
                this.dataCon.supplierList = repo.GetSupplierList();
            }
            catch (Exception)
            {

            }
        }

        private void btn_Name_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NameWindow subWindow = new NameWindow();
                subWindow.ShowInTaskbar = false;
                subWindow.ShowDialog();
                this.dataCon.genericMedList = repo.GetGenericMedicine();
            }
            catch (Exception)
            {

            }
        }

        private void btn_logs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.dataCon.ItemCode > 0)
                {
                    StockLogs subWindow = new StockLogs(this.dataCon.ItemCode);
                    subWindow.ShowInTaskbar = false;
                    subWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please select item to show logs.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);

                }


             
            }
            catch (Exception)
            {
            }
        }

        private void txt_Stock_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                this.dataCon.Value = this.dataCon.PurchasePrice * this.dataCon.Stock;
            }
            catch (Exception)
            {
            }
          
        }
    }
}
