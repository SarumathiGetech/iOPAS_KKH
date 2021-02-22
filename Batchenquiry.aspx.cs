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
public partial class Batchenquiry : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    smsclass sms = new smsclass();
    DateTime QDateto, QDatefrom;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            //Response.Redirect("opas.html?Session=End");
            Response.Redirect("iopas.html");
        }
        else
        {
            if (!IsPostBack)
            {
                locationreader();
                ddsnamereader();
            }
        }
        string scriptStr = "javascript:return popUpCalendar(this," + getClientID() + @", 'dd/mm/yyyy', '__doPostBack(\'" + getClientID() + @"\')')";
        imgCalendar.Attributes.Add("onclick", scriptStr);
        string scriptStr2 = "javascript:return popUpCalendar(this," + getClientIDtwo() + @", 'dd/mm/yyyy', '__doPostBack(\'" + getClientID() + @"\')')";
        img.Attributes.Add("onclick", scriptStr2);        
       
    }
    public string getClientID()
    {
        return txt_Date.ClientID;
    }
    public string getClientIDtwo()
    {
        return txtdate.ClientID;
    }

    // * Pharmacy Location Name Reader * \\
    public void locationreader()
    {

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
                        ddlphar.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }

    // * DDS Name Reader * \\
    public void ddsnamereader()
    {
        ddlddsname.Items.Clear();
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select d.DDS_Name from dds as d left join Pharmacy as p on p.PharmacyID=d.PharmacyID where p.Location_Name='" + ddlphar.SelectedValue.ToString() + "' and d.Status='Active' and d.DDS_Name not like 'B%' ";
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
    
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {       
        
    }
    //protected void btnsearch_Click(object sender, EventArgs e)
    protected void btnsearch_Click(object sender, ImageClickEventArgs e)
    {
        Enquirygrd.PageIndex = 0;
         int b = 0;
       
        if (txtrefno.Text.Trim() != "")
        {
            b = b + 1;
        }
      
        if (txtitemname.Text.Trim() != "")
        {
            b = b + 1;
        }
        if (txtitemcode.Text.Trim() != "")
        {
            b = b + 1;
        }         
        if (b == 0 || b == 1)
        {
            if (txt_Date.Text.Trim() == "" && txtdate.Text.Trim() == "")
            {
                griddisplay();
            }
            else if (txt_Date.Text.Trim() != "" && txtdate.Text.Trim() == "")
            {
                griddisplay();
            }
            else if (txt_Date.Text.Trim() != "" && txtdate.Text.Trim() != "")
            {
                datefun();
                if (QDatefrom <= QDateto)
                {
                    griddisplay();
                }
                else if (QDatefrom > QDateto)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Date to must be later than date from');</script>", false);
                }
            }

        }
        else if (b != 0 && b != 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Only one field entry is allowed');</script>", false);
        }
    }
    protected void Enquirygrd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        griddisplay();
        Enquirygrd.PageIndex = e.NewPageIndex;
        Enquirygrd.DataBind();
        DataSet dsData = Enquirygrd.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(Enquirygrd.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Enquirygrd.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
    }
    //protected void btnclear_Click(object sender, EventArgs e)
    protected void btnclear_Click(object sender, ImageClickEventArgs e)
    {
        ddlstatus.Items.Clear();
        ddlstatus.Items.Add("All");
        ddlstatus.Items.Add("Pending");
        ddlstatus.Items.Add("Cancelled");
        ddlstatus.Items.Add("System Aborted");
        ddlstatus.Items.Add("Completed");
        txt_Date.Text = "";
        txtdate.Text = "";
        txtitemcode.Text = "";
        txtitemname.Text = "";
        txtrefno.Text = "";
        sms.enquiryorderref(Enquirygrd, ddlphar.SelectedValue.ToString(), "");
        DataSet dsData = Enquirygrd.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(Enquirygrd.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Enquirygrd.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
    protected void ddlphar_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddsnamereader();
    }

    public void griddisplay()
    {
        lblpge.Visible = false;
        if (txtrefno.Text != "")
        {
            sms.enquiryorderref(Enquirygrd, ddlphar.SelectedValue.ToString(), txtrefno.Text);
           lblpge.Visible = true;
           DataSet dsData = Enquirygrd.DataSource as DataSet;
           DataTable dtData = dsData.Tables[0];
           lblpge.Text = "Page" + "  " + Convert.ToString(Enquirygrd.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Enquirygrd.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
           lblpge.ForeColor = Color.Black;
           lblpge.Font.Bold = false;
           if (dtData.Rows.Count == 0)
           {
               lblpge.Text = "No Record Found";
               lblpge.Font.Bold = true;
               lblpge.ForeColor = Color.Green;
           }
        }

        else if (txtrefno.Text == "" && txtitemcode.Text != "" && ddlstatus.SelectedValue.ToString() != "All" && txt_Date.Text.Trim()=="" && txtdate.Text.Trim()=="")
        {
            sms.enquiryitemcode(Enquirygrd, ddlphar.SelectedValue.ToString(), ddlddsname.SelectedValue.ToString(), ddlstatus.SelectedItem.ToString(), txtitemcode.Text, txt_Date.Text, txtdate.Text);
            lblpge.Visible = true;
            DataSet dsData = Enquirygrd.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(Enquirygrd.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Enquirygrd.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.ForeColor = Color.Black;
            lblpge.Font.Bold = false;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = Color.Green;
            }
        }
        else if (txtrefno.Text == "" && txtitemcode.Text != "" && ddlstatus.SelectedValue.ToString() != "All" && txt_Date.Text.Trim() != "" && txtdate.Text.Trim() != "")
        {
            sms.enquiryitemcode(Enquirygrd, ddlphar.SelectedValue.ToString(), ddlddsname.SelectedValue.ToString(), ddlstatus.SelectedItem.ToString(), txtitemcode.Text, txt_Date.Text, txtdate.Text);
            lblpge.Visible = true;
            DataSet dsData = Enquirygrd.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(Enquirygrd.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Enquirygrd.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.ForeColor = Color.Black;
            lblpge.Font.Bold = false;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = Color.Green;
            }
        }
        else if (txtrefno.Text == "" && txtitemcode.Text != "" && ddlstatus.SelectedValue.ToString() != "All" && txt_Date.Text.Trim() != "" && txtdate.Text.Trim() == "")
        {
            sms.enquiryitemcodesingle(Enquirygrd, ddlphar.SelectedValue.ToString(), ddlddsname.SelectedValue.ToString(), ddlstatus.SelectedItem.ToString(), txtitemcode.Text, txt_Date.Text, txtdate.Text);
            lblpge.Visible = true;
            DataSet dsData = Enquirygrd.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(Enquirygrd.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Enquirygrd.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.ForeColor = Color.Black;
            lblpge.Font.Bold = false;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = Color.Green;
            }
        }

        else if (txtrefno.Text == "" && txtitemcode.Text != "" && ddlstatus.SelectedValue.ToString() == "All" && txt_Date.Text.Trim() == "" && txtdate.Text.Trim() == "")
        {
            sms.enquiryitemcodenostatus(Enquirygrd, ddlphar.SelectedValue.ToString(), ddlddsname.SelectedValue.ToString(), txtitemcode.Text, txt_Date.Text, txtdate.Text);
            lblpge.Visible = true;
            DataSet dsData = Enquirygrd.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(Enquirygrd.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Enquirygrd.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.ForeColor = Color.Black;
            lblpge.Font.Bold = false;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = Color.Green;
            }
        }
        else if (txtrefno.Text == "" && txtitemcode.Text != "" && ddlstatus.SelectedValue.ToString() == "All" && txt_Date.Text.Trim() != "" && txtdate.Text.Trim() != "")
        {
            sms.enquiryitemcodenostatus(Enquirygrd, ddlphar.SelectedValue.ToString(), ddlddsname.SelectedValue.ToString(), txtitemcode.Text, txt_Date.Text, txtdate.Text);
            lblpge.Visible = true;
            DataSet dsData = Enquirygrd.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(Enquirygrd.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Enquirygrd.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.ForeColor = Color.Black;
            lblpge.Font.Bold = false;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = Color.Green;
            }
        }
        else if (txtrefno.Text == "" && txtitemcode.Text != "" && ddlstatus.SelectedValue.ToString() == "All" && txt_Date.Text.Trim() != "" && txtdate.Text.Trim() == "")
        {
            sms.enquiryitemcodenostatussingle(Enquirygrd, ddlphar.SelectedValue.ToString(), ddlddsname.SelectedValue.ToString(), txtitemcode.Text, txt_Date.Text, txtdate.Text);
            lblpge.Visible = true;
            DataSet dsData = Enquirygrd.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(Enquirygrd.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Enquirygrd.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.ForeColor = Color.Black;
            lblpge.Font.Bold = false;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = Color.Green;
            }
        }

        else if (txtitemcode.Text == "" && txtrefno.Text == "" && ddlstatus.SelectedValue.ToString() != "All" && txt_Date.Text.Trim() == "" && txtdate.Text.Trim() == "")
        {
            sms.enquirygrid(Enquirygrd, ddlphar.SelectedValue.ToString(), ddlddsname.SelectedValue.ToString(), ddlstatus.SelectedItem.ToString(), txtitemname.Text.Trim(), txt_Date.Text, txtdate.Text);
            lblpge.Visible = true;
            DataSet dsData = Enquirygrd.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(Enquirygrd.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Enquirygrd.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.ForeColor = Color.Black;
            lblpge.Font.Bold = false;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = Color.Green;
            }
        }
        else if (txtitemcode.Text == "" && txtrefno.Text == "" && ddlstatus.SelectedValue.ToString() != "All" && txt_Date.Text.Trim() != "" && txtdate.Text.Trim() != "")
        {
            sms.enquirygrid(Enquirygrd, ddlphar.SelectedValue.ToString(), ddlddsname.SelectedValue.ToString(), ddlstatus.SelectedItem.ToString(), txtitemname.Text.Trim(), txt_Date.Text, txtdate.Text);
            lblpge.Visible = true;
            DataSet dsData = Enquirygrd.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(Enquirygrd.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Enquirygrd.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.ForeColor = Color.Black;
            lblpge.Font.Bold = false;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = Color.Green;
            }
        }
        else if (txtitemcode.Text == "" && txtrefno.Text == "" && ddlstatus.SelectedValue.ToString() != "All" && txt_Date.Text.Trim() != "" && txtdate.Text.Trim() == "")
        {
            sms.enquirygridsingle(Enquirygrd, ddlphar.SelectedValue.ToString(), ddlddsname.SelectedValue.ToString(), ddlstatus.SelectedItem.ToString(), txtitemname.Text.Trim(), txt_Date.Text, txtdate.Text);
            lblpge.Visible = true;
            DataSet dsData = Enquirygrd.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(Enquirygrd.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Enquirygrd.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.ForeColor = Color.Black;
            lblpge.Font.Bold = false;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = Color.Green;
            }
        }

        else if (txtitemcode.Text == "" && txtrefno.Text == "" && ddlstatus.SelectedValue.ToString() == "All" && txt_Date.Text.Trim() == "" && txtdate.Text.Trim() == "")
        {
            sms.Enquirygrdall(Enquirygrd, ddlphar.SelectedValue.ToString(), ddlddsname.SelectedValue.ToString(), txtitemname.Text.Trim(), txt_Date.Text, txtdate.Text);
            lblpge.Visible = true;
            DataSet dsData = Enquirygrd.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(Enquirygrd.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Enquirygrd.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.ForeColor = Color.Black;
            lblpge.Font.Bold = false;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = Color.Green;
            }
        }
        else if (txtitemcode.Text == "" && txtrefno.Text == "" && ddlstatus.SelectedValue.ToString() == "All" && txt_Date.Text.Trim() != "" && txtdate.Text.Trim() != "")
        {
            sms.Enquirygrdall(Enquirygrd, ddlphar.SelectedValue.ToString(), ddlddsname.SelectedValue.ToString(), txtitemname.Text.Trim(), txt_Date.Text, txtdate.Text);
            lblpge.Visible = true;
            DataSet dsData = Enquirygrd.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(Enquirygrd.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Enquirygrd.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.ForeColor = Color.Black;
            lblpge.Font.Bold = false;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = Color.Green;
            }
        }
        else if (txtitemcode.Text == "" && txtrefno.Text == "" && ddlstatus.SelectedValue.ToString() == "All" && txt_Date.Text.Trim()!= "" && txtdate.Text.Trim() == "")
        {
            sms.Enquirygrdallsingle(Enquirygrd, ddlphar.SelectedValue.ToString(), ddlddsname.SelectedValue.ToString(), txtitemname.Text.Trim(), txt_Date.Text, txtdate.Text);
            lblpge.Visible = true;
            DataSet dsData = Enquirygrd.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(Enquirygrd.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Enquirygrd.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.ForeColor = Color.Black;
            lblpge.Font.Bold = false;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = Color.Green;
            }
        }
        else
        {
            sms.enquiryorderref(Enquirygrd, ddlphar.SelectedValue.ToString(),"");
            DataSet dsData = Enquirygrd.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(Enquirygrd.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Enquirygrd.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.ForeColor = Color.Black;
            lblpge.Font.Bold = false;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = Color.Green;
            }
        }
    }

    public void datefun()
    {
        string i = "";
        i = txt_Date.Text.Trim();
        Int32 len = i.Length;
        Int32 n = i.IndexOf('/');
        string str = i.Substring(n + 1, 2);
        string str1 = i.Substring(0, 2);
        string str2 = i.Substring(6, 4);
        string dat = str + "/" + str1 + "/" + str2;
        QDatefrom = Convert.ToDateTime(dat);

        if (txtdate.Text.Trim() != "")
        {
            string F = "";
            F = txtdate.Text.Trim();
            Int32 lenn = F.Length;
            Int32 nn = F.IndexOf('/');
            string strr = F.Substring(nn + 1, 2);
            string strr1 = F.Substring(0, 2);
            string strr2 = F.Substring(6, 4);
            string datt = strr + "/" + strr1 + "/" + strr2;
            QDateto = Convert.ToDateTime(datt);
        }
    }
    protected void Enquirygrd_Sorting(object sender, GridViewSortEventArgs e)
    {
        griddisplay();
        DataSet dsData = Enquirygrd.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = Enquirygrd.PageIndex;
        Enquirygrd.DataSource = SortDataTable(dtData, false);
        Enquirygrd.DataBind();
        Enquirygrd.PageIndex = pageIndex;   
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