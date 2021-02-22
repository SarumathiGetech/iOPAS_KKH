<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Druginventpopup.aspx.cs" Inherits="Druginventpopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>iOPAS</title>
    <link type="text/css" rel="stylesheet" href="css/cal.css" />  
</head>
<body style="background-color:#D2FECF">
    <form id="form1" runat="server" style="background-color:#D2FECF">
     <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <script language="JavaScript" type="text/jscript">

            function closepopup() {
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnok').click();
                //window.open('Druginventpopup.aspx', 'search', 'menubar=no,center:yes,scrollbars=no,width=620,height=500[color=blue]40,top=100,left=200')
                window.close();
            }

            function passValueToParent(val) {
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_searchvalue').value = val;
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnok').click();
                window.document.clear();
               // window.open('Druginventpopup.aspx', 'search', 'menubar=no,center:yes,scrollbars=no,width=620,height=500[color=blue]40,top=100,left=200')
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
                    document.getElementById('<%=btnsearch.ClientID%>').click();
                    event.keyCode = 0
                }
            }
</script>   
        <asp:UpdatePanel ID="updpanel2" runat="server">
     <ContentTemplate>
     <table width="100%" cellpadding="0" cellspacing="0" border="0">
     <tr>
     <td align="center">
    
     <table  cellpadding="0" cellspacing="0" border="0" 
             style="border: thin outset #C0C0C0">
     <tr>
     <td align="center" colspan="4">
     <asp:Label ID="lblhead" runat="server" Text="Item Search" CssClass="labelhead"></asp:Label>
     </td>
     </tr>
     
     <tr>
     <td align="left" style="padding-left:4px">
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
     <td align="left" style="padding-left:4px">
      <asp:Label ID="Label1" runat="server" Text="Item Name" CssClass= "labelall"></asp:Label>
     </td>
     <td colspan="3">
    <asp:TextBox ID="txtitemname" runat="server" Width="479px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
     </td>  
      
        
     </tr>  
     <tr>
      <td align="left" style="padding-left:4px">
      <asp:Label ID="Label4" runat="server" Text="Brand Name" CssClass= "labelall" 
              Width="80px"></asp:Label>
     </td>
     <td>
    <asp:TextBox ID="txtbrand" runat="server" Width="190px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
     </td> 
     <td align="left" style="padding-left:3px">
      <asp:Label ID="Label3" runat="server" Text="MFR Barcode" CssClass= "labelall" 
              Width="90px"></asp:Label>
     </td>
     
     <td >
    <asp:TextBox ID="txtmfrcode" runat="server" Width="190px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
     </td> 
      <td>
   <%--   <asp:Button ID="btnclear" runat="server" Text="Clear" Height="22px" CssClass="btn" 
              onclick="btnclear_Click" UseSubmitBehavior="False" Width="50px"/>--%>
  <asp:ImageButton ID="btnclear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnclear_Click" Height="20px"/>
     </td>
    <td >
<%--    <asp:Button ID="btnsearch" runat="server" Text="Search" Height="22px" CssClass="btn" 
            onclick="btnsearch_Click" Width="50px"/>--%>
         <asp:ImageButton ID="btnsearch" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Search.png" onclick="btnsearch_Click" Height="20px"/>     
     </td>  
     </tr>  
    
     </table>
     </td>
     </tr>
     <tr>
     <td align="center">     
     <table width="700"  cellpadding="0" cellspacing="0" border="0">
     <tr>
     <td align="left">
      <asp:GridView ID="Druginvepop" runat="server" AutoGenerateColumns="False" DataKeyNames="MasterID" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left" RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
             onrowdatabound="Druginvepop_RowDataBound" AllowPaging="True" 
             onpageindexchanging="Druginvepop_PageIndexChanging" 
             onselectedindexchanged="Druginvepop_SelectedIndexChanged" 
             onrowcommand="Druginvepop_RowCommand" CssClass="gridcss">  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns>
  
       <asp:ButtonField Text="Select" ButtonType="Image" CommandName="Btn" 
            ImageUrl="~/ButtonImages/GridSelect.png">            
                  <ControlStyle Height="18px" Font-Size="X-Small" />
                  <HeaderStyle Height="18px" />
                  <ItemStyle Height="18px"/>
             </asp:ButtonField>
        <asp:BoundField DataField="ID" Visible="false"/>
        
        <asp:BoundField DataField="Item_Code" HeaderText="Item Code" />
        <asp:BoundField DataField="Item_Name" HeaderText="Item Name" />
        <asp:BoundField DataField="Drug_Code" HeaderText="Drug Code" />
<%--        <asp:BoundField DataField="Packtype" HeaderText="Pack Type" />--%>
       
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
     </td>
     </tr>
     </table>  
     </ContentTemplate>
     </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>

