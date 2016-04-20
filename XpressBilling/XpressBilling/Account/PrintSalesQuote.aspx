<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintSalesQuote.aspx.cs" Inherits="XpressBilling.Account.PrintSalesQuote" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <cr:crystalreportviewer id="crViewer" reportsourceid="crSource" runat="server" autodatabind="true" height="100%" width="100%" enabledatabaselogonprompt="False"></cr:crystalreportviewer>
            <cr:crystalreportsource id="crSource" runat="server"></cr:crystalreportsource>
            <span id="spanNoRecords" name="spanNoRecords" runat="server"></span>
        </div>
    </form>
</body>
</html>
