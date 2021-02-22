<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Workstation.aspx.cs" Inherits="Workstation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="scrip1" runat="server"></asp:ScriptManager>
     <%--<script language="javascript" src="JS/checkbox.js" type="text/javascript">    
    </script>--%>
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
<asp:UpdatePanel ID="upd1" runat="server">
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label1" Text="Workstation" runat="server" CssClass="labelhead" ></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
<tr align="left" style="padding-top:10px">
<td>
<table cellpadding="0" cellspacing="0" border="0">

<tr >
<td align="left" >
<asp:Label ID="lblpharloc" runat="server" Text="Pharmacy Location" 
        CssClass="labelall" Width="111px"></asp:Label>
</td>
<td align="left">
<asp:DropDownList ID="ddlpharloc" runat="server" Width="300px" AutoPostBack="True" 
        onselectedindexchanged="ddlpharloc_SelectedIndexChanged" CssClass="textbox"></asp:DropDownList>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblhost" runat="server" Text="Work Station" CssClass="labelall"></asp:Label>
    <asp:RequiredFieldValidator ID="valhostname" runat="server" 
        ControlToValidate="txthost" ErrorMessage="*" ValidationGroup="Hostname"></asp:RequiredFieldValidator>
</td>
<td>
<asp:TextBox ID="txthost" runat="server" Width="295px" Font-Italic="False" 
        Font-Size="10pt" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="Label2" runat="server" Text="Description" CssClass="labelall"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtdescription" runat="server" Width="295px" TextMode="MultiLine" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
<tr>
<td>
<br />
</td>
</tr>
<tr>
<td colspan="2" align="right">
<%--        <asp:Button ID="btnadd" runat="server" Text="Clear" onclick="btnadd_Click1" 
            Width="50px" CssClass="btn" />--%>
              <asp:ImageButton ID="btnadd" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnadd_Click" Height="20px"/>
            &nbsp;
        <%--<asp:Button ID="Btnsearch" runat="server" Text="Search"  
            Width="50px" CssClass="btn" onclick="Btnsearch_Click" />--%>
            <asp:ImageButton ID="Btnsearch" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Search.png" onclick="Btnsearch_Click" Height="20px"/>
            &nbsp;
      <%-- <asp:Button ID="btnsave" runat="server" Text="Save" onclick="btnsave_Click" 
        ValidationGroup="Hostname" Width="50px" CssClass="btn" /> --%>
    <asp:ImageButton ID="btnsave" runat="server" CssClass="btn" ValidationGroup="Hostname"
            ImageUrl="~/ButtonImages/Save.png" onclick="btnsave_Click" 
        Height="20px"/>

        <asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" ValidationGroup="Hostname"
            ImageUrl="~/ButtonImages/Update.png" Height="20px" 
        onclick="btnupdate_Click"/>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0" width="100%">
<tr>
<td align="left">
<%--<asp:Button ID="btnactive" runat="server" Text="Activate" 
        onclick="btnactive_Click1" CssClass="btn" />--%>
  <asp:ImageButton ID="btnactive" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Activate.png" onclick="btnactive_Click1" Height="20px"/>        
        &nbsp;
<%--<asp:Button ID="btndeactive" runat="server" Text="Inactivate" 
        onclick="btndeactive_Click" Width="75px" CssClass="btn" />--%>
  <asp:ImageButton ID="btndeactive" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Inactivate.png" onclick="btndeactive_Click" Height="20px"/>  
<asp:Button ID="btnok" runat="server" CausesValidation="False" OnClick="btnok_Click" Style="position: static; display: none" Text="Ok" />   
</td>
</tr>
<tr>
<td>
<asp:GridView ID="hostgrid" runat="server" AutoGenerateColumns="False" DataKeyNames="hostid"        
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left" RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True"
        onselectedindexchanging="hostgrid_SelectedIndexChanging" 
        AllowPaging="True" onpageindexchanging="hostgrid_PageIndexChanging" 
        CssClass="gridcss" onsorting="hostgrid_Sorting" AllowSorting="True">
        <RowStyle BackColor="#EFF3FB" Wrap="True" /> 
    <Columns>        
        <asp:TemplateField> 
        <HeaderTemplate>
       <input id="chkAll" onclick="SelectAllCheckboxes(this);" 
              runat="server" type="checkbox" />
        </HeaderTemplate>    
        <ItemTemplate>
        <asp:CheckBox ID="chkaccess" runat="server" />
        </ItemTemplate>  
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>       
        <asp:CommandField SelectText="Edit" ShowSelectButton="True" />         
       <%-- <asp:BoundField DataField="hostid">
            <ItemStyle ForeColor="White" />
        </asp:BoundField>--%>     
        <asp:BoundField DataField="hostid" HeaderText="hostid" Visible="false" />      
     <%--   <asp:BoundField DataField="hostid" HeaderText="hostid" />--%>
        <asp:BoundField DataField="Host_Name" HeaderText="Workstation" SortExpression="Host_Name" />        
        <asp:BoundField DataField="Location_Name" HeaderText="Pharmacy Location" SortExpression="Location_Name" />
        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />       
        <asp:BoundField DataField="Created_by" HeaderText="Created by" SortExpression="Created_by" />
        <asp:BoundField DataField="Created_Date" HeaderText="Created Date Time" SortExpression="Created_Date" />
        <asp:BoundField DataField="Updated_by" HeaderText="Updated by" SortExpression="Updated_by" />
        <asp:BoundField DataField="Updated_Date" HeaderText="Updated Date Time" SortExpression="Updated_Date" />        
    </Columns>
    <FooterStyle BackColor="#507CD1" Font-Bold="False" ForeColor="#FF8000" />
    <PagerStyle BackColor="#169116" ForeColor="White" HorizontalAlign="Right" />   
    <HeaderStyle BackColor="#169116" Font-Bold="True" ForeColor="White" Wrap="True" HorizontalAlign="Center" />
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
