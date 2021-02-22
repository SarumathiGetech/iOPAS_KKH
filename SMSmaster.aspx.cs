using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Datalayer;
using System.Data;

public partial class SMSmaster : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    smsclass sms = new smsclass();
    string sessionuserid = "",msgtypecount = "";    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Userid"] != null)
        {
            sessionuserid = Session["Userid"].ToString();            
        }
        else
        {
            //Response.Redirect("opas.html?Session=End");
            Response.Redirect("iopas.html");
        }
        if (!IsPostBack)
        {
        SMStypereader();
        if (ddlmsgtyp.SelectedValue == "iOPAS Processing Error")
        {
            lblinque.Visible = false;
            txtinqueue.Visible = false;
            lblelapsed.Visible = false;
            txtlastprocess.Visible = false;            
        }
        }
    }
    protected void ddlmsgtyp_SelectedIndexChanged(object sender, EventArgs e)
    {
        SMStypereader();
        if (ddlmsgtyp.SelectedValue == "iOPAS Processing Error" || ddlmsgtyp.SelectedValue == "iOPAS DDS/BDS Communication Error")
        {
            lblinque.Visible = false;
            txtinqueue.Visible = false;
            lblelapsed.Visible = false;
            txtlastprocess.Visible = false;
        }
        else if (ddlmsgtyp.SelectedValue == "In Queue Alert")
        {
            lblinque.Visible = true;
            txtinqueue.Visible = true;
            lblelapsed.Visible = true;            
            txtlastprocess.Visible = true;
        }
    }
    //protected void btnsave_Click(object sender, EventArgs e)
    protected void btnsave_Click(object sender, ImageClickEventArgs e)
    {
        Countcheck();
        if (msgtypecount == "0" || msgtypecount==null)
        {
            typeinsert();
        }
        else if (msgtypecount != "0" || msgtypecount != null)
        {
            typeupdate();
        }
        
    }

    // * SMS TYPE INSERT * \\
    public void typeinsert()
    {
        if (ddlmsgtyp.SelectedValue == "iOPAS Processing Error" || ddlmsgtyp.SelectedValue == "iOPAS DDS/BDS Communication Error")
        {
           int Rtnvalue= sms.SMStypeinsert(ddlmsgtyp.SelectedItem.ToString(), 1,1, sessionuserid = Session["Userid"].ToString());
           if (Rtnvalue == 1)
           {
               ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record created');</script>", false);
           }
           else
           {
               ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occurred');</script>", false);
           }
        }
        else if (ddlmsgtyp.SelectedValue == "In Queue Alert")
        {
            int Rtnvalue = sms.SMStypeinsert(ddlmsgtyp.SelectedItem.ToString(), Convert.ToInt32(txtinqueue.Text.Trim()), Convert.ToInt32(txtlastprocess.Text.Trim()), sessionuserid = Session["Userid"].ToString());
            if (Rtnvalue == 1)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record created');</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occurred');</script>", false);
            }
        }       
    }

    // * SMS Type Update * \\
    public void typeupdate()
    {
       int Rtnvalue= sms.smstypeupdate(ddlmsgtyp.SelectedItem.ToString(), Convert.ToInt32(txtinqueue.Text.Trim()), Convert.ToInt32(txtlastprocess.Text.Trim()), sessionuserid = Session["Userid"].ToString());
       if (Rtnvalue == 1)
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record Updated');</script>", false);
       }
       else
       {
           ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error Occured');</script>", false);
       }
    }

    // * SMS TYPE COUNT CHECK * \\
    public void Countcheck()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select COUNT(*) from SMS_Type where Message_Type='" + ddlmsgtyp.SelectedItem.ToString() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        msgtypecount = dr[0].ToString();
                    }
                }
            }
        }
    }

    // * SMS MSG Type Reader * \\
    public void SMStypereader()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select Inqueue_Not_Processed,Elasped_Minutes from SMS_Type where Message_Type='" + ddlmsgtyp.SelectedItem.ToString() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtinqueue.Text = dr[0].ToString();
                        txtlastprocess.Text = dr[1].ToString();
                    }
                }
            }
        }
    }
}