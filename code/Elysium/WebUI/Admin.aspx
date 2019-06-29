<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Chyld.Elysium.WebUI.Admin" %>

<asp:Content ID="contentNavigation" runat="server" ContentPlaceHolderID="contentplaceholderNavigation">
    <table>
        <tr>
            <td><asp:ImageButton ID="imagebuttonExit" runat="server" ImageUrl="~/Images/cross.png" OnClick="imagebuttonExit_Click" ToolTip="Exit" /></td>
            <td><asp:ImageButton ID="imagebuttonNotes" runat="server" ImageUrl="~/Images/note.png" OnClick="imagebuttonNotes_Click" ToolTip="Notes" /></td>
            <td><asp:ImageButton ID="imagebuttonCalendar" runat="server" ImageUrl="~/Images/calendar.png" OnClick="imagebuttonCalendar_Click" ToolTip="Calendar" /></td>
            <td><asp:Image ID="imageSeparator1" runat="server" ImageUrl="~/Images/separator.gif" /></td>
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
            <table class="admin">
                <tr>
                    <td><asp:TextBox ID="textboxSection" runat="server" MaxLength="50" /></td>
                    <td><asp:ImageButton ID="imagebuttonAdd" runat="server" ImageUrl="~/Images/add.png" OnClick="imagebuttonAdd_Click" ToolTip="Add" /></td>
                    <td><asp:DropDownList ID="dropdownlistSection" runat="server" AutoPostBack="true" /></td>
                    <td>
                        <asp:Panel ID="panelStyle" runat="server">
                            <table class="style">
                                <tr>
                                    <td>Font</td>
                                    <td><asp:DropDownList ID="dropdownlistFont" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td>Size</td>
                                    <td><asp:TextBox ID="textboxSize" runat="server" MaxLength="2" /></td>
                                </tr>
                                <tr>
                                    <td>Spacing</td>
                                    <td><asp:TextBox ID="textboxSpacing" runat="server" MaxLength="2" /></td>
                                </tr>
                                <tr>
                                    <td>Color</td>
                                    <td><asp:TextBox ID="textboxColor" runat="server" MaxLength="6" /></td>
                                </tr>
                                <tr>
                                    <td>Bold</td>
                                    <td><asp:CheckBox ID="checkboxBold" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td>Italic</td>
                                    <td><asp:CheckBox ID="checkboxItalic" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td>Underline</td>
                                    <td><asp:CheckBox ID="checkboxUnderline" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td><asp:ImageButton ID="imagebuttonUpdate" runat="server" ImageUrl="~/Images/save.png" OnClick="imagebuttonUpdate_Click" ToolTip="Update" /></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
