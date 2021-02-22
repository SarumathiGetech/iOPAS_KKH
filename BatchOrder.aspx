<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="BatchOrder.aspx.cs" Inherits="BatchOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="scriptman" runat="server"></asp:ScriptManager>
     <script language="javascript" type="text/javascript">
         function itemsearch() {
             var icode = document.getElementById('<%=txtitemcode.ClientID%>').value;
             var dcode = document.getElementById('<%=txtdrugcode.ClientID%>').value;
             var iname = document.getElementById('<%=txtitemname.ClientID%>').value;
             window.document.clear();
             window.open(("Batchorderepopup.aspx?drcode=" + dcode + " &itcode=" + icode + " &itname=" + iname), 'search', 'menubar=no, toolbar=no,center:yes,scrollbars=no,width=720,height=500[color=blue]40,top=100,left=200')             
             window.close();
         }            
</script>
<script language="javascript" type="text/javascript" src="cal/popcalendar.js" ></script>
<asp:UpdatePanel ID="updpanel" runat="server">
<ContentTemplate>
<asp:Panel ID="pp" runat="server">
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="lblhead" runat="server" Text="Batch Order Scheduling" 
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
<td align="left" style="width:115px">
<asp:Label ID="Llblitemcode" runat="server" Text="Item Code" CssClass="labelall"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtitemcode" Display="Dynamic" ErrorMessage="*" 
        SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtitemcode" runat="server" Width="200px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox> 
</td>
<td align="left" style="padding-left:3px">
<asp:Label ID="lbldrugcode" runat="server" Text="Drug Code" CssClass="labelall" 
        Width="65px"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtdrugcode" runat="server" Width="180px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
</table>       
</td>
</tr>

<tr>
<td align="left">
<asp:Label ID="lbldrg" runat="server" Text="Item Name" CssClass="labelall"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="txtitemname" Display="Dynamic" ErrorMessage="*" 
        SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
</td>
<td align="left">
<asp:TextBox ID="txtitemname" runat="server" Width="454px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td>
<%--<asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn" />--%>
  <asp:ImageButton ID="btnsearch" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Search.png"  Height="20px"  />
</td>
 <td align="left">
 <asp:TextBox ID="searchvalue" runat="server" CausesValidation="false" Style="position: static; display: none"/>  
 </td>
<td align="left">
 <asp:Button ID="btnok" runat="server" CausesValidation="False" OnClick="btnok_Click" Style="position: static; display: none" Text="Ok" />           
 </td>
</tr>
<tr>
<td align="left">
<asp:Label ID="Label1" runat="server" Text="Pack Type" CssClass="labelall"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
        ControlToValidate="txtpacktype" Display="Dynamic" ErrorMessage="*" 
        ValidationGroup="save"></asp:RequiredFieldValidator>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtpacktype" runat="server" Width="110px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td align="left" style="padding-left:5px">
<asp:Label ID="Label2" runat="server" Text="Pack Size " CssClass="labelall"></asp:Label>
</td>
<td align="left" style="padding-left:5px">
<asp:TextBox ID="txtpacksize" runat="server" Width="115px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td align="left" style="padding-left:5px">
<asp:Label ID="lbluom" runat="server" Text="UOM " CssClass="labelall"></asp:Label>
</td>
<td align="left" style="padding-left:5px">
<asp:TextBox ID="txtuom" runat="server" Width="116px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblqtyperbag" runat="server" Text="Quantity Per Bag  " 
        CssClass="labelall" Width="95px"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
        ControlToValidate="txtqtyperbag" Display="Dynamic" ErrorMessage="*" 
        SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="*" 
        ControlToValidate="txtqtyperbag" MaximumValue="99" MinimumValue="1" 
        Display="Dynamic" SetFocusOnError="True" Type="Integer" ValidationGroup="save"></asp:RangeValidator>
</td>
<td align="left">
<asp:TextBox ID="txtqtyperbag" runat="server" Width="110px" CssClass="textbox" 
        AutoCompleteType="Disabled" MaxLength="2"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="Lbltotqty" runat="server" Text="No of Bags" CssClass="labelall"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
        ControlToValidate="txttotqty" Display="Dynamic" ErrorMessage="*" 
        SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="*" 
        ControlToValidate="txttotqty" MaximumValue="999" MinimumValue="1" 
        Display="Dynamic" SetFocusOnError="True" Type="Integer" ValidationGroup="save"></asp:RangeValidator>
</td>
<td align="left">
<asp:TextBox ID="txttotqty" runat="server" Width="110px" CssClass="textbox" 
        AutoCompleteType="Disabled" MaxLength="3"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="Label4" runat="server" Text="DDS Name" CssClass="labelall"></asp:Label>
 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
        ControlToValidate="ddlddsname" Display="Dynamic" ErrorMessage="*" 
        SetFocusOnError="True" ValidationGroup="save" InitialValue="- Select -"></asp:RequiredFieldValidator>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:DropDownList ID="ddlddsname" runat="server" Width="115px" CssClass="textbox" >   
    <asp:ListItem>- Select -</asp:ListItem>
    </asp:DropDownList>
