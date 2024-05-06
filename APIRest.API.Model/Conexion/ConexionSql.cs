using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIRest.API.Model.Conexion
{
    public class ConexionSql
    {
        private ConnectionString connectionString;

        public ConexionSql(IOptionsMonitor<ConnectionString> optionsMonitor)
        {
            connectionString = optionsMonitor.CurrentValue;
        }

        public IDbConnection CreateConnection() => new SqlConnection(connectionString.SqlConnection);
    }
}
