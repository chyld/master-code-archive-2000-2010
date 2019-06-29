<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Chyld.Chamaeleon.Revelation._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptmanager" runat="server" />        
        <asp:UpdatePanel ID="updatepanel" runat="server">
            <ContentTemplate>
                <div id="main">
                    <asp:TextBox ID="textboxA" runat="server"></asp:TextBox>
                    <asp:TextBox ID="textboxB" runat="server"></asp:TextBox>
                    <asp:TextBox ID="textboxC" runat="server"></asp:TextBox>
                    <asp:ImageButton ID="imagebutton1" runat="server" ImageUrl="~/Images/two.png" OnClick="Run" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
