using Dapper;
using InventoryProject.Models;
using InventoryProject.Models.InventoryManagementModule;
using InventoryProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Repository
{
    public class InventoryManagementRepository : BaseRepository
    {
        SQLFile sqlFile = new SQLFile();
        Config _config = new Config();


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
                this.sqlFile.sqlQuery = _config.SQLDirectory + "Items\\InsertItem.sql";
                sqlFile.setParameter("_ItemName", item.ItemName);
                sqlFile.setParameter("_ItemDescription", item.ItemDescription);
                sqlFile.setParameter("_GenericID", item.GenericID.ToString());
                sqlFile.setParameter("_CategoryID", item.CategoryID.ToString());
                sqlFile.setParameter("_ShelfID", item.ShelfID.ToString());
                sqlFile.setParameter("_SupplierID", item.SupplierID.ToString());
                sqlFile.setParameter("_UnitID", item.UnitID.ToString());
                sqlFile.setParameter("_Barcode", item.Barcode);
                sqlFile.setParameter("_PurchasePrice", item.PurchasePrice.ToString());
                sqlFile.setParameter("_MarkupValue", item.MarkupValue.ToString());
                sqlFile.setParameter("_Price", item.Price.ToString());
                sqlFile.setParameter("_ExpiryDate", item.ExpiryDate);
                sqlFile.setParameter("_Stock", item.Stock.ToString());
                sqlFile.setParameter("_SideEffect", item.SideEffect);
                sqlFile.setParameter("_UserID", 1.ToString());
                sqlFile.setParameter("_BranchCode", 1.ToString());
                sqlFile.setParameter("_TransactionCode", 1.ToString());
                sqlFile.setParameter("_TransYear", 2021.ToString());
                sqlFile.setParameter("_TransactionDate", DateTime.Today.ToString("yyyy-MM-dd"));
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







    }
}
