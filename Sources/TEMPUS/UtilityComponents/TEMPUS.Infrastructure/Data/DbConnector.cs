using System.Data;
using System.Data.SqlClient;

namespace TEMPUS.Infrastructure.Data
{
    public class DbConnector
    {
        public static void Execute(SqlConnection connection, SqlCommand cmd)
        {
            using (connection)
            {
                cmd.Connection = connection;

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void Execute(SqlConnection connection, SqlCommand cmd, out DataSet data)
        {
            using (connection)
            {
                cmd.Connection = connection;
                connection.Open();

                data = new DataSet();
                var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);

                connection.Close();
            }
        }
    }
}