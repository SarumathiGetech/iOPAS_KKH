<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="BDSStructureStatus.aspx.cs" Inherits="BDSStructureStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="scriptmana" runat="server"></asp:ScriptManager>
     <script language="javascript" src="JS/checkbox.js" type="text/javascript">    
     </script>
         <script language="javascript" type="text/javascript">
     
             function Printpage() {
                 window.open('Dprint.aspx', 'search', 'scrollbars=yes,width=650,height=100')
                 window.close();
             }   

    </script>
     <asp:UpdatePanel ID="upd1" runat="server">
<ContentTemplate> 

<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label2" runat="server" Text="BDS Cartridge Structure Status" 
        CssClass="labelhead"></asp:Label>
</td>
</tr>
</table>
<tr style="padding-top:10px">
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
    
<tr>
<td align="left" style="width:160px">
<asp:Label ID="lblmcno" runat="server" Text="BDS Name" CssClass="labelall" ></asp:Label>
</td>
<td align="left">
<asp:DropDownList ID="ddlmcno" runat="server" Width="300px"  CssClass="textbox" >
    </asp:DropDownList>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblcartno" runat="server" CssClass="labelall" Text="Virtual Cartridge Number"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtvcnumber" runat="server" Width="294px" CssClass="textbox" 
        AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="Label1" runat="server" CssClass="labelall" Text="VC Width Min"></asp:Label>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:TextBox ID="txtvcwidthmin" runat="server" Width="130px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td style="padding-left:4px">
<asp:Label ID="Label3" runat="server" CssClass="labelall" Text="Max"></asp:Label>
</td>
<td style="padding-left:4px">
<asp:TextBox ID="txtvcwidthmax" runat="server" Width="130px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
</table>

</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblcatno" runat="server" Text="Status" CssClass="labelall"></asp:Label>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:DropDownList ID="ddlcartno" runat="server" Width="300px" CssClass="textbox" >
    <asp:ListItem>All(Alloted & Open)</asp:ListItem>
    <asp:ListItem>Alloted</asp:ListItem>
    <asp:ListItem>Open</asp:ListItem>    
    </asp:DropDownList>
</td>
<td align="left">
  <asp:ImageButton ID="btnsear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Search.png"  Height="22px" 
        onclick="btnsear_Click"  />
</td>
<td>
 <asp:ImageButton ID="btnprint" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Print.png"  Height="22px" 
        onclick="btnprint_Click" />
</td>
</tr>
</table>
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
    <asp:GridView ID="Cartstructure" runat="server" AutoGenerateColumns="False" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        AllowSorting="True" AllowPaging="True" CssClass="gridcss" PageSize="30" 
        onpageindexchanging="Cartstructure_PageIndexChanging" 
        onsorting="Cartstructure_Sorting">  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
        <Columns>      
             <asp:BoundField DataField="V_Cartridge_No" HeaderText="V_Cartridge_Number" 
                SortExpression="V_Cartridge_No" />         
            <asp:BoundField DataField="V_Cell_No" HeaderText="V_Cell_Number" SortExpression="V_Cell_No" />                   
            <asp:BoundField DataField="V_Cartridge_Width" HeaderText="V_Cartridge_Width" SortExpression="V_Cartridge_Width"/>
            <asp:BoundField DataField="V_Cartridge_Height" HeaderText="V_Cartridge_Height" SortExpression="V_Cartridge_Height"/>       
            <asp:BoundField HeaderText="V_Cartridge_IsSelected" DataField="V_Cartridge_IsSelected" SortExpression="V_Cartridge_IsSelected">   
              <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Status" DataField="Status" />                                  
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



</td>
</tr>
</table> 
</ContentTemplate>
    </asp:UpdatePanel>   
</asp:Content>

