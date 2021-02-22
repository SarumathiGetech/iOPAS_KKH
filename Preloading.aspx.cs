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
using System.Text;
using System.Drawing;

public partial class Preloading : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
   Preload pre = new Preload();
   string sessionuserid="", location="",Editstatus= "",Actstatus="",DDSName="";   
   //public static string loadingid = "",sortid="";
   int Rtnvalue;
   protected void Page_Load(object sender, EventArgs e)
   {      
       if (Session["Userid"] != null)
       {
           sessionuserid = Session["Userid"].ToString();
           location = Session["location"].ToString();
           if (location != "")
           {
               txtcartno.Attributes.Add("onKeyPress", "doClick1(event)");              

               if (!IsPostBack)
               {
                   //btnpreloadsearch.Attributes.Add("onclick", "preload() ;return false;");
                   string scriptStr = "javascript:return popUpCalendar(this," + getClientID() + @", 'dd/mm/yyyy', '__doPostBack(\'" + getClientID() + @"\')')";
                   imgCalendar.Attributes.Add("onclick", scriptStr);
                   btnupdate.Visible = false;
                   gridreject.PageIndex = 0;
                   preloadgrid();
                   //sortid = "1";
                   ViewState["sortid"] = "1";
                   txtcartno.Focus();
                   txtexpdate.Attributes.Add("onKeyPress", "doClick(event)");
                   txtbatchno.Attributes.Add("onKeyPress", "doClick(event)");
                   
                   tdone.Visible = true;
                   tdtwo.Visible = true;
                   tdthree.Visible = true;
                   tdfour.Visible = false;

                   btnempenterupdate.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnempenterupdate, null) + ";");
                   btnempenter.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnempenter, null) + ";");
               }
           }
           else if (location == "" || location == null)
           {
               //txtcartno.ReadOnly = true;
               // btnpreloadsearch.Visible = false;
               pp.Visible = false;
               ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Workstation is not authorised to do PreLoading');</script>", false);
               return;
           }
       }
       else
       {
           //Response.Redirect("opas.html?Session=End");
           Response.Redirect("iopas.html");
       }     
   }

   public string getClientID()
   {
       return txtexpdate.ClientID;
   }
    //protected void Btngo_Click(object sender, EventArgs e)
   protected void Btngo_Click(object sender, ImageClickEventArgs e)
    {
        
        Rtnvalue = pre.Preloadvalidate(txtcartno.Text.Trim(), location, "Preloading");
        if (Rtnvalue == 1)
        {            
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popup", "Preloadcartridge()", true);            
        }
        else if (Rtnvalue == 5)
        {            
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid Cartridge Number');</script>", false);
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
        else if (Rtnvalue == 8)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Inventory must be closed at Cartridge Unloading before preloading');</script>", false);
            textboxclear();
            txtcartno.Text = "";
            txtcartno.Focus();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occured');</script>", false);
        }
    }  
   
    protected void btnok_Click(object sender, EventArgs e)
    {
        //textboxclear();
        txtqty.Text = "";
        txtexpdate.Text = "";
        txtbatchno.Text = "";

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select u.Item_Code,u.Drug_Code,u.Item_Name,b.brandname,pm.PackType,um.Pack_Size,l.UOM,um.Max_Cartridge_Qty from Item_user_Master as um left join Item_Master as u on u.MasterID=um.MasterID left join Item_Location as l on l.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=um.Brandid left join Packtype_Master as pm on pm.ID=UM.PacktypeID where um.ID='" + Convert.ToInt32(searchvalue.Text.Trim()) + "'";
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
                        txtdrug.Text = dr[2].ToString();
                        txtbrand.Text = dr[3].ToString();
                        txtpacktype.Text = dr[4].ToString();
                        txtpacksize.Text = dr[5].ToString();
                        txtuom.Text = dr[6].ToString();
                        txtmaxcartqty.Text = dr[7].ToString();
                    }
                }
            }
        }


        lblmax.Text = txtpacktype.Text + " " + "of" + " "+txtpacksize.Text + " "+ txtuom.Text;
        lblload.Text = txtpacktype.Text + " " + "of" + " " + txtpacksize.Text + " " + txtuom.Text; 
    }

   // protected void btnsave_Click(object sender, EventArgs e)
    protected void btnsave_Click(object sender, ImageClickEventArgs e)
    {
        Rtnvalue = pre.Preloadvalidate(txtcartno.Text.Trim(), location, "Preloading");
        if (Rtnvalue == 1)
        {
            tdone.Visible = false;
            tdtwo.Visible = false;
            tdthree.Visible = false;
            tdfour.Visible = true;
            btnempenter.Visible = true;
            btnempenterupdate.Visible = false;
            txtempid.Text = "";
            txtempid.Focus();
        }
        else if (Rtnvalue == 5)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid Cartridge Number');</script>", false);
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
        else if (Rtnvalue == 8)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Inventory must be closed at Cartridge Unloading before preloading');</script>", false);
            textboxclear();
            txtcartno.Text = "";
            txtcartno.Focus();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occured');</script>", false);
        }
    }

