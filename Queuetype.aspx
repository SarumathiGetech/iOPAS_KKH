<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Queuetype.aspx.cs" Inherits="Queuetype" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">    
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="script" runat="server"></asp:ScriptManager>
    <script language="javascript" type="text/javascript"> 
           function STot1() 
           {
               var m="";
               var s = document.getElementById('<%=txtfrom.ClientID%>').value;
               var s1 = document.getElementById('<%=txtto.ClientID%>').value;
                if ((s * 1) >= (s1 * 1)) {
                    alert('Queue To must be greater than Queue From');
                    document.getElementById('<%=txtto.ClientID%>').value = m;                                   
               }
            }
            function confirmProcess() 
            {
                if (confirm('Selected records will be activated')) {
                    document.getElementById('<%=btnok.ClientID%>').click();
                }            

            }
            function confirmProcess1() {
                if (confirm('Selected records will be inactivated')) {
                    document.getElementById('<%=btnok.ClientID%>').click();
                }
            }             
    </script>
<asp:UpdatePanel ID="updqueue" runat="server">
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label2" runat="server" Text="Queue Series" CssClass="labelhead"></asp:Label>
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
<asp:Label ID="lblpharmloca" runat="server" Text="Pharmacy Location" 
        CssClass="labelall"></asp:Label>
</td>
<td align="left">
<asp:DropDownList ID="ddlpharmloca" runat="server" Width="220px" 
        AutoPostBack="True" onselectedindexchanged="ddlpharmloca_SelectedIndexChanged" 
        style="height: 22px" CssClass="textbox"></asp:DropDownList>
</td>
<td align="left" rowspan="5">
<table cellpadding="0" cellspacing="0" border="0" style="margin-left:7px; margin-top:0px; width: 157px;">
<tr>
<td valign="top">
<div style="overflow:scroll; height:140px;">   
<asp:GridView ID="mcgrid" runat="server" AutoGenerateColumns="False" 
        Width="132px" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left"
        BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" CssClass="gridcss" > 
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns>                                
    <asp:BoundField DataField="DDS_Name" HeaderText="DDS / BDS Name" />
    <asp:TemplateField>
      <HeaderTemplate>
     <asp:CheckBox ID="chkheaderdds" runat="server" OnCheckedChanged ="chkheaderdds_CheckedChanged" AutoPostBack="true" />
     </HeaderTemplate>
     <ItemTemplate>
      <asp:CheckBox ID="chkrow" runat="server" Checked='<%# chkrow(Eval("DDS_Name"))%>'/>
     </ItemTemplate>
     <ItemStyle HorizontalAlign="Center" />
   </asp:TemplateField>                               
    </Columns>
         <FooterStyle BackColor="#507CD1" Font-Bold="False" ForeColor="#FF8000" />
    <PagerStyle BackColor="#169116" ForeColor="White" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="false" ForeColor="#336600" />
    <HeaderStyle BackColor="#169116" Font-Bold="True" ForeColor="White" 
         Wrap="True" HorizontalAlign="Center" />
    <EditRowStyle BackColor="#2461BF"  ForeColor="#CC3300" />
    <AlternatingRowStyle BackColor="White" />
</asp:GridView>    
  </div>               
</td>
</tr>
<tr>
<td style="margin-left: 40px">
<%--<asp:Button ID="Button4" runat="server" Text="Clear" Width="41px" 
        onclick="Button4_Click" CssClass="btn" />--%>
  
  <asp:ImageButton ID="Button4" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="Button4_Click" Height="20px"/>  
        &nbsp;
      <%--  <asp:Button ID="btnsave" runat="server" Text="Save" Font-Bold="false" 
        ValidationGroup="queue" onclick="btnsave_Click" CssClass="btn"  /> --%>  
        <asp:ImageButton ID="btnsave" runat="server" CssClass="btn" ValidationGroup="queue"
            ImageUrl="~/ButtonImages/Save.png" onclick="btnsave_Click" Height="20px"/>   
    <asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" ValidationGroup="queue"
            ImageUrl="~/ButtonImages/Update.png"  Height="20px" 
        onclick="btnupdate_Click"/>                    
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left" >
<asp:Label ID="lblqueue" runat="server" Width="114px" Text="Queue Series" 
        CssClass="labelall"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtquetype" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="queue"></asp:RequiredFieldValidator>
