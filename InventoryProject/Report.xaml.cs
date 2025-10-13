using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace InventoryProject
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        public ReportDocument cryRpt;
        public ReportDocument printing;
        public Report()
        {
            InitializeComponent();
        }

        private void print_Click(object sender, RoutedEventArgs e)
        {
            printDocument();
        }

        private void exportExcel(object sender, RoutedEventArgs e)
        {
            //ButtonAutomationPeer peer = new ButtonAutomationPeer(this.focuser);
            //IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider; invokeProv.Invoke();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            path += "\\Inventory Reports\\Excel";
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                // handle them here
            }


            path += "\\" + this.Title;

            DateTime nw = DateTime.Now;


            path += DateTime.Now.ToString("yyyymmddHHmmss") + ".xls";

            try
            {
                var myObject = this.Owner as MainWindow;

                ExportOptions CrExportOptions;

                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                ExcelFormatOptions CrFormatTypeOptions = new ExcelFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = path;
                CrExportOptions = cryRpt.ExportOptions;
                CrFormatTypeOptions.ExcelUseConstantColumnWidth = true;
                CrFormatTypeOptions.ExcelConstantColumnWidth = 1500;
                CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                CrExportOptions.ExportFormatType = ExportFormatType.Excel;
                CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                CrExportOptions.FormatOptions = CrFormatTypeOptions;
                cryRpt.Export();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            MessageBox.Show("Export Complete");
            ProcessStartInfo startInfo = new ProcessStartInfo(path);
            Process.Start(startInfo);
        }

        private void exportToPDF_Click(object sender, RoutedEventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            path += "\\Inventory Reports\\PDF";
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                // handle them here
            }


            path += "\\" + this.Title;

            DateTime nw = DateTime.Now;


            path += DateTime.Now.ToString("yyyymmddHHmmss") + ".pdf";
            try
            {
                var myObject = this.Owner as MainWindow;

                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = path;
                CrExportOptions = cryRpt.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                cryRpt.Export();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            ProcessStartInfo startInfo = new ProcessStartInfo(path);
            Process.Start(startInfo);
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        public void printDocument()
        {

            System.Windows.Forms.PrintDialog printer = new System.Windows.Forms.PrintDialog();
            printer.AllowSomePages = true;
            if (printer.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Printing.PrinterSettings printerSettings = new System.Drawing.Printing.PrinterSettings();
                int copies = printer.PrinterSettings.Copies;
                int fromPage = printer.PrinterSettings.FromPage;
                int toPage = printer.PrinterSettings.ToPage;
                bool collate = printer.PrinterSettings.Collate;

                printing.PrintOptions.PrinterName = printer.PrinterSettings.PrinterName;
                printing.PrintToPrinter(copies, collate, fromPage, toPage);
            }
        }
    }
}
