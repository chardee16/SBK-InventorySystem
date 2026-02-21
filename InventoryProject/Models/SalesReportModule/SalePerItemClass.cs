using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models.SalesReportModule
{
    public class SalePerItemClass : INotifyPropertyChanged
    {

        Int32 _ItemCode;
        public Int32 ItemCode
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


        String _CategoryDesc;
        public String CategoryDesc
        {
            get
            {
                return _CategoryDesc;
            }
            set
            {
                if (value != _CategoryDesc)
                {
                    _CategoryDesc = value;
                    NotifyPropertyChanged("CategoryDesc");
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

        Decimal _Income;
        public Decimal Income
        {
            get
            {
                return _Income;
            }
            set
            {
                if (value != _Income)
                {
                    _Income = value;
                    NotifyPropertyChanged("Income");
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
