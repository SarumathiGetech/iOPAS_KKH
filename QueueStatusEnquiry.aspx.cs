using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using Datalayer;

public partial class QueueStatusEnquiry : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    Searchquery scr = new Searchquery();
    string sessionuserid = "";
    DateTime QDateto, QDatefrom;
    int a = 0, b = 0, c = 0;
   // static string queno = "",Patid="",ord="",clicksts="";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Userid"] != null)
        {
            sessionuserid = Session["Userid"].ToString();

            if (!IsPostBack)
            {
                Locationname();
                txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtdateto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                details.Visible = false;
                Detailgrid.Visible = false;
                Nodds.Visible = false;

                txtqueuefst.Attributes.Add("onKeyPress", "doClick(event)");
                txtpatientidfst.Attributes.Add("onKeyPress", "doClick(event)");
                txtpatientnamefst.Attributes.Add("onKeyPress", "doClick(event)");                
            }
        }
        else
        {
            //Response.Redirect("opas.html?Session=End");
            Response.Redirect("iopas.html");
        }

        string scriptStr = "javascript:return popUpCalendar(this," + getClientID() + @", 'dd/mm/yyyy', '__doPostBack(\'" + getClientID() + @"\')')";
        imgCalendar.Attributes.Add("onclick", scriptStr);

        string scriptStrtwo = "javascript:return popUpCalendar(this," + getClientIDTo() + @", 'dd/mm/yyyy', '__doPostBack(\'" + getClientIDTo() + @"\')')";
        imgtwo.Attributes.Add("onclick", scriptStrtwo);
    }
    
    public string getClientID()
    {
        return txtdatefrom.ClientID;
    }
    public string getClientIDTo()
    {
        return txtdateto.ClientID;
    }

    // * Pharmacy Location Reader * \\
    public void Locationname()
    {

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select Location_Name from Pharmacy where Status='Active'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddlpharloc.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
    }
    
    
    protected void btnsear_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["ord"] = "order by Process_Time desc";
        //ViewState["ord"] = "order by ordertime desc";       
        Queuestatusgrid.PageIndex = 0;
        invalidfstgrd.PageIndex = 0;
        int b = 0;
        if (txtpatientidfst.Text.Trim() != "")
        {
            b =b+1;
        }
        if (txtpatientnamefst.Text.Trim() != "")
        {
            b = b + 1;
        }
        if (txtqueuefst.Text.Trim() != "")
        {
            b = b + 1;
        }
        if (b == 0 || b == 1)
        {
            if (txtqueuefst.Text.Length == 3)
            {
                txtqueuefst.Text = "0" + txtqueuefst.Text;
            }
            else if (txtqueuefst.Text.Length == 2)
            {
                txtqueuefst.Text = "00" + txtqueuefst.Text;
            }
            else if (txtqueuefst.Text.Length == 1)
            {
                txtqueuefst.Text = "000" + txtqueuefst.Text;
            }

            if (txtdatefrom.Text.Trim() != "")  
            {
                if (txtdateto.Text.Trim() != "")
                {
                    datefun();
                    if (QDatefrom <= QDateto)
                    {
                        if (ddlstatus.SelectedValue.ToString() != "Diverted")
                        {
                            Nodds.Visible = false;
                            details.Visible = false;
                            Detailgrid.Visible = false;
                            Queuestatusgrid.Visible = true;
                            lblpage1.Visible = true;
                            fstgrd.Visible = true;
                            fistgriddisplay();
                        }
                        else if (ddlstatus.SelectedValue.ToString() == "Diverted")
                        {
                            details.Visible = false;
                            Detailgrid.Visible = false;
                            Nodds.Visible = true;
                            fstgrd.Visible = false;
                            ViewState["ord"] = "order by ordertime desc";
                            Invalidorder();                         
                            Noddsgrd.Visible = false;
                            lblpage3.Visible = false;
                        }
                    }
                    else if (QDatefrom > QDateto)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Date to must be later than date from');</script>", false);
                    }
                }
                else if (txtdateto.Text.Trim() == "")
                {
                    if (txtpatientidfst.Text.Trim() == "" && txtqueuefst.Text.Trim() == "" && txtpatientnamefst.Text.Trim() == "")
                    {
                        datefun();
                        if (QDatefrom <= QDateto || QDatefrom >= QDateto)
                        {
                            if (ddlstatus.SelectedValue.ToString() != "Diverted")
                            {
                                Nodds.Visible = false;
                                details.Visible = false;
                                Detailgrid.Visible = false;
                                Queuestatusgrid.Visible = true;
                                lblpage1.Visible = true;
                                fstgrd.Visible = true;
                                fistgriddisplay();
                            }
                            else if (ddlstatus.SelectedValue.ToString() == "Diverted")
                            {
                                details.Visible = false;
                                Detailgrid.Visible = false;
                                Nodds.Visible = true;
                                fstgrd.Visible = false;
                             
                                Invalidorder();
                                DataSet dsData = invalidfstgrd.DataSource as DataSet;
                                DataTable dtData = dsData.Tables[0];
                                lblpage4.Text = "Page" + "  " + Convert.ToString(invalidfstgrd.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(invalidfstgrd.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
                                lblpage4.ForeColor = Color.Black;
                                lblpage4.Font.Bold = false;
                                if (dtData.Rows.Count == 0)
                                {
                                    lblpage4.Text = "No Record Found";
                                    lblpage4.Font.Bold = true;
                                    lblpage4.ForeColor = Color.Green;
                                }
                                Noddsgrd.Visible = false;
                                lblpage3.Visible = false;
                            }
                        }
                    }
                    else if (txtpatientidfst.Text.Trim() != "" || txtqueuefst.Text.Trim() != "" || txtpatientnamefst.Text.Trim() != "")
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please enter date to');</script>", false);
                    }
                }
            }
            else if (txtdatefrom.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please enter date from');</script>", false);
            }
        }
        else if (b != 0 && b != 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Only one field entry is allowed');</script>", false);
        }        
    }

   // * Invalid Order Grid Display * \\
    public void Invalidorder()
    {
        if (txtpatientidfst.Text.Trim() == "" && txtqueuefst.Text.Trim() == "" && txtpatientnamefst.Text.Trim() == "")
        {            
            scr.Noddsgrid(invalidfstgrd, ddlpharloc.SelectedValue.ToString(), txtdatefrom.Text, txtdatefrom.Text, ViewState["ord"].ToString());
        }
        else if (txtpatientidfst.Text.Trim() != "")
        {           
            scr.Noddsgridpatientid(invalidfstgrd, ddlpharloc.SelectedValue.ToString(), txtdatefrom.Text, txtdateto.Text, txtpatientidfst.Text.Trim());
        }
        else if (txtqueuefst.Text.Trim() != "")
        {            
            scr.Noddsgridqueuno(invalidfstgrd, ddlpharloc.SelectedValue.ToString(), txtdatefrom.Text, txtdateto.Text, txtqueuefst.Text.Trim());
        }
        else if (txtpatientnamefst.Text.Trim() != "")
        {            
            scr.Noddsgridpatname(invalidfstgrd, ddlpharloc.SelectedValue.ToString(), txtdatefrom.Text, txtdateto.Text, txtpatientnamefst.Text.Trim());
        }

        try
        {
            DataSet dsData = invalidfstgrd.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpage4.Text = "Page" + "  " + Convert.ToString(invalidfstgrd.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(invalidfstgrd.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
            lblpage4.ForeColor = Color.Black;
            lblpage4.Font.Bold = false;
            if (dtData.Rows.Count == 0)
            {
                lblpage4.Text = "No Record Found";
                lblpage4.Font.Bold = true;
                lblpage4.ForeColor = Color.Green;
            }
        }
        catch (Exception ex)
        {
        }
    }

    // *First grid Display * \\
    public void fistgriddisplay()
    {
        //Queue Number Search \\
       
       if (txtqueuefst.Text.Trim() != "" && ddlstatus.SelectedValue == "Assembly Completed")
        {
            scr.queuenum_Assembled_fstgrid(Queuestatusgrid, txtqueuefst.Text.Trim(), txtdatefrom.Text.Trim(), txtdateto.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }
        else if (txtqueuefst.Text.Trim() != "" && ddlstatus.SelectedValue == "DDS/BDS Work in progress")
        {
            scr.queuenum_WIP_fstgrid(Queuestatusgrid, txtqueuefst.Text.Trim(), txtdatefrom.Text.Trim(), txtdateto.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }
        else if (txtqueuefst.Text.Trim() != "" && ddlstatus.SelectedValue == "Pending DDS/BDS")
        {
            scr.queuenum_Pen_order_fstgrid(Queuestatusgrid, txtqueuefst.Text.Trim(), txtdatefrom.Text.Trim(), txtdateto.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }
        else if (txtqueuefst.Text.Trim() != "" && ddlstatus.SelectedValue == "All")
        {
            scr.queuenum_All_fstgrid(Queuestatusgrid, txtqueuefst.Text.Trim(), txtdatefrom.Text.Trim(), txtdateto.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }
        else if (txtqueuefst.Text.Trim() != "" && ddlstatus.SelectedValue == "Jump Queue Orders")
        {
            scr.queuenum_Jump(Queuestatusgrid, txtqueuefst.Text.Trim(), txtdatefrom.Text.Trim(), txtdateto.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }
        else if (txtqueuefst.Text.Trim() != "" && ddlstatus.SelectedValue == "Triger Orders")
        {
            scr.queuenum_trigger(Queuestatusgrid, txtqueuefst.Text.Trim(), txtdatefrom.Text.Trim(), txtdateto.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }


 //Patient ID Search \\
       else if (txtpatientidfst.Text.Trim() != "" && txtqueuefst.Text.Trim() == "" && ddlstatus.SelectedValue == "Assembly Completed")
        {
            scr.patientid_Assembled_fstgrid(Queuestatusgrid, txtpatientidfst.Text.Trim(), txtdatefrom.Text.Trim(), txtdateto.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }
        else if (txtpatientidfst.Text.Trim() != "" && txtqueuefst.Text.Trim() == "" && ddlstatus.SelectedValue == "DDS/BDS Work in progress")
        {
            scr.patientid_WIP_fstgrid(Queuestatusgrid, txtpatientidfst.Text.Trim(), txtdatefrom.Text.Trim(), txtdateto.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }
        else if (txtpatientidfst.Text.Trim() != "" && txtqueuefst.Text.Trim() == "" && ddlstatus.SelectedValue == "Pending DDS/BDS")
        {
            scr.patientid_Pen_order_fstgrid(Queuestatusgrid, txtpatientidfst.Text.Trim(), txtdatefrom.Text.Trim(), txtdateto.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }
        else if (txtpatientidfst.Text.Trim() != "" && txtqueuefst.Text.Trim() == "" && ddlstatus.SelectedValue == "All")
        {
            scr.patientid_All_fstgrid(Queuestatusgrid, txtpatientidfst.Text.Trim(), txtdatefrom.Text.Trim(), txtdateto.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }
        else if (txtpatientidfst.Text.Trim() != "" && txtqueuefst.Text.Trim() == "" && ddlstatus.SelectedValue == "Jump Queue Orders")
        {
            scr.patientid_jumpqueue(Queuestatusgrid, txtpatientidfst.Text.Trim(), txtdatefrom.Text.Trim(), txtdateto.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }
        else if (txtpatientidfst.Text.Trim() != "" && txtqueuefst.Text.Trim() == "" && ddlstatus.SelectedValue == "Triger Orders")
        {
            scr.patientid_trigger(Queuestatusgrid, txtpatientidfst.Text.Trim(), txtdatefrom.Text.Trim(), txtdateto.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }

 //Patient Name Search \\
    
        else if (txtpatientnamefst.Text.Trim() != "" && txtpatientidfst.Text.Trim() == "" && txtqueuefst.Text.Trim() == "" && ddlstatus.SelectedValue == "Assembly Completed")
        {
            scr.patientname_Assembled_fstgrid(Queuestatusgrid, txtpatientnamefst.Text.Trim(), txtdatefrom.Text.Trim(), txtdateto.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }
        else if (txtpatientnamefst.Text.Trim() != "" && txtpatientidfst.Text.Trim() == "" && txtqueuefst.Text.Trim() == "" && ddlstatus.SelectedValue == "DDS/BDS Work in progress")
        {
            scr.patientname_WIP_fstgrid(Queuestatusgrid, txtpatientnamefst.Text.Trim(), txtdatefrom.Text.Trim(), txtdateto.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }
        else if (txtpatientnamefst.Text.Trim() != "" && txtpatientidfst.Text.Trim() == "" && txtqueuefst.Text.Trim() == "" && ddlstatus.SelectedValue == "Pending DDS/BDS")
        {
            scr.patientname_Pen_Order_fstgrid(Queuestatusgrid, txtpatientnamefst.Text.Trim(), txtdatefrom.Text.Trim(), txtdateto.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }
        else if (txtpatientnamefst.Text.Trim() != "" && txtpatientidfst.Text.Trim() == "" && txtqueuefst.Text.Trim() == "" && ddlstatus.SelectedValue == "All")
        {
            scr.patientname_ALL_fstgrid(Queuestatusgrid, txtpatientnamefst.Text.Trim(), txtdatefrom.Text.Trim(), txtdateto.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }
        else if (txtpatientnamefst.Text.Trim() != "" && txtpatientidfst.Text.Trim() == "" && txtqueuefst.Text.Trim() == "" && ddlstatus.SelectedValue == "Jump Queue Orders")
        {
            scr.patientname_jumpqueue(Queuestatusgrid, txtpatientnamefst.Text.Trim(), txtdatefrom.Text.Trim(), txtdateto.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }
        else if (txtpatientnamefst.Text.Trim() != "" && txtpatientidfst.Text.Trim() == "" && txtqueuefst.Text.Trim() == "" && ddlstatus.SelectedValue == "Triger Orders")
        {
            scr.patientname_trigger(Queuestatusgrid, txtpatientnamefst.Text.Trim(), txtdatefrom.Text.Trim(), txtdateto.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }

//With Out Queue Number,Patient ID,Patient Name only Status and Single Day Search \\

      
        else if (txtpatientnamefst.Text.Trim() == "" && txtpatientidfst.Text.Trim() == "" && txtqueuefst.Text.Trim() == "" && ddlstatus.SelectedValue == "Assembly Completed")
        {
            scr.Sinleday_Assembled_fstgrid(Queuestatusgrid, txtdatefrom.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }
        else if (txtpatientnamefst.Text.Trim() == "" && txtpatientidfst.Text.Trim() == "" && txtqueuefst.Text.Trim() == "" && ddlstatus.SelectedValue == "DDS/BDS Work in progress")
        {
            scr.Sinleday_WIP_fstgrid(Queuestatusgrid, txtdatefrom.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }
        else if (txtpatientnamefst.Text.Trim() == "" && txtpatientidfst.Text.Trim() == "" && txtqueuefst.Text.Trim() == "" && ddlstatus.SelectedValue == "Pending DDS/BDS")
        {
            scr.Sinleday_Pen_Order_fstgrid(Queuestatusgrid, txtdatefrom.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }
        else if (txtpatientnamefst.Text.Trim() == "" && txtpatientidfst.Text.Trim() == "" && txtqueuefst.Text.Trim() == "" && ddlstatus.SelectedValue == "All")
        {
            scr.Sinleday_ALL_fstgrid(Queuestatusgrid, txtdatefrom.Text.Trim(), ddlpharloc.SelectedValue.ToString(), ViewState["ord"].ToString());
        }
        else if (txtpatientnamefst.Text.Trim() == "" && txtpatientidfst.Text.Trim() == "" && txtqueuefst.Text.Trim() == "" && ddlstatus.SelectedValue == "Jump Queue Orders")
        {
            scr.Sinleday_jumpqueue(Queuestatusgrid, txtdatefrom.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }
       else if (txtpatientnamefst.Text.Trim() == "" && txtpatientidfst.Text.Trim() == "" && txtqueuefst.Text.Trim() == "" && ddlstatus.SelectedValue == "Triger Orders")
       {
           scr.Sinleday_trigger(Queuestatusgrid, txtdatefrom.Text.Trim(), ddlpharloc.SelectedValue.ToString());


           DataSet dsData = Queuestatusgrid.DataSource as DataSet;
           DataTable dtData = dsData.Tables[0];
           lblpage1.Text = "Page" + "  " + Convert.ToString(Queuestatusgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Queuestatusgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
           lblpage1.ForeColor = Color.Black;
           lblpage1.Font.Bold = false;
           if (dtData.Rows.Count == 0)
           {
               lblpage1.Text = "No Record Found";
               lblpage1.Font.Bold = true;
               lblpage1.ForeColor = Color.Green;
           }
       }
    }

    protected void Queuestatusgrid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        details.Visible = true;
        Detailgrid.Visible = true;
        txtqueueno.Text = Queuestatusgrid.Rows[e.NewSelectedIndex].Cells[1].Text;
       
        txtqueueno.Text = txtqueueno.Text.Replace("&nbsp;", " ");
        txtpatientid.Text= Queuestatusgrid.Rows[e.NewSelectedIndex].Cells[2].Text;
        txtpatientname.Text = Queuestatusgrid.Rows[e.NewSelectedIndex].Cells[3].Text;
        txttrandt.Text = Queuestatusgrid.Rows[e.NewSelectedIndex].Cells[5].Text;
       // Queuestatusgrid.Visible = false;
        lblpage1.Visible = false;
        qeudetailgrid.PageIndex = 0;
        detailgriddisplay();       
    }

    // * Public void GridDisplay*\\
    public void detailgriddisplay()
    {
        if (ddlstatus.SelectedValue == "Assembly Completed")
        {
            scr.Quedetailedassembled(qeudetailgrid, txtqueueno.Text.Trim(), txtpatientid.Text.Trim(), txttrandt.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }     
        else if (ddlstatus.SelectedValue == "DDS/BDS Work in progress")
        {
            scr.QuedetailWIP(qeudetailgrid, txtqueueno.Text.Trim(), txtpatientid.Text.Trim(), txttrandt.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }
        else if (ddlstatus.SelectedValue == "Pending DDS/BDS")
        {
            scr.Quedetailpenorder(qeudetailgrid, txtqueueno.Text.Trim(), txtpatientid.Text.Trim(), txttrandt.Text.Trim(), ddlpharloc.SelectedValue.ToString());
        }
        else if (ddlstatus.SelectedValue == "All" || ddlstatus.SelectedValue == "Jump Queue Orders" || ddlstatus.SelectedValue == "Triger Orders")
        {
            scr.Quedetailall(qeudetailgrid, txtqueueno.Text.Trim().Trim(), txtpatientid.Text.Trim().Trim(), txttrandt.Text.Trim().Trim(), ddlpharloc.SelectedValue.ToString());
        }

        try
        {
            DataSet dsData = qeudetailgrid.DataSource as DataSet;
            DataTable dtData = dsData.Tables[0];
            lblpge.Text = "Page" + "  " + Convert.ToString(qeudetailgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(qeudetailgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
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
    protected void qeudetailgrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        detailgriddisplay();
        qeudetailgrid.PageIndex = e.NewPageIndex;
        qeudetailgrid.DataBind();
        DataSet dsData = qeudetailgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpge.Text = "Page" + "  " + Convert.ToString(qeudetailgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(qeudetailgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";       
    }
    protected void Queuestatusgrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        fistgriddisplay();
        Queuestatusgrid.PageIndex = e.NewPageIndex;
        Queuestatusgrid.DataBind();
        DataSet dsData = Queuestatusgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpage1.Text = "Page" + "  " + Convert.ToString(Queuestatusgrid.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Queuestatusgrid.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";       
    }

 
    protected void Noddsgrd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {        
        noddsdetailgrd();
        Noddsgrd.PageIndex = e.NewPageIndex;
        Noddsgrd.DataBind();
        DataSet dsData = Noddsgrd.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpage3.Text = "Page" + "  " + Convert.ToString(Noddsgrd.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Noddsgrd.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpage3.ForeColor = Color.Black;
        lblpage3.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpage3.Text = "No Record Found";
            lblpage3.Font.Bold = true;
            lblpage3.ForeColor = Color.Green;
        }
    }

    public void activeinactive()
    {
        for (int i = 0; i < invalidfstgrd.Rows.Count; i++)
        {
            GridViewRow row = invalidfstgrd.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("chkrow")).Checked;

            if (isChecked)
            {
                c++;
                string DMSTATUS = (invalidfstgrd.Rows[i].Cells[4].Text);
                string act = "NO DDS";
                string inc = "Order Deleted";
                if (act.ToLower() == DMSTATUS.ToLower())
                {
                    a++;
                }
                else if (DMSTATUS.ToLower() == inc.ToLower())
                {
                    b++;
                }
            }
        }
    }


    public void datefun()
    {
        string i = "";
        i = txtdatefrom.Text.Trim();
        Int32 len = i.Length;
        Int32 n = i.IndexOf('/');
        string str = i.Substring(n + 1, 2);
        string str1 = i.Substring(0, 2);
        string str2 = i.Substring(6, 4);
        string dat = str + "/" + str1 + "/" + str2;
        QDatefrom = Convert.ToDateTime(dat);

        if (txtdateto.Text.Trim() != "")
        {
            string F = "";
            F = txtdateto.Text.Trim();
            Int32 lenn = F.Length;
            Int32 nn = F.IndexOf('/');
            string strr = F.Substring(nn + 1, 2);
            string strr1 = F.Substring(0, 2);
            string strr2 = F.Substring(6, 4);
            string datt = strr + "/" + strr1 + "/" + strr2;
            QDateto = Convert.ToDateTime(datt);
        }
    }

    protected void Queuestatusgrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        
        fistgriddisplay();
        DataSet dsData = Queuestatusgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = Queuestatusgrid.PageIndex;
        Queuestatusgrid.DataSource = SortDataTable(dtData, false);
        Queuestatusgrid.DataBind();
        Queuestatusgrid.PageIndex = pageIndex; 
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
                    dataView.Sort = string.Format("{0} {1}",GridViewSortExpression, GridViewSortDirection);
                }
                else
                {
                    dataView.Sort = string.Format("{0} {1}",GridViewSortExpression, GetSortDirection());
                }
            }
            return dataView;
        }
        else
        {
            return new DataView();
        }
    }
    protected void qeudetailgrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["ord"] = "";
        detailgriddisplay();
        DataSet dsData = qeudetailgrid.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = qeudetailgrid.PageIndex;
        qeudetailgrid.DataSource = SortDataTable(dtData, false);
        qeudetailgrid.DataBind();
        qeudetailgrid.PageIndex = pageIndex; 
    }
    protected void Noddsgrd_Sorting(object sender, GridViewSortEventArgs e)
    {
        noddsdetailgrd();
        DataSet dsData = Noddsgrd.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = Noddsgrd.PageIndex;
        Noddsgrd.DataSource = SortDataTable(dtData, false);
        Noddsgrd.DataBind();
        Noddsgrd.PageIndex = pageIndex; 
    }

    protected void btnclear_Click(object sender, ImageClickEventArgs e)
    {
        txtqueuefst.Text = "";
        txtpatientidfst.Text = "";
        txtpatientnamefst.Text = "";
        txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        txtdateto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

        ddlstatus.Items.Clear();
        ddlstatus.Items.Add("All");
        ddlstatus.Items.Add("Pending DDS/BDS");
        ddlstatus.Items.Add("DDS/BDS Work in progress");
        //ddlstatus.Items.Add("Pending Assembly");
        ddlstatus.Items.Add("Assembly Completed");        
        ddlstatus.Items.Add("Triger Orders");
        ddlstatus.Items.Add("Jump Queue Orders");
        ddlstatus.Items.Add("Diverted");

        Nodds.Visible = false;
        details.Visible = false;
        Detailgrid.Visible = false;
        Queuestatusgrid.Visible = false;
    }
    protected void invalidfstgrd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        
        Invalidorder();
        
        invalidfstgrd.PageIndex = e.NewPageIndex;
        invalidfstgrd.DataBind();
        DataSet dsData1 = invalidfstgrd.DataSource as DataSet;
        DataTable dtData1 = dsData1.Tables[0];
        lblpage4.Text = "Page" + "  " + Convert.ToString(invalidfstgrd.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(invalidfstgrd.PageCount) + "  " + "(" + Convert.ToString(dtData1.Rows.Count) + "  " + "Items" + ")";
        lblpage4.ForeColor = Color.Black;
        lblpage4.Font.Bold = false;
        if (dtData1.Rows.Count == 0)
        {
            lblpage4.Text = "No Record Found";
            lblpage4.Font.Bold = true;
            lblpage4.ForeColor = Color.Green;
        }     
    }
    protected void invalidfstgrd_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["ord"] = "";
        Invalidorder();
        DataSet dsData = invalidfstgrd.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        GridViewSortExpression = e.SortExpression;
        int pageIndex = invalidfstgrd.PageIndex;
        invalidfstgrd.DataSource = SortDataTable(dtData, false);
        invalidfstgrd.DataBind();
        invalidfstgrd.PageIndex = pageIndex; 
    }
    protected void invalidfstgrd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {      

        ViewState["queno"] = "";
        ViewState["Patid"] = "";        
        ViewState["queno"] = invalidfstgrd.Rows[e.NewSelectedIndex].Cells[2].Text;
        ViewState["queno"] = ViewState["queno"].ToString().Replace("&nbsp;", " ");
        ViewState["Patid"] = invalidfstgrd.Rows[e.NewSelectedIndex].Cells[3].Text;
        Noddsgrd.PageIndex = 0;
        noddsdetailgrd();
    }

    public void noddsdetailgrd()
    {
        Noddsgrd.Visible = true;
        lblpage3.Visible = true;
        scr.Noddsgriddetail(Noddsgrd, ddlpharloc.SelectedValue.ToString(), txtdatefrom.Text, ViewState["queno"].ToString(), ViewState["Patid"].ToString());
        DataSet dsData = Noddsgrd.DataSource as DataSet;
        DataTable dtData = dsData.Tables[0];
        lblpage3.Text = "Page" + "  " + Convert.ToString(Noddsgrd.PageIndex + 1) + "  " + "of" + "  " + Convert.ToString(Noddsgrd.PageCount) + "  " + "(" + Convert.ToString(dtData.Rows.Count) + "  " + "Items" + ")";
        lblpage3.ForeColor = Color.Black;
        lblpage3.Font.Bold = false;
        if (dtData.Rows.Count == 0)
        {
            lblpage3.Text = "No Record Found";
            lblpage3.Font.Bold = true;
            lblpage3.ForeColor = Color.Green;
        }
    }

  
}