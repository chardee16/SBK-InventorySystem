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
    /// Interaction logic for SupplierWindow.xaml
    /// </summary>
    public partial class SupplierWindow : Window
    {
        InventoryManagementRepository repo = new InventoryManagementRepository();
        SupplierDataContext dataCon = new SupplierDataContext();
        public SupplierWindow()
        {
            InitializeComponent();
            btn_Edit.IsEnabled = false;
            this.DataContext = this.dataCon;
            LoadSupplier();
        }

        public void LoadSupplier()
        {
            try
            {
                this.dataCon.SupplierClass = this.repo.GetSupplierList();
            }
            catch (Exception)
            {
            }
        }

        public void ClearValues()
        {
            try
            {
                this.dataCon.SupplierDescription = "";
                this.dataCon.SupplierAddress = "";
                this.dataCon.SupplierID = 0;
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
        
        private void dg_supplier_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                SupplierClass selected = (SupplierClass)dg_supplier.SelectedItem;
                this.dataCon.SupplierID = selected.SupplierID;
                this.dataCon.SupplierDescription = selected.SupplierDescription;
                this.dataCon.SupplierAddress = selected.SupplierAddress;

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
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to add this supplier?", "CONFIRMATION", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    if (!String.IsNullOrEmpty(this.dataCon.SupplierDescription))
                    {
                        this.dataCon.supplierclass = new SupplierClass();

                        this.dataCon.supplierclass.SupplierAddress = this.dataCon.SupplierAddress;
                        this.dataCon.supplierclass.SupplierDescription = this.dataCon.SupplierDescription;
                        this.dataCon.supplierclass.SupplierID = this.dataCon.SupplierID;

                        if (this.repo.InsertSupplier(this.dataCon.supplierclass))
                        {
                            if (this.dataCon.SupplierID > 0)
                            {
                                MessageBox.Show("Supplier Successfully Updated!", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show("Supplier Successfully Saved.", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            LoadSupplier();
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

        public class SupplierDataContext : INotifyPropertyChanged
        {

            List<SupplierClass> _SupplierClass;
            public List<SupplierClass> SupplierClass
            {
                get { return _SupplierClass; }
                set
                {
                    _SupplierClass = value;
                    NotifyPropertyChanged("SupplierClass");
                }
            }


            SupplierClass _supplierclass;
            public SupplierClass supplierclass
            {
                get { return _supplierclass; }
                set
                {
                    _supplierclass = value;
                    NotifyPropertyChanged("supplierclass");
                }
            }

            Int32 _SupplierID;
            public Int32 SupplierID
            {
                get
                {
                    return _SupplierID;
                }
                set
                {
                    if (value != _SupplierID)
                    {
                        _SupplierID = value;
                        NotifyPropertyChanged("SupplierID");
                    }
                }
            }


            String _SupplierDescription;
            public String SupplierDescription
            {
                get
                {
                    return _SupplierDescription;
                }
                set
                {
                    if (value != _SupplierDescription)
                    {
                        _SupplierDescription = value;
                        NotifyPropertyChanged("SupplierDescription");
                    }
                }
            }

            String _SupplierAddress;
            public String SupplierAddress
            {
                get
                {
                    return _SupplierAddress;
                }
                set
                {
                    if (value != _SupplierAddress)
                    {
                        _SupplierAddress = value;
                        NotifyPropertyChanged("SupplierAddress");
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
