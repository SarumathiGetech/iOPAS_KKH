<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Drugmaster.aspx.cs" Inherits="Drugmaster" EnableEventValidation="false" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">   
</asp:Content>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="scrip1" runat="server"></asp:ScriptManager>

   <script language="javascript" type="text/javascript">

//       var openWindow = new Array();
//       function trackOpen(winName) {
//           openWindow[openWindow.length] = winName;
//       }
//       function closeWindows() {
//           var openCount = openWindow.length;
//           for (r = 0; r < openCount; r++) {
//               openWindow[r].close();
//           }
//       }

       function brand() {
           var icode = document.getElementById('<%=txtitemcode.ClientID%>').value;           
           var iname = document.getElementById('<%=txtitemnameadd.ClientID%>').value;  
                   
           if (icode != '' & iname != '') {
               window.open(("brandmaster.aspx?itcode=" + icode + "&itname=" + iname), 'search', 'menubar=no,center:yes,toolbar=no,scrollbars=no,width=820,height=600[color=blue]40,top=100,left=200')
               window.close();
           }
           else
               alert('Item code and Item Name should not be blank');
       }

       function itemsearch() {
           var icode = document.getElementById('<%=txtitemcode.ClientID%>').value;
           var dcode = document.getElementById('<%=txtdrugcode.ClientID%>').value;
           var iname = document.getElementById('<%=txtitemnameadd.ClientID%>').value;
           window.open(("Itemmastersearch.aspx?drcode=" + dcode + " &itcode=" + icode + " &itname=" + iname), 'search', 'menubar=no, toolbar=no,center:yes,scrollbars=no,width=720,height=500[color=blue]40,top=100,left=200')
       }

       function uomcalc() {
           var pack = document.getElementById('<%=txtpacksize.ClientID%>').value;
           if (pack != '') {
               document.getElementById('<%=btnuom.ClientID%>').click();
           }
       }

     // Search Window open using Enter Key \\ 
       function doClick(e)
        {            
           var key;

           if (window.event)
               key = window.event.keyCode;     //IE
           else
               key = e.which;     //firefox

           if (key == 13)
            {              
                   itemsearch()
                   event.keyCode = 0                 
           }
        }

           function Intcheck(e)
            {
               var key = window.event ? e.keyCode : e.which;
               var keychar = String.fromCharCode(key);
               if (((keychar < "0") || (keychar > "9")) && (keychar != "."))
                   return false;
               else
                   return true;
            }

           function Intchecktwo(e)
            {
               var key = window.event ? e.keyCode : e.which;
               var keychar = String.fromCharCode(key);
               if (((keychar < "0") || (keychar > "9")))
                   return false;
               else
                   return true;
            }

    
    // Using Arrow key \\ 
       function Test(one,two,e) {          
           var key;
       
           if (window.event)
               key = window.event.keyCode;     
           else
               key = e.which;
           
          
           if (key == 38) {
               var a = document.getElementById(one);                     
               a.focus();
               event.keyCode = 0
           }
           else if (key == 40)  {
          
               var b = document.getElementById(two);
               b.focus();

               if ((document.getElementById('<%=txtwidth.ClientID%>').value != '') && (one == 'ctl00_ContentPlaceHolder1_txtheight')) {                   

                   var sp = document.getElementById('<%=ddltype.ClientID%>').value;

                   if ((sp == 'BOTTLE') || (sp == 'BOX-BOTTLE')) {                       

                       var myBool = document.getElementById('<%=chkrotation.ClientID%>');
                       if (myBool.checked) {
                           var myFlt1 = document.getElementById('<%=txtheight.ClientID%>').value;
                           var myFlt2 = document.getElementById('<%=txtbottlepitch.ClientID%>').value;
                       }
                       else {
                           var myFlt1 = document.getElementById('<%=txtwidth.ClientID%>').value;
                           var myFlt2 = document.getElementById('<%=txtbottlepitch.ClientID%>').value;
                       }

                       
                       var my = (parseFloat(myFlt1) + parseFloat(myFlt2));

                       document.getElementById('<%=txtmaxcart.ClientID%>').value = (370 / (my));
                   }
                   else {
                       document.getElementById('<%=txtmaxcart.ClientID%>').value = (600 / (document.getElementById('<%=txtwidth.ClientID%>').value));
                       document.getElementById('<%=txtcalcvalue.ClientID%>').value=(600 / (document.getElementById('<%=txtwidth.ClientID%>').value));
                   }
                   var n = document.getElementById('<%=txtmaxcart.ClientID%>').value.indexOf(".");
                   if (n > 0) {
                       //document.getElementById('<%=txtmaxcart.ClientID%>').value = document.getElementById('<%=txtmaxcart.ClientID%>').value.substring(0, n);
                       var Res = document.getElementById('<%=txtmaxcart.ClientID%>').value.substring(0, n);
                       document.getElementById('<%=txtmaxcart.ClientID%>').value = Res;
                       document.getElementById('<%=txtcalcvalue.ClientID%>').value = Res;

                   }
               }



               event.keyCode = 0
           }


           else if (key == 9)
            {
                if ((document.getElementById('<%=txtwidth.ClientID%>').value != '') && (one == 'ctl00_ContentPlaceHolder1_txtwidth'))
                {
                   
                   var sp = document.getElementById('<%=ddltype.ClientID%>').value;

                   if ((sp == 'BOTTLE') || (sp == 'BOX-BOTTLE'))
                    {

                        var myBool = document.getElementById('<%=chkrotation.ClientID%>');
                        if (myBool.checked) {
                            var myFlt1 = document.getElementById('<%=txtheight.ClientID%>').value;
                            var myFlt2 = document.getElementById('<%=txtbottlepitch.ClientID%>').value;
                        }
                        else {
                            var myFlt1 = document.getElementById('<%=txtwidth.ClientID%>').value;
                            var myFlt2 = document.getElementById('<%=txtbottlepitch.ClientID%>').value;
                        }
                       var my = (parseFloat(myFlt1) + parseFloat(myFlt2));

                       document.getElementById('<%=txtmaxcart.ClientID%>').value = (370 / (my));
                       document.getElementById('<%=txtcalcvalue.ClientID%>').value = (370 / (my));
                    }
                   else
                   {
                       document.getElementById('<%=txtmaxcart.ClientID%>').value = (600 / (document.getElementById('<%=txtwidth.ClientID%>').value));
                       document.getElementById('<%=txtcalcvalue.ClientID%>').value = (600 / (document.getElementById('<%=txtwidth.ClientID%>').value));
                   }
                       var n = document.getElementById('<%=txtmaxcart.ClientID%>').value.indexOf(".");

                       if (n > 0)
                        {
                            // document.getElementById('<%=txtmaxcart.ClientID%>').value = document.getElementById('<%=txtmaxcart.ClientID%>').value.substring(0, n);
                            var Res= document.getElementById('<%=txtmaxcart.ClientID%>').value.substring(0, n);
                            document.getElementById('<%=txtmaxcart.ClientID%>').value = Res;
                            document.getElementById('<%=txtcalcvalue.ClientID%>').value = Res;
                        }
                       
                   }
               event.keyCode = 0
           }           
       }
   
