<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Pharmacymaster.aspx.cs" Inherits="Pharmacymaster" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="head">     
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <asp:ScriptManager ID="scriptmana" runat="server"></asp:ScriptManager> 
    <script language="javascript" type="text/javascript">     

    </script> 
<asp:UpdatePanel ID="upd1" runat="server" >
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label2" runat="server" Text="Pharmacy Location Master" CssClass="labelhead" ></asp:Label>
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
<asp:Label ID="Label3" runat="server" Text="Pharmacy Code" 
        CssClass="labelall" Width="100px"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ErrorMessage="*" ControlToValidate="txtpharcode" ValidationGroup="save" 
        SetFocusOnError="True"></asp:RequiredFieldValidator>
</td>
<td align="left">
<asp:TextBox ID="txtpharcode" runat="server" Width="364px" CssClass="textbox" 
        AutoCompleteType="Disabled" MaxLength="10" ></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblphar" runat="server" Text="Pharmacy Location" 
        CssClass="labelall"></asp:Label>
    <asp:RequiredFieldValidator ID="pharloc" runat="server" 
        ErrorMessage="*" ControlToValidate="txtphloc" ValidationGroup="save" 
        SetFocusOnError="True"></asp:RequiredFieldValidator>
</td>
<td align="left">
<asp:TextBox ID="txtphloc" runat="server" Width="364px" CssClass="textbox" 
        AutoCompleteType="Disabled" MaxLength="50"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="Label5" runat="server" Text="Pharmacy Abbreviation" 
        CssClass="labelall"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ErrorMessage="*" ControlToValidate="txtpharabbr" ValidationGroup="save" 
        SetFocusOnError="True"></asp:RequiredFieldValidator>
</td>
<td align="left">
<asp:TextBox ID="txtpharabbr" runat="server" Width="364px" CssClass="textbox" 
        AutoCompleteType="Disabled" MaxLength="1"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left" style="width:200px">
<asp:Label ID="lblcode2" runat="server" Text="Label Header" CssClass="labelall"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtheader" runat="server" Width="364px" TextMode="MultiLine" 
        CssClass="textbox" AutoCompleteType="Disabled" MaxLength="100"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="Label4" runat="server" Text="Label Footer Hospital Name" CssClass="labelall"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtfooter1" runat="server" Width="364px" CssClass="textbox" 
        AutoCompleteType="Disabled" MaxLength="100" ></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="Label1" runat="server" Text="Label Footer Address" CssClass="labelall"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtfooter" runat="server" Width="365px" TextMode="MultiLine" 
        Height="80px" CssClass="textbox" AutoCompleteType="Disabled" 
        MaxLength="255"></asp:TextBox>
</td>
</tr>
<%--<tr>
<td align="left">
<asp:Label ID="Label4" runat="server" Text="Pending Packing Alert Duration (Minutes)" 
        CssClass="labelall "></asp:Label>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="txtpackdur" ErrorMessage="*" ValidationGroup="save" 
        SetFocusOnError="True"></asp:RequiredFieldValidator>
  <asp:RangeValidator ID="RangeValidator3" runat="server" 
        ControlToValidate="txtpackdur" ErrorMessage="*" MaximumValue="999" 
        MinimumValue="1" SetFocusOnError="True" Type="Integer" 
        ValidationGroup="save"></asp:RangeValidator>           
</td>
<td align="left">
<asp:TextBox ID="txtpackdur" runat="server" MaxLength="3" Width="100px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>    
</td>
</tr>--%>
<%--<tr>
<td align="left">
<asp:Label ID="Label5" runat="server" Text="Pending Assemble Alert Duration (Minutes)" 
        CssClass="labelall "></asp:Label>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
        ControlToValidate="txtassemdur" ErrorMessage="*" ValidationGroup="save" 
        SetFocusOnError="True"></asp:RequiredFieldValidator>
   <asp:RangeValidator ID="RangeValidator4" runat="server" 
        ControlToValidate="txtassemdur" ErrorMessage="*" MaximumValue="999" 
        MinimumValue="1" SetFocusOnError="True" Type="Integer" 
        ValidationGroup="save"></asp:RangeValidator>   
</td>
<td align="left">
<asp:TextBox ID="txtassemdur" runat="server" MaxLength="3" Width="100px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>    
</td>
</tr>--%>
<tr>
<td align="left">
<asp:Label ID="lblprefix" runat="server" CssClass="labelall" Text="Cartridge Prefix"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ErrorMessage="*" ControlToValidate="txtcartprefix" Display="Dynamic" 
        SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
</td>
<td>
<asp:TextBox ID="txtcartprefix" runat="server" Width="100px" CssClass="textbox" 
        AutoCompleteType="Disabled" MaxLength="2"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblcartauto" runat="server" Text="Active" 
        CssClass="labelall"></asp:Label>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:CheckBox ID="chkauto" runat="server" Text="" />
</td>
<td style="padding-left:160px">
<%--<asp:Button ID="btnadd" runat="server" Text="Clear" onclick="btnadd_Click" CssClass="btn" />--%>
  <asp:ImageButton ID="btnadd" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnadd_Click" Height="20px"/>
</td>
<td>
<%--<asp:Button ID="btnsubmit" runat="server" Text="Save" Font-Bold="False" 
        onclick="btnsubmit_Click" ValidationGroup="save" CssClass="btn" />--%>   
      <asp:ImageButton ID="btnsubmit" runat="server" CssClass="btn" ValidationGroup="save"
            ImageUrl="~/ButtonImages/Save.png" onclick="btnsubmit_Click" Height="20px"/>    
              <asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" ValidationGroup="save"
            ImageUrl="~/ButtonImages/Update.png" Height="20px" 
        onclick="btnupdate_Click"/>  
</td>
</tr>
</table>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:GridView ID="griddetails" runat="server" AutoGenerateColumns="False" DataKeyNames="PharmacyID" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        onselectedindexchanging="griddetails_SelectedIndexChanging" 
        AllowPaging="True" onpageindexchanging="griddetails_PageIndexChanging" 
        CssClass="gridcss" onsorting="griddetails_Sorting" AllowSorting="True">  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />   
    <Columns>      
        <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
        <asp:BoundField DataField="PharmacyID" Visible="false"/>
        <%--<ControlStyle Width="1px" />
        <HeaderStyle Width="1px" Wrap="False" />
        <ItemStyle ForeColor="White" Width="1px" BorderColor="White" />
        </asp:BoundField>--%>
        <asp:BoundField DataField="Location_code" HeaderText="Pharmacy code" SortExpression="Location_code" />
        <asp:BoundField DataField="Location_Name" HeaderText="Pharmacy Name" SortExpression="Location_Name" />
        <asp:BoundField DataField="Phar_Abbrivation" HeaderText="Pharmacy Abbrivation" 
            SortExpression="Phar_Abbrivation" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
        <asp:BoundField DataField="Created_by" HeaderText="Created by" SortExpression="Created_by"/>
        <asp:BoundField DataField="Created_Date" HeaderText="Created Date Time" SortExpression="Created_Date" />
        <asp:BoundField DataField="Updated_by" HeaderText="Updated by" SortExpression="Updated_by"/>
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
    <td style="padding-left:300px">
        <asp:Label ID="lblpge" runat="server" CssClass="labelall"></asp:Label>
    </td>
</tr>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>


