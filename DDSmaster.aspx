<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DDSmaster.aspx.cs" Inherits="DDSmaster" %>
<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">    
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <asp:ScriptManager ID="scriptmana" runat="server"></asp:ScriptManager>   
    
    <script language="javascript" type="text/javascript">
        function Cellpopup() {

            var DDS = document.getElementById('<%=txtddsno.ClientID%>').value;
            window.open(("Cell.aspx?DDSno=" + DDS), 'search', 'menubar=no,center:yes,toolbar=no,scrollbars=no,width=450,height=400[color=blue]40,top=100,left=200')
            window.close();
        }
    
    
        function confirmProcess() {                       
                 if (confirm('Selected records will be activated'))            
             {
                 document.getElementById('<%=btnok.ClientID%>').click();
             }      
           
        }
        function confirmProcess1() {                       
                 if (confirm('Selected records will be inactivated'))            
             {
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
    
<asp:UpdatePanel ID="upd1" runat="server" >
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
 <tr>
   <td>
   <table align="center" cellpadding="0" cellspacing="0" border="0">
   <tr>
<td>
<asp:Label id="Label1" runat="server" Text="DDS / BDS Master" CssClass="labelhead"></asp:Label> 
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
<asp:Label ID="lblpharloc" runat="server" Text="Pharmacy Location" 
        CssClass="labelall"></asp:Label>
</td>
<td align="left">
<asp:DropDownList ID="ddlpharloc" runat="server" Width="227px" AutoPostBack="True" 
        onselectedindexchanged="ddlpharloc_SelectedIndexChanged" CssClass="textbox" ></asp:DropDownList><br />
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblmcno" runat="server" Text="DDS / BDS Name" CssClass="labelall" ></asp:Label>
    <asp:RequiredFieldValidator ID="valdds" runat="server" 
        ControlToValidate="txtddsno" ErrorMessage="*" ValidationGroup="DDS"></asp:RequiredFieldValidator>
</td>
<td align="left">
<asp:TextBox ID="txtddsno" runat="server" Width="222px" MaxLength="20" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
   
<%--<tr>
 <td align="left">
 <asp:Label ID="Label1" runat="server" CssClass="labelall" Text="Cell No From"></asp:Label>
     <asp:RangeValidator ID="RangeValidator1" runat="server" 
         ControlToValidate="txtcellfrom" ErrorMessage="*" MaximumValue="99" 
         MinimumValue="1" SetFocusOnError="True" Type="Integer" ValidationGroup="DDS"></asp:RangeValidator>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
         ControlToValidate="txtcellfrom" ErrorMessage="*" SetFocusOnError="True" 
         ValidationGroup="DDS"></asp:RequiredFieldValidator>
 </td>  
 <td align="left">
 <table cellpadding="0" cellspacing="0" border="0">
 <tr>
 <td align="left">
 <asp:TextBox ID="txtcellfrom" runat="server" Width="85px" ></asp:TextBox>
 </td>
 <td align="left" style="padding-left:5px">
 <asp:Label ID="Label3" runat="server" CssClass="labelall" Text="To"></asp:Label>
 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
     ErrorMessage="*" ControlToValidate="txtcellto" SetFocusOnError="True" 
     ValidationGroup="DDS"></asp:RequiredFieldValidator>
 <asp:RangeValidator ID="RangeValidator2" runat="server" 
   ControlToValidate="txtcellto" ErrorMessage="*" MaximumValue="99" 
   MinimumValue="1" SetFocusOnError="True" Type="Integer" ValidationGroup="DDS"></asp:RangeValidator>
 </td>
 <td align="left" style="padding-left:2px">
 <asp:TextBox ID="txtcellto" runat="server" Width="85px" ></asp:TextBox>   
 </td>
 </tr> 
 </table> 
 </td>
 </tr> --%>
 <tr>
 <td align="left">
 <asp:Label ID="lbldesc" runat="server" CssClass="labelall" Text="Description"></asp:Label>
 </td>
 <td align="left">
 <asp:TextBox ID="txtdescription" runat="server" Height="54px" 
          TextMode="MultiLine" Width="222px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
 </td>
 </tr> 
<tr>
<td align="left">
<asp:Label ID="lblcartauto" runat="server" Text="Auto Enabling of Cartridge" 
        CssClass="labelall"></asp:Label>
</td>
<td align="left">
<asp:CheckBox ID="chkauto" runat="server" Text="" />
</td>
</tr>        
<tr>
<td align="right" colspan="2">

<%--<asp:Button ID="btnadd" runat="server" CssClass="btn" Text="Clear" 
    onclick="btnadd_Click1"/>--%>
      <asp:ImageButton ID="btnadd" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnadd_Click1" Height="20px"/>
    &nbsp;
<%--<asp:Button ID="btnsubmit" runat="server" CssClass="btn" Text="Save" 
    ValidationGroup="DDS" onclick="btnsubmit_Click" />  --%>
      <asp:ImageButton ID="btnsubmit" runat="server" CssClass="btn" ValidationGroup="DDS"
            ImageUrl="~/ButtonImages/Save.png" onclick="btnsubmit_Click" 
        Height="20px"/> 
<asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" ValidationGroup="DDS"
            ImageUrl="~/ButtonImages/Update.png" Height="20px" 
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
<%--<asp:Button ID="btnactive" runat="server" Text="Activate " 
        onclick="btnactive_Click" Width="60px" />--%>
 
  <asp:ImageButton ID="btnactive" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Activate.png" onclick="btnactive_Click" Height="20px"/> 
        &nbsp;
<%--<asp:Button ID="btndeactive" runat="server" Text="Inactivate" 
        onclick="btndeactive_Click" Width="60px" />--%>
 <asp:ImageButton ID="btndeactive" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Inactivate.png" onclick="btndeactive_Click" Height="20px"/> 

 <asp:Button ID="btnok" runat="server" CausesValidation="False" OnClick="btnok_Click" Style="position: static; display: none" Text="Ok" />   
        
</td>
</tr>
<tr>
<td style="padding-top:10px;">
<asp:GridView ID="DDSGrid" runat="server" AutoGenerateColumns="False" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left" RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
         onselectedindexchanging="DDSGrid_SelectedIndexChanging" 
        AllowPaging="True" onpageindexchanging="DDSGrid_PageIndexChanging" 
        CssClass="gridcss" onsorting="DDSGrid_Sorting" AllowSorting="True" >  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />   
    <Columns>
    <asp:TemplateField>
    <HeaderTemplate>
     <input id="chkAll" onclick="SelectAllCheckboxes(this);" 
              runat="server" type="checkbox" />
    </HeaderTemplate>
    <ItemTemplate>
    <asp:CheckBox ID="chkrow" runat="server" />
    </ItemTemplate>    
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
        <asp:CommandField SelectText="Edit" ShowSelectButton="True"/>
        <asp:BoundField HeaderText="DDS / BDS Name" DataField="DDS_Name" SortExpression="DDS_Name" />
        <asp:BoundField HeaderText="Pharmacy Location" DataField="Location_Name" SortExpression="Location_Name" >
            <ItemStyle Wrap="True" />
        </asp:BoundField>
        <asp:BoundField DataField="Autoactivation" HeaderText="Auto Enabling" SortExpression="Autoactivation" />
        <asp:BoundField HeaderText="Status" DataField="Status" SortExpression="Status" />        
        <asp:BoundField DataField="Created_by" HeaderText="Created by" SortExpression="Created_by" />
        <asp:BoundField DataField="Created_Date" HeaderText="Created Date Time" SortExpression="Created_Date" />
        <asp:BoundField DataField="Updated_by" HeaderText="Updated by" SortExpression="Updated_by"/>
        <asp:BoundField DataField="Updated_Date" HeaderText="Updated Date Time" SortExpression="Updated_Date" />
    </Columns>
   <FooterStyle BackColor="#507CD1" Font-Bold="False" ForeColor="#FF8000" />
    <PagerStyle BackColor="#169116" ForeColor="White" HorizontalAlign="Right" />    
    <HeaderStyle BackColor="#169116" Font-Bold="True" ForeColor="White" 
         Wrap="True" HorizontalAlign="Center" />
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