</script>

<asp:UpdatePanel ID="updatdrug" runat="server">
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label2" runat="server" Text="Drug Master" CssClass="labelhead"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
<tr style="padding-top:10px">
<td align="left">        
 <table cellpadding="0" cellspacing="0" border="0">

<tr>
<td align="left">
 <asp:Label ID="lblitemcode" runat="server" Text="Item Code" CssClass="labelall" 
        Width="60px"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtitemcode" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
       
 </td>
 <td align="left">
 <table width="760px" cellpadding="0" cellspacing="0" border="0">
 <tr>
 <td align="left">
 <asp:TextBox ID="txtitemcode" runat="server" Width="155px" CssClass="textbox" 
         AutoCompleteType="Disabled" TabIndex="1"></asp:TextBox>
 </td>
 <td align="left" style="padding-left:7px; width:80px" >
   <asp:Label ID="lbldrugcodemain" runat="server" Text="Drug Code" 
         CssClass="labelall" Width="60px"></asp:Label>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
         ControlToValidate="txtdrugcode" ErrorMessage="*" SetFocusOnError="True" 
         ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
 </td>
 <td align="left">
   <asp:TextBox ID="txtdrugcode" runat="server" Width="168px" CssClass="textbox" 
         AutoCompleteType="Disabled" TabIndex="2" ></asp:TextBox>
 </td>
 <td align="left" style="padding-left:7px">
   <asp:Label ID="lbleffective" runat="server" Text="Item Effective Date " 
         CssClass="labelall" Width="105px"></asp:Label>
 </td>
 <td align="left">
  <asp:TextBox ID="txteffectivefrom" runat="server" Width="90px" ReadOnly="True" 
         CssClass="textbox" AutoCompleteType="Disabled" TabIndex="44"></asp:TextBox>
 </td>
 <td align="left" style="padding-left:5px; width:20px">
 <asp:Label ID="lbleffto" runat="server" Text="To" CssClass="labelall"></asp:Label>
 </td>
 <td align="right" style="padding-left:2px">
 <asp:TextBox ID="txteffto" runat="server" Width="90px" 
         ReadOnly="True" CssClass="textbox" AutoCompleteType="Disabled" 
         TabIndex="45"></asp:TextBox>
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
        ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
 </td>
 <td align="left">
 <table width="760px" cellpadding="0" cellspacing="0" border="0">
 <tr>
 <td align="left">
 <asp:TextBox ID="txtitemnameadd" runat="server" Width="755px" CssClass="textbox" 
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
<tr>
  <td align="left">
  <asp:Label ID="brand" runat="server" Text="Brand Name" CssClass="labelall" 
          Width="70px"></asp:Label>  
      <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
          ErrorMessage="*" ControlToValidate="ddlbrand" Display="Dynamic" 
          SetFocusOnError="True" ValidationGroup="itemmaster"></asp:RequiredFieldValidator>    
  </td>
  <td align="left">
  <table width="760px" cellpadding="0" cellspacing="0" border="0">
  <tr>
  <td align="left" style="width:390px">
   <asp:DropDownList ID="ddlbrand" runat="server" Width="390px" AutoPostBack="True" 
          onselectedindexchanged="ddlbrand_SelectedIndexChanged" CssClass="textbox" 
          TabIndex="4" ></asp:DropDownList>
  </td>
  <td align="left" style="padding-left:8px">
   <asp:Label ID="lblmfrname" runat="server" Text="MFR Barcode" CssClass="labelall" 
          Width="80px"></asp:Label>
  </td>
  <td align="left" >
   <asp:DropDownList ID="ddlbarcode" runat="server" Width="279px" 
          AutoPostBack="True" CssClass="textbox" TabIndex="5"></asp:DropDownList>
  </td>

  </tr>
  </table>        
  </td>  
    <td align="left" style="padding-left:3px">

          <asp:ImageButton ID="btnbrand" runat="server" CssClass="btn" Height="19px" 
            ImageUrl="~/ButtonImages/BrandMaster.png"  />
  </td>
  
  </tr>
  <tr>
  <td align="left" style="width:85px">
  <asp:Label ID="lbltype" runat="server" Text="Pack Type" CssClass="labelall" 
         Width="70px" ></asp:Label> 
<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
ControlToValidate="ddltype" ErrorMessage="*" SetFocusOnError="True" 
 ValidationGroup="itemmaster" InitialValue="-Select-"></asp:RequiredFieldValidator>
  </td>
  <td align="left">
  <table  cellpadding="0" cellspacing="0" border="0">
  <tr>
  <td align="left">
   <asp:DropDownList ID="ddltype" runat="server" Width="110px" AutoPostBack="True" 
         onselectedindexchanged="ddltype_SelectedIndexChanged" CssClass="textbox" 
          TabIndex="6" ></asp:DropDownList>
  </td>
  <td align="left" style="padding-left:7px">
  <asp:Label ID="lblpacksize" runat="server" CssClass="labelall" Text="Pack Size"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
