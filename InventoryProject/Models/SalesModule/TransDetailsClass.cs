using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models.SalesModule
{
    public class TransDetailsClass
    {
    
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

        Decimal _Discount;
        public Decimal Discount
        {
            get
            {
                return _Discount;
            }
            set
            {
                if (value != _Discount)
                {
                    _Discount = value;
                    NotifyPropertyChanged("Discount");
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

        Decimal _Amount;
        public Decimal Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                if (value != _Amount)
                {
                    _Amount = value;
                    NotifyPropertyChanged("Amount");
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
