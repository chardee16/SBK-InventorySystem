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
    /// Interaction logic for StockLogs.xaml
    /// </summary>
    public partial class StockLogs : Window
    {
        LogsDataContext dataCon = new LogsDataContext();
        ItemRepository repo = new ItemRepository();
        Int64 itemcode;
        public StockLogs(Int64 item)
        {
            InitializeComponent();
            itemcode = item;
            ShowLogs();

        }

        public void ShowLogs()
        {
            try
            {
                this.dataCon.sampleonly = repo.GetItemLogs(Convert.ToInt32(itemcode));
                this.dg_logs.ItemsSource = this.dataCon.sampleonly;
            }
            catch (Exception)
            {
            }

        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public class LogsDataContext : INotifyPropertyChanged
        {
            List<ItemLogs> _sampleonly;
            public List<ItemLogs> sampleonly
            {
                get { return _sampleonly; }
                set
                {
                    _sampleonly = value;
                    NotifyPropertyChanged("sampleonly");
                }
            }


            Int32 _Quantity;
            public Int32 Quantity
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

            String _DateTimeAdded;
            public String DateTimeAdded
            {
                get
                {
                    return _DateTimeAdded;
                }
                set
                {
                    if (value != _DateTimeAdded)
                    {
                        _DateTimeAdded = value;
                        NotifyPropertyChanged("DateTimeAdded");
                    }
                }
            }


            String _Username;
            public String Username
            {
                get
                {
                    return _Username;
                }
                set
                {
                    if (value != _Username)
                    {
                        _Username = value;
                        NotifyPropertyChanged("Username");
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
