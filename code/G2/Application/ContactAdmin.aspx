<%@ Page Language="C#" MasterPageFile="~/Application/MasterPage.master" AutoEventWireup="true" CodeFile="ContactAdmin.aspx.cs" Inherits="ContactAdmin" Title="Get Organized Team! Contact Administration" Theme="Default" %>
<asp:Content ID="contentContactAdmin" ContentPlaceHolderID="contentplaceholderMasterPage" Runat="Server">

<table>
<tr><td>


<div class="divModule">
    <div class="divModuleHeader">
        <img src="Images/PersonalUserData.jpg" />
    </div>
    <div class="divModuleContent">
        <asp:DetailsView ID="detailsviewContactAdmin" runat="server" AutoGenerateRows="False" DataKeyNames="UserId" DataSourceID="sqldatasourceContactAdmin">
            <Fields>
                <asp:BoundField DataField="UserId" HeaderText="UserId" ReadOnly="True" SortExpression="UserId" Visible="False" />
                <asp:BoundField DataField="First" HeaderText="First" SortExpression="First" />
                <asp:BoundField DataField="Middle" HeaderText="Middle" SortExpression="Middle" />
                <asp:BoundField DataField="Last" HeaderText="Last" SortExpression="Last" />
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                <asp:BoundField DataField="Company" HeaderText="Company" SortExpression="Company" />
                <asp:BoundField DataField="WorkAddress" HeaderText="Work Address" SortExpression="WorkAddress" />
                <asp:BoundField DataField="HomeAddress" HeaderText="Home Address" SortExpression="HomeAddress" />
                <asp:BoundField DataField="WorkNumber" HeaderText="Work Number" SortExpression="WorkNumber" />
                <asp:BoundField DataField="CellNumber" HeaderText="Cell Number" SortExpression="CellNumber" />
                <asp:BoundField DataField="HomeNumber" HeaderText="Home Number" SortExpression="HomeNumber" />
                <asp:BoundField DataField="FaxNumber" HeaderText="Fax Number" SortExpression="FaxNumber" />
                <asp:BoundField DataField="WorkEmail" HeaderText="Work Email" SortExpression="WorkEmail" />
                <asp:BoundField DataField="HomeEmail" HeaderText="Home Email" SortExpression="HomeEmail" />
                <asp:BoundField DataField="Website" HeaderText="Website" SortExpression="Website" />
                <asp:BoundField DataField="MSN" HeaderText="MSN IM" SortExpression="MSN" />
                <asp:BoundField DataField="AOL" HeaderText="AOL IM" SortExpression="AOL" />
                <asp:BoundField DataField="Yahoo" HeaderText="Yahoo IM" SortExpression="Yahoo" />
                <asp:BoundField DataField="Skype" HeaderText="Skype ID" SortExpression="Skype" />
                <asp:TemplateField HeaderText="UserColor" SortExpression="UserColor">
                    <ItemTemplate>
                       #<asp:Label ID="labelUserColor" runat="server" Text='<%# Eval("UserColor") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                       #<asp:TextBox ID="textboxUserColor" runat="server" Text='<%# Bind("UserColor") %>' ValidationGroup="ChangeUserColor"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="requiredfieldvalidatorUserColor" runat="server" ErrorMessage="*" ControlToValidate="textboxUserColor" ValidationGroup="ChangeUserColor" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regularexpressionvalidatorUserColor" runat="server" ErrorMessage="*" ControlToValidate="textboxUserColor" ValidationGroup="ChangeUserColor" Display="Dynamic" SetFocusOnError="True" ValidationExpression="[a-fA-F0-9]{6}"></asp:RegularExpressionValidator>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" ButtonType="Button" EditText="Edit Personal Data" UpdateText="Update Personal Data" ValidationGroup="ChangeUserColor" />
            </Fields>
        </asp:DetailsView>
        <asp:SqlDataSource ID="sqldatasourceContactAdmin" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="got_GetContactAdminUserData" SelectCommandType="StoredProcedure" UpdateCommand="got_UpdateContactAdminUserData" UpdateCommandType="StoredProcedure" EnableViewState="False">
            <SelectParameters>
                <asp:SessionParameter Name="UserId" SessionField="UserId" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</div>


