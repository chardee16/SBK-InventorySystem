using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Models.Users
{
    public class UserPrivileges
    {
        public Int64 ID { get; set; }
        public Int32 UserID { get; set; }
        public Byte PrivilegeID { get; set; }
        public bool IsAllowed { get; set; }
    }
}
