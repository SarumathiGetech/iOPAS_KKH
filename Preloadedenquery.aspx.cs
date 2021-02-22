using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data;
using Datalayer;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.html;
//using iTextSharp.text.html.simpleparser;
using System.Xml;
using System.Text;
using System.IO;
using System.Drawing;

public partial class Preloadedenquery : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    Preload pre = new Preload();
    LogWriter Log = new LogWriter();
    BDSBottle BDS = new BDSBottle();
    string sessionuserid = "",Statusval = "",Scestatus="";
    int statuscount;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Userid"] != null)
        {
            sessionuserid = Session["Userid"].ToString();
        }
        else
        {
            //Response.Redirect("opas.html?Session=End");
            Response.Redirect("iopas.html");
        }


        if (!IsPostBack)
        {
            Locationreader();
            lblpge.Visible = true;
            lblpge.Font.Bold = true;
            lblpge.ForeColor = System.Drawing.Color.Green;
            
            txtitemcode.Attributes.Add("onKeyPress", "doClick2(event)");
            txtdrugcode.Attributes.Add("onKeyPress", "doClick2(event)");
            txtitemname.Attributes.Add("onKeyPress", "doClick2(event)");
            txtbrand.Attributes.Add("onKeyPress", "doClick2(event)");
           // txtmfrcode.Attributes.Add("onKeyPress", "doClick2(event)");
            txtcartno.Attributes.Add("onKeyPress", "doClick2(event)");
           
            btncancel.Visible = false;            
        }
    }

    protected void Page_Prerender(object sender, EventArgs e)
    {
        //SaveToExcel.NavigateUrl = Page.ResolveUrl("~/EXCEL_ASHX/Pre_Loaded_Cartridge.ashx"
        //            + "?location=" + ddlpharname.SelectedValue + "&Itemcode=" + txtitemcode.Text +
        //            "&Drugcode=" + txtdrugcode.Text + "&itemname=" + txtitemname.Text + "&Brandname=" + txtbrand.Text +
        //            "&Barcode=" + txtmfrcode.Text + "&Cartridgeno=" + txtcartno.Text + "&status=" + ddlfilter.SelectedValue);
    }
  
    // * Public void Pharmacy Location Reader * \\
    public void Locationreader()
    {

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select Location_Name from pharmacy where Status='Active'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddlpharname.Items.Add(dr[0].ToString());
                    }
                }
            }
        }

    }

    //protected void btnsearch_Click(object sender, EventArgs e)
    protected void btnsearch_Click(object sender, ImageClickEventArgs e)
    {
        gridstatus.PageIndex = 0;
        int b = 0;      
        if (txtbrand.Text.Trim() != "")
        {
            b = b + 1;
        }
        if (txtcartno.Text.Trim() != "")
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
        //if (txtmfrcode.Text.Trim() != "")
        //{
        //    b = b + 1;
        //}

        if (b == 0 || b == 1)
        {
            if (ddlmachinename.SelectedValue.ToString() == "HDDS")
            {               
                btncancel.Visible = false;
                ViewState["ord"] = "order by p.Loading_Id desc";                
                griddisplay();
            }
            else if (ddlmachinename.SelectedValue.ToString() == "BDS")
            {              
                btncancel.Visible = false;

                if (ddlfilter.SelectedValue == "Carton Box WIP")
                {               
                    ViewState["ord"] = "order by p.Loading_Id desc";
                }
                else if (ddlfilter.SelectedValue == "Pending For Cartridge Allocation")
                {
                    ViewState["ord"] = "order by CartID desc";
                }
                else if (ddlfilter.SelectedValue == "Pending For Carton/Pallet BDS Loading")
                {                   
                    btncancel.Visible = true;
                    ViewState["ord"] = "order by CartID desc";
                }
                else if (ddlfilter.SelectedValue == "Pending For Carton/Pallet First Verification")
                {                   
                    btncancel.Visible = true;
                    ViewState["ord"] = "order by CartID desc";
                }
                else if (ddlfilter.SelectedValue == "Bottle Unloading")
                {
                    ViewState["ord"] = "order by UnloadingID desc";
                }      

                BDSgriddisplay();
            }
         
        }
        else if (b != 0 && b != 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Only one field entry is allowed');</script>", false);
        }      
    }

    //protected void btnclear_Click(object sender, EventArgs e)
    protected void btnclear_Click(object sender, ImageClickEventArgs e)
    {
        txtcartno.Text="";
        txtbrand.Text="";
        txtdrugcode.Text="";
        txtitemname.Text = "";        
        txtitemcode.Text = "";
        btncancel.Visible = false;
        BDSGrid.Visible = false;
        gridstatus.Visible = false;
        BotUnloadingGrid.Visible = false;

        pre.cartstatusitemcodeall(gridstatus, "", "","");
        DataSet dsData = gridstatus.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(gridstatus.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridstatus.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.Font.Bold = false;
        lblpge.ForeColor = System.Drawing.Color.Black;
        if (dtData.Rows.Count == 0)
        {           
            lblpge.Visible = true;
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = System.Drawing.Color.Green;
        }
    }


    //protected void btnprint_Click(object sender, ImageClickEventArgs e)
    //{
    //    Response.Redirect("EXCEL_ASHX/Pre_Loaded_Cartridge.ashx?location=" + ddlpharname.SelectedValue + "&Itemcode=" + txtitemcode.Text +
    //               "&Drugcode=" + txtdrugcode.Text + "&itemname=" + txtitemname.Text + "&Brandname=" + txtbrand.Text +
    //               "&Barcode=" + txtmfrcode.Text + "&Cartridgeno=" + txtcartno.Text + "&status=" + ddlfilter.SelectedValue);        
    //}

    
    public void Excel()
    {        
        HtmlForm form = new HtmlForm();
        form.Controls.Add(gridstatus);
        string attachment = "attachment; filename=Cartridge Enquiry.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter stw = new StringWriter();
        HtmlTextWriter htextw = new HtmlTextWriter(stw);
        form.Controls.Add(gridstatus);
        this.Controls.Add(form);
        form.RenderControl(htextw);
        Response.Write(stw.ToString());
        Response.End();
    }

    protected void gridstatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        griddisplay();  
        gridstatus.PageIndex = e.NewPageIndex;
        gridstatus.DataBind();
        DataSet dsData = gridstatus.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(gridstatus.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridstatus.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.Font.Bold = false;
        lblpge.ForeColor = System.Drawing.Color.Black;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Visible = true;
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = System.Drawing.Color.Green;
        }      
    }

    // * Loaded cartridge Status Count all * \\
    public void loadedcartridecountall()
    {

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select COUNT(*) from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Verification_Rejected as r on p.loading_id=r.loading_id left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2' and ph.Location_Name='" + ddlpharname.SelectedValue.ToString() + "' and P.Activation_Status='1'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        statuscount = Convert.ToInt32(dr[0].ToString());
                    }
                }
            }
        }
    }

    // * Loaded cartridge Status Count using filter * \\
    public void loadedcartridecount()
    {
        if (Statusval != "Both")
        {
            using (SqlConnection con = DBCon.getstring())
            {
                string Commt = "Select COUNT(*) from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Verification_Rejected as r on p.loading_id=r.loading_id left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2' and  p.Verified_Status='" + Statusval + "'  and ph.Location_Name='" + ddlpharname.SelectedValue.ToString() + "' and P.Activation_Status='1' ";
                cmd = new SqlCommand(Commt, con);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            statuscount = Convert.ToInt32(dr[0].ToString());
                        }
                    }
                }
            }
        }
        else if (Statusval == "Both")
        {

            using (SqlConnection con = DBCon.getstring())
            {
                string Commt = "Select COUNT(*) from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Verification_Rejected as r on p.loading_id=r.loading_id left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2' and  p.Verified_Status<>'Loaded' and p.Verified_Status<>'Unloading'  and ph.Location_Name='" + ddlpharname.SelectedValue.ToString() + "' and P.Activation_Status='1' ";
                cmd = new SqlCommand(Commt, con);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            statuscount = Convert.ToInt32(dr[0].ToString());
                        }
                    }
                }
            }
        }
    }


    // * Second Verification Required Status Check * \\
    public void secverificationstatuscheck()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select Second_Verification from Loading_Master as l left join Pharmacy as ph on ph.PharmacyID=l.PharmacyID where ph.Location_Name='" + ddlpharname.SelectedValue.ToString() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Scestatus = dr[0].ToString();
                    }
                }
            }
        }
    }


    // Grid Display Function * \\
    public void griddisplay()
    {
        BDSGrid.Visible = false;
        gridstatus.Visible = true;
        BotUnloadingGrid.Visible = false;
        secverificationstatuscheck();

        if (ddlfilter.SelectedValue == "Pending First Verification")
        {
            Statusval = "Loaded";
        }
        else if (ddlfilter.SelectedValue == "Pending Second Verification")
        {
            Statusval = "First Verified";
        }
        else if (ddlfilter.SelectedValue == "Pending For DDS Loading")
        {
            if (Scestatus == "1")
            {
                Statusval = "Second Verified";
            }
            else if (Scestatus == "2")
            {
                Statusval = "Both";
            }
        }
        else if (ddlfilter.SelectedValue == "All" && txtitemcode.Text != "")
        {
            pre.cartstatusitemcodeall(gridstatus, txtitemcode.Text.Trim(), ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = gridstatus.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(gridstatus.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridstatus.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
            }
        }
        else if (ddlfilter.SelectedValue == "All" && txtitemcode.Text == "" && txtdrugcode.Text != "")
        {
            pre.cartstatusdrugall(gridstatus, txtdrugcode.Text.Trim(), ddlpharname.SelectedItem.ToString(),ViewState["ord"].ToString());
            DataSet dsData = gridstatus.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(gridstatus.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridstatus.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
            }
        }
        else if (ddlfilter.SelectedValue == "All" && txtitemcode.Text == "" && txtitemname.Text.Trim() != "")
        {
            pre.cartstatusnameall(gridstatus, txtitemname.Text.Trim(), ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = gridstatus.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(gridstatus.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridstatus.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
            }
        }
        else if (ddlfilter.SelectedValue == "All" && txtitemcode.Text == "" && txtbrand.Text.Trim() != "")
        {
            pre.cartstatusbrandall(gridstatus, txtbrand.Text.Trim(), ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = gridstatus.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(gridstatus.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridstatus.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
            }
        }    

        else if (ddlfilter.SelectedValue == "All" && txtitemcode.Text == "" && txtcartno.Text.Trim() != "")
        {
            pre.cartstatuscartnoall(gridstatus, txtcartno.Text.Trim(), ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = gridstatus.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(gridstatus.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridstatus.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
            }
        }


        if (txtitemcode.Text != "" && ddlfilter.SelectedValue != "All")
        {
            pre.cartstatusitemcode(gridstatus, txtitemcode.Text.Trim(), Statusval, ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = gridstatus.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(gridstatus.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridstatus.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
            }
        }
        else if (txtitemcode.Text == "" && ddlfilter.SelectedValue != "All" && txtdrugcode.Text != "")
        {
            pre.cartstatusdrug(gridstatus, txtdrugcode.Text, Statusval, ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = gridstatus.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(gridstatus.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridstatus.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
            }
        }
        else if (txtitemcode.Text == "" && ddlfilter.SelectedValue != "All" && txtitemname.Text.Trim() != "")
        {
            pre.cartstatusname(gridstatus, txtitemname.Text.Trim(), Statusval, ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = gridstatus.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(gridstatus.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridstatus.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
            }
        }
        else if (txtitemcode.Text == "" && ddlfilter.SelectedValue != "All" && txtbrand.Text.Trim() != "")
        {
            pre.cartstatusbrand(gridstatus, txtbrand.Text.Trim(), Statusval, ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = gridstatus.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(gridstatus.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridstatus.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
            }
        }  
        else if (txtitemcode.Text == "" && ddlfilter.SelectedValue != "All" && txtcartno.Text.Trim() != "")
        {
            pre.cartstatuscartno(gridstatus, txtcartno.Text.Trim(), Statusval, ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = gridstatus.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(gridstatus.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridstatus.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
            }
        }
        else if (txtbrand.Text == "" && txtcartno.Text == "" && txtdrugcode.Text == "" && txtitemcode.Text == "" && txtitemname.Text == "" && ddlfilter.SelectedValue.ToString() == "All")
        {
            loadedcartridecountall();
            if (statuscount <= 100)
            {
                pre.cartstatusnameall(gridstatus, txtitemname.Text.Trim(), ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
                DataSet dsData = gridstatus.DataSource as DataSet;
                DataTable dtData = dsData.Tables[0];
                lblpge.Text = "Page" + "  " + Convert.ToString(gridstatus.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridstatus.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
                lblpge.Font.Bold = false;
                lblpge.ForeColor = System.Drawing.Color.Black;
                if (dtData.Rows.Count == 0)
                {
                    lblpge.Visible = true;
                    lblpge.Text = "No Record Found";
                    lblpge.Font.Bold = true;
                    lblpge.ForeColor = System.Drawing.Color.Green;
                }
            }
            else if (statuscount > 100)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Selected Record is " + statuscount + " use search parameter');</script>", false);
            }
        }
        else if (txtbrand.Text == "" && txtcartno.Text == "" && txtdrugcode.Text == "" && txtitemcode.Text == "" && txtitemname.Text == "" )
        {
            loadedcartridecount();
            if (statuscount <= 100)
            {
                pre.cartstatusname(gridstatus, txtitemname.Text.Trim(), Statusval, ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
                DataSet dsData = gridstatus.DataSource as DataSet;
                DataTable dtData = dsData.Tables[0];
                lblpge.Text = "Page" + "  " + Convert.ToString(gridstatus.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridstatus.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
                lblpge.Font.Bold = false;
                lblpge.ForeColor = System.Drawing.Color.Black;
                if (dtData.Rows.Count == 0)
                {
                    lblpge.Visible = true;
                    lblpge.Text = "No Record Found";
                    lblpge.Font.Bold = true;
                    lblpge.ForeColor = System.Drawing.Color.Green;
                }
            }
            else if (statuscount > 100)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Selected Record is " + statuscount + " use search parameter');</script>", false);
            }
        }
       
    }

    public void BDSgriddisplay()
    {
        BDSGrid.Visible = true ;
        gridstatus.Visible = false;
        BotUnloadingGrid.Visible = false; 

        if (ddlfilter.SelectedValue == "Carton Box WIP" && txtitemcode.Text != "")
        {
            pre.BDS_cartstatusitemcode_WIP(BDSGrid, txtitemcode.Text.Trim(), ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = gridstatus.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(gridstatus.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridstatus.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
            }
        }
        else if (ddlfilter.SelectedValue == "Carton Box WIP" && txtitemcode.Text == "" && txtdrugcode.Text != "")
        {
            pre.BDS_cartstatusdrugcode_WIP(BDSGrid, txtdrugcode.Text.Trim(), ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = BDSGrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(BDSGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(BDSGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
            }
        }
        else if (ddlfilter.SelectedValue == "Carton Box WIP" && txtitemcode.Text == "" && txtitemname.Text.Trim() != "")
        {
            pre.BDS_cartstatusname_WIP(BDSGrid, txtitemname.Text.Trim(), ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = BDSGrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(BDSGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(BDSGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
            }
        }
        else if (ddlfilter.SelectedValue == "Carton Box WIP" && txtitemcode.Text == "" && txtbrand.Text.Trim() != "")
        {
            pre.BDS_cartstatusbrand_WIP(BDSGrid, txtbrand.Text.Trim(), ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = BDSGrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(BDSGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(BDSGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
            }
        }
     

        else if (ddlfilter.SelectedValue == "Carton Box WIP" && txtitemcode.Text == "" && txtcartno.Text.Trim() != "")
        {
            pre.BDS_cartstatuscartno_WIP(BDSGrid, txtcartno.Text.Trim(), ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = BDSGrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(BDSGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(BDSGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
            }
        }
        else if  (ddlfilter.SelectedValue == "Carton Box WIP" )
        {
            pre.BDS_cartstatus_No_Filter_WIP(BDSGrid, ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = BDSGrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(BDSGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(BDSGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
            }
        }


        /// **********************

       
        if (ddlfilter.SelectedValue == "Pending For Carton/Pallet BDS Loading" && txtitemcode.Text != "")
        {
            pre.BDS_cartstatusitemcode_PreLoad(BDSGrid, txtitemcode.Text.Trim(), ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = gridstatus.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(gridstatus.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridstatus.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
              
                btncancel.Visible = false;
            }
        }
        else if (ddlfilter.SelectedValue == "Pending For Carton/Pallet BDS Loading" && txtitemcode.Text == "" && txtdrugcode.Text != "")
        {
            pre.BDS_cartstatusdrugcode_PreLoad(BDSGrid, txtdrugcode.Text.Trim(), ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = BDSGrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(BDSGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(BDSGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;             
                btncancel.Visible = false;
            }
        }
        else if (ddlfilter.SelectedValue == "Pending For Carton/Pallet BDS Loading" && txtitemcode.Text == "" && txtitemname.Text.Trim() != "")
        {
            pre.BDS_cartstatusname_PreLoad(BDSGrid, txtitemname.Text.Trim(), ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = BDSGrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(BDSGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(BDSGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;             
                btncancel.Visible = false;
            }
        }
        else if (ddlfilter.SelectedValue == "Pending For Carton/Pallet BDS Loading" && txtitemcode.Text == "" && txtbrand.Text.Trim() != "")
        {
            pre.BDS_cartstatusbrand_PreLoad(BDSGrid, txtbrand.Text.Trim(), ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = BDSGrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(BDSGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(BDSGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;              
                btncancel.Visible = false;
            }
        }

        else if (ddlfilter.SelectedValue == "Pending For Carton/Pallet BDS Loading")
        {
            pre.BDS_cartstatus_No_Filter_PreLoad(BDSGrid, ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = BDSGrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(BDSGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(BDSGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
             
                btncancel.Visible = false;
            }
        }




        /// **********************


        if (ddlfilter.SelectedValue == "Pending For Carton/Pallet First Verification" && txtitemcode.Text != "")
        {
            pre.BDS_cartstatusitemcode_First_Verfication(BDSGrid, txtitemcode.Text.Trim(), ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = gridstatus.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(gridstatus.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridstatus.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;            
                btncancel.Visible = false;
            }
        }
        else if (ddlfilter.SelectedValue == "Pending For Carton/Pallet First Verification" && txtitemcode.Text == "" && txtdrugcode.Text != "")
        {
            pre.BDS_cartstatusdrugcode_First_Verfication(BDSGrid, txtdrugcode.Text.Trim(), ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = BDSGrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(BDSGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(BDSGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;             
                btncancel.Visible = false;
            }
        }
        else if (ddlfilter.SelectedValue == "Pending For Carton/Pallet First Verification" && txtitemcode.Text == "" && txtitemname.Text.Trim() != "")
        {
            pre.BDS_cartstatusname_First_Verfication(BDSGrid, txtitemname.Text.Trim(), ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = BDSGrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(BDSGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(BDSGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;             
                btncancel.Visible = false;
            }
        }
        else if (ddlfilter.SelectedValue == "Pending For Carton/Pallet First Verification" && txtitemcode.Text == "" && txtbrand.Text.Trim() != "")
        {
            pre.BDS_cartstatusbrand_First_Verfication(BDSGrid, txtbrand.Text.Trim(), ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = BDSGrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(BDSGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(BDSGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;               
                btncancel.Visible = false;
            }
        }

        else if (ddlfilter.SelectedValue == "Pending For Carton/Pallet First Verification")
        {
            pre.BDS_cartstatus_No_Filter_First_Verfication(BDSGrid, ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = BDSGrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(BDSGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(BDSGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
              
                btncancel.Visible = false;
            }
        }



        /// **********************


        if (ddlfilter.SelectedValue == "Pending For Cartridge Allocation" && txtitemcode.Text != "")
        {
            pre.BDS_cartstatusitemcode_Pending_Allocation(BDSGrid, txtitemcode.Text.Trim(), ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = gridstatus.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(gridstatus.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridstatus.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
            }
        }
        else if (ddlfilter.SelectedValue == "Pending For Cartridge Allocation" && txtitemcode.Text == "" && txtdrugcode.Text != "")
        {
            pre.BDS_cartstatusdrugcode_Pending_Allocation(BDSGrid, txtdrugcode.Text.Trim(), ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = BDSGrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(BDSGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(BDSGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
            }
        }
        else if (ddlfilter.SelectedValue == "Pending For Cartridge Allocation" && txtitemcode.Text == "" && txtitemname.Text.Trim() != "")
        {
            pre.BDS_cartstatusname_Pending_Allocation(BDSGrid, txtitemname.Text.Trim(), ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = BDSGrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(BDSGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(BDSGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
            }
        }
        else if (ddlfilter.SelectedValue == "Pending For Cartridge Allocation" && txtitemcode.Text == "" && txtbrand.Text.Trim() != "")
        {
            pre.BDS_cartstatusbrand_Pending_Allocation(BDSGrid, txtbrand.Text.Trim(), ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
            DataSet dsData = BDSGrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(BDSGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(BDSGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = System.Drawing.Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Visible = true;
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = System.Drawing.Color.Green;
            }
        }

        else if (ddlfilter.SelectedValue == "Pending For Cartridge Allocation")
        {
            try
            {
                pre.BDS_cartstatus_No_Filter_Pending_Allocation(BDSGrid, ddlpharname.SelectedItem.ToString(), ViewState["ord"].ToString());
                DataSet dsData = BDSGrid.DataSource as DataSet;
                DataTable dtData = dsData.Tables[0];
                lblpge.Text = "Page" + "  " + Convert.ToString(BDSGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(BDSGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
                lblpge.Font.Bold = false;
                lblpge.ForeColor = System.Drawing.Color.Black;
                if (dtData.Rows.Count == 0)
                {
                    lblpge.Visible = true;
                    lblpge.Text = "No Record Found";
                    lblpge.Font.Bold = true;
                    lblpge.ForeColor = System.Drawing.Color.Green;
                }
            }
            catch (Exception ex)
            {
                Log.logwriter("iOPAS", ex.Message);
            }
        }

        if (ddlfilter.SelectedValue == "Bottle Unloading")
        {
            BDSGrid.Visible = false;
            gridstatus.Visible = false;
            BotUnloadingGrid.Visible = true; 
            UnloadingGridDisplay();
        }

    }

    void UnloadingGridDisplay()
    {
        BDS.BDSUnloadingGrid_Enquiry(BotUnloadingGrid, sessionuserid, ViewState["ord"].ToString());
        DataSet dsData = BotUnloadingGrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(BotUnloadingGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(BotUnloadingGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }

    protected void gridstatus_Sorting(object sender, GridViewSortEventArgs e)
    {
        griddisplay();
        DataSet dsData = gridstatus.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = gridstatus.PageIndex;
        gridstatus.DataSource = SortDataTable(dtData, false);
        gridstatus.DataBind();
        gridstatus.PageIndex = pageIndex;  
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
    protected void ddlmachinename_SelectedIndexChanged(object sender, EventArgs e)
    {     
        btncancel.Visible = false;
        ddlfilter.Items.Clear();
        if (ddlmachinename.SelectedValue.ToString() == "BDS")
        {
            ddlfilter.Items.Add("Pending For Carton/Pallet First Verification");
            ddlfilter.Items.Add("Pending For Carton/Pallet BDS Loading");
            ddlfilter.Items.Add("Pending For Cartridge Allocation");
            ddlfilter.Items.Add("Carton Box WIP");
            ddlfilter.Items.Add("Bottle Unloading");
        }
        else if (ddlmachinename.SelectedValue.ToString() == "HDDS")
        {
            ddlfilter.Items.Add("All");
            ddlfilter.Items.Add("Pending First Verification");
            ddlfilter.Items.Add("Pending Second Verification");
            ddlfilter.Items.Add("Pending For DDS Loading");            
        }
    }
    protected void ddlfilter_SelectedIndexChanged(object sender, EventArgs e)
    {        
        if (ddlfilter.SelectedValue == "Carton Box WIP")
        {
            ViewState["ord"] = "order by p.Loading_Id desc";
        }
        else if (ddlfilter.SelectedValue == "Pending For Cartridge Allocation")
        {
            ViewState["ord"] = "order by CartID desc";
        }
        else if (ddlfilter.SelectedValue == "Pending For Carton/Pallet First Verification")
        {
            ViewState["ord"] = "order by CartID desc";
        }
        else if (ddlfilter.SelectedValue == "Pending For Carton/Pallet BDS Loading")
        {
            ViewState["ord"] = "order by CartID desc";
        }
        else if (ddlfilter.SelectedValue == "Bottle Unloading")
        {
            ViewState["ord"] = "order by UnloadingID desc";
        }        
    }
    protected void BDSGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BDSgriddisplay ();
        BDSGrid.PageIndex = e.NewPageIndex;
        BDSGrid.DataBind();
        DataSet dsData = BDSGrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(BDSGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(BDSGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.Font.Bold = false;
        lblpge.ForeColor = System.Drawing.Color.Black;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Visible = true;
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = System.Drawing.Color.Green;
        }     
    }
    protected void BDSGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        BDSgriddisplay();
        DataSet dsData = BDSGrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = gridstatus.PageIndex;
        BDSGrid.DataSource = SortDataTable(dtData, false);
        BDSGrid.DataBind();
        BDSGrid.PageIndex = pageIndex; 
    }
    protected void BotUnloadingGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        UnloadingGridDisplay();
        DataSet dsData = BotUnloadingGrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = gridstatus.PageIndex;
        BotUnloadingGrid.DataSource = SortDataTable(dtData, false);
        BotUnloadingGrid.DataBind();
        BotUnloadingGrid.PageIndex = pageIndex; 
    }
    protected void BotUnloadingGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        UnloadingGridDisplay();
        BotUnloadingGrid.PageIndex = e.NewPageIndex;
        BotUnloadingGrid.DataBind();
        DataSet dsData = BotUnloadingGrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(BotUnloadingGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(BotUnloadingGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.Font.Bold = false;
        lblpge.ForeColor = System.Drawing.Color.Black;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Visible = true;
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = System.Drawing.Color.Green;
        }
    }
    protected void btncancel_Click(object sender, ImageClickEventArgs e)
    {
        Cancel();
        //int Rtnval = BDS.BDS_Bot_Preloading_Force_Cancel(txtcartonbar.Text.Trim(), Session["Userid"].ToString());

        //if (Rtnval == 1)
        //{
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Successfully Cancelled' );</script>", false);
        //    txtcartonbar.Text = "";
        //    BDSgriddisplay();
        //}
        //else if (Rtnval == 3)
        //{
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton box transaction already closed');</script>", false);
        //}
        //else if (Rtnval == 4)
        //{
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton box transaction already canceled');</script>", false);
        //}
        //else if (Rtnval == 5)
        //{
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton box cartridge allocation started you cannot cancel');</script>", false);
        //}
        //else if (Rtnval == 6)
        //{
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid barcode');</script>", false);
        //}
        //else if (Rtnval == 2)
        //{
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid barcode');</script>", false);
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occurred');</script>", false);
        //}
    }

    public bool CancelCheck()
    {
        for (int i = 0; i < BDSGrid.Rows.Count; i++)
        {

            GridViewRow row = BDSGrid.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("chkrow")).Checked;

            if (isChecked)
            {
                return true;
            }
        }
        return false;
    }
    public void Cancel()
    {
        if (CancelCheck())
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "popup", "confirmProcess()", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Records are not selected');</script>", false);
        }
        
    }

    protected void btnok_Click(object sender, EventArgs e)
    {
        int Rtnval = 0;
         for (int i = 0; i < BDSGrid.Rows.Count; i++)
            {
                GridViewRow row = BDSGrid.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkrow")).Checked;

                if (isChecked)
                {                    
                    Rtnval = BDS.BDS_Bot_Preloading_Force_Cancel(BDSGrid.Rows[i].Cells[1].Text.Trim(), Session["Userid"].ToString());

                     if (Rtnval == 3)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton box transaction already closed for this barcode : " + BDSGrid.Rows[i].Cells[1].Text.Trim() + " ' );</script>", false);
                        return;
                    }
                    else if (Rtnval == 4)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton box transaction already canceled for this barcode : " + BDSGrid.Rows[i].Cells[1].Text.Trim() + " ' );</script>", false);
                        return;
                    }
                    else if (Rtnval == 5)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton box cartridge allocation started you cannot cancel for this barcode : " + BDSGrid.Rows[i].Cells[1].Text.Trim() + " ' );</script>", false);
                        return;
                    }
                    else if (Rtnval == 6)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid barcode');</script>", false);
                        return;
                    }
                    else if (Rtnval == 2)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid barcode');</script>", false);
                        return;
                    }
                     else if (Rtnval != 1)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occurred');</script>", false);
                        return;
                    }
                }
            }

         if (Rtnval == 1)
         {
             ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Successfully Cancelled' );</script>", false);           
             BDSgriddisplay();
         }
           // gridload();     
       
    }
}