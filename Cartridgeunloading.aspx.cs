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

public partial class Cartridgeunloading : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    Preload pre = new Preload();
    string sessionuserid="",location = "";
    int activationid;
    //public static int loadingid;    
    protected void Page_Load(object sender, EventArgs e)
    {       
        try
        {            
            sessionuserid = Session["Userid"].ToString();
            location = Session["location"].ToString();
            if (!IsPostBack)
            {
                ViewState["loadingid"] = 0;
                // lbluom1.Visible = false;
                lbluom2.Visible = false;
                lbluom3.Visible = false;
                lbluom4.Visible = false;
                reason();
                txtphyinventory.Attributes.Add("onKeyPress", "doClick(event)");
            }

            if (location != "")
            {
                txtcartno.Focus();
            }
            else if (location == "" || location == null)
            {
                //txtcartno.ReadOnly = true;         
                pp.Visible = false;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Workstation is not authorised to do Unloading');</script>", false);
                return;
            }    
        }
        catch (NullReferenceException)
        {
            //Response.Redirect("opas.html?Session=End");
            Response.Redirect("opas.html");
        }                       
    }
    
  
    //protected void Btngo_Click(object sender, EventArgs e)
    protected void Btngo_Click(object sender, ImageClickEventArgs e)
    {
        int Rtnvalue = pre.Preloadvalidate(txtcartno.Text, location,"Cartridge Unloading");
        if (Rtnvalue == 1 || Rtnvalue == 8 || Rtnvalue==31)
        {
            clear();
            cartinventoryclose();
            if (activationid != 5)
            {
                txtphyinventory.Focus();
                if (txtitemcode.Text == "")
                {
                    clear();
                    txtcartno.Text = "";
                    txtcartno.Focus();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge is not preLoaded');</script>", false);
                }
            }
            else if (activationid == 5)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please disable the cartridge and close the inventory');</script>", false);
                clear();
                txtcartno.Text = "";
                txtcartno.Focus();
            }
        }
        else if (Rtnvalue == 5)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid cartridge number');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
        }
        else if (Rtnvalue == 6)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Cartridge does not belong to this pharmacy ');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
        }
        else if (Rtnvalue == 7)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Cartridge is Inactive');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
        }
        else if (Rtnvalue == 16)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge is loaded into DDS');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
        }        
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
        }       
    }


    protected void btnsave_Click(object sender, ImageClickEventArgs e)
    {
        if ((Convert.ToInt32(txtdiscrep.Text) != 0) && (ddlreason.SelectedValue.ToString() != "-Select-"))
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "popup", "confirmProcess('Discrepancy value is ( " + txtdiscrep.Text + " ) ')", true);
        }
        else if ((Convert.ToInt32(txtdiscrep.Text) != 0) && (ddlreason.SelectedValue.ToString() == "-Select-"))
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Discrepancy value is ( " + txtdiscrep.Text + " ) , Please select the reason');</script>", false);
        }
        else if ((Convert.ToInt32(txtdiscrep.Text) == 0))
        {
            UnloadSave();
        }
    }

    protected void btnok_Click(object sender, EventArgs e)
    {
        UnloadSave();
    }

    void UnloadSave()
    {
        int Rtnvalue = pre.Preloadvalidate(txtcartno.Text.Trim(), location, "Cartridge Unloading");

        if (Rtnvalue == 1 || Rtnvalue == 8 || Rtnvalue == 31)
        {
            cartinventoryclose();
            if (activationid != 5)
            {
                if ((Int64)ViewState["loadingid"] != 0)
                {
                    unloadinsert();
                    clear();
                    txtcartno.Text = "";
                }
                else if ((Int64)ViewState["loadingid"] == 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Incorrect cartridge Number');</script>", false);
                    clear();
                    txtcartno.Text = "";
                }
            }
            else if (activationid == 5)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please disable the cartridge and close the inventory');</script>", false);
                txtcartno.Text = "";
                clear();
                txtcartno.Focus();
            }
        }
        else if (Rtnvalue == 5)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid cartridge number');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
        }
        else if (Rtnvalue == 6)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Cartridge does not belong to this pharmacy ');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
        }
        else if (Rtnvalue == 7)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Cartridge is Inactive');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
        }
    }

    // * Inventory Reader Function * \\
    public void cartinventoryclose()
    {
      //  loadingid = 0;
        ViewState["loadingid"] = 0;

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select p.loading_id,i.item_code,i.item_name,p.Quantity,p.Aval_Quantity,IL.uom,p.Activation_Status,pm.PackType,UM.Pack_Size from cartridge_loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Item_Location as IL on IL.MasterID=i.MasterID left join Packtype_Master as pm on pm.ID=UM.PacktypeID  where p.Inventory_Status=2 and p.Cartridge_Id='" + txtcartno.Text.Trim() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lbluom2.Visible = true;
                        lbluom3.Visible = true;
                        lbluom4.Visible = true;
                        // loadingid = Convert.ToInt32(dr[0].ToString());
                        ViewState["loadingid"] = Convert.ToInt64(dr[0].ToString());
                        txtitemcode.Text = dr[1].ToString();
                        txtitemname.Text = dr[2].ToString();
                        int totqty = Convert.ToInt32(dr[3].ToString());
                        txtcurrentinv.Text = dr[4].ToString();
                        activationid = Convert.ToInt32(dr[6].ToString());
                        //lbluom1.Text = (dr[7].ToString() + " " + "Of" + " " + dr[8].ToString() + " " + dr[5].ToString());
                        lbluom2.Text = (dr[7].ToString() + " " + "Of" + " " + dr[8].ToString() + " " + dr[5].ToString());
                        lbluom3.Text = (dr[7].ToString() + " " + "Of" + " " + dr[8].ToString() + " " + dr[5].ToString());
                        lbluom4.Text = (dr[7].ToString() + " " + "Of" + " " + dr[8].ToString() + " " + dr[5].ToString());

                        //txtdispencesed.Text = Convert.ToString(totqty - (Convert.ToInt32(txtcurrentinv.Text)));
                    }
                }
            }
        }
    }

    // * Cartridge Unload and Discrepancy Insert Function * \\
    public void unloadinsert()
    {
        string reas = "";
        if (ddlreason.SelectedValue.ToString() == "-Select-")
        {
            reas = "";
        }
        else if ((ddlreason.SelectedValue.ToString() != "-Select-") && (Convert.ToInt32(txtdiscrep.Text) != 0))
        {
            reas = ddlreason.SelectedValue.ToString();
        }

        int Rtnval = pre.cartunloadinsert((Int64)ViewState["loadingid"], txtcartno.Text.Trim(), Convert.ToInt32(txtphyinventory.Text.Trim()), Convert.ToInt32(txtdiscrep.Text.Trim()), sessionuserid = Session["Userid"].ToString(), reas, location);
        if (Rtnval == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Unloaded successfully');</script>", false);
            clear();
            reason();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
        }
    }

    // * Reason reader * \\
    public void reason()
    {
        ddlreason.Items.Clear();
        ddlreason.Items.Add("-Select-");

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select Reason from Rejected_reason where status='Active' and Reason_type='Inventory Discrepancy'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddlreason.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }    

    // * Textbox Clear Function * \\
    public void clear()
    {
        //txtcartno.Text = "";
        txtcurrentinv.Text = "";
        txtdiscrep.Text = "";
        txtitemcode.Text = "";
        txtitemname.Text = "";
        txtphyinventory.Text = "";       
        lbluom2.Text = "";
        lbluom3.Text = "";
        lbluom4.Text = "";
        lbluom2.Visible = false;
        lbluom3.Visible = false;
        lbluom4.Visible = false;
    }

    //protected void btnclear_Click(object sender, EventArgs e)
    protected void btnclear_Click(object sender, ImageClickEventArgs e)
    {
        clear();
        reason();
        txtcartno.Text = "";        
    }
    
}
