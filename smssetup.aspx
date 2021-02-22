<%@ Page Language="C#" MasterPageFile="~/Site.master"  AutoEventWireup="true" CodeFile="smssetup.aspx.cs" Inherits="smssetup" %>
<asp:Content ID="content" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <asp:ScriptManager ID="scriptman" runat="server" ></asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        function smscontact() {
            var contactname = document.getElementById('<%=txtcontactname.ClientID%>').value;
            var mobno = document.getElementById('<%=txtmobnum.ClientID%>').value;
            window.open(("SMScontact.aspx?name="+contactname), 'search', 'menubar=no,center:yes,toolbar=no,scrollbars=no,width=700,height=400[color=blue]40,top=100,left=200')
            window.close();
        }
//        function smsmaster() {
//            window.open('SMSmaster.aspx', 'search', 'menubar=no,center:yes,toolbar=no,scrollbars=no,width=550,height=300[color=blue]40,top=0,left=0')
//            window.close();
//        }      
    
        function confirmProcess() {
            if (confirm('Selected records will be activated')) {
                document.getElementById('<%=btnok.ClientID%>').click();
            }

        }
        function confirmProcess1() {
            if (confirm('Selected records will be inactivated')) {
                document.getElementById('<%=btnok.ClientID%>').click();
            }

        }
        function SelectAllCheckboxes(spanChk) {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {
                if (elm[i].checked != xState)
                    elm[i].click();
            }
        }   
    </script>
<asp:UpdatePanel ID="updpanel" runat="server" >
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label2" runat="server" Text="SMS Alert" CssClass="labelhead" ></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
<tr style="padding-top:10px">
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left" style="width:270px">
<asp:Label ID="lblcontact" runat="server" CssClass="labelall" Text="Contact Person" 
        Width="212px"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtcontactname" Display="Dynamic" ErrorMessage="*" 
        SetFocusOnError="True" ValidationGroup="smssave"></asp:RequiredFieldValidator>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<%--<asp:DropDownList ID="ddlcontact" runat="server" Width="350px" ></asp:DropDownList>--%>
<asp:TextBox ID="txtcontactname" runat="server" Width="350px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td align="left" >
<%--<asp:Button ID="btnsearch" runat="server" Text="Search" Width="50px" 
        onclick="btnsearch_Click" CssClass="btn" />--%>
  <asp:ImageButton ID="btnsearch" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Search.png" onclick="btnsearch_Click" Height="20px"/>
</td>
<%--<td align="left">
<asp:Button ID="btnaddedit" runat="server" Text="Add/Edit" Width="60px" />
</td>--%>
</tr>
</table>
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
<asp:Label ID="lblmobnumber" runat="server" CssClass="labelall" Text="Mobile No" 
        Width="212px"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="txtmobnum" Display="Dynamic" ErrorMessage="*" 
        SetFocusOnError="True" ValidationGroup="smssave"></asp:RequiredFieldValidator>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtcode" runat="server" Width="50px" ReadOnly="True" CssClass="textbox" AutoCompleteType="Disabled">+65</asp:TextBox>
</td>
<td align="left" style="padding-left:3px">
<asp:TextBox ID="txtmobnum" runat="server" ReadOnly="True" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td>
 <asp:TextBox ID="searchvalue" runat="server" CausesValidation="false" Style="position: static; display: none"/> 
</td>
<td>
<asp:Button ID="btnclk" runat="server" CausesValidation="False" OnClick="btnclk_Click" Style="position: static; display: none" Text="Ok" />    
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblmsg1" runat="server" Text="Message Type" CssClass="labelall" 
        Width="270px"></asp:Label>
</td>
<td align="left">
<asp:DropDownList ID="ddlmsgtyp" runat="server" Width="214px" AutoPostBack="True" 
        onselectedindexchanged="ddlmsgtyp_SelectedIndexChanged" CssClass="textbox">
    <asp:ListItem>In Queue Alert</asp:ListItem>
   <asp:ListItem>iOPAS Processing Error</asp:ListItem>
   <asp:ListItem>iOPAS DDS/BDS Communication Error</asp:ListItem>   
    </asp:DropDownList>   
