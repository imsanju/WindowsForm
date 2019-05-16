using System.Configuration;
using System.Data.SqlClient;

namespace CRM_Final.Business.Data
{
    public class DbManager
    {
        public static SqlCommand GetDbCommandObject()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            SqlCommand cmd = conn.CreateCommand();
            return cmd;
        }
    }
}