// * Cartridge Preloading Insert Function * \\
    public void preloadinginsert()
    {       
       int rtnvalue = pre.preloadinginsert(Convert.ToInt32(searchvalue.Text.Trim()), txtcartno.Text.ToUpper().Trim(), txtitemcode.Text.Trim(), txtbrand.Text.Trim(), txtbatchno.Text.Trim(), Convert.ToInt32(txtqty.Text.Trim()),txtexpdate.Text.Trim(), sessionuserid = Session["Userid"].ToString(),location);
       if (rtnvalue == 2)
       {
           textboxclear();
           txtcartno.Text = "";
           preloadgrid();
           ViewState["sortid"] = "1";
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Loaded successfully');</script>", false);
       }
       else if (rtnvalue == 5)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Expiry date must be more than Minimum Drug Expiry for Loading ');</script>", false);
       }
       else if (rtnvalue == 6)
       {

           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Quantity loaded exceeds Maximum Quantity per Cartridge');</script>", false);
       }
       else if (rtnvalue == 7)
       {
          
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Item Effective Date To should not be earlier than current date');</script>", false);
       }
       else if (rtnvalue == 8)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Store Effective Date To should not be earlier than current date');</script>", false);
       }
       else if (rtnvalue == 9)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Inventory must be closed at Cartridge Unloading before preloading');</script>", false);
       }
       else if (rtnvalue == 39)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Item inactivated in drug master');</script>", false);
       }
       else
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
       }
    }

    // * Cartridge Preloading Update Function * \\
    public void preloadingupdate()
    {
        int rtnvalue = pre.preloadingupdate(Convert.ToInt64(ViewState["loadingid"].ToString()), txtcartno.Text.ToUpper().Trim(), txtitemcode.Text.Trim(), Convert.ToInt32(txtqty.Text.Trim()), txtexpdate.Text.Trim(), sessionuserid = Session["Userid"].ToString(), location, txtbatchno.Text.Trim());
        if (rtnvalue == 2)
        {       
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Loaded successfully');</script>", false);
            textboxclear();
            txtcartno.Text = "";
            preloadgrid();
            btnupdate.Visible = false;
            ViewState["sortid"] = "1";
        }
        else if (rtnvalue == 5)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Expiry date must be more than Minimum Drug Expiry for Loading');</script>", false);
            textboxclear();
            txtcartno.Text = "";
            preloadgrid();
            btnupdate.Visible = false;
        }
        else if (rtnvalue == 6)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Quantity loaded exceeds Maximum Quantity per Cartridge');</script>", false);
            textboxclear();
            txtcartno.Text = "";
            preloadgrid();
            btnupdate.Visible = false;
        }
        else if (rtnvalue == 7)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Item Effective Date To should not be earlier than current date');</script>", false);
            textboxclear();
            txtcartno.Text = "";
            preloadgrid();
            btnupdate.Visible = false;
        }
        else if (rtnvalue == 8)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Store Effective Date To should not be earlier than current date');</script>", false);
            textboxclear();
            txtcartno.Text = "";
            preloadgrid();
            btnupdate.Visible = false;
        }
        else if (rtnvalue == 20)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Verification Done');</script>", false);
            textboxclear();
            txtcartno.Text = "";
            preloadgrid();
            btnupdate.Visible = false;
        }
        else if (rtnvalue == 39)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Item inactivated in drug master');</script>", false);
            textboxclear();
            txtcartno.Text = "";
            preloadgrid();
            btnupdate.Visible = false;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
            textboxclear();
            txtcartno.Text = "";
            preloadgrid();
            btnupdate.Visible = false;
        }
    }    

    // * Textbox Clear Function * \\
    public void textboxclear()
    {        
        txtdrugcode.Text="";
        txtitemcode.Text="";
        txtdrug.Text="";
        txtuom.Text="";
        txtbrand.Text="";
        txtmaxcartqty.Text = "";
        txtbatchno.Text="";
        txtpacktype.Text="";
        txtpacksize.Text="";
        txtqty.Text = "";
        txtexpdate.Text = "";      
        btnupdate.Visible = false;
        btnsave.Visible = true;       
        txtcartno.ReadOnly = false;
        txtcartno.Focus();
        //preloadgrid();
    }
    
    // * Preloading Grid Display * \\
    public void preloadgrid()
    {        
        pre.preloadgrid(gridreject, sessionuserid = Session["Userid"].ToString(),location);
        gridreject.Visible = true;
        DataSet dsData = gridreject.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(gridreject.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridreject.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;                       
        }
    }    
   
    protected void gridreject_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        textboxclear();
        txtcartno.Text = "";      
        ViewState["loadingid"]=0;     
        //loadingid = (string)gridreject.DataKeys[e.NewSelectedIndex].Value.ToString();
        ViewState["loadingid"] = (string)gridreject.DataKeys[e.NewSelectedIndex].Value.ToString();    
        Editstatuscheck();
        if (Editstatus == "First Rejected" || Editstatus == "Second Rejected" || Editstatus == "Loaded")
        {
            if (Actstatus == "1")
            {
                txtcartno.Text = (gridreject.Rows[e.NewSelectedIndex].Cells[2].Text);
                txtitemcode.Text = (gridreject.Rows[e.NewSelectedIndex].Cells[3].Text);
                txtdrug.Text = (gridreject.Rows[e.NewSelectedIndex].Cells[4].Text);
                txtbrand.Text = (gridreject.Rows[e.NewSelectedIndex].Cells[5].Text);
                txtpacktype.Text = (gridreject.Rows[e.NewSelectedIndex].Cells[6].Text);
                txtpacksize.Text = (gridreject.Rows[e.NewSelectedIndex].Cells[7].Text);
                txtuom.Text = (gridreject.Rows[e.NewSelectedIndex].Cells[8].Text);
                rejectedcartridge();
                txtcartno.ReadOnly = true;
                btnupdate.Visible = true;
                btnsave.Visible = false;
                lblmax.Text = txtpacktype.Text + " " + "of" + " " + txtpacksize.Text + " " + txtuom.Text;
                lblload.Text = txtpacktype.Text + " " + "of" + " " + txtpacksize.Text + " " + txtuom.Text;
            }
            else if (Actstatus != "1")
            {
                if (DDSName == "")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge removed or DDS error. Unload cartridge in Loading menu and preload again');</script>", false);
                }
                else if (DDSName != "")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge is loaded into DDS');</script>", false);
                }
            }
        }
        else if (Editstatus == "Unloading")
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge already unloaded');</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Verification already done');</script>", false);
        }
    }

    // * Rejected Cartridge Details Reader * \\
    public void rejectedcartridge()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select l.batch_no,Quantity,convert (varchar,l.Expiry_Date,103),i.Drug_Code,um.Max_Cartridge_Qty from Cartridge_Loading as l left join Item_user_Master as um on um.ID= l.IUM_ID left join Item_Master as i on i.MasterID=um.MasterID where l.Loading_Id='" + Convert.ToInt64(ViewState["loadingid"].ToString()) + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtbatchno.Text = dr[0].ToString();
                        txtqty.Text = dr[1].ToString();
                        txtexpdate.Text = dr[2].ToString();
                        txtdrugcode.Text = dr[3].ToString();
                        txtmaxcartqty.Text = dr[4].ToString();
                    }
                }
            }
        }

    }
