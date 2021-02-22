using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Datalayer;
using System.Drawing;

public partial class Drugmaster : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    Drug drg = new Drug();
    string sessionuserid = "", Actstatus = "", location = "",CartActStatus="";
    int Rtnvalue;
    bool _ChkHRip = false; 
    bool _ChkVRip = false;
    bool _Divider = false;
    bool _Interleaf = false;
    bool _RotationFlag = false;
    bool _RtnCart = false;
    bool _RtnPreLoad = false;
    bool _RtnPreLoad_Exact = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (scrip1.IsInAsyncPostBack)
        {
            Page.Trace.IsEnabled = false;
        }
        try
        {            
            sessionuserid = Session["Userid"].ToString();
            location = Session["location"].ToString();
            
            if (!IsPostBack)
            {                
                ViewState["pharname"] = "";
                txtpacksize.Attributes.Add("onkeyup", "Test('" + txtlength.ClientID + "','" + txtitemnameadd.ClientID + "',event)");
                txtlength.Attributes.Add("onkeyup", "Test('" + txtpacksize.ClientID + "','" + txtheight.ClientID + "',event)");
                txtheight.Attributes.Add("onkeyup", "Test('" + txtlength.ClientID + "','" + txtwidth.ClientID + "',event)");
                txtwidth.Attributes.Add("onkeyup", "Test('" + txtheight.ClientID + "','" + txtmaxdds.ClientID + "',event)");               
                txtmaxdds.Attributes.Add("onkeyup", "Test('" + txtwidth.ClientID + "','" + txtsmallbag.ClientID + "',event)");
                txtsmallbag.Attributes.Add("onkeyup", "Test('" + txtmaxdds.ClientID + "','" + txtmedium.ClientID + "',event)");
                txtmedium.Attributes.Add("onkeyup", "Test('" + txtsmallbag.ClientID + "','" + txtlarge.ClientID + "',event)");
                txtlarge.Attributes.Add("onkeyup", "Test('" + txtmedium.ClientID + "','" + txtmaxcontainer.ClientID + "',event)");
                txtmaxcontainer.Attributes.Add("onkeyup", "Test('" + txtlarge.ClientID + "','" + chkactive.ClientID + "',event)");

                chkactive.Attributes.Add("onkeyup", "Test('" + txtmaxcontainer.ClientID + "','" + chkhrip.ClientID + "',event)");
                chkhrip.Attributes.Add("onkeyup", "Test('" + chkactive.ClientID + "','" + chkvrip.ClientID + "',event)");
                chkvrip.Attributes.Add("onkeyup", "Test('" + chkhrip.ClientID + "','" + txt_Car_type.ClientID + "',event)");
                txt_Car_type.Attributes.Add("onkeyup", "Test('" + chkvrip.ClientID + "','" + txtcar_length.ClientID + "',event)");

                txtcar_length.Attributes.Add("onkeyup", "Test('" + txt_Car_type.ClientID + "','" + txtcar_height.ClientID + "',event)");
                txtcar_height.Attributes.Add("onkeyup", "Test('" + txtcar_length.ClientID + "','" + txtcar_width.ClientID + "',event)");
                txtcar_width.Attributes.Add("onkeyup", "Test('" + txtcar_height.ClientID + "','" + txtcar_X_Offset.ClientID + "',event)");

                txtcar_X_Offset.Attributes.Add("onkeyup", "Test('" + txtcar_width.ClientID + "','" + txtcar_Y_Offset.ClientID + "',event)");
                txtcar_Y_Offset.Attributes.Add("onkeyup", "Test('" + txtcar_X_Offset.ClientID + "','" + txtcar_Z_offset.ClientID + "',event)");
                txtcar_Z_offset.Attributes.Add("onkeyup", "Test('" + txtcar_Y_Offset.ClientID + "','" + txtcar_X_Pitch.ClientID + "',event)");

                txtcar_X_Pitch.Attributes.Add("onkeyup", "Test('" + txtcar_Z_offset.ClientID + "','" + txtcar_Y_Pitch.ClientID + "',event)");
                txtcar_Y_Pitch.Attributes.Add("onkeyup", "Test('" + txtcar_X_Pitch.ClientID + "','" + txtcar_Z_Pitch.ClientID + "',event)");
                txtcar_Z_Pitch.Attributes.Add("onkeyup", "Test('" + txtcar_Y_Pitch.ClientID + "','" + txtcar_Row.ClientID + "',event)");

               
                txtcar_Row.Attributes.Add("onkeyup", "Test('" + txtcar_Z_Pitch.ClientID + "','" + txtcar_Column.ClientID + "',event)");
                txtcar_Column.Attributes.Add("onkeyup", "Test('" + txtcar_Row.ClientID + "','" + ddlcar_layer.ClientID + "',event)");

                ddlcar_layer.Attributes.Add("onkeyup", "Test('" + txtcar_Column.ClientID + "','" + Chkdivider.ClientID + "',event)");

                Chkdivider.Attributes.Add("onkeyup", "Test('" + ddlcar_layer.ClientID + "','" + Chklinterleaf.ClientID + "',event)");
                Chklinterleaf.Attributes.Add("onkeyup", "Test('" + Chkdivider.ClientID + "','" + gridpharloc.ClientID + "',event)");

                gridpharloc.Attributes.Add("onkeyup", "Test('" + Chklinterleaf.ClientID + "','" + btnclear.ClientID + "',event)");
                btnclear.Attributes.Add("onkeyup", "Test('" + gridpharloc.ClientID + "','" + btnsubmit.ClientID + "',event)");
                btnsubmit.Attributes.Add("onkeyup", "Test('" + btnclear.ClientID + "','" + btnupdate .ClientID + "',event)");
                btnupdate.Attributes.Add("onkeyup", "Test('" + btnsubmit.ClientID + "','" + btnupdate.ClientID + "',event)");

                txtitemcode.Attributes.Add("onKeyPress", "doClick(event)");
                txtdrugcode.Attributes.Add("onKeyPress", "doClick(event)");
                txtitemnameadd.Attributes.Add("onKeyPress", "doClick(event)");
                btnsearch.Attributes.Add("onclick", "itemsearch() ;return false;");
                btnbrand.Attributes.Add("onclick", "brand(); return false;");

                txtpacksize.Attributes["onkeypress"] = "return Intchecktwo(event);";
                txtsmallbag.Attributes["onkeypress"] = "return Intchecktwo(event);";
                txtlarge.Attributes["onkeypress"] = "return Intchecktwo(event);";
                txtmaxdds.Attributes["onkeypress"] = "return Intchecktwo(event);";

                txtlength.Attributes["onkeypress"] = "return Intcheck(event);";
                txtheight.Attributes["onkeypress"] = "return Intcheck(event);";
                txtwidth.Attributes["onkeypress"] = "return Intcheck(event);";
               


                txt_Car_type.Attributes["onkeypress"] = "return Intchecktwo(event);";
                txtcar_height.Attributes["onkeypress"] = "return Intcheck(event);";
                txtcar_width.Attributes["onkeypress"] = "return Intcheck(event);";
                txtcar_X_Offset.Attributes["onkeypress"] = "return Intcheck(event);";
                txtcar_Y_Offset.Attributes["onkeypress"] = "return Intcheck(event);";
                txtcar_Z_offset.Attributes["onkeypress"] = "return Intcheck(event);";
                txtcar_X_Pitch.Attributes["onkeypress"] = "return Intcheck(event);";
                txtcar_Y_Pitch.Attributes["onkeypress"] = "return Intcheck(event);";
                txtcar_Z_Pitch.Attributes["onkeypress"] = "return Intcheck(event);";
             
                txtcar_Row.Attributes["onkeypress"] = "return Intchecktwo(event);";
                txtcar_Column.Attributes["onkeypress"] = "return Intchecktwo(event);";               

                locationdisplay();                     
                editvisiblefalse();
                txtitemcode.Focus();

                btncartupdate.Visible = false;
                Btnonlybottleupdate.Visible = false;
                btnupdate.Visible = false;
                btnsubmit.Visible = true;
                btnaddnewcarton.Visible = false;
                btncartonsave.Visible = false;
                DDLCartonType.Visible = false;
                lblddlcarttype.Visible = false;
                CartlayerAdd();
                LoadingMasterBottlePitchReader();
                lblpacktypeinfo.Text = "";
            } 
        }
        catch (NullReferenceException)
        {
            //Response.Redirect("opas.html?Session=End");
            Response.Redirect("iopas.html");
        }
    }

    void Application_EndRequest(object sender, EventArgs e)
    {
        Response.Cookies.Add(new HttpCookie("Test", "Test"));
    }

    protected void btnok_Click(object sender, EventArgs e)
    {
        HideBottle_Carton_Update_Enabled();
        DDLCartonType.Visible = false;
        lblddlcarttype.Visible = false;
        txtpacksize.Text = "";
        txtlength.Text = "";
        txtheight.Text = "";
        txtwidth.Text = "";      
        txtmaxcart.Text = "";
        txtmaxdds.Text = "";
        txtsmallbag.Text = "";
        txtlarge.Text = "";
        txtmedium.Text = "";
        txtmaxcontainer.Text = "";
        chkactive.Checked = false;       
        
        txtmaxcontainer.Text = "";
        lblcontainer.Visible = true;
        txtmaxcontainer.Visible = true;        
        chkhrip.Checked = false;
        chkvrip.Checked = false;
        Chkdivider.Checked = false;
        Chklinterleaf.Checked = false;
        chkrotation.Checked = false;

        txt_Car_type.Text = "";
        txtcar_height.Text = "";
        txtcar_width.Text = "";
        txtcar_X_Offset.Text = "";
        txtcar_Y_Offset.Text = "";
        txtcar_Z_offset.Text = "";
        txtcar_X_Pitch.Text = "";
        txtcar_Y_Pitch.Text = "";
        txtcar_Z_Pitch.Text = "";
        //txtcar_layer.Text = "";
        txtcar_Row.Text = "";
        txtcar_Column.Text = "";
        txtcar_length.Text = "";
        CartlayerAdd();


        btnaddnewcarton.Visible = false;
        btncartupdate.Visible = false;
        btnupdate.Visible = false;
        btnsubmit.Visible = true;
        Btnonlybottleupdate.Visible = false;

        ViewState["pharname"] = "";
        locationdisplay();
        lblpacktypeinfo.Text = "";

        brandreader();
        uomread();
        itemmastervalue();
        gridedit.PageIndex = 0;
        useraddgrid();
        packtyperead();

        if (ddltype.SelectedValue.ToString().ToLower() == "Bundle".ToLower())
        {
            HideBunddle();
            lblpacktypeinfo.Text = " For HDDS";
        }
        else if (ddltype.SelectedValue.ToString().ToLower() == "Strip".ToLower())
        {
            HideStrips();
            lblpacktypeinfo.Text = " For HDDS";
        }
        else if ((ddltype.SelectedValue.ToString().ToLower() == "Bottle".ToLower()) || (ddltype.SelectedValue.ToString().ToLower() == "Box-Bottle".ToLower()))
        {
            HideBottle();
            txtmaxcontainer.Text = "";
            lblpacktypeinfo.Text = " For BDS";
            BoxOrPallet();
        }
        else if (ddltype.SelectedValue.ToString().ToLower() == "Box-Strip".ToLower()) 
        {
            HideBox();
            txtmaxcontainer.Text = "";
            lblpacktypeinfo.Text = " For HDDS";
        }    


        if (ddlbrand.SelectedItem != null)
        {
            mfrcodereader();
        }

        try
        {
            lbluom1.Text = ddltype.SelectedValue.ToString() + " " + "of" + " " + txtpacksize.Text + " " + ddluom.Text;
            lbluom2.Text = ddltype.SelectedValue.ToString() + " " + "of" + " " + txtpacksize.Text + " " + ddluom.Text;
            lblpatype.Text = ddltype.SelectedValue.ToString();
            lblsptype.Text = ddltype.SelectedValue.ToString();
            lblmetype.Text = ddltype.SelectedValue.ToString();
            lblmaxcont.Text = ddltype.SelectedValue.ToString();
            lblmaxbbbdsuom.Text = ddltype.SelectedValue.ToString();
        }
        catch (NullReferenceException)
        {

        }
    }

    protected void btnuom_Click(object sender, EventArgs e)
    {
        try
        {
            lbluom1.Text = ddltype.SelectedValue.ToString() + " " + "of" + " " + txtpacksize.Text + " " + ddluom.Text;
            lbluom2.Text = ddltype.SelectedValue.ToString() + " " + "of" + " " + txtpacksize.Text + " " + ddluom.Text;
            txtlength.Focus();
        }
        catch (NullReferenceException)
        {
        }
    }

    

    // * MFR Barcode Reader * \\
    public void mfrcodereader()
    {
        ddlbarcode.Items.Clear();

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select m.Mfrbarcode from MFR_Barcode as m left join Brand_Master as b on b.BrandID=m.Brandid left join Item_Master as i on i.MasterID=m.Masterid where i.Item_Code='" + txtitemcode.Text.Trim() + "' and b.Brandname='" + ddlbrand.SelectedItem.ToString() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddlbarcode.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }

    // * Brand Name Reader * \\
    public void brandreader()
    {
        ddlbrand.Items.Clear();
        ddlbarcode.Items.Clear();

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select ba.Defaultbrand,b.Brandname from  Brand_Allot as ba left join Brand_Master as b on b.BrandID= ba.brandid left join Item_Master as i on i.MasterID=ba.Masterid where i.Item_Code='" + txtitemcode.Text.Trim() + "'  and ba.Status='Active'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr[0].ToString() == "1")
                        {
                            ddlbrand.Items.Insert(0, new ListItem(dr[1].ToString()));
                        }
                        else
                        {
                            ddlbrand.Items.Add(dr[1].ToString());
                        }
                    }
                }
            }
        }
    }

    // * Loading Master Bottle Pitch Reader * \\
    public void LoadingMasterBottlePitchReader()
    {

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select Bottle_Pitch from Loading_Master as l left join Pharmacy as p on p.PharmacyID=l.PharmacyID where p.Location_Name='" + location + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtbottlepitch.Text = Convert.ToString(dr[0]);
                    }
                }
            }
        }
    }

    protected void btnnew_Click(object sender, EventArgs e)
    {
        locationdisplay();
        gridedit.Visible = false;
        editvisiblefalse();
        textclearone();
        textcleartwo();
    }


    protected void btnclear_Click(object sender, ImageClickEventArgs e)
    {
        HideBottle_Carton_Update_Enabled();
        ViewState["pharname"] = "";
        editvisiblefalse();
        textclearone();
        textcleartwo();
        gridedit.PageIndex = 0;
        useraddgrid();
        locationdisplay();
    }

    protected void Btndel_Click(object sender, EventArgs e)
    {
        gridedit.Visible = true;
        useraddgrid();
    }

    protected void btnedit_Click(object sender, EventArgs e)
    {
        gridedit.Visible = true;
        useraddgrid();
    }

    // * Pharmacy location Name Display * \\
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

    // * Item Master Value Reader Based On search *\\
    public void itemmastervalue()
    {
  
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select convert(varchar,[Item_Effective_date_From],103),convert(varchar,[Item_Effective_date_To],103) from Item_Master where Item_Code='" + txtitemcode.Text.Trim() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txteffectivefrom.Text = dr[0].ToString();
                        txteffto.Text = dr[1].ToString();
                    }
                }
            }
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
                        ddluom.Text = (dr[0].ToString());
                    }
                }
            }
        }
    }


    // * Pack type Reader Function * \\
    public void packtyperead()
    {
        ddltype.Items.Clear();     

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select Packtype from Packtype_master";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    ddltype.Items.Add("-Select-");
                    while (dr.Read())
                    {
                        ddltype.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }

    // * Item User Master Insert Function * \\
    public void usermasterinsert()
    {
        string Condition="";
        if ((ddltype.SelectedValue.ToString().ToLower() == "BOX-STRIP".ToLower()) || (ddltype.SelectedValue.ToString().ToLower() == "BUNDLE".ToLower()))
        {
            if (Convert.ToDecimal(txtwidth.Text.Trim()) >= 15)
            {
                Condition = "YES";
            }
            else
            {
                Condition = "NO";
                Rtnvalue = 11;
            }
        }
        else
        {
            Condition = "YES";
        }

        if ((ddltype.SelectedValue.ToString().ToLower() == "BOTTLE".ToLower()) || (ddltype.SelectedValue.ToString().ToLower() == "BOX-BOTTLE".ToLower()))
        {
            if (Convert.ToInt32(txt_Car_type.Text) == (Convert.ToInt32(txtcar_Row.Text)) * (Convert.ToInt32(txtcar_Column.Text)) * (Convert.ToInt32(ddlcar_layer.SelectedValue.ToString())))
            {
                Condition = "YES";
            }
            else
            {
                Condition = "NO";
                Rtnvalue = 12;
            }
        }

        if (Condition == "YES")
        {

            txtmaxcart.Text = txtcalcvalue.Text;
            int a = 0;

            for (int i = 0; i < gridpharloc.Rows.Count; i++)
            {
                GridViewRow row = gridpharloc.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkphar")).Checked;

                if (isChecked)
                {
                    if (ViewState["pharname"].ToString().ToLower() != (gridpharloc.Rows[i].Cells[0].Text.ToLower()))
                    {
                        Activestatus();
                        //if (ddltype.SelectedValue.ToString().ToLower() == "BOTTLE".ToLower())
                        //{
                        //    txtmaxcontainer.Text = "0";
                        //}

                        if (chkhrip.Checked == true)
                        {
                            _ChkHRip = true;
                        }
                        else if (chkhrip.Checked == false)
                        {
                            _ChkHRip = false;
                        }

                        if (chkvrip.Checked == true)
                        {
                            _ChkVRip = true;
                        }
                        else if (chkvrip.Checked == false)
                        {
                            _ChkVRip = false;
                        }

                        if (Chkdivider.Checked == true)
                        {
                            _Divider = true;
                        }
                        else if (Chkdivider.Checked == false)
                        {
                            _Divider = false;
                        }

                        if (Chklinterleaf.Checked == true)
                        {
                            _Interleaf = true;
                        }
                        else if (Chklinterleaf.Checked == false)
                        {
                            _Interleaf = false;
                        }

                        if (chkrotation.Checked == true)
                        {
                            _RotationFlag = true;
                        }
                        else if (chkrotation.Checked == false)
                        {
                            _RotationFlag = false;
                        }

                        if (ChkCartActive.Checked == true)
                        {
                            CartActStatus = "Yes";
                        }
                        else if (ChkCartActive.Checked == false)
                        {
                            CartActStatus = "No";
                        }


                        txtmaxcart.Text = txtcalcvalue.Text;
                        Rtnvalue = drg.usermasterinsert(txtitemcode.Text.Trim(), (gridpharloc.Rows[i].Cells[0].Text), ddluom.Text, ddlbrand.SelectedValue.ToString(),
                                   ddltype.SelectedValue.ToString(), txtpacksize.Text.Trim(), Convert.ToDecimal(txtlength.Text.Trim()), Convert.ToDecimal(txtheight.Text.Trim()), Convert.ToDecimal(txtwidth.Text.Trim()),
                                   Convert.ToInt32(txtmaxcart.Text.Trim()), Convert.ToInt32(txtmaxdds.Text.Trim()), Convert.ToInt32(txtsmallbag.Text.Trim()),
                                   Convert.ToInt32(txtmedium.Text.Trim()), Convert.ToInt32(txtlarge.Text.Trim()), Actstatus, sessionuserid = Session["Userid"].ToString(),
                                   Convert.ToInt32(txtmaxcontainer.Text), _ChkHRip, _ChkVRip,DDLBoxOrPallet.SelectedValue.ToString(), Convert.ToInt32(txt_Car_type.Text), Convert.ToDecimal(txtcar_length.Text), Convert.ToDecimal(txtcar_height.Text),
                                   Convert.ToDecimal(txtcar_width.Text), Convert.ToDecimal(txtcar_X_Offset.Text), Convert.ToDecimal(txtcar_Y_Offset.Text),
                                   Convert.ToDecimal(txtcar_Z_offset.Text), Convert.ToDecimal(txtcar_X_Pitch.Text), Convert.ToDecimal(txtcar_Y_Pitch.Text),
                                   Convert.ToDecimal(txtcar_Z_Pitch.Text), Convert.ToInt32(ddlcar_layer.SelectedValue.ToString()), Convert.ToInt32(txtcar_Row.Text),
                                   Convert.ToInt32(txtcar_Column.Text), _Divider, _Interleaf, _RotationFlag, CartActStatus, Convert.ToInt32(txtmaxbbbds.Text));


                        a++;
                        if (Rtnvalue == 2 || Rtnvalue == 3 || Rtnvalue == 4)
                        {
                            return;
                        }
                    }
                    else if (ViewState["pharname"].ToString().ToLower() == (gridpharloc.Rows[i].Cells[0].Text.ToLower()))
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
       
    }

    // * Item User Master Insert Function * \\
    public void UserMasterCartonInsert()
    {
        string Condition = "";
        if ((ddltype.SelectedValue.ToString().ToLower() == "BOX-STRIP".ToLower()) || (ddltype.SelectedValue.ToString().ToLower() == "BUNDLE".ToLower()))
        {
            if (Convert.ToDecimal(txtwidth.Text.Trim()) >= 15)
            {
                Condition = "YES";
            }
            else
            {
                Condition = "NO";
                Rtnvalue = 11;
            }
        }
        else
        {
            Condition = "YES";
        }

        if ((ddltype.SelectedValue.ToString().ToLower() == "BOTTLE".ToLower()) || (ddltype.SelectedValue.ToString().ToLower() == "BOX-BOTTLE".ToLower()))
        {
            if (Convert.ToInt32(txt_Car_type.Text) == (Convert.ToInt32(txtcar_Row.Text)) * (Convert.ToInt32(txtcar_Column.Text)) * (Convert.ToInt32(ddlcar_layer.SelectedValue.ToString())))
            {
                Condition = "YES";
            }
            else
            {
                Condition = "NO";
                Rtnvalue = 12;
            }
        }

        if (Condition == "YES")
        {
            if (chkhrip.Checked == true)
            {
                _ChkHRip = true;
            }
            else if (chkhrip.Checked == false)
            {
                _ChkHRip = false;
            }

            if (chkvrip.Checked == true)
            {
                _ChkVRip = true;
            }
            else if (chkvrip.Checked == false)
            {
                _ChkVRip = false;
            }

            if (Chkdivider.Checked == true)
            {
                _Divider = true;
            }
            else if (Chkdivider.Checked == false)
            {
                _Divider = false;
            }

            if (Chklinterleaf.Checked == true)
            {
                _Interleaf = true;
            }
            else if (Chklinterleaf.Checked == false)
            {
                _Interleaf = false;
            }

            if (chkrotation.Checked == true)
            {
                _RotationFlag = true;
            }
            else if (chkrotation.Checked == false)
            {
                _RotationFlag = false;
            }


            if (ChkCartActive.Checked == true)
            {
                CartActStatus="Yes";
            }
            else if (ChkCartActive.Checked == false)
            {
                CartActStatus = "No";
            }



            txtmaxcart.Text = txtcalcvalue.Text;
            Rtnvalue = drg.User_Master_Carton_Insert((int)ViewState["idno"], DDLBoxOrPallet.SelectedValue.ToString(), Convert.ToInt32(txt_Car_type.Text), Convert.ToDecimal(txtcar_length.Text), Convert.ToDecimal(txtcar_height.Text),
                       Convert.ToDecimal(txtcar_width.Text), Convert.ToDecimal(txtcar_X_Offset.Text), Convert.ToDecimal(txtcar_Y_Offset.Text),
                       Convert.ToDecimal(txtcar_Z_offset.Text), Convert.ToDecimal(txtcar_X_Pitch.Text), Convert.ToDecimal(txtcar_Y_Pitch.Text),
                       Convert.ToDecimal(txtcar_Z_Pitch.Text), Convert.ToInt32(ddlcar_layer.SelectedValue.ToString()), Convert.ToInt32(txtcar_Row.Text),
                       Convert.ToInt32(txtcar_Column.Text), _Divider, _Interleaf, sessionuserid = Session["Userid"].ToString(), CartActStatus);

        }

    }


    // * Item User Master Update Function * \\
    public void usermasterupdate()
    {

        string Condition="";
        if ((ddltype.SelectedValue.ToString().ToLower() == "BOX-STRIP".ToLower()) || (ddltype.SelectedValue.ToString().ToLower() == "BUNDLE".ToLower()))
        {           
            if (Convert.ToDecimal(txtwidth.Text.Trim()) >= 15)
            {
                Condition = "YES";
            }
            else
            {
                Condition = "NO";
                Rtnvalue = 11;
            }
        }
        else
        {
            Condition = "YES";
        }

        if ((ddltype.SelectedValue.ToString().ToLower() == "BOTTLE".ToLower()) || (ddltype.SelectedValue.ToString().ToLower() == "BOX-BOTTLE".ToLower()))
        {
            if (Convert.ToInt32(txt_Car_type.Text) == (Convert.ToInt32(txtcar_Row.Text)) * (Convert.ToInt32(txtcar_Column.Text))*(Convert.ToInt32(ddlcar_layer.SelectedValue.ToString())))
            {
                Condition = "YES";
            }
            else
            {
                Condition = "NO";
                Rtnvalue = 12;
            }
        }
      

        if (Condition == "YES")
        {
            txtmaxcart.Text = txtcalcvalue.Text;
            int a = 0;
            for (int i = 0; i < gridpharloc.Rows.Count; i++)
            {
                GridViewRow row = gridpharloc.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkphar")).Checked;
                if (isChecked)
                {
                    Activestatus();
                    //   old   drg.usermasterupdate(idno, (gridpharloc.Rows[i].Cells[0].Text), txtediuom.Text, txtbrand.Text, txteditpacktype.Text, txtpacksize.Text, txtmfrcode.Text, txtlength.Text, txtheight.Text, txtwidth.Text, txtweight.Text, Convert.ToInt32(txtmaxcart.Text), Convert.ToInt32(txtmaxdds.Text), sessionuserid = Session["Userid"].ToString());
                    ////Rtnvalue = drg.usermasterupdate(idno,txtitemcode.Text.Trim(),(gridpharloc.Rows[i].Cells[0].Text),ddluom.SelectedValue.ToString(),ddlbrand.SelectedValue.ToString(),ddltype.SelectedValue.ToString(), txtpacksize.Text.Trim(), txtlength.Text.Trim(), txtheight.Text.Trim(), txtwidth.Text.Trim(), txtweight.Text.Trim(), Convert.ToInt32(txtmaxcart.Text.Trim()), Convert.ToInt32(txtmaxdds.Text.Trim()), Convert.ToInt32(txtsmallbag.Text.Trim()), Convert.ToInt32(txtlarge.Text.Trim()), Actstatus, sessionuserid = Session["Userid"].ToString());
                    ////if (Rtnvalue == 2 || Rtnvalue == 3)
                    ////{
                    ////    return;  
                    ////}
                    a++;
                }
            }
            if (a == 1)
            {
                for (int i = 0; i < gridpharloc.Rows.Count; i++)
                {
                    GridViewRow row = gridpharloc.Rows[i];
                    bool isChecked = ((CheckBox)row.FindControl("chkphar")).Checked;
                    if (isChecked)
                    {
                        if (ViewState["pharname"].ToString().ToLower() == (gridpharloc.Rows[i].Cells[0].Text.ToLower()))
                        {
                            Activestatus();
                            if ((ddltype.SelectedValue.ToString().ToLower() == "STRIP".ToLower()))
                            {
                                txtmaxcontainer.Text = "0";
                            }
                            if (chkhrip.Checked == true)
                            {
                                _ChkHRip = true;
                            }
                            else if (chkhrip.Checked == false)
                            {
                                _ChkHRip = false;
                            }

                            if (chkvrip.Checked == true)
                            {
                                _ChkVRip = true;
                            }
                            else if (chkvrip.Checked == false)
                            {
                                _ChkVRip = false;
                            }

                            if (Chkdivider.Checked == true)
                            {
                                _Divider = true;
                            }
                            else if (Chkdivider.Checked == false)
                            {
                                _Divider = false;
                            }

                            if (Chklinterleaf.Checked == true)
                            {
                                _Interleaf = true;
                            }
                            else if (Chklinterleaf.Checked == false)
                            {
                                _Interleaf = false;
                            }

                            if (chkrotation.Checked == true)
                            {
                                _RotationFlag = true;
                            }
                            else if (chkrotation.Checked == false)
                            {
                                _RotationFlag = false;
                            }

                            if (ChkCartActive.Checked == true)
                            {
                                CartActStatus = "Yes";
                            }
                            else if (ChkCartActive.Checked == false)
                            {
                                CartActStatus = "No";
                            }

                            txtmaxcart.Text = txtcalcvalue.Text;
                            int OLDCtype = 0;
                            if ((DDLCartonType.SelectedValue.ToString() != "-Select-") && (ddltype.SelectedValue.ToString().ToLower() == "BOTTLE".ToLower()) ||
                                (ddltype.SelectedValue.ToString().ToLower() == "BOX-BOTTLE".ToLower()))
                            {
                                OLDCtype = Convert.ToInt32(DDLCartonType.SelectedValue.ToString());
                            }
                            else
                            {
                                OLDCtype = 0;
                            }

                           
                            Rtnvalue = drg.usermasterupdate((int)ViewState["idno"], txtitemcode.Text.Trim(), (gridpharloc.Rows[i].Cells[0].Text), ddluom.Text,
                                        ddlbrand.SelectedValue.ToString(), ddltype.SelectedValue.ToString(), txtpacksize.Text.Trim(), Convert.ToDecimal(txtlength.Text.Trim()),
                                        Convert.ToDecimal(txtheight.Text.Trim()), Convert.ToDecimal(txtwidth.Text.Trim()), Convert.ToInt32(txtmaxcart.Text.Trim()), Convert.ToInt32(txtmaxdds.Text.Trim()),
                                        Convert.ToInt32(txtsmallbag.Text.Trim()), Convert.ToInt32(txtmedium.Text.Trim()), Convert.ToInt32(txtlarge.Text.Trim()), Actstatus,
                                        sessionuserid = Session["Userid"].ToString(), Convert.ToInt32(txtmaxcontainer.Text), _ChkHRip, _ChkVRip, DDLBoxOrPallet.SelectedValue.ToString(), 
                                        Convert.ToInt32(txt_Car_type.Text),Convert.ToDecimal(txtcar_length.Text), Convert.ToDecimal(txtcar_height.Text), Convert.ToDecimal(txtcar_width.Text),
                                        Convert.ToDecimal(txtcar_X_Offset.Text),Convert.ToDecimal(txtcar_Y_Offset.Text), Convert.ToDecimal(txtcar_Z_offset.Text), 
                                        Convert.ToDecimal(txtcar_X_Pitch.Text),Convert.ToDecimal(txtcar_Y_Pitch.Text), Convert.ToDecimal(txtcar_Z_Pitch.Text),
                                        Convert.ToInt32(ddlcar_layer.SelectedValue.ToString()),
                                        Convert.ToInt32(txtcar_Row.Text), Convert.ToInt32(txtcar_Column.Text), _Divider, _Interleaf, _RotationFlag, OLDCtype, CartActStatus,
                                        Convert.ToInt32(txtmaxbbbds.Text));

                            if (Rtnvalue == 2 || Rtnvalue == 3 || Rtnvalue == 5)
                            {
                                return;
                            }
                        }
                        else if (ViewState["pharname"].ToString().ToLower() != (gridpharloc.Rows[i].Cells[0].Text.ToLower()))
                        {
                            Rtnvalue = 4;
                        }
                    }
                }
            }
            else if (a > 1)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Only one pharmacy location selection is allowed');</script>", false);
            }
            else if (a == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Select pharmacy location');</script>", false);
            }
        }                
    }


    // * Item User Master Save Button * \\
    
    protected void btnsubmit_Click(object sender, ImageClickEventArgs e)
    {

        if (txtsmallbag.Text.Trim() != "" && txtlarge.Text.Trim() != "" && txtmedium.Text.Trim() != "")
        {
            if (Convert.ToDecimal(txtwidth.Text.Trim()) <= Convert.ToDecimal(txtheight.Text.Trim()))
            {
                if (Convert.ToDecimal(txtheight.Text.Trim()) <= Convert.ToDecimal(txtlength.Text.Trim()))
                {
                    if (Convert.ToInt32(txtsmallbag.Text.Trim()) <= Convert.ToInt32(txtmedium.Text.Trim()))
                    {
                        if (Convert.ToInt32(txtmedium.Text.Trim()) <= Convert.ToInt32(txtlarge.Text.Trim()))
                        {
                            if (Convert.ToInt32(txtlarge.Text.Trim()) <= Convert.ToInt32(txtmaxcontainer.Text.Trim()))
                            {
                                Calculate();
                                usermasterinsert();

                                if (Rtnvalue == 1)
                                {
                                    gridedit.PageIndex = 0;
                                    useraddgrid();
                                    locationdisplay();
                                    textclearone();
                                    textcleartwo();
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record created');</script>", false);
                                }
                                else if (Rtnvalue == 2)
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Pack type and pack size already exist in this item code');</script>", false);
                                }
                                else if (Rtnvalue == 3)
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This UOM does not belong to this pharmacy');</script>", false);
                                }
                                else if (Rtnvalue == 4)
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Item code and brand name Max No. of Box per container value mismatch of existing pack size value.');</script>", false);
                                }
                                else if (Rtnvalue == 11)
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('L3 should be minimum 15mm.');</script>", false);
                                }
                                else if (Rtnvalue == 12)
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton box/Pallet bottle count not matched with multiplication of X and Y count');</script>", false);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occurred');</script>", false);
                                }                               

                            }
                            else
                            {
                                if (ddltype.SelectedValue.ToString().ToLower() != "Bundle".ToLower())
                                {
                                    Calculate();
                                    usermasterinsert();
                                    if (Rtnvalue == 1)
                                    {
                                        gridedit.PageIndex = 0;
                                        useraddgrid();
                                        locationdisplay();
                                        textclearone();
                                        textcleartwo();
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record created');</script>", false);
                                    }
                                    else if (Rtnvalue == 2)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Pack type and pack size already exist in this item code');</script>", false);
                                    }
                                    else if (Rtnvalue == 3)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This UOM does not belong to this pharmacy');</script>", false);
                                    }
                                    else if (Rtnvalue == 4)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Item code and brand name Max No. of Box per container value mismatch of existing pack size value.');</script>", false);
                                    }
                                    else if (Rtnvalue == 11)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('L3 should be minimum 15mm.');</script>", false);
                                    }
                                    else if (Rtnvalue == 12)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton box/Pallet bottle count not matched with multiplication of X and Y count');</script>", false);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occurred');</script>", false);
                                    }

                                }
                                else if (ddltype.SelectedValue.ToString().ToLower() == "Bundle".ToLower())
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Max No. of Bundle per Container equal or higher than Large bag quantity ');</script>", false);
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Large bag quantity equal or higher than medium bag quantity ');</script>", false);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Medium bag quantity equal or higher than small bag quantity ');</script>", false);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('L2 equal or lower than L1 value ');</script>", false);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('L3 equal or lower than L2 value ');</script>", false);
            }
        }
    }

    protected void btnupdate_Click(object sender, ImageClickEventArgs e)
    {
        if (txtsmallbag.Text.Trim() != "" && txtlarge.Text.Trim() != "" && txtmedium.Text.Trim() != "")
        {
            if (Convert.ToDecimal(txtwidth.Text.Trim()) <= Convert.ToDecimal(txtheight.Text.Trim()))
            {
                if (Convert.ToDecimal(txtheight.Text.Trim()) <= Convert.ToDecimal(txtlength.Text.Trim()))
                {
                    if (Convert.ToInt32(txtsmallbag.Text.Trim()) <= Convert.ToInt32(txtmedium.Text.Trim()))
                    {
                        if (Convert.ToInt32(txtmedium.Text.Trim()) <= Convert.ToInt32(txtlarge.Text.Trim()))
                        {
                            if (Convert.ToInt32(txtlarge.Text.Trim()) <= Convert.ToInt32(txtmaxcontainer.Text.Trim()))
                            {
                                Calculate();
                                usermasterupdate();
                                if (Rtnvalue == 1)
                                {
                                    gridedit.PageIndex = 0;
                                    useraddgrid();
                                    locationdisplay();
                                    textclearone();
                                    textcleartwo();
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated');</script>", false);
                                }
                                else if (Rtnvalue == 2)
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Pack type and pack size already exist in this item code');</script>", false);
                                }
                                else if (Rtnvalue == 3)
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This UOM does not belong to this pharmacy');</script>", false);
                                }
                                else if (Rtnvalue == 4)
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Wrong Pharmacy location');</script>", false);
                                }
                                else if (Rtnvalue == 5)
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Minimum alert quantity per DDS/BDS and Maximum Bottle / Box-Bottle for BDS only updated, because Item is running in DDS/BDS. Disable and remove cartridge from DDS/BDS and try again');</script>", false);
                                    gridedit.PageIndex = 0;
                                    useraddgrid();
                                    locationdisplay();
                                    textclearone();
                                    textcleartwo();
                                    HideBottle_Carton_Update_Enabled();
                                }
                                else if (Rtnvalue == 6)
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Minimum alert quantity per DDS/BDS and Maximum Bottle / Box-Bottle for BDS only updated, because BDS Bottle/Box Preloading transaction started. Cancel the Bottle/Box transaction and try again');</script>", false);
                                    gridedit.PageIndex = 0;
                                    useraddgrid();
                                    locationdisplay();
                                    textclearone();
                                    textcleartwo();
                                    HideBottle_Carton_Update_Enabled();
                                }
                                else if (Rtnvalue == 11)
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('L3 should be minimum 15mm.');</script>", false);
                                }
                                else if (Rtnvalue == 12)
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton box/Pallet bottle count not matched with multiplication of X and Y count');</script>", false);
                                }
                                else if (Rtnvalue == 16)
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Drug Information updated. Carton box info already exist.');</script>", false);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occurred');</script>", false);
                                }

                            }
                            else
                            {
                                if (ddltype.SelectedValue.ToString().ToLower() != "Bundle".ToLower())
                                {
                                    Calculate();
                                    usermasterupdate();
                                    if (Rtnvalue == 1)
                                    {
                                        gridedit.PageIndex = 0;
                                        useraddgrid();
                                        locationdisplay();
                                        textclearone();
                                        textcleartwo();
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated');</script>", false);
                                    }
                                    else if (Rtnvalue == 2)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Pack type and pack size already exist in this item code');</script>", false);
                                    }
                                    else if (Rtnvalue == 3)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This UOM does not belong to this pharmacy');</script>", false);
                                    }
                                    else if (Rtnvalue == 4)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Wrong Pharmacy location');</script>", false);
                                    }
                                    else if (Rtnvalue == 5)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Minimum alert quantity per DDS/BDS and Maximum Bottle / Box-Bottle for BDS only updated, because Item is running in DDS/BDS. Disable and remove cartridge from DDS/BDS and try again');</script>", false);
                                    }
                                    else if (Rtnvalue == 6)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('BDS Bottle/Box Preloading transaction started. Cancel the Bottle/Box transaction and try again');</script>", false);
                                    }
                                    else if (Rtnvalue == 11)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('L3 should be minimum 15mm.');</script>", false);
                                    }
                                    else if (Rtnvalue == 12)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton box/Pallet bottle count not matched with multiplication of X and Y count');</script>", false);
                                    }
                                    else if (Rtnvalue == 16)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Drug Information updated. Carton box info already exist.');</script>", false);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occurred');</script>", false);
                                    }

                                }
                                else if (ddltype.SelectedValue.ToString().ToLower() == "Bundle".ToLower())
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Max No. of Bundle per Container equal or higher than Large bag quantity ');</script>", false);
                                }
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Large bag quantity equal or higher than medium bag quantity ');</script>", false);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Medium bag quantity equal or higher than small bag quantity ');</script>", false);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('L2 equal or lower than L1 value ');</script>", false);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('L3 equal or lower than L2 value ');</script>", false);
            }
        }
    }

    
   
    // * User maste GridDisplay For Add * \\
    public void useraddgrid()
    {
        if (location == "" || location == null)
        {
            drg.usergridadd(gridedit, txtitemcode.Text);
            DataSet dsData = gridedit.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(gridedit.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridedit.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpge.ForeColor = Color.Black;
            lblpge.Font.Bold = false;
            if (dtData.Rows.Count == 0)
            {
                lblpge.Text = "No Record Found";
                lblpge.Font.Bold = true;
                lblpge.ForeColor = Color.Green;
            }
        }
        else if (location != "")
        {
            drg.usergriddefaultlocation(gridedit, txtitemcode.Text,location);
            DataSet dsData = gridedit.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(gridedit.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridedit.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
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

    // * Multiple Check box Selection for Enabled and disabled * \\
    protected void chkheader_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gridedit.Rows)
        {
            bool isChecked = ((CheckBox)(gridedit.HeaderRow.FindControl("chkheader"))).Checked;

            if (isChecked)
            {
                CheckBox cb = ((CheckBox)(gvr.FindControl("chkaccess")));
                cb.Checked = true;
            }
            else if (isChecked == false)
            {
                CheckBox cb = ((CheckBox)(gvr.FindControl("chkaccess")));
                cb.Checked = false;
            }
        }
    }

    // * Textbox Clear Function * \\
    public void textclearone()
    {
        txteffectivefrom.Text = "";
        txteffto.Text = "";
        ddluom.Text="";
        txtpacksize.Text = "";
        txtlength.Text = "";
        txtheight.Text = "";
        txtwidth.Text = "";
        //txtweight.Text = "";
        txtmaxcart.Text = "";
        txtmaxdds.Text = "";
        txtsmallbag.Text = "";
        txtlarge.Text = "";
        txtmedium.Text = "";
        txtmaxbbbds.Text = "";
        ddlbarcode.Items.Clear();
        ddlbrand.Items.Clear();
        ddluom.Text="";
        ddltype.Items.Clear();
        btncartupdate.Visible = false;
        btnaddnewcarton.Visible = false;
        DDLCartonType.Visible = false;
        lblddlcarttype.Visible = false;
        Btnonlybottleupdate.Visible = false;
        btncartonsave.Visible = false;
        txt_Car_type.Visible = true;
        btnupdate.Visible = false;
        btnsubmit.Visible = true;
        chkactive.Checked = false;
        txtmaxcontainer.Text = "";
        lblcontainer.Visible = true;
        txtmaxcontainer.Visible = true;
       // btnapply.Visible = false;
        chkhrip.Checked = false;
        chkvrip.Checked = false;
        Chkdivider.Checked = false;
        Chklinterleaf.Checked = false;
        chkrotation.Checked = false;
        ChkCartActive.Checked = false;

        lblmaxbbbds.Enabled = true;
        txtmaxbbbds.Enabled = true;
        lblmaxbbbdsuom.Enabled = true;

        txt_Car_type.Text = "";
        txtcar_height.Text = "";
        txtcar_width.Text = "";
        txtcar_X_Offset.Text = "";
        txtcar_Y_Offset.Text = "";
        txtcar_Z_offset.Text = "";
        txtcar_X_Pitch.Text = "";
        txtcar_Y_Pitch.Text = "";
        txtcar_Z_Pitch.Text = "";
        //txtcar_layer.Text = "";
        txtcar_Row.Text = "";
        txtcar_Column.Text = "";
        txtcar_length.Text = "";
        CartlayerAdd();
        ddlbrand.Enabled = true;

        DDLBoxOrPallet.Items.Clear();
        DDLCartonType.Items.Clear();
    }

    // * Textbox Clear Function * \\
    public void textcleartwo()
    {
        txtitemnameadd.Text = "";
        txtitemcode.Text = "";
        txtdrugcode.Text = "";
    }

    protected void gridedit_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        btnaddnewcarton.Visible = false;
        btncartonsave.Visible = false;
        btncartupdate.Visible = false;
        Btnonlybottleupdate.Visible = false;
        btncartupdate.Visible = false;
        DDLBoxOrPallet.Items.Clear();
        DDLCartonType.Items.Clear();     

        txt_Car_type.Text = "";
        txtcar_height.Text = "";
        txtcar_width.Text = "";
        txtcar_X_Offset.Text = "";
        txtcar_Y_Offset.Text = "";
        txtcar_Z_offset.Text = "";
        txtcar_X_Pitch.Text = "";
        txtcar_Y_Pitch.Text = "";
        txtcar_Z_Pitch.Text = "";        
        txtcar_Row.Text = "";
        txtcar_Column.Text = "";
        txtcar_length.Text = "";
        Chkdivider.Checked = false;
        Chklinterleaf.Checked = false;
        ChkCartActive.Checked = false;
        //chkrotation.Checked = false;


        ViewState["idno"] = "0";
        ViewState["idno"] = (int)gridedit.DataKeys[e.NewSelectedIndex].Value;

        ViewState["pharname"] = "";
        ViewState["pharname"] = (gridedit.Rows[e.NewSelectedIndex].Cells[3].Text);

        if ((CartridgeStatusCheck((int)gridedit.DataKeys[e.NewSelectedIndex].Value)) && (BDSPreloadStatusCheck((int)gridedit.DataKeys[e.NewSelectedIndex].Value)))
        {
            if (BrandStatusCheck((int)gridedit.DataKeys[e.NewSelectedIndex].Value))
            {
                LoadingMasterBottlePitchReader();
                
                txtmaxcontainer.Text = "";        
            
                locationdisplay();
                packtyperead();
                ddltype.Items.Remove(gridedit.Rows[e.NewSelectedIndex].Cells[4].Text);
                ddltype.Items.Insert(0, new ListItem(gridedit.Rows[e.NewSelectedIndex].Cells[4].Text));

                if (gridedit.Rows[e.NewSelectedIndex].Cells[7].Text == "Yes")
                {
                    chkactive.Checked = true;
                }
                else if (gridedit.Rows[e.NewSelectedIndex].Cells[7].Text == "No")
                {
                    chkactive.Checked = false;
                }

                using (SqlConnection con = DBCon.getstring())
                {
                    string Commt = "select u.pack_size,IL.uom,u.L1,u.L2,u.L3,u.Max_Cartridge_Qty,u.Max_Alert_Qty_DDS,u.Small_bag,u.Large_bag,b.brandname," +
                                          "convert(varchar,i.[Item_Effective_date_From],103),convert(varchar,i.[Item_Effective_date_To],103),i.Item_Code,i.Drug_Code,i.Item_Name,u.Medium_bag," +
                                          "u.Max_Box_Container,u.Horizontal_Rip,u.Vertical_Rip,u.Bot_RotationFlag,u.Max_Box_Bottle_BDS " +
                                          "from Item_user_Master as u left join Item_Master as i on i.MasterID=u.MasterID left join Item_Location as IL on IL.MasterID=i.MasterID " +
                                          "and IL.PharmacyID=u.PharmacyID left join Brand_Master as b on b.BrandID=u.Brandid where u.ID='" + (int)ViewState["idno"] + "'";
                    cmd = new SqlCommand(Commt, con);

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                txtpacksize.Text = dr[0].ToString();
                                txtlength.Text = Convert.ToString(dr[2]);
                                txtheight.Text = Convert.ToString(dr[3]);
                                txtwidth.Text = Convert.ToString(dr[4]);
                                txtmaxcart.Text = dr[5].ToString();
                                txtcalcvalue.Text = dr[5].ToString();
                                txtmaxdds.Text = dr[6].ToString();
                                txtsmallbag.Text = dr[7].ToString();
                                txtlarge.Text = dr[8].ToString();
                                brandreader();
                                ddlbrand.Items.Remove(dr[9].ToString());
                                ddlbrand.Items.Insert(0, new ListItem(dr[9].ToString()));
                                txteffectivefrom.Text = dr[10].ToString();
                                txteffto.Text = dr[11].ToString();
                                txtitemcode.Text = dr[12].ToString();
                                txtdrugcode.Text = dr[13].ToString();
                                txtitemnameadd.Text = dr[14].ToString();
                                txtmedium.Text = dr[15].ToString();
                                txtmaxcontainer.Text = dr[16].ToString();

                                if (Convert.ToBoolean(dr[17]) == true)
                                {
                                    chkhrip.Checked = true;
                                }
                                else if (Convert.ToBoolean(dr[17]) == false)
                                {
                                    chkhrip.Checked = false;
                                }

                                if (Convert.ToBoolean(dr[18]) == true)
                                {
                                    chkvrip.Checked = true;
                                }
                                else if (Convert.ToBoolean(dr[18]) == false)
                                {
                                    chkvrip.Checked = false;
                                }

                                if (Convert.ToBoolean(dr[19]) == true)
                                {
                                    chkrotation.Checked = true;
                                }
                                else if (Convert.ToBoolean(dr[19]) == false)
                                {
                                    chkrotation.Checked = false;
                                }

                                txtmaxbbbds.Text = dr[20].ToString();

                                uomread();
                            }
                        }
                    }
                }

                
                DDLCartonType.Visible = false;
                lblddlcarttype.Visible = false;
                Btnonlybottleupdate.Visible = true;

                if ((ddltype.SelectedValue.ToString().ToLower() == "Bottle".ToLower()) || (ddltype.SelectedValue.ToString().ToLower() == "Box-Bottle".ToLower()))
                {
                    DDLCartonType.Visible = true;
                    lblddlcarttype.Visible = true;
                    btnaddnewcarton.Visible = true;
                    btnupdate.Visible = false;
                    BoxOrPallet();
                }

                mfrcodereader();
                
                btncartupdate.Visible = false;
                btnsubmit.Visible = false;
                
                lblpacktypeinfo.Text = "";

                if (ddltype.SelectedValue.ToString().ToLower() == "Bundle".ToLower())
                {
                    HideBunddle();
                    lblpacktypeinfo.Text = " For HDDS";
                }
                else if (ddltype.SelectedValue.ToString().ToLower() == "Strip".ToLower())
                {
                    HideStrips();
                    lblpacktypeinfo.Text = " For HDDS";
                }
                else if ((ddltype.SelectedValue.ToString().ToLower() == "Bottle".ToLower()) || (ddltype.SelectedValue.ToString().ToLower() == "Box-Bottle".ToLower()))
                {
                    HideBottle();
                    lblpacktypeinfo.Text = " For BDS";
                }
                else if (ddltype.SelectedValue.ToString().ToLower() == "Box-Strip".ToLower())
                {
                    HideBox();
                    lblpacktypeinfo.Text = " For HDDS";
                }                

                try
                {
                    lbluom1.Text = ddltype.SelectedValue.ToString() + " " + "of" + " " + txtpacksize.Text + " " + ddluom.Text;
                    lbluom2.Text = ddltype.SelectedValue.ToString() + " " + "of" + " " + txtpacksize.Text + " " + ddluom.Text;
                }
                catch (NullReferenceException)
                {
                }
                lblpatype.Text = ddltype.SelectedValue.ToString();
                lblsptype.Text = ddltype.SelectedValue.ToString();
                lblmetype.Text = ddltype.SelectedValue.ToString();
                lblmaxcont.Text = ddltype.SelectedValue.ToString();
                lblmaxbbbdsuom.Text = ddltype.SelectedValue.ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Brand is inactivated you cannot edit this item ');</script>", false);
            }
        }
        else if ((BDSPreloadStatusCheck((int)gridedit.DataKeys[e.NewSelectedIndex].Value)) && (gridedit.Rows[e.NewSelectedIndex].Cells[4].Text.ToLower() == "BOTTLE".ToLower()) || (gridedit.Rows[e.NewSelectedIndex].Cells[4].Text.ToLower() == "BOX-BOTTLE".ToLower()))
        {

            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Item is currently running in BDS. System will allow to you edit carton box parameter only.');</script>", false);

            LoadingMasterBottlePitchReader();

            txtmaxcontainer.Text = "";

            locationdisplay();
            packtyperead();
            ddltype.Items.Remove(gridedit.Rows[e.NewSelectedIndex].Cells[4].Text);
            ddltype.Items.Insert(0, new ListItem(gridedit.Rows[e.NewSelectedIndex].Cells[4].Text));

            if (gridedit.Rows[e.NewSelectedIndex].Cells[7].Text == "Yes")
            {
                chkactive.Checked = true;
            }
            else if (gridedit.Rows[e.NewSelectedIndex].Cells[7].Text == "No")
            {
                chkactive.Checked = false;
            }



            using (SqlConnection con = DBCon.getstring())
            {
                string Commt = "select u.pack_size,IL.uom,u.L1,u.L2,u.L3,u.Max_Cartridge_Qty,u.Max_Alert_Qty_DDS,u.Small_bag,u.Large_bag,b.brandname," +
                                      "convert(varchar,i.[Item_Effective_date_From],103),convert(varchar,i.[Item_Effective_date_To],103),i.Item_Code,i.Drug_Code,i.Item_Name,u.Medium_bag," +
                                      "u.Max_Box_Container,u.Horizontal_Rip,u.Vertical_Rip,u.Bot_RotationFlag,u.Max_Box_Bottle_BDS " +
                                      "from Item_user_Master as u left join Item_Master as i on i.MasterID=u.MasterID left join Item_Location as IL on IL.MasterID=i.MasterID " +
                                      "and IL.PharmacyID=u.PharmacyID left join Brand_Master as b on b.BrandID=u.Brandid where u.ID='" + (int)ViewState["idno"] + "'";
                cmd = new SqlCommand(Commt, con);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            txtpacksize.Text = dr[0].ToString();
                            txtlength.Text = Convert.ToString(dr[2]);
                            txtheight.Text = Convert.ToString(dr[3]);
                            txtwidth.Text = Convert.ToString(dr[4]);
                            txtmaxcart.Text = dr[5].ToString();
                            txtcalcvalue.Text = dr[5].ToString();
                            txtmaxdds.Text = dr[6].ToString();
                            txtsmallbag.Text = dr[7].ToString();
                            txtlarge.Text = dr[8].ToString();
                            brandreader();
                            ddlbrand.Items.Remove(dr[9].ToString());
                            ddlbrand.Items.Insert(0, new ListItem(dr[9].ToString()));
                            txteffectivefrom.Text = dr[10].ToString();
                            txteffto.Text = dr[11].ToString();
                            txtitemcode.Text = dr[12].ToString();
                            txtdrugcode.Text = dr[13].ToString();
                            txtitemnameadd.Text = dr[14].ToString();
                            txtmedium.Text = dr[15].ToString();
                            txtmaxcontainer.Text = dr[16].ToString();
                            if (Convert.ToBoolean(dr[17]) == true)
                            {
                                chkhrip.Checked = true;
                            }
                            else if (Convert.ToBoolean(dr[17]) == false)
                            {
                                chkhrip.Checked = false;
                            }

                            if (Convert.ToBoolean(dr[18]) == true)
                            {
                                chkvrip.Checked = true;
                            }
                            else if (Convert.ToBoolean(dr[18]) == false)
                            {
                                chkvrip.Checked = false;
                            }

                            if (Convert.ToBoolean(dr[19]) == true)
                            {
                                chkrotation.Checked = true;
                            }
                            else if (Convert.ToBoolean(dr[19]) == false)
                            {
                                chkrotation.Checked = false;
                            }
                            txtmaxbbbds.Text = dr[20].ToString();
                            uomread();

                        }
                    }
                }
            }

            
            DDLCartonType.Visible = false;
            lblddlcarttype.Visible = false;
            Btnonlybottleupdate.Visible = true;

            if ((ddltype.SelectedValue.ToString().ToLower() == "Bottle".ToLower()) || (ddltype.SelectedValue.ToString().ToLower() == "Box-Bottle".ToLower()))
            {
                lblddlcarttype.Visible = true;
                DDLCartonType.Visible = true;
                btnaddnewcarton.Visible = true;
                btnupdate.Visible = false;
                btncartupdate.Visible = false;
                BoxOrPallet();
            }

            mfrcodereader();
            
            btnupdate.Visible = false;
            btnsubmit.Visible = false;
            

            lblpacktypeinfo.Text = "";
            if (ddltype.SelectedValue.ToString().ToLower() == "Bundle".ToLower())
            {
                HideBunddle();
                lblpacktypeinfo.Text = " For HDDS";
            }
            else if (ddltype.SelectedValue.ToString().ToLower() == "Strip".ToLower())
            {
                HideStrips();
                lblpacktypeinfo.Text = " For HDDS";
            }
            else if ((ddltype.SelectedValue.ToString().ToLower() == "Bottle".ToLower()) || (ddltype.SelectedValue.ToString().ToLower() == "Box-Bottle".ToLower()))
            {
                HideBottle();
                lblpacktypeinfo.Text = " For BDS";
            }
            else if (ddltype.SelectedValue.ToString().ToLower() == "Box-Strip".ToLower())
            {
                HideBox();
                lblpacktypeinfo.Text = " For HDDS";
            }

            HideBottle_Carton_Update_Disabled();

            try
            {
                lbluom1.Text = ddltype.SelectedValue.ToString() + " " + "of" + " " + txtpacksize.Text + " " + ddluom.Text;
                lbluom2.Text = ddltype.SelectedValue.ToString() + " " + "of" + " " + txtpacksize.Text + " " + ddluom.Text;
            }
            catch (NullReferenceException)
            {
            }
            lblpatype.Text = ddltype.SelectedValue.ToString();
            lblsptype.Text = ddltype.SelectedValue.ToString();
            lblmetype.Text = ddltype.SelectedValue.ToString();
            lblmaxcont.Text = ddltype.SelectedValue.ToString();
            lblmaxbbbdsuom.Text = ddltype.SelectedValue.ToString();
        }
        else if (!_RtnPreLoad)
        {
            ClearCart();
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('BDS Bottle/Box Preloading transaction started. Cancel the Bottle/Box transaction and try again');</script>", false);

            LoadingMasterBottlePitchReader();

            txtmaxcontainer.Text = "";

            locationdisplay();
            packtyperead();
            ddltype.Items.Remove(gridedit.Rows[e.NewSelectedIndex].Cells[4].Text);
            ddltype.Items.Insert(0, new ListItem(gridedit.Rows[e.NewSelectedIndex].Cells[4].Text));

            if (gridedit.Rows[e.NewSelectedIndex].Cells[7].Text == "Yes")
            {
                chkactive.Checked = true;
            }
            else if (gridedit.Rows[e.NewSelectedIndex].Cells[7].Text == "No")
            {
                chkactive.Checked = false;
            }


            using (SqlConnection con = DBCon.getstring())
            {
                string Commt = "select u.pack_size,IL.uom,u.L1,u.L2,u.L3,u.Max_Cartridge_Qty,u.Max_Alert_Qty_DDS,u.Small_bag,u.Large_bag,b.brandname," +
                                      "convert(varchar,i.[Item_Effective_date_From],103),convert(varchar,i.[Item_Effective_date_To],103),i.Item_Code,i.Drug_Code,i.Item_Name,u.Medium_bag," +
                                      "u.Max_Box_Container,u.Horizontal_Rip,u.Vertical_Rip,u.Bot_RotationFlag,u.Max_Box_Bottle_BDS " +
                                      "from Item_user_Master as u left join Item_Master as i on i.MasterID=u.MasterID left join Item_Location as IL on IL.MasterID=i.MasterID " +
                                      "and IL.PharmacyID=u.PharmacyID left join Brand_Master as b on b.BrandID=u.Brandid where u.ID='" + (int)ViewState["idno"] + "'";
                cmd = new SqlCommand(Commt, con);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            txtpacksize.Text = dr[0].ToString();
                            txtlength.Text = Convert.ToString(dr[2]);
                            txtheight.Text = Convert.ToString(dr[3]);
                            txtwidth.Text = Convert.ToString(dr[4]);
                            txtmaxcart.Text = dr[5].ToString();
                            txtcalcvalue.Text = dr[5].ToString();
                            txtmaxdds.Text = dr[6].ToString();
                            txtsmallbag.Text = dr[7].ToString();
                            txtlarge.Text = dr[8].ToString();
                            brandreader();
                            ddlbrand.Items.Remove(dr[9].ToString());
                            ddlbrand.Items.Insert(0, new ListItem(dr[9].ToString()));
                            txteffectivefrom.Text = dr[10].ToString();
                            txteffto.Text = dr[11].ToString();
                            txtitemcode.Text = dr[12].ToString();
                            txtdrugcode.Text = dr[13].ToString();
                            txtitemnameadd.Text = dr[14].ToString();
                            txtmedium.Text = dr[15].ToString();
                            txtmaxcontainer.Text = dr[16].ToString();
                            if (Convert.ToBoolean(dr[17]) == true)
                            {
                                chkhrip.Checked = true;
                            }
                            else if (Convert.ToBoolean(dr[17]) == false)
                            {
                                chkhrip.Checked = false;
                            }

                            if (Convert.ToBoolean(dr[18]) == true)
                            {
                                chkvrip.Checked = true;
                            }
                            else if (Convert.ToBoolean(dr[18]) == false)
                            {
                                chkvrip.Checked = false;
                            }

                            if (Convert.ToBoolean(dr[19]) == true)
                            {
                                chkrotation.Checked = true;
                            }
                            else if (Convert.ToBoolean(dr[19]) == false)
                            {
                                chkrotation.Checked = false;
                            }
                            txtmaxbbbds.Text = dr[20].ToString();
                            uomread();

                        }
                    }
                }
            }



            lblddlcarttype.Visible = false;
            DDLCartonType.Visible = false;
            Btnonlybottleupdate.Visible = true;
            if ((ddltype.SelectedValue.ToString().ToLower() == "Bottle".ToLower()) || (ddltype.SelectedValue.ToString().ToLower() == "Box-Bottle".ToLower()))
            {
                lblddlcarttype.Visible = true;
                DDLCartonType.Visible = true;
                btnaddnewcarton.Visible = true;                
                btnupdate.Visible = false;
                btncartupdate.Visible = false;
                BoxOrPallet();
               
            }


            mfrcodereader();
          
            btnsubmit.Visible = false;
            

            lblpacktypeinfo.Text = "";

            if (ddltype.SelectedValue.ToString().ToLower() == "Bundle".ToLower())
            {
                HideBunddle();
                lblpacktypeinfo.Text = " For HDDS";
            }
            else if (ddltype.SelectedValue.ToString().ToLower() == "Strip".ToLower())
            {
                HideStrips();
                lblpacktypeinfo.Text = " For HDDS";
            }
            else if ((ddltype.SelectedValue.ToString().ToLower() == "Bottle".ToLower()) || (ddltype.SelectedValue.ToString().ToLower() == "Box-Bottle".ToLower()))
            {
                HideBottle();
                lblpacktypeinfo.Text = " For BDS";
                btnaddnewcarton.Visible = true;
            }
            else if (ddltype.SelectedValue.ToString().ToLower() == "Box-Strip".ToLower())
            {
                HideBox();
                lblpacktypeinfo.Text = " For HDDS";
            }

            HideBottle_Carton_Update_Disabled();
            //HideBottle_Carton_Update_Disabled2();

            try
            {
                lbluom1.Text = ddltype.SelectedValue.ToString() + " " + "of" + " " + txtpacksize.Text + " " + ddluom.Text;
                lbluom2.Text = ddltype.SelectedValue.ToString() + " " + "of" + " " + txtpacksize.Text + " " + ddluom.Text;
            }
            catch (NullReferenceException)
            {
            }
            lblpatype.Text = ddltype.SelectedValue.ToString();
            lblsptype.Text = ddltype.SelectedValue.ToString();
            lblmetype.Text = ddltype.SelectedValue.ToString();
            lblmaxcont.Text = ddltype.SelectedValue.ToString();
            lblmaxbbbdsuom.Text = ddltype.SelectedValue.ToString();
        }

        else if (!_RtnCart)
        {
            ClearCart();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Item is running in DDS/BDS. Disable and remove cartridge from DDS/BDS and try again');</script>", false);

            LoadingMasterBottlePitchReader();
            txtmaxcontainer.Text = "";       
      
            locationdisplay();
            packtyperead();
            ddltype.Items.Remove(gridedit.Rows[e.NewSelectedIndex].Cells[4].Text);
            ddltype.Items.Insert(0, new ListItem(gridedit.Rows[e.NewSelectedIndex].Cells[4].Text));

            if (gridedit.Rows[e.NewSelectedIndex].Cells[7].Text == "Yes")
            {
                chkactive.Checked = true;
            }
            else if (gridedit.Rows[e.NewSelectedIndex].Cells[7].Text == "No")
            {
                chkactive.Checked = false;
            }



            using (SqlConnection con = DBCon.getstring())
            {
                string Commt = "select u.pack_size,IL.uom,u.L1,u.L2,u.L3,u.Max_Cartridge_Qty,u.Max_Alert_Qty_DDS,u.Small_bag,u.Large_bag,b.brandname," +
                                      "convert(varchar,i.[Item_Effective_date_From],103),convert(varchar,i.[Item_Effective_date_To],103),i.Item_Code,i.Drug_Code,i.Item_Name,u.Medium_bag," +
                                      "u.Max_Box_Container,u.Horizontal_Rip,u.Vertical_Rip,u.Bot_RotationFlag,u.Max_Box_Bottle_BDS " +
                                      "from Item_user_Master as u left join Item_Master as i on i.MasterID=u.MasterID left join Item_Location as IL on IL.MasterID=i.MasterID " +
                                      "and IL.PharmacyID=u.PharmacyID left join Brand_Master as b on b.BrandID=u.Brandid where u.ID='" + (int)ViewState["idno"] + "'";
                cmd = new SqlCommand(Commt, con);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            txtpacksize.Text = dr[0].ToString();
                            txtlength.Text = Convert.ToString(dr[2]);
                            txtheight.Text = Convert.ToString(dr[3]);
                            txtwidth.Text = Convert.ToString(dr[4]);
                            txtmaxcart.Text = dr[5].ToString();
                            txtcalcvalue.Text = dr[5].ToString();
                            txtmaxdds.Text = dr[6].ToString();
                            txtsmallbag.Text = dr[7].ToString();
                            txtlarge.Text = dr[8].ToString();
                            brandreader();
                            ddlbrand.Items.Remove(dr[9].ToString());
                            ddlbrand.Items.Insert(0, new ListItem(dr[9].ToString()));
                            txteffectivefrom.Text = dr[10].ToString();
                            txteffto.Text = dr[11].ToString();
                            txtitemcode.Text = dr[12].ToString();
                            txtdrugcode.Text = dr[13].ToString();
                            txtitemnameadd.Text = dr[14].ToString();
                            txtmedium.Text = dr[15].ToString();
                            txtmaxcontainer.Text = dr[16].ToString();
                            if (Convert.ToBoolean(dr[17]) == true)
                            {
                                chkhrip.Checked = true;
                            }
                            else if (Convert.ToBoolean(dr[17]) == false)
                            {
                                chkhrip.Checked = false;
                            }

                            if (Convert.ToBoolean(dr[18]) == true)
                            {
                                chkvrip.Checked = true;
                            }
                            else if (Convert.ToBoolean(dr[18]) == false)
                            {
                                chkvrip.Checked = false;
                            }

                            if (Convert.ToBoolean(dr[19]) == true)
                            {
                                chkrotation.Checked = true;
                            }
                            else if (Convert.ToBoolean(dr[19]) == false)
                            {
                                chkrotation.Checked = false;
                            }
                            txtmaxbbbds.Text = dr[20].ToString();
                            uomread();

                        }
                    }
                }
            }

            lblddlcarttype.Visible = false;
            DDLCartonType.Visible = false;
            Btnonlybottleupdate.Visible = true;

            if ((ddltype.SelectedValue.ToString().ToLower() == "Bottle".ToLower()) || (ddltype.SelectedValue.ToString().ToLower() == "Box-Bottle".ToLower()))
            {
                lblddlcarttype.Visible = true;
                DDLCartonType.Visible = true;
                btnaddnewcarton.Visible = true;                
                btnupdate.Visible = false;
                btncartupdate.Visible = false;
                BoxOrPallet();
            }

            mfrcodereader();
            
            btnsubmit.Visible = false;
            lblpacktypeinfo.Text = "";

            if (ddltype.SelectedValue.ToString().ToLower() == "Bundle".ToLower())
            {
                HideBunddle();
                lblpacktypeinfo.Text = " For HDDS";
            }
            else if (ddltype.SelectedValue.ToString().ToLower() == "Strip".ToLower())
            {
                HideStrips();
                lblpacktypeinfo.Text = " For HDDS";
            }
            else if ((ddltype.SelectedValue.ToString().ToLower() == "Bottle".ToLower()) || (ddltype.SelectedValue.ToString().ToLower() == "Box-Bottle".ToLower()))
            {
                btnaddnewcarton.Visible = true;
                HideBottle();
                lblpacktypeinfo.Text = " For BDS";
            }
            else if (ddltype.SelectedValue.ToString().ToLower() == "Box-Strip".ToLower())
            {
                HideBox();
                lblpacktypeinfo.Text = " For HDDS";
            }

            HideBottle_Carton_Update_Disabled();
            HideBottle_Carton_Update_Disabled2();

            try
            {
                lbluom1.Text = ddltype.SelectedValue.ToString() + " " + "of" + " " + txtpacksize.Text + " " + ddluom.Text;
                lbluom2.Text = ddltype.SelectedValue.ToString() + " " + "of" + " " + txtpacksize.Text + " " + ddluom.Text;
            }
            catch (NullReferenceException)
            {
            }

            lblpatype.Text = ddltype.SelectedValue.ToString();
            lblsptype.Text = ddltype.SelectedValue.ToString();
            lblmetype.Text = ddltype.SelectedValue.ToString();
            lblmaxcont.Text = ddltype.SelectedValue.ToString();
            lblmaxbbbdsuom.Text = ddltype.SelectedValue.ToString();
        }    

    }

    // * BRAND STATUS CHECK WHILE EDITING IN DRUG MASTER * \\

    public bool BrandStatusCheck(int ID)
    {
        bool Rtn=false;
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select ba.Status from Item_user_Master as u left join Item_Master as i on i.MasterID=u.MasterID  " +
                                  "left join Brand_Master as b on b.BrandID=u.Brandid " +
                                  "left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.BrandID=u.Brandid  and ba.MasterID=i.MasterID" +
                                  " where u.ID='" + ID + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        if (dr[0].ToString() == "Active")
                        {
                            Rtn = true;
                        }
                        else if (dr[0].ToString() == "Inactive")
                        {
                            Rtn = false;
                        }
                    }
                }
            }
        }


        return Rtn;
    }

    public bool CartridgeStatusCheck(int ID)
    {
        _RtnCart = false;
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select COUNT(distinct IUM_ID) from Cartridge_Loading where Activation_Status=5 and Inventory_Status=2 and IUM_ID='" + ID + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        if (Convert.ToInt32(dr[0].ToString()) == 0)
                        {
                            _RtnCart = true;
                        }
                        else if (Convert.ToInt32(dr[0].ToString()) == 1)
                        {
                            _RtnCart = false;
                        }
                    }
                }
            }
        }

        return _RtnCart;
    }

    public bool BDSPreloadStatusCheck(int ID)
    {        
       _RtnPreLoad = false;

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select COUNT(distinct IUM_ID) from BDS_PreLoading where IUM_ID='" + ID + "' and Status<>'Close' and Status<>'Cancel'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        if (Convert.ToInt32(dr[0].ToString()) == 0)
                        {
                            _RtnPreLoad = true;
                        }
                        else if (Convert.ToInt32(dr[0].ToString()) == 1)
                        {
                            _RtnPreLoad = false;
                        }
                    }
                }
            }
        }

        return _RtnPreLoad;
    }

    public bool BDSPreload_Status_Exact_Check(int ID,int CartType,string BoxOrPallet)
    {
        _RtnPreLoad_Exact = false;
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select COUNT(distinct IUM_ID) from BDS_PreLoading where Item_CartID=(select CartID from Item_Carton_Box_Master where IUMID='" + ID + "' and Cart_Type='" + CartType + "' and Box_Pallet='" + BoxOrPallet + "')  and Status<>'Close' and Status<>'Cancel'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        if (Convert.ToInt32(dr[0].ToString()) == 0)
                        {
                            _RtnPreLoad_Exact = true;
                        }
                        else if (Convert.ToInt32(dr[0].ToString()) == 1)
                        {
                            _RtnPreLoad_Exact = false;
                        }
                    }
                }
            }
        }

        return _RtnPreLoad;
    }


    // * Add Mode Visible False Items * \\
    public void editvisiblefalse()
    {
        txtitemcode.ReadOnly = false;
        txtitemnameadd.ReadOnly = false;
       // btnsubmit.Text = "Save";
        btncartupdate.Visible = false;
        btnupdate.Visible = false;
        btnsubmit.Visible = true;
    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Textclearthree();
        txtcalcvalue.Text = "";
        txtmaxcart.Text = txtcalcvalue.Text;
        Chkdivider.Checked = false;
        Chklinterleaf.Checked = false;
        txt_Car_type.Text = "";
        txtcar_length.Text = "";
        txtcar_height.Text = "";
        txtcar_width.Text = "";
        txtcar_X_Offset.Text = "";
        txtcar_Y_Offset.Text = "";
        txtcar_Z_offset.Text = "";
        txtcar_X_Pitch.Text = "";
        txtcar_Y_Pitch.Text = "";
        txtcar_Z_Pitch.Text = "";
        //txtcar_layer.Text = "";
        txtcar_Row.Text = "";
        txtcar_Column.Text = "";

        txtsmallbag.Text = "";
        txtmedium.Text = "";
        txtlarge.Text = "";            

        lblpatype.Text = ddltype.SelectedValue.ToString();
        lblsptype.Text  = ddltype.SelectedValue.ToString();
        lblmetype.Text = ddltype.SelectedValue.ToString();
        lblmaxcont.Text = ddltype.SelectedValue.ToString();
        lblmaxbbbdsuom.Text = ddltype.SelectedValue.ToString();
        lblpacktypeinfo.Text = "";

        if (ddltype.SelectedValue.ToString().ToLower() == "Bundle".ToLower())
        {
            HideBunddle();
            lblpacktypeinfo.Text = " For HDDS";
            DDLBoxOrPallet.Items.Add("");
        }
        else if (ddltype.SelectedValue.ToString().ToLower() == "Strip".ToLower())
        {
            HideStrips();
            lblpacktypeinfo.Text = " For HDDS";
            DDLBoxOrPallet.Items.Add("");
        }
        else if ((ddltype.SelectedValue.ToString().ToLower() == "Bottle".ToLower()) || (ddltype.SelectedValue.ToString().ToLower() == "Box-Bottle".ToLower()))
        {            
            HideBottle();
            txtmaxcontainer.Text = "";
            lblpacktypeinfo.Text = " For BDS";
            BoxOrPallet();
        }
        else if (ddltype.SelectedValue.ToString().ToLower() == "Box-Strip".ToLower())
        {
            HideBox();
            txtmaxcontainer.Text = "";
            lblpacktypeinfo.Text = " For HDDS";
            DDLBoxOrPallet.Items.Add("");
        }

        if (ddlcar_layer.SelectedValue.ToString() == "1")
        {
           // lblinterleaf.Enabled = false;
            Chklinterleaf.Enabled = false;
            Chklinterleaf.Checked = false;
        }
        else if (ddlcar_layer.SelectedValue.ToString() == "2")
        {
            //lblinterleaf.Enabled = true;
            Chklinterleaf.Enabled = true;
            Chklinterleaf.Checked = true;
        }

        try
        {
            if (DDLCartonType.SelectedValue.ToString() == "")
            {
                lblddlcarttype.Visible = false;
                DDLCartonType.Visible = false;
            }
        }
        catch (NullReferenceException)
        {
            lblddlcarttype.Visible = false;
            DDLCartonType.Visible = false;
        }

        try
        {
            lbluom1.Text = ddltype.SelectedValue.ToString() + " " + "of" + " " + txtpacksize.Text + " " + ddluom.Text;
            lbluom2.Text = ddltype.SelectedValue.ToString() + " " + "of" + " " + txtpacksize.Text + " " + ddluom.Text;
          
        }
        catch (NullReferenceException)
        {
        }
    }

    protected void txtmaxdds_TextChanged(object sender, EventArgs e)
    {
    }
    protected void ddluom_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbluom1.Text = ddltype.SelectedValue.ToString() + " " + "of" + " " + txtpacksize.Text + " " + ddluom.Text;
        lbluom2.Text = ddltype.SelectedValue.ToString() + " " + "of" + " " + txtpacksize.Text + " " + ddluom.Text;
    }
    protected void ddlbrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtmaxcart.Text = txtcalcvalue.Text;
        mfrcodereader();
    }



    // * Active Status * \\
    public void Activestatus()
    {
        if (chkactive.Checked == true)
        {
            Actstatus = "Yes";
        }
        else if (chkactive.Checked == false)
        {
            Actstatus = "No";
        }
    }
    protected void gridedit_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        useraddgrid();
        gridedit.PageIndex = e.NewPageIndex;
        gridedit.DataBind();
        DataSet dsData = gridedit.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(gridedit.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridedit.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }

    protected void btncalc_Click(object sender, ImageClickEventArgs e)
    {
        if (Convert.ToDecimal(txtwidth.Text.Trim()) <= Convert.ToDecimal(txtheight.Text.Trim()))
        {
            if (Convert.ToDecimal(txtheight.Text.Trim()) <= Convert.ToDecimal(txtlength.Text.Trim()))
            {
                Calculate();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('L2 equal or lower than L1 value ');</script>", false);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('L3 equal or lower than L2 value ');</script>", false);
        }
    }

    void Calculate()
    {
        LoadingMasterBottlePitchReader();
        if ((ddltype.SelectedValue.ToString().ToLower() == "Bottle".ToLower()) || (ddltype.SelectedValue.ToString().ToLower() == "Box-Bottle".ToLower()))
        {
            if (chkrotation.Checked == false)
            {
                if (txtwidth.Text != "")
                {
                    txtmaxcart.Text = Convert.ToString(370 / (Convert.ToDecimal(txtwidth.Text.Trim()) + Convert.ToDecimal(txtbottlepitch.Text)));
                    string i = "";
                    i = txtmaxcart.Text;
                    Int32 len = i.Length;
                    Int32 n = i.IndexOf('.');
                    if (n > 0)
                    {
                        txtmaxcart.Text = i.Substring(0, n);
                        txtcalcvalue.Text = i.Substring(0, n);
                    }
                    else
                    {
                        txtmaxcart.Text = i.ToString();
                        txtcalcvalue.Text = i.ToString();
                    }
                    txtmaxdds.Focus();
                }
            }
            else if (chkrotation.Checked == true)
            {
                if (txtheight.Text != "")
                {
                    txtmaxcart.Text = Convert.ToString(370 / (Convert.ToDecimal(txtheight.Text.Trim()) + Convert.ToDecimal(txtbottlepitch.Text)));
                    string i = "";
                    i = txtmaxcart.Text;
                    Int32 len = i.Length;
                    Int32 n = i.IndexOf('.');
                    if (n > 0)
                    {
                        txtmaxcart.Text = i.Substring(0, n);
                        txtcalcvalue.Text = i.Substring(0, n);
                    }
                    else
                    {
                        txtmaxcart.Text = i.ToString();
                        txtcalcvalue.Text = i.ToString();
                    }
                    txtmaxdds.Focus();
                }
            }
        }
        else
        {
            if (txtwidth.Text != "")
            {
                //txtmaxcart.Text = Convert.ToString(Convert.ToInt32(700 / Convert.ToDecimal(txtwidth.Text.Trim())));
                txtmaxcart.Text = Convert.ToString(600 / Convert.ToDecimal(txtwidth.Text.Trim()));
                string i = "";
                i = txtmaxcart.Text;
                Int32 len = i.Length;
                Int32 n = i.IndexOf('.');
                if (n > 0)
                {
                    txtmaxcart.Text = i.Substring(0, n);
                    txtcalcvalue.Text = i.Substring(0, n);
                }
                else
                {
                    txtmaxcart.Text = i.ToString();
                    txtcalcvalue.Text = i.ToString();
                }
                txtmaxdds.Focus();
            }
        }
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

    protected void gridedit_Sorting(object sender, GridViewSortEventArgs e)
    {
        useraddgrid();
        DataSet dsData = gridedit.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = gridedit.PageIndex;
        gridedit.DataSource = SortDataTable(dtData, false);
        gridedit.DataBind();
        gridedit.PageIndex = pageIndex;  
    }

    protected void btnbrand_Click(object sender, EventArgs e)
    {

    }    




    void HideStrips()
    {
        HideBottle_Carton_Update_Enabled();
        lblcontainer.Enabled = false;
        txtmaxcontainer.Enabled = false;
        txtmaxcontainer.Text = "1";

        lblsmallbag.Enabled = true;
        txtsmallbag.Enabled = true;
        lblsptype.Enabled = true;

        lblmedium.Enabled = true;
        txtmedium.Enabled = true;
        lblmetype.Enabled = true;

        lbllargebag.Enabled = true;
        txtlarge.Enabled = true;
        lblpatype.Enabled = true;

        lblmaxcont.Enabled = false;

        lblboxorpallet.Enabled = false;
        DDLBoxOrPallet.Enabled = false;

        lblcartype.Enabled = false;
        txt_Car_type.Enabled = false;

        lblcartheight.Enabled = false;
        txtcar_height.Enabled = false;

        lblcartwidth.Enabled = false;
        txtcar_width.Enabled = false;

        lblcart_x_offset.Enabled = false;
        txtcar_X_Offset.Enabled = false;

        lblcart_Y_Offset.Enabled = false;
        txtcar_Y_Offset.Enabled = false;

        lblcot_Z_offset.Enabled = false;
        txtcar_Z_offset.Enabled = false;

        lblcart_X_Pitch.Enabled = false;
        txtcar_X_Pitch.Enabled = false;

        lblcart_Y_Pitch.Enabled = false;
        txtcar_Y_Pitch.Enabled = false;

        lblcart_Z_Pitch.Enabled = false;
        txtcar_Z_Pitch.Enabled = false;

        lblcart_No_Layer.Enabled = false;
        //txtcar_layer.Enabled = false;
        ddlcar_layer.Enabled = false;
        lblcart_Column.Enabled = false;
        txtcar_Column.Enabled = false;

        lblcart_Row.Enabled = false;
        txtcar_Row.Enabled = false;

        chkhrip.Enabled = true;
        chkvrip.Enabled = true;

        lblmaxbbbds.Enabled = false;
        txtmaxbbbds.Enabled = false;
        lblmaxbbbdsuom.Enabled = false;

        lblcartlength.Enabled = false;
        txtcar_length.Enabled = false;

        lbldivider.Enabled = false;
        Chkdivider.Enabled = false;

        lblinterleaf.Enabled = false;
        Chklinterleaf.Enabled = false;

        lblrotation.Enabled = false;
        chkrotation.Enabled = false;

        LblCartActive.Enabled = false;
        ChkCartActive.Enabled = false;

        txt_Car_type.Text = "0";
        txtcar_length.Text = "0";
        txtcar_height.Text = "0";
        txtcar_width.Text = "0";
        txtcar_X_Offset.Text = "0";
        txtcar_Y_Offset.Text = "0";
        txtcar_Z_offset.Text = "0";
        txtcar_X_Pitch.Text = "0";
        txtcar_Y_Pitch.Text = "0";
        txtcar_Z_Pitch.Text = "0";        
        txtcar_Row.Text = "0";
        txtcar_Column.Text = "0";
        txtmaxbbbds.Text = "0";
        DDLCartonType.Visible = false;
        lblddlcarttype.Visible = false;
    }

    void HideBunddle()
    {
        HideBottle_Carton_Update_Enabled();
        lblcontainer.Enabled = true;
        txtmaxcontainer.Enabled = true;
        //txtmaxcontainer.Text = "";

        lblsmallbag.Enabled = true;
        txtsmallbag.Enabled = true;
        lblsptype.Enabled = true;

        lblmedium.Enabled = true;
        txtmedium.Enabled = true;
        lblmetype.Enabled = true;

        lbllargebag.Enabled = true;
        txtlarge.Enabled = true;
        lblpatype.Enabled = true;

        lblmaxcont.Enabled = true;

        lblboxorpallet.Enabled = false;
        DDLBoxOrPallet.Enabled = false;

        lblcartype.Enabled = false;
        txt_Car_type.Enabled = false;

        lblcartheight.Enabled = false;
        txtcar_height.Enabled = false;

        lblcartwidth.Enabled = false;
        txtcar_width.Enabled = false;

        lblcart_x_offset.Enabled = false;
        txtcar_X_Offset.Enabled = false;

        lblcart_Y_Offset.Enabled = false;
        txtcar_Y_Offset.Enabled = false;

        lblcot_Z_offset.Enabled = false;
        txtcar_Z_offset.Enabled = false;

        lblcart_X_Pitch.Enabled = false;
        txtcar_X_Pitch.Enabled = false;

        lblcart_Y_Pitch.Enabled = false;
        txtcar_Y_Pitch.Enabled = false;

        lblcart_Z_Pitch.Enabled = false;
        txtcar_Z_Pitch.Enabled = false;

        lblcart_No_Layer.Enabled = false;
        //txtcar_layer.Enabled = false;
        ddlcar_layer.Enabled = false;

        lblcart_Column.Enabled = false;
        txtcar_Column.Enabled = false;

        lblcart_Row.Enabled = false;
        txtcar_Row.Enabled = false;

        chkhrip.Enabled = false;
        chkvrip.Enabled = false;

        lblcartlength.Enabled = false;
        txtcar_length.Enabled = false;

        lbldivider.Enabled = false;
        Chkdivider.Enabled = false;

        lblinterleaf.Enabled = false;
        Chklinterleaf.Enabled = false;

        lblrotation.Enabled = false;
        chkrotation.Enabled = false;

        LblCartActive.Enabled = false;
        ChkCartActive.Enabled = false;

        lblmaxbbbds.Enabled = false;
        txtmaxbbbds.Enabled = false;
        lblmaxbbbdsuom.Enabled = false;

        txt_Car_type.Text = "0";
        txtcar_length.Text = "0";
        txtcar_height.Text = "0";
        txtcar_width.Text = "0";
        txtcar_X_Offset.Text = "0";
        txtcar_Y_Offset.Text = "0";
        txtcar_Z_offset.Text = "0";
        txtcar_X_Pitch.Text = "0";
        txtcar_Y_Pitch.Text = "0";
        txtcar_Z_Pitch.Text = "0";
        //txtcar_layer.Text = "0";
        txtcar_Row.Text = "0";
        txtcar_Column.Text = "0";
        txtmaxbbbds.Text = "0";
        DDLCartonType.Visible = false;
        lblddlcarttype.Visible = false;
    }


    void HideBox()
    {
        HideBottle_Carton_Update_Enabled();
        lblsmallbag.Enabled = false;
        txtsmallbag.Enabled = false;
        lblsptype.Enabled = false;

        lblmedium.Enabled = false;
        txtmedium.Enabled = false;
        lblmetype.Enabled = false;

        lbllargebag.Enabled = false;
        txtlarge.Enabled = false;
        lblpatype.Enabled = false;

        lblmaxcont.Enabled = true;

        lblcontainer.Enabled = true;
        txtmaxcontainer.Enabled = true;

        lblboxorpallet.Enabled = false;
        DDLBoxOrPallet.Enabled = false;
        
        lblcartype.Enabled = false;
        txt_Car_type.Enabled = false;

        lblcartheight.Enabled = false;
        txtcar_height.Enabled = false;

        lblcartwidth.Enabled = false;
        txtcar_width.Enabled = false;

        lblcart_x_offset.Enabled = false;
        txtcar_X_Offset.Enabled = false;

        lblcart_Y_Offset.Enabled = false;
        txtcar_Y_Offset.Enabled = false;

        lblcot_Z_offset.Enabled = false;
        txtcar_Z_offset.Enabled = false;

        lblcart_X_Pitch.Enabled = false;
        txtcar_X_Pitch.Enabled = false;

        lblcart_Y_Pitch.Enabled = false;
        txtcar_Y_Pitch.Enabled = false;

        lblcart_Z_Pitch.Enabled = false;
        txtcar_Z_Pitch.Enabled = false;

        lblcart_No_Layer.Enabled = false;
        //txtcar_layer.Enabled = false;
        ddlcar_layer.Enabled = false;


        lblcart_Column.Enabled = false;
        txtcar_Column.Enabled = false;

        lblcart_Row.Enabled = false;
        txtcar_Row.Enabled = false;

        chkhrip.Enabled = false;
        chkvrip.Enabled = false;

        lblcartlength.Enabled = false;
        txtcar_length.Enabled = false;

        lbldivider.Enabled = false;
        Chkdivider.Enabled = false;

        lblinterleaf.Enabled = false;
        Chklinterleaf.Enabled = false;

        lblrotation.Enabled = false;
        chkrotation.Enabled = false;


        LblCartActive.Enabled = false;
        ChkCartActive.Enabled = false;

        lblmaxbbbds.Enabled = false;
        txtmaxbbbds.Enabled = false;
        lblmaxbbbdsuom.Enabled = false;

        txtsmallbag.Text = "0";
        txtmedium.Text = "0";
        txtlarge.Text = "1";

        txt_Car_type.Text = "0";
        txtcar_length.Text = "0";
        txtcar_height.Text = "0";
        txtcar_width.Text = "0";
        txtcar_X_Offset.Text = "0";
        txtcar_Y_Offset.Text = "0";
        txtcar_Z_offset.Text = "0";
        txtcar_X_Pitch.Text = "0";
        txtcar_Y_Pitch.Text = "0";
        txtcar_Z_Pitch.Text = "0";
        //txtcar_layer.Text = "0";
        txtcar_Row.Text = "0";
        txtcar_Column.Text = "0";
        txtmaxbbbds.Text = "0";
        DDLCartonType.Visible = false;
        lblddlcarttype.Visible = false;

       // lblwidth.Text = "Pack Type Width (mm)";
    }

    void HideBottle()
    {
        HideBottle_Carton_Update_Enabled();
        lblsmallbag.Enabled = false;
        txtsmallbag.Enabled = false;
        lblsptype.Enabled = false;

        lblmedium.Enabled = false;
        txtmedium.Enabled = false;
        lblmetype.Enabled = false;

        lbllargebag.Enabled = false;
        txtlarge.Enabled = false;
        lblpatype.Enabled = false;

        lblmaxcont.Enabled = true;

        lblcontainer.Enabled = true;
        txtmaxcontainer.Enabled = true;

        lblboxorpallet.Enabled = true;
        DDLBoxOrPallet.Enabled = true;

        lblcartype.Enabled = true;
        txt_Car_type.Enabled = true;

        lblcartheight.Enabled = true;
        txtcar_height.Enabled = true;

        lblcartwidth.Enabled = true;
        txtcar_width.Enabled = true;

        lblcart_x_offset.Enabled = true;
        txtcar_X_Offset.Enabled = true;

        lblcart_Y_Offset.Enabled = true;
        txtcar_Y_Offset.Enabled = true;

        lblcot_Z_offset.Enabled = true;
        txtcar_Z_offset.Enabled = true;

        lblcart_X_Pitch.Enabled = true;
        txtcar_X_Pitch.Enabled = true;

        lblcart_Y_Pitch.Enabled = true;
        txtcar_Y_Pitch.Enabled = true;

        lblcart_Z_Pitch.Enabled = true;
        txtcar_Z_Pitch.Enabled = true;

        lblcart_No_Layer.Enabled = true;        
        ddlcar_layer.Enabled = true;

        lblcart_Column.Enabled = true;
        txtcar_Column.Enabled = true;

        lblcart_Row.Enabled = true;
        txtcar_Row.Enabled = true;

        chkhrip.Enabled = false;
        chkvrip.Enabled = false;

        lblcartlength.Enabled = true;
        txtcar_length.Enabled = true;

        lbldivider.Enabled = true;
        Chkdivider.Enabled = true;

        lblinterleaf.Enabled = true;
        Chklinterleaf.Enabled = true;

        lblrotation.Enabled = true;
        chkrotation.Enabled = true;

        LblCartActive.Enabled = true;
        ChkCartActive.Enabled = true;

        txtsmallbag.Text = "0";
        txtmedium.Text = "0";
        txtlarge.Text = "1";

        txt_Car_type.Visible = true;
        DDLCartonType.Visible = true;

        lblmaxbbbds.Enabled = true;
        txtmaxbbbds.Enabled = true;
        lblmaxbbbdsuom.Enabled = true;
    }


    void HideBottle_Carton_Update_Enabled()
    {
        txtlength.Enabled = true;
        txtheight.Enabled = true;
        txtwidth.Enabled = true;
        txtmaxcart.Enabled = true;
        txtmaxdds.Enabled = true;
        txtsmallbag.Enabled = true;
        txtmedium.Enabled = true;
        txtlarge.Enabled = true;
        txtmaxcontainer.Enabled = true;

        chkactive.Enabled = true;
        chkhrip.Enabled = true;
        chkvrip.Enabled = true;
        chkrotation.Enabled = true;
        ddlbrand.Enabled = true;
        ddltype.Enabled = true;
        txtpacksize.Enabled = true;
        ddluom.Enabled = true;
        btncalc.Enabled = true;
        lbluom1.Enabled = true;
        lbluom2.Enabled = true;
        lblmetype.Enabled = true;
        lblmaxcont.Enabled = true;
        lblcontainer.Enabled = true;
        lblrotation.Enabled = true;
        lblsmallbag.Enabled = true;
        lblmedium.Enabled = true;
        lbllargebag.Enabled = true;
        lblsptype.Enabled = true;
        lblpatype.Enabled = true;
        gridpharloc.Enabled = true;

        lblmaxbbbds.Enabled = true;
        txtmaxbbbds.Enabled = true;
        lblmaxbbbdsuom.Enabled = true;
    }

    void HideBottle_Carton_Update_Disabled()
    {
        txtlength.Enabled = false;
        txtheight.Enabled = false;
        txtwidth.Enabled = false;
        txtmaxcart.Enabled = false;
       // txtmaxdds.Enabled = false;
        txtsmallbag.Enabled = false;
        txtmedium.Enabled = false;
        txtlarge.Enabled = false;
        txtmaxcontainer.Enabled = false;

        chkactive.Enabled = false;
        chkhrip.Enabled = false;
        chkvrip.Enabled = false;
        chkrotation.Enabled = false;

        //lblmaxbbbds.Enabled = false;
        //txtmaxbbbds.Enabled = false;
        //lblmaxbbbdsuom.Enabled = false;

        ddltype.Enabled = false;
        ddlbrand.Enabled = false;
        txtpacksize.Enabled = false;
        ddluom.Enabled = false;
        btncalc.Enabled = false;
        lbluom1.Enabled = false;
        //lbluom2.Enabled = false;
        lblmetype.Enabled = false;
        lblmaxcont.Enabled = false;
        gridpharloc.Enabled = false;
    }

    void HideBottle_Carton_Update_Disabled2()
    {
        txt_Car_type.Enabled = false;
        txtcar_length.Enabled = false;
        txtcar_height.Enabled = false;
        txtcar_width.Enabled = false;
        txtcar_X_Offset.Enabled = false;
        txtcar_Y_Offset.Enabled = false;
        txtcar_Z_offset.Enabled = false;
        txtcar_X_Pitch.Enabled = false;
        txtcar_Y_Pitch.Enabled = false;
        txtcar_Z_Pitch.Enabled = false;
        txtcar_Row.Enabled = false;
        txtcar_Column.Enabled = false;
        Chkdivider.Enabled = false;
        Chklinterleaf.Enabled = false;
        //chkrotation.Enabled = false;
        gridpharloc.Enabled = false;
        ddlcar_layer.Enabled = false;
        ChkCartActive.Enabled = false;
    }

    void HideBottle_Carton_Update_Enabled2()
    {
        txt_Car_type.Enabled = true;
        txtcar_length.Enabled = true;
        txtcar_height.Enabled = true;
        txtcar_width.Enabled = true;
        txtcar_X_Offset.Enabled = true;
        txtcar_Y_Offset.Enabled = true;
        txtcar_Z_offset.Enabled = true;
        txtcar_X_Pitch.Enabled = true;
        txtcar_Y_Pitch.Enabled = true;
        txtcar_Z_Pitch.Enabled = true;
        txtcar_Row.Enabled = true;
        txtcar_Column.Enabled = true;
        Chkdivider.Enabled = true;
        Chklinterleaf.Enabled = true;
       // chkrotation.Enabled = true;
        gridpharloc.Enabled = true;
        ddlcar_layer.Enabled = true;
        ChkCartActive.Enabled = true;
    }


    void CartlayerAdd()
    {
        ddlcar_layer.Items.Clear();
        ddlcar_layer.Items.Add("1");
        ddlcar_layer.Items.Add("2");
    }

    protected void ddlcar_layer_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtmaxcart.Text = txtcalcvalue.Text;
        if (ddlcar_layer.SelectedValue.ToString() == "1")
        {
            lblinterleaf.Enabled = false;
            Chklinterleaf.Enabled = false;
            Chklinterleaf.Checked = false;
        }
        else if(ddlcar_layer.SelectedValue.ToString() == "2")
        {
            lblinterleaf.Enabled = true;
            Chklinterleaf.Enabled = true;
            Chklinterleaf.Checked = true;
        }
        
    }
    protected void chkrotation_CheckedChanged(object sender, EventArgs e)
    {
        LoadingMasterBottlePitchReader();
        if (chkrotation.Checked == true)
        {
            if ((ddltype.SelectedValue.ToString().ToLower() == "Bottle".ToLower()) || (ddltype.SelectedValue.ToString().ToLower() == "Box-Bottle".ToLower()))
            {
                if (txtwidth.Text != "")
                {
                    txtmaxcart.Text = Convert.ToString(370 / (Convert.ToDecimal(txtheight.Text.Trim()) + Convert.ToDecimal(txtbottlepitch.Text)));
                    string i = "";
                    i = txtmaxcart.Text;
                    Int32 len = i.Length;
                    Int32 n = i.IndexOf('.');
                    if (n > 0)
                    {
                        txtmaxcart.Text = i.Substring(0, n);
                        txtcalcvalue.Text = i.Substring(0, n);
                    }
                    else
                    {
                        txtmaxcart.Text = i.ToString();
                        txtcalcvalue.Text = i.ToString();
                    }
                    txtmaxdds.Focus();
                }
            }
        }
        else if(chkrotation.Checked==false)
        {
            
            if ((ddltype.SelectedValue.ToString().ToLower() == "Bottle".ToLower()) || (ddltype.SelectedValue.ToString().ToLower() == "Box-Bottle".ToLower()))
            {
                if (txtwidth.Text != "")
                {
                    txtmaxcart.Text = Convert.ToString(370 / (Convert.ToDecimal(txtwidth.Text.Trim()) + Convert.ToDecimal(txtbottlepitch.Text)));
                    string i = "";
                    i = txtmaxcart.Text;
                    Int32 len = i.Length;
                    Int32 n = i.IndexOf('.');
                    if (n > 0)
                    {
                        txtmaxcart.Text = i.Substring(0, n);
                        txtcalcvalue.Text = i.Substring(0, n);
                    }
                    else
                    {
                        txtmaxcart.Text = i.ToString();
                        txtcalcvalue.Text = i.ToString();
                    }
                    txtmaxdds.Focus();
                }
            }
        }
    }
    protected void btncartupdate_Click(object sender, ImageClickEventArgs e)
    {
        Carton_Box_Para_Update();
        if (Rtnvalue == 1)
        {
            HideBottle_Carton_Update_Enabled();
            gridedit.PageIndex = 0;
            useraddgrid();
            locationdisplay();
            textclearone();
            textcleartwo();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated');</script>", false);
        }
        else if (Rtnvalue == 2)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Update Fail');</script>", false);
        }
        else if (Rtnvalue == 3)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('BDS Bottle/Box Preloading transaction started. Cancel the Bottle/Box transaction and try again');</script>", false);
        }    
        else if (Rtnvalue == 4)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton box/Pallet bottle count not matched with multiplication of X and Y count');</script>", false);
        }
        else if (Rtnvalue == 16)
        {
            if (DDLBoxOrPallet.SelectedValue.ToString() == "BOX")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box of bottle count " + txt_Car_type.Text + " already exist for this item');</script>", false);
            }
            else if (DDLBoxOrPallet.SelectedValue.ToString() == "PALLET")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Pallet of bottle count " + txt_Car_type.Text + " already exist for this item');</script>", false);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occurred');</script>", false);
        }
    }

    void Carton_Box_Para_Update()
    {
        string Carton_Condition = "";
        if ((ddltype.SelectedValue.ToString().ToLower() == "BOTTLE".ToLower()) || (ddltype.SelectedValue.ToString().ToLower() == "BOX-BOTTLE".ToLower()))
        {
            if (Convert.ToInt32(txt_Car_type.Text) == (Convert.ToInt32(txtcar_Row.Text)) * (Convert.ToInt32(txtcar_Column.Text)) * (Convert.ToInt32(ddlcar_layer.SelectedValue.ToString())))
            {
                Carton_Condition = "YES";
            }
            else
            {
                Carton_Condition = "NO";
                Rtnvalue = 4;
            }
        }

        if (Carton_Condition == "YES")
        {
            int a = 0;
            for (int i = 0; i < gridpharloc.Rows.Count; i++)
            {
                GridViewRow row = gridpharloc.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkphar")).Checked;
                if (isChecked)
                {
                    a++;
                    if (ViewState["pharname"].ToString().ToLower() == (gridpharloc.Rows[i].Cells[0].Text.ToLower()))
                    {

                        if (Chkdivider.Checked == true)
                        {
                            _Divider = true;
                        }
                        else if (Chkdivider.Checked == false)
                        {
                            _Divider = false;
                        }

                        if (Chklinterleaf.Checked == true)
                        {
                            _Interleaf = true;
                        }
                        else if (Chklinterleaf.Checked == false)
                        {
                            _Interleaf = false;
                        }

                        if (chkrotation.Checked == true)
                        {
                            _RotationFlag = true;
                        }
                        else if (chkrotation.Checked == false)
                        {
                            _RotationFlag = false;
                        }

                        if (ChkCartActive.Checked == true)
                        {
                            CartActStatus = "Yes";
                        }
                        else if (ChkCartActive.Checked == false)
                        {
                            CartActStatus = "No";
                        }

                        txtmaxcart.Text = txtcalcvalue.Text;

                        int OLDCtype = 0;
                        if ((DDLCartonType.SelectedValue.ToString() != "-Select-") && (ddltype.SelectedValue.ToString().ToLower() == "BOTTLE".ToLower()) ||
                            (ddltype.SelectedValue.ToString().ToLower() == "BOX-BOTTLE".ToLower()))
                        {
                            OLDCtype = Convert.ToInt32(DDLCartonType.SelectedValue.ToString());
                        }
                        else
                        {
                            OLDCtype = 0;
                        }


                        Rtnvalue = drg.User_Master_Carton_Update((int)ViewState["idno"],(gridpharloc.Rows[i].Cells[0].Text), DDLBoxOrPallet.SelectedValue.ToString(), Convert.ToInt32(txt_Car_type.Text),
                                    Convert.ToDecimal(txtcar_length.Text), Convert.ToDecimal(txtcar_height.Text), Convert.ToDecimal(txtcar_width.Text), Convert.ToDecimal(txtcar_X_Offset.Text),
                                    Convert.ToDecimal(txtcar_Y_Offset.Text), Convert.ToDecimal(txtcar_Z_offset.Text), Convert.ToDecimal(txtcar_X_Pitch.Text),
                                    Convert.ToDecimal(txtcar_Y_Pitch.Text), Convert.ToDecimal(txtcar_Z_Pitch.Text), Convert.ToInt32(ddlcar_layer.SelectedValue.ToString()),
                                    Convert.ToInt32(txtcar_Row.Text), Convert.ToInt32(txtcar_Column.Text), _Divider, _Interleaf,  sessionuserid = Session["Userid"].ToString(),
                                    OLDCtype, CartActStatus);

                        if (Rtnvalue == 2 || Rtnvalue == 3)
                        {
                            return;
                        }
                    }
                    else if (ViewState["pharname"].ToString().ToLower() != (gridpharloc.Rows[i].Cells[0].Text.ToLower()))
                    {
                        Rtnvalue = 4;
                    }
                }
            }
            if (a == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Select pharmacy location');</script>", false);
            }
        }
    }

    void ClearCart()
    {
        HideBottle_Carton_Update_Enabled();

        txtpacksize.Text = "";
        txtlength.Text = "";
        txtheight.Text = "";
        txtwidth.Text = "";
        txtmaxcart.Text = "";
        txtmaxdds.Text = "";
        txtsmallbag.Text = "";
        txtlarge.Text = "";
        txtmedium.Text = "";
        txtmaxcontainer.Text = "";
        chkactive.Checked = false;

        btncartupdate.Visible = false;
        btnupdate.Visible = false;
        btnsubmit.Visible = true;

        txtmaxcontainer.Text = "";
        lblcontainer.Visible = true;
        txtmaxcontainer.Visible = true;
        chkhrip.Checked = false;
        chkvrip.Checked = false;
        Chkdivider.Checked = false;
        Chklinterleaf.Checked = false;
        chkrotation.Checked = false;
        chkactive.Checked = false;

        txt_Car_type.Text = "";
        txtcar_height.Text = "";
        txtcar_width.Text = "";
        txtcar_X_Offset.Text = "";
        txtcar_Y_Offset.Text = "";
        txtcar_Z_offset.Text = "";
        txtcar_X_Pitch.Text = "";
        txtcar_Y_Pitch.Text = "";
        txtcar_Z_Pitch.Text = "";
        //txtcar_layer.Text = "";
        txtcar_Row.Text = "";
        txtcar_Column.Text = "";
        txtcar_length.Text = "";
        CartlayerAdd();

       // ViewState["pharname"] = "";
        locationdisplay();
        lblpacktypeinfo.Text = "";

        brandreader();
        uomread();
        itemmastervalue();
        //gridedit.PageIndex = 0;
        //useraddgrid();
        packtyperead();

        if (ddltype.SelectedValue.ToString().ToLower() == "Bundle".ToLower())
        {
            HideBunddle();
            lblpacktypeinfo.Text = " For HDDS";
        }
        else if (ddltype.SelectedValue.ToString().ToLower() == "Strip".ToLower())
        {
            HideStrips();
            lblpacktypeinfo.Text = " For HDDS";
        }
        else if ((ddltype.SelectedValue.ToString().ToLower() == "Bottle".ToLower()) || (ddltype.SelectedValue.ToString().ToLower() == "Box-Bottle".ToLower()))
        {
            HideBottle();
            txtmaxcontainer.Text = "";
            lblpacktypeinfo.Text = " For BDS";
        }
        else if (ddltype.SelectedValue.ToString().ToLower() == "Box-Strip".ToLower())
        {
            HideBox();
            txtmaxcontainer.Text = "";
            lblpacktypeinfo.Text = " For HDDS";
        }

        if (ddlbrand.SelectedItem != null)
        {
            mfrcodereader();
        }
        try
        {
            lbluom1.Text = ddltype.SelectedValue.ToString() + " " + "of" + " " + txtpacksize.Text + " " + ddluom.Text;
            lbluom2.Text = ddltype.SelectedValue.ToString() + " " + "of" + " " + txtpacksize.Text + " " + ddluom.Text;
            lblpatype.Text = ddltype.SelectedValue.ToString();
            lblsptype.Text = ddltype.SelectedValue.ToString();
            lblmetype.Text = ddltype.SelectedValue.ToString();
            lblmaxcont.Text = ddltype.SelectedValue.ToString();
            lblmaxbbbdsuom.Text = ddltype.SelectedValue.ToString();
        }
        catch (NullReferenceException)
        {
        }
    }

    protected void DDLCartonType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_Car_type.Text = "";
        txtcar_length.Text = "";
        txtcar_height.Text = "";
        txtcar_width.Text = "";
        txtcar_X_Offset.Text = "";
        txtcar_Y_Offset.Text = "";
        txtcar_Z_offset.Text = "";
        txtcar_X_Pitch.Text = "";
        txtcar_Y_Pitch.Text = "";
        txtcar_Z_Pitch.Text = "";
        txtcar_Row.Text = "";
        txtcar_Column.Text = "";
        Chkdivider.Checked = false;
        Chklinterleaf.Checked = false;       
        
        Btnonlybottleupdate.Visible = false;        

        ChkCartActive.Checked = false;

        lblminpack.Enabled = true;
        txtmaxdds.Enabled = true;
        lbluom2.Enabled = true;
        lblmaxbbbds.Enabled = true;
        lblmaxbbbdsuom.Enabled = true;
        txtmaxbbbds.Enabled = true;

        if (txtlength.Enabled == true)
        {
            btnupdate.Visible = true;
        }
        else if (txtlength.Enabled == false && txt_Car_type.Enabled==true)
        {            
            btncartupdate.Visible = true;

            lblminpack.Enabled = false;
            txtmaxdds.Enabled = false;
            lbluom2.Enabled = false;
            lblmaxbbbds.Enabled = false;
            lblmaxbbbdsuom.Enabled = false;
            txtmaxbbbds.Enabled = false;
        }

        //HideBottle_Carton_Update_Disabled2();
        //HideBottle_Carton_Update_Enabled2();

        try
        {
            txt_Car_type.Text = DDLCartonType.SelectedValue.ToString();
            BDSPreload_Status_Exact_Check((int)ViewState["idno"], Convert.ToInt32(DDLCartonType.SelectedValue.ToString()),DDLBoxOrPallet.SelectedValue.ToString());
            if (_RtnPreLoad_Exact)
            {                
                HideBottle_Carton_Update_Enabled2();
                if (txtlength.Enabled == false && txt_Car_type.Enabled == true)
                {
                    btncartupdate.Visible = true;
                }
                locationdisplay();
            }
            else if (!_RtnPreLoad_Exact)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('BDS Bottle/Box Preloading transaction started. Cancel the Bottle/Box transaction and try again');</script>", false);
                HideBottle_Carton_Update_Disabled2();
                btncartupdate.Visible = false;

                lblminpack.Enabled = true;
                txtmaxdds.Enabled = true;
                lbluom2.Enabled = true;
                lblmaxbbbds.Enabled = true;
                lblmaxbbbdsuom.Enabled = true;
                txtmaxbbbds.Enabled = true;

                Btnonlybottleupdate.Visible = true;  
            }

                using (SqlConnection con = DBCon.getstring())
                {
                    string Commt = "select  Cart_Type,Cart_Height,Cart_Width,Cart_X_Offset,Cart_Y_Offset,Cart_Z_Offset,"
                          + "Cart_X_Pitch,Cart_Y_Pitch,Cart_Z_Pitch,Cart_No_Of_Layer,Cart_Rows,Cart_Column, "
                          + "Cart_Length,Divider,Interleaf,Active_Status from Item_Carton_Box_Master "
                          + "where IUMID='" + (int)ViewState["idno"] + "' and Cart_Type='" + Convert.ToInt32(DDLCartonType.SelectedValue.ToString()) +"'  and "
                          + "Box_Pallet='" + DDLBoxOrPallet.SelectedValue.ToString() + "'";
                    cmd = new SqlCommand(Commt, con);

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                txt_Car_type.Text = Convert.ToString(dr[0]);
                                txtcar_height.Text = Convert.ToString(dr[1]);
                                txtcar_width.Text = Convert.ToString(dr[2]);
                                txtcar_X_Offset.Text = Convert.ToString(dr[3]);
                                txtcar_Y_Offset.Text = Convert.ToString(dr[4]);
                                txtcar_Z_offset.Text = Convert.ToString(dr[5]);
                                txtcar_X_Pitch.Text = Convert.ToString(dr[6]);
                                txtcar_Y_Pitch.Text = Convert.ToString(dr[7]);
                                txtcar_Z_Pitch.Text = Convert.ToString(dr[8]);

                                CartlayerAdd();
                                if (dr[9].ToString() == "")
                                {
                                    ddlcar_layer.Items.Insert(0, new ListItem("0"));
                                }
                                else
                                {
                                    ddlcar_layer.Items.Remove(dr[9].ToString());
                                    ddlcar_layer.Items.Insert(0, new ListItem(dr[9].ToString()));
                                }

                                txtcar_Row.Text = Convert.ToString(dr[10]);
                                txtcar_Column.Text = Convert.ToString(dr[11]);
                                txtcar_length.Text = Convert.ToString(dr[12]);

                                if (Convert.ToBoolean(dr[13]) == true)
                                {
                                    Chkdivider.Checked = true;
                                }
                                else if (Convert.ToBoolean(dr[13]) == false)
                                {
                                    Chkdivider.Checked = false;
                                }

                                if (Convert.ToBoolean(dr[14]) == true)
                                {
                                    Chklinterleaf.Checked = true;
                                }
                                else if (Convert.ToBoolean(dr[14]) == false)
                                {
                                    Chklinterleaf.Checked = false;
                                }

                                if (dr[15].ToString() == "Yes")
                                {
                                    ChkCartActive.Checked = true;
                                }
                                else if (dr[15].ToString() == "No")
                                {
                                    ChkCartActive.Checked = false;
                                }
                            }
                        }
                    }
                }

        }
        catch (Exception ex)
        {
            txt_Car_type.Text = "";
        }
        //if (ddlcar_layer.SelectedValue.ToString() == "1")
        //{
        //    lblinterleaf.Enabled = false;
        //    Chklinterleaf.Enabled = false;
        //    Chklinterleaf.Checked = false;
        //}
        //else if (ddlcar_layer.SelectedValue.ToString() == "2")
        //{            
        //    Chklinterleaf.Enabled = true;
        //    Chklinterleaf.Checked = true;
        //}

       
    }

    protected void btnaddnewcarton_Click(object sender, ImageClickEventArgs e)
    {
        btncartonsave.Visible = true;
        DDLCartonType.Visible = false;
        lblddlcarttype.Visible = false;
        txt_Car_type.Visible = true;
        btnupdate.Visible = false;
        btncartupdate.Visible = false;
        ddlbrand.Enabled = false;
        Btnonlybottleupdate.Visible = false;
        txt_Car_type.Text = "";
        txtcar_length.Text = "";
        txtcar_height.Text = "";
        txtcar_width.Text = "";
        txtcar_X_Offset.Text = "";
        txtcar_Y_Offset.Text = "";
        txtcar_Z_offset.Text = "";
        txtcar_X_Pitch.Text = "";
        txtcar_Y_Pitch.Text = "";
        txtcar_Z_Pitch.Text = "";
        txtcar_Row.Text = "";
        txtcar_Column.Text = "";
        Chkdivider.Checked = false;
        Chklinterleaf.Checked = false;        
        ChkCartActive.Checked = false;

        txt_Car_type.Enabled = true;
        txtcar_length.Enabled = true;
        txtcar_height.Enabled = true;
        txtcar_width.Enabled = true;
        txtcar_X_Offset.Enabled = true;
        txtcar_Y_Offset.Enabled = true;
        txtcar_Z_offset.Enabled = true;
        txtcar_X_Pitch.Enabled = true;
        txtcar_Y_Pitch.Enabled = true;
        txtcar_Z_Pitch.Enabled = true;
        txtcar_Row.Enabled = true;
        txtcar_Column.Enabled = true;
        Chkdivider.Enabled = true;
        Chklinterleaf.Enabled = true;
        lblinterleaf.Enabled = true;
        ChkCartActive.Enabled = true;
        ddlcar_layer.Enabled = true;


        CartlayerAdd();

        txtlength.Enabled = false;
        txtheight.Enabled = false;
        txtwidth.Enabled = false;
        txtmaxcart.Enabled = false;
        txtmaxdds.Enabled = false;
        txtsmallbag.Enabled = false;
        txtmedium.Enabled = false;
        txtlarge.Enabled = false;
        txtmaxcontainer.Enabled = false;

        chkactive.Enabled = false;
        chkhrip.Enabled = false;
        chkvrip.Enabled = false;
        chkrotation.Enabled = false;

        ddltype.Enabled = false;
        txtpacksize.Enabled = false;
        ddluom.Enabled = false;
        btncalc.Enabled = false;
        lbluom1.Enabled = false;
        lbluom2.Enabled = false;
        lblmetype.Enabled = false;
        lblmaxcont.Enabled = false;
        gridpharloc.Enabled = false;

        lblmaxbbbds.Enabled = false;
        txtmaxbbbds.Enabled = false;
        lblmaxbbbdsuom.Enabled = false;

        BoxOrPallet();

    }

    protected void Btnonlybottleupdate_Click(object sender, ImageClickEventArgs e)
    {
        if (txtsmallbag.Text.Trim() != "" && txtlarge.Text.Trim() != "" && txtmedium.Text.Trim() != "")
        {
            if (Convert.ToDecimal(txtwidth.Text.Trim()) <= Convert.ToDecimal(txtheight.Text.Trim()))
            {
                if (Convert.ToDecimal(txtheight.Text.Trim()) <= Convert.ToDecimal(txtlength.Text.Trim()))
                {
                    if (Convert.ToInt32(txtsmallbag.Text.Trim()) <= Convert.ToInt32(txtmedium.Text.Trim()))
                    {
                        if (Convert.ToInt32(txtmedium.Text.Trim()) <= Convert.ToInt32(txtlarge.Text.Trim()))
                        {
                            if (Convert.ToInt32(txtlarge.Text.Trim()) <= Convert.ToInt32(txtmaxcontainer.Text.Trim()))
                            {
                                Calculate();
                                usermasterupdateOnlyDrug();
                                if (Rtnvalue == 1)
                                {
                                    gridedit.PageIndex = 0;
                                    useraddgrid();
                                    locationdisplay();
                                    textclearone();
                                    textcleartwo();
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated');</script>", false);
                                }
                                else if (Rtnvalue == 2)
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Pack type and pack size already exist in this item code');</script>", false);
                                }
                                else if (Rtnvalue == 3)
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This UOM does not belong to this pharmacy');</script>", false);
                                }
                                else if (Rtnvalue == 4)
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Wrong Pharmacy location');</script>", false);
                                }
                                else if (Rtnvalue == 5)
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Minimum alert quantity per DDS/BDS and Maximum Bottle / Box-Bottle for BDS only updated, because Item is running in DDS/BDS. Disable and remove cartridge from DDS/BDS and try again');</script>", false);
                                    gridedit.PageIndex = 0;
                                    useraddgrid();
                                    locationdisplay();
                                    textclearone();
                                    textcleartwo();
                                    HideBottle_Carton_Update_Enabled();
                                }
                                else if (Rtnvalue == 6)
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Minimum alert quantity per DDS/BDS and Maximum Bottle / Box-Bottle for BDS only updated, because BDS Bottle/Box Preloading transaction started. Cancel the Bottle/Box transaction and try again');</script>", false);
                                    gridedit.PageIndex = 0;
                                    useraddgrid();
                                    locationdisplay();
                                    textclearone();
                                    textcleartwo();
                                    HideBottle_Carton_Update_Enabled();
                                }
                                else if (Rtnvalue == 11)
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('L3 should be minimum 15mm.');</script>", false);
                                }
                                else if (Rtnvalue == 12)
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton box/Pallet bottle count not matched with multiplication of X and Y count');</script>", false);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occurred');</script>", false);
                                }
                            }
                            else
                            {
                                if (ddltype.SelectedValue.ToString().ToLower() != "Bundle".ToLower())
                                {
                                    Calculate();
                                    usermasterupdateOnlyDrug();
                                    if (Rtnvalue == 1)
                                    {
                                        gridedit.PageIndex = 0;
                                        useraddgrid();
                                        locationdisplay();
                                        textclearone();
                                        textcleartwo();
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated');</script>", false);
                                    }
                                    else if (Rtnvalue == 2)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Pack type and pack size already exist in this item code');</script>", false);
                                    }
                                    else if (Rtnvalue == 3)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This UOM does not belong to this pharmacy');</script>", false);
                                    }
                                    else if (Rtnvalue == 4)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Wrong Pharmacy location');</script>", false);
                                    }
                                    else if (Rtnvalue == 5)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Minimum alert quantity per DDS/BDS and Maximum Bottle / Box-Bottle for BDS only updated, because Item is running in DDS/BDS. Disable and remove cartridge from DDS/BDS and try again');</script>", false);
                                    }
                                    else if (Rtnvalue == 6)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('BDS Bottle/Box Preloading transaction started. Cancel the Bottle/Box transaction and try again');</script>", false);
                                    }
                                    else if (Rtnvalue == 11)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('L3 should be minimum 15mm.');</script>", false);
                                    }
                                    else if (Rtnvalue == 12)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton box bottle count not matched with multiplication of X and Y count');</script>", false);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occurred');</script>", false);
                                    }
                                }
                                else if (ddltype.SelectedValue.ToString().ToLower() == "Bundle".ToLower())
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Max No. of Bundle per Container equal or higher than Large bag quantity ');</script>", false);
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Large bag quantity equal or higher than medium bag quantity ');</script>", false);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Medium bag quantity equal or higher than small bag quantity ');</script>", false);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('L2 equal or lower than L1 value ');</script>", false);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('L3 equal or lower than L2 value ');</script>", false);
            }
        }
    }

    // * Item User Master Update Function * \\
    public void usermasterupdateOnlyDrug()
    {

        string Condition = "";
        if ((ddltype.SelectedValue.ToString().ToLower() == "BOX-STRIP".ToLower()) || (ddltype.SelectedValue.ToString().ToLower() == "BUNDLE".ToLower()))
        {
            if (Convert.ToDecimal(txtwidth.Text.Trim()) >= 15)
            {
                Condition = "YES";
            }
            else
            {
                Condition = "NO";
                Rtnvalue = 11;
            }
        }
        else
        {
            Condition = "YES";
        }


        if (Condition == "YES")
        {
            txtmaxcart.Text = txtcalcvalue.Text;
            int a = 0;
            for (int i = 0; i < gridpharloc.Rows.Count; i++)
            {
                GridViewRow row = gridpharloc.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkphar")).Checked;
                if (isChecked)
                {
                    Activestatus();               
                    a++;
                }
            }
            if (a == 1)
            {
                for (int i = 0; i < gridpharloc.Rows.Count; i++)
                {
                    GridViewRow row = gridpharloc.Rows[i];
                    bool isChecked = ((CheckBox)row.FindControl("chkphar")).Checked;
                    if (isChecked)
                    {
                        if (ViewState["pharname"].ToString().ToLower() == (gridpharloc.Rows[i].Cells[0].Text.ToLower()))
                        {
                            Activestatus();
                            if ((ddltype.SelectedValue.ToString().ToLower() == "STRIP".ToLower()))
                            {
                                txtmaxcontainer.Text = "0";
                            }
                            if (chkhrip.Checked == true)
                            {
                                _ChkHRip = true;
                            }
                            else if (chkhrip.Checked == false)
                            {
                                _ChkHRip = false;
                            }

                            if (chkvrip.Checked == true)
                            {
                                _ChkVRip = true;
                            }
                            else if (chkvrip.Checked == false)
                            {
                                _ChkVRip = false;
                            }

                            if (Chkdivider.Checked == true)
                            {
                                _Divider = true;
                            }
                            else if (Chkdivider.Checked == false)
                            {
                                _Divider = false;
                            }

                            if (Chklinterleaf.Checked == true)
                            {
                                _Interleaf = true;
                            }
                            else if (Chklinterleaf.Checked == false)
                            {
                                _Interleaf = false;
                            }

                            if (chkrotation.Checked == true)
                            {
                                _RotationFlag = true;
                            }
                            else if (chkrotation.Checked == false)
                            {
                                _RotationFlag = false;
                            }

                            if (ChkCartActive.Checked == true)
                            {
                                CartActStatus = "Yes";
                            }
                            else if (ChkCartActive.Checked == false)
                            {
                                CartActStatus = "No";
                            }

                            txtmaxcart.Text = txtcalcvalue.Text;
                            int OLDCtype = 0;
                            if ((DDLCartonType.SelectedValue.ToString() != "-Select-") && (ddltype.SelectedValue.ToString().ToLower() == "BOTTLE".ToLower()) &&
                                (ddltype.SelectedValue.ToString().ToLower() == "BOX-BOTTLE".ToLower()))
                            {
                                OLDCtype = Convert.ToInt32(DDLCartonType.SelectedValue.ToString());
                            }
                            else
                            {
                                OLDCtype = 0;
                            }


                            Rtnvalue = drg.usermasterupdate((int)ViewState["idno"], txtitemcode.Text.Trim(), (gridpharloc.Rows[i].Cells[0].Text), ddluom.Text,
                                        ddlbrand.SelectedValue.ToString(), ddltype.SelectedValue.ToString(), txtpacksize.Text.Trim(), Convert.ToDecimal(txtlength.Text.Trim()),
                                        Convert.ToDecimal(txtheight.Text.Trim()), Convert.ToDecimal(txtwidth.Text.Trim()), Convert.ToInt32(txtmaxcart.Text.Trim()), Convert.ToInt32(txtmaxdds.Text.Trim()),
                                        Convert.ToInt32(txtsmallbag.Text.Trim()), Convert.ToInt32(txtmedium.Text.Trim()), Convert.ToInt32(txtlarge.Text.Trim()), Actstatus,
                                        sessionuserid = Session["Userid"].ToString(), Convert.ToInt32(txtmaxcontainer.Text), _ChkHRip, _ChkVRip,"", 0,
                                        0, 0, 0, 0,
                                        0, 0, 0,
                                        0, 0, 0,
                                        0, 0, _Divider, _Interleaf, _RotationFlag, OLDCtype, CartActStatus,Convert.ToInt32(txtmaxbbbds.Text));

                            if (Rtnvalue == 2 || Rtnvalue == 3 || Rtnvalue == 5)
                            {
                                return;
                            }
                        }
                        else if (ViewState["pharname"].ToString().ToLower() != (gridpharloc.Rows[i].Cells[0].Text.ToLower()))
                        {
                            Rtnvalue = 4;
                        }
                    }
                }
            }
            else if (a > 1)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Only one pharmacy location selection is allowed');</script>", false);
            }
            else if (a == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Select pharmacy location');</script>", false);
            }
        }
    }

    protected void btncartonsave_Click(object sender, ImageClickEventArgs e)
    {
        if (txtsmallbag.Text.Trim() != "" && txtlarge.Text.Trim() != "" && txtmedium.Text.Trim() != "")
        {
            if (Convert.ToDecimal(txtwidth.Text.Trim()) <= Convert.ToDecimal(txtheight.Text.Trim()))
            {
                if (Convert.ToDecimal(txtheight.Text.Trim()) <= Convert.ToDecimal(txtlength.Text.Trim()))
                {
                    if (Convert.ToInt32(txtsmallbag.Text.Trim()) <= Convert.ToInt32(txtmedium.Text.Trim()))
                    {
                        if (Convert.ToInt32(txtmedium.Text.Trim()) <= Convert.ToInt32(txtlarge.Text.Trim()))
                        {
                            if (Convert.ToInt32(txtlarge.Text.Trim()) <= Convert.ToInt32(txtmaxcontainer.Text.Trim()))
                            {
                                Calculate();
                                UserMasterCartonInsert();

                                if (Rtnvalue == 1)
                                {
                                    gridedit.PageIndex = 0;
                                    useraddgrid();
                                    locationdisplay();
                                    textclearone();
                                    textcleartwo();
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record created');</script>", false);
                                }
                                else if (Rtnvalue == 3)
                                {
                                    if (DDLBoxOrPallet.SelectedValue.ToString() == "BOX")
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton Box of bottle count "+txt_Car_type.Text +" already exist for this item');</script>", false);
                                    }
                                    else if (DDLBoxOrPallet.SelectedValue.ToString() == "PALLET")
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Pallet of bottle count " + txt_Car_type.Text + " already exist for this item');</script>", false);
                                    }                                    
                                }                               
                                else if (Rtnvalue == 12)
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton box/Pallet bottle count not matched with multiplication of X and Y count');</script>", false);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occurred');</script>", false);
                                }
                            }
                            else
                            {
                                if (ddltype.SelectedValue.ToString().ToLower() != "Bundle".ToLower())
                                {
                                    Calculate();
                                    usermasterinsert();
                                    if (Rtnvalue == 1)
                                    {
                                        gridedit.PageIndex = 0;
                                        useraddgrid();
                                        locationdisplay();
                                        textclearone();
                                        textcleartwo();
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record created');</script>", false);
                                    }
                                    else if (Rtnvalue == 2)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Pack type and pack size already exist in this item code');</script>", false);
                                    }
                                    else if (Rtnvalue == 3)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This UOM does not belong to this pharmacy');</script>", false);
                                    }
                                    else if (Rtnvalue == 4)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Item code and brand name Max No. of Box per container value mismatch of existing pack size value.');</script>", false);
                                    }
                                    else if (Rtnvalue == 11)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('L3 should be minimum 15mm.');</script>", false);
                                    }
                                    else if (Rtnvalue == 12)
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton box/Pallet bottle count not matched with multiplication of X and Y count');</script>", false);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occurred');</script>", false);
                                    }

                                }
                                else if (ddltype.SelectedValue.ToString().ToLower() == "Bundle".ToLower())
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Max No. of Bundle per Container equal or higher than Large bag quantity ');</script>", false);
                                }
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Large bag quantity equal or higher than medium bag quantity ');</script>", false);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Medium bag quantity equal or higher than small bag quantity ');</script>", false);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('L2 equal or lower than L1 value ');</script>", false);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('L3 equal or lower than L2 value ');</script>", false);
            }
        }        
    }

    void BoxOrPallet()
    {
        DDLBoxOrPallet.Items.Clear();

        DDLBoxOrPallet.Items.Add("-Select-");
        DDLBoxOrPallet.Items.Add("BOX");
        DDLBoxOrPallet.Items.Add("PALLET");
    }

    protected void DDLBoxOrPallet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txt_Car_type.Text = "";
            txtcar_length.Text = "";
            txtcar_height.Text = "";
            txtcar_width.Text = "";
            txtcar_X_Offset.Text = "";
            txtcar_Y_Offset.Text = "";
            txtcar_Z_offset.Text = "";
            txtcar_X_Pitch.Text = "";
            txtcar_Y_Pitch.Text = "";
            txtcar_Z_Pitch.Text = "";
            txtcar_Row.Text = "";
            txtcar_Column.Text = "";
            Chkdivider.Checked = false;
            Chklinterleaf.Checked = false;            
            ChkCartActive.Checked = false;
            DDLCartonType.Items.Clear();
            DDLCartonType.Items.Add("-Select-");

            lblminpack.Enabled = true;
            txtmaxdds.Enabled = true;
            lbluom2.Enabled = true;
            lblmaxbbbds.Enabled = true;
            lblmaxbbbdsuom.Enabled = true;
            txtmaxbbbds.Enabled = true;

            if ((btnsubmit.Visible == false) && (btncartonsave.Visible==false))
            {
                Btnonlybottleupdate.Visible = true;
            }

            btnupdate.Visible = false;
            btncartupdate.Visible = false;

            if (DDLBoxOrPallet.SelectedItem.ToString() != "-Select-")
            {
                using (SqlConnection con = DBCon.getstring())
                {
                    string Commt = "select  Cart_Type from Item_Carton_Box_Master where IUMID='" + (int)ViewState["idno"] + "' and Box_Pallet='" + DDLBoxOrPallet.SelectedValue.ToString() + "'";
                    cmd = new SqlCommand(Commt, con);

                    con.Open();
                    using (SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                DDLCartonType.Items.Add(Convert.ToString(dr1[0]));
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (txtwidth.Text.Trim() != "" && txtheight.Text.Trim() != "")
            {
                if (Convert.ToDecimal(txtwidth.Text.Trim()) <= Convert.ToDecimal(txtheight.Text.Trim()))
                {
                    if (Convert.ToDecimal(txtheight.Text.Trim()) <= Convert.ToDecimal(txtlength.Text.Trim()))
                    {
                        Calculate();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('L2 equal or lower than L1 value ');</script>", false);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('L3 equal or lower than L2 value ');</script>", false);
                }
            }
        }
    }
    
}
