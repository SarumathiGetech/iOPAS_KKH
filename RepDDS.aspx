﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RepDDS.aspx.cs" Inherits="RepDDS" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
        <td>
        <asp:HyperLink ID="hyp" runat="server" NavigateUrl="~/Home.aspx" Text="Home" ></asp:HyperLink>
        </td>
        </tr>
        <tr>
      <%--  <td>           
            <rsweb:ReportViewer ID="Repdds" runat="server" ProcessingMode="Remote" Width="100%">
           <%-- <ServerReport ReportPath="/OPAS Report/DDSReport" ReportServerUrl="http://adgetech/ReportServer"/>
            </rsweb:ReportViewer>
        </td>--%>
        </tr>
        </table>      
    </div>
    </form>
</body>
</html>
