using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace LoginRegister
{
    public class AdminControl
    {
        string connection;
        string query;
        public AdminControl()
        {
            connection = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }
        public void Ban(string id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                query = "UPDATE accounts SET banned='True' WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteScalar();
            }
        }
        public void UnBan(string id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                query = "UPDATE accounts SET banned='False' WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteScalar();
            }
        }
        public bool GetThemeStatus()
        {
            using(SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                query = "SELECT theme FROM settings";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public void SetThemeStatus(bool status)
        {
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                query = "UPDATE settings SET theme=@theme";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@theme", (status ? 1 : 0).ToString());
                cmd.ExecuteScalar();
            }
        }
        public bool HasSalt()
        {
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                query = "SELECT CAST(CASE WHEN salt is NULL then 0 else 1 END as BIT) from settings";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public void SetSalt(string salt)
        {
            using(SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                query = "UPDATE settings SET salt=@salt";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@salt", salt);
                cmd.ExecuteScalar();
            }
        }
        public string GetSalt()
        {
            using(SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                query = "SELECT salt FROM settings";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                return cmd.ExecuteScalar().ToString();
            }
        }
    }
}
