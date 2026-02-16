using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models.ItemModule
{
    public class UnitClass : INotifyPropertyChanged
    {
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


        Int32 _id;
        public Int32 id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    NotifyPropertyChanged("id");
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


        String _UnitDescription;
        public String UnitDescription
        {
            get
            {
                return _UnitDescription;
            }
            set
            {
                if (value != _UnitDescription)
                {
                    _UnitDescription = value;
                    NotifyPropertyChanged("UnitDescription");
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
