<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popuppreload.aspx.cs" Inherits="popuppreload" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>iOPAS</title>
    <link href ="cal/popcalendar.css" type="text/css" rel="Stylesheet" />
    <link type="text/css" rel="stylesheet" href="css/cal.css" />  
    <script language="JavaScript" type="text/jscript">

        function closepopup() {
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnpresearch').click();
            //window.open('popuppreload.aspx', 'search', 'menubar=no,center:yes,scrollbars=no,width=620,height=500[color=blue]40,top=100,left=200')
            //top.location.refresh();
            //window.opener.location.refresh();
            window.close();
        }
        function passValueToParent(val1, val2, val3, val4, val5, val6, val7, val8) {  
        //function passValueToParent() {        
//            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtitemcode').value = document.getElementById('<%=txtitemcode.ClientID%>').value;
//            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtfilter').value = document.getElementById('<%=ddlfilter.ClientID%>').value;
//            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtdatval').value = document.getElementById('<%=txt_Date.ClientID%>').value;
//            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtcartno').value = document.getElementById('<%=txtcartno.ClientID%>').value;
//            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtmfrsearch').value = document.getElementById('<%=txtmfrcode.ClientID%>').value;
//            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtitemname').value = document.getElementById('<%=txtitemname.ClientID%>').value;
//            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtserdrgcode').value = document.getElementById('<%=txtdrugcode.ClientID%>').value;
//            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtbrandsear').value = document.getElementById('<%=txtbrand.ClientID%>').value;

            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtitcodesearch').value = val1;
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtfilter').value = val2;
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtdatval').value = val3;
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtcartno').value = val4;
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtmfrsearch').value = val5;
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtitemname').value = val6;
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtserdrgcode').value = val7;
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtbrandsear').value = val8;
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnpresearch').click();
            //window.open('popuppreload.aspx', 'search', 'menubar=no,center:yes,scrollbars=no,width=620,height=500[color=blue]40,top=100,left=200')
            //top.location.refresh();
           // window.opener.location.refresh();
            window.close();
        }
</script>
<script language="javascript" type="text/javascript" src="cal/popcalendar.js" ></script>
</head>
<body>
    <form id="form1" runat="server">
     <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:UpdatePanel ID="updpanel2" runat="server">
     <ContentTemplate>
     <table width="100%" cellpadding="0" cellspacing="0" border="0">
     <tr>
     <td align="left">     
     <table cellpadding="0" cellspacing="0" border="0">
     <tr>
     <td align="center" colspan="2">
     <asp:Label ID="lblhead" runat="server" Text="PreLoaded Cartridge Search" CssClass="labelhead" ></asp:Label>
     </td>
     </tr>     
     <tr>
     <td align="left">
     <asp:Label ID="lbldrugname" runat="server" Text="Item Code" CssClass= "labelall"></asp:Label>
     </td>
     <td align="left">
     <table cellpadding="0" cellspacing="0" border="0">
     <tr>
      <td style="width:190px">
     <asp:TextBox ID="txtitemcode" runat="server" Width="190px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>    
     </td>
     <td align="left" style="padding-left:3px">
     <asp:Label ID="Label2" runat="server" Text="Drug Code" CssClass= "labelall" 
             Width="80px"></asp:Label>
     </td>
     <td>
      <asp:TextBox ID="txtdrugcode" runat="server" Width="186px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>    
     </td>
     </tr>
     </table>
     </td>
             
     </tr>
     <tr>
     <td align="left">
      <asp:Label ID="Label1" runat="server" Text="Item Name" CssClass= "labelall"></asp:Label>
     </td>
     <td>
     <asp:TextBox ID="txtitemname" runat="server" Width="464px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
     </td>     
     </tr> 
     <tr>
     <td>
     <asp:Label ID="lblcartno" runat="server" Text="Cartridge No" CssClass="labelall"></asp:Label>
     </td>
     <td align="left">
     <table cellpadding="0" cellspacing="0" border="0">
     <tr>
     <td>
     <asp:TextBox ID="txtcartno" runat="server" Width="190px" CssClass="textbox" AutoCompleteType="Disabled" ></asp:TextBox>
     </td>
      <td align="left" style="padding-left:3px">
     <asp:Label ID="Label4" runat="server" Text="Brand Name" CssClass="labelall" 
              Width="80px"></asp:Label>
     </td>
     <td>
     <asp:TextBox ID="txtbrand" runat="server" Width="186px" CssClass="textbox" AutoCompleteType="Disabled" ></asp:TextBox>
     </td>
     </tr>
     </table>
     </td>
     
      
     </tr>  
     <tr>
     <td align="left">
      <asp:Label ID="Label3" runat="server" Text="MFR Barcode" CssClass= "labelall" 
              Width="100px"></asp:Label>
     </td>
     <td align="left">
     <table cellpadding="0" cellspacing="0" border="0">
     <tr>
    <td>
    <asp:TextBox ID="txtmfrcode" runat="server" Width="190px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
     </td>
     <td align="left" style="padding-left:3px">
     <asp:Label ID="lblfilter" runat="server" Text="Status" CssClass="labelall" 
             Width="80px"></asp:Label>
     </td>
     <td>
     <asp:DropDownList ID="ddlfilter" runat="server" Height="22px" Width="190px" 
             CssClass="textbox" AutoPostBack="True" 
             onselectedindexchanged="ddlfilter_SelectedIndexChanged">
         <asp:ListItem>All</asp:ListItem>
         <asp:ListItem>Unverified PreLoading</asp:ListItem>
         <asp:ListItem>Rejected PreLoading</asp:ListItem>        
         </asp:DropDownList>
     </td>     
     </tr>
     </table>
     </td>
     
     </tr> 
     <tr>
      <td align="left">
<asp:Label ID="lblexpdate" runat="server" Text="PreLoaded Date" CssClass="labelall" 
             Width="90px"></asp:Label>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
        ErrorMessage="*" ControlToValidate="txt_Date"        
        ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" 
        Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
    </td>
    <td align="left">
    <table cellpadding="0" cellspacing="0" border="0">
    <tr>
   <td align="left">
<asp:textbox id="txt_Date" runat="server" Width="155px" CssClass="textbox" AutoCompleteType="Disabled"></asp:textbox>
</td>
<td align="left">
<asp:image id="imgCalendar" runat="server" ImageUrl="~/cal/calendar.gif"></asp:image>
</td>		
<td>
</td>
 <td style="padding-left:175px">
 <table cellpadding="0" cellspacing="0" border="0">
 <tr>
 <td align="left">
    <asp:Button ID="btnclear" runat="server" Text="Clear" Height="22px" 
              onclick="btnclear_Click" Width="50px" UseSubmitBehavior="False" CssClass="btn" />
 </td>
  <td>
   <%-- <asp:Button ID="btnsearch" runat="server" Text="Search" Height="22px" 
             OnClientClick = "passValueToParent()" onclick="btnsearch_Click" 
         Width="50px" CssClass="btn" /> --%>
         <asp:Button ID="btnsearch" runat="server" Text="Search" Height="22px" 
              onclick="btnsearch_Click" Width="50px" CssClass="btn" /> 
     </td> 
    </tr>
    </table>
    </td>

 </tr>
 </table>  
     </td>       
     </tr>   
     </table>    
    </td>
     </tr>
     </table>
  
     </ContentTemplate>
     </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>