</td>
<td align="left" rowspan="3">
<table cellpadding="0" cellspacing="0" border="0" style="margin-left:7px; margin-top:0px; width: 157px;">
<tr>
<td valign="top">
<div style="height:80px; overflow:scroll; width: 269px; ">   
  <asp:GridView ID="gridpharloc" runat="server" AutoGenerateColumns="False" 
          BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
          CellPadding="1" EnableModelValidation="True" Width="245px" CssClass="gridcss" 
               TabIndex="26" ForeColor="#336600">
      <RowStyle BackColor="#EFF3FB" ForeColor="#336600" HorizontalAlign="Left" 
          VerticalAlign="Middle" Wrap="True" />
      <Columns>
         <asp:BoundField HeaderText="Pharmacy location" DataField="Location_Name" />
         <asp:TemplateField HeaderText="Select">
         <ItemTemplate>
         <asp:CheckBox ID="chkphar" runat="server" TabIndex="18" Checked='<%# chkphar(Eval("Location_Name"))%>'/>
         <%--<asp:CheckBox ID="chkphar" runat="server" TabIndex="18"/>--%>
         </ItemTemplate>
         <HeaderStyle Width="65px" />
         <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
         </asp:TemplateField>
      </Columns>
      <FooterStyle BackColor="#FFFFCC" ForeColor="#FF8000" />
      <PagerStyle BackColor="#169116" ForeColor="#FF8000" HorizontalAlign="Center" />
      <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#FF8000" />   
      <HeaderStyle BackColor="#169116" Font-Bold="True" ForeColor="White" 
        Font-Size="Small" Wrap="True" HorizontalAlign="Center" />
</asp:GridView>     
      </div>
</tr>

</table>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="Label1" runat="server" Text="Time To Alert (Minutes One)" CssClass="labelall" 
        Width="212px"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ErrorMessage="*" ControlToValidate="txtalertmin" ValidationGroup="smssave"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator1" runat="server" 
        ControlToValidate="txtalertmin" Display="Dynamic" ErrorMessage="*" 
        MaximumValue="99" MinimumValue="1" SetFocusOnError="True" Type="Integer" 
        ValidationGroup="smssave"></asp:RangeValidator>
</td>
<td align="left">
<asp:TextBox ID="txtalertmin" runat="server" MaxLength="2" Width="70px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
 </td>  
</tr>
<tr>
<td align="left">
<asp:Label ID="Label3" runat="server" Text="Time To Alert (Minutes Two)" CssClass="labelall" 
        Width="212px"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
        ErrorMessage="*" ControlToValidate="txtalertmin2" 
        ValidationGroup="smssave"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator2" runat="server" 
        ControlToValidate="txtalertmin2" Display="Dynamic" ErrorMessage="*" 
        MaximumValue="99" MinimumValue="1" SetFocusOnError="True" Type="Integer" 
        ValidationGroup="smssave"></asp:RangeValidator>
</td>
<td align="left">
<asp:TextBox ID="txtalertmin2" runat="server" MaxLength="2" Width="70px" 
        CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
 </td>  
</tr>
<tr>
<td align="left">
<asp:Label ID="lblinque" runat="server" CssClass="labelall" 
        Text="No of InQueue Orders Not Processed(Interface)" Width="240px"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
        ErrorMessage="*" ControlToValidate="txtinqueue" ValidationGroup="smssave"></asp:RequiredFieldValidator>
</td>
<td align="left" valign="top">
<asp:TextBox ID="txtinqueue" runat="server" Width="70px" BackColor="#CCCCCC" 
        ReadOnly="True" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblelapsed" runat="server" CssClass="labelall" 
        Text="Elapsed Time Since Last Processed Order(Iguana Queue in Minutes)" Width="250px"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
        ErrorMessage="*" ControlToValidate="txtlastprocess" ValidationGroup="smssave"></asp:RequiredFieldValidator>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtlastprocess" runat="server" Width="70px" BackColor="#CCCCCC" 
        ReadOnly="True" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
</table>
</td>
</tr>
<%--<tr>
<td align="left">
<asp:Label ID="Label1" runat="server" Text="Time To Alert (Minutes One)" CssClass="labelall" 
        Width="212px"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ErrorMessage="*" ControlToValidate="txtalertmin" ValidationGroup="smssave"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator1" runat="server" 
        ControlToValidate="txtalertmin" Display="Dynamic" ErrorMessage="*" 
        MaximumValue="99" MinimumValue="1" SetFocusOnError="True" Type="Integer" 
        ValidationGroup="smssave"></asp:RangeValidator>
</td>
<td align="left">
<asp:TextBox ID="txtalertmin" runat="server" MaxLength="2" Width="70px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
 </td>  
</tr>
<tr>
<td align="left">
<asp:Label ID="Label3" runat="server" Text="Time To Alert (Minutes Two)" CssClass="labelall" 
        Width="212px"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
        ErrorMessage="*" ControlToValidate="txtalertmin2" 
        ValidationGroup="smssave"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator2" runat="server" 
        ControlToValidate="txtalertmin2" Display="Dynamic" ErrorMessage="*" 
        MaximumValue="99" MinimumValue="0" SetFocusOnError="True" Type="Integer" 
        ValidationGroup="smssave"></asp:RangeValidator>
</td>
<td align="left">
<asp:TextBox ID="txtalertmin2" runat="server" MaxLength="2" Width="70px" 
        CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
 </td>  
</tr>--%>

