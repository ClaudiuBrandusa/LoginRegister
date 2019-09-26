<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="LoginRegister.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="icon" type="image/png" href="/favicon.png"/>
    <link href="https://fonts.googleapis.com/css?family=Raleway&display=swap" rel="stylesheet"/>
    <link href="https://fonts.googleapis.com/css?family=Roboto&display=swap" rel="stylesheet"/>
    <title></title>
    <style>
body{
    font-family: 'Raleway', sans-serif;
}
.container{
    margin-left:5vw;
    margin-right:5vw;
    margin-top: 2vh;
    padding-bottom:10px;
    border: 2px solid #878787;
}
.title{
    margin-top: 8px;
    text-align: center;
}
#lblUserDetails{
    font-weight: 600;
}
table{
    padding-top: 30px;
    margin: auto;
}
table tr td:first-child{
    text-align: right;
    font-weight: bold;
    font-family: 'Roboto', sans-serif;
}
.buttons{
    margin: 0;
    padding: 0;
}
.buttons ul li {
    display: inline-block;
    margin-right: 10vw;
}
.buttons ul li:last-child{
    margin: 0;
}
.buttons ul {
    margin: 0;
    text-align: center;
}
#imgBtn1, #imgBtn2, #imgBtn3{
    padding:5px 10px 5px 10px;
    display: block;
}
.exit{
    margin-top: 5px;
    margin-right: 5px;
    float: right;
}
#btnLogout{
    margin-left: 5vw;
}
.table{
    margin-top: 10px;
    font-family: 'Raleway', sans-serif;
}
h3{
    text-align: center;
}
.button {
    margin-left: 50%;
    margin-top: 10px;
    font-size: 18px;
    border-width: 1px;
    border-radius: 10%;
}
.pagination span{  /*Current page*/
    padding:3px 6px 3px  6px;
    border: 1px solid darkblue;
    border-radius: 10%;
    font-weight: bold;
}
.pagination {
    font-family: 'Roboto', sans-serif;
    font-size: 20px;
}
.pagination table{
    padding-top: 0;
}
.pagination a, a{ /*Noncurrent pages*/
    padding:2px 4px 2px  4px;
    border: 2px solid black;
    border-radius: 10%;
    font-weight: bold;
    text-decoration: none;
}
.element {
    text-align: center;
}
.element * {
    
    display: inline-block;
}
.setting_rows {
    margin-left: 15px;
}
    </style>
    
    <link id="theme" runat="server" rel="stylesheet" type="text/css" href="lightmode.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" >
            <div class="title">Welcome <asp:Label ID="lblUserDetails" runat="server" Text=""></asp:Label>, to the dashboard.</div>
        </div>
        <div class="container">
            <h3>My infos</h3>
            <table>
                <tr>
                    <td style="text-align: right">
                        Username : 
                    </td>
                    <td>
                        <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        Id : 
                    </td>
                    <td>
                        <asp:Label ID="lblId" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        Rank : 
                    </td>
                    <td>
                        <asp:Label ID="lblRank" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="container" id="control_panel" runat="server">
            <div class="buttons">
                <ul style="list-style-type: none;">
                    <li>
                        <asp:ImageButton ID="imgBtn1" runat="server" OnClick="imgBtn1_Click" draggable="false"/>
                        <p>Members List</p>
                    </li>
                    <li>
                        <asp:ImageButton ID="imgBtn2" runat="server" OnClick="imgBtn2_Click" draggable="false"/>
                        <p>Banned List</p>
                    </li>
                    <li>
                        <asp:ImageButton ID="imgBtn3" runat="server" OnClick="imgBtn3_Click" draggable="false"/>
                        <p>Settings</p>
                    </li>
                </ul>
            </div>
        </div>
        <div class="container" id="members" runat="server">
            <asp:Button CssClass="exit" ID="exit0" runat="server" Text="X" OnClick="exit0_Click"/>
            <!--There goes the table-->
            <asp:GridView ID="gV1" CssClass="table" runat="server" AutoGenerateColumns="False" EnableViewState="true" AllowPaging="True" PageSize="5"  onpageindexchanging="gV1_PageIndexChanging" OnSelectedIndexChanged="gV1_SelectedIndexChanged" OnRowDataBound="gV1_RowDataBound" AutoGenerateSelectButton="True">

                <Columns>
                    <asp:BoundField DataField="id" HeaderText="User id"/>
                    <asp:BoundField DataField="username" HeaderText="User Name"/>
                    <asp:BoundField DataField="rank" HeaderText="User Rank"/>
                </Columns>
                <SelectedRowStyle BackColor="white" Font-Bold="true"  ForeColor="Black" BorderWidth="2" BorderStyle="Inset"/>
                <PagerStyle  CssClass="pagination" />
            </asp:GridView>
        </div>
        <div class="container" id="banned" runat="server">
            <asp:Button CssClass="exit" ID="exit2" runat="server" Text="X" OnClick="exit2_Click"/>
            <!--There goes the table-->
            <asp:GridView ID="gVBanned" CssClass="table" runat="server"  AutoGenerateColumns="False" EnableViewState="true" AllowPaging="True" PageSize="5"  onpageindexchanging="gVBanned_PageIndexChanging" OnSelectedIndexChanged="gVBanned_SelectedIndexChanged" OnRowDataBound="gVBanned_RowDataBound" AutoGenerateSelectButton="True">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="User id"/>
                    <asp:BoundField DataField="username" HeaderText="User Name"/>
                    <asp:BoundField DataField="rank" HeaderText="User Rank"/>
                </Columns>
                <SelectedRowStyle BackColor="white" Font-Bold="true"  ForeColor="Black" BorderWidth="2" />
                <PagerStyle  CssClass="pagination" />
            </asp:GridView>
        </div>
        <div class="container" id="settings" runat="server">
            <asp:Button CssClass="exit" ID="exit1" runat="server" Text="X" OnClick="exit1_Click"/>
            <h3>Settings</h3>
            <div class="element">
                <h4>Change Theme :</h4>
                <asp:Button CssClass="setting_rows" ID="btnChangeTheme" runat="server" Text="" OnClick="btnChangeTheme_Click"/>
            </div>
        </div>
        <div class="container" id="selected_user" runat="server">
            <h3>Selected User infos</h3>
            <table>
                <tr>
                    <td>
                        Username :
                    </td>
                    <td>
                        <asp:Label ID="lblSUUsername" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td> 
                        Id :
                    </td>
                    <td>
                        <asp:Label ID="lblSUId" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Rank :
                    </td>
                    <td>
                        <asp:Label ID="lblSURank" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:Button CssClass="button" ID="btnBan" runat="server" Text="Ban" OnClick="btnBan_Click"/>
        </div>
        <p>
            <asp:Button ID="btnLogout" runat="server" Text="Logout" Width="58px" OnClick="btnLogout_Click" />
        </p>
    </form>
</body>
</html>