ControlToValidate="txtpacksize" ErrorMessage="*" SetFocusOnError="True" 
 ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
  </td>
  <td align="left">
   <asp:TextBox ID="txtpacksize" runat="server" Width="60px" 
          onblur ="void uomcalc()" CssClass="textbox" AutoCompleteType="Disabled" 
          TabIndex="7" MaxLength="4"></asp:TextBox>           
  </td>
  <td align="left" style="padding-left:7px">
   <asp:Label ID="lbluomed" runat="server" CssClass="labelall" Text="UOM"></asp:Label>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
ControlToValidate="ddluom" ErrorMessage="*" SetFocusOnError="True" 
 ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
  </td>
  <td align="left" style="padding-left:2px">
     <asp:TextBox ID="ddluom" runat="server" Width="92px" 
          onblur ="void uomcalc()" CssClass="textbox" AutoCompleteType="Disabled" 
          TabIndex="7" MaxLength="8" ReadOnly="True"></asp:TextBox>                       
  </td>
  <td style="padding-left:10px">
  <asp:Label runat="server" ID="lblpacktypeinfo"  CssClass="labelall" 
          Font-Bold="True" ></asp:Label>
  </td>
     <td>
<asp:Button ID="btnok" runat="server" CausesValidation="false" onclick="btnok_Click" style="position:static; display:none;"/>              
<asp:Button ID="btnuom" runat="server" CausesValidation="false" onclick="btnuom_Click" style="position:static; display:none;" />
  
 <asp:TextBox ID="txtcalcvalue" runat="server" CausesValidation="false" Style="position: static; display: none"/>     
 <asp:TextBox ID="txtbottlepitch" runat="server" CausesValidation="false" Style="position: static; display: none"/>           

  </td>
  </tr>
  </table>  
  </td> 
  <td align="left" style="padding-left:3px">
  <asp:ImageButton ID="btnaddnewcarton" runat="server" CssClass="btn" Height="19px" 
            ImageUrl="~/ButtonImages/AddCarton.png" 
          onclick="btnaddnewcarton_Click"  />
  </td>
  </tr> 
</table>
</td>
</tr>
 <tr>
  <td>
  <table width="100%" cellpadding="0" cellspacing="0" border="0" style="border-bottom: solid 1px #ffc0a0; border-bottom-color: #009900; border-bottom-width: thin; width:100%">  
  <tr>
  <td></td>
  </tr>
  </table>
  </td>
</tr>   
<tr>
<td align="left" style="padding-top:5px">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
</td>
<td>
</td>
<td>
</td>
<td style="padding-left:4px; width:175px" >
<asp:Label ID="lblboxorpallet" runat="server" Text="Carton Box / Pallet" 
         CssClass="labelall"></asp:Label>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator49" runat="server" 
        ControlToValidate="DDLBoxOrPallet" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="itemmaster" InitialValue="-Select-"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server" 
        ControlToValidate="DDLBoxOrPallet" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="Cartupdate" InitialValue="-Select-"></asp:RequiredFieldValidator>
</td>
  <td>
 <asp:DropDownList ID="DDLBoxOrPallet" runat="server" CssClass="textbox" Width="80px" 
         TabIndex="23" AutoPostBack="True" 
          onselectedindexchanged="DDLBoxOrPallet_SelectedIndexChanged"></asp:DropDownList>
 </td>

</tr>

<tr>
<td>

</td>
<td>
</td>
<td>
</td>
<td style="padding-left:4px; width:175px" >
<asp:Label ID="lblddlcarttype" runat="server" Text="Carton Box Bottle Count " 
         CssClass="labelall"></asp:Label>
</td>
  <td>
 <asp:DropDownList ID="DDLCartonType" runat="server" CssClass="textbox" Width="80px" 
         TabIndex="23" AutoPostBack="True" 
         onselectedindexchanged="DDLCartonType_SelectedIndexChanged"></asp:DropDownList>
 </td>

 <td style="padding-left:4px; width:175px" >

   
</td>

</tr>

<tr>
<td align="left">
<asp:Label id="lbllength" runat="server" Text="L - 1 (mm)   " 
        CssClass="labelall"  ></asp:Label> 
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
        ControlToValidate="txtlength" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
   <asp:RangeValidator ID="RangeValidator6" runat="server" 
         ControlToValidate="txtlength" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
        ValidationGroup="itemmaster"></asp:RangeValidator>

  <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" 
        ControlToValidate="txtlength" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="OnlyBottle"></asp:RequiredFieldValidator>
