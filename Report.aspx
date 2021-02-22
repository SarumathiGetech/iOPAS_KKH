<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script language="JavaScript" type="text/jscript">
        function closepopup(servername) {
            //loadornot();
            document.getElementById('<%=btnsearch.ClientID%>').click();
            window.open(servername, '', '')    
            //window.open('http://adgetech/Reports', '', '')     
                    
          // window.open('http://adgetech/Reports', '', 'fullscreen')
            //window.open('http://getech:8080/Reports')
            
        }
        
               

//        var popurls = new Array()
//        popurls[0] = "http://adgetech/Reports"
//        popurls[1] = "http://adgetech/Reports"
//        popurls[2] = "http://adgetech/Reports"
//        popurls[3] = "http://adgetech/Reports"

//        function openpopup(popurl) {
//            var winpops = window.open(popurl, "", "width=,height=,toolbar,location,status,scrollbars,menubar,resizable")
//        }

//        function get_cookie(Name) {
//            var search = Name + "="
//            var returnvalue = "";
//            if (document.cookie.length > 0) {
//                offset = document.cookie.indexOf(search)
//                if (offset != -1) { // if cookie exists
//                    offset += search.length
//                    // set index of beginning of value
//                    end = document.cookie.indexOf(";", offset);
//                    // set index of end of cookie value
//                    if (end == -1)
//                        end = document.cookie.length;
//                    returnvalue = unescape(document.cookie.substring(offset, end))
//                }
//            }
//            return returnvalue;
//        }

//        function loadornot() {
//            if (get_cookie('jkpopup') == '') {
//                openpopup(popurls[Math.floor(Math.random() * (popurls.length))])
//                document.cookie = "jkpopup=yes"
//            }
//        }

       // loadornot()

       
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table width="100%" cellpadding="0" cellspacing="0" border="0">
    <tr>
    <td align="center" style=" height: 70px;">
        <asp:ImageMap ID="ImageMap1" runat="server" ImageUrl="~/Image/OPASBanner.gif" 
            Height="70px" Width="929px">
        </asp:ImageMap>
        </td>         
        </tr>
        <tr>      
        <td align="left" style="background-color: #FFCC66;" >
        <table cellpadding="0" cellspacing="0" border="0">
        <tr>       
        <td align="left" style="padding-left:200px">
        <asp:Label ID="lblpharloc" Text="Pharmacy Location" runat="server"></asp:Label>
        </td>
        <td align="left" style="padding-left:2px">
        <asp:DropDownList ID="DropDownList1" runat="server" Width="300px" 
               Height="19px"></asp:DropDownList>
        </td>        
        <td align="left" style="padding-left:60px">
        <asp:HyperLink ID="home" runat="server" Text="Home" NavigateUrl="~/Home.aspx"></asp:HyperLink>
        </td>
        <%-- <td>
        <asp:Button ID="btnss" runat="server" Text="ass" onclick="btnss_Click" />
        </td>--%>
        </tr>
        </table>        
        </td>
        <td>
         <asp:Button ID="btnsearch" runat="server" onclick="btnsearch_Click" Text="Button" style="position:static; display:none;" />
        </td>
       
        </tr>
        </table> 
    </div>
    </form>
</body>
</html>
