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
public partial class smssetup : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    smsclass sms = new smsclass();
    Drug drg = new Drug();
    string sessionuserid = "", Allotchk = "", pharname = "";   
    int a = 0, b = 0, c = 0;
    //static string btnval, Allotid = "", statustype = "";
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
            ViewState["Allotid"] = "";
            ViewState["statustype"] = "";
            btnmsgsave.Visible = true;
            btnupdate.Visible = false;
            griddetails.PageIndex = 0;
            SMStypereader();
            if (ddlmsgtyp.SelectedValue == "iOPAS Processing Error" || ddlmsgtyp.SelectedValue == "iOPAS DDS/BDS Communication Error")
            {
                lblinque.Visible = false;
                txtinqueue.Visible = false;
                lblelapsed.Visible = false;
                txtlastprocess.Visible = false;
                griddisplay();                
            }
            locationdisplay();
            griddisplay();
            ViewState["statustype"] = "";
            
        }
        btnsearch.Attributes.Add("onclick", "smscontact() ;return false;");
    }

    protected void btnclk_Click(object sender, EventArgs e)
    {
        btnmsgsave.Visible = true;
        btnupdate.Visible = false;
        txtmobnum.Text = searchvalue.Text;
        griddisplay();
    }
    //protected void btnmsgsave_Click(object sender, EventArgs e)
    protected void btnmsgsave_Click(object sender, ImageClickEventArgs e)
    {
        griddetails.PageIndex = 0;
        if (ddlmsgtyp.SelectedItem.ToString() != "iOPAS Processing Error")
        {
            int a = 0;
            for (int i = 0; i < gridpharloc.Rows.Count; i++)
            {
                GridViewRow row = gridpharloc.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkphar")).Checked;
                if (isChecked)
                {
                    a++;
                }
            }
            if (a != 0)
            {
                smsinsert();
                griddisplay();
            }
            if (a == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Select pharmacy location');</script>", false);
            }
        }
        else if (ddlmsgtyp.SelectedItem.ToString() == "iOPAS Processing Error")
        {
            smsinsert();
            griddisplay();
        }             
    }
    //protected void btnupdate_Click(object sender, EventArgs e)
    protected void btnupdate_Click(object sender, ImageClickEventArgs e)
    {
        griddetails.PageIndex = 0;
        if (ddlmsgtyp.SelectedItem.ToString() == ViewState["statustype"].ToString())
        {
            if (ddlmsgtyp.SelectedItem.ToString() != "iOPAS Processing Error")
            {
                int a = 0;
                for (int i = 0; i < gridpharloc.Rows.Count; i++)
                {
                    GridViewRow row = gridpharloc.Rows[i];
                    bool isChecked = ((CheckBox)row.FindControl("chkphar")).Checked;
                    if (isChecked)
                    {

                        a++;
                    }
                }
                if (a != 0)
                {
                    smsupdate();
                    griddisplay();
                    locationdisplay();
                }
                if (a == 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Select pharmacy location');</script>", false);
                }
            }
            else if (ddlmsgtyp.SelectedItem.ToString() == "iOPAS Processing Error")
            {
                smsupdate();
                griddisplay();
                locationdisplay();
            }
        }
        else if (ddlmsgtyp.SelectedItem.ToString() != ViewState["statustype"].ToString())
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Update only will take existing messgae type');</script>", false);
        }
       
    }   
    protected void ddlmsgtyp_SelectedIndexChanged(object sender, EventArgs e)
    {
        SMStypereader();
        if (ddlmsgtyp.SelectedItem.ToString() == "iOPAS Processing Error" || ddlmsgtyp.SelectedValue == "iOPAS DDS/BDS Communication Error")
        {
            lblinque.Visible = false;
            txtinqueue.Visible = false;
            lblelapsed.Visible = false;
            txtlastprocess.Visible = false;
        }
        else if (ddlmsgtyp.SelectedItem.ToString() == "In Queue Alert")
        {
            lblinque.Visible = true;
            txtinqueue.Visible = true;
            lblelapsed.Visible = true;
            txtlastprocess.Visible = true;
        }       
    }
    public void smsinsert()
    {
        int Rtnval = sms.SMSalertinsert(txtmobnum.Text.Trim(), ddlmsgtyp.SelectedItem.ToString(), Convert.ToInt32(txtalertmin.Text.Trim()), Convert.ToInt32(txtalertmin2.Text.Trim()), "Active", sessionuserid = Session["Userid"].ToString());
       if (Rtnval == 1)
       {

           if (ddlmsgtyp.SelectedItem.ToString() != "iOPAS Processing Error")
           {
              
               for (int i = 0; i < gridpharloc.Rows.Count; i++)
               {
                   GridViewRow row = gridpharloc.Rows[i];
                   bool isChecked = ((CheckBox)row.FindControl("chkphar")).Checked;
                   if (isChecked)
                   {
                       sms.SMSalertlocationinsert(txtmobnum.Text.Trim(), ddlmsgtyp.SelectedItem.ToString(), (gridpharloc.Rows[i].Cells[0].Text));
                   }
               }
               ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record created');</script>", false);
               clear();
               locationdisplay();
           }
           else if (ddlmsgtyp.SelectedItem.ToString() == "iOPAS Processing Error")
           {
               ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record created');</script>", false);
               clear();
               locationdisplay();
           }
       }
       else if (Rtnval == 2)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('SMS alert already exist for this user');</script>", false);
       }
       else
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
       }
    }
    public void smsupdate()
    {
        int Rtnval = sms.SMSalertupdate(Convert.ToInt32(ViewState["Allotid"].ToString()), ddlmsgtyp.SelectedItem.Value, txtmobnum.Text.Trim(), Convert.ToInt32(txtalertmin.Text.Trim()), Convert.ToInt32(txtalertmin2.Text.Trim()), sessionuserid = Session["Userid"].ToString());
        if (Rtnval == 1)
        {
            if (ddlmsgtyp.SelectedItem.ToString() != "iOPAS Processing Error")
            {
                sms.SMSalertlocationupdate(txtmobnum.Text.Trim(), ddlmsgtyp.SelectedItem.ToString());
                for (int i = 0; i < gridpharloc.Rows.Count; i++)
                {
                    GridViewRow row = gridpharloc.Rows[i];
                    bool isChecked = ((CheckBox)row.FindControl("chkphar")).Checked;
                    if (isChecked)
                    {
                        sms.SMSalertlocationinsert(txtmobnum.Text.Trim(), ddlmsgtyp.SelectedItem.ToString(), (gridpharloc.Rows[i].Cells[0].Text));
                    }
                }
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated');</script>", false);
                clear();
                locationdisplay();
            }
            else if (ddlmsgtyp.SelectedItem.ToString() == "iOPAS Processing Error")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated');</script>", false);
                clear();
                locationdisplay();
            }            
        }
        else if (Rtnval == 2)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('SMS alert already exist for this user');</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
        }
    }
    // * Pharmacy location Name Display * \\
    public void locationdisplay()
    {
        drg.pharmlocation(gridpharloc);
    }

    // * SMS MSG Type Reader * \\
    public void SMStypereader()
    {

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select Inqueue_Not_Processed,Elasped_Minutes from SMS_Type where Message_Type='" + ddlmsgtyp.SelectedItem.ToString() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtinqueue.Text = dr[0].ToString();
                        txtlastprocess.Text = dr[1].ToString();
                    }
                }
            }
        }
    }   

    // Grid Display function *\\
    public void griddisplay()
    {
        sms.smsalertgrid(griddetails);
        DataSet dsData = griddetails.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(griddetails.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(griddetails.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }


    // * CARTRIDGE ACTIVE INACTIVE STATUS FUNCTIONS * \\
    public void activeinactive()
    {

        for (int i = 0; i < griddetails.Rows.Count; i++)
        {

            GridViewRow row = griddetails.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("chkrow")).Checked;

            if (isChecked)
            {
                c++;
                string stat = (griddetails.Rows[i].Cells[8].Text);
                string act = "Active";
                string inc = "Inactive";
                if (act == stat)
                {
                    a++;
                }
                else if (stat == inc)
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
        ViewState["btnval"]="";
        ViewState["btnval"] = "1";
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
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Records are not selected');</script>", false);
        }
        else
        {
            griddisplay();
        }
    }
    //protected void btndeactive_Click(object sender, EventArgs e)
    protected void btndeactive_Click(object sender, ImageClickEventArgs e)
    {
        activeinactive();
        ViewState["btnval"] = "";
        ViewState["btnval"] = "2";

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
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Records are not selected');</script>", false);
        }
        else
        {
            griddisplay();
        }

    }
    protected void btnok_Click(object sender, EventArgs e)
    {
        if (ViewState["btnval"].ToString() == "1")
        {
            for (int i = 0; i < griddetails.Rows.Count; i++)
            {
                GridViewRow row = griddetails.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkrow")).Checked;

                if (isChecked)
                {
                    sms.smsactivate((int)griddetails.DataKeys[i].Value, sessionuserid = Session["Userid"].ToString());
                }
            }
            griddisplay();
        }

        else if (ViewState["btnval"].ToString() == "2")
        {
            for (int i = 0; i < griddetails.Rows.Count; i++)
            {
                GridViewRow row = griddetails.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkrow")).Checked;

                if (isChecked)
                {
                    sms.smsinactivate((int)griddetails.DataKeys[i].Value, sessionuserid = Session["Userid"].ToString());
                }
            }
            griddisplay();
        }
    }
    protected void griddetails_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
       // statustype = "";
        ViewState["statustype"] = "";
        ViewState["Allotid"] = "";
        ddlmsgtyp.Items.Clear();
        ddlmsgtyp.Items.Add("iOPAS Processing Error");
        ddlmsgtyp.Items.Add("In Queue Alert");
        ddlmsgtyp.Items.Add("iOPAS DDS/BDS Communication Error");
        ViewState["Allotid"] = (string)griddetails.DataKeys[e.NewSelectedIndex].Value.ToString();
        //Allotid = (string)griddetails.DataKeys[e.NewSelectedIndex].Value.ToString();
        txtcontactname.Text = (griddetails.Rows[e.NewSelectedIndex].Cells[3].Text);
        txtmobnum.Text = (griddetails.Rows[e.NewSelectedIndex].Cells[4].Text);
        ddlmsgtyp.Items.Remove(griddetails.Rows[e.NewSelectedIndex].Cells[5].Text);
        ddlmsgtyp.Items.Insert(0, new ListItem(griddetails.Rows[e.NewSelectedIndex].Cells[5].Text));
        ViewState["statustype"] = griddetails.Rows[e.NewSelectedIndex].Cells[5].Text;
        txtalertmin.Text = (griddetails.Rows[e.NewSelectedIndex].Cells[6].Text);
        txtalertmin2.Text = (griddetails.Rows[e.NewSelectedIndex].Cells[7].Text);
        btnmsgsave.Visible = false;
        btnupdate.Visible = true;
        SMStypereader();
        if (ddlmsgtyp.SelectedValue == "iOPAS Processing Error" || ddlmsgtyp.SelectedValue == "iOPAS DDS/BDS Communication Error")
        {
            lblinque.Visible = false;
            txtinqueue.Visible = false;
            lblelapsed.Visible = false;
            txtlastprocess.Visible = false;           
        }
        else if (ddlmsgtyp.SelectedValue == "In Queue Alert")
        {
            lblinque.Visible = true;
            txtinqueue.Visible = true;
            lblelapsed.Visible = true;
            txtlastprocess.Visible = true;
        }
        locationdisplay();
    }

    public bool chkphar(object phname)
    {      
        string val = "";       
        val = phname.ToString();
        SMSlocationreader(val);       
        if (val == pharname)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // * SMS MSG Type Reader * \\
    public void SMSlocationreader(string vals)
    {  
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select p.Location_Name from Pharmacy as p left join SMS_Pharmacy_Location as sp on sp.PharmacyID=p.PharmacyID left join SMS_Alert as a on a.AlertID=sp.AlertID where a.AlertID='" + ViewState["Allotid"].ToString() + "' and p.Location_Name='" + vals + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        pharname = dr[0].ToString();
                    }
                }
            }
        }
    }

    protected void btnclear_Click(object sender, ImageClickEventArgs e)
    {       
        clear();
    }

    // * Textbox Clear Function * \\
    public void clear()
    {
        txtcontactname.Text = "";
        txtmobnum.Text = "";
        txtalertmin.Text = "";
        txtalertmin2.Text = "";
        btnmsgsave.Visible = true;
        btnupdate.Visible = false;
        ViewState["Allotid"] = "";
        ddlmsgtyp.Items.Clear();
        ddlmsgtyp.Items.Add("iOPAS Processing Error");
        ddlmsgtyp.Items.Add("In Queue Alert");
        ddlmsgtyp.Items.Add("iOPAS DDS/BDS Communication Error");
        ddlmsgtyp.Items.Remove("In Queue Alert");
        ddlmsgtyp.Items.Insert(0, new ListItem("In Queue Alert"));       
        SMStypereader();
        lblinque.Visible = true;
        txtinqueue.Visible = true;
        lblelapsed.Visible = true;
        txtlastprocess.Visible = true;
        locationdisplay();
        ViewState["statustype"] = "";
    }

    // * SMS Alert Allot Checking Function * \\
    public void allotcheck()
    {

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select COUNT(*) from SMS_Alert as a left join SMS_Type as t on t.SMS_TypeID=a.SMS_TypeID left join SMS_User as u on u.SMS_UserID=a.SMS_UserID where u.MobileNo='" + txtmobnum.Text.Trim() + "' and t.Message_Type='" + ddlmsgtyp.SelectedItem.Value + "' and a.Alert_Minutes='" + Convert.ToInt32(txtalertmin.Text.Trim()) + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Allotchk = dr[0].ToString();
                    }
                }
            }
        }
    }
    //protected void btnsearch_Click(object sender, EventArgs e)
    protected void btnsearch_Click(object sender, ImageClickEventArgs e)
    {
        btnmsgsave.Visible = true;
        btnupdate.Visible = false;
    }
    protected void griddetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        griddisplay();
        griddetails.PageIndex = e.NewPageIndex;
        griddetails.DataBind();
        DataSet dsData = griddetails.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(griddetails.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(griddetails.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
    protected void griddetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        griddisplay();
        DataSet dsData = griddetails.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = griddetails.PageIndex;
        griddetails.DataSource = SortDataTable(dtData, false);
        griddetails.DataBind();
        griddetails.PageIndex = pageIndex; 
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
