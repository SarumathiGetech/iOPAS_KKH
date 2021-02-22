<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="BottleUnloading.aspx.cs" Inherits="BottleUnloading" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>
       <script language="javascript" type="text/javascript">

            function doClick(e) {
               var key;

               if (window.event)
                   key = window.event.keyCode;     //IE
               else
                   key = e.which;     //firefox

               if (key == 13) {
                   document.getElementById('<%=btnsave.ClientID%>').click();
                   event.keyCode = 0
               }
           }

    </script>
<asp:UpdatePanel ID="updpanel" runat="server">
<ContentTemplate>

<asp:Panel ID="pp" runat="server">
<table width="100%" border="0" cellpadding="0" cellspacing="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label2" runat="server" Text="Bottle Unloading" CssClass="labelhead"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
<tr style="padding-top:10px">
<td align="left">
<table  border="0" cellpadding="0" cellspacing="0">

<tr>
<td align="left" width="115px" >
<asp:Label ID="lblcartno" runat="server" Text="Cartridge No" CssClass="labelall" 
        Width="77px"></asp:Label>
   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
        ControlToValidate="ddlcartno" InitialValue="-Select-" ErrorMessage="*" ValidationGroup="enter"></asp:RequiredFieldValidator>--%>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
        ControlToValidate="ddlcartno" InitialValue="-Select-" ErrorMessage="*" ValidationGroup="unload"></asp:RequiredFieldValidator>

</td>
<td align="left">
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left">
<%--<asp:TextBox ID="txtcartno" runat="server" Width="200px" MaxLength="10" 
        CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>--%>
<asp:DropDownList ID="ddlcartno" runat="server" Width="355px" CssClass="textbox" 
        AutoPostBack="True" 
        onselectedindexchanged="ddlcartno_SelectedIndexChanged" ></asp:DropDownList>

</td>
<%--<td align="left">
  <asp:ImageButton ID="Btngo" runat="server" CssClass="btn" ValidationGroup="enter"
            ImageUrl="~/ButtonImages/Enter24.png" 
        Height="22px" onclick="Btngo_Click"  />
</td>--%>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left" width="125px">
<asp:Label ID="Label3" runat="server" Text="BDS Name" CssClass="labelall" 
        Width="122px"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtbdsname" runat="server" Width="350px" ReadOnly="True" 
        BackColor="#CCCCCC" CssClass="textbox" AutoCompleteType="Disabled" 
        TabIndex="1"></asp:TextBox>        
</td>
</tr>
<tr>
<td align="left" width="125px">
<asp:Label ID="Llblitemcode" runat="server" Text="Item Code" CssClass="labelall" 
        Width="122px"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtitemcode" runat="server" Width="350px" ReadOnly="True" 
        BackColor="#CCCCCC" CssClass="textbox" AutoCompleteType="Disabled" 
        TabIndex="1"></asp:TextBox>        
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lbldrg" runat="server" Text="Item Name" CssClass="labelall" 
        Width="122px"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtitemname" runat="server" Width="350px" ReadOnly="True" 
        BackColor="#CCCCCC" CssClass="textbox" AutoCompleteType="Disabled" 
        TabIndex="1"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="Label1" runat="server" Text="Brand Name" CssClass="labelall" 
        Width="122px"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtbrandname" runat="server" Width="350px" ReadOnly="True" 
        BackColor="#CCCCCC" CssClass="textbox" AutoCompleteType="Disabled" 
        TabIndex="1"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left" >
<asp:Label ID="lblbalance" runat="server" Text="System Balance" CssClass="labelall"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
        ControlToValidate="txtsysbalance" ErrorMessage="*" 
        ValidationGroup="unload"></asp:RequiredFieldValidator>
</td>
<td align="left" style="height: 23px">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtsysbalance" runat="server" Width="50px" 
        BackColor="#CCCCCC" ReadOnly="True" CssClass="textbox" 
        AutoCompleteType="Disabled" TabIndex="1"></asp:TextBox>
</td>
<td align="left" style="padding-left:10px">
<asp:Label ID="lbluom2" runat="server" CssClass="labelall"></asp:Label>
</td>
</tr>
</table> 
</td>
</tr>



<tr>
<td colspan="2" align="right">

  <asp:ImageButton ID="btnclear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" Height="19px" 
        onclick="btnclear_Click"  />
  <asp:ImageButton ID="btnsave" runat="server" CssClass="btn" ValidationGroup="unload" 
            ImageUrl="~/ButtonImages/Save.png"  Height="20px" 
        onclick="btnsave_Click"  />


</td>
</tr>
</table>
<tr>
<td>
<table align="left" width="100%">   
<tr>
<td align="left">
<asp:GridView ID="Unloadinggrid" runat="server" AutoGenerateColumns="False"  
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" AllowPaging="True" 
        BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" CssClass="gridcss" 
         AllowSorting="True" PageSize="20" 
        onpageindexchanging="Unloadinggrid_PageIndexChanging" 
        onsorting="Unloadinggrid_Sorting"> 
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns>           
      <asp:BoundField DataField="DDS_Name" HeaderText="BDS Name" SortExpression="DDS_Name" />
        <asp:BoundField DataField="Cartridge_Id" HeaderText="Cartridge No" SortExpression="Cartridge_Id" />
        <asp:BoundField DataField="Item_Code" HeaderText="Item Code" SortExpression="Item_Code" />
        <asp:BoundField DataField="Item_Name" HeaderText="Item Name" SortExpression="Item_Name" />
        <asp:BoundField DataField="BrandName" HeaderText="Brand Name" SortExpression="BrandName" />        
        <asp:BoundField DataField="PackType" HeaderText="Pack Type" SortExpression="PackType" />
        <asp:BoundField DataField="Pack_Size" HeaderText="Pack Size" SortExpression="Pack_Size" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="UOM" HeaderText="UOM" SortExpression="UOM" />    
        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />    
        <asp:BoundField DataField="UnloadedBy" HeaderText="UnLoaded by" SortExpression="UnloadedBy" />
        <asp:BoundField DataField="UnloadedDate" HeaderText="UnLoaded Date Time" SortExpression="UnloadedDate" />         
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
<td>
<asp:Label ID="lblpge" runat="server" CssClass="labelall"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>






</td>
</tr>
</table>
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>


