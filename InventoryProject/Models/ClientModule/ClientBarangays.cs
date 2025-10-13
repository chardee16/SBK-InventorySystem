using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models.ClientModule
{
    public class ClientBarangays : INotifyPropertyChanged
    {
        Int64 _BrgyID;
        public Int64 BrgyID
        {
            get { return _BrgyID; }
            set
            {
                _BrgyID = value;
                NotifyPropertyChanged("BrgyID");
            }
        }

        Int64 _CityID;
        public Int64 CityID
        {
            get { return _CityID; }
            set
            {
                _CityID = value;
                NotifyPropertyChanged("CityID");
            }
        }

        String _Barangay;
        public String Barangay
        {
            get { return _Barangay; }
            set
            {
                _Barangay = value;
                NotifyPropertyChanged("Barangay");
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
