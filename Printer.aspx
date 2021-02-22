<%@ Page Language="C#" MasterPageFile="~/Site.master"  AutoEventWireup="true" CodeFile="Printer.aspx.cs" Inherits="Printer" %>
<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>
<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">    
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <asp:ScriptManager ID="scriptmana" runat="server"></asp:ScriptManager>
     <script language="javascript" type="text/javascript">
         function confirmProcess() {
             if (confirm('Selected records will be activated')) {
                 document.getElementById('<%=btnok.ClientID%>').click();
             }

         }
         function confirmProcess1() {
             if (confirm('Selected records will be inactivated')) {
                 document.getElementById('<%=btnok.ClientID%>').click();
             }
         }        

    </script>  
<asp:UpdatePanel ID="upd1" runat="server" >
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label1" runat="server"  Text="Printer Master" CssClass="labelhead"></asp:Label>
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
<asp:Label ID="labelpharlocad" runat="server" Text="Pharmacy Location" 
        CssClass="labelall" Width="111px"></asp:Label>
</td>
<td align="left">
<asp:DropDownList ID="ddlpharadd" runat="server" Width="229px" AutoPostBack="True" 
        onselectedindexchanged="ddlpharadd_SelectedIndexChanged" CssClass="textbox"></asp:DropDownList>
</td>
<td rowspan="4" style="vertical-align:top; padding-left:10px">
<table cellpadding="0" cellspacing="0" border="0" 
        style="border: thin ridge #C0C0C0" >
<tr>
<td align="left" style=" padding-left:20px">
<asp:Label ID="Label3" runat="server" CssClass="labelall" Text="Printer Function" 
        Width="100px" Height="32px" Font-Bold="True"></asp:Label>
</td>
<td align="center" >
<asp:Label ID="lbldl" runat="server" CssClass="labelall" Text="Select" Width="70px" 
        Height="32px" Font-Bold="True"></asp:Label>
</td>
<td align="center" >
<asp:Label ID="lblcb" runat="server" CssClass="labelall" Text="Default" 
        Width="70px" Height="32px" Font-Bold="True"></asp:Label>
</td>
</tr>
<tr>
<td align="left" style=" padding-left:20px">
<asp:Label ID="lbldefault" runat="server" CssClass="labelall" Text="Carton Box Barcode Label(CBL)" 
        Width="190px" Height="22px"></asp:Label>
</td>
<td align="left">
<asp:CheckBox ID="chkfdl" runat="server" Height="22px" />
</td>
<td align="left">
<asp:CheckBox ID="chkddl" runat="server" Height="22px" AutoPostBack="True" 
        oncheckedchanged="chkddl_CheckedChanged" />
</td>
</tr>

<tr>
<td align="left" style=" padding-left:20px">
<asp:Label ID="Label4" runat="server" CssClass="labelall" Text="Cartridge Drug Label(CL)" 
        Width="160px" Height="22px"></asp:Label>
</td>
<td align="left">
<asp:CheckBox ID="chkfcl" runat="server" Height="22px" />
</td>
<td align="left">
<asp:CheckBox ID="chkdcl" runat="server" Height="22px" AutoPostBack="True" 
        oncheckedchanged="chkdcl_CheckedChanged" />
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblprintername" runat="server" Text="Printer Name" CssClass="labelall"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtprintername" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="printer"></asp:RequiredFieldValidator>
</td>
<td align="left">
<asp:TextBox ID="txtprintername" runat="server" Width="224px" MaxLength="50" 
        CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
<%--<tr>
<td align="left">
<asp:Label ID="lblip" runat="server" Text="IP Address/Host Name" 
        CssClass="labelall"></asp:Label>    
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtipaddress" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="printer"></asp:RequiredFieldValidator>
</td>
<td align="left">
<asp:TextBox ID="txtipaddress" runat="server" Width="224px" MaxLength="30" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>--%>
<tr>
<td align="left">
<asp:Label ID="lbldesc" runat="server" Text="Description" CssClass="labelall"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtdesc" runat="server" Width="224px" TextMode="MultiLine" 
        Height="47px" MaxLength="255" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
