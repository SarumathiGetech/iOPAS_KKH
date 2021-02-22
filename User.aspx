<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="User.aspx.cs" Inherits="User" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="head">    
</asp:Content>
<asp:Content ID="contenmt2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager> 
<script language="javascript" type="text/javascript" src="cal/popcalendar.js" ></script>   
    <script language="javascript" type="text/javascript">
        function keyval() {
            var textboxText = document.getElementById('<%=txtpass.ClientID%>').value;
            document.getElementById('<%=txtprintpass.ClientID%>').value = textboxText;
        }
        function CallPrint() {
            var Password = document.getElementById('<%=txtprintpass.ClientID%>').value;
            var userid = document.getElementById('<%=txtid.ClientID%>').value;
            var windowUrl = 'OPAS';
            var windowName = 'Print' + new Date().getTime();
            var printWindow = window.open(windowUrl, windowName,'left=50000,top=50000,width=0,height=0');
            Password = 'Password ' + ' ::  ' + Password;
            userid = 'User ID ' + ' ::  ' + userid;           
            printWindow.document.write(userid);
            printWindow.document.write("<br />");
            printWindow.document.write("<br />");
            printWindow.document.write("<br />");
            printWindow.document.write(Password);                              
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        }

        function Intcheck(e) {

            var key = window.event ? e.keyCode : e.which;
            var keychar = String.fromCharCode(key);      
            if (((keychar < "A") || (keychar > "Z")) && ((keychar < "a") || (keychar > "z")) && event.keyCode!=32)
                return false;
            else
                return true;
        }
        // Search Window open using Enter Key \\ 
        function doClick(e) {
            var key;

            if (window.event)
                key = window.event.keyCode;     //IE
            else
                key = e.which;     //firefox

            if (key == 13) {
                document.getElementById('<%=btnsearch.ClientID%>').click();
                event.keyCode = 0
            }
        }     
           
    </script> 
<asp:UpdatePanel ID="updcell" runat="server">
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label2" runat="server" Text="User Creation" CssClass="labelhead"></asp:Label>
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
<asp:Label id="lblid" runat="server" Text="User ID" CssClass="labelall" ></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="txtid" Display="Dynamic" ErrorMessage="*" 
        SetFocusOnError="True" ValidationGroup="print"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="userid" runat="server" 
ErrorMessage="*" ControlToValidate="txtid" ValidationGroup="Login" 
        SetFocusOnError="True"></asp:RequiredFieldValidator> 
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left" width="175px">
<asp:TextBox id="txtid" runat="server" Width="170px" MaxLength="20" 
        ValidationGroup="Login" CssClass="textbox" AutoCompleteType="Disabled" 
        TabIndex="1"></asp:TextBox>
</td>
<td align="left" style="padding-left:7px">
<asp:Label id="empno" runat="server" Text="Employee No / NRIC" CssClass="labelall"></asp:Label>
</td>
<td align="left" style="padding-left:4px">
<asp:TextBox id="txtempno" runat="server" Width="150px" CssClass="textbox" 
        AutoCompleteType="Disabled" TabIndex="1" ></asp:TextBox>
</td>
</tr>
</table>
 </td>
 </tr>
 <tr>
 <td align="left">
 <asp:Label ID="lblname" runat="server" Text="User Name" CssClass="labelall"></asp:Label> 
 <span>
 <asp:RequiredFieldValidator ID="username" runat="server" Display="Dynamic" 
         ErrorMessage="*" ControlToValidate="txtname" ValidationGroup="Login" 
         SetFocusOnError="True"></asp:RequiredFieldValidator></span>
     
 </td>
 <td align="left">
 <asp:TextBox ID="txtname" runat="server" Width="450px" MaxLength="100" 
         ValidationGroup="Login" CssClass="textbox" AutoCompleteType="Disabled" 
         TabIndex="2"></asp:TextBox> 
 </td>
 </tr>
 <%--<asp:Panel ID="passwordpanel" runat="server" >--%>
 <tr>
 <td align="left">
 <asp:Label ID="lblpass" runat="server" Text="Password" CssClass="labelall" 
         Width="90px" ></asp:Label>
 <asp:Label ID="lblpasserror" runat="server" Text="*" CssClass="labelall" 
         Width="16px" Font-Size="Large" ForeColor="Red" Font-Bold="True" ></asp:Label> 
 </td>
 <td align="left">
 <table cellpadding="0" cellspacing="0" border="0">
 <tr>
 <td align="left">
  <asp:TextBox ID="txtpass" runat="server" Width="170px" MaxLength="20" 
         ValidationGroup="Login" onkeyup="void keyval()" CssClass="textbox" 
         AutoCompleteType="Disabled" TabIndex="3"></asp:TextBox>
 </td>
 <td align="left" style="padding-left:7px">
<%-- <asp:Button ID="btnautopass" runat="server" Height="22px" 
         Text="Auto password" Width="108px" ForeColor="Black" TabIndex="5" 
         onclick="btnautopass_Click" UseSubmitBehavior="False" CssClass="btn"/>--%>
  <asp:ImageButton ID="btnautopass" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/AutoPassword.png" onclick="btnautopass_Click" 
         Height="20px" TabIndex="10"/>
 </td>
 <td align="left" style="padding-left:60px">
