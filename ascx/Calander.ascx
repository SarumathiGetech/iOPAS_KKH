<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Calander.ascx.cs" Inherits="Calander" %>
<link href ="cal/popcalendar.css" type="text/css" rel="Stylesheet" />
<script language="javascript" type="text/javascript" src="cal/popcalendar.js" ></script>
<table  id="tbl_control" cellspacing="0" cellpadding="0" border="0">
	<tr>
		<%--<td align="right"><asp:label id="lbl_Date" Font-Bold="true" runat="server"></asp:label></td>--%>
		
		<td align="center"><asp:textbox id="txt_Date" runat="server" Columns="6" Width="100px"></asp:textbox></td>
		<td><asp:image id="imgCalendar" runat="server" ImageUrl="~/cal/calendar.gif"></asp:image></td>
	</tr>
</table> 


