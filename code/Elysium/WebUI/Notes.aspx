<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Notes.aspx.cs" Inherits="Chyld.Elysium.WebUI.Notes" %>

<asp:Content ID="contentNavigation" runat="server" ContentPlaceHolderID="contentplaceholderNavigation">
    <table>
        <tr>
            <td><asp:ImageButton ID="imagebuttonExit" runat="server" ImageUrl="~/Images/cross.png" OnClick="imagebuttonExit_Click" ToolTip="Exit" /></td>
            <td><asp:ImageButton ID="imagebuttonCalendar" runat="server" ImageUrl="~/Images/calendar.png" OnClick="imagebuttonCalendar_Click" ToolTip="Calendar" /></td>
            <td><asp:ImageButton ID="imagebuttonAdmin" runat="server" ImageUrl="~/Images/admin.png" OnClick="imagebuttonAdmin_Click" ToolTip="Administration" /></td>
            <td><asp:Image ID="imageSeparator1" runat="server" ImageUrl="~/Images/separator.gif" /></td>
            <td><asp:ImageButton ID="imagebuttonSave" runat="server" ImageUrl="~/Images/save.png" OnClick="imagebuttonSave_Click" ToolTip="Save" /></td>
            <td><asp:Image ID="imageSeparator2" runat="server" ImageUrl="~/Images/separator.gif" /></td>
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
    <asp:UpdatePanel ID="updatepanel" runat="server">
        <ContentTemplate>
            <table class="notebook">
                <tr>
                    <td class="notebookcontrols">
                        <div class="leftcontrol"><asp:TextBox ID="textboxNotebookName" runat="server" MaxLength="50" /></div>
                        <div class="leftcontrol"><asp:ImageButton ID="imagebuttonAddNotebook" runat="server" ImageUrl="~/Images/add.png" OnClick="imagebuttonAddNotebook_Click" ToolTip="Add New Notebook" /></div>
                        <div class="rightcontrol"><asp:DropDownList ID="dropdownlistNotebookName" runat="server" AutoPostBack="true" /></div>
                    </td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="textboxNotebookData" runat="server" TextMode="MultiLine" Rows="20" /></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