</td><td>


<div class="divModule">
    <div class="divModuleHeader">
        <img src="Images/SharePersonalInfo.jpg" />
    </div>
    <div class="divModuleContent">
        <table>
        <tr>
            <td>Share Field</td>
            <td>
                <asp:DropDownList ID="dropdownlistField" runat="server">
                    <asp:ListItem Selected="True">First</asp:ListItem>
                    <asp:ListItem>Middle</asp:ListItem>
                    <asp:ListItem>Last</asp:ListItem>
                    <asp:ListItem>Title</asp:ListItem>
                    <asp:ListItem>Company</asp:ListItem>
                    <asp:ListItem Value="WorkAddress">Work Address</asp:ListItem>
                    <asp:ListItem Value="HomeAddress">Home Address</asp:ListItem>
                    <asp:ListItem Value="WorkNumber">Work Number</asp:ListItem>
                    <asp:ListItem Value="CellNumber">Cell Number</asp:ListItem>
                    <asp:ListItem Value="HomeNumber">Home Number</asp:ListItem>
                    <asp:ListItem Value="FaxNumber">Fax Number</asp:ListItem>
                    <asp:ListItem Value="WorkEmail">Work Email</asp:ListItem>
                    <asp:ListItem Value="HomeEmail">Home Email</asp:ListItem>
                    <asp:ListItem>Website</asp:ListItem>
                    <asp:ListItem Value="MSN">MSN IM</asp:ListItem>
                    <asp:ListItem Value="AOL">AOL IM</asp:ListItem>
                    <asp:ListItem Value="Yahoo">Yahoo IM</asp:ListItem>
                    <asp:ListItem Value="Skype">Skype ID</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>Share Mode</td>
            <td>
                <asp:DropDownList ID="dropdownlistMode" runat="server" CssClass="fullWidth">
                    <asp:ListItem Selected="True">Global</asp:ListItem>
                    <asp:ListItem>Group</asp:ListItem>
                    <asp:ListItem>User</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>Target Name</td>
            <td>
                <asp:TextBox ID="textboxTarget" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="buttonAddPermission" runat="server" Text="Add Permission" OnClick="buttonAddPermission_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="5"></td>
        </tr>
        </table>
    </div>
</div>


<div class="divModule">
    <div class="divModuleHeader">
        <img src="Images/PermissionShareList.jpg" />
    </div>
    <div class="divModuleContent">
        <asp:GridView ID="gridviewPermissionList" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="PermissionId" DataSourceID="sqldatasourcePermissionList" PageSize="5">
            <Columns>
                <asp:BoundField DataField="PermissionId" HeaderText="PermissionId" ReadOnly="True" SortExpression="PermissionId" Visible="False" />
                <asp:BoundField DataField="ShareField" HeaderText="Share Field" SortExpression="ShareField" />
                <asp:BoundField DataField="TargetMode" HeaderText="Target Mode" SortExpression="TargetMode" />
                <asp:BoundField DataField="TargetName" HeaderText="Target Name" SortExpression="TargetName" />
                <asp:CommandField ShowDeleteButton="True" ButtonType="Button" DeleteText="Delete Permission" />
            </Columns>
            <PagerSettings Mode="NextPrevious" NextPageImageUrl="~/Application/Images/Forward.png" PreviousPageImageUrl="~/Application/Images/Back.png" />
        </asp:GridView>
        <asp:SqlDataSource ID="sqldatasourcePermissionList" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" DeleteCommand="got_DeletePermission" DeleteCommandType="StoredProcedure" SelectCommand="got_GetPermissionList" SelectCommandType="StoredProcedure" EnableViewState="false">
            <SelectParameters>
                <asp:SessionParameter Name="UserId" SessionField="UserId" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</div>


</td></tr>
</table>

</asp:Content>
