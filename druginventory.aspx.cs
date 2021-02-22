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
using Datalayer;
using System.Drawing;

public partial class druginventory : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    DDSquery dds = new DDSquery();
    int sysqty = 0;
    int Preqty = 0;
    int comqty = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Userid"] == null)
        {
            //Response.Redirect("opas.html?Session=End");
            Response.Redirect("iopas.html");
        }
        else
        {
            if (!IsPostBack)
            {
                Locationreader();
                DDSnameReader();
                visiblefalse();
                lblnorecord.Visible = false;
                lblpge.Visible = false;
            }

            txtitemcode.Attributes.Add("onKeyPress", "doClick2(event)");
            txtdrugcode.Attributes.Add("onKeyPress", "doClick2(event)");
            txtitemname.Attributes.Add("onKeyPress", "doClick2(event)");
            search.Attributes.Add("onclick", "itemsearch();return false;");  

            Session["drglocation"] = ddlpharloc.SelectedValue.ToString();
            
        }
            
    }

    protected void btnok_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Selected record is "+searchvalue.Text+"');</script>", false);
        visiblefalse();
        dds.inventorygridall(griddruginven, "", "", "", "", "");
        if (ddlddsno.SelectedValue.ToString() != "")
        {
            lblnorecord.Visible = false;
            lblpge.Visible = false;
            codereader();
            ddlpacksize.Items.Clear();
            ddlpacksize.Items.Add("-Select-");
            ddlpacktype.Items.Clear();
            ddlpacktype.Items.Add("-Select-");
            if (ddlddsno.SelectedItem.Value == "ALL")
            {
                Brandreaderall();
                if (ddlbrand.SelectedValue.ToString() != "")
                {
                    packtypereaderall();
                }
            }
            else if (ddlddsno.SelectedItem.Value != "ALL")
            {
                Brandreader();
                if (ddlbrand.SelectedValue.ToString() != "")
                {
                    packtypereader();
                }
            }
            if (ddlbrand.SelectedValue.ToString() == "")
            {
                lblnorecord.Visible = true;
                lblpge.Visible = false;
                lblnorecord.Text = "No Record Found";
                lblnorecord.Font.Bold = true;
                lblnorecord.ForeColor = Color.Green;               
            }
        }
    }

    // * Pharmacy Location Name Reader * \\
    public void Locationreader()
    {        
       using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select Location_Name from pharmacy where status='Active'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddlpharloc.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }

    // * DDS Name Reader * \\
    public void DDSnameReader()
    {
        ddlddsno.Items.Clear();
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select d.DDS_name from DDS as d left join Pharmacy as p on p.PharmacyID=d.PharmacyID where p.Location_Name='" + ddlpharloc.SelectedItem.Value + "' and d.Status='Active'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    ddlddsno.Items.Add("ALL");
                    while (dr.Read())
                    {
                        ddlddsno.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }

    // * Itemcode,Name Drugcode Reader * \\
    public void codereader()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select i.item_code,i.item_name,i.drug_code from Item_Master as i where i.MasterID ='" + Convert.ToInt32(searchvalue.Text) + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtitemcode.Text = dr[0].ToString();
                        txtitemname.Text = dr[1].ToString();
                        txtdrugcode.Text = dr[2].ToString();
                    }
                }
            }
        }

    }
    protected void ddlpharloc_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDSnameReader();
    }

    // * Brand Reader With Particular DDS Name * \\
    public void Brandreader()
    {
        ddlbrand.Items.Clear();
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "SELECT b.Brandname from Item_user_Master as UM left join Item_Master as i on i.MasterID=UM.MasterID left join Cartridge_Loading as c on c.IUM_ID=UM.ID left join Brand_Master as b on b.BrandID=c.Brandid left join Cartridge_Master as cm on cm.Cartridge_Id=c.Cartridge_Id left join Pharmacy as p on p.PharmacyID =cm.PharmacyID where i.Item_Code='" + txtitemcode.Text + "' and c.Activation_Status='5'and c.Inventory_Status='2' and c.DDS_Name='" + ddlddsno.SelectedItem.Value + "' and p.Location_Name='" + ddlpharloc.SelectedItem.Value + "' group by b.Brandname ";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddlbrand.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }

    // * Brand Reader all DDS    * \\
    public void Brandreaderall()
    {
        ddlbrand.Items.Clear();

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "SELECT b.Brandname from Item_user_Master as UM left join Item_Master as i on i.MasterID=UM.MasterID left join Cartridge_Loading as c on c.IUM_ID=UM.ID left join Brand_Master as b on b.BrandID=c.Brandid left join Cartridge_Master as cm on cm.Cartridge_Id=c.Cartridge_Id left join Pharmacy as p on p.PharmacyID =cm.PharmacyID where i.Item_Code='" + txtitemcode.Text + "' and c.Activation_Status='5'and c.Inventory_Status='2' and p.Location_Name='" + ddlpharloc.SelectedItem.Value + "' group by b.Brandname ";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddlbrand.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }

    // * Pack type Reader With DDS * \\
    public void packtypereader()
    {
        ddlpacktype.Items.Clear();
        ddlpacktype.Items.Add("-Select-");
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "SELECT pm.PackType  from Item_user_Master as UM left join Item_Master as i on i.MasterID=UM.MasterID left join Cartridge_Loading as c on c.IUM_ID=UM.ID left join Brand_Master as b on b.BrandID=c.Brandid left join Cartridge_Master as cm on cm.Cartridge_Id=c.Cartridge_Id left join Pharmacy as p on p.PharmacyID=cm.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where i.Item_Code='" + txtitemcode.Text + "' and c.Activation_Status='5'and c.Inventory_Status='2' and b.Brandname='" + ddlbrand.SelectedItem.Value + "' and p.Location_Name='" + ddlpharloc.SelectedItem.Value + "'and c.DDS_Name='" + ddlddsno.SelectedItem.Value + "' group by pm.PackType ";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddlpacktype.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }

    // * Pack type Reader With All DDS * \\
    public void packtypereaderall()
    {
        ddlpacktype.Items.Clear();
        ddlpacktype.Items.Add("-Select-");

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "SELECT pm.PackType  from Item_user_Master as UM left join Item_Master as i on i.MasterID=UM.MasterID left join Cartridge_Loading as c on c.IUM_ID=UM.ID left join Brand_Master as b on b.BrandID=c.Brandid left join Cartridge_Master as cm on cm.Cartridge_Id=c.Cartridge_Id left join Pharmacy as p on p.PharmacyID=cm.PharmacyID  left join Packtype_Master as pm on pm.ID=UM.PacktypeID where i.Item_Code='" + txtitemcode.Text + "' and c.Activation_Status='5'and c.Inventory_Status='2' and b.Brandname='" + ddlbrand.SelectedItem.Value + "' and p.Location_Name='" + ddlpharloc.SelectedItem.Value + "' group by pm.PackType ";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddlpacktype.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }

    // * Pack Size Reader With DDS * \\
    public void packsizereader()
    {
        ddlpacksize.Items.Clear();
        ddlpacksize.Items.Add("-Select-");

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "SELECT UM.Pack_Size,IL.UOM from Item_user_Master as UM left join Item_Master as i on i.MasterID=UM.MasterID left join Cartridge_Loading as c on c.IUM_ID=UM.ID left join Brand_Master as b on b.BrandID=c.Brandid left join Cartridge_Master as cm on cm.Cartridge_Id=c.Cartridge_Id left join Pharmacy as p on p.PharmacyID=cm.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and p.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where i.Item_Code='" + txtitemcode.Text + "' and c.Activation_Status='5'and c.Inventory_Status='2' and b.Brandname='" + ddlbrand.SelectedItem.Value + "'and pm.PackType='" + ddlpacktype.SelectedItem.Value + "' and p.Location_Name='" + ddlpharloc.SelectedItem.Value + "'and c.DDS_Name='" + ddlddsno.SelectedItem.Value + "' group by UM.Pack_Size,IL.UOM";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddlpacksize.Items.Add(dr[0].ToString());
                        txtuom.Text = dr[1].ToString();
                    }
                }
            }
        }
    }

    // * Pack Size Reader With ALL DDS * \\
    public void packsizereaderall()
    {
        ddlpacksize.Items.Clear();
        ddlpacksize.Items.Add("-Select-");

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "SELECT UM.Pack_Size,IL.UOM from Item_user_Master as UM left join Item_Master as i on i.MasterID=UM.MasterID left join Cartridge_Loading as c on c.IUM_ID=UM.ID left join Brand_Master as b on b.BrandID=c.Brandid left join Cartridge_Master as cm on cm.Cartridge_Id=c.Cartridge_Id left join Pharmacy as p on p.PharmacyID=cm.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and p.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where i.Item_Code='" + txtitemcode.Text + "' and c.Activation_Status='5'and c.Inventory_Status='2' and b.Brandname='" + ddlbrand.SelectedItem.Value + "'and pm.PackType='" + ddlpacktype.SelectedItem.Value + "' and p.Location_Name='" + ddlpharloc.SelectedItem.Value + "' group by UM.Pack_Size,IL.UOM";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddlpacksize.Items.Add(dr[0].ToString());
                        txtuom.Text = dr[1].ToString();
                    }
                }
            }
        }
    }
    protected void ddlpacktype_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtuom.Text = "";
        if (ddlddsno.SelectedValue.ToString() != "")
        {
            if (ddlddsno.SelectedItem.Value == "ALL")
            {
                packsizereaderall();
            }
            else if (ddlddsno.SelectedItem.Value != "ALL")
            {
                packsizereader();
            }
        }
    }
    protected void ddlddsno_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblnorecord.Visible = false;
        lblpge.Visible = false;
        ddlpacksize.Items.Clear();
        ddlpacksize.Items.Add("-Select-");
        ddlpacktype.Items.Clear();
        ddlpacktype.Items.Add("-Select-");
        txtuom.Text = "";
        dds.inventorygridall(griddruginven, "", "", "", "", "");
        if (searchvalue.Text != "")
        {
            if (ddlddsno.SelectedItem.Value == "ALL")
            {
                Brandreaderall();
                if (ddlbrand.SelectedValue.ToString() != "")
                {
                    packtypereaderall();
                }
            }
            else if (ddlddsno.SelectedItem.Value != "ALL")
            {
                Brandreader();
                if (ddlbrand.SelectedValue.ToString() != "")
                {
                    packtypereader();
                }
            }
            if (ddlbrand.SelectedValue.ToString() == "")
            {
                dds.inventorygridall(griddruginven, "", "", "", "", "");
                lblnorecord.Visible = true;
                lblpge.Visible = false;
                lblnorecord.Text = "No Record Found";
                lblnorecord.Font.Bold = true;
                lblnorecord.ForeColor = Color.Green;
            }
        }
    }

    // * Grid Display * \\
    public void inventorygriddisplay()
    {

         sysqty = 0;
         Preqty = 0;
         comqty = 0;

        lblnorecord.Visible = false;
        lblpge.Visible = true;
        if (ddlddsno.SelectedValue.ToString() != "")
        {
            if (ddlddsno.SelectedItem.Value != "ALL")
            {
                dds.inventorygrid(griddruginven, ddlddsno.SelectedItem.Value, ddlpharloc.SelectedItem.Value, txtitemcode.Text, ddlpacktype.SelectedItem.Value, ddlpacksize.SelectedItem.Value, ddlbrand.SelectedItem.Value);
                DataSet dsData = griddruginven.DataSource as DataSet;
                DataTable dtData = dsData.Tables[0];
                lblpge.Text = "Page" + "  " + Convert.ToString(griddruginven.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(griddruginven.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
               
                if (dtData.Rows.Count == 0)
                {
                    lblnorecord.Visible = true;
                    lblpge.Visible = false;
                    lblnorecord.Text = "No Record Found";
                    lblnorecord.Font.Bold = true;
                    lblnorecord.ForeColor = Color.Green;
                }
            }
            else if (ddlddsno.SelectedItem.Value == "ALL")
            {
                dds.inventorygridall(griddruginven, ddlpharloc.SelectedItem.Value, txtitemcode.Text, ddlpacktype.SelectedItem.Value, ddlpacksize.SelectedItem.Value, ddlbrand.SelectedItem.Value);
                DataSet dsData = griddruginven.DataSource as DataSet;
                DataTable dtData = dsData.Tables[0];
                lblpge.Text = "Page" + "  " + Convert.ToString(griddruginven.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(griddruginven.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";               
                if (dtData.Rows.Count == 0)
                {
                    lblnorecord.Visible = true;
                    lblpge.Visible = false;
                    lblnorecord.Text = "No Record Found";
                    lblnorecord.Font.Bold = true;
                    lblnorecord.ForeColor = Color.Green;
                }
            }

           

        }
    }
    protected void ddlpacksize_SelectedIndexChanged(object sender, EventArgs e)
    {
        griddruginven.PageIndex = 0;
        inventorygriddisplay();
        visiblefalse();
    }
    protected void ddlbrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlddsno.SelectedValue.ToString() != "")
        {
            if (ddlddsno.SelectedItem.Value == "ALL")
            {
                packtypereaderall();
            }
            else if (ddlddsno.SelectedItem.Value != "ALL")
            {
                packtypereader();
            }
        }
    }
    //protected void Button2_Click(object sender, EventArgs e)
    protected void Button2_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlbrand.SelectedValue.ToString() != "")
        {
            visibletrue();
            txtsysqty.Text = "0";
            txtpreallot.Text = "0";
            txtcomputed.Text = "0";
            lblptype.Text = "";

            for (int i = 0; i < griddruginven.Rows.Count; i++)
            {
                GridViewRow row = griddruginven.Rows[i];
                txtsysqty.Text = Convert.ToString(Convert.ToInt32(txtsysqty.Text) + (Convert.ToInt32(griddruginven.Rows[i].Cells[5].Text)));
                txtpreallot.Text = Convert.ToString(Convert.ToUInt32(txtpreallot.Text) + (Convert.ToUInt32(griddruginven.Rows[i].Cells[6].Text)));
                txtcomputed.Text = Convert.ToString(Convert.ToUInt32(txtcomputed.Text) + (Convert.ToUInt32(griddruginven.Rows[i].Cells[7].Text)));
            }
            if (ddlpacktype.SelectedItem.Value != "-Select-")
            {
                lblptype.Text = ddlpacktype.SelectedItem.Value;
            }
        }
    }
    //protected void btncl_Click(object sender, EventArgs e)
    protected void btncl_Click(object sender, ImageClickEventArgs e)
    {
        clear();
    }

    // * Textbox Clear * \\
    public void clear()
    {
        txtitemcode.Text = "";
        txtitemname.Text = "";
        txtdrugcode.Text = "";
        txtcomputed.Text = "";
        txtpreallot.Text = "";
        txtsysqty.Text = "";
        txtuom.Text = "";
        lblptype.Text = "";
        ddlpacksize.Items.Clear();
        ddlpacksize.Items.Add("-Select-");
        ddlpacktype.Items.Clear();
        ddlpacktype.Items.Add("-Select-");
        ddlbrand.Items.Clear();
    }

    // * Calculate Visible False * \\
    public void visiblefalse()
    {
        lbltot.Visible = false;
        txtpreallot.Visible = false;
        txtcomputed.Visible = false;
        txtsysqty.Visible = false;
        lblptype.Visible = false;
    }
    // * Calculate Visible True * \\
    public void visibletrue()
    {
        lbltot.Visible = true;
        txtpreallot.Visible = true;
        txtcomputed.Visible = true;
        txtsysqty.Visible = true;
        lblptype.Visible = true;
    }
    protected void griddruginven_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        inventorygriddisplay();
        griddruginven.PageIndex = e.NewPageIndex;
        griddruginven.DataBind();
    }
   
    protected void griddruginven_Sorting(object sender, GridViewSortEventArgs e)
    {
        inventorygriddisplay();
        DataSet dsData = griddruginven.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = griddruginven.PageIndex;
        griddruginven.DataSource = SortDataTable(dtData, false);
        griddruginven.DataBind();
        griddruginven.PageIndex = pageIndex;   
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
    protected void search_Click(object sender, ImageClickEventArgs e)
    {

    }
    

    protected void griddruginven_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblqy = (Label)e.Row.FindControl("lblavalqty");
            int qty = Int32.Parse(lblqy.Text);
            sysqty = sysqty + qty;

            Label lblpreqy = (Label)e.Row.FindControl("lblprealqty");
            int qty1 = Int32.Parse(lblpreqy.Text);
            Preqty = Preqty + qty1;

            Label lblcomqy = (Label)e.Row.FindControl("lblcompalqty");
            int qty2 = Int32.Parse(lblcomqy.Text);
            comqty = comqty + qty2;
        }
        
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblavalTotalqty = (Label)e.Row.FindControl("lblavalTotalqty");
            lblavalTotalqty.Text = sysqty.ToString();

            Label lblprealTotalqty = (Label)e.Row.FindControl("lblprealTotalqty");
            lblprealTotalqty.Text = Preqty.ToString();

            Label lblcompTotalqty = (Label)e.Row.FindControl("lblcompTotalqty");
            lblcompTotalqty.Text = comqty.ToString();
        }

    }
}
