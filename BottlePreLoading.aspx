<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="BottlePreLoading.aspx.cs" Inherits="BottlePreLoading" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="manager" runat="server"></asp:ScriptManager>   
  <script language="javascript" type="text/javascript" src="cal/popcalendar.js">
  </script>  
  
  <script language="javascript" type="text/javascript">
      
      function itemsearch() {
          window.open('BottlePreLoadingPopup.aspx', 'search', 'menubar=no,center:yes,toolbar=no,scrollbars=no,width=800,height=500[color=blue]40,top=100,left=200')
          window.close();
      }    

      function doClick(e) {
          var key;

          if (window.event)
              key = window.event.keyCode;     //IE
          else
              key = e.which;     //firefox

          if (key == 13) {
              document.getElementById('<%=btnsave.ClientID%>').click();
              event.keyCode = 0
          }
      }

</script>







  <asp:UpdatePanel ID="upddrug" runat="server">
<ContentTemplate>
<asp:Panel ID="pp" runat="server">
<table width="100%" border="0" cellpadding="0" cellspacing="0">
<tr>
<td id="tdone" runat="server">
<table width="100%" border="0" cellpadding="0" cellspacing="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label2" runat="server" Text="Bottle PreLoading" 
        CssClass="labelhead" ></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
<tr style="padding-top:10px">
<td align="left">
<table align="left" cellpadding="0px" cellspacing="0px" border="0" style=" padding-top:0px; ">

<tr >
<td align="left">
<asp:Label ID="Llblitemcode" runat="server" Text="Item Code" CssClass="labelall" 
        Width="65px" Height="16px"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtitemcode" ErrorMessage="*" ValidationGroup="preloading"></asp:RequiredFieldValidator>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtitemcode" runat="server" Width="240px" 
        CssClass="textbox" AutoCompleteType="Disabled" 
        TabIndex="1" BackColor="#E8E8E8" ReadOnly="True"></asp:TextBox>       
</td>
<td align="left" style="padding-left:3px">
<asp:Label ID="lbldrugcode" runat="server" CssClass="labelall" Text="Drug Code" 
  Width="65px"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
  ControlToValidate="txtdrugcode" ErrorMessage="*" ValidationGroup="preloading"></asp:RequiredFieldValidator>
</td>
<td align="left">
<asp:TextBox ID="txtdrugcode" runat="server" Width="210px" CssClass="textbox" 
        AutoCompleteType="Disabled" TabIndex="1" BackColor="#E8E8E8" 
        ReadOnly="True"></asp:TextBox>

</td>
</tr>
</table>      
</td>

<td>
  <asp:ImageButton ID="BtnSearch" runat="server" ValidationGroup="enter"
            ImageUrl="~/ButtonImages/Search.png"  Height="22px"/>
</td>
<td align="left">
<asp:TextBox ID="txtitemname" runat="server" CausesValidation="false" Style="position: static; display: none"/>  
</td>
<td align="left">
<asp:TextBox ID="searchvalue" runat="server" CausesValidation="false" Style="position: static; display: none"/>  
<asp:TextBox ID="txtcarttype" runat="server" CausesValidation="false" Style="position: static; display: none"/> 
<asp:TextBox ID="txt_Box_Or_Pallet" runat="server" CausesValidation="false" Style="position: static; display: none"/> 
</td>
<td>
<asp:TextBox ID="txtserdrgcode" runat="server" CausesValidation="false" Style="position: static; display: none"/>   
</td>
<td align="left">      
<asp:TextBox ID="txtmfrsearch" runat="server" CausesValidation="false" Style="position: static; display: none"/>        
</td>
<td align="left">
 <asp:Button ID="btnok" runat="server" CausesValidation="False" OnClick="btnok_Click" Style="position: static; display: none" Text="Ok" />           
</td> 
</tr>
<tr>
<td align="left" width="100px" >
<asp:Label ID="lbldrg" runat="server" Text="Item Name" CssClass="labelall" 
        Width="65px"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="txtdrugname" ErrorMessage="*" 
        ValidationGroup="preloading"></asp:RequiredFieldValidator>
