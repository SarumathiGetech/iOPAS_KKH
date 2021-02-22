<%@ Page Language="C#" MasterPageFile="~/Site.master"  AutoEventWireup="true" CodeFile="Cartridgestatus.aspx.cs" Inherits="Cartridgestatus" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">    
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <asp:ScriptManager ID="scriptmana" runat="server"></asp:ScriptManager>
     <script language="javascript" src="JS/checkbox.js" type="text/javascript">    
     </script> 
    <script language="javascript" type="text/javascript">
        function confirmProcess() {
            if (confirm('Selected records will be enabled')) {
                document.getElementById('<%=btnok.ClientID%>').click();
            }

        }
        function confirmProcess1() {
            if (confirm('Selected records will be disabled')) {
                document.getElementById('<%=btnok.ClientID%>').click();
            }
        }


        function Printpage() {
            window.open('Dprint.aspx', 'search', 'scrollbars=yes,width=650,height=100')
            window.close();
        }   

    </script>
    
<asp:UpdatePanel ID="upd1" runat="server">
<ContentTemplate>  
<asp:Panel ID="pp" runat="server">    
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label2" runat="server" Text="DDS / BDS Cartridge Status" 
        CssClass="labelhead"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
<tr style="padding-top:10px">
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
    
<tr>
<td align="left" style="width:110px">
<asp:Label ID="lblmcno" runat="server" Text="DDS / BDS Name" CssClass="labelall" ></asp:Label>
</td>
<td align="left">
<asp:DropDownList ID="ddlmcno" runat="server" Width="300px" 
        onselectedindexchanged="ddlmcno_SelectedIndexChanged" CssClass="textbox" >
    </asp:DropDownList>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="Label1" runat="server" CssClass="labelall" Text="Cartridge Number"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtcartnumber" runat="server" Width="294px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblitemcode" runat="server" CssClass="labelall" Text="Item Name"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtitemname" runat="server" Width="294px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblcatno" runat="server" Text="Status" CssClass="labelall"></asp:Label>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left" valign="top">
<asp:DropDownList ID="ddlcartno" runat="server" Width="225px" 
        onselectedindexchanged="ddlcartno_SelectedIndexChanged" 
        CssClass="textbox" Height="20px" >
    <asp:ListItem>Enabled</asp:ListItem>
    <asp:ListItem>Disabled</asp:ListItem>
    <asp:ListItem>All(Enabled & Disabled)</asp:ListItem>
    <asp:ListItem>BDS Partial Cartridge</asp:ListItem>
    <asp:ListItem>Empty Cartridge</asp:ListItem>
    <asp:ListItem>Low Stock</asp:ListItem>
    <asp:ListItem>Removed Cartridge</asp:ListItem>
    <asp:ListItem>Invalid Cartridge</asp:ListItem>
    </asp:DropDownList>
</td>
<td align="left">
  <asp:ImageButton ID="btnsear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Search.png" onclick="btnsear_Click" Height="22px"  />
</td>
</tr>
</table>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left" style="padding-top:10px;">

<table cellpadding="0" cellspacing="0" border="0" >
<tr>
<td>
 <asp:ImageButton ID="btnactive" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Enable.png" onclick="btnactive_Click" Height="22px" />       
</td>
<td>
 <asp:ImageButton ID="btndeactive" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Disable.png" onclick="btndeactive_Click" Height="22px" />
</td>
<td>
 <asp:ImageButton ID="btnexcel" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Excel.png" onclick="btnexcel_Click" Height="22px" />
</td>
<td>
 <asp:ImageButton ID="btnprint" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Print.png" onclick="btnprint_Click" Height="22px" />
</td>


<%--<td>
 <asp:HyperLink ID="SaveToExcel" runat="server">Save to Excel</asp:HyperLink> 
</td>--%>
<td>
<asp:Button ID="btnok" runat="server" CausesValidation="False" OnClick="btnok_Click" Style="position: static; display: none" Text="Ok" />      
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left" style="padding-top:5px;">
    <asp:GridView ID="cartmanage" runat="server" AutoGenerateColumns="False" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        AllowSorting="True" onsorting="cartmanage_Sorting" 
        onpageindexchanging="cartmanage_PageIndexChanging" 
         AllowPaging="True" CssClass="gridcss" PageSize="30">  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
        <Columns>               
            <asp:TemplateField>
            <HeaderTemplate>
            <input id="chkAll" onclick="SelectAllCheckboxes(this);" 
              runat="server" type="checkbox" />           
            </HeaderTemplate>
            <ItemTemplate>
            <asp:CheckBox ID="chk" runat="server" />            
            </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="DDS_Name" HeaderText="DDS / BDS Name" 
                SortExpression="DDS_Name" />
            <asp:BoundField DataField="Cell_Id" HeaderText="Cell No" SortExpression="Cell_Id" />
            <asp:BoundField DataField="Cartridge_Id" HeaderText="Cartridge No" 
                SortExpression="Cartridge_Id" />
            <asp:BoundField DataField="Item_Code" HeaderText="Item Code" SortExpression="Item_Code"/>
            <asp:BoundField DataField="Item_Name" HeaderText="Item Name" SortExpression="Item_Name"/>
            <asp:BoundField DataField="Aval_Quantity" HeaderText="System Qty" 
                SortExpression="Aval_Quantity">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
              <asp:BoundField DataField="Pre_Allocated" HeaderText="Preallocated Qty" 
                SortExpression="Pre_Allocated">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
              <asp:BoundField DataField="Computed_Bal" HeaderText="Computed Bal" 
                SortExpression="Computed_Bal">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="PackType" HeaderText="Pack Type" 
                SortExpression="PackType"/>
            <asp:BoundField DataField="Pack_Size" HeaderText="Pack Size" 
                SortExpression="Pack_Size">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="uom" HeaderText="UOM" SortExpression="uom"/>
            <asp:BoundField DataField="Batch_No" HeaderText="Batch No" SortExpression="Batch_No"/>
            <asp:BoundField DataField="Expiry_Date" HeaderText="Expiry Date" SortExpression="Expiry_Date"/>
            <asp:BoundField HeaderText="Status" DataField="Activation_Status" />
            <asp:BoundField HeaderText="Reason" DataField="Reason" />                                    
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
<td align="center">
<asp:Label ID="lblpge" runat="server" CssClass="labelall" Text="No Record Found" 
        Font-Bold="True"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
</table> 
</asp:Panel> 
</ContentTemplate>
    </asp:UpdatePanel>   
</asp:Content>
