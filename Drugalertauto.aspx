<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Drugalertauto.aspx.cs" Inherits="Drugalertauto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>iOPAS</title>   
    <link type="text/css" rel="Stylesheet" href="CSS/cal.css" /> 
    <%--  <meta http-equiv="Refresh" content="5" />   --%>
     <script type="text/javascript" src="JS/masterpage.js">     
</script>
    <script type="text/javascript">      
        javascript: window.history.forward(1);     
        var countDownInterval = 58;         
         var countDownTime = countDownInterval + 1;
         function countDown() 
            {                
                countDownTime--;
                if (countDownTime <= 0) 
                {
                    countDownTime = countDownInterval;
                    clearTimeout(counter)
                    window.location.reload()
                    return
                }                           
                if (document.all)
                    document.all.countdowntext.innerText = countDownTime + " ";                                         
                    counter = setTimeout("countDown()", 1000);
                }


        var timeout = '<%= Session.Timeout%>' * 60000;
        var timer = setInterval(function () 
        {
            timeout -= 1000;
            window.status = "Your Session will timeout in " + time(timeout) + " minutes.";           
            if (timeout == 0)
             {
                clearInterval(timer);
                alert('Your Session has expired. You will be redirected to the login page.');
                window.location.href = 'Opas.html';
                       
             }
        }, 1000);

        function two(x)
        {
            return ((x > 9) ? "" : "0") + x
        }

        function time(ms)
         {
            var t = '';
            var sec = Math.floor(ms / 1000);
            ms = ms % 1000

            var min = Math.floor(sec / 60);
            sec = sec % 60;
            t = two(sec);

            var hr = Math.floor(min / 120);
            min = min % 120;
            t = two(min);
           // t = two(min) + ":" + t;
            return t;
        }   
                                                   