</td>
<td align="left" >
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left">
<asp:TextBox ID="txtdrugname" runat="server" Width="535px" CssClass="textboxbigfont" 
        AutoCompleteType="Disabled" TabIndex="1" BackColor="#E8E8E8" 
        ReadOnly="True"></asp:TextBox>  
</td>

</tr>
</table>
</td>
</tr>

<tr>
<td align="left" width="100px" >
<asp:Label ID="lblbran" runat="server" Text="Brand Name" CssClass="labelall" 
        Width="70px"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
        ControlToValidate="txtbrand" ErrorMessage="*" ValidationGroup="preloading"></asp:RequiredFieldValidator>
</td>
<td align="left">
<table width="100%"  cellpadding="0px" cellspacing="0px" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtbrand" runat="server" Width="535px" CssClass="textboxbigfont" 
        AutoCompleteType="Disabled" TabIndex="1" BackColor="#E8E8E8" 
        ReadOnly="True"></asp:TextBox>
<asp:TextBox ID="txtbrandsear" runat="server" CausesValidation="false" Style="position: static; display: none"/> 
</td>

</tr>
</table>
</td>
</tr>

<tr>
<td align="left">
<asp:Label ID="lblpacktype" runat="server" Text="Pack Type" CssClass="labelall" 
        Width="99px"></asp:Label>   
</td>
<td align="left">
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left" width="120px" >
<asp:TextBox ID="txtpacktype" runat="server" Width="130px" ReadOnly="True" 
         BackColor="#E8E8E8" CssClass="textboxbigfont" AutoCompleteType="Disabled" 
        TabIndex="1" ></asp:TextBox>
</td>
<td align="left" width="80px" style="padding-left:10px;">
<asp:Label ID="lblpacksize" runat="server" Text="Pack Size" CssClass="labelall" 
        Width="65px"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
        ControlToValidate="txtpacksize" ErrorMessage="*" ValidationGroup="preloading"></asp:RequiredFieldValidator>         
</td>
<td align="left" width="110px">
 <asp:TextBox ID="txtpacksize" runat="server" Width="115px" ReadOnly="True" 
        BackColor="#E8E8E8" CssClass="textboxbigfont" AutoCompleteType="Disabled" TabIndex="1"></asp:TextBox>
</td>
<td align="left" width="50px" style="padding-left:5px;">
<asp:Label ID="lbluom" runat="server" Text=" UOM" CssClass="labelall" 
        Width="35px" ></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
        ControlToValidate="txtuom" ErrorMessage="*" ValidationGroup="preloading"></asp:RequiredFieldValidator>         
</td>
<td align="left" width="120px">
<asp:TextBox ID="txtuom" runat="server" Width="130px" ReadOnly="True" 
        BackColor="#E8E8E8" CssClass="textboxbigfont" AutoCompleteType="Disabled" 
        TabIndex="1" ></asp:TextBox>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td>
<asp:Label ID="Label4" runat="server" Text=" Carton Box / Pallet type" CssClass="labelall" 
        Width="170px" ></asp:Label>
</td>
<td align="left">
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left" width="130px">
<asp:TextBox ID="TxtBoxOrPallet" runat="server" Width="130px" ReadOnly="True" 
         BackColor="#E8E8E8" CssClass="textboxbigfont" AutoCompleteType="Disabled" 
        TabIndex="1" ></asp:TextBox>
</td>

</tr>
</table>
</td>

</tr>
<tr>
<td>
<asp:Label ID="Label1" runat="server" Text=" Carton Box / Pallet Count Of" CssClass="labelall" 
        Width="170px" ></asp:Label>
</td>
<td align="left">
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left" width="130px">
<asp:TextBox ID="txtcartonbox" runat="server" Width="130px" ReadOnly="True" 
         BackColor="#E8E8E8" CssClass="textboxbigfont" AutoCompleteType="Disabled" 
        TabIndex="1" ></asp:TextBox>
</td>
<td align="left" style="padding-left:5px;">
<asp:Label ID="lblcartboxof" runat="server" Text="" CssClass="labelall" Width="200px"></asp:Label>
</td>
</tr>
</table>
</td>

