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
using System.Drawing;
public partial class Drugpackmaster : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    smsclass sms = new smsclass();
    string sessionuserid = "";   
  //  public static string pcktypeid="";
   
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
            gridpack.PageIndex = 0;
            griddisplay();
            btnsubmit.Visible = true;
            btnupdate.Visible = false;
        }
    }

    public void packtypeinsert()
    {
       int Rtnval= sms.packtypeinsert(txtpacktype.Text.Trim(), txtdesc.Text.Trim(), sessionuserid = Session["Userid"].ToString());
       if (Rtnval == 1)
       {
           string msg = string.Format("alert('Record created')");

           ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
       }
       else if (Rtnval == 2)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Packtype already exist');</script>", false);
       }
       else
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
       }
       
    }

    //protected void btnsubmit_Click(object sender, EventArgs e)
    protected void btnsubmit_Click(object sender, ImageClickEventArgs e)
    {
        packtypeinsert();
        gridpack.PageIndex = 0;
        griddisplay();
    }

    protected void btnupdate_Click(object sender, ImageClickEventArgs e)
    {
        packtypeupdate();
        gridpack.PageIndex = 0;
        griddisplay();
        txtdesc.Text = "";
        txtpacktype.Text = "";
        btnsubmit.Visible = true;
        btnupdate.Visible = false;
    }
    public void packtypeupdate()
    {
        int Rtnvalue = sms.packtypeupdate(txtpacktype.Text.Trim(), txtdesc.Text.Trim(), sessionuserid = Session["Userid"].ToString(), ViewState["pcktypeid"].ToString());
       if (Rtnvalue == 1)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated');</script>", false);
       }
       else if (Rtnvalue == 2)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Packtype already exist');</script>", false);
       }
       else
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
       }
    }
    public void griddisplay()
    {
        sms.packtypegrid(gridpack);
        DataSet dsData = gridpack.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(gridpack.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridpack.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }    
    protected void gridpack_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        ViewState["pcktypeid"] = (string)gridpack.DataKeys[e.NewSelectedIndex].Value.ToString();  
        //pcktypeid = (string)gridpack.DataKeys[e.NewSelectedIndex].Value.ToString();  
        txtpacktype.Text = (gridpack.Rows[e.NewSelectedIndex].Cells[2].Text);
        btnsubmit.Visible = false;
        btnupdate.Visible = true;

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select description from packtype_master where ID='" + ViewState["pcktypeid"].ToString() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtdesc.Text = dr[0].ToString();
                    }
                }
            }
        }
    }
    //protected void btnadd_Click(object sender, EventArgs e)
    protected void btnadd_Click(object sender, ImageClickEventArgs e)
    {
        txtdesc.Text = "";
        txtpacktype.Text = "";
        btnsubmit.Visible = true;
        btnupdate.Visible = false;


       
        //Process p = new Process();
        // //p.StartInfo.FileName = ("C:\\Program Files\\Mobile Broadband E270\\Mobile Broadband E270.exe");
       // p.StartInfo.FileName = ("C:\\Documents and Settings\\GETECH1\\Desktop\\DDS_Simulator\\DDS Simulator.exe");
       //// p.StartInfo.FileName = ("Notepad.exe");
       // p.Start();
       // //System.Diagnostics.Process.Start("Notepad.exe");      
      
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
    protected void gridpack_Sorting(object sender, GridViewSortEventArgs e)
    {
        griddisplay();
        DataSet dsData = gridpack.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = gridpack.PageIndex;
        gridpack.DataSource = SortDataTable(dtData, false);
        gridpack.DataBind();
        gridpack.PageIndex = pageIndex;  
    }
    protected void gridpack_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
         griddisplay();
         gridpack.PageIndex = e.NewPageIndex;
         gridpack.DataBind();
         DataSet dsData = gridpack.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(gridpack.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridpack.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }


    
}
