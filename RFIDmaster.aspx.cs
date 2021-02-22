using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Datalayer;
public partial class RFIDmaster : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    Trigger tri = new Trigger();
    string sessionuserid = "", mode = "";
    int rtval;
   
    protected void Page_Load(object sender, EventArgs e)
    {    

        if (Session["Userid"] != null)
        {
            sessionuserid = Session["Userid"].ToString();
            if (!IsPostBack)
            {
                locationreade();
                Triggerreader();
                griddisplay();
            }
        }
        else
        {            
            Response.Redirect("iopas.html");
        }        
    }
    //protected void btnsubmit_Click(object sender, EventArgs e)
    protected void btnsubmit_Click(object sender, ImageClickEventArgs e)
    {
        insert();
        griddisplay();
    }  
  

    public void insert()
    {
        if (ChkNormal.Checked == true)
        {
            mode = "Normal";
        }
        else if (chkTrigger.Checked == true)
        {
            mode = "Trigger";
        }
        else
        {
            mode = "";
        }

        //Empty = "False";
        //Autass = "True";
        //JumQue = "True";

        if (mode != "")
        {
            rtval = tri.Triggerinsert(ddlpharmloc.SelectedValue.ToString(), mode, sessionuserid);

            if (rtval == 1)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record created');</script>", false);
            }
            else if (rtval == 2)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record Updated');</script>", false);
            }
            else if (rtval == 3)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record Updated');</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
            }
        }
        else if (mode == "")
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Operation Mode is not selected');</script>", false);
        }
    }

    public void locationreade()
    {
       using (SqlConnection con = DBCon.getstring())
       {
           string Commt = "Select Location_Name from pharmacy where status='Active'";
           cmd = new SqlCommand(Commt, con);

           con.Open();
           using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
           {
               if (dr.HasRows)
               {
                   while (dr.Read())
                   {
                       ddlpharmloc.Items.Add(dr[0].ToString());
                   }
               }
           }
       }
    }


    // Grid Display \\
    public void griddisplay()
    {
        tri.griddisp(griddetail);
    }

    // * Trigger Date Reader * \\
    public void Triggerreader()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select  t.Mode from Trigger_Master as t left join Pharmacy as p on p.PharmacyID=t.PharmacyID where p.Location_Name='" + ddlpharmloc.SelectedValue.ToString() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr[0].ToString() == "Normal")
                        {
                            ChkNormal.Checked = true;
                            chkTrigger.Checked = false;
                        }
                        else if (dr[0].ToString() == "Trigger")
                        {
                            ChkNormal.Checked = false;
                            chkTrigger.Checked = true;
                        }
                    }
                }
                if (dr.HasRows == false)
                {

                }
            }
        }
    }

    protected void ddlpharmloc_SelectedIndexChanged(object sender, EventArgs e)
    {
        Triggerreader();
    }

    protected void ChkNormal_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkNormal.Checked == true)
        {
            ChkNormal.Checked = true;
            chkTrigger.Checked = false;
        }
        else if (ChkNormal.Checked == false)
        {
            ChkNormal.Checked = false;
            chkTrigger.Checked = false;
        }
    }
    protected void chkTrigger_CheckedChanged(object sender, EventArgs e)
    {
        if (chkTrigger.Checked == true)
        {
            ChkNormal.Checked = false;
            chkTrigger.Checked = true;
        }
        else if (chkTrigger.Checked == false)
        {
            ChkNormal.Checked = false;
            chkTrigger.Checked = false;
        }
    }
}