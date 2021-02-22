<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Cartridgemaster.aspx.cs" Inherits="Cartridgemaster" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="script" runat="server" ></asp:ScriptManager>  
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
<asp:UpdatePanel ID="updwebsetting" runat="server">
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label5" Text="Cartridge Master " runat="server" CssClass="labelhead" ></asp:Label>
</td>
</tr>

</table>
</td>
</tr>
<tr style="padding-top:10px">
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">

<tr>
<td align="left" width="130px">
<asp:Label ID="lblphargrp" runat="server" Text="Pharmacy Location" 
        CssClass="labelall" Width="120px"></asp:Label>
</td>
<td align="left">
<asp:DropDownList ID="ddlphargrp" runat="server" Width="280px" 
        onselectedindexchanged="ddlphargrp_SelectedIndexChanged" 
        AutoPostBack="True" CssClass="textbox" ></asp:DropDownList>
</td>
</tr>
<asp:Panel ID="hsph" runat ="server">
<tr>
<td align="left" width="130px">
<asp:Label ID="Label6" runat="server" CssClass="labelall" Text="Cartridge No From "></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtfromcartno" ErrorMessage="*" ValidationGroup="cartmaster"></asp:RequiredFieldValidator>        
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox  ID="txtfromcartno" runat="server" Width="120px" MaxLength="6" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td align="left" style="padding-left:3px">
<asp:Label ID="Label8" runat="server" CssClass="labelall" Text="To"></asp:Label>
</td>
<td align="left" style="padding-left:3px">
<asp:TextBox ID="txttocartno" runat="server" Width="127px" MaxLength="6" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
</table>   
</td>
</tr>
</asp:Panel>
<tr>
<td align="left" width="130px">
<asp:Label ID="lblcartno" runat="server" CssClass="labelall" Text="Cartridge No " 
        ></asp:Label>
</td>
<td align="left">
<asp:TextBox  ID="txtcartnoedit" runat="server" Width="120px" MaxLength="6" 
        ReadOnly="True" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
  <%--  <tr>
        <td align="left" width="130px">
            <asp:Label ID="Label7" runat="server" CssClass="labelall" Text="Cartridge Type"></asp:Label>
            <asp:RequiredFieldValidator ID="req2" runat="server" 
                ControlToValidate="ddlcarttype" ErrorMessage="*" 
                InitialValue="--Select--" ValidationGroup="cartmaster"></asp:RequiredFieldValidator>
        </td>
        <td align="left">
            <asp:DropDownList ID="ddlcarttype" runat="server" 
                onselectedindexchanged="ddlcarttype_SelectedIndexChanged" Width="127px" CssClass="textbox" >
                <asp:ListItem>--Select--</asp:ListItem>                
            </asp:DropDownList>
        </td>
    </tr> --%>  
    <tr>
        <td align="left" width="130px">
            <asp:Label ID="lblremarla" runat="server" CssClass="labelall" 
                Text="Cartridge Description"></asp:Label>
        </td>
        <td align="left">
            <asp:TextBox ID="txtdescription" runat="server" Height="43px" MaxLength="100" 
                TextMode="MultiLine" Width="275px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2">
           <%-- <asp:Button ID="btnadd" runat="server" Font-Bold="False" onclick="btnadd_Click" 
                Text="Clear" CssClass="btn" UseSubmitBehavior="False" />--%>
              <asp:ImageButton ID="btnadd" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnadd_Click" Height="20px"/>

 <%--           <asp:Button ID="btnsearch" runat="server" onclick="btnsearch_Click" 
                    Text="Search" CssClass="btn" UseSubmitBehavior="False" />--%>
                           <asp:ImageButton ID="btnsearch" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Search.png" onclick="btnsearch_Click" Height="20px"/>
            
        <%--    <asp:Button ID="btnsave" runat="server" Font-Bold="False" CssClass="btn" 
                onclick="btnsave_Click" Text="Save" ValidationGroup="cartmaster" 
                UseSubmitBehavior="False" />--%>

            <asp:ImageButton ID="btnsave" runat="server" CssClass="btn" ValidationGroup="cartmaster"
            ImageUrl="~/ButtonImages/Save.png" onclick="btnsave_Click" Height="20px"/>

            <asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" ValidationGroup="cartmaster"
            ImageUrl="~/ButtonImages/Update.png" Height="20px" onclick="btnupdate_Click"/>
        </td>
    </tr>
    </table>
    </td>
