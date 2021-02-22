<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Pharmacytime.aspx.cs" Inherits="Pharmacytime" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="scriptmana" runat="server"></asp:ScriptManager>
<script language="javascript" type="text/javascript" src="cal/popcalendar.js" ></script>
    <asp:UpdatePanel ID="upd1" runat="server">
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label2" runat="server" Text="Pharmacy Operation Time" CssClass="labelhead"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
<tr style="padding-top:10px">
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:Label ID="lblpharloc" runat="server" Text="Pharmacy Location" 
        CssClass="labelall"></asp:Label>
</td>
<td align="left" colspan="2">
<asp:DropDownList ID="ddlpharloc" runat="server" Width="227px" AutoPostBack="True" 
        onselectedindexchanged="ddlpharloc_SelectedIndexChanged" CssClass="textbox" ></asp:DropDownList><br />
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblheader1" runat ="server" Text="Days" CssClass="labelall" 
        Font-Bold ="True"></asp:Label>
</td>
<td align="left">
<asp:Label ID="lblheader2" runat ="server" Text="Start Time" CssClass="labelall " Font-Bold ="true"></asp:Label>
</td>
<td align="left">
<asp:Label ID="lblheader3" runat ="server" Text="End Time" CssClass="labelall " Font-Bold ="true"></asp:Label>
</td>
<td rowspan="8">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="lblexpdate" runat="server" Text="Holiday Date" CssClass="labelall" 
        Font-Bold="True"></asp:Label>
<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
        ErrorMessage="*" ControlToValidate="txt_Date"        
        ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" 
        Display="Dynamic" SetFocusOnError="True" ValidationGroup="time"></asp:RegularExpressionValidator>
</td>
<td>
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:textbox id="txt_Date" runat="server" Columns="6" Width="80px" CssClass="textbox" AutoCompleteType="Disabled"></asp:textbox>
</td>
<td>
<asp:image id="imgCalendar" runat="server" ImageUrl="~/cal/calendar.gif"></asp:image>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left" >
<asp:Label ID="lblpubholiday" runat="server" Text="Holidays" Width="100px" 
        Font-Bold="True"></asp:Label>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<%--<asp:Button ID="btnaddholi" runat="server" Text="Add" onclick="btnaddholi_Click" CssClass="btn" 
        Width="40px" ValidationGroup="time" />--%>
  <asp:ImageButton ID="btnaddholi" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Add.png" onclick="btnaddholi_Click" Height="20px"/>
</td>
<td align="left" style="padding-left:3px">
<%--<asp:Button ID="btnrmv" runat="server" Text="Remove" onclick="btnrmv_Click" CssClass="btn" 
        style="height: 26px" />--%>
  <asp:ImageButton ID="btnrmv" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Remove.png" onclick="btnrmv_Click" Height="20px"/>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left" >
<asp:ListBox ID="lstbox" runat="server" Width="100px" Height="158px"></asp:ListBox>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblmon" runat ="server" Text="Monday" CssClass="labelall " 
        Width="70px"></asp:Label>   
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:DropDownList ID="ddlmhst" runat="server" Width="40px" CssClass="textbox">  
    </asp:DropDownList>
</td>
<td align="left">
<asp:DropDownList ID="ddlmmst" runat="server" Width="40px" CssClass="textbox">
</asp:DropDownList>
</td>
</tr>
</table>
</td>

