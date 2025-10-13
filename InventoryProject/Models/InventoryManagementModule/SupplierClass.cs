using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models.InventoryManagementModule
{
    public class SupplierClass : INotifyPropertyChanged
    {

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



        String _SupplierAddress;
        public String SupplierAddress
        {
            get
            {
                return _SupplierAddress;
            }
            set
            {
                if (value != _SupplierAddress)
                {
                    _SupplierAddress = value;
                    NotifyPropertyChanged("SupplierAddress");
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
