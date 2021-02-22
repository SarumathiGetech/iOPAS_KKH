<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="securityadmin.aspx.cs" Inherits="securityadmin" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="script" runat="server" ></asp:ScriptManager>
<asp:UpdatePanel ID="updwebsetting" runat="server">
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="head" Text="Security" runat="server" CssClass="labelhead"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
<tr style="padding-top:10px">
<td align="left">
<table cellpadding="0" cellspacing="0" border="0" style="text-align:left;">

<tr>
<td align="left">
<asp:Label ID="lblsestime" runat="server" CssClass="labelall" Text="Session Time Out (Minutes)"></asp:Label>
    <asp:RequiredFieldValidator ID="valsestime" runat="server" ErrorMessage="*" 
        ValidationGroup="Security" ControlToValidate="txtsession" 
        SetFocusOnError="True"></asp:RequiredFieldValidator>    
</td>
<td>
<asp:TextBox  ID="txtsession" runat="server" Width="100px" MaxLength="3" 
        CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td>
<asp:RangeValidator ID="RangeValidator1" runat="server" 
        ControlToValidate="txtsession" 
        ErrorMessage="Positive Number Only Allowed" MaximumValue="999" MinimumValue="1" 
        SetFocusOnError="True" ValidationGroup="Security" Type="Integer"></asp:RangeValidator>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblinvalid" runat="server" Text="Number of Password Retries" 
        CssClass="labelall"></asp:Label>
<asp:RequiredFieldValidator ID="valretries" runat="server" ErrorMessage="*" 
        ValidationGroup="Security" ControlToValidate="txtretries" 
        SetFocusOnError="True"></asp:RequiredFieldValidator>
</td>
<td align="left">
<asp:TextBox  ID="txtretries" runat="server" Width="100px" MaxLength="2" 
        CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td>
<asp:RangeValidator ID="RangeValidator2" runat="server" 
        ControlToValidate="txtretries" 
        ErrorMessage="Positive Number Only Allowed" MaximumValue="99" MinimumValue="1" 
        SetFocusOnError="True" ValidationGroup="Security" Type="Integer"></asp:RangeValidator>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblpassreuse" runat="server" Text="No of times Password cannot be reused" 
        CssClass="labelall"></asp:Label>
<asp:RequiredFieldValidator ID="valreuse" runat="server" ErrorMessage="*" 
        ValidationGroup="Security" ControlToValidate="txtpassreuse" 
        SetFocusOnError="True"></asp:RequiredFieldValidator>        
</td>
<td align="left">
<asp:TextBox  ID="txtpassreuse" runat="server" Width="100px" MaxLength="2" 
        CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td>
<asp:RangeValidator ID="RangeValidator3" runat="server" 
        ControlToValidate="txtpassreuse" 
        ErrorMessage="Positive Number Only Allowed" MaximumValue="99" MinimumValue="1" 
        SetFocusOnError="True" ValidationGroup="Security" Type="Integer"></asp:RangeValidator>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblpassinterval" runat="server" Text="Password Expiry Period (Days)" 
        CssClass="labelall"></asp:Label>
<asp:RequiredFieldValidator ID="valinterval" runat="server" ErrorMessage="*" 
        ValidationGroup="Security" ControlToValidate="txtpassexpiry" 
        SetFocusOnError="True"></asp:RequiredFieldValidator>
</td>
<td align="left">
<asp:TextBox  ID="txtpassexpiry" runat="server" Width="100px" MaxLength="3" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td>
<asp:RangeValidator ID="RangeValidator4" runat="server" 
        ControlToValidate="txtpassexpiry" 
        ErrorMessage="Positive Number Only Allowed" MaximumValue="999" MinimumValue="1" 
        SetFocusOnError="True" ValidationGroup="Security" Type="Integer"></asp:RangeValidator>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblpromt" runat="server" 
        Text="Days to Prompt prior to Password Expiry" CssClass="labelall"></asp:Label>
<asp:RequiredFieldValidator ID="valpromt" runat="server" ErrorMessage="*" 
        ValidationGroup="Security" ControlToValidate="txtpromt" 
        SetFocusOnError="True"></asp:RequiredFieldValidator>        
</td>
<td align="left">
<asp:TextBox ID="txtpromt" runat="server" Width="100px" MaxLength="2" 
        CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td>
<asp:RangeValidator ID="RangeValidator5" runat="server" 
        ControlToValidate="txtpromt" 
        ErrorMessage="Positive Number Only Allowed" MaximumValue="99" MinimumValue="1" 
        SetFocusOnError="True" ValidationGroup="Security" Type="Integer"></asp:RangeValidator>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="Label1" runat="server" 
        Text="Inactivate User Account if Never Login for (Days)" CssClass="labelall"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" 
        ValidationGroup="Security" ControlToValidate="txtinactivate" 
        SetFocusOnError="True"></asp:RequiredFieldValidator>        
</td>
<td align="left">
<asp:TextBox ID="txtinactivate" runat="server" Width="100px" MaxLength="2" 
        CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td>
<asp:RangeValidator ID="RangeValidator6" runat="server" 
        ControlToValidate="txtinactivate" 
        ErrorMessage="Positive Number Only Allowed" MaximumValue="99" MinimumValue="1" 
        SetFocusOnError="True" ValidationGroup="Security" Type="Integer"></asp:RangeValidator>
</td>
</tr>
<tr>
<td colspan="2" align="right">
<%--<asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn" 
        ValidationGroup="Security" onclick="btnwebset_Click" />--%>

  <asp:ImageButton ID="btnsave" runat="server" CssClass="btn" ValidationGroup="Security" 
                ImageUrl="~/ButtonImages/Save.png" onclick="btnsave_Click" Height="20px"/>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td>
<table width="100%" cellpadding="0" cellspacing="0" border="0" style="text-align:left">
<tr>
<td align="left">
<asp:GridView ID="gridsecurity" runat="server" AutoGenerateColumns="False" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left" RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" onselectedindexchanging="gridsecurity_SelectedIndexChanging" 
        EnableModelValidation="True" CssClass="gridcss" >
        <RowStyle BackColor="#EFF3FB" Wrap="True" />       
    
    <Columns>
        <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
        <asp:BoundField DataField="Session_Time" HeaderText="Session Time" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="NoOfRetries" HeaderText="No of Retries" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Password_Reuse" HeaderText="Password Reuse" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Password_ExpiryDays" HeaderText="Password Expiry" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Password_Promtdays" HeaderText="Promt" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Inactive_User" HeaderText="Inactive User" >       
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Created_by" HeaderText="Created by" />
        <asp:BoundField DataField="Created_Date" HeaderText="Created Date Time" />
        <asp:BoundField DataField="Updated_by" HeaderText="Updated by" />
        <asp:BoundField DataField="Updated_Date" HeaderText="Updated Date Time" />
    </Columns>
     <FooterStyle BackColor="#507CD1" Font-Bold="False" ForeColor="#FF8000" />
    <PagerStyle BackColor="#169116" ForeColor="White" HorizontalAlign="Center" />    
    <HeaderStyle BackColor="#169116" Font-Bold="True" ForeColor="White" 
         Wrap="True" HorizontalAlign="Center" />
    <EditRowStyle BackColor="#2461BF"  ForeColor="#CC3300" />    
    </asp:GridView>  
</td>
</tr>
</table>
</td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
