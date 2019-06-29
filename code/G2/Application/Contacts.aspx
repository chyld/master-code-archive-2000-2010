<%@ Page Language="C#" MasterPageFile="~/Application/MasterPage.master" AutoEventWireup="true" CodeFile="Contacts.aspx.cs" Inherits="Contacts" Title="Get Organized Team! Contacts" Theme="Default" %>
<asp:Content ID="contentContacts" ContentPlaceHolderID="contentplaceholderMasterPage" Runat="Server">

<table>
<tr><td>


<div class="divModule">
    <div class="divModuleHeader">
        <img src="Images/GroupDirectoryTree.jpg" />
    </div>
    <div class="divModuleContent">
        <asp:TreeView ID="treeviewContacts" runat="server" />
    </div>
</div>


</td><td>


<div class="divModule">
    <div class="divModuleHeader">
        <img src="Images/UserDetailView.jpg" />
    </div>
    <div class="divModuleContent">
        <asp:FormView ID="formviewContacts" runat="server" DataSourceID="sqldatasourceContacts">
            <ItemTemplate>
                <table>
                    <tr>
                        <td colspan="8" style="background-color: #<%# Eval("UserColor") %>;"><img src="Images/TransparentPixel.gif" width="1" height="10" /></td>
                    </tr>
                    <tr>
                        <td class="userdetailLabel">Username:</td>
                        <td class="userdetailData"><%# Eval("UserName") %></td>
                        <td class="userdetailLabel"># Owned:</td>
                        <td class="userdetailData"><%# Eval("OwnerCount") %></td>
                        <td class="userdetailLabel"># Memberships:</td>
                        <td class="userdetailData"><%# Eval("MembershipCount") %></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="8" style="height:3px; padding:0px; border-top: 1px dashed #ff9b04;"><img src="Images/TransparentPixel.gif" width="1" height="1" /></td>
                    </tr>
                    <tr>
                        <td class="userdetailLabel">Full Name:</td>
                        <td colspan="7" class="userdetailData"><%# PrintEval("First") + " " + PrintEval("Middle") + " " + PrintEval("Last") %></td>
                    </tr>
                    <tr>
                        <td class="userdetailLabel">Title:</td>
                        <td colspan="7" class="userdetailData"><%# PrintEval("Title") %></td>
                    </tr>
                    <tr>
                        <td class="userdetailLabel">Company:</td>
                        <td colspan="7" class="userdetailData"><%# PrintEval("Company") %></td>
                    </tr>
                    <tr>
                        <td colspan="8" style="height:3px; padding:0px; border-top: 1px dashed #ff9b04;"><img src="Images/TransparentPixel.gif" width="1" height="1" /></td>
                    </tr>
                    <tr>
                        <td colspan="8" align="center">
                            <table style="text-align:center;">
                                <tr>
                                    <td class="userdetailLabel">Work Number</td>
                                    <td class="userdetailLabel">Cell Number</td>
                                    <td class="userdetailLabel">Home Number</td>
                                    <td class="userdetailLabel">Fax Number</td>
                                </tr>
                                <tr>
                                    <td class="userdetailData"><%# PrintEval("WorkNumber") %></td>
                                    <td class="userdetailData"><%# PrintEval("CellNumber") %></td>
                                    <td class="userdetailData"><%# PrintEval("HomeNumber") %></td>
                                    <td class="userdetailData"><%# PrintEval("FaxNumber") %></td>
                                </tr>
                                <tr><td colspan="4">&nbsp;</td></tr>
                                <tr>
                                    <td class="userdetailLabel">Work Email</td>
                                    <td class="userdetailLabel">Home Email</td>
                                    <td class="userdetailLabel"></td>
                                    <td class="userdetailLabel">Website</td>
                                </tr>
                                <tr>
                                    <td class="userdetailData"><%# PrintEval("WorkEmail") %></td>
                                    <td class="userdetailData"><%# PrintEval("HomeEmail") %></td>
                                    <td class="userdetailData"></td>
                                    <td class="userdetailData"><%# PrintEval("Website") %></td>
                                </tr>
                                <tr><td colspan="4">&nbsp;</td></tr>
                                <tr>
                                    <td class="userdetailLabel">MSN Messenger</td>
                                    <td class="userdetailLabel">AOL Screen Name</td>
                                    <td class="userdetailLabel">Yahoo IM ID</td>
                                    <td class="userdetailLabel">Skype VoIP</td>
                                </tr>
                                <tr>
                                    <td class="userdetailData"><%# PrintEval("MSN") %></td>
                                    <td class="userdetailData"><%# PrintEval("AOL") %></td>
                                    <td class="userdetailData"><%# PrintEval("Yahoo") %></td>
                                    <td class="userdetailData"><%# PrintEval("Skype") %></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8" style="height:3px; padding:0px; border-top: 1px dashed #ff9b04;"><img src="Images/TransparentPixel.gif" width="1" height="1" /></td>
                    </tr>
                    <tr>
                        <td colspan="2" class="userdetailLabel">Work Address:</td>
                        <td colspan="6" class="userdetailData"><%# PrintEval("WorkAddress") %></td>
                    </tr>
                    <tr>
                        <td colspan="2" class="userdetailLabel">Home Address:</td>
                        <td colspan="6" class="userdetailData"><%# PrintEval("HomeAddress") %></td>
                    </tr>
                    <tr>
                        <td colspan="8" style="height:3px; padding:0px; border-top: 1px dashed #ff9b04;"><img src="Images/TransparentPixel.gif" width="1" height="1" /></td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
        <asp:SqlDataSource ID="sqldatasourceContacts" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="got_GetDetailedUserData" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter Name="UserName" SessionField="UserName" Type="String" />
                <asp:ControlParameter ControlID="treeviewContacts" Name="SelectedUser" PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</div>


</td></tr>
</table>

</asp:Content>
