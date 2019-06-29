<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dream.aspx.cs" Inherits="Chyld.Scribe.Scribe.DreamPage" Theme="Scribe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>:: Scribe ::</title>
</head>
<body>
    <form id="formDream" runat="server">
        <div id="master">
            <div id="header">
                ?
            </div>
            <div id="content">
                <div class="passwordcontainer"><asp:TextBox ID="textboxPasswordA" runat="server" TextMode="Password" CssClass="passwordbox" /></div>
                <div class="passwordcontainer"><asp:TextBox ID="textboxPasswordB" runat="server" TextMode="Password" CssClass="passwordbox" /></div>
                <div class="passwordcontainer"><asp:TextBox ID="textboxPasswordC" runat="server" TextMode="password" CssClass="passwordbox" /></div>
                <div class="passwordcontainer"><asp:Button ID="buttonPasswordAuthenticate" runat="server" Text="..." OnClick="buttonPasswordAuthenticate_Click" /></div>
            </div>
        </div>
    </form>
</body>
</html>
