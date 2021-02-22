<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Processingmaster.aspx.cs" Inherits="Processingmaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="scrman" runat="server" ></asp:ScriptManager>
<asp:UpdatePanel ID="upd1" runat="server">
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label2" runat="server" Text="Processing Master" CssClass="labelhead"></asp:Label>
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
<asp:Label ID="lblphar" runat="server" Text="Pharmacy Location" CssClass="labelall" 
        Width="250px"></asp:Label>
</td>
<td align="left">
<asp:DropDownList ID="ddlphar" runat="server" Width="220px" AutoPostBack="True" 
        onselectedindexchanged="ddlphar_SelectedIndexChanged" CssClass="textbox" ></asp:DropDownList>
</td>
</tr>

<tr>
<td align="left">
<asp:Label ID="lblpacktype" runat="server" Text="Pack Type" CssClass="labelall"></asp:Label>
</td>
<td align="left">
<asp:DropDownList ID="ddlpacktype" runat="server" Width="220px" CssClass="textbox" 
        AutoPostBack="True" onselectedindexchanged="ddlpacktype_SelectedIndexChanged" ></asp:DropDownList>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblcontainer" runat ="server" 
        Text="% of Maximum Cartridge Quantity Per Pick" CssClass="labelall" 
        Width="230px"></asp:Label>
    <asp:RangeValidator ID="RangeValidator1" runat="server" 
        ControlToValidate="txtcartqty" Display="Dynamic" ErrorMessage="*" 
        MaximumValue="100" MinimumValue="1" SetFocusOnError="True" Type="Integer" 
        ValidationGroup="save"></asp:RangeValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtcartqty" Display="Dynamic" ErrorMessage="*" 
        SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
</td>
<td align="left">
<asp:TextBox ID="txtcartqty" runat="server" Width="214px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lbldds" runat="server" Text="DDS / BDS Name" CssClass="labelall"></asp:Label>
</td>
<td align="left">
<asp:DropDownList ID="ddlddsno" runat="server" Width="220px" CssClass="textbox" 
        onselectedindexchanged="ddlddsno_SelectedIndexChanged"></asp:DropDownList>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblplasticbag" runat ="server" 
        Text="Max. No. of Bags per Container" CssClass="labelall" Width="200px"></asp:Label>
    <asp:RangeValidator ID="RangeValidator2" runat="server" 
        ControlToValidate="txtbagpercontainer" Display="Dynamic" ErrorMessage="*" 
        MaximumValue="99" MinimumValue="1" SetFocusOnError="True" Type="Integer" 
        ValidationGroup="save"></asp:RangeValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtbagpercontainer" Display="Dynamic" ErrorMessage="*" 
        SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
</td>
<td align="left">
<asp:TextBox ID="txtbagpercontainer" runat="server" Width="214px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
<tr>
<td colspan="2" align="right">
<%--<asp:Button ID="btnadd" runat="server" Text="Clear" onclick="btnadd_Click" CssClass="btn" />--%>
  <asp:ImageButton ID="btnadd" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnadd_Click" Height="20px"/>
<%--<asp:Button ID="btnsave" runat="server" Text="Save" onclick="btnsave_Click" 
        ValidationGroup="save" CssClass="btn" />--%>
  <asp:ImageButton ID="btnsave" runat="server" CssClass="btn" ValidationGroup="save"
            ImageUrl="~/ButtonImages/Save.png" onclick="btnsave_Click" Height="20px"/>
  <asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" ValidationGroup="save"
            ImageUrl="~/ButtonImages/Update.png"  Height="20px" 
        onclick="btnupdate_Click"/>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<asp:GridView ID="procssgrid" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left" RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        onselectedindexchanging="procssgrid_SelectedIndexChanging" 
        AllowPaging="True" onpageindexchanging="procssgrid_PageIndexChanging" 
        CssClass="gridcss" onsorting="procssgrid_Sorting">  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns>          
        <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
        <asp:BoundField DataField="ID" Visible="false"/>
           <%-- <ItemStyle ForeColor="White" Font-Size="XX-Small" />
        </asp:BoundField>--%>
        <asp:BoundField DataField="location_name" HeaderText="Pharmacy Location" SortExpression="location_name" />
        <asp:BoundField DataField="Packtype" HeaderText="Pack Type" SortExpression="Packtype" />       
        <asp:BoundField DataField="Max_cart_Qty" 
            HeaderText="Cartridge Quantity Per pick" SortExpression="Max_cart_Qty">
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="DDS_Name" HeaderText="DDS / BDS Name" SortExpression="DDS_Name"/>
        <asp:BoundField DataField="Bagper_Container" 
            HeaderText="Max. No. of Bag Per Container" SortExpression="Bagper_Container">
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Created_by" HeaderText="Created by" SortExpression="Created_by"/>
        <asp:BoundField DataField="created_date" HeaderText="Created Date Time" SortExpression="created_date"/>
        <asp:BoundField DataField="Updated_by" HeaderText="Updated by" SortExpression="Updated_by"/>
        <asp:BoundField DataField="Updated_date" HeaderText="Updated Date Time" SortExpression="Updated_date" />
    </Columns>
    <FooterStyle BackColor="#507CD1" Font-Bold="False" ForeColor="#FF8000" />
    <PagerStyle BackColor="#169116" ForeColor="White" HorizontalAlign="Right" />    
    <HeaderStyle BackColor="#169116" Font-Bold="True" ForeColor="White" 
         Wrap="True" />
    <EditRowStyle BackColor="#2461BF"  ForeColor="#CC3300" />    
    </asp:GridView>
</td>
</tr>
<tr>
<td align="left" style="padding-left:300px" >
<asp:Label ID="lblpge" runat="server" CssClass="labelall"></asp:Label>
</td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

