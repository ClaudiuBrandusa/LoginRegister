using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace LoginRegister
{
    public partial class Dashboard : System.Web.UI.Page
    {
        DataTable dt;
        DataTable dt1;
        string connection = @"Data Source=.\DB;initial Catalog=db;Integrated Security=True;";
        AdminControl ac = new AdminControl(@"Data Source=.\DB;initial Catalog=db;Integrated Security=True;");
        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateTheme();
            if (!Page.IsPostBack)
            {
                if (Session["username"] != null)
                {
                    lblUserDetails.Text = "" + Session["username"];
                    lblUsername.Text = "" + Session["username"];
                    lblId.Text = "" + Session["id"];
                    lblRank.Text = GetRankName(Convert.ToInt32(Session["rank"]));
                    members.Visible = false;
                    settings.Visible = false;
                    banned.Visible = false;
                    selected_user.Visible = false;
                    // buttons
                    if (Convert.ToInt32(Session["rank"]) > 0)
                    {
                        control_panel.Visible = true;
                        SetButtons();
                    }
                    else
                    {
                        control_panel.Visible = false;
                    }
                    // those two lines will generate random accounts, for more details go to generate.cs
                    //Generate delMe = new Generate();
                    //delMe.generate(20);
                }
                else
                {
                    Response.Redirect("login.aspx");
                }
            }    
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        protected void imgBtn1_Click(object sender, ImageClickEventArgs e)
        {
            members.Visible = true;
            DynamicDataTable();
            if (settings.Visible)
            {
                settings.Visible = false;
            }
            if (banned.Visible)
            {
                banned.Visible = false;
            }
            if (selected_user.Visible)
            {
                selected_user.Visible = false;
            }
        }
        protected void imgBtn2_Click(object sender, ImageClickEventArgs e)
        {
            banned.Visible = true;
            BannedDataTable();
            if (members.Visible)
            {
                members.Visible = false;
            }
            if (settings.Visible)
            {
                settings.Visible = false;
            }

            if (selected_user.Visible)
            {
                selected_user.Visible = false;
            }
        }
        protected void imgBtn3_Click(object sender, ImageClickEventArgs e)
        {
            settings.Visible = true;
            btnChangeTheme.Text = (ac.GetThemeStatus() ? "Dark Mode" : "Light Mode");
            if (members.Visible)
            {
                members.Visible = false;
            }
            if (banned.Visible)
            {
                banned.Visible = false;
            }
            if(selected_user.Visible)
            {
                selected_user.Visible = false;
            }
        }
        protected void exit0_Click(object sender, EventArgs e)
        {
            members.Visible = false;
        }

        protected void exit1_Click(object sender, EventArgs e)
        {
            settings.Visible = false;
        }

        protected void exit2_Click(object sender, EventArgs e)
        {
            banned.Visible = false;
        }
        //  functions  //
        // table generators //
        public void DynamicDataTable()
        {
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT id, username, rank FROM accounts WHERE banned='False'", sqlCon);
                dt = new DataTable();
                sqlDA.Fill(dt);
                gV1.DataSource = dt;
                gV1.CellPadding = 10;
                gV1.DataBind();
                foreach (GridViewRow row in gV1.Rows)
                {
                    for(int i =0;i<row.Cells.Count; i++)
                    {
                        if(i==2)
                        {
                            row.Cells[3].Text = GetRankName(Convert.ToInt32(row.Cells[3].Text));
                        }
                        row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                    }
                }
            }
        }
        public void BannedDataTable()
        {
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT id, username, rank FROM accounts WHERE banned='True'", sqlCon);
                dt1 = new DataTable();
                sqlDA.Fill(dt1);
                if (dt1.Rows.Count>0)
                {
                    gVBanned.DataSource = dt1;
                    gVBanned.CellPadding = 10;
                    gVBanned.DataBind();
                    foreach (GridViewRow row in gVBanned.Rows)
                    {
                        for (int i = 0; i < row.Cells.Count; i++)
                        {
                            if (i == 2)
                            {
                                row.Cells[3].Text = GetRankName(Convert.ToInt32(row.Cells[3].Text));
                            }
                            row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                        }
                    }
                }else
                {
                    banned.Visible = false;
                }
            }
        }
        public string GetRankName(int rank)
        {
            switch (rank)
            {
                case 0:
                    return "Member";
                case 1:
                    return "Moderator";
                case 2:
                    return "Admin";
                default:
                    return "dunno";
            }
        }
        //  Selection //
        protected void gV1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gV1.PageIndex = e.NewPageIndex;
            gV1.DataBind();
            members.Visible = true;
            DynamicDataTable();
        }
        protected void gV1_SelectedIndexChanged(object sender, EventArgs e)
        {

            selected_user.Visible = true;
            lblSUId.Text = (gV1.SelectedRow.Cells[1].Text).ToString();
            lblSUUsername.Text = (gV1.SelectedRow.Cells[2].Text).ToString();
            lblSURank.Text = (gV1.SelectedRow.Cells[3].Text).ToString();
            btnBan.Text = "Ban";
        }
        protected void gV1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // you can use this function to highlight your row when you hover over.
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='white';";
                //e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='#ebebeb';";
            }
        }
        // Banned //
        protected void gVBanned_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gVBanned.PageIndex = e.NewPageIndex;
            gVBanned.DataBind();
            banned.Visible = true;
            BannedDataTable();
        }
        protected void gVBanned_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected_user.Visible = true;
            lblSUId.Text = (gVBanned.SelectedRow.Cells[1].Text).ToString();
            lblSUUsername.Text = (gVBanned.SelectedRow.Cells[2].Text).ToString();
            lblSURank.Text = (gVBanned.SelectedRow.Cells[3].Text).ToString();
            btnBan.Visible = true;
            btnBan.Text = "Unban";
        }
        protected void gVBanned_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        //  Theme  //
        protected void btnChangeTheme_Click(object sender, EventArgs e)
        {
            ChangeTheme();
            ac.SetThemeStatus(!ac.GetThemeStatus());
            SetButtons();
        }
        // AdminControl //
        protected void SetButtons()
        {
            imgBtn1.ImageUrl = GetImgPath() + "imgBtn1.png";
            imgBtn2.ImageUrl = GetImgPath() + "imgBtn2.png";
            imgBtn3.ImageUrl = GetImgPath() + "imgBtn3.png";
        }
        protected string GetImgPath()
        {
            if(ac.GetThemeStatus())
            {
                return "darkmode/";
            }
            else
            {
                return "lightmode/";
            }
        }
        protected void ChangeTheme()
        {
            if (ac.GetThemeStatus())
            {
                btnChangeTheme.Text = "Light Mode";
                theme.Attributes["href"] = "lightmode.css";
            }
            else
            {
                btnChangeTheme.Text = "Dark Mode";
                theme.Attributes["href"] = "darkmode.css";
            }
        }
        protected void UpdateTheme()
        {
            if (ac.GetThemeStatus())
            {
                theme.Attributes["href"] = "darkmode.css";
            }
            else
            {
                theme.Attributes["href"] = "lightmode.css";
            }
            SetButtons();
        }
        protected void btnBan_Click(object sender, EventArgs e)
        {
            string id = lblSUId.Text.Trim();
            if (gV1.Visible)
            {
                ac.Ban(id);
                gV1.SelectedIndex = -1;
                DynamicDataTable();
            } else
            {
                ac.UnBan(id);
                gVBanned.SelectedIndex = -1;
                BannedDataTable();
            }
            selected_user.Visible = false;
        }
    }
}