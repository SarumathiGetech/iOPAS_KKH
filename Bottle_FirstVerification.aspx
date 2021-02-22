<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Bottle_FirstVerification.aspx.cs" Inherits="Bottle_FirstVerification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="manager" runat="server"></asp:ScriptManager>   
  <script language="javascript" type="text/javascript" src="cal/popcalendar.js">
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
<asp:Label ID="Label2" runat="server" Text="Bottle First Verification" 
        CssClass="labelhead" ></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
<tr style="padding-top:10px">
<td align="left">
<table align="left" cellpadding="0px" cellspacing="0px" border="0" style=" padding-top:0px; ">
<tr>
<td align="left">
<asp:Label ID="lblcartbarcode" runat="server" Text="Carton Barcode" CssClass="labelall" 
        Width="115px" Height="16px"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
        ControlToValidate="txtcartbarcode" ErrorMessage="*" 
        ValidationGroup="Enter"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
        ControlToValidate="txtcartbarcode" ErrorMessage="*" 
        ValidationGroup="FirstVerified"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
        ControlToValidate="txtcartbarcode" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="reject"></asp:RequiredFieldValidator>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtcartbarcode" runat="server" Width="240px" 
        CssClass="textbox" AutoCompleteType="Disabled" 
        TabIndex="1" ></asp:TextBox>       
</td>
<td align="left" style="padding-left:3px">
  <asp:ImageButton ID="Btngo" runat="server"  ValidationGroup="Enter"
            ImageUrl="~/ButtonImages/Enter24.png" Height="22px" 
        onclick="Btngo_Click"  />
</td>

</tr>
</table>      
</td>
</tr>

<tr>
<td align="left">
<asp:Label ID="Llblitemcode" runat="server" Text="Item Code" CssClass="labelall" 
        Width="65px" Height="16px"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtitemcode" ErrorMessage="*" 
        ValidationGroup="FirstVerified"></asp:RequiredFieldValidator>

<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
        ControlToValidate="txtitemcode" ErrorMessage="*" 
        ValidationGroup="reject"></asp:RequiredFieldValidator>
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
  ControlToValidate="txtdrugcode" ErrorMessage="*" ValidationGroup="FirstVerified"></asp:RequiredFieldValidator>
</td>
<td align="left">
<asp:TextBox ID="txtdrugcode" runat="server" Width="210px" CssClass="textbox" 
        AutoCompleteType="Disabled" TabIndex="1" BackColor="#E8E8E8" 
        ReadOnly="True"></asp:TextBox>

</td>
</tr>
</table>      
</td>
</tr>
<tr>
<td align="left" width="100px" >
<asp:Label ID="lbldrg" runat="server" Text="Item Name" CssClass="labelall" 
        Width="65px"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="txtdrugname" ErrorMessage="*" 
        ValidationGroup="FirstVerified"></asp:RequiredFieldValidator>
</td>
<td align="left" >
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left">
<asp:TextBox ID="txtdrugname" runat="server" Width="535px" CssClass="textboxbigfont" 
        AutoCompleteType="Disabled" TabIndex="1" BackColor="#E8E8E8" 
        ReadOnly="True"></asp:TextBox>  
</td>
<td align="left" style="padding-left:2px">
<input type="checkbox" id="chkitemname"  style="width:1.5em; height:1.5em;" runat="server"/>
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
        ControlToValidate="txtbrand" ErrorMessage="*" 
        ValidationGroup="FirstVerified"></asp:RequiredFieldValidator>
</td>
<td align="left">
<table cellpadding="0px" cellspacing="0px" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtbrand" runat="server" Width="535px" CssClass="textboxbigfont" 
        AutoCompleteType="Disabled" TabIndex="1" BackColor="#E8E8E8" 
        ReadOnly="True"></asp:TextBox>

</td>
<td align="left" style="padding-left:2px">
<input type="checkbox" id="chkbrandname"  style="width:1.5em; height:1.5em;" runat="server"/>
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
<td align="left" style="padding-left:2px">
<input type="checkbox" id="chkpacktype"  style="width:1.5em; height:1.5em;" runat="server"/>
</td>
<td align="left" width="80px" style="padding-left:10px;">
<asp:Label ID="lblpacksize" runat="server" Text="Pack Size" CssClass="labelall" 
        Width="65px"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
        ControlToValidate="txtpacksize" ErrorMessage="*" 
        ValidationGroup="FirstVerified"></asp:RequiredFieldValidator>         
</td>
<td align="left" width="60px">
 <asp:TextBox ID="txtpacksize" runat="server" Width="70px" ReadOnly="True" 
        BackColor="#E8E8E8" CssClass="textboxbigfont" AutoCompleteType="Disabled" TabIndex="1"></asp:TextBox>
</td>
<td align="left" style="padding-left:2px">
<input type="checkbox" id="chkpacksize"  style="width:1.5em; height:1.5em;" runat="server"/>
</td>
<td align="left" width="50px" style="padding-left:5px;">
<asp:Label ID="lbluom" runat="server" Text=" UOM" CssClass="labelall" 
        Width="35px" ></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
        ControlToValidate="txtuom" ErrorMessage="*" 
        ValidationGroup="FirstVerified"></asp:RequiredFieldValidator>         
</td>
<td align="left" width="120px">
<asp:TextBox ID="txtuom" runat="server" Width="126px" ReadOnly="True" 
        BackColor="#E8E8E8" CssClass="textboxbigfont" AutoCompleteType="Disabled" 
        TabIndex="1" ></asp:TextBox>
