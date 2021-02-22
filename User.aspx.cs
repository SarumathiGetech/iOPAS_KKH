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
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Datalayer;
using System.Drawing;
public partial class User : System.Web.UI.Page 
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    SqlQuery qry = new SqlQuery();
    grid gri = new grid();  
    DateTime dat1;
    string activesta="",dat2 = "", sessionuserid = "",userdmn="";  
   // public static string pwd = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Userid"] != null)
        {
            sessionuserid = Session["Userid"].ToString();
        }
        else
        {
            //Response.Redirect("opas.html?Session=End");
            Response.Redirect("iopas.html");
        }

        txtname.Attributes["onkeypress"] = "return Intcheck(event);";

        txtid.Attributes.Add("onKeyPress", "doClick(event)");
        txtname.Attributes.Add("onKeyPress", "doClick(event)");
        txtempno.Attributes.Add("onKeyPress", "doClick(event)");
        if (!IsPostBack)
        {
            usereditgrid.PageIndex = 0;
            pharlocread();
            usergridall();
            domainreader();
            rolereader();
            pageloadvisible();
            lblpasserror.Visible = false;
            btnreset.Visible = false;
            txtexpdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        }

        string scriptStr = "javascript:return popUpCalendar(this," + getClientID() + @", 'dd/mm/yyyy', '__doPostBack(\'" + getClientID() + @"\')')";
        imgCalendar.Attributes.Add("onclick",scriptStr);
        string scriptStrtwo = "javascript:return popUpCalendar(this," + getClientIDTo() + @", 'dd/mm/yyyy', '__doPostBack(\'" + getClientIDTo() + @"\')')";
        imgtwo.Attributes.Add("onclick", scriptStrtwo);
        btnprint.Attributes.Add("onclick", "CallPrint() ;return false;");
        txtid.Focus();
    }   
    
    public string getClientID()
    {
        return txtexpdate.ClientID;
    }
    public string getClientIDTo()
    {
        return txtdateto.ClientID;
    }

    protected void btnsubmit_Click(object sender, ImageClickEventArgs e)
    {
        usereditgrid.PageIndex = 0;
        
            if (ddldomain.SelectedItem.Value == "Local" && txtpass.Text == "")
            {
                lblpasserror.Visible = true;
               
            }
            else if (ddldomain.SelectedItem.Value == "Local" && txtpass.Text != "")
            {
                lblpasserror.Visible = false;
                userinsert();
            }
            else if (ddldomain.SelectedItem.Value != "Local")
            {
                lblpasserror.Visible = false;
                userinsert();
            }
            else
            {
                userinsert();
                insertclear();
            }
      
       
    }

    protected void btnupdate_Click(object sender, ImageClickEventArgs e)
    {
        userupdate();
        btnsubmit.Visible = true;
        btnupdate.Visible = false;
    }

    // * user Insert Function * \\

    public void userinsert()
    {
        activestatus();
        dateone();
        Userdomainnamereader();
        if (txtdateto.Text != "")
        {
            datetwo();
        }
        int lggdin = 0;
        if (userdmn.ToLower() == "Local".ToLower())
        {
            lggdin = 2;
        }
        else
        {
            lggdin = 1;
        }
        string pass = "";
        if (userdmn.ToLower() == "Local".ToLower() && txtpass.Text != "")
        {
            pass = txtpass.Text;
        }
        else if (userdmn.ToLower() == "Local".ToLower() && txtpass.Text == "")
        {
            pass = ViewState["pwd"].ToString();
        }
       
        int Rtnvalue = qry.userinsert(txtid.Text.ToLower().Trim(), txtempno.Text.Trim(), txtname.Text.Trim(), pass, ddldomain.SelectedItem.Value, ddlphaecloc.SelectedItem.Value, sessionuserid = Session["Userid"].ToString(), sessionuserid = Session["Userid"].ToString(), lggdin, txtremark.Text.Trim(), dat1, dat2, activesta);
        if (Rtnvalue == 1)
        {
            userroleinsert();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Record Created ');</script>", false);
            //usereditread();
            usergridall();
        }
        else if (Rtnvalue == 2)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('UserId already exist ');</script>", false);
        }
        else if (Rtnvalue == 3)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Effective Date From must be current date or greater ');</script>", false);
        }
        else if (Rtnvalue == 4)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Effective Date To must be greater than Effective Date From');</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Error Occurred ');</script>", false);
        }
    }

    // * user Update Function * \\
    public void userupdate()
    {
        activestatus();
        dateone();
        if (txtdateto.Text != "")
        {
            datetwo();
        }       
        int Rtnvalue = qry.userupdate(txtid.Text.Trim(), txtempno.Text.Trim(), txtname.Text.Trim(), ddldomain.SelectedItem.Value, ddlphaecloc.SelectedItem.Value, sessionuserid = Session["Userid"].ToString(), txtremark.Text.Trim(), dat1, dat2, activesta);
        if (Rtnvalue == 1)
        {
            Userdomainnamereader();
            if (userdmn.ToLower() == "Local".ToLower() && txtpass.Text != "")
            {
                qry.pwdchange(txtid.Text.ToLower().Trim(), txtpass.Text, 2);
            }
            else if (userdmn.ToLower() == "Local".ToLower() && txtpass.Text == "" && ViewState["pwd"].ToString() != "")
            {
                qry.pwdchange(txtid.Text.ToLower().Trim(), ViewState["pwd"].ToString(), 2);
            }
            qry.userroleupdate(txtid.Text);
            userroleinsert();
            qry.Audroleupdate(txtid.Text);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' User updated ');</script>", false);
            //usereditread();
            usergridall();
            pageloadvisible();
            pharlocread();
            domainreader();
            rolereader();
            insertclear();
            txtexpdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        }
        else if (Rtnvalue == 2)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Effective Date To must be greater than Effective Date From');</script>", false);
        }
        else if (Rtnvalue == 3)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Effective Date From must be current date or greater');</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occurred ');</script>", false);
        }
    }

    // * User Role Insert Function * \\
    public void userroleinsert()
    {
        foreach (ListItem itemins in lstadd.Items)
        {
            if (itemins != null)
            {
                qry.userroleinsert(itemins.ToString(), txtid.Text.Trim());
            }        
        }
    }

    // * User Role Reader For New User Adding  * \\
    public void rolereader()
    {     
        lstrole.Items.Clear();     

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select Role_Name from role where Status='Active'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lstrole.Items.Add(dr[0].ToString());            
                    }

                }
            }
        }
    }

