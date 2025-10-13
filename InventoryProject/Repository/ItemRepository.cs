using Dapper;
using InventoryProject.Models;
using InventoryProject.Models.ItemModule;
using InventoryProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Repository
{
    public class ItemRepository : BaseRepository
    {
        SQLFile sqlFile = new SQLFile();
        Config _config = new Config();

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


        public List<ItemLogs> GetItemLogs(Int32 ItemCode)
        {
            List<ItemLogs> toReturn = new List<ItemLogs>();
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\ItemLogs.sql";
                sqlFile.setParameter("_ItemCode", ItemCode.ToString());
                return Connection.Query<ItemLogs>(this.sqlFile.sqlQuery).ToList();
            }
            catch (Exception ex)
            {
                return toReturn;
            }

        }


        public List<UnitClass> GetUnitList()
        {
            List<UnitClass> toReturn = new List<UnitClass>();
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\GetUnitList.sql";
                return Connection.Query<UnitClass>(this.sqlFile.sqlQuery).ToList();
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
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\AddStock.sql";
                sqlFile.setParameter("_ItemCode", item.ItemCode.ToString());
                sqlFile.setParameter("_Stock", item.Stock.ToString());
                sqlFile.setParameter("_Price", item.Price.ToString());
                sqlFile.setParameter("_ExpiryDate", item.ExpiryDate);
                sqlFile.setParameter("_UserID", 1.ToString());
                sqlFile.setParameter("_BranchCode", 1.ToString());
                sqlFile.setParameter("_TransactionCode", 2.ToString());
                sqlFile.setParameter("_TransYear", DateTime.Today.Year.ToString());
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




        public List<ItemClass> GetItemList()
        {
            List<ItemClass> toReturn = new List<ItemClass>();
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "InventoryManagement\\GetProductList.sql";
                return Connection.Query<ItemClass>(this.sqlFile.sqlQuery).ToList();
            }
            catch (Exception ex)
            {
                return toReturn;
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
