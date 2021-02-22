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


public partial class BottlePreLoading : System.Web.UI.Page
{
     string sessionuserid="", location="",barcode="";
     DB_Connection DBCon = new DB_Connection();
     SqlCommand cmd = new SqlCommand();
     BDSBottle BDS = new BDSBottle();
     Print pri = new Print();
     int Rtnval;
     DataSet dsData = new DataSet();
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
                    string scriptStr = "javascript:return popUpCalendar(this," + getClientID() + @", 'dd/mm/yyyy', '__doPostBack(\'" + getClientID() + @"\')')";
                    imgCalendar.Attributes.Add("onclick", scriptStr);

                    ViewState["ord"] = "order by p.CartID desc";

                    txtexpdate.Attributes.Add("onKeyPress", "doClick(event)");
                    txtbatchno.Attributes.Add("onKeyPress", "doClick(event)");
                    BtnSearch.Attributes.Add("onclick", "itemsearch() ;return false;");
                    btnupdate.Visible = false;
                    btncancel.Visible = false;
                    GridDisplay();
                    tdone.Visible = true;
                    tdtwo.Visible = false;
                    BtnSearch.Visible = true;
                    defaultprinter();

               
                    btnempenterupdate.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnempenterupdate, null) + ";");
                    btnempenter.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnempenter, null) + ";");
                }
            }
            else if (location == "" || location == null)
            {
                pp.Visible = false;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Workstation is not authorised to do PreLoading');</script>", false);
                return;
            }
        }
        else
        {
            Response.Redirect("iopas.html");
        }
           
       }
       

   public string getClientID()
   {
       return txtexpdate.ClientID;
   }

   protected void btnok_Click(object sender, EventArgs e)
   {
       //textboxclear();
       //txtqty.Text = "";
       txtexpdate.Text = "";
       txtbatchno.Text = ""; 

       using (SqlConnection con = DBCon.getstring())
       {
           string Commt = "select u.Item_Code,u.Drug_Code,u.Item_Name,b.brandname,pm.PackType,um.Pack_Size,l.UOM,um.Max_Cartridge_Qty from Item_user_Master as um left join Item_Master as u on u.MasterID=um.MasterID left join Item_Location as l on l.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=um.Brandid left join Packtype_Master as pm on pm.ID=UM.PacktypeID  where um.ID='" + Convert.ToInt32(searchvalue.Text.Trim()) + "'";
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
                       txtcartonbox.Text = txtcarttype.Text.Trim();
                       TxtBoxOrPallet.Text = txt_Box_Or_Pallet.Text.Trim();
                        //txtcartonbox.Text = dr[8].ToString();
                        txtexpdate.Text = txtexpdate1.Text.Trim();
                        txtbatchno.Text = txtbatchno1.Text.Trim();
                    }

               }
           }
       }


       lblmax.Text = txtpacktype.Text.Trim() + " " + "of" + " " + txtpacksize.Text.Trim() + " " + txtuom.Text.Trim();
      // lblload.Text = txtpacktype.Text.Trim() + " " + "of" + " " + txtpacksize.Text.Trim() + " " + txtuom.Text.Trim();
       lblcartboxof.Text = txtpacktype.Text.Trim();
   }
   protected void btnsave_Click(object sender, ImageClickEventArgs e)
   {
       tdone.Visible = false;
       tdtwo.Visible = true;
       btnempenter.Visible = true;
       btnempenterupdate.Visible = false;
       BtnempenterCancel.Visible = false;
       txtempid.Text = "";
       txtempid.Focus();
   }

   protected void btnupdate_Click(object sender, ImageClickEventArgs e)
   {
       tdone.Visible = false;
       tdtwo.Visible = true;
       txtempid.Text = "";
       txtempid.Focus();
       btnempenter.Visible = false;
       btnempenterupdate.Visible = true;
       BtnempenterCancel.Visible = false;      
   }

   protected void btncancel_Click(object sender, ImageClickEventArgs e)
   {
       tdone.Visible = false;
       tdtwo.Visible = true;
       txtempid.Text = "";
       txtempid.Focus();
       btnempenter.Visible = false;
       btnempenterupdate.Visible = false;
       BtnempenterCancel.Visible = true;
       
   }

   protected void btnclear_Click(object sender, ImageClickEventArgs e)
   {
       clear();
       GridDisplay();      
   }

   void PreloadingInsert()
   {
       Rtnval = BDS.BDS_Bot_Preloading_Insert(location,Convert.ToInt32(searchvalue.Text.Trim()), Convert.ToInt32(txtmaxcartqty.Text), Convert.ToInt32(txtcartonbox.Text), txtexpdate.Text.Trim(), txtbatchno.Text, Session["Userid"].ToString(),TxtBoxOrPallet.Text.Trim(), out barcode);
  
       if (Rtnval == 1)
       {
           pri.PrintCartonBoxBarcode(barcode.Trim(), txtdrugname.Text.Trim(), ddlprintername.SelectedValue.ToString());
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Loaded successfully   " + barcode + " ' );</script>", false);
           clear();
           GridDisplay();
       }
       else if (Rtnval == 5)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Expiry date must be more than Minimum Drug Expiry for Loading ');</script>", false);
       }
       else if (Rtnval == 6)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Quantity loaded exceeds Maximum Quantity per Cartridge');</script>", false);
       }
       else if (Rtnval == 7)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Item Effective Date To should not be earlier than current date');</script>", false);
       }
       else if (Rtnval == 8)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Store Effective Date To should not be earlier than current date');</script>", false);
       }
       else if (Rtnval == 39)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Item inactivated in drug master');</script>", false);
       }
       else
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
       }
   }

   void PreLoadUpdate()
   {
       Rtnval = BDS.BDS_Bot_Preloading_Update((Int64)(ViewState["CartID"]), txtexpdate.Text, txtbatchno.Text.Trim(), Session["Userid"].ToString(), out barcode);
       
       if (Rtnval == 1)
       {
           pri.PrintCartonBoxBarcode(barcode.Trim(), txtdrugname.Text.Trim(), ddlprintername.SelectedValue.ToString());
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Successfully Updated   " + barcode + " ');</script>", false);
           clear();
           GridDisplay();
       }
       else if (Rtnval == 2)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton box cartridge allocation started. Cannot edit');</script>", false);
       }
       else if (Rtnval == 5)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Expiry date must be more than Minimum Drug Expiry for Loading ');</script>", false);
       }
       else if (Rtnval == 6)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Quantity loaded exceeds Maximum Quantity per Cartridge');</script>", false);
       }
       else if (Rtnval == 7)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Item Effective Date To should not be earlier than current date');</script>", false);
       }
       else if (Rtnval == 8)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Store Effective Date To should not be earlier than current date');</script>", false);
       }
       else if (Rtnval == 39)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Item inactivated in drug master');</script>", false);
       }
       else
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occurred');</script>", false);
       }
   }

   void Cancel()
   {
       Rtnval = BDS.BDS_Bot_Preloading_Cancel((Int64)(ViewState["CartID"]), Session["Userid"].ToString());
       if (Rtnval == 1)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Successfully Cancelled' );</script>", false);
           clear();
           GridDisplay();
       }
       else if (Rtnval == 2)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton box cartridge allocation started you cannot cancel');</script>", false);
       }
       else
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occurred');</script>", false);
       }
   }

   void GridDisplay()
   {
       try
       {
           BDS.BDSpreloadgrid(GridPreloaded, sessionuserid, location, ViewState["ord"].ToString());
           DataSet dsData = GridPreloaded.DataSource as DataSet;
           DataTable dtData = dsData.Tables[0];
           lblpge.Text = "Page" + "  " + Convert.ToString(GridPreloaded.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(GridPreloaded.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
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

   void EditReader()
   {     
       using (SqlConnection con = DBCon.getstring())
       {
           string Commt = "select convert (varchar,b.Expiry_Date,103),b.Batch_No,i.Drug_Code,um.Max_Cartridge_Qty from BDS_PreLoading as b left join Item_user_Master as um on um.ID= b.IUM_ID left join Item_Master as i on i.MasterID=um.MasterID where b.CartID='" + Convert.ToInt32(ViewState["CartID"].ToString()) + "'";
           cmd = new SqlCommand(Commt, con);

           con.Open();
           using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
           {
               if (dr.HasRows)
               {
                   while (dr.Read())
                   {
                       txtexpdate.Text = dr[0].ToString();
                       txtbatchno.Text = dr[1].ToString();
                       txtdrugcode.Text = dr[2].ToString();
                       txtmaxcartqty.Text = dr[3].ToString();
                   }
               }
           }
       }

   }

   //// * BDS Name Reader * \\
   //public void DDSName()
   //{
   //    ddlbdsname.Items.Clear();
   //    SqlDataReader dr = BDS.Prloadddsnamereader(location);
   //    if (dr.HasRows)
   //    {
   //        while (dr.Read())
   //        {
   //            ddlbdsname.Items.Add(dr[0].ToString());
   //        }
   //        dr.Close();
   //    }
   //}


   void clear()
   {
       txtitemcode.Text = "";
       txtdrugname.Text = "";
       txtdrugcode.Text = "";
       txtbrand.Text = "";
       txtpacktype.Text = "";
       txtpacksize.Text = "";
       txtuom.Text = "";      
       txtmaxcartqty.Text = "";
       txtexpdate.Text = "";
       txtbatchno.Text = "";
       txtcartonbox.Text = "";
       lblmax.Text = "";      
       lblcartboxof.Text = "";
       TxtBoxOrPallet.Text = "";
       btnsave.Visible = true;
       btnupdate.Visible = false;
       btncancel.Visible = false;
       BtnSearch.Visible = true;
       tdone.Visible = true;
       tdtwo.Visible = false;
       txtexpdate1.Text = "";
       txtbatchno1.Text = "";
       defaultprinter();
   }


   protected void GridPreloaded_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
   {
       clear();
       ViewState["CartID"] = 0;       
       ViewState["CartID"] =  (Int64)GridPreloaded.DataKeys[e.NewSelectedIndex].Value;

       if ((GridPreloaded.Rows[e.NewSelectedIndex].Cells[10].Text == "NEW") && (GridPreloaded.Rows[e.NewSelectedIndex].Cells[11].Text == "PreLoaded") ||
           (GridPreloaded.Rows[e.NewSelectedIndex].Cells[10].Text == "NEW") && (GridPreloaded.Rows[e.NewSelectedIndex].Cells[11].Text == "First Rejected"))
       {
           txtitemcode.Text = GridPreloaded.Rows[e.NewSelectedIndex].Cells[2].Text;
           txtdrugname.Text = GridPreloaded.Rows[e.NewSelectedIndex].Cells[3].Text;
           txtbrand.Text = GridPreloaded.Rows[e.NewSelectedIndex].Cells[4].Text;
           TxtBoxOrPallet.Text = GridPreloaded.Rows[e.NewSelectedIndex].Cells[5].Text.Trim();
           txtcartonbox.Text = GridPreloaded.Rows[e.NewSelectedIndex].Cells[6].Text;
           txtpacktype.Text = GridPreloaded.Rows[e.NewSelectedIndex].Cells[7].Text;
           txtpacksize.Text = GridPreloaded.Rows[e.NewSelectedIndex].Cells[8].Text;
           txtuom.Text = GridPreloaded.Rows[e.NewSelectedIndex].Cells[9].Text;

           EditReader();
           btnsave.Visible = false;
           btnupdate.Visible = true;
           btncancel.Visible = true;
           BtnSearch.Visible = false;
           lblmax.Text = txtpacktype.Text.Trim() + " " + "of" + " " + txtpacksize.Text.Trim() + " " + txtuom.Text.Trim();           
           lblcartboxof.Text = txtpacktype.Text.Trim();
       }
       else if ((GridPreloaded.Rows[e.NewSelectedIndex].Cells[10].Text == "NEW") && (GridPreloaded.Rows[e.NewSelectedIndex].Cells[11].Text == "First Verified"))
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton box already verified. Cannot edit');</script>", false);
       }

       else if (GridPreloaded.Rows[e.NewSelectedIndex].Cells[10].Text.ToUpper() == "ALLOT")
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton box cartridge allocation started. Cannot edit');</script>", false);
       }
       else if (GridPreloaded.Rows[e.NewSelectedIndex].Cells[10].Text.ToUpper() == "CLOSE")
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton box cartridge allocation completed. Cannot edit');</script>", false);
       }
       else if (GridPreloaded.Rows[e.NewSelectedIndex].Cells[10].Text.ToUpper() == "CANCEL")
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Carton box info already cancelled.');</script>", false);
       }
   }


   protected void btnempenter_Click(object sender, ImageClickEventArgs e)
   {
      
       if (Session["EmpID"].ToString().ToLower() == txtempid.Text.Trim().ToLower())
       {
           tdone.Visible = true;
           tdtwo.Visible = false;
           PreloadingInsert();
       }

       else if (Session["EmpID"].ToString().ToLower() != txtempid.Text.Trim().ToLower())
       {
           tdone.Visible = true;
           tdtwo.Visible = false;          
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('You are not authorised to do PreLoading. NRIC / Staff ID/ Pass Mismatch');</script>", false);
       }
   }
   protected void btnempenterupdate_Click(object sender, ImageClickEventArgs e)
   {
       
       if (Session["EmpID"].ToString().ToLower() == txtempid.Text.Trim().ToLower())
       {
           tdone.Visible = true;
           tdtwo.Visible = false;
           PreLoadUpdate();
       }

       else if (Session["EmpID"].ToString().ToLower() != txtempid.Text.Trim().ToLower())
       {
           tdone.Visible = true;
           tdtwo.Visible = false;
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('You are not authorised to do PreLoading. NRIC / Staff ID/ Pass Mismatch');</script>", false);
       }
      
   }
   protected void BtnempenterCancel_Click(object sender, ImageClickEventArgs e)
   {

       if (Session["EmpID"].ToString().ToLower() == txtempid.Text.Trim().ToLower())
       {
           tdone.Visible = true;
           tdtwo.Visible = false;
           Cancel();
       }

       else if (Session["EmpID"].ToString().ToLower() != txtempid.Text.Trim().ToLower())
       {
           tdone.Visible = true;
           tdtwo.Visible = false;
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('You are not authorised to do PreLoading. NRIC / Staff ID/ Pass Mismatch');</script>", false);
       }
       
   }

   // Default Printer Reader \\
   public void defaultprinter()
   {
       ddlprintername.Items.Clear();
      
       using (SqlConnection con = DBCon.getstring())
       {
           string Commt = "select p.Printer_Name,f.Default_Printer from Printer as p left join Printer_Function as f on f.PrinterID=p.PrinterID left join Pharmacy as ph on ph.PharmacyID=p.PharmacyID where f.Label_Name='CBL'and ph.Location_Name='" + location + "' and p.Status='Active'";
           cmd = new SqlCommand(Commt, con);

           con.Open();
           using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
           {
               if (dr.HasRows)
               {
                   while (dr.Read())
                   {
                       if (dr[1].ToString() == "Yes")
                       {
                           ddlprintername.Items.Insert(0, new ListItem(dr[0].ToString()));
                       }
                       else if (dr[1].ToString() == "No")
                       {
                           ddlprintername.Items.Add(dr[0].ToString());
                       }
                   }
               }
           }
       }

   }
   protected void GridPreloaded_PageIndexChanging(object sender, GridViewPageEventArgs e)
   {        
        GridDisplay();
        GridPreloaded.PageIndex = e.NewPageIndex;
        GridPreloaded.DataBind();
        DataSet dsData = GridPreloaded.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(GridPreloaded.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(GridPreloaded.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = System.Drawing.Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Visible = true;
            lblpge.Font.Bold = true;
            lblpge.ForeColor = System.Drawing.Color.Green;
        }
   }

   protected void GridPreloaded_Sorting(object sender, GridViewSortEventArgs e)
   {
       GridDisplay();
       DataSet dsData = GridPreloaded.DataSource as DataSet;
       DataTable dtData = dsData.Tables[0];
       GridViewSortExpression = e.SortExpression;
       int pageIndex = GridPreloaded.PageIndex;
       GridPreloaded.DataSource = SortDataTable(dtData, false);
       GridPreloaded.DataBind();
       GridPreloaded.PageIndex = pageIndex;
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
               if (GridViewSortDirection == "ASC")
               {
                   if (GridViewSortExpression.ToString() == "Loaded_Dat")
                   {
                       ViewState["ord"] = "order by" + " " + "p.LoadedDate" + " " + "desc";
                   }
                   else if (GridViewSortExpression.ToString() == "Upd_Dat")
                   {
                       ViewState["ord"] = "order by" + " " + "p.UpdatedDate" + " " + "desc";
                   }
                   else
                   {
                       ViewState["ord"] = "order by" + " " + GridViewSortExpression + " " + "desc";
                   }                   
               }
               else if (GridViewSortDirection == "DESC")
               {
                   if (GridViewSortExpression.ToString() == "Loaded_Dat")
                   {
                       ViewState["ord"] = "order by" + " " + "p.LoadedDate" + " " + "asc";
                   }
                   else if (GridViewSortExpression.ToString() == "Upd_Dat")
                   {
                       ViewState["ord"] = "order by" + " " + "p.UpdatedDate" + " " + "asc";
                   }
                   else
                   {
                       ViewState["ord"] = "order by" + " " + GridViewSortExpression + " " + "asc";
                   }
                   
               }

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