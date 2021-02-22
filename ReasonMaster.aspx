<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ReasonMaster.aspx.cs" Inherits="ReasonMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="scrip1" runat="server" ></asp:ScriptManager>    
    <script language="javascript" type="text/javascript">
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
<asp:UpdatePanel ID="updatdrug" runat="server" >
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label2" runat="server" Text="Reason Master" CssClass="labelhead"></asp:Label>
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
<asp:Label ID="lbltype" runat="server" CssClass="labelall" Text="Reason Type" 
        Width="100px"></asp:Label>
</td>
<td align="left">
<asp:DropDownList ID="ddltype" runat="server" Width="222px" CssClass="textbox" 
        onselectedindexchanged="ddltype_SelectedIndexChanged" >
    <asp:ListItem>Inventory Discrepancy</asp:ListItem>
    <asp:ListItem>Verification Rejection</asp:ListItem>
    </asp:DropDownList>
</td>
</tr>
<tr>
<td align="left" style="width:65px;">
<asp:Label ID="lblerror" runat="server" Text="Reason" CssClass="labelall"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtreason" ErrorMessage="*" 
        ValidationGroup="rejected" Display="Dynamic"></asp:RequiredFieldValidator>
</td>
<td align="left">
<%--<asp:TextBox ID ="txterror" runat="server" Width="217px" onfocus="void clearText()" onblur="void resetText()" >Enter The Reason</asp:TextBox>--%>
<asp:TextBox ID ="txtreason" runat="server" Width="217px" CssClass="textbox" 
        AutoCompleteType="Disabled" MaxLength="20"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lbldetails" runat="server" Text="Details" CssClass="labelall"></asp:Label>
   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtdetails" ErrorMessage="*" 
        ValidationGroup="rejected" Display="Dynamic"></asp:RequiredFieldValidator>--%>
</td>
<td align="left">
<asp:TextBox ID="txtdetails" runat="server" TextMode="MultiLine" Width="300px" 
        Height="56px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
<tr>
<td colspan="2" align="right">
<%-- <asp:Button ID="btnadd" Text="Clear" runat="server" onclick="btnadd_Click" CssClass="btn"/>--%>
  <asp:ImageButton ID="btnadd" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnadd_Click" Height="20px"/>
&nbsp;
<%--<asp:Button ID="btnprocess" Text="Save" runat="server" onclick="btnprocess_Click" 
 ValidationGroup="rejected" CssClass="btn" />--%>
  <asp:ImageButton ID="btnprocess" runat="server" CssClass="btn" ValidationGroup="rejected"
            ImageUrl="~/ButtonImages/Save.png" onclick="btnprocess_Click" 
        Height="20px"/>
  <asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" ValidationGroup="rejected"
            ImageUrl="~/ButtonImages/Update.png"  Height="20px" 
        onclick="btnupdate_Click"/>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<%--<asp:Button ID="btnactive" runat="server" Text="Activate" 
        onclick="btnactive_Click" Width="61px" CssClass="btn"/>--%>
    <asp:ImageButton ID="btnactive" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Activate.png" onclick="btnactive_Click" Height="20px"/>      
        &nbsp;
<%--<asp:Button ID="btndeactive" runat="server" Text="Inactivate" Width="63px" 
        onclick="btndeactive_Click" CssClass="btn"/>--%>
   <asp:ImageButton ID="btndeactive" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Inactivate.png" onclick="btndeactive_Click" Height="20px"/>     
<asp:Button ID="btnok" runat="server" CausesValidation="False" OnClick="btnok_Click" Style="position: static; display: none" Text="Ok" />   
</td>
</tr>
<tr>
<td>
<asp:GridView ID="gridreason" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        onselectedindexchanging="gridreason_SelectedIndexChanging" 
        AllowPaging="True" CssClass="gridcss" 
        onpageindexchanging="gridreason_PageIndexChanging" 
        onsorting="gridreason_Sorting" AllowSorting="True">  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />  
    <Columns>
        <asp:TemplateField>
        <HeaderTemplate>
        <input id="chkqueall" onclick="SelectAllCheckboxes(this);" runat="server" type="checkbox" />
        </HeaderTemplate>
        <ItemTemplate>
        <asp:CheckBox ID="chkphar" runat="server" />
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>        
        <asp:CommandField SelectText="Edit" ShowSelectButton="True"/>        
        <asp:BoundField DataField="Reason_type" HeaderText="Reason Type" SortExpression="Reason_type"/>
        <asp:BoundField DataField="Reason" HeaderText="Reason" SortExpression="Reason"/>        
        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status"/>        
        <asp:BoundField DataField="Created_by" HeaderText="Created by" SortExpression="Created_by"/>
        <asp:BoundField DataField="Created_date" HeaderText="Created Date Time" SortExpression="Created_date"/>
        <asp:BoundField DataField="Updated_by" HeaderText="Updated by" SortExpression="Updated_by" />
        <asp:BoundField DataField="Updated_dat" HeaderText="Updated Date Time" SortExpression="Updated_dat"/>        
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
