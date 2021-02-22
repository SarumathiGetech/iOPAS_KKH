using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Datalayer;
using System.Web.UI.WebControls;
namespace Datalayer
{   
 public class BDSBottle
    {
        DB_Connection con = new DB_Connection();
        SqlCommand cmd = new SqlCommand();
        DateTime Dateresult;
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
           // Datesearch = Convert.ToDateTime(str + "/" + Convert.ToString(Convert.ToInt32(str1)) + "/" + str2 + " 23:59:59.999");
        }

       

        //* NEW Drug Barcode Search * \\
        public void BDSpreloadpopupBarcode(GridView grid, int BarID, string location)
        {
            string Bot = "Bottle";
            string commt = "(Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname,ic.Cart_Type,ic.Box_Pallet from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID left join Item_Carton_Box_Master as ic on ic.IUMID=u.ID where u.status='Yes' and ba.Status='Active' and ic.Active_Status='Yes' and  U.ID='" + BarID + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To>=CONVERT(date,getdate()) and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) " +
                            "union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname,ic.Cart_Type,ic.Box_Pallet  from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID left join Item_Carton_Box_Master as ic on ic.IUMID=u.ID where u.status='Yes' and ba.Status='Active' and ic.Active_Status='Yes' and  U.ID='" + BarID + "' and p.Location_Name='" + location + "' and IL.Store_Effective_Date_To is null and i.Item_Effective_date_To>=CONVERT(date,getdate()) " +
                            "union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname,ic.Cart_Type,ic.Box_Pallet from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID left join Item_Carton_Box_Master as ic on ic.IUMID=u.ID where u.status='Yes' and ba.Status='Active' and ic.Active_Status='Yes' and  U.ID='" + BarID + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) " +
                           "union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname,ic.Cart_Type,ic.Box_Pallet from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID left join Item_Carton_Box_Master as ic on ic.IUMID=u.ID where u.status='Yes' and ba.Status='Active' and ic.Active_Status='Yes' and  U.ID='" + BarID + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To is null and pm.Packtype like '%" + Bot + "%')";
            con.View(commt, grid);
        } 

        // * Pre Loading Popup Search Window grid query like drugcode and itemname instring search* \\
        public void BDSpreloadpopup(GridView grid, string drugcode, string itemname, string location)
        {
            string Bot = "Bottle";
            string commt = "(Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname,ic.Cart_Type,ic.Box_Pallet from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID left join Item_Carton_Box_Master as ic on ic.IUMID=u.ID where u.status='Yes' and ba.Status='Active' and ic.Active_Status='Yes' and  i.Drug_Code like'" + drugcode + "%' and i.Item_Name like'%" + itemname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To>=CONVERT(date,getdate()) and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) " +
                            "union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname,ic.Cart_Type,ic.Box_Pallet from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID left join Item_Carton_Box_Master as ic on ic.IUMID=u.ID  where u.status='Yes' and ba.Status='Active' and ic.Active_Status='Yes' and  i.Drug_Code like'" + drugcode + "%' and i.Item_Name like'%" + itemname + "%' and p.Location_Name='" + location + "' and IL.Store_Effective_Date_To is null and i.Item_Effective_date_To>=CONVERT(date,getdate()) " +
                            "union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname,ic.Cart_Type,ic.Box_Pallet from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID left join Item_Carton_Box_Master as ic on ic.IUMID=u.ID where u.status='Yes' and ba.Status='Active' and ic.Active_Status='Yes' and  i.Drug_Code like'" + drugcode + "%' and i.Item_Name like'%" + itemname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) " +
                            "union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname,ic.Cart_Type,ic.Box_Pallet from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID left join Item_Carton_Box_Master as ic on ic.IUMID=u.ID where u.status='Yes' and ba.Status='Active' and ic.Active_Status='Yes' and  i.Drug_Code like'" + drugcode + "%' and i.Item_Name like'%" + itemname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To is null and pm.Packtype like '%" + Bot + "%')";
            con.View(commt, grid);
        }

        // * Pre loading Popupsearch for only itemcode *\\
        public void BDSpreloadpoppupitemcode(GridView grid, string itemcode, string location)
        {
            string Bot = "Bottle";
            string commt = "(Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name,u.id,pm.Packtype,u.Pack_size,IL.UOM, b.brandname,ic.Cart_Type,ic.Box_Pallet from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID left join Item_Carton_Box_Master as ic on ic.IUMID=u.ID where u.status='Yes' and ba.Status='Active' and ic.Active_Status='Yes' and  i.Item_Code='" + itemcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To>=CONVERT(date,getdate()) and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) " +
                            "union Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name,u.id,pm.Packtype,u.Pack_size,IL.UOM, b.brandname,ic.Cart_Type,ic.Box_Pallet  from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID left join Item_Carton_Box_Master as ic on ic.IUMID=u.ID where u.status='Yes' and ba.Status='Active' and ic.Active_Status='Yes' and  i.Item_Code='" + itemcode + "' and p.Location_Name='" + location + "' and IL.Store_Effective_Date_To is null and i.Item_Effective_date_To>=CONVERT(date,getdate()) " +
                            "union Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name,u.id,pm.Packtype,u.Pack_size,IL.UOM, b.brandname,ic.Cart_Type,ic.Box_Pallet  from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID left join Item_Carton_Box_Master as ic on ic.IUMID=u.ID where u.status='Yes' and ba.Status='Active' and ic.Active_Status='Yes' and  i.Item_Code='" + itemcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) " +
                            "union Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name,u.id,pm.Packtype,u.Pack_size,IL.UOM, b.brandname,ic.Cart_Type,ic.Box_Pallet from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID left join Item_Carton_Box_Master as ic on ic.IUMID=u.ID  where u.status='Yes' and ba.Status='Active' and ic.Active_Status='Yes' and  i.Item_Code='" + itemcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To is null and pm.Packtype  like '%" + Bot + "%')";
            con.View(commt, grid);
        }

        // * Brand Name Search * \\
        public void BDSbrandname(GridView grid, string brandname, string location)
        {
            string Bot = "Bottle";
            string commt = "(Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname,ic.Cart_Type,ic.Box_Pallet from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID left join Item_Carton_Box_Master as ic on ic.IUMID=u.ID where u.status='Yes' and ba.Status='Active' and ic.Active_Status='Yes' and b.brandname like '" + brandname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To>=CONVERT(date,getdate()) and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) " +
                            "union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname,ic.Cart_Type,ic.Box_Pallet  from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID left join Item_Carton_Box_Master as ic on ic.IUMID=u.ID where u.status='Yes' and ba.Status='Active' and ic.Active_Status='Yes' and b.brandname like '" + brandname + "%' and p.Location_Name='" + location + "' and IL.Store_Effective_Date_To is null and i.Item_Effective_date_To>=CONVERT(date,getdate()) " +
                            "union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname,ic.Cart_Type,ic.Box_Pallet  from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID left join Item_Carton_Box_Master as ic on ic.IUMID=u.ID where u.status='Yes' and ba.Status='Active' and ic.Active_Status='Yes' and b.brandname like '" + brandname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) " +
                            "union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname,ic.Cart_Type,ic.Box_Pallet from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID left join Item_Carton_Box_Master as ic on ic.IUMID=u.ID where u.status='Yes' and ba.Status='Active' and ic.Active_Status='Yes' and b.brandname like '" + brandname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To is null and pm.Packtype like '%" + Bot + "%')";
            con.View(commt, grid);
        }

        // * MFR Barcode Search * \\
        public void BDSmfrcode(GridView grid, string mfrcode, string location)
        {
            string Bot = "Bottle";
            string commt = "(Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname,ic.Cart_Type,ic.Box_Pallet from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join MFR_Barcode as m on m.Brandid =b.BrandID and m.Masterid=i.MasterID left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID left join Item_Carton_Box_Master as ic on ic.IUMID=u.ID where u.status='Yes' and ba.Status='Active' and ic.Active_Status='Yes' and m.Mfrbarcode='" + mfrcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To>=CONVERT(date,getdate()) and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) " +
                            "union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname,ic.Cart_Type,ic.Box_Pallet  from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join MFR_Barcode as m on m.Brandid =b.BrandID and m.Masterid=i.MasterID left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID left join Item_Carton_Box_Master as ic on ic.IUMID=u.ID where u.status='Yes' and ba.Status='Active' and ic.Active_Status='Yes' and m.Mfrbarcode='" + mfrcode + "' and p.Location_Name='" + location + "' and IL.Store_Effective_Date_To is null and i.Item_Effective_date_To>=CONVERT(date,getdate()) " +
                            "union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname,ic.Cart_Type,ic.Box_Pallet from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join MFR_Barcode as m on m.Brandid =b.BrandID and m.Masterid=i.MasterID left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID left join Item_Carton_Box_Master as ic on ic.IUMID=u.ID where u.status='Yes' and ba.Status='Active' and ic.Active_Status='Yes' and m.Mfrbarcode='" + mfrcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) " +
                            "union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname,ic.Cart_Type,ic.Box_Pallet  from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID  left join Brand_Master as b on b.BrandID=u.Brandid left join MFR_Barcode as m on m.Brandid =b.BrandID and m.Masterid=i.MasterID left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID left join Item_Carton_Box_Master as ic on ic.IUMID=u.ID where u.status='Yes' and ba.Status='Active' and ic.Active_Status='Yes' and m.Mfrbarcode='" + mfrcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To is null and pm.Packtype like '%" + Bot + "%')";
            con.View(commt, grid);
        }


        // * BDS Loaded Cartridge DIsplay * \\
        public void BDSpreloadgrid(GridView grid, string userid, string location,string OrdBy)
        {
            string commt = "Select top 100 p.Cart_Barcode,p.CartID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,p.Carton_Box_Quantity as Cart_Type,isnull(p.BoxOrPallet,' ') as BoxOrPallet,UM.Pack_Size,IL.UOM,p.Status,p.Loaded_By,convert(varchar(20),p.LoadedDate,113) as Loaded_Dat,p.UpdatedBy,convert(varchar(20),p.UpdatedDate,113) as Upd_Dat,p.Verified_Status,p.Reason from BDS_PreLoading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Brand_Master as b on b.BrandID=UM.Brandid left join Pharmacy as ph on ph.PharmacyID=p.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.Loaded_by='" + userid + "' and ph.Location_Name='" + location + "' " + OrdBy;
            con.View(commt, grid);
        }

   

        // * BDS Name Reader For PreLoading * \\
        //public SqlDataReader Prloadddsnamereader(string location)
        //{
        //    string cmd = "Select d.DDS_Name from dds as d left join Pharmacy as p on p.PharmacyID=d.PharmacyID where p.Location_Name='" + location + "' and d.Status='Active' and d.DDS_Name like 'B%' ";
        //    SqlDataReader dr = con.GetDataBase(cmd);
        //    return dr;
        //}


     /// <summary>
     /// BDS Botlle Preloading Insert
     /// </summary>
     /// <param name="PharmacyLocation">Pharmacy Location</param>
     /// <param name="IumID">Drug Master ID</param>
     /// <param name="Max_Cart_Quantity">Carrtridge Maximum Quantity</param>
     /// <param name="Cart_box_Quantity">Carton Box Quantity</param>
     /// <param name="ExpDate">Drug Expiry Date</param>
     /// <param name="BatchNo">Drug Batch Number</param>
     /// <param name="Loadedby">Logged in user</param>
     /// <param name="barcode">Carton Box Barcode return</param>
     /// <returns></returns>

        public int BDS_Bot_Preloading_Insert(string PharmacyLocation,int IumID,int Max_Cart_Quantity,int Cart_box_Quantity,string ExpDate,string BatchNo,string Loadedby,string BoxOrPallet, out string barcode )
        {
           
            datefun(ExpDate);
            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "BDS_Bottle_PreLoading_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pharmacy_Location", SqlDbType.VarChar, 20).Value = PharmacyLocation.ToString();
            cmd.Parameters.Add("@IUM_ID", SqlDbType.Int).Value = IumID;
            cmd.Parameters.Add("@Max_Cart_Quantity", SqlDbType.Int).Value = Max_Cart_Quantity;
            cmd.Parameters.Add("@Cart_Box_Quantity", SqlDbType.Int).Value = Cart_box_Quantity;
            cmd.Parameters.Add("@Expiry_Date", SqlDbType.Date).Value = Dateresult;
            cmd.Parameters.Add("@BatchNo", SqlDbType.VarChar, 20).Value = BatchNo.ToString();
            cmd.Parameters.Add("@LoadedBy", SqlDbType.VarChar, 20).Value = Loadedby.ToString();
            cmd.Parameters.Add("@BoxOrPallet", SqlDbType.VarChar, 20).Value = BoxOrPallet.ToString();
            cmd.Parameters.Add("@Barcode", SqlDbType.VarChar,20).Direction = ParameterDirection.Output;
            SqlParameter par = new SqlParameter();
            par.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(par);
            conn.Open();
            cmd.ExecuteNonQuery();
            barcode = (string)cmd.Parameters["@Barcode"].Value;
            conn.Close();
            int rtnval = (int)par.Value;
            return rtnval;
        }

     /// <summary>
     /// BDS Bottle Preloading Update
     /// </summary>
     /// <param name="CartID"> long</param>
     /// <param name="ExpDate">dd/mm/yyyy</param>
     /// <param name="BatchNo">Batch No</param>
     /// <param name="barcode">Barcode</param>
     /// <returns></returns>

        public int BDS_Bot_Preloading_Update(Int64 CartID,string ExpDate, string BatchNo,string Updatedby,out string barcode)
        {
            
            datefun(ExpDate);
            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "BDS_Bottle_PreLoading_Update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CartID", SqlDbType.BigInt).Value = CartID;           
            cmd.Parameters.Add("@Expiry_Date", SqlDbType.Date).Value = Dateresult;
            cmd.Parameters.Add("@BatchNo", SqlDbType.VarChar, 20).Value = BatchNo.ToString();
            cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = Updatedby.ToString(); 
            cmd.Parameters.Add("@Barcode", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
            SqlParameter par = new SqlParameter();
            par.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(par);
            conn.Open();
            cmd.ExecuteNonQuery();
            barcode = (string)cmd.Parameters["@Barcode"].Value;
            conn.Close();
            int rtnval = (int)par.Value;
            return rtnval;
        }

     /// <summary>
     /// BDS Bottle Preloading Cancel
     /// </summary>
     /// <param name="CartID"> long</param>
     /// <returns></returns>

        public int BDS_Bot_Preloading_Cancel(Int64 CartID, string Updatedby)
        {
           
            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "BDS_Bottle_PreLoading_Cancel";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CartID", SqlDbType.BigInt).Value = CartID;
            cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = Updatedby.ToString(); 
            SqlParameter par = new SqlParameter();
            par.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(par);
            conn.Open();
            cmd.ExecuteNonQuery();           
            conn.Close();
            int rtnval = (int)par.Value;
            return rtnval;
        }

     /// <summary>
        /// BDS_Bot_Preloading_Cancel
     /// </summary>
     /// <param name="CartBarcode"></param>
     /// <param name="Updatedby"></param>
     /// <returns></returns>


        public int BDS_Bot_Preloading_Force_Cancel(string CartBarcode, string Updatedby)
        {

            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "BDS_Bottle_PreLoading_Force_Cancel";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CartBarcode", SqlDbType.VarChar, 20).Value = CartBarcode.ToString();
            cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar, 20).Value = Updatedby.ToString();
            SqlParameter par = new SqlParameter();
            par.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(par);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            int rtnval = (int)par.Value;
            return rtnval;
        }


     /// <summary>
     /// BDS Cartridge Unloading Validate
     /// </summary>
     /// <param name="PharmacyLocation">Pharmacy Location</param>
     /// <param name="V_Cartridge_No">Virtual Cartridge Number</param>
     /// <returns></returns>
        public int BDS_Bot_Cartridge_Unloading_Validate(string PharmacyLocation,string V_Cartridge_No)
        {
            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "BDS_Cartridge_Unloading_Validate";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pharmacy_Location", SqlDbType.VarChar, 50).Value = PharmacyLocation.ToString();
            cmd.Parameters.Add("@V_Cartridge_No", SqlDbType.VarChar, 10).Value = V_Cartridge_No.ToString();           
            SqlParameter par = new SqlParameter();
            par.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(par);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            int rtnval = (int)par.Value;
            return rtnval;
        }

     /// <summary>
     /// BDS Cartridge Unloading
     /// </summary>
     /// <param name="PharmacyLocation"> Pharmacy Location</param>
     /// <param name="V_Cartridge_No">Virtual Cartridge Number</param>
     /// <param name="Unloadedby">Logged In UserID</param>
     /// <returns></returns>


        public int BDS_Bot_Cartridge_Unloading_Insert(string PharmacyLocation, string V_Cartridge_No,string Unloadedby)
        {
            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "BDS_Cartridge_Unloading_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pharmacy_Location", SqlDbType.VarChar, 50).Value = PharmacyLocation.ToString();
            cmd.Parameters.Add("@V_Cartridge_No", SqlDbType.VarChar, 10).Value = V_Cartridge_No.ToString();
            cmd.Parameters.Add("@UnloadedBy", SqlDbType.VarChar, 20).Value = Unloadedby.ToString();
            SqlParameter par = new SqlParameter();
            par.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(par);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            int rtnval = (int)par.Value;
            return rtnval;
        }   

        // * BDS Unloading Grid Display * \\
        public void BDSUnloadingGrid(GridView grid, string UnloadedBy,string OrdBy)
        {
            string commt = "select top(100) cl.DDS_Name,cl.Cartridge_Id,im.Item_Code,im.Item_Name,bm.Brandname,pm.PackType,iu.Pack_Size,il.UOM,Bu.UnloadedBy, convert(varchar(20),bu.UnloadedDate,113) as UnloadedDate,Bu.Status  from BDS_Unloading as Bu left join Cartridge_Loading as cl on cl.Loading_Id=Bu.LoadingID left join Item_user_Master as iu on iu.ID=cl.IUM_ID left join Item_Master as im on im.MasterID=iu.MasterID left join Brand_Master as bm on bm.BrandID=iu.Brandid left join Packtype_Master as pm on pm.ID=iu.PackTypeID left join Item_Location as il on il.MasterID=im.MasterID and il.ID=iu.UOM  where Bu.UnloadedBy='"+UnloadedBy+"'   "+ OrdBy;
            con.View(commt, grid);
        }

        // * BDS Unloading Grid Display * \\
        public void BDSUnloadingGrid_Enquiry(GridView grid, string UnloadedBy, string OrdBy)
        {
            string commt = "select top(100) cl.DDS_Name,cl.Cartridge_Id,im.Item_Code,im.Item_Name,bm.Brandname,pm.PackType,iu.Pack_Size,il.UOM,Bu.UnloadedBy, convert(varchar(20),bu.UnloadedDate,113) as UnloadedDate,Bu.Status  from BDS_Unloading as Bu left join Cartridge_Loading as cl on cl.Loading_Id=Bu.LoadingID left join Item_user_Master as iu on iu.ID=cl.IUM_ID left join Item_Master as im on im.MasterID=iu.MasterID left join Brand_Master as bm on bm.BrandID=iu.Brandid left join Packtype_Master as pm on pm.ID=iu.PackTypeID left join Item_Location as il on il.MasterID=im.MasterID and il.ID=iu.UOM  where Bu.Status<>'Finish'  " + OrdBy;
            con.View(commt, grid);
        }



     // * BDS FIRST VERIFICATION * \\

        // * BDS PRE Loading Validation Procedure * \\
        public int BDS_FirstVerification_validate(string Cart_Barcode, string location)
        {
            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "BDS_Preloading_Validate";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Cart_Barcode", SqlDbType.VarChar, 20).Value = Cart_Barcode.ToString();
            cmd.Parameters.Add("@Pharmacy_Location ", SqlDbType.VarChar, 50).Value = location.ToString();
            
            SqlParameter ret = new SqlParameter();
            ret.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(ret);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            int retvalue = (int)ret.Value;
            return retvalue;
        }


        // * BDS First Verification Accept * \\
        public int BDS_First_Verification_Accept(string Cart_Barcode,string Verifiedby, string location)
        {
            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "BDS_First_Verification_Accept";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Cart_Barcode", SqlDbType.VarChar, 20).Value = Cart_Barcode.ToString();
            cmd.Parameters.Add("@VerifiedBy", SqlDbType.VarChar, 20).Value = Verifiedby.ToString();
            cmd.Parameters.Add("@Pharmacy_Location ", SqlDbType.VarChar, 50).Value = location.ToString();

            SqlParameter ret = new SqlParameter();
            ret.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(ret);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            int retvalue = (int)ret.Value;
            return retvalue;
        }


        // * BDS First Verification Reject * \\
        public int BDS_First_Verification_Reject(string Cart_Barcode, string Verifiedby, string location,string Reject_Reason)
        {
            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "BDS_First_Verification_Reject";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Cart_Barcode", SqlDbType.VarChar, 20).Value = Cart_Barcode.ToString();
            cmd.Parameters.Add("@VerifiedBy", SqlDbType.VarChar, 20).Value = Verifiedby.ToString();
            cmd.Parameters.Add("@Pharmacy_Location", SqlDbType.VarChar, 50).Value = location.ToString();
            cmd.Parameters.Add("@Reject_Reason", SqlDbType.VarChar, 50).Value = Reject_Reason.ToString();

            SqlParameter ret = new SqlParameter();
            ret.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(ret);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            int retvalue = (int)ret.Value;
            return retvalue;
        }




        // * BDS Verified Cartridge DIsplay * \\
        public void BDS_FstVerified_grid(GridView grid, string userid, string location, string OrdBy)
        {
            string commt = "Select top 100 p.Cart_Barcode,p.CartID,I.Item_Code,I.Item_Name,b.brandname,pm.PackType,p.Carton_Box_Quantity as Cart_Type,isnull(p.BoxOrPallet,' ') as BoxOrPallet,UM.Pack_Size,IL.UOM,p.Status,p.Loaded_By,convert(varchar(20),p.LoadedDate,113) as Loaded_Dat,p.First_Verified,convert(varchar(20),p.First_Verified_DT,113) as Fst_Dat,p.Verified_Status from BDS_PreLoading as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Brand_Master as b on b.BrandID=UM.Brandid left join Pharmacy as ph on ph.PharmacyID=p.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and ph.PharmacyID=IL.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.First_Verified='" + userid + "' and ph.Location_Name='" + location + "' and p.Verified_Status<>'PreLoaded' " + OrdBy;
            con.View(commt, grid);
        }





     // * BDS Cartridge Structure Display With Single Status * 

        public void BDSStructureDisplay(GridView grid, string status,string VcNumber,string Location,string OrdBy)
        {
            string commt = "select Vc.V_Cartridge_No,Vc.V_Cell_No,Vc.V_Cartridge_Width,Vc.V_Cartridge_Height,Vc.V_Cartridge_IsSelected,case Vc.Status when 'Allot' then 'Alloted'  else  Vc.Status  end as Status  from BDS_VC_Cartridge_Structure as Vc left join Pharmacy as p on p.PharmacyID=Vc.PharmacyID where Vc.Status='" + status + "' and p.Location_Name='" + Location + "' and  Vc.V_Cartridge_No like '" + VcNumber + "%' " + OrdBy;
            con.View(commt, grid);
        }

        // * BDS Cartridge Structure Display With ALL Status * 

        public void BDSStructureDisplayAll(GridView grid, string VcNumber, string Location, string OrdBy)
        {
            string commt = "select Vc.V_Cartridge_No,Vc.V_Cell_No,Vc.V_Cartridge_Width,Vc.V_Cartridge_Height,Vc.V_Cartridge_IsSelected,case Vc.Status when 'Allot' then 'Alloted'  else  Vc.Status  end as Status from BDS_VC_Cartridge_Structure as Vc left join Pharmacy as p on p.PharmacyID=Vc.PharmacyID where p.Location_Name='" + Location + "' and  Vc.V_Cartridge_No like '" + VcNumber + "%'  " + OrdBy; 
            con.View(commt, grid);
        }

        // * BDS Cartridge Structure Display With Single Status and VC width * 

        public void BDSStructureDisplaySingleStatusWidth(GridView grid, string status, string Minwidth, string Maxwidth, string Location, string OrdBy)
        {
            string commt = "select Vc.V_Cartridge_No,Vc.V_Cell_No,Vc.V_Cartridge_Width,Vc.V_Cartridge_Height,Vc.V_Cartridge_IsSelected,case Vc.Status when 'Allot' then 'Alloted'  else  Vc.Status  end as Status  from BDS_VC_Cartridge_Structure as Vc  left join Pharmacy as p on p.PharmacyID=Vc.PharmacyID where Vc.Status='" + status + "' and p.Location_Name='" + Location + "' and Vc.V_Cartridge_Width>= convert(decimal(18,2),'" + Minwidth + "') and Vc.V_Cartridge_Width <=convert(decimal(18,2),'" + Maxwidth + "') " + OrdBy;
            con.View(commt, grid);
        }

        // * BDS Cartridge Structure Display With All Status and VC width * 

        public void BDSStructureDisplayAllStatusWidth(GridView grid, string Minwidth, string Maxwidth, string Location, string OrdBy)
        {
            string commt = "select Vc.V_Cartridge_No,Vc.V_Cell_No,Vc.V_Cartridge_Width,Vc.V_Cartridge_Height ,Vc.V_Cartridge_IsSelected,case Vc.Status when 'Allot' then 'Alloted'  else  Vc.Status  end as Status  from BDS_VC_Cartridge_Structure as Vc  left join Pharmacy as p on p.PharmacyID=Vc.PharmacyID where p.Location_Name='" + Location + "' and Vc.V_Cartridge_Width>= convert(decimal(18,2),'" + Minwidth + "') and Vc.V_Cartridge_Width <=convert(decimal(18,2),'" + Maxwidth + "')  " + OrdBy;
            con.View(commt, grid);
        }   
      
    }
}
