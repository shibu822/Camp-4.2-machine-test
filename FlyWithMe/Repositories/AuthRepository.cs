using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;

namespace AirlineManagement.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly string _connStr =
            ConfigurationManager.ConnectionStrings["FlyWithMeDB"].ConnectionString;

        public int ValidateLogin(string username, string password)
        {
            using SqlConnection con = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(
                "SELECT AdminId FROM Admin WHERE Username=@u AND Password=@p", con);

            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@p", password);

            con.Open();
            object result = cmd.ExecuteScalar();

            return result != null ? (int)result : -1;
        }
    }
}
