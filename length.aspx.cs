using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class length : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnlen_Click(object sender, EventArgs e)
    {
       
        txtresult.Text =Convert.ToString(txtlen.Text.Length);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        txtlen.Text = "";
        txtresult.Text = "";
    }
}