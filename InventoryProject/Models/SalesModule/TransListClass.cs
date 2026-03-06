using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models.SalesModule
{
    public class TransListClass
    {

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


        Int32 _CTLNo;
        public Int32 CTLNo
        {
            get
            {
                return _CTLNo;
            }
            set
            {
                if (value != _CTLNo)
                {
                    _CTLNo = value;
                    NotifyPropertyChanged("CTLNo");
                }
            }
        }

        Int32 _TransactionCode;
        public Int32 TransactionCode
        {
            get
            {
                return _TransactionCode;
            }
            set
            {
                if (value != _TransactionCode)
                {
                    _TransactionCode = value;
                    NotifyPropertyChanged("TransactionCode");
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
