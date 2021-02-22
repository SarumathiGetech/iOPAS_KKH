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

public partial class SMScontact : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    smsclass sms = new smsclass();
    string sessionuserid = "";
    string actval = "";
   // public static string userid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Userid"] != null)
        {
            sessionuserid = Session["Userid"].ToString();
        }
        else
        {
            //Response.Redirect("opas.html?Session=End");
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "popup", "closepopup()", true);
        }
        if (!IsPostBack)
        {
            txtcontact.Text = Request.QueryString["name"];
            griduser.PageIndex = 0;
            usergrid();
            btnsave.Visible = true;
            btnupdate.Visible = false;
        }
        txtmobnum.Attributes["onkeypress"] = "return Intcheck(event);";
    }
    //protected void btnsave_Click(object sender, EventArgs e)
    protected void btnsave_Click(object sender, ImageClickEventArgs e)
    {
        griduser.PageIndex = 0;
        checkbox();         
        Userinsert();       
        clear();
        usergrid();
    }
    //protected void btnupdate_Click(object sender, EventArgs e)
    protected void btnupdate_Click(object sender, ImageClickEventArgs e)
    {
        griduser.PageIndex = 0;
        checkbox();       
        userupdate();
        clear();
        usergrid();
    }

    //* User Insert Function *\\
    public void Userinsert()
    {
       int Rtnvalue= sms.SMSuserinsert(txtcontact.Text.Trim(), txtcode.Text.Trim() , txtmobnum.Text.Trim(), txtdescription.Text.Trim(), sessionuserid = Session["Userid"].ToString(), actval);
       if (Rtnvalue == 1)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<Script> alert('Contact person created');</script>", false);
       }
       else if (Rtnvalue == 2)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<Script> alert('Mobile number already exist');</script>", false);
       }
       else
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<Script> alert('Error Occured');</script>", false);
       }
     }

    // * User Update Function * \\
    public void userupdate()
    {
        int rtnval = sms.smsuserupdate(Convert.ToInt32(ViewState["userid"].ToString()), txtcontact.Text.Trim(), txtmobnum.Text.Trim(), txtdescription.Text.Trim(), sessionuserid = Session["Userid"].ToString(), actval);
       if (rtnval == 1)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<Script> alert('Contact person updated');</script>", false);
       }
       else if (rtnval == 2)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<Script> alert('Mobile Number Already Exist');</script>", false);
           btnupdate.Visible = true;
           btnsave.Visible = false;
       }
       else
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<Script> alert('Error Occured');</script>", false);
       }
    }

    // * Active Check Box Function * \\
    public void checkbox()
    {
        if (chkactive.Checked == true)
        {
            actval = "Yes";
        }
        else if (chkactive.Checked == false)
        {
            actval = "No";
        }
    }

    // * User Grid Display * \\
    public void usergrid()
    {
        sms.smsusergrid(griduser,txtcontact.Text.Trim());
        DataSet dsData = griduser.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(griduser.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(griduser.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }

    // * Mobile Number Exist Or Not Check * \\
        ////public void mobilenocheck()
        ////{
        ////    SqlDataReader dr = sms.usermobilenocheck(txtmobnum.Text);
        ////    if (dr.HasRows)
        ////    {
        ////        while (dr.Read())
        ////        {
        ////            mobcheck = dr[0].ToString();
        ////            editid = dr[1].ToString();
        ////        }
        ////        dr.Close();
        ////    }
        ////}

    protected void griduser_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //userid =Convert.ToString(griduser.DataKeys[e.NewSelectedIndex].Value); 
        ViewState["userid"] = "";
        ViewState["userid"] = Convert.ToString(griduser.DataKeys[e.NewSelectedIndex].Value); 
        txtcontact.Text=(griduser.Rows[e.NewSelectedIndex].Cells[2].Text);
        txtmobnum.Text = (griduser.Rows[e.NewSelectedIndex].Cells[3].Text);
        if (griduser.Rows[e.NewSelectedIndex].Cells[4].Text == "Yes")
        {
            chkactive.Checked = true;
        }
        else if (griduser.Rows[e.NewSelectedIndex].Cells[4].Text == "No")
        {
            chkactive.Checked = false;
        }
        Descriptionreader();
        btnsave.Visible = false;
        btnupdate.Visible = true;
    }

    // * User Description Reader * \\
    public void Descriptionreader()
    {

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select Description,country_code from SMS_user where MobileNo='" + txtmobnum.Text.Trim() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtdescription.Text = dr[0].ToString();
                        txtcode.Text = dr[1].ToString();
                    }
                }
            }
        }
    }

    //protected void btnclear_Click(object sender, EventArgs e)
    protected void btnclear_Click(object sender, ImageClickEventArgs e)
    {
        clear();
    }
    // * Text Box Clear * \\
    public void clear()
    {
        txtcontact.Text = "";
        txtdescription.Text = "";
        txtmobnum.Text = "";
        btnupdate.Visible = false;
        btnsave.Visible = true;
    }
    protected void griduser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if ((e.Row.RowType == DataControlRowType.DataRow))
        //{
        //    ((Button)e.Row.FindControl("btnSelect")).Attributes.Add("onclick", "javascript:passValueToParent('" + e.Row.Cells[3].Text + "','" + e.Row.Cells[4].Text + "')");
        //}
    }
    //protected void btnsearch_Click(object sender, EventArgs e)
    protected void btnsearch_Click(object sender, ImageClickEventArgs e)
    {
        griduser.PageIndex = 0;
        usergrid();
    }
    protected void griduser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Btn")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = griduser.Rows[index];
            if (selectedRow.Cells[4].Text == "Yes")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popup", "passValueToParent('" + selectedRow.Cells[2].Text + "','" + selectedRow.Cells[3].Text + "')", true);
            }
            else if (selectedRow.Cells[4].Text == "No")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<Script> alert('Selected record is inactive');</script>", false);
            }

        }
    }
    protected void griduser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        usergrid();
        griduser.PageIndex = e.NewPageIndex;
        griduser.DataBind();
        DataSet dsData = griduser.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(griduser.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(griduser.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
    protected void griduser_Sorting(object sender, GridViewSortEventArgs e)
    {
        usergrid();
        DataSet dsData = griduser.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = griduser.PageIndex;
        griduser.DataSource = SortDataTable(dtData, false);
        griduser.DataBind();
        griduser.PageIndex = pageIndex; 
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