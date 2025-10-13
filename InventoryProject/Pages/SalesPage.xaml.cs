using InventoryProject.Models;
using InventoryProject.Models.SalesModule;
using InventoryProject.Repository;
using InventoryProject.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryProject.Pages
{
    /// <summary>
    /// Interaction logic for SalesPage.xaml
    /// </summary>
    public partial class SalesPage : Page
    {
        public ObservableCollection<ItemClass> SaleItemList;
        BackgroundWorker worker = new BackgroundWorker();
        SalesDataContext dataCon = new SalesDataContext();
        SalesRepository repo = new SalesRepository();
        List<SalesItemClass> salesItem = new List<SalesItemClass>();



        private ICollectionView MyData;
        string SearchText = string.Empty;
        int currentRow = 0, currentColumn = 1;
        string username = "";
        public SalesPage(string user)
        {
            InitializeComponent();
            InitializeWorkers();
            this.PreviewKeyDown += new KeyEventHandler(HandleKeysEvent);
            this.DataContext = this.dataCon;

            try
            {
                username = user;
                worker.RunWorkerAsync();
            }
            catch (Exception ex)
            {

            }
            txt_Search.Focus();
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
                this.dataCon.itemList = repo.GetItemList();
                break;
            }
        }

        public void LoadClient(string id, string name)
        {
            this.dataCon.ClientID = id;
            this.dataCon.ClientName = name;
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {           
            SaleItemList = new ObservableCollection<ItemClass>(this.dataCon.itemList);
            DG_Items.ItemsSource = SaleItemList;
            MyData = CollectionViewSource.GetDefaultView(SaleItemList);
            DG_Items.Focus();
            DG_Items.SelectedIndex = 0;
            txt_Search.Focus();
        }

        private void HandleKeys(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    e.Handled = true;
                    Refresh();
                    break;
                case Key.Enter:
                    if (txt_DiscountAmount.IsFocused)
                    {
                        if (this.dataCon.DiscountAmount > 0)
                        {
                            this.dataCon.Discount = Math.Round(100 * (this.dataCon.Price - (this.dataCon.Price - this.dataCon.DiscountAmount)) / this.dataCon.Price, 2, MidpointRounding.AwayFromZero);
                            computeDiscount();
                            txt_Search.Focus();                           
                        }
                    }
                    else
                    {
                        selectedItemFunction();
                        computeDiscount();
                    }

                    txt_Search.SelectAll();
                    txt_Search.Focus();
                    e.Handled = true;
                    break;
                case Key.F11:
                    PaymentFunction();
                    e.Handled = true;
                    break;
            }
        }

        public void RemoveAllList()
        {
            try
            {
                this.salesItem.Clear();
                ReloadPage();
            }
            catch (Exception)
            {

            }

        }

        private void txt_Search_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
            {
                if (Keyboard.IsKeyDown(Key.Enter))
                {
                    addToCartFunction();
                }
                else if (Keyboard.IsKeyDown(Key.P))
                {
                    PaymentFunction();
                }
                else if (Keyboard.IsKeyDown(Key.R))
                {
                    RemoveAllList();
                }
                else if (Keyboard.IsKeyDown(Key.C))
                {
                    RemoveAllList();
                }
            }
            else if (e.Key == Key.Enter)
            {
                selectedItemFunction();
                e.Handled = true;
            }
            else if (e.Key == Key.Add)
            {
                if (this.dataCon.ItemCode != 0)
                {
                    this.dataCon.Quantity = this.dataCon.Quantity + 1;
                }
                
                e.Handled = true;
            }
            else if (e.Key == Key.Subtract)
            {
                if (this.dataCon.Quantity > 0)
                {
                    this.dataCon.Quantity = this.dataCon.Quantity - 1;
                }
                e.Handled = true;
            }
            else if (e.Key == Key.Tab)
            {
                this.txt_Discount.Focus();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                Refresh();
                e.Handled = true;
            }
        }


        private void selectedItemFunction()
        {
            try
            {
                ItemClass selected = (ItemClass)DG_Items.SelectedItem;

                if (selected.ItemCode == this.dataCon.ItemCode)
                {
                    this.dataCon.Quantity = this.dataCon.Quantity + 1;
                }
                else
                {
                    this.dataCon.ItemCode = selected.ItemCode;
                    this.dataCon.ItemName = selected.ItemName;
                    this.dataCon.ItemDescription = selected.ItemDescription;
                    this.dataCon.CategoryID = selected.CategoryID;
                    this.dataCon.Price = selected.Price;
                    this.dataCon.Stock = selected.Stock;
                    this.dataCon.UnitID = selected.UnitID;
                    this.dataCon.UnitAbbr = selected.UnitAbbr;
                    this.dataCon.Barcode = selected.Barcode;
                    this.dataCon.Quantity = 1;
                    this.dataCon.ExpiryDate = selected.ExpiryDate;
                }

                
            }
            catch
            {

            }
        }



        private void addToCartFunction()
        {
            try
            {
                if (this.dataCon.Stock > 0)
                {
                    Boolean isTheSame = false;
                    SalesItemClass items = new SalesItemClass();
                    items.ItemCode = this.dataCon.ItemCode;
                    items.ItemDescription = this.dataCon.ItemDescription;
                    items.Price = this.dataCon.Price;
                    items.Discount = this.dataCon.Discount;
                    items.DiscountAmount = Math.Round( this.dataCon.DiscountAmount * this.dataCon.Quantity, 2 , MidpointRounding.AwayFromZero);
                    items.Quantity = this.dataCon.Quantity;
                    items.Total = Math.Round((this.dataCon.Price * this.dataCon.Quantity) - (this.dataCon.DiscountAmount * this.dataCon.Quantity), 2, MidpointRounding.AwayFromZero);
                    items.ExpiryDate = this.dataCon.ExpiryDate;
                    items.UnitAbbr = this.dataCon.UnitAbbr;
                    items.UnitID = this.dataCon.UnitID;
                    //items.Total = Math.Round((this.dataCon.Price * this.dataCon.Quantity) - ((this.dataCon.Price * this.dataCon.Quantity) * (this.dataCon.Discount / 100 )), 2, MidpointRounding.AwayFromZero);

                    foreach (var item in this.salesItem)
                    {
                        if (item.ItemCode == items.ItemCode)
                        {
                            item.Quantity = item.Quantity + items.Quantity;
                            item.Total = item.Total + items.Total;
                            isTheSame = true;
                        }
                    }
                    if (!isTheSame)
                    {
                        this.salesItem.Add(items);
                    }

                    
                    this.dataCon.SaleitemList = new ObservableCollection<SalesItemClass>(this.salesItem);
                    ComputeTotal();
                    Refresh();
                }
            }
            catch
            {

            }
           
        }


        private void ComputeTotal()
        {
            Decimal total = 0;
            foreach (var item in this.dataCon.SaleitemList)
            {
                total += item.Total;
            }

            //this.dataCon.TaxAmount = Math.Round((total * Convert.ToDecimal(0.12)), 2, MidpointRounding.AwayFromZero);
            this.dataCon.TaxAmount = 0;

            this.dataCon.TotalPrice = total + this.dataCon.TaxAmount;
        }


        private void Refresh()
        {
            this.dataCon.ItemCode = 0;
            this.dataCon.ItemName = "";
            this.dataCon.ItemDescription = "";
            this.dataCon.CategoryID = 1;
            this.dataCon.Price = 0;
            this.dataCon.Stock = 0;
            this.dataCon.UnitID = 1;
            this.dataCon.UnitAbbr = "";
            this.dataCon.Barcode = "";
            this.dataCon.Quantity = 0;
            this.dataCon.Discount = 0;
            this.dataCon.DiscountAmount = 0;
            this.dataCon.ClientID = "";
            this.dataCon.ClientName = "";
        }
        
        private void ReloadPage()
        {
            Refresh();
            this.dataCon.SaleitemList = new ObservableCollection<SalesItemClass>();
            this.dataCon.TaxAmount = 0;
            this.dataCon.TotalPrice = 0;
            try
            {
                worker.RunWorkerAsync();
            }
            catch (Exception ex)
            {

            }
            txt_Search.Focus();
        }

        private void txt_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = sender as TextBox;
            SearchText = t.Text.ToString();
            MyData.Filter = FilterData;

            DG_Items.SelectedIndex = 0;
        }

        private bool FilterData(object item)
        {
            var value = (ItemClass)item;
            if (value == null || value.ItemCode <= 0)
                return false;
            return Convert.ToString(value.ItemCode).Contains(SearchText.ToLower()) || value.Barcode.ToLower().Contains(SearchText.ToLower()) || value.ItemDescription.ToLower().Contains(SearchText.ToLower()) || value.ItemName.ToLower().Contains(SearchText.ToLower());       
        }


        private void SelectedCell_Click(object sender, RoutedEventArgs e)
        {
           
            DataGridCell cell = sender as DataGridCell;
            if (cell.Column.DisplayIndex != this.currentColumn)
            {
                cell.IsSelected = false;
            }
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while ((dep != null) && !(dep is DataGridRow))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            DataGridRow row = dep as DataGridRow;
            this.currentRow = row.GetIndex();
            
        }

        private void DG_Items_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void btn_AddCart_Click(object sender, RoutedEventArgs e)
        {
            addToCartFunction();
            txt_Search.Focus();
            txt_Search.SelectAll();
        }

        private void txt_Discount_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.dataCon.Discount > 0)
                {
                    computeDiscount();
                    e.Handled = true;
                }
            }
            else if (e.Key == Key.Tab)
            {
                txt_DiscountAmount.Focus();
                e.Handled = true;
            }
        }


        private void computeDiscount()
        {
            this.dataCon.DiscountAmount = Math.Round(this.dataCon.Price * (this.dataCon.Discount / 100), 2, MidpointRounding.AwayFromZero);
        }

        private void txt_DiscountAmount_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.dataCon.DiscountAmount > 0)
                {
                    this.dataCon.Discount = Math.Round(100 * (this.dataCon.Price - (this.dataCon.Price - this.dataCon.DiscountAmount)) / this.dataCon.Price, 2, MidpointRounding.AwayFromZero);
                }
            }
            else if (e.Key == Key.Tab)
            {
                txt_Search.Focus();
                e.Handled = true;
            }
        }

        private void btn_Payment_Click(object sender, RoutedEventArgs e)
        {
            PaymentFunction();
            txt_Search.Focus();
            txt_Search.SelectAll();
        }


        private void PaymentFunction()
        {
            try
            {
                PaymentWindow payment = new PaymentWindow(new List<SalesItemClass>(this.dataCon.SaleitemList), this.dataCon.TaxAmount, this.dataCon.TotalPrice, this.dataCon.ClientID, this.dataCon.ClientName, username);
                payment.Topmost = true;
                payment.ShowInTaskbar = false;
                if (payment.ShowDialog() == true)
                {
                    if (payment.Answer.Equals("1"))
                    {
                        ReloadPage();
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
           
        }

        private void btn_client_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClientFindWindow subWindow = new ClientFindWindow(this);
                subWindow.ShowDialog();
                txt_Search.Focus();
                txt_Search.SelectAll();
            }
            catch (Exception)
            {

            }
         
        }

        private void DG_Cart_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                e.Handled = true;
                SalesItemClass item2 = (SalesItemClass)DG_Cart.SelectedItem;
                var selecteditem = DG_Cart.SelectedItem as SalesItemClass;
                if (selecteditem != null)
                {
                    this.salesItem.Remove(selecteditem);
                    this.dataCon.SaleitemList.Remove(selecteditem);
                    ComputeTotal();
                    Refresh();
                }
                txt_Search.Focus();
                txt_Search.SelectAll();
            }
        }

        private void btn_OtherIncome_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OtherIncomeWindow subWindow = new OtherIncomeWindow();
                subWindow.ShowDialog();
                txt_Search.Focus();
                txt_Search.SelectAll();
            }
            catch (Exception)
            {

            }
        }

        private void btn_RemoveAll_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to remove all item(s)?", "WARNING", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                RemoveAllList();
            }
            
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to cancel current work?", "QUESTION", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                RemoveAllList();
            }
          
        }

        private void HandleKeysEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                if (currentRow > 0)
                {
                    try
                    {
                        int previousIndex = DG_Items.SelectedIndex - 1;
                        if (previousIndex < 0)
                            return; DG_Items.SelectedIndex = previousIndex; DG_Items.ScrollIntoView(DG_Items.Items[currentRow]);
                    }
                    catch (Exception ex)
                    {
                        e.Handled = true;
                    }
                }
            }
            else if (e.Key == Key.Down)
            {
                if (currentRow < DG_Items.Items.Count - 1)
                {
                    try
                    {
                        int nextIndex = DG_Items.SelectedIndex + 1;

                        if (nextIndex > DG_Items.Items.Count - 1)
                            return;

                        DG_Items.SelectedIndex = nextIndex;
                        DG_Items.ScrollIntoView(DG_Items.Items[currentRow]);
                    }
                    catch (Exception ex)
                    {
                        e.Handled = true;
                    }
                } // end if (this.SelectedOverride > 0)            
            } // end else if (e.Key == Key.Down)
            else if (e.Key == Key.End && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                DG_Items.SelectedItem = SaleItemList.Last();
                DG_Items.ScrollIntoView(DG_Items.Items[DG_Items.SelectedIndex]);
            }
        }









    }


    public class SalesDataContext : INotifyPropertyChanged
    {
        List<ItemClass> _itemList;
        public List<ItemClass> itemList
        {
            get { return _itemList; }
            set
            {
                _itemList = value;
                NotifyPropertyChanged("itemList");
            }
        }


        ObservableCollection<SalesItemClass> _SaleitemList;
        public ObservableCollection<SalesItemClass> SaleitemList
        {
            get { return _SaleitemList; }
            set
            {
                _SaleitemList = value;
                NotifyPropertyChanged("SaleitemList");
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

        String _ClientID;
        public String ClientID
        {
            get
            {
                return _ClientID;
            }
            set
            {
                if (value != _ClientID)
                {
                    _ClientID = value;
                    NotifyPropertyChanged("ClientID");
                }
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


        Decimal _Discount;
        public Decimal Discount
        {
            get
            {
                return _Discount;
            }
            set
            {
                if (value != _Discount)
                {
                    _Discount = value;
                    NotifyPropertyChanged("Discount");
                }
            }
        }

        Decimal _DiscountAmount;
        public Decimal DiscountAmount
        {
            get
            {
                return _DiscountAmount;
            }
            set
            {
                if (value != _DiscountAmount)
                {
                    _DiscountAmount = value;
                    NotifyPropertyChanged("DiscountAmount");
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


        Decimal _Quantity;
        public Decimal Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                if (value != _Quantity)
                {
                    _Quantity = value;
                    NotifyPropertyChanged("Quantity");
                }
            }
        }



        Decimal _TotalPrice;
        public Decimal TotalPrice
        {
            get
            {
                return _TotalPrice;
            }
            set
            {
                if (value != _TotalPrice)
                {
                    _TotalPrice = value;
                    NotifyPropertyChanged("TotalPrice");
                }
            }
        }

        Decimal _TaxAmount;
        public Decimal TaxAmount
        {
            get
            {
                return _TaxAmount;
            }
            set
            {
                if (value != _TaxAmount)
                {
                    _TaxAmount = value;
                    NotifyPropertyChanged("TaxAmount");
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
