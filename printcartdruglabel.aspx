<%@ Page Language="C#" MasterPageFile="~/Site.master"  AutoEventWireup="true" CodeFile="printcartdruglabel.aspx.cs" Inherits="printcartdruglabel" %>

<asp:Content ID="content" ContentPlaceHolderID ="head" runat="server" ></asp:Content>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="scriptman" runat="server" ></asp:ScriptManager>
  <script language="javascript" type="text/javascript">        
        function itemsearch() {
            var icode = document.getElementById('<%=txtitemcode.ClientID%>').value;
            var dcode = document.getElementById('<%=txtdrugcode.ClientID%>').value;
            var iname = document.getElementById('<%=txtitemname.ClientID%>').value;            
            window.open(("Druglabelpopup.aspx?drcode=" + dcode + " &itcode=" + icode + " &itname=" + iname), 'search', 'menubar=no, toolbar=no,center:yes,scrollbars=no,width=720,height=500[color=blue]40,top=100,left=200')
            window.close();
        }

        // Search Window open using Enter Key \\ 
        function doClick2(e) {
            var key;

            if (window.event)
                key = window.event.keyCode;     //IE
            else
                key = e.which;     //firefox

            if (key == 13) {
                document.getElementById('<%=btnsearch.ClientID%>').click();
                event.keyCode = 0
            }
        }
                        
</script>
<asp:UpdatePanel ID="updpanel" runat="server" >
<ContentTemplate>
<asp:Panel ID="pp" runat="server">
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="lblhead" runat="server" Text="Print Cartridge Drug Label" 
        CssClass="labelhead"></asp:Label>
</td>
</tr>
</td>
</tr>
</table>
<tr style="padding-top:10px">
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">

<tr>
<td align="left">
<asp:Label ID="lblitencode" runat="server" Text="Item Code" CssClass="labelall"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ErrorMessage="*" ControlToValidate="txtitemcode" Display="Dynamic" 
        ValidationGroup="print"></asp:RequiredFieldValidator>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtitemcode" runat="server" Width="200px" CssClass="textbox" AutoCompleteType="Disabled" ></asp:TextBox>
</td>
<td align="left" style="padding-left:3px">
<asp:Label ID="Label1" runat="server" Text="Drug Code" CssClass="labelall" 
        Width="75px"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtdrugcode" runat="server" Width="185px" CssClass="textbox" AutoCompleteType="Disabled" ></asp:TextBox>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblitemname" runat="server" Text="Item Name" CssClass="labelall"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtitemname" runat="server" Width="468px" CssClass="textbox" AutoCompleteType="Disabled" ></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblbrand" runat="server" Text="Brand Name" CssClass="labelall"></asp:Label>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtbrand" runat="server" Width="200px" CssClass="textbox" AutoCompleteType="Disabled" ></asp:TextBox>
</td>
<td align="left" style="padding-left:3px">
<asp:Label ID="lblmfrcode" runat="server" CssClass="labelall" Text="MFR Barcode" 
        Width="75px"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtmfrcode" runat="server" Width="185px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblpacksize" runat="server" Text="Pack Type" CssClass="labelall"></asp:Label>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtpacktype" runat="server" Width="120px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td align="left" style="padding-left:5px">
<asp:Label ID="lblpacktype" Text="Pack Size" runat="server" CssClass="labelall" 
        Width="60px"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtpacksize" runat="server" Width="115px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td align="left" style="padding-left:5px">
<asp:Label ID="lblstrength" runat="server" Text="UOM" CssClass="labelall" 
        Width="35px" ></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtuom" runat="server" Width="116px" CssClass="textbox" AutoCompleteType="Disabled" ></asp:TextBox>
</td>
<td>
<%--<asp:Button ID="btnclear" runat="server" Text="Clear"  
        Height="22px" Width="50px" UseSubmitBehavior="False" onclick="btnclear_Click" CssClass="btn" />--%>
      <asp:ImageButton ID="btnclear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnclear_Click" Height="20px"/>
</td>
<td>
<%--<asp:Button ID="btnsearch" runat="server" Text="Search"  
        Height="22px" Width="50px" CssClass="btn" onclick="btnsearch_Click" />--%>
<asp:ImageButton ID="btnsearch" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Search.png" Height="20px"/>
</td>
</tr>
</table>
</td>
</tr>
<tr style="padding-top:5px">
<td align="left">
<asp:Label ID="lblprinter" runat="server" Text="Printer Name" CssClass="labelall" 
        Width="100px"></asp:Label>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:DropDownList ID="ddlprintername" runat="server" Width="475px" CssClass="textbox" >  
    </asp:DropDownList>
</td>
<td align="left">
<%--<asp:Button ID="btnsave" runat="server" Text="Print" Height="22px" Width="50px" 
        onclick="btnsave_Click" CssClass="btn" ValidationGroup="print" />--%>
<asp:ImageButton ID="btnsave" runat="server" CssClass="btn" ValidationGroup="print"
            ImageUrl="~/ButtonImages/Print.png" Height="20px" 
        onclick="btnsave_Click"/>
</td>
<td align="left">
 <asp:TextBox ID="searchvalue" runat="server" CausesValidation="false" Style="position: static; display: none"/>  
 </td>
<td align="left">
 <asp:Button ID="btnok" runat="server" CausesValidation="False" OnClick="btnok_Click" Style="position: static; display: none" Text="Ok" />           
 </td>
</tr>
</table>
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