using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportAppServer;
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
        SalesRepository salesrepo = new SalesRepository();
        UserRepository repouser = new UserRepository();
        ReportDocument report = new ReportDocument();
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

            Decimal _TotalSalesPrice;
            public Decimal TotalSalesPrice
            {
                get
                {
                    return _TotalSalesPrice;
                }
                set
                {
                    if (value != _TotalSalesPrice)
                    {
                        _TotalSalesPrice = value;
                        NotifyPropertyChanged("TotalSalesPrice");
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

            List<SalesItemClass> _SaleitemList2;
            public List<SalesItemClass> SaleitemList2
            {
                get { return _SaleitemList2; }
                set
                {
                    _SaleitemList2 = value;
                    NotifyPropertyChanged("SaleitemList2");
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
            try
            {

                if (this.dataCon.UserID == 0)
                {
                    MessageBox.Show("Please select delivery!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        CB_DeliveryMan.Focus();
                        Keyboard.Focus(CB_DeliveryMan);
                        CB_DeliveryMan.IsDropDownOpen = true;
                    }));
                    return;
                }
                else
                {
                    this.dataCon.deliverystatus = this.repo.GetDeliveryStatusList(Convert.ToInt64(this.dataCon.UserID), this.dataCon.Date);
                    this.dataCon.TotalSalesPrice = 0;
                    foreach (var item in this.dataCon.deliverystatus)
                    {
                        item.TotalPriceDelivered = Math.Round(item.Price * item.Delivered, 2);
                        this.dataCon.TotalSalesPrice += item.TotalPriceDelivered;
                    }

                    if (this.dataCon.deliverystatus.Count == 0)
                    {
                        MessageBox.Show(
                                "No delivery data found.",
                                "Warning",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);

                    }

                }

                  
            }
            catch (Exception)
            {

                throw;
            }
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
            try
            {

                var button = sender as Button;
                if (button == null) return;

                var rowData = button.DataContext;

                dynamic delivery = rowData;

                String itemName = delivery.ItemDescription;
                Int32 count = delivery.Remaining;

                MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure you want to return {count} remaining items for {itemName}?", "CONFIRMATION", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    this.dataCon.SaleitemList = new ObservableCollection<SalesItemClass>();
                    this.dataCon.SaleitemList2 = new List<SalesItemClass>();
                    this.dataCon.SaleitemList.Add(new SalesItemClass()
                    {
                        ItemCode = delivery.ItemCode,
                        Quantity = delivery.Remaining * -1,
                        Price = delivery.Price,
                        Discount = 0,
                        DiscountAmount = 0,
                        Total = delivery.Remaining * delivery.Price,
                        ExpiryDate = "1900-01-01"
                    });

                    this.dataCon.SaleitemList2.Add(new SalesItemClass()
                    {
                        ItemCode = delivery.ItemCode,
                        Quantity = delivery.Remaining * -1,
                        Price = delivery.Price,
                        Discount = 0,
                        DiscountAmount = 0,
                        Total = delivery.Remaining * delivery.Price,
                        ExpiryDate = "1900-01-01"
                    });


                    SetReturnFunction();
                }



            }
            catch (Exception)
            {

            }
        }


        private void SetReturnFunction()
        {
            try
            {
                

                if (this.repo.InsertForReturnItems(this.dataCon.SaleitemList, 0, this.dataCon.UserID, Convert.ToDateTime(this.dataCon.Date).ToString("yyyy/MM/dd")))
                {

                    if (this.salesrepo.InsertPayment(this.dataCon.SaleitemList2, 0, 0, 0, 0, "0", "", DateTime.Now.ToString("yyyy/MM/dd"), 0))
                    {

                        MessageBox.Show("Items have been returned.",
                                "Success",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);


                    }
                    else
                    {

                        MessageBox.Show("Something went wrong.\nPlease Contact Administrator!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Something went wrong.\nPlease Contact Administrator!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

                }


            }
            catch (Exception ex)
            {

            }

        }

        private void BTN_Generate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.dataCon.deliverystatus != null)
                {
                    GenerateReport();
                }
            }
            catch (Exception)
            {
            }
        }

        private void GenerateReport()
        {
            try
            {
                UserParam item2 = (UserParam)CB_DeliveryMan.SelectedItem;
                this.report = new Template.DeliveryReport();

                this.report.SetDataSource(this.dataCon.deliverystatus);
                this.report.SetParameterValue("DeliveryName", item2.Firstname);

                Report Viewer = new Report();
                Viewer.cryRpt = this.report;
                Viewer.printing = this.report;
                Viewer._CrystalReport.ViewerCore.ReportSource = report;

                Viewer._CrystalReport.ViewerCore.Zoom(100);
                Viewer.ShowDialog();
            }
            catch (Exception)
            {
            }

        }
    }
}
