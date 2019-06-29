<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoxOfficeAnalytics.aspx.cs" Inherits="Cingulariti.ANTLIA.Presence.Portfolio.BoxOfficeAnalytics" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register src="~/Controls/Movies.ascx" tagname="Movies" tagprefix="cingulariti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
</head>
<body class="portfolio">
    <form id="formBoxOfficeAnalytics" runat="server">
    <div class="portfoliomovies">
        <ajax:ToolkitScriptManager ID="toolkitscriptmanager" runat="server" />
        <cingulariti:Movies ID="movies" runat="server" />
    </div>
    </form>
</body>
</html>
