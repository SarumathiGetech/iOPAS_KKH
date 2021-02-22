using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Datalayer;

public partial class BDSStructureStatus : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    string sessionuserid = "", location = "";
    GridView GridView1 = new GridView();    
    BDSBottle BDS = new BDSBottle();
    DataSet dsData = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {       
        if (Session["Userid"] != null)
        {
            sessionuserid = Session["Userid"].ToString();
            location = Session["location"].ToString();

            if (!IsPostBack)
            {
                ViewState["ord"] = "";
                ddsnoreader();
            }
        }
        else
        {
            Response.Redirect("iopas.html");
        }
    }

    protected void btnsear_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["ord"] = "order by VC.VC_ID desc";
        GridDisplay();
    }

    public void GridDisplay()
    {
        dsData = new DataSet();
        if (ddlmcno.SelectedItem != null)
        {
            if ((ddlcartno.SelectedItem.Value == "All(Alloted & Open)") && (txtvcwidthmin.Text.Trim() == "") && (txtvcwidthmax.Text.Trim() == ""))
            {
                BDS.BDSStructureDisplayAll(Cartstructure, txtvcnumber.Text.Trim(), location, ViewState["ord"].ToString());
                dsData = Cartstructure.DataSource as DataSet;
            }
            else if ((ddlcartno.SelectedItem.Value == "Alloted") && (txtvcwidthmin.Text.Trim() == "") && (txtvcwidthmax.Text.Trim() == ""))
            {
                BDS.BDSStructureDisplay(Cartstructure, "Allot", txtvcnumber.Text.Trim(), location, ViewState["ord"].ToString());
                dsData = Cartstructure.DataSource as DataSet;
            }
            else if ((ddlcartno.SelectedItem.Value == "Open") && (txtvcwidthmin.Text.Trim() == "") && (txtvcwidthmax.Text.Trim() == ""))
            {
                BDS.BDSStructureDisplay(Cartstructure, "Open", txtvcnumber.Text.Trim(), location, ViewState["ord"].ToString());
                dsData = Cartstructure.DataSource as DataSet;
            }
            else if ((ddlcartno.SelectedItem.Value == "All(Alloted & Open)") && (txtvcwidthmin.Text.Trim() != "") && (txtvcwidthmax.Text.Trim() != ""))
            {
                if ((Convert.ToDecimal(txtvcwidthmin.Text.Trim())) > (Convert.ToDecimal(txtvcwidthmax.Text.Trim())))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('VC Cartridge Width Maximum must be greater than VC Cartridge Width Minimum');</script>", false);
                }
                else
                {
                    BDS.BDSStructureDisplayAllStatusWidth(Cartstructure, txtvcwidthmin.Text.Trim(), txtvcwidthmax.Text.Trim(), location, ViewState["ord"].ToString());
                    dsData = Cartstructure.DataSource as DataSet;
                }               
            }
            else if ((ddlcartno.SelectedItem.Value == "Alloted") && (txtvcwidthmin.Text.Trim() != "") && (txtvcwidthmax.Text.Trim() != ""))
            {
                if ((Convert.ToDecimal(txtvcwidthmin.Text.Trim())) > (Convert.ToDecimal(txtvcwidthmax.Text.Trim())))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('VC Cartridge Width Maximum must be greater than VC Cartridge Width Minimum');</script>", false);
                }
                else
                {
                    BDS.BDSStructureDisplaySingleStatusWidth(Cartstructure, "Allot", txtvcwidthmin.Text.Trim(), txtvcwidthmax.Text.Trim(), location, ViewState["ord"].ToString());
                    dsData = Cartstructure.DataSource as DataSet;
                }
            }
            else if ((ddlcartno.SelectedItem.Value == "Open") && (txtvcwidthmin.Text.Trim() != "") && (txtvcwidthmax.Text.Trim() != ""))
            {
                if ((Convert.ToDecimal(txtvcwidthmin.Text.Trim())) > (Convert.ToDecimal(txtvcwidthmax.Text.Trim())))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('VC Cartridge Width Maximum must be greater than VC Cartridge Width Minimum');</script>", false);
                }
                else
                {
                    BDS.BDSStructureDisplaySingleStatusWidth(Cartstructure, "Open", txtvcwidthmin.Text.Trim(), txtvcwidthmax.Text.Trim(), location, ViewState["ord"].ToString());
                    dsData = Cartstructure.DataSource as DataSet;
                }
            }

            try
            {
                DataTable dtData = dsData.Tables[0];
                lblpge.Text = "Page" + "  " + Convert.ToString(Cartstructure.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Cartstructure.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
                lblpge.ForeColor = System.Drawing.Color.Black;
                lblpge.Font.Bold = false;
                if (dtData.Rows.Count == 0)
                {
                    lblpge.Visible = true;
                    lblpge.Text = "No Record Found";
                    lblpge.Font.Bold = true;
                    lblpge.ForeColor = System.Drawing.Color.Green;
                }

                if (dsData.Tables[0].Rows.Count > 0)
                {
                    GridView1.AllowPaging = false;
                    GridView1.DataSource = dsData;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }


    // * DDS Number Reader Function * \\
    public void ddsnoreader()
    {
        ddlmcno.Items.Clear();

       using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select d.DDS_Name from dds as d left join Pharmacy as p on p.PharmacyID=d.PharmacyID  where p.Location_Name='" + location + "' and d.Status='Active' and d.DDS_Name like '%BDS%' ";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddlmcno.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }
    protected void btnprint_Click(object sender, ImageClickEventArgs e)
    {
        GridDisplay();
        Session["ctrl"] = GridView1;
        ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popups", "Printpage()", true);
    }
    protected void Cartstructure_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridDisplay();
        Cartstructure.PageIndex = e.NewPageIndex;
        Cartstructure.DataBind();
        dsData = Cartstructure.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(Cartstructure.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Cartstructure.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = System.Drawing.Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Visible = true;
            lblpge.Font.Bold = true;
            lblpge.ForeColor = System.Drawing.Color.Green;
        }        

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
                if (GridViewSortDirection == "ASC")
                {
                    ViewState["ord"] = "order by" + " " + GridViewSortExpression + " " + "desc";
                }
                else if (GridViewSortDirection == "DESC")
                {
                    ViewState["ord"] = "order by" + " " + GridViewSortExpression + " " + "asc";
                }
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
    protected void Cartstructure_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["ord"] = "";
        GridDisplay();
        DataSet dsData = Cartstructure.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = Cartstructure.PageIndex;
        Cartstructure.DataSource = SortDataTable(dtData, false);
        Cartstructure.DataBind();
        Cartstructure.PageIndex = pageIndex;
    }
}