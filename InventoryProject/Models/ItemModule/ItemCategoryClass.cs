using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models
{
    public class ItemCategoryClass : INotifyPropertyChanged
    {

        Int32 _CategoryID;
        public Int32 CategoryID
        {
            get
            {
                return _CategoryID;
            }
            set
            {
                if (value != _CategoryID)
                {
                    _CategoryID = value;
                    NotifyPropertyChanged("CategoryID");
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


        String _CategoryDesc;
        public String CategoryDesc
        {
            get
            {
                return _CategoryDesc;
            }
            set
            {
                if (value != _CategoryDesc)
                {
                    _CategoryDesc = value;
                    NotifyPropertyChanged("CategoryDesc");
                }
            }
        }


        String _CategoryDescription;
        public String CategoryDescription
        {
            get
            {
                return _CategoryDescription;
            }
            set
            {
                if (value != _CategoryDescription)
                {
                    _CategoryDescription = value;
                    NotifyPropertyChanged("CategoryDescription");
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
