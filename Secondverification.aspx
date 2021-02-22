<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Secondverification.aspx.cs" Inherits="Secondverification" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="manager" runat="server"></asp:ScriptManager>
<script language="javascript" type="text/javascript">
         function preload() {
             window.open('Secpopup.aspx', 'search', 'menubar=no,center:yes,toolbar=no,scrollbars=no,width=620,height=500[color=blue]40,top=0,left=0')
             window.close();
         }
</script>
<asp:UpdatePanel ID="upddrug" runat="server">
<ContentTemplate>
<asp:Panel ID="pp" runat="server">
<table width="100%" border="0" cellpadding="0" cellspacing="0">
<tr>
<td id="tdone" runat="server">
<table width="100%" border="0" cellpadding="0" cellspacing="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label1" runat="server" Text="Second Verification" CssClass="labelhead"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
<tr style="padding-top:10px">
<td  align="left">
<table align="left"  cellpadding="0px" cellspacing="0px" border="0" style=" padding-top:0px; ">

<tr>
<td align="left" width="107px">
<asp:Label ID="lblcartno" runat="server" Text="Cartridge No" CssClass="labelall" 
        Width="70px"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtcartno" ErrorMessage="*" 
        ValidationGroup="ent"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
        ControlToValidate="txtcartno" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="firstverifications"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
        ControlToValidate="txtcartno" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="reject"></asp:RequiredFieldValidator>
</td>
<td align="left">
<table cellpadding="0px" cellspacing="0px" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtcartno" runat="server" Width="240px" MaxLength="6" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td align="left">
<%--<asp:Button ID="Btngo" runat="server" Text="Enter" onclick="Btngo_Click" 
        ValidationGroup="ent" CssClass="btn" />--%>
  <asp:ImageButton ID="Btngo" runat="server" CssClass="btn" ValidationGroup="ent" 
            ImageUrl="~/ButtonImages/Enter24.png" onclick="Btngo_Click" Height="22px"  />
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="Llblitemcode" runat="server" Text="Item Code" CssClass="labelall" 
        Width="65px" Height="16px"></asp:Label>  
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtitemcode" ErrorMessage="*" 
        ValidationGroup="firstverifications"></asp:RequiredFieldValidator>        
</td>
<td align="left">
<table cellpadding="0px" cellspacing="0px" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtitemcode" runat="server" Width="240px" ReadOnly="True" 
        BackColor="#E8E8E8" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>   
</td>
<td align="left" style="padding-left:3px">
 <asp:Label ID="lbldrugcode" runat="server" CssClass="labelall" Text="Drug Code" 
  Width="65px"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtdrugcode" runat="server" Width="227px" ReadOnly="True" 
         BackColor="#E8E8E8" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td> 
</tr>
</table>     
</td> 
</tr>
<tr>
<td align="left" width="107px">
<asp:Label ID="lbldrg" runat="server" Text="Item Name" CssClass="labelall" 
        Width="65px"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
        ControlToValidate="txtitemname" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="reject"></asp:RequiredFieldValidator>
</td>
<td align="left">
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left">
<asp:TextBox ID="txtitemname" runat="server" Width="541px" ReadOnly="True" 
        BackColor="#E8E8E8" CssClass="textboxbigfont" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td align="left" style="padding-left:2px">
<input type="checkbox" id="chkitemname"  style="width:1.5em; height:1.5em;" runat="server" checked="checked" disabled="disabled" />
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblbran" runat="server" Text="Brand Name" CssClass="labelall" 
        Width="75px"></asp:Label>    
</td>
<td align="left">
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left">
<asp:TextBox ID="txtbrand" runat="server" Width="541px" ReadOnly="True" 
        BackColor="#E8E8E8" CssClass="textboxbigfont" AutoCompleteType="Disabled"></asp:TextBox>     
</td>
<td align="left" style="padding-left:2px">
<input type="checkbox" id="chkbrandname"  style="width:1.5em; height:1.5em;" runat="server" checked="checked" readonly="readonly" disabled="disabled"/>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblpacktype" runat="server" Text="Pack Type" CssClass="labelall" 
        Width="75px"></asp:Label>    