<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:DropDownList ID="ddlmhet" runat="server" Width="40px" CssClass="textbox">
</asp:DropDownList>
</td>
<td align="left">
<asp:DropDownList ID="ddlmmet" runat="server" Width="40px" CssClass="textbox"> 
</asp:DropDownList>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lbltues" runat ="server" Text="Tuesday" CssClass="labelall " 
        Width="70px"></asp:Label>  
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:DropDownList ID="ddltuhst" runat="server" Width="40px" CssClass="textbox">
</asp:DropDownList>
</td>
<td align="left">
<asp:DropDownList ID="ddltumst" runat="server" Width="40px" CssClass="textbox">  
</asp:DropDownList>
</td>
</tr>
</table>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:DropDownList ID="ddltuhet" runat="server" Width="40px" CssClass="textbox">
</asp:DropDownList>
</td>
<td align="left">
<asp:DropDownList ID="ddltumet" runat="server" Width="40px" CssClass="textbox">
</asp:DropDownList>
</td>
</tr>
</table>   
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblwed" runat ="server" Text="Wednesday" CssClass="labelall " 
        Width="70px"></asp:Label> 
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:DropDownList ID="ddlwhst" runat="server" Width="40px" CssClass="textbox">
</asp:DropDownList>
</td>
<td align="left">
<asp:DropDownList ID="ddlwmst" runat="server" Width="40px" CssClass="textbox"> 
</asp:DropDownList>
</td>
</tr>
</table>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:DropDownList ID="ddlwhet" runat="server" Width="40px" CssClass="textbox">
</asp:DropDownList>
</td>
<td align="left">
<asp:DropDownList ID="ddlwmet" runat="server" Width="40px" CssClass="textbox">  
</asp:DropDownList>
</td>
</tr>
</table>  
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblthu" runat ="server" Text="Thursday" CssClass="labelall " 
        Width="70px"></asp:Label>  
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:DropDownList ID="ddlthst" runat="server" Width="40px" CssClass="textbox">
</asp:DropDownList>
</td>
<td align="left">
<asp:DropDownList ID="ddlthmst" runat="server" Width="40px" CssClass="textbox"> 
</asp:DropDownList>
</td>
</tr>
</table>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:DropDownList ID="ddlthet" runat="server" Width="40px" CssClass="textbox">
</asp:DropDownList>
</td>
<td align="left">
<asp:DropDownList ID="ddlthmet" runat="server" Width="40px" CssClass="textbox">  
</asp:DropDownList>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblfri" runat ="server" Text="Friday" CssClass="labelall " 
        Width="70px"></asp:Label>   
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:DropDownList ID="ddlfhst" runat="server" Width="40px" CssClass="textbox">   
</asp:DropDownList>
</td>
<td align="left">
<asp:DropDownList ID="ddlfmst" runat="server" Width="40px" CssClass="textbox">  
</asp:DropDownList>
</td>
</tr>
</table>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:DropDownList ID="ddlfhet" runat="server" Width="40px" CssClass="textbox">
</asp:DropDownList>
</td>
<td align="left">
<asp:DropDownList ID="ddlfmet" runat="server" Width="40px" CssClass="textbox"> 
</asp:DropDownList>
</td>
</tr>
</table>   
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblsat" runat ="server" Text="Saturday" CssClass="labelall " 
        Width="70px"></asp:Label>   
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:DropDownList ID="ddlsahst" runat="server" Width="40px" CssClass="textbox">
</asp:DropDownList>
</td>
<td align="left">
<asp:DropDownList ID="ddlsamst" runat="server" Width="40px" CssClass="textbox"> 
</asp:DropDownList>
</td>
</tr>
</table>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:DropDownList ID="ddlsahet" runat="server" Width="40px" CssClass="textbox">
</asp:DropDownList>
</td>
<td align="left">
<asp:DropDownList ID="ddlsamet" runat="server" Width="40px" CssClass="textbox"> 
</asp:DropDownList>
</td>
</tr>
</table>  
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblsun" runat ="server" Text="Sunday" CssClass="labelall " 
        Width="70px"></asp:Label>  
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:DropDownList ID="ddlsuhst" runat="server" Width="40px" CssClass="textbox">
</asp:DropDownList>
</td>
<td align="left">
<asp:DropDownList ID="ddlsumst" runat="server" Width="40px" CssClass="textbox"> 
</asp:DropDownList>
</td>
</tr>
</table>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:DropDownList ID="ddlsuhet" runat="server" Width="40px" CssClass="textbox">
</asp:DropDownList>
</td>
<td align="left">
<asp:DropDownList ID="ddlsumet" runat="server" Width="40px" CssClass="textbox">  
</asp:DropDownList>
</td>
</tr>
</table>     
</td>
</tr>
<tr>
<td colspan="3" align="right">    
<%--<asp:Button ID="btnclear" runat="server" Text="Clear" onclick="btnclear_Click" 
        CssClass="btn" UseSubmitBehavior="False" />--%>
  <asp:ImageButton ID="btnclear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnclear_Click" Height="20px"/>
<%--
<asp:Button ID="btnsave" runat="server" Text="Save" onclick="btnsave_Click" CssClass="btn" />--%>

  <asp:ImageButton ID="btnsave" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Save.png" onclick="btnsave_Click" Height="20px"/>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<asp:GridView ID="gridphartime" runat="server" AutoGenerateColumns="False" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left" RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        onselectedindexchanging="gridphartime_SelectedIndexChanging" 
        AllowPaging="True" onpageindexchanging="gridphartime_PageIndexChanging" 
        CssClass="gridcss" onsorting="gridphartime_Sorting" AllowSorting="True">  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns>
        <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
        <asp:BoundField DataField="location_Name" HeaderText="Pharmacy Location" SortExpression="location_Name" />
        <asp:BoundField DataField="Created_by" HeaderText="Created by" SortExpression="Created_by" />
        <asp:BoundField DataField="Created_Date" HeaderText="Created Date Time" SortExpression="Created_Date" />
        <asp:BoundField DataField="Updated_by" HeaderText="Updated by" SortExpression="Updated_by" />
        <asp:BoundField DataField="Updated_Dates" HeaderText="Updated Date Time" 
            SortExpression="Updated_Dates" />
    </Columns>
    <FooterStyle BackColor="#507CD1" Font-Bold="False" ForeColor="#FF8000" />
    <PagerStyle BackColor="#169116" ForeColor="White" HorizontalAlign="Right" />    
    <HeaderStyle BackColor="#169116" Font-Bold="True" ForeColor="White" 
         Wrap="True" HorizontalAlign="Center" />
    <EditRowStyle BackColor="#2461BF"  ForeColor="#CC3300" />   
    </asp:GridView> 
</td>
</tr>
<tr>
<td align="left" style="padding-left:300px">
<asp:Label ID="lblpge" runat="server" CssClass="labelall"></asp:Label>
</td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

