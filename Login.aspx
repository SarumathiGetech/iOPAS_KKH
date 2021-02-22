<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">   
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<%--<meta http-equiv="X-UA-Compatible" content="IE=7" />--%>
<%--<meta http-equiv="X-UA-Compatible" content="IE=8" />--%>
<%--<meta http-equiv="X-UA-Compatible" content="IE=5; IE=7"/>--%>
<%--<META HTTP-EQUIV="Pragma" CONTENT="no-cache">--%>
<%--<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="-1" />--%>
    <title>Opas</title>   
    <link type="text/css" rel="Stylesheet" href="CSS/cal.css" />
   <script language ="JavaScript" type="text/jscript">
      javascript: window.history.forward(1);      
       function MM_displayStatusMsg(msgStr) {          
  status=msgStr;
  document.MM_returnValue = true;
}
// Login using Enter Key \\ 
function doClick(e) {
    var key;

    if (window.event)
        key = window.event.keyCode;     //IE
    else
        key = e.which;     //firefox

    if (key == 13) {
        document.getElementById('<%=lblloading.ClientID%>').innerHTML = 'Login In progress.....'; 
        
        document.getElementById('<%=Btnlogin.ClientID%>').focus();
        document.getElementById('<%=Btnlogin.ClientID%>').click();        
        event.keyCode = 0
    }
}

// Label Display \\ 
function doClick1() {

    if ((document.getElementById('<%=Txtuname.ClientID%>').value!='') && (document.getElementById('<%=TxtPass.ClientID%>').value!='')) {
        document.getElementById('<%=lblloading.ClientID%>').innerHTML = 'Login In progress.....';      
        
    }
}
</script>

   <script language="javascript" type="text/javascript">

       var submitFormOkay = false;
       function DisableButtons() {
           var inputs = document.getElementsByTagName("INPUT");
           var target = document.activeElement.toString();

           for (var i in inputs) {

               if (inputs[i].type == "submit" || inputs[i].type == "image") {

                   if (!target.match("closeCalendar") && (!target.match("Attachment.aspx"))) {
                       inputs[i].disabled = true;
                   }
               }
           }
       }

       window.onbeforeunload = DisableButtons;    
            
