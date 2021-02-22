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

public partial class Drugalertauto : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    Searchquery sear = new Searchquery();
    SqlQuery qry = new SqlQuery();
    string sessionuserid = "", location = "";
    protected void Page_Load(object sender, EventArgs e)
    {               
        if (Session["Userid"] != null)
        {
            sessionuserid = Session["Userid"].ToString();
            user.Text = Session["Userid"].ToString();
            location = Session["location"].ToString();
            txtlocation.Text = location.ToString();
        }
        else
        {
            //Response.Redirect("opas.html?Session=End");
            Response.Redirect("iopas.html");
        }
        if (location != "")
        {
            //lblresult.Text = System.DateTime.Now.ToString("dd-MMM-yyyy:HH:mm");
            lastorderdatetimequeue();
            pageload(); 
            if (!IsPostBack)
            {
               
               
            }
        }
        else if (location == "" || location == null)
        {
            pp.Visible = false;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Your Work Station Not Authorised to Do This Operation');</script>", false);
            return;
        }      
    }
    protected void btnhome_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    public void pageload()
    {
        txtalet.Text = Convert.ToString(sear.DDSAlert(ddsalertgrid, location));
        txtlowsta.Text = Convert.ToString(sear.lowstock(lowstockgrid, location));
       // txtptop.Text = Convert.ToString(sear.pendingtopack(pendingpackgrid, location));
       // txtptoa.Text = Convert.ToString(sear.pendingtoassemble(pendingassembledgrid, location));
        processdowntime();
        DDSdowntime();
    }

  


    // * Last Order Received Date Time and Queue Number * \\

    public void lastorderdatetimequeue()
    {

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select top(1) CONVERT(varchar(17),m.OPAS_Interface_Received_DateTime,113),ob.Queue_Number from HL7_MSH as m left join HL7_OBX as ob on ob.Msg_Control_ID=m.Msg_Control_ID left join Pharmacy as p on p.Location_code=ob.Pharmacy_Store where CONVERT(varchar,m.Opas_Received_Datetime,103)=CONVERT(varchar,getdate(),103) and p.Location_Name='" + location + "'  order by ID desc ";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lblresult.Text = dr[0].ToString();
                        txtqueueno.Text = dr[1].ToString();
                    }
                }
            }
        }
    }

    // * Processing Down Time Reade * \\
    public void processdowntime()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "SELECT convert(varchar(20),Last_Updated_DateTime,113) as time FROM DDS_Running_Status where Interface_Name='Process' and DATEADD(S,15,Last_Updated_DateTime)< GETDATE() and convert(varchar,Last_Updated_DateTime,103)=convert(varchar,GETDATE(),103)";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtprocesstime.Text = dr[0].ToString();
                    }
                }
            }
        }
    }
    // *DDS Down Time Reade * \\
    public void DDSdowntime()
    {

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "SELECT top(1)convert(varchar(20),ds.Error_Dt,113) as time FROM DDSError as ds left join DDS as d on d.DDS_Name=ds.DDS_Name left join Pharmacy as p on p.PharmacyID=d.PharmacyID where convert(varchar,ds.Error_Dt,103)=CONVERT(varchar,getdate(),103) and p.Location_Name='" + location + "' and d.Status='Active' and ds.Status='NEW' and d.DDSConnection='NO' order by ds.Error_Dt asc";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtddstime.Text = dr[0].ToString();
                    }
                }
            }
        }
    }

    protected void lnklogout_Click(object sender, EventArgs e)
    {
        string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_ADDR"]).HostName.Split(new Char[] { '.' });
        computer_name[0].ToString();
        Session.Abandon();
        Response.Cookies.Clear();      
        qry.logoutdate(user.Text,computer_name[0].ToString());
        Response.Redirect("iOpas.html");
    }
    protected void pendingpackgrid_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void pendingpackgrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //txtptop.Text = Convert.ToString(sear.pendingtopack(pendingpackgrid, location));
        //pendingpackgrid.PageIndex = e.NewPageIndex;
       // pendingpackgrid.DataBind();
    }
  
}
