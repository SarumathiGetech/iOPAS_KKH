using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Configuration;

namespace Datalayer
{
   public class smsclass
    {
       DB_Connection con = new DB_Connection();
       SqlCommand cmd = new SqlCommand();    
       DateTime Datefrom,Dateto;

      
       public void datefun(string dateval)
       {
           string i = "";
           i = dateval;           
           Int32 len = i.Length;
           Int32 n = i.IndexOf('/');
           string str = i.Substring(n + 1, 2);
           string str1 = i.Substring(0, 2);
           string str2 = i.Substring(6, 4);
           string dat = str + "/" + str1 + "/" + str2;
           Datefrom = Convert.ToDateTime(dat);
           //Datesearch = Convert.ToDateTime(str + "/" + Convert.ToString(Convert.ToInt32(str1)) + "/" + str2 + " 23:59:59.999");
       }
       public void datetofun(string dateval)
       {
           string i = "";
           i = dateval;
           Int32 len = i.Length;
           Int32 n = i.IndexOf('/');
           string str = i.Substring(n + 1, 2);
           string str1 = i.Substring(0, 2);
           string str2 = i.Substring(6, 4);
           string dat = str + "/" + str1 + "/" + str2;
           Dateto = Convert.ToDateTime(dat);
           //Datesearch = Convert.ToDateTime(str + "/" + Convert.ToString(Convert.ToInt32(str1)) + "/" + str2 + " 23:59:59.999");
       } 




       // * Common Active / Inactive Audit trail table Insert Procedure * \\
       public void commonactinact(string id, string name, string status, string updateby)
       {
           SqlConnection conn = con.getstring();;
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
       // * SMS Type insert *\\
       public int SMStypeinsert(string type,int notprocessed,int elapsemin,string createdby)
       {
           SqlConnection conn = con.getstring();;
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "SMS_Type_insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@SMStype", SqlDbType.VarChar, 200).Value = type.ToString();
           cmd.Parameters.Add("@Notprocessed", SqlDbType.Int).Value = notprocessed;
           cmd.Parameters.Add("@Elaspedminutes", SqlDbType.Int).Value = elapsemin;
           cmd.Parameters.Add("@Createdby", SqlDbType.VarChar, 20).Value = createdby.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int Rtnval = (int)par.Value;
           return Rtnval;
       }
   
       // * Message Content Update * \\
       public int smstypeupdate(string msgtype,int inquenotproce,int elaspemin, string updatedby)
       {
           SqlConnection conn = con.getstring();;
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "SMS_Type_Update";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@SMStype", SqlDbType.VarChar, 200).Value = msgtype.ToString();
           cmd.Parameters.Add("@Notprocessed", SqlDbType.Int).Value = inquenotproce;
           cmd.Parameters.Add("@Elaspedminutes", SqlDbType.Int).Value = elaspemin;
           cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = updatedby.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int Rtnval = (int)par.Value;
           return Rtnval;
       }

       // * SMS User insert *\\
       public int SMSuserinsert(string contactpreson,string code,string mobilenumber,string description,string createdby,string status)
       {   
           SqlConnection conn = con.getstring();;
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "SMS_User_Insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Contactperson", SqlDbType.VarChar, 200).Value = contactpreson.ToString();
           cmd.Parameters.Add("@Counrtycode", SqlDbType.VarChar, 10).Value = code.ToString();
           cmd.Parameters.Add("@Mobileno", SqlDbType.VarChar, 10).Value = mobilenumber.ToString();
           cmd.Parameters.Add("@Description", SqlDbType.VarChar, 255).Value = description.ToString();
           cmd.Parameters.Add("@Createdby", SqlDbType.VarChar, 20).Value = createdby.ToString();
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
       // * SMS User update * \\
       public int smsuserupdate(int smsuserid,string contactpreson,string mobilenumber, string description, string Updatedby, string status)
       {
           SqlConnection conn = con.getstring();;
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "SMS_User_Update";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = smsuserid;
           cmd.Parameters.Add("@Contactperson", SqlDbType.VarChar, 200).Value = contactpreson.ToString();           
           cmd.Parameters.Add("@Mobileno", SqlDbType.VarChar, 10).Value = mobilenumber.ToString();
           cmd.Parameters.Add("@Description", SqlDbType.VarChar, 255).Value = description.ToString();
           cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = Updatedby.ToString();
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
       // * SMS User Grid * \\
       public void smsusergrid(GridView grid, string contactpers)
       {
           string commt = "select SMS_UserID,Contact_Person,MobileNo ,Created_by,CONVERT(varchar(20), Created_Date,113) as created_date, updated_by,CONVERT(varchar(20),Updated_Date,113)as updated_dates,Status from SMS_User where Contact_Person like  '%" + contactpers + "%' order by Updated_Date desc ";
           con.View(commt, grid);
       }      

       // * SMS ALERT INSERT * \\
       public int SMSalertinsert(string mobileno, string msgtype, int alertmin1, int alertmin2, string status, string createdby)
       {
           SqlConnection conn = con.getstring();;
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "SMS_Alert_Insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Mobileno", SqlDbType.VarChar, 10).Value = mobileno.ToString();
           cmd.Parameters.Add("@Msgtype", SqlDbType.VarChar, 200).Value = msgtype.ToString();
           cmd.Parameters.Add("@Createdby", SqlDbType.VarChar, 20).Value = createdby.ToString();
           cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = status.ToString();
           cmd.Parameters.Add("@Alertmin1", SqlDbType.Int).Value = alertmin1;
           cmd.Parameters.Add("@Alertmin2", SqlDbType.Int).Value = alertmin2;
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int Rtnval = (int)par.Value;
           return Rtnval;
       }

