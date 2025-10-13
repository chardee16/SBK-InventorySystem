using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models.InventoryManagementModule
{
    public class GenericMedicineClass : INotifyPropertyChanged
    {
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
