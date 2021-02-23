using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Datalayer;
using System.Drawing;

public partial class brandmaster : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    Drug drg = new Drug();
    GS1BarcodeFunction gs1barcode = new GS1BarcodeFunction();
    string sessionuserid="", mfrbarcode="", activestatus = "";
    //public static string gridbrandid = "";    
    // GS1 Barcode Declaration
    string SymbolIdentifier = "";
    string GTIN = "";
    string Format = "";
    string ExpDate = "", BatchNumber = "", SerialNumber = "", ManufacturedDate = "";
    string mfrBarcode = "";
    int rtnValue = 0; int defaultval = 0;
    bool haveGS1Barcode = false;



    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Userid"] != null)
        {
            sessionuserid = Session["Userid"].ToString();
            if (!IsPostBack)
            {
                txtitemcode.Text = Request.QueryString["itcode"];
                txtitemname.Text = Request.QueryString["itname"];
                gridsearchitemcode();
                gridbrand.Visible = true;
                brandgrid.Visible = false;
                lblbrandpage.Visible = false;
                lblpge.Visible = true;
                btnbrandupd.Visible = false;
                btnsave.Visible = true;
                btnupdate.Visible = false;
                txtmfrcode.Attributes.Add("onKeyPress", "doClick(event)");
                txtbrandname.Attributes.Add("onKeyPress", "doClick(event)");
                txtbrandcode.Attributes.Add("onKeyPress", "doClick(event)");
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "popup", "closepopup()", true);
        }
    }
    //protected void btnadd_Click(object sender, EventArgs e)
    protected void btnadd_Click(object sender, ImageClickEventArgs e)
    {
        int d = 0;
        if (checkGS1Barcode(txtmfrcode.Text))
        {
            int returnValue = gs1barcode.GS1CodeCheck(txtmfrcode.Text.Trim(), ref ExpDate, ref BatchNumber, ref SerialNumber, ref ManufacturedDate);
            if(returnValue == 0) 
            { 
                txtmfrcode.Text = SerialNumber.ToString();
                if (txtmfrcode.Text.Trim() != "")
                {
                    mfrcodecheck();
                    if (mfrbarcode.Trim().ToUpper() != txtmfrcode.Text.Trim().ToUpper())
                    {
                        foreach (ListItem itemins in lstbox.Items)
                        {
                            if (itemins != null)
                            {
                                d = 1;
                                if (itemins.ToString() == txtmfrcode.Text.Trim())
                                {
                                    d = 2;
                                    txtmfrcode.Text = "";
                                    txtmfrcode.Focus();
                                    return;
                                }
                            }
                        }
                        if (d == 1)
                        {
                            lstbox.Items.Add(txtmfrcode.Text.Trim());
                            txtmfrcode.Text = "";
                            txtmfrcode.Focus();
                        }
                        else if (d == 0)
                        {
                            lstbox.Items.Add(txtmfrcode.Text.Trim());
                            txtmfrcode.Text = "";
                            txtmfrcode.Focus();
                        }
                    }
                    else if (mfrbarcode.Trim().ToUpper() == txtmfrcode.Text.Trim().ToUpper())
                    {
                        txtmfrcode.Text = "";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('MFR Barcode already exist');</script>", false);
                        txtmfrcode.Focus();
                    }

                }
            }
            else{
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Not a GS1 Barcode');</script>", false);
            }
        }
        else if(!checkGS1Barcode(txtmfrcode.Text.Trim()))
        {
            if (txtmfrcode.Text.Trim() != "")
            {
                mfrcodecheck();
                if (mfrbarcode.Trim().ToUpper() != txtmfrcode.Text.Trim().ToUpper())
                {
                    foreach (ListItem itemins in lstbox.Items)
                    {
                        if (itemins != null)
                        {
                            d = 1;
                            if (itemins.ToString() == txtmfrcode.Text.Trim())
                            {
                                d = 2;
                                txtmfrcode.Text = "";
                                txtmfrcode.Focus();
                                return;
                            }
                        }
                    }
                    if (d == 1)
                    {
                        lstbox.Items.Add(txtmfrcode.Text.Trim());
                        txtmfrcode.Text = "";
                        txtmfrcode.Focus();
                    }
                    else if (d == 0)
                    {
                        lstbox.Items.Add(txtmfrcode.Text.Trim());
                        txtmfrcode.Text = "";
                        txtmfrcode.Focus();
                    }
                }
                else if (mfrbarcode.Trim().ToUpper() == txtmfrcode.Text.Trim().ToUpper())
                {
                    txtmfrcode.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('MFR Barcode already exist');</script>", false);
                    txtmfrcode.Focus();
                }

            }
        }
        else { }
            
    }


    public Boolean checkGS1Barcode(string GS1Barcode)
    {
        bool SI = false;
        int barcodelength = GS1Barcode.Length;
        if ((GS1Barcode != "") && (barcodelength >3))
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

    //protected void Button2_Click(object sender, EventArgs e)
    protected void Button2_Click(object sender, ImageClickEventArgs e)
    {
        ListItem mfrcode = lstbox.SelectedItem;
        if (mfrcode != null)
        {
            lstbox.Items.Remove(mfrcode);
        }       
    }
    //protected void btnsave_Click(object sender, EventArgs e)
    protected void btnsave_Click(object sender, ImageClickEventArgs e)
    {             
            brandinsert();           
            gridbrand.Visible = true;
            brandgrid.Visible = false;
            gridsearchitemcode();       
    }

    protected void btnupdate_Click(object sender, ImageClickEventArgs e)
    {
        brandallotupdate();
        mfrupdate();
        mfrinsert();
        gridsearchitemcode();
        textclear();
        lblbrandpage.Visible = false;
        lblpge.Visible = true;
        txtbrandcode.ReadOnly = false;
        txtbrandname.ReadOnly = false;

        txtbrandcode.Enabled = true;
        txtbrandname.Enabled = true;
        btnsave.Visible = true;
        btnupdate.Visible = false;

        txtbrandcode.Text = "";
        txtbrandname.Text = "";
       
    }

    public void condictioncheck()
    {
        if (r.Checked == true)
        {
            defaultval = 1;
        }
        else
        {
            defaultval = 2;
        }

        if (chkactve.Checked == true)
        {
            activestatus = "Active";
        }
        else
        {
            activestatus = "Inactive";
        }
    }

    // * public void brand insert function * \\
    public void brandinsert()
    {
        condictioncheck();
       int Rtnvalue= drg.brandinsert(txtbrandcode.Text.Trim().ToUpper(), txtbrandname.Text.Trim().ToUpper(),txtitemcode.Text.Trim(),activestatus,defaultval, sessionuserid);
       if (Rtnvalue == 1)
       {
           mfrinsert();
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record created');</script>", false);
           txtbrandcode.Text = "";
           txtbrandname.Text = "";
       }
       else if (Rtnvalue == 2)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Brand code already exist');</script>", false);
       }
       else if (Rtnvalue == 3)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Brand name already exist');</script>", false);
       }
       else if (Rtnvalue == 6)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Item code and brand name already exist');</script>", false);
       }
       else
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
       }

    }   

