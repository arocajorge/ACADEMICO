using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public class CadenaDeConexion
    {
        public static string GetConnectionString()
        {
            string ip = @"localhost";
            string password = "admin*2016";
            string user = "sa";
            string InitialCatalog = "DBERP";

            return "data source=" + ip + ";initial catalog=" + InitialCatalog + ";user id=" + user + ";password=" + password + ";MultipleActiveResultSets=True;";
        }
    }
}
