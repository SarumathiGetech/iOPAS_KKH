<%@ Page Language="C#" MasterPageFile="~/Site.master"  AutoEventWireup="true" CodeFile="Domainmaster.aspx.cs" Inherits="Domainmaster" %>
<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">     
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
<asp:UpdatePanel ID="updatdrug" runat="server">
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label2" runat="server" Text="Domain Name" CssClass="labelhead"></asp:Label>
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
<asp:Label ID="lbldomain" runat="server" Text="Domain" CssClass="labelall"></asp:Label>
    <asp:RequiredFieldValidator ID="valdomain" runat="server" ErrorMessage="*" 
        ValidationGroup="Domain" ControlToValidate="txtdomain" Display="Dynamic"></asp:RequiredFieldValidator>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:TextBox ID ="txtdomain" runat="server" Width="140px" CssClass="textbox" AutoCompleteType="Disabled" ></asp:TextBox>
</td>
<td>
<asp:TextBox ID ="txtroot" runat="server" Width="140px" CssClass="textbox" AutoCompleteType="Disabled" ></asp:TextBox>
</td>
<td align="left">
<asp:TextBox ID="txtcom" runat="server" Width="40px" MaxLength="3" CssClass="textbox" AutoCompleteType="Disabled" >com</asp:TextBox>
</td>
<td align="left">
<asp:TextBox ID="txtsg" runat="server" Width="40px" MaxLength="2" CssClass="textbox" AutoCompleteType="Disabled">sg</asp:TextBox>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lbldmnname" runat="server" Text="Domain Name" CssClass="labelall"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" 
        ValidationGroup="Domain" ControlToValidate="txtdmnname" Display="Dynamic"></asp:RequiredFieldValidator>
</td>
<td align="left">
<asp:TextBox ID = "txtdmnname" runat="server" CssClass="textbox" 
        AutoCompleteType="Disabled" Width="378px" MaxLength="20"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lbldesc" runat="server" Text="Description" CssClass="labelall" Width="100px"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtdescrip" runat="server" TextMode="MultiLine" Width="378px" 
        Height="56px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>       
</td>
</tr>
<tr>
<td>
    &nbsp;</td>
</tr>
<tr>
<td colspan="2" align="right">
<%--<asp:Button ID="btnadd" Text="Clear" runat="server" onclick="btnadd_Click" CssClass="btn"
        Width="50px" />--%>
   <asp:ImageButton ID="btnadd" runat="server" CssClass="btn"
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnadd_Click" Height="20px"/>       
        &nbsp;
<asp:ImageButton ID="btnsave" runat="server" CssClass="btn" ValidationGroup="Domain"
            ImageUrl="~/ButtonImages/Save.png" onclick="btnsave_Click" Height="20px"/> 
             
<asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" ValidationGroup="Domain"
            ImageUrl="~/ButtonImages/Update.png"  Height="20px" 
        onclick="btnupdate_Click"/> 
<%--<asp:Button ID="btnsave" Text="Save" runat="server" ValidationGroup="Domain" CssClass="btn"
        onclick="btnsave_Click" Width="50px"/>   --%>     
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
<%--<asp:Button ID="btnactive" runat="server" Text="Activate " CssClass="btn" 
        onclick="btnactive_Click"/>--%>
  
  <asp:ImageButton ID="btnactive" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Activate.png" onclick="btnactive_Click" Height="20px"/>      
        &nbsp;
<%--<asp:Button ID="btndeactive" runat="server" Text="Inactivate" Width="68px" CssClass="btn" 
        onclick="btndeactive_Click"/>--%>
  <asp:ImageButton ID="btndeactive" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Inactivate.png" onclick="btndeactive_Click" Height="20px"/>
             
        <asp:Button ID="btnok" runat="server" CausesValidation="False" OnClick="btnok_Click" Style="position: static; display: none" Text="Ok" />   
</td>
</tr>
<tr>
<td>
<asp:GridView ID="griddomain" runat="server" AutoGenerateColumns="False" DataKeyNames="DomainID" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        onselectedindexchanging="griddomain_SelectedIndexChanging" 
        AllowPaging="True" onpageindexchanging="griddomain_PageIndexChanging" 
        CssClass="gridcss" onsorting="griddomain_Sorting" AllowSorting="True">    
    <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns>
        <asp:TemplateField>
        <HeaderTemplate>
        <input id ="chkheader" onclick="SelectAllCheckboxes(this);" runat="server" type="checkbox" />
        </HeaderTemplate>
         <ItemTemplate>
         <asp:CheckBox ID="chkrow" runat="server" />         
         </ItemTemplate>           
         <ItemStyle HorizontalAlign="Center" />
         </asp:TemplateField>        
        <asp:CommandField SelectText="Edit" ShowSelectButton="True" />        
        <asp:BoundField DataField="DomainID" Visible="false" />
          <%--  <ItemStyle ForeColor="White" Font-Size="XX-Small" />
        </asp:BoundField>--%>
        <asp:BoundField DataField="DomainNme" HeaderText="Domain Name" SortExpression="DC1" />
        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />        
        <asp:BoundField DataField="Created_by" HeaderText="Created by" SortExpression="Created_by" />
        <asp:BoundField DataField="Created_Date" HeaderText="Created Date Time" SortExpression="Created_Date" />
        <asp:BoundField DataField="Updated_by" HeaderText="Updated by" SortExpression="Updated_by" />
        <asp:BoundField DataField="UpdatedDate" HeaderText="Updated Date Time" 
            SortExpression="UpdatedDate" />        
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