// * Brand Update Function * \\
    public void brandallotupdate()
    {
        condictioncheck();
        int Rtnvalue = drg.brandallotupdate(txtitemcode.Text.Trim(), txtbrandcode.Text.Trim().ToUpper(), defaultval, activestatus, Session["Userid"].ToString());
        if (Rtnvalue == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated');</script>", false);
           
        }
        else if (Rtnvalue == 2)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Brand is running in DDS/BDS . Disable and remove cartridge from DDS/BDS and try again.\\nBut, MFR Code Add/Remove succeed');</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
        }
    }

    // * MFR Barcode insert function * \\
    public void mfrinsert()
    {
        foreach (ListItem itemins in lstbox.Items)
          {
            if (itemins != null)
            {
                mfrcodecheck();
                if (mfrbarcode != itemins.ToString())
                {
                    drg.Mfrinsert(txtbrandcode.Text.Trim().ToUpper(),txtitemcode.Text.Trim(),itemins.ToString());
                }
            }
         }       
    }

    // * MFR Barcode Delete While update *\\
    public void mfrupdate()
    {
        drg.Mfrdelete(txtbrandcode.Text.Trim().ToUpper(), txtitemcode.Text.Trim());
    }

    // * MFR barcode already exist or not * \\
    public void mfrcodecheck()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select mfrbarcode from MFR_Barcode where mfrbarcode='" + txtmfrcode.Text.Trim() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        mfrbarcode = dr[0].ToString();
                    }
                }
            }
        }

    }

    // * Text box clear function * \\
    public void textclear()
    {
        lstbox.Items.Clear();
        btnsave.Visible = true;
        btnupdate.Visible = false;
        txtmfrcode.Text = "";
    }
    // * Brand Allot Grid Display Function item code and brandcode * \\
    public void griddisplay()
    {
        drg.brandgrid(gridbrand, txtitemcode.Text.Trim(), txtbrandcode.Text.Trim());
    }

    // * Brand Master Grid Display * \\
    public void brandmastergrid()
    {
        if (txtbrandcode.Text.Trim() == "")
        {
            drg.brandmastergrid(brandgrid, txtbrandname.Text.Trim());
            DataSet dsData = brandgrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblbrandpage.Text = "Page" + "  " + Convert.ToString(brandgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(brandgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.ForeColor = Color.Black;
            lblpge.Font.Bold = false;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = Color.Green;
            }
        }
        else if (txtbrandcode.Text.Trim() != "")
        {

            drg.brandmastercodegrid(brandgrid, txtbrandcode.Text.Trim());
            DataSet dsData = brandgrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblbrandpage.Text = "Page" + "  " + Convert.ToString(brandgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(brandgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
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

    // * Data grid Display Function only item code * \\
    public void gridsearchitemcode()
    {
        drg.brandgriditecode(gridbrand, txtitemcode.Text.Trim());
        DataSet dsData = gridbrand.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(gridbrand.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridbrand.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
   
    protected void gridbrand_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       // griddisplay();
        gridsearchitemcode();
        gridbrand.PageIndex = e.NewPageIndex;
        gridbrand.DataBind();
        DataSet dsData = gridbrand.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(gridbrand.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridbrand.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
    protected void gridbrand_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        ViewState["gridbrandid"] = (string)gridbrand.DataKeys[e.NewSelectedIndex].Value.ToString();
        //gridbrandid = (string)gridbrand.DataKeys[e.NewSelectedIndex].Value.ToString(); 
        txtbrandcode.Text = (gridbrand.Rows[e.NewSelectedIndex].Cells[3].Text);
        txtbrandname.Text = (gridbrand.Rows[e.NewSelectedIndex].Cells[4].Text);
        if ((gridbrand.Rows[e.NewSelectedIndex].Cells[5].Text) == "Active")
        {
            chkactve.Checked = true;
        }
        else
        {
            chkactve.Checked = false;
        }
        defaultbrandreader();
        MFRbarcodereader();
        btnsave.Visible = false;
        btnupdate.Visible = true;
        txtbrandcode.ReadOnly = true;
        txtbrandname.ReadOnly = true;

        txtbrandcode.Enabled = false;
        txtbrandname.Enabled = false;

    }

    // * Default Brand Reader Function Grid Selection *\\
    public void defaultbrandreader()
    {
        r.Checked = false;
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select ba.defaultbrand from Brand_Allot as ba left join  Brand_Master as b on b.BrandID=ba.BrandID left join Item_Master as i on i.MasterID=ba.Masterid where ba.BrandID='" + Convert.ToInt32(ViewState["gridbrandid"]) + "' and i.Item_Code='" + txtitemcode.Text.Trim() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr[0].ToString() == "1")
                        {
                            r.Checked = true;
                        }
                        else
                        {
                            r.Checked = false;
                        }
                    }
                }
            }
        }
    }

    // * Default MFRBarcode Reader Function Grid Selection *\\
    public void MFRbarcodereader()
    {
        lstbox.Items.Clear();
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select m.mfrbarcode from MFR_Barcode as m left join Brand_allot as ba on ba.BrandID=m.Brandid left join Item_Master as i on i.MasterID =ba.Masterid and i.MasterID = m.Masterid left join Brand_Master  as b on b.BrandID=ba.BrandID where i.Item_Code='" + txtitemcode.Text.Trim() + "' and b.Brandcode='" + txtbrandcode.Text.Trim() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lstbox.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }

    //protected void Button3_Click(object sender, EventArgs e)
    protected void btnsearch_Click(object sender, ImageClickEventArgs e)
    {
        brandgrid.PageIndex = 0;
        gridbrand.Visible = false;
        brandgrid.Visible = true;
        int a = 0;
        if (txtbrandcode.Text.Trim() != "")
        {
            a = 1;
        }
        if (txtbrandname.Text.Trim() != "")
        {
            a = a + 1;
        }
        if (a == 0 || a == 1)
        {
            if (txtbrandcode.Text.Trim() != "")
            {
                brandmastergrid();
                lblbrandpage.Visible = true;
                lblpge.Visible = false;
            }
            else if (txtbrandcode.Text.Trim() == "")
            {
                brandmastergrid();
                lblbrandpage.Visible = true;
                lblpge.Visible = false;
            }
        }
        else if (a != 0 && a != 1)
        {
            lblpge.Visible = false;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Only one field entry is allowed');</script>", false);
        }
    }
    //protected void btnclear_Click(object sender, EventArgs e)
    protected void btnclear_Click(object sender, ImageClickEventArgs e)
    {
        gridbrand.PageIndex = 0;
        txtbrandcode.Text = "";
        txtbrandname.Text = "";
        txtmfrcode.Text = "";
        lstbox.Items.Clear();
        chkactve.Checked = false;
        r.Checked = false;
        gridsearchitemcode();
        gridbrand.Visible = true;
        brandgrid.Visible = false;
        btnbrandupd.Visible = false;
        btnsave.Visible = true;
        btnupdate.Visible = false;
        btnsave.Visible = true;
        lblbrandpage.Visible = false;
        lblpge.Visible = true;
        txtbrandcode.ReadOnly = false;
        txtbrandname.ReadOnly = false;

        txtbrandcode.Enabled = true;
        txtbrandname.Enabled = true;

        txtmfrcode.Enabled = true;
        lstbox.Enabled = true;
        chkactve.Enabled = true;
        r.Enabled = true;
        btnadd.Visible = true;
        Button2.Visible = true;
    }
    
    protected void brandgrid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
     
        ViewState["gridbrandid"] = (string)brandgrid.DataKeys[e.NewSelectedIndex].Value.ToString();        
        txtbrandcode.Text = (brandgrid.Rows[e.NewSelectedIndex].Cells[3].Text);
        txtbrandname.Text = (brandgrid.Rows[e.NewSelectedIndex].Cells[4].Text);
        btnbrandupd.Visible = true;
        btnsave.Visible = false;
        txtbrandcode.ReadOnly = false;
        txtbrandname.ReadOnly = false;
        txtbrandcode.Enabled = true ;
        txtbrandname.Enabled = true;
        txtmfrcode.Text = "";
        lstbox.Items.Clear();
        chkactve.Checked = false;
        r.Checked = false;


        txtmfrcode.Enabled = false;
        lstbox.Enabled = false;
        chkactve.Enabled = false;
        r.Enabled = false;
        btnadd.Visible = false;
        Button2.Visible = false;
    }
    protected void brandgrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Btn")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = brandgrid.Rows[index];
           // TableCell contactName = selectedRow.Cells[3];
           txtbrandcode.Text = selectedRow.Cells[3].Text;
           txtbrandname.Text = selectedRow.Cells[4].Text;
           btnbrandupd.Visible = false;
           btnsave.Visible = true;
           txtbrandcode.ReadOnly = true;
           txtbrandname.ReadOnly = true;

           txtbrandcode.Enabled = false;
           txtbrandname.Enabled = false;
           txtmfrcode.Text = "";
           lstbox.Items.Clear();
           chkactve.Checked = false;
           r.Checked = false;
           btnsave.Visible = true;
           btnupdate.Visible = false;
        }
    }
    //protected void btnbrandupd_Click(object sender, EventArgs e)
    protected void btnbrandupd_Click(object sender, ImageClickEventArgs e)
    {
        int Rtnval = drg.brandupdate(txtbrandcode.Text.Trim().ToUpper(), txtbrandname.Text.Trim().ToUpper(), Convert.ToInt32(ViewState["gridbrandid"]), sessionuserid);
        if (Rtnval == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated');</script>", false);
            btnbrandupd.Visible = false;
            btnsave.Visible = true;
            btnsave.Visible = true;
            btnupdate.Visible = false;         
            brandmastergrid();
            lblbrandpage.Visible = true;
            lblpge.Visible = false;

            txtmfrcode.Enabled = true;
            lstbox.Enabled = true;
            chkactve.Enabled = true;
            r.Enabled = true;
            btnadd.Visible = true;
            Button2.Visible = true;
            txtbrandcode.Text = "";
            txtbrandname.Text = "";
        }
        else if (Rtnval == 2)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Brand code already exist');</script>", false);
        }
        else if (Rtnval == 3)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Brand name already exist');</script>", false);
        }
    }
    protected void brandgrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        brandmastergrid();
        brandgrid.PageIndex = e.NewPageIndex;
        brandgrid.DataBind();
        DataSet dsData = brandgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblbrandpage.Text = "Page" + "  " + Convert.ToString(brandgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(brandgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
    protected void gridbrand_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void brandgrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        brandmastergrid();
        DataSet dsData = brandgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = brandgrid.PageIndex;
        brandgrid.DataSource = SortDataTable(dtData, false);
        brandgrid.DataBind();
        brandgrid.PageIndex = pageIndex;     
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
    protected void gridbrand_Sorting(object sender, GridViewSortEventArgs e)
    {
        gridsearchitemcode();
        DataSet dsData = gridbrand.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = gridbrand.PageIndex;
        gridbrand.DataSource = SortDataTable(dtData, false);
        gridbrand.DataBind();
        gridbrand.PageIndex = pageIndex;    
    }
    
}