<%@ Page Language="C#" MasterPageFile="~/Application/MasterPage.master" AutoEventWireup="true" CodeFile="CalendarHybrid.aspx.cs" Inherits="CalendarHybrid" Title="Get Organized Team! Hybrid Display" Theme="Default" %>
<asp:Content ID="contentCalendarHybrid" ContentPlaceHolderID="contentplaceholderMasterPage" Runat="Server">

<table>
<tr><td>


<div class="divModule">
    <div class="divModuleHeader">
        <img src="Images/HybridEventDisplay.jpg" />
    </div>
    <div class="divModuleContent">
        <%= DisplayHybrid() %>
    </div>
</div>


</td></tr>
</table>

</asp:Content>
