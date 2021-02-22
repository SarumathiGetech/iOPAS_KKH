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

public partial class Pharmacymaster : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    SqlQuery qry = new SqlQuery();
    grid gri = new grid();
    string sessionuserid="";
   // public static int pharmasterid;  
    DateTime dt;   
    protected void Page_Load(object sender, EventArgs e)
    {        
        try
        {
            sessionuserid = Session["Userid"].ToString();
        }
        catch (NullReferenceException)
        {
            //Response.Redirect("opas.html?Session=End");
            Response.Redirect("iopas.html");
        }
        if (!IsPostBack)
        {
            griddetails.PageIndex = 0;
            griddisplay();
            btnsubmit.Visible = true;
            btnupdate.Visible = false;
        }
    }
  
    //protected void btnsubmit_Click(object sender, EventArgs e)
    protected void btnsubmit_Click(object sender, ImageClickEventArgs e)
    {       
            griddetails.PageIndex = 0;
            pharmacyinsert();               
    }

    protected void btnupdate_Click(object sender, ImageClickEventArgs e)
    {
        griddetails.PageIndex = 0;
        pharmacyupdate();
        btnsubmit.Visible = true;
        btnupdate.Visible = false;
    }

    // * NEW PHARMACY LOCATION TEXTBOX CLEAR *\\ 
    public void addclear()
    {
        txtpharcode.Text = "";
        txtphloc.Text = "";       
        txtheader.Text = "";
        txtfooter.Text = "";
        txtfooter1.Text = "";
        txtpharabbr.Text = "";
        txtcartprefix.Text = "";
        btnsubmit.Visible = true;
        btnupdate.Visible = false;
        chkauto.Checked = false;
        txtcartprefix.ReadOnly = false;
    }

    public void griddisplay()
    {
        gri.locationdetails(griddetails);
        DataSet dsData = griddetails.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(griddetails.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(griddetails.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
    protected void griddetails_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
       // pharmasterid = 0;
        ViewState["pharmasterid"] = "";
        ViewState["pharmasterid"] = (int)griddetails.DataKeys[e.NewSelectedIndex].Value; 
        //pharmasterid = (int)griddetails.DataKeys[e.NewSelectedIndex].Value;  
        txtpharcode.Text = (griddetails.Rows[e.NewSelectedIndex].Cells[2].Text);
        txtphloc.Text = (griddetails.Rows[e.NewSelectedIndex].Cells[3].Text);
        txtpharabbr.Text = (griddetails.Rows[e.NewSelectedIndex].Cells[4].Text);
        string staedit = (griddetails.Rows[e.NewSelectedIndex].Cells[5].Text);
        if (staedit == "Active")
        {
            chkauto.Checked = true;
        }
        else if (staedit == "Inactive")
        {
            chkauto.Checked = false;
        }
        btnsubmit.Visible = false;
        btnupdate.Visible = true;
        address();
    }

    public void address()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select Label_header,Label_Footer1,Label_footer,Cartridge_Prefix from Pharmacy Where PharmacyID='" + (int)ViewState["pharmasterid"] + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtheader.Text = dr[0].ToString();
                        txtfooter1.Text = dr[1].ToString();
                        txtfooter.Text = dr[2].ToString();
                        txtcartprefix.Text = dr[3].ToString();
                        txtcartprefix.ReadOnly = true;
                    }
                }
            }
        }
    }

    public void pharmacyinsert()
    {
        string sta = "";
        if (chkauto.Checked == true)
        {
            sta = "Active";
        }
        else
        {
            sta = "Inactive";
        }
        int Rtnvalue = qry.pharmacyinsert(txtpharcode.Text.Trim(), txtphloc.Text.Trim(),txtpharabbr.Text.Trim(), txtheader.Text.Trim(), txtfooter1.Text.Trim(), txtfooter.Text.Trim().Trim(), sessionuserid = Session["Userid"].ToString(), sta, txtcartprefix.Text.Trim());
        if (Rtnvalue == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record created ');</script>", false);
            addclear();
            griddisplay();
        }
        else if (Rtnvalue == 2)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Pharmacy code already exist');</script>", false);
        }
        else if (Rtnvalue == 3)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Pharmacy name already exist');</script>", false);
        }
        else if (Rtnvalue == 4)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge prefix already exist');</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
        }
    }

    public void pharmacyupdate()
    {
        string sta = "";
        if (chkauto.Checked == true)
        {
            sta = "Active";
        }
        else
        {
            sta = "Inactive";
        }
        int Rtnvalue = qry.pharmacyupdate((int)ViewState["pharmasterid"], txtpharcode.Text.Trim(), txtphloc.Text.Trim(),txtpharabbr.Text.Trim(), txtheader.Text.Trim(),txtfooter1.Text.Trim(), txtfooter.Text.Trim(), sessionuserid = Session["Userid"].ToString(), sta );
        if (Rtnvalue == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record Updated ');</script>", false);
            addclear();
            griddisplay();
        }
        else if (Rtnvalue == 2)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Pharmacy code already exist');</script>", false);
        }
        else if (Rtnvalue == 3)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Pharmacy name already exist');</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
        }
    }
    //protected void btnadd_Click(object sender, EventArgs e)
    protected void btnadd_Click(object sender, ImageClickEventArgs e)
    {
        addclear();
        griddisplay();
    }  

    // * Date time conversion From DD/MM/YYYY to MM/DD/YYYY * \\
    public void date()
    {
        string i = "";
        Int32 len = i.Length;
        Int32 n = i.IndexOf('/');
        string str = i.Substring(n + 1, 2);
        string str1 = i.Substring(0, 2);
        string str2 = i.Substring(6, 4);
        string dat = str + "/" + str1 + "/" + str2;
        dt = Convert.ToDateTime(dat);
        // ------------------------------------------
    }
    protected void griddetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        griddisplay();
        griddetails.PageIndex = e.NewPageIndex;
        griddetails.DataBind();
        DataSet dsData = griddetails.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(griddetails.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(griddetails.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
    protected void griddetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        griddisplay();
        DataSet dsData = griddetails.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = griddetails.PageIndex;
        griddetails.DataSource = SortDataTable(dtData, false);
        griddetails.DataBind();
        griddetails.PageIndex = pageIndex;  
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
    
}
