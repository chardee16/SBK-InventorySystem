using Dapper;
using InventoryProject.Models;
using InventoryProject.Models.ClientDiscountDeliveryModule;
using InventoryProject.Models.ClientModule;
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
using System.Windows.Forms;
using System.Xml.Linq;
using static InventoryProject.Repository.SalesRepository;

namespace InventoryProject.Repository
{
    public class DiscountDeliveryRepository : BaseRepository
    {
        SQLFile sqlFile = new SQLFile();
        Config _config = new Config();
        APIKey api = new APIKey();


        public List<ItemClass> GetProductList()
        {
            //List<ItemClass> toReturn = new List<ItemClass>();
            //try
            //{
            //    this.sqlFile.sqlQuery = _config.SQLDirectory + "DiscountDelivery\\GetDeliveryProductList.sql";
            //    return Connection.Query<ItemClass>(this.sqlFile.sqlQuery).ToList();
            //}
            //catch (Exception ex)
            //{
            //    return toReturn;
            //}

            var productlist = new List<ItemClass>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = $"{api.http}/api/inventory/get_product_list";

                    client.DefaultRequestHeaders.Add("KEY", api.key);
                    client.DefaultRequestHeaders.Add("Accept", api.accept);
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);

                    HttpResponseMessage response = client.GetAsync(url).Result;
                    string json = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                        return productlist;

                    var result = JsonConvert.DeserializeObject<ApiResponse2<ItemClass>>(json);

