<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master"  AutoEventWireup="true" CodeFile="QueueStatusEnquiry.aspx.cs" Inherits="QueueStatusEnquiry" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="script" runat="server"></asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="cal/popcalendar.js" ></script>
     <script language="javascript" type="text/javascript">

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


             // Search Window open using Enter Key \\ 
             function doClick(e) {
                 var key;

                 if (window.event)
                     key = window.event.keyCode;     //IE
                 else
                     key = e.which;     //firefox

                 if (key == 13) {
                     document.getElementById('<%=btnsear.ClientID%>').click();
                     event.keyCode = 0
                 }
             }

    </script>

<asp:UpdatePanel ID="updenquery" runat="server">
<ContentTemplate>
<asp:Panel ID="pp" runat="server">
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<table align="center" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
<asp:Label ID="Label2" runat="server" Text="Queue Status Enquiry" CssClass="labelhead" ></asp:Label>
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
<asp:Label ID="pharloc" runat="server" CssClass="labelall" Text="Pharmacy Location" 
        Width="130px"></asp:Label>
</td>
<td colspan="3" align="left" style="margin-left: 40px">
<asp:DropDownList ID="ddlpharloc" runat="server" Width="260px" CssClass="textbox"></asp:DropDownList>
</td>
</tr>
<tr>
<td align="left">
<asp:Label ID="lblqueue" runat="server" CssClass="labelall" 
Text="Queue No " Width="75px"></asp:Label>
</td>
<td align="left" style="margin-left: 40px">
<asp:TextBox ID="txtqueuefst" runat="server" Width="150px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>    
</td>
<td align="left" style="padding-left:10px">
<asp:Label ID="lblcatno" runat="server" CssClass="labelall" 
Text="Status " Width="100px"></asp:Label>
</td>
<td align="left">
<asp:DropDownList ID="ddlstatus" runat="server" Width="286px" CssClass="textbox" >
<asp:ListItem>All</asp:ListItem>
<asp:ListItem>Pending DDS/BDS</asp:ListItem>
<asp:ListItem>DDS/BDS Work in progress</asp:ListItem>
<%--<asp:ListItem>Pending Assembly</asp:ListItem>--%>
<asp:ListItem>Assembly Completed</asp:ListItem>  
<asp:ListItem>Triger Orders</asp:ListItem>  
<asp:ListItem>Jump Queue Orders</asp:ListItem>  
<asp:ListItem>Diverted</asp:ListItem>  
</asp:DropDownList>              
</td>
</tr>   
<tr>
<td align="left">
<asp:Label ID="Label3" runat="server" Text="Patient ID" CssClass="labelall"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtpatientidfst" runat="server" Width="150px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td>
 <td align="left" style="padding-left:10px">
<asp:Label ID="Label4" runat="server" Text="Patient Name" 
        CssClass="labelall" Width="100px"></asp:Label>
</td>
<td align="left" >
<asp:TextBox ID="txtpatientnamefst" runat="server" Width="279px" CssClass="textbox" AutoCompleteType="Disabled"></asp:TextBox>
</td> 
</tr>
<tr>
<td align="left">
<asp:Label ID="Label6" runat="server" Text="Date From" CssClass="labelall" 
        Width="85px"></asp:Label>
 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
        ErrorMessage="*" ControlToValidate="txtdatefrom"        
        ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" 
        Display="Static" SetFocusOnError="True"></asp:RegularExpressionValidator>
</td>
<td align="left" >
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtdatefrom" runat="server" Width="115px" CssClass="textbox" 
        AutoCompleteType="Disabled" ></asp:TextBox> 
</td>
<td align="left" width="35px">
  <asp:image id="imgCalendar" runat="server" ImageUrl="~/cal/calendar.gif"></asp:image>
</td>
</tr>
</table>
</td>
<td align="left" style="padding-left:10px">
<asp:Label ID="Label7" runat="server" Text="Date To" CssClass="labelall" 
        Width="80px"></asp:Label>
