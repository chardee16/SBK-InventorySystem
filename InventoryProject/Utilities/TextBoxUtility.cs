using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Utilities
{
    public class TextBoxUtility
    {
        System.Windows.Controls.ToolTip ToolTip;
        System.Windows.Controls.DatePicker CalendarView;
        System.Windows.Controls.TextBox CalendarField;
        private static readonly System.ComponentModel.BackgroundWorker ToolTipWorker = new System.ComponentModel.BackgroundWorker();
        /// <summary>
        /// Filters accepted amount Filter Format
        /// </summary>
        /// <param name="Field"></param>
        public void AmountFilter(System.Windows.Controls.TextBox Field)
        {
            Field.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(AmountFilter_KeyDown);
            Field.GotFocus += new System.Windows.RoutedEventHandler(SelectAllOnFocus);
            ToolTipWorker.DoWork += ToolTipWorker_DoWork;
            ToolTipWorker.RunWorkerCompleted += ToolTipWorker_RunWorkerCompleted;
        }
        //Amount Filter KeyDown Hnadler
        private void AmountFilter_KeyDown(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!MatchAmountFilter(e.Text))
            {
                System.Windows.Controls.TextBox Field = sender as System.Windows.Controls.TextBox;
                try
                {
                    ToolTip = ((System.Windows.Controls.ToolTip)Field.ToolTip);
                    if (((System.Windows.Controls.ToolTip)Field.ToolTip) != null)
                    {
                        ToolTip.PlacementTarget = Field;
                        ToolTip.IsOpen = true;
                        if (!ToolTipWorker.IsBusy)
                        {
                            ToolTipWorker.RunWorkerAsync();
                        }
                    }
                }
                catch (Exception ex) { }
            }

            e.Handled = !MatchAmountFilter(e.Text);
        }
        //Worker for ToolTip Display
        private void ToolTipWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
        }
        //worker done for ToolTip Display
        private void ToolTipWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            ToolTip.IsOpen = false;
        }
        //Amount Filter matching Function
        private bool MatchAmountFilter(string text)
        {
            Boolean valid = false;
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[<>=0-9.]$");

            valid = regex.IsMatch(text);

            if (valid)
            {
                int countGT = text.Count(f => f == '>');
                int countLT = text.Count(f => f == '<');
                int countEqual = text.Count(f => f == '=');

                if (countGT > 1)
                {
                    valid = false;
                }
                else if (countLT > 1)
                {
                    valid = false;
                }
                else
                {
                    if (countEqual > 2)
                    {
                        valid = false;
                    }
                }

            }
            return valid;
        }

        public String SelectedDate { get; set; }

        public void DatePicker(System.Windows.Controls.TextBox Field, System.Windows.Controls.DatePicker Calendar = null)
        {
            this.CalendarView = Calendar;
            this.CalendarField = Field;
            this.CalendarView.Focus();
            this.CalendarView.Visibility = System.Windows.Visibility.Hidden;
            this.CalendarField.GotFocus += new System.Windows.RoutedEventHandler(SelectAllOnFocus);
            this.CalendarField.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(DatePickerKeyDownHandler);
            this.CalendarField.TextChanged += CalendarFieldTextChanged;

            this.CalendarView.CalendarClosed += CalendarClosed;
            this.CalendarView.PreviewMouseUp += CalendarMouseDown;
            this.CalendarView.PreviewKeyDown += CalendarKeyDownHandler;


        }


        private void CalendarMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DateTime sel = (DateTime)this.CalendarView.SelectedDate;
            this.CalendarField.Text = sel.ToString("MM/dd/yyyy");
        }


        private void CalendarClosed(object sender, System.Windows.RoutedEventArgs e)
        {
            this.CalendarField.Focus();
        }
        private void CalendarFieldTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                Console.WriteLine("asdasdsadas");
                DateTime sel = Convert.ToDateTime(this.CalendarField.Text);
                this.CalendarView.SelectedDate = sel;
                this.CalendarView.DisplayDate = sel;

            }
            catch (Exception ex)
            {
                if (this.SelectedDate != null)
                {
                    DateTime sel = Convert.ToDateTime(this.SelectedDate);
                    this.CalendarView.SelectedDate = sel;
                    this.CalendarView.DisplayDate = sel;
                }
            }
        }

        private void DatePickerKeyDownHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F2)
            {
                if (this.CalendarView != null)
                {
                    CalendarView.IsDropDownOpen = true;

                }
            }
        }

        private void CalendarKeyDownHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                DateTime sel = (DateTime)this.CalendarView.SelectedDate;
                this.CalendarField.Text = sel.ToString("MM/dd/yyyy");
            }
        }

        private void SelectAllOnFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.Controls.TextBox Field = sender as System.Windows.Controls.TextBox;
            Field.SelectAll();
        }
    }
}
