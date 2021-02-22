<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="Role.aspx.cs" Inherits="Role" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">      
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>   
 
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
<asp:UpdatePanel ID="updgroup" runat="server">
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label1" runat="server" Text="Role Administration" CssClass="labelhead"></asp:Label>
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
<asp:Label ID="lblgroup" runat="server" Text="Role " CssClass="labelall" ></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtroleadd" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="UserRole"></asp:RequiredFieldValidator>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:DropDownList ID="ddlgroupname" runat="server" Width="238px" 
        AutoPostBack="True" 
        onselectedindexchanged="ddlgroupname_SelectedIndexChanged" Height="22px" CssClass="textbox"></asp:DropDownList>

        <td>        
        <asp:TextBox ID="txtroleadd" runat="server" Width="232px" CssClass="textbox" AutoCompleteType="Disabled" ></asp:TextBox>
        </td>
  </td> 
  <td>

  <asp:ImageButton ID="btnadd" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Create.png" onclick="btnadd_Click" 
        Height="20px"/>
  </td>
</tr>
</table>

</td>

</tr>
<tr>
<td align="left">
<asp:Label ID="lbldesc" runat="server" Text="Job Description" CssClass="labelall" 
        Width="111px"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtdesc" runat="server" Width="232px" TextMode="MultiLine" 
        Height="40px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
<tr>
<td>
</td>
<td align="left" style="padding-left:150px">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<%--<td align="left">

  <asp:ImageButton ID="btnacces" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Save.png" onclick="btnacces_Click" Height="20px"/>
</td>--%>
<td align="left">
<%--<asp:Button ID="btnclear" runat="server" Text="Clear" Font-Bold="false" 
        onclick="btnclear_Click" CssClass="btn"/>--%>
  <asp:ImageButton ID="btnclear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnclear_Click" Height="20px"/>
</td>
<td>
<%--<asp:Button ID="btngroupadd" runat ="server" Text="Save" CssClass="btn" 
        onclick="btngroupadd_Click1" ValidationGroup="UserRole" Visible="False" />--%>     
  <asp:ImageButton ID="btngroupadd" runat="server" CssClass="btn" ValidationGroup="UserRole" 
            ImageUrl="~/ButtonImages/Save.png" onclick="btngroupadd_Click1" Height="20px"/>

  <asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" ValidationGroup="UserRole" 
            ImageUrl="~/ButtonImages/Update.png"  Height="20px" 
        onclick="btnupdate_Click"/>
</td>
<td align="right">        
<%--<asp:Button ID="btndel" runat="server" Text="Return To Previous Screen" Width="170px" 
        Font-Bold="False" onclick="btndel_Click" CssClass="btn"/>--%>
  <asp:ImageButton ID="btndel" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Return.png" onclick="btndel_Click" Height="20px"/>
</td>
</tr>
</table>   
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left" style="padding-top:10px;">
<asp:Panel ID="panelmenu" runat="server">
<table  cellpadding="0" cellspacing="0" border="0" >
<tr>
<td align="left" >
<table cellpadding="0" cellspacing="0" border="0" style="border: thin double #169116; height:18px;background-color: #009933;">
<tr>
<td align="left">
  <asp:ImageButton ID="btnloading" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Loading.png" onclick="btnloading_Click" Height="24px"/>
</td>

<td> 
  <asp:ImageButton ID="btnlblprinting" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Label.png" onclick="btnlblprinting_Click" 
        Height="24px"/>
</td>
<td> 
  <asp:ImageButton ID="btnenquery" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Enquiry.png" onclick="btnenquery_Click" 
        Height="24px"/>
</td>
<td> 
  <asp:ImageButton ID="btnmanual" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/BatchOrder.png" onclick="btnmanual_Click" 
        Height="24px"/>
</td>
<td> 
  <asp:ImageButton ID="btnsetting" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/General.png" onclick="btnsetting_Click" 
        Height="24px"/>
</td>
<td> 
  <asp:ImageButton ID="btnsystem" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/System.png" onclick="btnsystem_Click" 
        Height="24px"/>
</td>
<td> 
  <asp:ImageButton ID="btnsecurity" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Security.png" onclick="btnsecurity_Click" 
        Height="24px"/>
</td>
<td> 
  <asp:ImageButton ID="btnddsmon" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Monitoring.png" onclick="btnddsmon_Click" 
        Height="24px"/>
</td>
<td> 
  <asp:ImageButton ID="btnreport" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Reports.png" onclick="btnreport_Click" 
        Height="24px"/>
</td>    
           
