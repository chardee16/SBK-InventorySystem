using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models.SalesReportModule
{
    public class TopSales : INotifyPropertyChanged
    {

        Int32 _Sales;
        public Int32 Sales
        {
            get
            {
                return _Sales;
            }
            set
            {
                if (value != _Sales)
                {
                    _Sales = value;
                    NotifyPropertyChanged("Sales");
                }
            }
        }

        Int32 _TotalStocks;
        public Int32 TotalStocks
        {
            get
            {
                return _TotalStocks;
            }
            set
            {
                if (value != _TotalStocks)
                {
                    _TotalStocks = value;
                    NotifyPropertyChanged("TotalStocks");
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
