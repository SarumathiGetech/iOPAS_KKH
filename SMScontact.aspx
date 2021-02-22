<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SMScontact.aspx.cs" Inherits="SMScontact" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>iOPAS</title>
  <link type="text/css" rel="stylesheet" href="css/cal.css" />  
  <script language="JavaScript" type="text/jscript">

      function closepopup() {
          window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnclk').click();            
            // window.open("SMScontact.aspx", 'search', 'menubar=no,center:yes,toolbar=no,scrollbars=no,width=700,height=400[color=blue]40,top=100,left=200')
             window.close();
         }

         function passValueToParent(nme,mob) {
             window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtcontactname').value = nme;
             window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtmobnum').value = mob;
             window.opener.document.getElementById('ctl00_ContentPlaceHolder1_searchvalue').value = mob;
             window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnclk').click();           
             //window.open("SMScontact.aspx", 'search', 'menubar=no,center:yes,toolbar=no,scrollbars=no,width=700,height=400[color=blue]40,top=100,left=200')
             window.close();
         }
         function closer() {            
             window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnclk').click();
         }

         function Intcheck(e) {
             var key = window.event ? e.keyCode : e.which;
             var keychar = String.fromCharCode(key);
             if (((keychar < "0") || (keychar > "9")) && (keychar != "."))
                 return false;
             else
                 return true;
         }      
</script>
</head>
<body onbeforeunload="closer()">
    <form id="form1" runat="server" style="background-color:#D2FECF">
    <div>
    <asp:ScriptManager ID="scrman" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="updpan" runat="server" >
    <ContentTemplate>
    <table align="center" cellpadding="0" cellspacing="0" border="0" >
     <tr>
     <td align="center">
     <asp:Label ID="lblhead" runat="server" Text="SMS Contact Details" CssClass="labelhead"></asp:Label>
     </td>
     </tr>
    </table>
    <table align="center" cellpadding="0" cellspacing="0" border="0" >    
     <tr>
<td align="left">
<asp:Label ID="lblcontact" runat="server" CssClass="labelall" Text="Contact Person" 
        Width="90px"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtcontact" Display="Dynamic" ErrorMessage="*" 
        SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtcontact" runat="server" Width="400px" CssClass="textbox" AutoCompleteType="Disabled"> </asp:TextBox>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblmobnumber" runat="server" CssClass="labelall" Text="Mobile No"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtmobnum" Display="Dynamic" ErrorMessage="*" 
        SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="txtcode" Display="Dynamic" ErrorMessage="*" 
        SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtcode" runat="server" Width="50px" MaxLength="3" CssClass="textbox" AutoCompleteType="Disabled">+65</asp:TextBox>
</td>
<td align="left" style="padding-left:3px">
<asp:TextBox ID="txtmobnum" runat="server" MaxLength="8" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td align="right" >
<%--<asp:Button ID="btnsearch" runat="server" Text="Search" onclick="btnsearch_Click" CssClass="btn" />--%>
  <asp:ImageButton ID="btnsearch" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Search.png" onclick="btnsearch_Click" Height="20px"/>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lbldescription" runat="server" CssClass="labelall" 
        Text="Description" Width="110px"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtdescription" runat="server" TextMode="MultiLine" Width="400px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblactive" runat="server" Text="Active" CssClass="labelall"></asp:Label>
</td>
<td align="left">
<asp:CheckBox ID="chkactive" runat="server" />
</td>
</tr>
<tr>
<td colspan="2" align="right">
<%--<asp:Button ID="btnclear" runat="server" Text="Clear" onclick="btnclear_Click" CssClass="btn" />--%>
  <asp:ImageButton ID="btnclear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnclear_Click" Height="20px"/>

<%--<asp:Button ID="btnsave" runat="server" Text="Save" onclick="btnsave_Click" CssClass="btn" 
        ValidationGroup="save" />--%>
  <asp:ImageButton ID="btnsave" runat="server" CssClass="btn" ValidationGroup="save"
            ImageUrl="~/ButtonImages/Save.png" onclick="btnsave_Click" Height="20px"/>

<%--<asp:Button ID="btnupdate" runat="server" Text="Save"
ValidationGroup="save" onclick="btnupdate_Click" CssClass="btn" />--%>
  <asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" ValidationGroup="save"
            ImageUrl="~/ButtonImages/Save.png" onclick="btnupdate_Click" 
        Height="20px"/>
</td>
</tr>
</table>   
<table  width="670px" cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:GridView ID="griduser" runat="server" AutoGenerateColumns="False" DataKeyNames="SMS_UserID" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        onselectedindexchanging="griduser_SelectedIndexChanging" 
        onrowdatabound="griduser_RowDataBound" onrowcommand="griduser_RowCommand" 
        CssClass="gridcss" AllowPaging="True" 
        onpageindexchanging="griduser_PageIndexChanging" 
        onsorting="griduser_Sorting" AllowSorting="True">  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns> 
  <%--   <asp:TemplateField>                   
        <ItemTemplate>
        <asp:Button ID="btnSelect" runat="server" Text="Select" Font-Bold="false" Font-Size="X-Small" Height="18px" />
        </ItemTemplate>
     </asp:TemplateField> --%>  
       <asp:ButtonField Text="Select" ButtonType="Image" CommandName="Btn" 
            ImageUrl="~/ButtonImages/GridSelect.png">            
                  <ControlStyle Height="18px" Font-Size="X-Small" />
                  <HeaderStyle Height="18px" />
                  <ItemStyle Height="18px"/>
             </asp:ButtonField> 
        <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
      <%--  <asp:BoundField DataField="SMS_UserID">
            <HeaderStyle Width="0px" Wrap="False" />
            <ItemStyle Font-Size="XX-Small" ForeColor="#EFF3FB" />
        </asp:BoundField>--%>
        <asp:BoundField DataField="Contact_Person" HeaderText="Contact Person" SortExpression="Contact_Person" />
        <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" SortExpression="MobileNo" />       
        <asp:BoundField DataField="Status" HeaderText="Active" SortExpression="Status" />
        <asp:BoundField DataField="Created_by" HeaderText="Created by" SortExpression="Created_by" />
        <asp:BoundField DataField="Created_Date" HeaderText="Created Date Time" SortExpression="Created_Date" />
        <asp:BoundField DataField="Updated_by" HeaderText="Updated by" SortExpression="Updated_by" />
        <asp:BoundField DataField="Updated_Dates" HeaderText="Updated Date Time" SortExpression="Updated_Dates" />
    </Columns>
    <FooterStyle BackColor="#507CD1" Font-Bold="False" ForeColor="#FF8000" />
    <PagerStyle BackColor="#169116" ForeColor="White" HorizontalAlign="Right" />    
    <HeaderStyle BackColor="#169116" Font-Bold="True" ForeColor="White" 
         Wrap="True" />
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
</ContentTemplate>    
</asp:UpdatePanel>
</div>
</form>
</body>
</html>