<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
        ErrorMessage="*" ControlToValidate="txtdateto"        
        ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" 
        Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
</td>
<td align="left">
 <table cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="left">
<asp:TextBox ID="txtdateto" runat="server" Width="115px" CssClass="textbox" AutoCompleteType="Disabled" ></asp:TextBox> 
</td>
<td align="left" width="35px">
  <asp:image id="imgtwo" runat="server" ImageUrl="~/cal/calendar.gif"></asp:image>
</td>
<td>
<%--<asp:Button ID="btnclear" runat="server" Text="Clear" CssClass="btn" 
        onclick="btnclear_Click" UseSubmitBehavior="False"/>--%>
  <asp:ImageButton ID="btnclear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Clear.png" onclick="btnclear_Click" Height="20px"/>
</td>
<td align="left" >
<%--<asp:Button ID="btnsear" runat="server" Text="Search" onclick="btnsear_Click" CssClass="btn" />--%>
  <asp:ImageButton ID="btnsear" runat="server" CssClass="btn" 
            ImageUrl="~/ButtonImages/Search.png" onclick="btnsear_Click" Height="20px"/>
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
<table width="100%" cellpadding="0" cellspacing="0" border="0" id="fstgrd" runat="server">
<tr>
<td style="padding-top:10px;">
<asp:GridView ID="Queuestatusgrid" runat="server" AutoGenerateColumns="False" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True"         
        AllowPaging="True" 
        onselectedindexchanging="Queuestatusgrid_SelectedIndexChanging" 
        CssClass="gridcss" onpageindexchanging="Queuestatusgrid_PageIndexChanging" 
        onsorting="Queuestatusgrid_Sorting" AllowSorting="True" >  
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns>    
        <asp:CommandField SelectText="Details" ShowSelectButton="True" />
        <asp:BoundField HeaderText="Queue No" DataField="Queue_No" SortExpression="Queue_No" />
        <asp:BoundField HeaderText="Patient ID" DataField="PatientID" SortExpression="PatientID" />
        <asp:BoundField HeaderText="Patient Name" DataField="Patientname" SortExpression="Patientname" />
        <asp:BoundField HeaderText="Status" DataField="status" SortExpression="status" />
        <asp:BoundField HeaderText="Transaction Date" DataField="Transactiondate" SortExpression="Transactiondate" />
        <asp:BoundField DataField="ordertime" HeaderText="Order Time" SortExpression="ordertime" />
        <asp:BoundField DataField="Process_Time" HeaderText="Process Time" SortExpression="Process_Time" />
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
<td style="padding-left:300px">
<asp:Label ID="lblpage1" runat="server" CssClass="labelall"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left" style="padding-top:15px">

<table cellpadding="0" cellspacing="0" border="0" id="details" runat="server">
<tr>
<td align="left">
<asp:Label ID="lblqueueno" runat="server" Text="Queue No" CssClass="labelall" Width="130"></asp:Label>
</td>


<td align="left">
<asp:TextBox ID="txtqueueno" runat="server" Width="115px" CssClass="textbox" 
        AutoCompleteType="Disabled" ReadOnly="True"></asp:TextBox>
</td>
<td align="left" style="padding-left:10px">
<asp:Label ID="lblpatientid" runat="server" Text="Patient ID" CssClass="labelall" Width="70"></asp:Label>
</td>
<td align="left" >
<asp:TextBox ID="txtpatientid" runat="server" Width="150px" CssClass="textbox" 
        AutoCompleteType="Disabled" ReadOnly="True" ></asp:TextBox>
</td>




</tr>
<%--<tr>
<td align="left">
<asp:Label ID="lblpatientid" runat="server" Text="Patient ID" CssClass="labelall"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtpatientid" runat="server" Width="150px" CssClass="textbox" 
        AutoCompleteType="Disabled" ></asp:TextBox>
