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
    public partial class Login : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        Encryption Enc = new Encryption();
        NoEncryption noEnc;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                string banQuery = "SELECT banned FROM accounts WHERE username=@username";
                SqlCommand GetBanStatus = new SqlCommand(banQuery, sqlCon);
                GetBanStatus.Parameters.AddWithValue("@username", txtUserName.Text.Trim());
                if (Convert.ToInt32(GetBanStatus.ExecuteScalar()) != 1)
                {
                    noEnc = new NoEncryption(connection);
                    string query = "SELECT COUNT(1) FROM accounts WHERE username=@username AND password=@password";
                    string idQuery = "SELECT id FROM accounts WHERE username=@username";
                    string rankQuery = "SELECT rank FROM accounts WHERE username=@username";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    SqlCommand getIdCmd = new SqlCommand(idQuery, sqlCon);
                    SqlCommand getRankCmd = new SqlCommand(rankQuery, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@username", txtUserName.Text.Trim());
                    getIdCmd.Parameters.AddWithValue("@username", txtUserName.Text.Trim());
                    getRankCmd.Parameters.AddWithValue("@username", txtUserName.Text.Trim());
                    bool status = false; // true - connected
                    try
                    {
                        if (noEnc.isIn(txtUserName.Text)) // this will let us to use non encrypted passwords that we have added from the Database
                        {
                            sqlCmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            status = Convert.ToBoolean(sqlCmd.ExecuteScalar());
                        }
                        else
                        {
                            string getPass = "SELECT password FROM accounts WHERE username=@username";
                            SqlCommand GetPass = new SqlCommand(getPass, sqlCon);
                            GetPass.Parameters.AddWithValue("@username", txtUserName.Text.Trim());
                            string pass = GetPass.ExecuteScalar().ToString();
                            status = Enc.Verify(txtPassword.Text.Trim().ToString(), pass);
                        }
                    } catch
                    {
                        lblErrorMessage.Text = "Invalid credentials.";
                        lblErrorMessage.Visible = true;
                    }

                    if (status)
                    {
                        Session["username"] = txtUserName.Text.Trim();
                        Session["id"] = getIdCmd.ExecuteScalar();
                        Session["rank"] = getRankCmd.ExecuteScalar();
                        Response.Redirect("Dashboard.aspx");
                    }
                    else
                    {
                        lblErrorMessage.Text = "Invalid credentials.";
                        lblErrorMessage.Visible = true;
                    }
                }else
                {
                    lblErrorMessage.Text = "This account has been banned from this server.";
                    lblErrorMessage.Visible = true;
                }
            }
        }
    }
}
