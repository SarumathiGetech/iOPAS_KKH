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
public partial class Printer : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    grid gri = new grid();
    string sessionuserid = "",chkfunctionval="",chkdefaultval = "";    
    int a = 0, b = 0, c = 0;
    //static string btnval = "";
  //  public static int Printerid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            locationreader();            
            try
            {
                sessionuserid = Session["Userid"].ToString();
            }
            catch (NullReferenceException)
            {
                //Response.Redirect("opas.html?Session=End");
                Response.Redirect("iopas.html");
            }
            btnadd.Visible = true;
            btnupdate.Visible = false;
            griddetail.PageIndex = 0;
            printergrid();           
        }
    }
   
    //* Pharmacy Location Reader * \\
    public void locationreader()
    {
        ddlpharadd.Items.Clear();

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select Location_Name from Pharmacy where Status='Active'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddlpharadd.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }

    // * New Printer Name Insert Function * \\
    public void printerinsert()
    {
      int Rtnvalue= gri.printerinsert(ddlpharadd.SelectedItem.ToString(), txtprintername.Text.Trim(), "", txtdesc.Text.Trim(), sessionuserid = Session["Userid"].ToString());
      if (Rtnvalue == 1)
      {
          if (chkfdl.Checked == true)
          {
              functioninsertone();
          }
         
          if (chkfcl.Checked == true)
          {
              functioninserthree();
          }
          printergrid();
          clear();
          ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record created');</script>", false);
      }
      else if (Rtnvalue == 2)
      {
          ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Printer name already exist');</script>", false);
      }
      else if (Rtnvalue == 3)
      {
          ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('IP / Host name already exist');</script>", false);
      }
      else
      {
          ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
      }
    }
    //* Printer Update Function * \\
    public void printerupdate()
    {
        int Rtnvalue = gri.printerupdate((int)ViewState["Printerid"], ddlpharadd.SelectedItem.ToString(), txtprintername.Text.Trim(), "", txtdesc.Text.Trim(), sessionuserid = Session["Userid"].ToString());
        if (Rtnvalue == 1)
        {
            functiondelete();
            if (chkfdl.Checked == true)
            {
                functioninsertone();
            }
       
            if (chkfcl.Checked == true)
            {
                functioninserthree();
            }
            printergrid();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated');</script>", false);
            clear();
        }
        else if (Rtnvalue == 2)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Printer name already exist');</script>", false);
        }
        else if (Rtnvalue == 3)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('IP / Host name already exist');</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
        }
    }
    //protected void btnadd_Click(object sender, EventArgs e)
    protected void btnadd_Click(object sender, ImageClickEventArgs e)
    {
       
            griddetail.PageIndex = 0;
            printerinsert();
      
    }

    protected void btnupdate_Click(object sender, ImageClickEventArgs e)
    {
        griddetail.PageIndex = 0;
        printerupdate();

        btnadd.Visible = true;
        btnupdate.Visible = false;
    }

    // * Printer Grid Display * \\
    public void printergrid()
    {
        gri.printergrid(griddetail, ddlpharadd.SelectedItem.Value);
        DataSet dsData = griddetail.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(griddetail.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(griddetail.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
    protected void ddlpharadd_SelectedIndexChanged(object sender, EventArgs e)
    {
        clear();       
        printergrid();
        checkboxclear();
    }


    // * Printer Function Insert  * \\
    public void functioninsertone()
    {
        if (chkfdl.Checked == true)
        {
            chkfunctionval = "CBL";            
        }
        else if(chkfdl.Checked==false)
        {
            chkfunctionval = "";
        }
        if (chkddl.Checked == true)
        {
            chkdefaultval = "Yes";
        }
        else if (chkddl.Checked == false)
        {
            chkdefaultval = "No";
        }
        gri.functioninsert(txtprintername.Text,ddlpharadd.SelectedValue.ToString(), "CBL", chkfunctionval, chkdefaultval);
    }
 

    public void functioninserthree()
    {
        if (chkfcl.Checked == true)
        {
            chkfunctionval = "CL";
        }
        else if (chkfcl.Checked == false)
        {
            chkfunctionval = "";
        }
        if (chkdcl.Checked == true)
        {
            chkdefaultval = "Yes";
        }
        else if (chkdcl.Checked == false)
        {
            chkdefaultval = "No";
        }
        gri.functioninsert(txtprintername.Text.Trim(),ddlpharadd.SelectedValue.ToString(),"CL", chkfunctionval, chkdefaultval);
    }

    // * Printer Function Delete(For during Update Process)
    public void functiondelete()
    {
        gri.functionupdate(txtprintername.Text.Trim(),ddlpharadd.SelectedValue.ToString()); 
    }


    // * Allotment grid Display * \\
    public void editreader()
    {        
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select f.Function_Printer,f.Default_Printer from Printer_Function as f left join Printer as p on p.PrinterID=f.PrinterID left join Pharmacy as ph on ph.PharmacyID=p.PharmacyID where p.Printer_Name='" + txtprintername.Text.Trim() + "' and ph.Location_Name='" + ddlpharadd.SelectedValue.ToString() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr[0].ToString() == "CBL" && dr[1].ToString() == "Yes")
                        {
                            chkfdl.Checked = true;
                            chkddl.Checked = true;
                        }

                        else if (dr[0].ToString() == "CL" && dr[1].ToString() == "Yes")
                        {
                            chkfcl.Checked = true;
                            chkdcl.Checked = true;
                        }
                        else if (dr[0].ToString() == "CBL")
                        {
                            chkfdl.Checked = true;
                        }

                        else if (dr[0].ToString() == "CL")
                        {
                            chkfcl.Checked = true;
                        }
                    }
                }
            }
        }
    }  
   
    protected void chkheader_CheckedChanged(object sender, EventArgs e)
    {        
        foreach (GridViewRow gvr in griddetail.Rows)
        {
            bool isChecked = ((CheckBox)(griddetail.HeaderRow.FindControl("chkheader"))).Checked;         
           
            if (isChecked)
            {
                CheckBox cb = ((CheckBox)(gvr.FindControl("chkrow")));
                cb.Checked = true;
            }
            else if (isChecked == false)
            {
               
                CheckBox cb = ((CheckBox)(gvr.FindControl("chkrow")));
                cb.Checked = false;
            }
        }
    }    

    // * textbox clear function * \\
    public void clear()
    {
        txtprintername.Text = "";
       // txtipaddress.Text = "";
        txtdesc.Text = "";
       // txtprintername.ReadOnly = false;
        btnadd.Visible = true;
        btnupdate.Visible = false;
        checkboxclear();
    }
    protected void griddetail_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        clear();
        checkboxclear();
        locationreader();
        ViewState["Printerid"] = "";
        ViewState["Printerid"] = (int)griddetail.DataKeys[e.NewSelectedIndex].Value;  
        txtprintername.Text = (griddetail.Rows[e.NewSelectedIndex].Cells[4].Text);
        //txtipaddress.Text = (griddetail.Rows[e.NewSelectedIndex].Cells[5].Text);
        ddlpharadd.Items.Remove(griddetail.Rows[e.NewSelectedIndex].Cells[3].Text);
        ddlpharadd.Items.Insert(0, new ListItem(griddetail.Rows[e.NewSelectedIndex].Cells[3].Text));
        descriptionreader();
        editreader();
       // txtprintername.ReadOnly = true;
        btnadd.Visible = false;
        btnupdate.Visible = true;
    }
    // * Printer Description reader * \\
    public void descriptionreader()
    {       
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select p.description from printer as p left join Pharmacy as ph on ph.PharmacyID=p.PharmacyID where p.printer_Name='" + txtprintername.Text.Trim() + "' and ph.Location_Name='" + ddlpharadd.SelectedValue.ToString() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtdesc.Text = dr[0].ToString();
                    }
                }
            }
        }
    }

    protected void btnclear_Click(object sender, ImageClickEventArgs e)
    {
        clear();
        griddetail.PageIndex = 0;
        printergrid(); 
    }  

    // * CARTRIDGE ACTIVE INACTIVE STATUS FUNCTIONS * \\
    public void activeinactive()
    {

        for (int i = 0; i < griddetail.Rows.Count; i++)
        {

            GridViewRow row = griddetail.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("chkrow")).Checked;

            if (isChecked)
            {
                c++;
                string ddsno = (griddetail.Rows[i].Cells[6].Text);
                string act = "Active";
                string inc = "Inactive";
                if (act == ddsno)
                {
                    a++;
                }
                else if (ddsno == inc)
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
        ViewState["btnval"] = "1";
        activeinactive();
        if (a != 0 & b == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Selected records are already Active');</script>", false);
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
            printergrid();
        }       
    }   

    //protected void btndeactive_Click(object sender, EventArgs e)
    protected void btndeactive_Click(object sender, ImageClickEventArgs e)
    {

        ViewState["btnval"] = "2";
        activeinactive();
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
            printergrid();
        }        
      
    }

    protected void btnok_Click(object sender, EventArgs e)
    {
        if (ViewState["btnval"].ToString() == "1")
        {
            for (int i = 0; i < griddetail.Rows.Count; i++)
            {
                GridViewRow row = griddetail.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkrow")).Checked;

                if (isChecked)
                {
                   // string printeid = (griddetail.Rows[i].Cells[2].Text);
                    string printeid = (string)griddetail.DataKeys[i].Value.ToString();
                    gri.printerenabled(Convert.ToInt32(printeid), sessionuserid = Session["Userid"].ToString());
                }
            }
            printergrid();
        }

        else if (ViewState["btnval"].ToString() == "2")
        {
            for (int i = 0; i < griddetail.Rows.Count; i++)
            {
                GridViewRow row = griddetail.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkrow")).Checked;

                if (isChecked)
                {
                    //string printeid = (griddetail.Rows[i].Cells[2].Text);
                    string printeid = (string)griddetail.DataKeys[i].Value.ToString();
                    gri.printerdisabled(Convert.ToInt32(printeid), sessionuserid = Session["Userid"].ToString());
                }
            }
            printergrid();
        }

    }
    protected void chkddl_CheckedChanged(object sender, EventArgs e)
    {
        //if (chkddl.Checked == true)
        //{
        //    chkdbl.Checked = false;
        //    chkdcl.Checked = false;
        //}
    }
    protected void chkdbl_CheckedChanged(object sender, EventArgs e)
    {
        //if (chkdbl.Checked == true)
        //{
        //    chkddl.Checked = false;
        //    chkdcl.Checked = false;
        //}
    }
    protected void chkdcl_CheckedChanged(object sender, EventArgs e)
    {
        //if (chkdcl.Checked == true)
        //{
        //    chkddl.Checked = false;
        //    chkdbl.Checked = false;
        //}
    }

    // * Check box Clear Function * \\
    public void checkboxclear()
    {
       
        chkdcl.Checked = false;
        chkddl.Checked = false;
        
        chkfcl.Checked = false;
        chkfdl.Checked = false;        
    }
    protected void griddetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        printergrid();
        griddetail.PageIndex = e.NewPageIndex;
        griddetail.DataBind();
        DataSet dsData = griddetail.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(griddetail.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(griddetail.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
    protected void griddetail_Sorting(object sender, GridViewSortEventArgs e)
    {
        printergrid();
        DataSet dsData = griddetail.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = griddetail.PageIndex;
        griddetail.DataSource = SortDataTable(dtData, false);
        griddetail.DataBind();
        griddetail.PageIndex = pageIndex; 
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
