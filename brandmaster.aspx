<%@ Page Language="C#" AutoEventWireup="true" CodeFile="brandmaster.aspx.cs" Inherits="brandmaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>iOPAS</title>
    <link type="text/css" rel="stylesheet" href="css/cal.css" />  
    <script language="JavaScript" type="text/jscript">
        function closepopup() {
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnok').click();
            window.open('brandmaster.aspx', 'search', 'menubar=no,center:yes,scrollbars=no,width=720,height=500[color=blue]40,top=100,left=200')
            window.close();
        }
          function closer() {
              //alert("This window is about to close.");
              window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnok').click();
        }       
</script>
</head>
<body onbeforeunload="closer()" style="background-color:#D2FECF">
    <form id="form1" runat="server" style="background-color:#D2FECF">
     <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:UpdatePanel ID="updpanel2" runat="server">
     <ContentTemplate>
     <table width="100%" cellpadding="0" cellspacing="0" border="0">
     <tr>
     <td align="left">     
     <table width="100%" cellpadding="0" cellspacing="0" border="0">
     <tr>
     <td align="center" colspan="4">
     <asp:Label ID="lblhead" runat="server" Text="Brand Master" CssClass="labelhead" ></asp:Label>
     </td>
     </tr>    
     <tr>
     <td align="left">
     <asp:Label ID="Label1" runat="server" Text="Item Code" CssClass= "labelall"></asp:Label>
     </td>
     <td>     
      <asp:TextBox ID="txtitemcode" runat="server" Width="150px" ReadOnly="True" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
     </td>
     <td align="left">
     <asp:Label ID="lblitemname" runat="server" Text="Item Name" CssClass="labelall"></asp:Label>
     </td>
     <td align="left">
     <asp:TextBox ID="txtitemname" runat="server" Width="400px" ReadOnly="True" 
             CssClass="textbox" AutoCompleteType="Disabled" ></asp:TextBox>     
     </td>       
     </tr> 
     <tr>
     <td align="left">
     <asp:Label ID="lblbrandcode" runat="server" Text="Brand Code" CssClass="labelall"></asp:Label>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
             ControlToValidate="txtbrandcode" ErrorMessage="*" SetFocusOnError="True" 
             ValidationGroup="brand"></asp:RequiredFieldValidator>
     </td>
     <td align="left">
     <asp:TextBox ID="txtbrandcode" runat="server" Width="150" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
     </td>
    <td align="left">
     <asp:Label ID="Label2" runat="server" Text="Brand Name" CssClass= "labelall"></asp:Label>
     
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="txtbrandname" ErrorMessage="*" SetFocusOnError="True" 
            ValidationGroup="brand"></asp:RequiredFieldValidator>
     
     </td>
     <td>     
      <asp:TextBox ID="txtbrandname" runat="server" Width="400px" CssClass="textbox" 
             AutoCompleteType="Disabled"></asp:TextBox>
     </td> 
     </tr>
     <tr>     
        <td align="left">
     <asp:Label ID="Label3" runat="server" Text="MFR Barcode" CssClass= "labelall"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtmfrcode" ErrorMessage="*" SetFocusOnError="True" 
                ValidationGroup="mfr"></asp:RequiredFieldValidator>
     </td>
     <td>     
      <asp:TextBox ID="txtmfrcode" runat="server" Width="150px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
     </td>  
     <td>
    <%-- <asp:Button ID="btnadd" runat="server" Text="Add" onclick="btnadd_Click" CssClass="btn" 
              Width="60px" ValidationGroup="mfr" />--%>
  <asp:ImageButton ID="btnadd" runat="server" CssClass="btn" ValidationGroup="mfr"
            ImageUrl="~/ButtonImages/MFRAdd.png" onclick="btnadd_Click" 
             Height="20px"  />
     </td> 
     <td>
     <table cellpadding="0" cellspacing="0" border="0">
     <tr>
     <td align="left">
     <asp:CheckBox ID="chkdefault" runat="server" Text="Default Brand" 
             TextAlign="Left" CssClass="labelall" />
     </td>
     <td align="left" style="padding-left:20px">
      <asp:CheckBox ID="chkactve" runat="server" Text="Active" 
             TextAlign="Left" CssClass="labelall" />
     </td>
     <td align="left" style="padding-left:25px">
  <asp:ImageButton ID="btnclear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnclear_Click" Height="20px"/>
     </td>
     <td align="left">

  <asp:ImageButton ID="btnsearch" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/BrandMaster.png" onclick="btnsearch_Click" 
             Height="20px"/>
     </td>
     </tr>
     </table>                  
     </td>     
     </tr> 
     <tr>
     <td>
          
     </td>               
     <td>
     <asp:ListBox ID="lstbox" runat="server" Width="154px" CssClass="textbox"></asp:ListBox>
      </td>     
     <td>
   <%--  <asp:Button ID="Button2" runat="server" Text="Remove" Width="60px" CssClass="btn" 
             onclick="Button2_Click" UseSubmitBehavior="False" />--%>
               <asp:ImageButton ID="Button2" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/MFRRemove.png" onclick="Button2_Click" 
             Height="20px" />
             <br />
   <%--  <asp:Button ID="btnsave" runat="server" Text="Save" Width="60px" CssClass="btn" 
             onclick="btnsave_Click" ValidationGroup="brand" UseSubmitBehavior="False"/> --%>
                <asp:ImageButton ID="btnsave" runat="server" CssClass="btn" ValidationGroup="brand"
            ImageUrl="~/ButtonImages/Save.png" onclick="btnsave_Click" Height="20px" />

             <asp:ImageButton ID="btnupdate" runat="server" CssClass="btn" ValidationGroup="brand"
            ImageUrl="~/ButtonImages/Update.png" Height="20px"  onclick="btnupdate_Click" />
