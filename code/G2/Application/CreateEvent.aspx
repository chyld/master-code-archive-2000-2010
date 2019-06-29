<%@ Page Language="C#" MasterPageFile="~/Application/MasterPage.master" AutoEventWireup="true" CodeFile="CreateEvent.aspx.cs" Inherits="CreateEvent" Title="Get Organized Team! Create New Event" Theme="Default" %>
<asp:Content ID="contentCreateEvent" ContentPlaceHolderID="contentplaceholderMasterPage" Runat="Server">

<table>
<tr><td>


<div class="divModule">
    <div class="divModuleHeader">
        <img src="Images/CreateNewEvent.jpg" />
    </div>
    <div class="divModuleContent">
        <table>
            <tr>
                <td>Event Name</td>
                <td><asp:TextBox ID="textboxEventName" CssClass="fullWidth" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="requiredfieldvalidatorEventName" runat="server" ErrorMessage="e.g., Book Seller's Convention" ControlToValidate="textboxEventName" SetFocusOnError="True" ValidationGroup="AddEvent" Display="Dynamic"></asp:RequiredFieldValidator></td>
                <td>Event Location</td>
                <td><asp:TextBox ID="textboxEventLocation" CssClass="fullWidth" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="requiredfieldvalidatorEventLocation" runat="server" ErrorMessage="e.g., Hilton Hotel, Ste 5, Miami, Fl" ControlToValidate="textboxEventLocation" SetFocusOnError="True" ValidationGroup="AddEvent" Display="Dynamic"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:SqlDataSource ID="sqldatasourceGroupName" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="got_GetGroupList" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="UserId" SessionField="UserId" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource></td>
                <td>Participating Group</td>
                <td><asp:DropDownList ID="dropdownlistGroupName" CssClass="fullWidth" runat="server" DataSourceID="sqldatasourceGroupName" DataTextField="GroupName" DataValueField="GroupId"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>Event Start Time</td>
                <td><asp:TextBox ID="textboxStartTime" CssClass="fullWidth" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="requiredfieldvalidatorStartTime" runat="server" ErrorMessage="e.g., 8:00 AM" ControlToValidate="textboxStartTime" SetFocusOnError="True" ValidationGroup="AddEvent" Display="Dynamic"></asp:RequiredFieldValidator></td>
                <td>Event End Time</td>
                <td><asp:TextBox ID="textboxEndTime" CssClass="fullWidth" runat="server"></asp:TextBox><br /><asp:RequiredFieldValidator ID="requiredfieldvalidatorEndTime" runat="server" ErrorMessage="e.g., 8:30 AM" ControlToValidate="textboxEndTime" SetFocusOnError="True" ValidationGroup="AddEvent" Display="Dynamic"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>Event Start Date</td>
                <td><asp:Calendar ID="calendarStartDate" runat="server"></asp:Calendar><br /><asp:CustomValidator ID="customvalidatorStartDate" runat="server" Display="Dynamic"></asp:CustomValidator></td>
                <td>Event End Date</td>
                <td><asp:Calendar ID="calendarEndDate" runat="server"></asp:Calendar><br /><asp:CustomValidator ID="customvalidatorEndDate" runat="server" Display="Dynamic"></asp:CustomValidator></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Button ID="buttonCreateEvent" runat="server" Text="Create New Event" OnClick="buttonCreateEvent_Click" ValidationGroup="AddEvent" /><br /><br /><asp:CustomValidator ID="customvalidatorCreateEvent" runat="server" Display="Dynamic"></asp:CustomValidator></td>
                <td></td>
                <td></td>
            </tr>
        </table>
    </div>
</div>


</td></tr>
</table>

</asp:Content>
