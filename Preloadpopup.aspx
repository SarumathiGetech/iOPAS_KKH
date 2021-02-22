<%@ Page Language="C#"  ValidateRequest="false" AutoEventWireup ="true" CodeFile="Preloadpopup.aspx.cs" Inherits="Preloadpopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>iOPAS</title>
    <link type="text/css" rel="stylesheet" href="css/cal.css" />  
    <script language="JavaScript" type="text/jscript">

        function closepopup() {
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnok').click();
            window.open('Preloadpopup.aspx', 'search', 'menubar=no,center:yes,scrollbars=no,width=620,height=500[color=blue]40,top=100,left=200')
            window.close();
        }

        function passValueToParent(val1) {
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_searchvalue').value = val1;
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnok').click();
            window.open('Preloadpopup.aspx', 'search', 'menubar=no,center:yes,scrollbars=no,width=620,height=500[color=blue]40,top=100,left=200')
            window.close();
        }
        function passValuesToParent(val1, val2, val3) {
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_searchvalue').value = val1;
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtexpdate1').value = val2;
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtbatchno1').value = val3;
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnok').click();
            window.open('Preloadpopup.aspx', 'search', 'menubar=no,center:yes,scrollbars=no,width=620,height=500[color=blue]40,top=100,left=200')
            window.close();
        }

        // Search Window open using Enter Key \\ 
        function doClick(e) {
            var key;

            if (window.event)
                key = window.event.keyCode;     //IE
            else
                key = e.which;     //firefox

            if (key == 13) {
                             
                document.getElementById('<%=btnbarcodesearch.ClientID%>').click();
                event.keyCode = 0
            }
        }
       

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

       <%-- function doClick3(e) {
            var key;

            if (window.event)
                key = window.event.keyCode;     //IE
            else
                key = e.which;     //firefox

            if (key == 13) {
                document.getElementById('<%=btngs1barcodesearch.ClientID%>').click();
                 event.keyCode = 0
             }
        }--%>

        <%--function doClick4(e) {
            var key;

            if (window.event)
                key = window.event.keyCode;     //IE
            else
                key = e.which;     //firefox

            if (key == 13) {
                document.getElementById('<%=btngs1barcodecancel.ClientID%>').click();
                event.keyCode = 0
            }
        }--%>

    </script>
</head>
<body style="background-color:#D2FECF">
    <form id="form1" runat="server" style="background-color:#D2FECF">
     <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:UpdatePanel ID="updpanel2" runat="server">
     <ContentTemplate>
     <table width="99%" cellpadding="0" cellspacing="0" border="0">
     <tr>
     <td align="center" >
    
     <table id="Fsttd" runat="server"  cellpadding="0" cellspacing="0" border="0" 
             style="border: thin outset #C0C0C0">
     <tr>
     <td align="center" >
     <asp:Label ID="lblhead" runat="server" Text="PreLoading Item Search" CssClass="labelhead" ></asp:Label>
     </td>
     </tr>
     <tr>
          
     <td>
     <table align="left" border="0" cellpadding="0" cellspacing="0">
     <tr>
       <td align="left" style="padding-left:5px">
        <asp:Label ID="Label5" runat="server" Text="Drug BarCode" CssClass= "labelall" Width="85px"></asp:Label>
     </td>
 
    
     <td>
     <asp:TextBox ID="txtbarcode" runat="server" Width="467px" Height="30px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
     </td>
     </tr>
    </table>
     </td>
     </tr>
  
     
     <tr>
     <td>
     <table border="0" cellpadding="0" cellspacing="0">
     
   
     <tr>
     <td align="left" style="padding-left:5px">
     <asp:Label ID="lbldrugname" runat="server" Text="Item Code" CssClass= "labelall"></asp:Label>
     </td>
     <td align="left">
     <asp:TextBox ID="txtitemcode" runat="server" Width="190px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
     </td>
     <td align="left" style="padding-left:3px">
     <asp:Label ID="Label2" runat="server" Text="Drug Code" CssClass= "labelall"></asp:Label>
     </td>
     <td>
     <asp:TextBox ID="txtdrugcode" runat="server" Width="190px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
     </td>
      
     </tr>
     <tr>
     <td align="left" style="padding-left:5px">
      <asp:Label ID="Label1" runat="server" Text="Item Name" CssClass= "labelall"></asp:Label>
     </td>
     <td colspan="3">
    <asp:TextBox ID="txtitemname" runat="server" Width="467px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
     </td>  
       
        
     </tr>  
     <tr>
      <td align="left" style="padding-left:5px">
      <asp:Label ID="Label4" runat="server" Text="Brand Name" CssClass= "labelall" 
              Width="85px"></asp:Label>
     </td>
     <td>
    <asp:TextBox ID="txtbrand" runat="server" Width="190px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
     </td> 
     <td align="left" style="padding-left:3px">
      <asp:Label ID="Label3" runat="server" Text="MFR Barcode" CssClass= "labelall" 
              Width="80px"></asp:Label>
     </td>
     <td >
    <asp:TextBox ID="txtmfrcode" runat="server" Width="190px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
     </td> 
     <td>
 <%--     <asp:Button ID="btnclear" runat="server" Text="Clear" Height="22px" 
              onclick="btnclear_Click" UseSubmitBehavior="False" Width="50px" CssClass="btn" />--%>
  <asp:ImageButton ID="btnclear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnclear_Click" Height="20px"/>
     </td>
    <td >
