<%@ Page Language="C#" MasterPageFile="~/Application/MasterPage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" Title="Get Organized Team! Change Password" Theme="Default" %>
<asp:Content ID="contentChangePassword" ContentPlaceHolderID="contentplaceholderMasterPage" Runat="Server">

<table>
<tr><td>


<div class="divModule">
    <div class="divModuleHeader">
        <img src="Images/ChangePassword.jpg" />
    </div>
    <div class="divModuleContent">
        <asp:ChangePassword ID="changepasswordChangePassword" runat="server" CancelDestinationPageUrl="../Default.aspx" ContinueDestinationPageUrl="../Default.aspx" ChangePasswordTitleText=""></asp:ChangePassword>
    </div>
</div>


</td></tr>
</table>

</asp:Content>