// * Edit Status Checking Function * \\
    public void Editstatuscheck()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select Verified_Status,Activation_Status,DDS_Name  from Cartridge_Loading where Loading_Id='" + Convert.ToInt64(ViewState["loadingid"].ToString()) + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Editstatus = dr[0].ToString();
                        Actstatus = dr[1].ToString();
                    }
                }
            }
        }

    }

    //protected void btnupdate_Click(object sender, EventArgs e)
    protected void btnupdate_Click(object sender, ImageClickEventArgs e)
    {        
        Rtnvalue = pre.Preloadvalidate(txtcartno.Text.Trim(), location, "Preloading");
        if (Rtnvalue == 1 || Rtnvalue == 8)
        {
            tdone.Visible = false;
            tdtwo.Visible = false;
            tdthree.Visible = false;
            tdfour.Visible = true;
            btnempenter.Visible = false;
            btnempenterupdate.Visible = true;
            txtempid.Text = "";
            txtempid.Focus();
                    
        }
        else if (Rtnvalue == 5)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid Cartridge Number');</script>", false);
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
        //else if (Rtnvalue == 8)
        //{
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Inventory must be closed at Cartridge Unloading before preloading');</script>", false);
        //    textboxclear();
        //    txtcartno.Text = "";
        //    txtcartno.Focus();
        //}
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occured');</script>", false);
        }      
             
    }
   // protected void btn_Click(object sender, EventArgs e)
      protected void btn_Click(object sender, ImageClickEventArgs e)
    {
        gridreject.PageIndex = 0;
        textboxclear();
        txtcartno.Text = "";
        preloadgrid();
        ViewState["sortid"] = "1";

        tdone.Visible = true;
        tdtwo.Visible = true;
        tdthree.Visible = true;
        tdfour.Visible = false;
    }      

    protected void btnpresearch_Click(object sender, EventArgs e)
    {
        gridreject.PageIndex = 0;
        //searchquery();
        ViewState["sortid"] = "2";
    }

    //public void searchquery()
    //{
    //    txtcartno.ReadOnly = false;
    //    if (txtfilter.Text == "Unverified PreLoading" && txtitcodesearch.Text.Trim() != "")
    //    {
    //        pre.itcodeloadedcart(gridreject, Session["Userid"].ToString(), txtitcodesearch.Text.Trim(), location);
    //        textboxclear();
    //        txtcartno.Text = "";
    //    }
    //    else if (txtfilter.Text == "Rejected PreLoading" && txtitcodesearch.Text.Trim() != "")
    //    {
    //        gridreject.Visible = true;
    //        pre.itcodepreloadreject(gridreject, Session["Userid"].ToString(), txtitcodesearch.Text.Trim(), location);
    //        textboxclear();
    //        txtcartno.Text = "";
    //    }
    //    else if (txtfilter.Text == "All" && txtitcodesearch.Text.Trim() != "")
    //    {
    //        pre.itcodeloadandrejec(gridreject, Session["Userid"].ToString(), txtitcodesearch.Text.Trim(), location);
    //        textboxclear();
    //        txtcartno.Text = "";
    //    }
    //    else if (txtfilter.Text == "All" && txtdatval.Text.Trim() != "")
    //    {
    //        pre.preloadeddateall(gridreject, Session["Userid"].ToString(), txtdatval.Text.Trim(), location);
    //    }

    //    else if (txtfilter.Text == "Unverified PreLoading" && txtdatval.Text.Trim() != "")
    //    {
    //        pre.preloaddateunverified(gridreject, Session["Userid"].ToString(), txtdatval.Text.Trim(), location);
    //    }
    //    else if (txtfilter.Text == "Rejected PreLoading" && txtdatval.Text.Trim() != "")
    //    {
    //        pre.preloaddatereject(gridreject, Session["Userid"].ToString(), txtdatval.Text.Trim(), location);
    //    }
    //    else if (txtcartno.Text.Trim() != "")
    //    {
    //        pre.preloadcartnosearch(gridreject, Session["Userid"].ToString(), txtcartno.Text.Trim(), location);
    //        textboxclear();
    //        txtcartno.Text = "";
    //    }
    //    else if (txtmfrsearch.Text.Trim() != "" && txtfilter.Text.Trim() == "All")
    //    {
    //        pre.preloadmfrcodesearch(gridreject, Session["Userid"].ToString(), txtmfrsearch.Text.Trim(), location);
    //    }
    //    else if (txtmfrsearch.Text.Trim() != "" && txtfilter.Text.Trim() == "Unverified PreLoading")
    //    {
    //        pre.preloadmfrcodeloaded(gridreject, Session["Userid"].ToString(), txtmfrsearch.Text.Trim(), location);
    //    }
    //    else if (txtmfrsearch.Text.Trim() != "" && txtfilter.Text.Trim() == "Rejected PreLoading")
    //    {
    //        pre.preloadmfrcoderejected(gridreject, Session["Userid"].ToString(), txtmfrsearch.Text.Trim(), location);
    //    }

    //    else if (txtitemname.Text.Trim() != "" && txtfilter.Text.Trim() == "Unverified PreLoading")
    //    {
    //        pre.preloaditemnameloaded(gridreject, Session["Userid"].ToString(), txtitemname.Text.Trim(), location);
    //    }
    //    else if (txtitemname.Text.Trim() != "" && txtfilter.Text.Trim() == "Rejected PreLoading")
    //    {
    //        pre.preloaditemnamerejected(gridreject, Session["Userid"].ToString(), txtitemname.Text.Trim(), location);
    //    }
    //    else if (txtitemname.Text.Trim() != "" && txtfilter.Text.Trim() == "All")
    //    {
    //        pre.preloaditemnameallsearch(gridreject, Session["Userid"].ToString(), txtitemname.Text.Trim(), location);
    //    }
    //    else if (txtserdrgcode.Text.Trim() != "" && txtfilter.Text.Trim() == "All")
    //    {
    //        pre.preloaddrugcodeallsearch(gridreject, Session["Userid"].ToString(), txtserdrgcode.Text.Trim(), location);
    //    }
    //    else if (txtserdrgcode.Text.Trim() != "" && txtfilter.Text.Trim() == "Unverified PreLoading")
    //    {
    //        pre.preloaddrugcodeloaded(gridreject, Session["Userid"].ToString(), txtserdrgcode.Text.Trim(), location);
    //    }
    //    else if (txtserdrgcode.Text.Trim() != "" && txtfilter.Text.Trim() == "Rejected PreLoading")
    //    {
    //        pre.preloaddrugcoderejected(gridreject, Session["Userid"].ToString(), txtserdrgcode.Text.Trim(), location);
    //    }
    //    else if (txtbrandsear.Text.Trim() != "" && txtfilter.Text.Trim() == "Rejected PreLoading")
    //    {
    //        pre.preloadbrandrejected(gridreject, Session["Userid"].ToString(), txtbrandsear.Text.Trim(), location);

    //    }
    //    else if (txtbrandsear.Text.Trim() != "" && txtfilter.Text.Trim() == "Unverified PreLoading")
    //    {
    //        pre.Preloadbrandloaded(gridreject, Session["Userid"].ToString(), txtbrandsear.Text.Trim(), location);
    //    }
    //    else if (txtbrandsear.Text.Trim() != "" && txtfilter.Text.Trim() == "All")
    //    {
    //        pre.Preloadbrandall(gridreject, Session["Userid"].ToString(), txtbrandsear.Text.Trim(), location);
    //    }
    //    else
    //    {
    //        pre.preloadgrid(gridreject, "No", location);
    //        //gridreject.Visible = false;
    //    }
    //    DataSet dsData = gridreject.DataSource as DataSet;
    //    DataTable dtData = dsData.Tables[0];
    //    lblpge.Text = "Page" + "  " + Convert.ToString(gridreject.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridreject.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
    //    lblpge.ForeColor = Color.Black;
    //    lblpge.Font.Bold = false;
    //    if (dtData.Rows.Count == 0)
    //    {
    //        lblpge.Text = "No Record Found";
    //        lblpge.Font.Bold = true;
    //        lblpge.ForeColor = Color.Green;
    //    }     
    //}

    protected void btnpreloadsearch_Click(object sender, EventArgs e)
    {
        //textboxclear();
        ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popup", "preload()", true);
    }
    protected void gridreject_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (ViewState["sortid"].ToString() == "1")
        {
            preloadgrid();
        }
        else if (ViewState["sortid"].ToString() == "2")
        {
            //searchquery();
        }
        gridreject.PageIndex = e.NewPageIndex;
        gridreject.DataBind();
        DataSet dsData = gridreject.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(gridreject.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridreject.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";       
    }

    protected void gridreject_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (ViewState["sortid"].ToString() == "1")
        {
            preloadgrid();
        }
        else if (ViewState["sortid"].ToString() == "2")
        {
            //searchquery();
        }
        DataSet dsData = gridreject.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = gridreject.PageIndex;
        gridreject.DataSource = SortDataTable(dtData, false);
        gridreject.DataBind();
        gridreject.PageIndex = pageIndex;
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
    //protected void btnempenter_Click(object sender, EventArgs e)
    protected void btnempenter_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["EmpID"].ToString().ToLower() == txtempid.Text.Trim().ToLower())
        {
            tdone.Visible = true;
            tdtwo.Visible = true;
            tdthree.Visible = true;
            tdfour.Visible = false;
            preloadinginsert();           
        }

        else if (Session["EmpID"].ToString().ToLower() != txtempid.Text.Trim().ToLower())
        {
            tdone.Visible = true;
            tdtwo.Visible = true;
            tdthree.Visible = true;
            tdfour.Visible = false;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('You are not authorised to do PreLoading. NRIC / Staff ID/ Pass Mismatch');</script>", false);
        }
       
    }
    //protected void btnempenterreject_Click(object sender, EventArgs e)
    protected void btnempenterreject_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["EmpID"].ToString().ToLower() == txtempid.Text.Trim().ToLower())
        {
            tdone.Visible = true;
            tdtwo.Visible = true;
            tdthree.Visible = true;
            tdfour.Visible = false;
            preloadingupdate();  
        }

        else if (Session["EmpID"].ToString().ToLower() != txtempid.Text.Trim().ToLower())
        {
            tdone.Visible = true;
            tdtwo.Visible = true;
            tdthree.Visible = true;
            tdfour.Visible = false;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('You are not authorised to do PreLoading. NRIC / Staff ID/ Pass Mismatch');</script>", false);
        }
            
    }
   
}
