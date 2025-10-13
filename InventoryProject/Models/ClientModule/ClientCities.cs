using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models.ClientModule
{
    public class ClientCities : INotifyPropertyChanged
    {
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

        Int64 _ProvinceID;
        public Int64 ProvinceID
        {
            get { return _ProvinceID; }
            set
            {
                _ProvinceID = value;
                NotifyPropertyChanged("ProvinceID");
            }
        }

        String _City;
        public String City
        {
            get { return _City; }
            set
            {
                _City = value;
                NotifyPropertyChanged("City");
            }
        }

        String _ZipCode;
        public String ZipCode
        {
            get { return _ZipCode; }
            set
            {
                _ZipCode = value;
                NotifyPropertyChanged("ZipCode");
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
