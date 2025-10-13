using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models.InventoryManagementModule
{
    public class ShelveClass : INotifyPropertyChanged
    {

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
