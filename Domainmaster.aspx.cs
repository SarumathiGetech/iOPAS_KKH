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

public partial class Domainmaster : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    SqlQuery qry = new SqlQuery();
    grid gri = new grid();
    string sessionuserid="";    
   // public static int domainid;
    int a = 0, b = 0, c = 0;
    //static string btnval = "";
   
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
            griddomain.PageIndex = 0;
            gridreading();
            btnsave.Visible = true;
            btnupdate.Visible = false;
        }          
    }

    //protected override void OnInit(EventArgs e)
    //{
    //    base.OnInit(e);

    //    //btnIncrement.Click += new EventHandler(btnIncrement_Click);
    //    //btnIncrement.Text = "Click Me";
    //    //Controls.Add(btnIncrement);
    //}



    //protected void btnsave_Click(object sender, EventArgs e)
    protected void btnsave_Click(object sender, ImageClickEventArgs e)
    {
                    
            int Rtnval = qry.Domaininsert(txtdomain.Text.Trim(),txtroot.Text.Trim(),txtcom.Text.Trim(),txtsg.Text.Trim(), txtdescrip.Text.Trim(), sessionuserid = Session["Userid"].ToString(),txtdmnname.Text.Trim());
            if (Rtnval == 1)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record created ');</script>", false);
                domainclear();
                gridreading();
            }
            else if (Rtnval == 2)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Domain already exist ');</script>", false);
            }
            else if (Rtnval == 3)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Domain name already exist ');</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Error occured ');</script>", false);
            }
        
    }

    protected void btnupdate_Click(object sender, ImageClickEventArgs e)
    {

        int Rtnvalue = qry.Domainupdate(txtdomain.Text.Trim(), txtroot.Text.Trim(), txtcom.Text.Trim(), txtsg.Text.Trim(), txtdescrip.Text.Trim(), sessionuserid = Session["Userid"].ToString(), (int)ViewState["domainid"], txtdmnname.Text.Trim());
        if (Rtnvalue == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated ');</script>", false);
            domainclear();
            gridreading();
        }
        else if (Rtnvalue == 2)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Domain already exist ');</script>", false);
        }
        else if (Rtnvalue == 3)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Domain name already exist ');</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Error occured ');</script>", false);
        }
    }

    public void domainclear()
    {
        txtdomain.Text = "";
        txtdescrip.Text = "";
        txtroot.Text = "";
        txtdmnname.Text = "";
        btnsave.Visible = true;
        btnupdate.Visible = false;
        txtdomain.ReadOnly = false;
        txtcom.ReadOnly = false;
        txtsg.ReadOnly = false;
        txtroot.ReadOnly = false;
        txtcom.Text = "com";
        txtsg.Text = "sg";
    }

    public void gridreading()
    {
        gri.domaingrid(griddomain);
        DataSet dsData = griddomain.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(griddomain.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(griddomain.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }   
    
    protected void griddomain_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
       //txtdomain.Text = (griddomain.Rows[e.NewSelectedIndex].Cells[3].Text);
        ViewState["domainid"] = "";
        ViewState["domainid"] = (int)griddomain.DataKeys[e.NewSelectedIndex].Value;        
       //domainid = (int)griddomain.DataKeys[e.NewSelectedIndex].Value; 
      // domainid =Convert.ToInt32((griddomain.Rows[e.NewSelectedIndex].Cells[2].Text));
       descriptionreader();
       if (txtdomain.Text.ToLower() == "Local".ToLower())
       {
           txtdomain.ReadOnly = true;
           txtcom.ReadOnly = true;
           txtsg.ReadOnly = true;
           txtroot.ReadOnly = true;
       }
       else if (txtdomain.Text.ToLower() != "Local".ToLower())
       {
           txtdomain.ReadOnly = false;
           txtcom.ReadOnly = false;
           txtsg.ReadOnly = false;
           txtroot.ReadOnly = false;
       }
       btnsave.Visible = false;
       btnupdate.Visible = true;
      
    }

    public void descriptionreader()
    {

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select DomainName,CN,DC2,DC3,description,DomainNme from Domain where DomainID='" + (int)ViewState["domainid"] + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtdomain.Text = dr[0].ToString();
                        txtroot.Text = dr[1].ToString();
                        txtcom.Text = dr[2].ToString();
                        txtsg.Text = dr[3].ToString();
                        txtdescrip.Text = dr[4].ToString();
                        txtdmnname.Text = dr[5].ToString();
                    }
                }
            }
        }
    }
    
    //protected void btnadd_Click(object sender, EventArgs e)
    protected void btnadd_Click(object sender, ImageClickEventArgs e)
    {
        domainclear();
      //  ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popup", "sectime()", true);
    }
    // * DOMAIN ACTIVE INACTIVE STATUS FUNCTIONS * \\
    public void activeinactive()
    {

        for (int i = 0; i < griddomain.Rows.Count; i++)
        {
            GridViewRow row = griddomain.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("chkrow")).Checked;

            if (isChecked)
            {
                c++;
                string DMSTATUS = (griddomain.Rows[i].Cells[4].Text);
                string act = "Active";
                string inc = "Inactive";
                if (act.ToLower() == DMSTATUS.ToLower())
                {
                    a++;
                }
                else if (DMSTATUS.ToLower() == inc.ToLower())
                {
                    b++;
                }
            }
        }
    }
    //protected void btnactive_Click(object sender, EventArgs e)
    protected void btnactive_Click(object sender, ImageClickEventArgs e)
    {
        
        ViewState["btnval"] = "1";
        //btnval = "";
        //btnval = "1";
        activeinactive();
        if (a != 0 & b == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Selected records are already active');</script>", false);
        }
        else if (a != 0 & b != 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Selected records are of different status');</script>", false);
        }
        else if (c != 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "popup", "confirmProcess()", true);
        }
        else if (c == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Records are not selected ');</script>", false);
        }
        else
        {
            gridreading();
        }
    }
    //protected void btndeactive_Click(object sender, EventArgs e)
    protected void btndeactive_Click(object sender, ImageClickEventArgs e)
    {
        activeinactive();
        ViewState["btnval"] = "2";
        //btnval = "";
        //btnval = "2";

        if (b != 0 & a == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Selected records are already Inactive');</script>", false);
        }
        else if (a != 0 & b != 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Selected records are of different status');</script>", false);
        }
        else if (c != 0)
        {

            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "popup", "confirmProcess1()", true);
        }
        else if (c == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Records are not selected');</script>", false);
        }
        else
        {
            gridreading();
        }
        
    }
    protected void btnok_Click(object sender, EventArgs e)
    {

        if ("1" == ViewState["btnval"].ToString())
        {
            for (int i = 0; i < griddomain.Rows.Count; i++)
            {
                GridViewRow row = griddomain.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkrow")).Checked;

                if (isChecked)
                {
                    //domainid = Convert.ToInt32((griddomain.Rows[i].Cells[2].Text));
                    int domainid = (int)griddomain.DataKeys[i].Value;  
                    gri.domainenabled(domainid, sessionuserid = Session["Userid"].ToString());
                }
            }
            gridreading();
        }

        else if ( "2" == ViewState["btnval"].ToString())
        {
            for (int i = 0; i < griddomain.Rows.Count; i++)
            {
                GridViewRow row = griddomain.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkrow")).Checked;

                if (isChecked)
                {
                   // domainid = Convert.ToInt32((griddomain.Rows[i].Cells[2].Text));
                    int domainid = (int)griddomain.DataKeys[i].Value;
                    gri.domaindisabled(domainid, sessionuserid = Session["Userid"].ToString());
                }
            }
            gridreading();
        }
    }

    protected void griddomain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridreading();
        griddomain.PageIndex = e.NewPageIndex;
        griddomain.DataBind();
        DataSet dsData = griddomain.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(griddomain.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(griddomain.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
    protected void griddomain_Sorting(object sender, GridViewSortEventArgs e)
    {
        gridreading(); 
        DataSet dsData = griddomain.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = griddomain.PageIndex;
        griddomain.DataSource = SortDataTable(dtData, false);
        griddomain.DataBind();
        griddomain.PageIndex = pageIndex; 
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


    
}
