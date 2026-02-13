using Dapper;
using InventoryProject.Models;
using InventoryProject.Models.InventoryManagementModule;
using InventoryProject.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Repository
{
    public class InventoryManagementRepository : BaseRepository
    {
        SQLFile sqlFile = new SQLFile();
        Config _config = new Config();
        APIKey api = new APIKey();

        public List<GenericMedicineClass> GetGenericMedicine()
        {
            List<GenericMedicineClass> toReturn = new List<GenericMedicineClass>();
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "InventoryManagement\\GetGenericList.sql";
                return Connection.Query<GenericMedicineClass>(this.sqlFile.sqlQuery).ToList();
            }
            catch (Exception ex)
            {
                return toReturn;
            }

        }

        public Boolean InsertCategory(ItemCategoryClass category)
        {
            try
            {
                if (category.CategoryID > 0)
                {
                    this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\UpdateCategory.sql";
                }
                else
                {
                    this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\InsertCategory.sql";
                }

                sqlFile.setParameter("_CategoryID", category.CategoryID.ToString());
                sqlFile.setParameter("_CategoryDescription", category.CategoryDescription);

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
            catch(Exception ex)
            {
                return false;
            }
        }

        public Boolean InsertSupplier(SupplierClass supplier)
        {
            try
            {
                if (supplier.SupplierID > 0)
                {
                    this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\UpdateSupplier.sql";
                }
                else
                {
                    this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\InsertSupplier.sql";
                }

                sqlFile.setParameter("_SupplierID", supplier.SupplierID.ToString());
                sqlFile.setParameter("_SupplierDescription", supplier.SupplierDescription);
                sqlFile.setParameter("_SupplierAddress", supplier.SupplierAddress);

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


        public Boolean InsertStoreShelve(ShelveClass storeshelve)
        {
            try
            {
                if (storeshelve.ShelfID > 0)
                {
                    this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\UpdateStoreShelve.sql";
                }
                else
                {
                    this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\InsertStoreShelve.sql";
                }

                sqlFile.setParameter("_ShelfID", storeshelve.ShelfID.ToString());
                sqlFile.setParameter("_ShelfDescription", storeshelve.ShelfDescription);

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



        public Boolean InsertName(GenericMedicineClass generic)
        {
            try
            {
                if (generic.GenericID > 0)
                {
                    this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\UpdateName.sql";
                }
                else
                {
                    this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\InsertName.sql";
                }

                sqlFile.setParameter("_GenericID", generic.GenericID.ToString());
                sqlFile.setParameter("_GenericName", generic.GenericName);

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
            catch(Exception ex)
            {
                return false;
            }
        }


        public List<ItemCategoryClass> GetCategoryList()
        {
            List<ItemCategoryClass> toReturn = new List<ItemCategoryClass>();
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\GetCategoryList.sql";
                return Connection.Query<ItemCategoryClass>(this.sqlFile.sqlQuery).ToList();
            }
            catch (Exception ex)
            {
                return toReturn;
            }

        }



        public List<ShelveClass> GetShelveList()
        {
            List<ShelveClass> toReturn = new List<ShelveClass>();
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "InventoryManagement\\GetShelveList.sql";
                return Connection.Query<ShelveClass>(this.sqlFile.sqlQuery).ToList();
            }
            catch (Exception ex)
            {
                return toReturn;
            }

        }



        public List<SupplierClass> GetSupplierList()
        {
            List<SupplierClass> toReturn = new List<SupplierClass>();
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "InventoryManagement\\GetSupplierList.sql";
                return Connection.Query<SupplierClass>(this.sqlFile.sqlQuery).ToList();
            }
            catch (Exception ex)
            {
                return toReturn;
            }

        }




        public Boolean InsertItem(ItemClass item)
        {
            try
            {
                //this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\InsertItem.sql";
                //sqlFile.setParameter("_ItemName", item.ItemName);
                //sqlFile.setParameter("_ItemDescription", item.ItemDescription);
                //sqlFile.setParameter("_GenericID", item.GenericID.ToString());
                //sqlFile.setParameter("_CategoryID", item.CategoryID.ToString());
                //sqlFile.setParameter("_ShelfID", item.ShelfID.ToString());
                //sqlFile.setParameter("_SupplierID", item.SupplierID.ToString());
                //sqlFile.setParameter("_UnitID", item.UnitID.ToString());
                //sqlFile.setParameter("_Barcode", item.Barcode);
                //sqlFile.setParameter("_PurchasePrice", item.PurchasePrice.ToString());
                //sqlFile.setParameter("_MarkupValue", item.MarkupValue.ToString());
                //sqlFile.setParameter("_Price", item.Price.ToString());
                //sqlFile.setParameter("_ExpiryDate", item.ExpiryDate);
                //sqlFile.setParameter("_Stock", item.Stock.ToString());
                //sqlFile.setParameter("_SideEffect", item.SideEffect);
                //sqlFile.setParameter("_UserID", 1.ToString());
                //sqlFile.setParameter("_BranchCode", 1.ToString());
                //sqlFile.setParameter("_TransactionCode", 1.ToString());
                //sqlFile.setParameter("_TransYear", 2021.ToString());
                //sqlFile.setParameter("_TransactionDate", DateTime.Today.ToString("yyyy-MM-dd"));
                //sqlFile.setParameter("_Value", item.Value.ToString());


                //var affectedRow = Connection.Execute(sqlFile.sqlQuery);


                //if (affectedRow > 0)
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}


                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var url = "https://inventory-api-railway-production.up.railway.app/api/item/insert_item";

                        client.DefaultRequestHeaders.Add("KEY", api.key);
                        client.DefaultRequestHeaders.Add("accept", api.accept);
                        client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);

                        var load = new
                        {
                            item_name = item.ItemName,
                            item_description = item.ItemDescription,
                            generic_id = item.GenericID.ToString(),
                            category_id = item.CategoryID.ToString(),
                            unit_id = item.UnitID.ToString(),
                            purchase_price = item.PurchasePrice.ToString(),
                            price = item.Price.ToString(),
                            markup_value = item.MarkupValue.ToString(),
                            shelf_id = item.ShelfID.ToString(),
                            supplier_id = item.SupplierID.ToString(),
                            side_effect = item.SideEffect,
                            barcode = item.Barcode,
                            stock = item.Stock.ToString(),
                            expiry_date = Convert.ToDateTime(item.ExpiryDate).ToString("yyyy/MM/dd"),
                            user_id = 1.ToString(),
                            value = item.Value.ToString(),
                        };

                        var json = JsonConvert.SerializeObject(load);

                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = client.PostAsync(url, content).Result;

                        var responseBody = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine(responseBody);

                        return response.IsSuccessStatusCode;


                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }



        public Boolean GetDuplicateBarCode(String Barcode)
        {
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\ListDuplicate.sql";
                sqlFile.setParameter("Barcode", Barcode);

                var affectedRow = Connection.Query<String>(this.sqlFile.sqlQuery).FirstOrDefault();


                if (!String.IsNullOrEmpty(affectedRow) || !String.IsNullOrWhiteSpace(affectedRow))
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception)
            {
                return false;
            }


        }


        public Boolean UpdateItem(ItemClass item)
        {
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\UpdateItem.sql";
                sqlFile.setParameter("_ItemCode", item.ItemCode.ToString());
                sqlFile.setParameter("_ItemName", item.ItemName);
                sqlFile.setParameter("_ItemDescription", item.ItemDescription);
                sqlFile.setParameter("_GenericID", item.GenericID.ToString());
                sqlFile.setParameter("_CategoryID", item.CategoryID.ToString());
                sqlFile.setParameter("_ShelfID", item.ShelfID.ToString());
                sqlFile.setParameter("_SupplierID", item.SupplierID.ToString());
                sqlFile.setParameter("_Barcode", item.Barcode);
                sqlFile.setParameter("_PurchasePrice", item.PurchasePrice.ToString());
                sqlFile.setParameter("_MarkupValue", item.MarkupValue.ToString());
                sqlFile.setParameter("_Price", item.Price.ToString());
                sqlFile.setParameter("_Stock", item.Stock.ToString());
                sqlFile.setParameter("_SideEffect", item.SideEffect);
                sqlFile.setParameter("_UnitID", item.UnitID.ToString());
                sqlFile.setParameter("_Value", item.Value.ToString());

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





        public List<ItemClass> GetProductList()
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
            var productlist = new List<ItemClass>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = "https://inventory-api-railway-production.up.railway.app/api/inventory/get_product_list";

                    client.DefaultRequestHeaders.Add("KEY", api.key);
                    client.DefaultRequestHeaders.Add("Accept", api.accept);
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);

                    HttpResponseMessage response = client.GetAsync(url).Result;
                    string json = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                        return productlist;

                    var result = JsonConvert.DeserializeObject<ApiResponse<List<ItemClass>>>(json);

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







    }
}