                    if (result != null && result.status == "SUCCESS" && result.data != null)
                    {
                        productlist = result.data;
                    }
                }
            }
            catch (Exception ex)
            {
                // log error if needed
            }

            return productlist;

        }


        public List<ItemClass> GetClientProductList(Int64 ClientID)
        {
            //List<ItemClass> toReturn = new List<ItemClass>();
            //Int64 clientid = ClientID;
            //try
            //{
            //    this.sqlFile.sqlQuery = _config.SQLDirectory + "DiscountDelivery\\GetClientDiscountProductList.sql";

            //    sqlFile.setParameter("_ClientID", clientid.ToString());

            //    return Connection.Query<ItemClass>(this.sqlFile.sqlQuery).ToList();
            //}
            //catch (Exception ex)
            //{
            //    return toReturn;
            //}


            var productlist = new List<ItemClass>();
            Int64 clientid = ClientID;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = $"{api.http}/api/delivery/get_client_discount_product_list/{clientid}";

                    client.DefaultRequestHeaders.Add("KEY", api.key);
                    client.DefaultRequestHeaders.Add("Accept", api.accept);
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);

                    HttpResponseMessage response = client.GetAsync(url).Result;
                    string json = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                        return productlist;

                    var result = JsonConvert.DeserializeObject<ApiResponse2<ItemClass>>(json);

                    if (result != null && result.status == "SUCCESS" && result.data != null)
                    {
                        productlist = result.data;
                    }
                }
            }
            catch (Exception ex)
            {
                // log error if needed
            }

            return productlist;

        }

        public List<ItemClass> GetUserProductList(Int64 UserID, Int64 ClientID)
        {
            List<ItemClass> toReturn = new List<ItemClass>();
            //Int64 clientid = ClientID;
            //try
            //{
            //    this.sqlFile.sqlQuery = _config.SQLDirectory + "DiscountDelivery\\GetClientDiscountProductList.sql";

            //    sqlFile.setParameter("_ClientID", clientid.ToString());

            //    return Connection.Query<ItemClass>(this.sqlFile.sqlQuery).ToList();
            //}
            //catch (Exception ex)
            //{
                return toReturn;
            //}

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


        public class ClientDiscountList
        {

            public string id { get; set; }
            public string BranchCode { get; set; }
            public string ClientID { get; set; }
            public string ItemCode { get; set; }
            public string Discount { get; set; }
        }

        public class TransactionDelivery
        {
            public string item_code { get; set; }
            public string quantity { get; set; }
            public string price { get; set; }
            public string discount { get; set; }
            public string total_discount { get; set; }
            public string amount { get; set; }
            public string delivery_id { get; set; }
            public string encoded_by { get; set; }
            public string client_id { get; set; }


        }

        public class TransactionReturn
        {
            public string item_code { get; set; }
            public string quantity { get; set; }
            public string price { get; set; }
            public string discount { get; set; }
            public string total_discount { get; set; }
            public string amount { get; set; }
            public string delivery_id { get; set; }
            public string encoded_by { get; set; }
            public string client_id { get; set; }
            public string transaction_code { get; set; }
            public string transaction_date { get; set; }

        }

        public Boolean InsertForDelivery(ObservableCollection<SalesItemClass> saleItemList, Int64 ClientID, Int64 DeliveryID, String TransactionDate, Int32? AccountCode)
        {
            try
            {

                String TransactionDetailValue = "";
                //String TransactionDetailValue2 = "";
                int counter = 0;
                Int64 clientid = ClientID;
                Int64 deliveryid = DeliveryID;
                Int32? account_code = AccountCode;
                String date = TransactionDate;
                String Last = "";
                List<TransactionDelivery> delivery_items = new List<TransactionDelivery>();
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
                    TransactionDetailValue += "(" + 1 + "," + 5 + ",@ControlNo," + DateTime.Today.ToString("yyyy") + "," + item.ItemCode
                                    + "," + item.Quantity + "," + item.Price + "," + item.Discount + "," + item.DiscountAmount
                                    + "," + item.Total + "," + 1 + ",'" + date + "'," + 1
                                    + ",'" + clientid + "', " + deliveryid + ")" + Last;





                    delivery_items.Add(new TransactionDelivery()
                    {
                        item_code = item.ItemCode.ToString(),
                        quantity = item.Quantity.ToString(),
                        price = item.Price.ToString(),
                        discount = item.Discount.ToString(),
                        total_discount = item.DiscountAmount.ToString(),
                        amount = item.Total.ToString(),
                        encoded_by = 1.ToString(),
                        client_id = clientid.ToString(),
                        delivery_id = deliveryid.ToString(),
                    });

                    transaction_details.Add(new TransactionDetails()
                    {
                        item_code = item.ItemCode.ToString(),
                        quantity = (Convert.ToInt32(item.Quantity) * -1).ToString(),
                        price = item.Price.ToString(),
                        discount = item.Discount.ToString(),
                        discount_amount = item.DiscountAmount.ToString(),
                        total_amount = item.Total.ToString(),
                        expiry_date = "1970-01-01",
                        unit_id = 0.ToString(),
                        client_id = clientid.ToString(),
                        account_code = account_code.ToString(),
                        updtag = 1.ToString(),
                    });


                    transaction_details.Add(new TransactionDetails()
                    {
                        item_code = item.ItemCode.ToString(),
                        quantity = item.Quantity.ToString(),
                        price = item.Price.ToString(),
                        discount = item.Discount.ToString(),
                        discount_amount = item.DiscountAmount.ToString(),
                        total_amount = item.Total.ToString(),
                        expiry_date = "0001-01-01",
                        unit_id = 0.ToString(),
                        client_id = clientid.ToString(),
                        account_code = 1001.ToString(),
                        updtag = 1.ToString(),
                    });

                    //this.sqlFile.sqlQuery = _config.SQLDirectory + "DiscountDelivery\\GetClientDiscount.sql";
                    //sqlFile.setParameter("_ClientID", clientid.ToString());
                    //sqlFile.setParameter("_ItemCode", item.ItemCode.ToString());

                    //int exists = Connection.ExecuteScalar<int>(sqlFile.sqlQuery);

                    //if (exists > 0)
                    //{

                    //    this.sqlFile.sqlQuery = _config.SQLDirectory + "DiscountDelivery\\UpdateClientDiscount.sql";
                    //}
                    //else
                    //{
                    //    this.sqlFile.sqlQuery = _config.SQLDirectory + "DiscountDelivery\\InsertClientDiscount.sql";
                    //}

                    //sqlFile.setParameter("_ClientID", clientid.ToString());
                    //sqlFile.setParameter("_ItemCode", item.ItemCode.ToString());
                    //sqlFile.setParameter("_Discount", item.Discount.ToString());
                    //sqlFile.setParameter("_BranchCode", 1.ToString());

                    //var affectedRowAddUpdate = Connection.Execute(sqlFile.sqlQuery);


                    #region
                    //using (HttpClient client = new HttpClient())
                    //{
                    //    var url = $"{api.http}/api/delivery/get_client_discount/{clientid}/{item.ItemCode}";

                    //    client.DefaultRequestHeaders.Add("KEY", api.key);
                    //    client.DefaultRequestHeaders.Add("accept", api.accept);
                    //    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);


                    //    HttpResponseMessage response = client.GetAsync(url).Result;

                    //    var json = response.Content.ReadAsStringAsync().Result;
                    //    var result = JsonConvert.DeserializeObject<ApiResponse<ClientDiscountList>>(json);

                    //    if (result != null && result.data != null)
                    //    {
                    //        var urlupdate = $"{api.http}/api/delivery/update_client_discount";

                    //        var load = new
                    //        {
                    //            client_id = clientid.ToString(),
                    //            item_code = item.ItemCode.ToString(),
                    //            discount = item.Discount.ToString(),
                    //        };

                    //        var jsonupdate = JsonConvert.SerializeObject(load);

                    //        var contentupdate = new StringContent(jsonupdate, Encoding.UTF8, "application/json");

                    //        HttpResponseMessage responseupdate = client.PostAsync(urlupdate, contentupdate).Result;

                    //    }
                    //    else
                    //    {
                    //        var urlinsert = $"{api.http}/api/delivery/insert_client_discount";

                    //        var load = new
                    //        {
                    //            client_id = clientid.ToString(),
                    //            item_code = item.ItemCode.ToString(),
                    //            discount = item.Discount.ToString(),
                    //        };

                    //        var jsoninsert = JsonConvert.SerializeObject(load);

                    //        var contentinsert = new StringContent(jsoninsert, Encoding.UTF8, "application/json");

                    //        HttpResponseMessage responseinsert = client.PostAsync(urlinsert, contentinsert).Result;

                    //    }

                    //}
                    #endregion

                }

                //this.sqlFile.sqlQuery = _config.SQLDirectory + "DiscountDelivery\\SaveForDelivery.sql";

                //sqlFile.setParameter("_TransactionCode", 5.ToString());
                //sqlFile.setParameter("_TransYear", DateTime.Today.ToString("yyyy"));
                //sqlFile.setParameter("_TransactionDL", TransactionDetailValue);

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
                    var url = $"{api.http}/api/delivery/save_delivery";

                    client.DefaultRequestHeaders.Add("KEY", api.key);
                    client.DefaultRequestHeaders.Add("accept", api.accept);
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);

                    var saveload = new
                    {
                        user_id = 1.ToString(),
                        delivery_items,

                    };

                    var json = JsonConvert.SerializeObject(saveload);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(url, content).Result;

                    if (response.Content != null)
                    {

                        var savepurchase = $"{api.http}/api/pos/save_purchase";

                        var load = new
                        {

                            user_id = 1.ToString(),
                            client_id = "",
                            client_name = "",
                            transaction_code = 5,
                            transaction_details,
                            tax_amount = 0.ToString(),
                            tendered_amount = 0.ToString(),
                            change_amount = 0.ToString(),
                            total_amount = 0.ToString(),

                            //item_code = itemCode.ToString(),
                            //quantity = itemQuantity.ToString(),
                            //price = itemPrice.ToString(),
                            //discount = itemDiscount.ToString(),
                            //discount_amount = itemDiscountAmount.ToString(),
                            //unit_id = itemUnitID.ToString(),
                            //expiry_date = expiryDate,
                        };

                        var json2 = JsonConvert.SerializeObject(load);

                        var content2 = new StringContent(json2, Encoding.UTF8, "application/json");

                        HttpResponseMessage response2 = client.PostAsync(savepurchase, content2).Result;




                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
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
            public string account_code { get; set; }
            public string updtag { get; set; }
        }


        public Boolean InsertForClientDiscount(Int64 ClientID, Int64 ItemCode, Decimal DiscountPrice)
        {
            try
            {
             
                Int64 clientid = ClientID;
                Int64 itemcode = ItemCode;
                Decimal discountprice = DiscountPrice;
                List<TransactionDelivery> delivery_items = new List<TransactionDelivery>();

                using (HttpClient client = new HttpClient())
                {
                    var url = $"{api.http}/api/delivery/get_client_discount/{clientid}/{itemcode}";

                    client.DefaultRequestHeaders.Add("KEY", api.key);
                    client.DefaultRequestHeaders.Add("accept", api.accept);
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);


                    HttpResponseMessage response = client.GetAsync(url).Result;

                    var json = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<ApiResponse<ClientDiscountList>>(json);

                    if (result != null && result.data != null)
                    {
                        var urlupdate = $"{api.http}/api/delivery/update_client_discount";

                        var load = new
                        {
                            client_id = clientid.ToString(),
                            item_code = itemcode.ToString(),
                            discount = discountprice.ToString(),
                        };

                        var jsonupdate = JsonConvert.SerializeObject(load);

                        var contentupdate = new StringContent(jsonupdate, Encoding.UTF8, "application/json");

                        HttpResponseMessage responseupdate = client.PostAsync(urlupdate, contentupdate).Result;

                    }
                    else
                    {
                        var urlinsert = $"{api.http}/api/delivery/insert_client_discount";

                        var load = new
                        {
                            client_id = clientid.ToString(),
                            item_code = itemcode,
                            discount = discountprice,
                        };

                        var jsoninsert = JsonConvert.SerializeObject(load);

                        var contentinsert = new StringContent(jsoninsert, Encoding.UTF8, "application/json");

                        HttpResponseMessage responseinsert = client.PostAsync(urlinsert, contentinsert).Result;

                    }


                    return true;

                }   
            }
            catch
            {
                return false;
            }
        }


        public List<DeliveryStatusClass> GetDeliveryStatusList(Int64 UserID, String Date)
        {
            //List<DeliveryStatusClass> toReturn = new List<DeliveryStatusClass>();
            //Int64 clientid = UserID;
            //try
            //{
            //    this.sqlFile.sqlQuery = _config.SQLDirectory + "DiscountDelivery\\GetDeliveryStatusList.sql";

            //    sqlFile.setParameter("_UserID", clientid.ToString());
            //    sqlFile.setParameter("_Date", Date);

            //    return Connection.Query<DeliveryStatusClass>(this.sqlFile.sqlQuery).ToList();
            //}
            //catch (Exception ex)
            //{
            //    return toReturn;
            //}


            var toReturn = new List<DeliveryStatusClass>();
            Int64 userid = UserID;
            String date = Convert.ToDateTime(Date).ToString("yyyy-MM-dd");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = $"{api.http}/api/delivery/get_delivery_status_list/{userid}/{date}";

                    client.DefaultRequestHeaders.Add("KEY", api.key);
                    client.DefaultRequestHeaders.Add("Accept", api.accept);
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);

                    HttpResponseMessage response = client.GetAsync(url).Result;


                    string json = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                        return toReturn;

                    var result = JsonConvert.DeserializeObject<ApiResponse2<DeliveryStatusClass>>(json);

                    if (result != null && result.status == "SUCCESS" && result.data != null)
                    {
                        toReturn = result.data;
                    }
                }
            }
            catch (Exception ex)
            {
                // log error if needed
            }

            return toReturn;

        }


        public Boolean InsertForReturnItems(ObservableCollection<SalesItemClass> saleItemList, Int64 ClientID, Int64 DeliveryID, String TransactionDate)
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
                List<TransactionReturn> delivery_items = new List<TransactionReturn>();

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
                    TransactionDetailValue += "(" + 1 + "," + 7 + ",@ControlNo," + DateTime.Today.ToString("yyyy") + "," + item.ItemCode
                                    + "," + item.Quantity + "," + item.Price + "," + item.Discount + "," + item.DiscountAmount
                                    + "," + item.Total + "," + 1 + ",'" + date + "'," + 1
                                    + ",'" + clientid + "', " + deliveryid + ")" + Last;





                    delivery_items.Add(new TransactionReturn()
                    {
                        item_code = item.ItemCode.ToString(),
                        quantity = item.Quantity.ToString(),
                        price = item.Price.ToString(),
                        discount = item.Discount.ToString(),
                        total_discount = item.DiscountAmount.ToString(),
                        amount = item.Total.ToString(),
                        encoded_by = 1.ToString(),
                        client_id = clientid.ToString(),
                        delivery_id = deliveryid.ToString(),
                        transaction_code = 7.ToString(),
                        transaction_date = date

                    });
                }
              

                using (HttpClient client = new HttpClient())
                {
                    var url = $"{api.http}/api/delivery/save_return";

                    client.DefaultRequestHeaders.Add("KEY", api.key);
                    client.DefaultRequestHeaders.Add("accept", api.accept);
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);

                    var saveload = new
                    {
                        user_id = 1.ToString(),
                        delivery_items,

                    };

                    var json = JsonConvert.SerializeObject(saveload);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(url, content).Result;

                    if (response.Content != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }

        }

        public List<ReturnListClass> GetReturnItemList(Int32? AccountCode, String TransactionDate)
        {

            var productlist = new List<ReturnListClass>();
            string date = Convert.ToDateTime(TransactionDate).ToString("yyyy-MM-dd");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = $"{api.http}/api/delivery/get_return_item_list/{AccountCode}/{date}";

                    client.DefaultRequestHeaders.Add("KEY", api.key);
                    client.DefaultRequestHeaders.Add("Accept", api.accept);
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);

                    HttpResponseMessage response = client.GetAsync(url).Result;
                    string json = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                        return productlist;

                    var result = JsonConvert.DeserializeObject<ApiResponse2<ReturnListClass>>(json);

                    if (result != null && result.status == "SUCCESS" && result.data != null)
                    {
                        productlist = result.data;
                    }

                    return productlist;
                }
            }
            catch (Exception ex)
            {
                // log error if needed
            }

            return productlist;
        }

        public Boolean ReturnItems(Int32 TransactionCode, Int32 CTLNo, String TransactionDate)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = $"{api.http}/api/delivery/return_items";

                    client.DefaultRequestHeaders.Add("KEY", api.key);
                    client.DefaultRequestHeaders.Add("accept", api.accept);
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);

                    var load = new
                    {
                        transaction_code = TransactionCode.ToString(),
                        transaction_date = TransactionDate,
                        ctl_no = CTLNo.ToString()

                    };

                    var json = JsonConvert.SerializeObject(load);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(url, content).Result;

                    if (response.Content != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }

        }

    }
}
