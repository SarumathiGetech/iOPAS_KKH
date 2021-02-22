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
public partial class securityadmin : System.Web.UI.Page
{
    // * Connection Class and Sql Query Object Creation * \\  
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    SqlQuery qry = new SqlQuery();
    grid gri = new grid();
    // * UserID Get from Session * \\
    string sessionuserid="";
    // * ------------------------ * \\
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            try
            {
                sessionuserid = Session["Userid"].ToString();
                griddisplay();
                securityreader();
            }
            catch (NullReferenceException)
            {
                //Response.Redirect("opas.html?Session=End");
                Response.Redirect("iopas.html");
            }
        }
    }
    //protected void btnsave_Click(object sender, EventArgs e)
    protected void btnsave_Click(object sender, ImageClickEventArgs e)
    {
        securityinsert();
        griddisplay();
    }
    // * Display The Value to Gridview * \\
    public void griddisplay()
    {
        gri.Securitygrid(gridsecurity);
    }
    protected void gridsecurity_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {        
        txtsession.Text = (gridsecurity.Rows[e.NewSelectedIndex].Cells[1].Text);
        txtretries.Text = (gridsecurity.Rows[e.NewSelectedIndex].Cells[2].Text);
        txtpassreuse.Text =(gridsecurity.Rows[e.NewSelectedIndex].Cells[3].Text);
        txtpassexpiry.Text =(gridsecurity.Rows[e.NewSelectedIndex].Cells[4].Text);
        txtpromt.Text =(gridsecurity.Rows[e.NewSelectedIndex].Cells[5].Text);
        txtinactivate.Text = (gridsecurity.Rows[e.NewSelectedIndex].Cells[6].Text);       
    }
    // * Security Insert Function * \\
    public void securityinsert()
    {     
      int Rtnvalue=qry.Securityinsert(Convert.ToInt32(txtsession.Text.Trim()), Convert.ToInt32(txtretries.Text.Trim()), Convert.ToInt32(txtpassreuse.Text.Trim()), Convert.ToInt32(txtpassexpiry.Text.Trim()), Convert.ToInt32(txtpromt.Text.Trim()), Convert.ToInt32(txtinactivate.Text.Trim()), sessionuserid = Session["Userid"].ToString());
      if (Rtnvalue == 1)
      {
          ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Record created ');</script>", false);
          //clear();
      }
      else if (Rtnvalue == 2)
      {
          ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Record updated ');</script>", false);
          //clear();
      }
      else
      {
          ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert(' Error Occured ');</script>", false);
      }
    }

    // * Security reader *\\
    public void securityreader()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select Session_Time,NoOfRetries,Password_Reuse,Password_ExpiryDays,Password_PromtDays,Inactive_user from Security ";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtsession.Text = dr[0].ToString();
                        txtretries.Text = dr[1].ToString();
                        txtpassreuse.Text = dr[2].ToString();
                        txtpassexpiry.Text = dr[3].ToString();
                        txtpromt.Text = dr[4].ToString();
                        txtinactivate.Text = dr[5].ToString();
                    }
                }
            }
        }

    }
   
    // * Security Textbox clear Function * \\
    public void clear()
    {
        txtsession.Text = "";
        txtretries.Text = "";
        txtpromt.Text = "";
        txtpassreuse.Text = "";
        txtpassexpiry.Text = "";
        txtinactivate.Text = "";      
    }
  
}
