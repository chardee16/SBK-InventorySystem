using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models.ClientModule
{
    public class InsertClient : INotifyPropertyChanged
    {
        byte _TitleID;
        public byte TitleID
        {
            get { return _TitleID; }
            set
            {
                _TitleID = value;
                NotifyPropertyChanged("TitleID");
            }
        }

        Int64 _ClientID;
        public Int64 ClientID
        {
            get { return _ClientID; }
            set
            {
                _ClientID = value;
                NotifyPropertyChanged("ClientID");
            }
        }

        Int64 _id;
        public Int64 id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged("id");
            }
        }

        string _DateOfBirth;
        public string DateOfBirth
        {
            get { return _DateOfBirth; }
            set
            {
                _DateOfBirth = value;
                NotifyPropertyChanged("DateOfBirth");
            }

        }

        string _BirthDate;
        public string BirthDate
        {
            get { return _BirthDate; }
            set
            {
                _BirthDate = value;
                NotifyPropertyChanged("BirthDate");
            }

        }

        byte _Age;
        public byte Age
        {
            get { return _Age; }
            set
            {
                _Age = value;
                NotifyPropertyChanged("Age");
            }
        }

        string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value;
                NotifyPropertyChanged("LastName");
            }
        }

        string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                _FirstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }

        string _MiddleName;
        public string MiddleName
        {
            get { return _MiddleName; }
            set
            {
                _MiddleName = value;
                NotifyPropertyChanged("MiddleName");
            }
        }

        byte _SuffixID;
        public byte SuffixID
        {
            get { return _SuffixID; }
            set
            {
                _SuffixID = value;
                NotifyPropertyChanged("SuffixID");
            }
        }

        byte _GenderID;
        public byte GenderID
        {
            get { return _GenderID; }
            set
            {
                _GenderID = value;
                NotifyPropertyChanged("GenderID");
            }
        }

        byte _CivilStatusID;
        public byte CivilStatusID
        {
            get { return _CivilStatusID; }
            set
            {
                _CivilStatusID = value;
                NotifyPropertyChanged("CivilStatusID");
            }
        }

        string _Company;
        public string Company
        {
            get { return _Company; }
            set
            {
                _Company = value;
                NotifyPropertyChanged("Company");
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

        string _DateAdded;
        public string DateAdded
        {
            get { return _DateAdded; }
            set
            {
                _DateAdded = value;
                NotifyPropertyChanged("DateAdded");
            }
        }

        string _DateTimeAdded;
        public string DateTimeAdded
        {
            get { return _DateTimeAdded; }
            set
            {
                _DateTimeAdded = value;
                NotifyPropertyChanged("DateTimeAdded");
            }
        }

        string _DateTimeModified;
        public string DateTimeModified
        {
            get { return _DateTimeModified; }
            set
            {
                _DateTimeModified = value;
                NotifyPropertyChanged("DateTimeModified");
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
