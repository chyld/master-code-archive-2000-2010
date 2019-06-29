<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Chyld.Apocrypha.Fiction._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head" runat="server">
    <title></title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="formDefault" runat="server">
        <asp:ScriptManager ID="scriptmanager" runat="server" />        
        <asp:UpdatePanel ID="updatepanel" runat="server">
            <ContentTemplate>
                <div id="main">
                    <div id="image">
                        <asp:ImageButton ID="imageA" runat="server" ImageUrl="~/Images/dice.png" OnClick="Run" />
                    </div>
                    <table cellpadding="0" cellspacing="10" id="data">
                        <tr>
                            <td><asp:TextBox ID="textboxA" runat="server" /></td>
                            <td><asp:TextBox ID="textboxB" runat="server" /></td>
                            <td><asp:TextBox ID="textboxC" runat="server" /></td>
                            <td><asp:TextBox ID="textboxD" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="textboxE" runat="server" /></td>
                            <td><asp:TextBox ID="textboxF" runat="server" /></td>
                            <td><asp:TextBox ID="textboxG" runat="server" /></td>
                            <td><asp:TextBox ID="textboxH" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="textboxI" runat="server" TextMode="MultiLine" /></td>
                            <td><asp:TextBox ID="textboxJ" runat="server" TextMode="MultiLine" /></td>
                            <td><asp:TextBox ID="textboxK" runat="server" TextMode="MultiLine" /></td>
                            <td><asp:TextBox ID="textboxL" runat="server" TextMode="MultiLine" /></td>
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="textboxM" runat="server" /></td>
                            <td><asp:TextBox ID="textboxN" runat="server" /></td>
                            <td><asp:TextBox ID="textboxO" runat="server" /></td>
                            <td><asp:TextBox ID="textboxP" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="textboxQ" runat="server" /></td>
                            <td><asp:TextBox ID="textboxR" runat="server" /></td>
                            <td><asp:TextBox ID="textboxS" runat="server" /></td>
                            <td><asp:TextBox ID="textboxT" runat="server" /></td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