</td>
<%--<td align="left" style="padding-left:10px">
<asp:Label ID="lblpackreq" runat="server" CssClass="labelall" Text="Packing Required"></asp:Label>
</td>
<td align="left">
<asp:CheckBox ID="chkpack" runat="server" />
</td>--%>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="Label3" runat="server" Text="Schedule Date" CssClass="labelall"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txt_Date" ErrorMessage="*" ValidationGroup="save"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
        ErrorMessage="*" ControlToValidate="txt_Date"        
        ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" 
        SetFocusOnError="True" Display="Dynamic"></asp:RegularExpressionValidator>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:textbox id="txt_Date" runat="server" Columns="6" Width="110px" CssClass="textbox" AutoCompleteType="Disabled"></asp:textbox>
</td>
<td align="left">
<asp:image id="imgCalendar" runat="server" ImageUrl="~/cal/calendar.gif"></asp:image>
</td>
<td align="left" style="padding-left:10px">
<asp:Label ID="lblpacktime" runat="server" Text="Schedule Time" CssClass="labelall"></asp:Label>   
 
</td>
<td align="left" style="padding-left:3px">
<asp:DropDownList ID="ddlhr" runat="server" Width="40px" CssClass="textbox">  
    </asp:DropDownList>
<asp:DropDownList ID="ddlmin" runat="server" Width="40px"> 
    </asp:DropDownList>
  
<asp:Label ID="lbltime" runat="server" Text="(HH MM 2400 Hrs)" Font-Size="X-Small"></asp:Label>
</td>
</tr>
</table>		
</td>

</tr>
<tr>

<td align="left"  >
<asp:Label ID="Label5" runat="server" Text="Pharmacy Instruction" CssClass="labelall"></asp:Label>

</td>
<td>
<asp:TextBox ID="txtpharinstruction" runat="server" CssClass="textbox" 
        TextMode="MultiLine" Width="460px" Height="75px" MaxLength="500"></asp:TextBox>
</td>
<td align="left" >
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">

  <asp:ImageButton ID="btnclear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnclear_Click" Height="20px"  />
</td>
<td align="left">

<asp:ImageButton ID="btncancel" runat="server" CssClass="btn" ValidationGroup="save"
            ImageUrl="~/ButtonImages/Cancel.png" onclick="btncancel_Click" Height="20px"  />
</td>
<td align="left">

<asp:ImageButton ID="btnsave" runat="server" CssClass="btn" ValidationGroup="save"
            ImageUrl="~/ButtonImages/Save.png" onclick="btnsave_Click" Height="20px"  />
</td>
<td align="left">
<asp:ImageButton ID="Btnupdate" runat="server" CssClass="btn" ValidationGroup="save"
            ImageUrl="~/ButtonImages/Update.png"  Height="20px" 
        onclick="Btnupdate_Click"  />
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
<td align="left">
<asp:GridView ID="batchgrid" runat="server" AutoGenerateColumns="False" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        onselectedindexchanging="batchgrid_SelectedIndexChanging" 
        AllowPaging="True" onpageindexchanging="batchgrid_PageIndexChanging" 
        CssClass="gridcss" onsorting="batchgrid_Sorting" AllowSorting="True" >  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns>
        <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
        <asp:BoundField DataField="OrderRef_No" HeaderText="Order Reference No" SortExpression="OrderRef_No" />
        <asp:BoundField DataField="DDSName" HeaderText="DDS Name" SortExpression="DDSName" />
        <asp:BoundField DataField="Item_Code" HeaderText="Item Code" SortExpression="Item_Code" />
        <asp:BoundField DataField="Item_Name" HeaderText="Item Name" SortExpression="Item_Name" />
        <asp:BoundField DataField="Brandname" HeaderText="Brand Name" SortExpression="Brandname" />
        <asp:BoundField DataField="Quantity_Perbag" HeaderText="Quantity Per Bag" SortExpression="Quantity_Perbag" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="NoofBags" HeaderText="No of Bags" SortExpression="NoofBags" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Schedule_Date" HeaderText="Schedule Date" SortExpression="Schedule_Date" />
        <asp:BoundField DataField="Schedule_Time" HeaderText="Schedule Time" SortExpression="Schedule_Time" />
        <asp:BoundField DataField="Updated_by" HeaderText="Updated by" SortExpression="Updated_by" />
        <asp:BoundField DataField="Updateddate" HeaderText="Updated Date Time" SortExpression="Updateddate" />
    </Columns>
   <FooterStyle BackColor="#507CD1" Font-Bold="False" ForeColor="#FF8000" />
    <PagerStyle BackColor="#169116" ForeColor="White" HorizontalAlign="Right" />    
    <HeaderStyle BackColor="#169116" Font-Bold="True" ForeColor="White" 
         Wrap="True" HorizontalAlign="Center" />
    <EditRowStyle BackColor="#2461BF" ForeColor="#CC3300" />    
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
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

