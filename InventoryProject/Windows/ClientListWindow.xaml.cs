using InventoryProject.Models.ClientModule;
using InventoryProject.Pages;
using InventoryProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ClientListWindow.xaml
    /// </summary>
    public partial class ClientListWindow : Window
    {
        public string clientid = "";
        public string clientname = "";
        public string fullname = "";
        ClientDataContext dataCon = new ClientDataContext();
        ClientRepository repo = new ClientRepository();
        SalesPage sales;
        ClientFindWindow mv;
        ClientDiscountDelivery delivery;
        public ClientListWindow(string id, string name, ClientFindWindow mw, SalesPage mw2, ClientDiscountDelivery mw3)
        {
            InitializeComponent();
            clientid = id;
            clientname = name;
            mv = mw;
            sales = mw2;
            delivery = mw3;
            LoadClients();

            this.DataContext = this.dataCon;
        }

        private void LoadClients()
        {
            this.dataCon.SelectClients = this.repo.GetListClients(clientid, clientname);
        }

        private void HandleKeys(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    this.Close();
                    break;
            }
        }

        private void DG_Client_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                InsertClient selected = (InsertClient)DG_Clients.SelectedItem;
                fullname = selected.LastName + ", " + selected.FirstName + " " + selected.MiddleName;

                if (sales != null)
                {
                    sales.LoadClient(selected.id.ToString(), fullname);
                }
                else
                {
                    delivery.LoadClient(selected.id.ToString(), fullname);
                }


                    this.mv.Hide();
                this.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public class ClientDataContext : INotifyPropertyChanged
        {         
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
         
            String _Barcode;
            public String Barcode
            {
                get
                {
                    return _Barcode;
                }
                set
                {
                    if (value != _Barcode)
                    {
                        _Barcode = value;
                        NotifyPropertyChanged("Barcode");
                    }
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
