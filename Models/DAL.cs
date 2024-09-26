using System.Data;
using System.Data.SqlClient;

namespace AyurYogi.Models
{
    public class DAL
    {
        public Responce Registration(Users users, SqlConnection connection)
        {
            Responce responce = new Responce();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_register", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Uname", users.Uname);
                    cmd.Parameters.AddWithValue("@Email", users.Email);
                    cmd.Parameters.AddWithValue("@Phone", users.Phone);
                    cmd.Parameters.AddWithValue("@Password", users.Password);
                    cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200).Direction = ParameterDirection.Output;

                    connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    string message = (string)cmd.Parameters["@ErrorMessage"].Value;

                    responce.StatusCode = i > 0 ? 200 : 100;
                    responce.StatusMessage = message;
                }
            }
            catch (Exception ex)
            {
                responce.StatusCode = 100;
                responce.StatusMessage = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return responce;
        }

        public Responce Login(Users users, SqlConnection connection)
        {
            Responce responce = new Responce();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_login", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", users.Email);
                cmd.Parameters.AddWithValue("@Password", users.Password);

                // Output parameter for error message
                SqlParameter errorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 200);
                errorMessage.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(errorMessage);

                connection.Open();
                cmd.ExecuteNonQuery();

                // Get the message from the output parameter
                string message = (string)cmd.Parameters["@ErrorMessage"].Value;

                // Set the response message
                if (message == "Login Successful!")
                {
                    responce.StatusCode = 200;
                    responce.StatusMessage = message;
                }
                else
                {
                    responce.StatusCode = 100;
                    responce.StatusMessage = message;
                }
            }
            catch (Exception ex)
            {
                responce.StatusCode = 100;
                responce.StatusMessage = "Error: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return responce;
        }

    }
}
