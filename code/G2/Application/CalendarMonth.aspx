<%@ Page Language="C#" MasterPageFile="~/Application/MasterPage.master" AutoEventWireup="true" CodeFile="CalendarMonth.aspx.cs" Inherits="CalendarMonth" Title="Get Organized Team! Month Display" Theme="Default" %>
<asp:Content ID="contentCalendarMonth" ContentPlaceHolderID="contentplaceholderMasterPage" Runat="Server">

<table>
<tr><td>


<div class="divModule">
    <div class="divModuleHeader">
        <img src="Images/MonthlyEventDisplay.jpg" />
    </div>
    <div class="divModuleContent">
        <%= DisplayMonth() %>
    </div>
</div>


</td></tr>
</table>

</asp:Content>