</tr>
    
<tr>
<td align="left" style="padding-top:10px">
  <table border="0" cellpadding="0" cellspacing="0" >     
    <tr>          
    <td align="left">
    <table cellpadding="0" cellspacing="0" border="0">
    <tr>          
    <td>
    <%--    <asp:Button ID="btnactive" runat="server" onclick="btnactive_Click" 
            Text="Activate " Width="65px" CssClass="btn" UseSubmitBehavior="False" />--%>
  <asp:ImageButton ID="btnactive" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Activate.png" onclick="btnactive_Click" Height="20px"/>
    </td>
     <td>
<%--        <asp:Button ID="btndeactive" runat="server" onclick="btndeactive_Click" 
            Text=" Inactivate  " Width="65px" CssClass="btn" 
             UseSubmitBehavior="False" />--%>
  <asp:ImageButton ID="btndeactive" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Inactivate.png" onclick="btndeactive_Click" Height="20px"/>
             </td>
            <td>                 
        <asp:Button ID="btnok" runat="server" CausesValidation="False" 
            OnClick="btnok_Click" Style="position: static; display: none" Text="Ok" />
    </td>
    </tr>
    </table>                
    </td>           
    </tr>
    </table>
</td>
</tr>
<tr>
<td align="left">
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td>
    <asp:GridView ID="griddel" runat="server" AutoGenerateColumns="False" 
    BackColor="#FFFFCC" CaptionAlign="Left" CellPadding="0" 
    EnableModelValidation="True"
    ForeColor="#336600" HorizontalAlign="Left" 
    onselectedindexchanging="griddel_SelectedIndexChanging" 
    RowStyle-HorizontalAlign="Left" RowStyle-VerticalAlign="Middle" 
    Width="100%" AllowPaging="True" 
    onpageindexchanging="griddel_PageIndexChanging" CssClass="gridcss" 
            onsorting="griddel_Sorting" AllowSorting="True">
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
    <asp:CommandField SelectText="Edit" ShowSelectButton="True" >
    <ItemStyle HorizontalAlign="Center" />
    </asp:CommandField>
    <asp:BoundField DataField="location_Name" HeaderText=" Pharmacy Location" SortExpression="location_Name" />
    <asp:BoundField DataField="Cartridge_Id" HeaderText="Cartridge No" SortExpression="Cartridge_Id">
    <HeaderStyle Width="75px" />
    </asp:BoundField>
<%--    <asp:BoundField DataField="Cartridge_Type" HeaderText="Cartridge Type" SortExpression="Cartridge_Type">
    <HeaderStyle Width="50px" />
    </asp:BoundField>--%>
    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
    <asp:BoundField DataField="Created_by" HeaderText="Created by" SortExpression="Created_by" />
    <asp:BoundField DataField="Created_Date" HeaderText="Created Date Time" SortExpression="Created_Date" />
    <asp:BoundField DataField="Updated_by" HeaderText="Updated  by" SortExpression="Updated_by" />
    <asp:BoundField DataField="Updated_Date" HeaderText="Updated Date Time" SortExpression="Updated_Date" />
    </Columns>
    <FooterStyle BackColor="#507CD1" Font-Bold="False" ForeColor="#FF8000" />
    <PagerStyle BackColor="#169116" ForeColor="White" HorizontalAlign="Right" />
    <HeaderStyle BackColor="#169116" Font-Bold="True"  
    ForeColor="White" HorizontalAlign="Center" Wrap="True" />
    <EditRowStyle BackColor="#2461BF"  ForeColor="#CC3300" />
    </asp:GridView>
    </td>
    </tr>
    <tr>
    <td>
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

