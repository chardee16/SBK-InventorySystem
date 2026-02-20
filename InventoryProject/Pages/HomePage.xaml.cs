using InventoryProject.Models.SalesReportModule;
using InventoryProject.Repository;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        BackgroundWorker worker = new BackgroundWorker();
        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection PieCollection { get; set; }
        public string[] Labels { get; set; }

        //public Decimal LastYearIncome = 0;
        //public Decimal ThisYearIncome = 0;
        public string ResultIncome = "";
        public string ResultPercentageIncome = "";
        public Func<double, string> Formatter { get; set; }
        ItemDataContext dataCon = new ItemDataContext();
        SaleReportRepository repo = new SaleReportRepository();
        List<string> list = new List<string>();
        public HomePage()
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
            this.DataContext = this.dataCon;

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
                this.dataCon.itemList = repo.TopSalesList();
                this.dataCon.itemIncomeList = repo.TopIncomeList();
                foreach (var item in this.dataCon.itemIncomeList)
                {
                    //txtblck_LastYearIncome.Text = (item.LastYearIncome ?? 0).ToString();
                    this.dataCon.LastYearIncome = item.LastYearIncome ?? 0;
                    //txtblck_ThisYearIncome.Text = (item.ThisYearIncome ?? 0).ToString();
                    this.dataCon.ThisYearIncome = item.ThisYearIncome ?? 0;
                }


                break;
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {

               

                SeriesCollection = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = DateTime.Now.ToShortDateString(),
                        Values = new ChartValues<double> {  },
                    }
                };

                SeriesCollection.Add(new ColumnSeries
                {
                    Title = "TOP SALES",
                    Values = new ChartValues<double> { }
                });

                //Pie Graph        

                Func<ChartPoint, string> labelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

                PieCollection = new SeriesCollection { };
                foreach (var item in this.dataCon.itemList)
                {
                    list.Add(item.ItemDescription);
                    SeriesCollection[1].Values.Add(Convert.ToDouble(item.Sales));

                    PieCollection.Add(new PieSeries
                    {
                        Title = item.ItemDescription,
                        Values = new ChartValues<double> { Convert.ToDouble(item.Sales) },
                        DataLabels = true,
                        LabelPoint = labelPoint,
                    }
                    );
                }

                Labels = list.ToArray();
                Formatter = value => value.ToString("N");

                labelPoint = chartPoint =>
                   string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

                var stocks = this.dataCon.itemList.FirstOrDefault();
                if (stocks != null)
                {
                    TotalStocks = stocks.TotalStocks;
                    RemainingStocks = stocks.Sales;
                }

                txtblck_LastYearIncome.Text = this.dataCon.LastYearIncome.ToString();
                txtblck_ThisYearIncome.Text = this.dataCon.ThisYearIncome.ToString();
                txtblck_ResultIncome.Text = GetResult(this.dataCon.LastYearIncome, this.dataCon.ThisYearIncome);
                txtblck_ResultPercentageIncome.Text = GetPercentageResult(this.dataCon.LastYearIncome, this.dataCon.ThisYearIncome);

                DataContext = this;

             


            }
            catch (Exception)
            {
                //throw;
            }


         

        }

        private double _totalStocks;
        public double TotalStocks
        {
            get => _totalStocks;
            set { _totalStocks = value; OnPropertyChanged(); }
        }

        private double _remainingStocks;
        public double RemainingStocks
        {
            get => _remainingStocks;
            set { _remainingStocks = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string n = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));

        public string GetResult(Decimal? LastYearIncome, Decimal? ThisYearIncome)
        {
            string result = "";
            decimal? increase = 0;
            decimal? decrease = 0;

            if (ThisYearIncome > LastYearIncome)//Increase
            {
                increase = ThisYearIncome - LastYearIncome;

                result = "This is an increase of "+ string.Format("{0:#,##0.00}", increase);
            }
            else//Decrease
            {
                decrease = LastYearIncome - ThisYearIncome;
                result = "This is a decrease of "+ string.Format("{0:#,##0.00}", decrease);
            }

            return result;
        }
        public string GetPercentageResult(Decimal? LastYearIncome, Decimal? ThisYearIncome)
        {
            string result = "";
            decimal? increase = 0;
            decimal? decrease = 0;

            if (ThisYearIncome > LastYearIncome)//Increase
            {
                //% increase = Increase ÷ Original Number × 100.
                increase = ThisYearIncome - LastYearIncome * 100;

                result = "an increase of " + string.Format("{0:#,##0.00}", increase) + "%";
            }
            else//Decrease
            {
                //% Decrease = Decrease ÷ Original Number × 100
                decrease = LastYearIncome - ThisYearIncome * 100;
                result = "a decrease of " + string.Format("{0:#,##0.00}", decrease) +"%";
            }

            return result;
        }

        public Func<ChartPoint, string> PointLabel { get; set; }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }

        public class ItemDataContext : INotifyPropertyChanged
        {


            List<TopSales> _itemList;
            public List<TopSales> itemList
            {
                get { return _itemList; }
                set
                {
                    _itemList = value;
                    NotifyPropertyChanged("itemList");
                }
            }

            List<TopIncome> _itemIncomeList;
            public List<TopIncome> itemIncomeList
            {
                get { return _itemIncomeList; }
                set
                {
                    _itemIncomeList = value;
                    NotifyPropertyChanged("itemIncomeList");
                }
            }

            Decimal? _ThisYearIncome;
            public Decimal? ThisYearIncome
            {
                get
                {
                    return _ThisYearIncome;
                }
                set
                {
                    if (value != _ThisYearIncome)
                    {
                        _ThisYearIncome = value;
                        NotifyPropertyChanged("ThisYearIncome");
                    }
                }
            }


            Decimal? _LastYearIncome;
            public Decimal? LastYearIncome
            {
                get
                {
                    return _LastYearIncome;
                }
                set
                {
                    if (value != _LastYearIncome)
                    {
                        _LastYearIncome = value;
                        NotifyPropertyChanged("LastYearIncome");
                    }
                }
            }


            String _ResultIncome;
            public String ResultIncome
            {
                get
                {
                    return _ResultIncome;
                }
                set
                {
                    if (value != _ResultIncome)
                    {
                        _ResultIncome = value;
                        NotifyPropertyChanged("RemaininResultIncomegStocks");
                    }
                }
            }

            String _ResultPercentageIncome;
            public String ResultPercentageIncome
            {
                get
                {
                    return _ResultPercentageIncome;
                }
                set
                {
                    if (value != _ResultPercentageIncome)
                    {
                        _ResultPercentageIncome = value;
                        NotifyPropertyChanged("ResultPercentageIncome");
                    }
                }
            }

            Int32 _ItemCount;
            public Int32 ItemCount
            {
                get
                {
                    return _ItemCount;
                }
                set
                {
                    if (value != _ItemCount)
                    {
                        _ItemCount = value;
                        NotifyPropertyChanged("ItemCount");
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
}
