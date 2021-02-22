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
using Datalayer;

public partial class Home : System.Web.UI.Page
{
    string sessionuserid = "" ;
 
    SqlQuery qry = new SqlQuery();  
    protected void Page_Load(object sender, EventArgs e)
    {       

        if (Session["Userid"] != null)
        {            
            sessionuserid = Session["Userid"].ToString();
            GC.Collect();
            //if (msg == "4")
            //{
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Your Work Station Not Authorised to Do this Operation');</script>", false);
            //}
        }
        else
        {
            Session.Abandon();
            Response.Cookies.Clear();           
            Response.Redirect("iopas.html");            
        }        
    }
}
