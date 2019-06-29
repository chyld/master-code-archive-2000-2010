<%@ Page Language="C#" MasterPageFile="~/Application/MasterPage.master" AutoEventWireup="true" CodeFile="Messaging.aspx.cs" Inherits="Messaging" Title="Get Organized Team! Message Center" Theme="Default" %>
<asp:Content ID="contentMessaging" ContentPlaceHolderID="contentplaceholderMasterPage" Runat="Server">

<table>
<tr><td>


<div class="divModule">
    <div class="divModuleHeader">
        <img src="Images/SendUserMessages.jpg" />
    </div>
    <div class="divModuleContent">
        <table>
            <tr>
                <td>Send Message to User:</td>
                <td style="width:200px;"></td>
                <td></td>
            </tr>
            <tr>
                <td><asp:DropDownList ID="dropdownlistMessaging" CssClass="fullWidth" runat="server" ValidationGroup="SendMessage" DataSourceID="sqldatasourceMessaging" DataTextField="UserName" DataValueField="UserName"></asp:DropDownList></td>
                <td><asp:TextBox ID="textboxMessaging" CssClass="fullWidth" runat="server" Rows="10" TextMode="MultiLine" ValidationGroup="SendMessage" EnableViewState="False"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="requiredfieldvalidatorMessaging" runat="server" ErrorMessage="*" ControlToValidate="textboxMessaging" ValidationGroup="SendMessage"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td><asp:SqlDataSource ID="sqldatasourceMessaging" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" EnableViewState="False" SelectCommand="got_GetAssociatedUsernames" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="UserId" SessionField="UserId" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource></td>
                <td><asp:Button ID="buttonMessaging" runat="server" Text="Send Message" ValidationGroup="SendMessage" OnClick="buttonMessaging_Click" /></td>
                <td></td>
            </tr>
        </table>
    </div>
</div>


</td><td>


<div class="divModule">
    <div class="divModuleHeader">
        <img src="Images/ReceivedMessagesDirectory.jpg" />
    </div>
    <div class="divModuleContent">
        <table>
            <tr>
                <td>
                    <asp:TreeView ID="treeviewReceivedMessage" runat="server" SkinID="ReceiveMail" />
                </td>
                <td>
                    <asp:FormView ID="formviewReceivedMessage" runat="server" DataSourceID="sqldatasourceReceivedMessage" DataKeyNames="MessageId">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td class="userdetailLabel">From:</td>
                                    <td class="userdetailData"><%# Eval("UserName") %></td>
                                </tr>
                                <tr>
                                    <td class="userdetailLabel">To:</td>
                                    <td class="userdetailData"><%# Session["UserName"].ToString() %></td>
                                </tr>
                                <tr>
                                    <td class="userdetailLabel">Date:</td>
                                    <td class="userdetailData"><%# Eval("MessageDate") %></td>
                                </tr>
                                <tr>
                                    <td class="userdetailLabel">Body:</td>
                                    <td class="userdetailData" style="width:300px;"><asp:TextBox ID="textboxViewEmail" CssClass="fullWidth" runat="server" Text='<%# Eval("MessageText") %>' ReadOnly="True" Rows="20" TextMode="MultiLine" ForeColor="#235b9c"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td><asp:Button ID="buttonDeleteMessage" runat="server" Text="Delete Message" CommandName="Delete" /></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:SqlDataSource ID="sqldatasourceReceivedMessage" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="got_GetMailMessage" SelectCommandType="StoredProcedure" DeleteCommand="got_DeleteMailMessage" DeleteCommandType="StoredProcedure" EnableViewState="false" OnDeleted="RefreshContent">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="treeviewReceivedMessage" Name="MessageId" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
</div>


</td></tr>
</table>

</asp:Content>