<%-- <asp:Button ID="btnreset" runat="server" Height="22px" Text="Reset password" Width="104px" 
        ForeColor="Black" TabIndex="6" onclick="btnreset_Click" 
         UseSubmitBehavior="False" CssClass="btn"/>  --%>
 <asp:ImageButton ID="btnreset" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/ResetPassword.png" onclick="btnreset_Click" 
         Height="20px" TabIndex="10"/>
 </td>
 </tr>
 </table>        
 </td>
 </tr>
 <%--</asp:Panel>--%>
 <tr>
 <td align="left">
 <asp:Label ID="lbldomain" runat="server" Text="Domain Name" CssClass="labelall"></asp:Label>      
     <asp:RequiredFieldValidator ID="reqdomain" runat="server" 
         ErrorMessage="*" ControlToValidate="ddldomain" ValidationGroup="Login" 
         InitialValue="-Select-" SetFocusOnError="True"></asp:RequiredFieldValidator>   
 </td>
 <td align="left">
 <asp:DropDownList ID="ddldomain" runat="server" Width="220px" 
         ValidationGroup="Login" 
         onselectedindexchanged="ddldomain_SelectedIndexChanged" CssClass="textbox" 
         TabIndex="7">
     </asp:DropDownList>   
     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
         ControlToValidate="txtpass" ErrorMessage="Password must be min 8 max 20 characters with at least one non alphabet"        
    ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9][\w!@#$%^&*]{7,20})$" 
         Font-Size="8pt" ValidationGroup="Login"></asp:RegularExpressionValidator>  
 </td> 
 </tr>
 <tr>
 <td align="left">
 <asp:Label ID="lblphar" runat="server" Text="Pharmacy Location" 
         CssClass="labelall" Width="110px"></asp:Label>    
 
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
         ControlToValidate="ddlphaecloc" ErrorMessage="*" InitialValue="-Select-" 
         SetFocusOnError="True" ValidationGroup="Login"></asp:RequiredFieldValidator> 
 </td>
 <td align="left" >
 <asp:DropDownList ID="ddlphaecloc" runat="server" Width="220px" ValidationGroup="Login" 
         onselectedindexchanged="ddlphaecloc_SelectedIndexChanged" 
         CssClass="textbox" TabIndex="8" >
 </asp:DropDownList>       
 </td> 
 <td align="left">
 <asp:TextBox ID="txtprintpass" runat="server" CausesValidation="false" Style="position: static; display: none"/>  
 </td> 
 </tr>
 <tr>
 <td align="left" >
 <asp:Label ID="lblgroup" runat="server" Text="Role" CssClass="labelall"></asp:Label>      
  </td>
 <td align="left">   
 <table cellpadding="0" cellspacing="0" border="0">
 <tr>
 <td align="left">
 <asp:ListBox ID="lstrole" runat="server" Height="79px" Width="210px" 
         CssClass="textbox" TabIndex="9" ></asp:ListBox>
 </td>
 <td align="left">
  <asp:Button id="btnmove" runat="server" Text=">>" onclick="btnmove_Click" 
         Width="30px" Font-Bold="True" 
  ForeColor="White" UseSubmitBehavior="False" TabIndex="10" BackColor="#169116" /><br />
  <asp:Button id="btnremove" runat="server" Text="<<" onclick="btnremove_Click" 
  Width="30px" Font-Bold="True" ForeColor="White" UseSubmitBehavior="False" 
         TabIndex="11" BackColor="#169116" />
 </td>
 <td align="left">
 <asp:ListBox ID="lstadd" runat="server" Height="79px" Width="210px" 
         CssClass="textbox" TabIndex="12" ></asp:ListBox>   
 </td>
 </tr>
 </table> 
 </td>  
 </tr> 
 <tr>
<td align="left">
<asp:Label ID="Label6" runat="server" Text="Effective Date From" CssClass="labelall" 
        Width="110px"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtexpdate" Display="Dynamic" ErrorMessage="*" 
        SetFocusOnError="True" ValidationGroup="Login"></asp:RequiredFieldValidator>
</td>
<td align="left" >
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtexpdate" runat="server" Width="90px" CssClass="textbox" 
        AutoCompleteType="Disabled" TabIndex="21" ></asp:TextBox> 
</td>
<td align="left" width="35px">
  <asp:image id="imgCalendar" runat="server" ImageUrl="~/cal/calendar.gif" 
        TabIndex="13"></asp:image>
</td>
<td align="left" style="padding-left:10px">
<asp:Label ID="Label7" runat="server" Text="To" CssClass="labelall" 
        Width="30px"></asp:Label></td>
<td align="left">
<asp:TextBox ID="txtdateto" runat="server" Width="90px" CssClass="textbox" 
        AutoCompleteType="Disabled" TabIndex="22" ></asp:TextBox> 
</td>
<td align="left" width="35px">
  <asp:image id="imgtwo" runat="server" ImageUrl="~/cal/calendar.gif" TabIndex="14"></asp:image>
</td>
<td align="left" style="padding-left:93px">
<asp:Label ID="lblactive" runat="server" CssClass="labelall" Text="Active"></asp:Label>
</td>
<td align="left">
<asp:CheckBox ID="chkactive" runat="server" Checked="True" TabIndex="15" />
</td>
</tr>
</table>
</td>
</tr> 
 <tr>
 <td align="left">
 <asp:Label ID="lblremark" runat="server" Text="Remarks" CssClass="labelall" 
         Width="150px"></asp:Label>
 </td>
 <td align="left">
 <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine" Width="447px" 
         Height="40px" MaxLength="255" CssClass="textbox" 
         AutoCompleteType="Disabled" TabIndex="16" ></asp:TextBox>
 </td>
 </tr>
 <tr>
 <td colspan="2" align="center" style="padding-left:180px">
<%-- <asp:Button ID="Button1" runat="server" Text="Search" onclick="Button1_Click" 
         Height="22px" CssClass="btn" TabIndex="17"/>--%>
      <asp:ImageButton ID="btnsearch" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Search.png" onclick="btnsearch_Click" 
         Height="20px"/>    
         &nbsp;
<%--  <asp:Button ID="Button2" runat="server" Height="22px" Text="Clear" Width="48px" 
        ForeColor="Black" onclick="Button2_Click" TabIndex="18" CssClass="btn"/>--%>
         <asp:ImageButton ID="btnclear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnclear_Click" Height="20px"/>   
        &nbsp;
       <%--  <asp:Button ID="btnsubmit" runat="server" Text="Save" Height="22px" Width="51px" 
         onclick="btnsubmit_Click" ValidationGroup="Login" TabIndex="19" 
         CssClass="btn" />--%>
            <asp:ImageButton ID="btnsubmit" runat="server" CssClass="btn" ValidationGroup="Login"
            ImageUrl="~/ButtonImages/Save.png" onclick="btnsubmit_Click" Height="20px"/>   
              <asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" ValidationGroup="Login"
            ImageUrl="~/ButtonImages/Update.png"  Height="20px" 
         onclick="btnupdate_Click"/> 
           &nbsp;
      <%--   <asp:Button ID="btnprint" runat="server" Text="Print" Height="22px" 
         Width="51px" TabIndex="20" onclick="btnprint_Click" 
         ValidationGroup="print" CssClass="btn" /> --%>
              <asp:ImageButton ID="btnprint" runat="server" CssClass="btn" ValidationGroup="print"
            ImageUrl="~/ButtonImages/Print.png"  Height="20px" 
         onclick="btnprint_Click"/> 
 <%--<button id="buttprint" onclick ="ss()">Print</button>--%>         
 </td>
  </tr>
  </table>
    </td>
  </tr>
  <tr>
  <td align="left">
<asp:UpdatePanel ID="updtest" runat="server">
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:GridView ID="usereditgrid" runat="server" AutoGenerateColumns="False" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" AllowSorting ="True" AllowPaging="True" onpageindexchanging="usereditgrid_PageIndexChanging" 
        onsorting="usereditgrid_Sorting" 
        onselectedindexchanging="usereditgrid_SelectedIndexChanging" 
        BackColor="#FFFFCC" CaptionAlign="Left" RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" CssClass="gridcss"> 
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns>   
        <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
        <asp:BoundField HeaderText="Pharmacy Location" DataField="Location_Name" 
            SortExpression="Location_Name"/>        
        <asp:BoundField HeaderText="User ID" DataField="UserId" 
            SortExpression="UserId"/>
        <asp:BoundField DataField="empid" HeaderText="Employee No/NRIC" 
            SortExpression="empid" />
        <asp:BoundField HeaderText="User Name" DataField="UserName" SortExpression="UserName"/>
        <asp:BoundField HeaderText="Domain" DataField="DomainName" 
            SortExpression="DomainName" />
        <asp:BoundField HeaderText="Active" DataField="Status" SortExpression="Status"/>       
        <asp:BoundField DataField="Effectivefrom" HeaderText="Effective From" SortExpression="Effectivefrom"/>  
        <asp:BoundField DataField="Effectiveto" HeaderText="Effective To" SortExpression="Effectiveto"/>
        <asp:BoundField DataField="Created_by" HeaderText="Created by" SortExpression="Created_by" />
        <asp:BoundField HeaderText="Created Date Time" DataField="Created_Date" 
            SortExpression="Created_Date" >
            <ItemStyle Wrap="True" />
        </asp:BoundField>
        <asp:BoundField DataField="Updated_by" HeaderText="Updated by" SortExpression="Updated_by"/>
        <asp:BoundField DataField="Updated_Date" HeaderText="Updated Date Time" 
            SortExpression="Updated_Date" >
            <ItemStyle Wrap="True" />
        </asp:BoundField>           
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
<td align="left" style="padding-left:300px">  
  <asp:Label ID="lblpge" runat="server" CssClass="labelall"></asp:Label> 
</td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

