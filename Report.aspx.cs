using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Userid"] != null)
        {
            // sessionuserid = Session["Userid"].ToString();
            if (!IsPostBack)
            {
                string servername = System.Configuration.ConfigurationManager.AppSettings["Servername"];
                ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "popup", "closepopup('" + servername + "')", true);
            }
        }
        else
        {
            Response.Redirect("opas.html?Session=End");
        }
        

        //HttpCookie aCookie = new HttpCookie("lastVisit");
        //aCookie.Value = DateTime.Now.ToString();
        //aCookie.Expires = DateTime.Now.AddDays(1);
        //Response.Cookies.Add(aCookie);

        //HttpCookie aCookie;
        //string cookieName;
        //int limit = Request.Cookies.Count;
        //for (int i = 0; i < limit; i++)
        //{
        //    cookieName = Request.Cookies[i].Name;
        //    aCookie = new HttpCookie(cookieName);
        //    aCookie.Expires = DateTime.Now.AddDays(-1);
        //    Response.Cookies.Add(aCookie);
        //}
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    //protected void btnss_Click(object sender, EventArgs e)
    //{
    //    ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "popup", "closepopup()", true);
    //}
}