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
using System.Data.SqlClient;
using System.Text;
using System.IO;
using Datalayer;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Xml;
using System.Drawing;

public partial class Cartridgestatus : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    Preload pre = new Preload();
    string sessionuserid="",location="";
    //int locationid;
    int actstatus;
    int a = 0, b = 0, c = 0;
    //static string btnval = "";
    GridView GridView1 = new GridView();
    DataSet dsData = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (Session["Userid"] != null)
        {
            sessionuserid = Session["Userid"].ToString();
            location = Session["location"].ToString();
            if (location != "")
            {
                if (!IsPostBack)
                {
                    ViewState["ord"] = "order by p.Loaded_MC_Date desc";
                    ddsnoreader();
                    cartstatusgrid();
                }
            }
            else if (location == "" || location == null)
            {
                pp.Visible = false;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Workstation is not authorised to do this operation');</script>", false);
                return;
            }
        }
        else
        {
            //Response.Redirect("opas.html?Session=End");
            Response.Redirect("iopas.html");
        }      
    }
    protected void Page_Prerender(object sender, EventArgs e)
    {
        //SaveToExcel.NavigateUrl = Page.ResolveUrl("~/EXCEL_ASHX/Cartridge_Status_Excel.ashx"
        //            + "?DDS=" + ddlmcno.SelectedValue + "&location=" + location + "&itemname=" + txtitemname.Text + "&status=" + ddlcartno.SelectedValue);
    }
    protected void ddlcartno_SelectedIndexChanged(object sender, EventArgs e)
    {        
       
    }    

    // * DDS Number Reader Function * \\
    public void ddsnoreader()
    {
        ddlmcno.Items.Clear();

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select d.DDS_Name from dds as d left join Pharmacy as p on p.PharmacyID=d.PharmacyID  where p.Location_Name='" + location + "' and d.Status='Active'";          
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
    //protected void btnsear_Click(object sender, EventArgs e)
    protected void btnsear_Click(object sender, ImageClickEventArgs e)
    {
        cartmanage.PageIndex = 0;
        ViewState["ord"] = "order by p.Loaded_MC_Date desc";
        cartstatusgrid();
    }
    //*Grid Display Function *\\
    public void cartstatusgrid()
    {
        if (ddlmcno.SelectedItem != null)
        {
            if ((ddlcartno.SelectedItem.Value == "Enabled") && (txtcartnumber.Text.Trim()==""))
            {
                btnactive.Visible = true;
                btndeactive.Visible = true;
                actstatus = 5;
                pre.cartstatusgrid(cartmanage, ddlmcno.SelectedItem.Value, actstatus, location, txtitemname.Text.Trim(), ViewState["ord"].ToString());

                dsData = cartmanage.DataSource as DataSet;                
            }
            else if ((ddlcartno.SelectedItem.Value == "Enabled") && (txtcartnumber.Text.Trim() != ""))
            {
                btnactive.Visible = true;
                btndeactive.Visible = true;
                actstatus = 5;
                pre.Cartstatusgrid_Enable_CartNo(cartmanage, ddlmcno.SelectedItem.Value, actstatus, location, txtcartnumber.Text.Trim(), ViewState["ord"].ToString());

                dsData = cartmanage.DataSource as DataSet;
            }
            else if ((ddlcartno.SelectedItem.Value == "Disabled") && (txtcartnumber.Text.Trim()==""))
            {
                btnactive.Visible = true;
                btndeactive.Visible = true;
                actstatus = 2;
                pre.cartstatusgriddisable(cartmanage, ddlmcno.SelectedItem.Value, location, txtitemname.Text.Trim(), ViewState["ord"].ToString());
                dsData = cartmanage.DataSource as DataSet; 
            }

            else if ((ddlcartno.SelectedItem.Value == "Disabled") && (txtcartnumber.Text.Trim() != ""))
            {
                btnactive.Visible = true;
                btndeactive.Visible = true;
                actstatus = 2;
                pre.Cartstatusgrid_disable_CartNo(cartmanage, ddlmcno.SelectedItem.Value, location, txtcartnumber.Text.Trim(), ViewState["ord"].ToString());
                dsData = cartmanage.DataSource as DataSet;
            }
            else if ((ddlcartno.SelectedItem.Value == "BDS Partial Cartridge") && (txtcartnumber.Text.Trim() == ""))
            {
                btnactive.Visible = true;
                btndeactive.Visible = true;
                actstatus = 2;
                pre.BDS_Partial_Cartridge(cartmanage, ddlmcno.SelectedItem.Value, location, txtitemname.Text.Trim(), ViewState["ord"].ToString());
                dsData = cartmanage.DataSource as DataSet;
            }

            else if ((ddlcartno.SelectedItem.Value == "BDS Partial Cartridge") && (txtcartnumber.Text.Trim() != ""))
            {
                btnactive.Visible = true;
                btndeactive.Visible = true;
                actstatus = 2;
                pre.BDS_Partial_Cartridge_CartNo(cartmanage, ddlmcno.SelectedItem.Value, location, txtcartnumber.Text.Trim(), ViewState["ord"].ToString());
                dsData = cartmanage.DataSource as DataSet;
            }
            else if ((ddlcartno.SelectedItem.Value == "All(Enabled & Disabled)") && (txtcartnumber.Text.Trim() == ""))
            {
                btnactive.Visible = true;
                btndeactive.Visible = true;
                pre.cartstatusgridall(cartmanage, ddlmcno.SelectedItem.Value, location, txtitemname.Text.Trim(), ViewState["ord"].ToString());
                dsData = cartmanage.DataSource as DataSet;
            }
            else if ((ddlcartno.SelectedItem.Value == "All(Enabled & Disabled)") && (txtcartnumber.Text.Trim() != ""))
            {
                btnactive.Visible = true;
                btndeactive.Visible = true;
                pre.cartstatusgridall_CartNo(cartmanage, ddlmcno.SelectedItem.Value, location, txtcartnumber.Text.Trim(), ViewState["ord"].ToString());
                dsData = cartmanage.DataSource as DataSet;
            }
            else if ((ddlcartno.SelectedItem.Value == "Empty Cartridge") && (txtcartnumber.Text.Trim() == ""))
            {
                btnactive.Visible = true;
                btndeactive.Visible = true;
                pre.cartstatusgridempty(cartmanage, ddlmcno.SelectedItem.Value, location, txtitemname.Text.Trim());
                dsData = cartmanage.DataSource as DataSet;
            }
            else if ((ddlcartno.SelectedItem.Value == "Empty Cartridge") && (txtcartnumber.Text.Trim() != ""))
            {
                btnactive.Visible = true;
                btndeactive.Visible = true;
                pre.cartstatusgridempty_CartNo(cartmanage, ddlmcno.SelectedItem.Value, location, txtcartnumber.Text.Trim());
                dsData = cartmanage.DataSource as DataSet;
            }
            else if ((ddlcartno.SelectedItem.Value == "Low Stock") && (txtcartnumber.Text.Trim() == ""))
            {
                btnactive.Visible = true;
                btndeactive.Visible = true;
                pre.cartstatuslowstock(cartmanage, ddlmcno.SelectedItem.Value, location, txtitemname.Text.Trim(), ViewState["ord"].ToString());
                dsData = cartmanage.DataSource as DataSet;                
            }
            else if ((ddlcartno.SelectedItem.Value == "Low Stock") && (txtcartnumber.Text.Trim() != ""))
            {
                btnactive.Visible = true;
                btndeactive.Visible = true;
                pre.cartstatuslowstock_CartNo(cartmanage, ddlmcno.SelectedItem.Value, location, txtcartnumber.Text.Trim(), ViewState["ord"].ToString());
                dsData = cartmanage.DataSource as DataSet;
            }

            else if (ddlcartno.SelectedItem.Value == "Invalid Cartridge")
            {
                btnactive.Visible = false;
                btndeactive.Visible = false;
                pre.invalidcartridge(cartmanage, ddlmcno.SelectedItem.Value);
                dsData = cartmanage.DataSource as DataSet; 
            }
            else if (ddlcartno.SelectedItem.Value == "Removed Cartridge")
            {
                btnactive.Visible = false;
                btndeactive.Visible = false;
                pre.Cartstatus_Removed(cartmanage, ddlmcno.SelectedItem.Value, location);
                dsData = cartmanage.DataSource as DataSet;
            }


            try
            {
                DataTable dtData = dsData.Tables[0];
                lblpge.Text = "Page" + "  " + Convert.ToString(cartmanage.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(cartmanage.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
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
        else
        {
            //pre.cartstatusgrid(cartmanage,"test",actstatus);
        }
    }
    protected void ddlmcno_SelectedIndexChanged(object sender, EventArgs e)
    {
       // cartstatusgrid();
    }

    //protected void btnprint_Click(object sender, EventArgs e)
    protected void btnprint_Click(object sender, ImageClickEventArgs e)
    {
        cartstatusgrid();
        Session["ctrl"] = GridView1;
        ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popups", "Printpage()", true);

        //Excel();
      //  PDF_Export();      
      // Word_Export();
        
    }
    //protected void btnexcel_Click(object sender, EventArgs e)
    protected void btnexcel_Click(object sender, ImageClickEventArgs e)
    {
        string status = "";
        status = ddlcartno.SelectedValue.ToString();
        if (status == "All(Enabled & Disabled)")
        {
            status = "All";
        }      

        Response.Redirect("EXCEL_ASHX/Cartridge_Status_Excel.ashx?DDS=" + ddlmcno.SelectedValue + "&location=" + location + "&itemname=" + txtitemname.Text + "&status=" + status);
                
    }

    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    return;
    //}
    public void Excel()
    {
        cartstatusgrid();
        HtmlForm form = new HtmlForm();
        form.Controls.Add(cartmanage);
        string attachment = "attachment; filename=Cartstatus.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter stw = new StringWriter();
        HtmlTextWriter htextw = new HtmlTextWriter(stw);
        form.Controls.Add(cartmanage);
        this.Controls.Add(form);
        form.RenderControl(htextw);
        Response.Write(stw.ToString());
        Response.End(); 
    }

    public void PDF_Export()
    {
        Response.Clear();
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        cartmanage.RenderControl(htw);
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment; filename=Cartmgt.pdf");
        Document document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        PdfWriter.GetInstance(document, Response.OutputStream);
        document.Open();
        Chunk c = new Chunk
        ("Cartridge Management \n",
        FontFactory.GetFont("Verdana", 15));
        Paragraph p = new Paragraph();
        p.Alignment = Element.ALIGN_CENTER;
        p.Add(c);
        Chunk chunk1 = new Chunk
                ("By Ramesh, Ramesh@Getecha.com \n",
                FontFactory.GetFont("Verdana", 8));
        Paragraph p1 = new Paragraph();
        p1.Alignment = Element.ALIGN_RIGHT;
        p1.Add(chunk1);
        document.Add(p);
        document.Add(p1);
        string html = sb.ToString();
        XmlTextReader reader = new XmlTextReader(new StringReader(html));
        HtmlParser.Parse(document, reader);
        document.Close();
        sw.Close();
        Response.Flush();
        Response.End(); 
    }     

    private void Word_Export()
    {
        HtmlForm form = new HtmlForm();
        Response.AddHeader("content-disposition","attachment;filename=Cartmgt.doc");
        Response.ContentType = "application/vnd.ms-word ";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        //cartmanage.AllowPaging = false;
        form.Controls.Add(cartmanage);
        this.Controls.Add(form);
        form.RenderControl(hw);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();        
    }


    protected void cartmanage_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["ord"] = "";
        cartstatusgrid();
        DataSet dsData = cartmanage.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = cartmanage.PageIndex;
        cartmanage.DataSource = SortDataTable(dtData, false);
        cartmanage.DataBind();
        cartmanage.PageIndex = pageIndex;        
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
    protected void cartmanage_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        cartstatusgrid();
        cartmanage.PageIndex = e.NewPageIndex;
        cartmanage.DataBind();
        DataSet dsData = cartmanage.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(cartmanage.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(cartmanage.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = System.Drawing.Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Visible = true;
            lblpge.Font.Bold = true;
            lblpge.ForeColor = System.Drawing.Color.Green;
        }
    }   

    // * CARTRIDGE ACTIVE INACTIVE STATUS FUNCTIONS * \\
    public void activeinactive()
    {
        for (int i = 0; i < cartmanage.Rows.Count; i++)
        {
            GridViewRow row = cartmanage.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("chk")).Checked;

            if (isChecked)
            {
                c++;
                string stat = (cartmanage.Rows[i].Cells[14].Text).Trim();
                string act = "Enabled";
                string inc = "Disabled";
                if (stat.ToLower() == act.ToLower())
                {
                    a++;
                }
                else if (stat.ToLower() == inc.ToLower())
                {
                    b++;
                }
            }
        }
    }
    //protected void btnactive_Click(object sender, EventArgs e)
    protected void btnactive_Click(object sender, ImageClickEventArgs e)
    {       
        activeinactive();
        //btnval = "";
        //btnval = "1";
        ViewState["btnval"] = "1";
        if (a != 0 & b == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Selected records are already enabled.');</script>", false);
        }
        else if (a != 0 & b != 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Selected records are of different status. ');</script>", false);
        }
        else if (c != 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "popup", "confirmProcess()", true);             
            //enable();
            //cartstatusgrid();
        }
        else if (c == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Records are not selected');</script>", false);
        }
        else
        {
            
        }
    }
    //protected void btndeactive_Click(object sender, EventArgs e)
    protected void btndeactive_Click(object sender, ImageClickEventArgs e)
    {
        activeinactive();
        //btnval = "";
        //btnval = "2";
        ViewState["btnval"] = "2";
        if (b != 0 & a == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Selected records are already disabled. ');</script>", false);
        }
        else if (a != 0 & b != 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Selected records are of different status.');</script>", false);
        }
        else if (c != 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "popup", "confirmProcess1()", true);
           // Disable();
            //cartstatusgrid();
        }
        else if (c == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Records are not selected');</script>", false);
        }
        else
        {
            
        }
    }
    protected void btnok_Click(object sender, EventArgs e)
    {
        if (ViewState["btnval"].ToString() == "1")
        {
            enable();
            cartstatusgrid();
        }
        else if (ViewState["btnval"].ToString() == "2")
        {
            Disable();
            cartstatusgrid();
        }
    }
    
    // * Cartridge Enable * \\
    public void enable()
    {
        for (int i = 0; i < cartmanage.Rows.Count; i++)
        {
            GridViewRow row = cartmanage.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("chk")).Checked;

            if (isChecked)
            {
                if ((cartmanage.Rows[i].Cells[2].Text != "0") && (cartmanage.Rows[i].Cells[2].Text != "") && (cartmanage.Rows[i].Cells[2].Text != "&nbsp;"))
                {
                    if ((ddlcartno.SelectedItem.Value == "BDS Partial Cartridge"))
                    {
                        pre.BDS_Parctial_Cartridge_Enable(cartmanage.Rows[i].Cells[3].Text, cartmanage.Rows[i].Cells[2].Text, cartmanage.Rows[i].Cells[1].Text);
                    }

                    int Rtnval = pre.Cartenable(location, cartmanage.Rows[i].Cells[3].Text, cartmanage.Rows[i].Cells[2].Text, cartmanage.Rows[i].Cells[4].Text, cartmanage.Rows[i].Cells[1].Text, sessionuserid);

                    if (cartmanage.Rows[i].Cells[1].Text.Contains("BDS"))
                    {
                        if (Rtnval == 2)
                        {
                            if (cartmanage.Rows[i].Cells[14].Text == "Zero Inventory")
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge has zero inventory. Unload cartridge in Loading menu and preload again ');</script>", false);
                                return;
                            }
                            else if (cartmanage.Rows[i].Cells[14].Text == "Drug Master Updated")
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Drug master updated. Unload cartridge in Loading menu and preload again ');</script>", false);
                                return;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('BDS cartridge structure disabled or BDS error. Unload cartridge in Loading menu and preload again ');</script>", false);
                                return;
                            }
                        }
                        if (Rtnval == 3)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('BDS is stop production user cannot enable the cartridge ');</script>", false);
                            return;
                        }
                        if (Rtnval == 4)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('BDS cartridge structure disabled or BDS error. Unload cartridge in Loading menu and preload again');</script>", false);
                            return;
                        }
                        if (Rtnval == 5)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('First verification not done. Unload cartridge in Loading menu and preload again');</script>", false);
                            return;
                        }
                        else if (Rtnval == 6)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Cartridge does not belong to this pharmacy');</script>", false);
                            return;
                        }
                        else if (Rtnval == 7)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Expiry date reached Minimum Drug Expiry. Manual Enabling not allowed.');</script>", false);
                            return;
                        }
                        else if (Rtnval == 8)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Item Effective Date To should not be earlier than current date');</script>", false);
                            return;
                        }
                        else if (Rtnval == 9)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Store Effective Date To should not be earlier than current date');</script>", false);
                            return;
                        }
                        else if (Rtnval == 10)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Second verification not done. Unload cartridge in Loading menu and preload again');</script>", false);
                            return;
                        }
                        else if (Rtnval == 12)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Second verification rejected. Unload cartridge in Loading menu and preload again');</script>", false);
                            return;
                        }
                        else if (Rtnval == 13)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('First verification rejected. Unload cartridge in Loading menu and preload again');</script>", false);
                            return;
                        }
                        else if (Rtnval == 11)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('BDS is inactive');</script>", false);
                            return;
                        }
                        else if (Rtnval == 14)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Cartridge is Inactive');</script>", false);
                            return;
                        }
                        else if (Rtnval == 15)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Item Inactivated in Drug Master');</script>", false);
                            return;
                        }
                        else if (Rtnval == 16)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Drug Master Updated so cartridge disabled');</script>", false);
                            return;
                        }
                        else if (Rtnval == 17)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Brand Inactivated in Brand Master');</script>", false);
                            return;
                        }
                        else if (Rtnval == 22)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('System will not allow enabling two different brands of the same drug');</script>", false);
                            return;
                        }
                    }
                    else
                    {

                        if (Rtnval == 2)
                        {
                            if (cartmanage.Rows[i].Cells[14].Text == "Zero Inventory")
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge has zero inventory. Unload cartridge in Loading menu and preload again ');</script>", false);
                                return;
                            }
                            else if (cartmanage.Rows[i].Cells[14].Text == "Drug Master Updated")
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Drug master updated. Unload cartridge in Loading menu and preload again ');</script>", false);
                                return;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge removed or DDS error. Unload cartridge in Loading menu and preload again ');</script>", false);
                                return;
                            }
                        }
                        if (Rtnval == 3)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('DDS is stop production user cannot enable the cartridge ');</script>", false);
                            return;
                        }
                        if (Rtnval == 4)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge removed or DDS error. Unload cartridge in Loading menu and preload again');</script>", false);
                            return;
                        }
                        if (Rtnval == 5)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('First verification not done. Unload cartridge in Loading menu and preload again');</script>", false);
                            return;
                        }
                        else if (Rtnval == 6)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Cartridge does not belong to this pharmacy');</script>", false);
                            return;
                        }
                        else if (Rtnval == 7)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Expiry date reached Minimum Drug Expiry. Manual Enabling not allowed.');</script>", false);
                            return;
                        }
                        else if (Rtnval == 8)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Item Effective Date To should not be earlier than current date');</script>", false);
                            return;
                        }
                        else if (Rtnval == 9)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Store Effective Date To should not be earlier than current date');</script>", false);
                            return;
                        }
                        else if (Rtnval == 10)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Second verification not done. Unload cartridge in Loading menu and preload again');</script>", false);
                            return;
                        }
                        else if (Rtnval == 12)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Second verification rejected. Unload cartridge in Loading menu and preload again');</script>", false);
                            return;
                        }
                        else if (Rtnval == 13)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('First verification rejected. Unload cartridge in Loading menu and preload again');</script>", false);
                            return;
                        }
                        else if (Rtnval == 11)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('DDS is inactive');</script>", false);
                            return;
                        }
                        else if (Rtnval == 14)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Cartridge is Inactive');</script>", false);
                            return;
                        }
                        else if (Rtnval == 15)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Item Inactivated in Drug Master');</script>", false);
                            return;
                        }
                        else if (Rtnval == 16)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Drug Master Updated so cartridge disabled');</script>", false);
                            return;
                        }
                        else if (Rtnval == 17)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Brand Inactivated in Brand Master');</script>", false);
                            return;
                        }
                        else if (Rtnval == 22)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('System will not allow enabling two different brands of the same drug');</script>", false);
                            return;
                        }
                    }
                }
                else if ((cartmanage.Rows[i].Cells[2].Text == "0")|| (cartmanage.Rows[i].Cells[2].Text != "") || (cartmanage.Rows[i].Cells[2].Text == "&nbsp;"))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('DDS is stop production user cannot enable the cartridge ');</script>", false);
                    return;
                }
            }
        }        
    }

    // * Cartridge Disable * \\
    public void Disable()
    {
        if ((ddlcartno.SelectedItem.Value != "BDS Partial Cartridge"))
        {
            for (int i = 0; i < cartmanage.Rows.Count; i++)
            {
                GridViewRow row = cartmanage.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chk")).Checked;

                if (isChecked)
                {
                    int Rtnval = pre.Cartdisable(location, cartmanage.Rows[i].Cells[3].Text, sessionuserid);
                    if (Rtnval == 2)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Cartridge does not belong to this pharmacy');</script>", false);
                        return;
                    }
                    else if ((Rtnval == 3) && (cartmanage.Rows[i].Cells[1].Text.Contains("DDS")))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge is already removed. Unload cartridge in Loading menu and preload again ');</script>", false);
                        return;
                    }
                    else if (Rtnval == 4)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Current order DDS/BDS using this cartridge inventory . please try again later');</script>", false);
                        return;
                    }
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('BDS partial loaded cartridge alreay disable mode.');</script>", false);
        }

    }



 
}
