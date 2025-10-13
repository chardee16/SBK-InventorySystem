using InventoryProject.Models.SalesModule;
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
    /// Interaction logic for PaymentWindow.xaml
    /// </summary>
    public partial class OtherIncomeWindow : Window
    {


        PaymentDataContext dataCon = new PaymentDataContext();
        SalesRepository repo = new SalesRepository();
        Decimal totaldiscount = 0;
        Decimal total = 0;
        public OtherIncomeWindow()
        {
            InitializeComponent();
            this.DataContext = this.dataCon;
            this.dataCon.TransactionDate = DateTime.Now.ToString("yyyy-MM-dd");       
            txt_Price.Focus();
        }

        public string Answer
        {
            get { return this.dataCon.Message; }
        }



        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want close window?", "CONFIRMATION", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                this.dataCon.Message = "0";
                this.DialogResult = true;
            }
                
        }


        private void txt_Price_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.dataCon.ChangeAmount = Math.Round(this.dataCon.TenderedAmount - this.dataCon.TotalAmount,2,MidpointRounding.AwayFromZero);
            }
        }


        private void SaveTransaction()
        {
            if (Checking())
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to post transaction?", "CONFIRMATION", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.Yes)
                {

                    if (this.dataCon.ClientID == "" || String.IsNullOrEmpty(this.dataCon.ClientID))
                    {
                        this.dataCon.ClientID = 0.ToString();
                    }
                    ListSales();
                    if (this.repo.InsertOtherIncome(this.dataCon.SaleitemList, this.dataCon.TenderedAmount, this.dataCon.ChangeAmount, this.dataCon.TaxAmount, this.dataCon.TotalAmount, this.dataCon.ClientID,this.dataCon.ClientName, this.dataCon.TransactionDate, this.dataCon.DiscountAmount,this.dataCon.Explanation))
                    {
                        MessageBox.Show("Transaction Saved.", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.dataCon.Message = "1";
                        this.DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong.\nPlease Contact Administrator!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.dataCon.Message = "0";
                        this.DialogResult = true;
                    }
                   
                }
                
            }
        }


        private void ListSales()
        {

            try
            {
                this.dataCon.SaleitemList = new List<SalesItemClass>();
                this.dataCon.SaleitemList.Add(new SalesItemClass()
                {
                    Discount = 0,
                    DiscountAmount = 0,
                    ExpiryDate = "1900-01-01",
                    ItemCode = 0,
                    ItemDescription = "OTHER INCOME",
                    Price = this.dataCon.TotalAmount,
                    Quantity = 1,
                    Total = this.dataCon.TotalAmount,
                    UnitAbbr = "",
                    UnitID = 0,

                });
            }
            catch (Exception)
            {
            }
        }

        private Boolean Checking()
        {
            if ((this.dataCon.TenderedAmount - this.dataCon.TotalAmount) < 0)
            {
                MessageBox.Show("Tendered amount not enough.","ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
                return false;
            }
            else
            {
                this.dataCon.ChangeAmount = Math.Round(this.dataCon.TenderedAmount - this.dataCon.TotalAmount, 2, MidpointRounding.AwayFromZero);
                return true;
            }
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveTransaction();
        }


        private void HandleKeys(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    MessageBoxResult messageBoxResult = MessageBox.Show("Do you want close window?", "CONFIRMATION", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        this.dataCon.Message = "0";
                        this.DialogResult = true;
                    }
                    e.Handled = true;
                    break;
                case Key.F11:
                    SaveTransaction();
                    e.Handled = true;
                    break;
            }
        }



        public class PaymentDataContext : INotifyPropertyChanged
        {
            List<SalesItemClass> _SaleitemList;
            public List<SalesItemClass> SaleitemList
            {
                get { return _SaleitemList; }
                set
                {
                    _SaleitemList = value;
                    NotifyPropertyChanged("SaleitemList");
                }
            }


            Decimal _TenderedAmount;
            public Decimal TenderedAmount
            {
                get
                {
                    return _TenderedAmount;
                }
                set
                {
                    if (value != _TenderedAmount)
                    {
                        _TenderedAmount = value;
                        NotifyPropertyChanged("TenderedAmount");
                    }
                }
            }



            Decimal _ChangeAmount;
            public Decimal ChangeAmount
            {
                get
                {
                    return _ChangeAmount;
                }
                set
                {
                    if (value != _ChangeAmount)
                    {
                        _ChangeAmount = value;
                        NotifyPropertyChanged("ChangeAmount");
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


            Decimal _TotalAmount;
            public Decimal TotalAmount
            {
                get
                {
                    return _TotalAmount;
                }
                set
                {
                    if (value != _TotalAmount)
                    {
                        _TotalAmount = value;
                        NotifyPropertyChanged("TotalAmount");
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


            String _Message;
            public String Message
            {
                get
                {
                    return _Message;
                }
                set
                {
                    if (value != _Message)
                    {
                        _Message = value;
                        NotifyPropertyChanged("Message");
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


            String _TransactionDate;
            public String TransactionDate
            {
                get
                {
                    return _TransactionDate;
                }
                set
                {
                    if (value != _TransactionDate)
                    {
                        _TransactionDate = value;
                        NotifyPropertyChanged("TransactionDate");
                    }
                }
            }

            String _Explanation;
            public String Explanation
            {
                get
                {
                    return _Explanation;
                }
                set
                {
                    if (value != _Explanation)
                    {
                        _Explanation = value;
                        NotifyPropertyChanged("Explanation");
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


            public event PropertyChangedEventHandler PropertyChanged;
            private void NotifyPropertyChanged(String info)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(info));
                }
            }
        }

        private void txt_DisCount_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                totaldiscount = total - this.dataCon.DiscountAmount;
                this.dataCon.TotalAmount = totaldiscount;
            }
            catch (Exception)
            {
                
            }
        }

        private void txt_Price_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                this.dataCon.TotalAmount = this.dataCon.TenderedAmount;
            }
            catch (Exception)
            {

            }
        }
    }
}
