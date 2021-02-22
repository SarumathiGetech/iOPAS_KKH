using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Collections.Generic;
using Datalayer;
public partial class Site : System.Web.UI.MasterPage
{    
    //*Role varible Declaration*//   
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    SqlQuery qry = new SqlQuery();    
    int sessionid, passexpday, passpromtday;
    DateTime passchgdate;
    string domainname="",hostnme="",roleid = "";
    string HasDNS = "";


    LogWriter Log = new LogWriter();
    protected void Page_Load(object sender, EventArgs e)
    {        
        //workstationread();
        //Session["location"] = txtphaloc.Text;
        //sessiontime();
        //if (sessionid == 0)
        //{
        //    sessionid = 1;
        //}
        //else
        //{
        //    Session.Timeout = sessionid;
        //}

        try
        {
            if (Session["FSTLogin"].ToString() == "YES")
            {
                HasDNS = System.Configuration.ConfigurationManager.AppSettings["HasDNS"];
                if (HasDNS.ToUpper() == "YES".ToUpper())
                {
                    hostnme = Session["HST"].ToString();
                    workstationread();
                    Session["location"]= txtphaloc.Text;
                }
                else
                {
                    hostnme = Session["HST"].ToString();
                    txtphaloc.Text = hostnme;
                    Session["location"] = txtphaloc.Text;
                }
                sessiontime();
                if (sessionid == 0)
                {
                    sessionid = 1;
                }
                else
                {
                    Session.Timeout = sessionid;
                }
               

                Session["FSTLogin"] = "NO";

                user.Text = Session["Userid"].ToString();
                lblAppserver.Text = Session["App"].ToString();

                rolenamereader();

                if (Convert.ToInt32(Session["promptcount"].ToString()) == 0)
                {
                    passchangedate();
                    if (domainname == "1")
                    {
                        passchgdate = Convert.ToDateTime(passchgdate.AddDays(passexpday).ToString("MM-dd-yyyy"));
                        DateTime dt = Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy"));

                        int dta = passchgdate.Subtract(dt).Days;

                        if (dta <= passpromtday)
                        {
                            //ViewState["promptcount"]= ++;
                            Session["promptcount"] = Convert.ToInt32(Session["promptcount"].ToString()) + 1;
                            if (dta == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Your password will expire today');</script>", false);
                            }
                            else if (dta > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Your password will expire in " + dta + " days');</script>", false);
                            }
                        }
                    }
                }
            }
            else if (Session["FSTLogin"].ToString() == "NO")
            {
                
                GC.Collect();
                hostnme = Session["HST"].ToString();
                user.Text = Session["Userid"].ToString();
                lblAppserver.Text = Session["App"].ToString();
                txtphaloc.Text = Session["location"].ToString();
                sessionid = Convert.ToInt32(Session["SessionTime"].ToString());
                rolenamereader();
                if (sessionid == 0)
                {
                    sessionid = 1;
                }
                else
                {
                    Session.Timeout = sessionid;
                }
            }
        }
        catch (NullReferenceException)
        {
            Session["promptcount"] = 0;
            if (user.Text != "")
            {
                qry.logoutdate(user.Text, hostnme);
            }

            Log.logwriter("iOPAS", "Null Exception   -" + user.Text);
            Response.Redirect("iopas.html");
        }

        ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "pagecount", "sectime(" + sessionid + ")", true);
    }

     
    protected void menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        if (menu1.SelectedItem.Value == "Log off")
        {
            HttpContext.Current.Session.Abandon();
            Session.Clear();
            Response.Cookies.Clear();
            Session["promptcount"] = 0;
            qry.logoutdate(user.Text, hostnme);
            Response.Redirect("iopas.html");          
        }
    }    

    // Session Reader Function *\\
    public void sessiontime()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select Session_Time,Password_ExpiryDays,Password_PromtDays from Security";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        sessionid = Convert.ToInt32(dr[0].ToString());
                        passexpday = Convert.ToInt32(dr[1].ToString());
                        passpromtday = Convert.ToInt32(dr[2].ToString());

                        Session["SessionTime"] = dr[0].ToString();
                    }                    
                }
            }
        }
    }
    // * User Password Change date*\\
    public void passchangedate()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select convert(varchar,u.Pwd_Change_Date,101),u.domain,u.Empid from User_tbl as u left join Pharmacy as p on p.PharmacyID=u.Pharmacy_Id  where u.userid='" + Session["Userid"].ToString() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        passchgdate = Convert.ToDateTime(dr[0].ToString());
                        domainname = dr[1].ToString();
                        Session["EmpID"] = dr[2].ToString();
                    }
                }
            }
        }
    }


    //* Role hiding Function *\\

    public void rolenamereader()
    {
        menuname();

        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select ur.RoleId From UserRole as ur left join Role as r on r.RoleID=ur.RoleId  where ur.Userid='" + user.Text + "' and r.Status ='Active'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        roleid = dr[0].ToString();
                        usermenunamereader();
                    }
                }
            }
        }
    }

    public void usermenunamereader()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select menuid from RoleControl where RoleId='" + roleid + "'";
            cmd = new SqlCommand(Commt, con);
            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr[0].ToString() == "1")
                        {
                            MenuItem BU = menu1.Items[0].ChildItems[0];
                            BU.Text = "Bottle Unloading";
                        }
                        else if (dr[0].ToString() == "2")
                        {
                            MenuItem BPL = menu1.Items[0].ChildItems[1];
                            BPL.Text = "Bottle Pre loading";
                        }
                        else if (dr[0].ToString() == "35")
                        {
                            MenuItem BFV = menu1.Items[0].ChildItems[2];
                            BFV.Text = "Bottle First Verification";
                        }
                        else if (dr[0].ToString() == "3")
                        {
                            MenuItem CU = menu1.Items[0].ChildItems[3];
                            CU.Text = "Cartridge Unloading";
                        }
                        else if (dr[0].ToString() == "4")
                        {
                            MenuItem CP = menu1.Items[0].ChildItems[4];
                            CP.Text = "Cartridge Pre Loading";
                        }

                        else if (dr[0].ToString() == "5")
                        {
                            MenuItem FV = menu1.Items[0].ChildItems[5];
                            FV.Text = "First Verification";
                        }
                        else if (dr[0].ToString() == "6")
                        {
                            MenuItem SV = menu1.Items[0].ChildItems[6];
                            SV.Text = "Second Verification";
                        }
                        else if (dr[0].ToString() == "7")
                        {
                            MenuItem CS = menu1.Items[0].ChildItems[7];
                            CS.Text = "Cartridge Status Enquiry";
                        }
                        else if (dr[0].ToString() == "8")
                        {
                            MenuItem CS = menu1.Items[0].ChildItems[8];
                            CS.Text = "DDS / BDS Cartridge Status";
                        }
                        else if (dr[0].ToString() == "34")
                        {
                            MenuItem CS = menu1.Items[0].ChildItems[9];
                            CS.Text = "BDS Cartridge Structure Status";
                        }


                        else if (dr[0].ToString() == "9")
                        {
                            MenuItem CD = menu1.Items[1].ChildItems[0];
                            CD.Text = "Print Cartridge Drug Label";
                        }

                        else if (dr[0].ToString() == "10")
                        {
                            MenuItem PQ = menu1.Items[2].ChildItems[0];
                            PQ.Text = "Queue Status Enquiry";
                        }
                        else if (dr[0].ToString() == "11")
                        {
                            MenuItem DI = menu1.Items[2].ChildItems[1];
                            DI.Text = "Drug Inventory";
                        }

                        else if (dr[0].ToString() == "12")
                        {
                            MenuItem BP = menu1.Items[3].ChildItems[0];
                            BP.Text = "Batch Order Scheduling";
                        }
                        else if (dr[0].ToString() == "13")
                        {
                            MenuItem BOE = menu1.Items[3].ChildItems[1];
                            BOE.Text = "Batch Order Enquiry";
                        }


                        else if (dr[0].ToString() == "14")
                        {
                            MenuItem GE = menu1.Items[4].ChildItems[0];
                            GE.Text = "General";

                            MenuItem DM = menu1.Items[4].ChildItems[0].ChildItems[0];
                            DM.Text = "Drug Master";
                        }
                        else if (dr[0].ToString() == "36")
                        {
                            MenuItem GE = menu1.Items[4].ChildItems[0];
                            GE.Text = "General";

                            MenuItem UOM = menu1.Items[4].ChildItems[0].ChildItems[1];
                            UOM.Text = "UOM Mapping Master";
                        }
                        else if (dr[0].ToString() == "15")
                        {
                            MenuItem GE = menu1.Items[4].ChildItems[0];
                            GE.Text = "General";

                            MenuItem CM = menu1.Items[4].ChildItems[0].ChildItems[2];
                            CM.Text = "Cartridge Master";
                        }
                        else if (dr[0].ToString() == "16")
                        {
                            MenuItem GE = menu1.Items[4].ChildItems[0];
                            GE.Text = "General";

                            MenuItem LM = menu1.Items[4].ChildItems[0].ChildItems[3];
                            LM.Text = "Loading Master";
                        }
                        else if (dr[0].ToString() == "17")
                        {
                            MenuItem GE = menu1.Items[4].ChildItems[0];
                            GE.Text = "General";

                            MenuItem LP = menu1.Items[4].ChildItems[0].ChildItems[4];
                            LP.Text = "Printer Master";
                        }
                        else if (dr[0].ToString() == "18")
                        {
                            MenuItem GE = menu1.Items[4].ChildItems[0];
                            GE.Text = "General";

                            MenuItem RR = menu1.Items[4].ChildItems[0].ChildItems[5];
                            RR.Text = "Reason Master";
                        }
                        else if (dr[0].ToString() == "19")
                        {
                            MenuItem SY = menu1.Items[4].ChildItems[1];
                            SY.Text = "System";

                            MenuItem PH = menu1.Items[4].ChildItems[1].ChildItems[0];
                            PH.Text = "Pharmacy Location Master";
                        }
                        else if (dr[0].ToString() == "20")
                        {
                            MenuItem SY = menu1.Items[4].ChildItems[1];
                            SY.Text = "System";

                            MenuItem DDS = menu1.Items[4].ChildItems[1].ChildItems[1];
                            DDS.Text = "DDS / BDS Master";
                        }
                        else if (dr[0].ToString() == "21")
                        {
                            MenuItem SY = menu1.Items[4].ChildItems[1];
                            SY.Text = "System";

                            MenuItem QT = menu1.Items[4].ChildItems[1].ChildItems[2];
                            QT.Text = "Queue Series";
                        }
                        else if (dr[0].ToString() == "22")
                        {
                            MenuItem SY = menu1.Items[4].ChildItems[1];
                            SY.Text = "System";

                            MenuItem PM = menu1.Items[4].ChildItems[1].ChildItems[3];
                            PM.Text = "Processing Master";
                        }
                        else if (dr[0].ToString() == "23")
                        {
                            MenuItem SY = menu1.Items[4].ChildItems[1];
                            SY.Text = "System";

                            MenuItem TRM = menu1.Items[4].ChildItems[1].ChildItems[4];
                            TRM.Text = "RFID Master";
                        }
                        else if (dr[0].ToString() == "24")
                        {
                            MenuItem SY = menu1.Items[4].ChildItems[1];
                            SY.Text = "System";

                            MenuItem RR = menu1.Items[4].ChildItems[1].ChildItems[5];
                            RR.Text = "SMS Master";
                        }
                        else if (dr[0].ToString() == "25")
                        {
                            MenuItem SY = menu1.Items[4].ChildItems[1];
                            SY.Text = "System";

                            MenuItem RR = menu1.Items[4].ChildItems[1].ChildItems[6];
                            RR.Text = "SMS Alert";
                        }


                        else if ((dr[0].ToString() == "26") && (Session["Userid"].ToString() == "system"))
                        {
                            MenuItem SY = menu1.Items[4].ChildItems[1];
                            SY.Text = "System";

                            MenuItem DP = menu1.Items[4].ChildItems[1].ChildItems[7];
                            DP.Text = "Drug Pack Type";
                        }

                        else if (dr[0].ToString() == "27")
                        {
                            MenuItem SC = menu1.Items[4].ChildItems[2];
                            SC.Text = "Security";

                            MenuItem SE = menu1.Items[4].ChildItems[2].ChildItems[0];
                            SE.Text = "User Creation";
                        }
                        else if (dr[0].ToString() == "28")
                        {
                            MenuItem SC = menu1.Items[4].ChildItems[2];
                            SC.Text = "Security";

                            MenuItem RO = menu1.Items[4].ChildItems[2].ChildItems[1];
                            RO.Text = "Role Administration";
                        }
                        else if (dr[0].ToString() == "29")
                        {
                            MenuItem SC = menu1.Items[4].ChildItems[2];
                            SC.Text = "Security";

                            MenuItem SEC = menu1.Items[4].ChildItems[2].ChildItems[2];
                            SEC.Text = "Security";
                        }
                        else if (dr[0].ToString() == "30")
                        {
                            MenuItem SC = menu1.Items[4].ChildItems[2];
                            SC.Text = "Security";

                            MenuItem HN = menu1.Items[4].ChildItems[2].ChildItems[3];
                            HN.Text = "WorkStation";
                        }
                        else if (dr[0].ToString() == "31")
                        {
                            MenuItem SC = menu1.Items[4].ChildItems[2];
                            SC.Text = "Security";

                            MenuItem DO = menu1.Items[4].ChildItems[2].ChildItems[4];
                            DO.Text = "Domain";
                        }
                        else if (dr[0].ToString() == "32")
                        {
                            MenuItem UA = menu1.Items[5].ChildItems[0];
                            UA.Text = "User Alert";
                        }

                        // Tempe \\\---------------

                        else if (dr[0].ToString() == "33")
                        {
                            btnreport.Visible = true;
                        }
                    }
                }               
            }
        }
    }


    //--------------------------------------      

    public void menuname()
    {
        MenuItem CU = menu1.Items[0].ChildItems[0];
        CU.Text = "  ";
        MenuItem BU = menu1.Items[0].ChildItems[1];
        BU.Text = "  ";
        MenuItem BFV = menu1.Items[0].ChildItems[2];
        BFV.Text = "  ";
        MenuItem CP = menu1.Items[0].ChildItems[3];
        CP.Text = "  ";
        MenuItem BPL = menu1.Items[0].ChildItems[4];
        BPL.Text = "  ";
        MenuItem FV = menu1.Items[0].ChildItems[5];
        FV.Text = "  ";
        MenuItem SV = menu1.Items[0].ChildItems[6];
        SV.Text = "  ";
        MenuItem PL = menu1.Items[0].ChildItems[7];
        PL.Text = "  ";
        MenuItem CS = menu1.Items[0].ChildItems[8];
        CS.Text = "  ";
        MenuItem VCS = menu1.Items[0].ChildItems[9];
        VCS.Text = "  ";

        //MenuItem AS = menu1.Items[1].ChildItems[0];
        //AS.Text = "  ";
        //MenuItem QP = menu1.Items[1].ChildItems[0];
        //QP.Text = "  ";
        //MenuItem PD = menu1.Items[3].ChildItems[0];
        //PD.Text = "  ";
        MenuItem CD = menu1.Items[1].ChildItems[0];
        CD.Text = "  ";

        //MenuItem BL = menu1.Items[1].ChildItems[1];
        //BL.Text = "  ";

        MenuItem PQ = menu1.Items[2].ChildItems[0];
        PQ.Text = "  ";
        MenuItem DI = menu1.Items[2].ChildItems[1];
        DI.Text = "  ";
        //MenuItem DA = menu1.Items[2].ChildItems[2];
        //DA.Text = "  ";
        //MenuItem mla = menu1.Items[5];
        //mla.Enabled = false;
        MenuItem BP = menu1.Items[3].ChildItems[0];
        BP.Text = "  ";
        MenuItem BOE = menu1.Items[3].ChildItems[1];
        BOE.Text = "  ";
        MenuItem GE = menu1.Items[4].ChildItems[0];
        GE.Text = "  ";
        MenuItem DM = menu1.Items[4].ChildItems[0].ChildItems[0];
        DM.Text = "  ";
        MenuItem UOM = menu1.Items[4].ChildItems[0].ChildItems[1];
        UOM.Text = "  ";
        MenuItem CM = menu1.Items[4].ChildItems[0].ChildItems[2];
        CM.Text = "  ";
        MenuItem LM = menu1.Items[4].ChildItems[0].ChildItems[3];
        LM.Text = "  ";       
           
        MenuItem LP = menu1.Items[4].ChildItems[0].ChildItems[4];
        LP.Text = "  ";

        MenuItem RR = menu1.Items[4].ChildItems[0].ChildItems[5];
        RR.Text = "  ";

        MenuItem SY = menu1.Items[4].ChildItems[1];
        SY.Text = "  ";

        MenuItem PH = menu1.Items[4].ChildItems[1].ChildItems[0];
        PH.Text = "  ";

        MenuItem DDS = menu1.Items[4].ChildItems[1].ChildItems[1];
        DDS.Text = "  ";

        MenuItem QT = menu1.Items[4].ChildItems[1].ChildItems[2];
        QT.Text = "  ";

        MenuItem PM = menu1.Items[4].ChildItems[1].ChildItems[3];
        PM.Text = "  ";

        MenuItem TR = menu1.Items[4].ChildItems[1].ChildItems[4];
        TR.Text = "  ";

        MenuItem SMSMAS = menu1.Items[4].ChildItems[1].ChildItems[5];
        SMSMAS.Text = "  ";

        MenuItem SMSALT = menu1.Items[4].ChildItems[1].ChildItems[6];
        SMSALT.Text = "  ";
      
        MenuItem DP = menu1.Items[4].ChildItems[1].ChildItems[7];
        DP.Text = "  ";

        MenuItem SC = menu1.Items[4].ChildItems[2];
        SC.Text = "  ";
        MenuItem SE = menu1.Items[4].ChildItems[2].ChildItems[0];
        SE.Text = "  ";
        MenuItem RO = menu1.Items[4].ChildItems[2].ChildItems[1];
        RO.Text = "  ";
        MenuItem SEC = menu1.Items[4].ChildItems[2].ChildItems[2];
        SEC.Text = "  ";
        MenuItem HN = menu1.Items[4].ChildItems[2].ChildItems[3];
        HN.Text = "  ";
        MenuItem DO = menu1.Items[4].ChildItems[2].ChildItems[4];
        DO.Text = "  ";
        MenuItem UA = menu1.Items[5].ChildItems[0];
        UA.Text = "  ";


        btnreport.Visible = false;

    }

   // * Workstation Reader * \\
    public void workstationread()
    {  
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select p.location_name from Workstation as w left join Pharmacy as p on p.PharmacyID=w.PharmacyID where w.status='Active' and w.[Host_Name]='" + hostnme + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtphaloc.Text = dr[0].ToString();
                    }                    
                }
            }
        }
    }

    protected void btnreport_Click(object sender, EventArgs e)
    {       
        if (Session["Userid"] != null)
        {
            string servername = System.Configuration.ConfigurationManager.AppSettings["Servername"];       

            string externalUrl = servername;
            string url = "window.open('" + externalUrl + "', '','');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", url, true);            
        }  
        else
        {
            Response.Redirect("iopas.html?Session=End");
        }      
    }

    protected void btnmanual_Click(object sender, EventArgs e)
    {
        if (user.Text != "")
        {
            qry.logoutdate(user.Text, hostnme);
        }
        Response.Redirect("iopas.html");    
    }

    protected void llk_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Session.Abandon();
        GC.Collect();
        Session.Clear();
        Response.Cookies.Clear();
        Session["promptcount"] = 0;
        qry.logoutdate(user.Text, hostnme);
        Response.Redirect("iopas.html"); 
    }
}
