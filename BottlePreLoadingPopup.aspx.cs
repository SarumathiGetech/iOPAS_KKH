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
using System.Globalization;
using System.IO;

public partial class BottlePreLoadingPopup : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    BDSBottle BDSsearchqry = new BDSBottle();
    Searchquery searchqry = new Searchquery();
    GS1BarcodeFunction gs1barcode = new GS1BarcodeFunction();
    string location = "";

    // GS1 Barcode Declaration
    string SymbolIdentifier = "";
    string GTIN = "";
    string Format = "";
    string ExpDate = "", BatchNumber = "", SerialNumber = "", ManufacturedDate = "";
    string mfrBarcode = "";
    int rtnValue = 0;
    bool haveMFRCODE = false;
    string bottlepreloadmfrbarcode = "";
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
                //gs1Table.Visible = false;
                Fsttd.Visible = true;
                secondtd.Visible = true;

                txtmfrbarcode.Attributes.Add("onKeyPress", "doClick(event)");
                txtitemcode.Attributes.Add("onKeyPress", "doClick2(event)");
                txtdrugcode.Attributes.Add("onKeyPress", "doClick2(event)");
                txtitemname.Attributes.Add("onKeyPress", "doClick2(event)");
                txtbrand.Attributes.Add("onKeyPress", "doClick2(event)");
                txtmfrcode.Attributes.Add("onKeyPress", "doClick2(event)");
                //txtgs1barcode.Attributes.Add("onKeyPress", "doClick3(event)");
               // btngs1barcodecancel.Attributes.Add("onKeyPress", "doClick4(event)");
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
        {
            // * Search itemcode Exact search *\\
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
            if (GS1BarcodeExtractMFRCode())
            {
                BDSsearchqry.BDSmfrcode(preloadpopgrid, bottlepreloadmfrbarcode, location);
            }
            else
            {
                BDSsearchqry.BDSmfrcode(preloadpopgrid, txtmfrcode.Text.Trim(), location);
            }
           
        }
        else
        {
            BDSsearchqry.BDSpreloadpoppupitemcode(preloadpopgrid, "", location);
        }

    }

    public Boolean GS1BarcodeExtractMFRCode()
    {
        int barcodeLength = txtmfrcode.Text.Length;
        bool gs1bol = false;
        if (barcodeLength > 0)
        {
            if ((txtmfrcode.Text.Trim() != "") && (checkGS1Barcode(txtmfrcode.Text.Trim())))
            {

                int returnValue = gs1barcode.GS1CodeCheck(txtmfrcode.Text.Trim(), ref ExpDate, ref BatchNumber, ref SerialNumber, ref ManufacturedDate);
                if (returnValue == 0)
                {
                    bottlepreloadmfrbarcode = SerialNumber.Trim();
                    gs1bol = true;
                }
                else{
                    gs1bol = false;
                }
            }
        }
        return gs1bol;
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
                //gs1Table.Visible = true;
                //Fsttd.Visible = false;
                //secondtd.Visible = false;
                //txtgs1barcode.Text = "";
                //txtgs1barcode.Focus();
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
                            mfrBarcode = dr[0].ToString();
                            return bol;
                        }
                        else if (dr[0].ToString().ToLower() == SerialNumber.Trim().ToLower())
                        {
                            bol = true;
                            mfrBarcode = dr[0].ToString();
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
        
        int barcodeLength = txtmfrbarcode.Text.Length;
        if (barcodeLength > 0)
        {
            if ((txtmfrbarcode.Text.Trim() != "") && (checkGS1Barcode(txtmfrbarcode.Text)))
            {
                int returnValue = gs1barcode.GS1CodeCheck(txtmfrbarcode.Text.Trim(), ref ExpDate, ref BatchNumber, ref SerialNumber, ref ManufacturedDate);
                ViewState["ExpDate"] = ExpDate.ToString();
                ViewState["BatchNumber"] = BatchNumber.ToString();
                ViewState["SerialNumber"] = SerialNumber.ToString();
                if (returnValue == 1)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('String doesn't contains any value after GTIN,Please check the Barcode string');</script>", false);
                    txtmfrbarcode.Text = "";
                }
                else if (returnValue == 2)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('String not belongs to GTIN AI,Please check the Barcode string');</script>", false);
                    txtmfrbarcode.Text = "";
                }
                else if (returnValue == 3)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Not a Valid GS1 Barcode,Please check the Barcode string');</script>", false);
                    txtmfrbarcode.Text = "";
                }
                else if (returnValue == 0)
                {
                    haveMFRCODE = MFRcodecheck(Convert.ToInt32(ViewState["Drgid"].ToString()));
                    if (haveMFRCODE)
                    {
                        if ((SerialNumber != "") && (SerialNumber.ToUpper() == mfrBarcode.ToUpper()))
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popup", "passValuesToParent('" + Convert.ToInt32(ViewState["Drgid"].ToString()) + "','" + Convert.ToInt32(ViewState["Carttype"].ToString()) + "','" + ViewState["BoxOrPallet"].ToString() + "','" + (ViewState["ExpDate"].ToString()) + "','" + (ViewState["BatchNumber"].ToString()) + "')", true);
                        }
                    }
                    else
                    {
                        threetd.Visible = false;
                        //gs1Table.Visible = false;
                        Fsttd.Visible = true;
                        secondtd.Visible = true;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('MFR Barcode Mismatch with a GS1 Serial Number');</script>", false);
                    }

                }
            }
            else if ((!checkGS1Barcode(txtmfrbarcode.Text)) && (MFRcodecheck(Convert.ToInt32(ViewState["Drgid"].ToString()))))
            {
                haveMFRCODE = MFRcodecheck(Convert.ToInt32(ViewState["Drgid"].ToString()));
                if (haveMFRCODE)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popup", "passValueToParent('" + Convert.ToInt32(ViewState["Drgid"].ToString()) + "','" + Convert.ToInt32(ViewState["Carttype"].ToString()) + "','" + ViewState["BoxOrPallet"].ToString() + "')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('MFR Barcode Not Matched with a Configuration');</script>", false);
                }
            }
            else
            {
                threetd.Visible = false;
                //gs1Table.Visible = false;
                Fsttd.Visible = true;
                secondtd.Visible = true;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Its not a GS1 Barcode and MFRBarcode Mismatch');</script>", false);

            }
        }
       
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
    //protected void btngs1barcodesearch_Click(object sender, ImageClickEventArgs e)
    //{

    //    int barcodeLength = txtgs1barcode.Text.Length;
    //    if (barcodeLength > 0)
    //    {
    //        if ((txtgs1barcode.Text.Trim() != "") && (checkGS1Barcode(txtgs1barcode.Text.Trim())))
    //        {

    //            int returnValue = gs1barcode.GS1CodeCheck(txtgs1barcode.Text.Trim(), ref ExpDate, ref BatchNumber, ref SerialNumber, ref ManufacturedDate);
    //            ViewState["ExpDate"] = ExpDate.ToString();
    //            ViewState["BatchNumber"] = BatchNumber.ToString();
    //            ViewState["SerialNumber"] = SerialNumber.ToString();
    //            if (returnValue == 0)
    //            {
    //                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popup", "passValuesToParent('" + Convert.ToInt32(ViewState["Drgid"].ToString()) + "','" + Convert.ToInt32(ViewState["Carttype"].ToString()) + "','" + ViewState["BoxOrPallet"].ToString() + "','" + (ViewState["ExpDate"].ToString()) + "','" + (ViewState["BatchNumber"].ToString()) + "')", true);
    //                txtgs1barcode.Text = "";
    //            }
    //            else if (returnValue == 1)
    //            {
    //                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('String doesn't contains any value after GTIN,Please check the Barcode string');</script>", false);
    //                txtgs1barcode.Text = "";
    //            }
    //            else if (returnValue == 2)
    //            {
    //                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('String not belongs to GTIN AI,Please check the Barcode string');</script>", false);
    //                txtgs1barcode.Text = "";
    //            }
    //            else if (returnValue == 3)
    //            {
    //                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Not a Valid GS1 Barcode,Please check the Barcode string');</script>", false);
    //                txtgs1barcode.Text = "";
    //            }
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popup", "passValueToParent('" + Convert.ToInt32(ViewState["Drgid"].ToString()) + "','" + Convert.ToInt32(ViewState["Carttype"].ToString()) + "','" + ViewState["BoxOrPallet"].ToString() + "')", true);
    //            // ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Not a Valid GS1 Barcode');</script>", false);
    //            threetd.Visible = false;
    //            gs1Table.Visible = false;
    //            Fsttd.Visible = true;
    //            secondtd.Visible = true;
    //        }
    //    }
        
    //}

    //protected void btngs1barcodecancel_Click(object sender, ImageClickEventArgs e)
    //{
    //ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popup", "passValueToParent('" + Convert.ToInt32(ViewState["Drgid"].ToString()) + "','" + Convert.ToInt32(ViewState["Carttype"].ToString()) + "','" + ViewState["BoxOrPallet"].ToString() + "')", true);
    //threetd.Visible = false;
    //gs1Table.Visible = false;
    //Fsttd.Visible = true;
    //}
}