</tr>
<tr>
<td align="left">
<asp:Label ID="lblmaxcart" runat="server" Text="Maximum  Quantity Per Cartridge" 
        CssClass="labelall" Width="180px"></asp:Label>
</td>
<td align="left">
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left" width="130px">
<asp:TextBox ID="txtmaxcartqty" runat="server" Width="130px" ReadOnly="True" 
        BackColor="#E8E8E8" CssClass="textbox" AutoCompleteType="Disabled" TabIndex="1"></asp:TextBox>
</td>
<td align="left" style="padding-left:5px;">
<asp:Label ID="lblmax" runat="server" CssClass="labelall" Width="200px"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>



<tr>
<td align="left" >
<asp:Label ID="lblexpdate" runat="server" Text="Expiry Date" CssClass="labelall" 
        Height="16px" Width="65px"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
        ControlToValidate="txtexpdate" ErrorMessage="*" ValidationGroup="preloading"></asp:RequiredFieldValidator>  
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
        ErrorMessage="*" ControlToValidate="txtexpdate"        
        ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" 
        SetFocusOnError="True" Display="Dynamic" ValidationGroup="preloading"></asp:RegularExpressionValidator>
    </td>
<td align="left">
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left" width="75px">
 <asp:TextBox ID="txtexpdate" runat="server" Width="85px" CssClass="textbox" AutoCompleteType="Disabled" ></asp:TextBox> 
</td>
<td align="left" width="35px">
  <asp:image id="imgCalendar" runat="server" ImageUrl="~/cal/calendar.gif"></asp:image>
</td>
<td align="left" width="80px" style="padding-left:10px;">
 <asp:Label ID="lblbatchno" runat="server" Text="Batch No  " CssClass="labelall" 
        Width="65px" ></asp:Label>  
 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
        ControlToValidate="txtbatchno" ErrorMessage="*" ValidationGroup="preloading"></asp:RequiredFieldValidator>         
</td>
<td align="left" width="120px">
 <asp:TextBox ID="txtbatchno" runat="server" Width="110px" ReadOnly="False" 
        CssClass="textbox" AutoCompleteType="Disabled" MaxLength="20" ></asp:TextBox>
</td>
</tr>

</table> 
</td>
</tr>
<tr>
<td>
 <asp:Label ID="Label3" runat="server" Text="Printer Name  " CssClass="labelall" 
        Width="111px" ></asp:Label>  
</td>
<td align="left">
<asp:DropDownList ID="ddlprintername" runat="server" Width="540px" CssClass="textbox" >  
    </asp:DropDownList>
</td>
</tr>
<%--<tr>
<td align="left" width="80px" >
 <asp:Label ID="Label3" runat="server" Text="BDS Name" CssClass="labelall" 
        Width="65px" ></asp:Label>  
 <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
        ControlToValidate="ddlbdsname" Display="Dynamic" ErrorMessage="*" 
        SetFocusOnError="True" ValidationGroup="save" InitialValue="- Select -"></asp:RequiredFieldValidator>      
</td>
<td align="left">
<asp:DropDownList ID="ddlbdsname" runat="server" Width="130px" CssClass="textbox" >   
    <asp:ListItem>- Select -</asp:ListItem>
    </asp:DropDownList>
</td>
</tr>--%>

<tr style="padding-top:20px">
<td colspan="2" align="right">


 <asp:ImageButton ID="btnclear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png"  Height="20px" 
        onclick="btnclear_Click"  />
 <asp:ImageButton ID="btnsave" runat="server" CssClass="btn" ValidationGroup="preloading"
            ImageUrl="~/ButtonImages/SavePrint.png"  Height="20px" 
        onclick="btnsave_Click"  />            
   <asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" ValidationGroup="preloading" 
            ImageUrl="~/ButtonImages/SavePrint.png"  Height="20px" 
        onclick="btnupdate_Click"  />  
   <asp:ImageButton ID="btncancel" runat="server" CssClass="btn" ValidationGroup="preloading" 
            ImageUrl="~/ButtonImages/Cancel.png"  Height="20px" 
        onclick="btncancel_Click"  />                     
