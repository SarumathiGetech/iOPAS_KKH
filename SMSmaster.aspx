<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SMSmaster.aspx.cs" Inherits="SMSmaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="scriptman" runat="server" ></asp:ScriptManager>
<asp:UpdatePanel ID="updpanel" runat="server" >
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
 <tr>
 <td>
 <asp:Label ID="lblhead" runat="server" Text="SMS  Master" CssClass="labelhead"></asp:Label>
 </td>
 </tr> 
</table>
</td>
</tr>
<tr style="padding-top:10px">
<td align="left">
<table cellpadding="0" cellspacing="0" border="0" >
    
<tr >
<td align="left">
<asp:Label ID="lblmsg1" runat="server" Text="Message Type" CssClass="labelall" 
        Width="212px"></asp:Label>
</td>
<td align="left">
<asp:DropDownList ID="ddlmsgtyp" runat="server" Width="250px" AutoPostBack="True" 
        onselectedindexchanged="ddlmsgtyp_SelectedIndexChanged" CssClass="textbox">
    <asp:ListItem>iOPAS Processing Error</asp:ListItem>
     <asp:ListItem>iOPAS DDS/BDS Communication Error</asp:ListItem>
    <asp:ListItem>In Queue Alert</asp:ListItem>
</asp:DropDownList>   
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblinque" runat="server" CssClass="labelall" 
        Text="No of InQueue Orders Not Processed (Interface)" Width="300px"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtinqueue" runat="server" Width="70px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtinqueue" Display="Dynamic" ErrorMessage="*" 
        SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator2" runat="server" 
        ControlToValidate="txtinqueue" Display="Dynamic" ErrorMessage="*" 
        MaximumValue="99" MinimumValue="1" SetFocusOnError="True" 
        ValidationGroup="save" Type="Integer"></asp:RangeValidator>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblelapsed" runat="server" CssClass="labelall" 
        Text="Elapsed Time Since Last Processed Order(Iguana Queue in Minutes)" Width="300px"></asp:Label>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtlastprocess" runat="server" Width="70px" MaxLength="2" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtlastprocess" Display="Dynamic" ErrorMessage="*" 
        SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator1" runat="server" 
        ControlToValidate="txtlastprocess" Display="Dynamic" ErrorMessage="*" 
        MaximumValue="99" MinimumValue="1" SetFocusOnError="True" 
        ValidationGroup="save" Type="Integer"></asp:RangeValidator>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td colspan="2" align="right">
<%--<asp:Button ID="btnsave" runat="server" Text="Save" onclick="btnsave_Click" 
        ValidationGroup="save" CssClass="btn" />--%>
  <asp:ImageButton ID="btnsave" runat="server" CssClass="btn" ValidationGroup="save"
            ImageUrl="~/ButtonImages/Save.png" onclick="btnsave_Click" Height="20px"/>
</td>
</tr>
</table>
</td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>