</td>
<td align="left">
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left" width="120px" >
<asp:TextBox ID="txtpacktype" runat="server" Width="120px" ReadOnly="True" 
        BackColor="#E8E8E8" CssClass="textboxbigfont" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td align="left" style="padding-left:2px">
<input type="checkbox" id="chkpacktype"  style="width:1.5em; height:1.5em;" runat="server" checked="checked" readonly="readonly" disabled="disabled"/>
</td>
<td align="left" width="60px" style="padding-left:10px;">
<asp:Label ID="lblpacksize" runat="server" Text="Pack Size" CssClass="labelall" 
        Width="65px"></asp:Label>          
</td>
<td align="left" width="100px">
 <asp:TextBox ID="txtpacksize" runat="server" Width="100px" ReadOnly="True" 
        BackColor="#E8E8E8" CssClass="textboxbigfont" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td align="left" style="padding-left:2px">
<input type="checkbox" id="chkpacksize"  style="width:1.5em; height:1.5em;" runat="server" checked="checked" readonly="readonly" disabled="disabled"/>
</td>
<td align="left" width="35px" style="padding-left:5px;">
<asp:Label ID="lbluom" runat="server" Text=" UOM" CssClass="labelall" 
        Width="48px" ></asp:Label>
</td>
<td align="left" width="120px">
<asp:TextBox ID="txtuom" runat="server" Width="130px" ReadOnly="True" 
        BackColor="#E8E8E8" CssClass="textboxbigfont" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblmaxcart" runat="server" Text="Maximum  Quantity Per Cartridge" 
        CssClass="labelall" Width="180px"></asp:Label>
</td>
<td align="left">
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left" width="120px">
<asp:TextBox ID="txtmaxcartqty" runat="server" Width="120px" ReadOnly="True" BackColor="#E8E8E8" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td>
</td>
<td align="left" style="padding-left:37px;">
<asp:Label ID="lblmax" runat="server" CssClass="labelall" Width="200px"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left" width="108px">
<asp:Label ID="lblqty" runat="server" Text="Quantity" CssClass="labelall" 
        Width="75px"></asp:Label>
</td>
<td align="left">
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left" width="120px">
<asp:TextBox ID="txtqty" runat="server" Width="120px" ReadOnly="True" BackColor="#E8E8E8" CssClass="textboxbigfont" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td align="left" style="padding-left:2px">
<input type="checkbox" id="chkqty"  style="width:1.5em; height:1.5em;" runat="server" checked="checked" readonly="readonly" disabled="disabled"/>
</td>
<td align="left" style="padding-left:10px;">
<asp:Label ID="lblload" runat="server" CssClass="labelall" Width="200px"></asp:Label>

</td>

</tr>
</table>      
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblexpdate" runat="server" Text="Expiry Date" CssClass="labelall" 
        Width="65px" ></asp:Label>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtexpdate" runat="server" Width="120px" ReadOnly="True" 
        BackColor="#E8E8E8" CssClass="textboxbigfont" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td align="left" style="padding-left:2px">
<input type="checkbox" id="chkexpdate"  style="width:1.5em; height:1.5em;" runat="server" checked="checked" readonly="readonly" disabled="disabled"/>
</td>
<td align="left" width="60px" style="padding-left:10px;">
<asp:Label ID="lblbatchno" runat="server" Text="Batch No" CssClass="labelall" 
        Width="50px" ></asp:Label> 
</td>
<td align="left" width="100px">
<asp:TextBox ID="txtbatchno" runat="server" Width="100px" ReadOnly="True" 
        BackColor="#E8E8E8" CssClass="textboxbigfont" AutoCompleteType="Disabled"></asp:TextBox>      
</td>
<td align="left" style="padding-left:2px">
<input type="checkbox" id="Checkbox1"  style="width:1.5em; height:1.5em;" runat="server" checked="checked" readonly="readonly" disabled="disabled"/>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lbllodedby" runat="server" Text="PreLoaded by" CssClass="labelall" 
        Width="80px"></asp:Label>