</td>
<td align="left">
<asp:TextBox ID="txtquetype" runat="server" Width="215px" CssClass="textbox" 
        AutoCompleteType="Disabled" MaxLength="50"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblfrom" runat="server" Text="From" CssClass="labelall"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtfrom" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="queue"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator1" runat="server" 
        ControlToValidate="txtfrom" ErrorMessage="*" MaximumValue="9999" 
        MinimumValue="1" SetFocusOnError="True" Type="Integer" 
        ValidationGroup="queue"></asp:RangeValidator>
</td>
<td align="left">
<asp:TextBox ID="txtfrom" runat="server" Width="215px" CssClass="textbox" 
        AutoCompleteType="Disabled" MaxLength="4" ></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblto" runat="server" Text="To" CssClass="labelall"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="txtto" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="queue"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator2" runat="server" 
        ControlToValidate="txtto" ErrorMessage="*" MaximumValue="9999" MinimumValue="1" 
        SetFocusOnError="True" Type="Integer" ValidationGroup="queue"></asp:RangeValidator>
</td>
<td align="left">
<asp:TextBox ID="txtto" runat="server" Width="215px" onblur ="void STot1()" 
        CssClass="textbox" AutoCompleteType="Disabled" MaxLength="4" ></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lbldesc" runat="server" Text="Description" CssClass="label"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtdesc" runat="server" TextMode="MultiLine" Width="215px" 
        Height="55px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
<tr>
<td colspan="2" align="center">
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
<%--<asp:Button ID="btninactive" runat="server"  Text="Inactivate" 
        onclick="btninactive_Click" Width="60px" CssClass="btn"/>--%>
          <asp:ImageButton ID="btninactive" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Inactivate.png" onclick="btninactive_Click" 
        Height="20px"/>
          <asp:ImageButton ID="btnactive" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Activate.png" onclick="btnactive_Click" 
        Height="20px" Visible="False"/>
<%--<asp:Button ID="btnactive" runat="server"  Text="Activate"  
        Width="60px" onclick="btnactive_Click" CssClass="btn" Visible="False"/>--%>&nbsp;

<asp:Button ID="btnok" runat="server" CausesValidation="False" OnClick="btnok_Click" Style="position: static; display: none" Text="Ok" />   
</td>
</tr>
<tr>
<td align="left">
<asp:GridView ID="Gridsecond" runat="server" AutoGenerateColumns="False" DataKeyNames="QueueID" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        onselectedindexchanging="Gridsecond_SelectedIndexChanging" 
        AllowPaging="True" onpageindexchanging="Gridsecond_PageIndexChanging" 
        CssClass="gridcss" onsorting="Gridsecond_Sorting" AllowSorting="True">  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />    
<Columns>
        <asp:TemplateField>
        <HeaderTemplate>
        <asp:CheckBox ID="chkheader" runat="server" OnCheckedChanged ="chkheader_CheckedChanged" AutoPostBack="true" />
        </HeaderTemplate>
        <ItemTemplate>
         <asp:CheckBox ID="chkque" runat="server" />
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
        <asp:BoundField DataField="QueueID" Visible="false"/>
       <%-- <ItemStyle ForeColor="White" Width="10px" Font-Size="XX-Small" />
        </asp:BoundField>--%>
        <asp:BoundField DataField="Location_Name" HeaderText="Pharmacy Location" SortExpression="Location_Name" />
        <asp:BoundField DataField="Queue_type" HeaderText="Queue Series" SortExpression="Queue_type" />
        <asp:BoundField DataField="Queue_From" HeaderText="Queue From" SortExpression="Queue_From" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Queue_To" HeaderText="Queue To" SortExpression="Queue_To" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="DDS_Name" HeaderText="DDS / BDS Name" SortExpression="DDS_Name" />
        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
        <asp:BoundField DataField="Created_by" HeaderText="Created by" SortExpression="Created_by" />
        <asp:BoundField DataField="Created_Date" HeaderText="Created Date Time" SortExpression="Created_Date" />
        <asp:BoundField DataField="Updated_by" HeaderText="Updated by" SortExpression="Updated_by" />
        <asp:BoundField DataField="Updated_Date" HeaderText="Updated Date Time" SortExpression="Updated_Date" />
    </Columns>
    <FooterStyle BackColor="#507CD1" Font-Bold="False" ForeColor="#FF8000" />
    <PagerStyle BackColor="#169116" ForeColor="White" HorizontalAlign="Right" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="false" ForeColor="#336600" />
    <HeaderStyle BackColor="#169116" Font-Bold="True" ForeColor="White" 
        Wrap="True" HorizontalAlign="Center" />
    <EditRowStyle BackColor="#2461BF"  ForeColor="#CC3300" />
    <AlternatingRowStyle BackColor="White" />
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