using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportAppServer;
using InventoryProject.Models;
using InventoryProject.Models.ClientDiscountDeliveryModule;
using InventoryProject.Models.SalesModule;
using InventoryProject.Models.Users;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace InventoryProject.Pages
{
    /// <summary>
    /// Interaction logic for ClientDiscountDelivery.xaml
    /// </summary>
    public partial class ClientDiscountDelivery : Page
    {
        DiscountDataContext dataCon = new DiscountDataContext();
        DiscountDeliveryRepository repo = new DiscountDeliveryRepository();
        List<SalesItemClass> salesItem = new List<SalesItemClass>();
        ReportDocument report = new ReportDocument();
        UserRepository repouser = new UserRepository();
        ObservableCollection<ItemClass> filter;
        private ICollectionView MyData;
        string SearchText = string.Empty;
        public ClientDiscountDelivery()
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

            //this.dataCon.ProductList = repo.GetClientProductList(Convert.ToInt64(this.dataCon.id));
            this.dataCon.ProductList = repo.GetProductList();

            //this.dataCon.DeliveryClass = this.repo.GetDeliveryList();
            filter = new ObservableCollection<ItemClass>(this.dataCon.ProductList);
            DG_Products.ItemsSource = filter;
            MyData = CollectionViewSource.GetDefaultView(filter);
        }

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

        private void DG_Products_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //if (this.dataCon.id == null)
            //{
            //    MessageBox.Show("Please select client first!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
            //else
            //{
                DoubleClick();
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    txt_Quantity.Focus();
                    Keyboard.Focus(txt_Quantity);
                }));
            //}
        }

        public void DoubleClick()
        {
            try
            {
                this.dataCon.Quantity = 0;
                this.dataCon.DiscountPrice = 0;
                //this.dataCon.Price = 0;
                ItemClass selected = (ItemClass)DG_Products.SelectedItem;
                this.dataCon.ItemCode = selected.ItemCode ?? 0;
                this.dataCon.ItemName = selected.ItemName;
                this.dataCon.Price = selected.Price ?? 0;
                this.dataCon.Stock = selected.Stock ?? 0;
                this.dataCon.DiscountPrice = selected.DiscountPrice;
            }
            catch
            {

            }


        }

        private void btn_Find_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (this.dataCon.SaleitemList != null)
                {
                    if (this.dataCon.SaleitemList.Count == 0)
                    {
                        ClientFindWindow subWindow = new ClientFindWindow(this);
                        subWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show(
                                           "There is an existing delivery queue. Are you sure you want to remove it and select another client?",
                                           "Confirm Action",
                                           MessageBoxButton.YesNo,
                                           MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            ClientFindWindow subWindow = new ClientFindWindow(this);
                            subWindow.ShowDialog();
                        }
                        else
                        {
                            return;
                        }

                    }
                   
                }
                else
                {
                    ClientFindWindow subWindow = new ClientFindWindow(this);
                    subWindow.ShowDialog();
                }

                   
            }
            catch (Exception)
            {

            }
        }

        public void LoadClient(string id, string name)
        {
            this.dataCon.id = id;
            this.dataCon.ClientName = name;

            this.dataCon.ItemCode = 0;
            this.dataCon.ItemName = "";
            this.dataCon.Quantity = 0;
            this.dataCon.DiscountPrice = 0;
            this.dataCon.Price = 0;
            this.dataCon.SaleitemList = new ObservableCollection<SalesItemClass>();
            this.salesItem = new List<SalesItemClass>();
            this.dataCon.TotalPrice = 0;
            this.dataCon.DeliveryID = 0;
            this.dataCon.UserID = 0;

            InitializeWorkers();



        }

        public class DiscountDataContext : INotifyPropertyChanged
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


            String _Firstname;
            public String Firstname
            {
                get
                {
                    return _Firstname;
                }
                set
                {
                    if (value != _Firstname)
                    {
                        _Firstname = value;
                        NotifyPropertyChanged("Firstname");
                    }
                }
            }

            String _id;
            public String id
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

            public event PropertyChangedEventHandler PropertyChanged;
            private void NotifyPropertyChanged(String info)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(info));
                }
            }
        }

        private void btn_AddDelivery_Click(object sender, RoutedEventArgs e)
        {
            if (this.dataCon.ItemCode == 0)
            {
                MessageBox.Show(
                    "No product is selected.",
                    "Warning",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }
            else if (this.dataCon.DiscountPrice > this.dataCon.Price)
            {
                MessageBox.Show(
                    "The discount price cannot be greater than the original price.",
                    "Warning",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.dataCon.DiscountPrice = 0;
                    txt_DiscountPrice.Focus();
                    Keyboard.Focus(txt_DiscountPrice);
                }));

                return;

            }
            else if (this.dataCon.Quantity == 0)
            {
                MessageBox.Show(
                    "Quantity cannot be zero.",
                    "Warning",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }
            else
            {
                addToCartFunction();

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
                    items.ItemDescription = this.dataCon.ItemName;
                    items.Price = this.dataCon.Price;
                    items.Discount = this.dataCon.DiscountPrice;
                    items.Quantity = this.dataCon.Quantity;
                    items.Total = Math.Round((this.dataCon.Price * this.dataCon.Quantity) - (this.dataCon.DiscountPrice * this.dataCon.Quantity), 2, MidpointRounding.AwayFromZero);
                    items.DiscountAmount = Math.Round(this.dataCon.DiscountPrice * this.dataCon.Quantity, 2, MidpointRounding.AwayFromZero);

                    foreach (var item in this.salesItem)
                    {
                        if (item.ItemCode == items.ItemCode)
                        {
                            item.Quantity = item.Quantity + items.Quantity;
                            item.Total = item.Total + items.Total;
                            item.DiscountAmount = item.Discount * item.Quantity;
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

        private void Refresh()
        {
            this.dataCon.ItemCode = 0;
            this.dataCon.ItemName = "";
            this.dataCon.Price = 0;
            this.dataCon.Stock = 0;
            this.dataCon.Quantity = 0;
            this.dataCon.DiscountPrice = 0;
        }

        private void ClearValues()
        {
            this.dataCon.ItemCode = 0;
            this.dataCon.ItemName = "";
            this.dataCon.Price = 0;
            this.dataCon.Stock = 0;
            this.dataCon.Quantity = 0;
            this.dataCon.DiscountPrice = 0;
            this.dataCon.id = 0.ToString();
            this.dataCon.ClientName = "";
            this.dataCon.DeliveryID = 0;
            this.dataCon.UserID = 0;
            this.dataCon.TotalPrice = 0;
            this.dataCon.SaleitemList.Clear();
            InitializeWorkers();
        }


        private void ComputeTotal()
        {
            Decimal total = 0;
            foreach (var item in this.dataCon.SaleitemList)
            {
                total += item.Total;
            }

            this.dataCon.TotalPrice = total;
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

        private void btn_Name_Click(object sender, RoutedEventArgs e)
        {
            //DeliveryWindow subWindow = new DeliveryWindow();
            //subWindow.ShowDialog();
        }

        private void btn_SetForDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.dataCon.UserID == 0)
                {
                    MessageBox.Show("Please select delivery!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        cmb_Delivery.Focus();
                        Keyboard.Focus(cmb_Delivery);
                        cmb_Delivery.IsDropDownOpen = true;
                    }));
                }
                else
                {
                    if (this.dataCon.SaleitemList != null)
                    {
                        SetDeliveryFunction();

                    }
                    else
                    {
                        MessageBox.Show(
                                            "No items to be delivered.",
                                            "Warning",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Warning
                                        );
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetDeliveryFunction()
        {
            try
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to set these item(s) for delivery?", "CONFIRMATION", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    if (this.repo.InsertForDelivery(this.dataCon.SaleitemList, Convert.ToInt64(this.dataCon.id), this.dataCon.UserID, DateTime.Now.ToString("yyyy/MM/dd")))
                    {
                        MessageBox.Show("Items have been set for delivery.", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);



                        MessageBoxResult messageBoxResult2 = MessageBox.Show("Do you want to print the delivery document?", "CONFIRMATION", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes);
                        if (messageBoxResult2 == MessageBoxResult.Yes)
                        {
                            UserParam item2 = (UserParam)cmb_Delivery.SelectedItem;
                            GenerateDeliveryInventory(item2.Firstname);

                        }



                            
                        ClearValues();
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong.\nPlease Contact Administrator!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

                    }

                }
            }
            catch (Exception ex)
            {

            }

        }


        private void GenerateDeliveryInventory(String FirstName)
        {
            try
            {
                this.report = new Template.DeliveryInventory();

                this.report.SetDataSource(this.dataCon.SaleitemList);
                this.report.SetParameterValue("DeliveryName", FirstName);

                Report Viewer = new Report();
                Viewer.cryRpt = this.report;
                Viewer.printing = this.report;
                Viewer._CrystalReport.ViewerCore.ReportSource = report;

                Viewer._CrystalReport.ViewerCore.Zoom(80);
                Viewer.ShowDialog();
            }
            catch (Exception)
            {
            }  
        
        }

        private void btn_ShowLogs_Click(object sender, RoutedEventArgs e)
        {
            DeliveryStatus window = new DeliveryStatus();
            window.ShowDialog();
        }
    }
}