</tr>
</table>
</td>
</tr>
<tr>
<td align="left" >
<table  cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left" >
<asp:GridView ID="gridgroup" runat="server" AutoGenerateColumns="False"  DataKeyNames="MenuID"
        Width="201px" CellPadding="1" CellSpacing="1" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left" RowStyle-HorizontalAlign="Left" RowStyle-VerticalAlign="Middle"  
        EnableModelValidation="True" CssClass="gridcss"> 
         <RowStyle BackColor="#EFF3FB" Wrap="True" />   
    <Columns>
        <asp:BoundField DataField="MenuID" Visible="false"/>
          <%--  <HeaderStyle Width="0px" Wrap="False" />
            <ItemStyle Font-Size="XX-Small" ForeColor="#EFF3FB" />
        </asp:BoundField>--%>
        <asp:BoundField HeaderText="Menu Name" DataField="Child_Menu">              
            <HeaderStyle Width="200px" />
        </asp:BoundField>
        <asp:TemplateField>
        <HeaderTemplate>
        <input id="chkAll" onclick="SelectAllCheckboxes(this);" 
              runat="server" type="checkbox" />
        </HeaderTemplate>
        <ItemTemplate>
        <asp:CheckBox ID="chk" runat="server" Checked='<%# chk(Eval("MenuID")) %>' />      
        </ItemTemplate>        
        </asp:TemplateField>
    </Columns>
    <FooterStyle BackColor="#507CD1" Font-Bold="False" ForeColor="#FF8000" />
    <PagerStyle BackColor="#169116" ForeColor="White" HorizontalAlign="Center" />    
    <HeaderStyle BackColor="#169116" Font-Bold="True" ForeColor="White" 
         Wrap="True" HorizontalAlign="Center" />
    <EditRowStyle BackColor="#2461BF"  ForeColor="#CC3300" />   
    <SelectedRowStyle BackColor="#EFF3FB" />
</asp:GridView>    
</td>
<td align="left" style="vertical-align:bottom; padding-left:5px">
  <asp:ImageButton ID="btnacces" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Save.png" onclick="btnacces_Click" Height="25px"/>
</td>
</tr>
</table>
<%--<table width="100%" cellpadding="0" cellspacing="0" border="0" >
<tr>
<td align="left" style="padding-top:30px">
<asp:GridView ID="fstgrid" runat="server" AutoGenerateColumns="False" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left" Font-Italic="False" 
        Font-Overline="False" Font-Size="Medium" Font-Strikeout="False" 
        Font-Underline="False" RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" onselectedindexchanging="gridgrouping_SelectedIndexChanging" 
        EnableModelValidation="True" AllowPaging="True" 
        onpageindexchanging="fstgrid_PageIndexChanging"> 
        <RowStyle BackColor="#EFF3FB" Wrap="True" />   
    <Columns>       
        <asp:BoundField DataField="Role_Name" HeaderText="Role" />
        <asp:BoundField DataField="Status" HeaderText="Status" />
        <asp:BoundField DataField="Created_by" HeaderText="Created by" />
        <asp:BoundField DataField="Created_Date" HeaderText="Cretaed Date Time" />
        <asp:BoundField DataField="Updated_by" HeaderText="Updated by" />
        <asp:BoundField DataField="Updated_Date" HeaderText="Updated Date Time" />
   </Columns>
    <FooterStyle BackColor="#507CD1" Font-Bold="False" ForeColor="#FF8000" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Right" />    
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
        Font-Size="Small" Wrap="True" HorizontalAlign="Center" />
    <EditRowStyle BackColor="#2461BF" Font-Size="Small" ForeColor="#CC3300" />    
</asp:GridView>  
</td>
</tr>
<tr>
<td style="padding-left:250px">
<asp:Label ID="lblpge2" runat="server" CssClass="labelall"></asp:Label>
</td>
</tr>
</table>--%>
</table>
</asp:Panel>
</td>
</tr>
<tr>
<td align="left">
<asp:Panel ID="gridpanel" runat="server">
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<%--<asp:Button ID="btndelgrp" runat="server" Text="Activate" CssClass="btn" 
        onclick="btndelgrp_Click"/>--%>
  <asp:ImageButton ID="btndelgrp" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Activate.png" onclick="btndelgrp_Click" Height="22px"/>
        &nbsp;       
<%--<asp:Button ID="Btndeactive" runat="server" Text="Inactivate" CssClass="btn"
        Width="72px" onclick="Btndeactive_Click"/> --%>
  <asp:ImageButton ID="Btndeactive" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Inactivate.png" onclick="Btndeactive_Click" Height="22px"/>
<asp:Button ID="btnok" runat="server" CausesValidation="False" OnClick="btnok_Click" Style="position: static; display: none" Text="Ok" />              
</td>
</tr>
<tr>
<td align="left">
<asp:GridView ID="gridgrouping" runat="server" AutoGenerateColumns="False" DataKeyNames="Roleid" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left" RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" onselectedindexchanging="gridgrouping_SelectedIndexChanging" 
        EnableModelValidation="True" AllowPaging="True" 
        onpageindexchanging="gridgrouping_PageIndexChanging" CssClass="gridcss" 
        onsorting="gridgrouping_Sorting" AllowSorting="True"> 
        <RowStyle BackColor="#EFF3FB" Wrap="True" />   
    <Columns>
        <asp:TemplateField HeaderText ="Access">
        <HeaderTemplate>
        <input id="chkAll" onclick="SelectAllCheckboxes(this);" 
              runat="server" type="checkbox" />
        </HeaderTemplate>
        <ItemTemplate>
        <asp:CheckBox ID="chkgrpinc" runat="server"  />
        </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
        <asp:BoundField DataField="Roleid" Visible="false" />            
            <%--<ItemStyle Font-Size="XX-Small" ForeColor="White" />
        </asp:BoundField> --%>      
        <asp:BoundField DataField="Role_Name" HeaderText="Role" SortExpression="Role_Name" />
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
</asp:Panel>
</td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