       // * SMS ALERT TIE WITH LOCATION INSERT
       public void SMSalertlocationinsert(string mobileno, string msgtype, string location)
       {
           SqlConnection conn = con.getstring();;
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "SMS_Pharmacy_Location_Insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Mobileno", SqlDbType.VarChar, 10).Value = mobileno.ToString();
           cmd.Parameters.Add("@Msgtype", SqlDbType.VarChar, 200).Value = msgtype.ToString();
           cmd.Parameters.Add("@Pharmacylocation", SqlDbType.VarChar, 50).Value = location.ToString();         
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           //int Rtnval = (int)par.Value;
          // return Rtnval;
       }

       // * SMS ALERT TIE WITH LOCATION INSERT
       public void SMSalertlocationupdate(string mobileno, string msgtype)
       {
           SqlConnection conn = con.getstring();;
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "SMS_Pharmacy_Location_Update";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Mobileno", SqlDbType.VarChar, 10).Value = mobileno.ToString();
           cmd.Parameters.Add("@Msgtype", SqlDbType.VarChar, 200).Value = msgtype.ToString();          
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           //int Rtnval = (int)par.Value;
           // return Rtnval;
       }

       // * SMS Alert Update * \\
       public int SMSalertupdate(int allotid, string type, string Mobno, int alertmin1, int alertmin2, string updatedby)
       {
           SqlConnection conn = con.getstring();;
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "SMS_Alert_Update";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Alertid", SqlDbType.Int).Value = allotid;
           cmd.Parameters.Add("@Mobileno", SqlDbType.VarChar, 10).Value = Mobno.ToString();
           cmd.Parameters.Add("@Msgtype", SqlDbType.VarChar, 200).Value = type.ToString();
           cmd.Parameters.Add("@Alertmin1", SqlDbType.Int).Value = alertmin1;
           cmd.Parameters.Add("@Alertmin2", SqlDbType.Int).Value = alertmin2;
           cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = updatedby.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int Rtnval = (int)par.Value;
           return Rtnval;
       }



       // * SMS User Alert Grid *\\
       public void smsalertgrid(GridView grid)
       {
           string commt = "select u.Contact_Person,u.mobileno,t.Message_Type,a.AlertID,a.Alert_Minutes1,a.Alert_Minutes2,a.status,a.created_by,CONVERT(varchar(20),a.created_Date,113) as Created_date,a.updated_by,CONVERT(varchar(20),a.updated_date,113) as updated_date from SMS_Alert as a left join SMS_Type as t on t.SMS_TypeID=a.SMS_TypeID left join SMS_User as u on u.SMS_UserID=a.SMS_UserID where u.Status='YES' order by a.updated_date desc ";
           con.View(commt, grid);
       }


       // * SMS Active Update Query * \\
       public void smsactivate(int Alertid,string updatedid)
       {
           string cmd = "Update SMS_Alert set Status='Active',updated_by='" + updatedid + "',updated_date='" + System.DateTime.Now + "' where Alertid ='" + Alertid + "'";
           con.SetDataBase(cmd);
           commonactinact(Convert.ToString(Alertid), "SMS Alert", "Active", updatedid);
       }
       // * SMS Inactive Update Query * \\
       public void smsinactivate(int alertid,string updatedid)
       {
           string cmd = "Update SMS_Alert set Status='Inactive' ,updated_by='" + updatedid + "',updated_date='" + System.DateTime.Now + "' where Alertid ='" + alertid + "'";
           con.SetDataBase(cmd);
           commonactinact(Convert.ToString(alertid), "SMS Alert", "Inactive", updatedid);
       }
      

       //* CARTRIDGE MASTER * \\

      
       //* Search One Cartridge info to grid based on particular cartridge number * \\
       public void searchdisplay(GridView grid, string cartnom,string pharmacyloc)
       {
           string commt = "Select c.Cartridge_Id,p.location_Name,c.Cartridge_Type,c.Description,c.Created_by,convert(varchar(20),c.Created_Date,113) as Created_Date ,c.Updated_by,convert(varchar(20),c.Updated_Date,113) as Updated_Date ,c.Status from Cartridge_Master as c left join Pharmacy as p on p.PharmacyID=c.PharmacyID where c.Cartridge_Id ='" + cartnom + "' and p.Location_Name='" + pharmacyloc + "'  order by id desc";
           con.View(commt, grid);
       }

       // * Search Cartridge Number Range * \\

       public void searchrange(GridView grid, string cartnofrom, string cartnoto, string pharmacy)
       {
           string commt = "Select c.Cartridge_Id,p.location_Name,c.Cartridge_Type,c.Description,c.Created_by,convert(varchar(20),c.Created_Date,113) as Created_Date ,c.Updated_by,convert(varchar(20),c.Updated_Date,113) as Updated_Date ,c.Status from Cartridge_Master as c left join Pharmacy as p on p.PharmacyID=c.PharmacyID where  SUBSTRING(c.Cartridge_Id,3,4) between SUBSTRING('" + cartnofrom + "',3,4)and SUBSTRING('" + cartnoto + "',3,4) and p.Location_Name='" + pharmacy + "'  order by id desc ";
           con.View(commt, grid);
       }

