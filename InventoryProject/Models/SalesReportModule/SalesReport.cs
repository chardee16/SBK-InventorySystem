using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models.SalesReportModule
{
    public class SalesReport
    {
        public Int64 CTLNo { get; set; }
        public Decimal Amt { get; set; }
        public Decimal Price { get; set; }
        public String GenericName { get; set; }
        public String CategoryDescription { get; set; }
        public String CategoryDesc { get; set; }
        public String UnitDescription { get; set; }
        public Int32 UnitID { get; set; }
        public Int32 CategoryID { get; set; }
        public Int32 ItemGenericID { get; set; }
        public String TransactionDate { get; set; }
        public Decimal Quantity { get; set; }
        public String ItemDescription { get; set; }

    }
}
