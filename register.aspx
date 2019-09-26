<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="LoginRegister.register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://fonts.googleapis.com/css?family=Raleway&display=swap" rel="stylesheet"/>
    <link href="https://fonts.googleapis.com/css?family=Roboto&display=swap" rel="stylesheet"/>
<link rel="icon" type="image/png" href="/favicon.png"/>
    <title></title>
    <style>
body{
    padding:20px;
}
div{
text-align: center;
}
.btn{
    display: block;
}
.container{
    margin-left:15vw;
    margin-right:15vw;
    margin-top:15vh;
    padding-bottom:10px;
    border: 2px solid #878787;
    background-color:#ebebeb;
    /*Shadow*/
    -webkit-box-shadow: -4px 4px 10px -3px rgba(0,0,0,0.5);
    -moz-box-shadow: -4px 4px 10px -3px rgba(0,0,0,0.5);
    box-shadow: -4px 4px 10px -3px rgba(0,0,0,0.5);
}
table{
    margin: auto;
}
td{
    font-family: 'Roboto';
}
.error{
    display: block;
	margin-bottom: 5px;
    margin-top: 5px;
    font-family: 'Raleway';
    font-weight: 600;
}
a{
	text-decoration: none;
    color: blue;
}
.hint{
    display: block;
    font-family: 'Trebuchet MS', Arial, sans-serif;
}
    </style>
</head>
<body>
    <div class="container">
        <form id="form1" runat="server">
            <div>
                <table class="auto-style2">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label> 
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox> 
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lPass" runat="server" Text="Password"></asp:Label> 
                        </td>
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox> 
                        </td>
                    </tr>
                </table>
            
            </div>
            <div class="btn"><asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" /></div>
            <div class="error"><asp:Label ID="lbl1ErrorMessage" runat="server" Text="" ForeColor="#FF3300"></asp:Label></div>
            <div class="error"><asp:Label ID="lbl2ErrorMessage" runat="server" Text="" ForeColor="#FF3300"></asp:Label></div>
        </form>
        <div class="hint">Do you already have an account? Then <a href="login.aspx">login</a>.</div>
    </div>
</body>
</html>
