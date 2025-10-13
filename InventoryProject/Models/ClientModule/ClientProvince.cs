using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models.ClientModule
{
    public class ClientProvince : INotifyPropertyChanged
    {

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

        String _Province;
        public String Province
        {
            get { return _Province; }
            set
            {
                _Province = value;
                NotifyPropertyChanged("Province");
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
