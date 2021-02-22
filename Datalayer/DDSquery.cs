using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datalayer;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls;

namespace Datalayer
{
   public class DDSquery
    {
       DB_Connection con = new DB_Connection();
       SqlCommand cmd = new SqlCommand();
       
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
       
       // * DDS Querys  * \\ 

       // * DDS Insert Procedure * \\
       public int DDSinsert(string pharmid, string DDSNO, string Userid, string Description,string autoactivation)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "DDS_Master_insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@pharlocation", SqlDbType.VarChar, 50).Value = pharmid.ToString();
           cmd.Parameters.Add("@DDSNO", SqlDbType.VarChar, 20).Value = DDSNO.ToString();
           cmd.Parameters.Add("@Createdby", SqlDbType.VarChar, 20).Value = Userid.ToString();
           cmd.Parameters.Add("@Description ", SqlDbType.VarChar, 255).Value = Description.ToString();
           cmd.Parameters.Add("@autoactivation ", SqlDbType.VarChar, 10).Value = autoactivation.ToString();          
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int rtnval = (int)par.Value;
           return rtnval;
       }

       // * DDS Update Query * \\
       public int DDSupdate(string pharmid, string DDSNO, string Userid, string Description,string autoactivation)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "DDS_Master_update";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@pharlocation", SqlDbType.VarChar, 50).Value = pharmid.ToString();
           cmd.Parameters.Add("@DDSNO", SqlDbType.VarChar, 20).Value = DDSNO.ToString();
           cmd.Parameters.Add("@updatedby", SqlDbType.VarChar, 20).Value = Userid.ToString();
           cmd.Parameters.Add("@Description ", SqlDbType.VarChar, 255).Value = Description.ToString();
           cmd.Parameters.Add("@autoactivation ", SqlDbType.VarChar, 10).Value = autoactivation.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int rtnval = (int)par.Value;
           return rtnval;
       }
       // * CELL Insert Procedure * \\
       public int Cellinsert(string cellno,string ddsno,string status)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Cell_insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Cellno", SqlDbType.VarChar, 3).Value = cellno;
           cmd.Parameters.Add("@DDSName", SqlDbType.VarChar, 20).Value = ddsno.ToString();
           cmd.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = status.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int rtnval = (int)par.Value;
           return rtnval;
       }

       // * Cell No Reader *\\

       //public SqlDataReader cellreader(string ddsname, string prefix)
       //{
       //    string cmd = "select min(CONVERT(int,SUBSTRING(Cell_No,2,3))),max(CONVERT(int,SUBSTRING(Cell_No,2,3))) from Cell where DDS_Name='"+ddsname+"' and Cell_No like'"+prefix+"%'";
       //    SqlDataReader dr = con.GetDataBase(cmd);
       //    return dr;
       //}

       // * Cell grid Display * \\
       public void cellgrid(GridView grid,string ddsname,string prefix)
       {
           string commt="select DDS_Name,Cell_No  from Cell where DDS_Name='"+ddsname+"' and Cell_No like'"+prefix+"%' order by id";
           con.View(commt,grid);
       }

       // Cel Number Delete * \\

       public void celldelete(string ddsname, string prefix)
       {
           string cmd = "delete From Cell where DDS_Name='" + ddsname + "' and Cell_No like '" + prefix + "%'";
           con.SetDataBase(cmd);
       }

       // * DDS grid Display * \\
       public void ddsgrid(GridView grid,string location)
       {
           string commt = "Select d.DDS_Name,p.Location_Name,d.AutoActivation,d.Created_by,convert(varchar(20),d.[Created_Date],113) as Created_Date ,d.Updated_by,convert(varchar(20),d.[Updated_Date] ,113) as Updated_Date,d.Status from dds as d left join Pharmacy as p on p.PharmacyID=d.PharmacyID where p.Location_Name='" + location + "' order by d.Updated_Date desc ";
           con.View(commt, grid);
       }

       // * DDS Active / Inactive Query * \\
       public void ddsenabled(string ddsno,string updateduser,string status)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "DDS_Active_Inactive";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@DDSName", SqlDbType.VarChar, 20).Value = ddsno.ToString();
           cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = status.ToString();
           cmd.Parameters.Add("@updatedby", SqlDbType.VarChar, 20).Value = updateduser.ToString();
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();          
       }

       // * ============== *\\

       // * Host Name * \\
       public int Hostnameinsert(string pharmid, string Hostname, string Description, string Userid)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Workstation_insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@pharlocation", SqlDbType.VarChar, 50).Value = pharmid.ToString();
           cmd.Parameters.Add("@Hostname", SqlDbType.VarChar, 50).Value = Hostname.ToString();
           cmd.Parameters.Add("@Description ", SqlDbType.VarChar, 255).Value = Description.ToString();
           cmd.Parameters.Add("@Createdby", SqlDbType.VarChar, 20).Value = Userid.ToString();           
           SqlParameter ret = new SqlParameter();
           ret.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(ret);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int retvalue = (int)ret.Value;
           return retvalue;
       }

       public int Hostnameupdate(int hostid,string pharmid, string Hostname, string Description, string Userid)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Workstation_update";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@hostid ", SqlDbType.Int).Value = hostid;
           cmd.Parameters.Add("@pharlocation", SqlDbType.VarChar, 50).Value = pharmid.ToString();
           cmd.Parameters.Add("@Hostname", SqlDbType.VarChar, 50).Value = Hostname.ToString();
           cmd.Parameters.Add("@Description ", SqlDbType.VarChar, 255).Value = Description.ToString();
           cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = Userid.ToString();
           SqlParameter ret = new SqlParameter();
           ret.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(ret);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int retvalue = (int)ret.Value;
           return retvalue;
       }

       // * Host Name Display With Grid * \\
       public void gridhost(GridView grid)
       {
           string commt = "Select w.hostid,p.Location_Name,w.[Host_Name],w.Created_by,convert(varchar(20),w.Created_Date,113) as Created_Date ,w.Updated_by,convert(varchar(20),w.Updated_Date,113) as Updated_Date,w.Status from workstation as w left join Pharmacy as p on p.PharmacyID= w.pharmacyid order by w.Updated_Date desc";
           con.View(commt, grid);
       }

       public void gridhostsearch(GridView grid,string hstname)
       {
           string commt = "Select w.hostid,p.Location_Name,w.[Host_Name],w.Created_by,convert(varchar(20),w.Created_Date,113) as Created_Date ,w.Updated_by,convert(varchar(20),w.Updated_Date,113) as Updated_Date,w.Status from workstation as w left join Pharmacy as p on p.PharmacyID= w.pharmacyid where [Host_Name] like '%"+ hstname+"%' order by w.Updated_Date desc";
           con.View(commt, grid);
       }

       public void hostenabled(int hostidenable,string updateduser)
       {
           string cmd = "Update Workstation set status='Active',updated_by='" + updateduser + "',updated_date='" + System.DateTime.Now + "' where Hostid='" + hostidenable + "'";
           con.SetDataBase(cmd);
           commonactinact(Convert.ToString(hostidenable), "Work Station", "Active", updateduser);
       }

       public void hostdisabled(int hostiddisable, string updateduser)
       {
           string cmd = "Update Workstation set status='Inactive',updated_by ='" + updateduser + "',updated_date='" + System.DateTime.Now + "' where Hostid='" + hostiddisable + "'";
           con.SetDataBase(cmd);
           commonactinact(Convert.ToString(hostiddisable), "Work Station", "Inactive", updateduser);
       }           
 

       // * DRUG INVENTORY * \\

       // * DRug Inventory Grid Display With One DDS * \\
       public void inventorygrid(GridView grid, string ddsname, string location, string itemcode, string packtype, string packsize,string brandmame)
       {
           string commt = "SELECT  p.DDS_Name,p.Cell_Id,p.Cartridge_Id,CONVERT(varchar,p.Expiry_Date,103)as Expiry_Date,p.Batch_No,p.Aval_Quantity,p.Pre_Allocated,(p.Aval_Quantity-p.Pre_Allocated) as computedbal,pm.PackType,UM.Pack_Size,IL.UOM,UM.Max_Alert_Qty_DDS from Cartridge_Loading  as p  left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on  i.MasterID=UM.MasterID  left join Cartridge_Master as c on  c.Cartridge_Id=p.Cartridge_Id left join Pharmacy as ph on  ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Brand_Master as b on b.BrandID=p.Brandid left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.DDS_Name='" + ddsname + "' and p.Inventory_Status='2' and ph.Location_Name='" + location + "' and p.Activation_Status='5'and i.Item_Code='" + itemcode + "' and pm.PackType='" + packtype + "'  and UM.Pack_Size='" + packsize + "' and b.Brandname='" + brandmame + "'";
           con.View(commt, grid);
       }

       // * DRug Inventory Grid Display With ALL DDS * \\
       public void inventorygridall(GridView grid, string location, string itemcode, string packtype, string packsize,string brandname)
       {
           string commt = "SELECT  p.DDS_Name,p.Cell_Id,p.Cartridge_Id,CONVERT(varchar,p.Expiry_Date,103)as Expiry_Date,p.Batch_No,p.Aval_Quantity,p.Pre_Allocated,(p.Aval_Quantity-p.Pre_Allocated) as computedbal,pm.PackType,UM.Pack_Size,IL.UOM,UM.Max_Alert_Qty_DDS from Cartridge_Loading  as p  left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on  i.MasterID=UM.MasterID left join Cartridge_Master as c on  c.Cartridge_Id=p.Cartridge_Id left join Pharmacy as ph on  ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Brand_Master as b on b.BrandID=p.Brandid left join Packtype_Master as pm on pm.ID=UM.PacktypeID  where p.Inventory_Status='2' and ph.Location_Name='" + location + "' and p.Activation_Status='5'and i.Item_Code='" + itemcode + "' and pm.PackType='" + packtype + "'  and UM.Pack_Size='" + packsize + "' and b.Brandname='" + brandname + "'";
           con.View(commt, grid);
       }
    

    // * PROCESSING MASTER * \\

    // * Processing Master Insert * \\
     public int Processinginsert(string location,string packtype,int cartqty,string ddsname,int bagpercontainer,string createdby)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Processing_Master_Insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Pharmacylocation", SqlDbType.VarChar, 50).Value = location.ToString();
           cmd.Parameters.Add("@Packtype", SqlDbType.VarChar, 10).Value = packtype.ToString();
           cmd.Parameters.Add("@Maxcartqty", SqlDbType.Int).Value = cartqty;         
           cmd.Parameters.Add("@DDSname ", SqlDbType.VarChar, 20).Value = ddsname.ToString();
           cmd.Parameters.Add("@Bagpercontainer", SqlDbType.Int).Value = bagpercontainer;
           cmd.Parameters.Add("@Createdby", SqlDbType.VarChar, 20).Value = createdby.ToString();
           SqlParameter ret = new SqlParameter();
           ret.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(ret);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int retvalue = (int)ret.Value;
           return retvalue;
       }

     // * Processing Master Update * \\
     public int Processingupdate(int Procesid,string location, string packtype, int cartqty, string ddsname, int bagpercontainer, string createdby)
     {
         SqlConnection conn = con.getstring();
         conn.Close();
         cmd.Parameters.Clear();
         cmd.Connection = conn;
         cmd.CommandText = "Processing_Master_Update";
         cmd.CommandType = CommandType.StoredProcedure;
         cmd.Parameters.Add("@Processid", SqlDbType.Int).Value = Procesid;
         cmd.Parameters.Add("@Pharmacylocation", SqlDbType.VarChar, 50).Value = location.ToString();
         cmd.Parameters.Add("@Packtype", SqlDbType.VarChar, 10).Value = packtype.ToString();
         cmd.Parameters.Add("@Maxcartqty", SqlDbType.Int).Value = cartqty;
         cmd.Parameters.Add("@DDSname ", SqlDbType.VarChar, 20).Value = ddsname.ToString();
         cmd.Parameters.Add("@Bagpercontainer", SqlDbType.Int).Value = bagpercontainer;
         cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = createdby.ToString();
         SqlParameter ret = new SqlParameter();
         ret.Direction = ParameterDirection.ReturnValue;
         cmd.Parameters.Add(ret);
         conn.Open();
         cmd.ExecuteNonQuery();
         conn.Close();
         int retvalue = (int)ret.Value;
         return retvalue;
     }
     // * Processing Master grid Display * \\
     public void processinggrid(GridView grid,string location)
     {
         string commt = "select p.ID,Ph.location_name,pt.Packtype,p.Max_cart_Qty,p.DDS_Name,p.Bagper_Container,P.Created_by,CONVERT(varchar(20),p.created_date,113)as created_date,p.Updated_by,CONVERT(varchar(20),p.Updated_date,113)as Updated_date from Processing_Master as p left join Pharmacy as ph on ph.PharmacyID = p.PharmacyID left join Packtype_Master as pt on pt.ID=p.PacktypeID where Ph.location_name='" + location + "' order by p.Updated_date desc ";
         con.View(commt, grid);
     }
   }    
}
