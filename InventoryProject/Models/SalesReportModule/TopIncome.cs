using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models.SalesReportModule
{
    public class TopIncome : INotifyPropertyChanged
    {

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
