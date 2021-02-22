using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Datalayer;
using System.Data.SqlClient;

public partial class Druglabelpopup : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    Searchquery searchqry = new Searchquery();
    GS1BarcodeFunction gs1barcode = new GS1BarcodeFunction();

    string location = "", drugmfrbarcode="";

    // GS1 Barcode Declaration
    string SymbolIdentifier = "";
    string GTIN = "";
    string Format = "";
    string ExpDate = "", BatchNumber = "", SerialNumber = "", ManufacturedDate = "";
    string mfrBarcode = "";
    string preloadmfrbarcode = "";
    int rtnValue = 0;
    bool haveMFRCODE = false;
    protected void Page_Load(object sender, EventArgs e)
    {
       if (Session["Userid"] == null)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "popup", "closepopup()", true);
        }
        else
        {
            location = Session["location"].ToString();
           if (!IsPostBack)
            {
                ViewState["I-E-TO"] = "";
                ViewState["S-E-TO"] = "";
                ViewState["U-Status"] = "";
                ViewState["B-Status"] = "";
                txtitemcode.Text = Request.QueryString["itcode"];
                txtdrugcode.Text = Request.QueryString["drcode"];
                txtitemname.Text = Request.QueryString["itname"];

                if (txtitemcode.Text.Trim() != "")
                {
                    itemcodesearch();
                    DataSet dsData = Druglabelgrid.DataSource as DataSet;
                    DataTable dtData = dsData.Tables[0];
                    lblpge.Text = "Page" + "  " + Convert.ToString(Druglabelgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Druglabelgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
                    lblpge.ForeColor = System.Drawing.Color.Black;
                    lblpge.Font.Bold = false;
                    if (dtData.Rows.Count == 0)
                    {
                        lblpge.Text = "No Record Found";
                        lblpge.ForeColor = System.Drawing.Color.Green;
                        lblpge.Font.Bold = true;

                        ItemCodeCheck(txtitemcode.Text.Trim());

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
                else if (txtdrugcode.Text.Trim() != "")
                {
                    drgitnamesearch();
                    DataSet dsData = Druglabelgrid.DataSource as DataSet;
                    DataTable dtData = dsData.Tables[0];
                    lblpge.Text = "Page" + "  " + Convert.ToString(Druglabelgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Druglabelgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
                    lblpge.ForeColor = System.Drawing.Color.Black;
                    lblpge.Font.Bold = false;
                    if (dtData.Rows.Count == 0)
                    {
                        lblpge.Text = "No Record Found";
                        lblpge.ForeColor = System.Drawing.Color.Green;
                        lblpge.Font.Bold = true;

                        DrugCodeCheck(txtdrugcode.Text.Trim());

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
                else if (txtitemname.Text.Trim() != "")
                {
                    drgitnamesearch();
                    DataSet dsData = Druglabelgrid.DataSource as DataSet;
                    DataTable dtData = dsData.Tables[0];
                    lblpge.Text = "Page" + "  " + Convert.ToString(Druglabelgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Druglabelgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
                    lblpge.ForeColor = System.Drawing.Color.Black;
                    lblpge.Font.Bold = false;
                    if (dtData.Rows.Count == 0)
                    {
                        lblpge.Text = "No Record Found";
                        lblpge.ForeColor = System.Drawing.Color.Green;
                        lblpge.Font.Bold = true;

                        ItemNameCheck(txtitemname.Text.Trim());

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
                else
                {
                    lblpge.Text = "No Record Found";
                    lblpge.ForeColor = System.Drawing.Color.Green;
                    lblpge.Font.Bold = true;
                }
                txtitemcode.Attributes.Add("onKeyPress", "doClick2(event)");
                txtdrugcode.Attributes.Add("onKeyPress", "doClick2(event)");
                txtitemname.Attributes.Add("onKeyPress", "doClick2(event)");
                txtbrand.Attributes.Add("onKeyPress", "doClick2(event)");
                txtmfrcode.Attributes.Add("onKeyPress", "doClick2(event)");  
            }
        }
    }
    // * Search itemcode Exact search *\\
    public void itemcodesearch()
    {
        searchqry.preloadpoppupitemcode(Druglabelgrid, txtitemcode.Text.Trim(),location);
        DataSet dsData = Druglabelgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];

        if (dtData.Rows.Count == 0)
        {
            ItemCodeCheck(txtitemcode.Text.Trim());
        }
    }

    // * Search DrugCode Begin and Item Name In string Search Function * \\
    public void drgitnamesearch()
    {
        searchqry.preloadpopup(Druglabelgrid, txtdrugcode.Text.Trim(), txtitemname.Text.Trim(), location);
        DataSet dsData = Druglabelgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];

        if (dtData.Rows.Count == 0)
        {
            ItemNameCheck(txtitemname.Text.Trim());
        }
    }

    public void DrugCodeSearch()
    {
        searchqry.preloadpopup(Druglabelgrid, txtdrugcode.Text.Trim(), txtitemname.Text.Trim(), location);

        DataSet dsData = Druglabelgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];

        if (dtData.Rows.Count == 0)
        {
            DrugCodeCheck(txtdrugcode.Text.Trim());
        }
    }

    // * Brand Name Instring Search * \\
    public void brandname()
    {
        searchqry.brandname(Druglabelgrid, txtbrand.Text.Trim(), location);

        DataSet dsData = Druglabelgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];

        if (dtData.Rows.Count == 0)
        {
            BrandNameCheck(txtbrand.Text.Trim());
        }
    }

    // * MFR Code Exact Search * \\
    public void MFRCODE()
    {

        if (GS1BarcodeExtractMFRCode())
        {
            searchqry.mfrcode(Druglabelgrid, drugmfrbarcode, location);
        }
        else
        {
            searchqry.mfrcode(Druglabelgrid, txtmfrcode.Text.Trim(), location);
        }
       // searchqry.mfrcode(Druglabelgrid, txtmfrcode.Text.Trim(), location);
        DataSet dsData = Druglabelgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];

        if (dtData.Rows.Count == 0)
        {
            MFRCodeCheck(txtmfrcode.Text.Trim());
        }
    }

    public Boolean GS1BarcodeExtractMFRCode()
    {
        int barcodeLength = txtmfrcode.Text.Length;
        if (barcodeLength > 0)
        {
            if ((txtmfrcode.Text.Trim() != "") && (checkGS1Barcode(txtmfrcode.Text.Trim())))
            {

                int returnValue = gs1barcode.GS1CodeCheck(txtmfrcode.Text.Trim(), ref ExpDate, ref BatchNumber, ref SerialNumber, ref ManufacturedDate);
                if (returnValue == 0)
                {
                    drugmfrbarcode = SerialNumber.Trim();
                    return true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('MFR Barcode Mismatch with a GS1 Serial Number');</script>", false);
                }

            }
        }
        return false;
    }

    public Boolean checkGS1Barcode(string GS1Barcode)
    {
        bool SI = false;
        int barcodelength = GS1Barcode.Length;
        if ((GS1Barcode != "") && (barcodelength > 3))
        {
            SymbolIdentifier = GS1Barcode.Substring(0, 3);
            if ((SymbolIdentifier == "]E0") || (SymbolIdentifier == "]E1") || (SymbolIdentifier == "]E2") || (SymbolIdentifier == "]E3") || (SymbolIdentifier == "]E4") ||
                         (SymbolIdentifier == "]I1") || (SymbolIdentifier == "]C1") || (SymbolIdentifier == "]e0") || (SymbolIdentifier == "]e1") || (SymbolIdentifier == "]e2") ||
                         (SymbolIdentifier == "]d1") || (SymbolIdentifier == "]d2") || (SymbolIdentifier == "]Q3"))
            {
                return SI = true;
            }
        }
        else
        {

        }
        return SI;
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
            searchquery();
            DataSet dsData = Druglabelgrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(Druglabelgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Druglabelgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.ForeColor = System.Drawing.Color.Black;
            lblpge.Font.Bold = false;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.ForeColor = System.Drawing.Color.Green;
                lblpge.Font.Bold = true;

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
            searchqry.preloadpoppupitemcode(Druglabelgrid, "", location);
            DataSet dsData = Druglabelgrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(Druglabelgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Druglabelgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.ForeColor = System.Drawing.Color.Black;
            lblpge.Font.Bold = false;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.ForeColor = System.Drawing.Color.Green;
                lblpge.Font.Bold = true;
            }
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Only one field entry is allowed');</script>", false);
        }
    }

    public void searchquery()
    {
        if (txtitemcode.Text.Trim() != "")
        {
            itemcodesearch();
        }
        else if (txtdrugcode.Text.Trim() != "")
        {
            DrugCodeSearch();
        }
        else if (txtitemname.Text.Trim() != "")
        {
            drgitnamesearch();
        }
        else if (txtbrand.Text.Trim() != "")
        {
            brandname();
        }
        else if (txtmfrcode.Text != "" && txtbrand.Text.Trim() == "" && txtitemname.Text.Trim() == "" && txtdrugcode.Text.Trim() == "" && txtitemcode.Text.Trim() == "")
        {
         MFRCODE();
        }
        else
        {
            searchqry.preloadpoppupitemcode(Druglabelgrid, "", location);
        }
    }
    //protected void btnclear_Click(object sender, EventArgs e)
    protected void btnclear_Click(object sender, ImageClickEventArgs e)
    {
        clear();
    }
    // * Textbox Clear Function *\\
    public void clear()
    {
        txtitemcode.Text = "";
        txtdrugcode.Text = "";
        txtitemname.Text = "";
        txtbrand.Text = "";
        txtmfrcode.Text = "";
    }
    protected void Druglabelgrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if ((e.Row.RowType == DataControlRowType.DataRow))
        //{
        //    int Drgid = (int)Druglabelgrid.DataKeys[e.Row.RowIndex].Value;
        //    ((Button)e.Row.FindControl("btnSelect")).Attributes.Add("onclick", "javascript:passValueToParent('" + Drgid + "')");
        //}
    }
    protected void Druglabelgrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        searchquery();
        Druglabelgrid.PageIndex = e.NewPageIndex;
        Druglabelgrid.DataBind();
        DataSet dsData = Druglabelgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(Druglabelgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Druglabelgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
        }
    }
    protected void Druglabelgrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Btn")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = Druglabelgrid.Rows[index];
            int Drgid = (int)Druglabelgrid.DataKeys[selectedRow.RowIndex].Value;
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popup","passValueToParent('" + Drgid + "')", true);
        }
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
}