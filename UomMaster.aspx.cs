using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datalayer;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;

public partial class UomMaster : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    Drug drg = new Drug();
    UOM um = new UOM();
    string sessionuserid = "", location = "", Actstatus="";
    int Rtnvalue;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sessionuserid = Session["Userid"].ToString();
            location = Session["location"].ToString();

            if (!IsPostBack)
            {
                ViewState["pharname"] = "";
                locationdisplay();

                txtitemcode.Attributes.Add("onKeyPress", "doClick(event)");
                txtdrugcode.Attributes.Add("onKeyPress", "doClick(event)");
                txtitemnameadd.Attributes.Add("onKeyPress", "doClick(event)");
                btnsearch.Attributes.Add("onclick", "itemsearch() ;return false;");

                txtcurrentqty.Attributes["onkeypress"] = "return Intchecks(event);";
                txtconversionqty.Attributes["onkeypress"] = "return Intchecks(event);";
                btnsave.Visible = true;
                btnupdate.Visible = false;
            }
        }
        catch (NullReferenceException)
        {
            //Response.Redirect("opas.html?Session=End");
            Response.Redirect("iopas.html");
        }
    }

    protected void btnok_Click(object sender, EventArgs e)
    {
        //txtitemcode.Text = "";
        //txtdrugcode.Text = "";
        //txtitemnameadd.Text = "";
        txtcurrentqty.Text = "";
        txtcurrentuom.Text = "";
        txtconversionqty.Text = "";
        txtconversionuom.Text = "";
        ViewState["pharname"] = "";
        locationdisplay();
        uomread();
        txtcurrentuom.ReadOnly = true;
        txtcurrentqty.Focus();
        useraddgrid();
        btnsave.Visible = true;
        btnupdate.Visible = false;
    }

    public void locationdisplay()
    {
        drg.pharmlocation(gridpharloc);
    }

    public bool chkphar(object phname)
    {
        string val = "";
        val = phname.ToString();
        ////locationidreader();       
        if (val == ViewState["pharname"].ToString())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // * Uom Reader Based On Search * \\
    public void uomread()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select distinct u.uom from Item_Location as u left join Item_Master as i on i.MasterID=u.MasterID where i.Item_Code='" + txtitemcode.Text.Trim() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtcurrentuom.Text = dr[0].ToString();
                    }
                }
            }
        }
    }
    protected void btnclear_Click(object sender, ImageClickEventArgs e)
    {
        Clear();
        useraddgrid();
    }
    void Clear()
    {
        txtitemcode.Text = "";
        txtdrugcode.Text = "";
        txtitemnameadd.Text = "";
        txtcurrentqty.Text = "";
        txtcurrentuom.Text = "";
        txtconversionqty.Text = "";
        txtconversionuom.Text = "";
        ViewState["pharname"] = "";
        locationdisplay();
        chkactive.Checked = false;
        btnsave.Visible = true;
        btnupdate.Visible = false;
    }

    void ClearUpdate()
    {        
        txtcurrentqty.Text = "";      
        txtconversionqty.Text = "";
        txtconversionuom.Text = "";
        ViewState["pharname"] = "";
        locationdisplay();
        chkactive.Checked = false;
        btnsave.Visible = true;
        btnupdate.Visible = false;
    }
    protected void btnsave_Click(object sender, ImageClickEventArgs e)
    {
        UOMInsert();
        if (Rtnvalue == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record created');</script>", false);
            ClearUpdate();
            useraddgrid();
        }
        else if (Rtnvalue == 3)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('UOM Mapping already exist');</script>", false);
        }
        else if (Rtnvalue == 2)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occurred');</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occurred');</script>", false);
        }        
    }

    void UOMInsert()
    {
        int a = 0;

        for (int i = 0; i < gridpharloc.Rows.Count; i++)
        {
            GridViewRow row = gridpharloc.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("chkphar")).Checked;

            if (isChecked)
            {
                if (ViewState["pharname"].ToString().ToLower() != (gridpharloc.Rows[i].Cells[0].Text.ToLower()))
                {

                    if (chkactive.Checked == true)
                    {
                        Actstatus = "Yes";
                    }
                    else if (chkactive.Checked == false)
                    {
                        Actstatus = "No";
                    }
                      
                   Rtnvalue= um.Uom_Mapping_Insert(gridpharloc.Rows[i].Cells[0].Text.ToLower(), txtitemcode.Text.Trim(), Convert.ToInt32(txtcurrentqty.Text), txtcurrentuom.Text,
                            Convert.ToInt32(txtconversionqty.Text), txtconversionuom.Text.Trim().ToUpper(), Actstatus, Session["Userid"].ToString());

                    a++;
                    if (Rtnvalue == 2 || Rtnvalue == 3 )
                    {
                        return;
                    }
                }                
            }
        }
        if (a == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Select pharmacy location');</script>", false);
        }
    }

    protected void btnupdate_Click(object sender, ImageClickEventArgs e)
    {
        UOMUpdate();
        if (Rtnvalue == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated');</script>", false);

            ClearUpdate();
            useraddgrid();
        }
        else if (Rtnvalue == 3)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('UOM Mapping already exist');</script>", false);
        }
        else if (Rtnvalue == 4)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Wrong Pharmacy location');</script>", false);
        }
        else if (Rtnvalue == 2)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occurred');</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occurred');</script>", false);
        }
    }

    void UOMUpdate()
    {
        int a = 0;

        for (int i = 0; i < gridpharloc.Rows.Count; i++)
        {
            GridViewRow row = gridpharloc.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("chkphar")).Checked;

            if (isChecked)
            {
                if (ViewState["pharname"].ToString().ToLower() == (gridpharloc.Rows[i].Cells[0].Text.ToLower()))
                {
                    if (chkactive.Checked == true)
                    {
                        Actstatus = "Yes";
                    }
                    else if (chkactive.Checked == false)
                    {
                        Actstatus = "No";
                    }

                    Rtnvalue = um.Uom_Mapping_Update((Int64)ViewState["MapID"], gridpharloc.Rows[i].Cells[0].Text.ToLower(), txtitemcode.Text.Trim(), Convert.ToInt32(txtcurrentqty.Text), txtcurrentuom.Text,
                             Convert.ToInt32(txtconversionqty.Text), txtconversionuom.Text.Trim().ToUpper(), Actstatus, Session["Userid"].ToString());

                    a++;
                    if (Rtnvalue == 2 || Rtnvalue == 3)
                    {
                        return;
                    }
                }
                else if (ViewState["pharname"].ToString().ToLower() != (gridpharloc.Rows[i].Cells[0].Text.ToLower()))
                {
                    a++;
                    Rtnvalue = 4;
                }
            }
        }
        if (a == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Select pharmacy location');</script>", false);
        }
    }


    public void useraddgrid()
    {
        um.Uomgridall(UomGrid, txtitemcode.Text);
        DataSet dsData = UomGrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(UomGrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(UomGrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }

    }
    protected void UomGrid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        btnsave.Visible = false;
        btnupdate.Visible = true;
        txtcurrentqty.Text = "";      
        txtconversionqty.Text = "";
        txtconversionuom.Text = "";
        
        ViewState["MapID"] = "0";
        ViewState["MapID"] = (Int64)UomGrid.DataKeys[e.NewSelectedIndex].Value;

        ViewState["pharname"] = "";
        ViewState["pharname"] = (UomGrid.Rows[e.NewSelectedIndex].Cells[1].Text);
        locationdisplay();

        txtcurrentqty.Text = (UomGrid.Rows[e.NewSelectedIndex].Cells[2].Text);        
        txtconversionqty.Text = (UomGrid.Rows[e.NewSelectedIndex].Cells[4].Text);
        txtconversionuom.Text = (UomGrid.Rows[e.NewSelectedIndex].Cells[5].Text);

        if (UomGrid.Rows[e.NewSelectedIndex].Cells[6].Text == "Yes")
        {
            chkactive.Checked = true;
        }
        else if (UomGrid.Rows[e.NewSelectedIndex].Cells[6].Text == "No")
        {
            chkactive.Checked = false;
        }
    }


}