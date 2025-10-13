using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models.SalesModule
{
    public class SalesItemClass : INotifyPropertyChanged
    {


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




        Decimal _Quantity;
        public Decimal Quantity
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



        Decimal _Total;
        public Decimal Total
        {
            get
            {
                return _Total;
            }
            set
            {
                if (value != _Total)
                {
                    _Total = value;
                    NotifyPropertyChanged("Total");
                }
            }
        }


        String _ExpiryDate;
        public String ExpiryDate
        {
            get
            {
                return _ExpiryDate;
            }
            set
            {
                if (value != _ExpiryDate)
                {
                    _ExpiryDate = value;
                    NotifyPropertyChanged("ExpiryDate");
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
