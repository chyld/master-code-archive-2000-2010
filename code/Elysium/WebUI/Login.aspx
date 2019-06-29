<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Chyld.Elysium.WebUI.Login" %>

<asp:Content ID="contentNavigation" runat="server" ContentPlaceHolderID="contentplaceholderNavigation">
    <table>
        <tr>
            <td><asp:ImageButton ID="imagebuttonLogin" runat="server" OnClick="imagebuttonLogin_Click" ImageUrl="~/Images/lock.png" ToolTip="Login" /></td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="contentMain" runat="server" ContentPlaceHolderID="contentplaceholderMain">
    <table class="login">
        <tr>
            <td><asp:Image ID="imageUsername" runat="server" ImageUrl="~/Images/user.png" ToolTip="Username" /></td>
            <td><asp:TextBox ID="textboxUsername" runat="server" MaxLength="50" /></td>
        </tr>
        <tr>
            <td><asp:Image ID="imagePassword" runat="server" ImageUrl="~/Images/key.png" ToolTip="Password" /></td>
            <td><asp:TextBox ID="textboxPassword" runat="server" MaxLength="50" TextMode="Password" /></td>
        </tr>
    </table>
</asp:Content>
