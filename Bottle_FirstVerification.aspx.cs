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

public partial class Bottle_FirstVerification : System.Web.UI.Page
{
    string sessionuserid = "", location = "";
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    BDSBottle BDS = new BDSBottle();
    Preload pre = new Preload();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Userid"] != null)
        {
            sessionuserid = Session["Userid"].ToString();
            location = Session["location"].ToString();

            if (location != "")
            {
                if (!IsPostBack)
                {
                    errorcode();
                    ViewState["ord"] = "order by p.First_Verified_DT desc";
                    tdone.Visible = true;
                    tdtwo.Visible = false;
                    tdthree.Visible = true;
                    GridDisplay();
                    txtcartbarcode.Focus();
                }
            }
            else if (location == "" || location == null)
            {
                pp.Visible = false;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Workstation is not authorised to do verification');</script>", false);
                return;
            }
        }
        else
        {
            Response.Redirect("iopas.html");
        }
    }

    protected void Btngo_Click(object sender, ImageClickEventArgs e)
    {
        chkitemname.Checked = false;
        chkpacktype.Checked = false;
        chkpacksize.Checked = false;
        chkuom.Checked = false;
        chkexpiry.Checked = false;
        chkbatchno.Checked = false;
        chkboxOrPallet.Checked = false;
        chkcartonboxcount.Checked = false;
        chkbrandname.Checked = false;

        int Rtnval = BDS.BDS_FirstVerification_validate(txtcartbarcode.Text.Trim(), location);

        if (Rtnval == 1)
        {
            Fstreader();
        }
        else if (Rtnval == 4 || Rtnval == 6)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box already verified');</script>", false);
        }
        else if (Rtnval == 5)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box already rejected');</script>", false);
        }
        else if (Rtnval == 7)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box already cancelled');</script>", false);
        }
        else if (Rtnval == 8)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box transaction already closed');</script>", false);
        }
        else if (Rtnval == 9)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid Barcode');</script>", false);
            txtcartbarcode.Text = "";
            TextClear();
            errorcode();
            txtcartbarcode.Focus();  
        }
        else if (Rtnval == 10)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box transaction already rejected');</script>", false);
        }
        else if (Rtnval == 3)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Wrong pharmacy location');</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occured');</script>", false);
        }

    }
    protected void btnsave_Click(object sender, ImageClickEventArgs e)
    {
        if ((chkitemname.Checked == true) && (chkbrandname.Checked == true) && (chkpacktype.Checked == true) && (chkpacksize.Checked == true)
            && (chkuom.Checked == true) && (chkexpiry.Checked == true) && (chkbatchno.Checked == true) &&
            (chkboxOrPallet.Checked == true) && (chkcartonboxcount.Checked == true))
        {
            int Rtnval = BDS.BDS_FirstVerification_validate(txtcartbarcode.Text.Trim(), location);

            if (Rtnval == 1)
            {
                tdone.Visible = false;
                tdtwo.Visible = true;
                tdthree.Visible = false;
                btnempenter.Visible = true;
                btnempenterreject.Visible = false;
                txtempid.Text = "";
                txtempid.Focus();
            }
            else if (Rtnval == 4 || Rtnval == 6)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box already verified');</script>", false);
                txtcartbarcode.Text = "";
                txtcartbarcode.Focus();
            }
            else if (Rtnval == 5)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box already rejected');</script>", false);
                txtcartbarcode.Text = "";
                txtcartbarcode.Focus();
            }
            else if (Rtnval == 7)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box already cancelled');</script>", false);
                txtcartbarcode.Text = "";
                txtcartbarcode.Focus();
            }
            else if (Rtnval == 8)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box transaction already closed');</script>", false);
                txtcartbarcode.Text = "";
                txtcartbarcode.Focus();
            }
            else if (Rtnval == 9)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid Barcode');</script>", false);
                txtcartbarcode.Text = "";               
                TextClear();
                errorcode();
                txtcartbarcode.Focus();    
            }
            else if (Rtnval == 10)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box transaction already rejected');</script>", false);
                txtcartbarcode.Text = "";
                txtcartbarcode.Focus();
            }
            else if (Rtnval == 3)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Wrong pharmacy location');</script>", false);
                txtcartbarcode.Text = "";
                txtcartbarcode.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occured');</script>", false);
                txtcartbarcode.Text = "";
                txtcartbarcode.Focus();
            }
        }


        else if (chkitemname.Checked == false)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please check item name checkbox');</script>", false);
        }
        else if (chkbrandname.Checked == false)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please check brand name checkbox');</script>", false);
        }
        else if (chkpacktype.Checked == false)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please check pack type checkbox');</script>", false);
        }
        else if (chkpacksize.Checked == false)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please check pack size checkbox');</script>", false);
        }
        else if (chkuom.Checked == false)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please check uom checkbox');</script>", false);
        }
        else if (chkboxOrPallet.Checked == false)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please check box or pallet checkbox');</script>", false);
        }
        else if (chkcartonboxcount.Checked == false)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please check box or pallet bottle count checkbox');</script>", false);
        }
        else if (chkexpiry.Checked == false)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please check expiry date checkbox');</script>", false);
        }
        else if (chkbatchno.Checked == false)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please check batch number checkbox');</script>", false);
        }    
  
    }


    void Fstreader()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select I.Item_Code,i.Drug_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,um.Max_Cartridge_Qty,p.Carton_Box_Quantity ,isnull(p.BoxOrPallet,' ') as BoxOrPallet,convert (varchar,p.[Expiry_Date],103),p.Batch_No,p.Loaded_By,convert(varchar(20),p.LoadedDate,113) as Loaded_Dat from BDS_PreLoading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Brand_Master as b on b.BrandID=UM.Brandid left join Pharmacy as ph on ph.PharmacyID=p.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Cart_Barcode='" + txtcartbarcode.Text.Trim() + "' ";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        txtitemcode.Text = dr[0].ToString();
                        txtdrugcode.Text = dr[1].ToString();
                        txtdrugname.Text = dr[2].ToString();
                        txtbrand.Text = dr[3].ToString();
                        txtpacktype.Text = dr[4].ToString();
                        txtpacksize.Text = dr[5].ToString();
                        txtuom.Text = dr[6].ToString();
                        txtmaxcartqty.Text = dr[7].ToString();
                        txtcartonbox.Text = dr[8].ToString();
                        TxtBoxOrPallet.Text = dr[9].ToString();
                        txtexpdate.Text = dr[10].ToString();
                        txtbatchno.Text = dr[11].ToString();
                        txtlodedby.Text = dr[12].ToString();
                        txtdate.Text = dr[13].ToString();

                    }
                }
            }
        }


        lblmax.Text = txtpacktype.Text.Trim() + " " + "of" + " " + txtpacksize.Text.Trim() + " " + txtuom.Text.Trim();
        // lblload.Text = txtpacktype.Text.Trim() + " " + "of" + " " + txtpacksize.Text.Trim() + " " + txtuom.Text.Trim();
        lblcartboxof.Text = txtpacktype.Text.Trim();
    }


    // * Error Code Reader Function * \\
    public void errorcode()
    {
        ddlreason.Items.Clear();
        ddlreason.Items.Add("-Blank-");

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select Reason from Rejected_reason where status='Active' and Reason_type='Verification Rejection'";
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

    // * Text Clear * \\

    void TextClear()
    {
        
        txtitemcode.Text = "";
        txtdrugname.Text = "";
        txtdrugcode.Text = "";
        txtbrand.Text = "";
        txtpacktype.Text = "";
        txtpacksize.Text = "";
        txtuom.Text = "";
        txtmaxcartqty.Text = "";
        txtcartonbox.Text = "";
        TxtBoxOrPallet.Text = "";
        txtexpdate.Text = "";
        txtbatchno.Text = "";
        txtlodedby.Text = "";
        txtdate.Text = "";

        chkitemname.Checked = false;
        chkpacktype.Checked = false;
        chkpacksize.Checked = false;
        chkbrandname.Checked = false;
        chkuom.Checked = false;
        chkexpiry.Checked = false;
        chkbatchno.Checked = false;
        chkboxOrPallet.Checked = false;
        chkcartonboxcount.Checked = false;
    }

    protected void btnclear_Click(object sender, ImageClickEventArgs e)
    {
        txtcartbarcode.Text = "";
        TextClear();
        errorcode();
        txtcartbarcode.Focus();        
    }

    protected void btnempenter_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["EmpID"].ToString().ToLower() == txtempid.Text.Trim().ToLower())
        {
            tdone.Visible = true;
            tdtwo.Visible = false;
            tdthree.Visible = true;

            AcceptInsert();
            errorcode();
            GridDisplay();            
        }

        else if (Session["EmpID"].ToString().ToLower() != txtempid.Text.Trim().ToLower())
        {
            tdone.Visible = true;
            tdtwo.Visible = false;
            tdthree.Visible = true;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('You are not authorised to do Verification. NRIC / Staff ID/ Pass Mismatch');</script>", false);
        }
    } 


    protected void btnupdate_Click(object sender, ImageClickEventArgs e)
    {
        int Rtnval = BDS.BDS_FirstVerification_validate(txtcartbarcode.Text.Trim(), location);

        if (Rtnval == 1)
        {
            tdone.Visible = false;
            tdtwo.Visible = true;
            tdthree.Visible = false;
            btnempenter.Visible = false;
            btnempenterreject.Visible = true;            
            txtempid.Text = "";
            txtempid.Focus();
        }
        else if (Rtnval == 4 || Rtnval == 6)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box already verified');</script>", false);
            txtcartbarcode.Text = "";
            txtcartbarcode.Focus();
        }
        else if (Rtnval == 5)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box already rejected');</script>", false);
            txtcartbarcode.Text = "";
            txtcartbarcode.Focus();
        }
        else if (Rtnval == 7)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box already cancelled');</script>", false);
            txtcartbarcode.Text = "";
            txtcartbarcode.Focus();
        }
        else if (Rtnval == 8)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box transaction already closed');</script>", false);
            txtcartbarcode.Text = "";
            txtcartbarcode.Focus();
        }
        else if (Rtnval == 9)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid Barcode');</script>", false);
            txtcartbarcode.Text = "";
            TextClear();
            errorcode();
            txtcartbarcode.Focus();  
        }
        else if (Rtnval == 10)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box transaction already rejected');</script>", false);
            txtcartbarcode.Text = "";
            txtcartbarcode.Focus();
        }
        else if (Rtnval == 3)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Wrong pharmacy location');</script>", false);
            txtcartbarcode.Text = "";
            txtcartbarcode.Focus();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occured');</script>", false);
            txtcartbarcode.Text = "";
            txtcartbarcode.Focus();
        }
    }


    protected void btnempenterreject_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["EmpID"].ToString().ToLower() == txtempid.Text.Trim().ToLower())
        {
            tdone.Visible = true;
            tdtwo.Visible = false;
            tdthree.Visible = true;

            RejectInsert();
            errorcode();
            GridDisplay();
        }

        else if (Session["EmpID"].ToString().ToLower() != txtempid.Text.Trim().ToLower())
        {
            tdone.Visible = true;
            tdtwo.Visible = false;
            tdthree.Visible = true;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('You are not authorised to do Verification. NRIC / Staff ID/ Pass Mismatch');</script>", false);
        }
    }



    void AcceptInsert()
    {
        int Rtnval = BDS.BDS_First_Verification_Accept(txtcartbarcode.Text.Trim(), Session["Userid"].ToString(), location);

        if (Rtnval == 1)
        {
            TextClear();
            //fstverifigrid();
            txtcartbarcode.Text = "";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton / Pallet verified');</script>", false);
        }
        else if (Rtnval == 3)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Loader and first verifier must be different');</script>", false);
        }
        else if (Rtnval == 4 || Rtnval == 6)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box already verified');</script>", false);
        }
        else if (Rtnval == 5)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box already rejected');</script>", false);
        }
        else if (Rtnval == 7)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box already cancelled');</script>", false);
        }
        else if (Rtnval == 8)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box transaction already closed');</script>", false);
        }
        else if (Rtnval == 9)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid Barcode');</script>", false);
        }
        else if (Rtnval == 3)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Wrong pharmacy location');</script>", false);
        }
        else if (Rtnval == 10)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box transaction already rejected');</script>", false);
        }
        else if (Rtnval == 2)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Not verified');</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occured');</script>", false);

        }

    }

    void RejectInsert()
    {
        int Rtnval = BDS.BDS_First_Verification_Reject(txtcartbarcode.Text.Trim(), Session["Userid"].ToString(), location, ddlreason.SelectedItem.ToString());

        if (Rtnval == 1)
        {
            TextClear();            
            txtcartbarcode.Text = "";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton / Pallet rejected');</script>", false);
        }
        else if (Rtnval == 3)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Loader and first verifier must be different');</script>", false);
        }
        else if (Rtnval == 4 || Rtnval == 6)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box already verified');</script>", false);
        }
        else if (Rtnval == 5)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box already rejected');</script>", false);
        }
        else if (Rtnval == 7)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box already cancelled');</script>", false);
        }
        else if (Rtnval == 8)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box transaction already closed');</script>", false);
        }
        else if (Rtnval == 9)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid Barcode');</script>", false);
        }
        else if (Rtnval == 3)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Wrong pharmacy location');</script>", false);
        }
        else if (Rtnval == 2)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Not verified');</script>", false);
        }
        else if (Rtnval == 10)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box transaction already rejected');</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occured');</script>", false);

        }

    }

    void GridDisplay()
    {
        try
        {
            BDS.BDS_FstVerified_grid(GridFstVerified, sessionuserid, location, ViewState["ord"].ToString());
            DataSet dsData = GridFstVerified.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(GridFstVerified.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(GridFstVerified.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.ForeColor = Color.Black;
            lblpge.Font.Bold = false;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = Color.Green;
            }
        }
        catch (Exception ex)
        {
        }
    }
}