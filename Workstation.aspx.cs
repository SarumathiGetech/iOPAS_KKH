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
using Datalayer;
using System.Drawing;

public partial class Workstation : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    SqlQuery qry = new SqlQuery();
    DDSquery dds = new DDSquery();
    string sessionuserid = "";    
   // public static int hostid,D=0;
    int a = 0, b = 0, c = 0;
   // static string btnval = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["hostid"] = 0;
            ViewState["D"] = 0;
            locationreader();
            hostgrid.PageIndex = 0;
            griddisplay();
            btnsave.Visible = true;
            btnupdate.Visible = false;
            try
            {
                sessionuserid = Session["Userid"].ToString();
            }
            catch (NullReferenceException)
            {
               // ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Session');</script>", false);
                //Response.Redirect("opas.html?Session=End");
                Response.Redirect("iopas.html");
            }

            // panelgrid.Visible = false;
        }
    }
   
    protected void btnedit_Click(object sender, EventArgs e)
    {
        //paneledit.Visible = true;
        //panelgrid.Visible = false;
        //paneladd.Visible = false;

    }
    //protected void btnactive_Click(object sender, EventArgs e)
    //{
    //    panelgrid.Visible = false;


    //}
    public void locationreader()
    {
        ddlpharloc.Items.Clear();      
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select Location_Name from pharmacy where Status='Active'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddlpharloc.Items.Add(dr[0].ToString());
                    }

                }
            }
        }
    }
    protected void btnsave_Click(object sender, ImageClickEventArgs e)
    {
        hostgrid.PageIndex = 0;
        insert();
        griddisplay();
        clear();
    }

    protected void btnupdate_Click(object sender, ImageClickEventArgs e)
    {
        hostgrid.PageIndex = 0;
        update();
        //txthost.BackColor = System.Drawing.Color.White;
        //txthost.ReadOnly = false;

        btnsave.Visible = true;
        btnupdate.Visible = false;
        griddisplay();
        clear();
    }

    public void insert()
    {       
    int Retvalue=dds.Hostnameinsert(ddlpharloc.SelectedValue.ToString(), txthost.Text.Trim(), txtdescription.Text.Trim(), sessionuserid = Session["Userid"].ToString());
    if (Retvalue == 2)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record Created ');</script>", false);
        txthost.Text = "";
        txtdescription.Text = "";
    }
    else if (Retvalue == 3)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Host Name already exist');</script>", false);
    }
    else
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occured');</script>", false);
    }
        
    }

    public void update()
    {
        int updretvalue = dds.Hostnameupdate((int)ViewState["hostid"], ddlpharloc.SelectedValue.ToString(), txthost.Text.Trim(), txtdescription.Text.Trim(), sessionuserid = Session["Userid"].ToString());
    if (updretvalue == 2)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated');</script>", false);
    }
    else if (updretvalue == 3)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Host Name already exist');</script>", false);
    }
    else
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occured');</script>", false);
    }
        
    }

    public void griddisplay()
    {
        ViewState["D"] = 1;
       // D = 1;
        dds.gridhost(hostgrid);
        DataSet dsData = hostgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(hostgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(hostgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }

    public void griddisplaysearch()
    {
        //D = 2;
        ViewState["D"] = 2;
        
        dds.gridhostsearch(hostgrid,txthost.Text);
        DataSet dsData = hostgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(hostgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(hostgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }

    protected void hostgrid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        btnsave.Visible = false;
        btnupdate.Visible = true;
        //int index = Convert.ToInt32(e.NewSelectedIndex);
        //hostid = (int)hostgrid.DataKeys[e.NewSelectedIndex].Value;        
       // hostid = Convert.ToInt32(hostgrid.Rows[e.NewSelectedIndex].Cells[2].Text);
        
        ViewState["hostid"]= (int)hostgrid.DataKeys[e.NewSelectedIndex].Value;    
        txthost.Text = hostgrid.Rows[e.NewSelectedIndex].Cells[3].Text;
        locationreader();
        ddlpharloc.Items.Remove(hostgrid.Rows[e.NewSelectedIndex].Cells[4].Text);
        ddlpharloc.Items.Insert(0, new ListItem(hostgrid.Rows[e.NewSelectedIndex].Cells[4].Text));
        description();
  

        //DataKeyArray keylist = hostgrid.DataKeys as DataKeyArray;
        //if (keylist != null)
        //{
        //    string str2 = Convert.ToString(keylist[e.NewSelectedIndex].Value);
        //}

    }
    protected void ddlpharloc_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    public void clear()
    {
        txthost.Text = "";
        txtdescription.Text = "";
    }

    public void description()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select Description From Workstation where Hostid='" + (int)ViewState["hostid"] + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtdescription.Text = dr[0].ToString();
                    }
                }
            }
        }
    }

    protected void btnadd_Click(object sender, ImageClickEventArgs e)
    {
        //txthost.BackColor = System.Drawing.Color.White;
        //txthost.ReadOnly = false;
        btnsave.Visible = true;
        btnupdate.Visible = false;
        clear();
        hostgrid.PageIndex = 0;
        griddisplay();
    }


    // * CARTRIDGE ACTIVE INACTIVE STATUS FUNCTIONS * \\
    public void activeinactive()
    {
        for (int i = 0; i < hostgrid.Rows.Count; i++)
        {
            GridViewRow row = hostgrid.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("chkaccess")).Checked;

            if (isChecked)
            {
                c++;
                string ddsno = (hostgrid.Rows[i].Cells[5].Text);
                string act = "Active";
                string inc = "Inactive";
                if (act == ddsno)
                {
                    a++;
                }
                else if (ddsno == inc)
                {
                    b++;
                }
            }
        }
    }
    protected void btnactive_Click1(object sender, ImageClickEventArgs e)
    {
        //btnval = "";
        //btnval = "1";
        ViewState["btnval"] = "";
        ViewState["btnval"] = "1";
        activeinactive();
        if (a != 0 & b == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Selected records are already active');</script>", false);
        }
        else if (a != 0 & b != 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Selected records are of different status');</script>", false);
        }
        else if (c != 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "popup", "confirmProcess()", true);
        }
        else if (c == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Records are not selected');</script>", false);
        }
        else
        {
            griddisplay();
        }
    }
    protected void btndeactive_Click(object sender, ImageClickEventArgs e)
    {
        //btnval = "";
        //btnval = "2";
        ViewState["btnval"] = "";
        ViewState["btnval"] = "2";
        activeinactive();
        if (b != 0 & a == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Selected records are already Inactive');</script>", false);
        }
        else if (a != 0 & b != 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Selected records are of different status');</script>", false);
        }
        else if (c != 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "popup", "confirmProcess1()", true);
        }
        else if (c == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Records are not selected ');</script>", false);
        }
        else
        {
            griddisplay();
        }
    }

    protected void btnok_Click(object sender, EventArgs e)
    {
        if (ViewState["btnval"].ToString() == "1")
        {
            for (int i = 0; i < hostgrid.Rows.Count; i++)
            {
                GridViewRow row = hostgrid.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkaccess")).Checked;

                if (isChecked)
                {
                   // int index = Convert.ToInt32(i);
                    int hostname = (int)hostgrid.DataKeys[i].Value;                    
                   // int hostname = Convert.ToInt32((hostgrid.Rows[i].Cells[2].Text));
                   dds.hostenabled(hostname, sessionuserid = Session["Userid"].ToString());
                }
            }
            griddisplay();
        }

        else if (ViewState["btnval"].ToString() == "2")
        {
            for (int i = 0; i < hostgrid.Rows.Count; i++)
            {
                GridViewRow row = hostgrid.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkaccess")).Checked;

                if (isChecked)
                {
                   // int index = Convert.ToInt32(i);
                    int hostname = (int)hostgrid.DataKeys[i].Value; 
                    //int hostname = Convert.ToInt32((hostgrid.Rows[i].Cells[2].Text));
                    dds.hostdisabled(hostname, sessionuserid = Session["Userid"].ToString());
                }
            }
            griddisplay();
        }
    }
    protected void hostgrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        if ((int)ViewState["D"] == 1)
        {
            griddisplay();
        }
        else if ((int)ViewState["D"] == 2)
        {
            griddisplaysearch();
        }
        hostgrid.PageIndex = e.NewPageIndex;
        hostgrid.DataBind();
        DataSet dsData = hostgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(hostgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(hostgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }

    protected void hostgrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        if ((int)ViewState["D"] == 1)
        {
            griddisplay();
        }
        else if ((int)ViewState["D"] == 2)
        {
            griddisplaysearch();
        }
        DataSet dsData = hostgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = hostgrid.PageIndex;
        hostgrid.DataSource = SortDataTable(dtData, false);
        hostgrid.DataBind();
        hostgrid.PageIndex = pageIndex; 
    }
    private string GridViewSortDirection
    {
        get
        {
            return ViewState["SortDirection"] as string ?? "ASC";
        }
        set
        {
            ViewState["SortDirection"] = value;
        }
    }
    /// Gets or Sets the gridview sortexpression property
    private string GridViewSortExpression
    {
        get
        {
            return ViewState["SortExpression"] as string ?? string.Empty;
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }
    /// Returns the direction of the sorting
    private string GetSortDirection()
    {
        switch (GridViewSortDirection)
        {
            case "ASC":
                GridViewSortDirection = "DESC";
                break;
            case "DESC":
                GridViewSortDirection = "ASC";
                break;
        }
        return GridViewSortDirection;
    }
    protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging)
    {
        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            if (GridViewSortExpression != string.Empty)
            {
                if (isPageIndexChanging)
                {
                    dataView.Sort = string.Format("{0} {1}",
                    GridViewSortExpression, GridViewSortDirection);
                }
                else
                {
                    dataView.Sort = string.Format("{0} {1}",
                   GridViewSortExpression, GetSortDirection());
                }
            }
            return dataView;
        }
        else
        {
            return new DataView();
        }
    }
    protected void Btnsearch_Click(object sender, ImageClickEventArgs e)
    {
        hostgrid.PageIndex = 0;
        griddisplaysearch();
    }

   
}
