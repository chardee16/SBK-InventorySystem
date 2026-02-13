using Dapper;
using InventoryProject.Models.ClientModule;
using InventoryProject.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Repository
{
    public class ClientRepository : BaseRepository
    {
        SQLFile sqlFile = new SQLFile();
        Config _config = new Config();
        APIKey api = new APIKey();

        public List<InsertClient> GetAllClients()
        {
            //List<InsertClient> toReturn = new List<InsertClient>();
            //try
            //{
            //    this.sqlFile.sqlQuery = _config.SQLDirectory + "Client\\SelectClient.sql";
            //    return Connection.Query<InsertClient>(this.sqlFile.sqlQuery).ToList();
            //}
            //catch (Exception ex)
            //{
            //    return toReturn;
            //}

            var clientList = new List<InsertClient>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = "https://inventory-api-railway-production.up.railway.app/api/client/get_all_client";

                    client.DefaultRequestHeaders.Add("KEY", api.key);
                    client.DefaultRequestHeaders.Add("Accept", api.accept);
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);

                    HttpResponseMessage response = client.GetAsync(url).Result;
                    string json = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                        return clientList;

                    var result = JsonConvert.DeserializeObject<ApiResponse<List<InsertClient>>>(json);

                    if (result != null && result.status == "SUCCESS" && result.data != null)
                    {
                        clientList = result.data;
                    }
                }
            }
            catch (Exception ex)
            {
                // log error if needed
            }

            return clientList;
        }

        public List<InsertClient> GetListClients(String id, String name)
        {
            //List<InsertClient> toReturn = new List<InsertClient>();
            //try
            //{
            //    this.sqlFile.sqlQuery = _config.SQLDirectory + "Client\\SelectListClients.sql";
            //    sqlFile.setParameter("_clientid", id);
            //    sqlFile.setParameter("_fullname", name);
            //    return Connection.Query<InsertClient>(this.sqlFile.sqlQuery).ToList();
            //}
            //catch (Exception ex)
            //{
            //    return toReturn;
            //}

            var clientList = new List<InsertClient>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = $"https://inventory-api-railway-production.up.railway.app/api/client/get_client_details/{id}";

                    client.DefaultRequestHeaders.Add("KEY", api.key);
                    client.DefaultRequestHeaders.Add("accept", api.accept);
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);


                    HttpResponseMessage response = client.GetAsync(url).Result;

                    if (!response.IsSuccessStatusCode)
                        return clientList;

                    var json = response.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<ApiResponse<List<InsertClient>>>(json);

                    if (result != null && result.status == "SUCCESS" && result.data != null)
                    {
                        clientList.AddRange(result.data);
                    }
                }
            }
            catch (Exception ex)
            {
                // log error if needed
            }

            return clientList;
        }

        public Boolean InsertInventoryClient(InsertClient insertclientlist)
        {
            //try
            //{
             
            //    this.sqlFile.sqlQuery = _config.SQLDirectory + "Client\\InsertClient.sql";
            //    sqlFile.setParameter("_TitleID", insertclientlist.TitleID.ToString());
            //    sqlFile.setParameter("_LastName", insertclientlist.LastName);
            //    sqlFile.setParameter("_MiddleName", insertclientlist.MiddleName);
            //    sqlFile.setParameter("_FirstName", insertclientlist.FirstName);
            //    sqlFile.setParameter("_SuffixID", insertclientlist.SuffixID.ToString());
            //    sqlFile.setParameter("_GenderID", insertclientlist.GenderID.ToString());
            //    sqlFile.setParameter("_CivilStatusID", insertclientlist.CivilStatusID.ToString());
            //    sqlFile.setParameter("_Company", insertclientlist.Company.ToString());
            //    sqlFile.setParameter("_ProvinceID", insertclientlist.ProvinceID.ToString());
            //    sqlFile.setParameter("_CityID", insertclientlist.CityID.ToString());
            //    sqlFile.setParameter("_BarangayID", insertclientlist.BrgyID.ToString());
            //    sqlFile.setParameter("_DateAdded", insertclientlist.DateAdded);
            //    sqlFile.setParameter("_DateTimeAdded", insertclientlist.DateTimeAdded);
            //    sqlFile.setParameter("_Age", insertclientlist.Age.ToString());
            //    sqlFile.setParameter("_DateOfBirth", insertclientlist.BirthDate);

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
                    var url = "https://inventory-api-railway-production.up.railway.app/api/client/insert_client";

                    client.DefaultRequestHeaders.Add("KEY", api.key);
                    client.DefaultRequestHeaders.Add("accept", api.accept);
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);

                    var load = new
                    {
                        title_id = insertclientlist.TitleID,
                        firstname = insertclientlist.FirstName ?? "",
                        middlename = insertclientlist.MiddleName ?? "",
                        lastname = insertclientlist.LastName ?? "",
                        date_of_birth = Convert.ToDateTime(insertclientlist.BirthDate).ToString("yyyy-MM-dd"),
                        age = insertclientlist.Age,
                        suffix_id = insertclientlist.SuffixID,
                        gender_id = insertclientlist.GenderID,
                        civil_status_id = insertclientlist.CivilStatusID,
                        company = insertclientlist.Company ?? "",
                        province_id = insertclientlist.ProvinceID,
                        city_id = insertclientlist.CityID,
                        brgy_id = insertclientlist.BrgyID
                    };

                    var json = JsonConvert.SerializeObject(load);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(url, content).Result;
                  
                    //var responseBody = response.Content.ReadAsStringAsync().Result;
                    //Console.WriteLine(responseBody);

                    return response.IsSuccessStatusCode;


                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public Boolean UpdateInventoryClient(InsertClient updateclientlist)
        {
            //try
            //{

            //    this.sqlFile.sqlQuery = _config.SQLDirectory + "Client\\UpdateClient.sql";
            //    sqlFile.setParameter("_TitleID", updateclientlist.TitleID.ToString());
            //    sqlFile.setParameter("_LastName", updateclientlist.LastName);
            //    sqlFile.setParameter("_MiddleName", updateclientlist.MiddleName);
            //    sqlFile.setParameter("_FirstName", updateclientlist.FirstName);
            //    sqlFile.setParameter("_SuffixID", updateclientlist.SuffixID.ToString());
            //    sqlFile.setParameter("_GenderID", updateclientlist.GenderID.ToString());
            //    sqlFile.setParameter("_CivilStatusID", updateclientlist.CivilStatusID.ToString());
            //    sqlFile.setParameter("_Company", updateclientlist.Company.ToString());
            //    sqlFile.setParameter("_ProvinceID", updateclientlist.ProvinceID.ToString());
            //    sqlFile.setParameter("_CityID", updateclientlist.CityID.ToString());
            //    sqlFile.setParameter("_BrgyID", updateclientlist.BrgyID.ToString());
            //    sqlFile.setParameter("_DateAdded", updateclientlist.DateAdded);
            //    sqlFile.setParameter("_DateTimeAdded", updateclientlist.DateTimeAdded);
            //    sqlFile.setParameter("_Age", updateclientlist.Age.ToString());
            //    sqlFile.setParameter("_BirthDate", updateclientlist.BirthDate);
            //    sqlFile.setParameter("_DateTimeModified", updateclientlist.BirthDate);
            //    sqlFile.setParameter("_ClientID", updateclientlist.ClientID.ToString());

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
                    var url = "https://inventory-api-railway-production.up.railway.app/api/client/update_client";

                    client.DefaultRequestHeaders.Add("KEY", api.key);
                    client.DefaultRequestHeaders.Add("accept", api.accept);
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token);

                    var load = new
                    {
                        client_id = updateclientlist.ClientID,
                        title_id = updateclientlist.TitleID,
                        firstname = updateclientlist.FirstName ?? "",
                        middlename = updateclientlist.MiddleName ?? "",
                        lastname = updateclientlist.LastName ?? "",
                        date_of_birth = Convert.ToDateTime(updateclientlist.BirthDate).ToString("yyyy-MM-dd"),
                        age = updateclientlist.Age,
                        suffix_id = updateclientlist.SuffixID,
                        gender_id = updateclientlist.GenderID,
                        civil_status_id = updateclientlist.CivilStatusID,
                        company = updateclientlist.Company ?? "",
                        province_id = updateclientlist.ProvinceID,
                        city_id = updateclientlist.CityID,
                        brgy_id = updateclientlist.BrgyID
                    };

                    var json = JsonConvert.SerializeObject(load);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(url, content).Result;

                    //var responseBody = response.Content.ReadAsStringAsync().Result;
                    //Console.WriteLine(responseBody);

                    return response.IsSuccessStatusCode;


                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
