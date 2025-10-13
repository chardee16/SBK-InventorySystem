using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models
{
    public class ItemClass : INotifyPropertyChanged
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


        Int32 _GenericID;
        public Int32 GenericID
        {
            get
            {
                return _GenericID;
            }
            set
            {
                if (value != _GenericID)
                {
                    _GenericID = value;
                    NotifyPropertyChanged("GenericID");
                }
            }
        }



        String _GenericName;
        public String GenericName
        {
            get
            {
                return _GenericName;
            }
            set
            {
                if (value != _GenericName)
                {
                    _GenericName = value;
                    NotifyPropertyChanged("GenericName");
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


        Decimal _PurchasePrice;
        public Decimal PurchasePrice
        {
            get
            {
                return _PurchasePrice;
            }
            set
            {
                if (value != _PurchasePrice)
                {
                    _PurchasePrice = value;
                    NotifyPropertyChanged("PurchasePrice");
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


        


        Decimal _MarkupValue;
        public Decimal MarkupValue
        {
            get
            {
                return _MarkupValue;
            }
            set
            {
                if (value != _MarkupValue)
                {
                    _MarkupValue = value;
                    NotifyPropertyChanged("MarkupValue");
                }
            }
        }


        Decimal _Value;
        public Decimal Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (value != _Value)
                {
                    _Value = value;
                    NotifyPropertyChanged("Value");
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


        Int32 _ShelfID;
        public Int32 ShelfID
        {
            get
            {
                return _ShelfID;
            }
            set
            {
                if (value != _ShelfID)
                {
                    _ShelfID = value;
                    NotifyPropertyChanged("ShelfID");
                }
            }
        }



        String _ShelfDescription;
        public String ShelfDescription
        {
            get
            {
                return _ShelfDescription;
            }
            set
            {
                if (value != _ShelfDescription)
                {
                    _ShelfDescription = value;
                    NotifyPropertyChanged("ShelfDescription");
                }
            }
        }



        Int32 _SupplierID;
        public Int32 SupplierID
        {
            get
            {
                return _SupplierID;
            }
            set
            {
                if (value != _SupplierID)
                {
                    _SupplierID = value;
                    NotifyPropertyChanged("SupplierID");
                }
            }
        }



        String _SupplierDescription;
        public String SupplierDescription
        {
            get
            {
                return _SupplierDescription;
            }
            set
            {
                if (value != _SupplierDescription)
                {
                    _SupplierDescription = value;
                    NotifyPropertyChanged("SupplierDescription");
                }
            }
        }



        String _SideEffect;
        public String SideEffect
        {
            get
            {
                return _SideEffect;
            }
            set
            {
                if (value != _SideEffect)
                {
                    _SideEffect = value;
                    NotifyPropertyChanged("SideEffect");
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
