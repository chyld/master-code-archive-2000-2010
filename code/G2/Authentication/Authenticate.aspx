<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Authenticate.aspx.cs" Inherits="Authenticate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Get Organized Team! User Authentication</title>
    <style type="text/css">
        body, table, input
        {
            font-family: Tahoma;
            font-size: 11px;
        }
        
        a
        {
            color: #336699;
            text-decoration: none;
        }
        
        a:hover
        {
            text-decoration: underline;
        }
                      
        .LoginRoot
        {
            margin: 50px auto 50px auto;
            border-bottom: 5px double #669933;
        }
        
        .LoginLabel
        {
            padding: 10px 30px 10px 0px;
            color: #336699;
            text-align: left;
        }
        
        .LoginTextBox
        {
            border: 1px solid #669933;
            color: #000000;
            width: 150px;
        }
        
        .LoginButton
        {
            padding: 1px;
            border: 1px solid #264D73;
            background-color: #336699;
            color: #ffffff;
        }
        
        .LoginTitle
        {
            font-size: 13px;
            color: #ffffff;
            border: 1px solid #669933;
            height: 30px;
            background: #000000 url(Images/AuthenticationHeader.gif) top left repeat-x;
        }
        
        .LoginHyperLink
        {
            padding: 20px 0px 10px 0px;
        }
    </style>
</head>
<body>
    <form id="formAuthenticate" runat="server">
    <div>
        <asp:Login ID="loginAuthenticate" runat="server" CssClass="LoginRoot" LoginButtonText="Submit Credentials" TitleText="User Authentication System" DisplayRememberMe="False" DestinationPageUrl="../Default.aspx" CreateUserIconUrl="Images/User.jpg" CreateUserText="New User?" CreateUserUrl="Register.aspx" PasswordRecoveryIconUrl="Images/Password.jpg" PasswordRecoveryText="Forget Password?" PasswordRecoveryUrl="ResetPassword.aspx" LabelStyle-CssClass="LoginLabel" TextBoxStyle-CssClass="LoginTextBox" TitleTextStyle-CssClass="LoginTitle" LoginButtonStyle-CssClass="LoginButton" HyperLinkStyle-CssClass="LoginHyperLink"></asp:Login>
    </div>
    </form>
    <script type="text/javascript">
        document.getElementById("loginAuthenticate_UserName").focus();
    </script>
</body>
</html>
