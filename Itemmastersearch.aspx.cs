using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using Datalayer;
using System.Drawing;

public partial class Itemmastersearch : System.Web.UI.Page
{
    Searchquery searqry = new Searchquery();
   // string sessionuserid = ""; 
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cookies.Clear();
        if (Session["Userid"] == null)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "popup", "closepopup()", true);
        }       
        else
        {
            if (!IsPostBack)
            {
                txtitemcode.Text = Request.QueryString["itcode"];
                txtdrgcode.Text = Request.QueryString["drcode"];
                txtitemname.Text = Request.QueryString["itname"];
                
                if (txtitemcode.Text.Trim() != "")
                {
                    itemcodesearch();
                }
                else if (txtdrgcode.Text.Trim() != "")
                {
                    codeandnamesearch();
                }
                else if (txtitemname.Text.Trim() != "")
                {
                    Itemnamesearch();
                }
                else
                {
                    lblpge.Text = "No Record Found";
                    lblpge.Font.Bold = true;
                    lblpge.ForeColor = Color.Green;
                }

                txtitemcode.Attributes.Add("onKeyPress", "doClick(event)");
                txtdrgcode.Attributes.Add("onKeyPress", "doClick(event)");
                txtitemname.Attributes.Add("onKeyPress", "doClick(event)");
            }           
        }
    }

    protected void btnadd_Click(object sender, ImageClickEventArgs e)
    {   
        itemsearch.PageIndex = 0;
        int a = 0;
        if (txtitemcode.Text.Trim() != "")
        {
            a = 1;
        }
        if (txtdrgcode.Text.Trim() != "")
        {
            a = a+ 1;
        }
        if (txtitemname.Text.Trim() != "")
        {
            a = a + 1;
        }

        if (a == 0 || a == 1)
        {

            if (txtitemcode.Text.Trim() != "")
            {
                itemcodesearch();
            }
            else if (txtdrgcode.Text.Trim() != "")
            {
                codeandnamesearch();
            }
            else if (txtitemname.Text.Trim() != "")
            {
                Itemnamesearch();
            }
            else
            {
                searqry.itemmastersearch(itemsearch, "");
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = Color.Green;
            }
        }
        else if (a != 0 && a != 1)
        {
            searqry.itemmastersearch(itemsearch, "");
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Only one field entry is allowed');</script>", false);
        }
    }
    // * Drug code  Search Function * \\
    public void codeandnamesearch()
    {        
        searqry.itemmasternamecode(itemsearch,txtdrgcode.Text.Trim());
        DataSet dsData = itemsearch.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(itemsearch.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(itemsearch.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.Font.Bold = false;
        lblpge.ForeColor = Color.Black;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;

            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Such item not found in item master');</script>", false);
        }
    }
    // * Item Name Search Function * \\
    public void Itemnamesearch()
    {
        searqry.itemmastername(itemsearch, txtitemname.Text.Trim());
        DataSet dsData = itemsearch.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(itemsearch.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(itemsearch.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.Font.Bold = false;
        lblpge.ForeColor = Color.Black;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;

            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Such item not found in item master');</script>", false);
        }
    }
    // * Item code search * \\
    public void itemcodesearch()
    {
        searqry.itemmastersearch(itemsearch, txtitemcode.Text.Trim());
        DataSet dsData = itemsearch.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(itemsearch.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(itemsearch.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.Font.Bold = false;
        lblpge.ForeColor = Color.Black;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;

            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Such item not found in item master');</script>", false);
        }
    }
    protected void itemsearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        codeandnamesearch();
        itemsearch.PageIndex = e.NewPageIndex;
        itemsearch.DataBind();
        DataSet dsData = itemsearch.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(itemsearch.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(itemsearch.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
        }        
    }
    protected void itemsearch_Sorting(object sender, GridViewSortEventArgs e)
    {
        codeandnamesearch();
        DataSet dsData = itemsearch.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = itemsearch.PageIndex;
        itemsearch.DataSource = SortDataTable(dtData, false);
        itemsearch.DataBind();
        itemsearch.PageIndex = pageIndex;  
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
    protected void itemsearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if ((e.Row.RowType == DataControlRowType.DataRow))
        //{             
        //   ((Button)e.Row.FindControl("btnSelect")).Attributes.Add("onclick", "javascript:passValueToParent('" + e.Row.Cells[1].Text + "','" + e.Row.Cells[2].Text + "','" + e.Row.Cells[3].Text + "')");        
            
        //}
      
    }
    protected void btnclr_Click(object sender, ImageClickEventArgs e)
    {  
        txtdrgcode.Text = "";
        txtitemcode.Text = "";
        txtitemname.Text = "";
        searqry.itemmastersearch(itemsearch, "");
        lblpge.Text = "No Record Found";
        lblpge.Font.Bold = true;
        lblpge.ForeColor = Color.Green;
    }
    protected void itemsearch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Btn")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = itemsearch.Rows[index];          
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popup", "passValueToParent('" + selectedRow.Cells[1].Text + "','" + selectedRow.Cells[2].Text + "','" + selectedRow.Cells[3].Text.Replace("'","") + "')", true);
           
        }
    }


   
}