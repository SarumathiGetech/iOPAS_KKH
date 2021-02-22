using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Diagnostics;
using Datalayer;

public partial class Login : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
  SqlQuery qry = new SqlQuery();
  LogWriter log = new LogWriter();
  string DC1 = "", DC2 = "", DC3 = "", CN = "", domain = "", hostnme = "", LoginSessionCheck, AppserverName = "", HasDNS="";
  protected void Page_Load(object sender, EventArgs e)
  {
      try
      {
          Txtuname.Focus();
          LoginSessionCheck = Request.QueryString["id"];

          if (LoginSessionCheck == null)
          {
              Response.Redirect("iopas.html");
          }

            LoginSessionCheck = "";
            Session["promptcount"] = 0;
            TxtPass.Attributes.Add("onKeyPress", "doClick(event)");
            Btnlogin.Attributes.Add("onclick", "doClick1();");
            HasDNS = System.Configuration.ConfigurationManager.AppSettings["HasDNS"];
            AppserverName = System.Configuration.ConfigurationManager.AppSettings["AppServer"];
            try
            {
                //string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_ADDR"]).HostName.Split(new Char[] { '.' });          
                //hostnme = computer_name[0].ToString();
                // Session["HST"] = hostnme;
                if (!IsPostBack)
                {
                    lblPharmacyLocation.Visible = false;
                    dllPharmacyLocation.Visible = false;
                    dllPharmacyLocation.SelectedIndex = 0;
                    if (HasDNS.ToUpper() == "YES".ToUpper())
                    {

                        String clientIP = GetClientIPAddress(this);
                        string[] computer_name = System.Net.Dns.GetHostEntry(clientIP).HostName.Split(new Char[] { '.' });
                        hostnme = computer_name[0].ToString();
                        Session["HST"] = hostnme;
                        Session["App"] = AppserverName;
                        lblapp.Text = AppserverName;
                    }
                    else
                    {
                        GetPharmacyLocation();
                        lblPharmacyLocation.Visible = true;
                        dllPharmacyLocation.Visible = true;
                        hostnme = dllPharmacyLocation.Text;
                        Session["HST"] = hostnme;
                        Session["App"] = AppserverName;
                        lblapp.Text = AppserverName;
                    }
                }

            }

            catch (Exception ex)
            {
                Session["HST"] = "NIL";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please restart your PC, Because client host name is " + hostnme + "');</script>", false);
            }
        }
        catch (Exception exc)
        {
            log.logwriter("iOPAS", exc.Message);
        }
    }

    public void GetPharmacyLocation()
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = DBCon.getstring())
            {
                string query = "SELECT Location_Name FROM Pharmacy where Status = 'Active'";
                cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dllPharmacyLocation.DataSource = dt;
                dllPharmacyLocation.DataTextField = "Location_Name";
                dllPharmacyLocation.DataValueField = "Location_Name";
                dllPharmacyLocation.DataBind();
            }
        }
        catch(Exception ex)
        {
            log.logwriter("iOPAS", ex.Message);
        }
    }

  

    public static string GetClientIPAddress(System.Web.UI.Page page)
    {

        string szRemoteAddr = page.Request.ServerVariables["remote_ADDR"];

        string szXForwardedFor = page.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        string clientIP = "";

        if (szXForwardedFor == null || szXForwardedFor.Length <= 0)
        {
            clientIP = szRemoteAddr;
        }
        else
        {
            if (szXForwardedFor.IndexOf(",") > 0)
            {
                string[] arIPs = szXForwardedFor.Split(',');
                clientIP = arIPs[0];
            }
            else
            {
                clientIP = szXForwardedFor;
            }
        }

        return clientIP;
    }
    
    protected void Btnlogin_Click(object sender, ImageClickEventArgs e)
    {
     
        int Rtnvalue = qry.loginchk(Txtuname.Text.Trim().ToLower(), TxtPass.Text);
        if (Rtnvalue == 11)
        {       
            Session["Userid"] = Txtuname.Text.ToLower().Trim();
            Session["FSTLogin"] = "YES";
            if (HasDNS.ToUpper() == "NO".ToUpper())
            {
                Session["HST"] = dllPharmacyLocation.Text;
                hostnme = Session["HST"].ToString();
            }
            qry.logindate(Txtuname.Text.Trim().ToLower(), hostnme);
            Response.Redirect("Home.aspx?usname=" + Txtuname.Text.ToLower().Trim());
        }
        else if (Rtnvalue == 12)
        {
            Session["FSTLogin"] = "YES";
            Session["Userid"] = Txtuname.Text.ToLower().Trim();

            if (HasDNS.ToUpper() == "NO".ToUpper())
            {
                Session["HST"] = dllPharmacyLocation.Text;
            }
            //Response.Redirect("Changepwd.aspx?pwd=" + TxtPass.Text);
            Response.Redirect("Changepwd.aspx");
        }
        else if (Rtnvalue == 2)
        {
            ADdomain();
            mscode();
        }
        else if (Rtnvalue == 3)
        {
            lblloading.Text = "";
            lblmsg.Visible = true;
            lblmsg.Text = "Your account has been Inactivated. Contact System Administrator";
        }
        else if (Rtnvalue == 4)
        {
            lblloading.Text = "";
            lblmsg.Visible = true;
            lblmsg.Text = "Your domain has been Inactivated. Contact System Administrator";
        }
        else if (Rtnvalue == 5)
        {
            lblloading.Text = "";
            lblmsg.Visible = true;
            lblmsg.Text = "Incorrect password.Password is case sensitive.";
        }
        else if (Rtnvalue == 0)
        {
            lblloading.Text = "";
            lblmsg.Visible = true;
            lblmsg.Text = "Incorrect user name";
        }
        else if (Rtnvalue == 6)
        {
            lblloading.Text = "";
            lblmsg.Visible = true;
            lblmsg.Text = "Your account has expired. Contact System Administrator";
        }
        else if (Rtnvalue == 7)
        {
            lblloading.Text = "";
            lblmsg.Visible = true;
            lblmsg.Text = "Your account has been locked. Contact System Administrator";
        }
        else if (Rtnvalue == 8)
        {
            lblloading.Text = "";
            lblmsg.Visible = true;
            lblmsg.Text = "Your password has expired. Contact System Administrator";
        }
    }
    