<%--
             <asp:Button ID="btnbrandupd" runat="server" Text="Save" Width="60px" CssClass="btn" 
             ValidationGroup="brand" UseSubmitBehavior="False" 
             onclick="btnbrandupd_Click" BackColor="#169116" BorderStyle="None" 
             ForeColor="White"/> --%>

                    <asp:ImageButton ID="btnbrandupd" runat="server" CssClass="btn" ValidationGroup="brand"
            ImageUrl="~/ButtonImages/Save.png" Height="20px"  onclick="btnbrandupd_Click" />
          
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
     <asp:GridView ID="gridbrand" runat="server" AutoGenerateColumns="False" DataKeyNames="BrandID" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left" RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" AllowPaging="True" 
             onpageindexchanging="gridbrand_PageIndexChanging" 
             onselectedindexchanging="gridbrand_SelectedIndexChanging" 
             CssClass="gridcss" onselectedindexchanged="gridbrand_SelectedIndexChanged" 
             onsorting="gridbrand_Sorting" AllowSorting="True">  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
         <Columns>
             <asp:CommandField SelectText="Edit" ShowSelectButton="True" 
                 HeaderText="MFR Code/Active /Inactive" >
                <%-- <HeaderStyle Width="40px" />--%>
                 <ItemStyle Width="40px" />
             </asp:CommandField>
             <asp:BoundField DataField="BrandID" Visible="false"/>
                 <%--<ItemStyle Font-Size="XX-Small" ForeColor="White" Width="10px" />
             </asp:BoundField>--%>
             <asp:BoundField DataField="Item_Code" HeaderText="Item Code" SortExpression="Item_Code" >
               <%--  <HeaderStyle Width="110px" />--%>
             </asp:BoundField>
             <asp:BoundField DataField="brandcode" HeaderText="Brand Code" SortExpression="brandcode" >
                <%-- <HeaderStyle Width="111px" />--%>
                 <ItemStyle Wrap="True" />
             </asp:BoundField>
             <asp:BoundField DataField="brandname" HeaderText="Brand Name" SortExpression="brandname">
           <%--  <HeaderStyle Width="200px" />--%>
              <ItemStyle Wrap="True" />
             </asp:BoundField>
             <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" >
                 <%--<HeaderStyle Width="110px" />--%>
             </asp:BoundField>
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
     <td align="center">
     <asp:Label ID="lblpge" runat="server" CssClass="labelall"></asp:Label>
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
     <asp:GridView ID="brandgrid" runat="server" AutoGenerateColumns="False" DataKeyNames="Brandid" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" AllowPaging="True"              
             onselectedindexchanging="brandgrid_SelectedIndexChanging"              
             onrowcommand="brandgrid_RowCommand" 
             onpageindexchanging="brandgrid_PageIndexChanging" CssClass="gridcss" 
             onsorting="brandgrid_Sorting" AllowSorting="True">  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
         
         <Columns> 
            
              <asp:ButtonField Text="Select" ButtonType="Image" CommandName="Btn" 
                  ImageUrl="~/ButtonImages/GridSelect.png" HeaderText="Apply to New Code">
            
                  <ControlStyle Height="20px" />
                  <HeaderStyle Height="18px" Width="45px" />
                  <ItemStyle Height="18px" Width="45px" />
             </asp:ButtonField>
            
              <asp:CommandField SelectText="Edit" ShowSelectButton="True" 
                  HeaderText="Brand Master Edit" >
                 <ItemStyle Width="40px" />
             </asp:CommandField>
             <asp:BoundField DataField="Brandid" Visible="false"/>
              <%--<ItemStyle Font-Size="XX-Small" ForeColor="White" Width="10px" />              
               </asp:BoundField>--%>
             <asp:BoundField DataField="brandcode" HeaderText="Brand Code" SortExpression="brandcode" >
                 <ItemStyle Wrap="True" />
             </asp:BoundField>
             <asp:BoundField DataField="brandname" HeaderText="Brand Name" SortExpression="brandname">
              <ItemStyle Wrap="True" />
             </asp:BoundField>
             <asp:BoundField DataField="Created_by" HeaderText="Created by" SortExpression="Created_by" >
             </asp:BoundField>
             <asp:BoundField DataField="Createddate" HeaderText="Created Date Time" SortExpression="Createddate" />
             <asp:BoundField DataField="Updated_by" HeaderText="Updated by" SortExpression="Updated_by" />
             <asp:BoundField DataField="updateddate" HeaderText="Updated Date Time" SortExpression="updateddate" />
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
     <td align="center">
     <asp:Label ID="lblbrandpage" runat="server" CssClass="labelall"></asp:Label>
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