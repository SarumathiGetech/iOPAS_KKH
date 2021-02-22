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
   public class Drug
    {
       DB_Connection con = new DB_Connection();
       SqlCommand cmd = new SqlCommand();

       // * DRUG MASTER * \\
       // * Pharmacy Location Reader For Drug master * \\
       public void pharmlocation(GridView grid)
       {
           string commt = "Select Location_Name from pharmacy where Status='Active'";
           con.View(commt, grid);
       }      
 

       // * Item User Master Insert Procedure * \\

       public int usermasterinsert(string itemcode, string pharmid, string uom, string brand, string packtype, string packsize,
            decimal L1, decimal L2, decimal L3, int maxcart, int maxdds, int smallbag, int medium, int bigbag, string status, string createdby, int maxbox,
           bool HorizantalRip, bool VerticalRip,string Box_Pallet, int CartType, decimal CartLength, decimal CartHeight, decimal CartWidth, decimal X_Offset, decimal Y_Offset, decimal Z_Offset, decimal X_Pitch,
           decimal Y_Pitch, decimal Z_Pitch, int No_Of_Layer, int Rows, int Columns, bool Divider, bool Interleaf, bool BotRotationFlag,string Activestatus,
           int Max_Box_Bot_BDS)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Item_usermaster_insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Itemcode", SqlDbType.VarChar, 20).Value = itemcode.ToString();
           cmd.Parameters.Add("@pharmacyname", SqlDbType.VarChar, 50).Value = pharmid.ToString();
           cmd.Parameters.Add("@Uom", SqlDbType.VarChar, 20).Value = uom.ToString();
           cmd.Parameters.Add("@Brand", SqlDbType.VarChar, 100).Value = brand.ToString();
           cmd.Parameters.Add("@Packtype", SqlDbType.VarChar, 10).Value = packtype.ToString();
           cmd.Parameters.Add("@Packsize", SqlDbType.VarChar, 10).Value = packsize.ToString();
           cmd.Parameters.Add("@L1 ", SqlDbType.Decimal).Value = L1;
           cmd.Parameters.Add("@L2", SqlDbType.Decimal).Value = L2;
           cmd.Parameters.Add("@L3", SqlDbType.Decimal).Value = L3;

           cmd.Parameters.Add("@Box_Pallet", SqlDbType.VarChar, 10).Value = Box_Pallet.ToString();
           cmd.Parameters.Add("@Packsizecart", SqlDbType.Int).Value = maxcart;
           cmd.Parameters.Add("@Packsizedds", SqlDbType.Int).Value = maxdds;
           cmd.Parameters.Add("@Max_Box_Bottle_BDS", SqlDbType.Int).Value = Max_Box_Bot_BDS;          

           cmd.Parameters.Add("@Smallbag", SqlDbType.Int).Value = smallbag;
           cmd.Parameters.Add("@Mediumbag", SqlDbType.Int).Value = medium;
           cmd.Parameters.Add("@Bigbag", SqlDbType.Int).Value = bigbag;
           cmd.Parameters.Add("@Maxboxcontainer", SqlDbType.Int).Value = maxbox;

           cmd.Parameters.Add("@Horizontal_Rip", SqlDbType.Bit).Value = HorizantalRip;
           cmd.Parameters.Add("@Vertical_Rip", SqlDbType.Bit).Value = VerticalRip;
           cmd.Parameters.Add("@Cart_Type", SqlDbType.Decimal).Value = CartType;
           cmd.Parameters.Add("@Cart_Length", SqlDbType.Decimal).Value = CartLength;
           cmd.Parameters.Add("@Cart_Height", SqlDbType.Decimal).Value = CartHeight;
           cmd.Parameters.Add("@Cart_Width", SqlDbType.Decimal).Value = CartWidth;
           cmd.Parameters.Add("@Cart_X_Offset", SqlDbType.Decimal).Value = X_Offset;
           cmd.Parameters.Add("@Cart_Y_Offset", SqlDbType.Decimal).Value = Y_Offset;
           cmd.Parameters.Add("@Cart_Z_Offset", SqlDbType.Decimal).Value = Z_Offset;
           cmd.Parameters.Add("@Cart_X_Pitch", SqlDbType.Decimal).Value = X_Pitch;
           cmd.Parameters.Add("@Cart_Y_Pitch", SqlDbType.Decimal).Value = Y_Pitch;
           cmd.Parameters.Add("@Cart_Z_Pitch", SqlDbType.Decimal).Value = Z_Pitch;
           cmd.Parameters.Add("@Cart_No_Of_Layer", SqlDbType.Int).Value = No_Of_Layer;
           cmd.Parameters.Add("@Cart_Rows", SqlDbType.Int).Value = Rows;
           cmd.Parameters.Add("@Cart_Columns", SqlDbType.Int).Value = Columns;
           cmd.Parameters.Add("@Divider", SqlDbType.Bit).Value = Divider;
           cmd.Parameters.Add("@Interleaf", SqlDbType.Bit).Value = Interleaf;
           cmd.Parameters.Add("@BotRotationFlag", SqlDbType.Bit).Value = BotRotationFlag;
           cmd.Parameters.Add("@Createdby", SqlDbType.VarChar, 20).Value = createdby.ToString();
           cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = status.ToString();
           cmd.Parameters.Add("@Active_Status", SqlDbType.VarChar, 10).Value = Activestatus.ToString();

           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int Rtnval = (int)par.Value;
           return Rtnval;
       }


              // * Item User Master Insert Procedure * \\

       public int User_Master_Carton_Insert(int IUMID, string Box_Pallet, int CartType, decimal CartLength, decimal CartHeight, decimal CartWidth, decimal X_Offset, decimal Y_Offset, decimal Z_Offset, decimal X_Pitch,
           decimal Y_Pitch, decimal Z_Pitch, int No_Of_Layer, int Rows, int Columns, bool Divider, bool Interleaf, string createdby,string Activestatus)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Item_Carton_Box_Para_Insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@IUMID", SqlDbType.Int).Value = IUMID;
           cmd.Parameters.Add("@Box_Pallet", SqlDbType.VarChar, 10).Value = Box_Pallet.ToString();
           cmd.Parameters.Add("@Cart_Type", SqlDbType.Decimal).Value = CartType;
           cmd.Parameters.Add("@Cart_Length", SqlDbType.Decimal).Value = CartLength ;
           cmd.Parameters.Add("@Cart_Height", SqlDbType.Decimal).Value = CartHeight;
           cmd.Parameters.Add("@Cart_Width", SqlDbType.Decimal).Value = CartWidth;
           cmd.Parameters.Add("@Cart_X_Offset", SqlDbType.Decimal).Value = X_Offset;
           cmd.Parameters.Add("@Cart_Y_Offset", SqlDbType.Decimal).Value = Y_Offset;
           cmd.Parameters.Add("@Cart_Z_Offset", SqlDbType.Decimal).Value = Z_Offset;
           cmd.Parameters.Add("@Cart_X_Pitch", SqlDbType.Decimal).Value = X_Pitch;
           cmd.Parameters.Add("@Cart_Y_Pitch", SqlDbType.Decimal).Value = Y_Pitch;
           cmd.Parameters.Add("@Cart_Z_Pitch", SqlDbType.Decimal).Value = Z_Pitch;
           cmd.Parameters.Add("@Cart_No_Of_Layer", SqlDbType.Int).Value = No_Of_Layer;
           cmd.Parameters.Add("@Cart_Rows", SqlDbType.Int).Value = Rows;
           cmd.Parameters.Add("@Cart_Columns", SqlDbType.Int).Value = Columns;
           cmd.Parameters.Add("@Divider", SqlDbType.Bit).Value = Divider ;
           cmd.Parameters.Add("@Interleaf", SqlDbType.Bit).Value = Interleaf;
           //cmd.Parameters.Add("@BotRotationFlag", SqlDbType.Bit).Value = BotRotationFlag;
           cmd.Parameters.Add("@Createdby", SqlDbType.VarChar, 20).Value = createdby.ToString();
           cmd.Parameters.Add("@Active_Status", SqlDbType.VarChar, 10).Value = Activestatus.ToString();           
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int Rtnval = (int)par.Value;
           return Rtnval;
       }



       // * Item User Master Update Procedure * \\

       public int usermasterupdate(int id,string itemcode, string pharmid, string uom, string brand, string packtype, string packsize,
             decimal L1, decimal L2, decimal L3, int maxcart, int maxdds, int smallbag, int medium, int bigbag, string status, string updatedby, int maxbox,
           bool HorizantalRip, bool VerticalRip, string Box_Pallet,  int CartType, decimal CartLength, decimal CartHeight, decimal CartWidth, decimal X_Offset, decimal Y_Offset, decimal Z_Offset, decimal X_Pitch,
           decimal Y_Pitch, decimal Z_Pitch, int No_Of_Layer, int Rows, int Columns, bool Divider, bool Interleaf, bool BotRotationFlag, int OlDCartType, string Activestatus,
           int Max_Box_Bot_BDS)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Item_usermaster_update";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
           cmd.Parameters.Add("@Itemcode", SqlDbType.VarChar, 20).Value = itemcode.ToString();
           cmd.Parameters.Add("@pharmacyname", SqlDbType.VarChar, 50).Value = pharmid.ToString();
           cmd.Parameters.Add("@Uom", SqlDbType.VarChar, 20).Value = uom.ToString();
           cmd.Parameters.Add("@Brand", SqlDbType.VarChar, 100).Value = brand.ToString();
           cmd.Parameters.Add("@Packtype", SqlDbType.VarChar, 10).Value = packtype.ToString();
           cmd.Parameters.Add("@Packsize", SqlDbType.VarChar, 10).Value = packsize.ToString();
           cmd.Parameters.Add("@L1 ", SqlDbType.Decimal).Value = L1;
           cmd.Parameters.Add("@L2", SqlDbType.Decimal).Value = L2;
           cmd.Parameters.Add("@L3", SqlDbType.Decimal).Value = L3;

           cmd.Parameters.Add("@Max_Box_Bottle_BDS", SqlDbType.Int).Value = Max_Box_Bot_BDS;          
           cmd.Parameters.Add("@Packsizecart", SqlDbType.Int).Value = maxcart;
           cmd.Parameters.Add("@Packsizedds", SqlDbType.Int).Value = maxdds;
           cmd.Parameters.Add("@Smallbag", SqlDbType.Int).Value = smallbag;
           cmd.Parameters.Add("@Mediumbag", SqlDbType.Int).Value = medium;
           cmd.Parameters.Add("@Bigbag", SqlDbType.Int).Value = bigbag;
           cmd.Parameters.Add("@Maxboxcontainer", SqlDbType.Int).Value = maxbox;
           cmd.Parameters.Add("@Horizontal_Rip", SqlDbType.Bit).Value = HorizantalRip;
           cmd.Parameters.Add("@Vertical_Rip", SqlDbType.Bit).Value = VerticalRip;

           cmd.Parameters.Add("@Box_Pallet", SqlDbType.VarChar, 10).Value = Box_Pallet.ToString();           
           cmd.Parameters.Add("@Cart_Type", SqlDbType.Int).Value = CartType;
           cmd.Parameters.Add("@OLD_Cart_Type", SqlDbType.Int).Value = OlDCartType;
           cmd.Parameters.Add("@Cart_Length", SqlDbType.Decimal).Value = CartLength;
           cmd.Parameters.Add("@Cart_Height", SqlDbType.Decimal).Value = CartHeight;
           cmd.Parameters.Add("@Cart_Width", SqlDbType.Decimal).Value = CartWidth;
           cmd.Parameters.Add("@Cart_X_Offset", SqlDbType.Decimal).Value = X_Offset;
           cmd.Parameters.Add("@Cart_Y_Offset", SqlDbType.Decimal).Value = Y_Offset;
           cmd.Parameters.Add("@Cart_Z_Offset", SqlDbType.Decimal).Value = Z_Offset;
           cmd.Parameters.Add("@Cart_X_Pitch", SqlDbType.Decimal).Value = X_Pitch;
           cmd.Parameters.Add("@Cart_Y_Pitch", SqlDbType.Decimal).Value = Y_Pitch;
           cmd.Parameters.Add("@Cart_Z_Pitch", SqlDbType.Decimal).Value = Z_Pitch;
           cmd.Parameters.Add("@Cart_No_Of_Layer", SqlDbType.Int).Value = No_Of_Layer;
           cmd.Parameters.Add("@Cart_Rows", SqlDbType.Int).Value = Rows;
           cmd.Parameters.Add("@Cart_Columns", SqlDbType.Int).Value = Columns;
           cmd.Parameters.Add("@Divider", SqlDbType.Bit).Value = Divider;
           cmd.Parameters.Add("@Interleaf", SqlDbType.Bit).Value = Interleaf;
           cmd.Parameters.Add("@BotRotationFlag", SqlDbType.Bit).Value = BotRotationFlag;
           cmd.Parameters.Add("Updatedby", SqlDbType.VarChar, 20).Value = updatedby.ToString();
           cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = status.ToString();
           cmd.Parameters.Add("@Active_Status", SqlDbType.VarChar, 10).Value = Activestatus.ToString(); 
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int Rtnval = (int)par.Value;
           return Rtnval;
       }

       // * Item User Master Carton Box Parameter Update Procedure * \\

       public int User_Master_Carton_Update(int id, string pharmid,string Box_Pallet, int CartType, decimal CartLength, decimal CartHeight, decimal CartWidth, decimal X_Offset,
                decimal Y_Offset, decimal Z_Offset, decimal X_Pitch,decimal Y_Pitch, decimal Z_Pitch, int No_Of_Layer, int Rows, int Columns, bool Divider, bool Interleaf,
                 string updatedby, int OlDCartType, string Activestatus)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Item_Carton_Box_Para_Update";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@IUMID", SqlDbType.Int).Value = id;           
           cmd.Parameters.Add("@Box_Pallet", SqlDbType.VarChar, 10).Value = Box_Pallet.ToString();
           cmd.Parameters.Add("@Cart_Type", SqlDbType.Int).Value = CartType;
           cmd.Parameters.Add("@OLD_Cart_Type", SqlDbType.Int).Value = OlDCartType;
           cmd.Parameters.Add("@Cart_Length", SqlDbType.Decimal).Value = CartLength;
           cmd.Parameters.Add("@Cart_Height", SqlDbType.Decimal).Value = CartHeight;
           cmd.Parameters.Add("@Cart_Width", SqlDbType.Decimal).Value = CartWidth;
           cmd.Parameters.Add("@Cart_X_Offset", SqlDbType.Decimal).Value = X_Offset;
           cmd.Parameters.Add("@Cart_Y_Offset", SqlDbType.Decimal).Value = Y_Offset;
           cmd.Parameters.Add("@Cart_Z_Offset", SqlDbType.Decimal).Value = Z_Offset;
           cmd.Parameters.Add("@Cart_X_Pitch", SqlDbType.Decimal).Value = X_Pitch;
           cmd.Parameters.Add("@Cart_Y_Pitch", SqlDbType.Decimal).Value = Y_Pitch;
           cmd.Parameters.Add("@Cart_Z_Pitch", SqlDbType.Decimal).Value = Z_Pitch;
           cmd.Parameters.Add("@Cart_No_Of_Layer", SqlDbType.Int).Value = No_Of_Layer;
           cmd.Parameters.Add("@Cart_Rows", SqlDbType.Int).Value = Rows;
           cmd.Parameters.Add("@Cart_Columns", SqlDbType.Int).Value = Columns;
           cmd.Parameters.Add("@Divider", SqlDbType.Bit).Value = Divider;
           cmd.Parameters.Add("@Interleaf", SqlDbType.Bit).Value = Interleaf;
           //cmd.Parameters.Add("@BotRotationFlag", SqlDbType.Bit).Value = BotRotationFlag;
           cmd.Parameters.Add("Updatedby", SqlDbType.VarChar, 20).Value = updatedby.ToString();
           cmd.Parameters.Add("@Active_Status", SqlDbType.VarChar, 10).Value = Activestatus.ToString(); 

           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int Rtnval = (int)par.Value;
           return Rtnval;
       }

       // * Item user Master Grid Display For General *\\
       public void usergridadd(GridView grid, string itemcode)
       {
           string commt = "Select U.ID,pm.PackType,u.Pack_size,b.Brandname,p.location_name,IL.UOM,u.created_by,CONVERT(varchar(20), U.Created_Date,113) as Created_Date ,U.Updated_by,CONVERT(varchar(20), U.Updated_Date,113) as Updated_Date ,u.status from Item_user_Master as u left join  item_master as i on i.MasterID=u.MasterID left join Pharmacy as p on p.PharmacyID=u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and p.PharmacyID=IL.PharmacyID left join Brand_Master as b on b.BrandID=u.Brandid left join Packtype_Master as pm on pm.ID=u.PacktypeID where i.Item_Code='" + itemcode + "' order by p.Location_Name,b.Brandname asc";
           con.View(commt, grid);
       }
       //* Item user Master Default Location First search * \\
       public void usergriddefaultlocation(GridView grid, string itemcode,string location)
       {
          // string commt = "Select U.ID,pm.PackType,u.Pack_size,b.Brandname,p.location_name,IL.UOM,u.created_by,CONVERT(varchar(20), U.Created_Date,113) as Created_Date ,U.Updated_by,CONVERT(varchar(20), U.Updated_Date,113) as Updated_Date ,u.status from Item_user_Master as u left join  item_master as i on i.MasterID=u.MasterID left join Pharmacy as p on p.PharmacyID=u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and p.PharmacyID=IL.PharmacyID left join Brand_Master as b on b.BrandID=u.Brandid left join Packtype_Master as pm on pm.ID=u.PacktypeID where i.Item_Code='"+itemcode+"' and p.Location_Name='"+location+"' union all Select U.ID,pm.PackType,u.Pack_size,b.Brandname,p.location_name,IL.UOM,u.created_by,CONVERT(varchar(20), U.Created_Date,113) as Created_Date ,U.Updated_by,CONVERT(varchar(20), U.Updated_Date,113) as Updated_Date ,u.status from Item_user_Master as u left join  item_master as i on i.MasterID=u.MasterID left join Pharmacy as p on p.PharmacyID=u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and p.PharmacyID=IL.PharmacyID left join Brand_Master as b on b.BrandID=u.Brandid left join Packtype_Master as pm on pm.ID=u.PacktypeID where i.Item_Code='"+itemcode+"' and p.Location_Name<>'"+location+"'";
           string commt = "Select ID,PackType,Pack_size,Brandname,location_name,UOM,created_by, Created_Date ,Updated_by,Updated_Date ,status from (Select top 1000 U.ID,pm.PackType,u.Pack_size,b.Brandname,p.location_name,IL.UOM,u.created_by,CONVERT(varchar(20), U.Created_Date,113) as Created_Date ,U.Updated_by,CONVERT(varchar(20), U.Updated_Date,113) as Updated_Date ,u.status from Item_user_Master as u left join  item_master as i on i.MasterID=u.MasterID left join Pharmacy as p on p.PharmacyID=u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and p.PharmacyID=IL.PharmacyID left join Brand_Master as b on b.BrandID=u.Brandid left join Packtype_Master as pm on pm.ID=u.PacktypeID where i.Item_Code='" + itemcode + "' and p.Location_Name='" + location + "' order by Location_Name,Brandname) as ab union all Select ID,PackType,Pack_size,Brandname,location_name,UOM,created_by, Created_Date ,Updated_by,Updated_Date ,status from(Select top 1000 U.ID,pm.PackType,u.Pack_size,b.Brandname,p.location_name,IL.UOM,u.created_by,CONVERT(varchar(20), U.Created_Date,113) as Created_Date ,U.Updated_by,CONVERT(varchar(20), U.Updated_Date,113) as Updated_Date ,u.status from Item_user_Master as u left join  item_master as i on i.MasterID=u.MasterID left join Pharmacy as p on p.PharmacyID=u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and p.PharmacyID=IL.PharmacyID left join Brand_Master as b on b.BrandID=u.Brandid left join Packtype_Master as pm on pm.ID=u.PacktypeID where i.Item_Code='" + itemcode + "' and p.Location_Name<>'" + location + "' order by Location_Name,Brandname)as ab";
           con.View(commt, grid);
       }

      
       // * Item User Master Location Grid For Edit Mode * \\
       public void locationeditgrid(GridView grid, int id)
       {
           string commt = "Select Pharmacy_Id from Item_user_Master where ID='" + id + "'";
           con.View(commt, grid);
       }
       
       // * BRAND MASTER * \\

       // * BRAND MASTER INSERT PROCEDURE *\\
       public int brandinsert(string brandcode, string brandname, string itemcode, string status, int defaultbrand,string createdby)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Brand_insert";
           cmd.CommandType = CommandType.StoredProcedure;           
           cmd.Parameters.Add("@Brandcode", SqlDbType.VarChar, 20).Value = brandcode.ToString();
           cmd.Parameters.Add("@Brandname", SqlDbType.VarChar, 100).Value = brandname.ToString();
           cmd.Parameters.Add("@Createdby", SqlDbType.VarChar, 20).Value = createdby.ToString();
           cmd.Parameters.Add("@ItemCode", SqlDbType.VarChar, 100).Value = itemcode.ToString();
           cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = status.ToString();
           cmd.Parameters.Add("@Defaultbrand", SqlDbType.Int).Value = defaultbrand;
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int rtnval = (int)par.Value;
           return rtnval;
       }
       public int brandupdate(string brandcode, string brandname,int brandid , string updatedby)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Brand_Update";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Brandcode", SqlDbType.VarChar, 20).Value = brandcode.ToString();
           cmd.Parameters.Add("@Brandname", SqlDbType.VarChar, 100).Value = brandname.ToString();
           cmd.Parameters.Add("@updatedby", SqlDbType.VarChar, 20).Value = updatedby.ToString();
           cmd.Parameters.Add("@Brandid ", SqlDbType.Int).Value = brandid;
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int rtnval = (int)par.Value;
           return rtnval;
       }
      

       // * BRAND ALLOT UPDATE PROCEDURE *\\
       public int brandallotupdate(string itemcode, string brandcode, int defalutbrand, string status,string Updatedby)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Brand_Allot_Update";
           cmd.CommandType = CommandType.StoredProcedure;           
           cmd.Parameters.Add("@itemcode", SqlDbType.VarChar, 20).Value = itemcode.ToString();
           cmd.Parameters.Add("@Brandcode", SqlDbType.VarChar, 20).Value = brandcode.ToString();           
           cmd.Parameters.Add("@Defaultbrand", SqlDbType.Int).Value = defalutbrand;
           cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = status.ToString();
           cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 10).Value = Updatedby.ToString();
           SqlParameter par = new SqlParameter();
           par.Direction = ParameterDirection.ReturnValue;
           cmd.Parameters.Add(par);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
           int rtnval = (int)par.Value;
           return rtnval;
       }

       // * MFRCODE INSERT PROCEDURE * \\
       public void Mfrinsert(string brandcode,string itemcode, string mfrbarcode)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText  = "Mfrcode_insert";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@brandcode", SqlDbType.VarChar, 20).Value = brandcode.ToString();
           cmd.Parameters.Add("@Itemcode", SqlDbType.VarChar, 20).Value = itemcode.ToString();
           cmd.Parameters.Add("@Mfrbarcode", SqlDbType.VarChar, 100).Value = mfrbarcode.ToString();
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
       }
