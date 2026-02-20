using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models.ClientDiscountDeliveryModule
{
    public class DeliveryStatusClass : INotifyPropertyChanged
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

        [JsonProperty("Out for Delivery")]
        Int32 _OutForDelivery;
        public Int32 Out_For_Delivery
        {
            get
            {
                return _OutForDelivery;
            }
            set
            {
                if (value != _OutForDelivery)
                {
                    _OutForDelivery = value;
                    NotifyPropertyChanged("Out_For_Delivery");
                }
            }
        }


        Int32 _Delivered;
        public Int32 Delivered
        {
            get
            {
                return _Delivered;
            }
            set
            {
                if (value != _Delivered)
                {
                    _Delivered = value;
                    NotifyPropertyChanged("Delivered");
                }
            }
        }


        Int32 _Remaining;
        public Int32 Remaining
        {
            get
            {
                return _Remaining;
            }
            set
            {
                if (value != _Remaining)
                {
                    _Remaining = value;
                    NotifyPropertyChanged("Remaining");
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


        Decimal _TotalPriceDelivered;
        public Decimal TotalPriceDelivered
        {
            get
            {
                return _TotalPriceDelivered;
            }
            set
            {
                if (value != _TotalPriceDelivered)
                {
                    _TotalPriceDelivered = value;
                    NotifyPropertyChanged("TotalPriceDelivered");
                }
            }
        }

        Decimal _TotalSalesPrice;
        public Decimal TotalSalesPrice
        {
            get
            {
                return _TotalSalesPrice;
            }
            set
            {
                if (value != _TotalSalesPrice)
                {
                    _TotalSalesPrice = value;
                    NotifyPropertyChanged("TotalSalesPrice");
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
