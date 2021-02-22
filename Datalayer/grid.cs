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
   public class grid
    {
        DB_Connection con = new DB_Connection();
        SqlCommand cmd = new SqlCommand();
        DateTime dt = System.DateTime.Now;

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

        public void useregridall(GridView grid)
        {
            string Commt = "select p.Location_Name,u.userid,u.username,u.empid,d.DomainNme as DomainName,u.created_by,convert(varchar(20),u.Created_Date,113) as Created_Date, u.Updated_by,convert(varchar(20),u.Updated_Date,113) as Updated_Date,u.Status,CONVERT(varchar,u.Effective_from,103) as Effectivefrom,CONVERT(varchar,u.Effective_to,103) as Effectiveto from User_tbl as u left join Pharmacy as p on p.PharmacyID=u.Pharmacy_Id  left join Domain as d on d.DomainID=u.Domain order by u.Updated_Date desc";
            con.View(Commt, grid);
        }

        public void usereditname(GridView grid, string name)
        {
            string Commt = "select p.Location_Name,u.userid,u.username,u.empid,d.DomainNme as DomainName,u.created_by,convert(varchar(20),u.Created_Date,113) as Created_Date, u.Updated_by,convert(varchar(20),u.Updated_Date,113) as Updated_Date,u.Status,CONVERT(varchar,u.Effective_from,103) as Effectivefrom,CONVERT(varchar,u.Effective_to,103) as Effectiveto from User_tbl as u left join Pharmacy as p on p.PharmacyID=u.Pharmacy_Id  left join Domain as d on d.DomainID=u.Domain  where u.username like '%" + name + "%' order by u.Updated_Date desc";
            con.View(Commt, grid);
        }
        public void useredituserid(GridView grid, string userid)
        {
            string Commt = "select p.Location_Name,u.userid,u.username,u.empid,d.DomainNme as DomainName,u.created_by,convert(varchar(20),u.Created_Date,113) as Created_Date, u.Updated_by,convert(varchar(20),u.Updated_Date,113) as Updated_Date,u.Status,CONVERT(varchar,u.Effective_from,103) as Effectivefrom,CONVERT(varchar,u.Effective_to,103) as Effectiveto from User_tbl as u left join Pharmacy as p on p.PharmacyID=u.Pharmacy_Id  left join Domain as d on d.DomainID=u.Domain  where u.userid='" + userid + "' ";
            con.View(Commt, grid);
        }
        public void usereditempno(GridView grid, string empno)
        {
            string Commt = "select p.Location_Name,u.userid,u.username,u.empid,d.DomainNme as DomainName,u.created_by,convert(varchar(20),u.Created_Date,113) as Created_Date, u.Updated_by,convert(varchar(20),u.Updated_Date,113) as Updated_Date,u.Status,CONVERT(varchar,u.Effective_from,103) as Effectivefrom,CONVERT(varchar,u.Effective_to,103) as Effectiveto from User_tbl as u left join Pharmacy as p on p.PharmacyID=u.Pharmacy_Id  left join Domain as d on d.DomainID=u.Domain  where u.empid='" + empno + "' ";
            con.View(Commt, grid);
        }
        public void userdisabaled(string userid,string updateuser)
        {
            string disabled = "update User_tbl set status='Inactive',Updated_by='" + updateuser + "',updated_Date='" + dt + "' where userid ='" + userid + "'";
            con.SetDataBase(disabled);
        }
        public void userenabled(string userid,string updateuser)
        {
            string enabled = "update User_tbl set status='Active',Updated_by='" + updateuser + "',updated_Date='" + dt + "' where userid ='" + userid + "'";
            con.SetDataBase(enabled);
        }
        // ------------------------------------------------------------------- \\
        // * Domain * \\

        public void domaingrid(GridView grid)
        {
            string commt = "select domainid,DomainNme,Created_by,convert(varchar(20),Created_Date,113) as Created_Date ,Updated_by,convert(varchar(20),Updated_Date,113) as UpdatedDate,Status from Domain order by Updated_Date desc";
            con.View(commt, grid);
        }
        public void domainenabled(int domainid,string updateduser)
        {
            string enabled = "update [Domain] set status='Active',Updated_by='" + updateduser + "',updated_date='"+dt+"' where DomainID='" + domainid + "'";
            con.SetDataBase(enabled);
            commonactinact(Convert.ToString(domainid), "Domain Name", "Active", updateduser);
        }
        public void domaindisabled(int domainid, string updateduser)
        {
            string disabled = "update [Domain] set status='Inactive', Updated_by='" + updateduser + "',updated_date='" + dt + "' where DomainID ='" + domainid + "'";
            con.SetDataBase(disabled);
            commonactinact(Convert.ToString(domainid), "Domain Name", "Inactive", updateduser);
        }


       
        // ------------------------------------------------------------------- \\

        // * Pharmacy location *\\
        public void locationdetails(GridView grid)
        {
            string commt = "Select PharmacyID,Location_code,Location_Name,Phar_Abbrivation,Created_by,convert(varchar(20),Created_Date,113) as Created_Date,Updated_by,convert(varchar(20),Updated_Date,113) as Updated_Dates,Status from Pharmacy order by Updated_Date desc";
            con.View(commt, grid);
        }

        // * Pharmacy Location Active * \\
        public void locationenabled(int Apharmid)
        {
            string cmd = "Update pharmacy set Status='Active', updated_date='" + dt + "' where Pharmacyid='" + Apharmid + "'";
            con.SetDataBase(cmd);
        }
        // * Pharmacy Location Inactive * \\
        public void locationdisabled(int Ipharmidi)
        {
            string cmd = "Update pharmacy set Status='Inactive', updated_date='" + dt + "' where Pharmacyid='" + Ipharmidi + "'";
            con.SetDataBase(cmd);
        }

        // ------------------------------------------------------------------- \\

        // Security \\
        public void Securitygrid(GridView grid)
        {
            string commt = "Select Session_Time,NoOfRetries,Password_Reuse,Password_ExpiryDays,Password_Promtdays,Inactive_User,Created_by,convert(varchar(20),Created_Date,113) as Created_Date,Updated_by,convert(varchar(20),Updated_Date,113) as Updated_Date from Security";
            con.View(commt, grid);
        }

        // ------------------------------------------------------------------- \\

        // * Role * \\

        public void roleaccess(GridView grid, string menuname)
        {
            string commt = "select MenuID,Child_Menu from MenuItems where Parent_Menu='" + menuname + "' and Child_Menu<>'Drug pack Type' order by Sno";
            con.View(commt, grid);
        }

       // * Role Name Active Query * \\
        public void roleactive(string roleid,string updatedid)
        {
            string active = "update [role] set status='Active',Updated_by='" + updatedid + "',Updated_Date='"+dt+"' where RoleId='" + roleid + "'";
            con.SetDataBase(active);
            commonactinact(roleid, "Role", "Active", updatedid);
        }
       // * Role Name Inactive Query * \\
        public void roledeactive(string roleid,string updatedid)
        {
            string deactive = "update [role] set status='Inactive',Updated_by='" + updatedid + "',Updated_Date='" + dt + "' where RoleId='" + roleid + "'";
            con.SetDataBase(deactive);
            commonactinact(roleid, "Role", "Inactive", updatedid);
        }

        // * Menuaccess Control Delete two *
        public void menuaccessdeletetwo(int menuid, int roleid,string menuname)
        {
            //string cmd = "delete from RoleControl where MenuId='" + menuid + "' and RoleId='" + roleid + "'";
            //con.SetDataBase(cmd);
            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "RoleControl_Delete";
            cmd.CommandType = CommandType.StoredProcedure;         
            cmd.Parameters.Add("@Menuid", SqlDbType.Int).Value = menuid;
            cmd.Parameters.Add("@Roleid", SqlDbType.Int).Value = roleid;
            cmd.Parameters.Add("@Menuname", SqlDbType.VarChar, 50).Value = menuname.ToString();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }   

        // * RoleControl Inser Procedure* \\
        public void rolecontrolinsert(string roleid, int menuid, string userid)
        {
            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "Rolecontrol_insert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Rolename", SqlDbType.VarChar, 20).Value = roleid.ToString();
            cmd.Parameters.Add("@Menuid", SqlDbType.Int).Value = menuid;
            cmd.Parameters.Add("@Createdby", SqlDbType.VarChar, 20).Value = userid.ToString();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }       
       
        // * Display the first grid * \\
        public void displaygridfirst(GridView grid)
        {
            string commt = "Select Roleid,Role_Name,Created_by,convert(varchar(20),Created_Date,113) as Created_Date,Updated_by,Created_by,convert(varchar(20),Updated_Date,113) as UpdatedDate,Status from [Role] order by Updated_Date desc";
            con.View(commt, grid);
        }
        // * Display the first Page grid * \\
        public void displaygridfirstpage(GridView grid)
        {
            string commt = "Select Role_Name,Created_by,convert(varchar(20),Created_Date,113) as Created_Date,Updated_by,Created_by,convert(varchar(20),Updated_Date,113) as Updated_Date,Status from [Role] order by roleid desc";
            con.View(commt, grid);
        }    

        // * REJECTED REASON * \\
        // Rejected Reason insert \\
        public int rejectedinsert(string Reasontype,string Reason, string details, string created)
        {
            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "Reason_Insert";            
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Reasontype", SqlDbType.VarChar, 25).Value = Reasontype.ToString();
            cmd.Parameters.Add("@Reason", SqlDbType.VarChar, 20).Value = Reason.ToString();
            cmd.Parameters.Add("@Details", SqlDbType.VarChar, 255).Value = details.ToString();
            cmd.Parameters.Add("@Createdby", SqlDbType.VarChar, 20).Value = created.ToString();            
            SqlParameter par = new SqlParameter();
            par.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(par);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            int rtnval = (int)par.Value;
            return rtnval;
        }

        // * Reason Display Grid * \\
        public void reasongrid(GridView grid)
        {
            string commt = "Select ID,Reason,Reason_type,Created_by,convert(varchar(20),Created_Date,113) as Created_Date,Updated_by,convert(varchar(20),Updated_date,113)as Updated_dat ,Status from Rejected_reason order by Updated_date desc";
            con.View(commt, grid);
        }

        // * Rejected Reason Enabled * \\
        public void reasonenable(int reasonid,string updatedby)
        {
            string cmd = "update Rejected_reason set status='Active',Updated_by='" + updatedby + "',Updated_Date='" + System.DateTime.Now + "' where ID='" + reasonid + "'";
            con.SetDataBase(cmd);
            commonactinact(Convert.ToString(reasonid), "Reason Master", "Active", updatedby);
        }
        // * Rejected Reason Disabled * \\
        public void reasonDisabled(int reasonid, string updatedby)
        {
            string cmd = "update Rejected_reason set status='Inactive',Updated_by='"+updatedby+"',Updated_Date='"+System.DateTime.Now+"' where ID='" + reasonid + "'";
            con.SetDataBase(cmd);
            commonactinact(Convert.ToString(reasonid), "Reason Master", "Inactive", updatedby);
        }

        // * Rejected Reason Update * \\
        public int reasonupdate(int reasonid,string Reasontype,string Reason, string details, string updated)
        {
            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "Reason_Update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Reasonid", SqlDbType.Int).Value = reasonid;
            cmd.Parameters.Add("@Reasontype", SqlDbType.VarChar, 25).Value = Reasontype.ToString();
            cmd.Parameters.Add("@Reason", SqlDbType.VarChar, 20).Value = Reason.ToString();
            cmd.Parameters.Add("@Details", SqlDbType.VarChar, 255).Value = details.ToString();
            cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = updated.ToString();
            SqlParameter par = new SqlParameter();
            par.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(par);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            int rtnval = (int)par.Value;
            return rtnval;
        }
      

        // * PRINTER MASTER * \\


        // * Printer insert procedure * \\
        public int printerinsert(string pharmacyid, string printername, string ipaddress, string description, string createdby)
        {
            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "printer_insert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pharmacyid", SqlDbType.VarChar, 50).Value = pharmacyid.ToString();
            cmd.Parameters.Add("@Printername", SqlDbType.VarChar, 50).Value = printername.ToString();
            cmd.Parameters.Add("@Ipaddress", SqlDbType.VarChar, 30).Value = ipaddress.ToString();
            cmd.Parameters.Add("@Description", SqlDbType.VarChar, 255).Value = description.ToString();
            cmd.Parameters.Add("@Createdby", SqlDbType.VarChar, 20).Value = createdby.ToString();
            SqlParameter par = new SqlParameter();
            par.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(par);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            int rtnval = (int)par.Value;
            return rtnval;
        }

        // * Printer Update procedure * \\
        public int printerupdate(int Printid,string pharmacyid, string printername, string ipaddress, string description, string createdby)
        {
            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "printer_update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PrinterID", SqlDbType.Int).Value = Printid;
            cmd.Parameters.Add("@Pharmacyid", SqlDbType.VarChar, 50).Value = pharmacyid.ToString();
            cmd.Parameters.Add("@Printername", SqlDbType.VarChar, 50).Value = printername.ToString();
            cmd.Parameters.Add("@Ipaddress", SqlDbType.VarChar, 30).Value = ipaddress.ToString();
            cmd.Parameters.Add("@Description", SqlDbType.VarChar, 255).Value = description.ToString();
            cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = createdby.ToString();
            SqlParameter par = new SqlParameter();
            par.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(par);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            int rtnval = (int)par.Value;
            return rtnval;
        }

       // * Printer Function Insert * \\
        public void functioninsert(string printername,string location, string label, string function, string defaul)
        {
            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "Printer_Function_Insert";
            cmd.CommandType = CommandType.StoredProcedure;           
            cmd.Parameters.Add("@Printername", SqlDbType.VarChar, 50).Value = printername.ToString();
            cmd.Parameters.Add("@Pharmacylocation", SqlDbType.VarChar, 50).Value = location.ToString();
            cmd.Parameters.Add("@LabelName", SqlDbType.VarChar, 10).Value = label.ToString();
            cmd.Parameters.Add("@Function", SqlDbType.VarChar, 10).Value = function.ToString();
            cmd.Parameters.Add("@default", SqlDbType.VarChar, 10).Value = defaul.ToString();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        // * Printer Function Delete(For During Update Process)  * \\
        public void functionupdate(string printername,string location)
        {
            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "Printer_Function_Update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Printername", SqlDbType.VarChar, 50).Value = printername.ToString();
            cmd.Parameters.Add("@Pharmacylocation", SqlDbType.VarChar, 50).Value = location.ToString();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // * Printer Grid display * \\
        public void printergrid(GridView grid, string pharmsid)
        {
            string commt = "Select p.Printerid,p.Printer_Name,pp.location_name,p.Ip_Hostname,f.Default_Printer,f.function_printer,p.Created_by,convert(varchar(20),p.Created_Date,113) as Created_Date,p.Updated_by,convert(varchar(20),p.Updated_Date,113) as Updated_Date,p.Status from Printer as p left join Printer_Function as f on f.PrinterID=p.PrinterID left join Pharmacy as pp on pp.PharmacyID=p.Pharmacyid where pp.Location_Name='" + pharmsid + "' order by p.Updated_Date desc ";
            con.View(commt, grid);
        }

        
        // * Printer Name Enabled * \\
        public void printerenabled(int printid, string updatedby)
        {
            string cmd = "update Printer set status ='Active',Updated_by='"+updatedby+"',updated_Date='"+System.DateTime.Now+"' where printerID='" + printid + "'";
            con.SetDataBase(cmd);
            commonactinact(Convert.ToString(printid), "Printer Master", "Active", updatedby);
        }

        // * Printer Name Disabled * \\
        public void printerdisabled(int printid,string updatedby)
        {
            string cmd = "update Printer set status ='Inactive',Updated_by='" + updatedby + "',updated_Date='" + System.DateTime.Now + "' where printerID='" + printid + "'";
            con.SetDataBase(cmd);
            commonactinact(Convert.ToString(printid), "Printer Master", "Inactive", updatedby);
        }


       



       // * PHARMACY TIME MASTER * \\
       // * Pharmacy Time Insert Procedure * \\

        public int pharmacytimeinsert(string pharmacylocation,string mst, string met, string tst, string tet, string wst, string wet, string thst, string thet, string fst, string fet, string sst, string set, string sust, string suet,string createdby)
        {
            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "Pharmacytime_insert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@pharmacylocation", SqlDbType.VarChar, 50).Value = pharmacylocation.ToString();
            cmd.Parameters.Add("@Createdby", SqlDbType.VarChar, 20).Value = createdby.ToString();
            cmd.Parameters.Add("@Mst", SqlDbType.VarChar, 6).Value = mst.ToString();
            cmd.Parameters.Add("@Met", SqlDbType.VarChar, 6).Value = met.ToString();
            cmd.Parameters.Add("@tst", SqlDbType.VarChar, 6).Value = tst.ToString();
            cmd.Parameters.Add("@Tet", SqlDbType.VarChar, 6).Value = tet.ToString();
            cmd.Parameters.Add("@Wst", SqlDbType.VarChar, 6).Value = wst.ToString();
            cmd.Parameters.Add("@Wet", SqlDbType.VarChar, 6).Value = wet.ToString();
            cmd.Parameters.Add("@Thst", SqlDbType.VarChar, 6).Value = thst.ToString();
            cmd.Parameters.Add("@Thet", SqlDbType.VarChar, 6).Value = thet.ToString();
            cmd.Parameters.Add("@Fst", SqlDbType.VarChar, 6).Value = fst.ToString();
            cmd.Parameters.Add("@Fet", SqlDbType.VarChar, 6).Value = fet.ToString();
            cmd.Parameters.Add("@Sst", SqlDbType.VarChar, 6).Value = sst.ToString();
            cmd.Parameters.Add("@Set", SqlDbType.VarChar, 6).Value = set.ToString();
            cmd.Parameters.Add("@Sust", SqlDbType.VarChar, 6).Value = sust.ToString();
            cmd.Parameters.Add("@Suet", SqlDbType.VarChar, 6).Value = suet.ToString();
            SqlParameter par = new SqlParameter();
            par.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(par);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            int Rtnval = (int)par.Value;
            return Rtnval;
        }

      // * Pharmacy Time Update Procedure * \\
        public int pharmacytimeupdate(string pharmacylocation, string mst, string met, string tst, string tet, string wst, string wet, string thst, string thet, string fst, string fet, string sst, string set, string sust, string suet, string updatedby)
        {
            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "Pharmacytime_update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@pharmacylocation", SqlDbType.VarChar, 50).Value = pharmacylocation.ToString();
            cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = updatedby.ToString();
            cmd.Parameters.Add("@Mst", SqlDbType.VarChar, 6).Value = mst.ToString();
            cmd.Parameters.Add("@Met", SqlDbType.VarChar, 6).Value = met.ToString();
            cmd.Parameters.Add("@tst", SqlDbType.VarChar, 6).Value = tst.ToString();
            cmd.Parameters.Add("@Tet", SqlDbType.VarChar, 6).Value = tet.ToString();
            cmd.Parameters.Add("@Wst", SqlDbType.VarChar, 6).Value = wst.ToString();
            cmd.Parameters.Add("@Wet", SqlDbType.VarChar, 6).Value = wet.ToString();
            cmd.Parameters.Add("@Thst", SqlDbType.VarChar, 6).Value = thst.ToString();
            cmd.Parameters.Add("@Thet", SqlDbType.VarChar, 6).Value = thet.ToString();
            cmd.Parameters.Add("@Fst", SqlDbType.VarChar, 6).Value = fst.ToString();
            cmd.Parameters.Add("@Fet", SqlDbType.VarChar, 6).Value = fet.ToString();
            cmd.Parameters.Add("@Sst", SqlDbType.VarChar, 6).Value = sst.ToString();
            cmd.Parameters.Add("@Set", SqlDbType.VarChar, 6).Value = set.ToString();
            cmd.Parameters.Add("@Sust", SqlDbType.VarChar, 6).Value = sust.ToString();
            cmd.Parameters.Add("@Suet", SqlDbType.VarChar, 6).Value = suet.ToString();
            SqlParameter par = new SqlParameter();
            par.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(par);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            int Rtnval = (int)par.Value;
            return Rtnval;
        }

      // * Holidays Insert * \\
        public void holidaysinsert(string location,DateTime holidays,string Createdby)
        {
            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "Holidays_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Location", SqlDbType.VarChar, 50).Value = location.ToString();
            cmd.Parameters.Add("@Holidays", SqlDbType.Date).Value = holidays;
            cmd.Parameters.Add("@Createdby", SqlDbType.VarChar, 20).Value = Createdby.ToString();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();          
        }

       // * Holidays Remove * \\
       public void holidaysremove(string phlocation,DateTime holiremove,string updatedby)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Holidays_Update";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Location", SqlDbType.VarChar, 50).Value = phlocation.ToString();
           cmd.Parameters.Add("@Holidays", SqlDbType.Date).Value = holiremove;
           cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = updatedby.ToString();
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close(); 
       }


       // * Pharmacy Time Grid Display * \\
       public void pharmacygrid(GridView grid)
       {
           string commt = "Select p.location_Name, pt.created_by,convert(varchar(20),pt.Created_Date,113) as Created_date,pt.Updated_by,convert(varchar(20),pt.updated_Date,113) as Updated_Dates from Pharmacy_Time as pt left join Pharmacy as p on p.PharmacyID=pt.PharmacyID order by pt.updated_Date desc ";
           con.View(commt, grid);
       }

    }
}
