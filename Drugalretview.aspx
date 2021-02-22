<%@ Page Language="C#" MasterPageFile="~/Site.master"  AutoEventWireup="true" CodeFile="Drugalretview.aspx.cs" Inherits="Drugalretview" %>
<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">    
    
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="scrip1" runat="server" ></asp:ScriptManager>
<asp:UpdatePanel ID="updatdrug" runat="server" >
<ContentTemplate>

<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label2" runat="server" Text="Low Stock Alert Details" CssClass="labelhead"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:Button ID="btnback" runat="server" Text="Return To Previous Screen" 
        CssClass="btn" onclick="btnback_Click" Width="170px" BackColor="#169116" 
        ForeColor="White" />
</td>
<td align="left">
<asp:Button ID="btnprint" runat="server" Text="Print" onclick="btnprint_Click" 
        CssClass="btn" BackColor="#169116" ForeColor="White" />
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
 <asp:GridView ID="griddrugalertdetails" runat="server" AutoGenerateColumns="False" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True"  
        CssClass="gridcss" 
        onsorting="griddrugalertdetails_Sorting" 
        onpageindexchanging="griddrugalertdetails_PageIndexChanging" 
        AllowPaging="True" PageSize="30" AllowSorting="True">  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns>
        <asp:BoundField DataField="Item_Code" HeaderText="Item Code" 
            SortExpression="Item_Code" >
        <ItemStyle Width="90px" />
        </asp:BoundField>
        <asp:BoundField DataField="Item_Name" HeaderText="Item Name" SortExpression="Item_Name" />
        <asp:BoundField DataField="DDS_Name" HeaderText="DDS / BDS Name" SortExpression="DDS_Name" />
        <asp:BoundField DataField="Cell_Id" HeaderText="Cell No" SortExpression="Cell_Id" />
        <asp:BoundField DataField="Aval_Quantity" 
        HeaderText="Cartridge Qty" SortExpression="Aval_Quantity" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Max_Cartridge_Qty" HeaderText="Cartridge Max Qty" SortExpression="Max_Cartridge_Qty" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="totqty" 
        HeaderText="DDS/BDS Qty" SortExpression="totqty" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="PackType" HeaderText="Pack Type" SortExpression="PackType" />
        <asp:BoundField DataField="Pack_Size" HeaderText="Pack Size" SortExpression="Pack_Size" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="UOM" HeaderText="UOM" SortExpression="UOM" />
    </Columns>
   <FooterStyle BackColor="#507CD1" Font-Bold="False" ForeColor="#FF8000" />
    <PagerStyle BackColor="#169116" ForeColor="White" HorizontalAlign="Right" />  
    <HeaderStyle BackColor="#169116" Font-Bold="True" ForeColor="White" 
        Font-Size="Small" Wrap="True" HorizontalAlign="Center" />
    <EditRowStyle BackColor="#2461BF" Font-Size="Small" ForeColor="#CC3300" />
    </asp:GridView>  
</td>
</tr>
<tr>
<td align="center">
<asp:Label ID="lblpge" runat="server" CssClass="labelall" Text="No Record Found" 
        Font-Bold="True"></asp:Label>
</td>
</tr>

</table>
</td>
</tr>
</table>

</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>