</script>    
</head>
<body>
    <form id="form1" runat="server" style="background-color:#D2FECF">
    <div>
       
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:UpdatePanel ID="updpanel1" runat="server">
        <ContentTemplate>    
     
     <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color: #59DD63;">
    <tr>
 <%--   <td align="center" style=" height: 70px;">
        <asp:ImageMap ID="ImageMap1" runat="server" ImageUrl="~/Image/iOPASBanner.gif" 
            Height="70px" Width="100%">
        </asp:ImageMap>
        </td>  --%>   
        
        <td>
   <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color: #59DD63;">
   <tr>
 <td align="left" valign="top" style="height: 70px; width:265px;">
          <asp:ImageMap ID="ImageMap1" runat="server" ImageUrl="~/Image/banner1.gif" 
            Height="70px" Width="265px">
        </asp:ImageMap>
        </td>   

       <td align="left" valign="top" style=" height: 70px;">
          <asp:ImageMap ID="ImageMap2" runat="server" ImageUrl="~/Image/banner2.gif" 
            Height="70px" Width="100%">
        </asp:ImageMap>
        </td>  
          <td align="left" valign="top" style=" height: 70px;width:430px;">
          <asp:ImageMap ID="ImageMap3" runat="server" ImageUrl="~/Image/banner3.gif" 
            Height="70px" Width="430px">
        </asp:ImageMap>
        </td>  
            <td align="left" valign="top" style=" height: 70px">
          <asp:ImageMap ID="ImageMap4" runat="server" ImageUrl="~/Image/banner4.gif" 
            Height="70px" Width="100%">
        </asp:ImageMap>
        </td> 
            <td align="right" valign="top" style=" height: 70px;width:265px;">
          <asp:ImageMap ID="ImageMap5" runat="server" ImageUrl="~/Image/banner5.gif" 
            Height="70px" Width="265px">
        </asp:ImageMap>
        </td> 
   </tr>
   </table> 
   </td>    
        </tr>
        <tr>      
        <td align="left" style="background-color: #009933;" >
        <table  cellpadding="0" cellspacing="0" border="0">
        <tr>
          <td align="left">
        <asp:Label ID="Label1" Text="User Login :" runat="server" 
                CssClass="label" ForeColor="White" ></asp:Label>
        </td>
        <td align="left">
         <asp:Label ID="user" runat="server" ForeColor="White" 
                Font-Bold="True" Width="200px" Font-Names="Arial" Font-Size="10pt"></asp:Label> 
        </td>       
        <td align="left" style="padding-left:10px">
        <asp:Label ID="lblpharloc" Text="Pharmacy Location" runat="server" 
                CssClass="label" ForeColor="White" ></asp:Label>
        </td>
        <td align="left" style="padding-left:2px">
        <%--<asp:DropDownList ID="DropDownList1" runat="server" Width="280px" 
               Height="19px"></asp:DropDownList>--%>
         <asp:TextBox ID="txtlocation" runat="server" Width="280px"></asp:TextBox>      
        </td>        
       <td align="left" style="padding-left:10px">
        <%--<asp:HyperLink ID="HyperLink1" runat="server" Text="Change Password" NavigateUrl="~/Changepassword.aspx"></asp:HyperLink>--%>
        <asp:HyperLink ID="home" runat="server" Text="Home" NavigateUrl="~/Home.aspx" 
                Font-Names="Arial" Font-Size="10pt" ForeColor="White"></asp:HyperLink>
        </td>
        <td align="left" style="padding-left:15px">
        <%--<asp:HyperLink ID="home" runat="server" Text="Home" NavigateUrl="~/Home.aspx"></asp:HyperLink>--%>       
        <asp:HyperLink ID="HyperLink1" runat="server" Text="Change Password" 
                NavigateUrl="~/Changepassword.aspx" Font-Names="Arial" Font-Size="10pt" 
                ForeColor="White"></asp:HyperLink>
        </td>        
        <td align="left" style="padding-left:15px">
        <asp:LinkButton ID="lnklogout" runat="server" Text="Log Off" 
                onclick="lnklogout_Click" Font-Names="Arial" Font-Size="10pt" 
                ForeColor="White"></asp:LinkButton>
        </td>
        </tr>
        </table>        
        </td>
        </tr>
        </table>        
     </ContentTemplate>
     </asp:UpdatePanel>
     <asp:UpdatePanel ID="updpanel2" runat="server">
     <ContentTemplate>   
     <asp:Panel ID="pp" runat="server">  
     <table align="center" cellpadding="0" cellspacing="0" border="0">    
     <tr>      
     <td align="left" width="250" 
             style="padding-left:60px; font-family: Arial; font-size: 13px;">     
     <script type="text/javascript">
         document.write('Refreshing in <b id="countdowntext">' + countDownTime + '</b> seconds.     <a href="javascript:window.location.reload()"> Refresh </a> ');
          //document.write('Next<a href="javascript:window.location.reload()"> Refresh </a> in <b id="countDownText">' + countDownTime + '</b> seconds  ')
          countDown()      
     </script>     
     </td> 
      <td align="left" style="padding-left:188px">
      <asp:Label ID="lblorder" runat="server" ForeColor="Black" 
      Text="Last Order Received at" Font-Bold="False" Font-Names="Arial" 
              Font-Size="13pt" ></asp:Label>
      </td>       
      <td style="padding-left:3px">
      <asp:Label ID="lblresult" runat="server" ForeColor="Black" Font-Bold="True" 
      Font-Size="13pt" Font-Names="Arial"></asp:Label>
      </td>      
       <td align="left" style="padding-left:8px">
       <asp:Label ID="lblqueueno" runat="server" Text="Queue No" Font-Bold="False" 
               Font-Size="13pt" Font-Names="Arial"></asp:Label>     
       </td>    
       <td align="left" style="padding-left:3px">
       <%-- <asp:TextBox ID="txtqueueno" runat="server" Font-Bold="True" Font-Size="X-Large" 
             Width="60px" ReadOnly="True" Font-Names="Arial" ></asp:TextBox>--%>
    <asp:Label ID="txtqueueno" runat="server" ForeColor="Black" Font-Bold="True" 
      Font-Size="13pt" Font-Names="Arial"></asp:Label>
       </td>        
     </tr>              
     </table> 
     <table  align="center" cellpadding="0" cellspacing="0" border="0" style="padding-top:20px">
     <tr>
     <td width="600px"  align="left" style="padding-left:10px">
     <table cellpadding="0" cellspacing="0" border="0" >
     <tr>
     <td>     
     <asp:Label ID="lblprodown" runat="server" Text="iOPAS Processing Module Down Since" 
             Font-Bold="True" Font-Size="14pt" ForeColor="Red" Width="380px" 
             Font-Names="Arial"></asp:Label>
     </td>
     <td align="left">
     <asp:TextBox ID="txtprocesstime" runat="server" Font-Bold="True" Font-Size="Small" 
             Width="130px" ReadOnly="True" ></asp:TextBox>
     </td>
     </td>
     </tr>
      </table> 
     <td width="600px" align="left" style="padding-left:25px">
       <table cellpadding="0" cellspacing="0" border="0" >
     <tr>
     <td>
     <asp:Label ID="Label5" runat="server" Text="iOPAS DDS / BDS Communication Down Since" 
             Font-Bold="True" Font-Size="14pt" ForeColor="Red" Width="450px" 
             Font-Names="Arial"></asp:Label>
     </td>
      <td align="left">
     <asp:TextBox ID="txtddstime" runat="server" Font-Bold="True" Font-Size="Small" 
             Width="130px" ReadOnly="True" ></asp:TextBox>
     </td>
       </td>
     </tr>
      </table> 
     </tr>
     </table>      
     <table align="center" cellpadding="0" cellspacing="0" border="0" >
     <tr>
      <td width="600px">
       <asp:Panel ID="paneldds" runat="server" GroupingText=" " Height="920px" BackColor="#D2FECF">   
      <table  cellpadding="0" cellspacing="0" border="0">
      <tr>
      <td align="left">
       <asp:Label ID="dd" runat="server" Text="DDS / BDS Alert" CssClass="labelall" 
              Font-Bold="True" Font-Size="Larger" Width="200px" Font-Names="Arial"></asp:Label> 
      </td>
      <td align="left">
      <asp:TextBox ID="txtalet" runat="server" Width="50px" Font-Bold="True" 
             Font-Size="Larger" ReadOnly="True" ></asp:TextBox> 
      </td>
      </tr>
      </table>      
      <div style="overflow:scroll; height:870px; width:600px" >  
     <asp:GridView ID="ddsalertgrid" runat="server" AutoGenerateColumns="False" 
                      BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" 
                      CellPadding="1" EnableModelValidation="True" Width="580px" ForeColor="#336600" 
               CssClass="gridcss" PageSize="20">
                       <RowStyle BackColor="#EFF3FB" Wrap="True" />
         <Columns>
             <asp:BoundField DataField="DDSName" HeaderText="DDS / BDS Name" >
                 <ItemStyle Font-Bold="True" Font-Size="Large" />
             </asp:BoundField>
             <asp:BoundField DataField="Alertmsg" HeaderText="Alert Message" />
             <asp:BoundField DataField="Date" HeaderText="Date Time" />
         </Columns>
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                      <PagerSettings Mode="NextPrevious" />
                      <PagerStyle ForeColor="#169116" HorizontalAlign="Right" />
                      <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                      <HeaderStyle BackColor="#169116" Font-Bold="True" ForeColor="White" 
                          HorizontalAlign="Center" />
         </asp:GridView>  
            </div>            
        </asp:Panel>
     </td>     
          
    <td style="padding-left:30px;" width="600px">
      <asp:Panel ID="panel2" runat="server" GroupingText=" " Font-Bold="True" 
            Height="920px" BackColor="#D2FECF"> 
      <table cellpadding="0" cellspacing="0" border="0">
      <tr>
      <td align="left">
      <asp:Label ID="Label2" runat="server" Text=" Low Stock Alert" CssClass="labelall" 
              Font-Bold="True" Font-Size="Larger" Width="220px" Font-Names="Arial"></asp:Label>
      </td>
      <td align="right" >
      <asp:TextBox ID="txtlowsta" runat="server" Width="50px" Font-Bold="True" 
              Font-Size="Larger" ReadOnly="True"></asp:TextBox>
      </td>
      </tr>
      </table>    
      
