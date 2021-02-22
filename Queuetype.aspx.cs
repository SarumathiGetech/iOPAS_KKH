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

public partial class Queuetype : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    SqlQuery qry = new SqlQuery();    
  //  public static int editqueueid = 0;   
    string val="",ddsval = "",sessionuserid = "";
    int  a, b, c, d = 0, f = 0,g = 0;
    //static string btnval = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["editqueueid"] = "0";
            locationreader();
            Gridsecond.PageIndex = 0;
            queuegrid();
            ddsgrid();
            btnsave.Visible = true;
            btnupdate.Visible = false;
        }
        try
        {
            sessionuserid = Session["Userid"].ToString();
        }
        catch (NullReferenceException)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Session time out');</script>", false);
            //Response.Redirect("opas.html?Session=End");
            Response.Redirect("iopas.html");
        }       
    }   
   
    // * Pharmacy Location Reader * \\
    public void locationreader()
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
                        ddlpharmloca.Items.Add(dr[0].ToString());
                    }

                }
            }
        }
    }
    // *Queue Insert Function * \\

    public void queueinsert()
    {
       int Rtnval= qry.queueinsert(ddlpharmloca.SelectedItem.ToString(), txtquetype.Text.Trim(), txtfrom.Text.Trim(), txtto.Text.Trim(), txtdesc.Text.Trim(), sessionuserid = Session["Userid"].ToString());
       if (Rtnval == 1)
       {        
           allocationinsert();
           queuegrid();
           clear();                     
       }
       else if (Rtnval == 2)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Queue Series already exist');</script>", false);
       }
    }

    // * Queue Update Function * \\
    public void queueupdate()
    {
        int Rtnval = qry.queueupdate(ddlpharmloca.SelectedItem.ToString(), txtquetype.Text.Trim(), txtfrom.Text.Trim(), txtto.Text.Trim(), txtdesc.Text.Trim(), sessionuserid = Session["Userid"].ToString(),(int) ViewState["editqueueid"]);
        if (Rtnval == 1)
        {
            allocationremove();
            allocationinsert();
            queuegrid();
            clear();
            ViewState["editqueueid"] = "0";
            ddsgrid();
        }
        else if (Rtnval == 2)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Queue Series Already Exist');</script>", false);
        }
    }   

    // * textBox clear Function * \\
    public void clear()
    {
        txtquetype.Text = "";
        txtfrom.Text = "";
        txtto.Text = "";
        txtdesc.Text = "";
        btnsave.Visible = true;
        btnupdate.Visible = false;
    }   
    // * Queue Type Grid Display * \\
    public void queuegrid()
    {
        //locationidreader();      
        qry.queueenablegrid(Gridsecond, ddlpharmloca.SelectedItem.ToString());
        DataSet dsData = Gridsecond.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(Gridsecond.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Gridsecond.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
// * DDS No Display grid for allocation * \\
    public void ddsgrid()
    {
        qry.queueddsname(mcgrid, ddlpharmloca.SelectedItem.ToString());
    }
    protected void ddlpharmloca_SelectedIndexChanged(object sender, EventArgs e)
    {
        ////locationidreader();
        queuegrid();
        ddsgrid();
        clear();
    }
    ////public void allocationupdate()
    ////{
    ////    for (int i = 0; i < mcgrid.Rows.Count; i++)
    ////    {
    ////        GridViewRow row = mcgrid.Rows[i];
    ////        bool isChecked = ((CheckBox)row.FindControl("chkrow")).Checked;

    ////        if (isChecked)
    ////        {
    ////            string DDSno = (mcgrid.Rows[i].Cells[0].Text);               
    ////           // qry.Queueallocationinsert(pharmacyid, DDSno, editqueueid); 
    ////            qry.Queueallocationinsert(ddlpharmloca.SelectedItem.Value, DDSno, txtquetype.Text);
    ////        }
    ////    }

    ////}

    // * Allocation insert Function * \\
    public void allocationinsert()
    {
        for (int i = 0; i < mcgrid.Rows.Count; i++)
        {
            GridViewRow row = mcgrid.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("chkrow")).Checked;

            if (isChecked)
            {
                string DDSno = (mcgrid.Rows[i].Cells[0].Text);
                //qry.Queueallocationinsert(pharmacyid, DDSno, queueid);
                qry.Queueallocationinsert(ddlpharmloca.SelectedItem.Value, DDSno, txtquetype.Text.Trim());
            }
        }
    }     
    
    //protected void Button4_Click(object sender, EventArgs e)
    protected void Button4_Click(object sender, ImageClickEventArgs e)
    {
        clear();
        ViewState["editqueueid"]="0";
        ddsgrid();
    }
    //protected void btnsave_Click(object sender, EventArgs e)
    protected void btnsave_Click(object sender, ImageClickEventArgs e)
    {
       
            queuefromchecking();
            if (a == 0)
            {
                Gridsecond.PageIndex = 0;
                queueinsert();
            }
      
    }

     protected void btnupdate_Click(object sender, ImageClickEventArgs e)
    {
        queueupdatechecking();
        if ((int)ViewState["editqueueid"] == b & c == 1)
        {
            Gridsecond.PageIndex = 0;
            queueupdate();
        }
        else if (b == 0)
        {
            queueupdate();
        }
        else if (b != 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid Queue Number');</script>", false);
        }
    }


    // * Allocation Remove Function * \\
    public void allocationremove()
    {
        //for (int i = 0; i < mcgrid.Rows.Count; i++)
        //{
        //    GridViewRow row = mcgrid.Rows[i];
        //    bool isChecked = ((CheckBox)row.FindControl("chkrow")).Checked;

        //    if (isChecked)
        //    {
                //string ddsno = (mcgrid.Rows[i].Cells[0].Text);
        qry.queallocationremove(ddlpharmloca.SelectedValue.ToString(), txtquetype.Text.Trim());
        //    }
        //}
    }
   
    // *Queue Description Reader * \\
    public void descriptionreader()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select description from Queue_Master where Queueid='" + (int)ViewState["editqueueid"] + "' and Status='Active'";
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


    protected void Gridsecond_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (Gridsecond.Rows[e.NewSelectedIndex].Cells[8].Text == "Active")
        {


            ViewState["editqueueid"] = "";
            ViewState["editqueueid"] = (int)Gridsecond.DataKeys[e.NewSelectedIndex].Value;
            //editqueueid = (int)Gridsecond.DataKeys[e.NewSelectedIndex].Value;  
            txtquetype.Text = (Gridsecond.Rows[e.NewSelectedIndex].Cells[4].Text);
            txtfrom.Text = (Gridsecond.Rows[e.NewSelectedIndex].Cells[5].Text);
            txtto.Text = (Gridsecond.Rows[e.NewSelectedIndex].Cells[6].Text);
            btnsave.Visible = false;
            btnupdate.Visible = true;
            descriptionreader();
            ddsgrid();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Selected record is already Inactive');</script>", false);
        }
    }

    protected void chkheader_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in Gridsecond.Rows)
        {
            bool isChecked = ((CheckBox)(Gridsecond.HeaderRow.FindControl("chkheader"))).Checked;

            if (isChecked)
            {
                CheckBox cb = ((CheckBox)(gvr.FindControl("chkque")));
                cb.Checked = true;
            }
            else if (isChecked == false)
            {

                CheckBox cb = ((CheckBox)(gvr.FindControl("chkque")));
                cb.Checked = false;
            }
        }
    }

    protected void chkheaderdds_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in mcgrid.Rows)
        {
            bool isChecked = ((CheckBox)(mcgrid.HeaderRow.FindControl("chkheaderdds"))).Checked;

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

    public bool chkrow(object DDS_No)
    {
        val = "";
        val = DDS_No.ToString();
        ////locationidreader();
        allocationautocheck();
        if (val == ddsval)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // * Allocation Auto Check Reader * \\
    public void allocationautocheck()
    {   
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select qa.DDS_Name from Queue_Allocation as qa left join Queue_Master as qm on qm.QueueID=qa.Queueid left join Pharmacy as p on p.PharmacyID=qa.Pharmacyid and qa.Pharmacyid=qm.PharmacyId  where qm.Queue_type='" + txtquetype.Text.Trim() + "' and p.Location_Name='" + ddlpharmloca.SelectedValue.ToString() + "' and  qa.DDS_Name='" + val + "' and qm.Status='Active'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddsval = dr[0].ToString();
                    }

                }
            }
        }
    }

    // * Queue Number already Exist Or not Checking Function * \\
    public void queuefromchecking()
    {
     
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select q.Queue_From,q.Queue_To,q.QueueID from Queue_Master as q left join Pharmacy as p on p.PharmacyID=q.PharmacyId where p.Location_Name='" + ddlpharmloca.SelectedValue.ToString() + "' and q.Status='Active'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (Convert.ToInt32(txtfrom.Text.Trim()) < Convert.ToInt32(dr[0].ToString()) & Convert.ToInt32(txtto.Text.Trim()) < Convert.ToInt32(dr[0].ToString()))
                        {
                        }
                        else
                        {
                            if (Convert.ToInt32(txtfrom.Text.Trim()) > Convert.ToInt32(dr[1].ToString()) & Convert.ToInt32(txtto.Text.Trim()) > Convert.ToInt32(dr[1].ToString()))
                            {

                            }

                            else
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Queue Series already exist');</script>", false);
                                a = 1;
                                return;
                            }
                        }
                    }

                }
            }
        }


    }

    // * Queue Number already Exist Or not Checking Function * \\
    public void queueupdatechecking()
    {
     

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select q.Queue_From,q.Queue_To,q.QueueID from Queue_Master as q left join Pharmacy as p on p.PharmacyID=q.PharmacyId where p.Location_Name='" + ddlpharmloca.SelectedValue.ToString() + "' and q.Status='Active'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    c = 0;

                    while (dr.Read())
                    {
                        if (Convert.ToInt32(txtfrom.Text.Trim()) < Convert.ToInt32(dr[0].ToString()) & Convert.ToInt32(txtto.Text.Trim()) < Convert.ToInt32(dr[0].ToString()))
                        {
                        }
                        else
                        {
                            if (Convert.ToInt32(txtfrom.Text.Trim()) > Convert.ToInt32(dr[1].ToString()) & Convert.ToInt32(txtto.Text.Trim()) > Convert.ToInt32(dr[1].ToString()))
                            {
                            }
                            else
                            {
                                b = Convert.ToInt32(dr[2].ToString());
                                c++;
                            }
                        }
                    }

                }
            }
        }


    }

    // * CARTRIDGE ACTIVE INACTIVE STATUS FUNCTIONS * \\
    public void activeinactive()
    {
        for (int i = 0; i < Gridsecond.Rows.Count; i++)
        {
            GridViewRow row = Gridsecond.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("chkque")).Checked;

            if (isChecked)
            {
               g++;
                string ddsno = (Gridsecond.Rows[i].Cells[8].Text);
                string act = "Active";
                string inc = "Inactive";
                if (act == ddsno)
                {
                    d++;
                }
                else if (ddsno == inc)
                {
                   f++;
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
        if (d != 0 & f == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Selected records are already Active.');</script>", false);
        }
      
        else if(d!=0  & f!= 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Selected records are of different status.');</script>", false);
        }
        else if (g != 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "popup", "confirmProcess()", true);
        }
        else if (g == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Records are not selected.');</script>", false);
        }
        else
        {
            queuegrid();
        }
        
    }
    //protected void btninactive_Click(object sender, EventArgs e)
    protected void btninactive_Click(object sender, ImageClickEventArgs e)
    {
        //btnval = "";
        //btnval = "2";
        ViewState["btnval"] = "2";
        activeinactive();
        if (f != 0 & d== 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Selected records are already Inactive.');</script>", false);
        }
        else if (d != 0 & f != 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Selected records are of different status.');</script>", false);
        }
        else if (g != 0)
        {

            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "popup", "confirmProcess1()", true);
        }
        else if (g == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Records are not selected.');</script>", false);
        }
        else
        {
            queuegrid();
        }
        
    }

    protected void btnok_Click(object sender, EventArgs e)
    {
        if (ViewState["btnval"].ToString() == "1")
        {
            for (int i = 0; i < Gridsecond.Rows.Count; i++)
            {
                GridViewRow row = Gridsecond.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkque")).Checked;

                if (isChecked)
                {
                    //int queueid = Convert.ToInt32((Gridsecond.Rows[i].Cells[2].Text));
                    int queueid = (int)Gridsecond.DataKeys[i].Value;
                    qry.queuetypeenabled(queueid, sessionuserid);
                }
            }
            queuegrid();
        }

        else if (ViewState["btnval"].ToString() == "2")
        {
           

            for (int i = 0; i < Gridsecond.Rows.Count; i++)
            {
                GridViewRow row = Gridsecond.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkque")).Checked;

                if (isChecked)
                {
                    qry.queallocationremove(ddlpharmloca.SelectedValue.ToString(), Gridsecond.Rows[i].Cells[4].Text);
                    
                    int queueid = (int)Gridsecond.DataKeys[i].Value;
                    qry.queuetypedisabled(queueid, sessionuserid);
                }
            }
            queuegrid();
        }

    }
    protected void Gridsecond_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        queuegrid();
        Gridsecond.PageIndex = e.NewPageIndex;
        Gridsecond.DataBind();
        DataSet dsData = Gridsecond.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(Gridsecond.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Gridsecond.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
    protected void Gridsecond_Sorting(object sender, GridViewSortEventArgs e)
    {
        queuegrid();
        DataSet dsData = Gridsecond.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = Gridsecond.PageIndex;
        Gridsecond.DataSource = SortDataTable(dtData, false);
        Gridsecond.DataBind();
        Gridsecond.PageIndex = pageIndex; 
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