</script>
</head>

    <body onload ="MM_displayStatusMsg('www.getecha.com!!!');return document.MM_returnValue" > 
    <%--<body>--%>
    <form id="Form1" method="post" runat="server" target="Content" defaultfocus="Txtuname">  
   <%-- <form id="form1" runat="server" defaultfocus="Txtuname">     --%> 
    <div> 
   
     <table width="100%" cellpadding="0" cellspacing="0" border="0">
    <tr>
 
  <%--  <td align="center" style=" height: 70px;">
        <asp:ImageMap ID="ImageMap1" runat="server" ImageUrl="~/Image/iOPASBanner.gif" 
            Height="70px" Width="100%">
        </asp:ImageMap>
        </td> --%>
        
        <td align="left" valign="top" style="height: 70px; width:265px;">
          <asp:ImageMap ID="ImageMap1" runat="server" ImageUrl="~/Image/banner1.gif" 
            Height="70px" Width="265px">
        </asp:ImageMap>
        </td>   

       <td align="left" valign="top" style=" height: 70px;">
          <asp:ImageMap ID="ImageMap2" runat="server" ImageUrl="~/Image/banner2.gif" 
            Height="70px" Width="100%">
        </asp:ImageMap>
        </td>  
          <td align="left" valign="top" style=" height: 70px;width:430px;">
          <asp:ImageMap ID="ImageMap3" runat="server" ImageUrl="~/Image/banner3.gif" 
            Height="70px" Width="430px">
        </asp:ImageMap>
        </td>  
            <td align="left" valign="top" style=" height: 70px">
          <asp:ImageMap ID="ImageMap4" runat="server" ImageUrl="~/Image/banner4.gif" 
            Height="70px" Width="100%">
        </asp:ImageMap>
        </td> 
            <td align="right" valign="top" style=" height: 70px;width:265px;">
          <asp:ImageMap ID="ImageMap5" runat="server" ImageUrl="~/Image/banner5.gif" 
            Height="70px" Width="265px">
        </asp:ImageMap>
        </td> 
        </tr>        
        </table>     
     <table width="100%" cellpadding="0" cellspacing="0" border="0">
     <tr style="height:80px;">  
     <td style="vertical-align:top">
     <table align="right" style="vertical-align:top"  cellpadding="0" cellspacing="0" border="0">
     <tr>
       <td  style="vertical-align:top; padding-right:5px" >  
     <asp:Label ID="lblversion" runat="server" Text="1.0.0.28" CssClass="labelall"></asp:Label>     
     </td>
     </tr>
     <tr>
       <td style="vertical-align:top; padding-right:5px" >  
     <asp:Label ID="lblapp" runat="server" Text="" CssClass="labelall"></asp:Label>     
     </td>
     </tr>
       </table>
     </td> 
   
 
     </tr>   
     </table>
   
    <table align="center" cellpadding="5px" cellspacing="5px"     
      style="border: thick inset #006600; background-color: #D2FECF; padding-top:100px;">
   
    <tr>
    <td colspan="2" align="center" 
            style="border-bottom: solid 1px #ffc0a0; border-bottom-color: #009900;">
    <asp:Label runat="server" ID="Title" ForeColor="Black" Font-Bold="False" CssClass="loginlabel">iOPAS LOGIN</asp:Label>      
     </td>     
     </tr>
     <tr> 
     <td style=" height:10px">
     <asp:Label runat="server" ID="lbluserid" CssClass="labelall" Font-Bold="False" 
             Width="120px"> User ID</asp:Label>     
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
             ControlToValidate="Txtuname" ErrorMessage="*" SetFocusOnError="True" 
             ValidationGroup="lgn"></asp:RequiredFieldValidator>
     </td>         
     <td> 
     <asp:TextBox ID="Txtuname" runat="server" CssClass="textbox" 
             AutoCompleteType="Disabled" Width="170px" ></asp:TextBox>
     </td>
     </tr>
     <tr>
     <td>     
     <asp:Label runat="server" ID="lblpass" CssClass="labelall" Font-Bold="False" 
             Width="120px">Password</asp:Label>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
             ControlToValidate="TxtPass" ErrorMessage="*" SetFocusOnError="True" 
             ValidationGroup="lgn"></asp:RequiredFieldValidator>
     </td>
     <td>
     <asp:TextBox ID="TxtPass" runat="server" TextMode="Password" CssClass="textbox" 
             Width="170px" ></asp:TextBox>
     </td>
     </tr>
     <tr><td> <asp:Label runat="server" ID="lblPharmacyLocation" CssClass="labelall" Font-Bold="False" 
             Width="120px">Pharmacy Location</asp:Label>
     </td>
     <td>
    <asp:DropDownList ID="dllPharmacyLocation" AutoPostBack="false" Width="175px" CssClass="textbox" runat="server"  >
    </asp:DropDownList>
     </td></tr>
     <tr>
     <td>

     </td>
     <td >
  <%--   <asp:Button ID="Btnlogin" runat="server" Text="Enter" CssClass="btn"
             onclick="Btnlogin_Click" ValidationGroup="lgn" />--%>

  <asp:ImageButton ID="Btnlogin" runat="server" CssClass="btn" ValidationGroup="lgn"
            ImageUrl="~/ButtonImages/Enter24.png" onclick="Btnlogin_Click" Width="100px" 
             Height="24px"  />
     </td>
     </tr>
    <%-- <tr>
     <td colspan="2" align="center">
     <asp:Label ID="lblmsg" runat="server" 
             Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
     </td>     
     </tr> --%>  
      
     </table>
    
   <table align="center">   
     <tr>
     <td align="center">
     <asp:Label runat="server" ID="lblloading"  Font-Bold="True"
              ForeColor="#000099" Font-Names="Arial" Font-Size="10pt"></asp:Label>
     </td>     
     </tr>
     <tr>
     <td align="center">
     <asp:Label ID="lblmsg" runat="server" 
             Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
     </td>     
     </tr>
   </table> 
    </div>
    </form>
</body>
</html>
