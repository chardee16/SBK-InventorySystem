using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models.Users
{
    public class UserParam
    {
        public Int32 Id { get; set; }
        public Int32 UserID { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Firstname { get; set; }
        public String Middlename { get; set; }
        public String Lastname { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsAdmin { get; set; }
        public Int32? JobPositionID { get; set; }
        public Boolean? IsReset { get; set; }
        public Boolean? IsDelivery { get; set; }
        
        public List<UserPrivileges> privil { get; set; }
    }
}
