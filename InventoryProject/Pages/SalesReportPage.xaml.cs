using CrystalDecisions.CrystalReports.Engine;
using InventoryProject.Models;
using InventoryProject.Models.InventoryManagementModule;
using InventoryProject.Models.SalesReportModule;
using InventoryProject.Repository;
using InventoryProject.Utilities;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryProject.Pages
{
    /// <summary>
    /// Interaction logic for SalesReportPage.xaml
    /// </summary>
    public partial class SalesReportPage : Page
    {
        BackgroundWorker worker = new BackgroundWorker();
        ItemDataContext dataCon = new ItemDataContext();
        SaleReportRepository repo = new SaleReportRepository();
        ReportDocument report = new ReportDocument();
        InventoryManagementRepository repo2 = new InventoryManagementRepository();
        public SalesReportPage()
        {
            InitializeComponent();
            InitializeWorkers();

            ListBoxUtility Groupings = new ListBoxUtility();
            ListBoxUtility Groupings2 = new ListBoxUtility();
 
            Groupings.selectTo(lb_name, lb_nameselected);
            Groupings.selectToCallBack += new ListBoxUtility.CallbackEventHandler(selectToCallBack);
            Groupings.selectFromCallBack += new ListBoxUtility.CallbackEventHandler(selectFromCallBack);

            Groupings2.selectTo(lb_category, lb_selectedcategory);
            Groupings2.selectToCallBack += new ListBoxUtility.CallbackEventHandler(selectToCallBack);
            Groupings2.selectFromCallBack += new ListBoxUtility.CallbackEventHandler(selectFromCallBack);

            this.dataCon.NameSelected = new List<GenericMedicineClass>();
            this.dataCon.itemCategoriesSelected = new List<ItemCategoryClass>();

            this.DataContext = this.dataCon;

            DateTime firstDayOfTheMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.dataCon.DateStart = firstDayOfTheMonth.ToString("yyyy-MM-dd");
            this.dataCon.DateEnd = firstDayOfTheMonth.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");

            this.dataCon.DateStartReport = firstDayOfTheMonth.ToString("yyyy-MM-dd");
            this.dataCon.DateEndReport = DateTime.Now.ToString("yyyy-MM-dd");

            try
            {
                worker.RunWorkerAsync();
            }
            catch (Exception ex)
            {

            }
        }

        //Select Callback
        public void selectToCallBack(ListBox From, ListBox To, ListBox ParentorChild, Boolean IsParent, Boolean MultiSelect)
        {
            switch (From.Name)
            {
                case "lb_name": selectName(From, To); break;
                case "lb_category": selectCategory(From, To); break;
                default: break;
            }
        }

        //Deselected Callback
        public void selectFromCallBack(ListBox From, ListBox To, ListBox ParentorChild, Boolean IsParent, Boolean MultiSelect)
        {
            switch (From.Name)
            {
                case "lb_name": deselectName(From, To); break;
                case "lb_category": deselectCategory(From, To); break;
                default: break;
            }
        }

        private void selectName(ListBox GlobalFrom, ListBox GlobalTo)
        {
            foreach (GenericMedicineClass s in GlobalFrom.SelectedItems)
            {
                GenericMedicineClass tpass = s;
                int last = this.dataCon.NameSelected.FindAll(t => String.Compare(t.GenericName, s.GenericName) < 1).Count;
                this.dataCon.genericMedList.Remove(s);
                this.dataCon.NameSelected.Insert(last, tpass);

            }
            GlobalFrom.Items.Refresh();
            GlobalTo.Items.Refresh();
        }

        private void deselectName(ListBox From, ListBox To)
        {
            foreach (GenericMedicineClass s in To.SelectedItems)
            {
                GenericMedicineClass tpass = s;
                int last = this.dataCon.genericMedList.FindAll(t => String.Compare(t.GenericName, s.GenericName) < 1).Count;
                this.dataCon.NameSelected.Remove(s);
                this.dataCon.genericMedList.Insert(last, tpass);
            }
            From.Items.Refresh();
            To.Items.Refresh();
        }

        private void selectCategory(ListBox GlobalFrom, ListBox GlobalTo)
        {
            foreach (ItemCategoryClass s in GlobalFrom.SelectedItems)
            {
                ItemCategoryClass tpass = s;
                int last = this.dataCon.itemCategoriesSelected.FindAll(t => String.Compare(t.CategoryDescription, s.CategoryDescription) < 1).Count;
                this.dataCon.itemCategories.Remove(s);
                this.dataCon.itemCategoriesSelected.Insert(last, tpass);

            }
            GlobalFrom.Items.Refresh();
            GlobalTo.Items.Refresh();
        }

        private void deselectCategory(ListBox From, ListBox To)
        {
            foreach (ItemCategoryClass s in To.SelectedItems)
            {
                ItemCategoryClass tpass = s;
                int last = this.dataCon.itemCategories.FindAll(t => String.Compare(t.CategoryDescription, s.CategoryDescription) < 1).Count;
                this.dataCon.itemCategoriesSelected.Remove(s);
                this.dataCon.itemCategories.Insert(last, tpass);
            }
            From.Items.Refresh();
            To.Items.Refresh();
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
                this.dataCon.itemList = repo.GetItemList(this.dataCon.DateStart,this.dataCon.DateEnd);          
                this.dataCon.genericMedList = repo2.GetGenericMedicine();
                this.dataCon.itemCategories = repo2.GetCategoryList();
                break;
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.dataCon.TotalIncome = 0;
            this.dataCon.TotalItemCount = this.dataCon.itemList.Sum(o => o.Quantity);
            this.dataCon.TotalDiscountAmount = this.dataCon.itemList.Sum(o => o.DiscountAmount);
            this.dataCon.TotalNetAmount = this.dataCon.itemList.Sum(o => o.TotalAmount);         

            foreach (var item in this.dataCon.itemList)
            {
                this.dataCon.TotalIncome += item.Income;
            }

        }


        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                worker.RunWorkerAsync();
            }
            catch (Exception ex)
            {

            }
        }


        private void btn_Generate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GenerateSales();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }


        public void GenerateSales()
        {
            try
            {

                if (this.dataCon.NameSelected.Count > 0)
                {
                    this.dataCon.NameSelectedReport = this.dataCon.NameSelected;
                }
                else
                {
                    this.dataCon.NameSelectedReport = this.dataCon.genericMedList;
                }


                if (this.dataCon.itemCategoriesSelected.Count > 0)
                {
                    this.dataCon.itemCategoriesSelectedReport = this.dataCon.itemCategoriesSelected;
                }
                else
                {
                    this.dataCon.itemCategoriesSelectedReport = this.dataCon.itemCategories;
                }


                this.dataCon.salesreport = repo.GetSalesReport(Convert.ToDateTime(this.dataCon.DateStartReport).ToString("MM/dd/yyyy"), Convert.ToDateTime(this.dataCon.DateEndReport).ToString("MM/dd/yyyy"), this.dataCon.NameSelectedReport, this.dataCon.itemCategoriesSelectedReport);

                this.report = new Template.SalesReportCrystal();

                this.report.SetDataSource(this.dataCon.salesreport);
                this.report.SetParameterValue("TodaysDate", DateTime.Now.ToString("MMMM dd, yyyy  hh:mm tt"));
              
                Report Viewer = new Report();
                Viewer.cryRpt = this.report;
                Viewer.printing = this.report;
                Viewer._CrystalReport.ViewerCore.ReportSource = report;

                Viewer._CrystalReport.ViewerCore.Zoom(150);
                Viewer.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
          

        }


        public class ItemDataContext : INotifyPropertyChanged
        {

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
        
            List<GenericMedicineClass> _NameSelected;
            public List<GenericMedicineClass> NameSelected
            {
                get
                {
                    return _NameSelected;
                }
                set
                {
                    _NameSelected = value;
                    NotifyPropertyChanged("NameSelected");
                }
            }


            List<GenericMedicineClass> _NameSelectedReport;
            public List<GenericMedicineClass> NameSelectedReport
            {
                get
                {
                    return _NameSelectedReport;
                }
                set
                {
                    _NameSelectedReport = value;
                    NotifyPropertyChanged("NameSelectedReport");
                }
            }

            List<ItemCategoryClass> _itemCategoriesSelectedReport;
            public List<ItemCategoryClass> itemCategoriesSelectedReport
            {
                get { return _itemCategoriesSelectedReport; }
                set
                {
                    _itemCategoriesSelectedReport = value;
                    NotifyPropertyChanged("itemCategoriesSelectedReport");
                }
            }

            List<ItemCategoryClass> _itemCategoriesSelected;
            public List<ItemCategoryClass> itemCategoriesSelected
            {
                get { return _itemCategoriesSelected; }
                set
                {
                    _itemCategoriesSelected = value;
                    NotifyPropertyChanged("itemCategoriesSelected");
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

            List<SalePerItemClass> _itemList;
            public List<SalePerItemClass> itemList
            {
                get { return _itemList; }
                set
                {
                    _itemList = value;
                    NotifyPropertyChanged("itemList");
                }
            }

            List<SalesReport> _salesreport;
            public List<SalesReport> salesreport
            {
                get { return _salesreport; }
                set
                {
                    _salesreport = value;
                    NotifyPropertyChanged("salesreport");
                }
            }

            List<SalesReport> _itemsstockreport;
            public List<SalesReport> itemsstockreport
            {
                get { return _itemsstockreport; }
                set
                {
                    _itemsstockreport = value;
                    NotifyPropertyChanged("itemsstockreport");
                }
            }

            String _DateStart;
            public String DateStart
            {
                get
                {
                    return _DateStart;
                }
                set
                {
                    if (value != _DateStart)
                    {
                        _DateStart = value;
                        NotifyPropertyChanged("DateStart");
                    }
                }
            }


            String _DateEnd;
            public String DateEnd
            {
                get
                {
                    return _DateEnd;
                }
                set
                {
                    if (value != _DateEnd)
                    {
                        _DateEnd = value;
                        NotifyPropertyChanged("DateEnd");
                    }
                }
            }



            String _DateStartReport;
            public String DateStartReport
            {
                get
                {
                    return _DateStartReport;
                }
                set
                {
                    if (value != _DateStartReport)
                    {
                        _DateStartReport = value;
                        NotifyPropertyChanged("_DateStartReport");
                    }
                }
            }


            String _DateEndReport;
            public String DateEndReport
            {
                get
                {
                    return _DateEndReport;
                }
                set
                {
                    if (value != _DateEndReport)
                    {
                        _DateEndReport = value;
                        NotifyPropertyChanged("_DateEndReport");
                    }
                }
            }


            Decimal _TotalItemCount;
            public Decimal TotalItemCount
            {
                get
                {
                    return _TotalItemCount;
                }
                set
                {
                    if (value != _TotalItemCount)
                    {
                        _TotalItemCount = value;
                        NotifyPropertyChanged("TotalItemCount");
                    }
                }
            }


            Decimal _TotalDiscountAmount;
            public Decimal TotalDiscountAmount
            {
                get
                {
                    return _TotalDiscountAmount;
                }
                set
                {
                    if (value != _TotalDiscountAmount)
                    {
                        _TotalDiscountAmount = value;
                        NotifyPropertyChanged("TotalDiscountAmount");
                    }
                }
            }



            Decimal _TotalNetAmount;
            public Decimal TotalNetAmount
            {
                get
                {
                    return _TotalNetAmount;
                }
                set
                {
                    if (value != _TotalNetAmount)
                    {
                        _TotalNetAmount = value;
                        NotifyPropertyChanged("TotalNetAmount");
                    }
                }
            }


            Decimal _TotalIncome;
            public Decimal TotalIncome
            {
                get
                {
                    return _TotalIncome;
                }
                set
                {
                    if (value != _TotalIncome)
                    {
                        _TotalIncome = value;
                        NotifyPropertyChanged("TotalIncome");
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

        private void btn_view_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.dataCon.itemsstockreport = this.repo.GetAllItemStock();

                this.report = new Template.ItemStocksReportCrystal();

                this.report.SetDataSource(this.dataCon.itemsstockreport);
                this.report.SetParameterValue("TodaysDate", DateTime.Now.ToString("MMMM dd, yyyy  hh:mm tt"));

                Report Viewer = new Report();
                Viewer.cryRpt = this.report;
                Viewer.printing = this.report;
                Viewer._CrystalReport.ViewerCore.ReportSource = report;

                Viewer._CrystalReport.ViewerCore.Zoom(150);
                Viewer.ShowDialog();
            }
            catch (Exception)
            {
                
            }
        }

        private void btn_allview_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.dataCon.itemsstockreport = this.repo.GetAllStock();

                this.report = new Template.ItemStocksReportCrystal();

                this.report.SetDataSource(this.dataCon.itemsstockreport);
                this.report.SetParameterValue("TodaysDate", DateTime.Now.ToString("MMMM dd, yyyy  hh:mm tt"));

                Report Viewer = new Report();
                Viewer.cryRpt = this.report;
                Viewer.printing = this.report;
                Viewer._CrystalReport.ViewerCore.ReportSource = report;

                Viewer._CrystalReport.ViewerCore.Zoom(150);
                Viewer.ShowDialog();
            }
            catch (Exception)
            {

            }
        }
    }
}
