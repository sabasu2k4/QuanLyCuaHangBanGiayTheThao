using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DTO;

namespace DAO
{
    public class ConnectDB
    {
        static String stringConnection = @"Data Source=.;Initial Catalog=GiayTheThao_DB;Integrated Security=True";
        protected SqlConnection _conn = new SqlConnection(stringConnection);
    }
}

