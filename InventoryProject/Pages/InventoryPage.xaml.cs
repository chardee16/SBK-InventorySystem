using InventoryProject.Models;
using InventoryProject.Models.ItemModule;
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
    /// Interaction logic for InventoryPage.xaml
    /// </summary>
    public partial class InventoryPage : Page
    {
        public ObservableCollection<ItemClass> ItemList;
        BackgroundWorker worker = new BackgroundWorker();
        ItemRepository repo = new ItemRepository();
        ItemDataContext dataCon = new ItemDataContext();

        private ICollectionView MyData;
        string SearchText = string.Empty;
        int currentRow = 0, currentColumn = 1;

        public InventoryPage()
        {
            InitializeComponent();
            InitializeWorkers();
            this.PreviewKeyDown += new KeyEventHandler(HandleKeysEvent);

            this.DataContext = this.dataCon;
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

        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                
                this.dataCon.itemList = repo.GetItemList();
                break;
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            ItemList = new ObservableCollection<ItemClass>(this.dataCon.itemList);
            DG_Items.ItemsSource = ItemList;
            MyData = CollectionViewSource.GetDefaultView(ItemList);
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
                    selectedItemFunction();
                    txt_Search.SelectAll();
                    txt_Search.Focus();
                    e.Handled = true;
                    break;
            }
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
            return value.Barcode.ToLower().Contains(SearchText.ToLower())
                || value.ItemDescription.ToLower().Contains(SearchText.ToLower())
                || value.ItemName.ToLower().Contains(SearchText.ToLower())
                || value.CategoryDescription.ToLower().Contains(SearchText.ToLower());
                
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
                DG_Items.SelectedItem = ItemList.Last();
                DG_Items.ScrollIntoView(DG_Items.Items[DG_Items.SelectedIndex]);
            }
        }





        private void DG_Items_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                selectedItemFunction();
            }
            catch
            {

            }
           
        }


        private void selectedItemFunction()
        {
            try
            {
                ItemClass selected = (ItemClass)DG_Items.SelectedItem;
                this.dataCon.ItemCode = selected.ItemCode ?? 0;
                this.dataCon.ItemName = selected.ItemName;
                this.dataCon.ItemDescription = selected.ItemDescription;
                this.dataCon.CategoryID = selected.CategoryID ?? 0;
                this.dataCon.Price = selected.Price ?? 0;
                this.dataCon.Stock = selected.Stock ?? 0;
                this.dataCon.UnitID = selected.UnitID ?? 0;
                this.dataCon.UnitAbbr = selected.UnitAbbr;
                this.dataCon.Barcode = selected.Barcode;
            }
            catch
            {

            }
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
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void txt_Search_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

     
    }//end of page







    public class ItemDataContext : INotifyPropertyChanged
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
