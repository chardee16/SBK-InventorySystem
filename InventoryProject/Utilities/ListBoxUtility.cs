using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InventoryProject.Utilities
{
    public class ListBoxUtility
    {
        private System.Windows.Controls.ListBox GlobalFrom;
        private System.Windows.Controls.ListBox GlobalTo;
        private System.Windows.Controls.ListBox GlobalParent;

        private Boolean GlobalIsParent;
        private Boolean GlobalIsMultiSelect;


        public delegate void CallbackEventHandler(System.Windows.Controls.ListBox From, System.Windows.Controls.ListBox To, System.Windows.Controls.ListBox ParentOrChild = null, Boolean IsParent = false, Boolean MultiSelect = false);

        public event CallbackEventHandler selectToCallBack;

        public event CallbackEventHandler selectFromCallBack;

        public event CallbackEventHandler selectParentChildCallBack;
        /// <summary>
        /// Automates events of selecting items froma listbox to another
        /// </summary>
        /// <param name="From">ListBox where items will be housed or come from. Passes Data to using the Right Key</param>
        /// <param name="To">ListBox where selected items will be accepting. Passes Data back using the Left Key</param>
        /// <param name="ParentOrChild">Parent ListBox where the From Data will be dependent or Child ListBox where the To will Populate</param>
        /// <param name="IsParent">Toggles the the ParentOrChild as Parent. Default(False) ParentorChild is Child</param>
        /// <param name="MultiSelect">Allows Parent to do multiple selections</param>
        public void selectTo(System.Windows.Controls.ListBox From, System.Windows.Controls.ListBox To, System.Windows.Controls.ListBox ParentOrChild = null, Boolean IsParent = false, Boolean MultiSelect = false)
        {
            GlobalFrom = From;
            GlobalTo = To;
            GlobalParent = ParentOrChild;
            From.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(HandleFromListKeyDown);
            To.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(HandleToListKeyDown);

            From.GotFocus += new System.Windows.RoutedEventHandler(HandleFocus);
            To.GotFocus += new System.Windows.RoutedEventHandler(HandleFocus);

            From.LostFocus += new System.Windows.RoutedEventHandler(HandleLostFocus);
            if (ParentOrChild == null || MultiSelect)
                To.LostFocus += new System.Windows.RoutedEventHandler(HandleLostFocus);

            From.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(FromSelectionChanged);
            To.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(ToSelectionChanged);

            From.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(HandleDoubleClickFrom);
            To.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(HandleDoubleClickTo);


        }

        private void FromSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //try
            //{
            //    System.Windows.Controls.ListBox l = sender as System.Windows.Controls.ListBox;
            //    if (l.Items.Count > 0)
            //    {
            //        var listBoxItem = (System.Windows.Controls.ListBoxItem)l.ItemContainerGenerator.ContainerFromItem(l.SelectedItem);
            //        listBoxItem.Focus();
            //    }
            //}catch(Exception ex)
            //{

            //}
        }

        private void ToSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (selectParentChildCallBack != null)
                selectParentChildCallBack(GlobalFrom, GlobalTo, GlobalParent, GlobalIsParent, GlobalIsMultiSelect);
        }
        private void HandleFocus(object sender, System.Windows.RoutedEventArgs e)
        {

            System.Windows.Controls.ListBox l = sender as System.Windows.Controls.ListBox;
            l.SelectedIndex = 0;

            if (selectFromCallBack != null)
            {
                Boolean load = true;

                if (!GlobalIsMultiSelect)
                {
                    if (l.Name != GlobalTo.Name)
                    {
                        load = false;
                    }
                }
                if (load)
                {
                    if (selectParentChildCallBack != null)
                        selectParentChildCallBack(GlobalFrom, GlobalTo, GlobalParent, GlobalIsParent, GlobalIsMultiSelect);
                }
            }

            //if(l.Items.Count>0)
            //{
            //    var listBoxItem = (System.Windows.Controls.ListBoxItem)l.ItemContainerGenerator.ContainerFromItem(l.SelectedItem);
            //    listBoxItem.Focus();
            //}
        }

        private void ChildFocus(object sender, System.Windows.RoutedEventArgs e)
        {

            if (selectParentChildCallBack != null)
                selectParentChildCallBack(GlobalFrom, GlobalTo, GlobalParent, GlobalIsParent, GlobalIsMultiSelect);
        }
        private void HandleLostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.Controls.ListBox l = sender as System.Windows.Controls.ListBox;
            l.SelectedIndex = -1;
        }
        private void HandleDoubleClickFrom(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Controls.ListBox l = sender as System.Windows.Controls.ListBox;
            if (selectToCallBack != null)
                selectToCallBack(GlobalFrom, GlobalTo, GlobalParent, GlobalIsParent, GlobalIsMultiSelect);

            l.SelectedIndex = 0;
        }
        private void HandleDoubleClickTo(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Controls.ListBox l = sender as System.Windows.Controls.ListBox;
            if (selectFromCallBack != null)
                selectFromCallBack(GlobalFrom, GlobalTo, GlobalParent, GlobalIsParent, GlobalIsMultiSelect);
            l.SelectedIndex = 0;
        }
        private void HandleFromListKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Space)
            {
                System.Windows.Controls.ListBox l = sender as System.Windows.Controls.ListBox;
                int selected = l.SelectedIndex;
                //e.Handled = true;
                if (selectToCallBack != null)
                    selectToCallBack(GlobalFrom, GlobalTo, GlobalParent, GlobalIsParent, GlobalIsMultiSelect);
                if (selected >= l.Items.Count)
                {
                    selected = l.Items.Count - 1;
                }
                l.SelectedIndex = selected;
                System.Windows.Controls.ListBoxItem li = l.SelectedItem as System.Windows.Controls.ListBoxItem;
                var listBoxItem = (System.Windows.Controls.ListBoxItem)l.ItemContainerGenerator.ContainerFromItem(l.SelectedItem);
                //listBoxItem.Focus();
            }
            else if (e.Key == System.Windows.Input.Key.Down || e.Key == System.Windows.Input.Key.Up)
            {
                System.Windows.Controls.ListBox l = sender as System.Windows.Controls.ListBox;
                var listBoxItem = (System.Windows.Controls.ListBoxItem)l.ItemContainerGenerator.ContainerFromItem(l.SelectedItem);
                if (listBoxItem != null)
                {
                    listBoxItem.Focus();
                }
            }
            else if (e.Key == System.Windows.Input.Key.Tab && System.Windows.Input.Keyboard.Modifiers != System.Windows.Input.ModifierKeys.Shift)
            {
                GlobalTo.Focus();

            }
        }
        private void HandleToListKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Space)
            {
                System.Windows.Controls.ListBox l = sender as System.Windows.Controls.ListBox;
                int selected = l.SelectedIndex;
                //e.Handled = true;
                if (selectFromCallBack != null)
                    selectFromCallBack(GlobalFrom, GlobalTo, GlobalParent, GlobalIsParent, GlobalIsMultiSelect);
                //l.SelectedIndex = 0;
                if (selected >= l.Items.Count)
                {
                    selected = l.Items.Count - 1;
                }
                l.SelectedIndex = selected;
            }
            else if (e.Key == System.Windows.Input.Key.Down || e.Key == System.Windows.Input.Key.Up)
            {
                System.Windows.Controls.ListBox l = sender as System.Windows.Controls.ListBox;
                var listBoxItem = (System.Windows.Controls.ListBoxItem)l.ItemContainerGenerator.ContainerFromItem(l.SelectedItem);
                listBoxItem.Focus();
            }

        }

    }

}
