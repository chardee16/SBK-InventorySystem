using Dapper;
using InventoryProject.Models;
using InventoryProject.Models.ClientDiscountDeliveryModule;
using InventoryProject.Models.SalesModule;
using InventoryProject.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Repository
{
    public class DiscountDeliveryRepository : BaseRepository
    {
        SQLFile sqlFile = new SQLFile();
        Config _config = new Config();
        APIKey api = new APIKey();


        public List<ItemClass> GetProductList()
        {
            List<ItemClass> toReturn = new List<ItemClass>();
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "DiscountDelivery\\GetDeliveryProductList.sql";
                return Connection.Query<ItemClass>(this.sqlFile.sqlQuery).ToList();
            }
            catch (Exception ex)
            {
                return toReturn;
            }

        }

        public List<ItemDeliveryClass> GetDeliveryList()
        {
            List<ItemDeliveryClass> toReturn = new List<ItemDeliveryClass>();
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "DiscountDelivery\\GetDeliveryList.sql";
                return Connection.Query<ItemDeliveryClass>(this.sqlFile.sqlQuery).ToList();
            }
            catch (Exception ex)
            {
                return toReturn;
            }

        }


        public Boolean InsertDelivery(ItemDeliveryClass delivery)
        {
            try
            {
                if (delivery.DeliveryID > 0)
                {
                    this.sqlFile.sqlQuery = _config.SQLDirectory + "DiscountDelivery\\UpdateDelivery.sql";
                }
                else
                {
                    this.sqlFile.sqlQuery = _config.SQLDirectory + "DiscountDelivery\\InsertDelivery.sql";
                }

                sqlFile.setParameter("_DeliveryID", delivery.DeliveryID.ToString());
                sqlFile.setParameter("_DeliveryDescription", delivery.DeliveryDescription);

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
            catch (Exception ex)
            {
                return false;
            }
        }

        public Boolean InsertForDelivery(ObservableCollection<SalesItemClass> saleItemList, Int64 ClientID, Int64 DeliveryID, String TransactionDate)
        {
            try
            {

                String TransactionDetailValue = "";
                //String TransactionDetailValue2 = "";
                int counter = 0;
                Int64 clientid = ClientID;
                Int64 deliveryid = DeliveryID;
                String date = TransactionDate;
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
                    TransactionDetailValue += "(" + 1 + "," + 5 + ",@ControlNo," + DateTime.Today.ToString("yyyy") + "," + item.ItemCode
                                    + "," + item.Quantity + "," + item.Price + "," + item.Discount + "," + item.DiscountAmount
                                    + "," + item.Total + "," + 1 + ",'" + date + "'," + 1
                                    + ",'" + clientid + "', " + deliveryid + ")" + Last;

                    //TransactionDetailValue2 += "(" + 1 + "," + clientid + "," + item.ItemCode + "," + item.Discount+ ")" + Last;


                    this.sqlFile.sqlQuery = _config.SQLDirectory + "DiscountDelivery\\GetClientDiscount.sql";
                    sqlFile.setParameter("_ClientID", clientid.ToString());
                    sqlFile.setParameter("_ItemCode", item.ItemCode.ToString());

                    int exists = Connection.ExecuteScalar<int>(sqlFile.sqlQuery);

                    if (exists > 0){

                        this.sqlFile.sqlQuery = _config.SQLDirectory + "DiscountDelivery\\UpdateClientDiscount.sql";
                    }
                    else {
                        this.sqlFile.sqlQuery = _config.SQLDirectory + "DiscountDelivery\\InsertClientDiscount.sql";
                    }

                    sqlFile.setParameter("_ClientID", clientid.ToString());
                    sqlFile.setParameter("_ItemCode", item.ItemCode.ToString());
                    sqlFile.setParameter("_Discount", item.Discount.ToString());
                    sqlFile.setParameter("_BranchCode", 1.ToString());

                    var affectedRowAddUpdate = Connection.Execute(sqlFile.sqlQuery);



                }

                this.sqlFile.sqlQuery = _config.SQLDirectory + "DiscountDelivery\\SaveForDelivery.sql";

                sqlFile.setParameter("_TransactionCode", 5.ToString());
                sqlFile.setParameter("_TransYear", DateTime.Today.ToString("yyyy"));
                sqlFile.setParameter("_TransactionDL", TransactionDetailValue);

                var affectedRow = Connection.Execute(sqlFile.sqlQuery);


                if (affectedRow > 0)
                {

                    //this.sqlFile.sqlQuery = _config.SQLDirectory + "DiscountDelivery\\GetClientDiscount.sql";

                    //this.sqlFile.sqlQuery = _config.SQLDirectory + "DiscountDelivery\\InsertClientDiscount.sql";

                    //sqlFile.setParameter("_InsertClientDiscount", TransactionDetailValue2);

                    //var affectedRow2 = Connection.Execute(sqlFile.sqlQuery);


                    //if (affectedRow2 > 0)
                    //{
                    //    return true;
                    //}
                    //else
                    //{
                    //    return false;
                    //}

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
