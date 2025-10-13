using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models.SalesModule
{
    class TransactionCheckClass : INotifyPropertyChanged
    {


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



        Int64 _CTLNo;
        public Int64 CTLNo
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



        Int64 _TransYear;
        public Int64 TransYear
        {
            get
            {
                return _TransYear;
            }
            set
            {
                if (value != _TransYear)
                {
                    _TransYear = value;
                    NotifyPropertyChanged("TransYear");
                }
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




        Int32 _UPDTag;
        public Int32 UPDTag
        {
            get
            {
                return _UPDTag;
            }
            set
            {
                if (value != _UPDTag)
                {
                    _UPDTag = value;
                    NotifyPropertyChanged("UPDTag");
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
