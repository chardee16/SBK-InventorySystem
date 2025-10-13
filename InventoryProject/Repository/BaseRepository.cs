using InventoryProject.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Repository
{
    public abstract class BaseRepository
    {
        public SQLFile queryFile = new SQLFile();

        IDbConnection _connection;


        internal IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    var source = new Config();
                    var connectionString = source.SQLServerConnectionString;
                    _connection = new SqlConnection(connectionString);
                }

                return _connection;
            }
        }
    }
}
