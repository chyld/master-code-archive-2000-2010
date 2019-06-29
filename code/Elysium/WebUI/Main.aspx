<%@ Page EnableEventValidation="false" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Chyld.Elysium.WebUI.Main" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<%@ Register Assembly="Controls" Namespace="Chyld.Elysium.Controls" TagPrefix="chyld" %>

<asp:Content ID="contentNavigation" runat="server" ContentPlaceHolderID="contentplaceholderNavigation">
    <table>
        <tr>
            <td><asp:ImageButton ID="imagebuttonExit" runat="server" ImageUrl="~/Images/cross.png" OnClick="imagebuttonExit_Click" ToolTip="Exit" /></td>
            <td><asp:ImageButton ID="imagebuttonNotes" runat="server" ImageUrl="~/Images/note.png" OnClick="imagebuttonNotes_Click" ToolTip="Notes" /></td>
            <td><asp:ImageButton ID="imagebuttonAdmin" runat="server" ImageUrl="~/Images/admin.png" OnClick="imagebuttonAdmin_Click" ToolTip="Administration" /></td>
            <td><asp:Image ID="imageSeparator1" runat="server" ImageUrl="~/Images/separator.gif" /></td>
            <td><asp:ImageButton ID="imagebuttonHome" runat="server" ImageUrl="~/Images/house.png" OnClick="imagebuttonHome_Click" ToolTip="Home" /></td>
            <td><asp:ImageButton ID="imagebuttonLeft" runat="server" ImageUrl="~/Images/left.png" OnClick="imagebuttonLeft_Click" ToolTip="Back" /></td>
            <td><asp:ImageButton ID="imagebuttonRight" runat="server" ImageUrl="~/Images/right.png" OnClick="imagebuttonRight_Click" ToolTip="Forward" /></td>
            <td><asp:Image ID="imageSeparator2" runat="server" ImageUrl="~/Images/separator.gif" /></td>
            <td><asp:ImageButton ID="imagebuttonWeek" runat="server" ImageUrl="~/Images/week.gif" OnClick="imagebuttonWeek_Click" ToolTip="Week" /></td>
            <td><asp:ImageButton ID="imagebuttonBiWeek" runat="server" ImageUrl="~/Images/biweek.gif" OnClick="imagebuttonBiWeek_Click" ToolTip="BiWeekly" /></td>
            <td><asp:ImageButton ID="imagebuttonMonth" runat="server" ImageUrl="~/Images/month.gif" OnClick="imagebuttonMonth_Click" ToolTip="Month" /></td>
            <td><asp:ImageButton ID="imagebuttonYear" runat="server" ImageUrl="~/Images/year.gif" OnClick="imagebuttonYear_Click" ToolTip="Year" /></td>
            <td><asp:Image ID="imageSeparator3" runat="server" ImageUrl="~/Images/separator.gif" /></td>
            <td>
                <asp:UpdateProgress ID="updateprogress" runat="server">
                    <ProgressTemplate>
                        <asp:Image ID="imageWaiting" runat="server" ImageUrl="~/Images/waiting.gif" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="contentMain" runat="server" ContentPlaceHolderID="contentplaceholderMain">
    <table>
        <tr>
            <td>
                <asp:Panel ID="panelHeader" runat="server" CssClass="panelheader">
                    <asp:ImageButton ID="imagebutton" runat="server" ImageUrl="~/Images/down.png" />
                </asp:Panel>
                <asp:Panel ID="panelFilter" runat="server" CssClass="panelfilter"/>
                <act:CollapsiblePanelExtender ID="collapsiblepanelextender" runat="server" TargetControlID="panelfilter" ExpandControlID="panelheader" CollapseControlID="panelheader" Collapsed="true" SuppressPostBack="true" ImageControlID="imagebutton" ExpandedImage="~/Images/up.png" CollapsedImage="~/Images/down.png" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="updatepanel" runat="server">
                    <ContentTemplate>
                        <chyld:Calendar ID="calendar" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