// * Pre Login Check For domain Name And Account status * \\
    public void ADdomain()
    {
        using (SqlConnection  con = DBCon.getstring())
        {
            string Commt = "select d.DomainName,d.DC1,d.DC2,d.DC3,d.CN from Domain as d left join User_tbl as u on u.Domain= d.DomainID where u.UserId='" + Txtuname.Text.ToLower().Trim() + "'";  
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        domain = dr[0].ToString();
                        DC1 = dr[1].ToString();
                        DC2 = dr[2].ToString();
                        DC3 = dr[3].ToString();
                        CN = dr[4].ToString();
                    }                    
                }
            }
        }
    }

// * Domain User Login Function * \\
    // * Reference * \\
    
//----- ---------------------------------------------------------------------------------------------------------- \\
    //LDAP://company.com.au/DC=company,DC=com,DC=au"/>
    //  LDAP://dept.company.com.au/CN=dept,DC=company,DC=com,DC=au"/>

//----- ---------------------------------------------------------------------------------------------------------- \\
    //domainname =(domainname+"." +"com.sg");
    //string adPath = "LDAP://Getechopas.com.sg/DC=Getechopas,DC=com,DC=sg";
    //adPath = "LDAP://" + domainname + "/" + "DC=" + domain + ","+"DC=" + "com"+","+"DC="+"sg";
//----- ---------------------------------------------------------------------------------------------------------- \\

    public void mscode()
    {
        Session["Userid"] = Txtuname.Text.ToLower().Trim();

        string adPath = "";

        if (CN == "")
        {
            adPath = "LDAP://" + DC1 + "/" + "DC=" + domain + "," + "DC=" + DC2 + "," + "DC=" + DC3;           
        }
        else if (CN != "")
        {
            adPath = "LDAP://" + DC1 + "/" + "DC=" +domain + "," + "DC=" + CN + "," + "DC=" + DC2 + "," + "DC=" + DC3;
        }

        LdapAuthentication adAuth = new LdapAuthentication(adPath);

        try
        {
            if (true == adAuth.IsAuthenticated(domain, Txtuname.Text.ToLower(), TxtPass.Text))
            {
                // Retrieve the user's groups
                // string groups = adAuth.GetGroups();
                // FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, Txtuname.Text, DateTime.Now, DateTime.Now.AddMilliseconds(10), false, groups);
                // string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                // HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                //  if (true == authTicket.IsPersistent)
                // authCookie.Expires = authTicket.Expiration;
                // Response.Cookies.Add(authCookie);
                //Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUserName.Text, false));

                Session["FSTLogin"] = "YES";

                if (HasDNS.ToUpper() == "NO".ToUpper())
                {
                    Session["HST"] = dllPharmacyLocation.Text;
                    hostnme = Session["HST"].ToString();
                }

                qry.logindate(Txtuname.Text.Trim().ToLower(), hostnme);
                Response.Redirect("Home.aspx");
                Response.Cookies.Clear();
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Authentication failed, check username and password.";
            }           
        }
        catch (Exception ex)
        {
            lblmsg.Visible = true;
            lblmsg.Text = ex.Message;
            if (lblmsg.Text.Trim() == "Logon failure: unknown user name or bad password.")
            {
                lblmsg.Text = "Incorrect password.Password is case sensitive.";
            }
            else
            {
                lblmsg.Text = ex.Message;
            }
        }
    }

    public System.Web.UI.Page page { get; set; }

}
