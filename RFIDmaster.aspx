<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RFIDmaster.aspx.cs" Inherits="RFIDmaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>
<script language="javascript" type="text/javascript">
    
</script>

<asp:UpdatePanel ID="updcell" runat="server">
<ContentTemplate>

<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label2" runat="server" Text="RFID Master" CssClass="labelhead"></asp:Label>
</td>
</tr>
</table>

<tr style="padding-top:10px">
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:Label ID="lblpharmloc" runat="server" Text="Pharmacy Location" 
        CssClass="labelall" Width="250px"></asp:Label>
</td>
<td>
<asp:DropDownList ID="ddlpharmloc" runat="server" Width="200px" 
        CssClass="textbox" AutoPostBack="True" 
        onselectedindexchanged="ddlpharmloc_SelectedIndexChanged" ></asp:DropDownList>
</td>
</tr>
<tr style="padding-top:5px">
<td align="left">
<asp:Label ID="lblmode" runat="server" Text="Operation Mode" 
        CssClass="labelall"></asp:Label>
</td>
<td>
<%--<asp:DropDownList ID="ddlmode" runat="server" Width="200px" AutoPostBack="True" 
         CssClass="textbox" onselectedindexchanged="ddlmode_SelectedIndexChanged" >
    <asp:ListItem Value="Normal"></asp:ListItem>
    <asp:ListItem Value="Trigger"></asp:ListItem>
    </asp:DropDownList>--%>
    <table width="100%" cellpadding="0" cellspacing="0" border="0"  
        style="border: thin ridge #CCCCCC;">
    <tr>
    <td>
      <asp:CheckBox ID="ChkNormal" runat="server" Text="Normal" CssClass="labelall" 
            TextAlign="Left" AutoPostBack="True" 
            oncheckedchanged="ChkNormal_CheckedChanged"  />
    </td> 
    <td style="padding-left:20px">
    <asp:CheckBox ID="chkTrigger" runat="server" Text="Trigger" CssClass="labelall" 
            TextAlign="Left" AutoPostBack="True" oncheckedchanged="chkTrigger_CheckedChanged" 
            />
    </td>   
    </tr>
    
    </table>

  
</td>

</tr>
<%--<tr style="padding-top:10px">
<td align="left">
<asp:Label ID="lblassemble" runat="server" 
        Text="Auto assembly" CssClass="labelall" 
        Width="250px"></asp:Label>
</td>
<td align="left">
<asp:CheckBox ID="chknormalass" runat="server" Text="" />
</td>
</tr>--%>
<%--<tr>
<td align="left">
<asp:Label ID="lblemptybag" runat="server" 
        Text="Print empty bag" CssClass="labelall" 
        Width="250px"></asp:Label>
</td>
<td align="left">
<asp:CheckBox ID="chkemptybag" runat="server" Text="" />
</td>
</tr>--%>
<%--<tr>
<td align="left">
<asp:Label ID="lbltrigremote" runat="server" 
        Text="Allow Jump Queue by remote application" CssClass="labelall" 
        Width="250px"></asp:Label>
</td>
<td align="left">
<asp:CheckBox ID="chkjump" runat="server" Text="" />
</td>
</tr>--%>
<tr>
<td colspan="2" align="right">

<%--<asp:Button ID="btnsubmit" runat="server" Text="Save" CssClass="btn" 
        style="height: 26px" onclick="btnsubmit_Click" />--%>
   <asp:ImageButton ID="btnsubmit" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Save.png" onclick="btnsubmit_Click" Height="20px"/>     
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
        AllowPaging="True"  CssClass="gridcss" >  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns>       
        <asp:BoundField DataField="Location_Name" HeaderText=" Pharmacy Location" />
         <asp:BoundField DataField="Mode" HeaderText="Operation Mode" />
        <asp:BoundField DataField="Createdby" HeaderText="Created by" />
        <asp:BoundField DataField="Createddate" HeaderText="Created Date Time" />
        <asp:BoundField DataField="Updatedby" HeaderText="Updated by" />
        <asp:BoundField DataField="Updateddate" HeaderText="Updated Date Time" />
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
</td> 
</tr> 
</table> 

</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

