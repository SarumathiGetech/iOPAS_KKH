<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Preloading.aspx.cs" Inherits="Preloading" Title="Untitled Page" %>
<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <asp:ScriptManager ID="manager" runat="server"></asp:ScriptManager>   
    <script language="javascript" type="text/javascript" src="cal/popcalendar.js"></script>  
    <script language="javascript" type="text/javascript">

        function Preloadcartridge() {
            window.open('Preloadpopup.aspx', 'search', 'menubar=no,center:yes,toolbar=no,scrollbars=no,width=800,height=500[color=blue]40,top=100,left=200');
            window.close();
        }

        function preload() {
            window.open('popuppreload.aspx', 'search', 'menubar=no,center:yes,toolbar=no,scrollbars=no,width=750,height=500[color=blue]40,top=100,left=200');
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


       function doClick1(e) {
           var key;

           if (window.event)
               key = window.event.keyCode;     //IE
           else
               key = e.which;     //firefox

           if ((key == 13) || (key == 9)) {
               document.getElementById('<%=Btngo.ClientID%>').click();
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
<asp:Label ID="Label2" runat="server" Text="Cartridge PreLoading" CssClass="labelhead" ></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
<tr style="padding-top:10px">
<td align="left">
<table align="left" cellpadding="0px" cellspacing="0px" border="0" style=" padding-top:0px; ">


<tr>
<td align="left" width="100px">
<asp:Label ID="lblcartno" runat="server" Text="Cartridge No" CssClass="labelall" 
        Width="75px"></asp:Label>    
    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
        ControlToValidate="txtcartno" ErrorMessage="*" ValidationGroup="preloading"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
        ControlToValidate="txtcartno" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="enter"></asp:RequiredFieldValidator>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtcartno" runat="server" Width="240px"  MaxLength="6" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td align="left">
  <asp:ImageButton ID="Btngo" runat="server" ValidationGroup="enter"
            ImageUrl="~/ButtonImages/Enter24.png" onclick="Btngo_Click" Height="22px"/>
</td>
<td align="left">
 <asp:Button ID="btnok" runat="server" CausesValidation="False" OnClick="btnok_Click" Style="position: static; display: none" Text="Ok" />           
</td> 
<td align="left">
 <asp:TextBox ID="searchvalue" runat="server" CausesValidation="false" Style="position: static; display: none"/>  
</td>
<td align="left">
 <asp:TextBox ID="txtdatval" runat="server" CausesValidation="false" Style="position: static; display: none"/>
</td> 
<td align="left">
 <asp:TextBox ID="txtitcodesearch" runat="server" CausesValidation="false" Style="position: static; display: none"/>  
</td>
 <td align="left">
 <asp:TextBox ID="txtexpdate1" runat="server" CausesValidation="false" Style="position: static; display: none" AutoCompleteType="Disabled"/>
</td> 
<td align="left">
 <asp:TextBox ID="txtbatchno1" runat="server" CausesValidation="false" Style="position: static; display: none"  AutoCompleteType="Disabled"/>  
</td>
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
        ControlToValidate="txtitemcode" ErrorMessage="*" ValidationGroup="preloading"></asp:RequiredFieldValidator>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtitemcode" runat="server" Width="240px" BackColor="#E8E8E8" 
        CssClass="textbox" AutoCompleteType="Disabled" ReadOnly="True" 
        TabIndex="1"></asp:TextBox>       
</td>
<td align="left" style="padding-left:3px">
<asp:Label ID="lbldrugcode" runat="server" CssClass="labelall" Text="Drug Code" 
  Width="65px"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
  ControlToValidate="txtdrugcode" ErrorMessage="*" ValidationGroup="preloading"></asp:RequiredFieldValidator>
</td>
<td align="left">
<asp:TextBox ID="txtdrugcode" runat="server" Width="210px" ReadOnly="True" 
        BackColor="#E8E8E8" CssClass="textbox" AutoCompleteType="Disabled" TabIndex="1"></asp:TextBox>
<asp:TextBox ID="txtserdrgcode" runat="server" CausesValidation="false" Style="position: static; display: none"/>   
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
        ControlToValidate="txtdrug" ErrorMessage="*" ValidationGroup="preloading"></asp:RequiredFieldValidator>
</td>
<td align="left" >
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left">
<asp:TextBox ID="txtdrug" runat="server" Width="535px" ReadOnly="true" 
        BackColor="#E8E8E8" CssClass="textboxbigfont" AutoCompleteType="Disabled" TabIndex="1"></asp:TextBox>  
</td>
<td align="left">
<asp:TextBox ID="txtitemname" runat="server" CausesValidation="false" Style="position: static; display: none"/>  
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
<asp:TextBox ID="txtbrand" runat="server" Width="535px" ReadOnly="True" 
        BackColor="#E8E8E8" CssClass="textboxbigfont" AutoCompleteType="Disabled" TabIndex="1"></asp:TextBox>
<asp:TextBox ID="txtbrandsear" runat="server" CausesValidation="false" Style="position: static; display: none"/> 
</td>
<td align="left">      
<asp:TextBox ID="txtmfrsearch" runat="server" CausesValidation="false" Style="position: static; display: none"/>        
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
<td align="left">
<asp:Label ID="lblmaxcart" runat="server" Text="Maximum  Quantity Per Cartridge" 
        CssClass="labelall" Width="180px"></asp:Label>
</td>
<td align="left">
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left" width="120px">
<asp:TextBox ID="txtmaxcartqty" runat="server" Width="120px" ReadOnly="True" 
        BackColor="#E8E8E8" CssClass="textbox" AutoCompleteType="Disabled" TabIndex="1"></asp:TextBox>
</td>
<td align="left" style="padding-left:10px;">
<asp:Label ID="lblmax" runat="server" CssClass="labelall" Width="200px"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left" >
<asp:Label ID="lblqty" runat="server" Text="Quantity" CssClass="labelall" 
        Width="65px" Height="22px"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
        ControlToValidate="txtqty" ErrorMessage="*" ValidationGroup="preloading"></asp:RequiredFieldValidator>
<asp:RangeValidator ID="RangeValidator1" runat="server" 
        ControlToValidate="txtqty" ErrorMessage="*" MaximumValue="999" MinimumValue="0" 
        Type="Integer" ValidationGroup="preloading"></asp:RangeValidator>          
</td>
<td align="left">
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left" width="120px">
<asp:TextBox ID="txtqty" runat="server" Width="120px" ReadOnly="False" 
        CssClass="textbox" AutoCompleteType="Disabled" MaxLength="3"></asp:TextBox>
</td>
<td align="left" style="padding-left:10px">
<asp:Label ID="lblload" runat="server" CssClass="labelall" Width="200px"></asp:Label>
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
<td colspan="2" align="right">
<%--<asp:Button ID="btn" runat="server" Text="Clear" Height="23px" 
        onclick="btn_Click" CssClass="btn" />
<asp:Button ID="btnsave" runat="server" Text="Save" Height="23px" 
        onclick="btnsave_Click" ValidationGroup="preloading" CssClass="btn" />
<asp:Button ID="btnupdate" runat="server" Text="Save" Height="23px" 
        onclick="btnupdate_Click" ValidationGroup="preloading" CssClass="btn" />
--%>
 <asp:ImageButton ID="btn" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btn_Click" Height="20px"  />
 <asp:ImageButton ID="btnsave" runat="server" CssClass="btn" ValidationGroup="preloading"
            ImageUrl="~/ButtonImages/Save.png" onclick="btnsave_Click" Height="20px"  />            
   <asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" ValidationGroup="preloading" 
            ImageUrl="~/ButtonImages/Save.png" onclick="btnupdate_Click" Height="20px"  />          
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td id="tdtwo" runat="server">
<asp:UpdatePanel ID="test" runat="server">
<ContentTemplate>
<table align="left">
<tr>
<td>
<asp:Button ID="btnpreloadsearch" runat="server" Text="PreLoaded Cartridge Search" 
        Height="23px" Width="193px" onclick="btnpreloadsearch_Click" 
        CssClass="btn" Visible="False"/>
<asp:Button ID="btnpresearch" runat="server" CausesValidation="False" OnClick="btnpresearch_Click" Style="position: static;display: none" Text="Ok" />                   
<asp:TextBox ID="txtfilter" runat="server" CausesValidation="false" Style="position: static; display: none"/> 
</td> 
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel> 
</td>
</tr>
<tr>
<td id="tdthree" runat="server"> 
<table align="left" width="100%">   
<tr>
<td align="left">
<asp:GridView ID="gridreject" runat="server" AutoGenerateColumns="False" DataKeyNames="Loading_ID" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" AllowPaging="True" 
        BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        onselectedindexchanging="gridreject_SelectedIndexChanging" 
        onpageindexchanging="gridreject_PageIndexChanging" CssClass="gridcss" 
        onsorting="gridreject_Sorting" AllowSorting="True" PageSize="20"> 
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns>
        <asp:CommandField ShowSelectButton="True" SelectText="Edit" />
        <asp:BoundField DataField="Loading_ID" Visible="false"/>
           <%-- <ItemStyle ForeColor="White" Font-Size="XX-Small" />
        </asp:BoundField>--%>
        <asp:BoundField DataField="Cartridge_Id" HeaderText="Cartridge No" SortExpression="Cartridge_Id" />
        <asp:BoundField DataField="Item_Code" HeaderText="Item Code" SortExpression="Item_Code" />
        <asp:BoundField DataField="Item_Name" HeaderText="Item Name" SortExpression="Item_Name" />
        <asp:BoundField DataField="BrandName" HeaderText="Brand Name" SortExpression="BrandName" />
        <asp:BoundField DataField="PackType" HeaderText="Pack Type" SortExpression="PackType" />
        <asp:BoundField DataField="Pack_Size" HeaderText="Pack Size" SortExpression="Pack_Size" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="UOM" HeaderText="UOM" SortExpression="UOM" />
        <asp:BoundField DataField="Verified_Status" HeaderText="Status" SortExpression="Verified_Status" />
        <asp:BoundField DataField="Loaded_by" HeaderText="PreLoaded by" SortExpression="Loaded_by" />
        <asp:BoundField DataField="Loaded_Date" HeaderText="PreLoaded Date Time" SortExpression="Loaded_Date" />
        <%--<asp:BoundField DataField="rejected_by" HeaderText="Rejected by" />
        <asp:BoundField DataField="Rejected_Date" HeaderText="Rejected Date Time" />--%>
        <asp:BoundField DataField="Reason" HeaderText="Reason" SortExpression="Reason" />        
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

   <tr>
     <td id="tdfour" runat="server" style="padding-top:70px" >
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
  <%-- <asp:Button ID="btnempenter" runat="server" Text="Enter" CssClass="btnbigfont" 
           onclick="btnempenter_Click" Height="35px" Width="100px" 
           ValidationGroup="empid"/>--%>
 <asp:ImageButton ID="btnempenter" runat="server" CssClass="btn" ValidationGroup="empid"
            ImageUrl="~/ButtonImages/Enter.png" onclick="btnempenter_Click" Height="40px"  />
   </td>
      <td>
<%--   <asp:Button ID="btnempenterupdate" runat="server" Text="Enter" CssClass="btnbigfont" 
            Height="35px" Width="100px" onclick="btnempenterreject_Click" 
              ValidationGroup="empid"/>--%>
<asp:ImageButton ID="btnempenterupdate" runat="server" CssClass="btn" ValidationGroup="empid"
            ImageUrl="~/ButtonImages/Enter.png" onclick="btnempenterreject_Click" Height="40px"  />
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