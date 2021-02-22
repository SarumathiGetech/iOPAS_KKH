using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using Datalayer;
using System.Data.SqlClient;

public partial class BottlePreLoadingPopup : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    BDSBottle BDSsearchqry = new BDSBottle();
    Searchquery searchqry = new Searchquery();
    string location = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cookies.Clear();
        if (Session["Userid"] == null)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "popup", "closepopup()", true);
        }
        else
        {
            location = Session["location"].ToString();
            if (!IsPostBack)
            {   
                //txtbarcode.Focus();
                threetd.Visible = false;
                Fsttd.Visible = true;
                secondtd.Visible = true;

                txtmfrbarcode.Attributes.Add("onKeyPress", "doClick(event)");
                txtitemcode.Attributes.Add("onKeyPress", "doClick2(event)");
                txtdrugcode.Attributes.Add("onKeyPress", "doClick2(event)");
                txtitemname.Attributes.Add("onKeyPress", "doClick2(event)");
                txtbrand.Attributes.Add("onKeyPress", "doClick2(event)");
                txtmfrcode.Attributes.Add("onKeyPress", "doClick2(event)");
               // txtbarcode.Attributes.Add("onKeyPress", "doClick2(event)");   
            }
        }
    }
    protected void btnsearch_Click(object sender, ImageClickEventArgs e)
    {
        int b = 0;

        if (txtbrand.Text.Trim() != "")
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
            Searchquery();
            DataSet dsData = preloadpopgrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(preloadpopgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(preloadpopgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = Color.Green;
            }
        }
        else if (b != 0 && b != 1)
        {
            BDSsearchqry.BDSpreloadpoppupitemcode(preloadpopgrid, "", location);
            DataSet dsData = preloadpopgrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(preloadpopgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(preloadpopgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = Color.Green;
            }
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Only one field entry is allowed');</script>", false);
        }
    }

    public void Searchquery()
    {
        if (txtitemcode.Text.Trim() != "")
        {// * Search itemcode Exact search *\\
            BDSsearchqry.BDSpreloadpoppupitemcode(preloadpopgrid, txtitemcode.Text.Trim(), location);
        }
        //else if (txtbarcode.Text.Trim() != "")
        //{
        //    int barcode = Convert.ToInt32(txtbarcode.Text.Remove(0, 6));
        //    BDSsearchqry.BDSpreloadpopupBarcode(preloadpopgrid, barcode, location);
        //    txtbarcode.Text = "";
        //}
        else if (txtdrugcode.Text.Trim() != "")
        {
            BDSsearchqry.BDSpreloadpopup(preloadpopgrid, txtdrugcode.Text.Trim(), txtitemname.Text.Trim(), location);
        }
        else if (txtitemname.Text.Trim() != "")
        {
            BDSsearchqry.BDSpreloadpopup(preloadpopgrid, txtdrugcode.Text.Trim(), txtitemname.Text.Trim(), location);
        }
        else if (txtbrand.Text.Trim() != "")
        {
            // * Brand Name Begin Search * \\
            BDSsearchqry.BDSbrandname(preloadpopgrid, txtbrand.Text.Trim(), location);
        }
        else if (txtmfrcode.Text != "" && txtbrand.Text.Trim() == "" && txtitemname.Text.Trim() == "" && txtdrugcode.Text.Trim() == "" && txtitemcode.Text.Trim() == "")
        {
            // * MFR Code Exact Search * \\
            BDSsearchqry.BDSmfrcode(preloadpopgrid, txtmfrcode.Text.Trim(), location);
        }
        else
        {
            BDSsearchqry.BDSpreloadpoppupitemcode(preloadpopgrid, "", location);
        }

    }
    protected void btnclear_Click(object sender, ImageClickEventArgs e)
    {
       // txtbarcode.Text = "";
        txtdrugcode.Text = "";
        txtitemcode.Text = "";
        txtitemname.Text = "";
        txtmfrcode.Text = "";
        txtbrand.Text = "";
        BDSsearchqry.BDSpreloadpoppupitemcode(preloadpopgrid, txtitemcode.Text.Trim(), location);
        DataSet dsData = preloadpopgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(preloadpopgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(preloadpopgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.Font.Bold = false;
        lblpge.ForeColor = Color.Black;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
    protected void preloadpopgrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {    
        Searchquery();
        preloadpopgrid.PageIndex = e.NewPageIndex;
        preloadpopgrid.DataBind();
        DataSet dsData = preloadpopgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(preloadpopgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(preloadpopgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.Font.Bold = false;
        lblpge.ForeColor = Color.Black;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
    protected void preloadpopgrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Btn")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = preloadpopgrid.Rows[index];           
            ViewState["Drgid"] = (int)preloadpopgrid.DataKeys[selectedRow.RowIndex].Value;
            int check = Convert.ToInt32(preloadpopgrid.Rows[selectedRow.RowIndex].Cells[6].Text);
            ViewState["Carttype"] = Convert.ToInt32(preloadpopgrid.Rows[selectedRow.RowIndex].Cells[6].Text);
            ViewState["BoxOrPallet"] = preloadpopgrid.Rows[selectedRow.RowIndex].Cells[5].Text;

            if (MFRcodecountcheck(Convert.ToInt32(ViewState["Drgid"].ToString())))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popup", "passValueToParent('" + Convert.ToInt32(ViewState["Drgid"].ToString()) + "','" + Convert.ToInt32(ViewState["Carttype"].ToString()) + "','" + ViewState["BoxOrPallet"].ToString() + "')", true);
            }
            else
            {               
                threetd.Visible = true;
                Fsttd.Visible = false;
                secondtd.Visible = false;
                txtmfrbarcode.Text = "";
                txtmfrbarcode.Focus();
            }
        }
    }

    public Boolean MFRcodecountcheck(int IumID)
    {
        bool bol = true;
       


        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select Mfrbarcode from  MFR_Barcode where Brandid=(select Brandid  from Item_user_Master where ID='" + IumID + "') and Masterid =(select Masterid from Item_user_Master where ID='" + IumID + "')";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        bol = false;
                        return bol;
                    }

                }
                else
                {
                    bol = true;
                }
            }
        }

        return bol;
    }


    public Boolean MFRcodecheck(int IumID)
    {
        bool bol = true;        
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select Mfrbarcode from  MFR_Barcode where Brandid=(select Brandid  from Item_user_Master where ID='" + IumID + "') and Masterid =(select Masterid from Item_user_Master where ID='" + IumID + "')";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr[0].ToString().ToLower() == txtmfrbarcode.Text.Trim().ToLower())
                        {
                            bol = true;
                            return bol;
                        }
                        else
                        {
                            bol = false;
                        }

                    }
                    dr.Close();
                }
                else
                {
                    bol = false;
                }

            }
        }

        return bol;
    }

    protected void preloadpopgrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        Searchquery();
        DataSet dsData = preloadpopgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = preloadpopgrid.PageIndex;
        preloadpopgrid.DataSource = SortDataTable(dtData, false);
        preloadpopgrid.DataBind();
        preloadpopgrid.PageIndex = pageIndex; 
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

    protected void btnbarcodesearch_Click(object sender, ImageClickEventArgs e)
    {
        if (MFRcodecheck(Convert.ToInt32(ViewState["Drgid"].ToString())))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popup", "passValueToParent('" + Convert.ToInt32(ViewState["Drgid"].ToString()) + "','" + Convert.ToInt32(ViewState["Carttype"].ToString()) + "','" + ViewState["BoxOrPallet"].ToString() + "')", true);
        }
        else
        {
            threetd.Visible = false;
            Fsttd.Visible = true;
            secondtd.Visible = true;
            //btnsearch.UseSubmitBehavior = true;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Manufacture Barcode Mismatch');</script>", false);
        }       
    }
}