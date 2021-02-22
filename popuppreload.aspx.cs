using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class popuppreload : System.Web.UI.Page
{
    string sessionuserid = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Userid"] != null)
        {
            if (!IsPostBack)
            {
               // btnsearch.Attributes.Add("onclick", "javascript:passValueToParent()");
                string scriptStr = "javascript:return popUpCalendar(this," + getClientID() + @", 'dd/mm/yyyy', '__doPostBack(\'" + getClientID() + @"\')')";
                imgCalendar.Attributes.Add("onclick", scriptStr);
                try
                {
                    sessionuserid = Session["Userid"].ToString();
                }
                catch (NullReferenceException)
                {
                    Response.Redirect("iopas.html");
                }
            }
            //Request.Cookies.Clear();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "popup", "closepopup()", true);
        }
       
    }
    public string getClientID()
    {
        return txt_Date.ClientID;
    }
    
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        int b=0;
        if (txt_Date.Text.Trim() != "")
        {
            b = 1;
        }
        if (txtbrand.Text.Trim() != "")
        {
            b = b + 1;
        }
        if (txtcartno.Text.Trim() != "")
        {
            b = b + 1;
        }
        if (txtdrugcode.Text.Trim() != "")
        {
            b = b + 1;
        }
        if (txtitemcode.Text.Trim() != "")
        {
            b = b + 1;
        }
        if (txtitemname.Text.Trim() != "")
        {
            b = b + 1;
        }
        if (txtmfrcode.Text.Trim() != "")
        {
            b = b + 1;
        }

        if (b == 0 || b == 1)
        {
            string a = ddlfilter.SelectedValue.ToString();
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popup", "passValueToParent('" + txtitemcode.Text + "','" + a + "','" + txt_Date.Text + "','" + txtcartno.Text + "','" + txtmfrcode.Text + "','" + txtitemname.Text + "','" + txtdrugcode.Text + "','" + txtbrand.Text + "')", true);
            // ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popup", "passValueToParent()", true);
        }
        else if (b != 0 && b != 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Only one field entry is allowed');</script>", false);
        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        txt_Date.Text = "";
        txtbrand.Text = "";
        txtcartno.Text = "";
        txtdrugcode.Text = "";
        txtitemcode.Text = "";
        txtmfrcode.Text = "";
        txtitemname.Text = "";
    }

    protected void ddlfilter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}