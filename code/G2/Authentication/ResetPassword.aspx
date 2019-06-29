<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="ResetPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Get Organized Team! Lost Password</title>
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
                      
        .PasswordRoot
        {
            margin: 50px auto 50px auto;
            border-bottom: 5px double #669933;
        }
        
        .PasswordLabel
        {
            padding: 10px 30px 10px 0px;
            color: #336699;
            text-align: left;
        }
        
        .PasswordTextBox
        {
            border: 1px solid #669933;
            color: #000000;
            width: 150px;
        }
        
        .PasswordButton
        {
            padding: 1px;
            border: 1px solid #264D73;
            background-color: #336699;
            color: #ffffff;
        }
        
        .PasswordTitle
        {
            font-size: 13px;
            color: #ffffff;
            border: 1px solid #669933;
            height: 30px;
            background: #000000 url(Images/AuthenticationHeader.gif) top left repeat-x;
        }
    </style>    
</head>
<body>
    <form id="formResetPassword" runat="server">
    <div>
        <asp:PasswordRecovery ID="passwordrecoveryResetPassword" runat="server" CssClass="PasswordRoot" LabelStyle-CssClass="PasswordLabel" SubmitButtonStyle-CssClass="PasswordButton" TextBoxStyle-CssClass="PasswordTextBox" TitleTextStyle-CssClass="PasswordTitle" SuccessPageUrl="../Login.aspx" UserNameInstructionText="Enter your User Name." QuestionInstructionText="Answer the following question.">
        </asp:PasswordRecovery>
    </div>
    </form>
</body>
</html>
