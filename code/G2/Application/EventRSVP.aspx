<%@ Page Language="C#" MasterPageFile="~/Application/MasterPage.master" AutoEventWireup="true" CodeFile="EventRSVP.aspx.cs" Inherits="EventRSVP" Title="Get Organized Team! Event RSVP" Theme="Default" %>
<asp:Content ID="contentEventRSVP" ContentPlaceHolderID="contentplaceholderMasterPage" Runat="Server">

<table>
<tr><td>


<div class="divModule">
    <div class="divModuleHeader">
        <img src="Images/GroupEventConfirmation.jpg" />
    </div>
    <div class="divModuleContent">
    
        <asp:FormView ID="formvieweventRSVP" runat="server" DataSourceID="sqldatasourceEventRSVP">
            <ItemTemplate>
                <table>
                    <tr>
                        <td class="userdetailLabel">Event Name</td>
                        <td class="userdetailData"><%# Eval("EventName") %></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="userdetailLabel">Event Location</td>
                        <td class="userdetailData"><%# Eval("EventLocation") %></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="userdetailLabel">Event Owner</td>
                        <td class="userdetailData"><%# Eval("UserName") %></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="userdetailLabel">Group Name</td>
                        <td class="userdetailData"><%# Eval("GroupName") %></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="userdetailLabel">Start Date</td>
                        <td class="userdetailData"><%# Eval("EventStartDate") %></td>
                        <td class="userdetailLabel">End Date</td>
                        <td class="userdetailData"><%# Eval("EventEndDate") %></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><asp:Button ID="buttonDecline" runat="server" Text="Decline Invitation" OnClick="DeclineInvitation" /></td>
                        <td></td>
                        <td><asp:Button ID="buttonAccept" runat="server" Text="Accept Invitation" OnClick="AcceptInvitation" /></td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
    
    
        <asp:SqlDataSource ID="sqldatasourceEventRSVP" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="got_GetEventInformation" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter Name="UserId" SessionField="UserId" Type="String" />
                <asp:QueryStringParameter Name="EventId" QueryStringField="id" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</div>


</td></tr>
</table>

</asp:Content>
