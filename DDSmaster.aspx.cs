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

public partial class DDSmaster : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    SqlQuery qry = new SqlQuery();
    DDSquery dds = new DDSquery();   

    string sessionuserid = "";
  // static string val = "",DDSstatus="",btnval = "";
   int a = 0, b = 0, c = 0;   
   protected void Page_Load(object sender, EventArgs e)
   {
       if (!IsPostBack)
       {
           locationreader();
           DDSGrid.PageIndex = 0;
           gridload();
          // Btncelledit.Visible = false;
           btnsubmit.Visible = true;
           btnupdate.Visible = false;
       }
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
    protected void btnadd_Click(object sender, EventArgs e)
    {
       
    }
    protected void btnstatus_Click(object sender, EventArgs e)
    {
       
    }
    public void locationreader()
    {
        ddlpharloc.Items.Clear();  
        

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
                        ddlpharloc.Items.Add(dr[0].ToString());
                    }
                   
                }
            }
        }
    }

    //protected void btnsubmit_Click(object sender, EventArgs e)
    protected void btnsubmit_Click(object sender, ImageClickEventArgs e)
    {                   
            ddsinsert();       
    }

    protected void btnupdate_Click(object sender, ImageClickEventArgs e)
    {
        ddsupdate();
        btnsubmit.Visible = true;
        btnupdate.Visible = false;
    }

    // * Grid Display Function * \\
    public void gridload()
    {
        dds.ddsgrid(DDSGrid,ddlpharloc.SelectedValue.ToString());
        DataSet dsData = DDSGrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(DDSGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(DDSGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }

    public void activeinactive()
    {        
        for (int i = 0; i < DDSGrid.Rows.Count; i++)
        {
            
            
            GridViewRow row = DDSGrid.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("chkrow")).Checked;
            
            if (isChecked)
            {
                c++;
                string ddsno = (DDSGrid.Rows[i].Cells[5].Text);
                string act= "Active";
                string inc = "Inactive";
                if (act == ddsno)
                {
                    a++;                    
                }
                else if(ddsno==inc)
                {
                    b++;        
                }                
            }
        }
    }

    public void enabled()
    {
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
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Records are not selected');</script>", false);
        }
        else
        {
            gridload();
        }
    }
    //protected void Button5_Click(object sender, EventArgs e)
    //{
    //  gridload(); 
    //}
    protected void btnok_Click(object sender, EventArgs e)
    {
        if (ViewState["btnval"].ToString() == "1")
        {
            for (int i = 0; i < DDSGrid.Rows.Count; i++)
            {
                GridViewRow row = DDSGrid.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkrow")).Checked;

                if (isChecked)
                {
                    string ddsno = (DDSGrid.Rows[i].Cells[2].Text);
                    dds.ddsenabled(ddsno, sessionuserid = Session["Userid"].ToString(),"Active");
                }
            }
            gridload();
        }
        else if (ViewState["btnval"].ToString() == "2")
        {
            {
                for (int i = 0; i < DDSGrid.Rows.Count; i++)
                {
                    GridViewRow row = DDSGrid.Rows[i];
                    bool isChecked = ((CheckBox)row.FindControl("chkrow")).Checked;

                    if (isChecked)
                    {
                        string ddsno = (DDSGrid.Rows[i].Cells[2].Text);                       
                        dds.ddsenabled(ddsno, sessionuserid = Session["Userid"].ToString(), "Inactive");
                    }
                }
                gridload();
            }
        }
    }
   

    public void disabled()
    {
        if (b!= 0 & a == 0)
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
            gridload();
        }
        //{
        //    for (int i = 0; i < DDSGrid.Rows.Count; i++)
        //    {
        //        GridViewRow row = DDSGrid.Rows[i];
        //        bool isChecked = ((CheckBox)row.FindControl("chkrow")).Checked;

        //        if (isChecked)
        //        {
        //            string ddsno = (DDSGrid.Rows[i].Cells[2].Text);
        //            dds.ddsdisabled(ddsno, sessionuserid = Session["Userid"].ToString());
        //        }
        //    }
        //}
    }
    
    //protected void btnactive_Click(object sender, EventArgs e)
    protected void btnactive_Click(object sender, ImageClickEventArgs e)
    {
        //DDSstatus = "";
        //btnval = "";
        //btnval = "1";
        ViewState["btnval"] = "1";
        activeinactive();
        enabled();
       // gridload();    
    }
    //protected void btndeactive_Click(object sender, EventArgs e)
    protected void btndeactive_Click(object sender, ImageClickEventArgs e)
    {
       // DDSstatus = "";
        //btnval = "";
        //btnval = "2";
        ViewState["btnval"] = "2";
        activeinactive();       
        disabled();
       // gridload();
    }
    protected void DDSGrid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
      //  DDSstatus = "";    
        txtddsno.ReadOnly = true;
        txtddsno.BackColor = System.Drawing.Color.Silver;
        btnsubmit.Visible = false;
        btnupdate.Visible = true;
        txtddsno.Text = (DDSGrid.Rows[e.NewSelectedIndex].Cells[2].Text);       
        locationreader();
        ddlpharloc.Items.Remove(DDSGrid.Rows[e.NewSelectedIndex].Cells[3].Text);
        ddlpharloc.Items.Insert(0,new ListItem (DDSGrid.Rows[e.NewSelectedIndex].Cells[3].Text));
       // DDSstatus = (DDSGrid.Rows[e.NewSelectedIndex].Cells[5].Text);
        string autocart = (DDSGrid.Rows[e.NewSelectedIndex].Cells[4].Text);
        if (autocart == "Yes")
        {
            chkauto.Checked = true;
        }
        else if (autocart == "No")
        {
            chkauto.Checked = false;
        }
        //Btncelledit.Visible = true;
        descriptionreader();
    }
    protected void ddlpharloc_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridload();
    }
    // * Auto Activation Checkbox Function * \\
    public void autoactivationcheck()
    {
       // val = "";
        ViewState["val"] = "";
        if (chkauto.Checked == true)
        {
            ViewState["val"] = "Yes";
        }
        else if (chkauto.Checked == false)
        {
            ViewState["val"] = "No";
        }
    }

    public void ddsinsert()
    {
        autoactivationcheck();
        int Rtnval = dds.DDSinsert(ddlpharloc.SelectedValue.ToString(), txtddsno.Text.Trim(), sessionuserid = Session["Userid"].ToString(), txtdescription.Text, ViewState["val"].ToString());
        if (Rtnval == 1)
        {           
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Machine Name Created');</script>", false);
            DDSGrid.PageIndex = 0;
            gridload();
            //txtddsno.Text = "";
            txtdescription.Text = "";
           // ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popup", "Cellpopup()", true);
        }
        else if (Rtnval == 2)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Machine name already exist');</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
        }   
         
    }
    
    public void ddsupdate()
    {
      autoactivationcheck();
      int Rtnvalue = dds.DDSupdate(ddlpharloc.SelectedValue.ToString(), txtddsno.Text.Trim(), sessionuserid = Session["Userid"].ToString(), txtdescription.Text, ViewState["val"].ToString());
      if (Rtnvalue == 1)
      {
          ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated');</script>", false);
          DDSGrid.PageIndex = 0;
          gridload();
          txtddsno.ReadOnly = false;
          txtddsno.BackColor = System.Drawing.Color.White;
          txtddsno.Text = "";
          txtdescription.Text = "";
          btnsubmit.Visible = true;
          btnupdate.Visible = false;
          chkauto.Checked = false;          
      }
      else if (Rtnvalue == 2)
      {
          ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Remove the queue series in this DDS/BDS .Go to queue series master');</script>", false);
      }
      else
      {
          ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Error occured ');</script>", false);
      }
    }

    public void descriptionreader()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select Description From DDS where DDS_Name='" + txtddsno.Text + "'";
            cmd = new SqlCommand(Commt, con);
            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtdescription.Text = dr[0].ToString();
                    }
                }
            }
        }
    }

    //protected void btnadd_Click1(object sender, EventArgs e)
    protected void btnadd_Click1(object sender, ImageClickEventArgs e)
    {
        DDSGrid.PageIndex = 0;
        gridload();
        txtddsno.ReadOnly = false;
        txtddsno.BackColor = System.Drawing.Color.White;
        txtddsno.Text = "";
        txtdescription.Text = "";
        btnsubmit.Visible = true;
        btnupdate.Visible = false;
        chkauto.Checked = false;
       // Btncelledit.Visible = false;
        locationreader();
    }
    protected void DDSGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridload();
        DDSGrid.PageIndex = e.NewPageIndex;
        DDSGrid.DataBind();
        DataSet dsData = DDSGrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(DDSGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(DDSGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    if (DDSstatus == "Active")
    //    {
    //        txtddsno.ReadOnly = false;
    //        txtddsno.BackColor = System.Drawing.Color.White;
    //        txtddsno.Text = "";
    //        txtdescription.Text = "";
    //        btnsubmit.Text = "Save";
    //        chkauto.Checked = false;
    //        Btncelledit.Visible = false;
    //        locationreader();
    //        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' DDS is active mode,Inactive and do the operation ');</script>", false);
    //    }
    //    else if (DDSstatus == "Inactive")
    //    {           
    //        ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popup", "Cellpopup()", true);
    //    }       
    //}
    protected void DDSGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        gridload();
        DataSet dsData = DDSGrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = DDSGrid.PageIndex;
        DDSGrid.DataSource = SortDataTable(dtData, false);
        DDSGrid.DataBind();
        DDSGrid.PageIndex = pageIndex;  
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
