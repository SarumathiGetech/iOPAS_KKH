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
   public class Preload
    {
       DB_Connection con = new DB_Connection();
       SqlCommand cmd = new SqlCommand();
       DateTime Dateresult,Datesearch;
       

       // * CARTRIDGE PRE LOADING * \\
       // *--------------------------------*\\
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
           Dateresult = Convert.ToDateTime(dat);
           Datesearch = Convert.ToDateTime(str + "/" + Convert.ToString(Convert.ToInt32(str1)) + "/" + str2 + " 23:59:59.999");
       }

       // * Cartridge Preloading Insert * \\
       public int preloadinginsert(int id,string cartridgeid,string itemcode,string brand,
       string batchno, int quantity, string expdate, string loadedby,string location)
       {
           datefun(expdate);
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Preloading_insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@IUM_ID", SqlDbType.Int).Value = id;
           cmd.Parameters.Add("@Cartridgeid", SqlDbType.VarChar, 6).Value = cartridgeid.ToString();
           cmd.Parameters.Add("@Itemcode", SqlDbType.VarChar, 20).Value = itemcode.ToString();
           cmd.Parameters.Add("@Brand", SqlDbType.VarChar, 100).Value = brand.ToString();
           cmd.Parameters.Add("@Batchno", SqlDbType.VarChar, 20).Value = batchno.ToString();
           cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
           cmd.Parameters.Add("@Expirydate", SqlDbType.DateTime).Value = Dateresult.Date;
           cmd.Parameters.Add("@Loadedby", SqlDbType.VarChar, 20).Value = loadedby.ToString();
           cmd.Parameters.Add("@pharmacylocation", SqlDbType.VarChar, 50).Value = location.ToString();
           SqlParameter ret = new SqlParameter();
           ret.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(ret);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int retvalue = (int)ret.Value;
           return retvalue;
       }

       // * PRE Loading Validation Procedure * \\
       public int Preloadvalidate(string cartid, string location,string manuname)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Preloading_Validate";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Cartridgeid", SqlDbType.VarChar, 6).Value = cartid.ToString();
           cmd.Parameters.Add("@Pharmacylocation ", SqlDbType.VarChar, 50).Value = location.ToString();
           cmd.Parameters.Add("@Menuname ", SqlDbType.VarChar, 20).Value = manuname.ToString();
           SqlParameter ret = new SqlParameter();
           ret.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(ret);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int retvalue = (int)ret.Value;
           return retvalue;
       }

       // * Cartridge Preloading Rejected Cartridge Update * \\
       public int preloadingupdate(Int64 loadingid, string cartridgeid, string itemcode, int quantity, string expdate, string loadedby, string location, string batchno)
       {
           datefun(expdate);
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Preloading_update";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@loadingid ", SqlDbType.BigInt).Value = loadingid;
           cmd.Parameters.Add("@Cartridgeid", SqlDbType.VarChar, 6).Value = cartridgeid.ToString();           
           cmd.Parameters.Add("@Itemcode", SqlDbType.VarChar, 20).Value = itemcode.ToString();               
           cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
           cmd.Parameters.Add("@Expirydate", SqlDbType.DateTime).Value = Dateresult.Date;
           cmd.Parameters.Add("@Loadedby", SqlDbType.VarChar, 20).Value = loadedby.ToString();
           cmd.Parameters.Add("@pharmacylocation", SqlDbType.VarChar, 50).Value = location.ToString();
           cmd.Parameters.Add("@Batchno", SqlDbType.VarChar, 20).Value = batchno.ToString();
           SqlParameter ret = new SqlParameter();
           ret.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(ret);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int retvalue = (int)ret.Value;
           return retvalue;
       }      
      
       // * Loaded Cartridge DIsplay * \\
       public void preloadgrid(GridView grid, string userid,string location)
       {
           string Bot = "Bottle";
           // 29-11-2013// Select top 50 p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,r.rejected_by,r.rejected_Date,r.reason from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Verification_Rejected as r on p.loading_id=r.loading_id left join Cartridge_Master as c on c.Cartridge_Id =p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID=c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2' and l.Loaded_by='" + userid + "' and ph.Location_Name='" + location + "' and convert(varchar,L.Loaded_Date,103)= convert(varchar,Getdate(),103) order by p.Loading_Id desc";
           string commt = "Select top 100 p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,r.rejected_by,r.rejected_Date,r.reason from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Verification_Rejected as r on p.loading_id=r.loading_id left join Cartridge_Master as c on c.Cartridge_Id =p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID=c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where l.Loaded_by='" + userid + "' and ph.Location_Name='" + location + "' and pm.Packtype not like '%" + Bot + "%' order by p.Loading_Id desc";
           con.View(commt, grid);
       }
       



       


       // * FIRST VERIFICATION * \\



       // * First Verification Display Grid * \\
       public void fstverificationgrid(GridView grid,string userid,string location)
       {
           //29-11-2013  //  string commt = Select top(50) p.Cartridge_Id,i.Item_Code,i.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,r.reason,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.Verified_by,convert(varchar(20),f.Verified_Date,113) as Fvdate from Cartridge_Loading as p left join Loaded_by as l on p.loading_id=l.Loading_Id left join Verification_Rejected as r on p.loading_id=r.loading_id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.brandid left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Cartridge_Master as c on c.Cartridge_Id =p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID=c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where  p.Inventory_Status='2' and f.Verified_by='" + userid + "' and ph.Location_Name='" + location + "' and convert(varchar,f.Verified_Date,103)= convert(varchar,Getdate(),103) order by f.Verified_Date desc";
           string commt = "Select top(100) p.Cartridge_Id,i.Item_Code,i.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,r.reason,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.Verified_by,convert(varchar(20),f.Verified_Date,113) as Fvdate from Cartridge_Loading as p left join Loaded_by as l on p.loading_id=l.Loading_Id left join Verification_Rejected as r on p.loading_id=r.loading_id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.brandid left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Cartridge_Master as c on c.Cartridge_Id =p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID=c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where f.Verified_by='" + userid + "' and ph.Location_Name='" + location + "'  order by f.Verified_Date desc";
           con.View(commt, grid);
       }

       //* First Verification Accept * \\
       public int fstaccept(string cartno, Int64 loadingid, string Itemcode, int quantity, string expdate, string location, string verifiedby)
       {
           datefun(expdate);
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "First_accept";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@cartridgeno", SqlDbType.VarChar, 6).Value = cartno.ToString();
           cmd.Parameters.Add("@loadingid", SqlDbType.BigInt).Value = loadingid;
           cmd.Parameters.Add("@verifiedby", SqlDbType.VarChar, 20).Value = verifiedby.ToString();
           cmd.Parameters.Add("@Itemcode", SqlDbType.VarChar, 20).Value = Itemcode.ToString();
           cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
           cmd.Parameters.Add("@Expirydate", SqlDbType.DateTime).Value = Dateresult.Date;          
           cmd.Parameters.Add("@pharmacylocation", SqlDbType.VarChar, 50).Value = location.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int Retval = (int)par.Value;
           return Retval;           
       }

       // * First Verification Rejected * \\
       public int fstreject(string cartno, Int64 loadingid, string verifiedby, string reasoncode, string location)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "First_reject";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@cartridgeno", SqlDbType.VarChar, 6).Value = cartno.ToString();
           cmd.Parameters.Add("@loadingid", SqlDbType.BigInt).Value = loadingid;
           cmd.Parameters.Add("@Rejectedby", SqlDbType.VarChar, 20).Value = verifiedby.ToString();
           cmd.Parameters.Add("@Reason", SqlDbType.VarChar, 20).Value = reasoncode.ToString();
           cmd.Parameters.Add("@pharmacylocation", SqlDbType.VarChar, 50).Value = location.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int Retval = (int)par.Value;
           return Retval;
       }

       //* SECOND VERIFICATION * \\


       //* Second Verification Grid Display * \\
       public void secondverficationgrid(GridView grid,string userid,string location)
       {
           //29-11-2013// string commt = "Select top(50) p.Cartridge_Id,i.Item_Code,i.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,r.reason,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.Verified_by as fVerified_by ,convert(varchar(20),f.Verified_Date,113) as Fvdate,s.Verified_by as Sverifiedby,convert(varchar(20),s.Verified_Date,113) as Svdate from Cartridge_Loading as p left join Loaded_by as l on p.loading_id=l.Loading_Id left join Verification_Rejected as r on p.loading_id=r.loading_id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.brandid left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Cartridge_Master as c on c.Cartridge_Id =p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID=c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2' and s.Verified_by='" + userid + "' and ph.Location_Name='" + location + "' and convert(varchar,s.Verified_Date,103)=convert(varchar,getdate(),103) order by s.Verified_Date desc ";
           string commt = "Select top(100) p.Cartridge_Id,i.Item_Code,i.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,r.reason,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.Verified_by as fVerified_by ,convert(varchar(20),f.Verified_Date,113) as Fvdate,s.Verified_by as Sverifiedby,convert(varchar(20),s.Verified_Date,113) as Svdate from Cartridge_Loading as p left join Loaded_by as l on p.loading_id=l.Loading_Id left join Verification_Rejected as r on p.loading_id=r.loading_id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.brandid left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Cartridge_Master as c on c.Cartridge_Id =p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID=c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where s.Verified_by='" + userid + "' and ph.Location_Name='" + location + "'  order by s.Verified_Date desc ";
           con.View(commt, grid);
       }

       //* Second Verification Accept * \\
       public int secondaccept(string cartno, Int64 loadingid, string Itemcode, int quantity, string expdate, string location, string verifiedby)
       {
           datefun(expdate);
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Second_accept";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@cartridgeno", SqlDbType.VarChar, 6).Value = cartno.ToString();
           cmd.Parameters.Add("@loadingid", SqlDbType.BigInt).Value = loadingid;
           cmd.Parameters.Add("@verifiedby", SqlDbType.VarChar, 20).Value = verifiedby.ToString();
           cmd.Parameters.Add("@Itemcode", SqlDbType.VarChar, 20).Value = Itemcode.ToString();
           cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
           cmd.Parameters.Add("@Expirydate", SqlDbType.DateTime).Value = Dateresult.Date;
           cmd.Parameters.Add("@pharmacylocation", SqlDbType.VarChar, 50).Value = location.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int Retval = (int)par.Value;
           return Retval;
       }

       // * Second Verification Rejected * \\
       public int secondreject(string cartno, Int64 loadingid, string verifiedby, string reasoncode, string location)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Second_reject";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@cartridgeno", SqlDbType.VarChar, 6).Value = cartno.ToString();
           cmd.Parameters.Add("@loadingid", SqlDbType.BigInt).Value = loadingid;
           cmd.Parameters.Add("@Rejectedby", SqlDbType.VarChar, 20).Value = verifiedby.ToString();
           cmd.Parameters.Add("@Reason", SqlDbType.VarChar, 20).Value = reasoncode.ToString();
           cmd.Parameters.Add("@pharmacylocation", SqlDbType.VarChar, 50).Value = location.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int Retval = (int)par.Value;
           return Retval;
       }


       //* CARTRIDGE UNLOADING *\\


       // * Cartridge Unload Insert Procedure * \\
       public int cartunloadinsert(Int64 loadingid, string cartno, int physical, int discrepency, string verifiedby, string remarks, string location)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Cartridge_unload";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@loadingid", SqlDbType.BigInt).Value = loadingid;
           cmd.Parameters.Add("@Cartid", SqlDbType.VarChar, 6).Value = cartno.ToString();           
           cmd.Parameters.Add("@Physical", SqlDbType.Int).Value = physical;
           cmd.Parameters.Add("@Discrepancy", SqlDbType.Int).Value = discrepency;
           cmd.Parameters.Add("@Disverified", SqlDbType.VarChar, 20).Value = verifiedby.ToString();
           cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 255).Value = remarks.ToString();
           cmd.Parameters.Add("@pharmacylocation", SqlDbType.VarChar, 50).Value = location.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int rtnval = (int)par.Value;
           return rtnval;
       } 

       // * DDS CARTRIDGE STATUS ENABLE / DISABLE * \\


       // * Cartridge Status Display Grid Enable * \\
       public void cartstatusgrid(GridView grid, string ddsno, int actstatus, string location, string itemname,string Ordby)
       {
           string commt = "SELECT p.DDS_Name,p.Cell_Id,p.Cartridge_Id,i.Item_Code,i.Item_Name,pm.PackType,UM.Pack_Size,IL.UOM,p.Aval_Quantity ,case p.Activation_Status when '5' Then (p.Aval_Quantity-p.Pre_Allocated) else p.Aval_Quantity end as Computed_Bal,case p.Activation_Status when '5' Then (p.Pre_Allocated) else  '0' end as Pre_Allocated,CONVERT(varchar,p.Expiry_Date,103) as Expiry_Date,case p.Activation_Status when '2' Then 'Disabled' when '4' Then 'Disabled' when '6' Then 'Disabled' when '3' Then 'Disabled' when '5' then 'Enabled' else '' end as Activation_Status,p.Reason,p.Batch_No from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Cartridge_Master as c on c.Cartridge_Id=p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID  where p.DDS_Name='" + ddsno + "' and p.Inventory_Status='2' and p.Activation_Status='" + actstatus + "' and ph.Location_Name='" + location + "' and i.Item_Name like'%" + itemname + "%' " + Ordby; 
           con.View(commt, grid);
       }

       // * Cartridge Status Display Grid Enable With CartridgeID * \\
       public void Cartstatusgrid_Enable_CartNo(GridView grid, string ddsno, int actstatus, string location, string CartridgeNo, string Ordby)
       {
           string commt = "SELECT p.DDS_Name,p.Cell_Id,p.Cartridge_Id,i.Item_Code,i.Item_Name,pm.PackType,UM.Pack_Size,IL.UOM,p.Aval_Quantity ,case p.Activation_Status when '5' Then (p.Aval_Quantity-p.Pre_Allocated) else p.Aval_Quantity end as Computed_Bal,case p.Activation_Status when '5' Then (p.Pre_Allocated) else  '0' end as Pre_Allocated,CONVERT(varchar,p.Expiry_Date,103) as Expiry_Date,case p.Activation_Status when '2' Then 'Disabled' when '4' Then 'Disabled' when '6' Then 'Disabled' when '3' Then 'Disabled' when '5' then 'Enabled' else '' end as Activation_Status,p.Reason,p.Batch_No from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Cartridge_Master as c on c.Cartridge_Id=p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID  where p.DDS_Name='" + ddsno + "' and p.Inventory_Status='2' and p.Activation_Status='" + actstatus + "' and ph.Location_Name='" + location + "' and p.Cartridge_Id='" + CartridgeNo + "' " + Ordby;
           con.View(commt, grid);
       }   

       // * Cartridge Status Display Grid Disable  * \\
       public void cartstatusgriddisable(GridView grid, string ddsno, string location, string itemname,string Ordby)
       {
           string commt = "SELECT p.DDS_Name,p.Cell_Id,p.Cartridge_Id,i.Item_Code,i.Item_Name,pm.PackType,UM.Pack_Size,IL.UOM,p.Aval_Quantity,p.Aval_Quantity as Computed_Bal,'' as Pre_Allocated,CONVERT(varchar,p.Expiry_Date,103) as Expiry_Date,case p.Activation_Status when '2' Then 'Disabled' when '4' Then 'Disabled' when '3' Then 'Disabled'  when '6' Then 'Disabled'  when '5' then 'Enabled' else '' end as Activation_Status,p.Reason,p.Batch_No from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Cartridge_Master as c on c.Cartridge_Id=p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID  left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.DDS_Name='" + ddsno + "' and p.Inventory_Status='2' and p.Activation_Status<>'5' and p.Activation_Status<>'1' and p.Activation_Status<>'7' and ph.Location_Name='" + location + "' and i.Item_Name like'%" + itemname + "%' " + Ordby; 
           con.View(commt, grid);
       }

       // * Cartridge Status Display Grid Disable With CartridgeID * \\
       public void Cartstatusgrid_disable_CartNo(GridView grid, string ddsno, string location, string CartridgeNo, string Ordby)
       {
           string commt = "SELECT p.DDS_Name,p.Cell_Id,p.Cartridge_Id,i.Item_Code,i.Item_Name,pm.PackType,UM.Pack_Size,IL.UOM,p.Aval_Quantity,p.Aval_Quantity as Computed_Bal,'' as Pre_Allocated,CONVERT(varchar,p.Expiry_Date,103) as Expiry_Date,case p.Activation_Status when '2' Then 'Disabled' when '4' Then 'Disabled' when '3' Then 'Disabled'  when '6' Then 'Disabled'  when '5' then 'Enabled' else '' end as Activation_Status,p.Reason,p.Batch_No from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Cartridge_Master as c on c.Cartridge_Id=p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID  left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.DDS_Name='" + ddsno + "' and p.Inventory_Status='2' and p.Activation_Status<>'5' and p.Activation_Status<>'1' and p.Activation_Status<>'7' and ph.Location_Name='" + location + "' and p.Cartridge_Id='" + CartridgeNo + "' " + Ordby;
           con.View(commt, grid);
       }


       // * Cartridge Status Display Grid Disable  * \\
       public void BDS_Partial_Cartridge(GridView grid, string ddsno, string location, string itemname, string Ordby)
       {
           string commt = "SELECT p.DDS_Name,p.Cell_Id,p.Cartridge_Id,i.Item_Code,i.Item_Name,pm.PackType,UM.Pack_Size,IL.UOM,p.Aval_Quantity,p.Aval_Quantity as Computed_Bal,'' as Pre_Allocated,CONVERT(varchar,p.Expiry_Date,103) as Expiry_Date,case p.Activation_Status when '2' Then 'Disabled' when '4' Then 'Disabled' when '3' Then 'Disabled'  when '6' Then 'Disabled'  when '5' then 'Enabled' else '' end as Activation_Status,p.Reason,p.Batch_No from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Cartridge_Master as c on c.Cartridge_Id=p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID  left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.DDS_Name='" + ddsno + "' and p.Inventory_Status='2' and p.Activation_Status='7' and ph.Location_Name='" + location + "' and i.Item_Name like'%" + itemname + "%' " + Ordby;
           con.View(commt, grid);
       }

       // * Cartridge Status Display Grid Disable With CartridgeID * \\
       public void BDS_Partial_Cartridge_CartNo(GridView grid, string ddsno, string location, string CartridgeNo, string Ordby)
       {
           string commt = "SELECT p.DDS_Name,p.Cell_Id,p.Cartridge_Id,i.Item_Code,i.Item_Name,pm.PackType,UM.Pack_Size,IL.UOM,p.Aval_Quantity,p.Aval_Quantity as Computed_Bal,'' as Pre_Allocated,CONVERT(varchar,p.Expiry_Date,103) as Expiry_Date,case p.Activation_Status when '2' Then 'Disabled' when '4' Then 'Disabled' when '3' Then 'Disabled'  when '6' Then 'Disabled'  when '5' then 'Enabled' else '' end as Activation_Status,p.Reason,p.Batch_No from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Cartridge_Master as c on c.Cartridge_Id=p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID  left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.DDS_Name='" + ddsno + "' and p.Inventory_Status='2' and p.Activation_Status='7' and ph.Location_Name='" + location + "' and p.Cartridge_Id='" + CartridgeNo + "' " + Ordby;
           con.View(commt, grid);
       }


       // * Cartridge Status Display Grid All * \\
       public void cartstatusgridall(GridView grid, string ddsno, string location, string itemname, string Ordby)
       {
           string commt = "SELECT p.DDS_Name,p.Cell_Id,p.Cartridge_Id,i.Item_Code,i.Item_Name,pm.PackType,UM.Pack_Size,IL.UOM,p.Aval_Quantity,case p.Activation_Status when '5' Then (p.Aval_Quantity-p.Pre_Allocated) else p.Aval_Quantity end as Computed_Bal,case p.Activation_Status when '5' Then (p.Pre_Allocated) else '' end as Pre_Allocated,CONVERT(varchar,p.Expiry_Date,103) as Expiry_Date,case p.Activation_Status when '2' Then 'Disabled' when '4' Then 'Disabled' when '3' Then 'Disabled' when '6' Then 'Disabled'  when '5' then 'Enabled 'else '' end as Activation_Status,p.Reason,p.Batch_No from Cartridge_Loading  as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID  left join Cartridge_Master as c on c.Cartridge_Id=p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.DDS_Name='" + ddsno + "' and p.Inventory_Status='2' and p.Activation_Status<>'1' and p.Activation_Status<>'7' and ph.Location_Name='" + location + "' and i.Item_Name like'%" + itemname + "%' " + Ordby; 
           con.View(commt, grid);
       }

       // * Cartridge Status Display Grid All With CartridgeID * \\
       public void cartstatusgridall_CartNo(GridView grid, string ddsno, string location, string CartridgeNo, string Ordby)
       {
           string commt = "SELECT p.DDS_Name,p.Cell_Id,p.Cartridge_Id,i.Item_Code,i.Item_Name,pm.PackType,UM.Pack_Size,IL.UOM,p.Aval_Quantity,case p.Activation_Status when '5' Then (p.Aval_Quantity-p.Pre_Allocated) else p.Aval_Quantity end as Computed_Bal,case p.Activation_Status when '5' Then (p.Pre_Allocated) else '' end as Pre_Allocated,CONVERT(varchar,p.Expiry_Date,103) as Expiry_Date,case p.Activation_Status when '2' Then 'Disabled' when '4' Then 'Disabled' when '3' Then 'Disabled' when '6' Then 'Disabled'  when '5' then 'Enabled 'else '' end as Activation_Status,p.Reason,p.Batch_No from Cartridge_Loading  as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID  left join Cartridge_Master as c on c.Cartridge_Id=p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.DDS_Name='" + ddsno + "' and p.Inventory_Status='2' and p.Activation_Status<>'1' and p.Activation_Status<>'7'  and ph.Location_Name='" + location + "' and p.Cartridge_Id='" + CartridgeNo + "' " + Ordby;
           con.View(commt, grid);
       }
  

       // * Empty Cell Location Display * \\
       public void cartstatusgridempty(GridView grid, string ddsno, string location, string itemname)
       {
           //string commt = "SELECT DDS_Name,cell_no as Cell_Id ,Cartridge_Id,'' as Item_Code,'' as Item_Name,'' as PackType,'' as Pack_Size,'' as UOM,'' as Aval_Quantity,'' as Expiry_Date,'' as Activation_Status,'' as Reason from Cell where DDS_Name='" + ddsno + "' and Status='empty' order by ID asc ";
           string commt = "SELECT p.DDS_Name,p.Cell_Id,p.Cartridge_Id,i.Item_Code,i.Item_Name,pm.PackType,UM.Pack_Size,IL.UOM,p.Aval_Quantity,'' as Computed_Bal,'' as Pre_Allocated,CONVERT(varchar,p.Expiry_Date,103) as Expiry_Date,case p.Activation_Status when '2' Then 'Disabled' when '4' Then 'Disabled' when '3' Then 'Disabled' when '6' Then 'Disabled'  when '5' then 'Enabled 'else '' end as Activation_Status,p.Reason,p.Batch_No from Cartridge_Loading  as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Cartridge_Master as c on c.Cartridge_Id=p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.DDS_Name='" + ddsno + "' and p.Inventory_Status='2' and p.Activation_Status<>'1'  and ph.Location_Name='" + location + "' and i.Item_Name like'%" + itemname + "%' and p.Aval_Quantity=0 order by p.Loaded_MC_Date desc";
           con.View(commt, grid);
       }

       // * Empty Cell Location Display With CartridgeID* \\
       public void cartstatusgridempty_CartNo(GridView grid, string ddsno, string location, string CartridgeNo)
       {
           //string commt = "SELECT DDS_Name,cell_no as Cell_Id ,Cartridge_Id,'' as Item_Code,'' as Item_Name,'' as PackType,'' as Pack_Size,'' as UOM,'' as Aval_Quantity,'' as Expiry_Date,'' as Activation_Status,'' as Reason from Cell where DDS_Name='" + ddsno + "' and Status='empty' order by ID asc ";
           string commt = "SELECT p.DDS_Name,p.Cell_Id,p.Cartridge_Id,i.Item_Code,i.Item_Name,pm.PackType,UM.Pack_Size,IL.UOM,p.Aval_Quantity,'' as Computed_Bal,'' as Pre_Allocated,CONVERT(varchar,p.Expiry_Date,103) as Expiry_Date,case p.Activation_Status when '2' Then 'Disabled' when '4' Then 'Disabled' when '3' Then 'Disabled' when '6' Then 'Disabled'  when '5' then 'Enabled 'else '' end as Activation_Status,p.Reason,p.Batch_No from Cartridge_Loading  as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Cartridge_Master as c on c.Cartridge_Id=p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.DDS_Name='" + ddsno + "' and p.Inventory_Status='2' and p.Activation_Status<>'1'  and ph.Location_Name='" + location + "' and p.Cartridge_Id='" + CartridgeNo + "' and p.Aval_Quantity=0 order by p.Loaded_MC_Date desc";
           con.View(commt, grid);
       }

  

       // * Low Stock grid Display * \\
       public void cartstatuslowstock(GridView grid, string ddsno, string location, string itemname, string Ordby)
       {
           string commt = "SELECT p.DDS_Name,p.Cell_Id,p.Cartridge_Id,i.Item_Code,i.Item_Name,pm.PackType,UM.Pack_Size,IL.UOM,p.Aval_Quantity,'' as Computed_Bal,'' as Pre_Allocated,CONVERT(varchar,p.Expiry_Date,103) as Expiry_Date,case p.Activation_Status when '2' Then 'Disabled' when '4' Then 'Disabled' when '6' Then 'Disabled' when '3' Then 'Disabled' when '5' then 'Enabled'else '' end as Activation_Status,p.Reason,p.Batch_No from Cartridge_Loading  as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Cartridge_Master as c on c.Cartridge_Id=p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.DDS_Name='" + ddsno + "' and p.Inventory_Status='2' and p.Activation_Status='5' and ph.Location_Name='" + location + "' and i.Item_Name like'%" + itemname + "%' and p.totqty <=UM.Max_Alert_Qty_DDS " + Ordby; 
           con.View(commt, grid);
       }


       // * Low Stock grid Display With CartridgeID* \\
       public void cartstatuslowstock_CartNo(GridView grid, string ddsno, string location, string CartridgeNo, string Ordby)
       {
           string commt = "SELECT p.DDS_Name,p.Cell_Id,p.Cartridge_Id,i.Item_Code,i.Item_Name,pm.PackType,UM.Pack_Size,IL.UOM,p.Aval_Quantity,'' as Computed_Bal,'' as Pre_Allocated,CONVERT(varchar,p.Expiry_Date,103) as Expiry_Date,case p.Activation_Status when '2' Then 'Disabled' when '4' Then 'Disabled' when '6' Then 'Disabled' when '3' Then 'Disabled' when '5' then 'Enabled'else '' end as Activation_Status,p.Reason,p.Batch_No from Cartridge_Loading  as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Cartridge_Master as c on c.Cartridge_Id=p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.DDS_Name='" + ddsno + "' and p.Inventory_Status='2' and p.Activation_Status='5' and ph.Location_Name='" + location + "' and p.Cartridge_Id='" + CartridgeNo + "' and p.totqty <=UM.Max_Alert_Qty_DDS " + Ordby;
           con.View(commt, grid);
       }


       // * Removed Cartridge grid Display * \\
       public void Cartstatus_Removed(GridView grid, string ddsno, string location)
       {
           string commt = "SELECT p.Cart_Remove_From as DDS_Name,p.Cell_Id,p.Cartridge_Id,i.Item_Code,i.Item_Name,pm.PackType,UM.Pack_Size,IL.UOM,p.Aval_Quantity,'' as Computed_Bal,'' as Pre_Allocated,CONVERT(varchar,p.Expiry_Date,103) as Expiry_Date,case p.Activation_Status when '2' Then 'Disabled' when '4' Then 'Disabled' when '6' Then 'Disabled' when '3' Then 'Disabled' when '5' then 'Enabled'else '' end as Activation_Status,p.Reason,p.Batch_No from Cartridge_Loading  as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Cartridge_Master as c on c.Cartridge_Id=p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Cart_Remove_From='" + ddsno + "' and p.Inventory_Status=2 and p.Activation_Status=6 and ph.Location_Name='" + location + "' and p.Cell_Id='0' and p.DDS_Name is null order by p.Remove_MC_Date "; 
           con.View(commt, grid);
       }  

       // * Invalid Cartridge Display * \\
       public void invalidcartridge(GridView grid, string ddsno)
       {
           string commt = "select DDSName as DDS_Name,CellNo as Cell_Id,CartridgeID as cartridge_Id,'' as Item_Code,'' as Item_Name,'' as PackType,'' as Pack_Size,'' as UOM,'' as Aval_Quantity,'' as Computed_Bal,'' as Pre_Allocated,'' as Expiry_Date,'' as Activation_Status,Reason,'' as Batch_No from Invalid_Cartridge where Status='New' and DDSName='" + ddsno + "' order by ID desc"; 
           con.View(commt,grid);
       }

       // * Preloaded Count checking status All  * \\
       public void BDS_Parctial_Cartridge_Enable(string cartid,string Cell,string BDSName)
       {
           string cmd2 = "update BDS_Temp_Loading set Status='Finish' where LoadingID=(select Loading_Id from Cartridge_Loading where DDS_Name='" + BDSName + "' and Cartridge_Id='" + cartid + "' and Cell_Id='" + Cell + "' and Inventory_Status=2 and Activation_Status=7)";
           con.SetDataBase(cmd2);
           
           string cmd = "update Cartridge_Loading set Activation_Status=2 where DDS_Name='" + BDSName + "' and Cartridge_Id='"+cartid+"' and Cell_Id='"+Cell+"' and Inventory_Status=2 and Activation_Status=7";
           con.SetDataBase(cmd);
          
       }


       // * Cartridge Enableing * \\
       public int Cartenable(string location, string cartid,string Cell, string itemcode,string DDSName,string enableby)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Cartridge_Enable";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Cartridgeid", SqlDbType.VarChar, 10).Value = cartid.ToString();
           cmd.Parameters.Add("@pharmacylocation", SqlDbType.VarChar, 50).Value = location.ToString();
           cmd.Parameters.Add("@Itemcode", SqlDbType.VarChar, 20).Value = itemcode.ToString();
           cmd.Parameters.Add("@DDSName", SqlDbType.VarChar, 20).Value = DDSName.ToString();
           cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = enableby.ToString();
           cmd.Parameters.Add("@Actmode", SqlDbType.VarChar, 10).Value = "Manual";
           cmd.Parameters.Add("@Cellno", SqlDbType.VarChar, 10).Value = Cell.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int rtnval = (int)par.Value;
           return rtnval;
       }
       // * Cartridge Disableing * \\
       public int Cartdisable(string location, string cartid,string Disableby)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Cartridge_Disable";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Cartridgeid", SqlDbType.VarChar,10).Value = cartid.ToString();
           cmd.Parameters.Add("@pharmacylocation", SqlDbType.VarChar, 50).Value = location.ToString();
           cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = Disableby.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int rtnval = (int)par.Value;
           return rtnval;
       }

       // * PRELOADED CARTRIDGE STATUS ENQUIRY * \\

       // * Item Code Exact Search Based On Status * \\
       public void cartstatusitemcode(GridView grid, string itemcod, string status, string location, string OrdBy)
       {
           string Bot = "Bottle";
           if (status != "Both")
           {
               string commt = "Select p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.Verified_by,convert(varchar(20),f.Verified_Date ,113) as FVdate,s.Verified_by as Sverify,convert(varchar(20),s.Verified_Date,113) as SVdate,r.reason from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Verification_Rejected as r on p.loading_id=r.loading_id left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2' and  p.Verified_Status='" + status + "' and i.Item_Code='" + itemcod + "' and ph.Location_Name='" + location + "' and P.Activation_Status='1' and pm.Packtype not like '%" + Bot + "%'" + OrdBy;
               con.View(commt, grid);
           }
           else if (status == "Both")
           {
               string commt = "Select p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.Verified_by,convert(varchar(20),f.Verified_Date ,113) as FVdate,s.Verified_by as Sverify,convert(varchar(20),s.Verified_Date,113) as SVdate,r.reason from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Verification_Rejected as r on p.loading_id=r.loading_id left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2' and p.Verified_Status<>'Loaded' and p.Verified_Status<>'Unloading' and p.Verified_Status<>'First Rejected' and p.Verified_Status<>'Second Rejected' and i.Item_Code='" + itemcod + "' and ph.Location_Name='" + location + "' and P.Activation_Status='1' and pm.Packtype not like '%" + Bot + "%' " + OrdBy; ;
               con.View(commt, grid);
           }
       }
       // * Itema Code Exact With Status All * \\
       public void cartstatusitemcodeall(GridView grid, string itemcod, string location,string OrdBy)
       {
           string Bot = "Bottle";
           string commt = "Select p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.Verified_by,convert(varchar(20),f.Verified_Date ,113) as FVdate,s.Verified_by as Sverify,convert(varchar(20),s.Verified_Date,113) as SVdate,r.reason from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Verification_Rejected as r on p.loading_id=r.loading_id left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2' and i.Item_Code='" + itemcod + "' and ph.Location_Name='" + location + "' and P.Activation_Status='1' and pm.Packtype not like '%" + Bot + "%'" + OrdBy;
           con.View(commt, grid);
       }
       // * Drug code Begin Search Based On Status * \\
       public void cartstatusdrug(GridView grid, string Drugcode, string status, string location, string OrdBy)
       {
           string Bot = "Bottle";
           if (status != "Both")
           {
               string commt = "Select p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.Verified_by,convert(varchar(20),f.Verified_Date ,113) as FVdate,s.Verified_by as Sverify,convert(varchar(20),s.Verified_Date,113) as SVdate,r.reason from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Verification_Rejected as r on p.loading_id=r.loading_id left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2' and  p.Verified_Status='" + status + "' and i.Drug_Code like'" + Drugcode + "%' and ph.Location_Name='" + location + "' and P.Activation_Status='1' and pm.Packtype not like '%" + Bot + "%'" + OrdBy;
               con.View(commt, grid);
           }
           else if (status == "Both")
           {
               string commt = "Select p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.Verified_by,convert(varchar(20),f.Verified_Date ,113) as FVdate,s.Verified_by as Sverify,convert(varchar(20),s.Verified_Date,113) as SVdate,r.reason from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Verification_Rejected as r on p.loading_id=r.loading_id left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2' and  p.Verified_Status<>'Loaded' and p.Verified_Status<>'Unloading' and p.Verified_Status<>'First Rejected' and p.Verified_Status<>'Second Rejected' and i.Drug_Code like'" + Drugcode + "%' and ph.Location_Name='" + location + "' and P.Activation_Status='1' and pm.Packtype not like '%" + Bot + "%'" + OrdBy;
               con.View(commt, grid);
           }
       }
       // * Drug Code Begin search Status All * \\
       public void cartstatusdrugall(GridView grid, string Drugcode, string location, string OrdBy)
       {
           string Bot = "Bottle";
           string commt = "Select p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.Verified_by,convert(varchar(20),f.Verified_Date ,113) as FVdate,s.Verified_by as Sverify,convert(varchar(20),s.Verified_Date,113) as SVdate,r.reason from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Verification_Rejected as r on p.loading_id=r.loading_id left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2' and i.Drug_Code like'" + Drugcode + "%' and ph.Location_Name='" + location + "' and P.Activation_Status='1' and pm.Packtype not like '%" + Bot + "%'" + OrdBy;
           con.View(commt, grid);
          
       }

       // * Item Name Instring Search Based On Status * \\
       public void cartstatusname(GridView grid, string itmname, string status, string location, string OrdBy)
       {
           string Bot = "Bottle";
           if (status != "Both")
           {
               string commt = "Select p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.Verified_by,convert(varchar(20),f.Verified_Date ,113) as FVdate,s.Verified_by as Sverify,convert(varchar(20),s.Verified_Date,113) as SVdate,r.reason from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Verification_Rejected as r on p.loading_id=r.loading_id left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2' and  p.Verified_Status='" + status + "' and i.Item_Name like'%" + itmname + "%' and ph.Location_Name='" + location + "' and P.Activation_Status='1' and pm.Packtype not like '%" + Bot + "%'" + OrdBy;
               con.View(commt, grid);
           }
           else if (status == "Both")
           {
               string commt = "Select p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.Verified_by,convert(varchar(20),f.Verified_Date ,113) as FVdate,s.Verified_by as Sverify,convert(varchar(20),s.Verified_Date,113) as SVdate,r.reason from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Verification_Rejected as r on p.loading_id=r.loading_id left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2' and  p.Verified_Status<>'Loaded' and p.Verified_Status<>'Unloading' and p.Verified_Status<>'First Rejected' and p.Verified_Status<>'Second Rejected' and i.Item_Name like'%" + itmname + "%' and ph.Location_Name='" + location + "' and P.Activation_Status='1' and pm.Packtype not like '%" + Bot + "%'" + OrdBy;
               con.View(commt, grid);
           }
       }
       // * Item Name Instring search Status All * \\
       public void cartstatusnameall(GridView grid, string itmname, string location, string OrdBy)
       {
           string Bot = "Bottle";
           string commt = "Select p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.Verified_by,convert(varchar(20),f.Verified_Date ,113) as FVdate,s.Verified_by as Sverify,convert(varchar(20),s.Verified_Date,113) as SVdate,r.reason from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Verification_Rejected as r on p.loading_id=r.loading_id left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2' and i.Item_Name like'%" + itmname + "%' and ph.Location_Name='" + location + "' and P.Activation_Status='1' and pm.Packtype not like '%" + Bot + "%' " + OrdBy;
           con.View(commt, grid);
       }
       // * Brand Name Begin Search Based on status * \\
       public void cartstatusbrand(GridView grid, string brandname, string status, string location, string OrdBy)
       {
           string Bot = "Bottle";
           if (status != "Both")
           {
               string commt = "Select p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.Verified_by,convert(varchar(20),f.Verified_Date ,113) as FVdate,s.Verified_by as Sverify,convert(varchar(20),s.Verified_Date,113) as SVdate, r.reason from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Verification_Rejected as r on p.loading_id=r.loading_id left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2' and p.Verified_Status='" + status + "' and  b.Brandname like '" + brandname + "%' and ph.Location_Name='" + location + "' and P.Activation_Status='1' and pm.Packtype not like '%" + Bot + "%'" + OrdBy;
               con.View(commt, grid);
           }
           else if (status == "Both")
           {
               string commt = "Select p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.Verified_by,convert(varchar(20),f.Verified_Date ,113) as FVdate,s.Verified_by as Sverify,convert(varchar(20),s.Verified_Date,113) as SVdate, r.reason from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Verification_Rejected as r on p.loading_id=r.loading_id left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2' and p.Verified_Status<>'Loaded' and p.Verified_Status<>'Unloading' and p.Verified_Status<>'First Rejected' and p.Verified_Status<>'Second Rejected' and  b.Brandname like '" + brandname + "%' and ph.Location_Name='" + location + "' and P.Activation_Status='1' and pm.Packtype not like '%" + Bot + "%'" + OrdBy;
               con.View(commt, grid);
           }
       }
       // * Brand Name Begin Search Status All * \\
       public void cartstatusbrandall(GridView grid, string brandname, string location, string OrdBy)
       {
           string Bot = "Bottle";
           string commt = "Select p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.Verified_by,convert(varchar(20),f.Verified_Date ,113) as FVdate,s.Verified_by as Sverify,convert(varchar(20),s.Verified_Date,113) as SVdate, r.reason from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Verification_Rejected as r on p.loading_id=r.loading_id left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2' and  b.Brandname like '" + brandname + "%' and ph.Location_Name='" + location + "' and P.Activation_Status='1' and pm.Packtype not like '%" + Bot + "%'" + OrdBy;
           con.View(commt, grid);
       }

       // * MFR Barcode Exact Search Based On Status * \\
       public void cartstatusmfr(GridView grid, string mfrcode, string status, string location, string OrdBy)
       {
           string Bot = "Bottle";
           if (status != "Both")
           {
               string commt = "Select p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.Verified_by,convert(varchar(20),f.Verified_Date ,113) as FVdate,s.Verified_by as Sverify,convert(varchar(20),s.Verified_Date,113) as SVdate, r.reason from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID  left join Loaded_by as l on l.loading_id=p.Loading_Id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Brand_Master as b on b.BrandID=p.Brandid left join MFR_Barcode as m on m.Brandid =b.BrandID and m.Masterid=i.MasterID left join Verification_Rejected as r on p.loading_id=r.loading_id left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2'  and ph.Location_Name='" + location + "' and p.Verified_Status='" + status + "' and m.Mfrbarcode='" + mfrcode + "' and P.Activation_Status='1' and pm.Packtype not like '%" + Bot + "%'" + OrdBy;
               con.View(commt, grid);
           }
           else if (status == "Both")
           {
               string commt = "Select p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.Verified_by,convert(varchar(20),f.Verified_Date ,113) as FVdate,s.Verified_by as Sverify,convert(varchar(20),s.Verified_Date,113) as SVdate, r.reason from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID  left join Loaded_by as l on l.loading_id=p.Loading_Id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Brand_Master as b on b.BrandID=p.Brandid left join MFR_Barcode as m on m.Brandid =b.BrandID and m.Masterid=i.MasterID left join Verification_Rejected as r on p.loading_id=r.loading_id left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2'  and ph.Location_Name='" + location + "' and p.Verified_Status<>'Loaded' and p.Verified_Status<>'Unloading' and p.Verified_Status<>'First Rejected' and p.Verified_Status<>'Second Rejected' and m.Mfrbarcode='" + mfrcode + "' and P.Activation_Status='1' and pm.Packtype not like '%" + Bot + "%'" + OrdBy;
               con.View(commt, grid);
           }
       }

       // * MFR Barcode Exact Search Status All * \\
       public void cartstatusmfrall(GridView grid, string mfrcode, string location, string OrdBy)
       {
           string Bot = "Bottle";
           string commt = "Select p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.Verified_by,convert(varchar(20),f.Verified_Date ,113) as FVdate,s.Verified_by as Sverify,convert(varchar(20),s.Verified_Date,113) as SVdate, r.reason from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Brand_Master as b on b.BrandID=p.Brandid left join MFR_Barcode as m on m.Brandid =b.BrandID and m.Masterid=i.MasterID left join Verification_Rejected as r on p.loading_id=r.loading_id left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2'  and ph.Location_Name='" + location + "' and m.Mfrbarcode='" + mfrcode + "' and P.Activation_Status='1' and pm.Packtype not like '%" + Bot + "%'" + OrdBy;
           con.View(commt, grid);
       }

       // * Cartridge Number Exact Search Based on Status * \\
       public void cartstatuscartno(GridView grid, string cartno, string status, string location, string OrdBy)
       {
           string Bot = "Bottle";
           if (status != "Both")
           {
               string commt = "Select p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.Verified_by,convert(varchar(20),f.Verified_Date ,113) as FVdate,s.Verified_by as Sverify,convert(varchar(20),s.Verified_Date,113) as SVdate,r.reason from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID  left join Loaded_by as l on l.loading_id=p.Loading_Id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Verification_Rejected as r on p.loading_id=r.loading_id left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2' and p.Verified_Status='" + status + "' and p.Cartridge_Id='" + cartno + "' and ph.Location_Name='" + location + "' and P.Activation_Status='1' and pm.Packtype not like '%" + Bot + "%'" + OrdBy;
               con.View(commt, grid);
           }
           else if (status == "Both")
           {
               string commt = "Select p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.Verified_by,convert(varchar(20),f.Verified_Date ,113) as FVdate,s.Verified_by as Sverify,convert(varchar(20),s.Verified_Date,113) as SVdate,r.reason from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID  left join Loaded_by as l on l.loading_id=p.Loading_Id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Verification_Rejected as r on p.loading_id=r.loading_id left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2' and p.Verified_Status<>'Loaded' and p.Verified_Status<>'Unloading' and p.Verified_Status<>'First Rejected' and p.Verified_Status<>'Second Rejected' and p.Cartridge_Id='" + cartno + "' and ph.Location_Name='" + location + "' and P.Activation_Status='1' and pm.Packtype not like '%" + Bot + "%'" + OrdBy; 
               con.View(commt, grid);
           }
       }

       // * Cartridge Number Exact Search Status All * \\
       public void cartstatuscartnoall(GridView grid, string cartno, string location, string OrdBy)
       {
           string Bot = "Bottle";
           string commt = "Select p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,IL.UOM,p.Verified_Status,L.Loaded_by,convert(varchar(20),L.Loaded_Date,113) as Loaded_Date,f.Verified_by,convert(varchar(20),f.Verified_Date ,113) as FVdate,s.Verified_by as Sverify,convert(varchar(20),s.Verified_Date,113) as SVdate,r.reason from Cartridge_Loading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID  left join Loaded_by as l on l.loading_id=p.Loading_Id left join First_Verification as f on f.Loading_id=p.Loading_Id left join Second_Verification as s on s.Loading_Id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Verification_Rejected as r on p.loading_id=r.loading_id left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status='2'  and p.Cartridge_Id='" + cartno + "' and ph.Location_Name='" + location + "' and P.Activation_Status='1' and pm.Packtype not like '%" + Bot + "%'" + OrdBy;
           con.View(commt, grid);
       }

       //-------------------------------------------------------

       // *BDS PRELOADED CARTRIDGE STATUS ENQUIRY FOR  \\ *

       // * Itema Code Exact With Status WIP * \\
       public void BDS_cartstatusitemcode_WIP(GridView grid, string itemcod, string location,string OrdBy)
       {
           string commt = "Select P.Cart_Barcode,p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,P.Cart_Type,IL.UOM,'' as Pending_Quantity,p.Quantity as Alloted_Quantity,p.Aval_Quantity as Loaded_Quantity,'Carton Box Item Cartridge Allocated' as Verified_Status,Bp.Loaded_By,convert(varchar(20),Bp.LoadedDate,113) as Loaded_Date,Bp.First_Verified,convert(varchar(20),Bp.First_Verified_DT,113) as First_Verified_DT from Cartridge_Loading as p left join BDS_PreLoading as Bp on Bp.Cart_Barcode=p.Cart_Barcode left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status=2 and i.Item_Code='" + itemcod + "' and ph.Location_Name='" + location + "' and P.Activation_Status=1 and DDS_Name like'%BDS%'  " + OrdBy;
           con.View(commt, grid);
       }

       // * Drug Code Begin search Status WIP * \\
       public void BDS_cartstatusdrugcode_WIP(GridView grid, string Drugcode, string location,string OrdBy)
       {
           string commt = "Select P.Cart_Barcode,p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,P.Cart_Type,IL.UOM,'' as Pending_Quantity,p.Quantity as Alloted_Quantity,p.Aval_Quantity as Loaded_Quantity,'Carton Box Item Cartridge Allocated' as Verified_Status,Bp.Loaded_By,convert(varchar(20),Bp.LoadedDate,113) as Loaded_Date,Bp.First_Verified,convert(varchar(20),Bp.First_Verified_DT,113) as First_Verified_DT from Cartridge_Loading as p left join BDS_PreLoading as Bp on Bp.Cart_Barcode=p.Cart_Barcode left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status=2 and i.Drug_Code like'" + Drugcode + "%' and ph.Location_Name='" + location + "' and P.Activation_Status=1 and DDS_Name like'%BDS%' " + OrdBy;
           con.View(commt, grid);
       }

       // * Item Name Instring search Status WIP * \\
       public void BDS_cartstatusname_WIP(GridView grid, string itmname, string location,string OrdBy)
       {
           string commt = "Select P.Cart_Barcode,p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,P.Cart_Type,IL.UOM,'' as Pending_Quantity,p.Quantity as Alloted_Quantity,p.Aval_Quantity as Loaded_Quantity,'Carton Box Item Cartridge Allocated' as Verified_Status,Bp.Loaded_By,convert(varchar(20),Bp.LoadedDate,113) as Loaded_Date,Bp.First_Verified,convert(varchar(20),Bp.First_Verified_DT,113) as First_Verified_DT from Cartridge_Loading as p left join BDS_PreLoading as Bp on Bp.Cart_Barcode=p.Cart_Barcode left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status=2 and i.Item_Name like'%" + itmname + "%' and ph.Location_Name='" + location + "' and P.Activation_Status=1 and DDS_Name like'%BDS%' " + OrdBy;
           con.View(commt, grid);
       }

       // * Brand Name Begin Search Status WIP * \\
       public void BDS_cartstatusbrand_WIP(GridView grid, string brandname, string location,string OrdBy)
       {
           string commt = "Select P.Cart_Barcode,p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,P.Cart_Type,IL.UOM,'' as Pending_Quantity,p.Quantity as Alloted_Quantity,p.Aval_Quantity as Loaded_Quantity,'Carton Box Item Cartridge Allocated' as Verified_Status,Bp.Loaded_By,convert(varchar(20),Bp.LoadedDate,113) as Loaded_Date,Bp.First_Verified,convert(varchar(20),Bp.First_Verified_DT,113) as First_Verified_DT from Cartridge_Loading as p left join BDS_PreLoading as Bp on Bp.Cart_Barcode=p.Cart_Barcode left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status=2 and b.Brandname like '" + brandname + "%' and ph.Location_Name='" + location + "' and P.Activation_Status=1 and DDS_Name like'%BDS%' " + OrdBy;
           con.View(commt, grid);
       }

       // * Cartridge Number Exact Search Status WIP * \\
       public void BDS_cartstatuscartno_WIP(GridView grid, string cartno, string location,string Ordby)
       {
           string commt = "Select P.Cart_Barcode,p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,P.Cart_Type,IL.UOM,'' as Pending_Quantity,p.Quantity as Alloted_Quantity,p.Aval_Quantity as Loaded_Quantity,'Carton Box Item Cartridge Allocated' as Verified_Status,Bp.Loaded_By,convert(varchar(20),Bp.LoadedDate,113) as Loaded_Date,Bp.First_Verified,convert(varchar(20),Bp.First_Verified_DT,113) as First_Verified_DT from Cartridge_Loading as p left join BDS_PreLoading as Bp on Bp.Cart_Barcode=p.Cart_Barcode left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status=2 and p.Cartridge_Id='" + cartno + "' and ph.Location_Name='" + location + "' and P.Activation_Status=1 and DDS_Name like'%BDS%' " + Ordby;
           con.View(commt, grid);
       }

       // * NO Status Status WIP * \\
       public void BDS_cartstatus_No_Filter_WIP(GridView grid,  string location,string OrdBy)
       {
           string commt = "Select P.Cart_Barcode,p.Cartridge_Id,P.Loading_ID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,P.Cart_Type,IL.UOM,'' as Pending_Quantity,p.Quantity as Alloted_Quantity,p.Aval_Quantity as Loaded_Quantity,'Carton Box Item Cartridge Allocated' as Verified_Status,Bp.Loaded_By,convert(varchar(20),Bp.LoadedDate,113) as Loaded_Date,Bp.First_Verified,convert(varchar(20),Bp.First_Verified_DT,113) as First_Verified_DT from Cartridge_Loading as p left join BDS_PreLoading as Bp on Bp.Cart_Barcode=p.Cart_Barcode left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Loaded_by as l on l.loading_id=p.Loading_Id left join Brand_Master as b on b.BrandID=p.Brandid left join Cartridge_Master as c on c.Cartridge_Id = p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Inventory_Status=2 and ph.Location_Name='" + location + "' and P.Activation_Status=1 and DDS_Name like'%BDS%' " + OrdBy;
           con.View(commt, grid);
       }

       //-------------------------------------------------------

       // * Itema Code Exact With StatusPreLoad * \\
       public void BDS_cartstatusitemcode_PreLoad(GridView grid, string itemcod, string location,string Ordby)
       {
           string commt = "Select Bp.Cart_Barcode,'' as Cartridge_Id,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,Bp.Carton_Box_Quantity as Cart_Type,IL.UOM,'' as Pending_Quantity,'' as Alloted_Quantity,'' as Loaded_Quantity,'First Verified' as Verified_Status,Bp.Loaded_By,convert(varchar(20),Bp.LoadedDate,113) as Loaded_Date,Bp.First_Verified,convert(varchar(20),Bp.First_Verified_DT,113) as First_Verified_DT from BDS_PreLoading  as Bp left join Item_user_Master as UM on UM.ID=Bp.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Brand_Master as b on b.BrandID=um.Brandid left join Pharmacy as ph on ph.PharmacyID = Bp.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where  i.Item_Code='" + itemcod + "' and ph.Location_Name='" + location + "' and Bp.Status='NEW' and Bp.Verified_Status='First Verified' " + Ordby;
           con.View(commt, grid);
       }

       // * Drug Code Begin search Status PreLoad * \\
       public void BDS_cartstatusdrugcode_PreLoad(GridView grid, string Drugcode, string location, string Ordby)
       {
           string commt = "Select Bp.Cart_Barcode,'' as Cartridge_Id,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,Bp.Carton_Box_Quantity as Cart_Type,IL.UOM,'' as Pending_Quantity,'' as Alloted_Quantity,'' as Loaded_Quantity,'First Verified' as Verified_Status,Bp.Loaded_By,convert(varchar(20),Bp.LoadedDate,113) as Loaded_Date,Bp.First_Verified,convert(varchar(20),Bp.First_Verified_DT,113) as First_Verified_DT from BDS_PreLoading  as Bp left join Item_user_Master as UM on UM.ID=Bp.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Brand_Master as b on b.BrandID=um.Brandid left join Pharmacy as ph on ph.PharmacyID = Bp.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where  i.Drug_Code like'" + Drugcode + "%' and ph.Location_Name='" + location + "' and Bp.Status='NEW' and Bp.Verified_Status='First Verified' " + Ordby;
           con.View(commt, grid);
       }

       // * Item Name Instring search Status PreLoad * \\
       public void BDS_cartstatusname_PreLoad(GridView grid, string itmname, string location, string Ordby)
       {
           string commt = "Select Bp.Cart_Barcode,'' as Cartridge_Id,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,Bp.Carton_Box_Quantity as Cart_Type,IL.UOM,'' as Pending_Quantity,'' as Alloted_Quantity,'' as Loaded_Quantity,'First Verified' as Verified_Status,Bp.Loaded_By,convert(varchar(20),Bp.LoadedDate,113) as Loaded_Date,Bp.First_Verified,convert(varchar(20),Bp.First_Verified_DT,113) as First_Verified_DT from BDS_PreLoading  as Bp left join Item_user_Master as UM on UM.ID=Bp.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Brand_Master as b on b.BrandID=um.Brandid left join Pharmacy as ph on ph.PharmacyID = Bp.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where  i.Item_Name like'%" + itmname + "%' and ph.Location_Name='" + location + "' and Bp.Status='NEW' and Bp.Verified_Status='First Verified' " + Ordby;
           con.View(commt, grid);
       }

       // * Brand Name Begin Search Status PreLoad * \\
       public void BDS_cartstatusbrand_PreLoad(GridView grid, string brandname, string location, string Ordby)
       {
           string commt = "Select Bp.Cart_Barcode,'' as Cartridge_Id,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,Bp.Carton_Box_Quantity as Cart_Type,IL.UOM,'' as Pending_Quantity,'' as Alloted_Quantity,'' as Loaded_Quantity,'First Verified' as Verified_Status,Bp.Loaded_By,convert(varchar(20),Bp.LoadedDate,113) as Loaded_Date,Bp.First_Verified,convert(varchar(20),Bp.First_Verified_DT,113) as First_Verified_DT from BDS_PreLoading  as Bp left join Item_user_Master as UM on UM.ID=Bp.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Brand_Master as b on b.BrandID=um.Brandid left join Pharmacy as ph on ph.PharmacyID = Bp.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where  b.Brandname like '" + brandname + "%' and ph.Location_Name='" + location + "' and Bp.Status='NEW' and Bp.Verified_Status='First Verified'" + Ordby;
           con.View(commt, grid);
       }

       // * NO Status Status PreLoad * \\
       public void BDS_cartstatus_No_Filter_PreLoad(GridView grid, string location, string Ordby)
       {
           string commt = "Select Bp.Cart_Barcode,'' as Cartridge_Id,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,Bp.Carton_Box_Quantity as Cart_Type,IL.UOM,'' as Pending_Quantity,'' as Alloted_Quantity,'' as Loaded_Quantity,'First Verified' as Verified_Status,Bp.Loaded_By,convert(varchar(20),Bp.LoadedDate,113) as Loaded_Date,Bp.First_Verified,convert(varchar(20),Bp.First_Verified_DT,113) as First_Verified_DT from BDS_PreLoading  as Bp left join Item_user_Master as UM on UM.ID=Bp.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Brand_Master as b on b.BrandID=um.Brandid left join Pharmacy as ph on ph.PharmacyID = Bp.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where ph.Location_Name='" + location + "' and Bp.Status='NEW' and Bp.Verified_Status='First Verified'" + Ordby;
           con.View(commt, grid);
       }


       //-------------------------------------------------------


       // * Itema Code Exact With Status Pending First Verfication * \\
       public void BDS_cartstatusitemcode_First_Verfication(GridView grid, string itemcod, string location, string Ordby)
       {
           string commt = "Select Bp.Cart_Barcode,'' as Cartridge_Id,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,Bp.Carton_Box_Quantity as Cart_Type,IL.UOM,'' as Pending_Quantity,'' as Alloted_Quantity,'' as Loaded_Quantity,'Carton Box Preloaded' as Verified_Status,Bp.Loaded_By,convert(varchar(20),Bp.LoadedDate,113) as Loaded_Date,Bp.First_Verified,convert(varchar(20),Bp.First_Verified_DT,113) as First_Verified_DT from BDS_PreLoading  as Bp left join Item_user_Master as UM on UM.ID=Bp.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Brand_Master as b on b.BrandID=um.Brandid left join Pharmacy as ph on ph.PharmacyID = Bp.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where  i.Item_Code='" + itemcod + "' and ph.Location_Name='" + location + "' and Bp.Status='NEW' and Bp.Verified_Status='PreLoaded' " + Ordby;
           con.View(commt, grid);
       }

       // * Drug Code Begin search Status Pending First Verfication * \\
       public void BDS_cartstatusdrugcode_First_Verfication(GridView grid, string Drugcode, string location, string Ordby)
       {
           string commt = "Select Bp.Cart_Barcode,'' as Cartridge_Id,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,Bp.Carton_Box_Quantity as Cart_Type,IL.UOM,'' as Pending_Quantity,'' as Alloted_Quantity,'' as Loaded_Quantity,'Carton Box Preloaded' as Verified_Status,Bp.Loaded_By,convert(varchar(20),Bp.LoadedDate,113) as Loaded_Date,Bp.First_Verified,convert(varchar(20),Bp.First_Verified_DT,113) as First_Verified_DT from BDS_PreLoading  as Bp left join Item_user_Master as UM on UM.ID=Bp.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Brand_Master as b on b.BrandID=um.Brandid left join Pharmacy as ph on ph.PharmacyID = Bp.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where  i.Drug_Code like'" + Drugcode + "%' and ph.Location_Name='" + location + "' and Bp.Status='NEW' and Bp.Verified_Status='PreLoaded' " + Ordby;
           con.View(commt, grid);
       }

       // * Item Name Instring search Status Pending First Verfication * \\
       public void BDS_cartstatusname_First_Verfication(GridView grid, string itmname, string location, string Ordby)
       {
           string commt = "Select Bp.Cart_Barcode,'' as Cartridge_Id,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,Bp.Carton_Box_Quantity as Cart_Type,IL.UOM,'' as Pending_Quantity,'' as Alloted_Quantity,'' as Loaded_Quantity,'Carton Box Preloaded' as Verified_Status,Bp.Loaded_By,convert(varchar(20),Bp.LoadedDate,113) as Loaded_Date,Bp.First_Verified,convert(varchar(20),Bp.First_Verified_DT,113) as First_Verified_DT from BDS_PreLoading  as Bp left join Item_user_Master as UM on UM.ID=Bp.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Brand_Master as b on b.BrandID=um.Brandid left join Pharmacy as ph on ph.PharmacyID = Bp.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where  i.Item_Name like'%" + itmname + "%' and ph.Location_Name='" + location + "' and Bp.Status='NEW' and Bp.Verified_Status='PreLoaded' " + Ordby;
           con.View(commt, grid);
       }

       // * Brand Name Begin Search Status Pending First Verfication * \\
       public void BDS_cartstatusbrand_First_Verfication(GridView grid, string brandname, string location, string Ordby)
       {
           string commt = "Select Bp.Cart_Barcode,'' as Cartridge_Id,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,Bp.Carton_Box_Quantity as Cart_Type,IL.UOM,'' as Pending_Quantity,'' as Alloted_Quantity,'' as Loaded_Quantity,'Carton Box Preloaded' as Verified_Status,Bp.Loaded_By,convert(varchar(20),Bp.LoadedDate,113) as Loaded_Date,Bp.First_Verified,convert(varchar(20),Bp.First_Verified_DT,113) as First_Verified_DT from BDS_PreLoading  as Bp left join Item_user_Master as UM on UM.ID=Bp.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Brand_Master as b on b.BrandID=um.Brandid left join Pharmacy as ph on ph.PharmacyID = Bp.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where  b.Brandname like '" + brandname + "%' and ph.Location_Name='" + location + "' and Bp.Status='NEW' and Bp.Verified_Status='PreLoaded' " + Ordby;
           con.View(commt, grid);
       }

       // * NO Status Status Pending First Verfication * \\
       public void BDS_cartstatus_No_Filter_First_Verfication(GridView grid, string location, string Ordby)
       {
           string commt = "Select Bp.Cart_Barcode,'' as Cartridge_Id,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,Bp.Carton_Box_Quantity as Cart_Type,IL.UOM,'' as Pending_Quantity,'' as Alloted_Quantity,'' as Loaded_Quantity,'Carton Box Preloaded' as Verified_Status,Bp.Loaded_By,convert(varchar(20),Bp.LoadedDate,113) as Loaded_Date,Bp.First_Verified,convert(varchar(20),Bp.First_Verified_DT,113) as First_Verified_DT from BDS_PreLoading  as Bp left join Item_user_Master as UM on UM.ID=Bp.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Brand_Master as b on b.BrandID=um.Brandid left join Pharmacy as ph on ph.PharmacyID = Bp.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where ph.Location_Name='" + location + "' and Bp.Status='NEW' and Bp.Verified_Status='PreLoaded'" + Ordby;
           con.View(commt, grid);
       }



       //-------------------------------------------------------

       // * Itema Code Exact With Pending Allocation * \\
       public void BDS_cartstatusitemcode_Pending_Allocation(GridView grid, string itemcod, string location, string Ordby)
       {
           string commt = "Select Bp.Cart_Barcode,'' as Cartridge_Id,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,Bp.Carton_Box_Quantity as Cart_Type,IL.UOM,Bp.Cart_Allocate_Quantity as Pending_Quantity,(Bp.Carton_Box_Quantity-Bp.Cart_Allocate_Quantity) as Alloted_Quantity,'' as Loaded_Quantity,'Pending For VC Allocation' as Verified_Status,Bp.Loaded_By,convert(varchar(20),Bp.LoadedDate,113) as Loaded_Date,Bp.First_Verified,convert(varchar(20),Bp.First_Verified_DT,113) as First_Verified_DT from BDS_PreLoading  as Bp left join Item_user_Master as UM on UM.ID=Bp.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Brand_Master as b on b.BrandID=um.Brandid left join Pharmacy as ph on ph.PharmacyID = Bp.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where  i.Item_Code='" + itemcod + "' and ph.Location_Name='" + location + "'  and Bp.Status='Allot' and Bp.Cart_Allocate_Quantity>0  and Bp.Verified_Status='First Verified'" + Ordby;
           con.View(commt, grid);
       }

       // * Drug Code Begin search Status Pending Allocation * \\
       public void BDS_cartstatusdrugcode_Pending_Allocation(GridView grid, string Drugcode, string location, string Ordby)
       {
           string commt = "Select Bp.Cart_Barcode,'' as Cartridge_Id,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,Bp.Carton_Box_Quantity as Cart_Type,IL.UOM,Bp.Cart_Allocate_Quantity as Pending_Quantity,(Bp.Carton_Box_Quantity-Bp.Cart_Allocate_Quantity) as Alloted_Quantity,'' as Loaded_Quantity,'Pending For VC Allocation' as Verified_Status,Bp.Loaded_By,convert(varchar(20),Bp.LoadedDate,113) as Loaded_Date,Bp.First_Verified,convert(varchar(20),Bp.First_Verified_DT,113) as First_Verified_DT from BDS_PreLoading  as Bp left join Item_user_Master as UM on UM.ID=Bp.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Brand_Master as b on b.BrandID=um.Brandid left join Pharmacy as ph on ph.PharmacyID = Bp.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where  i.Drug_Code like'" + Drugcode + "%' and ph.Location_Name='" + location + "'  and Bp.Status='Allot' and Bp.Cart_Allocate_Quantity>0 and Bp.Verified_Status='First Verified'" + Ordby;
           con.View(commt, grid);
       }

       // * Item Name Instring search Status Pending Allocation * \\
       public void BDS_cartstatusname_Pending_Allocation(GridView grid, string itmname, string location, string Ordby)
       {
           string commt = "Select Bp.Cart_Barcode,'' as Cartridge_Id,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,Bp.Carton_Box_Quantity as Cart_Type,IL.UOM,Bp.Cart_Allocate_Quantity as Pending_Quantity,(Bp.Carton_Box_Quantity-Bp.Cart_Allocate_Quantity) as Alloted_Quantity,'' as Loaded_Quantity,'Pending For VC Allocation' as Verified_Status,Bp.Loaded_By,convert(varchar(20),Bp.LoadedDate,113) as Loaded_Date,Bp.First_Verified,convert(varchar(20),Bp.First_Verified_DT,113) as First_Verified_DT from BDS_PreLoading  as Bp left join Item_user_Master as UM on UM.ID=Bp.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Brand_Master as b on b.BrandID=um.Brandid left join Pharmacy as ph on ph.PharmacyID = Bp.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where  i.Item_Name like'%" + itmname + "%' and ph.Location_Name='" + location + "' and  Bp.Status='Allot' and Bp.Cart_Allocate_Quantity>0 and Bp.Verified_Status='First Verified'" + Ordby;
           con.View(commt, grid);
       }

       // * Brand Name Begin Search Status Pending Allocation * \\
       public void BDS_cartstatusbrand_Pending_Allocation(GridView grid, string brandname, string location, string Ordby)
       {
           string commt = "Select Bp.Cart_Barcode,'' as Cartridge_Id,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,Bp.Carton_Box_Quantity as Cart_Type,IL.UOM,Bp.Cart_Allocate_Quantity as Pending_Quantity,(Bp.Carton_Box_Quantity-Bp.Cart_Allocate_Quantity) as Alloted_Quantity,'' as Loaded_Quantity,'Pending For VC Allocation' as Verified_Status,Bp.Loaded_By,convert(varchar(20),Bp.LoadedDate,113) as Loaded_Date,Bp.First_Verified,convert(varchar(20),Bp.First_Verified_DT,113) as First_Verified_DT from BDS_PreLoading  as Bp left join Item_user_Master as UM on UM.ID=Bp.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Brand_Master as b on b.BrandID=um.Brandid left join Pharmacy as ph on ph.PharmacyID = Bp.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where  b.Brandname like '" + brandname + "%' and ph.Location_Name='" + location + "' and  Bp.Status='Allot' and Bp.Cart_Allocate_Quantity>0 and Bp.Verified_Status='First Verified'" + Ordby;
           con.View(commt, grid);
       }

       // * NO Status Status Pending Allocation * \\
       public void BDS_cartstatus_No_Filter_Pending_Allocation(GridView grid, string location, string Ordby)
       {
           string commt = "Select Bp.Cart_Barcode,'' as Cartridge_Id,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,UM.Pack_Size,Bp.Carton_Box_Quantity as Cart_Type,IL.UOM,Bp.Cart_Allocate_Quantity as Pending_Quantity, (Bp.Carton_Box_Quantity-Bp.Cart_Allocate_Quantity) as Alloted_Quantity,'' as Loaded_Quantity,'Pending For VC Allocation' as Verified_Status,Bp.Loaded_By,convert(varchar(20),Bp.LoadedDate,113) as Loaded_Date,Bp.First_Verified,convert(varchar(20),Bp.First_Verified_DT,113) as First_Verified_DT from BDS_PreLoading  as Bp left join Item_user_Master as UM on UM.ID=Bp.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Brand_Master as b on b.BrandID=um.Brandid left join Pharmacy as ph on ph.PharmacyID = Bp.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where ph.Location_Name='" + location + "' and Bp.Status='Allot' and Bp.Cart_Allocate_Quantity>0 and Bp.Verified_Status='First Verified'" + Ordby;
           con.View(commt, grid);
       }
    }
}
