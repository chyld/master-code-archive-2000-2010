<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<html>
<head runat="server">
    <title>Get Organized Team! User Registration</title> 
    <style type="text/css">
        body
        {
            text-align: center;
        }
        
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
                      
        .RegisterRoot
        {
            margin: 50px auto 50px auto;
            border-bottom: 5px double #669933;
        }
        
        .RegisterLabel
        {
            padding: 10px 30px 10px 0px;
            color: #336699;
            text-align: left;
        }
        
        .RegisterTextBox
        {
            border: 1px solid #669933;
            color: #000000;
            width: 150px;
        }
        
        .RegisterTitle
        {
            font-size: 13px;
            color: #ffffff;
            border: 1px solid #669933;
            height: 30px;
            background: #000000 url(Images/AuthenticationHeader.gif) top left repeat-x;
        }
        
        .RegisterHyperLink
        {
            padding: 20px 0px 10px 0px;
        }
        
        .RegisterButton
        {
            padding: 1px;
            border: 1px solid #264D73;
            background-color: #336699;
            color: #ffffff;
        }
    </style>    
</head>
<body>
    <form id="formRegister" runat="server">
    <div>
        <asp:CreateUserWizard ID="createuserwizardRegister" runat="server" CssClass="RegisterRoot" CancelDestinationPageUrl="Authenticate.aspx" ContinueDestinationPageUrl="../Default.aspx" DisplayCancelButton="True" FinishDestinationPageUrl="../Default.aspx" LabelStyle-CssClass="RegisterLabel" TextBoxStyle-CssClass="RegisterTextBox" TitleTextStyle-CssClass="RegisterTitle" HyperLinkStyle-CssClass="RegisterHyperLink" CancelButtonStyle-CssClass="RegisterButton" ContinueButtonStyle-CssClass="RegisterButton" CreateUserButtonStyle-CssClass="RegisterButton" FinishCompleteButtonStyle-CssClass="RegisterButton">
            <WizardSteps>
                <asp:CreateUserWizardStep ID="createuserwizardstepRegister" runat="server" Title="Register for a New Account">
                </asp:CreateUserWizardStep>
                <asp:CompleteWizardStep ID="completewizardstepRegister" runat="server" Title="Your Account is Active">
                </asp:CompleteWizardStep>
            </WizardSteps>
        </asp:CreateUserWizard>
    </div>
    </form>
</body>
</html>
