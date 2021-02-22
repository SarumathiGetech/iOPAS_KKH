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
using System.Drawing;
public partial class Secondverification : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    Preload pre = new Preload();
    string sessionuserid="", location  = "";
    //public static int loadingid;      
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sessionuserid = Session["Userid"].ToString();
            location = Session["location"].ToString();
            if (!IsPostBack)
            {
                gridsecond.PageIndex = 0;
                errorcode();
                secondverficationgrid();

                tdone.Visible = true;
                tdtwo.Visible = true;
                tdthree.Visible = false;
            }

            if (location != "")
            {
                txtcartno.Focus();
            }

            else if (location == "" || location == null)
            {
                //txtcartno.ReadOnly = true;
                pp.Visible = false;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Workstation is not authorised to do Verification');</script>", false);
                return;
            }
        }
        catch (NullReferenceException)
        {
            //Response.Redirect("opas.html?Session=End");
            Response.Redirect("iopas.html");
        }       
    }

// * Second Verification Reader Function * \\
    public void secondverification()
    {

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select p.Loading_id,i.Drug_Code,i.Item_Code,i.Item_Name,IL.UOM,b.Brandname,p.Batch_No,pm.PackType,UM.Pack_Size,p.Quantity,UM.Max_Cartridge_Qty,convert (varchar,p.[Expiry_Date],103),l.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.verified_by,convert(varchar(20), f.verified_date,113) as Verified_Date from Cartridge_Loading as p left join Loaded_by as l on  p.loading_id=l.Loading_Id left join first_verification as f on p.loading_id=f.loading_id left join Brand_Master as b on b.BrandID=p.Brandid left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID = UM.MasterID left join Item_Location as IL on IL.MasterID=i.MasterID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Verified_Status='First Verified' and  p.Inventory_Status='2' and p.cartridge_id ='" + txtcartno.Text.Trim() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        // loadingid = Convert.ToInt32(dr[0].ToString());
                        ViewState["loadingid"] = Convert.ToInt64(dr[0].ToString());
                        txtdrugcode.Text = dr[1].ToString();
                        txtitemcode.Text = dr[2].ToString();
                        txtitemname.Text = dr[3].ToString();
                        txtuom.Text = dr[4].ToString();
                        txtbrand.Text = dr[5].ToString();
                        txtbatchno.Text = dr[6].ToString();
                        txtpacktype.Text = dr[7].ToString();
                        txtpacksize.Text = dr[8].ToString();
                        txtqty.Text = dr[9].ToString();
                        txtmaxcartqty.Text = dr[10].ToString();
                        txtexpdate.Text = dr[11].ToString();
                        txtlodedby.Text = dr[12].ToString();
                        txtdate.Text = dr[13].ToString();
                        txtfstby.Text = dr[14].ToString();
                        txtfstdate.Text = dr[15].ToString();
                    }
                }
            }
        }

        lblmax.Text = txtpacktype.Text + " " + "of" + " " + txtpacksize.Text + " " + txtuom.Text;
        lblload.Text = txtpacktype.Text + " " + "of" + " " + txtpacksize.Text + " " + txtuom.Text; 
    }

    //protected void Btngo_Click(object sender, EventArgs e)
    protected void Btngo_Click(object sender, ImageClickEventArgs e)
    {
        int Rtnvalue = pre.Preloadvalidate(txtcartno.Text.Trim(), location,"First Verified");
        if (Rtnvalue == 1 || Rtnvalue == 8)
        {
            textclear();
            secondverification();
        }
        else if (Rtnvalue == 5)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid Cartridge Number');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
        }
        else if (Rtnvalue == 6)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Cartridge does not belong to this pharmacy ');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
        }
        else if (Rtnvalue == 7)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Cartridge is Inactive');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
        }
        else if (Rtnvalue == 10)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('First verification not done');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
        }
        else if (Rtnvalue == 12 )
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Second verification done');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
        }
        else if (Rtnvalue == 13)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge failed first verification');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
        }
        else if (Rtnvalue == 14)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge failed second verification');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
        }
        else if (Rtnvalue == 15)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Cartridge is not PreLoaded');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
        }
        else if (Rtnvalue == 16)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge is loaded into DDS');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
        }
        else if (Rtnvalue == 31)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge removed or DDS error.Unload cartridge in Loading menu and preload again');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occurred');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
        }    
    }
    // * Textbox Clear Function * \\
    public void textclear()
    {
        txtdrugcode.Text = "";
        txtitemcode.Text = "";
        txtitemname.Text = "";
        txtuom.Text = "";
        txtbrand.Text = "";
        txtmaxcartqty.Text = "";
        txtbatchno.Text = "";
        txtpacktype.Text = "";
        txtpacksize.Text = "";
        txtqty.Text = "";
        txtexpdate.Text = "";
        txtlodedby.Text = "";
        txtdate.Text = "";
        txtfstby.Text = "";
        txtfstdate.Text = "";
        ViewState["loadingid"] = 0;
    }
    // * Grid Display Function * \\
    public void secondverficationgrid()
    {
        pre.secondverficationgrid(gridsecond,sessionuserid = Session["Userid"].ToString(),location);
        DataSet dsData = gridsecond.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(gridsecond.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridsecond.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
           
        }
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

    //protected void btnaccept_Click(object sender, EventArgs e)
    protected void btnaccept_Click(object sender, ImageClickEventArgs e)
    {
        //if (ddlreason.SelectedValue.ToString() == "-Blank-")
        //{
        int Rtnvalue = pre.Preloadvalidate(txtcartno.Text.Trim(), location,"First Verified");
        if (Rtnvalue == 1 || Rtnvalue == 8)
        {
            gridsecond.PageIndex = 0;
            textclear();
            secondverification();
            if ((Int64)ViewState["loadingid"] != 0)
            {
                tdone.Visible = false;
                tdtwo.Visible = false;
                tdthree.Visible = true;
                btnempenter.Visible = true;
                btnempenterreject.Visible = false;
                txtempid.Text = "";
                txtempid.Focus();

                //Acceptinsert();
                //errorcode();
            }
            else if ((Int64)ViewState["loadingid"] == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid Cartridge Number');</script>", false);
                txtcartno.Text = "";
                txtcartno.Focus();
                textclear();
                
                secondverficationgrid();                
            }
        }
        else if (Rtnvalue == 5)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid Cartridge Number');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
            secondverficationgrid();
        }
        else if (Rtnvalue == 6)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Cartridge does not belong to this pharmacy ');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
            secondverficationgrid();
        }
        else if (Rtnvalue == 7)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Cartridge is Inactive');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
            secondverficationgrid();
        }
        else if (Rtnvalue == 10)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('First verification not done');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
            secondverficationgrid();
        }
        else if (Rtnvalue == 12)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Second verification done');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
            secondverficationgrid();
        }
        else if (Rtnvalue == 13 )
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge failed first verification');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
            secondverficationgrid();
        }
        else if ( Rtnvalue == 14)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge failed second verification');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
            secondverficationgrid();
        }
        else if (Rtnvalue == 15)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Cartridge is not PreLoaded');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
            secondverficationgrid();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occurred');</script>", false);
        }
        
        //}
        //else if (ddlreason.SelectedValue.ToString() != "-Blank-")
        //{
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Select reason and press Reject button');</script>", false);
        //}
    }
    // FIRST VERIFICATION ACCEPT INSERT \\
    public void Acceptinsert()
    {
        int Rtnvalue = pre.secondaccept(txtcartno.Text.Trim(), (Int64)ViewState["loadingid"], txtitemcode.Text.Trim(), Convert.ToInt32(txtqty.Text.Trim()), txtexpdate.Text.Trim(), location, sessionuserid = Session["Userid"].ToString());

      if (Rtnvalue == 2)
      {
          textclear();
          secondverficationgrid();
          txtcartno.Text = "";
          ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge verified');</script>", false);
      }
      else if (Rtnvalue == 3)
      {
          ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge already verified');</script>", false);
      }
      else if (Rtnvalue == 5)
      {
          ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Expiry date must be more than Minimum Drug Expiry for Loading');</script>", false);
      }
      else if (Rtnvalue == 6)
      {
          ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Quantity loaded exceeds Maximum Quantity per Cartridge');</script>", false);
      }
      else if (Rtnvalue == 7)
      {
          ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Item Effective Date To should not be earlier than current date');</script>", false);
      }
      else if (Rtnvalue == 8)
      {
          ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Store Effective Date To should not be earlier than current date');</script>", false);
      }
      else if (Rtnvalue == 39)
      {
          ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Item inactivated in drug master');</script>", false);
      }
      else
      {
          ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occurred');</script>", false);
      }
    }

    //protected void btnreject_Click(object sender, EventArgs e)
    protected void btnreject_Click(object sender, ImageClickEventArgs e)
    {
        gridsecond.PageIndex = 0;
        int Rtnvalue = pre.Preloadvalidate(txtcartno.Text.Trim(), location, "First Verified");
        if (Rtnvalue == 1 || Rtnvalue == 8)
        {
            tdone.Visible = false;
            tdtwo.Visible = false;
            tdthree.Visible = true;
            btnempenter.Visible = false;
            btnempenterreject.Visible = true;
            txtempid.Text = "";
            txtempid.Focus();
            //Reject();
            //errorcode();          
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
            textclear();
        }
        else if (Rtnvalue == 7)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Cartridge is Inactive');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
        }
        else if (Rtnvalue == 10)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('First verification not Done');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
        }
        else if (Rtnvalue == 12)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Second verification done');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
        }
        else if (Rtnvalue == 13 || Rtnvalue == 14)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Cartridge already rejected');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
        }
        else if (Rtnvalue == 15)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Cartridge is not PreLoaded');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occurred');</script>", false);
        }       
    }
    protected void ddlcartlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        secondverficationgrid();
    }
    //protected void Button2_Click(object sender, EventArgs e)
    protected void Button2_Click(object sender, ImageClickEventArgs e)
    {
        textclear();
        txtcartno.Text = "";
        errorcode();
        tdone.Visible = true;
        tdtwo.Visible = true;
        tdthree.Visible = false;
    }

    // * Second Verification Reject * \\
    public void Reject()
    {
        int Rtnvalue = pre.secondreject(txtcartno.Text.Trim(), (Int64)ViewState["loadingid"], sessionuserid = Session["Userid"].ToString(), ddlreason.SelectedItem.ToString(), location);
        if (Rtnvalue == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge rejected');</script>", false);
            textclear();
            secondverficationgrid();
            txtcartno.Text = "";
        }
        else if (Rtnvalue == 2)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid Cartridge number');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
        }
        else if (Rtnvalue == 3)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge already rejected');</script>", false);
            textclear();
            secondverficationgrid();
            txtcartno.Text = "";
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occurred');</script>", false);
            txtcartno.Text = "";
            txtcartno.Focus();
            textclear();
        }
    }
    protected void gridsecond_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gridsecond_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        secondverficationgrid();
        gridsecond.PageIndex = e.NewPageIndex;
        gridsecond.DataBind();
        DataSet dsData = gridsecond.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(gridsecond.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(gridsecond.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";       
    }
    protected void gridsecond_Sorting(object sender, GridViewSortEventArgs e)
    {
        secondverficationgrid();
        DataSet dsData = gridsecond.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = gridsecond.PageIndex;
        gridsecond.DataSource = SortDataTable(dtData, false);
        gridsecond.DataBind();
        gridsecond.PageIndex = pageIndex; 
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

    protected void ddlreason_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void btnempenter_Click(object sender, EventArgs e)
    protected void btnempenter_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["EmpID"].ToString().ToLower() == txtempid.Text.Trim().ToLower())
        {
            tdone.Visible = true;
            tdtwo.Visible = true;
            tdthree.Visible = false;

            Acceptinsert();
            errorcode();
            chkitemname.Checked = false;
            chkbrandname.Checked = false;
            chkpacktype.Checked = false;
            chkpacksize.Checked = false;
            chkexpdate.Checked = false;
        }

        else if (Session["EmpID"].ToString().ToLower() != txtempid.Text.Trim().ToLower())
        {
            tdone.Visible = true;
            tdtwo.Visible = true;
            tdthree.Visible = false;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('You are not authorised to do Verification. NRIC / Staff ID/ Pass Mismatch');</script>", false);
        }
    }
    //protected void btnempenterreject_Click(object sender, EventArgs e)
    protected void btnempenterreject_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["EmpID"].ToString().ToLower() == txtempid.Text.Trim().ToLower())
        {
            tdone.Visible = true;
            tdtwo.Visible = true;
            tdthree.Visible = false;

            Reject();
            errorcode();

            chkitemname.Checked = false;
            chkbrandname.Checked = false;
            chkpacktype.Checked = false;
            chkpacksize.Checked = false;
            chkexpdate.Checked = false;
        }

        else if (Session["EmpID"].ToString().ToLower() != txtempid.Text.Trim().ToLower())
        {
            tdone.Visible = true;
            tdtwo.Visible = true;
            tdthree.Visible = false;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('You are not authorised to do Verification. NRIC / Staff ID/ Pass Mismatch');</script>", false);
        }
    }
}

