using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Datalayer;
using System.Drawing;

public partial class Preloadpopup : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    Searchquery searchqry = new Searchquery();

    string  location = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Userid"] != null)
        {
            location = Session["location"].ToString();
           // txtitemcode.Focus();
            ViewState["I-E-TO"] = "";
            ViewState["S-E-TO"] = "";
            ViewState["U-Status"] = "";
            ViewState["B-Status"] = "";
            
            txtbarcode.Focus();
            threetd.Visible = false;
            Fsttd.Visible = true;
            secondtd.Visible = true;

            txtbarcode.Attributes.Add("onKeyPress", "doClick2(event)");
            txtmfrbarcode.Attributes.Add("onKeyPress", "doClick(event)");
            txtitemcode.Attributes.Add("onKeyPress", "doClick2(event)");
            txtdrugcode.Attributes.Add("onKeyPress", "doClick2(event)");
            txtitemname.Attributes.Add("onKeyPress", "doClick2(event)");
            txtbrand.Attributes.Add("onKeyPress", "doClick2(event)");
            txtmfrcode.Attributes.Add("onKeyPress", "doClick2(event)");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "popup", "closepopup()", true);
        }
    }
  
    //protected void btnsearch_Click(object sender, EventArgs e)
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

                if (ViewState["U-Status"].ToString().ToLower() == "NO".ToLower())
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Drug Master Inactivate');</script>", false);
                }
                else if (ViewState["B-Status"].ToString().ToLower() == "Inactive".ToLower())
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Brand Master Inactivate');</script>", false);
                }
                else if (ViewState["I-E-TO"].ToString() != null && ViewState["I-E-TO"].ToString() != "")
                {
                    if (Convert.ToDateTime(ViewState["I-E-TO"].ToString()) < System.DateTime.Now)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Item Effective to date reached');</script>", false);
                    }
                }
                else if (ViewState["S-E-TO"].ToString() != null && ViewState["S-E-TO"].ToString() != "")
                {
                    if (Convert.ToDateTime(ViewState["S-E-TO"].ToString()) < System.DateTime.Now)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Store Effective to date reached');</script>", false);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Such item not found in drug master');</script>", false);
                }

            }
        }
        else if (b != 0 && b != 1)
        {
            searchqry.preloadpoppupitemcode(preloadpopgrid, "", location);
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
            searchqry.preloadpoppupitemcode(preloadpopgrid, txtitemcode.Text.Trim(),location);

            DataSet dsData = preloadpopgrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            
            if (dtData.Rows.Count == 0)
            {
                ItemCodeCheck(txtitemcode.Text.Trim());                
            }
        }
        else if (txtbarcode.Text.Trim() != "")
        {
            string barcodeStr = txtbarcode.Text.Trim();
            barcodeStr = barcodeStr.Remove(0, barcodeStr.LastIndexOf("-") + 1);
            int barcode = Convert.ToInt32(barcodeStr);
            
            searchqry.preloadpopupBarcode(preloadpopgrid, barcode, location);
            txtbarcode.Text = "";
            DataSet dsData = preloadpopgrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            
            if (dtData.Rows.Count == 0)
            {
                BarcodeCheck(barcode);              
            }            
        }
        else if (txtdrugcode.Text.Trim() != "")
        {
            searchqry.preloadpopup(preloadpopgrid, txtdrugcode.Text.Trim(), txtitemname.Text.Trim(),location);

            DataSet dsData = preloadpopgrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];

            if (dtData.Rows.Count == 0)
            {
                DrugCodeCheck(txtdrugcode.Text.Trim());
            }
        }
        else if (txtitemname.Text.Trim() != "")
        {
            searchqry.preloadpopup(preloadpopgrid, txtdrugcode.Text.Trim(), txtitemname.Text.Trim(),location);
            DataSet dsData = preloadpopgrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];

            if (dtData.Rows.Count == 0)
            {
                ItemNameCheck(txtitemname.Text.Trim());
            }
        }
        else if (txtbrand.Text.Trim() != "")
        {
            // * Brand Name Begin Search * \\
            searchqry.brandname(preloadpopgrid, txtbrand.Text.Trim(),location );

            DataSet dsData = preloadpopgrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];

            if (dtData.Rows.Count == 0)
            {
                BrandNameCheck(txtbrand.Text.Trim());
            }
        }
        else if (txtmfrcode.Text != "" && txtbrand.Text.Trim() == "" && txtitemname.Text.Trim() == "" && txtdrugcode.Text.Trim() == "" && txtitemcode.Text.Trim() == "")
        {
            // * MFR Code Exact Search * \\
            searchqry.mfrcode(preloadpopgrid, txtmfrcode.Text.Trim(), location);

            DataSet dsData = preloadpopgrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];

            if (dtData.Rows.Count == 0)
            {
                MFRCodeCheck(txtmfrcode.Text.Trim());
            }
        }
        else
        {
            searchqry.preloadpoppupitemcode(preloadpopgrid, "", location);
        }

    }
    protected void preloadpopgrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if ((e.Row.RowType == DataControlRowType.DataRow))
        //{
        //    int Drgid = (int)preloadpopgrid.DataKeys[e.Row.RowIndex].Value;
        //    ((Button)e.Row.FindControl("btnSelect")).Attributes.Add("onclick", "javascript:passValueToParent('" + Drgid + "')");
        //}

    }
    protected void preloadpopgrid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    //protected void btnclear_Click(object sender, EventArgs e)
    protected void btnclear_Click(object sender, ImageClickEventArgs e)
    {
        txtbarcode.Text = "";
        txtdrugcode.Text = "";
        txtitemcode.Text = "";
        txtitemname.Text = "";
        txtmfrcode.Text = "";
        txtbrand.Text = "";
        searchqry.preloadpoppupitemcode(preloadpopgrid, txtitemcode.Text.Trim(),location);
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
            //int Drgid = (int)preloadpopgrid.DataKeys[selectedRow.RowIndex].Value;
            ViewState["Drgid"] = (int)preloadpopgrid.DataKeys[selectedRow.RowIndex].Value;

            if (MFRcodecountcheck(Convert.ToInt32(ViewState["Drgid"].ToString())))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popup", "passValueToParent('" + Convert.ToInt32(ViewState["Drgid"].ToString()) + "')", true);
            }
            else
            {
                //btnsearch.UseSubmitBehavior = false;
                threetd.Visible = true;
                Fsttd.Visible = false;
                secondtd.Visible = false;
                txtmfrbarcode.Text = "";
                txtmfrbarcode.Focus();
            }            
        }
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
                    dr.Close();
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



    // * NEW SEARCH CHECK FOR * \\

    public void Clear()
    {
        ViewState["I-E-TO"] = "";
        ViewState["S-E-TO"] = "";
        ViewState["U-Status"] = "";
        ViewState["B-Status"] = "";
    }

    // Item Code
    public void ItemCodeCheck(string ItemCode)
    {
        Clear();

        using (SqlConnection con = DBCon.getstring())
        {
            string Bot = "Bottle";
            string Commt = "Select i.Item_Effective_date_To,il.Store_Effective_Date_To,u.Status,ba.Status from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where   i.Item_Code='" + ItemCode + "' and p.Location_Name='" + location + "'  and pm.Packtype not like '%" + Bot + "%' ";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ViewState["I-E-TO"] = dr[0].ToString();
                        ViewState["S-E-TO"] = dr[1].ToString();
                        ViewState["U-Status"] = dr[2].ToString();
                        ViewState["B-Status"] = dr[3].ToString();
                    }
                }
            }
        }

    }

    // Drug Barcode
    public void BarcodeCheck(int barcode)
    {
        Clear();
        using (SqlConnection con = DBCon.getstring())
        {
            string Bot = "Bottle";
            string Commt = "Select i.Item_Effective_date_To ,il.Store_Effective_Date_To,u.Status,ba.Status from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where  U.ID='" + barcode + "' and p.Location_Name='" + location + "'  and pm.Packtype not like '%" + Bot + "%' ";                   
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ViewState["I-E-TO"] = dr[0].ToString();
                        ViewState["S-E-TO"] = dr[1].ToString();
                        ViewState["U-Status"] = dr[2].ToString();
                        ViewState["B-Status"] = dr[3].ToString();
                    }
                }
            }
        }
    }

    // Drug Code
    public void DrugCodeCheck(string DrugCode)
    {
        Clear();       
        using (SqlConnection con = DBCon.getstring())
        {
            string Bot = "Bottle";
            string Commt = "Select i.Item_Effective_date_To ,il.Store_Effective_Date_To,u.Status,ba.Status  from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where   i.Drug_Code ='" + DrugCode + "'  and p.Location_Name='" + location + "'  and pm.Packtype not like '%" + Bot + "%' ";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ViewState["I-E-TO"] = dr[0].ToString();
                        ViewState["S-E-TO"] = dr[1].ToString();
                        ViewState["U-Status"] = dr[2].ToString();
                        ViewState["B-Status"] = dr[3].ToString();
                    }
                }
            }
        }

    }

    // Item Name
    public void ItemNameCheck(string ItemName)
    {
        Clear();       
        using (SqlConnection con = DBCon.getstring())
        {
            string Bot = "Bottle";
            string Commt = "Select i.Item_Effective_date_To ,il.Store_Effective_Date_To,u.Status,ba.Status  from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where   i.Item_Name ='" + ItemName + "'  and p.Location_Name='" + location + "'  and pm.Packtype not like '%" + Bot + "%' ";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ViewState["I-E-TO"] = dr[0].ToString();
                        ViewState["S-E-TO"] = dr[1].ToString();
                        ViewState["U-Status"] = dr[2].ToString();
                        ViewState["B-Status"] = dr[3].ToString();
                    }
                }
            }
        }
    }


    // Brand Name
    public void BrandNameCheck(string BrandName)
    {
        Clear();

        using (SqlConnection con = DBCon.getstring())
        {
            string Bot = "Bottle";
            string Commt = "Select i.Item_Effective_date_To ,il.Store_Effective_Date_To,u.Status,ba.Status from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where  b.brandname like '" + BrandName + "%' and p.Location_Name='" + location + "' and  pm.Packtype not like '%" + Bot + "%' ";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ViewState["I-E-TO"] = dr[0].ToString();
                        ViewState["S-E-TO"] = dr[1].ToString();
                        ViewState["U-Status"] = dr[2].ToString();
                        ViewState["B-Status"] = dr[3].ToString();
                    }
                }
            }
        }
    }


    // MFR Barcode
    public void MFRCodeCheck(string MFRCode)
    {
        Clear();

        using (SqlConnection con = DBCon.getstring())
        {
            string Bot = "Bottle";
            string Commt = "Select i.Item_Effective_date_To ,il.Store_Effective_Date_To,u.Status,ba.Status from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join MFR_Barcode as m on m.Brandid =b.BrandID and m.Masterid=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where m.Mfrbarcode='" + MFRCode + "' and p.Location_Name='" + location + "' and  pm.Packtype not like '%" + Bot + "%' ";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ViewState["I-E-TO"] = dr[0].ToString();
                        ViewState["S-E-TO"] = dr[1].ToString();
                        ViewState["U-Status"] = dr[2].ToString();
                        ViewState["B-Status"] = dr[3].ToString();
                    }
                }
            }
        }
    }


    protected void btnbarcodesearch_Click(object sender, ImageClickEventArgs e)
    {

        if (MFRcodecheck(Convert.ToInt32(ViewState["Drgid"].ToString())))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popup", "passValueToParent('" + Convert.ToInt32(ViewState["Drgid"].ToString()) + "')", true);
        }
        else
        {
            threetd.Visible = false;
            Fsttd.Visible = true;
            secondtd.Visible = true;
            // btnsearch.UseSubmitBehavior = true;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Manufacture Barcode Mismatch');</script>", false);
        }
    }

}