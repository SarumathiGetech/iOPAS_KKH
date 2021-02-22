<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="length.aspx.cs" Inherits="length" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="scriptman" runat="server" ></asp:ScriptManager>
<asp:UpdatePanel ID="updpanel" runat="server" >
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:TextBox ID="txtlen" runat="server" TextMode="MultiLine" Width="500px" ></asp:TextBox>
</td>

</tr>
<tr>
<td align="center">
<asp:TextBox ID="txtresult" runat="server"  Width="100px" ></asp:TextBox>
</td>

</tr>
<tr>
<td align="center">
<asp:Button ID="btnlen" runat="server" Text="Length" onclick="btnlen_Click" />
<asp:Button ID="Button1" runat="server" Text="Clear" onclick="Button1_Click"  />
</td>

</tr>
</table>
</td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

