using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace LoginRegister
{
    public partial class register : System.Web.UI.Page
    {
        Encryption Enc = new Encryption();
        string connection = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            lbl1ErrorMessage.Visible = false;
            lbl2ErrorMessage.Visible = false;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            bool check = false; 
            lbl1ErrorMessage.Visible = false;
            if (txtPassword.Text.ToString().Length > 7)
            {
                check = true;
            }
            else if(txtPassword.Text.ToString().Length > 23)
            {
                lbl1ErrorMessage.Text = "The password you entered is too long.";
                lbl1ErrorMessage.Visible = true;
            }
            else
            {
                lbl1ErrorMessage.Text = "The password you entered is too short.";
                lbl1ErrorMessage.Visible = true;
            }
            lbl2ErrorMessage.Visible = false;
            if (txtUserName.Text.ToString().Length > 2)
            {
                using (SqlConnection sqlCon = new SqlConnection(connection))
                {
                    sqlCon.Open();
                    string query = "SELECT COUNT(1) FROM accounts WHERE username=@username";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@username", txtUserName.Text.Trim());
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    if (count != 1)
                    {
                        //count == 0 means that the introduced username is not used.
                        check = true;
                    }
                    else
                    {
                        lbl1ErrorMessage.Text = "This username has been taken. Please use another nickname.";
                        lbl1ErrorMessage.Visible = true;
                        check = false;
                    }
                }
            }
            else if (txtUserName.Text.ToString().Length > 23)
            {
                lbl2ErrorMessage.Text = "The username you entered is too long.";
                lbl2ErrorMessage.Visible = true;
                check = false;
            }
            else
            {
                lbl2ErrorMessage.Text = "The username you entered is too short.";
                lbl2ErrorMessage.Visible = true;
                check = false;
            }
            if(check)
            {
                using (SqlConnection sqlCon = new SqlConnection(connection))
                {
                    sqlCon.Open();
                    string query = "INSERT INTO accounts (username, password) VALUES (@username,@password)";
                    string idQuery = "SELECT id FROM accounts WHERE username=@username";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    SqlCommand getIdCmd = new SqlCommand(idQuery, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@username", txtUserName.Text.Trim());
                    getIdCmd.Parameters.AddWithValue("@username", txtUserName.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@password", Enc.PassHash(txtPassword.Text.Trim()));
                    sqlCmd.ExecuteScalar();
                    Session["username"] = txtUserName.Text.Trim();
                    Session["id"] = getIdCmd.ExecuteScalar();
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }
    }
}