</td>
<td align="left" >
<asp:TextBox ID="txtlength" runat="server" Width="75px" CssClass="textbox" 
        AutoCompleteType="Disabled" TabIndex="9" MaxLength="5"></asp:TextBox>
 </td> 
 <td>
 
 </td>
  <td style="padding-left:4px; width:175px" >
 <asp:Label ID="lblcartype" runat="server" Text="Carton Box Bottle Count " 
         CssClass="labelall"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
        ControlToValidate="txt_Car_type" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
   <asp:RangeValidator ID="RangeValidator11" runat="server" 
         ControlToValidate="txt_Car_type" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
        ValidationGroup="itemmaster"></asp:RangeValidator>

    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" 
        ControlToValidate="txt_Car_type" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="Cartupdate"></asp:RequiredFieldValidator>
   <asp:RangeValidator ID="RangeValidator8" runat="server" 
         ControlToValidate="txt_Car_type" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
        ValidationGroup="Cartupdate"></asp:RangeValidator>
 </td>
 <td>
  <asp:TextBox ID="txt_Car_type" runat="server" CssClass="textbox" Width="75px" 
         TabIndex="23"></asp:TextBox>
 </td>
  <td style="vertical-align:top">
  <table cellpadding="0" cellspacing="0" border="0">
          <tr>
          <td style="padding-left:4px; width:165px">
              <asp:Label ID="lblcart_Row" runat="server" CssClass="labelall" 
                  Text="Carton Box X Count"></asp:Label>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" 
        ControlToValidate="txtcar_Row" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
   <asp:RangeValidator ID="RangeValidator21" runat="server" 
         ControlToValidate="txtcar_Row" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
        ValidationGroup="itemmaster"></asp:RangeValidator>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" 
        ControlToValidate="txtcar_Row" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="Cartupdate"></asp:RequiredFieldValidator>
   <asp:RangeValidator ID="RangeValidator20" runat="server" 
         ControlToValidate="txtcar_Row" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
        ValidationGroup="Cartupdate"></asp:RangeValidator>
          </td>
          <td>
              <asp:TextBox ID="txtcar_Row" runat="server" CssClass="textbox" Width="75px" 
                  TabIndex="33"></asp:TextBox>
          </td>
      </tr>
  </table>
  </td>
  </tr>
 </tr> 
 <tr >
 <td align="left">
 <asp:Label ID="lblheight" runat="server" Text="L - 2 (mm)  " 
         CssClass="labelall"></asp:Label>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
         ControlToValidate="txtheight" ErrorMessage="*" SetFocusOnError="True" 
         ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
      <asp:RangeValidator ID="RangeValidator7" runat="server" 
         ControlToValidate="txtheight" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" 
         ValidationGroup="itemmaster"></asp:RangeValidator>

      <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" 
         ControlToValidate="txtheight" ErrorMessage="*" SetFocusOnError="True" 
         ValidationGroup="OnlyBottle"></asp:RequiredFieldValidator>
 </td>
<td align="left">
 <asp:TextBox ID="txtheight" runat="server" Width="75px" CssClass="textbox" 
        AutoCompleteType="Disabled" TabIndex="10" MaxLength="5"></asp:TextBox>
 </td> 
 <td>
 </td>
    <td style="padding-left:4px; width:175px" >
 <asp:Label ID="lblcartlength" runat="server" Text="Carton Box Length (mm) " 
          CssClass="labelall"></asp:Label>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator181" runat="server" 
        ControlToValidate="txtcar_length" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
   <asp:RangeValidator ID="RangeValidator121" runat="server" 
         ControlToValidate="txtcar_length" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
        ValidationGroup="itemmaster"></asp:RangeValidator>

   <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" 
        ControlToValidate="txtcar_length" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="Cartupdate"></asp:RequiredFieldValidator>
   <asp:RangeValidator ID="RangeValidator24" runat="server" 
         ControlToValidate="txtcar_length" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
        ValidationGroup="Cartupdate"></asp:RangeValidator>
 </td>
 <td>
 <asp:TextBox ID="txtcar_length" runat="server" CssClass="textbox" Width="75px" 
         TabIndex="24"></asp:TextBox>
 </td>



   <td>
  <table cellpadding="0" cellspacing="0" border="0"> 
      <tr>
          <td style="padding-left:4px; width:165px">
              <asp:Label ID="lblcart_Column" runat="server" CssClass="labelall" 
                  Text="Carton Box Y Count"></asp:Label>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" 
        ControlToValidate="txtcar_Column" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
   <asp:RangeValidator ID="RangeValidator22" runat="server" 
         ControlToValidate="txtcar_Column" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
        ValidationGroup="itemmaster"></asp:RangeValidator>

   <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" 
        ControlToValidate="txtcar_Column" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="Cartupdate"></asp:RequiredFieldValidator>
   <asp:RangeValidator ID="RangeValidator25" runat="server" 
         ControlToValidate="txtcar_Column" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
        ValidationGroup="Cartupdate"></asp:RangeValidator>
          </td>
          <td>
              <asp:TextBox ID="txtcar_Column" runat="server" CssClass="textbox" Width="75px" 
                  TabIndex="34"></asp:TextBox>
          </td>
      </tr>
  </table>
  </td>

 </tr>
 <tr>
 <td align="left">
 <asp:Label ID="lblwidth" runat="server" Text="L - 3 (mm) " 
         CssClass="labelall"></asp:Label>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
         ControlToValidate="txtwidth" ErrorMessage="*" SetFocusOnError="True" 
         ValidationGroup="itemmaster"></asp:RequiredFieldValidator>

