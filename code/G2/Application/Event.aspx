<%@ Page Language="C#" MasterPageFile="~/Application/MasterPage.master" AutoEventWireup="true" CodeFile="Event.aspx.cs" Inherits="Event" Title="Get Organized Team! Event Viewer" Theme="Default" %>
<asp:Content ID="contentEvent" ContentPlaceHolderID="contentplaceholderMasterPage" Runat="Server">

<table>
<tr><td colspan="2">

<%-- *-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+ --%>

<div class="divModule">
    <div class="divModuleHeader">
        <img src="Images/EventInformationOverview.jpg" />
    </div>
    <div class="divModuleContent">
        <asp:FormView ID="formviewEventHeader" runat="server" DataSourceID="sqldatasourceEventHeader">
            <ItemTemplate>
                <table>
                    <tr>
                        <td><img style='background-color:#<%# Eval("GroupColor") %>;' src="Images/TransparentPixel.gif" width="17" height="17" /></td>
                        <td><img style='background-color:#<%# Eval("UserColor") %>;' src="Images/TransparentPixel.gif" width="17" height="17" /></td>
                        <td class="tdEventTitle"><%# Eval("EventName") %></td>
                        <td><img style='background-color:#<%# Eval("UserColor") %>;' src="Images/TransparentPixel.gif" width="17" height="17" /></td>
                        <td><img style='background-color:#<%# Eval("UserColor") %>;' src="Images/TransparentPixel.gif" width="17" height="17" /></td>
                        <td><img style='background-color:#<%# Eval("UserColor") %>;' src="Images/TransparentPixel.gif" width="17" height="17" /></td>
                        <td><img style='background-color:#<%# Eval("GroupColor") %>;' src="Images/TransparentPixel.gif" width="17" height="17" /></td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td class="userdetailLabel">What:</td>
                        <td class="userdetailData"><%# Eval("EventName") %></td>
                        <td><asp:Button ID="buttonDeleteEvent" Enabled='<%# IsEventOwner() %>' runat="server" Text="Delete Event" OnClick="buttonDeleteEvent_Click" /></td>
                    </tr>
                    <tr>
                        <td class="userdetailLabel">Where:</td>
                        <td class="userdetailData"><%# Eval("EventLocation") %></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="userdetailLabel">When:</td>
                        <td class="userdetailData"><%# Eval("EventStartDate") %> - <%# Eval("EventEndDate") %></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="userdetailLabel">Group:</td>
                        <td class="userdetailData"><%# Eval("GroupName") %></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="userdetailLabel">Contact:</td>
                        <td class="userdetailData"><%# Eval("UserName") %></td>
                        <td></td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
        <asp:SqlDataSource ID="sqldatasourceEventHeader" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="got_GetEventHeader" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:QueryStringParameter Name="EventId" QueryStringField="id" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</div>

<%-- *-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+ --%>

</td></tr>
<tr><td>

<%-- *-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+ --%>

<div class="divModule">
    <div class="divModuleHeader">
        <img src="Images/EventRsvpList.jpg" />
    </div>
    <div class="divModuleContent">
        <asp:GridView ID="gridviewRSVPList" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="sqldatasourceRSVPList" DataKeyNames="UserId" PageSize="5">
            <Columns>
                <asp:BoundField DataField="UserName" HeaderText="Username" SortExpression="UserName" />
                <asp:BoundField DataField="RSVPDate" HeaderText="RSVP Date" SortExpression="RSVPDate" />
                <asp:TemplateField HeaderText="Status" SortExpression="Status">
                    <ItemTemplate>
                        <img src='Images/<%# GetStatusImage((int)Eval("Status")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings Mode="NextPrevious" NextPageImageUrl="~/Application/Images/Forward.png" PreviousPageImageUrl="~/Application/Images/Back.png" />
        </asp:GridView>
        <asp:SqlDataSource ID="sqldatasourceRSVPList" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="got_GetEventRSVPList" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:QueryStringParameter Name="EventId" QueryStringField="id" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</div>

<%-- *-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+ --%>

</td><td>

<%-- *-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+ --%>

