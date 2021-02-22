using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using Datalayer;
using System.Data;

public partial class Changepwd : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    SqlQuery qry = new SqlQuery();
    string sessionuserid = "", EncryptedData = "", oldpassword = "";
    int passreusecount = 0, a = 0, b = 0;    
    protected void Page_Load(object sender, EventArgs e)
    {       
        if (!IsPostBack)
        {          
            try
            {
                sessionuserid = Session["Userid"].ToString();
               // oldpassword = Request.QueryString["pwd"].ToString();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please change your password!');</script>", false);
                txtOldPass.Focus();
            }
            catch (NullReferenceException)
            {
                Response.Redirect("iopas.html");
            }
        }
    }
    //protected void btnupdate_Click(object sender, EventArgs e)
    protected void btnupdate_Click(object sender, ImageClickEventArgs e)
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
                   // qry.logindate(sessionuserid.Trim(),"");
                    //Response.Redirect("Home.aspx?pwdval=" + 0);             
                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "popup", "Success()", true);
                    //Response.Redirect("opas.html");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Record updated');</script>", false);
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
    }
}