// * MFRBARCODE DELETE WHILE UPDATE * \\
       public void Mfrdelete(string brandcode, string itemcode)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           cmd.Parameters.Clear();
           cmd.Connection = conn;
           cmd.CommandText = "Mfrcode_Delete";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@brandcode", SqlDbType.VarChar, 20).Value = brandcode.ToString();
           cmd.Parameters.Add("@Itemcode", SqlDbType.VarChar, 20).Value = itemcode.ToString();          
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
       }

       // * Brand Master Grid Display *\\
       public void brandgrid(GridView grid, string itemcode, string brandcode)
       {
           //string commt = "select i.Item_Code,b.brandcode,b.brandname,m.mfrbarcode from Brand_Master as b left join Item_Master as i on i.MasterID=b.Masterid left join MFR_Barcode as m on m.Brandid = b.BrandID and m.Masterid=i.MasterID where i.Item_Code='"+itemcode+"' and b.Brandcode='" + brandcode + "'";
           string commt = "select i.Item_Code,b.brandid,b.brandcode,b.brandname,b.Status from Brand_Master as b left join Item_Master as i on i.MasterID=b.Masterid where i.Item_Code='" + itemcode + "' and b.Brandcode='" + brandcode + "'";
           con.View(commt, grid);
       }

       // * Brand Master grid disply search Only Itemcode search *\\
       public void brandgriditecode(GridView grid, string itemcode)
       {
           string commt = "select i.Item_Code,b.brandid,b.brandcode,b.brandname,ba.Status from Brand_Master as b left join brand_Allot as ba on Ba.brandid=b.BrandID left join Item_Master as i on i.MasterID=ba.Masterid where i.Item_Code='" + itemcode + "' order by b.Brandname asc";
           con.View(commt, grid);
       }

       // * Brand Master Grid Display * \\
       public void brandmastergrid(GridView grid, string brandnameser)
       {
           string commt = "select BrandID,Brandcode,Brandname,Created_by,CONVERT(varchar(20),created_date,113) as createddate,Updated_by,CONVERT(varchar(20),updated_date,113) as updateddate from Brand_Master  where Brandname like'%"+brandnameser+"%' order by Brandname asc";
           con.View(commt, grid); 
       }
       // * Brand Master Grid Display with Brand Code * \\
       public void brandmastercodegrid(GridView grid, string brancode)
       {
           string commt = "select BrandID,Brandcode,Brandname,Created_by,CONVERT(varchar(20),created_date,113) as createddate,Updated_by,CONVERT(varchar(20),updated_date,113) as updateddate from Brand_Master  where Brandcode ='" + brancode + "' order by Brandname asc";
           con.View(commt, grid);
       }
    }
}