</td>
</tr>



</td>
</tr>
</table>
<tr>
<td id="tdthree" runat="server"> 
<table align="left" width="100%">   
<tr>
<td align="left">
<asp:GridView ID="GridPreloaded" runat="server" AutoGenerateColumns="False" DataKeyNames="CartID" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" AllowPaging="True" 
        BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" CssClass="gridcss" 
         AllowSorting="True" PageSize="25" 
        onselectedindexchanging="GridPreloaded_SelectedIndexChanging" 
        onpageindexchanging="GridPreloaded_PageIndexChanging" 
        onsorting="GridPreloaded_Sorting"> 
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns>
 
        <asp:CommandField ShowSelectButton="True" SelectText="Edit" />   

        <asp:BoundField DataField="Cart_Barcode" HeaderText="Carton Barcode" SortExpression="Cart_Barcode" />      
        <asp:BoundField DataField="Item_Code" HeaderText="Item Code" SortExpression="Item_Code" />
        <asp:BoundField DataField="Item_Name" HeaderText="Item Name" SortExpression="Item_Name" />
        <asp:BoundField DataField="BrandName" HeaderText="Brand Name" SortExpression="BrandName" />
         <asp:BoundField DataField="BoxOrPallet" HeaderText="Carton Box / Pallet type" SortExpression="BoxOrPallet">
          <ItemStyle HorizontalAlign="Center" Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="Cart_Type" HeaderText="Carton Box / Pallet Bottle Count" SortExpression="Cart_Type">
          <ItemStyle HorizontalAlign="Center" Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="PackType" HeaderText="Pack Type" SortExpression="PackType" />
        <asp:BoundField DataField="Pack_Size" HeaderText="Pack Size" SortExpression="Pack_Size" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="UOM" HeaderText="UOM" SortExpression="UOM" />
        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
        <asp:BoundField DataField="Verified_Status" HeaderText="Verified Status" SortExpression="Verified_Status" />
        <asp:BoundField DataField="Reason" HeaderText="Reason" SortExpression="Reason" />
        <asp:BoundField DataField="Loaded_by" HeaderText="PreLoaded by" SortExpression="Loaded_by" />
        <asp:BoundField DataField="Loaded_Dat" HeaderText="PreLoaded Date Time" SortExpression="Loaded_Dat" />    
        <asp:BoundField DataField="Updatedby" HeaderText="Updated by" SortExpression="Loaded_by" />
        <asp:BoundField DataField="Upd_Dat" HeaderText="Updated Date Time" SortExpression="Loaded_Dat" />         
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
<td>
<asp:Label ID="lblpge" runat="server" CssClass="labelall"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>

</td> 
</tr> 

<tr>
     <td id="tdtwo" runat="server" style="padding-top:70px" >
     <table   align="left" cellpadding="0" cellspacing="0" border="0">
     <tr>
     <td align="left">
     <asp:Label ID="lblempno" runat="server" CssClass="labelallbigfont" Text="Scan NRIC / Staff ID / Pass" Width="250px"></asp:Label>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
        ControlToValidate="txtempid" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="empid"></asp:RequiredFieldValidator>
     </td>
   
     <td>
     <asp:TextBox ID="txtempid" runat="server" CssClass="textboxbigfont" Width="350px" 
             Height="30px" AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox>
     </td>
   <td>
 
 <asp:ImageButton ID="btnempenter" runat="server" CssClass="btn" ValidationGroup="empid"
            ImageUrl="~/ButtonImages/Enter.png"  Height="40px" 
           onclick="btnempenter_Click"/>
   </td>
      <td>

<asp:ImageButton ID="btnempenterupdate" runat="server" CssClass="btn" ValidationGroup="empid"
            ImageUrl="~/ButtonImages/Enter.png"  Height="40px" 
              onclick="btnempenterupdate_Click" />
   </td>
         <td>

<asp:ImageButton ID="BtnempenterCancel" runat="server" CssClass="btn" ValidationGroup="empid"
            ImageUrl="~/ButtonImages/Enter.png"  Height="40px" 
                 onclick="BtnempenterCancel_Click" />
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