</td>
<td align="left" style="padding-left:2px">
<input type="checkbox" id="chkuom"  style="width:1.5em; height:1.5em;" runat="server"/>
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
<td align="left" style="padding-left:2px">
<input type="checkbox" id="chkboxOrPallet"  style="width:1.5em; height:1.5em;" runat="server"/>
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
<td align="left" style="padding-left:2px">
<input type="checkbox" id="chkcartonboxcount"  style="width:1.5em; height:1.5em;" runat="server"/>
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
        ControlToValidate="txtexpdate" ErrorMessage="*" 
        ValidationGroup="FirstVerified"></asp:RequiredFieldValidator>  
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
        ErrorMessage="*" ControlToValidate="txtexpdate"        
        ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" 
        SetFocusOnError="True" Display="Dynamic" ValidationGroup="FirstVerified"></asp:RegularExpressionValidator>
    </td>
<td align="left">
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left" width="130px">
 <asp:TextBox ID="txtexpdate" runat="server" Width="130px" ReadOnly="True" 
        BackColor="#E8E8E8" CssClass="textbox" AutoCompleteType="Disabled" ></asp:TextBox> 
</td>
<td align="left" style="padding-left:2px">
<input type="checkbox" id="chkexpiry"  style="width:1.5em; height:1.5em;" runat="server"/>
</td>

<td align="left" width="80px" style="padding-left:10px;">
 <asp:Label ID="lblbatchno" runat="server" Text="Batch No  " CssClass="labelall" 
        Width="65px" ></asp:Label>  
 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
        ControlToValidate="txtbatchno" ErrorMessage="*" 
        ValidationGroup="FirstVerified"></asp:RequiredFieldValidator>         
</td>
<td align="left" width="120px">
 <asp:TextBox ID="txtbatchno" runat="server" Width="110px"
        ReadOnly="True" BackColor="#E8E8E8" CssClass="textbox" AutoCompleteType="Disabled" MaxLength="20" ></asp:TextBox>
</td>
<td align="left" style="padding-left:2px">
<input type="checkbox" id="chkbatchno"  style="width:1.5em; height:1.5em;" runat="server"/>
</td>
</tr>

</table> 
</td>
</tr>

<tr>
<td align="left" width="108px">
<asp:Label ID="lbllodedby" runat="server" Text="PreLoaded By" CssClass="labelall" 
        Width="100px"></asp:Label>
</td>
<td align="left">
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left" width="205px">
<asp:TextBox ID="txtlodedby" runat="server" Width="326px" ReadOnly="false" 
        BackColor="#E8E8E8" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td align="left" width="65px" style="padding-left:5px">
<asp:Label ID="lbllodeddate" runat="server" Text="Date Time" CssClass="labelall" 
        Width="65px"></asp:Label>
</td>
<td align="left" width="140px">
<asp:TextBox ID="txtdate" runat="server" Width="140px" ReadOnly="True" 
        BackColor="#E8E8E8" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox> 
</td>
</tr>
</table>           
</td>
</tr>

<tr>
<td align="left" width="140px">
<asp:Label ID="Lblreason" runat="server" Text="Reason For Reject" 
        CssClass="labelall" Width="105px" ></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
        ErrorMessage="*" ControlToValidate="ddlreason" InitialValue="-Blank-" 
        SetFocusOnError="True" ValidationGroup="reject" Display="Dynamic"></asp:RequiredFieldValidator>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:DropDownList ID="ddlreason" runat="server" Width="330px" CssClass="textbox" 
        AutoPostBack="True"  >
    <asp:ListItem>-Blank-</asp:ListItem>
    </asp:DropDownList>
</td>
<td style="padding-left:60px; padding-top:5px"> 

 <asp:ImageButton ID="btnclear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png"  Height="20px" 
        onclick="btnclear_Click"/>
 <asp:ImageButton ID="btnsave" runat="server" CssClass="btn" ValidationGroup="FirstVerified"
            ImageUrl="~/ButtonImages/Accept.png"  Height="20px" 
        onclick="btnsave_Click"/>            
   <asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" ValidationGroup="reject" 
            ImageUrl="~/ButtonImages/Reject.png"  Height="20px" 
        onclick="btnupdate_Click"/>  

</td>
</tr>
</table>
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
<asp:GridView ID="GridFstVerified" runat="server" AutoGenerateColumns="False" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" AllowPaging="True" 
        BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" CssClass="gridcss" 
         AllowSorting="True" PageSize="25"> 
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns>      

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
        <asp:BoundField DataField="Verified_Status" HeaderText="Verified Status" SortExpression="Verified_Status"/>
        <asp:BoundField DataField="Loaded_by" HeaderText="PreLoaded by" SortExpression="Loaded_by" />
        <asp:BoundField DataField="Loaded_Dat" HeaderText="PreLoaded Date Time" SortExpression="Loaded_Dat" />  
           <asp:BoundField DataField="First_Verified" HeaderText="First Verified by" SortExpression="First_Verified" />
        <asp:BoundField DataField="Fst_Dat" HeaderText="First Verified Date Time" SortExpression="Fst_Dat" />  
               
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

<asp:ImageButton ID="btnempenterreject" runat="server" CssClass="btn" ValidationGroup="empid"
            ImageUrl="~/ButtonImages/Enter.png"  Height="40px" 
              onclick="btnempenterreject_Click"/>
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



