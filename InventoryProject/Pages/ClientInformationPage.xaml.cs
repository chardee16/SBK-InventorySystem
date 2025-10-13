using InventoryProject.Models.ClientModule;
using InventoryProject.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InventoryProject.Pages
{
    /// <summary>
    /// Interaction logic for ClientInformationPage.xaml
    /// </summary>
    public partial class ClientInformationPage : Page
    {
        ItemDataContext dataCon = new ItemDataContext();
        ClientRepository repo = new ClientRepository();
        public String BaseDirectory;
        private static string CurrentPath = Environment.CurrentDirectory;
        public bool _ispassed = false;
        String message = "";
        public ClientInformationPage()
        {
            InitializeComponent();
            this.DataContext = this.dataCon;
            BaseDirectory = CurrentPath.Replace("\\bin\\Debug", "");

            this.dataCon.ClientCities = new List<ClientCities>();
            this.dataCon.Genders = new JavaScriptSerializer().ConvertToType<List<Genders>>(Attributes("Gender")); //-to be comment
            this.dataCon.CivilStatuses = new JavaScriptSerializer().ConvertToType<List<CivilStatuses>>(Attributes("CivilStatus")); //-to be comment
            this.dataCon.ClientSuffix = new JavaScriptSerializer().ConvertToType<List<ClientSuffix>>(Attributes("Suffix")); //-to be comment
            this.dataCon.ClientTitle = new JavaScriptSerializer().ConvertToType<List<ClientTitle>>(Attributes("Title")); //-to be comment
            this.dataCon.ClientProvince = new JavaScriptSerializer().ConvertToType<List<ClientProvince>>(Attributes("Provinces")).OrderBy(o => o.Province).ToList(); //-to be comment
            LoadData();

            Disable();
        }

        private void LoadData()
        {

            this.dataCon.SelectClients = this.repo.GetAllClients();
            foreach (var item in this.dataCon.SelectClients)
            {
                item.BirthDate = Convert.ToDateTime(item.BirthDate).ToString("MM/dd/yyyy");
            }

        }

        private void btn_New_Click(object sender, RoutedEventArgs e)
        {
            ClearValues();
            Enable();
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            _ispassed = true;
            Enable();
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveClient();
        }

        private void myCurrencyTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void DG_Client_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                InsertClient selected = (InsertClient)DG_Clients.SelectedItem;
                this.dataCon.ClientID = selected.ClientID;
                this.dataCon.TitleID = selected.TitleID;
                this.dataCon.LastName = selected.LastName;
                this.dataCon.FirstName = selected.FirstName;
                this.dataCon.MiddleName = selected.MiddleName;
                this.dataCon.SuffixID = selected.SuffixID;
                this.dataCon.GenderID = selected.GenderID;
                this.dataCon.CivilStatusID = selected.CivilStatusID;
                this.dataCon.Company = selected.Company;
                this.dataCon.ProvinceID = selected.ProvinceID;
                this.dataCon.CityID = selected.CityID;
                this.dataCon.BrgyID = selected.BrgyID;
                this.dataCon.DateAdded = selected.DateAdded;
                this.dataCon.DateTimeAdded = selected.DateTimeAdded;
                this.dataCon.Age = selected.Age;
                this.dataCon.BirthDate = Convert.ToDateTime(selected.BirthDate).ToString("MM/dd/yyyy");
                Disable();

            }
            catch
            {
            }

        }

        private void ClearValues()
        {
            this.dataCon.ClientID = 0;
            this.dataCon.LastName = String.Empty;
            this.dataCon.FirstName = String.Empty;
            this.dataCon.MiddleName = String.Empty;
            this.dataCon.Company = String.Empty;
            this.dataCon.TitleID = 0;
            this.dataCon.SuffixID = 0;
            this.dataCon.GenderID = 0;
            this.dataCon.CivilStatusID = 0;
            this.dataCon.ProvinceID = 0;
            this.dataCon.CityID = 0;
            this.dataCon.BrgyID = 0;
            this.dataCon.Age = 0;
            this.dataCon.BirthDate = String.Empty;
        }

        private void Disable()
        {
            this.txt_FirstName.IsReadOnly = true;
            this.txt_LastName.IsReadOnly = true;
            this.txt_MiddleName.IsReadOnly = true;
            this.cmb_Title.IsHitTestVisible = false;
            this.cmb_Suffix.IsHitTestVisible = false;
            this.cmb_Gender.IsHitTestVisible = false;
            this.cmb_CivilStatus.IsHitTestVisible = false;
            this.txt_Company.IsReadOnly = true;
            this.cmb_Province.IsHitTestVisible = false;
            this.cmb_City.IsHitTestVisible = false;
            this.cmb_Barangay.IsHitTestVisible = false;
            this.dtp_birthdate.IsHitTestVisible = false;

        }

        private void Enable()
        {
            this.txt_FirstName.IsReadOnly = false;
            this.txt_LastName.IsReadOnly = false;
            this.txt_MiddleName.IsReadOnly = false;
            this.cmb_Title.IsHitTestVisible = true;
            this.cmb_Suffix.IsHitTestVisible = true;
            this.cmb_Gender.IsHitTestVisible = true;
            this.cmb_CivilStatus.IsHitTestVisible = true;
            this.txt_Company.IsReadOnly = false;
            this.cmb_Province.IsHitTestVisible = true;
            this.cmb_City.IsHitTestVisible = true;
            this.cmb_Barangay.IsHitTestVisible = true;
            this.dtp_birthdate.IsHitTestVisible = true;

        }

        private void SaveClient()
        {
            if (_ispassed)
            {
                message = "Are you sure you want to update this client?";
            }
            else
            {
                message = "Are you sure you want to save this client?";
            }

            MessageBoxResult messageBoxResult = MessageBox.Show(message, "CONFIRMATION", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                ClientTitle title = (ClientTitle)cmb_Title.SelectedItem;
                ClientSuffix suff = (ClientSuffix)cmb_Suffix.SelectedItem;
                Genders gen = (Genders)cmb_Gender.SelectedItem;
                CivilStatuses civil = (CivilStatuses)cmb_CivilStatus.SelectedItem;
                ClientProvince province = (ClientProvince)cmb_Province.SelectedItem;
                ClientCities city = (ClientCities)cmb_City.SelectedItem;
                ClientBarangays barangay = (ClientBarangays)cmb_Barangay.SelectedItem;

                InsertClient client = new InsertClient();
                client.TitleID = (cmb_Title.Text.Equals("") ? Convert.ToByte(0) : title.TitleID);
                client.LastName = txt_LastName.Text;
                client.FirstName = txt_FirstName.Text;
                client.MiddleName = txt_MiddleName.Text;
                client.SuffixID = (cmb_Suffix.Text.Equals("") ? Convert.ToByte(0) : Convert.ToByte(suff.SuffixID));
                client.GenderID = (cmb_Title.Text.Equals("") ? Convert.ToByte(0) : Convert.ToByte(gen.GenderID));
                client.CivilStatusID = (cmb_CivilStatus.Text.Equals("") ? Convert.ToByte(0) : Convert.ToByte(civil.CivilStatusID));
                client.Company = txt_Company.Text;
                client.ProvinceID = (cmb_CivilStatus.Text.Equals("") ? Convert.ToInt64(0) : Convert.ToInt64(province.ProvinceID));
                client.CityID = (cmb_CivilStatus.Text.Equals("") ? Convert.ToInt64(0) : Convert.ToInt64(city.CityID));
                client.BrgyID = (cmb_CivilStatus.Text.Equals("") ? Convert.ToInt64(0) : Convert.ToInt64(barangay.BrgyID));
                client.DateAdded = DateTime.Now.ToString("MM/dd/yyyy");
                client.DateTimeAdded = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                client.Age = Convert.ToByte(txt_Age.Text);
                client.BirthDate = Convert.ToDateTime(dtp_birthdate.Text).ToString("MM/dd/yyyy");

                if (_ispassed)
                {
                    client.ClientID = Convert.ToInt64(txt_ClientID.Text);
                    if (this.repo.UpdateInventoryClient(client))
                    {
                        MessageBox.Show("Client Successfully Updated.", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearValues();
                        LoadData();
                        _ispassed = false;
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong.\nPlease Contact Administrator!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    if (this.repo.InsertInventoryClient(client))
                    {
                        MessageBox.Show("Client Successfully Saved.", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearValues();
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong.\nPlease Contact Administrator!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }            

            }

        }

        public Object Attributes(String tableName, String Where = null, String Sort = null)
        {
            List<ExpandoObject> ListO = new List<ExpandoObject>();

            try
            {
                using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=" + this.BaseDirectory + "\\Attributes.db"))
                {
                    connect.Open();
                    using (SQLiteCommand fmd = connect.CreateCommand())
                    {
                        String sqliteQuery = "SELECT  *  FROM " + tableName;
                        if (Where != null)
                        {
                            sqliteQuery += " WHERE " + Where;
                        }
                        if (Sort != null)
                        {
                            sqliteQuery += " ORDER BY " + Sort;
                        }
                        fmd.CommandText = @sqliteQuery;
                        fmd.CommandType = CommandType.Text;
                        SQLiteDataReader r = fmd.ExecuteReader();
                        int resultColumns = r.FieldCount;
                        while (r.Read())
                        {
                            dynamic returnO = new ExpandoObject();
                            var p = returnO as IDictionary<String, object>;
                            String[] arr = new String[resultColumns];
                            for (int i = 0; i < arr.Length; i++)
                            {
                                p[r.GetName(i)] = r.GetValue(i).ToString();
                            }

                            ListO.Add(returnO);
                        }
                    }
                }

               
            }
            catch (Exception )
            {
                //MessageBox.Show(ex.Message);
            }

            return ListO;
        }

        public class ItemDataContext : INotifyPropertyChanged
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

            Int16 _SuffixID;
            public Int16 SuffixID
            {
                get { return _SuffixID; }
                set
                {
                    _SuffixID = value;
                    NotifyPropertyChanged("SuffixID");
                }
            }

            Int64 _GenderID;
            public Int64 GenderID
            {
                get { return _GenderID; }
                set
                {
                    _GenderID = value;
                    NotifyPropertyChanged("GenderID");
                }
            }

            Int64 _CivilStatusID;
            public Int64 CivilStatusID
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

            List<Genders> _Genders;
            public List<Genders> Genders
            {
                get { return _Genders; }
                set
                {
                    _Genders = value;
                    NotifyPropertyChanged("Genders");
                }
            }

            List<CivilStatuses> _CivilStatuses;
            public List<CivilStatuses> CivilStatuses
            {
                get
                {
                    return _CivilStatuses;
                }
                set
                {
                    _CivilStatuses = value;
                    NotifyPropertyChanged("CivilStatuses");
                }
            }


            List<ClientSuffix> _ClientSuffix;
            public List<ClientSuffix> ClientSuffix
            {
                get
                {
                    return _ClientSuffix;
                }
                set
                {
                    _ClientSuffix = value;
                    NotifyPropertyChanged("ClientSuffix");
                }
            }


            List<InsertClient> _SelectClients;
            public List<InsertClient> SelectClients
            {
                get
                {
                    return _SelectClients;
                }
                set
                {
                    _SelectClients = value;
                    NotifyPropertyChanged("SelectClients");
                }
            }


            List<ClientTitle> _ClientTitle;
            public List<ClientTitle> ClientTitle
            {
                get
                {
                    return _ClientTitle;
                }
                set
                {
                    _ClientTitle = value;
                    NotifyPropertyChanged("ClientTitle");
                }
            }


            List<ClientProvince> _ClientProvince;
            public List<ClientProvince> ClientProvince
            {
                get
                {
                    return _ClientProvince;
                }
                set
                {
                    _ClientProvince = value;
                    NotifyPropertyChanged("ClientProvince");
                }
            }


            List<ClientCities> _ClientCities;
            public List<ClientCities> ClientCities
            {
                get
                {
                    return _ClientCities;
                }
                set
                {
                    _ClientCities = value;
                    NotifyPropertyChanged("ClientCities");
                }
            }

            List<ClientBarangays> _ClientBarangays;
            public List<ClientBarangays> ClientBarangays
            {
                get
                {
                    return _ClientBarangays;
                }
                set
                {
                    _ClientBarangays = value;
                    NotifyPropertyChanged("ClientBarangays");
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

        private void cmb_Province_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.dataCon.ClientCities.Count > 0)
                {
                    this.dataCon.ClientCities.Clear();
                    List<ClientCities> samp = new List<ClientCities>();
                    this.dataCon.ClientCities = samp;
                    this.dataCon.CityID = 0;
                }

                if (cmb_Barangay.ItemsSource != null)
                {
                    this.dataCon.ClientBarangays.Clear();
                    List<ClientBarangays> samp = new List<ClientBarangays>();
                    this.dataCon.ClientBarangays = samp;
                    this.dataCon.BrgyID = 0;
                }
                         
                var item = (ClientProvince)cmb_Province.SelectedItem;
                if (item != null)
                {
                    this.dataCon.ClientCities = new JavaScriptSerializer().ConvertToType<List<ClientCities>>(Attributes("Cities", "ProvinceID=" + item.ProvinceID + "")).OrderBy(o => o.City).ToList();
                }

                

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
          
        }

        private void cmb_Barangay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void cmb_City_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmb_City.SelectedItem != null)
                {

                    if (this.dataCon.BrgyID > 0)
                    {
                        this.dataCon.ClientBarangays.Clear();
                        List<ClientBarangays> samp = new List<ClientBarangays>();
                        this.dataCon.ClientBarangays = samp;
                        this.dataCon.BrgyID = 0;
                    }

                    var item = (ClientCities)cmb_City.SelectedItem;

                    this.dataCon.ClientBarangays = new JavaScriptSerializer().ConvertToType<List<ClientBarangays>>(Attributes("Barangays", "CityID=" + item.CityID + "")).OrderBy(o => o.Barangay).ToList(); ;
                }
             
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void cmb_Suffix_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (cmb_Suffix.IsFocused == true)
                {
                    if (cmb_Suffix.SelectedIndex != -1)
                    {
                        cmb_Suffix.SelectedIndex = -1;
                        this.dataCon.SuffixID = 0;
                    }
                }
                else if (cmb_Title.IsFocused == true)
                {
                    if (cmb_Title.SelectedIndex != -1)
                    {
                        cmb_Title.SelectedIndex = -1;
                        this.dataCon.TitleID = 0;
                    }

                }
                else if (cmb_CivilStatus.IsFocused == true)
                {
                    if (cmb_CivilStatus.SelectedIndex != -1)
                    {
                        cmb_CivilStatus.SelectedIndex = -1;
                        this.dataCon.CivilStatusID = 0;
                    }
                }
                else if (cmb_Gender.IsFocused == true)
                {
                    if (cmb_Gender.SelectedIndex != -1)
                    {
                        cmb_Gender.SelectedIndex = -1;
                        this.dataCon.GenderID = 0;
                    }

                }
            }
        }

        private void dtp_birthdate_GotFocus(object sender, RoutedEventArgs e)
        {
            DateTime currentDate = DateTime.Parse(DateTime.Now.Date.ToShortDateString());
            //String SelectedDate = (dateOfBirthPicker.SelectedDate.Equals(null) ? "" : dateOfBirthPicker.SelectedDate.Value.ToString("yyyyMMdd"));

            DateTime selecteddate;
            string SelectedDate = "";
            if (DateTime.TryParseExact(dtp_birthdate.Text, "MM/dd/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out selecteddate))
            {
                SelectedDate = selecteddate.ToString("yyyyMMdd");
            }
            else
            {
                return;
            }
            String CurrentDate = Convert.ToString(currentDate.ToString("yyyyMMdd"));
            Int64 getAge = 0;
            if (SelectedDate == "")
            {
                getAge = 0;
            }
            else
            {
                getAge = (Convert.ToInt64(CurrentDate) - Convert.ToInt64(SelectedDate)) / 10000;
            }


            txt_Age.Text = getAge.ToString();
        }

    }
}