<tr>
<td colspan="3" align="right">
<%-- <asp:Button ID="btnclear" runat="server" Text="Clear" onclick="Button1_Click" 
        Width="50px" CssClass="btn"/>--%>
   <asp:ImageButton ID="btnclear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnclear_Click" Height="20px"/>      
        &nbsp;
<%--<asp:Button ID="btnadd" runat="server" Text="Save" onclick="btnadd_Click" 
        ValidationGroup="printer"  Width="50px" CssClass="btn" />--%>       
       <asp:ImageButton ID="btnadd" runat="server" CssClass="btn" ValidationGroup="printer"
            ImageUrl="~/ButtonImages/Save.png" onclick="btnadd_Click" Height="20px"/>  
   <asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" ValidationGroup="printer"
            ImageUrl="~/ButtonImages/Update.png" Height="20px" 
        onclick="btnupdate_Click"/>  
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<%--<asp:Button ID="btnactive" runat="server" Text="Activate" 
        onclick="btnactive_Click" Width="59px" CssClass="btn" />--%>
    <asp:ImageButton ID="btnactive" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Activate.png" onclick="btnactive_Click" Height="20px"/>      
        &nbsp;
<%--<asp:Button ID="btndeactive" runat="server" Text="Inactivate" 
        onclick="btndeactive_Click" Width="66px" CssClass="btn" />--%>
 <asp:ImageButton ID="btndeactive" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Inactivate.png" onclick="btndeactive_Click" Height="20px"/>   
             
<asp:Button ID="btnok" runat="server" CausesValidation="False" OnClick="btnok_Click" Style="position: static; display: none" Text="Ok" />   
</td>
</tr>
<tr>
<td>
<asp:GridView ID="griddetail" runat="server" AutoGenerateColumns="False" DataKeyNames="PrinterID" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True"
        onselectedindexchanging="griddetail_SelectedIndexChanging" 
        AllowPaging="True" onpageindexchanging="griddetail_PageIndexChanging" 
        CssClass="gridcss" onsorting="griddetail_Sorting" AllowSorting="True" >  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />   
    <Columns>
    <asp:TemplateField>
    <HeaderTemplate>
    <asp:CheckBox ID="chkheader" runat="server" OnCheckedChanged ="chkheader_CheckedChanged" AutoPostBack="true" />
    </HeaderTemplate>
    <ItemTemplate>
    <asp:CheckBox ID="chkrow" runat="server" />
    </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
        <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
        <asp:BoundField DataField="PrinterID" Visible="false"/>       
           <%-- <ItemStyle ForeColor="White" Width="1px" BorderColor="White" />
             </asp:BoundField>--%>
        <asp:BoundField DataField="location_name" HeaderText="Pharmacy Location" SortExpression="location_name" />
        <asp:BoundField DataField="Printer_Name" HeaderText="Printer Name" SortExpression="Printer_Name" />
      <%--  <asp:BoundField DataField="IP_Hostname" HeaderText="IP /Host Name" SortExpression="IP_Hostname" />--%>
        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
        <asp:BoundField DataField="Default_Printer" HeaderText="Default" SortExpression="Default_Printer" />
        <asp:BoundField DataField="function_printer" HeaderText="Function" SortExpression="function_printer" />
        <asp:BoundField DataField="Created_by" HeaderText="Created by" SortExpression="Created_by" />
        <asp:BoundField DataField="Created_Date" HeaderText="Created Date Time" SortExpression="Created_Date" />
        <asp:BoundField DataField="Updated_by" HeaderText="Updated by" SortExpression="Updated_by" />
        <asp:BoundField DataField="Updated_Date" HeaderText="Updated Date Time" SortExpression="Updated_Date" />
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
<td style="padding-left:300px">
<asp:Label ID="lblpge" runat="server" CssClass="labelall"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
