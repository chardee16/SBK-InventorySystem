using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models.ItemModule
{
    public class ItemLogs : INotifyPropertyChanged
    {
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


        String _Username;
        public String Username
        {
            get
            {
                return _Username;
            }
            set
            {
                if (value != _Username)
                {
                    _Username = value;
                    NotifyPropertyChanged("Username");
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
