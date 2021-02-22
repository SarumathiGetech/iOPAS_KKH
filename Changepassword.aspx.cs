using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.IO;
using Datalayer;
//using BusinessLayer;
public partial class Changepassword : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    SqlQuery qry = new SqlQuery(); 
    string sessionuserid = "",domain = "",EncryptedData = "",oldpassword = "";  
    int passreusecount = 0, a =0,b=0;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            txtOldPass.Focus();
            try
            {
                sessionuserid = Session["Userid"].ToString();
            }
            catch (NullReferenceException)
            {
                //Response.Redirect("opas.html?Session=End");
                Response.Redirect("iopas.html");
            }
        }
    }
    //protected void btnsubmit_Click(object sender, EventArgs e)
    protected void btnsubmit_Click(object sender, ImageClickEventArgs e)
    {
        prelogin();
        if (domain == "Local")
        {
            oldpassreader();
            Encrypt(txtOldPass.Text);
            if (oldpassword == EncryptedData)
            {
                previouspassreader();
                if (b == 0)
                {
                    int Rtnvalue = qry.pwdchange(sessionuserid = Session["Userid"].ToString(), txtconpass.Text, 1);
                    if (Rtnvalue == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated');</script>", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Error occured');</script>", false);
                    }
                }
                else if (b != 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please enter valid password');</script>", false);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Incorrect old password');</script>", false);
            }
        }
        else if (domain != "Local")
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('You are not local user!');</script>", false);
        }
    }
    // * local User or Ad user Checking * \\
    public void prelogin()
    {
      
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select d.DomainName,d.DC1,d.DC2,d.DC3,d.CN from Domain as d left join User_tbl as u on u.Domain= d.DomainID where u.UserId='" + Session["Userid"].ToString() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        domain = dr[0].ToString();    
                    }

                }
            }
        }

    }
   
    // * Password No of reuse Reader * \\
    public void passreusereader()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "Select password_reuse from security ";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        passreusecount = Convert.ToInt32(dr[0].ToString());
                    }
                    
                }
            }
        }
    }

    // * Previous Password Reader Checking * \\
    public void previouspassreader()
    {
        passreusereader();
        Encrypt(txtconpass.Text);
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select password from Password_tbl where userid='" + Session["Userid"].ToString() + "' order by id desc";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        a++;
                        if (passreusecount >= a)
                        {
                            //previouspass = dr[0].ToString();
                            if (EncryptedData == dr[0].ToString())
                            {
                                b = 1;
                                return;
                            }
                        }
                        else if (passreusecount < a)
                        {
                            return;
                        }
                    }

                }
            }
        }
    }

    // * old password Reader * \\
    public void oldpassreader()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select password from User_tbl where  userid='" + Session["Userid"].ToString() + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        oldpassword = dr[0].ToString();
                    }

                }
            }
        }
    }

    public void Encrypt(string TextToBeEncrypted)
    {
        TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider();
        //RijndaelManaged RijndaelCipher = new RijndaelManaged();          
        string Password = "CSC";
        byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(TextToBeEncrypted);
        byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
        PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);

        //ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(16), SecretKey.GetBytes(16));
        ICryptoTransform Encryptor = trip.CreateEncryptor(SecretKey.GetBytes(16), SecretKey.GetBytes(16));
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(PlainText, 0, PlainText.Length);
        cryptoStream.FlushFinalBlock();
        byte[] CipherBytes = memoryStream.ToArray();
        memoryStream.Close();
        cryptoStream.Close();
        EncryptedData = Convert.ToBase64String(CipherBytes);
        //return EncryptedData;
    }
}