<%--    <asp:Button ID="btnsearch" runat="server" Text="Search" Height="22px" CssClass="btn" 
             onclick="btnsearch_Click" Width="50px" />--%>
        <asp:ImageButton ID="btnsearch" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Search.png" onclick="btnsearch_Click" Height="20px"/>      
     </td>  
     </tr>  
      </table>
     </td>
     </tr>
     </table>
     </td>
     </tr>
     <tr>
     <td align="center" >     
     <table id="secondtd" runat="server" width="100%" cellpadding="0" cellspacing="0" border="0">
     <tr>
     <td align="left">
      <asp:GridView ID="preloadpopgrid" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" 
        Width="99%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left" RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" 
        onrowdatabound="preloadpopgrid_RowDataBound" onselectedindexchanging="preloadpopgrid_SelectedIndexChanging" 
        AllowPaging="True" onpageindexchanging="preloadpopgrid_PageIndexChanging" 
        onrowcommand="preloadpopgrid_RowCommand" CssClass="gridcss" 
             onsorting="preloadpopgrid_Sorting">  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns>
       <%--<asp:TemplateField>                 
                    <ItemTemplate>
                        <asp:Button ID="btnSelect" runat="server" Text="Select" Font-Bold="false" Font-Size="X-Small" Height="18px" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                 <asp:ButtonField Text="Select" ButtonType="Image" CommandName="Btn" 
            ImageUrl="~/ButtonImages/GridSelect.png">            
                  <ControlStyle Height="18px" Font-Size="X-Small" />
                  <HeaderStyle Height="18px" />
                  <ItemStyle Height="18px" Width="20px"/>
             </asp:ButtonField>
        <asp:BoundField DataField="ID" Visible="false"/>
            <%--<ItemStyle BackColor="White" ForeColor="White" />
        </asp:BoundField>--%>
        <asp:BoundField DataField="Item_Code" HeaderText="Item Code" SortExpression="Item_Code" />
        <asp:BoundField DataField="Item_Name" HeaderText="Item Name" SortExpression="Item_Name" />
        <asp:BoundField DataField="Brandname" HeaderText="Brand Name" SortExpression="Brandname" />
        <asp:BoundField DataField="PackType" HeaderText="Pack Type" SortExpression="PackType" />
        <asp:BoundField DataField="Pack_Size" HeaderText="Pack Size" SortExpression="Pack_Size" />
        <asp:BoundField DataField="UOM" HeaderText="UOM" SortExpression="UOM" />
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
     <td>
     <asp:Label ID="lblpge" runat="server" CssClass="labelall"></asp:Label>
     </td>
     </tr>
     </table>   
     
     <tr>
     <td >
     <table id="threetd" runat="server" align="center" cellpadding="0" cellspacing="0" border="0">
     <tr>
     <td>
     <asp:Label ID="lblmfrbarcode" runat="server" CssClass="labelall tdalign"  Text="MFR/GS1 Barcode" width="110px"></asp:Label>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtmfrbarcode" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="barcode"
                ></asp:RequiredFieldValidator>
     </td>
   
     <td>
     <asp:TextBox ID="txtmfrbarcode" runat="server" CssClass="textbox" AutoCompleteType="Disabled" width="510px"></asp:TextBox>
     </td>
   <td>
   <%--<asp:Button ID="btnbarcodesearch" runat="server" Text="Search" CssClass="btn" 
           onclick="btnbarcodesearch_Click" />--%>
  <asp:ImageButton ID="btnbarcodesearch" runat="server" CssClass="btn" ValidationGroup="barcode"
            ImageUrl="~/ButtonImages/Search.png" onclick="btnbarcodesearch_Click" Height="22px"/>
   </td>
     </tr>
     </table> 
  
    </td>
     </tr>
     <tr>
     <td >
     <%--<table id="gs1Table" runat="server" align="center" cellpadding="0" cellspacing="1" border="0">
     <tr>--%>
     <%--<td>
     <asp:Label ID="lblgs1barcode" runat="server" CssClass="labelall tdalign gs1style"  Text="GS1 Barcode" Width="80px"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txtgs1barcode" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="barcode"
                ></asp:RequiredFieldValidator>
         </td>--%>
   
    <%-- <td>
     <asp:TextBox ID="txtgs1barcode" runat="server" CssClass="textbox " Width="500px" AutoCompleteType="Disabled"></asp:TextBox>
     </td>--%>
       
   <%--<td>
   <asp:ImageButton ID="btngs1barcodesearch" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Search.png" ValidationGroup="barcode" onclick="btngs1barcodesearch_Click" Height="22px"/>
   </td>
  <td>
		   <asp:ImageButton ID="btngs1barcodecancel" runat="server" CssClass="btn" 
					ImageUrl="~/ButtonImages/Cancel.png"  onclick="btngs1barcodecancel_Click" Height="21px"/>
	</td>
     </tr>
     </table> --%>
  
     </td>
     </tr>
     </table>

     </td>
     </tr> 
     </td>
     </tr>  
     </table>
  
     </ContentTemplate>
     </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>

