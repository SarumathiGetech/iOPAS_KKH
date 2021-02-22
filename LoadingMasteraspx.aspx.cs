using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using Datalayer;
using System.Data;
using System.Drawing;

public partial class LoadingMasteraspx : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    smsclass sms = new smsclass();
    string sessionuserid = "";
    int countval = 0;
    //public static int locationid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            locationreader();
            locationidreader();
            masterreader();
            griddisplay();
            try
            {
                sessionuserid = Session["Userid"].ToString();
            }
            catch (NullReferenceException)
            {
                Response.Redirect("iopas.html");
            }
        }
    }

    // * Location Reader * \\
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
                        ddlpharmloc.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }

    //protected void btnsecond_Click(object sender, EventArgs e)
    protected void btnsecond_Click(object sender, ImageClickEventArgs e)
    {
        if(Convert.ToInt16(txtexpdate.Text.Trim())>Convert.ToInt32(txtdisable.Text.Trim()))
        {
        loadingmasterinsert();
        loadingclear();
        masterreader();
        }
        else if(Convert.ToInt16(txtexpdate.Text.Trim())<=Convert.ToInt32(txtdisable.Text.Trim()))
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Drug Expiry for Loading must be greater than Drug Expiry for Auto Disabling');</script>", false);
        }
    }

    // * Loading Master Insert and Update Function *\\
    public void loadingmasterinsert()
    {
        counterreader();
        locationidreader();
        int a = 0;
        int b = 0;
        if (chkfist.Checked == true)
        {
            a = 1;
        }
        else if (chkfist.Checked == false)
        {
            a = 2;
        }

        if (chksecond.Checked == true)
        {
            b = 1;
        }
        else if (chksecond.Checked == false)
        {
            b = 2;
        }
        if (countval == 1)
        {
            //sms.masterupdate(locationid, a, b, Convert.ToInt32(txtexpdate.Text),Convert.ToInt32(txtdisable.Text),Convert.ToInt32(txtpackdur.Text),Convert.ToInt32(txtassemdur.Text),sessionuserid = Session["Userid"].ToString(), (System.DateTime.Now));
            sms.masterupdate(ddlpharmloc.SelectedValue.ToString(), a, b, Convert.ToInt32(txtexpdate.Text.Trim()), Convert.ToInt32(txtdisable.Text.Trim()), Convert.ToInt32(Txtpartialcart.Text.Trim()), sessionuserid = Session["Userid"].ToString());
            griddisplay();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated');</script>", false);
        }
        else if (countval == 0)
        {
            // sms.masterinsert(locationid, a, b, Convert.ToInt32(txtexpdate.Text),Convert.ToInt32(txtdisable.Text),Convert.ToInt32(txtpackdur.Text),Convert.ToInt32(txtassemdur.Text),sessionuserid = Session["Userid"].ToString(), (System.DateTime.Now));
            sms.masterinsert((int)ViewState["locationid"], a, b, Convert.ToInt32(txtexpdate.Text.Trim()), Convert.ToInt32(txtdisable.Text.Trim()),Convert.ToInt32(Txtpartialcart.Text.Trim()), sessionuserid = Session["Userid"].ToString(), (System.DateTime.Now));
            griddisplay();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record created');</script>", false);
        }

    }

    // * Loading Master verification Reader * \\
    public void masterreader()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select LF_Verification,Second_Verification,Min_Exp_Loading,Min_Exp_Disabling,BDS_Cart_Auto_Enable from Loading_Master where PharmacyID='" + (int)ViewState["locationid"] + "'";
            cmd = new SqlCommand(Commt, con);
            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtexpdate.Text = dr[2].ToString();
                        txtdisable.Text = dr[3].ToString();
                        Txtpartialcart.Text = dr[4].ToString();

                        if (dr[0].ToString() == "1")
                        {
                            chkfist.Checked = true;
                        }
                        else if (dr[0].ToString() == "2")
                        {
                            chkfist.Checked = false;
                        }

                        if (dr[1].ToString() == "1")
                        {
                            chksecond.Checked = true;
                        }
                        else if (dr[1].ToString() == "2")
                        {
                            chksecond.Checked = false;
                        }
                    }
                }
            }
        }
    }
    // * Loading vefirication counter allready exist or not checking * \\
    public void counterreader()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select COUNT(PharmacyID)as coun from Loading_Master where PharmacyID='" + (int)ViewState["locationid"] + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
            {
                countval = Convert.ToInt32(dr[0].ToString());
            }

                }
            }
        }
    }
    protected void ddlpharmloc_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadingclear();
        locationidreader();
        masterreader();
    }
    // * Loading text Box Clear * \\
    public void loadingclear()
    {
        chkfist.Checked = false;
        chksecond.Checked = false;
        txtexpdate.Text = "";
        txtdisable.Text = "";
        Txtpartialcart.Text = "";
    }

    //* Grid Display Function *\\
    public void griddisplay()
    {
        sms.displaygrid(griddetail);
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
    // * Location Id Reader * \\
    public void locationidreader()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select pharmacyID from pharmacy where Location_Name='" + ddlpharmloc.SelectedItem.Value + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //locationid = Convert.ToInt32(dr[0].ToString());
                        ViewState["locationid"] = Convert.ToInt32(dr[0].ToString());
                    }
                }
            }
        }
    }
    protected void griddetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        griddisplay();
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
}