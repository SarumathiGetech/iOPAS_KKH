<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UomMaster.aspx.cs" Inherits="UomMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="scriptmana" runat="server"></asp:ScriptManager>

   <script language="javascript" type="text/javascript">

       function itemsearch() {
           var icode = document.getElementById('<%=txtitemcode.ClientID%>').value;
           var dcode = document.getElementById('<%=txtdrugcode.ClientID%>').value;
           var iname = document.getElementById('<%=txtitemnameadd.ClientID%>').value;
           window.open(("UOMmastersearch.aspx?drcode=" + dcode + " &itcode=" + icode + " &itname=" + iname), 'search', 'menubar=no, toolbar=no,center:yes,scrollbars=no,width=720,height=500[color=blue]40,top=100,left=200')
       }

       // Search Window open using Enter Key \\ 
       function doClick(e) {
           var key;

           if (window.event)
               key = window.event.keyCode;     //IE
           else
               key = e.which;     //firefox

           if (key == 13) {
               itemsearch()
               event.keyCode = 0
           }
       }

       function Intchecks(e) {
           var key = window.event ? e.keyCode : e.which;
           var keychar = String.fromCharCode(key);
           if (((keychar < "0") || (keychar > "9")))
               return false;
           else
               return true;
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
<asp:Label ID="Label1" runat="server"  Text="UOM Mapping Master" CssClass="labelhead"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
<tr style="padding-top:10px">
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">

<tr>
<td align="left" style="width:122px">
 <asp:Label ID="lblitemcode" runat="server" Text="Item Code" CssClass="labelall" 
        Width="60px"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtitemcode" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="Uommaster"></asp:RequiredFieldValidator>
       
 </td>
 <td align="left">
 <table  cellpadding="0" cellspacing="0" border="0">
 <tr>
 <td align="left" >
 <asp:TextBox ID="txtitemcode" runat="server" Width="275px" CssClass="textbox" 
         AutoCompleteType="Disabled" TabIndex="1"></asp:TextBox>
 </td>
 <td align="left" style="padding-left:10px; width:80px" >
   <asp:Label ID="lbldrugcodemain" runat="server" Text="Drug Code" 
         CssClass="labelall" Width="60px"></asp:Label>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
         ControlToValidate="txtdrugcode" ErrorMessage="*" SetFocusOnError="True" 
         ValidationGroup="Uommaster"></asp:RequiredFieldValidator>
 </td>
 <td align="left">
   <asp:TextBox ID="txtdrugcode" runat="server" Width="275px" CssClass="textbox" 
         AutoCompleteType="Disabled" TabIndex="2" ></asp:TextBox>
 </td>

 </tr>
 </table>         
 </td>  
</tr>
<tr>
<td align="left">
 <asp:Label ID="lblitemna" runat="server" Text="Item Name" CssClass="labelall" 
        Width="63px"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtitemnameadd" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="Uommaster"></asp:RequiredFieldValidator>
 </td>
 <td align="left">
 <table cellpadding="0" cellspacing="0" border="0">
 <tr>
 <td align="left">
 <asp:TextBox ID="txtitemnameadd" runat="server" Width="645px" CssClass="textbox" 
         AutoCompleteType="Disabled" TabIndex="3"></asp:TextBox>
 </td>

 </tr>
 </table> 
 </td> 
  <td align="left" style="padding-left:3px">

  <asp:ImageButton ID="btnsearch" runat="server" CssClass="btn" Height="19px"
            ImageUrl="~/ButtonImages/ItemMaster.png"  />
 </td>
</tr>  
</table>
</td>
</tr>
<tr>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td style="vertical-align:top;width:120px">
<asp:Label ID="lblQty" runat="server" Text="Current Quantity" CssClass="labelall" Width="90px"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
        ControlToValidate="txtcurrentqty" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="Uommaster"></asp:RequiredFieldValidator>
</td>
<td style="vertical-align:top" >
<asp:TextBox ID="txtcurrentqty" runat="server" CssClass="textbox" Width="175px" 
        TabIndex="4" MaxLength="10"></asp:TextBox>
</td>
 <td style="padding-left:5px; padding-top:2px; vertical-align:top " align="left" rowspan="4" >
  <table cellpadding="0" cellspacing="0" border="0">  
       <tr>
       <td align="left">         
       <div style="height:90px; overflow:scroll; width: 269px; ">   
  <asp:GridView ID="gridpharloc" runat="server" AutoGenerateColumns="False" ForeColor="#336600"  
          BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
          CellPadding="1" EnableModelValidation="True" Width="245px" CssClass="gridcss" 
               TabIndex="8" >
      <RowStyle BackColor="#EFF3FB"   HorizontalAlign="Left" 
          VerticalAlign="Middle" Wrap="True" />

      <Columns>
         <asp:BoundField HeaderText="Pharmacy Location" DataField="Location_Name" />
         <asp:TemplateField HeaderText="Select">
         <ItemTemplate>
         <asp:CheckBox ID="chkphar" runat="server" TabIndex="36" Checked='<%# chkphar(Eval("Location_Name"))%>'/>
         </ItemTemplate>
         <HeaderStyle Width="65px" />
         <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
         </asp:TemplateField>
      </Columns>
        <FooterStyle BackColor="#169116" Font-Bold="False" ForeColor="#FF8000" />
    <PagerStyle BackColor="#169116" ForeColor="White" HorizontalAlign="Right" />  
    <HeaderStyle BackColor="#169116" Font-Bold="True" ForeColor="White" 
         Wrap="True" HorizontalAlign="Center" />
    <EditRowStyle BackColor="#2461BF"  ForeColor="#CC3300" />
</asp:GridView>     
      </div>
  </td>
  <td style="vertical-align:bottom; padding-left:2px">
    <asp:ImageButton ID="btnclear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" Height="20px" 
          onclick="btnclear_Click" TabIndex="9"  />
  </td>
  <td style="vertical-align:bottom; ">
  <asp:ImageButton ID="btnsave" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Save.png"  
         ValidationGroup="Uommaster" TabIndex="10" Height="20px" 
          onclick="btnsave_Click" EnableTheming="True"  />
  </td>
  <td style="vertical-align:bottom; ">
  <asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Update.png" ValidationGroup="Uommaster" 
         TabIndex="11"  Height="20px" onclick="btnupdate_Click"   />
  </td>
  <td>
  <asp:Button ID="btnok" runat="server" CausesValidation="false" onclick="btnok_Click" style="position:static; display:none;"/>   
  </td>
  </tr> 
  </table>
 </td>
</tr>

<tr>
<td style="vertical-align:top" >
<asp:Label ID="lblcurrentuom" runat="server" Text="Current Uom" CssClass="labelall" Width="90px"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
        ControlToValidate="txtcurrentuom" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="Uommaster"></asp:RequiredFieldValidator>
</td>
<td style="vertical-align:top" >
<asp:TextBox ID="txtcurrentuom" runat="server" CssClass="textbox" Width="175px" 
        BackColor="#E8E8E8" TabIndex="5"></asp:TextBox>
</td>

</tr>
<tr>

<td style="vertical-align:top" >
<asp:Label ID="lblconqty" runat="server" Text="Conversion Quantity" CssClass="labelall" Width="110px"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
        ControlToValidate="txtconversionqty" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="Uommaster"></asp:RequiredFieldValidator>
</td>
<td style="vertical-align:top">
<asp:TextBox ID="txtconversionqty" runat="server" CssClass="textbox" Width="175px" 
        TabIndex="6" MaxLength="10"></asp:TextBox>
</td>
</tr>
<tr>
<td style="vertical-align:top" >
<asp:Label ID="lblconuom" runat="server" Text="Conversion Uom" CssClass="labelall" Width="110px"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
        ControlToValidate="txtconversionuom" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="Uommaster"></asp:RequiredFieldValidator>
</td>
<td style="vertical-align:top" >
<asp:TextBox ID="txtconversionuom" runat="server" CssClass="textbox" Width="175px" 
        TabIndex="7" style= "text-transform:uppercase; " MaxLength="10" ></asp:TextBox>
</td>
</tr>
<tr>
   <td style="width:90px">
    <asp:Label ID="lblactivate" runat="server" CssClass="labelall" Text="Active"></asp:Label>
  </td>
 <td align="left" style="width:10px">
    <asp:CheckBox ID="chkactive" runat="server" TabIndex="20" />
  </td


  
</tr>

</table>
</td>
</tr>

<tr>
  <td align="left">  
  <table width="100%" cellpadding="0" cellspacing="0" border="0">    
  <tr>
  <td align="left">   
        <asp:GridView ID="UomGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="MapID" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left" RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True"          
        AllowPaging="True"  
        CssClass="gridcss"  AllowSorting="True" 
            onselectedindexchanging="UomGrid_SelectedIndexChanging">  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />        
         <Columns>
      
          <asp:CommandField SelectText="Edit" ShowSelectButton="True"  />         
          <asp:BoundField DataField="Location_Name" HeaderText="Pharmacy Location" SortExpression="location_name" />
          <asp:BoundField DataField="Current_Qty" HeaderText="Current Qty" SortExpression="Current_Qty">
            <ItemStyle HorizontalAlign="Center" />
             </asp:BoundField>
          <asp:BoundField DataField="Current_Uom" HeaderText="Current Uom" SortExpression="Current_Uom" >
             <ItemStyle HorizontalAlign="Center" />
             </asp:BoundField>
           <asp:BoundField DataField="Conversion_Qty" HeaderText="Conversion Qty" SortExpression="Conversion_Qty">
            <ItemStyle HorizontalAlign="Center" />
             </asp:BoundField>
             <asp:BoundField DataField="Conversion_Uom" HeaderText="Conversion Uom" SortExpression="Conversion_Uom">
            <ItemStyle HorizontalAlign="Center" />
             </asp:BoundField>
          <asp:BoundField DataField="Status" HeaderText="Active" />
          <asp:BoundField DataField="Created_By" HeaderText="Created by" />
          <asp:BoundField DataField="Created_Date" HeaderText="Created Date Time" />
          <asp:BoundField DataField="Updated_By" HeaderText="Updated by" />
          <asp:BoundField DataField="Updated_Date" HeaderText="Updated Date Time" />
      </Columns>
    <FooterStyle BackColor="#169116" Font-Bold="False" ForeColor="#FF8000" />
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