<asp:RangeValidator ID="RangeValidator3" runat="server" 
         ControlToValidate="txtwidth" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" 
         ValidationGroup="cal"></asp:RangeValidator>

     <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" 
         ControlToValidate="txtwidth" ErrorMessage="*" SetFocusOnError="True" 
         ValidationGroup="OnlyBottle"></asp:RequiredFieldValidator>
 </td>
 <td align="left">
 <asp:TextBox ID="txtwidth" runat="server" Width="75px" CssClass="textbox" 
         AutoCompleteType="Disabled" TabIndex="11" MaxLength="5" ></asp:TextBox>
 </td>

 <td align="left">

  <asp:ImageButton ID="btncalc" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Calculate.png" onclick="btncalc_Click" 
         Height="19px"  />
 </td>


    <td style="padding-left:4px; width:175px" >
 <asp:Label ID="lblcartheight" runat="server" Text="Carton Box Height (mm) " 
          CssClass="labelall"></asp:Label>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" 
        ControlToValidate="txtcar_height" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
   <asp:RangeValidator ID="RangeValidator12" runat="server" 
         ControlToValidate="txtcar_height" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
        ValidationGroup="itemmaster"></asp:RangeValidator>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" 
        ControlToValidate="txtcar_height" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="Cartupdate"></asp:RequiredFieldValidator>
   <asp:RangeValidator ID="RangeValidator23" runat="server" 
         ControlToValidate="txtcar_height" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
        ValidationGroup="Cartupdate"></asp:RangeValidator>
 </td>
 <td>
 <asp:TextBox ID="txtcar_height" runat="server" CssClass="textbox" Width="75px" 
         TabIndex="24"></asp:TextBox>
 </td>
   <td>
  <table cellpadding="0" cellspacing="0" border="0"> 
      <tr>
          <td style="padding-left:4px; width:165px">
              <asp:Label ID="lblcart_No_Layer" runat="server" CssClass="labelall" 
                  Text="Carton Box No .Of Layer"></asp:Label>
  
          </td>
          <td>
         

                 <asp:DropDownList ID="ddlcar_layer" runat="server" Width="75px" AutoPostBack="True" 
                   CssClass="textbox"  TabIndex="8" 
                  onselectedindexchanged="ddlcar_layer_SelectedIndexChanged" >
            </asp:DropDownList>      

          </td>

          </tr> 
          </table> 
          </td> 


 </tr>
 <tr>

  <td align="left">
 <asp:Label ID="lblmax" runat="server" Text="Maximum  Quantity Per Cartridge" 
         CssClass="labelall"></asp:Label>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
         ControlToValidate="txtmaxcart" ErrorMessage="*" SetFocusOnError="True" 
         ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
     <asp:RangeValidator ID="RangeValidator1" runat="server" 
         ControlToValidate="txtmaxcart" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Integer" 
         ValidationGroup="itemmaster"></asp:RangeValidator>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" 
         ControlToValidate="txtmaxcart" ErrorMessage="*" SetFocusOnError="True" 
         ValidationGroup="OnlyBottle"></asp:RequiredFieldValidator>
 </td>
 <td align="left">
 <asp:TextBox ID="txtmaxcart" runat="server" Width="75px" CssClass="textbox" 
         AutoCompleteType="Disabled" BackColor="#F0F0F0" ReadOnly="True" ></asp:TextBox>
 </td> 
  <td>
 <asp:Label ID="lbluom1" runat="server" CssClass="labelall" Width="150px"></asp:Label>
 </td>


  <td style="padding-left:4px; width:175px" >
    <asp:Label ID="lblcartwidth" runat="server" Text="Carton Box width (mm) " 
    CssClass="labelall"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
    ControlToValidate="txtcar_width" ErrorMessage="*" SetFocusOnError="True" 
    ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator13" runat="server" 
    ControlToValidate="txtcar_width" ErrorMessage="*" MaximumValue="999" 
    MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
    ValidationGroup="itemmaster"></asp:RangeValidator>

        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" 
    ControlToValidate="txtcar_width" ErrorMessage="*" SetFocusOnError="True" 
    ValidationGroup="Cartupdate"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator26" runat="server" 
    ControlToValidate="txtcar_width" ErrorMessage="*" MaximumValue="999" 
    MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
    ValidationGroup="Cartupdate"></asp:RangeValidator>
 </td>

 <td>
 <asp:TextBox ID="txtcar_width" runat="server" CssClass="textbox" Width="75px" 
         TabIndex="25"></asp:TextBox>
 </td>
 
  <td>
  <table cellpadding="0" cellspacing="0" border="0"> 
      <tr>
          <td style="padding-left:4px; width:165px">
              <asp:Label ID="lbldivider" runat="server" CssClass="labelall" 
                  Text="Divider"></asp:Label>
       
          </td>
          <td align="left">
              <asp:CheckBox ID="Chkdivider" runat="server" />
          </td>

          </tr> 
          </table> 
          </td> 



 </tr>
 <tr>

  <td align="left">
 <asp:Label ID="lblminpack" runat="server" Text="Minimum Alert Quantity Per DDS / BDS " 
         CssClass="labelall"></asp:Label>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
         ControlToValidate="txtmaxdds" ErrorMessage="*" SetFocusOnError="True" 
         ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
     <asp:RangeValidator ID="RangeValidator2" runat="server" 
         ControlToValidate="txtmaxdds" ErrorMessage="*" MaximumValue="9999" 
         MinimumValue="0" SetFocusOnError="True" Type="Integer" 
         ValidationGroup="itemmaster"></asp:RangeValidator>
  <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" 
         ControlToValidate="txtmaxdds" ErrorMessage="*" SetFocusOnError="True" 
         ValidationGroup="OnlyBottle"></asp:RequiredFieldValidator>
 </td>
 <td align="left">
 <asp:TextBox ID="txtmaxdds" runat="server" Width="75px"  
         ontextchanged="txtmaxdds_TextChanged" CssClass="textbox" 
         AutoCompleteType="Disabled" TabIndex="15" MaxLength="4" ></asp:TextBox>
 </td> 
<td>
 <asp:Label ID="lbluom2" runat="server" CssClass="labelall" Width="150px"></asp:Label>
