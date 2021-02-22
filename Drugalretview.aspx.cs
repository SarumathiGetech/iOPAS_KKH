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

public partial class Drugalretview : System.Web.UI.Page
{
    Searchquery scr = new Searchquery();
    string location = "", ddsname="";
    protected void Page_Load(object sender, EventArgs e)
    {       
        location = Session["location"].ToString();
        ddsname = Request.QueryString["dds"];
        griddrugalertdetails.PageIndex = 0;
        griddetails();
    }

    public void griddetails()
    {
        scr.lowstockdetails(griddrugalertdetails, location, ddsname);
        DataSet dsData = griddrugalertdetails.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(griddrugalertdetails.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(griddrugalertdetails.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = System.Drawing.Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Visible = true;
            lblpge.Font.Bold = true;
            lblpge.ForeColor = System.Drawing.Color.Green;
        }
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXCEL_ASHX/Cartridge_Lowstock_Export_Excel.ashx?DDS=" + ddsname + "&location=" + location);
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("Drugalertauto.aspx");
    }
    protected void griddrugalertdetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        griddetails();
        DataSet dsData = griddrugalertdetails.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = griddrugalertdetails.PageIndex;
        griddrugalertdetails.DataSource = SortDataTable(dtData, false);
        griddrugalertdetails.DataBind();
        griddrugalertdetails.PageIndex = pageIndex;  
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
    protected void griddrugalertdetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        griddetails();
        griddrugalertdetails.PageIndex = e.NewPageIndex;
        griddrugalertdetails.DataBind();
        DataSet dsData = griddrugalertdetails.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(griddrugalertdetails.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(griddrugalertdetails.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = System.Drawing.Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Visible = true;
            lblpge.Font.Bold = true;
            lblpge.ForeColor = System.Drawing.Color.Green;
        }
    }
}
