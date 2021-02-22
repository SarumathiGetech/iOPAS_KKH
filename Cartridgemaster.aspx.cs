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
using System.Net;
using System.Data.SqlClient;
using System.Drawing;
using Datalayer;
public partial class Cartridgemaster : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    smsclass sms = new smsclass();
    string sessionuserid="",prefixfrom="",prefixto = "",cartnumfrom="",cartnumto = "";   
    int cartno, Rtnvalue;    
    int a = 0, b = 0, c = 0;
    //static string btnval = "";//,cartbtn="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pharmlocationreader();
            //carttype();
            griddel.PageIndex = 0;
            //cartbtn = "2";
            ViewState["cartbtn"] = "2";
            cartgriddisplay();
            texthide();
            btnsave.Visible = true;
            btnupdate.Visible = false;

            txtfromcartno.Attributes.Add("onKeyPress", "doClick(event)");
            txttocartno.Attributes.Add("onKeyPress", "doClick(event)");
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
    }
    protected void ddlcarttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    //protected void btnsave_Click(object sender, EventArgs e)
    protected void btnsave_Click(object sender, ImageClickEventArgs e)
    {
       
            string i, j = "";
            i = txtfromcartno.Text.Trim();
            Int32 lenfrom = i.Length;
            if (lenfrom == 6)
            {
                prefixfrom = i.Substring(0, 2);
                cartnumfrom = i.Substring(2, 4);

                j = txttocartno.Text.Trim();
                Int32 lento = j.Length;
                if (lento == 6)
                {
                    prefixto = j.Substring(0, 2);
                    cartnumto = j.Substring(2, 4);
                }
                else if (lento != 0 || lento > 1 && lento < 6)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge number is two alphabet and four numbers');</script>", false);
                }
                if (lento == 0 || lento == 6)
                {
                    presavefunction();
                    //textboxclear();
                    //carttype();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge number is two alphabet and four numbers');</script>", false);
            }
        }
       


    protected void btnupdate_Click(object sender, ImageClickEventArgs e)
    {
        cartupdate();
        textboxclear();
        textboxcleartwo();
        texthide();
        hsph.Visible = true;
        //cartbtn = "2";
        ViewState["cartbtn"] = "2";
        cartgriddisplay();
    }
  
    // * New Cartridge validation Function *\\
    public void presavefunction()
    {
        cartno = Convert.ToInt32(cartnumfrom);
        
            if (txttocartno.Text.Trim() != "")
            {
                int result;
                int a = 0;

                if ((Convert.ToInt32(cartnumfrom)) > (Convert.ToInt32(cartnumto)))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge No To must be greater than Cartridge No From');</script>", false);
                }
                else
                {
                    result = (Convert.ToInt32(cartnumto) - Convert.ToInt32(cartnumfrom));
                    if (Convert.ToInt32(cartnumto) != 0 && prefixfrom.ToUpper()==prefixto.ToUpper())
                    {
                        result = result + 1;
                    }
                    else if(prefixfrom.ToUpper()!=prefixto.ToUpper())
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Do not match cartridge prefix ');</script>", false);
                        return;
                    }
                    while (a < result)
                    {
                        if (Rtnvalue == 0 ||Rtnvalue == 1)
                        {
                            cartinsert();
                            a++;
                            cartno++;
                        }
                        else if(Rtnvalue==2)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Cartridge number already exist');</script>", false);
                            //cartbtn = "2";
                            ViewState["cartbtn"] = "2";
                            cartgriddisplay();
                            return;
                        }
                        else if (Rtnvalue == 3)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Do not match cartridge prefix');</script>", false);
                            //cartbtn = "2";
                            ViewState["cartbtn"] = "2";
                            cartgriddisplay();
                            return;
                        }
                    }
                }
            }
            else
            {
                cartinsert();                
                cartgriddisplay();
            }
            if (Rtnvalue == 1)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Record created ');</script>", false);
                textboxcleartwo();
                cartgriddisplay();
                //carttype();                
            }
            else if (Rtnvalue == 2)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Cartridge number already exist ');</script>", false);
                cartgriddisplay();
                return;
            }
            else if (Rtnvalue == 3)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Do not match cartridge prefix');</script>", false);
                cartgriddisplay();
                return;
            }
           
           // textboxcleartwo();
    }

  // * Pharmacy Location Reader * \\
    public void pharmlocationreader()
    {
        ddlphargrp.Items.Clear();       

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
                        ddlphargrp.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }

 //// * Cartridge type Reader * \\
 //   public void carttype()
 //   {
 //       ddlcarttype.Items.Clear();
 //       ddlcarttype.Items.Add("--Select--");      
 //       SqlDataReader dr = sms.carttypereader();
 //       if (dr.HasRows)
 //       {
 //           while (dr.Read())
 //           {
 //               ddlcarttype.Items.Add(dr[0].ToString());
 //           }
 //           dr.Close();
 //       }
 //   }

 // * New Cartridge Insert Function * \\
    public void cartinsert()
    {
        string cartnum = "";        
        int len = Convert.ToString(cartno).Length;
        if (len == 4)
        {
             cartnum = ((prefixfrom.ToUpper()) + Convert.ToString(cartno));
        }
        else if (len == 1)
        {
            cartnum = ((prefixfrom.ToUpper()) + "000" + Convert.ToString(cartno));
        }
        else if (len == 2)
        {
            cartnum = ((prefixfrom.ToUpper()) + "00" + Convert.ToString(cartno));
        }
        else if (len == 3)
        {
              cartnum = ((prefixfrom.ToUpper()) + "0" + Convert.ToString(cartno));
        }
        
        Rtnvalue= sms.cartridgeinsert(ddlphargrp.SelectedValue.ToString(), cartnum, "", txtdescription.Text.Trim(), sessionuserid = Session["Userid"].ToString());
                
    }
    // * Cartridge Update Function * \\
    public void cartupdate()
    {
       int Rtvalue= sms.cartridgeupdate(ddlphargrp.SelectedValue.ToString(), txtcartnoedit.Text.Trim(), "", txtdescription.Text.Trim(), sessionuserid = Session["Userid"].ToString());
       if (Rtvalue == 1)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated ');</script>", false);
       }
       else
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Error occured ');</script>", false);
       }
    }
 // * Grid Display Function * \\
    public void cartgriddisplay()
    {
        sms.ddldisplay(griddel, ddlphargrp.SelectedItem.Value);
        DataSet dsData = griddel.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(griddel.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(griddel.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }
    //protected void btnsearch_Click(object sender, EventArgs e)
    protected void btnsearch_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["cartbtn"] = "1";
        griddel.PageIndex = 0;
        cartsearchfn();
    }


    public void cartsearchfn()
    {
        // * cartridge number Search Function * \\
        if (txttocartno.Text == "" && txtfromcartno.Text != "")
        {
            sms.searchdisplay(griddel, txtfromcartno.Text,ddlphargrp.SelectedValue.ToString());
            DataSet dsData = griddel.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(griddel.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(griddel.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.ForeColor = Color.Black;
            lblpge.Font.Bold = false;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = Color.Green;
            }
        }
        else if (txttocartno.Text != "" && txtfromcartno.Text != "")
        {
            sms.searchrange(griddel, txtfromcartno.Text.Trim(), txttocartno.Text.Trim(), ddlphargrp.SelectedValue.ToString());
            DataSet dsData = griddel.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(griddel.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(griddel.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.ForeColor = Color.Black;
            lblpge.Font.Bold = false;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = Color.Green;
            }
        }

        else if (txttocartno.Text == "" && txtfromcartno.Text== "")
        {
            sms.searchtype(griddel, ddlphargrp.SelectedValue.ToString());
            DataSet dsData = griddel.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(griddel.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(griddel.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
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

    // * CARTRIDGE ACTIVE INACTIVE STATUS FUNCTIONS * \\
    public void activeinactive()
    {
        for (int i = 0; i < griddel.Rows.Count; i++)
        {
            GridViewRow row = griddel.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("chkaccess")).Checked;

            if (isChecked)
            {
                c++;
                string ddsno = (griddel.Rows[i].Cells[4].Text);
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
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Selected records are already Active.');</script>", false);
        }
        else if (a != 0 & b != 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Selected records are of different status.');</script>", false);
        }
        else if (c != 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "popup", "confirmProcess()", true);
        }
        else if (c == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Records are not selected .');</script>", false);
        }
        else
        {
            cartgriddisplay();
        }       
    }


    //protected void btndeactive_Click(object sender, EventArgs e)
    protected void btndeactive_Click(object sender, ImageClickEventArgs e)
    {
        activeinactive();
        //btnval = "";
        //btnval = "2";
        ViewState["btnval"] = "2";
        if (b != 0 & a == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Selected records are already Inactive. ');</script>", false);
        }
        else if (a != 0 & b != 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Selected records are of different status.');</script>", false);
        }
        else if (c != 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "popup", "confirmProcess1()", true);
        }
        else if (c == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Records are not selected.');</script>", false);
        }
        else
        {
            cartgriddisplay();
        }
    }

    protected void btnok_Click(object sender, EventArgs e)
    {
        DateTime dta = System.DateTime.Now;
        if (ViewState["btnval"].ToString() == "1")
        {
            for (int i = 0; i < griddel.Rows.Count; i++)
            {
                GridViewRow row = griddel.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkaccess")).Checked;

                if (isChecked)
                {
                    sms.cartenable((griddel.Rows[i].Cells[3].Text), sessionuserid = Session["Userid"].ToString(), dta);
                }
            }
            cartgriddisplay();
        }

        else if (ViewState["btnval"].ToString() == "2")
        {
            for (int i = 0; i < griddel.Rows.Count; i++)
            {
                GridViewRow row = griddel.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkaccess")).Checked;

                if (isChecked)
                {
                    if (Cart_State_Check(griddel.Rows[i].Cells[3].Text))
                    {
                        sms.cartdisabled((griddel.Rows[i].Cells[3].Text), sessionuserid = Session["Userid"].ToString(), dta);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Inventory must be closed at Cartridge Unloading before inactivate.');</script>", false);
                        return;
                    }
                }
            }
            cartgriddisplay();
        }
    }


    public bool Cart_State_Check(string CartID)
    {
        bool Rtn = true;

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select count(*) from Cartridge_Loading where Cartridge_Id='" + CartID + "' and Inventory_Status =2";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        if (Convert.ToInt32(dr[0].ToString()) > 0)
                        {
                            Rtn = false;
                        }
                        else if (Convert.ToInt32(dr[0].ToString()) == 0)
                        {
                            Rtn = true;
                        }
                    }
                }
            }
        }

        return Rtn;
        
    }

// *==================================================*==============================================================

    protected void griddel_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {        
        textvisible();
        pharmlocationreader();
        ddlphargrp.Items.Remove(griddel.Rows[e.NewSelectedIndex].Cells[2].Text);
        ddlphargrp.Items.Insert(0,new ListItem (griddel.Rows[e.NewSelectedIndex].Cells[2].Text));
        txtcartnoedit.Text = (griddel.Rows[e.NewSelectedIndex].Cells[3].Text);


        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select Description from Cartridge_Master where Cartridge_Id='" + (griddel.Rows[e.NewSelectedIndex].Cells[3].Text) + "'";
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

    protected void ddlphargrp_SelectedIndexChanged(object sender, EventArgs e)
    {        
    // cartsearch.Text = "";
     //sms.ddldisplay(griddel, ddlphargrp.SelectedItem.Value); 
        textboxclear();
        textboxcleartwo();
        texthide();
        hsph.Visible = true;
        cartgriddisplay();
        //carttype();
    }
    // * TextBox Clear Function * \\
    public void textboxclear()
    {      
        txtdescription.Text = "";
        btnsave.Visible = true;
        btnupdate.Visible = false;
    }
    // * TextBox Clear Function Two * \\
    public void textboxcleartwo()
    {
        txtfromcartno.Text = "";
        txttocartno.Text = "";
    }
   // * Text Box Enable Visible True Function * \\\
    public void textvisible()
    {
        hsph.Visible = false;
        txtdescription.Text = "";
        btnsave.Visible = false;
        btnupdate.Visible = true;    
        lblcartno.Visible = true;
        txtcartnoedit.Visible = true;       
    }

    // * Text Box Enable Visible False Function * \\\
    public void texthide()
    {       
        lblcartno.Visible = false;
        txtcartnoedit.Visible = false;
    }
    //protected void btnadd_Click(object sender, EventArgs e)
    protected void btnadd_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["cartbtn"] = "2";
       // cartbtn = "2";
        textboxclear();
        textboxcleartwo();       
        texthide();
        hsph.Visible = true;
        griddel.PageIndex = 0;
        cartgriddisplay();
        //carttype();
    }
    protected void griddel_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //if (cartbtn == "1")
        if( ViewState["cartbtn"].ToString()=="1")
        {
            cartsearchfn();
        }
        else if (ViewState["cartbtn"].ToString() == "2")
        {
            cartgriddisplay();
        }
        griddel.PageIndex = e.NewPageIndex;
        griddel.DataBind();
        DataSet dsData = griddel.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(griddel.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(griddel.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
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
    protected void griddel_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (ViewState["cartbtn"].ToString() == "1")
        {
            cartsearchfn();
        }
        else if (ViewState["cartbtn"].ToString() == "2")
        {
            cartgriddisplay();
        }
       
        DataSet dsData = griddel.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = griddel.PageIndex;
        griddel.DataSource = SortDataTable(dtData, false);
        griddel.DataBind();
        griddel.PageIndex = pageIndex; 
    }
   
}