</td>


   <td style="padding-left:4px; width:175px" >
 <asp:Label ID="lblcart_x_offset" runat="server" Text="Carton Box X-Offset" 
         CssClass="labelall"></asp:Label>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" 
        ControlToValidate="txtcar_X_Offset" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
   <asp:RangeValidator ID="RangeValidator14" runat="server" 
         ControlToValidate="txtcar_X_Offset" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
        ValidationGroup="itemmaster"></asp:RangeValidator>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" 
        ControlToValidate="txtcar_X_Offset" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="Cartupdate"></asp:RequiredFieldValidator>
   <asp:RangeValidator ID="RangeValidator27" runat="server" 
         ControlToValidate="txtcar_X_Offset" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
        ValidationGroup="Cartupdate"></asp:RangeValidator>
 </td>
 <td>
 <asp:TextBox ID="txtcar_X_Offset" runat="server" CssClass="textbox" Width="75px" 
         TabIndex="26"></asp:TextBox>
 </td>

   <td>
  <table cellpadding="0" cellspacing="0" border="0"> 
      <tr>
          <td style="padding-left:4px; width:165px">
              <asp:Label ID="lblinterleaf" runat="server" CssClass="labelall" 
                  Text="Interleaf"></asp:Label>
     
          </td>
          <td align="left">
               <asp:CheckBox ID="Chklinterleaf" runat="server" TabIndex="22" />
          </td>

          </tr> 
          </table> 
          </td> 


 </tr> 
 <tr>

    <td>
   <asp:Label ID="lblmaxbbbds" runat="server" Text="Maximum Box / Bottle For BDS  " 
         CssClass="labelall"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator53" runat="server" 
         ControlToValidate="txtmaxcontainer" ErrorMessage="*" SetFocusOnError="True" 
         ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
     <asp:RangeValidator ID="RangeValidator34" runat="server" 
         ControlToValidate="txtmaxcontainer" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="1" SetFocusOnError="True" Type="Integer" 
         ValidationGroup="itemmaster"></asp:RangeValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" 
         ControlToValidate="txtmaxcontainer" ErrorMessage="*" SetFocusOnError="True" 
         ValidationGroup="OnlyBottle"></asp:RequiredFieldValidator>
  </td>
  <td align="left">
  <asp:TextBox ID="txtmaxbbbds" runat="server" Width="75px" CssClass="textbox" 
          AutoCompleteType="Disabled" TabIndex="19" MaxLength="3" ></asp:TextBox>
 </td>
  <td align="left">
 <asp:Label ID="lblmaxbbbdsuom" runat="server" CssClass="labelall" Width="150px"></asp:Label>
 </td>


     <td style="padding-left:4px; width:155px" >
 <asp:Label ID="lblcart_Y_Offset" runat="server" Text="Carton Box Y-Offset" 
           CssClass="labelall"></asp:Label>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" 
        ControlToValidate="txtcar_Y_Offset" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
   <asp:RangeValidator ID="RangeValidator15" runat="server" 
         ControlToValidate="txtcar_Y_Offset" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
        ValidationGroup="itemmaster"></asp:RangeValidator>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" 
        ControlToValidate="txtcar_Y_Offset" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="Cartupdate"></asp:RequiredFieldValidator>
   <asp:RangeValidator ID="RangeValidator28" runat="server" 
         ControlToValidate="txtcar_Y_Offset" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
        ValidationGroup="Cartupdate"></asp:RangeValidator>
 </td>
 <td>
 <asp:TextBox ID="txtcar_Y_Offset" runat="server" CssClass="textbox" Width="75px" 
         TabIndex="27"></asp:TextBox>
 </td>

 <td>
    <table cellpadding="0" cellspacing="0" border="0"> 
    <tr>
    <td style="padding-left:4px; width:165px">
    <asp:Label ID="LblCartActive" runat="server" CssClass="labelall" 
    Text="Active"></asp:Label>
     
    </td>
    <td align="left">
    <asp:CheckBox ID="ChkCartActive" runat="server" TabIndex="22" AutoPostBack="True"/>
    </td>

    </tr> 
    </table> 
    </td>
 </tr>   
 <tr>

  <td align="left">
 <asp:Label ID="lblsmallbag" runat="server" Text="Max Quantity Per Small Bag " 
         CssClass="labelall"></asp:Label>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
         ControlToValidate="txtsmallbag" ErrorMessage="*" SetFocusOnError="True" 
         ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
     <asp:RangeValidator ID="RangeValidator4" runat="server" 
         ControlToValidate="txtsmallbag" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Integer" 
         ValidationGroup="itemmaster"></asp:RangeValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" 
         ControlToValidate="txtsmallbag" ErrorMessage="*" SetFocusOnError="True" 
         ValidationGroup="OnlyBottle"></asp:RequiredFieldValidator>
 </td>
  <td align="left">
  <asp:TextBox ID="txtsmallbag" runat="server" Width="75px" CssClass="textbox" 
          AutoCompleteType="Disabled" TabIndex="16" MaxLength="3"></asp:TextBox>
 </td>

  <td align="left">
  <asp:Label ID="lblsptype" runat="server" CssClass="labelall" Width="150px"></asp:Label>
 </td>

