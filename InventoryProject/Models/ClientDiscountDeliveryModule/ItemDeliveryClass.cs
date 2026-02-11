using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models.ClientDiscountDeliveryModule
{
   
    public class ItemDeliveryClass : INotifyPropertyChanged
    {

        Int32 _DeliveryID;
        public Int32 DeliveryID
        {
            get
            {
                return _DeliveryID;
            }
            set
            {
                if (value != _DeliveryID)
                {
                    _DeliveryID = value;
                    NotifyPropertyChanged("DeliveryID");
                }
            }
        }



        String _DeliveryDescription;
        public String DeliveryDescription
        {
            get
            {
                return _DeliveryDescription;
            }
            set
            {
                if (value != _DeliveryDescription)
                {
                    _DeliveryDescription = value;
                    NotifyPropertyChanged("DeliveryDescription");
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

