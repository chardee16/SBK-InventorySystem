using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Services
{
    public class Config
    {
        private static string CurrentPath = Environment.CurrentDirectory;

        //public static String IPAddress = "DESKTOP-QR1I6HF\\CHARDEE";
        //public string SQLServerConnectionString = "Data Source=" + IPAddress + ";Database=InventorySystemDB;User ID=sa;Password=~dimasalanG";

        //public static String IPAddress = "10.25.1.112";
        //public static String IPAddress = "DEVELOPER1\\DEVELOPER";
        //public string SQLServerConnectionString = "Data Source=" + IPAddress + ";Database=InventorySystemDB;User ID=sa;Password=developer1@)@)";

        //public static String IPAddress = "RODCIMWFG\\SQLEXPRESS";
        //public string SQLServerConnectionString = "Data Source=" + IPAddress + ";Database=InventorySystemDB;User ID=sa;Password=LegaC@)@!";

        //public static String IPAddress = "DESKTOP-EJGNBS6\\SQLEXPRESS";
        //public string SQLServerConnectionString = "Data Source=" + IPAddress + ";Database=InventorySystemDB;User ID=sa;Password=Edge@)@@";

        public static String IPAddress = "LAPTOP-4DVO2GA7\\SQLDEV";
        public string SQLServerConnectionString = "Data Source=" + IPAddress + ";Database=InventorySystemDB;User ID=sa;Password=12345678";

        //public static String IPAddress = "00ICT2021018";
        //public string SQLServerConnectionString = "Data Source=" + IPAddress + ";Database=InventorySystemDB;User ID=sa;Password=D3f@ult!";

        //public static String IPAddress = "DESKTOP-9JD2NFI\\SCHHAI";
        //public string SQLServerConnectionString = "Data Source=" + IPAddress + ";Database=WaterBillingDB;User ID=sa;Password=amac0386";

        public string SQLDirectory = CurrentPath.Replace("\\bin\\Debug", "\\SQLScripts\\");
        //public string SQLDirectory = CurrentPath + "\\SQLScripts\\";

    }
}