</td>
</tr>--%>
<tr>
<td align="left" >
<asp:Label ID="lbltrndt" runat="server" CssClass="labelall" Text="Transaction Date" 
        Width="100px"></asp:Label>
</td>

<td align="left" >
<asp:TextBox ID="txttrandt" runat="server" Width="115px" CssClass="textbox" 
        AutoCompleteType="Disabled" ReadOnly="True" ></asp:TextBox>
</td>
<td align="left" style="padding-left:10px">
<asp:Label ID="lblpatientname" runat="server" Text="Patient Name" 
        CssClass="labelall" Width="85px"></asp:Label>
</td>
<td align="left">
<asp:TextBox ID="txtpatientname" runat="server" Width="380px" CssClass="textbox" 
        AutoCompleteType="Disabled" ReadOnly="True"></asp:TextBox>
</td>
</tr>
</table>

</td>
</tr>
<tr>
<td align="left">
<table width="100%" cellpadding="0" cellspacing="0" border="0" id="Detailgrid" runat="server">
<tr>
<td style="padding-top:10px;">
<asp:GridView ID="qeudetailgrid" runat="server" AutoGenerateColumns="False" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        AllowPaging="True" CssClass="gridcss" 
        onpageindexchanging="qeudetailgrid_PageIndexChanging" 
        onsorting="qeudetailgrid_Sorting" AllowSorting="True" PageSize="15"> 
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns>    
        <asp:BoundField DataField="Item_Code" HeaderText="Item Code" SortExpression="Item_Code" />
        <asp:BoundField DataField="Item_Name" HeaderText="Item Name" SortExpression="Item_Name" />
        <asp:BoundField DataField="Order_Qty" HeaderText="Order Qty" SortExpression="Order_Qty" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="UOM" HeaderText="UOM" SortExpression="UOM" />    
        <asp:BoundField DataField="DDS_Name" HeaderText="DDS / BDS Name" SortExpression="DDS_Name" />
        <asp:BoundField DataField="cell_location" HeaderText="Cell No" SortExpression="cell_location" />
        <asp:BoundField DataField="cartridge_no" HeaderText="Cartridge No" SortExpression="cartridge_no" />        
        <asp:BoundField DataField="pickedqty" HeaderText="Picked Qty" SortExpression="pickedqty" >
           <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="PackType" HeaderText="Packtype" SortExpression="Packtype" />
        <asp:BoundField DataField="PackSize" HeaderText="PackSize" SortExpression="PackSize" >
           <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="RFID_No" HeaderText="RFID_No" SortExpression="RFID_No" />
        <asp:BoundField DataField="Container_No" HeaderText="ContNo" SortExpression="Container_No" >
           <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Ind" HeaderText="Ind" SortExpression="Ind" >
           <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
        <asp:BoundField DataField="Intervention" HeaderText="Remarks" SortExpression="Intervention" />
        <asp:BoundField DataField="ReProcessed" HeaderText="ReProcessed" SortExpression="ReProcessed" />
        <asp:BoundField DataField="OrderTime" HeaderText="Ordered Time" SortExpression="OrderTime" />
        <asp:BoundField DataField="Opasreceivedtime" HeaderText="OPAS Received Time" SortExpression="Opasreceivedtime">
        <ItemStyle Width="4px" />
        </asp:BoundField>
        <asp:BoundField DataField="OpasProcessingtime" HeaderText="OPAS Processed Time" 
            SortExpression="OpasProcessingtime" >
        <ItemStyle Width="4px" />
        </asp:BoundField>
         <asp:BoundField DataField="MC_Order_Received_Dt" HeaderText="Machine Received Time" 
            SortExpression="MC_Order_Received_Dt" >
        <ItemStyle Width="4px" />
        </asp:BoundField>
        <asp:BoundField DataField="Packedtime" HeaderText="Packed Time" SortExpression="Packedtime" />
       <%-- <asp:BoundField DataField="Assembledtime" HeaderText="Assembled Time" SortExpression="Assembledtime" />--%>
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
<tr>
<td align="left">
<table width="100%" cellpadding="0" cellspacing="0" border="0" id="Nodds" runat="server">