</td>
<td align="left">
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left">
<asp:TextBox ID="txtlodedby" runat="server" Width="320px" ReadOnly="false" 
        BackColor="#E8E8E8" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td align="left" width="65px" style="padding-left:5px">
<asp:Label ID="lbllodeddate" runat="server" Text="Date Time" CssClass="labelall" 
        Width="65px"></asp:Label>
</td>
<td align="left" width="40px">
<asp:TextBox ID="txtdate" runat="server" Width="140px" ReadOnly="True" 
        BackColor="#E8E8E8" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox> 
</td>
</tr>
</table>           
</td>
</tr>
<tr>
<td align="left" width="107px">
<asp:Label ID="lblfstveri" runat="server" CssClass="labelall" 
 Text="First Verified By" Width="90px"></asp:Label>
</td>
<td align="left">
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left" >
<asp:TextBox ID="txtfstby" runat="server" BackColor="#E8E8E8" ReadOnly="True" 
            Width="320px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td align="left" width="65px" style="padding-left:5px">
<asp:Label ID="lblfstdate" runat="server" CssClass="labelall" 
            Text="Date Time" Width="65px"></asp:Label>
            </td>
<td align="left" width="140px">
<asp:TextBox ID="txtfstdate" runat="server" BackColor="#E8E8E8" ReadOnly="True" 
            Width="140px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>      
</table>           
</td> 
</tr>
<tr>
<td align="left" width="115px">
<asp:Label ID="Lblreason" runat="server" Text="Reason For Reject" CssClass="labelall" ></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ErrorMessage="*" ControlToValidate="ddlreason" InitialValue="-Blank-" 
            SetFocusOnError="True" ValidationGroup="reject" Display="Dynamic"></asp:RequiredFieldValidator>
</td>
<td align="left">
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left">
<asp:DropDownList ID="ddlreason" runat="server" Width="324px" CssClass="textbox" 
        AutoPostBack="True" 
        onselectedindexchanged="ddlreason_SelectedIndexChanged">
<asp:ListItem>-Blank-</asp:ListItem>
</asp:DropDownList>
</td>
<td style="padding-left:60px">
<%--<asp:Button ID="Button2" runat="server" Height="23px" onclick="Button2_Click" 
   Text="Clear" CssClass="btn" />
<asp:Button ID="btnreject" runat="server" Height="23px" 
onclick="btnreject_Click" Text="Reject" ValidationGroup="reject" CssClass="btn" />

<asp:Button ID="btnaccept" runat="server" Height="23px" 
onclick="btnaccept_Click" Text="Accept" ValidationGroup="firstverifications" CssClass="btn" />  --%>    

  <asp:ImageButton ID="Button2" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="Button2_Click" Height="20px"  />

  <asp:ImageButton ID="btnreject" runat="server" CssClass="btn"  ValidationGroup="reject" 
            ImageUrl="~/ButtonImages/Reject.png" onclick="btnreject_Click" Height="20px"  />

  <asp:ImageButton ID="btnaccept" runat="server" CssClass="btn" ValidationGroup="firstverifications"
            ImageUrl="~/ButtonImages/Accept.png" onclick="btnaccept_Click" Height="20px"  />

      
</td>
</tr>        
</table> 
</td>
</tr>   
</table>
</td>
</tr>
</table>

</td>
</tr>

