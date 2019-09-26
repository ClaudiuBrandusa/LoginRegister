using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace LoginRegister
{
    public class AdminControl
    {
        string connection;
        public AdminControl(string Connection)
        {
            connection = Connection;
        }
        public void Ban(string id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                string query = "UPDATE accounts SET banned='True' WHERE Id=@Id";
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
                string query = "UPDATE accounts SET banned='False' WHERE Id=@Id";
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
                string query = "SELECT theme FROM settings";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public void SetThemeStatus(bool status)
        {
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                string query = "UPDATE settings SET theme=" + (status?1:0).ToString();
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.ExecuteScalar();
            }
        }
    }
}