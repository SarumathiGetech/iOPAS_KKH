<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Changepassword.aspx.cs" Inherits="Changepassword" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>    
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">         
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
<td align="left">
<asp:Label id="lbloldpass" runat="server" CssClass="labelall" Text="Old Password" 
        Width="110px"></asp:Label> 
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="txtOldPass" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="pwdcng"></asp:RequiredFieldValidator>
</td>
<td>
<asp:TextBox id="txtOldPass" runat="server" Width="150px" TextMode="Password"></asp:TextBox>
 </td>
 <td align="left">  
  <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
         ControlToValidate="txtOldPass" ErrorMessage="Password must be min 8 max 20 characters with at least one non alphabet"        
    ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9!@#$%^&*_]{8,20})$" 
         Font-Size="Smaller" SetFocusOnError="True" ValidationGroup="pwdcng" 
         Display="Dynamic"></asp:RegularExpressionValidator>
        
   </td>
    </tr>
 <tr>
 <td align="left"> 
 <asp:Label id="lblnewpass" runat="server" CssClass="labelall" Text="New Password" 
         Width="110px"></asp:Label>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
         ControlToValidate="txtnewpass" Display="Dynamic" ErrorMessage="*" 
         SetFocusOnError="True" ValidationGroup="pwdcng"></asp:RequiredFieldValidator>
  </td>
  <td align="left">
  <asp:TextBox id="txtnewpass" runat="server" Width="150px" TextMode="Password"></asp:TextBox>
   </td>
 <td align="left">  
  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
         ControlToValidate="txtnewpass" ErrorMessage="Password must be min 8 max 20 characters with at least one non alphabet"        
    ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9!@#$%^&*_]{8,20})$" 
         Font-Size="Smaller" SetFocusOnError="True" ValidationGroup="pwdcng" 
         Display="Dynamic"></asp:RegularExpressionValidator>
   </td>
   </tr>
   <tr>
   <td align="left">
   <asp:Label id="lblconpass" runat="server" CssClass="labelall" 
           Text="Confirm Password" Width="110px"></asp:Label> 
       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
           ControlToValidate="txtconpass" Display="Dynamic" ErrorMessage="*" 
           SetFocusOnError="True" ValidationGroup="pwdcng"></asp:RequiredFieldValidator>
   </td>
   <td align="left">
   <asp:TextBox id="txtconpass" runat="server" Width="150px" TextMode="Password"></asp:TextBox> 
   </td>
   <td align="left">     
        <asp:CompareValidator ID="CompareValidator1" runat="server" 
            ErrorMessage="Password Does Not Match" 
            ControlToCompare="txtnewpass" ControlToValidate="txtconpass" Display="Dynamic" 
            SetFocusOnError="True" ValidationGroup="pwdcng" Font-Size="Smaller"></asp:CompareValidator>     
     </td>
   </tr> 
  
    <tr>
    <td align="center" colspan="2">
  <%--  <asp:Button id="btnupdate" runat="server" Text="Save" OnClick="btnsubmit_Click" 
            ValidationGroup="pwdcng" CssClass="btn"></asp:Button>  --%>  
  <asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" ValidationGroup="pwdcng"
            ImageUrl="~/ButtonImages/Save.png" onclick="btnsubmit_Click" Height="20px"/>
     </td>    
     </tr>
     </table>
  </td>
     </tr>
     </table>
</ContentTemplate>         
         </asp:UpdatePanel>
</asp:Content>