<div class="divModule">
    <div class="divModuleHeader">
        <img src="Images/EventActionItems.jpg" />
    </div>
    <div class="divModuleContent">
        <asp:GridView ID="gridviewActionItems" runat="server" AllowPaging="True" AllowSorting="True" DataKeyNames="ActionItemId" DataSourceID="sqldatasourceActionItems" AutoGenerateColumns="False" PageSize="5">
            <Columns>
                <asp:TemplateField HeaderText="Action Item" SortExpression="ActionItemName">
                    <ItemTemplate>
                        <%# Eval("ActionItemName") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Assigned To" SortExpression="UserName">
                    <ItemTemplate>
                        <%# Eval("UserName") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Is Completed?" SortExpression="IsCompleted">
                    <ItemTemplate>
                        <img src='Images/<%# GetActionCompletedImage((bool)Eval("IsCompleted")) %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:CheckBox ID="checkboxIsCompleted" Checked='<%# Bind("IsCompleted") %>' runat="server" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Completion Date" SortExpression="CompletionDate">
                    <ItemTemplate>
                        <%# Eval("CompletionDate") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="buttonEditActionItem" Enabled='<%# IsActionItemOwner(Eval("ActionItemId").ToString()) %>' runat="server" CausesValidation="False" CommandName="Edit" Text="Update Status" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button ID="buttonUpdateActionItem" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />&nbsp;<asp:Button ID="buttonCancelUpdateActionItem" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="buttonDeleteActionItem" Enabled='<%# IsEventOwner() %>' runat="server" CausesValidation="False" CommandName="Delete" Text="Delete Action Item" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings Mode="NextPrevious" NextPageImageUrl="~/Application/Images/Forward.png" PreviousPageImageUrl="~/Application/Images/Back.png" />
        </asp:GridView>
        <asp:SqlDataSource ID="sqldatasourceActionItems" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="got_GetActionItems" SelectCommandType="StoredProcedure" DeleteCommand="got_DeleteActionItem" DeleteCommandType="StoredProcedure" EnableViewState="False" UpdateCommand="got_UpdateActionItem" UpdateCommandType="StoredProcedure">
            <SelectParameters>
                <asp:QueryStringParameter Name="EventId" QueryStringField="id" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="IsCompleted" Type="Boolean" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <% if(IsEventOwner())
           { %>
            <table>
                <tr>
                    <td>New Action Item:</td>
                    <td><asp:TextBox ID="textboxNewActionItem" runat="server" ValidationGroup="InsertActionItem"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="textboxNewActionItem" SetFocusOnError="True" ValidationGroup="InsertActionItem"></asp:RequiredFieldValidator></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Assigned To:</td>
                    <td><asp:DropDownList CssClass="fullWidth" ID="dropdownlistActionItemAssignedTo" runat="server" ValidationGroup="InsertActionItem" DataSourceID="sqldatasourceActionItemAssignedTo" DataTextField="UserName" DataValueField="UserId"></asp:DropDownList></td>
                    <td><asp:Button ID="buttonInsertActionItem" runat="server" Text="Add Action Item" OnClick="buttonInsertActionItem_Click" ValidationGroup="InsertActionItem" /></td>
                </tr>
            </table>
            <asp:SqlDataSource ID="sqldatasourceActionItemAssignedTo" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="got_GetUsersInGroup" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:QueryStringParameter Name="EventId" QueryStringField="id" />
                </SelectParameters>
            </asp:SqlDataSource>
        <% } %>
    </div>
</div>

<%-- *-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+ --%>

</td></tr>
<tr><td colspan="2">

<%-- *-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+ --%>

