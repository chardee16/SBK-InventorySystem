using Dapper;
using InventoryProject.Models;
using InventoryProject.Models.SalesModule;
using InventoryProject.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Repository
{
    public class SalesRepository : BaseRepository
    {
        SQLFile sqlFile = new SQLFile();
        Config _config = new Config();
        APIKey api = new APIKey();


        public List<ItemClass> GetItemList()
        {
            //List<ItemClass> toReturn = new List<ItemClass>();
            //try
            //{
            //    this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\GetItemList.sql";
            //    return Connection.Query<ItemClass>(this.sqlFile.sqlQuery).ToList();
            //}
            //catch (Exception ex)
            //{
            //    return toReturn;
            //}

            var itemlist = new List<ItemClass>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = $"{api.http}/api/item/get_item_list";

                    client.DefaultRequestHeaders.Add("KEY", api.key);
                    client.DefaultRequestHeaders.Add("Accept", api.accept);
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);

                    HttpResponseMessage response = client.GetAsync(url).Result;
                    string json = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                        return itemlist;

                    var result = JsonConvert.DeserializeObject<ApiResponse<List<ItemClass>>>(json);

                    if (result != null && result.status == "SUCCESS" && result.data != null)
                    {
                        itemlist = result.data;


                        foreach (var item in itemlist)
                        {
                            item.CategoryDescription = item.CategoryDesc;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // log error if needed
            }

            return itemlist;


        }

        public class TransactionDetails
        { 
            public string item_code { get; set; }
            public string quantity { get; set; }
            public string price { get; set; }
            public string discount { get; set; }
            public string discount_amount { get; set; }
            public string total_amount { get; set; }
            public string expiry_date { get; set; }
            public string unit_id { get; set; }
            public string client_id { get; set; }
        }


        public Boolean InsertPayment(List<SalesItemClass> saleItemList, Decimal TenderedAmount, Decimal ChangeAmount, Decimal taxAmount, Decimal totalAmount, String id, String name, String date, Decimal discount)
        {
            try
            {
                String TransactionDetailValue = "";
                int counter = 0;
                String Last = "";
                Int64 itemCode = 0;
                decimal itemQuantity = 0;
                decimal itemPrice = 0;
                decimal itemDiscount = 0;
                decimal itemDiscountAmount = 0;
                int itemUnitID = 0;
                string expiryDate = "";

                List<TransactionDetails> transaction_details = new List<TransactionDetails>();

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

                    itemCode = item.ItemCode;
                    itemQuantity = item.Quantity;
                    itemPrice = item.Price;
                    itemDiscount = item.Discount;
                    itemDiscountAmount = item.DiscountAmount;
                    itemUnitID = item.UnitID;
                    expiryDate = item.ExpiryDate;

                    transaction_details.Add(new TransactionDetails()
                    {
                        item_code = itemCode.ToString(),
                        quantity = itemQuantity.ToString(),
                        price = itemPrice.ToString(),
                        discount = itemDiscount.ToString(),
                        discount_amount = itemDiscountAmount.ToString(),
                        total_amount = item.Total.ToString(),
                        expiry_date = expiryDate.ToString(),
                        unit_id = itemUnitID.ToString(),
                        client_id = id,
                    });

                }

                //this.sqlFile.sqlQuery = _config.SQLDirectory + "POS\\SavePurchase.sql";
                //sqlFile.setParameter("_UserID", 1.ToString());
                //sqlFile.setParameter("_BranchCode", 1.ToString());
                //sqlFile.setParameter("_TransactionCode", 3.ToString());
                //sqlFile.setParameter("_TransYear", DateTime.Today.ToString("yyyy"));
                //sqlFile.setParameter("_TransactionDate", date);
                //sqlFile.setParameter("_TenderedAmount", TenderedAmount.ToString());
                //sqlFile.setParameter("_ChangeAmount", ChangeAmount.ToString());
                //sqlFile.setParameter("_TaxAmount", taxAmount.ToString());
                //sqlFile.setParameter("_TotalAmount", totalAmount.ToString());
                //sqlFile.setParameter("_ClientID", id);
                //sqlFile.setParameter("_ClientName", name);
                //sqlFile.setParameter("_TransactionDT", TransactionDetailValue);


                //var affectedRow = Connection.Execute(sqlFile.sqlQuery);


                //if (affectedRow > 0)
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}


                using (HttpClient client = new HttpClient())
                {
                    var url = $"{api.http}/api/pos/save_purchase";

                    client.DefaultRequestHeaders.Add("KEY", api.key);
                    client.DefaultRequestHeaders.Add("accept", api.accept);
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);

                    var load = new
                    {
                       
                        user_id = 1.ToString(),
                        client_id = id,
                        client_name = name,
                        transaction_details,
                        tax_amount = taxAmount.ToString(),
                        tendered_amount = TenderedAmount.ToString(),
                        change_amount = ChangeAmount.ToString(),
                        total_amount = totalAmount.ToString(),

                        //item_code = itemCode.ToString(),
                        //quantity = itemQuantity.ToString(),
                        //price = itemPrice.ToString(),
                        //discount = itemDiscount.ToString(),
                        //discount_amount = itemDiscountAmount.ToString(),
                        //unit_id = itemUnitID.ToString(),
                        //expiry_date = expiryDate,
                    };

                    var json = JsonConvert.SerializeObject(load);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(url, content).Result;

                    //var responseBody = response.Content.ReadAsStringAsync().Result;
                    //Console.WriteLine(responseBody);

                    return true;
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
