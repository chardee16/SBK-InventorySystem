using Dapper;
using InventoryProject.Models.ClientModule;
using InventoryProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Repository
{
    public class ClientRepository : BaseRepository
    {
        SQLFile sqlFile = new SQLFile();
        Config _config = new Config();


        public List<InsertClient> GetAllClients()
        {
            List<InsertClient> toReturn = new List<InsertClient>();
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "Client\\SelectClient.sql";
                return Connection.Query<InsertClient>(this.sqlFile.sqlQuery).ToList();
            }
            catch (Exception ex)
            {
                return toReturn;
            }
        }

        public List<InsertClient> GetListClients(String id, String name)
        {
            List<InsertClient> toReturn = new List<InsertClient>();
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "Client\\SelectListClients.sql";
                sqlFile.setParameter("_clientid", id);
                sqlFile.setParameter("_fullname", name);
                return Connection.Query<InsertClient>(this.sqlFile.sqlQuery).ToList();
            }
            catch (Exception ex)
            {
                return toReturn;
            }
        }

        public Boolean InsertInventoryClient(InsertClient insertclientlist)
        {
            try
            {
             
                this.sqlFile.sqlQuery = _config.SQLDirectory + "Client\\InsertClient.sql";
                sqlFile.setParameter("_TitleID", insertclientlist.TitleID.ToString());
                sqlFile.setParameter("_LastName", insertclientlist.LastName);
                sqlFile.setParameter("_MiddleName", insertclientlist.MiddleName);
                sqlFile.setParameter("_FirstName", insertclientlist.FirstName);
                sqlFile.setParameter("_SuffixID", insertclientlist.SuffixID.ToString());
                sqlFile.setParameter("_GenderID", insertclientlist.GenderID.ToString());
                sqlFile.setParameter("_CivilStatusID", insertclientlist.CivilStatusID.ToString());
                sqlFile.setParameter("_Company", insertclientlist.Company.ToString());
                sqlFile.setParameter("_ProvinceID", insertclientlist.ProvinceID.ToString());
                sqlFile.setParameter("_CityID", insertclientlist.CityID.ToString());
                sqlFile.setParameter("_BarangayID", insertclientlist.BrgyID.ToString());
                sqlFile.setParameter("_DateAdded", insertclientlist.DateAdded);
                sqlFile.setParameter("_DateTimeAdded", insertclientlist.DateTimeAdded);
                sqlFile.setParameter("_Age", insertclientlist.Age.ToString());
                sqlFile.setParameter("_DateOfBirth", insertclientlist.BirthDate);

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

        public Boolean UpdateInventoryClient(InsertClient updateclientlist)
        {
            try
            {

                this.sqlFile.sqlQuery = _config.SQLDirectory + "Client\\UpdateClient.sql";
                sqlFile.setParameter("_TitleID", updateclientlist.TitleID.ToString());
                sqlFile.setParameter("_LastName", updateclientlist.LastName);
                sqlFile.setParameter("_MiddleName", updateclientlist.MiddleName);
                sqlFile.setParameter("_FirstName", updateclientlist.FirstName);
                sqlFile.setParameter("_SuffixID", updateclientlist.SuffixID.ToString());
                sqlFile.setParameter("_GenderID", updateclientlist.GenderID.ToString());
                sqlFile.setParameter("_CivilStatusID", updateclientlist.CivilStatusID.ToString());
                sqlFile.setParameter("_Company", updateclientlist.Company.ToString());
                sqlFile.setParameter("_ProvinceID", updateclientlist.ProvinceID.ToString());
                sqlFile.setParameter("_CityID", updateclientlist.CityID.ToString());
                sqlFile.setParameter("_BrgyID", updateclientlist.BrgyID.ToString());
                sqlFile.setParameter("_DateAdded", updateclientlist.DateAdded);
                sqlFile.setParameter("_DateTimeAdded", updateclientlist.DateTimeAdded);
                sqlFile.setParameter("_Age", updateclientlist.Age.ToString());
                sqlFile.setParameter("_BirthDate", updateclientlist.BirthDate);
                sqlFile.setParameter("_DateTimeModified", updateclientlist.BirthDate);
                sqlFile.setParameter("_ClientID", updateclientlist.ClientID.ToString());

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
