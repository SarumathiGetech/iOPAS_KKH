using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using Datalayer;
using System.Drawing;
using System.Data;
public partial class ReasonMaster : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    grid gri = new grid();
    string sessionuserid = "";      
    int a = 0, b = 0, c = 0;
   //public static string btnval = "";
  // public static int Reasonid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txterror.Attributes.Add("onfocus", "clearText()");
            //txterror.Attributes.Add("onblur", "resetText()"); 
            gridreason.PageIndex = 0;
            reasongrid();
            btnprocess.Visible = true;
            btnupdate.Visible = false;
            try
            {
                sessionuserid = Session["Userid"].ToString();
            }
            catch (NullReferenceException)
            {
                //Response.Redirect("opas.html?Session=End");
                Response.Redirect("iopas.html");
            }
        }
    }
    protected void btnprocess_Click(object sender, ImageClickEventArgs e)
    {

        reasoninsert();
        gridreason.PageIndex = 0;
        reasongrid();
    }
    protected void btnupdate_Click(object sender, ImageClickEventArgs e)
    {
        reasonupdate();       
        txtreason.ReadOnly = false;
        clear();
        gridreason.PageIndex = 0;
        reasongrid();
        btnprocess.Visible = true;
        btnupdate.Visible = false;
    }

    // * Rejected Reason insert * \\
    public void reasoninsert()
    {       
       int Rtnvalue= gri.rejectedinsert(ddltype.SelectedItem.ToString(),txtreason.Text.Trim(), txtdetails.Text.Trim(), sessionuserid = Session["Userid"].ToString());
       if (Rtnvalue == 1)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record created');</script>", false);
           clear();
       }
       else if (Rtnvalue == 2)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Reason already exist');</script>", false);
       }
       else
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occured');</script>", false);
       }
    }

     //*Reason Grid Display * \\
    public void reasongrid()
    {
        gri.reasongrid(gridreason);
        DataSet dsData = gridreason.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(gridreason.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridreason.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
    protected void gridreason_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
       ViewState["Reasonid"]="";
        //Reasonid = (int)gridreason.DataKeys[e.NewSelectedIndex].Value;
       ViewState["Reasonid"] = (int)gridreason.DataKeys[e.NewSelectedIndex].Value;
        txtreason.Text = (gridreason.Rows[e.NewSelectedIndex].Cells[3].Text);
        btnprocess.Visible = false;
        btnupdate.Visible = true;
        txtreason.ReadOnly = true;
        ddltype.Items.Clear();
        ddltype.Items.Add("Inventory Discrepancy");
        ddltype.Items.Add("Verification Rejection");
        ddltype.Items.Remove((gridreason.Rows[e.NewSelectedIndex].Cells[2].Text));
        ddltype.Items.Insert(0, new ListItem(gridreason.Rows[e.NewSelectedIndex].Cells[2].Text));
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select Details from Rejected_reason where ID='" + (int)ViewState["Reasonid"] + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtdetails.Text = dr[0].ToString();
                    }
                }
            }
        }

    }

    // * Rejected Reason Update * \\
    public void reasonupdate()
    {
        int Rtnvalue = gri.reasonupdate((int)ViewState["Reasonid"], ddltype.SelectedValue.ToString(), txtreason.Text.Trim(), txtdetails.Text.Trim(), sessionuserid = Session["Userid"].ToString());
      if (Rtnvalue == 1)
      {
          ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated');</script>", false);
      }

      else if (Rtnvalue == 2)
      {
          ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Reason already exist');</script>", false);
      }
      else
      {
          ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
      }
    }
    // * Reason textbox clear Function * \\
    public void clear()
    {
        txtreason.Text = "";
        txtdetails.Text = "";
        btnprocess.Visible = true;
        btnupdate.Visible = false;
        txtreason.ReadOnly = false;        
        txtreason.Focus();
    }
   
    protected void btnadd_Click(object sender, EventArgs e)
    {
        clear();
    }
    // * CARTRIDGE ACTIVE INACTIVE STATUS FUNCTIONS * \\
    public void activeinactive()
    {

        for (int i = 0; i < gridreason.Rows.Count; i++)
        {

            GridViewRow row = gridreason.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("chkphar")).Checked;

            if (isChecked)
            {
                c++;
                string status = (gridreason.Rows[i].Cells[4].Text);
                string act = "Active";
                string inc = "Inactive";
                if (act == status)
                {
                    a++;
                }
                else if (status == inc)
                {
                    b++;
                }
            }
        }
    }

    //protected void btnactive_Click(object sender, EventArgs e)
    protected void btnactive_Click(object sender, ImageClickEventArgs e)
    {
        //btnval = "";
        //btnval = "1";
        ViewState["btnval"] = "";
        ViewState["btnval"] = "1";
        activeinactive();
        if (a != 0 & b == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Selected records are already Active ');</script>", false);
        }
        else if (a != 0 & b != 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Selected records are of different status ');</script>", false);
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
            reasongrid();
        }
    }

    //protected void btndeactive_Click(object sender, EventArgs e)
    protected void btndeactive_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["btnval"] = "";
        ViewState["btnval"] = "2";
        activeinactive();
        if (b != 0 & a == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Selected records are already Inactive ');</script>", false);
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
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Records are not selected ');</script>", false);
        }
        else
        {
            reasongrid();
        }


    }

    protected void btnok_Click(object sender, EventArgs e)
    {
        if (ViewState["btnval"].ToString() == "1")
        {
            for (int i = 0; i < gridreason.Rows.Count; i++)
            {
                GridViewRow row = gridreason.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkphar")).Checked;

                if (isChecked)
                {
                    int Reason = (int)gridreason.DataKeys[i].Value;  
                    //string errortype = (gridreason.Rows[i].Cells[3].Text);
                    gri.reasonenable(Reason, sessionuserid = Session["Userid"].ToString());
                }
            }
            reasongrid();
        }

        else if (ViewState["btnval"].ToString() == "2")
        {
            for (int i = 0; i < gridreason.Rows.Count; i++)
            {
                GridViewRow row = gridreason.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkphar")).Checked;

                if (isChecked)
                {
                    int Reason = (int)gridreason.DataKeys[i].Value;  
                  //  string errortype = (gridreason.Rows[i].Cells[3].Text);
                    gri.reasonDisabled(Reason, sessionuserid = Session["Userid"].ToString());
                }
            }
            reasongrid();
        }
    }

    protected void gridreason_Sorting(object sender, GridViewSortEventArgs e)
    {
        reasongrid();
        DataSet dsData = gridreason.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = gridreason.PageIndex;
        gridreason.DataSource = SortDataTable(dtData, false);
        gridreason.DataBind();
        gridreason.PageIndex = pageIndex; 
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
    protected void gridreason_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        reasongrid();
        gridreason.PageIndex = e.NewPageIndex;
        gridreason.DataBind();
        DataSet dsData = gridreason.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(gridreason.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridreason.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnadd_Click(object sender, ImageClickEventArgs e)
    {

    }
}