// * User edit role reader * \\
    public void rolereaderuseredit()
    {
        lstadd.Items.Clear();    
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select r.Role_Name  from Role as r left join UserRole as u on u.RoleId =r.RoleID left join User_tbl as s on s.UserId=u.UserId where s.UserId='"+txtid.Text.Trim() +"'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lstadd.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }

    // * Pharmacy Location Reader Function * \\
    public void pharlocread()
    {
        ddlphaecloc.Items.Clear();
        ddlphaecloc.Items.Add("-Select-");
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
                        ddlphaecloc.Items.Add(dr[0].ToString());           
                    }

                }
            }
        }
    }

    // * Domain Reaader * \\
    public void domainreader()
    {     
        ddldomain.Items.Clear();
        ddldomain.Items.Add("-Select-"); 
       
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select DomainNme from Domain where status ='Active'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddldomain.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }

    // * Remarks Reader For Editing Purpose * \\
    public void remarksreader()
    {  

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select remarks from User_tbl where userid ='" + txtid.Text.Trim() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtremark.Text = dr[0].ToString();
                    }

                }
            }
        }
    }


    // * User domain name reader * \\

    // * Remarks Reader For Editing Purpose * \\
    public void Userdomainnamereader()
    {

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select DomainName from Domain where DomainNme='" + ddldomain.SelectedItem.Value + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        userdmn = dr[0].ToString();
                    }
                }
            }
        }
    }

    // * Insert Text Box Clearing * \\    
    public void insertclear()
    {
        txtid.Text = "";
        txtname.Text = "";        
        txtpass.Text = "";        
        txtremark.Text = "";
        txtempno.Text = "";
        txtexpdate.Text = "";
        txtdateto.Text = "";
        lstadd.Items.Clear();
        txtpass.ReadOnly = false;
        btnreset.Visible = false;
    }
    public void usereditread()
    {
        gri.useredituserid(usereditgrid, txtid.Text.Trim());
        DataSet dsData = usereditgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(usereditgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(usereditgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";        
    }
    protected void btnactive_Click(object sender, EventArgs e)
    {       
        //panelgrid.Visible = true;              
        //usereditread();        
    }
    protected void usereditgrid_Sorting(object sender, GridViewSortEventArgs e)
    {
       // usereditread();
        //gri.usereditname(usereditgrid, txtname.Text);
        usergridall();
        DataSet dsData = usereditgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = usereditgrid.PageIndex;
        usereditgrid.DataSource = SortDataTable(dtData, false);
        usereditgrid.DataBind();
        usereditgrid.PageIndex = pageIndex;

        ////DataSet dsData = usereditgrid.DataSource as DataSet;
        ////DataTable dtData = dsData.Tables[0];
        ////DataView dataView = new DataView(dtData);
        ////dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);
        ////usereditgrid.DataSource = dataView;
        ////usereditgrid.DataBind();
                    
    }

    ////private string ConvertSortDirectionToSql(SortDirection sortDirection)
    ////{
    ////    string newSortDirection = String.Empty;

    ////    switch (sortDirection)
    ////    {
    ////        case SortDirection.Ascending:
    ////            newSortDirection = "ASC";
    ////            break;

    ////        case SortDirection.Descending:
    ////            newSortDirection = "DESC";
    ////            break;
    ////    }

    ////    return newSortDirection;
    ////}

    protected void usereditgrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       // usereditread();
        
        //gri.usereditname(usereditgrid, txtname.Text);
        usergridall();
        DataSet dsData = usereditgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        usereditgrid.DataSource = SortDataTable(dtData, true);
        usereditgrid.PageIndex = e.NewPageIndex;
        usereditgrid.DataBind();     
        lblpge.Text = "Page" + "  " + Convert.ToString(usereditgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(usereditgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        
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
   
    protected void usereditgrid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {        
        txtid.Text  = (usereditgrid.Rows[e.NewSelectedIndex].Cells[2].Text);
        txtempno.Text = (usereditgrid.Rows[e.NewSelectedIndex].Cells[3].Text);
        if (txtempno.Text == "&nbsp;")
        {
            txtempno.Text = "";
        }
        txtname.Text = (usereditgrid.Rows[e.NewSelectedIndex].Cells[4].Text);            
        remarksreader();
        editvisible();
        rolereaderuseredit();      
        pharlocread();        
        domainreader();
        ddlphaecloc.Items.Remove("-Select-");
        ddldomain.Items.Remove("-Select-");
        ddldomain.Items.Remove((usereditgrid.Rows[e.NewSelectedIndex].Cells[5].Text));
        ddlphaecloc.Items.Remove((usereditgrid.Rows[e.NewSelectedIndex].Cells[1].Text));
        ddldomain.Items.Insert(0, new ListItem(usereditgrid.Rows[e.NewSelectedIndex].Cells[5].Text));
        ddlphaecloc.Items.Insert(0, new ListItem(usereditgrid.Rows[e.NewSelectedIndex].Cells[1].Text));
        txtexpdate.Text = (usereditgrid.Rows[e.NewSelectedIndex].Cells[7].Text);
        txtdateto.Text = (usereditgrid.Rows[e.NewSelectedIndex].Cells[8].Text);
        if (txtdateto.Text == "&nbsp;")
        {
            txtdateto.Text = "";
        }
        if ((usereditgrid.Rows[e.NewSelectedIndex].Cells[6].Text) == "YES")
        {
            chkactive.Checked = true;
        }
        else if ((usereditgrid.Rows[e.NewSelectedIndex].Cells[6].Text) == "NO")
        {
            chkactive.Checked = false;
        }
        rolereader();
        btnreset.Visible = true ;
        txtpass.ReadOnly = true;
        btnautopass.Visible = false;
        txtpass.Text = "";

        ViewState["pwd"] = "";
    }  

    // * Page Load visible True * \\
    public void pageloadvisible()
    {
        btnsubmit.Visible = true;
        btnupdate.Visible = false;        
        txtid.ReadOnly = false;        
        txtid.BackColor = System.Drawing.Color.White;
        btnautopass.Visible = true;
        ////passwordpanel.Visible = true;
    }
    // * Edit Mode Visible * \\
    public void editvisible()
    {
        btnsubmit.Visible = false;
        btnupdate.Visible = true;    
        txtid.ReadOnly = true;        
        txtid.BackColor = System.Drawing.Color.Silver;
        ////passwordpanel.Visible = false;
    }  
  
    // * User grid Display All * \\
    public void usergridall()
    {
        gri.useregridall(usereditgrid);
        DataSet dsData = usereditgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(usereditgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(usereditgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";        
    }
   
    //protected void Button1_Click(object sender, EventArgs e)
    protected void btnsearch_Click(object sender, ImageClickEventArgs e)
    {
        usereditgrid.PageIndex = 0;
        if (txtid.Text != "")
        {
            gri.useredituserid(usereditgrid, txtid.Text.ToLower());
        }
        else if (txtempno.Text != "")
        {
            gri.usereditempno(usereditgrid, txtempno.Text);
        }
        else if(txtname.Text!="")
        {
            gri.usereditname(usereditgrid, txtname.Text);
        }
        if (txtid.Text.Trim() != "" || txtempno.Text.Trim() != "" || txtname.Text.Trim() != "")
        {
            DataSet dsData = usereditgrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            int a = dtData.Rows.Count;
            int b = usereditgrid.PageCount;
            int c = usereditgrid.PageIndex + 1;
            int D = usereditgrid.Rows.Count;
            lblpge.Text = "Page" + "  " + Convert.ToString(usereditgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(usereditgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.Font.Bold = false;
            lblpge.ForeColor = Color.Black;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Font.Bold = true;
                lblpge.ForeColor =Color.Green;
                lblpge.Text = "No Record Found";
            }
        }
        else if (txtid.Text.Trim() == "" || txtempno.Text.Trim() == "" || txtname.Text.Trim() == "")
        {
            usergridall();
            //lblpge.Font.Bold = true;
            //lblpge.ForeColor = Color.Green;
            //lblpge.Text = "No Record Found";
        }        
    }
    protected void ddlphaecloc_SelectedIndexChanged(object sender, EventArgs e)
    {           
         
    }   
    protected void ddldomain_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void btnreset_Click(object sender, ImageClickEventArgs e)
    {
        txtpass.ReadOnly = false;
        btnautopass.Visible = true;
    }
    protected void btnclear_Click(object sender, ImageClickEventArgs e)
    {
        txtprintpass.Text = "";
        pageloadvisible();
        insertclear();
        pharlocread();
        domainreader();
        rolereader();
        ViewState["pwd"] = "";
        //gri.useredituserid(usereditgrid, txtid.Text.ToLower());
        lblpge.Text = "";
        usergridall();        
        btnautopass.Visible = true;
        txtexpdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        lblpasserror.Visible = false;
    }    
    protected void btnmove_Click(object sender, EventArgs e)
    {       
        int a = 0;
        ListItem item = lstrole.SelectedItem;
        if (item != null)
        {
            foreach (ListItem itm in lstadd.Items)
            {
                if (item.ToString() == itm.ToString())
                {
                    a = 1;
                    lstrole.ClearSelection();
                    return;
                }
            }

            if (a == 0)
            {
                ListItem item1 = lstrole.SelectedItem;
                if (item != null)
                {
                    lstrole.ClearSelection();
                    lstadd.Items.Add(item1);
                }
            }
        }        
    }
    protected void btnremove_Click(object sender, EventArgs e)
    {
        ListItem itemrmv = lstadd.SelectedItem;
        if (itemrmv != null)
        {
            lstadd.ClearSelection();
            lstadd.Items.Remove(itemrmv);
        }
    }
    protected void btnprint_Click(object sender, ImageClickEventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' password is "+pwd+"');</script>", false);        
    }    

    public void dateone()
    {
        string i = "";
        i = txtexpdate.Text;
        Int32 len = i.Length;
        Int32 n = i.IndexOf('/');
        string str = i.Substring(n + 1, 2);
        string str1 = i.Substring(0, 2);
        string str2 = i.Substring(6, 4);
        string dat = str + "/" + str1 + "/" + str2;
        dat1  = Convert.ToDateTime(dat);
    }
    public void datetwo()
    {
        string i = "";
        i = txtdateto.Text;
        Int32 len = i.Length;
        Int32 n = i.IndexOf('/');
        string str = i.Substring(n + 1, 2);
        string str1 = i.Substring(0, 2);
        string str2 = i.Substring(6, 4);
        dat2 = str + "/" + str1 + "/" + str2;
        //dat2 = Convert.ToDateTime(dat);
    }

    public void activestatus()
    {
        if (chkactive.Checked == true)
        {
            activesta = "YES";
        }
        else
        {
            activesta = "NO";
        }
    }

    public string Passwordcreate()
    {
        string id, pwd;
        System.Guid guid = System.Guid.NewGuid();
        id = guid.ToString();
        id.Replace("-", string.Empty);
        pwd ="iopas"+ id.Substring(0, 7);
        return pwd;
    }
    protected void btnautopass_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["pwd"] = Passwordcreate();
        lblpasserror.Visible = false;
        txtprintpass.Text = ViewState["pwd"].ToString();
        txtpass.Text = ViewState["pwd"].ToString();
    }
   
}
