using InventoryProject.Models;
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
    /// Interaction logic for CategoryWindow.xaml
    /// </summary>
    public partial class CategoryWindow : Window
    {
        InventoryManagementRepository repo = new InventoryManagementRepository();
        CategoryDataContext dataCon = new CategoryDataContext();
        public CategoryWindow()
        {
            InitializeComponent();
            btn_Edit.IsEnabled = false;
            this.DataContext = this.dataCon;
            LoadCategory();
        }

        public void LoadCategory()
        {
            try
            {
                this.dataCon.CategoryClass = this.repo.GetCategoryList();
            }
            catch (Exception)
            {

            }

        }

        public void ClearValues()
        {
            try
            {
                this.dataCon.CategoryDesc = "";
                this.dataCon.id = 0;
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

        private void dg_category_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ItemCategoryClass selected = (ItemCategoryClass)dg_category.SelectedItem;
                this.dataCon.id = selected.id;
                this.dataCon.CategoryDesc = selected.CategoryDesc;

                btn_Save.IsEnabled = false;
                btn_Edit.IsEnabled = true;

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


        public void Save()
        {
            try
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to add this category?", "CONFIRMATION", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    if (!String.IsNullOrEmpty(this.dataCon.CategoryDesc))
                    {
                        this.dataCon.category = new ItemCategoryClass();

                        this.dataCon.category.CategoryDesc = this.dataCon.CategoryDesc;
                        this.dataCon.category.id = this.dataCon.id;

                        if (this.repo.InsertCategory(this.dataCon.category))
                        {
                            if (this.dataCon.id > 0)
                            {
                                MessageBox.Show("Category Successfully Updated!", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show("Category Successfully Saved.", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            LoadCategory();
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

        public class CategoryDataContext : INotifyPropertyChanged
        {

            List<ItemCategoryClass> _CategoryClass;
            public List<ItemCategoryClass> CategoryClass
            {
                get { return _CategoryClass; }
                set
                {
                    _CategoryClass = value;
                    NotifyPropertyChanged("CategoryClass");
                }
            }


            ItemCategoryClass _category;
            public ItemCategoryClass category
            {
                get { return _category; }
                set
                {
                    _category = value;
                    NotifyPropertyChanged("category");
                }
            }

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