<td style="padding-left:4px; width:145px">
  <asp:Label ID="lblcot_Z_offset" runat="server" Text="Carton Box Z-Offset " 
          CssClass="labelall"></asp:Label>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" 
        ControlToValidate="txtcar_Z_offset" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
   <asp:RangeValidator ID="RangeValidator16" runat="server" 
         ControlToValidate="txtcar_Z_offset" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
        ValidationGroup="itemmaster"></asp:RangeValidator>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" 
        ControlToValidate="txtcar_Z_offset" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="Cartupdate"></asp:RequiredFieldValidator>
   <asp:RangeValidator ID="RangeValidator29" runat="server" 
         ControlToValidate="txtcar_Z_offset" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
        ValidationGroup="Cartupdate"></asp:RangeValidator>
 </td>
 <td>
 <asp:TextBox ID="txtcar_Z_offset" runat="server" CssClass="textbox" Width="75px" 
         TabIndex="28"></asp:TextBox>
 </td>
   
  </tr> 
  <tr>

    <td align="left">
 <asp:Label ID="lblmedium" runat="server" Text="Max Quantity Per Medium Bag " 
         CssClass="labelall"></asp:Label>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
         ControlToValidate="txtmedium" ErrorMessage="*" SetFocusOnError="True" 
         ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
     <asp:RangeValidator ID="RangeValidator9" runat="server" 
         ControlToValidate="txtmedium" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Integer" 
         ValidationGroup="itemmaster"></asp:RangeValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator46" runat="server" 
         ControlToValidate="txtmedium" ErrorMessage="*" SetFocusOnError="True" 
         ValidationGroup="OnlyBottle"></asp:RequiredFieldValidator>
 </td>
  <td align="left">
  <asp:TextBox ID="txtmedium" runat="server" Width="75px" CssClass="textbox" 
          AutoCompleteType="Disabled" TabIndex="17" MaxLength="3"></asp:TextBox>
 </td>
 <td align="left">
 <asp:Label ID="lblmetype" runat="server" CssClass="labelall" Width="150px"></asp:Label>
 </td>
 
         <td style="padding-left:4px; width:155px">
              <asp:Label ID="lblcart_X_Pitch" runat="server" CssClass="labelall" 
                  Text="Carton Box X-Pitch"></asp:Label>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" 
        ControlToValidate="txtcar_X_Pitch" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
   <asp:RangeValidator ID="RangeValidator17" runat="server" 
         ControlToValidate="txtcar_X_Pitch" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
        ValidationGroup="itemmaster"></asp:RangeValidator>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" 
        ControlToValidate="txtcar_X_Pitch" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="Cartupdate"></asp:RequiredFieldValidator>
   <asp:RangeValidator ID="RangeValidator30" runat="server" 
         ControlToValidate="txtcar_X_Pitch" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
        ValidationGroup="Cartupdate"></asp:RangeValidator>
          </td>
          <td>
              <asp:TextBox ID="txtcar_X_Pitch" runat="server" CssClass="textbox" Width="75px" 
                  TabIndex="29"></asp:TextBox>
          </td>
        <td style="padding-left:5px; vertical-align:top " align="left" rowspan="4" >
  <table cellpadding="0" cellspacing="0" border="0">  
       <tr>
       <td align="left">         
       <div style="height:80px; overflow:scroll; width: 269px; ">   
  <asp:GridView ID="gridpharloc" runat="server" AutoGenerateColumns="False" ForeColor="#336600"  
          BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
          CellPadding="1" EnableModelValidation="True" Width="245px" CssClass="gridcss" 
               TabIndex="35" >
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
  </tr> 
  </table>
 </td>

  </tr> 
  <tr>

  <td align="left">
 <asp:Label ID="lbllargebag" runat="server" Text="Max Quantity Per Large Bag " 
         CssClass="labelall"></asp:Label>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
         ControlToValidate="txtlarge" ErrorMessage="*" SetFocusOnError="True" 
         ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
     <asp:RangeValidator ID="RangeValidator5" runat="server" 
         ControlToValidate="txtlarge" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="1" SetFocusOnError="True" Type="Integer" 
         ValidationGroup="itemmaster"></asp:RangeValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator47" runat="server" 
         ControlToValidate="txtlarge" ErrorMessage="*" SetFocusOnError="True" 
         ValidationGroup="OnlyBottle"></asp:RequiredFieldValidator>
 </td>
  <td align="left">
  <asp:TextBox ID="txtlarge" runat="server" Width="75px" CssClass="textbox" 
          AutoCompleteType="Disabled" TabIndex="18" MaxLength="3" ></asp:TextBox>
 </td>
 <td align="left">
 <asp:Label ID="lblpatype" runat="server" CssClass="labelall" Width="150px"></asp:Label>
 </td>


  
      <td style="padding-left:4px; width:155px" >
 <asp:Label ID="lblcart_Y_Pitch" runat="server" Text="Carton Box Y-Pitch" 
            CssClass="labelall"></asp:Label>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" 
        ControlToValidate="txtcar_Y_Pitch" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
   <asp:RangeValidator ID="RangeValidator18" runat="server" 
         ControlToValidate="txtcar_Y_Pitch" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
        ValidationGroup="itemmaster"></asp:RangeValidator>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" 
        ControlToValidate="txtcar_Y_Pitch" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="Cartupdate"></asp:RequiredFieldValidator>
   <asp:RangeValidator ID="RangeValidator31" runat="server" 
         ControlToValidate="txtcar_Y_Pitch" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="0" SetFocusOnError="True" Type="Double" Display="Dynamic" 
        ValidationGroup="Cartupdate"></asp:RangeValidator>
 </td>
 <td>
 <asp:TextBox ID="txtcar_Y_Pitch" runat="server" CssClass="textbox" Width="75px" 
         TabIndex="30"></asp:TextBox>
 </td>
    
  </tr> 
  <tr>   
<td>
   <asp:Label ID="lblcontainer" runat="server" Text="Max No. of Bundle/ Box / Bottle Per Container " 
         CssClass="labelall"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
         ControlToValidate="txtmaxcontainer" ErrorMessage="*" SetFocusOnError="True" 
         ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
     <asp:RangeValidator ID="RangeValidator10" runat="server" 
         ControlToValidate="txtmaxcontainer" ErrorMessage="*" MaximumValue="999" 
         MinimumValue="1" SetFocusOnError="True" Type="Integer" 
         ValidationGroup="itemmaster"></asp:RangeValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator48" runat="server" 
         ControlToValidate="txtmaxcontainer" ErrorMessage="*" SetFocusOnError="True" 
         ValidationGroup="OnlyBottle"></asp:RequiredFieldValidator>
  </td>
  <td align="left">
  <asp:TextBox ID="txtmaxcontainer" runat="server" Width="75px" CssClass="textbox" 
          AutoCompleteType="Disabled" TabIndex="19" MaxLength="3" ></asp:TextBox>
 </td>
 <td align="left">
 <asp:Label ID="lblmaxcont" runat="server" CssClass="labelall" Width="150px"></asp:Label>
 </td>
      <td style="padding-left:4px; width:155px; vertical-align:top ">
          <asp:Label ID="lblcart_Z_Pitch" runat="server" CssClass="labelall" 
              Text="Carton Box Z-Pitch"></asp:Label>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" 
              ControlToValidate="txtcar_Z_Pitch" ErrorMessage="*" SetFocusOnError="True" 
              ValidationGroup="itemmaster"></asp:RequiredFieldValidator>
          <asp:RangeValidator ID="RangeValidator19" runat="server" 
              ControlToValidate="txtcar_Z_Pitch" Display="Dynamic" ErrorMessage="*" 
              MaximumValue="999" MinimumValue="0" SetFocusOnError="True" Type="Double" 
              ValidationGroup="itemmaster"></asp:RangeValidator>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" 
              ControlToValidate="txtcar_Z_Pitch" ErrorMessage="*" SetFocusOnError="True" 
              ValidationGroup="Cartupdate"></asp:RequiredFieldValidator>
          <asp:RangeValidator ID="RangeValidator32" runat="server" 
              ControlToValidate="txtcar_Z_Pitch" Display="Dynamic" ErrorMessage="*" 
              MaximumValue="999" MinimumValue="0" SetFocusOnError="True" Type="Double" 
              ValidationGroup="Cartupdate"></asp:RangeValidator>
      </td>
      <td style="vertical-align:top ">
          <asp:TextBox ID="txtcar_Z_Pitch" runat="server" CssClass="textbox" 
              TabIndex="31" Width="75px"></asp:TextBox>
      </td>
  
  </tr> 

  <tr>
   <td align="left" colspan="2">
          <table style="border: thin groove #59DD63">
              <tr>
                  <td style="width:90px">
                      <asp:Label ID="lblactivate" runat="server" CssClass="labelall" Text="Active"></asp:Label>
                  </td>
                  <td align="left" style="width:20px">
                      <asp:CheckBox ID="chkactive" runat="server" TabIndex="20" />
                  </td>
                     <td style="width:120px">
              <asp:Label ID="lblrotation" runat="server" CssClass="labelall" 
                  Text="Bot RotationFlag"></asp:Label>
     
          </td>
          <td align="left">
               <asp:CheckBox ID="chkrotation" runat="server" TabIndex="22" AutoPostBack="True" 
                   oncheckedchanged="chkrotation_CheckedChanged" />
          </td>
              </tr>
          </table>
      </td>
  

  </tr>
  <tr>
   <td align="left" colspan="2">
          <table style="border: thin groove #59DD63">
              <tr>
                      <td style="width:90px">
                      <asp:Label ID="lblhrip" runat="server" CssClass="labelall" 
                          Text="Horizontal Rip"></asp:Label>
                  </td>
                  <td style="width:20px">
                      <asp:CheckBox ID="chkhrip" runat="server" TabIndex="21" />
                  </td>
                  <td style="width:120px">
                      <asp:Label ID="lblvrip" runat="server" CssClass="labelall" Text="Vertical Rip"></asp:Label>
                  </td>
                  <td>
                      <asp:CheckBox ID="chkvrip" runat="server" TabIndex="22" />
                  </td>
              </tr>
          </table>
      </td>
      <td></td>
      <td></td>
      <td></td>
        <td align="left" colspan="3">

  <asp:ImageButton ID="btnclear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" Height="20px" 
         onclick="btnclear_Click"  />
  <asp:ImageButton ID="btnsubmit" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Save.png" onclick="btnsubmit_Click" 
         ValidationGroup="itemmaster" TabIndex="37" Height="20px"  />
  <asp:ImageButton ID="btncartonsave" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Save.png" 
         ValidationGroup="Cartupdate" TabIndex="37" Height="20px" 
         onclick="btncartonsave_Click"  />
  <asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Update.png" ValidationGroup="itemmaster" 
         TabIndex="38" onclick="btnupdate_Click" Height="20px"   />
  <asp:ImageButton ID="btncartupdate" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Update.png" ValidationGroup="Cartupdate" 
         TabIndex="38"  Height="20px" onclick="btncartupdate_Click" />
