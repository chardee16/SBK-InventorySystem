using Dapper;
using InventoryProject.Models;
using InventoryProject.Models.InventoryManagementModule;
using InventoryProject.Models.SalesReportModule;
using InventoryProject.Services;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Repository
{
    public class SaleReportRepository : BaseRepository
    {
        SQLFile sqlFile = new SQLFile();
        Config _config = new Config();
        APIKey api = new APIKey();


        public List<SalePerItemClass> GetItemList(String DateStart,String DateEnd)
        {
            List<SalePerItemClass> toReturn = new List<SalePerItemClass>();
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "SaleReport\\GetSalePerItem.sql";
                sqlFile.setParameter("_DateStart", DateStart);
                sqlFile.setParameter("_DateEnd", DateEnd);
                return Connection.Query<SalePerItemClass>(this.sqlFile.sqlQuery).ToList();
            }
            catch (Exception ex)
            {
                return toReturn;
            }

        }


        public List<SalesReport> GetSalesReport(String DateStart, String DateEnd, List<GenericMedicineClass> _NameSelectedReport, List<ItemCategoryClass> _itemCategoriesSelectedReport)
        {
            List<SalesReport> toReturn = new List<SalesReport>();
            ArrayList myList = new ArrayList();
            ArrayList myList2 = new ArrayList();
            string addqueryname = "";
            string addquerycategory = "";

            foreach (var item in _NameSelectedReport)
            {
                myList.Add(item.GenericID);

                addqueryname = "and items.ItemGenericID IN (" + string.Join(",", myList.ToArray()) + ")";
            }


            foreach (var item in _itemCategoriesSelectedReport)
            {
                myList2.Add(item.CategoryID);

                addquerycategory = "and items.CategoryID IN (" + string.Join(",", myList2.ToArray()) + ")";
            }

            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "SaleReport\\ViewReportSales.sql";
                sqlFile.setParameter("_DateStart", DateStart);
                sqlFile.setParameter("_DateEnd", DateEnd);
                sqlFile.setParameter("_Name", addqueryname);
                sqlFile.setParameter("_Category", addquerycategory);
                return Connection.Query<SalesReport>(this.sqlFile.sqlQuery).ToList();
            }
            catch (Exception ex)
            {
                return toReturn;
            }

        }

        public List<SalesReport> GetAllItemStock()
        {
            List<SalesReport> toReturn = new List<SalesReport>();
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "SaleReport\\GetItemStocks.sql";
                return Connection.Query<SalesReport>(this.sqlFile.sqlQuery).ToList();
            }
            catch (Exception ex)
            {
                return toReturn;
            }

        }


        public List<SalesReport> GetAllStock()
        {
            List<SalesReport> toReturn = new List<SalesReport>();
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "SaleReport\\GetAllStocks.sql";
                return Connection.Query<SalesReport>(this.sqlFile.sqlQuery).ToList();
            }
            catch (Exception ex)
            {
                return toReturn;
            }

        }


        public List<TopSales> TopSalesList()
        {
            //List<TopSales> toReturn = new List<TopSales>();
            //try
            //{
            //    this.sqlFile.sqlQuery = _config.SQLDirectory + "SaleReport\\GetTopSalesItem.sql";
            //    return Connection.Query<TopSales>(this.sqlFile.sqlQuery).ToList();
            //}
            //catch (Exception ex)
            //{
            //    return toReturn;
            //}

            var toReturn = new List<TopSales>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = $"{api.http}/api/report/get_top_sales_item";

                    client.DefaultRequestHeaders.Add("KEY", api.key);
                    client.DefaultRequestHeaders.Add("Accept", api.accept);
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);

                    HttpResponseMessage response = client.GetAsync(url).Result;
                    string json = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                        return toReturn;

                    var result = JsonConvert.DeserializeObject<ApiResponse2<TopSales>>(json);

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

        public List<TopIncome> TopIncomeList()
        {
            //List<TopIncome> toReturn = new List<TopIncome>();
            //try
            //{
            //    this.sqlFile.sqlQuery = _config.SQLDirectory + "SaleReport\\GetOverviewIncome.sql";
            //    return Connection.Query<TopIncome>(this.sqlFile.sqlQuery).ToList();
            //}
            //catch (Exception ex)
            //{
            //    return toReturn;
            //}


            var toReturn = new List<TopIncome>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = $"{api.http}/api/report/get_overview_income";

                    client.DefaultRequestHeaders.Add("KEY", api.key);
                    client.DefaultRequestHeaders.Add("Accept", api.accept);
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);

                    HttpResponseMessage response = client.GetAsync(url).Result;
                    string json = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                        return toReturn;

                    var result = JsonConvert.DeserializeObject<ApiResponse<TopIncome>>(json);

                    if (result?.status == "SUCCESS" && result.data != null)
                    {
                        toReturn.Add(result.data);
                    }
                }
            }
            catch
            {
            }

            return toReturn;

        }
    }
}