<tr>
<td id="tdtwo" runat="server" align="left" style="padding-top:10px">
<table align="left" width="100%" border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left">
<asp:UpdatePanel ID="updgrid" runat="server">
<ContentTemplate>
<asp:GridView ID="gridsecond" runat="server" AutoGenerateColumns="False" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        onselectedindexchanged="gridsecond_SelectedIndexChanged" 
        AllowPaging="True" onpageindexchanging="gridsecond_PageIndexChanging" 
        CssClass="gridcss" onsorting="gridsecond_Sorting" AllowSorting="True" 
        PageSize="20">  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
 <Columns>
        <asp:BoundField DataField="Cartridge_Id" HeaderText="Cartridge No" SortExpression="Cartridge_Id" />
        <asp:BoundField DataField="Item_Code" HeaderText="Item Code" SortExpression="Item_Code" />
        <asp:BoundField DataField="Item_Name" HeaderText="Item Name" SortExpression="Item_Name" />
        <asp:BoundField DataField="Brandname" HeaderText="Brand Name" SortExpression="Brandname" />
        <asp:BoundField DataField="PackType" HeaderText="Pack Type" SortExpression="PackType" />
        <asp:BoundField DataField="Pack_Size" HeaderText="Pack Size" SortExpression="Pack_Size" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="UOM" HeaderText="UOM" SortExpression="UOM" />
        <asp:BoundField DataField="Verified_Status" HeaderText="Status" SortExpression="Verified_Status" />
        <asp:BoundField DataField="reason" HeaderText="Rejected Reason" SortExpression="reason" />
        <asp:BoundField DataField="Loaded_by" HeaderText="PreLoaded by" SortExpression="Loaded_by" />
        <asp:BoundField DataField="Loaded_Date" HeaderText="PreLoaded Date Time" SortExpression="Loaded_Date" />   
        <asp:BoundField DataField="fVerified_by" HeaderText="First Verified by" SortExpression="fVerified_by" />
        <asp:BoundField DataField="Fvdate" 
            HeaderText="First Verified Date Time" SortExpression="Fvdate" />
        <asp:BoundField DataField="Sverifiedby" HeaderText="Second Verified by" SortExpression="Sverifiedby" />
        <asp:BoundField DataField="Svdate" HeaderText="Second Verified Date Time" SortExpression="Svdate" />
</Columns>
    <FooterStyle BackColor="#507CD1" Font-Bold="False" ForeColor="#FF8000" />
    <PagerStyle BackColor="#169116" ForeColor="White" HorizontalAlign="Right" />    
    <HeaderStyle BackColor="#169116" Font-Bold="True" ForeColor="White" 
         Wrap="True" HorizontalAlign="Center" />
    <EditRowStyle BackColor="#2461BF"  ForeColor="#CC3300" />    
    </asp:GridView>  
    </ContentTemplate>
</asp:UpdatePanel>
</td>
</tr>
<tr>
<td style="padding-left:350px">
<asp:Label ID="lblpge" runat="server" CssClass="labelall"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>

<tr>
     <td id="tdthree" runat="server" style="padding-top:70px" >
     <table align="left" cellpadding="0" cellspacing="0" border="0">
     <tr>
     <td align="left">
     <asp:Label ID="lblempno" runat="server" CssClass="labelallbigfont" Text="Scan NRIC / Staff ID / Pass" Width="250px"></asp:Label>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
        ControlToValidate="txtempid" ErrorMessage="*" SetFocusOnError="True" 
        ValidationGroup="empid"></asp:RequiredFieldValidator>
     </td>
   
     <td>
     <asp:TextBox ID="txtempid" runat="server" CssClass="textboxbigfont" Width="350px" 
             Height="30px" AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox>
     </td>
   <td>
<%--   <asp:Button ID="btnempenter" runat="server" Text="Enter" CssClass="btnbigfont" 
            Height="35px" Width="100px" 
           ValidationGroup="empid" onclick="btnempenter_Click"/>--%>
<asp:ImageButton ID="btnempenter" runat="server" CssClass="btn" ValidationGroup="empid"
            ImageUrl="~/ButtonImages/Enter24.png" onclick="btnempenter_Click" Height="22px"  />
   </td>
      <td>
  <%-- <asp:Button ID="btnempenterreject" runat="server" Text="Enter" CssClass="btnbigfont" 
            Height="35px" Width="100px"  
              ValidationGroup="empid" onclick="btnempenterreject_Click"/>--%>
 <asp:ImageButton ID="btnempenterreject" runat="server" CssClass="btn"  
            ImageUrl="~/ButtonImages/Enter24.png" onclick="btnempenterreject_Click" 
              Height="22px" ValidationGroup="empid"  />
   </td>
     </tr>
     </table> 


     </td>
     </tr>

          </table> 
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>