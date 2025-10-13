using InventoryProject.Pages;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ClientFind.xaml
    /// </summary>
    public partial class ClientFindWindow : Window
    {
        SalesPage mv;
        public ClientFindWindow(SalesPage mw)
        {
            InitializeComponent();
            txt_ClientID.Focus();
            mv = mw;
        }

        private void HandleKeys(object sender, KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.Escape:
                    this.Close();
                    break;
                case Key.Enter:
                    e.Handled = true;
                    ClientListWindow subWindow = new ClientListWindow(txt_ClientID.Text, txt_ClientName.Text, this,mv);
                    subWindow.ShowDialog();
                    break;             
            }
        }

        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            ClientListWindow subWindow = new ClientListWindow(txt_ClientID.Text,txt_ClientName.Text,this, mv);
            subWindow.ShowDialog();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {     
            this.Close();             
        }

        private void txt_ClientName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_ClientID.Text))
            {
                txt_ClientID.Text = String.Empty;
            }
        }

        private void txt_ClientID_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_ClientName.Text))
            {
                txt_ClientName.Text = String.Empty;
            }
        }

    }
}
