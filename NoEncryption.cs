using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace LoginRegister
{
    public class NoEncryption
    {
        List<string> usernames;
        public NoEncryption(string connection)
        {
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                SqlDataReader dr;
                usernames = new List<string>();
                string query = "SELECT * FROM no_encrypt";
                string howMany = "SELECT Count(*) FROM no_encrypt";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                SqlCommand hM = new SqlCommand(howMany, sqlCon);
                int count = Convert.ToInt32(hM.ExecuteScalar());
                dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    usernames.Add(dr[0].ToString());
                    //dr.NextResult();
                }
            }
        }
        public bool isIn(string name)
        {
            if(usernames.Contains(name))
            {
                return true;
            }
            return false;
        }

    }
}