<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="LoadingMasteraspx.aspx.cs" Inherits="LoadingMasteraspx" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="head">    
</asp:Content>
<asp:Content ID="contenmt2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="updcell" runat="server">
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label2" runat="server" Text="Loading Master" CssClass="labelhead"></asp:Label>
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
<asp:Label ID="lblpharmloc" runat="server" Text="Pharmacy Location" 
        CssClass="labelall"></asp:Label>
</td>
<td>
<asp:DropDownList ID="ddlpharmloc" runat="server" Width="200px" AutoPostBack="True" 
        onselectedindexchanged="ddlpharmloc_SelectedIndexChanged" CssClass="textbox" ></asp:DropDownList>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="Label4" runat="server" 
        Text="Loader and First verifier must be Different " CssClass="labelall" 
        Width="250px"></asp:Label>
</td>
<td align="left">
<asp:CheckBox ID="chkfist" runat="server" Text="" />
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblsecond" runat="server" Text="Second Verification Required" CssClass="labelall"></asp:Label>
</td>
<td align="left">
<asp:CheckBox ID="chksecond" runat="server" Text="" />
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblexpdate" runat="server" Text="Minimum Drug Expiry  for Loading (Days)" 
        CssClass="labelall "></asp:Label>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtexpdate" ErrorMessage="*" ValidationGroup="master"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator1" runat="server" 
        ControlToValidate="txtexpdate" ErrorMessage="*" MaximumValue="999" 
        MinimumValue="0" SetFocusOnError="True" Type="Integer" 
        ValidationGroup="master"></asp:RangeValidator>
</td>
<td align="left">
<asp:TextBox ID="txtexpdate" runat="server" MaxLength="3" Width="100px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>    
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="Label1" runat="server" Text="Minimum Drug Expiry for Auto Disabling (Days)" 
        CssClass="labelall "></asp:Label>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtdisable" ErrorMessage="*" ValidationGroup="master"></asp:RequiredFieldValidator>
 <asp:RangeValidator ID="RangeValidator2" runat="server" 
        ControlToValidate="txtdisable" ErrorMessage="*" MaximumValue="999" 
        MinimumValue="0" SetFocusOnError="True" Type="Integer" 
        ValidationGroup="master"></asp:RangeValidator>       
</td>
<td align="left">
<asp:TextBox ID="txtdisable" runat="server" MaxLength="3" Width="100px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>    
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="Label3" runat="server" Text="BDS Partial Cartridge Auto Enabling (Minutes)" 
        CssClass="labelall "></asp:Label>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="Txtpartialcart" ErrorMessage="*" ValidationGroup="master"></asp:RequiredFieldValidator>
 <asp:RangeValidator ID="RangeValidator3" runat="server" 
        ControlToValidate="Txtpartialcart" ErrorMessage="*" MaximumValue="999" 
        MinimumValue="0" SetFocusOnError="True" Type="Integer" 
        ValidationGroup="master"></asp:RangeValidator>       
</td>
<td align="left">
<asp:TextBox ID="Txtpartialcart" runat="server" MaxLength="3" Width="100px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>    
</td>
</tr>
<tr>
<td colspan="2" align="right">

<%--<asp:Button ID="btnsecond" runat="server" Text="Save" CssClass="btn" 
        onclick="btnsecond_Click" ValidationGroup="master" style="height: 26px" />--%>
  <asp:ImageButton ID="btnsecond" runat="server" CssClass="btn" ValidationGroup="master"
            ImageUrl="~/ButtonImages/Save.png" onclick="btnsecond_Click" Height="20px"/>
</td>
</tr>
<tr>
<td>
<br />
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
<asp:GridView ID="griddetail" runat="server" AutoGenerateColumns="False" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        AllowPaging="True" onpageindexchanging="griddetail_PageIndexChanging" CssClass="gridcss" >  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns>
        <asp:BoundField DataField="Location_Name" HeaderText=" Pharmacy Location" />
        <asp:BoundField DataField="Created_by" HeaderText="Created by" />
        <asp:BoundField DataField="Created_Date" HeaderText="Created Date Time" />
        <asp:BoundField DataField="Updated_by" HeaderText="Updated by" />
        <asp:BoundField DataField="Updated_Date" HeaderText="Updated Date Time" />
    </Columns>
   <FooterStyle BackColor="#507CD1" Font-Bold="False" ForeColor="#FF8000" />
    <PagerStyle BackColor="#169116" ForeColor="White" HorizontalAlign="Right" />    
    <HeaderStyle BackColor="#169116" Font-Bold="True" ForeColor="White" 
         Wrap="True" HorizontalAlign="Center" />
    <EditRowStyle BackColor="#2461BF"  ForeColor="#CC3300" />   
    </asp:GridView> 
</td>
</tr >
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


