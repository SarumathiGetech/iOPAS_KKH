using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Datalayer;
using System.Drawing;
using System.Data;

public partial class Pharmacytime : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    string sessionuserid = "";
    grid gri = new grid();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Userid"] != null)
        {
            sessionuserid = Session["Userid"].ToString();
            
            if (!IsPostBack)
            {
                locationreader();
                holidaysdisplay();
                gridphartime.PageIndex = 0;
                griddisplay();
                //ddlitemsaddhour();
                //ddlitemsaddminute();
                //editreader(ddlpharloc.SelectedValue.ToString());
            }
        }
        else
        {
            //Response.Redirect("opas.html?Session=End");
            Response.Redirect("iopas.html");
        }
        string scriptStr = "javascript:return popUpCalendar(this," + getClientID() + @", 'dd/mm/yyyy', '__doPostBack(\'" + getClientID() + @"\')')";
        imgCalendar.Attributes.Add("onclick", scriptStr); 
    }
    public string getClientID()
    {
        return txt_Date.ClientID;
    }  
    //protected void btnaddholi_Click(object sender, EventArgs e)
    protected void btnaddholi_Click(object sender, ImageClickEventArgs e)
    {
        //if (txt_Date.Text != "")
        //{
        //    string i = txt_Date.Text;
        //    Int32 len = i.Length;
        //    Int32 n = i.IndexOf('/');
        //    string str = i.Substring(n + 1, 2);
        //    string str1 = i.Substring(0, 2);
        //    string str2 = i.Substring(6, 4);
        //    string dat = str + "/" + str1 + "/" + str2;

        //    if (Convert.ToDateTime(dat) >= Convert.ToDateTime(System.DateTime.Now.ToString("MM/dd/yyyy")))
        //    {
        //        SqlDataReader dr = gri.holidaysdatecheck(ddlpharloc.SelectedValue.ToString(), dat);
        //        if (dr.HasRows)
        //        {
        //            dr.Read();
        //            if (dr[0].ToString() == dat)
        //            {
        //                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Holiday date already exist ');</script>", false);
        //            }
        //            else
        //            {
        //                gri.holidaysinsert(ddlpharloc.SelectedValue.ToString(), Convert.ToDateTime(dat), sessionuserid);
        //                lstbox.Items.Add(txt_Date.Text);
        //                txt_Date.Text = "";
        //            }
        //        }
        //        if (dr.HasRows == false)
        //        {
        //            gri.holidaysinsert(ddlpharloc.SelectedValue.ToString(), Convert.ToDateTime(dat), sessionuserid);
        //            //lstbox.Items.Add(txt_Date.Text);           
        //            txt_Date.Text = "";
        //            holidaysdisplay();
        //        }
        //        dr.Close();
        //    }
        //    else if (Convert.ToDateTime(dat) < Convert.ToDateTime(System.DateTime.Now.ToString("MM/dd/yyyy")))
        //    {
        //        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Holiday date is greater than days of current days');</script>", false);
        //    }
        //}
    }
    //protected void btnrmv_Click(object sender, EventArgs e)
    protected void btnrmv_Click(object sender, ImageClickEventArgs e)
    {
        ListItem itemrmv = lstbox.SelectedItem;
        if (itemrmv != null)
        {
            string i = itemrmv.ToString();
            Int32 len = i.Length;
            Int32 n = i.IndexOf('/');
            string str = i.Substring(n + 1, 2);
            string str1 = i.Substring(0, 2);
            string str2 = i.Substring(6, 4);
            string dat = str + "/" + str1 + "/" + str2;
            gri.holidaysremove(ddlpharloc.SelectedValue.ToString(),Convert.ToDateTime(dat),sessionuserid);
            lstbox.ClearSelection();
            lstbox.Items.Remove(itemrmv);
        }
    }
    //protected void btnsave_Click(object sender, EventArgs e)
    protected void btnsave_Click(object sender, ImageClickEventArgs e)
    {
        //SqlDataReader dr = gri.pharmacytimecount(ddlpharloc.SelectedItem.ToString());
        //if (dr.HasRows)
        //{
        //    dr.Read();
        //    if (dr[0].ToString() == "0")
        //    {
        //        int Rtnvalue = gri.pharmacytimeinsert(ddlpharloc.SelectedItem.ToString(), (ddlmhst.SelectedValue.ToString() + ":" + ddlmmst.SelectedValue.ToString()), ddlmhet.SelectedValue.ToString() + ":" + ddlmmet.SelectedValue.ToString(), ddltuhst.SelectedValue.ToString() + ":" + ddltumst.SelectedValue.ToString(), ddltuhet.SelectedValue.ToString() + ":" + ddltumet.SelectedValue.ToString(), ddlwhst.SelectedValue.ToString() + ":" + ddlwmst.SelectedValue.ToString(), ddlwhet.SelectedValue.ToString() + ":" + ddlwmet.SelectedValue.ToString(), ddlthst.SelectedValue.ToString() + ":" + ddlthmst.SelectedValue.ToString(), ddlthet.SelectedValue.ToString() + ":" + ddlthmet.SelectedValue.ToString(), ddlfhst.SelectedValue.ToString() + ":" + ddlfmst.SelectedValue.ToString(), ddlfhet.SelectedValue.ToString() + ":" + ddlfmet.SelectedValue.ToString(), ddlsahst.SelectedValue.ToString() + ":" + ddlsamst.SelectedValue.ToString(), ddlsahet.SelectedValue.ToString() + ":" + ddlsamet.SelectedValue.ToString(), ddlsuhst.SelectedValue.ToString() + ":" + ddlsumst.SelectedValue.ToString(), ddlsuhet.SelectedValue.ToString() + ":" + ddlsumet.SelectedValue.ToString(), sessionuserid = Session["Userid"].ToString());
        //        if (Rtnvalue == 1)
        //        {
        //            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record created ');</script>", false);
        //            txtclear();
        //            gridphartime.PageIndex = 0;
        //            griddisplay();
        //            ddlitemsaddhour();
        //            ddlitemsaddminute();
        //            editreader(ddlpharloc.SelectedValue.ToString());
        //        }
        //        else if (Rtnvalue == 3)
        //        {
        //            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Start time must be equal to or greater than end time ');</script>", false);
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occured ');</script>", false);
        //        }
        //    }
        //    else if (dr[0].ToString() != "0")
        //    {
        //        int Rtnvalue = gri.pharmacytimeupdate(ddlpharloc.SelectedItem.ToString(), (ddlmhst.SelectedValue.ToString() + ":" + ddlmmst.SelectedValue.ToString()), ddlmhet.SelectedValue.ToString() + ":" + ddlmmet.SelectedValue.ToString(), ddltuhst.SelectedValue.ToString() + ":" + ddltumst.SelectedValue.ToString(), ddltuhet.SelectedValue.ToString() + ":" + ddltumet.SelectedValue.ToString(), ddlwhst.SelectedValue.ToString() + ":" + ddlwmst.SelectedValue.ToString(), ddlwhet.SelectedValue.ToString() + ":" + ddlwmet.SelectedValue.ToString(), ddlthst.SelectedValue.ToString() + ":" + ddlthmst.SelectedValue.ToString(), ddlthet.SelectedValue.ToString() + ":" + ddlthmet.SelectedValue.ToString(), ddlfhst.SelectedValue.ToString() + ":" + ddlfmst.SelectedValue.ToString(), ddlfhet.SelectedValue.ToString() + ":" + ddlfmet.SelectedValue.ToString(), ddlsahst.SelectedValue.ToString() + ":" + ddlsamst.SelectedValue.ToString(), ddlsahet.SelectedValue.ToString() + ":" + ddlsamet.SelectedValue.ToString(), ddlsuhst.SelectedValue.ToString() + ":" + ddlsumst.SelectedValue.ToString(), ddlsuhet.SelectedValue.ToString() + ":" + ddlsumet.SelectedValue.ToString(), sessionuserid = Session["Userid"].ToString());
        //        if (Rtnvalue == 1)
        //        {
        //            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated ');</script>", false);
        //            txtclear();
        //            gridphartime.PageIndex = 0;
        //            griddisplay();
        //            ddlitemsaddhour();
        //            ddlitemsaddminute();
        //            editreader(ddlpharloc.SelectedValue.ToString());
        //        }
        //        else if (Rtnvalue == 3)
        //        {
        //            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Start time must be equal to or greater than end time');</script>", false);
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occured ');</script>", false);
        //        }
        //    }
        //}
        holidaysdisplay(); 
    }

    // * Location Name Reader * \\
    public void locationreader()
    {
        ddlpharloc.Items.Clear();

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
                        ddlpharloc.Items.Add(dr[0].ToString());
                    }
                }
            }
        }

    }

    // * Holidays Display Reader * \\
    public void holidaysdisplay()
    {
       // lstbox.Items.Clear();
       //SqlDataReader dr = gri.holidayreader(ddlpharloc.SelectedValue.ToString());
       //if(dr.HasRows)
       //{
       //    while(dr.Read())
       //    {
       //        lstbox.Items.Add(dr[0].ToString());
       //    }
       //    dr.Close();
       //}
    }

    // * Pharmacy Time Grid Display * \\
    public void griddisplay()
    {
        gri.pharmacygrid(gridphartime);
        DataSet dsData = gridphartime.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(gridphartime.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridphartime.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
    public void txtclear()
    {
        txt_Date.Text = "";
        txt_Date.Focus();
     //txtmonsttime.Text="";
     //txtmonendtime.Text="";
     //txttuesttime.Text="";
     //txttuendtime.Text="";
     //txtwedsttime.Text="";
     //txtwedendtime.Text="";
     //txtthursttime.Text="";
     //txtthurendtime.Text="";
     //txtfristtime.Text="";
     //txtfriendtime.Text="";
     //txtsatsttime.Text="";
     //txtsatendtime.Text="";
     //txtsunsttime.Text="";
     //txtsunendtime.Text="";
    }
    protected void gridphartime_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        locationreader();
        ddlpharloc.Items.Remove(gridphartime.Rows[e.NewSelectedIndex].Cells[1].Text);
        ddlpharloc.Items.Insert(0, new ListItem(gridphartime.Rows[e.NewSelectedIndex].Cells[1].Text));
       // editreader((gridphartime.Rows[e.NewSelectedIndex].Cells[1].Text));
        holidaysdisplay();        
    }
    // * Edit Reader * \\
    //public void editreader(string locaname)
    //{
    //    SqlDataReader dr = gri.phartimeedit(locaname);
    //    if (dr.HasRows)
    //    {
    //        while (dr.Read())
    //        {
    //            //ddldomain.Items.Insert(0, new ListItem(usereditgrid.Rows[e.NewSelectedIndex].Cells[5].Text));
    //           // int len = dr[0].ToString().Length;
    //            ddlitemsaddhour();
    //            ddlitemsaddminute();
    //            ddlmhst.Items.Remove(dr[0].ToString().Substring(0, 2));
    //            ddlmhst.Items.Insert(0,new ListItem(dr[0].ToString().Substring(0,2)));
    //            ddlmmst.Items.Remove(dr[0].ToString().Substring(3, 2));
    //            ddlmmst.Items.Insert(0, new ListItem(dr[0].ToString().Substring(3, 2)));

    //            ddlmhet.Items.Remove(dr[1].ToString().Substring(0, 2));
    //            ddlmhet.Items.Insert(0, new ListItem(dr[1].ToString().Substring(0, 2)));
    //            ddlmmet.Items.Remove(dr[1].ToString().Substring(3, 2));
    //            ddlmmet.Items.Insert(0, new ListItem(dr[1].ToString().Substring(3, 2)));

    //            ddltuhst.Items.Remove(dr[2].ToString().Substring(0, 2));
    //            ddltuhst.Items.Insert(0, new ListItem(dr[2].ToString().Substring(0, 2)));
    //            ddltumst.Items.Remove(dr[2].ToString().Substring(3, 2));
    //            ddltumst.Items.Insert(0, new ListItem(dr[2].ToString().Substring(3, 2)));

    //            ddltuhet.Items.Remove(dr[3].ToString().Substring(0, 2));
    //            ddltuhet.Items.Insert(0, new ListItem(dr[3].ToString().Substring(0, 2)));
    //            ddltumet.Items.Remove(dr[3].ToString().Substring(3, 2));
    //            ddltumet.Items.Insert(0, new ListItem(dr[3].ToString().Substring(3, 2)));

    //            ddlwhst.Items.Remove(dr[4].ToString().Substring(0, 2));
    //            ddlwhst.Items.Insert(0, new ListItem(dr[4].ToString().Substring(0, 2)));
    //            ddlwmst.Items.Remove(dr[4].ToString().Substring(3, 2));
    //            ddlwmst.Items.Insert(0, new ListItem(dr[4].ToString().Substring(3, 2)));

    //            ddlwhet.Items.Remove(dr[5].ToString().Substring(0, 2));
    //            ddlwhet.Items.Insert(0, new ListItem(dr[5].ToString().Substring(0, 2)));
    //            ddlwmet.Items.Remove(dr[5].ToString().Substring(3, 2));
    //            ddlwmet.Items.Insert(0, new ListItem(dr[5].ToString().Substring(3, 2)));

    //            ddlthst.Items.Remove(dr[6].ToString().Substring(0, 2));
    //            ddlthst.Items.Insert(0, new ListItem(dr[6].ToString().Substring(0, 2)));
    //            ddlthmst.Items.Remove(dr[6].ToString().Substring(3, 2));
    //            ddlthmst.Items.Insert(0, new ListItem(dr[6].ToString().Substring(3, 2)));

    //            ddlthet.Items.Remove(dr[7].ToString().Substring(0, 2));
    //            ddlthet.Items.Insert(0, new ListItem(dr[7].ToString().Substring(0, 2)));
    //            ddlthmet.Items.Remove(dr[7].ToString().Substring(3, 2));
    //            ddlthmet.Items.Insert(0, new ListItem(dr[7].ToString().Substring(3, 2)));

    //            ddlfhst.Items.Remove(dr[8].ToString().Substring(0, 2));
    //            ddlfhst.Items.Insert(0, new ListItem(dr[8].ToString().Substring(0, 2)));
    //            ddlfmst.Items.Remove(dr[8].ToString().Substring(3, 2));
    //            ddlfmst.Items.Insert(0, new ListItem(dr[8].ToString().Substring(3, 2)));

    //            ddlfhet.Items.Remove(dr[9].ToString().Substring(0, 2));
    //            ddlfhet.Items.Insert(0, new ListItem(dr[9].ToString().Substring(0, 2)));
    //            ddlfmet.Items.Remove(dr[9].ToString().Substring(3, 2));
    //            ddlfmet.Items.Insert(0, new ListItem(dr[9].ToString().Substring(3, 2)));

    //            ddlsahst.Items.Remove(dr[10].ToString().Substring(0, 2));
    //            ddlsahst.Items.Insert(0, new ListItem(dr[10].ToString().Substring(0, 2)));
    //            ddlsamst.Items.Remove(dr[10].ToString().Substring(3, 2));
    //            ddlsamst.Items.Insert(0, new ListItem(dr[10].ToString().Substring(3, 2)));

    //            ddlsahet.Items.Remove(dr[11].ToString().Substring(0, 2));
    //            ddlsahet.Items.Insert(0, new ListItem(dr[11].ToString().Substring(0, 2)));
    //            ddlsamet.Items.Remove(dr[11].ToString().Substring(3, 2));
    //            ddlsamet.Items.Insert(0, new ListItem(dr[11].ToString().Substring(3, 2)));

    //            ddlsuhst.Items.Remove(dr[12].ToString().Substring(0, 2));
    //            ddlsuhst.Items.Insert(0, new ListItem(dr[12].ToString().Substring(0, 2)));
    //            ddlsumst.Items.Remove(dr[12].ToString().Substring(0, 2));
    //            ddlsumst.Items.Insert(0, new ListItem(dr[12].ToString().Substring(3, 2)));

    //            ddlsuhet.Items.Remove(dr[13].ToString().Substring(0, 2));
    //            ddlsuhet.Items.Insert(0, new ListItem(dr[13].ToString().Substring(0, 2)));
    //            ddlsumet.Items.Remove(dr[13].ToString().Substring(0, 2));
    //            ddlsumet.Items.Insert(0, new ListItem(dr[13].ToString().Substring(3, 2)));
    //        }
    //        dr.Close();
    //    }
    //}

    //public void ddlitemsaddhour()
    //{
    //    hourclear();
    //    SqlDataReader dr = gri.pharmacyhour();
    //    if (dr.HasRows)
    //    {
    //        while (dr.Read())
    //        {
    //            ddlmhst.Items.Add(dr[0].ToString());
    //            ddlmhet.Items.Add(dr[0].ToString());
    //            ddltuhst.Items.Add(dr[0].ToString());
    //            ddltuhet.Items.Add(dr[0].ToString());
    //            ddlwhst.Items.Add(dr[0].ToString());
    //            ddlwhet.Items.Add(dr[0].ToString());
    //            ddlthst.Items.Add(dr[0].ToString());
    //            ddlthet.Items.Add(dr[0].ToString());
    //            ddlfhst.Items.Add(dr[0].ToString());
    //            ddlfhet.Items.Add(dr[0].ToString());
    //            ddlsahst.Items.Add(dr[0].ToString());
    //            ddlsahet.Items.Add(dr[0].ToString());
    //            ddlsuhst.Items.Add(dr[0].ToString());
    //            ddlsuhet.Items.Add(dr[0].ToString());               
    //        }
    //    }       
    //}

    //public void ddlitemsaddminute()
    //{
    //    minuteclear();
    //    SqlDataReader dr = gri.pharmacyMinute();
    //    if (dr.HasRows)
    //    {
    //        while (dr.Read())
    //        {                
    //            ddlmmst.Items.Add(dr[0].ToString());
    //            ddlmmet.Items.Add(dr[0].ToString());
    //            ddltumst.Items.Add(dr[0].ToString());
    //            ddltumet.Items.Add(dr[0].ToString());
    //            ddlwmst.Items.Add(dr[0].ToString());
    //            ddlwmet.Items.Add(dr[0].ToString());
    //            ddlthmst.Items.Add(dr[0].ToString());
    //            ddlthmet.Items.Add(dr[0].ToString());
    //            ddlfmst.Items.Add(dr[0].ToString());
    //            ddlfmet.Items.Add(dr[0].ToString());
    //            ddlsamst.Items.Add(dr[0].ToString());
    //            ddlsamet.Items.Add(dr[0].ToString());
    //            ddlsumst.Items.Add(dr[0].ToString());
    //            ddlsumet.Items.Add(dr[0].ToString().Trim());
    //        }
    //    }
    //}
    //protected void btnclear_Click(object sender, EventArgs e)
    protected void btnclear_Click(object sender, ImageClickEventArgs e)
    {
        txtclear();     
        //ddlitemsaddhour();
        //ddlitemsaddminute();
        locationreader();
       // editreader(ddlpharloc.SelectedValue.ToString());
        holidaysdisplay();   
    }
    protected void ddlpharloc_SelectedIndexChanged(object sender, EventArgs e)
    {
       // ddlitemsaddhour();
       // ddlitemsaddminute();
        holidaysdisplay();
       // editreader(ddlpharloc.SelectedValue.ToString());
    }
    protected void gridphartime_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        griddisplay();
        gridphartime.PageIndex = e.NewPageIndex;
        gridphartime.DataBind();
        DataSet dsData = gridphartime.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(gridphartime.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridphartime.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }


    public void hourclear()
    {
        ddlmhst.Items.Clear();
        ddlmhet.Items.Clear();
        ddltuhst.Items.Clear();
        ddltuhet.Items.Clear();
        ddlwhst.Items.Clear();
        ddlwhet.Items.Clear();
        ddlthst.Items.Clear();
        ddlthet.Items.Clear();
        ddlfhst.Items.Clear();
        ddlfhet.Items.Clear();
        ddlsahst.Items.Clear();
        ddlsahet.Items.Clear();
        ddlsuhst.Items.Clear();
        ddlsuhet.Items.Clear();
    }

    public void minuteclear()
    {
        ddlmmst.Items.Clear();
        ddlmmet.Items.Clear();
        ddltumst.Items.Clear();
        ddltumet.Items.Clear();
        ddlwmst.Items.Clear();
        ddlwmet.Items.Clear();
        ddlthmst.Items.Clear();
        ddlthmet.Items.Clear();
        ddlfmst.Items.Clear();
        ddlfmet.Items.Clear();
        ddlsamst.Items.Clear();
        ddlsamet.Items.Clear();
        ddlsumst.Items.Clear();
        ddlsumet.Items.Clear();
    }
    protected void gridphartime_Sorting(object sender, GridViewSortEventArgs e)
    {
        griddisplay();
        DataSet dsData = gridphartime.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = gridphartime.PageIndex;
        gridphartime.DataSource = SortDataTable(dtData, false);
        gridphartime.DataBind();
        gridphartime.PageIndex = pageIndex; 
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