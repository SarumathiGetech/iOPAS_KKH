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

public partial class BatchOrder : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    smsclass sms = new smsclass();
    grid gri = new grid();
    string sessionuserid = "", location = "";
    int orderreno;    
   // public static long orderrefnum;
    DateTime scheduledate;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sessionuserid = Session["Userid"].ToString();
            location = Session["location"].ToString();
            if (location != "")
            {
                if (!IsPostBack)
                {
                    btnsearch.Attributes.Add("onclick", "itemsearch() ;return true;");
                    ddsgrid();
                    string scriptStr = "javascript:return popUpCalendar(this," + getClientID() + @", 'dd/mm/yyyy', '__doPostBack(\'" + getClientID() + @"\')')";
                    imgCalendar.Attributes.Add("onclick", scriptStr);
                    batchgrid.PageIndex = 0;
                    griddisplay();
                    DDSName();
                    addhour();
                    addminute();
                    Btnupdate.Visible = false;
                }
            }
            else if (location == "" || location == null)
            {
                //txtitemcode.ReadOnly = true;
                //btnsearch.Visible = false;
                pp.Visible = false;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Workstation is not authorised to do this operation');</script>", false);
                return;
            }
        }
        catch (NullReferenceException)
        {
            //Response.Redirect("opas.html?Session=End");
            Response.Redirect("iopas.html");
        }        
    }

    public string getClientID()
    {
        return txt_Date.ClientID;
    }
    public void ddsgrid()
    {
        // qry.queueddsname(mcgrid, 1);
    }
    protected void btnok_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Selected record is "+searchvalue.Text+"');</script>", false);
        codereader();
    }

    // * Batch Order Insert * \\
    public void orderinsert()
    {
        date();
       // Orderref();
        if (searchvalue.Text != "")
        {
            int Rtnval = sms.batchorderinsert(location,Convert.ToInt32(searchvalue.Text), Convert.ToInt32(txtqtyperbag.Text), Convert.ToInt32(txttotqty.Text), ddlddsname.SelectedItem.ToString(), scheduledate, (ddlhr.SelectedValue.ToString() + ":" + ddlmin.SelectedValue.ToString()), "Pending", " ",txtpharinstruction.Text.Trim(), sessionuserid = Session["Userid"].ToString());
            if (Rtnval == 1)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record created');</script>", false);
                griddisplay();
                clear();
                addhour();
                addminute();
                DDSName();
            }
            else if (Rtnval == 2)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Quantity Per Bag has exceeded set maximum quantity per bag');</script>", false);
            }
            else if (Rtnval == 3)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Scheduled date time must be equal to or greater than current date time');</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
            }
        }
    }


    // * Batch Order Update * \\

    public void batchorderupdate()
    {
        date();
        int Rtnval = sms.batchorderupdate((long)ViewState["orderrefnum"], location, Convert.ToInt32(txtqtyperbag.Text), Convert.ToInt32(txttotqty.Text), ddlddsname.SelectedItem.ToString(), scheduledate, (ddlhr.SelectedValue.ToString() + ":" + ddlmin.SelectedValue.ToString()), txtpharinstruction.Text.Trim(), sessionuserid = Session["Userid"].ToString());

       if (Rtnval == 1)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated');</script>", false);
           griddisplay();
           clear();
           addhour();
           addminute();
           DDSName();
       }
       else if (Rtnval == 2)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Quantity Per Bag has exceeded set maximum quantity per bag');</script>", false);
       }
       else if (Rtnval == 3)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Scheduled date time must be equal to or greater than current date time');</script>", false);
       }
       else if (Rtnval == 4)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Updating will be allowed if schedule time is at least 5 minutes before current time');</script>", false);
       }
       else if (Rtnval == 5)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Order already cancelled');</script>", false);
       }
    }


    public void Orderref()
    {
        string i = System.DateTime.Now.ToString("MM/dd/yyyy");        
        Int32 len = i.Length;
        Int32 n = i.IndexOf('/');
        string str = i.Substring(n + 1, 2);
        string str1 = i.Substring(0, 2);
        string str2 = i.Substring(6, 4);
        orderreno = Convert.ToInt32(str + str1 +str2+1);        
    }

    public void date()
    {
        string i = txt_Date.Text;
        Int32 len = i.Length;
        Int32 n = i.IndexOf('/');
        string str = i.Substring(n + 1, 2);
        string str1 = i.Substring(0, 2);
        string str2 = i.Substring(6, 4);
        scheduledate = Convert.ToDateTime(str + "/" + str1 + "/" + str2);                
    }
    //protected void btnsave_Click(object sender, EventArgs e)
    protected void btnsave_Click(object sender, ImageClickEventArgs e)
    {       
            orderinsert();       
             
    }

    protected void Btnupdate_Click(object sender, ImageClickEventArgs e)
    {
        batchorderupdate();
    }

    public void griddisplay()
    {
        sms.batchordergrid(batchgrid,location);
        DataSet dsData = batchgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(batchgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(batchgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
   
    // * Itemcode,Name Drugcode Reader * \\
    public void codereader()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select i.item_code,i.item_name,i.drug_code,pm.PackType,iu.Pack_Size,IL.UOM  from Item_Master as i left join Item_user_Master as iu on iu.MasterID=i.MasterID left join Item_Location as IL on IL.MasterID=i.MasterID left join Packtype_Master as pm on pm.ID=iu.PacktypeID where iu.ID='" + Convert.ToInt32(searchvalue.Text) + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtitemcode.Text = dr[0].ToString();
                        txtitemname.Text = dr[1].ToString();
                        txtdrugcode.Text = dr[2].ToString();
                        txtpacktype.Text = dr[3].ToString();
                        txtpacksize.Text = dr[4].ToString();
                        txtuom.Text = dr[5].ToString();
                    }
                }
            }
        }
    }
    //protected void btnclear_Click(object sender, EventArgs e)
    protected void btnclear_Click(object sender, ImageClickEventArgs e)
    {
     clear();
     addhour();
     addminute();
     griddisplay();
     DDSName();
    }

    // * TextBox Clear Function * \\
    public void clear()
    {
        txtitemcode.Text = "";
        txtdrugcode.Text = "";
        txtitemname.Text = "";
        txtpacktype.Text = "";
        txtpacksize.Text = "";
        txtuom.Text = "";
        txtqtyperbag.Text = "";
        txttotqty.Text = "";
        txt_Date.Text = "";
        txtpharinstruction.Text = "";
        txtitemcode.ReadOnly = false;
        txtdrugcode.ReadOnly = false;
        txtitemname.ReadOnly = false;
        txtpacksize.ReadOnly = false;
        txtpacktype.ReadOnly = false;
        txtuom.ReadOnly = false;
        btnsearch.Visible = true;
        btnsave.Visible = true;
        Btnupdate.Visible = false;
            //orderrefnum = 0;
    }

    protected void batchgrid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        DDSName();
        addhour();
        addminute();
        ViewState["orderrefnum"] = Convert.ToInt64((batchgrid.Rows[e.NewSelectedIndex].Cells[1].Text));
        //orderrefnum = Convert.ToInt64((batchgrid.Rows[e.NewSelectedIndex].Cells[1].Text));
        txtqtyperbag.Text = (batchgrid.Rows[e.NewSelectedIndex].Cells[6].Text);
        txttotqty.Text = (batchgrid.Rows[e.NewSelectedIndex].Cells[7].Text);        
        //Txtpacktime.Text = (batchgrid.Rows[e.NewSelectedIndex].Cells[8].Text); 
        ddlhr.Items.Remove (batchgrid.Rows[e.NewSelectedIndex].Cells[9].Text.Substring(0,2));
        ddlhr.Items.Insert(0,new ListItem(batchgrid.Rows[e.NewSelectedIndex].Cells[9].Text.Substring(0,2)));
        ddlmin.Items.Remove(batchgrid.Rows[e.NewSelectedIndex].Cells[9].Text.Substring(3, 2));
        ddlmin.Items.Insert(0, new ListItem(batchgrid.Rows[e.NewSelectedIndex].Cells[9].Text.Substring(3, 2)));
        ddlddsname.Items.Remove(batchgrid.Rows[e.NewSelectedIndex].Cells[2].Text);
        ddlddsname.Items.Insert(0, new ListItem (batchgrid.Rows[e.NewSelectedIndex].Cells[2].Text));
        editcancel();
        searchvalue.Text = "";

        txtitemcode.ReadOnly = true;
        txtdrugcode.ReadOnly = true;
        txtitemname.ReadOnly = true;
        txtpacksize.ReadOnly = true;
        txtpacktype.ReadOnly = true;
        txtuom.ReadOnly = true;
        btnsearch.Visible = false;
        Btnupdate.Visible = true;
        btnsave.Visible = false;

    }

    // * DDS Name Reader * \\
    public void DDSName()
    {
        ddlddsname.Items.Clear();
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select d.DDS_Name from dds as d left join Pharmacy as p on p.PharmacyID=d.PharmacyID where p.Location_Name='" + location + "' and d.Status='Active' and d.DDS_Name not like 'B%' ";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddlddsname.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }

    
    // * Edit Cancel Item code,Item Name,Packtype ,Size,Uom Reader * \\
    public void editcancel()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select i.item_code,i.item_name,i.drug_code,pm.PackType,iu.Pack_Size,IL.UOM ,convert (varchar,b.Schedule_Date,103),b.Pharmacy_Instruction from Item_Master as i left join Item_user_Master as iu on iu.MasterID=i.MasterID left join Item_Location as IL on IL.MasterID=i.MasterID left join Batch_Order as b on b.IUM_ID=iu.ID left join Packtype_Master as pm on pm.ID=iu.PacktypeID where b.OrderRef_No='" + (long)ViewState["orderrefnum"] + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtitemcode.Text = dr[0].ToString();
                        txtitemname.Text = dr[1].ToString();
                        txtdrugcode.Text = dr[2].ToString();
                        txtpacktype.Text = dr[3].ToString();
                        txtpacksize.Text = dr[4].ToString();
                        txtuom.Text = dr[5].ToString();
                        txt_Date.Text = dr[6].ToString();
                        txtpharinstruction.Text = dr[7].ToString();
                    }
                }
            }
        }
    }
    //protected void btncancel_Click(object sender, EventArgs e)
    protected void btncancel_Click(object sender, ImageClickEventArgs e)
    {
        int Rtnval = sms.ordercancel((long)ViewState["orderrefnum"], location, sessionuserid = Session["Userid"].ToString());
        if (Rtnval == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Order cancelled');</script>", false);
        }
        else if(Rtnval==3)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Order Work in progress');</script>", false);
        }
        else if (Rtnval == 4)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Preallocation work in progress,Try again');</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occurred');</script>", false);
        }
        clear();
        addhour();
        addminute();
        griddisplay();
        DDSName();
    }
    protected void batchgrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        griddisplay();    
        batchgrid.PageIndex = e.NewPageIndex;
        batchgrid.DataBind();
        DataSet dsData = batchgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(batchgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(batchgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
    }

    public void addhour()
    {
        ddlhr.Items.Clear();

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select Hour from Hour";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddlhr.Items.Add(dr[0].ToString());
                    }
                }
            }
        }

    }

    public void addminute()
    {
        ddlmin.Items.Clear();


        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select Minute from Hour where Minute<>''";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddlmin.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }
    protected void batchgrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        griddisplay(); 
        DataSet dsData = batchgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = batchgrid.PageIndex;
        batchgrid.DataSource = SortDataTable(dtData, false);
        batchgrid.DataBind();
        batchgrid.PageIndex = pageIndex;    

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