<div class="divModule">
    <div class="divModuleHeader">
        <img src="Images/EventNotes.jpg" />
    </div>
    <div class="divModuleContent">
        <table class="fullWidth">
            <tr>
                <td>
                    <asp:TextBox ID="textboxNotes" CssClass="fullWidth" runat="server" Rows="10" TextMode="MultiLine" ValidationGroup="InsertNote" EnableViewState="False"></asp:TextBox>
                    <br /><br />
                    <asp:Button ID="buttonNotes" runat="server" Text="Insert Event Note" ValidationGroup="InsertNote" OnClick="buttonNotes_Click" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="requiredfieldvalidatorNotes" runat="server" ValidationGroup="InsertNote" ControlToValidate="textboxNotes" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
                <td width="100%">
                    <asp:GridView ID="gridviewNotes" runat="server" AllowPaging="True" AllowSorting="True" DataSourceID="sqldatasourceNotes" PageSize="5" DataKeyNames="NoteId" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Submitter" SortExpression="UserName">
                                <ItemTemplate>
                                    <%# Eval("UserName") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date" SortExpression="NoteDate">
                                <ItemTemplate>
                                    <%# Eval("NoteDate") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Note" SortExpression="NoteText">
                                <ItemTemplate>
                                    <div><%# Eval("NoteText") %></div>
                                </ItemTemplate>
                                <ItemStyle Width="100%" HorizontalAlign="Left" Wrap="true" />
                                <EditItemTemplate>
                                    <asp:TextBox ID="textboxUpdateNote" TextMode="MultiLine" Rows="10" runat="server" Text='<%# Bind("NoteText") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="Button1" runat="server" Enabled='<%# IsNoteOwner(Eval("NoteId").ToString()) %>' CausesValidation="False" CommandName="Edit" Text="Edit Note" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="Button1" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />&nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="buttonDeleteNote" Enabled='<%# IsNoteOwner(Eval("NoteId").ToString()) %>' runat="server" CausesValidation="False" CommandName="Delete" Text="Delete Note" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings Mode="NextPrevious" NextPageImageUrl="~/Application/Images/Forward.png" PreviousPageImageUrl="~/Application/Images/Back.png" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="sqldatasourceNotes" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" DeleteCommand="got_DeleteEventNotes" DeleteCommandType="StoredProcedure" SelectCommand="got_GetEventNotes" SelectCommandType="StoredProcedure" UpdateCommand="got_UpdateEventNotes" UpdateCommandType="StoredProcedure" EnableViewState="false">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="EventId" QueryStringField="id" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="NoteText" Type="String" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
</div>

<%-- *-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+ --%>

</td></tr>
<tr><td colspan="2">

<%-- *-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+ --%>

<div class="divModule">
    <div class="divModuleHeader">
        <img src="Images/EventFiles.jpg" />
    </div>
    <div class="divModuleContent">
        <table class="fullWidth">
            <tr>
                <td>
                    <asp:GridView ID="gridviewEventFiles" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="sqldatasourceEventFiles" PageSize="5">
                        <Columns>
                            <asp:BoundField DataField="UserName" HeaderText="Submitter" SortExpression="UserName" />
                            <asp:BoundField DataField="FileDate" HeaderText="Date" SortExpression="FileDate" />
                            <asp:TemplateField HeaderText="Filename" SortExpression="FileName">
                                <ItemTemplate>
                                    <a class="link" target="_blank" href='FileUpload/{<%# Eval("FileId") %>}-<%# Eval("FileName") %>'><%# Eval("FileName") %></a>
                                </ItemTemplate>
                                <ItemStyle CssClass="fullWidth" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="buttonDeleteFile" OnClick="DeleteFile" CommandArgument='<%# "{" + Eval("FileId").ToString() + "}-" + Eval("FileName").ToString() %>' Enabled='<%# IsFileOwner(Eval("FileId").ToString()) %>' runat="server" CausesValidation="False" Text="Delete File" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings Mode="NextPrevious" NextPageImageUrl="~/Application/Images/Forward.png" PreviousPageImageUrl="~/Application/Images/Back.png" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="sqldatasourceEventFiles" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="got_GetEventFiles" SelectCommandType="StoredProcedure" EnableViewState="false">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="EventId" QueryStringField="id" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:FileUpload ID="fileuploadEventFile" runat="server" />
                    <br /><br />
                    <asp:Button ID="buttonEventFile" runat="server" Text="Upload File" OnClick="buttonEventFile_Click" />
                    <asp:Label ID="labelEventFile" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</div>

<%-- *-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+*-*+ --%>

</td></tr>
</table>

</asp:Content>