</table>
</td>
</tr>
<tr>
<%--<td align="right" style="padding-right:130px">--%>
<td colspan="2" align="center">
<%--<asp:Button ID="btnclear" Text="Clear" runat="server"  
         Width="50px" onclick="btnclear_Click" CssClass="btn"/>--%>
    <asp:ImageButton ID="btnclear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnclear_Click" Height="20px"/>
                
<%--<asp:Button ID="btnmsgsave" Text="Save" runat="server" onclick="btnmsgsave_Click" 
        ValidationGroup="smssave" Width="50px" CssClass="btn" />--%>
    <asp:ImageButton ID="btnmsgsave" runat="server" CssClass="btn" ValidationGroup="smssave"
            ImageUrl="~/ButtonImages/Save.png" onclick="btnmsgsave_Click" Height="20px"/>

<%--<asp:Button ID="btnupdate" Text="Update" runat="server"  
ValidationGroup="smssave" Width="50px" onclick="btnupdate_Click" CssClass="btn" />--%>
    <asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" ValidationGroup="smssave"
            ImageUrl="~/ButtonImages/Save.png" onclick="btnupdate_Click" Height="20px"/>
</td>
</tr>
<tr>
<td align="left">
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<%--<asp:Button ID="btnactive" runat="server" Text="Activate" 
        onclick="btnactive_Click" Width="70px" CssClass="btn" />--%>
  <asp:ImageButton ID="btnactive" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Activate.png" onclick="btnactive_Click" Height="20px"/>
        &nbsp;
<%--<asp:Button ID="btndeactive" runat="server" Text="Inactivate" 
        onclick="btndeactive_Click" Width="70px" CssClass="btn" />--%>
  <asp:ImageButton ID="btndeactive" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Inactivate.png" onclick="btndeactive_Click" Height="20px"/>

 <asp:Button ID="btnok" runat="server" CausesValidation="False" OnClick="btnok_Click" Style="position: static; display: none" Text="Ok" />           
</td>
</tr>
<tr>
<td align="left">
<asp:GridView ID="griddetails" runat="server" AutoGenerateColumns="False" DataKeyNames="AlertID" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left" RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        onselectedindexchanging="griddetails_SelectedIndexChanging" 
        AllowPaging="True" onpageindexchanging="griddetails_PageIndexChanging" 
        CssClass="gridcss" onsorting="griddetails_Sorting" AllowSorting="True">  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns>
           <%-- <HeaderStyle Width="0px" Wrap="False" />
            <ItemStyle Font-Size="XX-Small" ForeColor="#EFF3FB" />
        </asp:BoundField>--%>
      <asp:TemplateField>
        <HeaderTemplate>
        <input id ="chkheader" type="checkbox" runat="server" onclick="SelectAllCheckboxes(this);" />
        </HeaderTemplate>
         <ItemTemplate>
         <asp:CheckBox ID="chkrow" runat="server" />
         </ItemTemplate>
          <ItemStyle HorizontalAlign="Center" />
         </asp:TemplateField> 
        <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
         <asp:BoundField DataField="AlertID" Visible="False" />
        <asp:BoundField DataField="Contact_Person" HeaderText="Contact Person" SortExpression="Contact_Person" />
        <asp:BoundField DataField="mobileno" HeaderText="Mobile No" SortExpression="mobileno" />
        <asp:BoundField DataField="Message_Type" HeaderText="Message Type" SortExpression="Message_Type" />
        <asp:BoundField DataField="Alert_Minutes1" HeaderText="Message(Minutes One)" SortExpression="Alert_Minutes1" >
        <ItemStyle HorizontalAlign="Center" Width="80px" />
        </asp:BoundField>
         <asp:BoundField DataField="Alert_Minutes2" HeaderText="Message(Minutes Two)" SortExpression="Alert_Minutes2">
           <ItemStyle HorizontalAlign="Center" Width="83px" />
           </asp:BoundField>
        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
        <asp:BoundField DataField="Created_by" HeaderText="Created by" SortExpression="Created_by" />
        <asp:BoundField DataField="Created_Date" HeaderText="Created Date Time" SortExpression="Created_Date" />
        <asp:BoundField DataField="Updated_by" HeaderText="Updated by" SortExpression="Updated_by"/>
        <asp:BoundField DataField="Updated_Date" HeaderText="Updated Date Time" SortExpression="Updated_Date"/>
    </Columns>
      <FooterStyle BackColor="#507CD1" Font-Bold="False" ForeColor="#FF8000" />
    <PagerStyle BackColor="#169116" ForeColor="White" HorizontalAlign="Right" />    
    <HeaderStyle BackColor="#169116" Font-Bold="True" ForeColor="White" 
         Wrap="True" />
    <EditRowStyle BackColor="#2461BF" ForeColor="#CC3300" />    
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
