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


        Int32 _OutForDelivery;
        public Int32 OutForDelivery
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
                    NotifyPropertyChanged("OutForDelivery");
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
