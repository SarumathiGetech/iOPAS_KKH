<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Changepwd.aspx.cs" Inherits="Changepwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <link type="text/css" rel="Stylesheet" href="CSS/cal.css" /> 
    <title> Opas</title>
    <script language="javascript" type="text/javascript">

        function Success() {
            alert('Your new password successfully updated.You will be redirected to the login page');
            window.location.href = 'iOpas.html';   
      }
    
    </script>
</head>
<body>
    <form id="form1" runat="server" style="background-color:#D2FECF">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
           <asp:UpdatePanel ID="updpanel1" runat="server">
        <ContentTemplate>
        <table width="100%" align="center">
    <tr>
 <%--   <td align="center"style="width: 100%; height:70px;">
        <asp:ImageMap ID="ImageMap1" runat="server" ImageUrl="~/Image/iOPASBanner.gif" 
            Height="70px" Width="100%">
        </asp:ImageMap></td>--%>
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
     </ContentTemplate>
     </asp:UpdatePanel>
      <asp:UpdatePanel ID="updpanel2" runat="server">
     <ContentTemplate>
     <table width="100%" cellpadding="0" cellspacing="0" border="0">
      <tr>
   <td>
   <table align="center" cellpadding="0" cellspacing="0" border="0">
   <tr>
<td>
<asp:Label id="Label2" runat="server" Text="Change Password" CssClass="labelhead"></asp:Label> 
</td>
</tr>
   </table>
   </td>
   </tr>
     <tr>
     <td align="left">
     
  <table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left" style="padding-left:200px">
<asp:Label id="lbloldpass" runat="server" CssClass="labelall" Text="Old Password" 
        Width="110px"></asp:Label> 
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="txtOldPass" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="cnpwd"></asp:RequiredFieldValidator>
</td>
<td>
<asp:TextBox id="txtOldPass" runat="server" Width="150px" TextMode="Password"></asp:TextBox>
 </td>
  <td align="left" >
<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
         ControlToValidate="txtOldPass" ErrorMessage="Password must be min 8 max 20 characters with at least one non alphabet"        
    ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9!@#$%^&*_]{8,20})$"
         Font-Size="Smaller" ValidationGroup="cnpwd" SetFocusOnError="True" 
         Display="Dynamic"></asp:RegularExpressionValidator>  
   </td>
 </tr>
 <tr>
 <td align="left" style="padding-left:200px"> 
 <asp:Label id="lblnewpass" runat="server" CssClass="labelall" Text="New Password" 
         Width="110px"></asp:Label>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
         ControlToValidate="txtnewpass" Display="Dynamic" ErrorMessage="*" 
         SetFocusOnError="True" ValidationGroup="cnpwd"></asp:RequiredFieldValidator>
  </td>
  <td>
  <asp:TextBox id="txtnewpass" runat="server" Width="150px" TextMode="Password"></asp:TextBox>
   </td>
 <td align="left" >
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
         ControlToValidate="txtnewpass" ErrorMessage="Password must be min 8 max 20 characters with at least one non alphabet"        
    ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9!@#$%^&*_]{8,20})$"
         Font-Size="Smaller" ValidationGroup="cnpwd" SetFocusOnError="True" 
         Display="Dynamic"></asp:RegularExpressionValidator>  
   </td>
   </tr>
   <tr>
   <td align="left" style="padding-left:200px">
   <asp:Label id="lblconpass" runat="server" CssClass="labelall" 
           Text="Confirm Password" Width="110px"></asp:Label> 
       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
           ControlToValidate="txtconpass" Display="Dynamic" ErrorMessage="*" 
           SetFocusOnError="True" ValidationGroup="cnpwd"></asp:RequiredFieldValidator>
   </td>
   <td>
   <asp:TextBox id="txtconpass" runat="server" Width="150px" TextMode="Password"></asp:TextBox> 
   </td>
     <td align="left">
        <asp:CompareValidator ID="CompareValidator1" runat="server" 
            ErrorMessage="Password Does Not Match" 
            ControlToCompare="txtnewpass" ControlToValidate="txtconpass" 
            SetFocusOnError="True" Display="Dynamic" ValidationGroup="cnpwd" 
            Font-Size="Smaller"></asp:CompareValidator>   
     </td>
   </tr>  
      
    <tr>
    <td align="right" colspan="2">
<%--    <asp:Button id="btnupdate" runat="server" Text="Save" onclick="btnupdate_Click" 
            ValidationGroup="cnpwd" CssClass="btn"></asp:Button>--%>    
  <asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" ValidationGroup="cnpwd"
            ImageUrl="~/ButtonImages/Save.png" onclick="btnupdate_Click" Height="20px"/>
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
