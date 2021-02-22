using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Datalayer;
using System.Drawing;
using System.Data;

public partial class Processingmaster : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    DDSquery dds = new DDSquery();
    string sessionuserid="";
  //  static int processid;
    protected void Page_Load(object sender, EventArgs e)
    {                  
        try
        {
            sessionuserid = Session["Userid"].ToString();                
        }
        catch (NullReferenceException)
        {
            //Response.Redirect("opas.html?Session=End");
            Response.Redirect("iopas.html");
        }

        if (!IsPostBack)
        {
            locationreader();
            ddsnamereader();
            Packtypereader();
            procssgrid.PageIndex = 0;
            griddisplay();
            btnsave.Visible = true;
            btnupdate.Visible = false;

            if ((ddlpacktype.SelectedValue.ToString().ToLower() == "Bottle".ToLower()) || (ddlpacktype.SelectedValue.ToString().ToLower() == "BOX".ToLower()))
            {
                txtbagpercontainer.Enabled = false;
                txtbagpercontainer.Text = "1";
            }
            else
            {
                txtbagpercontainer.Enabled = true;
                txtbagpercontainer.Text = "";
            }
        }
    }

    // * DDS Name reader * \\
    public void ddsnamereader()
    {
        ddlddsno.Items.Clear();


        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select d.DDS_Name from dds as d left join Pharmacy as p on p.PharmacyID=d.PharmacyID where p.Location_Name='" + ddlphar.SelectedValue.ToString() + "' and d.Status='Active'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddlddsno.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }

    // * Pharmacy Location Name Reader * \\
    public void locationreader()
    {
        ddlphar.Items.Clear();
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
                        ddlphar.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }

    // * Pack Type Reader Function * \\
    public void Packtypereader()
    {
        ddlpacktype.Items.Clear();

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select PackType from Packtype_Master";
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

    //protected void btnsave_Click(object sender, EventArgs e)
    protected void btnsave_Click(object sender, ImageClickEventArgs e)
    {
        insert();
        procssgrid.PageIndex = 0;
        griddisplay();
    }

    protected void btnupdate_Click(object sender, ImageClickEventArgs e)
    {
        Update();
        procssgrid.PageIndex = 0;
        griddisplay();
        btnsave.Visible = true;
        btnupdate.Visible = false;
    }

    // * Processing Insert * \\
    public void insert()
    {
        int Rtnvalue = dds.Processinginsert(ddlphar.SelectedValue.ToString(), ddlpacktype.SelectedValue.ToString(), Convert.ToInt32(txtcartqty.Text.Trim()), ddlddsno.SelectedValue.ToString(), Convert.ToInt32(txtbagpercontainer.Text.Trim()), sessionuserid);
        if (Rtnvalue == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record created');</script>", false);
           // clear();
        }
        else if (Rtnvalue == 2)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Pack type and DDS Name already exist for this location');</script>", false);
        }
        else if (Rtnvalue == 3)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please check bag per container,previous record in this DDS');</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
        }
    }

    // * Processing Update * \\
    public void Update()
    {
        int Rtnval = dds.Processingupdate((int)ViewState["processid"], ddlphar.SelectedValue.ToString(), ddlpacktype.SelectedValue.ToString(), Convert.ToInt32(txtcartqty.Text.Trim()), ddlddsno.SelectedValue.ToString(), Convert.ToInt32(txtbagpercontainer.Text.Trim()), sessionuserid);
        if (Rtnval == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated');</script>", false);
            btnsave.Visible = true;
            btnupdate.Visible = false;
            clear();
        }
        else if (Rtnval == 2)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Pack type and DDS Name already exist for this location');</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
        }
    }

    // * Grid Display * \\
    public void griddisplay()
    {
        dds.processinggrid(procssgrid,ddlphar.SelectedValue.ToString());
        DataSet dsData = procssgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(procssgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(procssgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
    protected void procssgrid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        txtbagpercontainer.Text = "";
        ViewState["processid"] = "";
        ViewState["processid"] = (int)procssgrid.DataKeys[e.NewSelectedIndex].Value;
        //processid = (int)procssgrid.DataKeys[e.NewSelectedIndex].Value;  
        locationreader();
        ddlphar.Items.Remove(procssgrid.Rows[e.NewSelectedIndex].Cells[2].Text);
        ddlphar.Items.Insert(0, new ListItem(procssgrid.Rows[e.NewSelectedIndex].Cells[2].Text));
        Packtypereader();
        ddlpacktype.Items.Remove(procssgrid.Rows[e.NewSelectedIndex].Cells[3].Text);
        ddlpacktype.Items.Insert(0,new ListItem(procssgrid.Rows[e.NewSelectedIndex].Cells[3].Text));
        txtcartqty.Text=(procssgrid.Rows[e.NewSelectedIndex].Cells[4].Text);
        ddsnamereader();
        ddlddsno.Items.Remove(procssgrid.Rows[e.NewSelectedIndex].Cells[5].Text);
        ddlddsno.Items.Insert(0, new ListItem(procssgrid.Rows[e.NewSelectedIndex].Cells[5].Text));
        txtbagpercontainer.Text = (procssgrid.Rows[e.NewSelectedIndex].Cells[6].Text);
        btnsave.Visible = false;
        btnupdate.Visible = true;

        if ((ddlpacktype.SelectedValue.ToString().ToLower() == "STRIP".ToLower()) || (ddlpacktype.SelectedValue.ToString().ToLower() == "BUNDLE".ToLower()))
        {
            
            //txtbagpercontainer.Text = "1";
            txtbagpercontainer.Enabled = true; 
        }
        else
        {
            txtbagpercontainer.Enabled = false;
        }

    }
    //protected void btnadd_Click(object sender, EventArgs e)
    protected void btnadd_Click(object sender, ImageClickEventArgs e)
    {
        clear();
    }

    // * textbox value Clear * \\
    public void clear()
    {
        txtbagpercontainer.Text = "";
        txtcartqty.Text = "";
        //locationreader();
       // Packtypereader();
        //ddsnamereader();
        btnsave.Visible = true;
        btnupdate.Visible = false;
    }
    protected void ddlphar_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddsnamereader();
        griddisplay();
    }
    protected void procssgrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        griddisplay();
        procssgrid.PageIndex = e.NewPageIndex;
        procssgrid.DataBind();
        DataSet dsData = procssgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(procssgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(procssgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
    protected void ddlddsno_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void procssgrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        griddisplay();
        DataSet dsData = procssgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = procssgrid.PageIndex;
        procssgrid.DataSource = SortDataTable(dtData, false);
        procssgrid.DataBind();
        procssgrid.PageIndex = pageIndex; 
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

    protected void ddlpacktype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((ddlpacktype.SelectedValue.ToString().ToLower() == "STRIP".ToLower()) || (ddlpacktype.SelectedValue.ToString().ToLower() == "BUNDLE".ToLower()) )
        {
            txtbagpercontainer.Enabled = true;
            txtbagpercontainer.Text = "";
        }
        else
        { 
            txtbagpercontainer.Enabled = false;
            txtbagpercontainer.Text = "1";
        }
    }
}