<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Drugpackmaster.aspx.cs" Inherits="Drugpackmaster" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">    
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
<asp:ScriptManager ID="scriptmana" runat="server"></asp:ScriptManager>   
<script  type="text/javascript">
function Test() 
{
test1() 
//alert(event.keyCode);
}
function test1() 
{
//if ( event.keyCode == 112) 

//     {
alert(event.keyCode);
//     }
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
<asp:Label ID="Label2" runat="server" Text="Drug Pack Type Master" CssClass="labelhead" ></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
<tr style="padding-top:10px">
<td align="left">
<table cellpadding="0" cellspacing="0" border="0" >

<tr>
<td align="left">
<asp:Label ID="lblcatno" runat="server" Text="Pack Type" CssClass="labelall"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtpacktype" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="packtype"></asp:RequiredFieldValidator>
</td>
<td align="left" >
<%--<asp:TextBox ID="txtpacktype" runat="server" Width="181px" onkeyup="Test();"></asp:TextBox>--%>
<asp:TextBox ID="txtpacktype" runat="server" Width="240px" MaxLength="10" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lbldesc" runat="server" Text="Description " CssClass="labelall"></asp:Label>
</td>
<td align="left" >
<asp:TextBox ID="txtdesc" runat="server" TextMode="MultiLine" Width="240px" 
        Height="47px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
<tr>
<td colspan="2" align="right">

<%--<asp:Button ID="btnadd" runat="server" Text="Clear" Font-Bold="False" 
        onclick="btnadd_Click" CssClass="btn" />--%>
<%--<asp:Button ID="btnsubmit" runat="server" Text="Save" Font-Bold="False" 
        onclick="btnsubmit_Click" ValidationGroup="packtype" CssClass="btn" />--%>
  <asp:ImageButton ID="btnadd" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnadd_Click" Height="20px"/>

 <asp:ImageButton ID="btnsubmit" runat="server" CssClass="btn" ValidationGroup="packtype"
            ImageUrl="~/ButtonImages/Save.png" onclick="btnsubmit_Click" Height="20px"/>

 <asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" ValidationGroup="packtype"
            ImageUrl="~/ButtonImages/Update.png"  Height="20px" 
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
<asp:GridView ID="gridpack" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        onselectedindexchanging="gridpack_SelectedIndexChanging" 
        AllowPaging="True" CssClass="gridcss" 
        onpageindexchanging="gridpack_PageIndexChanging" 
        onsorting="gridpack_Sorting" AllowSorting="True">  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns>
        <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
          <asp:BoundField DataField="ID" Visible="false"/>
            <%--  <ItemStyle Font-Size="XX-Small" ForeColor="#FFFFCC" Width="1px" />
          </asp:BoundField>--%>
        <asp:BoundField DataField="packtype" HeaderText="Pack Type" SortExpression="packtype" />
        <asp:BoundField DataField="Created_by" HeaderText="Created by" SortExpression="Created_by" />
        <asp:BoundField DataField="Created_Date" HeaderText="Created Date Time" SortExpression="Created_Date" />
        <asp:BoundField DataField="Updated_by" HeaderText="Updated by" SortExpression="Updated_by" />
        <asp:BoundField DataField="UpdatedDate" HeaderText="Updated Date Time" 
            SortExpression="UpdatedDate">
            <ItemStyle Wrap="True" />
        </asp:BoundField>
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