       // * Search Cartridge Number only type * \\

       public void searchtype(GridView grid, string pharmacy)
       {
           string commt = "Select c.Cartridge_Id,p.location_Name,c.Cartridge_Type,c.Description,c.Created_by,convert(varchar(20),c.Created_Date,113) as Created_Date ,c.Updated_by,convert(varchar(20),c.Updated_Date,113) as Updated_Date ,c.Status from Cartridge_Master as c left join Pharmacy as p on p.PharmacyID=c.PharmacyID where p.Location_Name='" + pharmacy + "'  order by id desc ";
           con.View(commt, grid);
       }

       // * Display All the cartridge info to grid based on particular pharmacy name * \\
       public void ddldisplay(GridView grid,string pharmid)
       {
           string commt = "Select c.Cartridge_Id,p.location_Name,c.Cartridge_Type,c.Description,c.Created_by,convert(varchar(20),c.Created_Date,113) as Created_Date ,c.Updated_by,convert(varchar(20),c.Updated_Date,113) as Updated_Date ,c.Status from Cartridge_Master as c left join Pharmacy as p on p.PharmacyID=c.PharmacyID  where p.Location_Name='" + pharmid + "' and c.Cartridge_Type<>'BDS' order by c.Updated_Date desc";
           con.View(commt, grid);
       }
       // * All Pharmacy Display *\\
       public void Alldisplay(GridView grid)
       {
           string commt = "Select c.Cartridge_Id,p.location_Name,c.Cartridge_Type,c.Description,c.Created_by,convert(varchar(20),c.Created_Date,113) as Created_Date ,c.Updated_by,convert(varchar(20),c.Updated_Date,113) as Updated_Date ,c.Status from Cartridge_Master as c left join Pharmacy as p on p.PharmacyID=c.PharmacyID  order by c.id desc";
           con.View(commt, grid);
       }
       // * Cartridge Enabled * \\
       public void cartenable(string cartid,string updateuser,DateTime dts)
       {         
           string cmd = "Update Cartridge_Master set status='Active',updated_by='" + updateuser + "',updated_date='" + dts+ "' where Cartridge_Id='" + cartid + "'";         
           con.SetDataBase(cmd);
           commonactinact(cartid, "Cartridge Master", "Active", updateuser);
       }
       // * Cartridge Disabled * \\
       public void cartdisabled(string cartid, string updateuser, DateTime dts)
       {
           string cmd = "Update Cartridge_Master set status='Inactive',updated_by='" + updateuser + "',updated_date='" + dts + "'  where Cartridge_Id='" + cartid + "'";                
           con.SetDataBase(cmd);
           commonactinact(cartid, "Cartridge Master", "Inactive", updateuser);
       }

     

       // * New Cartridge Insert * \\
       public int cartridgeinsert(string pharmacylocation, string cartridgeno, string cartridgetype, string description, string user)
       {
           SqlConnection conn = con.getstring();;
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Cartridge_insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Pharmacylocation", SqlDbType.VarChar, 50).Value = pharmacylocation.ToString();
           cmd.Parameters.Add("@Cartridge_id", SqlDbType.VarChar, 6).Value = cartridgeno.ToString();
           cmd.Parameters.Add("@Cartridge_Type", SqlDbType.VarChar, 20).Value = cartridgetype.ToString();
           cmd.Parameters.Add("@Description", SqlDbType.VarChar, 255).Value = description.ToString();
           cmd.Parameters.Add("@Created_by", SqlDbType.VarChar, 20).Value = user.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int Rtnvalue = (int)par.Value;
           return Rtnvalue;
       }

