using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaoDinhVu.DAL.DBContext
{
    public class DBConnection
    {
        public SqlConnection CreateConnection()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source = VU-CAO\SQLEXPRESS; Initial Catalog = CaoDinhVu_TTTN; Integrated Security = True; Persist Security Info = False; Pooling = False; MultipleActiveResultSets = False; Encrypt = False; TrustServerCertificate = False";
            //conn.ConnectionString = @"Data Source=VU-CAO\SQLEXPRESS; Initial Catalog=QLBH; User Id=sa; Password=vu";
            return conn;
        }
    }
}
