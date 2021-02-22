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
using System.Diagnostics;
using Datalayer;
using System.Drawing;
public partial class Role : System.Web.UI.Page
{
    // * Connection and SqlQuery Class Calling * \\
    // ----------------------------------------------\\
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    SqlQuery qry = new SqlQuery();
    grid gri = new grid();
    // ----------------------------------------------\\
    // * VARIABLE DECLARATION * \\
    //public static string menuname="",autocheckroleid = "", btnval = "";
    //public static int roleid;      
    string checkval="", val = "",sessionuserid="";  
    int a = 0, b = 0, c = 0;   
    // ----------------------------------------------\\
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                sessionuserid = Session["UserId"].ToString();
                rolereader();
                ViewState["menuname"] = "Loading";
               // menuname = "Loading";
                getroleid();
                gridgrouping.PageIndex = 0;
                roledisplay();
                txtroleadd.Visible = false;               
                btnclear.Visible = false;
                btndel.Visible = false;               
                btnacces.Visible = true;
                btngroupadd.Visible = false;
                btnupdate.Visible = false;
            }
            catch (NullReferenceException)
            {
                //Response.Redirect("opas.html?Session=End");
                Response.Redirect("iopas.html");
            }            
        }
        gridpanel.Visible = false;
    }

    //protected void btnadd_Click(object sender, EventArgs e)
    protected void btnadd_Click(object sender, ImageClickEventArgs e)
    {
        roleclear();
        btngroupadd.Visible = true;
        btngroupadd.Visible = true;
        btnupdate.Visible = false;
        gridshowone();  
        panelmenu.Visible = false;
        gridpanel.Visible = true;              
        ddlgroupname.Visible = false;
        txtroleadd.Visible = true;
        btndel.Visible = true;
        btnclear.Visible = true;
        btnacces.Visible = false;
        Label1.Text = "Role Creation";
        btnadd.Visible = false;          
    }
    //protected void btndel_Click(object sender, EventArgs e)
    protected void btndel_Click(object sender, ImageClickEventArgs e)
    {
       // menuname = "Loading";
        ViewState["menuname"] = "Loading";
        getroleid();       
        roledisplay();
        rolereader();
        descreader();
        btngroupadd.Visible = false;        
        panelmenu.Visible = true;
        gridpanel.Visible = false;
        ddlgroupname.Visible = true;
        txtroleadd.Visible = false;
        btndel.Visible = false;
        btnclear.Visible = false;
        btnacces.Visible = true;
        Label1.Text = "Role Administration";
        btnadd.Visible = true;        
        btnupdate.Visible = false;
    }
   //protected void btngroupadd_Click1(object sender, EventArgs e)
    protected void btngroupadd_Click1(object sender, ImageClickEventArgs e)
    {     
            roleinsert();
            roleclear();
            gridshowone();
            rolereader();
            gridpanel.Visible = true;
      
    }

    protected void btnupdate_Click(object sender, ImageClickEventArgs e)
    {
        roleupdate();
        gridshowone();
        rolereader();
        roleclear();
        gridpanel.Visible = true;
        btngroupadd.Visible = true;
        btnupdate.Visible = false;
    }
      
    // * New Role Insert Function * \\
    public void roleinsert()
   {
       int Rtnval = qry.roleinsert(txtroleadd.Text.Trim(), txtdesc.Text.Trim(), sessionuserid = Session["UserId"].ToString(), sessionuserid = Session["UserId"].ToString());
       if (Rtnval == 1)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record Created');</script>", false);
       }
       else if (Rtnval == 2)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Role Name already exist');</script>", false);
       }
       else
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occurred');</script>", false);
       }

   }
    //--------------------------------------- \\

    //* Role textbox Clear Function * \\
    public void roleclear()
    {
        txtroleadd.Text = "";
        txtdesc.Text = "";
    }
    //--------------------------------------- \\  
   

    // * User Role Reader Function * \\
    public void rolereader()
    {
        ddlgroupname.Items.Clear();    

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select Role_Name,Description from role where Status='Active'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddlgroupname.Items.Add(dr[0].ToString());
                        descreader();
                    }

                }
            }
        }
    }
    // * ----------------------------------------------------- * \\\
  
    // * MenuName Display Function * \\
    public void roledisplay()
    {
        gri.roleaccess(gridgroup,  ViewState["menuname"].ToString());
    }

    // * ----------------------------------------------------- * \\\
    // * Menu Name Button Click Evenets * \\
    //protected void btnloading_Click(object sender, EventArgs e)
    protected void btnloading_Click(object sender, ImageClickEventArgs e)    
    {
        getroleid();        
        ViewState["menuname"] = "";
        ViewState["menuname"] = "Loading";
        roledisplay();
    }
    protected void btnassembly_Click(object sender, ImageClickEventArgs e)
    {
        getroleid();       
        ViewState["menuname"] = "";
        ViewState["menuname"] = "Assembly";
        roledisplay();
    }
    protected void btnquepriority_Click(object sender, ImageClickEventArgs e)
    {
        getroleid();        
        ViewState["menuname"] = "";
        ViewState["menuname"] = "Queue Priority";
        roledisplay();
    }
    protected void btnlblprinting_Click(object sender, ImageClickEventArgs e)
    {
        getroleid();       
        ViewState["menuname"] = "";
        ViewState["menuname"] = "Label";
        roledisplay();
    }
    protected void btnenquery_Click(object sender, ImageClickEventArgs e)
    {
        getroleid();       
        ViewState["menuname"] = "";
        ViewState["menuname"] = "Enquiry";
        roledisplay();
    }
    protected void btnmanual_Click(object sender, ImageClickEventArgs e)
    {
        getroleid();        
        ViewState["menuname"] = "";
        ViewState["menuname"] = "Manual Orders";
        roledisplay();
    }
    protected void btnsetting_Click(object sender, ImageClickEventArgs e)
    {
        getroleid();        
        ViewState["menuname"] = "";
        ViewState["menuname"] = "General";
        roledisplay();
    }
    protected void btnsystem_Click(object sender, ImageClickEventArgs e)
    {
        getroleid();        
        ViewState["menuname"] = "";
        ViewState["menuname"] = "System";
        roledisplay();
    }
    protected void btnsecurity_Click(object sender, ImageClickEventArgs e)
    {
        getroleid();       
        ViewState["menuname"] = "";
        ViewState["menuname"] = "Security";
        roledisplay();
    }
    protected void btnddsmon_Click(object sender, ImageClickEventArgs e)
    {
        getroleid();        
        ViewState["menuname"] = "";
        ViewState["menuname"] = "Monitoring";
        roledisplay();
    }     


    // Temp ------------------------------
    protected void btnreport_Click(object sender, ImageClickEventArgs e)
    {
        getroleid();        
        ViewState["menuname"] = "";
        ViewState["menuname"] = "Reports";
        roledisplay();
    }
    // ----------------------------------------------------------
        
    // * Menu Accessing Premission Function * \\
    public void roleuser()
    {
        for (int i = 0; i < gridgroup.Rows.Count; i++)
        {
            GridViewRow row = gridgroup.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("chk")).Checked;

            if (isChecked)
            {
                //menuid = (gridgroup.Rows[i].Cells[0].Text);  
                int menuid = (int)gridgroup.DataKeys[i].Value; 
                gri.rolecontrolinsert(ddlgroupname.SelectedValue.ToString(), Convert.ToInt32(menuid), sessionuserid = Session["UserId"].ToString());
            }            
        }
    }
    //protected void btnacces_Click(object sender, EventArgs e)
    protected void btnacces_Click(object sender, ImageClickEventArgs e)
    {
        gridgrouping.PageIndex = 0;
        menuaccessdelete();
        roleuser();        
    }  
   
    // * Display the existing Role Name in gridview for new Role Adding screen* \\
    public void gridshowone()
    {
        gri.displaygridfirst(gridgrouping);
        DataSet dsData = gridgrouping.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(gridgrouping.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridgrouping.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }


    // * Role Name Display First page Grid * \\

    ////public void gridshowonefirstpage()
    ////{
    ////    gri.displaygridfirst(fstgrid);
    ////    DataSet dsData = fstgrid.DataSource as DataSet;
    ////    DataTable dtData = dsData.Tables[0];
    ////    lblpge2.Text = "Page" + "  " + Convert.ToString(fstgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(fstgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
    ////    lblpge2.ForeColor = Color.Black;
    ////    lblpge2.Font.Bold = false;
    ////    if (dtData.Rows.Count == 0)
    ////    {
    ////        lblpge2.Text = "No Record Found";
    ////        lblpge2.Font.Bold = true;
    ////        lblpge2.ForeColor = Color.Green;
    ////    }
    ////}  
  


    protected void ddlpharadd_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridshowone();               
        gridpanel.Visible = true;
    }   
   
    public void roleupdate()
    {
        int Rtnvalue = qry.roleupdate(txtroleadd.Text.Trim(), txtdesc.Text.Trim(), sessionuserid = Session["UserId"].ToString(),(int) ViewState["roleid"]);
        if (Rtnvalue == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record Updated');</script>", false);
            gridshowone();
        }
        else if (Rtnvalue == 2)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Role Name already exist ');</script>", false);
            gridpanel.Visible = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Error Occured ');</script>", false);
            gridpanel.Visible = true;
        }
    }
    protected void gridgrouping_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //roleid = 0;
        ViewState["roleid"] = "";
        roleclear();    
        txtroleadd.Text=(gridgrouping.Rows[e.NewSelectedIndex].Cells[3].Text);
       // roleid = (int)gridgrouping.DataKeys[e.NewSelectedIndex].Value;
        ViewState["roleid"] = (int)gridgrouping.DataKeys[e.NewSelectedIndex].Value;  
        roledescription();
        btngroupadd.Visible = false;
        btnupdate.Visible = true;
        gridpanel.Visible = true;                 
    }
    // * Role Description Reader for Edit Mode * \\
    public void roledescription()
    {

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select Description from role where Role_Name ='" + txtroleadd.Text.Trim() + "'";
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
    public void editclear()
    {       
    }
    protected void ddlgroupname_SelectedIndexChanged(object sender, EventArgs e)
    {     
        descreader();
        getroleid();
        roledisplay();   
    }
 // * Discrepation reader for menu access mode * \\
    public void descreader()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select Description from role where Role_Name ='" + ddlgroupname.SelectedValue.ToString() + "'";
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
    
    // * Bool value menu Name Auto Checked * \\
    public bool chk(object MenuID)
    {
        val = "";
        val = MenuID.ToString();
        autocheck();
        if (val == checkval)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
// * Role Name Auto checked Function * \\
    public void autocheck()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select r.menuid from RoleControl as r left join MenuItems as m on m. menuid= r.MenuId where m.Parent_Menu='" + ViewState["menuname"].ToString() + "' and r.RoleId='" + ViewState["autocheckroleid"].ToString() + "' and r.MenuId='" + Convert.ToInt32(val) + "' ";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        checkval = dr[0].ToString();
                    }
                }
            }
        }
    }

    // * Role ID Reader baserd on role name *\\
    public void getroleid()
    {
        //autocheckroleid = "";
        ViewState["autocheckroleid"] = "";

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select roleid from Role where role_name='" + ddlgroupname.SelectedValue.ToString() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ViewState["autocheckroleid"] = dr[0].ToString();
                    }
                }
            }
        }
    }

    // * Menu Access Delete Function * \\
    public void menuaccessdelete()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select MenuID  from  MenuItems where Parent_Menu='" + ViewState["menuname"].ToString() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string accessmenuid = dr[0].ToString();
                        gri.menuaccessdeletetwo(Convert.ToInt32(accessmenuid), Convert.ToInt32(ViewState["autocheckroleid"].ToString()), ViewState["menuname"].ToString());
                    }
                }
            }
        }

    }

    // * CARTRIDGE ACTIVE INACTIVE STATUS FUNCTIONS * \\
    public void activeinactive()
    {

        for (int i = 0; i < gridgrouping.Rows.Count; i++)
        {
            GridViewRow row = gridgrouping.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("chkgrpinc")).Checked;
            if (isChecked)
            {
                c++;
                string ddsno = (gridgrouping.Rows[i].Cells[4].Text);
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

    //protected void btndelgrp_Click(object sender, EventArgs e)
    protected void btndelgrp_Click(object sender, ImageClickEventArgs e)
    {
        gridpanel.Visible = true;

        //btnval = "";
        //btnval = "1";
        ViewState["btnval"] = "";
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
            gridshowone();
        }        
    }
    //protected void Btndeactive_Click(object sender, EventArgs e)
    protected void Btndeactive_Click(object sender, ImageClickEventArgs e)
    {
        gridpanel.Visible = true;
        //btnval = "";
        //btnval = "2";
        ViewState["btnval"] = "";
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
            gridshowone();
        }    

    }
    protected void btnok_Click(object sender, EventArgs e)
    {
        if (ViewState["btnval"].ToString() == "1")
        {
            for (int i = 0; i < gridgrouping.Rows.Count; i++)
            {
                GridViewRow row = gridgrouping.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkgrpinc")).Checked;

                if (isChecked)
                {
                   // string rolename = (gridgrouping.Rows[i].Cells[2].Text);
                    string rolename = (string)gridgrouping.DataKeys[i].Value.ToString();
                    gri.roleactive(rolename, sessionuserid = Session["UserId"].ToString());
                }
            }
            gridshowone();
            gridpanel.Visible = true;
        }

        else if (ViewState["btnval"].ToString() == "2")
        {
            for (int i = 0; i < gridgrouping.Rows.Count; i++)
            {
                GridViewRow row = gridgrouping.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkgrpinc")).Checked;

                if (isChecked)
                {
                   //string rolename = (gridgrouping.Rows[i].Cells[2].Text);
                    string rolename = (string)gridgrouping.DataKeys[i].Value.ToString();
                    gri.roledeactive(rolename, sessionuserid = Session["UserId"].ToString());
                }
            }
            gridshowone();
            gridpanel.Visible = true;
        }
    }

    //protected void btnclear_Click(object sender, EventArgs e)
    protected void btnclear_Click(object sender, ImageClickEventArgs e)
    {
        roleclear();
        gridpanel.Visible = true;
        btngroupadd.Visible = true;
        btnupdate.Visible = false;
        gridshowone();
    }

    //protected void fstgrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gridshowonefirstpage();
    //    fstgrid.PageIndex = e.NewPageIndex;
    //    fstgrid.DataBind();
    //    DataSet dsData = fstgrid.DataSource as DataSet;
    //    DataTable dtData = dsData.Tables[0];
    //    lblpge2.Text = "Page" + "  " + Convert.ToString(fstgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(fstgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
    //    lblpge2.ForeColor = Color.Black;
    //    lblpge2.Font.Bold = false;
    //    if (dtData.Rows.Count == 0)
    //    {
    //        lblpge2.Text = "No Record Found";
    //        lblpge2.Font.Bold = true;
    //        lblpge2.ForeColor = Color.Green;
    //    }
    //}
    protected void gridgrouping_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridpanel.Visible = true;
        gridshowone();
        gridgrouping.PageIndex = e.NewPageIndex;
        gridgrouping.DataBind();        
        DataSet dsData = gridgrouping.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(gridgrouping.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridgrouping.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
    protected void gridgrouping_Sorting(object sender, GridViewSortEventArgs e)
    {
        gridpanel.Visible = true;
        gridshowone();
        DataSet dsData = gridgrouping.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = gridgrouping.PageIndex;
        gridgrouping.DataSource = SortDataTable(dtData, false);
        gridgrouping.DataBind();
        gridgrouping.PageIndex = pageIndex; 
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
