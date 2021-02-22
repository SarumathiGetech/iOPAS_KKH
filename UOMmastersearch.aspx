<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UOMmastersearch.aspx.cs" Inherits="UOMmastersearch" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>iOPAS</title>
    <link type="text/css" rel="stylesheet" href="css/cal.css" />  
    <script language="JavaScript" type="text/jscript">

        function closepopup() {
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnok').click();
            //window.open("Itemmastersearch.aspx", 'search', 'menubar=no,center:yes,scrollbars=no,width=720,height=500[color=blue]40,top=0,left=0')
            window.close();
        }
        function passValueToParent(val,val1,val2) {
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtitemcode').value = val;
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtdrugcode').value = val1;
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtitemnameadd').value = val2;
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnok').click();
            //window.open("Itemmastersearch.aspx",'search', 'menubar=no,center:yes,scrollbars=no,width=720,height=500[color=blue]40,top=0,left=0')
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
                document.getElementById('<%=btnadd.ClientID%>').click();
                event.keyCode = 0
            }
        }     
</script>
</head>
<body style="background-color:#D2FECF">
    <form id="form1" runat="server" style="background-color:#D2FECF">
     <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:UpdatePanel ID="updpanel2" runat="server">
     <ContentTemplate>
     <table width="100%" cellpadding="0" cellspacing="0" border="0">
     <tr>
     <td>
     <table align="center" cellpadding="0" cellspacing="0" border="0">
     <tr>
     <td>
     <asp:Label ID="lblhead" runat="server" Text="Item Master Search" CssClass="labelhead" ></asp:Label>
     </td>
     </tr> 
     </table>
     </td>
     </tr>
     <tr>
     <td align="center">    
     <table  cellpadding="0" cellspacing="0" border="0" 
             style="border: thin outset #C0C0C0">
          
     <tr>
     <td align="left" style="padding-left:3px">
     <asp:Label ID="Label1" runat="server" Text="Item Code" CssClass= "labelall"></asp:Label>
     </td>
     <td>     
      <asp:TextBox ID="txtitemcode" runat="server" Width="190px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>&nbsp;
      <asp:Label ID="Label2" runat="server" Text="Drug Code " CssClass= "labelall"></asp:Label>
       <asp:TextBox ID="txtdrgcode" runat="server" Width="190px"  
              CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
     </td> 
                   
     </tr> 
     <tr>     
        <td align="left" style="padding-left:3px">
     <asp:Label ID="Label3" runat="server" Text="Item Name" CssClass= "labelall"></asp:Label>
     </td>
     <td>     
      <asp:TextBox ID="txtitemname" runat="server" Width="458px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
     </td>  
     <td align="left">
   <%--  <asp:Button ID="btnclr" runat="server" Text="Clear" onclick="btnclr_Click" CssClass="btn" 
              Width="50px" TabIndex="1" UseSubmitBehavior="False" />--%>    
  <asp:ImageButton ID="btnclr" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnclr_Click" Height="20px"/>
     </td> 
      <td align="left">
  <%--   <asp:Button ID="btnadd" runat="server" Text="Search" onclick="btnadd_Click" CssClass="btn" 
             Width="50px" AccessKey="2" />--%>
  <asp:ImageButton ID="btnadd" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Search.png" onclick="btnadd_Click" Height="20px"/>
     </td>      
     </tr>     
     </table>
     </td>
     </tr>
     <tr>
     <td align="center">    
     <table width="700" cellpadding="0" cellspacing="0" border="0">
     <tr>
     <td align="left">
     <asp:GridView ID="itemsearch" runat="server" AutoGenerateColumns="False" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left" RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" AllowPaging="True" 
             onpageindexchanging="itemsearch_PageIndexChanging" 
             onsorting="itemsearch_Sorting" AllowSorting="True" 
             onrowdatabound="itemsearch_RowDataBound" 
             onrowcommand="itemsearch_RowCommand" CssClass="gridcss">  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
         <Columns>
         
                 <asp:ButtonField Text="Select" ButtonType="Image" CommandName="Btn" 
                     ImageUrl="~/ButtonImages/GridSelect.png">            
                  <ControlStyle Height="18px" Font-Size="X-Small" />
                  <HeaderStyle Height="18px" />
                  <ItemStyle Height="18px"/>
             </asp:ButtonField>
             <asp:BoundField DataField="Item_Code" HeaderText="Item Code" 
                 SortExpression="Item_Code" >
                 <HeaderStyle Width="110px"/>
             </asp:BoundField>
             <asp:BoundField DataField="Drug_Code" HeaderText="Drug Code"  
                 SortExpression="Drug_Code"/>
             <asp:BoundField DataField="Item_Name" HeaderText="Item Name" 
                 SortExpression="Item_Name" />
             <asp:BoundField DataField="iefrom" HeaderText="Item Effective Date From" 
                 SortExpression="iefrom" >
                 <HeaderStyle Width="80px"  />
             </asp:BoundField>
             <asp:BoundField DataField="ieto" HeaderText="Item Effective Date To" 
                 SortExpression="ieto" >
                 <HeaderStyle Width="80px" />
             </asp:BoundField>               
         </Columns>
         <FooterStyle BackColor="#507CD1" Font-Bold="False" ForeColor="#FF8000" />
    <PagerStyle BackColor="#169116" ForeColor="White" HorizontalAlign="Right" 
             VerticalAlign="Middle" />    
    <HeaderStyle BackColor="#169116" Font-Bold="True" ForeColor="White" 
         Wrap="True" HorizontalAlign="Center" />
    <EditRowStyle BackColor="#2461BF" ForeColor="#CC3300" />   
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