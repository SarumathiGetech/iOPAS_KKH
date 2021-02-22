<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="druginventory.aspx.cs" Inherits="druginventory" %>
<asp:Content ID="content" runat="server" ContentPlaceHolderID="head" >
    </asp:Content>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <asp:ScriptManager ID="script1" runat="server" ></asp:ScriptManager>
     <script language="javascript" type="text/javascript">
         function itemsearch() {
             var icode = document.getElementById('<%=txtitemcode.ClientID%>').value;
             var dcode = document.getElementById('<%=txtdrugcode.ClientID%>').value;
             var iname = document.getElementById('<%=txtitemname.ClientID%>').value;
             window.document.clear();
             window.open(("Druginventpopup.aspx?drcode=" + dcode + " &itcode=" + icode + " &itname=" + iname), 'search', 'menubar=no, toolbar=no,center:yes,scrollbars=no,width=720,height=500[color=blue]40,top=100,left=200')
             window.close();
         }

         // Search Window open using Enter Key \\ 
         function doClick2(e) {
             var key;

             if (window.event)
                 key = window.event.keyCode;     //IE
             else
                 key = e.which;     //firefox

             if (key == 13) {
                 document.getElementById('<%=search.ClientID%>').click();
                 event.keyCode = 0
             }
         }
     </script>
<asp:UpdatePanel ID="updpanel" runat="server">
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="lblhead" runat="server" Text="Drug Inventory" CssClass="labelhead" ></asp:Label>
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
        CssClass="labelall" Width="105px" ></asp:Label>
</td>
<td align="left">
<asp:DropDownList ID="ddlpharloc" runat="server" Width="250px" AutoPostBack="True" 
        onselectedindexchanged="ddlpharloc_SelectedIndexChanged" CssClass="textbox"></asp:DropDownList>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblddsno" runat="server" Text="DDS / BDS Name" CssClass="labelall" ></asp:Label>
</td>
<td align="left">
<asp:DropDownList ID="ddlddsno" runat="server" Width="250px" AutoPostBack="True" 
        onselectedindexchanged="ddlddsno_SelectedIndexChanged" CssClass="textbox" >
    <asp:ListItem>ALL</asp:ListItem>
    </asp:DropDownList>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="Label1" runat="server" Text="Item code" CssClass="labelall" ></asp:Label>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtitemcode" runat="server" Width="190px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td align="left" style="padding-left:3px">
<asp:Label ID="Label2" runat="server" Text="Drug code" CssClass="labelall" ></asp:Label>
</td>
<td align="left" style="padding-left:3px">
<asp:TextBox ID="txtdrugcode" runat="server" Width="190px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblitemname" runat="server" Text="Item Name" CssClass="labelall"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtitemname" runat="server" Width="447px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>

</td>
 <td align="left">
 <asp:TextBox ID="searchvalue" runat="server" CausesValidation="false" Style="position: static; display: none"/>  
 </td>
<td align="left">
 <asp:Button ID="btnok" runat="server" CausesValidation="False" OnClick="btnok_Click" Style="position: static; display: none" Text="Ok" />           
 </td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblbrand" runat="server" CssClass="labelall" Text="Brand Name"></asp:Label>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:DropDownList ID="ddlbrand" runat="server" Width="453px" AutoPostBack="True" 
        onselectedindexchanged="ddlbrand_SelectedIndexChanged" CssClass="textbox" ></asp:DropDownList>
</td>

</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblpacktype" runat="server" Text="Pack Type" CssClass="labelall"></asp:Label>
</td>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:DropDownList ID="ddlpacktype" runat="server" Width="130px" AutoPostBack="True" 
        onselectedindexchanged="ddlpacktype_SelectedIndexChanged" CssClass="textbox" >
<asp:ListItem>-Select-</asp:ListItem>
</asp:DropDownList>
</td>
<td align="left" style="padding-left:3px">
<asp:Label ID="Label3" runat="server" Text="Pack Size " CssClass="labelall" 
        Width="60px"></asp:Label>
</td>
<td align="left">
<asp:DropDownList ID="ddlpacksize" runat="server" Width="105px" AutoPostBack="True" 
        onselectedindexchanged="ddlpacksize_SelectedIndexChanged" style="height: 22px" CssClass="textbox" >
<asp:ListItem>-Select-</asp:ListItem>
</asp:DropDownList>
</td>
<td align="left" style="padding-left:3px">
<asp:Label ID="Label4" runat="server" Text="UOM" CssClass="labelall" Width="35px"></asp:Label>
</td>
<td align="left">

<asp:TextBox ID="txtuom" runat="server" Width="112px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
<td align="left" style="padding-left:3px">
   <asp:ImageButton ID="btncl" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btncl_Click" Height="20px"/>
</td>
<td align="left">
       <asp:ImageButton ID="search" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Search.png" onclick="search_Click" 
        Height="20px"/>
