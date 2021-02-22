<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Batchenquiry.aspx.cs" Inherits="Batchenquiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="scriptman" runat="server"></asp:ScriptManager>
 <script language="javascript" type="text/javascript" src="cal/popcalendar.js" ></script>
 <script language="javascript" type="text/javascript">
      
    </script>
 <asp:UpdatePanel ID="updpanel" runat="server">
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
 <tr>
<td >
<asp:Label ID="lblhead" runat="server" Text="Batch Order Enquiry" 
        CssClass="labelhead" ></asp:Label>
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
<asp:Label ID="lblphar" runat="server" Text="Pharmacy Location" CssClass="labelall"></asp:Label>
</td>
<td align="left">
<asp:DropDownList ID="ddlphar" runat="server" Width="220px" AutoPostBack="True" 
        onselectedindexchanged="ddlphar_SelectedIndexChanged" CssClass="textbox"></asp:DropDownList>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="Label3" runat="server" Text="DDS Name" CssClass="labelall"></asp:Label>
</td>
<td align="left">
<asp:DropDownList ID="ddlddsname" runat="server" Width="220px" CssClass="textbox"></asp:DropDownList>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="Label4" runat="server" Text="Order Ref No" CssClass="labelall"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtrefno" runat="server" Width="214px" CssClass="textbox" AutoCompleteType="Disabled" ></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="Label5" runat="server" Text="Item Code" CssClass="labelall" 
        Width="140px"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtitemcode" runat="server" Width="214px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="Label6" runat="server" Text="Item Name" CssClass="labelall"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtitemname" runat="server" Width="343px" CssClass="textbox" AutoCompleteType="Disabled" ></asp:TextBox>
</td>
</tr>
<tr>
    <td align="left">
        <asp:Label ID="lbldatefrom" runat="server" CssClass="labelall" 
            Text="Scheduled Date From" Width="120px"></asp:Label>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
        ErrorMessage="*" ControlToValidate="txt_Date"        
        ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" 
        SetFocusOnError="True" Display="Dynamic" ValidationGroup="batch"></asp:RegularExpressionValidator>
    </td>
    <td align="left">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                    <asp:TextBox ID="txt_Date" runat="server" Columns="6" Width="115px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:Image ID="imgCalendar" runat="server" ImageUrl="~/cal/calendar.gif" />
                </td>
                <td align="left" style="padding-left:5px; width:30px">
                    <asp:Label ID="Label1" runat="server" CssClass="labelall" Text="To" 
                        Width="15px"></asp:Label>  
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
        ErrorMessage="*" ControlToValidate="txtdate"        
        ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" 
        SetFocusOnError="True" Display="Dynamic" ValidationGroup="batch"></asp:RegularExpressionValidator>                          
                </td>
                <td align="left">
                    <asp:TextBox ID="txtdate" runat="server" Columns="6" Width="120px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>                    
                </td>
                <td align="left">
                    <asp:Image ID="img" runat="server" ImageUrl="~/cal/calendar.gif" />
                </td>
            </tr>
        </table>
    </td>
</tr>

<tr>
<td align="left">
<asp:Label ID="Label2" runat="server" CssClass="labelall" 
        Text="Status"></asp:Label>
</td>
<td>
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:DropDownList ID="ddlstatus" runat="server" Width="230px" 
        onselectedindexchanged="ddlstatus_SelectedIndexChanged" AutoCompleteType="Disabled">
    <asp:ListItem>All</asp:ListItem>
    <asp:ListItem>Pending</asp:ListItem>
    <asp:ListItem>Cancelled</asp:ListItem>
    <asp:ListItem>System Aborted</asp:ListItem>
    <asp:ListItem>Completed</asp:ListItem>
</asp:DropDownList>
</td>
<td align="left" style="padding-left:2px">
<%--<asp:Button ID="btnclear" runat="server" Text="Clear" Width="60px" CssClass="btn" 
        onclick="btnclear_Click" />--%>
  <asp:ImageButton ID="btnclear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnclear_Click" Height="20px"  />
</td>
<td align="left" style="padding-left:2px">
<%--<asp:Button ID="btnsearch" runat="server" Text="Search" onclick="btnsearch_Click" CssClass="btn" 
        Width="60px" ValidationGroup="batch" />--%>
  <asp:ImageButton ID="btnsearch" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Search.png" onclick="btnsearch_Click" Height="20px"  />
</td>
</tr>
</table>

</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left" style="padding-top:5px">
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:GridView ID="Enquirygrd" runat="server" AutoGenerateColumns="False" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        AllowPaging="True" onpageindexchanging="Enquirygrd_PageIndexChanging" 
        CssClass="gridcss" onsorting="Enquirygrd_Sorting" AllowSorting="True" >  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns>
        <asp:BoundField DataField="OrderRef_No" HeaderText="Order Ref No" SortExpression="OrderRef_No" />
        <asp:BoundField DataField="DDSName" HeaderText="DDS Name" SortExpression="DDSName" />
        <asp:BoundField DataField="Item_Code" HeaderText="Item Code" SortExpression="Item_Code" />
        <asp:BoundField DataField="Item_Name" HeaderText="Item Name" SortExpression="Item_Name" />
        <asp:BoundField DataField="Quantity_Perbag" HeaderText="Quantity Per Bag" SortExpression="Quantity_Perbag" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="PackType" HeaderText="Pack Type" SortExpression="PackType" />
        <asp:BoundField DataField="Pack_Size" HeaderText="Pack Size" SortExpression="Pack_Size" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="UOM" HeaderText="UOM" SortExpression="UOM" />
        <asp:BoundField DataField="NoofBags" HeaderText="No of Bags" SortExpression="NoofBags" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Schedule_Date" HeaderText="Schedule Date" SortExpression="Schedule_Date" />
        <asp:BoundField DataField="Schedule_Time" HeaderText="Schedule Time" SortExpression="Schedule_Time" />
        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
        <asp:BoundField DataField="Aborted_Reason" HeaderText="Aborted Reason" SortExpression="Aborted_Reason" />
        <asp:BoundField DataField="Status_Datetime" HeaderText="Status Date Time" SortExpression="Status_Datetime" />
        <asp:BoundField DataField="Updated_by" HeaderText="Updated by" SortExpression="Updated_by" />
        <asp:BoundField DataField="Updateddate" HeaderText="Updated Date Time" SortExpression="Updateddate" />
    </Columns>
     <FooterStyle BackColor="#507CD1" Font-Bold="False" ForeColor="#FF8000" />
    <PagerStyle BackColor="#169116" ForeColor="White" HorizontalAlign="Right" />    
    <HeaderStyle BackColor="#169116" Font-Bold="True" ForeColor="White" 
         Wrap="True" HorizontalAlign="Center" />
    <EditRowStyle BackColor="#2461BF" ForeColor="#CC3300" />  
    </asp:GridView> 
</td>
</tr>
<tr>
<td align="left" style="padding-left:300px">
<asp:Label ID="lblpge" runat="server" CssClass="labelall"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
</table>
</td>
</tr>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>


