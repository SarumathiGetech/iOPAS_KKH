<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Cartridgeunloading.aspx.cs" Inherits="Cartridgeunloading" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>
       <script language="javascript" type="text/javascript">

           function confirmProcess(msg) {
               if (confirm(msg)) {
                   document.getElementById('<%=btnok.ClientID%>').click();
               }
           }


           function STot1() {
               var m = '-';
               var s = document.getElementById('<%=txtcurrentinv.ClientID%>').value;
               var s1 = document.getElementById('<%=txtphyinventory.ClientID%>').value;
               
               if ((s * 1) >= (s1 * 1)) {

                   document.getElementById('<%=txtdiscrep.ClientID%>').value = (s * 1) - (s1 * 1);
                
               }
               else if ((s * 1) < (s1 * 1)) {
                    
                   //document.getElementById('<%=txtdiscrep.ClientID%>').value = m + ((s * 1) - (s1 * 1));
                   document.getElementById('<%=txtdiscrep.ClientID%>').value = ((s * 1) - (s1 * 1));
                   alert('Physical Balance is higher than System Balance');
               }
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
<asp:UpdatePanel ID="updpanel" runat="server">
<ContentTemplate>

<asp:Panel ID="pp" runat="server">
<table width="100%" border="0" cellpadding="0" cellspacing="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label2" runat="server" Text="Cartridge Unloading" CssClass="labelhead"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
<tr style="padding-top:10px">
<td align="left">
<table  border="0" cellpadding="0" cellspacing="0">


<tr>
<td align="left" width="125px">
<asp:Label ID="lblcartno" runat="server" Text="Cartridge No" CssClass="labelall" 
        Width="77px"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
        ControlToValidate="txtcartno" ErrorMessage="*" ValidationGroup="enter"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
        ControlToValidate="txtcartno" ErrorMessage="*" ValidationGroup="unload"></asp:RequiredFieldValidator>
</td>
<td align="left">
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left">
<asp:TextBox ID="txtcartno" runat="server" Width="200px" MaxLength="6" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td align="left">
<%--<asp:Button ID="Btngo" runat="server" Text="Enter" onclick="Btngo_Click" CssClass="btn" 
        ValidationGroup="enter" />--%>

  <asp:ImageButton ID="Btngo" runat="server" CssClass="btn" ValidationGroup="enter"
            ImageUrl="~/ButtonImages/Enter24.png" onclick="Btngo_Click" 
        Height="22px"  />
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left" width="125px">
<asp:Label ID="Llblitemcode" runat="server" Text="Item Code" CssClass="labelall" 
        Width="122px"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtitemcode" runat="server" Width="350px" ReadOnly="True" 
        BackColor="#CCCCCC" CssClass="textbox" AutoCompleteType="Disabled" 
        TabIndex="1"></asp:TextBox>        
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lbldrg" runat="server" Text="Item Name" CssClass="labelall" 
        Width="122px"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtitemname" runat="server" Width="350px" ReadOnly="True" 
        BackColor="#CCCCCC" CssClass="textbox" AutoCompleteType="Disabled" 
        TabIndex="1"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left" >
<asp:Label ID="lblbalance" runat="server" Text="System Balance" CssClass="labelall"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
        ControlToValidate="txtcurrentinv" ErrorMessage="*" ValidationGroup="unload"></asp:RequiredFieldValidator>
</td>
<td align="left" style="height: 23px">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtcurrentinv" runat="server" Width="50px" 
        BackColor="#CCCCCC" ReadOnly="True" CssClass="textbox" 
        AutoCompleteType="Disabled" TabIndex="1"></asp:TextBox>
</td>
<td align="left" style="padding-left:10px">
<asp:Label ID="lbluom2" runat="server" CssClass="labelall"></asp:Label>
</td>
</tr>
</table> 
</td>
</tr>
<tr>
<td align="left" >
<asp:Label ID="Label6" runat="server" Text="Physical Balance" CssClass="labelall"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtphyinventory" ErrorMessage="*" ValidationGroup="unload"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator1" runat="server" 
        ControlToValidate="txtphyinventory" ErrorMessage="*" MaximumValue="999" 
        MinimumValue="0" SetFocusOnError="True" Type="Integer" ValidationGroup="unload"></asp:RangeValidator>
</td>
<td align="left" style="height: 23px">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtphyinventory" runat="server" Width="50px" 
        onkeyup ="void STot1()" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td align="left" style="padding-left:10px">
<asp:Label ID="lbluom3" runat="server" CssClass="labelall"></asp:Label>
</td>
</tr>
</table>   
</td>
</tr>
<tr>
<td align="left" >
<asp:Label ID="lbldiscrep" runat="server" Text="Discrepancy" CssClass="labelall"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtdiscrep" ErrorMessage="*" ValidationGroup="unload"></asp:RequiredFieldValidator>
</td>
<td align="left" style="height: 23px">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtdiscrep" runat="server" Width="50px" 
        BackColor="#CCCCCC" ReadOnly="false" CssClass="textbox" 
        AutoCompleteType="Disabled" TabIndex="1" Font-Bold="True" ></asp:TextBox>
</td>
<td align="left" style="padding-left:10px">
<asp:Label ID="lbluom4" runat="server" CssClass="labelall"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lbldisdescr" runat="server" Text="Reason" CssClass="labelall" ></asp:Label>    
<%--<asp:RequiredFieldValidator ID="Req" runat="server" 
        ControlToValidate="ddlreason" Display="Dynamic" ErrorMessage="*" 
        InitialValue="-Select-" SetFocusOnError="True" ValidationGroup="unload"></asp:RequiredFieldValidator>--%>
</td>
<td align="left">
<asp:DropDownList ID="ddlreason" runat="server" Width="208" CssClass="textbox" >    
    </asp:DropDownList>            
</td>
</tr>
<tr>
<td colspan="2" align="right">

  <asp:ImageButton ID="btnclear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnclear_Click" Height="20px"  />
  <asp:ImageButton ID="btnsave" runat="server" CssClass="btn" ValidationGroup="unload" 
            ImageUrl="~/ButtonImages/Save.png" onclick="btnsave_Click" Height="20px"  />
    <asp:Button ID="btnok" runat="server" CausesValidation="False" 
            OnClick="btnok_Click" Style="position: static; display: none" Text="Ok" />

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


