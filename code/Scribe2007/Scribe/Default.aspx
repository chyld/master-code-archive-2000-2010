<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Chyld.Scribe.Scribe.DefaultPage" Theme="Scribe" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxTK" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>:: Scribe ::</title>
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="cache-control" content="no-cache" /> 
    <meta http-equiv="expires" content="0" />
    <meta name="robots" content="noindex,nofollow" />
</head>
<body>
    <form id="formScribe" runat="server">
    <ajaxTK:ToolkitScriptManager ID="toolkitscriptmanagerScribe" runat="server" />
    <div id="master">
        <div id="header">
            scribe v1.0
        </div>
        <div id="content">
            <div id="in">
                <!-- ==================================================================================== -->
                <ajaxTK:TabContainer ID="tabcontainerScribeInput" runat="server" ActiveTabIndex="0">
                    <ajaxTK:TabPanel ID="tabpanelData" runat="server" HeaderText="Scribe Data">
                        <ContentTemplate>
                            <asp:TextBox ID="textboxNoteData" runat="server" TextMode="MultiLine" CssClass="notedata" />
                        </ContentTemplate>
                    </ajaxTK:TabPanel>
                    <ajaxTK:TabPanel ID="tabpanelTags" runat="server" HeaderText="Scribe Tags">
                        <ContentTemplate>
                            <asp:TextBox ID="textboxNoteTags" runat="server" CssClass="notetags" MaxLength="50" />
                        </ContentTemplate>
                    </ajaxTK:TabPanel>
                    <ajaxTK:TabPanel ID="tabpanelFiles" runat="server" HeaderText="Scribe Files">
                        <ContentTemplate>
                            <asp:FileUpload ID="fileupload001" runat="server" CssClass="notefiles" />
                            <asp:FileUpload ID="fileupload002" runat="server" CssClass="notefiles" />
                            <asp:FileUpload ID="fileupload003" runat="server" CssClass="notefiles" />
                            <asp:FileUpload ID="fileupload004" runat="server" CssClass="notefiles" />
                            <asp:FileUpload ID="fileupload005" runat="server" CssClass="notefiles" />
                            <asp:FileUpload ID="fileupload006" runat="server" CssClass="notefiles" />
                            <asp:FileUpload ID="fileupload007" runat="server" CssClass="notefiles" />
                        </ContentTemplate>
                    </ajaxTK:TabPanel>
                </ajaxTK:TabContainer>
                <!-- ==================================================================================== -->
                <asp:Button ID="buttonSubmit" runat="server" Text="Save Note!" OnClick="buttonSubmit_Click" />
                <asp:Label ID="labelApplicationStatus" runat="server" Text="" />
            </div>
            <div id="out">
                <asp:GridView ID="gridviewNotes" runat="server" AutoGenerateColumns="False" OnRowDataBound="gridviewNotes_RowDataBoundEvent" GridLines="None">
                    <Columns>
                        <asp:TemplateField></asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div id="footer">
            (c) 2007 chyld.medford
        </div>
    </div>
    </form>
</body>
</html>
