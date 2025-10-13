using InventoryProject.Models.InventoryManagementModule;
using InventoryProject.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InventoryProject.Windows
{
    /// <summary>
    /// Interaction logic for NameWindow.xaml
    /// </summary>
    public partial class NameWindow : Window
    {
        InventoryManagementRepository repo = new InventoryManagementRepository();
        NameDataContext dataCon = new NameDataContext();
        public NameWindow()
        {
            InitializeComponent();
            btn_Edit.IsEnabled = false;
            this.DataContext = this.dataCon;
            LoadName();
        }

        public void LoadName()
        {
            try
            {
                this.dataCon.GenericMedicineClass = this.repo.GetGenericMedicine();
            }
            catch (Exception)
            {

            }

        }
        
        public void ClearValues()
        {
            try
            {
                this.dataCon.GenericName = "";
                this.dataCon.GenericID = 0;
            }
            catch (Exception)
            {

            }
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Save();
            }
            catch (Exception)
            {
            }
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btn_Edit.IsEnabled = false;
                btn_Save.IsEnabled = true;
            }
            catch (Exception)
            {
            }
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btn_Edit.IsEnabled = false;
                btn_Save.IsEnabled = true;
                ClearValues();
            }
            catch (Exception)
            {
            }
        }

        private void dg_name_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                GenericMedicineClass selected = (GenericMedicineClass)dg_name.SelectedItem;
                this.dataCon.GenericID = selected.GenericID;
                this.dataCon.GenericName = selected.GenericName;

                btn_Save.IsEnabled = false;
                btn_Edit.IsEnabled = true;

            }
            catch (Exception)
            {

            }
        }

        public void Save()
        {
            try
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to add this name?", "CONFIRMATION", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    if (!String.IsNullOrEmpty(this.dataCon.GenericName))
                    {
                        this.dataCon.genericmedicineclass = new GenericMedicineClass();

                        this.dataCon.genericmedicineclass.GenericName = this.dataCon.GenericName;
                        this.dataCon.genericmedicineclass.GenericID = this.dataCon.GenericID;

                        if (this.repo.InsertName(this.dataCon.genericmedicineclass))
                        {
                            if (this.dataCon.GenericID > 0)
                            {
                                MessageBox.Show("Name Successfully Updated!", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show("Name Successfully Saved.", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            LoadName();
                            ClearValues();

                        }
                        else
                        {
                            MessageBox.Show("Something went wrong.\nPlease Contact Administrator!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }


            }
            catch (Exception)
            {

            }
        }

        public class NameDataContext : INotifyPropertyChanged
        {

            List<GenericMedicineClass> _GenericMedicineClass;
            public List<GenericMedicineClass> GenericMedicineClass
            {
                get { return _GenericMedicineClass; }
                set
                {
                    _GenericMedicineClass = value;
                    NotifyPropertyChanged("GenericMedicineClass");
                }
            }


            GenericMedicineClass _genericmedicineclass;
            public GenericMedicineClass genericmedicineclass
            {
                get { return _genericmedicineclass; }
                set
                {
                    _genericmedicineclass = value;
                    NotifyPropertyChanged("genericmedicineclass");
                }
            }

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
}