       // * Cartridge Update * \\
       public int cartridgeupdate(string pharmacylocation, string cartridgeno, string cartridgetype, string description, string user)
       {
           DataTable dTable = new DataTable();
           SqlConnection conn = con.getstring();;
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Cartridge_update";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Pharmacylocation", SqlDbType.VarChar, 50).Value = pharmacylocation.ToString();
           cmd.Parameters.Add("@Cartridge_id", SqlDbType.VarChar, 6).Value = cartridgeno.ToString();
           cmd.Parameters.Add("@Cartridge_Type", SqlDbType.VarChar, 20).Value = cartridgetype.ToString();
           cmd.Parameters.Add("@Description", SqlDbType.VarChar, 255).Value = description.ToString();
           cmd.Parameters.Add("@Updated_by", SqlDbType.VarChar, 20).Value = user.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();           
           cmd.ExecuteNonQuery();
           conn.Close();           
           int Rtnvalue = (int)par.Value;
           return Rtnvalue;
       }
       // ** ------------------------------------------------------------------------------------------------------------------------**\\
       // ** LOADING MASTER * \\       
       // * Loading master insert * \\
       public void masterinsert(int pharmlocation, int lfv, int sv, int drugexp,int disabling,int Cart_Auto_Enable, string createdby, DateTime createddt)
       {
           double Dou = 8.0;
           string cmd = "Insert into Loading_Master(PharmacyID,LF_Verification,Second_Verification,Min_Exp_Loading,Min_Exp_Disabling,Bottle_Pitch,BDS_Cart_Auto_Enable,Created_by,Created_Date,Updated_by,Updated_Date)values('" + pharmlocation + "','" + lfv + "','" + sv + "','" + drugexp + "','" + disabling + "','" + Dou + "','" + Cart_Auto_Enable + "','" + createdby + "','" + createddt + "','" + createdby + "','" + createddt + "')";
           con.SetDataBase(cmd);
       }
       // * Loading Update * \\
       public void masterupdate(string pharmlocation, int lfv, int sv, int drugexp, int disabling,int Cart_Auto_Enabble, string updatedby)
       {          
           SqlConnection conn = con.getstring();;
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "LoadingMaster_Update";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Pharmacylocation", SqlDbType.VarChar, 50).Value = pharmlocation.ToString();
           cmd.Parameters.Add("@Lf_Verification", SqlDbType.Int).Value = lfv;
           cmd.Parameters.Add("@Secondverification", SqlDbType.Int).Value = sv;
           cmd.Parameters.Add("@Minexploading", SqlDbType.Int).Value = drugexp;
           cmd.Parameters.Add("@Minexpdisabling", SqlDbType.Int).Value = disabling;
           cmd.Parameters.Add("@Cart_Auto_Enable", SqlDbType.Int).Value = Cart_Auto_Enabble;
           cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = updatedby.ToString();          
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
          
       }      

       // * Grid Display loading master *\\
       public void displaygrid(GridView grid)
       {
           string commt = "Select P.location_name,l.Created_by,convert(varchar(20),l.Created_Date,113) as Created_Date,l.Updated_by,convert(varchar(20),l.Updated_Date,113) as Updated_Date from Loading_Master as l left join Pharmacy as p on p.PharmacyID=l.PharmacyID order by l.Updated_Date desc ";
           con.View(commt, grid);
       }      
       // -------------------------------------------------------------------------------------------------------------\\
       // ** ASSEMBLY MASTER ** \\
       // * Assembly Master Insert *\\
       public void assemblymasterinsert(int pharmloc, int opone, int optwo, int opthree, string createdby, DateTime createddt)
       {
           string cmd = "Insert into Assembly_Master(PharmacyID,Option_1,Option_2,Option_3,Created_by,Created_Date,Updated_by,Updated_Date)values('" + pharmloc + "','" + opone + "','" + optwo + "','" + opthree + "','" + createdby + "','" + createddt + "','" + createdby + "','" + createddt + "')";
           con.SetDataBase(cmd);
       }
       // * Assembly Master Update * \\
       public void assemblymasterupdate(string pharmloc, int opone, int optwo, int opthree, string updatedby)
       {
           SqlConnection conn = con.getstring();;
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Assembly_Master_Update";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Pharmacylocation", SqlDbType.VarChar, 50).Value = pharmloc.ToString();
           cmd.Parameters.Add("@Optionone", SqlDbType.Int).Value = opone;
           cmd.Parameters.Add("@Optiontwo", SqlDbType.Int).Value = optwo;
           cmd.Parameters.Add("@Optionthree", SqlDbType.Int).Value = opthree;           
           cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = updatedby.ToString();
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
       }

       // * Assembly Master grid Display * \\
       public void assemblymastergrid(GridView grid)
       {
           string commt = "Select p.Location_Name,a.Created_by,convert(varchar(20),a.Created_Date,113) as Created_Date,a.Updated_by,convert(varchar(20),a.Updated_Date,113)as Updated_Date  from Assembly_Master as a  left join Pharmacy as p on p.PharmacyID= a.PharmacyID order by a.Updated_Date desc ";
           con.View(commt, grid);
       }
       

       // ** PACK TYPE MASTER ** \\
       // * Pack Type Insert * \\

       public int packtypeinsert(string packtype, string description, string createdid)
       {
           SqlConnection conn = con.getstring();;
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Packtype_Insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Packtype", SqlDbType.VarChar, 10).Value = packtype.ToString();
           cmd.Parameters.Add("@Description", SqlDbType.VarChar, 255).Value = description.ToString();
           cmd.Parameters.Add("@Createdby", SqlDbType.VarChar, 20).Value = createdid.ToString();         
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int Rtnvalue = (int)par.Value;
           return Rtnvalue;
       }
       // * Pack Type Update * \\
       public int packtypeupdate(string packtype, string descrition, string updatedby, string pcktypid)
       {
           SqlConnection conn = con.getstring();;
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Packtype_Upadte";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Packtype", SqlDbType.VarChar, 10).Value = packtype.ToString();
           cmd.Parameters.Add("@pcktypeid", SqlDbType.VarChar, 10).Value = pcktypid.ToString();
           cmd.Parameters.Add("@Description", SqlDbType.VarChar, 255).Value = descrition.ToString();
           cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = updatedby.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int Rtnvalue = (int)par.Value;
           return Rtnvalue;
       }
       // * Packtype Grid Display * \\
       public void packtypegrid(GridView grid)
       {
           string commt = "Select ID,Packtype,Created_by,convert(varchar(20),Created_Date,113) as Created_Date,Updated_by,convert(varchar(20),Updated_Date,113) as UpdatedDate from Packtype_Master order by Updated_Date desc";
           con.View(commt, grid);
       }

