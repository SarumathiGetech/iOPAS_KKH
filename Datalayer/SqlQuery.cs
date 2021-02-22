using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Configuration;
using System.Security.Cryptography;
using System.Xml;
using System.IO;

namespace Datalayer
{
   public class SqlQuery
    {
       DB_Connection con = new DB_Connection();
       SqlCommand cmd = new SqlCommand();
       string EncryptedData = "",DecryptedData = "";
      
       
       
       
       // * Common Active / Inactive Audit trail table Insert Procedure * \\
       public void commonactinact(string id, string name, string status, string updateby)
       {           
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "AUD_Active_Inactive";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = id.ToString();
           cmd.Parameters.Add("@Name", SqlDbType.VarChar, 20).Value = name.ToString();
           cmd.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = status.ToString();
           cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = updateby.ToString();
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
       }
       
 

       public int loginchk(string userid,string pword )
       {
           Encrypt(pword);           
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "PreLogin_Check";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = userid.ToString();
           cmd.Parameters.Add("@Password", SqlDbType.VarChar, 100).Value = EncryptedData.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int retvalue = (int)par.Value;
           return retvalue;
       }

       // * Local user password change and update * \\
       public int pwdchange(string userid, string pwd,int loggedin)
       {
           Encrypt(pwd);
           
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Password_Change";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = userid.ToString();           
           cmd.Parameters.Add("@Password", SqlDbType.VarChar, 100).Value = EncryptedData.ToString();
           cmd.Parameters.Add("@Loggedin", SqlDbType.Int).Value = loggedin;
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int retvalue = (int)par.Value;
           return retvalue;
       }      
  

       // * Login Date time Update * \\
       public void logindate(string userid,string hostname)
       {           
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "UserAccess_Login";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = userid.ToString();
           cmd.Parameters.Add("@Hostname", SqlDbType.VarChar, 50).Value = hostname.ToString();
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();  
       }

       // * Logout Date Time Update * \\
       public void logoutdate(string userid,string hostname)
       {           
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "UserAccess_Logout";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = userid.ToString();
           cmd.Parameters.Add("@Hostname", SqlDbType.VarChar, 50).Value = hostname.ToString();
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close(); 
       }     



       // * -------------------------------------------------------------------------------------------------- \\
       //* USER INSERT PROCEDURE *\\

