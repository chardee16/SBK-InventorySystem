using Dapper;
using InventoryProject.Models;
using InventoryProject.Models.SalesModule;
using InventoryProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Repository
{
    public class SalesRepository : BaseRepository
    {
        SQLFile sqlFile = new SQLFile();
        Config _config = new Config();



        public List<ItemClass> GetItemList()
        {
            List<ItemClass> toReturn = new List<ItemClass>();
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\GetItemList.sql";
                return Connection.Query<ItemClass>(this.sqlFile.sqlQuery).ToList();
            }
            catch (Exception ex)
            {
                return toReturn;
            }

        }


        public Boolean InsertPayment(List<SalesItemClass> saleItemList, Decimal TenderedAmount, Decimal ChangeAmount, Decimal taxAmount, Decimal totalAmount, String id, String name, String date, Decimal discount)
        {
            try
            {
                String TransactionDetailValue = "";
                int counter = 0;
                String Last = "";
                foreach (var item in saleItemList)
                {
                    counter++;
                    if (counter == saleItemList.Count)
                    {
                        Last = ";";
                    }
                    else
                    {
                        Last = ",\n";
                    }
                    TransactionDetailValue += "(" + 1 + "," + 3 + ",@ControlNo," + DateTime.Today.ToString("yyyy") + "," + item.ItemCode
                                    + "," + item.Quantity *-1 + "," + item.Price + "," + item.Discount + "," + item.DiscountAmount
                                    + "," + item.Total + "," + 1 + ",'" + date + "'," + 1
                                    + ",'" + item.ExpiryDate + "', "+item.UnitID+", "+id+")" + Last;
                }




                this.sqlFile.sqlQuery = _config.SQLDirectory + "POS\\SavePurchase.sql";
                sqlFile.setParameter("_UserID", 1.ToString());
                sqlFile.setParameter("_BranchCode", 1.ToString());
                sqlFile.setParameter("_TransactionCode", 3.ToString());
                sqlFile.setParameter("_TransYear", DateTime.Today.ToString("yyyy"));
                sqlFile.setParameter("_TransactionDate", date);
                sqlFile.setParameter("_TenderedAmount", TenderedAmount.ToString());
                sqlFile.setParameter("_ChangeAmount", ChangeAmount.ToString());
                sqlFile.setParameter("_TaxAmount", taxAmount.ToString());
                sqlFile.setParameter("_TotalAmount", totalAmount.ToString());
                sqlFile.setParameter("_ClientID", id);
                sqlFile.setParameter("_ClientName", name);
                sqlFile.setParameter("_TransactionDT", TransactionDetailValue);


                var affectedRow = Connection.Execute(sqlFile.sqlQuery);


                if (affectedRow > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public Boolean InsertOtherIncome(List<SalesItemClass> saleItemList, Decimal TenderedAmount, Decimal ChangeAmount, Decimal taxAmount, Decimal totalAmount, String id, String name, String date, Decimal discount, String Explanation)
        {
            try
            {
                String TransactionDetailValue = "";
                int counter = 0;
                String Last = "";
                foreach (var item in saleItemList)
                {
                    counter++;
                    if (counter == saleItemList.Count)
                    {
                        Last = ";";
                    }
                    else
                    {
                        Last = ",\n";
                    }
                    TransactionDetailValue += "(" + 1 + "," + 4 + ",@ControlNo," + DateTime.Today.ToString("yyyy") + "," + item.ItemCode
                                    + "," + item.Quantity + "," + item.Price + "," + item.Discount + "," + item.DiscountAmount
                                    + "," + item.Total + "," + 1 + ",'" + date + "'," + 1
                                    + ",'" + item.ExpiryDate + "', " + item.UnitID + ", " + id + ")" + Last;
                }




                this.sqlFile.sqlQuery = _config.SQLDirectory + "OtherIncome\\SaveOtherIncome.sql";
                sqlFile.setParameter("_UserID", 1.ToString());
                sqlFile.setParameter("_BranchCode", 1.ToString());
                sqlFile.setParameter("_TransactionCode", 4.ToString());
                sqlFile.setParameter("_TransYear", DateTime.Today.ToString("yyyy"));
                sqlFile.setParameter("_TransactionDate", date);
                sqlFile.setParameter("_TenderedAmount", TenderedAmount.ToString());
                sqlFile.setParameter("_ChangeAmount", ChangeAmount.ToString());
                sqlFile.setParameter("_TaxAmount", taxAmount.ToString());
                sqlFile.setParameter("_TotalAmount", totalAmount.ToString());
                sqlFile.setParameter("_ClientID", id);
                sqlFile.setParameter("_Explanation", Explanation);
                sqlFile.setParameter("_ClientName", name);
                sqlFile.setParameter("_TransactionDT", TransactionDetailValue);


                var affectedRow = Connection.Execute(sqlFile.sqlQuery);


                if (affectedRow > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


    }
}
