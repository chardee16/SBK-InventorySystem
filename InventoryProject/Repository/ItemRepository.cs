using Dapper;
using InventoryProject.Models;
using InventoryProject.Models.ItemModule;
using InventoryProject.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static InventoryProject.Repository.SalesRepository;

namespace InventoryProject.Repository
{
    public class ItemRepository : BaseRepository
    {
        SQLFile sqlFile = new SQLFile();
        Config _config = new Config();
        APIKey api = new APIKey();
        public List<ItemCategoryClass> GetCategoryList()
        {
            //List<ItemCategoryClass> toReturn = new List<ItemCategoryClass>();
            //try
            //{
            //    this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\GetCategoryList.sql";
            //    return Connection.Query<ItemCategoryClass>(this.sqlFile.sqlQuery).ToList();
            //}
            //catch (Exception ex)
            //{
            //    return toReturn;
            //}

            var categorylist = new List<ItemCategoryClass>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = $"{api.http}/api/item/get_category";

                    client.DefaultRequestHeaders.Add("KEY", api.key);
                    client.DefaultRequestHeaders.Add("Accept", api.accept);
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);

                    HttpResponseMessage response = client.GetAsync(url).Result;
                    string json = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                        return categorylist;

                    var result = JsonConvert.DeserializeObject<ApiResponse<List<ItemCategoryClass>>>(json);

                    if (result != null && result.status == "SUCCESS" && result.data != null)
                    {
                        categorylist = result.data;
                        foreach (var item in categorylist)
                        {
                            item.CategoryDescription = item.CategoryDesc;
                            item.CategoryID = item.id;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // log error if needed
            }

            return categorylist;

        }


        public List<ItemLogs> GetItemLogs(Int32 ItemCode)
        {
            //List<ItemLogs> toReturn = new List<ItemLogs>();
            //try
            //{
            //    this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\ItemLogs.sql";
            //    sqlFile.setParameter("_ItemCode", ItemCode.ToString());
            //    return Connection.Query<ItemLogs>(this.sqlFile.sqlQuery).ToList();
            //}
            //catch (Exception ex)
            //{
            //    return toReturn;
            //}

            var toReturn = new List<ItemLogs>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = $"{api.http}/api/item/get_item_logs/{ItemCode}";

                    client.DefaultRequestHeaders.Add("KEY", api.key);
                    client.DefaultRequestHeaders.Add("Accept", api.accept);
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);

                    HttpResponseMessage response = client.GetAsync(url).Result;
                    string json = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                        return toReturn;

                    var result = JsonConvert.DeserializeObject<ApiResponse<List<ItemLogs>>>(json);

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


        public List<UnitClass> GetUnitList()
        {
            //List<UnitClass> toReturn = new List<UnitClass>();
            //try
            //{
            //    this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\GetUnitList.sql";
            //    return Connection.Query<UnitClass>(this.sqlFile.sqlQuery).ToList();
            //}
            //catch (Exception ex)
            //{
            //    return toReturn;
            //}


            var toReturn = new List<UnitClass>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = $"{api.http}/api/item/get_units";

                    client.DefaultRequestHeaders.Add("KEY", api.key);
                    client.DefaultRequestHeaders.Add("Accept", api.accept);
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);

                    HttpResponseMessage response = client.GetAsync(url).Result;
                    string json = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                        return toReturn;

                    var result = JsonConvert.DeserializeObject<ApiResponse<List<UnitClass>>>(json);

                    if (result != null && result.status == "SUCCESS" && result.data != null)
                    {
                        toReturn = result.data;

                        foreach (var item in toReturn)
                        {
                            item.UnitID = item.id;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // log error if needed
            }

            return toReturn;

        }



        public Boolean InsertItem(ItemClass item)
        {
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\InsertItem.sql";
                sqlFile.setParameter("_ItemName", item.ItemName);
                sqlFile.setParameter("_ItemDescription", item.ItemDescription);
                sqlFile.setParameter("_CategoryID", item.CategoryID.ToString());
                sqlFile.setParameter("_UnitID", item.UnitID.ToString());
                sqlFile.setParameter("_Price", item.Price.ToString());
                sqlFile.setParameter("_Barcode", item.Barcode);
                sqlFile.setParameter("_Stock", item.Stock.ToString());
                sqlFile.setParameter("_UserID", 1.ToString());
                sqlFile.setParameter("_BranchCode", 1.ToString());
                sqlFile.setParameter("_TransactionCode", 1.ToString());
                sqlFile.setParameter("_TransYear", 2020.ToString());
                sqlFile.setParameter("_TransactionDate", DateTime.Today.ToString("yyyy-MM-dd"));


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



        public Boolean AddStock(ItemClass item)
        {
            //try
            //{
            //    this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\AddStock.sql";
            //    sqlFile.setParameter("_ItemCode", item.ItemCode.ToString());
            //    sqlFile.setParameter("_Stock", item.Stock.ToString());
            //    sqlFile.setParameter("_Price", item.Price.ToString());
            //    sqlFile.setParameter("_ExpiryDate", item.ExpiryDate);
            //    sqlFile.setParameter("_UserID", 1.ToString());
            //    sqlFile.setParameter("_BranchCode", 1.ToString());
            //    sqlFile.setParameter("_TransactionCode", 2.ToString());
            //    sqlFile.setParameter("_TransYear", DateTime.Today.Year.ToString());
            //    sqlFile.setParameter("_TransactionDate", DateTime.Today.ToString("yyyy-MM-dd"));


            //    var affectedRow = Connection.Execute(sqlFile.sqlQuery);


            //    if (affectedRow > 0)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //catch
            //{
            //    return false;
            //}

            try
            {
            
                using (HttpClient client = new HttpClient())
                {
                    var url = $"{api.http}/api/item/add_stock";

                    client.DefaultRequestHeaders.Add("KEY", api.key);
                    client.DefaultRequestHeaders.Add("accept", api.accept);
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);

                    var load = new
                    {

                        user_id = 1.ToString(),
                        item_code = item.ItemCode,
                        stock = item.Stock,
                        price = item.Price,
                        expiry_date = Convert.ToDateTime(item.ExpiryDate).ToString("yyyy/MM/dd"),
                        account_code = 1001,
                        updtag = 1,

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




        public List<ItemClass> GetItemList()
        {
            //List<ItemClass> toReturn = new List<ItemClass>();
            //try
            //{
            //    this.sqlFile.sqlQuery = _config.SQLDirectory + "InventoryManagement\\GetProductList.sql";
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
                            item.GenericName = item.GenericName;
                            item.CategoryDescription = item.CategoryDesc;
                            item.SupplierDescription = item.Supplier;
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



        public Boolean UpdateItem(ItemClass item)
        {
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\UpdateItem.sql";
                sqlFile.setParameter("_ItemCode", item.ItemCode.ToString());
                sqlFile.setParameter("_ItemName", item.ItemName);
                sqlFile.setParameter("_ItemDescription", item.ItemDescription);
                sqlFile.setParameter("_CategoryID", item.CategoryID.ToString());
                sqlFile.setParameter("_UnitID", item.UnitID.ToString());
                sqlFile.setParameter("_Price", item.Price.ToString());
                sqlFile.setParameter("_Barcode", item.Barcode);
                

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




    }//end of class



}
