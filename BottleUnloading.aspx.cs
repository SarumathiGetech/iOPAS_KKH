using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Datalayer;
using System.Drawing;

public partial class BottleUnloading : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    BDSBottle BDS = new BDSBottle();
    string sessionuserid = "", location = "";

   protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sessionuserid = Session["Userid"].ToString();
            location = Session["location"].ToString();
            if (!IsPostBack)
            {
                ViewState["ord"] = "order by Bu.UnloadingID desc";
                UnloadingGridDisplay();
                Unloading_CartID_Reader();
            }

            if (location != "")
            {
               // txtcartno.Focus();
            }
            else if (location == "" || location == null)
            {
                pp.Visible = false;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This Workstation is not authorised to do Unloading');</script>", false);
                return;
            }
        }
        catch (NullReferenceException)
        {
            Response.Redirect("iopas.html");
        }           
    }
  
    //protected void Btngo_Click(object sender, ImageClickEventArgs e)
    //{
    //    int Rtnvalue = BDS.BDS_Bot_Cartridge_Unloading_Validate(location, ddlcartno.SelectedValue.ToString().Trim());         

    //    if (Rtnvalue == 1)
    //    {
    //        UnloadingReader();
    //    }
    //    else if (Rtnvalue == 2)
    //    {
    //        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge is allocated for Preloadding');</script>", false);
    //        //txtcartno.Text = "";
    //        //txtcartno.Focus();
    //    }
    //    else if (Rtnvalue == 3)
    //    {
    //        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please disable the cartridge and unload');</script>", false);
    //        //txtcartno.Text = "";
    //        //txtcartno.Focus();
    //    }
    //    else if (Rtnvalue == 4)
    //    {
    //        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge is already submitted for unloading');</script>", false);
    //        //txtcartno.Text = "";
    //        //txtcartno.Focus();
    //    }
    //    else if (Rtnvalue == 5)
    //    {
    //        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge is being loaded.');</script>", false);
    //        //txtcartno.Text = "";
    //        //txtcartno.Focus();
    //    }
    //    else if (Rtnvalue == 6)
    //    {
    //        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge is not Preloaded');</script>", false);
    //        //txtcartno.Text = "";
    //        //txtcartno.Focus();
    //    }
    //    else if (Rtnvalue == 7)
    //    {
    //        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge is required for manual unloading. Manually unload the cartridge and update inventory');</script>", false);
    //        //txtcartno.Text = "";
    //        //txtcartno.Focus();
    //    }
    //    else if (Rtnvalue == 10)
    //    {
    //        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid cartridge number');</script>", false);
    //        //txtcartno.Text = "";
    //        //txtcartno.Focus();
    //    }
    //    else if (Rtnvalue == 11)
    //    {
    //        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge not preloaded');</script>", false);
    //        //txtcartno.Text = "";
    //        //txtcartno.Focus();
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occurred');</script>", false);
    //        //txtcartno.Text = "";
    //        //txtcartno.Focus();
    //    }
    //}


    protected void btnsave_Click(object sender, ImageClickEventArgs e)
    {
        int Rtnvalue = BDS.BDS_Bot_Cartridge_Unloading_Validate(location, ddlcartno.SelectedValue.ToString().Trim());
        if (Rtnvalue == 1)
        {
            UnloadingInsert();
        }
        else if (Rtnvalue == 2)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge is allocated for Preloadding');</script>", false);            
            Clear();
        }
        else if (Rtnvalue == 3)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please disable the cartridge and unload');</script>", false);
            Clear();
        }
        else if (Rtnvalue == 4)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge is already submitted for unloading');</script>", false);
            Clear();
        }
        else if (Rtnvalue == 5)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge is being loaded.');</script>", false);
            Clear();
        }
        else if (Rtnvalue == 6)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge is not Preloaded');</script>", false);
            Clear();
        }
        else if (Rtnvalue == 7)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge is required for manual unloading. Manually unload the cartridge and update inventory');</script>", false);
            Clear();
        }
        else if (Rtnvalue == 10)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid cartridge number');</script>", false);
            Clear();
        }
        else if (Rtnvalue == 11)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge not preloaded');</script>", false);
            Clear();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occurred');</script>", false);
            Clear();
        }
    }


    void UnloadingInsert()
    {
        int Rtnvalue = BDS.BDS_Bot_Cartridge_Unloading_Insert(location, ddlcartno.SelectedValue.ToString().Trim(), sessionuserid);
        if (Rtnvalue == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Successfully Inserted');</script>", false);
            Clear();
            ViewState["ord"] = "order by Bu.UnloadingID desc";
            UnloadingGridDisplay();
            Unloading_CartID_Reader();
        }
        else if (Rtnvalue == 3)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please disable the cartridge and unload');</script>", false);
            Clear();
        }
        else if (Rtnvalue == 4)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge is already submitted for unloading');</script>", false);
            Clear();
        }
        else if (Rtnvalue == 5)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid cartridge number');</script>", false);
            Clear();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occurred');</script>", false);
            Clear();
        }
    }

    void UnloadingReader()
    {      
       using (SqlConnection con = DBCon.getstring())
       {
           string Commt = "select cl.DDS_Name,im.Item_Code,im.Item_Name,bm.Brandname,cl.Aval_Quantity from Cartridge_Loading as cl left join Item_user_Master as iu on iu.ID=cl.IUM_ID left join Item_Master as im on im.MasterID=iu.MasterID left join Brand_Master as bm on bm.BrandID=iu.Brandid where cl.Inventory_Status=2 and cl.Activation_Status<>1 and cl.Cartridge_Id='" + ddlcartno.SelectedValue.ToString().Trim() + "' and  cl.DDS_Name not like '%'+'DDS'+'%'";
           cmd = new SqlCommand(Commt, con);

           con.Open();
           using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
           {
               if (dr.HasRows)
               {
                   while (dr.Read())
                   {
                       txtbdsname.Text = dr[0].ToString();
                       txtitemcode.Text = dr[1].ToString();
                       txtitemname.Text = dr[2].ToString();
                       txtbrandname.Text = dr[3].ToString();
                       txtsysbalance.Text = dr[4].ToString();
                   }
               }
           }
       }
    }

    void Unloading_CartID_Reader()
    {
        ddlcartno.Items.Clear();
        ddlcartno.Items.Add("-Select-");
  
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select Cartridge_Id from Cartridge_Loading where Inventory_Status=2 and Activation_Status=2 and DDS_Name not like '%'+'DDS'+'%' order by Loading_Id asc";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddlcartno.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }

    void UnloadingGridDisplay()
    {
        BDS.BDSUnloadingGrid(Unloadinggrid, sessionuserid, ViewState["ord"].ToString());
        DataSet dsData = Unloadinggrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(Unloadinggrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Unloadinggrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Text = "No Record Found";
            lblpge.Font.Bold = true;
            lblpge.ForeColor = Color.Green;
        }
    }

    void Clear()
    {
        //txtcartno.Text = "";
        txtbdsname.Text = "";
        txtbrandname.Text = "";
        txtsysbalance.Text = "";
        txtitemcode.Text = "";
        txtitemname.Text = "";
        //txtcartno.Focus();
    }
    protected void btnclear_Click(object sender, ImageClickEventArgs e)
    {
        Clear();
        ViewState["ord"] = "order by Bu.UnloadingID desc";
        UnloadingGridDisplay();
        Unloading_CartID_Reader();
    }

    protected void Unloadinggrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        UnloadingGridDisplay();
        Unloadinggrid.PageIndex = e.NewPageIndex;
        Unloadinggrid.DataBind();
        DataSet dsData = Unloadinggrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(Unloadinggrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Unloadinggrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpge.ForeColor = System.Drawing.Color.Black;
        lblpge.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpge.Visible = true;
            lblpge.Font.Bold = true;
            lblpge.ForeColor = System.Drawing.Color.Green;
        }
    }

    protected void Unloadinggrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["ord"] = "";
        UnloadingGridDisplay();
        DataSet dsData = Unloadinggrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = Unloadinggrid.PageIndex;
        Unloadinggrid.DataSource = SortDataTable(dtData, false);
        Unloadinggrid.DataBind();
        Unloadinggrid.PageIndex = pageIndex;
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
                    ViewState["ord"] = "order by" + " " + GridViewSortExpression + " " + "desc";
                }
                else if (GridViewSortDirection == "DESC")
                {
                    ViewState["ord"] = "order by" + " " + GridViewSortExpression + " " + "asc";
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

    protected void ddlcartno_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcartno.SelectedValue.ToString().Trim() != "-Select-")
        {

            int Rtnvalue = BDS.BDS_Bot_Cartridge_Unloading_Validate(location, ddlcartno.SelectedValue.ToString().Trim());

            if (Rtnvalue == 1)
            {
                UnloadingReader();
            }
            else if (Rtnvalue == 2)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge is allocated for Preloadding');</script>", false);
                //txtcartno.Text = "";
                //txtcartno.Focus();
            }
            else if (Rtnvalue == 3)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please disable the cartridge and unload');</script>", false);
                //txtcartno.Text = "";
                //txtcartno.Focus();
            }
            else if (Rtnvalue == 4)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge is already submitted for unloading');</script>", false);
                //txtcartno.Text = "";
                //txtcartno.Focus();
            }
            else if (Rtnvalue == 5)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge is being loaded.');</script>", false);
                //txtcartno.Text = "";
                //txtcartno.Focus();
            }
            else if (Rtnvalue == 6)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge is not Preloaded');</script>", false);
                //txtcartno.Text = "";
                //txtcartno.Focus();
            }
            else if (Rtnvalue == 7)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge is required for manual unloading. Manually unload the cartridge and update inventory');</script>", false);
                //txtcartno.Text = "";
                //txtcartno.Focus();
            }
            else if (Rtnvalue == 10)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid cartridge number');</script>", false);
                //txtcartno.Text = "";
                //txtcartno.Focus();
            }
            else if (Rtnvalue == 11)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Cartridge not preloaded');</script>", false);
                //txtcartno.Text = "";
                //txtcartno.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occurred');</script>", false);
                //txtcartno.Text = "";
                //txtcartno.Focus();
            }
        }
        else
        {
            Clear();
        }
    }
}