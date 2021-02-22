<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Preloadedenquery.aspx.cs" Inherits="Preloadedenquery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
 <script language="javascript" type="text/javascript" src="cal/popcalendar.js" ></script>  


 <script language="JavaScript" type="text/jscript">
     // Search Window open using Enter Key \\ 
     function doClick2(e) {
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

     function confirmProcess() {
         if (confirm('Selected records will be canceled')) {
             document.getElementById('<%=btnok.ClientID%>').click();
                 }

             }
</script>  
 <asp:UpdatePanel ID="upd1" runat="server">
 <ContentTemplate> 
     <table width="100%" cellpadding="0" cellspacing="0" border="0">
     <tr>
     <td>
     <table align="center" cellpadding="0" cellspacing="0" border="0">
     <tr>
     <td>
     <asp:Label ID="lblhead" runat="server" Text="Cartridge Status Enquiry" CssClass="labelhead" ></asp:Label>
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
     <asp:Label ID="Label5" runat="server" Text="Pharmacy Location" CssClass= "labelall" 
             Width="130px"></asp:Label>
     </td>
     <td colspan="3" >
     <asp:DropDownList ID="ddlpharname" runat="server" Width="598px" CssClass="textbox" ></asp:DropDownList>
     </td>
     </tr>   
     <tr>
     <td align="left">
     <asp:Label ID="lbldrugname" runat="server" Text="Item Code" CssClass= "labelall"></asp:Label>
     </td>
     <td>
     <asp:TextBox ID="txtitemcode" runat="server" Width="250px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>    
     </td>
     <td align="left" style="padding-left:5px">
     <asp:Label ID="Label2" runat="server" Text="Drug Code" CssClass= "labelall"></asp:Label>
     </td>
     <td>
      <asp:TextBox ID="txtdrugcode" runat="server" Width="253px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>    
     </td>         
     </tr>
     <tr>
     <td align="left">
      <asp:Label ID="Label1" runat="server" Text="Item Name" CssClass= "labelall"></asp:Label>
     </td>
     <td colspan="3">
     <asp:TextBox ID="txtitemname" runat="server" Width="595px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
     </td>     
     </tr> 
     <tr>    
      <td align="left" >
     <asp:Label ID="Label4" runat="server" Text="Brand Name" CssClass="labelall" 
              Width="75px"></asp:Label>
     </td>
     <td>
     <asp:TextBox ID="txtbrand" runat="server" Width="250px" CssClass="textbox" AutoCompleteType="Disabled" ></asp:TextBox>
     </td>
     <td align="left" style="padding-left:5px">
     <%-- <asp:Label ID="Label3" runat="server" Text="MFR Barcode" CssClass= "labelall" 
              Width="80px"></asp:Label>--%>
<asp:Label ID="lblcartno" runat="server" Text="Cartridge No" CssClass="labelall"></asp:Label>
     </td>
     <td>
     <asp:TextBox ID="txtcartno" runat="server" Width="253px" CssClass="textbox" AutoCompleteType="Disabled" ></asp:TextBox>
   <%-- <asp:TextBox ID="txtmfrcode" runat="server" Width="182px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>--%>
     </td>  
     <%--     <td>
     <asp:Label ID="lblcartno" runat="server" Text="Cartridge No" CssClass="labelall"></asp:Label>
     </td>
     <td>
     <asp:TextBox ID="txtcartno" runat="server" Width="190px" CssClass="textbox" AutoCompleteType="Disabled" ></asp:TextBox>
     </td>     --%>
     </tr>  
     <tr>
       <td align="left" >
     <asp:Label ID="Label6" runat="server" Text="HDDS/BDS Name" CssClass="labelall"></asp:Label>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
              ErrorMessage="*" ControlToValidate="ddlmachinename" Display="Dynamic" 
              InitialValue="-Select-" ValidationGroup="Mcname"></asp:RequiredFieldValidator>
     </td>
     <td>
     <asp:DropDownList ID="ddlmachinename" runat="server" Height="22px" Width="254px" 
             CssClass="textbox" AutoPostBack="True" 
             onselectedindexchanged="ddlmachinename_SelectedIndexChanged">
          <asp:ListItem>-Select-</asp:ListItem>
         <asp:ListItem>BDS</asp:ListItem>         
         <asp:ListItem>HDDS</asp:ListItem>
         </asp:DropDownList>
     </td>
     <td align="left" style="padding-left:5px">
     <asp:Label ID="lblfilter" runat="server" Text="Status" CssClass="labelall"></asp:Label>
     
     </td>
     <td>
     <asp:DropDownList ID="ddlfilter" runat="server" Height="22px" Width="258px" 
             CssClass="textbox" onselectedindexchanged="ddlfilter_SelectedIndexChanged">
         </asp:DropDownList>         
     </td>
        </tr>
        <tr>
        <td colspan="4" align="right" style="padding-top:10px" class="style1" >
            <table cellpadding="0" cellspacing="0" border="0">
      <tr>
    
      <td >
   
          <asp:ImageButton ID="btnclear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnclear_Click" Height="20px"/>
       </td>
       <td>   
      
       <asp:ImageButton ID="btnsearch" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Search.png" onclick="btnsearch_Click" Height="20px" 
               ValidationGroup="Mcname"/>
       </td>
<%--      <td>
      
 <asp:ImageButton ID="btnprint" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Print.png" onclick="btnprint_Click" Height="20px" 
              ValidationGroup="Mcname" Visible="False"/>
      </td>  --%> 
  </tr>
      </table> 
        </td>
        </tr>
          </table>      
    </td>
     </tr>    
     <tr>
     <td>
     <table align="left"  border="0" cellpadding="0" cellspacing="0">
     <tr>

       <td style="padding-left:5px">     
       <asp:ImageButton ID="btncancel" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Cancel.png"  Height="20px" 
                onclick="btncancel_Click"/>
           <asp:Button ID="btnok" runat="server" CausesValidation="False" OnClick="btnok_Click" Style="position: static; display: none" Text="Ok" />   
       </td>
     </tr>
     </table>
     </td>
  
     </tr>   
     <tr>
<td align="left">
<table align="left" width="100%" border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left">
<asp:GridView ID="gridstatus" runat="server" AutoGenerateColumns="False" 
        Width="100%"  CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        AllowPaging="True" onpageindexchanging="gridstatus_PageIndexChanging" 
        CssClass="gridcss" onsorting="gridstatus_Sorting" AllowSorting="True" 
        PageSize="20" >  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
 <Columns>
        <asp:BoundField DataField="Cartridge_Id" HeaderText="Cartridge No" SortExpression="Cartridge_Id" />        
        <asp:BoundField DataField="Item_Code" HeaderText="Item Code" SortExpression="Item_Code" />
        <asp:BoundField DataField="Item_Name" HeaderText="Item Name" SortExpression="Item_Name" />
        <asp:BoundField DataField="brandname" HeaderText="Brand Name" SortExpression="brandname" />
        <asp:BoundField DataField="PackType" HeaderText="Pack Type" SortExpression="PackType" />
        <asp:BoundField DataField="Pack_Size" HeaderText="Pack Size" SortExpression="Pack_Size" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="UOM" HeaderText="UOM" SortExpression="UOM" />
        <asp:BoundField DataField="Verified_Status" HeaderText="Status" SortExpression="Verified_Status" />
        <asp:BoundField DataField="Loaded_by" HeaderText="PreLoaded by" SortExpression="Loaded_by" />
        <asp:BoundField DataField="Loaded_Date" HeaderText="PreLoaded Date Time" SortExpression="Loaded_Date" />   
        <asp:BoundField DataField="verified_by" HeaderText="First Verified by" SortExpression="verified_by" />
        <asp:BoundField DataField="FVdate" 
            HeaderText="First Verified Date Time" SortExpression="FVdate" />
        <asp:BoundField DataField="Sverify" HeaderText="Second Verified by" SortExpression="Sverify" />
        <asp:BoundField DataField="SVdate" HeaderText="Second Verified Date Time" SortExpression="SVdate" />
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
<td align="left">
<asp:GridView ID="BDSGrid" runat="server" AutoGenerateColumns="False" 
        Width="100%"  CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        AllowPaging="True"  
        CssClass="gridcss"  AllowSorting="True" 
        PageSize="20" onpageindexchanging="BDSGrid_PageIndexChanging" 
        onsorting="BDSGrid_Sorting" >  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
 <Columns>
        <asp:TemplateField>
 
    <ItemTemplate>
        <asp:CheckBox ID="chkrow" runat="server" />
    </ItemTemplate>    
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
        <asp:BoundField DataField="Cart_Barcode" HeaderText="Barcode" SortExpression="Cart_Barcode" />  
        <asp:BoundField DataField="Cartridge_Id" HeaderText="Cartridge No" SortExpression="Cartridge_Id" />        
        <asp:BoundField DataField="Item_Code" HeaderText="Item Code" SortExpression="Item_Code" />
        <asp:BoundField DataField="Item_Name" HeaderText="Item Name" SortExpression="Item_Name" />
        <asp:BoundField DataField="brandname" HeaderText="Brand Name" SortExpression="brandname" />
        <asp:BoundField DataField="Cart_Type" HeaderText="Carton Box Of" SortExpression="Cart_Type">
        <ItemStyle HorizontalAlign="Center" />
         </asp:BoundField>   
        <asp:BoundField DataField="PackType" HeaderText="Pack Type" SortExpression="PackType" />
        <asp:BoundField DataField="Pack_Size" HeaderText="Pack Size" SortExpression="Pack_Size" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="UOM" HeaderText="UOM" SortExpression="UOM" />   
           <asp:BoundField DataField="Pending_Quantity" HeaderText="Pending For Allocation" SortExpression="Pending_Quantity" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField> 
         <asp:BoundField DataField="Alloted_Quantity" HeaderText="Alloted Quantity" SortExpression="Alloted_Quantity" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>     
        <asp:BoundField DataField="Loaded_Quantity" HeaderText="Loaded Quantity" SortExpression="Loaded_Quantity" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>  
        <asp:BoundField DataField="Verified_Status" HeaderText="Status" SortExpression="Verified_Status" />
        <asp:BoundField DataField="Loaded_by" HeaderText="PreLoaded by" SortExpression="Loaded_by" />
        <asp:BoundField DataField="Loaded_Date" HeaderText="PreLoaded DT" SortExpression="Loaded_Date" />   
         <asp:BoundField DataField="First_Verified" HeaderText="First Verified by" SortExpression="First_Verified" />
        <asp:BoundField DataField="First_Verified_DT" HeaderText="First Verified DT" SortExpression="First_Verified_DT" />   
        
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
<td align="left">
<asp:GridView ID="BotUnloadingGrid" runat="server" AutoGenerateColumns="False" 
        Width="100%"  CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        AllowPaging="True"  
        CssClass="gridcss"  AllowSorting="True" 
        PageSize="20" onpageindexchanging="BotUnloadingGrid_PageIndexChanging" 
        onsorting="BotUnloadingGrid_Sorting">  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
 <Columns>
         <asp:BoundField DataField="DDS_Name" HeaderText="BDS Name" SortExpression="DDS_Name" />
        <asp:BoundField DataField="Cartridge_Id" HeaderText="Cartridge No" SortExpression="Cartridge_Id" />
        <asp:BoundField DataField="Item_Code" HeaderText="Item Code" SortExpression="Item_Code" />
        <asp:BoundField DataField="Item_Name" HeaderText="Item Name" SortExpression="Item_Name" />
        <asp:BoundField DataField="BrandName" HeaderText="Brand Name" SortExpression="BrandName" />        
        <asp:BoundField DataField="PackType" HeaderText="Pack Type" SortExpression="PackType" />
        <asp:BoundField DataField="Pack_Size" HeaderText="Pack Size" SortExpression="Pack_Size" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="UOM" HeaderText="UOM" SortExpression="UOM" />    
        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />    
        <asp:BoundField DataField="UnloadedBy" HeaderText="UnLoaded by" SortExpression="UnloadedBy" />
        <asp:BoundField DataField="UnloadedDate" HeaderText="UnLoaded Date Time" SortExpression="UnloadedDate" />  
        
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
<td style="padding-left:250px">
<asp:Label ID="lblpge" runat="server" CssClass="labelall" Text="No Record Found"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
</table> 
 </ContentTemplate>
 </asp:UpdatePanel> 
</asp:Content>

