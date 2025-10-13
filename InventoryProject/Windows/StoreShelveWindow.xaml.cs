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
    /// Interaction logic for StoreShelveWindow.xaml
    /// </summary>
    public partial class StoreShelveWindow : Window
    {
        InventoryManagementRepository repo = new InventoryManagementRepository();
        StoreShelfDataContext dataCon = new StoreShelfDataContext();
        public StoreShelveWindow()
        {
            InitializeComponent();
            btn_Edit.IsEnabled = false;
            this.DataContext = this.dataCon;
            LoadStoreShelf();
        }

        public void LoadStoreShelf()
        {
            try
            {
                this.dataCon.ShelveClass = this.repo.GetShelveList();
            }
            catch (Exception)
            {

            }
        }


        public void ClearValues()
        {
            try
            {
                this.dataCon.ShelfDescription = "";
                this.dataCon.ShelfID = 0;
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

        private void dg_storeshelf_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ShelveClass selected = (ShelveClass)dg_storeshelf.SelectedItem;
                this.dataCon.ShelfID = selected.ShelfID;
                this.dataCon.ShelfDescription = selected.ShelfDescription;

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
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to add this shelve?", "CONFIRMATION", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    if (!String.IsNullOrEmpty(this.dataCon.ShelfDescription))
                    {
                        this.dataCon.shelveclass = new ShelveClass();

                        this.dataCon.shelveclass.ShelfDescription = this.dataCon.ShelfDescription;
                        this.dataCon.shelveclass.ShelfID = this.dataCon.ShelfID;

                        if (this.repo.InsertStoreShelve(this.dataCon.shelveclass))
                        {
                            if (this.dataCon.ShelfID > 0)
                            {
                                MessageBox.Show("StoreShelve Successfully Updated!", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show("StoreShelve Successfully Saved.", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            LoadStoreShelf();
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

        public class StoreShelfDataContext : INotifyPropertyChanged
        {

            List<ShelveClass> _ShelveClass;
            public List<ShelveClass> ShelveClass
            {
                get { return _ShelveClass; }
                set
                {
                    _ShelveClass = value;
                    NotifyPropertyChanged("ShelveClass");
                }
            }


            ShelveClass _shelveclass;
            public ShelveClass shelveclass
            {
                get { return _shelveclass; }
                set
                {
                    _shelveclass = value;
                    NotifyPropertyChanged("shelveclass");
                }
            }

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
}