<tr>
<td align="left">
<asp:GridView ID="invalidfstgrd" runat="server" AutoGenerateColumns="False" 
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        CssClass="gridcss" onpageindexchanging="invalidfstgrd_PageIndexChanging" 
        onselectedindexchanging="invalidfstgrd_SelectedIndexChanging" 
        onsorting="invalidfstgrd_Sorting" AllowSorting="True" AllowPaging="True" > 
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns>   
     <asp:TemplateField Visible="False">
        <HeaderTemplate>
        <input id ="chkheaders" onclick="SelectAllCheckboxes(this);" runat="server" type="checkbox" />
        </HeaderTemplate>
         <ItemTemplate>
         <asp:CheckBox ID="chkrow" runat="server" />         
         </ItemTemplate>           
         <ItemStyle HorizontalAlign="Center" />
         </asp:TemplateField> 
          <asp:CommandField SelectText="Details" ShowSelectButton="True" />
          <asp:BoundField DataField="Queue_No" HeaderText="Queue No" SortExpression="Queue_No" />  
          <asp:BoundField DataField="PatientID" HeaderText="PatientID" SortExpression="PatientID" />               
         <asp:BoundField DataField="Pre_Allocation" HeaderText="Status" 
            SortExpression="Pre_Allocation" />    
         <asp:BoundField DataField="Ordertime" HeaderText="Order Time" 
            SortExpression="Ordertime" >      
        <ItemStyle Width="100px" />
        </asp:BoundField>
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
<asp:Label ID="lblpage4" runat="server" CssClass="labelall"></asp:Label>
</td>
</tr>
<tr>
<td align="left">
<asp:GridView ID="Noddsgrd" runat="server" AutoGenerateColumns="False"  
        Width="100%" CellPadding="0" ForeColor="#336600" 
        HorizontalAlign="Left" BackColor="#FFFFCC" CaptionAlign="Left"  RowStyle-HorizontalAlign="Left" 
        RowStyle-VerticalAlign="Middle" EnableModelValidation="True" 
        AllowPaging="True" CssClass="gridcss" 
        onpageindexchanging="Noddsgrd_PageIndexChanging" 
        onsorting="Noddsgrd_Sorting" AllowSorting="True" > 
        <RowStyle BackColor="#EFF3FB" Wrap="True" />
    <Columns>   
  <%--   <asp:TemplateField>
        <HeaderTemplate>
        <input id ="chkheader" onclick="SelectAllCheckboxes(this);" runat="server" type="checkbox" />
        </HeaderTemplate>
         <ItemTemplate>
         <asp:CheckBox ID="chkrow" runat="server" />         
         </ItemTemplate>           
         <ItemStyle HorizontalAlign="Center" />
         </asp:TemplateField> --%>
          <asp:BoundField DataField="Queue_No" HeaderText="Queue No" SortExpression="Queue_No" />
          <asp:BoundField DataField="PatientID" HeaderText="Patient ID" SortExpression="PatientID" />
        <asp:BoundField DataField="Item_Code" HeaderText="Item Code" SortExpression="Item_Code" />
        <asp:BoundField DataField="Drug_Name" HeaderText="Item Name" SortExpression="Drug_Name" />
        <asp:BoundField DataField="Sales_Quantity" HeaderText="Order Qty" SortExpression="Sales_Quantity" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>         
         <asp:BoundField DataField="Ind" HeaderText="Indicator" SortExpression="Ind" >
         <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Pre_Allocation" HeaderText="Remarks" 
            SortExpression="Pre_Allocation" />        
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
<asp:Label ID="lblpage3" runat="server" CssClass="labelall"></asp:Label>
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