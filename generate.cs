using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace LoginRegister
{

    public class Generate
    {
        string[] names = new string[] { "Radu", "Paula", "Denis", "Liviu", "Dan", "Darius", "Ana", "Florin", "Victor", "Dana", "Eusebiu", "Tudor", "Silviu", "Raul" };
        public void generate(int sum)
        {
            for(int i =0;i<sum;i++)
            {
                Register(GetName(), GetPassword());
            }
        }
        
        string GetName()
        {
            Random random = new Random();
            int index = random.Next(names.Length);
            return names[index];
        }

        string GetPassword()
        {
            Random random = new Random();
            string pass = "";
            int len = random.Next(6,10);
            for(int i=0;i<len;i++)
            {
                pass+= Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            }
            return pass;
        }

        void Register(string name, string pass)
        {
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost;initial Catalog=db;Integrated Security=True;"))
            {
                sqlCon.Open();
                string query = "SELECT COUNT(1) FROM accounts WHERE username=@username";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@username", name);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count != 1)
                {
                    Encryption Enc = new Encryption();
                    query = "INSERT INTO accounts (username, password) VALUES (@username,@password)";
                    sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@username", name);
                    sqlCmd.Parameters.AddWithValue("@password", Enc.PassHash(pass));
                    sqlCmd.ExecuteScalar();
                }
            }
        }
    }
}