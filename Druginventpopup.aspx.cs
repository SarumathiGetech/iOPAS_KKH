using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Datalayer;
using System.Drawing;
using System.Data.SqlClient;

public partial class Druginventpopup : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    Searchquery searchqry = new Searchquery();

    string location = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Userid"] == null)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "popup", "closepopup()", true);
        }
        else
        {
            location = Session["drglocation"].ToString();
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
                    DataSet dsData = Druginvepop.DataSource as DataSet;
                    DataTable dtData = dsData.Tables[0];
                    lblpge.Text = "Page" + "  " + Convert.ToString(Druginvepop.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Druginvepop.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
                    if (dtData.Rows.Count == 0)
                    {
                        lblpge.Text = "No Record Found";
                        lblpge.Font.Bold = true;
                        lblpge.ForeColor = Color.Green;

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
                    DataSet dsData = Druginvepop.DataSource as DataSet;
                    DataTable dtData = dsData.Tables[0];
                    lblpge.Text = "Page" + "  " + Convert.ToString(Druginvepop.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Druginvepop.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
                    lblpge.ForeColor = System.Drawing.Color.Black;
                    lblpge.Font.Bold = false;
                    if (dtData.Rows.Count == 0)
                    {
                        lblpge.Text = "No Record Found";
                        lblpge.Font.Bold = true;
                        lblpge.ForeColor = Color.Green;

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
                    DataSet dsData = Druginvepop.DataSource as DataSet;
                    DataTable dtData = dsData.Tables[0];
                    lblpge.Text = "Page" + "  " + Convert.ToString(Druginvepop.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Druginvepop.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
                    lblpge.ForeColor = System.Drawing.Color.Black;
                    lblpge.Font.Bold = false;
                    if (dtData.Rows.Count == 0)
                    {
                        lblpge.Text = "No Record Found";
                        lblpge.Font.Bold = true;
                        lblpge.ForeColor = Color.Green;

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
                    lblpge.Font.Bold = true;
                    lblpge.ForeColor = Color.Green;  
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
        searchqry.Druginvpoppupitemcode(Druginvepop, txtitemcode.Text.Trim(), location);
        DataSet dsData = Druginvepop.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        if (dtData.Rows.Count == 0)
        {
            ItemCodeCheck(txtitemcode.Text.Trim());
        }
    }

    // * Search DrugCode Begin and Item Name In string Search Function * \\
    public void drgitnamesearch()
    {
        searchqry.Druginvpopup(Druginvepop, txtdrugcode.Text.Trim(), txtitemname.Text.Trim(), location);

        DataSet dsData = Druginvepop.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        if (dtData.Rows.Count == 0)
        {
            if (txtdrugcode.Text.Trim() != "")
            {
                DrugCodeCheck(txtdrugcode.Text.Trim());
            }
            else if (txtitemname.Text.Trim() != "")
            {
                ItemNameCheck(txtitemname.Text.Trim());
            }
            
        }
    }

    // * Brand Name Instring Search * \\
    public void brandname()
    {
        searchqry.Druginvbrandname(Druginvepop, txtbrand.Text.Trim(), location);
        DataSet dsData = Druginvepop.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        if (dtData.Rows.Count == 0)
        {
            BrandNameCheck(txtbrand.Text.Trim());
        }
    }

    // * MFR Code Exact Search * \\
    public void MFRCODE()
    {
        searchqry.Druginvmfrcode(Druginvepop, txtmfrcode.Text.Trim(), location);

        DataSet dsData = Druginvepop.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        if (dtData.Rows.Count == 0)
        {
            MFRCodeCheck(txtmfrcode.Text.Trim());
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
            searchquery();
            DataSet dsData = Druginvepop.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(Druginvepop.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Druginvepop.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.ForeColor = System.Drawing.Color.Black;
            lblpge.Font.Bold = false;
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
            searchqry.preloadpoppupitemcode(Druginvepop, "", location);
            DataSet dsData = Druginvepop.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(Druginvepop.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Druginvepop.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.ForeColor = System.Drawing.Color.Black;
            lblpge.Font.Bold = false;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = Color.Green;
            }
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Only one field entry is allowed');</script>", false);
        }
    }

    // Search Queary \\
     public void searchquery()
     {
         if (txtitemcode.Text.Trim() != "")
         {
             itemcodesearch();
         }
         else if (txtdrugcode.Text.Trim() != "")
         {
             drgitnamesearch();
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
             searchqry.preloadpoppupitemcode(Druginvepop, "", location);
         }
     }
    //protected void Batchpopgrid_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if ((e.Row.RowType == DataControlRowType.DataRow))
    //    {
    //        ((Button)e.Row.FindControl("btnSelect")).Attributes.Add("onclick", "javascript:passValueToParent('" + e.Row.Cells[1].Text + "')");
    //    }
    //}

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
        searchqry.preloadpoppupitemcode(Druginvepop, "", location);
        DataSet dsData = Druginvepop.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(Druginvepop.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Druginvepop.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = System.Drawing.Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
    protected void Druginvepop_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       //if ((e.Row.RowType == DataControlRowType.DataRow))
       //{
       //    int Drgid = (int)Druginvepop.DataKeys[e.Row.RowIndex].Value;
       //    ((Button)e.Row.FindControl("btnSelect")).Attributes.Add("onclick", "javascript:passValueToParent('" + Drgid + "')");
       //}
    }


    protected void Druginvepop_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Druginvepop_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        searchquery();
        Druginvepop.PageIndex = e.NewPageIndex;
        Druginvepop.DataBind();
        DataSet dsData = Druginvepop.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(Druginvepop.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Druginvepop.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
    }
    protected void Druginvepop_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Btn")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = Druginvepop.Rows[index];
            int Drgid = (int)Druginvepop.DataKeys[selectedRow.RowIndex].Value;
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popup", "passValueToParent('" + Drgid + "')", true);
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
            string Commt = "Select i.Item_Effective_date_To,il.Store_Effective_Date_To,u.Status,ba.Status from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where   i.Item_Code='" + ItemCode + "' and p.Location_Name='" + location + "'  ";
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
            string Commt = "Select i.Item_Effective_date_To ,il.Store_Effective_Date_To,u.Status,ba.Status  from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where   i.Drug_Code ='" + DrugCode + "'  and p.Location_Name='" + location + "' ";
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
            string Commt = "Select i.Item_Effective_date_To ,il.Store_Effective_Date_To,u.Status,ba.Status  from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where   i.Item_Name ='" + ItemName + "'  and p.Location_Name='" + location + "'  ";
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
            string Commt = "Select i.Item_Effective_date_To ,il.Store_Effective_Date_To,u.Status,ba.Status from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where  b.brandname like '" + BrandName + "%' and p.Location_Name='" + location + "' ";
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
            string Commt = "Select i.Item_Effective_date_To ,il.Store_Effective_Date_To,u.Status,ba.Status from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join MFR_Barcode as m on m.Brandid =b.BrandID and m.Masterid=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where m.Mfrbarcode='" + MFRCode + "' and p.Location_Name='" + location + "'";
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