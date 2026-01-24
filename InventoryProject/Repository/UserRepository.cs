using Dapper;
using InventoryProject.Models.Users;
using InventoryProject.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Repository
{
    public class UserRepository : BaseRepository
    {
        SQLFile sqlFile = new SQLFile();
        Config _config = new Config();
        APIKey api = new APIKey();

        public List<UserParam> GetUserList(String username, String password)
        {
            List<UserParam> toReturn = new List<UserParam>();
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "User\\GetUserParam.sql";
                sqlFile.setParameter("_username", username);
                sqlFile.setParameter("_password", password);
                return Connection.Query<UserParam>(this.sqlFile.sqlQuery).ToList();
            }
            catch (Exception ex)
            {
                return toReturn;
            }

            //var users = new List<UserParam>();

            //try
            //{
            //    using (HttpClient client = new HttpClient())
            //    {
            //        var url = "https://inventory-api-railway-production.up.railway.app/api/auth/login";

            //        client.DefaultRequestHeaders.Add("KEY", api.key);
            //        client.DefaultRequestHeaders.Add("accept", api.accept);
            //        client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", api.token); 

            //        var load = new
            //        {
            //            username = username,
            //            password = "123",
            //        };

            //        var content = new StringContent(
            //            JsonConvert.SerializeObject(load),
            //            Encoding.UTF8,
            //            "application/json"
            //        );

            //        HttpResponseMessage response = client.PostAsync(url, content).Result;

            //        if (!response.IsSuccessStatusCode)
            //            return users;

            //        var json = response.Content.ReadAsStringAsync().Result;

            //        var result = JsonConvert.DeserializeObject<ApiResponse<UserParam>>(json);

            //        if (result != null && result.status == "SUCCESS" && result.data != null)
            //        {
            //            users.Add(result.data);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // log error if needed
            //}

            //return users;

        }



        public List<UserParam> GetAllUsers()
        {
            List<UserParam> toReturn = new List<UserParam>();
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "User\\GetAllUsers.sql";
                return Connection.Query<UserParam>(this.sqlFile.sqlQuery).ToList();
            }
            catch (Exception ex)
            {
                return toReturn;
            }

        }

        public List<UserPrivileges> GetUserLogin(int UserID)
        {
            List<UserPrivileges> toReturn = new List<UserPrivileges>();
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "User\\GetUserLogin.sql";
                sqlFile.setParameter("_UserID", UserID.ToString());
                return Connection.Query<UserPrivileges>(this.sqlFile.sqlQuery).ToList();
            }
            catch (Exception ex)
            {
                return toReturn;
            }
        
        }

        public List<Privilege> GetAllPrivilege()
        {
            List<Privilege> toReturn = new List<Privilege>();
            try
            {
                this.sqlFile.sqlQuery = _config.SQLDirectory + "User\\GetAllPrivilege.sql";
                return Connection.Query<Privilege>(this.sqlFile.sqlQuery).ToList();
            }
            catch (Exception ex)
            {
                return toReturn;
            }

        }

        public Boolean UpdateUserInfo(UserParam updateuser)
        {
            try
            {

                this.sqlFile.sqlQuery = _config.SQLDirectory + "User\\UpdateUser.sql";
                sqlFile.setParameter("_Password", updateuser.Password);
                sqlFile.setParameter("_UserID", updateuser.UserID.ToString());
                             
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


        public Boolean ResetUserInfo(UserParam updateuser)
        {
            try
            {

                this.sqlFile.sqlQuery = _config.SQLDirectory + "User\\ResetPass.sql";
                sqlFile.setParameter("_Password", updateuser.Password);
                sqlFile.setParameter("_UserID", updateuser.UserID.ToString());

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


        public Boolean ActivateUserInfo(UserParam updateuser)
        {
            try
            {

                this.sqlFile.sqlQuery = _config.SQLDirectory + "User\\ActivateUser.sql";
                sqlFile.setParameter("_UserID", updateuser.UserID.ToString());
                sqlFile.setParameter("_IsActive", updateuser.IsActive.ToString());

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

        public Boolean InsertUserInfo(UserParam updateuser)
        {
            try
            {

                this.sqlFile.sqlQuery = _config.SQLDirectory + "User\\InsertUser.sql";
                sqlFile.setParameter("_Username", updateuser.Username);
                sqlFile.setParameter("_Password", updateuser.Password);
                sqlFile.setParameter("_Firstname", updateuser.Firstname);
                sqlFile.setParameter("_Middlename", updateuser.Middlename);
                sqlFile.setParameter("_Lastname", updateuser.Lastname);
           
                var affectedRow = Connection.Execute(sqlFile.sqlQuery);

                if (affectedRow > 0)
                {
                    if (updateuser.privil.Count > 0)
                    {
                        InsertUpdatePrivilege(updateuser.privil);
                    }

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

        public Boolean InsertUpdatePrivilege(List<UserPrivileges> userprivil)
        {
            try
            {
                SQLFile sqlFilePrivilege = new SQLFile();

                foreach (var item in userprivil)
                {
                    sqlFilePrivilege.sqlQuery = _config.SQLDirectory + "User\\InsertPrivileges.sql";
                    sqlFilePrivilege.setParameter("_UserID", item.UserID.ToString());
                    sqlFilePrivilege.setParameter("_PrivilegeID", item.PrivilegeID.ToString());
                    sqlFilePrivilege.setParameter("_IsAllowed", item.IsAllowed.ToString());

                    Connection.Execute(sqlFilePrivilege.sqlQuery);
                }

            }
            catch
            {
                return false;
            }

            return true;

        }


        public Boolean UpdateUserInformation(UserParam updateuser)
        {
            try
            {

                this.sqlFile.sqlQuery = _config.SQLDirectory + "User\\UpdateUserInformation.sql";
                sqlFile.setParameter("_Username", updateuser.Username);
                sqlFile.setParameter("_Firstname", updateuser.Firstname);
                sqlFile.setParameter("_Middlename", updateuser.Middlename);
                sqlFile.setParameter("_Lastname", updateuser.Lastname);
                sqlFile.setParameter("_UserID", updateuser.UserID.ToString());

                var affectedRow = Connection.Execute(sqlFile.sqlQuery);

                if (affectedRow > 0)
                {
                    if (updateuser.privil.Count > 0)
                    {
                        UpdatePrivilege(updateuser.privil);
                    }

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

        public List<UserPrivileges> UpdatePrivilege(List<UserPrivileges> userprivil)
        {
            List<UserPrivileges> toreturn = new List<UserPrivileges>();
            try
            {
                
                SQLFile sqlFilePrivilege = new SQLFile();
                SQLFile sqlFilePrivilegeselect = new SQLFile();
                SQLFile sqlFilePrivilegeInsert = new SQLFile();

                foreach (var item in userprivil)
                {

                    sqlFilePrivilegeselect.sqlQuery = _config.SQLDirectory + "User\\GetUser.sql";
                    sqlFilePrivilegeselect.setParameter("_UserID", item.UserID.ToString());
                    sqlFilePrivilegeselect.setParameter("_PrivilegeID", item.PrivilegeID.ToString());

                    toreturn = Connection.Query<UserPrivileges>(sqlFilePrivilegeselect.sqlQuery).ToList();

                    if (toreturn.Count > 0)
                    {
                        sqlFilePrivilege.sqlQuery = _config.SQLDirectory + "User\\UpdatePrivileges.sql";
                        sqlFilePrivilege.setParameter("_UserID", item.UserID.ToString());
                        sqlFilePrivilege.setParameter("_PrivilegeID", item.PrivilegeID.ToString());
                        sqlFilePrivilege.setParameter("_IsAllowed", item.IsAllowed.ToString());

                        Connection.Execute(sqlFilePrivilege.sqlQuery);

                    }
                    else
                    {
                        sqlFilePrivilegeInsert.sqlQuery = _config.SQLDirectory + "User\\InsertPrivileges.sql";
                        sqlFilePrivilegeInsert.setParameter("_UserID", item.UserID.ToString());
                        sqlFilePrivilegeInsert.setParameter("_PrivilegeID", item.PrivilegeID.ToString());
                        sqlFilePrivilegeInsert.setParameter("_IsAllowed", item.IsAllowed.ToString());

                        Connection.Execute(sqlFilePrivilegeInsert.sqlQuery);
                    }
                    
                     
                }

            }
            catch
            {
                return toreturn;
            }

            return toreturn;

        }
    }
}