       public int userinsert(string userid,string empid, string username,string password, string domainname, string locationname, string createdby,
        string updatedby,int loggedin, string remarks,DateTime efffrom,string effto, string status)
       {          
           Encrypt(password);          
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "User_Insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = userid.ToString();
           cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = username.ToString();
           cmd.Parameters.Add("@Empid", SqlDbType.VarChar, 20).Value = empid.ToString();
           cmd.Parameters.Add("@Password", SqlDbType.VarChar, 100).Value = EncryptedData.ToString();
           cmd.Parameters.Add("@Domainname", SqlDbType.VarChar, 100).Value = domainname.ToString();
           cmd.Parameters.Add("@Locationname", SqlDbType.VarChar, 100).Value = locationname.ToString();
           cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 20).Value = createdby.ToString();          
           cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 20).Value = updatedby.ToString();   
           cmd.Parameters.Add("@Loggedin", SqlDbType.Int).Value = loggedin.ToString();
           cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 255).Value = remarks.ToString();
           cmd.Parameters.Add("@Effectivefrom", SqlDbType.Date).Value = efffrom;
           if (effto == "")
           {
               cmd.Parameters.Add("@Effectiveto", SqlDbType.Date).Value = DBNull.Value;
           }
           else
           {
               cmd.Parameters.Add("@Effectiveto", SqlDbType.Date).Value = DateTime.Parse(effto).ToString();
           }           
           cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = status.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int retvalue = (int)par.Value;
           return retvalue;

       }
       // * USER UPDATE PROCEDURE * \\

       public int userupdate(string userid,string empide, string username, string domainname, string locationname,
          string updatedby, string remarks,DateTime efffrom,string effto,string status)
       {           
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "user_update";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = userid.ToString();
           cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = username.ToString();
           cmd.Parameters.Add("@Empid", SqlDbType.VarChar, 20).Value = empide.ToString();
           cmd.Parameters.Add("@Domainname", SqlDbType.VarChar, 100).Value = domainname.ToString();
           cmd.Parameters.Add("@Locationname", SqlDbType.VarChar, 100).Value = locationname.ToString();
           cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 20).Value = updatedby.ToString();
           cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 255).Value = remarks.ToString();
           cmd.Parameters.Add("@Effectivefrom", SqlDbType.Date).Value = efffrom;
           if (effto == "")
           {
               cmd.Parameters.Add("@Effectiveto", SqlDbType.Date).Value = DBNull.Value;
           }
           else
           {
               cmd.Parameters.Add("@Effectiveto", SqlDbType.Date).Value = DateTime.Parse(effto).ToString();
           }
           cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = status.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int retvalue = (int)par.Value;
           return retvalue;
       }



       // * User Role Insert * \\
       public void userroleinsert(string rolename, string userid)
       {           
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "userrole_insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Rolename", SqlDbType.VarChar, 20).Value = rolename.ToString();
           cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = userid.ToString();
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();          
       }

       public void Audroleupdate(string userid)
       {           
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "AUD_New_Role_Update";
           cmd.CommandType = CommandType.StoredProcedure;          
           cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = userid.ToString();
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
       }

       // * User Role Update * \\
       public void userroleupdate(string userid)
       {
           string cmd = "Delete from UserRole where UserId ='" + userid + "'";
           con.SetDataBase(cmd);
       }

       //---------------------------------------------------------------------------------\\
       //* PHARMACY LOCATION INSERT PROCEDURE*\\

       public int pharmacyinsert(string pharmcode,string pharmName,string PharAbbr, string lblheaderi,string Lblhospitalnme, string lblfooteri, string userid,string status,string cartprefix)
       {           
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Pharm_insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@pharmacycode", SqlDbType.VarChar, 10).Value = pharmcode.ToString();
           cmd.Parameters.Add("@Location", SqlDbType.VarChar, 50).Value = pharmName.ToString();
           cmd.Parameters.Add("@Phar_Abbr", SqlDbType.Char, 1).Value = PharAbbr.ToString();
           cmd.Parameters.Add("@Labelheader", SqlDbType.VarChar, 100).Value = lblheaderi.ToString();
           cmd.Parameters.Add("@Labelfooter_HospitalName", SqlDbType.VarChar, 100).Value = Lblhospitalnme.ToString();
           cmd.Parameters.Add("@Labelfooter", SqlDbType.VarChar, 255).Value = lblfooteri.ToString();
           cmd.Parameters.Add("@Createdby", SqlDbType.VarChar, 20).Value = userid.ToString();
           cmd.Parameters.Add("@Status ", SqlDbType.VarChar, 10).Value = status.ToString();          
           cmd.Parameters.Add("@Cartridgeprefix", SqlDbType.VarChar, 2).Value = cartprefix.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int rtnval = (int)par.Value;
           return rtnval;
       }

       //* PHARMACY LOCATION UPDATE PROCEDURE*\\

       public int pharmacyupdate(int pharmacyid, string pharmcode, string pharmName, string PharAbbr, string lblheader, string Lblhospitalnme, string lblfooter, string userid, string status)
       {           
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Pharm_update";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@pharmacyid", SqlDbType.Int).Value=pharmacyid;
           cmd.Parameters.Add("@pharmacycode", SqlDbType.VarChar, 10).Value = pharmcode.ToString();
           cmd.Parameters.Add("@Location", SqlDbType.VarChar, 50).Value = pharmName.ToString();
           cmd.Parameters.Add("@Phar_Abbr", SqlDbType.Char, 1).Value = PharAbbr.ToString();
           cmd.Parameters.Add("@Labelheader", SqlDbType.VarChar, 100).Value = lblheader.ToString();
           cmd.Parameters.Add("@Labelfooter_HospitalName", SqlDbType.VarChar, 100).Value = Lblhospitalnme.ToString();
           cmd.Parameters.Add("@Labelfooter", SqlDbType.VarChar, 255).Value = lblfooter.ToString();
           cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = userid.ToString();
           cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = status.ToString();
           
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int rtnval = (int)par.Value;
           return rtnval;
       }
      
       //-------------------------------------------------------------------------------------\\

       //* NEW ROLE CREATION INSERT PROCEDURE*\\

       public int roleinsert(string roleid, string description, string created, string updatedby)
       {           
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "role_insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Rolename", SqlDbType.VarChar, 20).Value = roleid.ToString();           
           cmd.Parameters.Add("@Description", SqlDbType.VarChar, 255).Value = description.ToString();
           cmd.Parameters.Add("@Createdby", SqlDbType.VarChar, 20).Value = created.ToString();
           cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = updatedby.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int retvalue = (int)par.Value;
           return retvalue;
       }
       // ----------------------------------------------------------------------------------------------- \\
       //*  ROLE UPDATE PROCEDURE*\\

       public int roleupdate(string rolename, string description, string updatedby, int roleid)
       {           
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "role_update";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Rolename", SqlDbType.VarChar, 20).Value = rolename.ToString();          
           cmd.Parameters.Add("@Description", SqlDbType.VarChar, 255).Value = description.ToString();
           cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = updatedby.ToString();
           cmd.Parameters.Add("@roleid", SqlDbType.Int).Value = roleid;
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int retvalue = (int)par.Value;
           return retvalue;
       }
       // ----------------------------------------------------------------------------------------------- \\
       // * New Domain Insert Procedure * \\

       public int Domaininsert(string Domainname,string CN,string dc2,string dc3, string description, string created,string Domainnme)
       {           
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Domain_insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Domain", SqlDbType.VarChar, 100).Value = Domainname.ToString();
           cmd.Parameters.Add("@CN", SqlDbType.VarChar, 20).Value = CN.ToString();
           cmd.Parameters.Add("@DCtwo", SqlDbType.VarChar, 10).Value = dc2.ToString();
           cmd.Parameters.Add("@DCthree", SqlDbType.VarChar, 10).Value = dc3.ToString();
           cmd.Parameters.Add("@Domainname", SqlDbType.VarChar, 20).Value = Domainnme.ToString();
           cmd.Parameters.Add("@Description", SqlDbType.VarChar, 255).Value = description.ToString();
           cmd.Parameters.Add("@Createdby", SqlDbType.VarChar, 20).Value = created.ToString();
           cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = created.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int retvalue = (int)par.Value;
           return retvalue;           
       }

       // * Domain Update Procedure * \\

       public int Domainupdate(string Domainname,string CN, string dc2, string dc3, string description, string created, int domainid,string Domainnme)
       {           
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Domain_update";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Domain", SqlDbType.VarChar, 50).Value = Domainname.ToString();
           cmd.Parameters.Add("@CN", SqlDbType.VarChar, 20).Value = CN.ToString();
           cmd.Parameters.Add("@DCtwo", SqlDbType.VarChar, 10).Value = dc2.ToString();
           cmd.Parameters.Add("@DCthree", SqlDbType.VarChar, 10).Value = dc3.ToString();
           cmd.Parameters.Add("@Domainname", SqlDbType.VarChar, 20).Value = Domainnme.ToString();
           cmd.Parameters.Add("@Description", SqlDbType.VarChar, 255).Value = description.ToString();
           cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = created.ToString();
           cmd.Parameters.Add("@domainid", SqlDbType.Int).Value = domainid;
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int retvalue = (int)par.Value;
           return retvalue;
       }
       // -------------------------------------------------------------------------------------------------- \\
       // * Security Insert Procedure * \\

       public int Securityinsert(int Session, int Retries, int Reuse, int Expiry, int Promt,int inactive,string Created)
       {           
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Security_insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Sessiontime", SqlDbType.Int).Value = Session;
           cmd.Parameters.Add("@Noofretries", SqlDbType.Int).Value = Retries;
           cmd.Parameters.Add("@Passwordreuse", SqlDbType.Int).Value = Reuse;
           cmd.Parameters.Add("@Passwordexpiry", SqlDbType.Int).Value = Expiry;
           cmd.Parameters.Add("@passwordpromt", SqlDbType.Int).Value = Promt;
           cmd.Parameters.Add("@inactive", SqlDbType.Int).Value = inactive;       
           cmd.Parameters.Add("@Createdby", SqlDbType.VarChar, 20).Value = Created.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int retvalue = (int)par.Value;
           return retvalue;
       }
       // --------------------------------------------------------------------------------- \\
    
       // ** QUEUE TYPE MASTER ** \\
       // * Queue type Insert * \\
       public int queueinsert(string pharmid, string queuetye, string queuefrom, string queueto, string description, string created)
       {
           
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Queue_insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@pharmid", SqlDbType.VarChar, 50).Value = pharmid.ToString();
           cmd.Parameters.Add("@Queuetype", SqlDbType.VarChar, 50).Value = queuetye.ToString();
           cmd.Parameters.Add("@Queuefrom", SqlDbType.VarChar, 10).Value = queuefrom.ToString();
           cmd.Parameters.Add("@Queueto", SqlDbType.VarChar, 10).Value = queueto.ToString();
           cmd.Parameters.Add("@Description", SqlDbType.VarChar, 255).Value = description.ToString();
           cmd.Parameters.Add("@Createdby", SqlDbType.VarChar, 20).Value = created.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int retvalue = (int)par.Value;
           return retvalue;
       }
       // * Queue type Update * \\
       public int queueupdate(string pharmid, string queuetye, string queuefrom, string queueto, string description, string created, int queueid)
       {
           
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Queue_update";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@pharmid", SqlDbType.VarChar, 50).Value = pharmid.ToString();
           cmd.Parameters.Add("@Queuetype", SqlDbType.VarChar, 50).Value = queuetye.ToString();
           cmd.Parameters.Add("@Queuefrom", SqlDbType.VarChar, 10).Value = queuefrom.ToString();
           cmd.Parameters.Add("@Queueto", SqlDbType.VarChar, 10).Value = queueto.ToString();
           cmd.Parameters.Add("@Description", SqlDbType.VarChar, 255).Value = description.ToString();
           cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = created.ToString();
           cmd.Parameters.Add("@queueid ", SqlDbType.Int).Value = queueid;
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int retvalue = (int)par.Value;
           return retvalue;
       }   

           

       // * DDS Name Display Grid * \\
       public void queueddsname(GridView grid,string pharlocaname)
       {
           string commt = "Select d.DDS_Name from DDS as d left join Pharmacy as p on p.PharmacyID=d.PharmacyID  where p.Location_Name='" + pharlocaname + "' and d.Status='Active'";
           con.View(commt, grid);
       }       
       public void Queueallocationinsert(string  pharmname, string ddsno, string queuetype)
       {
         
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Queue_Allot_Insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Pharmacyname", SqlDbType.VarChar, 50).Value = pharmname.ToString();
           cmd.Parameters.Add("@Queuetype", SqlDbType.VarChar, 50).Value = queuetype.ToString();
           cmd.Parameters.Add("@DDSName", SqlDbType.VarChar, 10).Value = ddsno.ToString();           
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
       }
       // * Queue Type Enable * \\
       public void queuetypeenabled(int queueid,string updatedby)
       {
           string cmd = "update Queue_Master set status='Active',Updated_by='"+updatedby+"',Updated_Date='"+System.DateTime.Now+"' where Queueid='" + queueid + "'";
           con.SetDataBase(cmd);
           commonactinact(Convert.ToString(queueid), "Queue Series", "Active", updatedby);
       }
       // * Queue Type Disabled * \\
       public void queuetypedisabled(int queueid,string updatedby)
       {
           string cmd = "update Queue_Master set status='Inactive',Updated_by='"+updatedby+"',Updated_Date='" + System.DateTime.Now + "' where Queueid='" + queueid + "'";
           con.SetDataBase(cmd);
           commonactinact(Convert.ToString(queueid), "Queue Series", "Inactive", updatedby);
       }

       // * Enable disabled grid * \\
       public void queueenablegrid(GridView grid, string pharlocation)
       {
           string commt = "Select p.Location_Name,q.queueid,q.Queue_type,q.Queue_From,q.Queue_To,a.DDS_Name,q.Created_by,convert(varchar(20),q.Created_Date,113) as Created_Date,q.Updated_by,convert(varchar(20),q.Updated_Date,113) as Updated_Date,q.Status from Queue_Master as q left join Queue_Allocation as a on a.Queueid=q.QueueID left join Pharmacy as p on p.PharmacyID=q.PharmacyId  where p.Location_Name='" + pharlocation + "' order by q.Updated_Date desc";
           con.View(commt, grid);
       }
      
       // * Queue Allocation Remove * \\
       public void queallocationremove(string pharmname,string queuetype)
       {
           
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Queue_Allot_Remove";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Pharmacyname", SqlDbType.VarChar, 50).Value = pharmname.ToString();
           cmd.Parameters.Add("@Queuetype", SqlDbType.VarChar, 50).Value = queuetype.ToString();          
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
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

       public void Decrypt(string TextToBeDecrypted)
       {
           TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider();
           //RijndaelManaged RijndaelCipher = new RijndaelManaged();
           string Password = "CSC";          
           try
           {
               byte[] EncryptedData = Convert.FromBase64String(TextToBeDecrypted);
               byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());              
               PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);              
              // ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(20), SecretKey.GetBytes(16));
               ICryptoTransform Decryptor = trip.CreateDecryptor(SecretKey.GetBytes(16), SecretKey.GetBytes(16));
               MemoryStream memoryStream = new MemoryStream(EncryptedData);              
               CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);
               byte[] PlainText = new byte[EncryptedData.Length];
               int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
               memoryStream.Close();
               cryptoStream.Close();              
               DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
           }
           catch
           {
               DecryptedData = TextToBeDecrypted;
           }          
       }
       
    }
}
