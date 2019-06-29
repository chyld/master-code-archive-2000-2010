<%@ Page Language="C#" MasterPageFile="~/Application/MasterPage.master" AutoEventWireup="true" CodeFile="GroupAdmin.aspx.cs" Inherits="GroupAdmin" Title="Get Organized Team! Group Administration" Theme="Default" %>
<asp:Content ID="contentGroupAdmin" ContentPlaceHolderID="contentplaceholderMasterPage" Runat="Server">

<table>
<tr><td colspan="2">


<div class="divModule">
    <div class="divModuleHeader">
        <img src="Images/GroupMembershipList.jpg" />
    </div>
    <div class="divModuleContent">
        <asp:GridView ID="gridviewGroupAdmin" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="MembershipId" DataSourceID="sqldatasourceGroupAdmin" PageSize="5">
            <Columns>
                <asp:BoundField DataField="MembershipId" HeaderText="MembershipId" ReadOnly="True" SortExpression="MembershipId" Visible="False" />
                <asp:BoundField DataField="GroupName" HeaderText="Group Name" SortExpression="GroupName" />
                <asp:TemplateField HeaderText="Group Color" SortExpression="GroupColor">
                    <ItemTemplate>
                        <img style="border:1px solid #000000; background-color:#<%# Eval("GroupColor") %>;" src="Images/TransparentPixel.gif" width="12" height="12" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="MemberCount" HeaderText="Member Count" SortExpression="MemberCount" />
                <asp:TemplateField HeaderText="Owner" SortExpression="IsOwner">
                    <ItemTemplate>
                        <img src="Images/<%# GetGroupOwnerImage((bool)Eval("IsOwner")) %>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="MemberSince" HeaderText="Membership Date" SortExpression="MemberSince" DataFormatString="{0:dddd, MMMM dd, yyyy @ hh:mm:ss tt}" />
                <asp:TemplateField HeaderText="Terminate Membership" SortExpression="IsOwner">
                    <ItemTemplate>
                        <asp:Button ID="buttonCancelMembership" Enabled='<%# !((bool)Eval("IsOwner")) %>' runat="server" CausesValidation="False" OnClick="buttonCancelMembership_Click" CommandArgument='<%# Eval("GroupName") %>' Text="Cancel Membership" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings Mode="NextPrevious" NextPageImageUrl="~/Application/Images/Forward.png" PreviousPageImageUrl="~/Application/Images/Back.png" />
        </asp:GridView>
        <asp:SqlDataSource ID="sqldatasourceGroupAdmin" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="got_GetGroupAdminList" SelectCommandType="StoredProcedure" EnableViewState="False">
            <SelectParameters>
                <asp:SessionParameter Name="UserId" SessionField="UserId" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</div>


</td></tr>
<tr><td>


<div class="divModule">
    <div class="divModuleHeader">
        <img src="Images/GroupAdministration.jpg" />
    </div>
    <div class="divModuleContent">
        <table>
            <tr>
                <td>        
                    <asp:DropDownList ID="dropdownlistGroupAdmin" runat="server" ValidationGroup="GroupAdministration">
                        <asp:ListItem Selected="True" Value="Join">Join Existing Group</asp:ListItem>
                        <asp:ListItem Value="Cancel">Cancel Group Membership</asp:ListItem>
                        <asp:ListItem Value="Create">Create New Group</asp:ListItem>
                        <asp:ListItem Value="Delete">Delete Owned Group</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>Group Name</td>
                <td><asp:TextBox ID="textboxGroupAdminGroupname" runat="server" ValidationGroup="GroupAdministration"></asp:TextBox><asp:RequiredFieldValidator ID="requiredfieldvalidatorGroupAdminGroupname" runat="server" ErrorMessage="*" SetFocusOnError="True" ControlToValidate="textboxGroupAdminGroupname" ValidationGroup="GroupAdministration"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td></td>
                <td>Group Password</td>
                <td><asp:TextBox ID="textboxGroupAdminPassword" runat="server" ValidationGroup="GroupAdministration"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td><asp:Button ID="buttonGroupAdmin" runat="server" Text="Submit Request" OnClick="buttonGroupAdmin_Click" ValidationGroup="GroupAdministration" /></td>
            </tr>
            <tr>
                <td colspan="3"><asp:Label ID="labelGroupAdminStatus" runat="server" Text="" Visible="false" SkinID="StatusCode"></asp:Label></td>
            </tr>
        </table>
    </div>
</div>


</td><td>


<div class="divModule">
    <div class="divModuleHeader">
        <img src="Images/ChangeGroupColor.jpg" />
    </div>
    <div class="divModuleContent">
        <table>
            <tr>
                <td>Group Name</td>
                <td>
                    <asp:DropDownList ID="dropdownlistGroupColor" runat="server" ValidationGroup="GroupColor" DataSourceID="sqldatasourceGroupColor" DataTextField="GroupName" DataValueField="GroupId"></asp:DropDownList>
                    <asp:SqlDataSource ID="sqldatasourceGroupColor" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="got_GetOwnedGroupNames" SelectCommandType="StoredProcedure" EnableViewState="False">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserId" SessionField="UserId" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>Group Color</td>
                <td><asp:TextBox ID="textboxGroupColor" Text="#cccccc" runat="server" ValidationGroup="GroupColor"></asp:TextBox><asp:RequiredFieldValidator ID="requiredfieldvalidatorGroupColor" runat="server" ErrorMessage="*" SetFocusOnError="True" ControlToValidate="textboxGroupColor" ValidationGroup="GroupColor" Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="regularexpressionvalidatorGroupColor" runat="server" ErrorMessage="*" ValidationGroup="GroupColor" ControlToValidate="textboxGroupColor" SetFocusOnError="True" ValidationExpression="#[a-fA-F0-9]{6}" Display="Dynamic"></asp:RegularExpressionValidator></td>
                <td><asp:Button ID="buttonGroupColor" runat="server" Text="Change Group Color" OnClick="buttonGroupColor_Click" ValidationGroup="GroupColor" /></td>
            </tr>
        </table>
    </div>
</div>


</td></tr>
</table>

</asp:Content>