<div style="overflow:scroll; height:870px; width:600px" >                      
      <asp:GridView ID="lowstockgrid" runat="server" AutoGenerateColumns="False" ForeColor="#336600" 
          BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" 
          CellPadding="2" Width="580px" CssClass="gridcss" PageSize="3">
          <RowStyle BackColor="#EFF3FB" Wrap="True" /> 
          <Columns>                                  
              <asp:HyperLinkField DataNavigateUrlFields="DDS_Name,Item_Name" DataNavigateUrlFormatString="~/Drugalretview.aspx?dds={0}&amp;Itemname={1}"
                DataTextField="DDS_Name" HeaderText="DDS / BDS Name" >
              <ControlStyle ForeColor="#336600" />
              <ItemStyle Font-Bold="True" Font-Size="Large" ForeColor="#336600"  />
              </asp:HyperLinkField>
          <asp:BoundField DataField="PackType" HeaderText="Pack Type" />
          <asp:BoundField DataField="Pack_Size" HeaderText="Pack Size" >
              <FooterStyle HorizontalAlign="Center" />
              <ItemStyle HorizontalAlign="Center" />
              </asp:BoundField>
          <asp:BoundField DataField="Item_Name" HeaderText="Item Name" />
          <asp:BoundField DataField="AvalQty" HeaderText="Aval Qty" >
              <ItemStyle HorizontalAlign="Center" />
              </asp:BoundField>
          </Columns>
          <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
          <PagerSettings Mode="NextPrevious" />
          <PagerStyle ForeColor="#169116" HorizontalAlign="Right" />
          <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
          <HeaderStyle BackColor="#169116" Font-Bold="True" ForeColor="White" 
              HorizontalAlign="Center" />
      </asp:GridView>
       </div> 
          </asp:Panel>
      </td>        
     
     </tr>
     </table>
       </asp:Panel> 
     </ContentTemplate>
     </asp:UpdatePanel>
   
    </div>
    </form>
</body>
</html>