</td>
<td align="left" style="padding-left:3px">
      <asp:ImageButton ID="Button2" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/CaculateTotal.png" onclick="Button2_Click" 
        Height="20px" Visible="False"/>
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
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left" style="padding-left:250px">
<asp:Label ID="lblnorecord" runat="server" CssClass="labelall"></asp:Label>
</td>
</tr>
<tr>
<td>
<asp:GridView ID="griddruginven" runat="server" AutoGenerateColumns="False" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" onpageindexchanging="griddruginven_PageIndexChanging" 
        CssClass="gridcss" onsorting="griddruginven_Sorting" AllowSorting="True" 
        onrowdatabound="griddruginven_RowDataBound" ShowFooter="true" 
        PageSize="50">  
        <RowStyle BackColor="#EFF3FB" Wrap="True"/>
    <Columns>
        <asp:BoundField DataField="DDS_Name" HeaderText="DDS / BDS Name" />
        <asp:BoundField DataField="Cell_Id" HeaderText="Cell No"  />
        <asp:BoundField DataField="Cartridge_Id" HeaderText="Cartridge No" />
        <asp:BoundField DataField="Expiry_Date" HeaderText="Expiry Date" />
        <asp:BoundField DataField="Max_Alert_Qty_DDS" HeaderText="Min Alert Quantity Per DDS / BDS ">
         <ItemStyle HorizontalAlign="Center" />            
        </asp:BoundField>
<%--        <asp:BoundField DataField="Batch_No" HeaderText="Batch No" SortExpression="Batch_No"/>--%>

  <asp:TemplateField HeaderText="Batch No" >
        <ItemTemplate>
				<div style="text-align: center;">
				<asp:Label ID="lblBatch_No" runat="server" Text='<%# Eval("Batch_No") %>'/>
				</div>
			 </ItemTemplate>
             <FooterTemplate>
				<div style="text-align: center;">
				<asp:Label ID="lblBatch_Nodis" runat="server" Text="Total"/>
				</div>
			 </FooterTemplate>
        </asp:TemplateField>
      <%--  <asp:BoundField DataField="Aval_Quantity" HeaderText="System Qty" SortExpression="Aval_Quantity">
            <ItemStyle HorizontalAlign="Center" />
            
        </asp:BoundField>--%>
           <asp:TemplateField HeaderText="System Qty">
        <ItemTemplate>
				<div style="text-align: center;">
				<asp:Label ID="lblavalqty" runat="server" Text='<%# Eval("Aval_Quantity") %>'/>
				</div>
			 </ItemTemplate>
             <FooterTemplate>
				<div style="text-align: center;">
				<asp:Label ID="lblavalTotalqty" runat="server"/>
				</div>
			 </FooterTemplate>
        </asp:TemplateField>

        <%--<asp:BoundField DataField="Pre_Allocated" HeaderText="Preallocated Qty" SortExpression="Pre_Allocated">    
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>--%>

         <asp:TemplateField HeaderText="Preallocated Qty">
        <ItemTemplate>
				<div style="text-align: center;">
				<asp:Label ID="lblprealqty" runat="server" Text='<%# Eval("Pre_Allocated") %>'/>
				</div>
			 </ItemTemplate>
             <FooterTemplate>
				<div style="text-align: center;">
				<asp:Label ID="lblprealTotalqty" runat="server"/>
				</div>
			 </FooterTemplate>
        </asp:TemplateField>

<%--
         <asp:BoundField DataField="computedbal" HeaderText="Computed Balance" SortExpression="computedbal">        
             <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>--%>

         <asp:TemplateField HeaderText="Computed Balance">
        <ItemTemplate>
				<div style="text-align: center;">
				<asp:Label ID="lblcompalqty" runat="server" Text='<%# Eval("computedbal") %>' />
				</div>
			 </ItemTemplate>
             <FooterTemplate>
				<div style="text-align: center;">
				<asp:Label ID="lblcompTotalqty" runat="server"/>
				</div>
			 </FooterTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="PackType" HeaderText="Pack Type" />        
        <asp:BoundField DataField="Pack_Size" HeaderText="Pack Size"  >
        <ItemStyle HorizontalAlign="Center"/>
        </asp:BoundField>
        <asp:BoundField DataField="UOM" HeaderText="Uom"/>     
       
    </Columns>
      <FooterStyle BackColor="#169116" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#169116" ForeColor="White" HorizontalAlign="Right" />  
    <HeaderStyle BackColor="#169116" Font-Bold="True" ForeColor="White" 
       Wrap="True" HorizontalAlign="Center" />
    <EditRowStyle BackColor="#2461BF"  ForeColor="#CC3300" />
    </asp:GridView>  
</td>
</tr>
<tr>
<td align="left">
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td style="width:400px">
<asp:Label ID="lblpge" runat="server" CssClass="labelall"></asp:Label>
</td>
<td align="left" style="padding-left:90px">
<asp:Label ID="lbltot" runat="server" CssClass="labelall" Text="Total"></asp:Label>
</td>
<td align="left" style="padding-left:11px">
<asp:TextBox ID="txtsysqty" runat="server" Width="50px" CssClass="inventorytxt"></asp:TextBox>
</td>
<td align="left" style="padding-left:80px">
<asp:TextBox ID="txtpreallot" runat="server" Width="50px" CssClass="inventorytxt"></asp:TextBox>
</td>
<td align="left" style="padding-left:115px">
<asp:TextBox ID="txtcomputed" runat="server" Width="50px" CssClass="inventorytxt"></asp:TextBox>
</td>
<td align="left" style="padding-left:5px">
<asp:Label ID="lblptype" runat="server" CssClass="labelall"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
</table>
</td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