<asp:ImageButton ID="Btnonlybottleupdate" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Update.png" ValidationGroup="OnlyBottle" 
         TabIndex="38"  Height="20px" onclick="Btnonlybottleupdate_Click"  />


 </td>

  </tr>  
<%--  <tr>
  <td></td>
  <td></td>
  <td></td>
  <td></td>
  <td></td>  
  </tr>--%>

  </table> 
   </td>
  </tr> 
  <%--    <tr>
 
    <td align="right" style="padding-right:160px">

  <asp:ImageButton ID="btnclear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" Height="20px" 
         onclick="btnclear_Click"  />
  <asp:ImageButton ID="btnsubmit" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Save.png" onclick="btnsubmit_Click" 
         ValidationGroup="itemmaster" TabIndex="37" Height="20px"  />
  <asp:ImageButton ID="btncartonsave" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Save.png" 
         ValidationGroup="Cartupdate" TabIndex="37" Height="20px" 
         onclick="btncartonsave_Click"  />
  <asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Update.png" ValidationGroup="itemmaster" 
         TabIndex="38" onclick="btnupdate_Click" Height="20px"   />
  <asp:ImageButton ID="btncartupdate" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Update.png" ValidationGroup="Cartupdate" 
         TabIndex="38"  Height="20px" onclick="btncartupdate_Click" />
<asp:ImageButton ID="Btnonlybottleupdate" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Update.png" ValidationGroup="OnlyBottle" 
         TabIndex="38"  Height="20px" onclick="Btnonlybottleupdate_Click"  />


 </td>
    </tr>--%>
    
    <tr>
    <td>
    
    </td>
    </tr>
  <tr>
  <td align="left">  
  <table width="100%" cellpadding="0" cellspacing="0" border="0">    
  <tr>
  <td align="left">   
        <asp:GridView ID="gridedit" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left" RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        onselectedindexchanging="gridedit_SelectedIndexChanging" 
        AllowPaging="True" onpageindexchanging="gridedit_PageIndexChanging" 
            CssClass="gridcss" onsorting="gridedit_Sorting" AllowSorting="True">  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />        
         <Columns>
      
          <asp:CommandField SelectText="Edit" ShowSelectButton="True"  />
         
          <asp:BoundField DataField="ID" Visible="false"/>
        
          <asp:BoundField DataField="Brandname" HeaderText="Brand Name" SortExpression="Brandname" />
          <asp:BoundField DataField="location_name" HeaderText="Pharmacy Location" SortExpression="location_name" />
          <asp:BoundField DataField="PackType" HeaderText="Pack Type" SortExpression="PackType" />
          <asp:BoundField DataField="Pack_Size" HeaderText="Pack Size" SortExpression="Pack_Size" >
             <ItemStyle HorizontalAlign="Center" />
             </asp:BoundField>
          <asp:BoundField DataField="uom" HeaderText="UOM" />
          <%-- <asp:BoundField DataField="Cart_Type" HeaderText="Cart Box Of" SortExpression="Cart_Type" >
             <ItemStyle HorizontalAlign="Center" />
             </asp:BoundField>--%>
          <asp:BoundField DataField="Status" HeaderText="Active" />
          <asp:BoundField DataField="Created_by" HeaderText="Created by" />
          <asp:BoundField DataField="Created_Date" HeaderText="Created Date Time" />
          <asp:BoundField DataField="Updated_by" HeaderText="Updated by" />
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