// * ----------------------------------------------------------------------------------------------------------*\\
       // * JUMP QUEUE *\\
       // * JumpQueue Insert Procedure * \\

       public int jumpqueinsert(string queueno, string patientid, string patientname,string userid, string pharmacylocation)
       {
           SqlConnection conn = con.getstring();;
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Jumpqueue_Insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Queueno", SqlDbType.VarChar, 10).Value = queueno.ToString();
           cmd.Parameters.Add("@patientid", SqlDbType.VarChar, 20).Value = patientid.ToString();
           cmd.Parameters.Add("@Patientname", SqlDbType.VarChar, 100).Value = patientname.ToString();
          // cmd.Parameters.Add("@Jqdate", SqlDbType.DateTime).Value = jqdate;
           cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = userid.ToString();
           cmd.Parameters.Add("@pharmacylocation", SqlDbType.VarChar, 50).Value = pharmacylocation.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int Rtnvalue = (int)par.Value;
           return Rtnvalue;
       }

       // * Jumpqueue Grid Display * \\
       public void Jqgrid(GridView grid,string location)
       {
           string commt = "Select j.QueueNo,j.PatientID,j.Patientname,convert(varchar(20),j.JQdatetime,113) as JQDATE from Jumpqueue as j left join Pharmacy as p on p.PharmacyID=j.PharmacyID where p.Location_Name='" + location + "' and convert(varchar,j.JQdatetime,103)=CONVERT(varchar,getdate(),103) order by id desc";
           con.View(commt, grid);
       }

       // * Jumb Queue Queue Number Checking * \\
       public DataSet queuenumcheck(string queueno,string location)
       {
           string cmd = "select COUNT(distinct dp.PatientID) from DDS_Pick as dp left join Pharmacy as p on p.PharmacyID=dp.PharmacyID where Queue_No='" + queueno + "' and CONVERT(varchar,Queue_Date,103)=CONVERT(varchar,getdate(),103) and p.Location_Name='" + location + "' group by PatientID";
           DataSet ds1 = new DataSet();
           ds1 = con.getdataset(cmd);
           return ds1;
       }

       // * Jump Queue popscreen Grid Query for Queue No More Than One Patient *\\
       public void jumppopqueue(GridView grid, string Queueno,string location)
       {
           string commt = "select dp.PatientID,pid.Patient_Name_FN1 as PatientName from DDS_Pick as dp left join HL7_PID as pid on pid.PatientID=dp.PatientID left join Pharmacy as p on p.PharmacyID=dp.PharmacyID where dp.Queue_No='" + Queueno + "' and CONVERT(varchar,dp.Queue_Date,103)=CONVERT(varchar,getdate(),103) and dp.Status='NORMAL' and p.Location_Name='"+location+"' group by dp.PatientID,pid.Patient_Name_FN1";
           con.View(commt, grid);
       }

      

       // * Jump Queue popscreen Grid Query for One PatientID More Than One Queue *\\
       public DataSet Patientidchecking(string Patientid,string location)
       {
           string cmd = "select COUNT(distinct dp.Queue_No) from DDS_Pick as dp left join Pharmacy as p on p.PharmacyID=dp.PharmacyID where PatientID='" + Patientid + "' and CONVERT(varchar,Queue_Date,103)=CONVERT(varchar,getdate(),103) and p.Location_Name='"+location+"' group by Queue_No ";
           DataSet ds2 = new DataSet();
           ds2 = con.getdataset(cmd);
           return ds2;
       }

      


       // * BATCH ORDER *\\
       // * Batch Order Insert Procedure * \\

       public int batchorderinsert(string pharmacylocation, int iumno, int quantityperbag, int noofbags,
           string ddsname, DateTime scheduledate, string scheduletime, string status, string aborted,string PharmacyInstruction, string updatedby)
       {
           SqlConnection conn = con.getstring();;
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Batch_order_Insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@pharmacylocation", SqlDbType.VarChar, 50).Value = pharmacylocation.ToString();        
           cmd.Parameters.Add("@iumcode", SqlDbType.Int).Value = iumno;
           cmd.Parameters.Add("@Quantityperbag", SqlDbType.Int).Value = quantityperbag;
           cmd.Parameters.Add("@Noofbags", SqlDbType.Int).Value = noofbags;
           cmd.Parameters.Add("@DDSName", SqlDbType.VarChar, 20).Value = ddsname.ToString();
           cmd.Parameters.Add("@Scheduledate", SqlDbType.DateTime).Value = scheduledate.ToShortDateString();
           cmd.Parameters.Add("@Scheduletime", SqlDbType.VarChar, 6).Value = scheduletime.ToString();
           cmd.Parameters.Add("@status", SqlDbType.VarChar, 20).Value = status.ToString();
           cmd.Parameters.Add("@Pharmacy_Instruction", SqlDbType.VarChar, 500).Value = PharmacyInstruction.ToString();
           cmd.Parameters.Add("@Aborted_Reason", SqlDbType.VarChar, 20).Value = aborted.ToString();
           cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = updatedby.ToString();           
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int Rtnvalue = (int)par.Value;
           return Rtnvalue;
       }

       // * Batch Order Update Procedure * \\

       public int batchorderupdate(long orderrefno, string pharmacylocation, int quantityperbag, int noofbags,
           string ddsname, DateTime scheduledate, string scheduletime, string PharmacyInstruction, string updatedby)
       {
           SqlConnection conn = con.getstring();;
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Batch_Order_Update";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@orderrefno", SqlDbType.BigInt).Value = orderrefno;
           cmd.Parameters.Add("@pharmacylocation", SqlDbType.VarChar, 50).Value = pharmacylocation.ToString();           
           cmd.Parameters.Add("@Quantityperbag", SqlDbType.Int).Value = quantityperbag;
           cmd.Parameters.Add("@Noofbags", SqlDbType.Int).Value = noofbags;
           cmd.Parameters.Add("@DDSName", SqlDbType.VarChar, 20).Value = ddsname.ToString();
           cmd.Parameters.Add("@Scheduledate", SqlDbType.DateTime).Value = scheduledate.ToShortDateString();
           cmd.Parameters.Add("@Scheduletime", SqlDbType.VarChar, 6).Value = scheduletime.ToString();
           cmd.Parameters.Add("@Pharmacy_Instruction", SqlDbType.VarChar, 500).Value = PharmacyInstruction.ToString();
           cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = updatedby.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int Rtnvalue = (int)par.Value;
           return Rtnvalue;
       }

      // * Grid View Display * \\
       public void batchordergrid(GridView grid, string location)
       {
           string commt = "select p.location_Name,b.OrderRef_No,b.DDSName,b.Quantity_Perbag,b.NoofBags,CONVERT(varchar(17),b.Schedule_Date,113)as Schedule_Date,b.Schedule_Time,b.Updated_by,CONVERT(varchar(20),b.Updateddate,113) as Updateddate,i.item_code,i.item_name,br.Brandname from Batch_Order as b  left join Item_user_Master as iu on iu.ID = b.IUM_ID  left join Item_Master as i on i.MasterID=iu.MasterID left join Brand_Master as br on br.BrandID=iu.Brandid left join Pharmacy as p on p.PharmacyID=b.Pharmacy_Location where b.status='Pending' and p.location_Name='" + location + "' order by b.Updateddate desc";
           con.View(commt, grid);
       }


       // * Batch Ordercancel Update * \\
       public int ordercancel(long orderrefno, string pharmacylocation, string updatedby)
       {
           SqlConnection conn = con.getstring();;
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Batch_Order_Cancel";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@pharmacylocation", SqlDbType.VarChar, 50).Value = pharmacylocation.ToString();
           cmd.Parameters.Add("@orderrefno", SqlDbType.BigInt).Value = orderrefno;
           cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = updatedby.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int Rtnvalue = (int)par.Value;
           return Rtnvalue;
       }

       // * BATCH ORDER ENQUIRY * \\
       // * Enquiry Grid Display Status Filter Query * \\
       public void enquirygrid(GridView grid,string location,string ddsname,string stats,string itemname,string datfrom,string datto)
       {
           if (datfrom == "")
           {
               datefun(System.DateTime.Now.ToString("dd/MM/yyyy"));
               datetofun(System.DateTime.Now.ToString("dd/MM/yyyy"));
           }
           else
           {
               datefun(datfrom);
               datetofun(datto);
           }
           string commt = "select p.location_Name,b.OrderRef_No,b.DDSName,b.Quantity_Perbag,b.NoofBags,CONVERT(varchar(17),b.Schedule_Date,113)as Schedule_Date,b.Schedule_Time,b.Updated_by,CONVERT(varchar(20),b.Updateddate,113) as Updateddate,b.Status,b.Aborted_Reason,CONVERT(varchar(20),b.Status_Datetime,113)as Status_Datetime,i.item_code,i.item_name,pm.PackType,iu.Pack_Size,IL.UOM from Batch_Order as b left join Item_user_Master as iu on iu.ID = b.IUM_ID  left join Item_Master as i on i.MasterID=iu.MasterID  left join Pharmacy as p on p.PharmacyID=b.Pharmacy_Location left join Item_Location as IL on IL.MasterID=i.MasterID and p.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=iu.PacktypeID where b.status='" + stats + "' and i.Item_Name like '%" + itemname + "%' and p.Location_Name='" + location + "' and b.DDSName='" + ddsname + "' and b.Schedule_Date between'" + Datefrom + "' and '" + Dateto + "' order by b.Updateddate desc";
           con.View(commt, grid);
       }
       // * Enquiry Grid Display Status Filter Query single date* \\
       public void enquirygridsingle(GridView grid, string location, string ddsname, string stats, string itemname, string datfrom, string datto)
       {
           datefun(datfrom);
           datetofun(datfrom);
                  
           string commt = "select p.location_Name,b.OrderRef_No,b.DDSName,b.Quantity_Perbag,b.NoofBags,CONVERT(varchar(17),b.Schedule_Date,113)as Schedule_Date,b.Schedule_Time,b.Updated_by,CONVERT(varchar(20),b.Updateddate,113) as Updateddate,b.Status,b.Aborted_Reason,CONVERT(varchar(20),b.Status_Datetime,113)as Status_Datetime,i.item_code,i.item_name,pm.PackType,iu.Pack_Size,IL.UOM from Batch_Order as b left join Item_user_Master as iu on iu.ID = b.IUM_ID  left join Item_Master as i on i.MasterID=iu.MasterID  left join Pharmacy as p on p.PharmacyID=b.Pharmacy_Location left join Item_Location as IL on IL.MasterID=i.MasterID and p.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=iu.PacktypeID where b.status='" + stats + "' and i.Item_Name like '%" + itemname + "%' and p.Location_Name='" + location + "' and b.DDSName='" + ddsname + "' and b.Schedule_Date between'" + Datefrom + "' and '" + Dateto + "' order by b.Updateddate desc";
           con.View(commt, grid);
       }
       // * Batch order Grid Order ref No Exact Search * \\
       public void enquiryorderref(GridView grid, string location,string orderrefno)
       {
           string commt = "select p.location_Name,b.OrderRef_No,b.DDSName,b.Quantity_Perbag,b.NoofBags,CONVERT(varchar(17),b.Schedule_Date,113)as Schedule_Date,b.Schedule_Time,b.Updated_by,CONVERT(varchar(20),b.Updateddate,113) as Updateddate,b.Status,b.Aborted_Reason,CONVERT(varchar(20),b.Status_Datetime,113)as Status_Datetime,i.item_code,i.item_name,pm.PackType,iu.Pack_Size,IL.UOM from Batch_Order as b left join Item_user_Master as iu on iu.ID = b.IUM_ID  left join Item_Master as i on i.MasterID=iu.MasterID left join Pharmacy as p on p.PharmacyID=b.Pharmacy_Location left join Item_Location as IL on IL.MasterID=i.MasterID and p.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=iu.PacktypeID where b.OrderRef_No='" + orderrefno + "' and p.Location_Name='" + location + "' order by b.Updateddate desc";
           con.View(commt, grid);
       }
       // * Batch order Grid Itemcode Exact Search * \\
       public void enquiryitemcode(GridView grid, string location, string ddsname, string stats, string itemcode, string datfrom, string datto)
       {
           if (datfrom == "")
           {
               datefun(System.DateTime.Now.ToString("dd/MM/yyyy"));
               datetofun(System.DateTime.Now.ToString("dd/MM/yyyy"));
           }
           else
           {
               datefun(datfrom);
               datetofun(datto);
           }
           string commt = "select p.location_Name,b.OrderRef_No,b.DDSName,b.Quantity_Perbag,b.NoofBags,CONVERT(varchar(17),b.Schedule_Date,113)as Schedule_Date,b.Schedule_Time,b.Updated_by,CONVERT(varchar(20),b.Updateddate,113) as Updateddate,b.Status,b.Aborted_Reason,CONVERT(varchar(20),b.Status_Datetime,113)as Status_Datetime,i.item_code,i.item_name,pm.PackType,iu.Pack_Size,IL.UOM from Batch_Order as b left join Item_user_Master as iu on iu.ID = b.IUM_ID  left join Item_Master as i on i.MasterID=iu.MasterID left join Pharmacy as p on p.PharmacyID=b.Pharmacy_Location left join Item_Location as IL on IL.MasterID=i.MasterID and p.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=iu.PacktypeID where b.status='" + stats + "' and  i.Item_Code='" + itemcode + "' and p.Location_Name='" + location + "' and b.DDSName='" + ddsname + "' and b.Schedule_Date between'" + Datefrom + "' and '" + Dateto + "' order by b.Updateddate desc";
           con.View(commt, grid);
       }
       // * Batch order Grid Itemcode Exact Search single Date * \\
       public void enquiryitemcodesingle(GridView grid, string location, string ddsname, string stats, string itemcode, string datfrom, string datto)
       {
           datefun(datfrom);
           datetofun(datfrom);
                
           string commt = "select p.location_Name,b.OrderRef_No,b.DDSName,b.Quantity_Perbag,b.NoofBags,CONVERT(varchar(17),b.Schedule_Date,113)as Schedule_Date,b.Schedule_Time,b.Updated_by,CONVERT(varchar(20),b.Updateddate,113) as Updateddate,b.Status,b.Aborted_Reason,CONVERT(varchar(20),b.Status_Datetime,113)as Status_Datetime,i.item_code,i.item_name,pm.PackType,iu.Pack_Size,IL.UOM from Batch_Order as b left join Item_user_Master as iu on iu.ID = b.IUM_ID  left join Item_Master as i on i.MasterID=iu.MasterID left join Pharmacy as p on p.PharmacyID=b.Pharmacy_Location left join Item_Location as IL on IL.MasterID=i.MasterID and p.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=iu.PacktypeID where b.status='" + stats + "' and  i.Item_Code='" + itemcode + "' and p.Location_Name='" + location + "' and b.DDSName='" + ddsname + "' and b.Schedule_Date between'" + Datefrom + "' and '" + Dateto + "' order by b.Updateddate desc";
           con.View(commt, grid);
       }
       // * Batch order Grid Itemcode Exact Search No status * \\
       public void enquiryitemcodenostatus(GridView grid, string location, string ddsname, string itemcode, string datfrom, string datto)
       {
           if (datfrom == "")
           {
               datefun(System.DateTime.Now.ToString("dd/MM/yyyy"));
               datetofun(System.DateTime.Now.ToString("dd/MM/yyyy"));
           }
           else
           {
               datefun(datfrom);
               datetofun(datto);
           }
           string commt = "select p.location_Name,b.OrderRef_No,b.DDSName,b.Quantity_Perbag,b.NoofBags,CONVERT(varchar(17),b.Schedule_Date,113)as Schedule_Date,b.Schedule_Time,b.Updated_by,CONVERT(varchar(20),b.Updateddate,113) as Updateddate,b.Status,b.Aborted_Reason,CONVERT(varchar(20),b.Status_Datetime,113)as Status_Datetime,i.item_code,i.item_name,pm.PackType,iu.Pack_Size,IL.UOM from Batch_Order as b left join Item_user_Master as iu on iu.ID = b.IUM_ID  left join Item_Master as i on i.MasterID=iu.MasterID left join Pharmacy as p on p.PharmacyID=b.Pharmacy_Location left join Item_Location as IL on IL.MasterID=i.MasterID and p.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=iu.PacktypeID where i.Item_Code='" + itemcode + "' and p.Location_Name='" + location + "' and b.DDSName='" + ddsname + "' and b.Schedule_Date between'" + Datefrom + "' and '" + Dateto + "' order by b.Updateddate desc";
           con.View(commt, grid);
       }

       // * Batch order Grid Itemcode Exact Search No status single date * \\
       public void enquiryitemcodenostatussingle(GridView grid, string location, string ddsname, string itemcode, string datfrom, string datto)
       {
           datefun(datfrom);
           datetofun(datfrom);
                 
           string commt = "select p.location_Name,b.OrderRef_No,b.DDSName,b.Quantity_Perbag,b.NoofBags,CONVERT(varchar(17),b.Schedule_Date,113)as Schedule_Date,b.Schedule_Time,b.Updated_by,CONVERT(varchar(20),b.Updateddate,113) as Updateddate,b.Status,b.Aborted_Reason,CONVERT(varchar(20),b.Status_Datetime,113)as Status_Datetime,i.item_code,i.item_name,pm.PackType,iu.Pack_Size,IL.UOM from Batch_Order as b left join Item_user_Master as iu on iu.ID = b.IUM_ID  left join Item_Master as i on i.MasterID=iu.MasterID left join Pharmacy as p on p.PharmacyID=b.Pharmacy_Location left join Item_Location as IL on IL.MasterID=i.MasterID and p.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=iu.PacktypeID where i.Item_Code='" + itemcode + "' and p.Location_Name='" + location + "' and b.DDSName='" + ddsname + "' and b.Schedule_Date between'" + Datefrom + "' and '" + Dateto + "' order by b.Updateddate desc";
           con.View(commt, grid);
       }


       // * Enquery Grid Display All * \\
       public void Enquirygrdall(GridView grid,string location,string ddsname,string itemname,string datfrom, string datto)
       {
           if (datfrom == "")
           {
               datefun(System.DateTime.Now.ToString("dd/MM/yyyy"));
               datetofun(System.DateTime.Now.ToString("dd/MM/yyyy"));
           }
           else
           {
               datefun(datfrom);
               datetofun(datto);
           }
           string commt = "select p.location_Name,b.OrderRef_No,b.DDSName,b.Quantity_Perbag,b.NoofBags,CONVERT(varchar(17),b.Schedule_Date,113)as Schedule_Date,b.Schedule_Time,b.Updated_by,CONVERT(varchar(20),b.Updateddate,113) as Updateddate,b.Status,b.Aborted_Reason,CONVERT(varchar(20),b.Status_Datetime,113)as Status_Datetime,i.item_code,i.item_name,pm.PackType,iu.Pack_Size,IL.UOM from Batch_Order as b  left join Item_user_Master as iu on iu.ID = b.IUM_ID  left join Item_Master as i on i.MasterID=iu.MasterID left join Pharmacy as p on p.PharmacyID=b.Pharmacy_Location left join Item_Location as IL on IL.MasterID=i.MasterID and p.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=iu.PacktypeID where p.Location_Name='" + location + "' and b.DDSName='" + ddsname + "'  and i.Item_Name like '%" + itemname + "%' and b.Schedule_Date between'" + Datefrom + "' and '" + Dateto + "' order by b.Updateddate desc";
           con.View(commt, grid);
       }
       // * Enquery Grid Display All single Date * \\
       public void Enquirygrdallsingle(GridView grid, string location, string ddsname, string itemname, string datfrom, string datto)
       {
           datefun(datfrom);
           datetofun(datfrom);
           
           string commt = "select p.location_Name,b.OrderRef_No,b.DDSName,b.Quantity_Perbag,b.NoofBags,CONVERT(varchar(17),b.Schedule_Date,113)as Schedule_Date,b.Schedule_Time,b.Updated_by,CONVERT(varchar(20),b.Updateddate,113) as Updateddate,b.Status,b.Aborted_Reason,CONVERT(varchar(20),b.Status_Datetime,113)as Status_Datetime,i.item_code,i.item_name,pm.PackType,iu.Pack_Size,IL.UOM from Batch_Order as b  left join Item_user_Master as iu on iu.ID = b.IUM_ID  left join Item_Master as i on i.MasterID=iu.MasterID left join Pharmacy as p on p.PharmacyID=b.Pharmacy_Location left join Item_Location as IL on IL.MasterID=i.MasterID and p.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=iu.PacktypeID where p.Location_Name='" + location + "' and b.DDSName='" + ddsname + "'  and i.Item_Name like '%" + itemname + "%' and b.Schedule_Date between'" + Datefrom + "' and '" + Dateto + "' order by b.Updateddate desc";
           con.View(commt, grid);
       }

       // * PRINT CARTRIDGE DRUG LABEL * \\   
 
       // *User Transaction Insert * \\
       public void PrescriptionDLtrans(string printedby, string location,string activity)
       {
           SqlConnection conn = con.getstring();;
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Prescription_Drug_Label";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Pharmacylocation", SqlDbType.VarChar, 50).Value = location.ToString();
           cmd.Parameters.Add("@Printedby", SqlDbType.VarChar, 20).Value = printedby.ToString();
           cmd.Parameters.Add("@Tran_Activity", SqlDbType.VarChar, 100).Value = activity.ToString(); 
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();          
       }

    }
}
