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
   public class Searchquery
    {
       DB_Connection con = new DB_Connection();
       SqlCommand cmd = new SqlCommand();
       DateTime QDatefrom, Qdateto;

       #region DATE TIME FUNCTION

       // * Date Time Format Conver Function * \\
       public void datefromfun(string dateval)
       {
           string i = "";
           i = dateval;
           Int32 len = i.Length;
           Int32 n = i.IndexOf('/');
           string str = i.Substring(n + 1, 2);
           string str1 = i.Substring(0, 2);
           string str2 = i.Substring(6, 4);
           string dat = str + "/" + str1 + "/" + str2;
           QDatefrom = Convert.ToDateTime(dat);           
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
           Qdateto = Convert.ToDateTime(dat);
       }

       #endregion


       #region PRELOADING,BATCH ORDER AND DRUG INVENTORY POPUP SEARCH

       // * PRELOADING & All Popup window Item Search Expect Drug Inventory *\\



       //* NEW Drug Barcode Search * \\

       public void preloadpopupBarcode(GridView grid, int BarID, string location)
       {
           string Bot ="Bottle";
           string commt = "(Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  U.ID='" + BarID + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To>=CONVERT(date,getdate()) and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) and pm.Packtype not like '%" + Bot + "%' " +
                   " union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes'  and ba.Status='Active' and  U.ID='" + BarID + "' and p.Location_Name='" + location + "' and IL.Store_Effective_Date_To is null and i.Item_Effective_date_To>=CONVERT(date,getdate()) and pm.Packtype not like '%" + Bot + "%' " +
                  "  union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes'  and ba.Status='Active' and  U.ID='" + BarID + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) and pm.Packtype not like '%" + Bot + "%'  " +
                  "  union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid  left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes'  and ba.Status='Active' and  U.ID='" + BarID + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To is null and pm.Packtype not like '%" + Bot + "%')";
           con.View(commt, grid);
       }

       // * Pre Loading Popup Search Window grid query like drugcode and itemname instring search* \\
       public void preloadpopup(GridView grid, string drugcode, string itemname, string location)
       {
            string Bot ="Bottle";

            string commt = "(Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Drug_Code like'" + drugcode + "%' and i.Item_Name like'%" + itemname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To>=CONVERT(date,getdate()) and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) and pm.Packtype not like '%" + Bot + "%' " +
                     "union  Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Drug_Code like'" + drugcode + "%' and i.Item_Name like'%" + itemname + "%' and p.Location_Name='" + location + "' and IL.Store_Effective_Date_To is null and i.Item_Effective_date_To>=CONVERT(date,getdate()) and pm.Packtype not like '%" + Bot + "%' " +
                     "union  Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Drug_Code like'" + drugcode + "%' and i.Item_Name like'%" + itemname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) and pm.Packtype not like '%" + Bot + "%' " +
                     "union  Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Drug_Code like'" + drugcode + "%' and i.Item_Name like'%" + itemname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To is null and pm.Packtype not like '%" + Bot + "%')";
           con.View(commt,grid);
       }

       // * Pre loading Popupsearch for only itemcode *\\
       public void preloadpoppupitemcode(GridView grid, string itemcode,string location)
       {
           string Bot = "Bottle";

           string commt = "(Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name,u.id,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Item_Code='" + itemcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To>=CONVERT(date,getdate()) and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) and pm.Packtype not like '%" + Bot + "%' " +
                    " union Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name,u.id,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Item_Code='" + itemcode + "' and p.Location_Name='" + location + "' and IL.Store_Effective_Date_To is null and i.Item_Effective_date_To>=CONVERT(date,getdate()) and pm.Packtype not like '%" + Bot + "%' " +
                    " union Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name,u.id,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Item_Code='" + itemcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) and pm.Packtype not like '%" + Bot + "%' " +
                    " union Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name,u.id,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Item_Code='" + itemcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To is null and pm.Packtype not like '%" + Bot + "%')";
           con.View(commt, grid);
       }

       // * Brand Name Search * \\
       public void brandname(GridView grid, string brandname, string location)
       {
           string Bot = "Bottle";

           string commt = "(Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and b.brandname like '" + brandname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To>=CONVERT(date,getdate()) and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) and pm.Packtype not like '%" + Bot + "%' " +
                    " union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and b.brandname like '" + brandname + "%' and p.Location_Name='" + location + "' and IL.Store_Effective_Date_To is null and i.Item_Effective_date_To>=CONVERT(date,getdate()) and pm.Packtype not like '%" + Bot + "%' " +
                    " union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and b.brandname like '" + brandname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To>=CONVERT(date,getdate())  and pm.Packtype not like '%" + Bot + "%' " +
                    " union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and b.brandname like '" + brandname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To is null and pm.Packtype not like '%" + Bot + "%')";
           con.View(commt, grid);
       }

       // * MFR Barcode Search * \\
       public void mfrcode(GridView grid, string mfrcode, string location)
       {
           string Bot = "Bottle";

           string commt = "(Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join MFR_Barcode as m on m.Brandid =b.BrandID and m.Masterid=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and m.Mfrbarcode='" + mfrcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To>=CONVERT(date,getdate()) and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) and pm.Packtype not like '%" + Bot + "%' " +
               "union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join MFR_Barcode as m on m.Brandid =b.BrandID and m.Masterid=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and m.Mfrbarcode='" + mfrcode + "' and p.Location_Name='" + location + "' and IL.Store_Effective_Date_To is null and i.Item_Effective_date_To>=CONVERT(date,getdate()) and pm.Packtype not like '%" + Bot + "%' " +
               "union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join MFR_Barcode as m on m.Brandid =b.BrandID and m.Masterid=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and m.Mfrbarcode='" + mfrcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) and pm.Packtype not like '%" + Bot + "%' " +
               "union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID  left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join MFR_Barcode as m on m.Brandid =b.BrandID and m.Masterid=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and m.Mfrbarcode='" + mfrcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To is null and pm.Packtype not like '%" + Bot + "%')";
           con.View(commt, grid);
       }

   

      // * BATCH ORDER SEARCH QUERY Pop Up window Search Query

       // * Pre Loading Popup Search Window grid query like drugcode and itemname instring search* \\
       public void Batchpreloadpopup(GridView grid, string drugcode, string itemname, string location)
       {          
          
           string commt = "(Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Drug_Code like'" + drugcode + "%' and i.Item_Name like'%" + itemname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To>=CONVERT(date,getdate()) and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) and pm.Packtype='STRIP'  " +
               "union  Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Drug_Code like'" + drugcode + "%' and i.Item_Name like'%" + itemname + "%' and p.Location_Name='" + location + "' and IL.Store_Effective_Date_To is null and i.Item_Effective_date_To>=CONVERT(date,getdate()) and pm.Packtype='STRIP'  " +
               "union  Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Drug_Code like'" + drugcode + "%' and i.Item_Name like'%" + itemname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) and pm.Packtype='STRIP'  " +
               "union  Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Drug_Code like'" + drugcode + "%' and i.Item_Name like'%" + itemname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To is null and pm.Packtype='STRIP' )";
           con.View(commt, grid);
       }

       // * Pre loading Popupsearch for only itemcode *\\
       public void Batchpreloadpoppupitemcode(GridView grid, string itemcode, string location)
       {
           string commt = "(Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name,u.id,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Item_Code='" + itemcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To>=CONVERT(date,getdate()) and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) and pm.Packtype='STRIP' " +
                    " union Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name,u.id,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Item_Code='" + itemcode + "' and p.Location_Name='" + location + "' and IL.Store_Effective_Date_To is null and i.Item_Effective_date_To>=CONVERT(date,getdate()) and pm.Packtype ='STRIP' " +
                    " union Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name,u.id,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Item_Code='" + itemcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) and pm.Packtype ='STRIP' " +
                    " union Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name,u.id,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Item_Code='" + itemcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To is null and pm.Packtype ='STRIP')";
           con.View(commt, grid);
       }

       // * Brand Name Search * \\
       public void Batchbrandname(GridView grid, string brandname, string location)
       {
           string commt = "(Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and b.brandname like '" + brandname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To>=CONVERT(date,getdate()) and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) and pm.Packtype='STRIP' " +
                       " union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and b.brandname like '" + brandname + "%' and p.Location_Name='" + location + "' and IL.Store_Effective_Date_To is null and i.Item_Effective_date_To>=CONVERT(date,getdate()) and pm.Packtype ='STRIP' " +
                       " union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and b.brandname like '" + brandname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To>=CONVERT(date,getdate())  and pm.Packtype='STRIP' " +
                       " union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and b.brandname like '" + brandname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To is null and pm.Packtype ='STRIP')";
           con.View(commt, grid);
       }

       // * MFR Barcode Search * \\
       public void Batchmfrcode(GridView grid, string mfrcode, string location)
       {
           string commt = "(Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join MFR_Barcode as m on m.Brandid =b.BrandID and m.Masterid=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and m.Mfrbarcode='" + mfrcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To>=CONVERT(date,getdate()) and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) and pm.Packtype ='STRIP' " +
                    "union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join MFR_Barcode as m on m.Brandid =b.BrandID and m.Masterid=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and m.Mfrbarcode='" + mfrcode + "' and p.Location_Name='" + location + "' and IL.Store_Effective_Date_To is null and i.Item_Effective_date_To>=CONVERT(date,getdate()) and pm.Packtype='STRIP' " +
                    "union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join MFR_Barcode as m on m.Brandid =b.BrandID and m.Masterid=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and m.Mfrbarcode='" + mfrcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To>=CONVERT(date,getdate()) and pm.Packtype ='STRIP' " +
                    "union Select i.Drug_Code,i.Item_Code,i.Item_Name,U.ID,pm.Packtype,u.Pack_size,IL.UOM, b.brandname from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID  left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join MFR_Barcode as m on m.Brandid =b.BrandID and m.Masterid=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and m.Mfrbarcode='" + mfrcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To is null and pm.Packtype ='STRIP')";
           con.View(commt, grid);
       }


       // * DRUG INVENTORY POP SEARCH QUERY * \\

       // * Drug Inventory Popup Search Window grid query like drugcode and itemname instring search* \\
       public void Druginvpopup(GridView grid, string drugcode, string itemname, string location)
       {
           string commt = "(Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Drug_Code like'" + drugcode + "%' and i.Item_Name like'%" + itemname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To>=CONVERT(date,getdate()) and IL.Store_Effective_Date_To>=CONVERT(date,getdate())   " +
                  "union   Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Drug_Code like'" + drugcode + "%' and i.Item_Name like'%" + itemname + "%' and p.Location_Name='" + location + "' and IL.Store_Effective_Date_To is null and i.Item_Effective_date_To>=CONVERT(date,getdate())   " +
                  "union   Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Drug_Code like'" + drugcode + "%' and i.Item_Name like'%" + itemname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To>=CONVERT(date,getdate())   " +
                  "union   Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Drug_Code like'" + drugcode + "%' and i.Item_Name like'%" + itemname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To is null  )";
           con.View(commt, grid);
       }
       // * Drug Inventory Popupsearch for only itemcode *\\
       public void Druginvpoppupitemcode(GridView grid, string itemcode, string location)
       {
           string commt = "(Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Item_Code='" + itemcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To>=CONVERT(date,getdate()) and IL.Store_Effective_Date_To>=CONVERT(date,getdate())  " +
                  " union  Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Item_Code='" + itemcode + "' and p.Location_Name='" + location + "' and IL.Store_Effective_Date_To is null and i.Item_Effective_date_To>=CONVERT(date,getdate())  " +
                  " union  Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Item_Code='" + itemcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To>=CONVERT(date,getdate())  " +
                  " union  Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and  i.Item_Code='" + itemcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To is null)";
           con.View(commt, grid);
       }

       // *  Drug Inventory Brand Name Search * \\
       public void Druginvbrandname(GridView grid, string brandname, string location)
       {
           string commt = "(Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and b.brandname like '" + brandname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To>=CONVERT(date,getdate()) and IL.Store_Effective_Date_To>=CONVERT(date,getdate())  " +
                        " union Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and b.brandname like '" + brandname + "%' and p.Location_Name='" + location + "' and IL.Store_Effective_Date_To is null and i.Item_Effective_date_To>=CONVERT(date,getdate())  " +
                        " union Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and b.brandname like '" + brandname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To>=CONVERT(date,getdate())  " +
                        " union Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and b.brandname like '" + brandname + "%' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To is null )";
           con.View(commt, grid);
       }

       // * Drug Inventory MFR Barcode Search * \\
       public void Druginvmfrcode(GridView grid, string mfrcode, string location)
       {
           string commt = "(Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join MFR_Barcode as m on m.Brandid =b.BrandID and m.Masterid=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and m.Mfrbarcode='" + mfrcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To>=CONVERT(date,getdate()) and IL.Store_Effective_Date_To>=CONVERT(date,getdate())  " +
                   "union Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join MFR_Barcode as m on m.Brandid =b.BrandID and m.Masterid=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and m.Mfrbarcode='" + mfrcode + "' and p.Location_Name='" + location + "' and IL.Store_Effective_Date_To is null and i.Item_Effective_date_To>=CONVERT(date,getdate())  " +
                   "union Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join MFR_Barcode as m on m.Brandid =b.BrandID and m.Masterid=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and m.Mfrbarcode='" + mfrcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To>=CONVERT(date,getdate())  " +
                   "union Select i.MasterID,i.Drug_Code,i.Item_Code,i.Item_Name from Item_user_Master as u left join item_master as i on i.MasterID=u.MasterID  left join Brand_Master as b on b.BrandID=u.Brandid left join Brand_Allot as ba on ba.BrandID=b.BrandID and ba.MasterID=i.MasterID left join MFR_Barcode as m on m.Brandid =b.BrandID and m.Masterid=i.MasterID left join Pharmacy as p on p.PharmacyID =u.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=p.PharmacyID left join Packtype_Master as pm on pm.ID=u.PacktypeID where u.status='Yes' and ba.Status='Active' and m.Mfrbarcode='" + mfrcode + "' and p.Location_Name='" + location + "' and i.Item_Effective_date_To is null and IL.Store_Effective_Date_To is null )";
           con.View(commt, grid);
       }


       // * ITEM MASTER SEARCH QUERY * \\
       // * Exact item code * \\
       public void itemmastersearch(GridView grid, string itemcode)
       {
           string commt = "Select Item_Code,Item_Name,Drug_Code,convert(varchar,Item_Effective_date_From,105) as iefrom,convert(varchar,Item_Effective_date_To,105) as ieto from Item_Master where Item_Code='" + itemcode + "' order by Item_Name asc ";
           con.View(commt, grid);
       }

       // *  drug code search * \\
       public void itemmasternamecode(GridView grid, string drgcode)
       {
           string commt = "Select Item_Code,Item_Name,Drug_Code,convert(varchar,Item_Effective_date_From,105) as iefrom,convert(varchar,Item_Effective_date_To,105) as ieto from Item_Master where Drug_Code like'" + drgcode + "%'  order by Item_Name asc";
           con.View(commt, grid);
       }

       // * Item name search * \\
       public void itemmastername(GridView grid, string itemname)
       {
           string commt = "Select Item_Code,Item_Name,Drug_Code,convert(varchar,Item_Effective_date_From,105) as iefrom,convert(varchar,Item_Effective_date_To,105) as ieto from Item_Master where Item_Name like'%" + itemname + "%' order by Item_Name asc";
           con.View(commt, grid);
       }


       // * MONITORING, DRUG ALERT AUTO REFRESH SCREEN* \\


       // * DDS Auto Alert * \\

       public int DDSAlert(GridView grid, string location)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           SqlDataAdapter da = new SqlDataAdapter("DDS_Alert_Display", conn);
           da.SelectCommand.CommandType = CommandType.StoredProcedure;
           da.SelectCommand.Parameters.Add("@Location ", SqlDbType.VarChar, 50).Value = location.ToString();
           DataSet DSet = new DataSet();
           da.SelectCommand.CommandType = CommandType.StoredProcedure;
           SqlParameter ret = new SqlParameter();
           ret.Direction = ParameterDirection.ReturnValue;
           da.SelectCommand.Parameters.Add(ret);
           conn.Open();
           da.Fill(DSet);
           grid.DataSource = DSet;
           grid.DataBind();
           conn.Close();
           int retvalue = (int)ret.Value;
           return retvalue;
       }

       // * Drug Auto Low stock Alert * \\

       public int lowstock(GridView grid,string location)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           SqlDataAdapter da = new SqlDataAdapter("DDS_Low_Stock_Alert ", conn);
           da.SelectCommand.CommandType = CommandType.StoredProcedure;
           da.SelectCommand.Parameters.Add("@Location ", SqlDbType.VarChar, 50).Value = location.ToString();
           DataSet DSet = new DataSet();
           da.SelectCommand.CommandType = CommandType.StoredProcedure;
           SqlParameter ret = new SqlParameter();
           ret.Direction = ParameterDirection.ReturnValue;
           da.SelectCommand.Parameters.Add(ret);
           conn.Open();
           da.Fill(DSet);
           grid.DataSource = DSet;
           grid.DataBind();
           conn.Close();
           int retvalue = (int)ret.Value;
           return retvalue;
       }
       // * Drug Low Stock Alert Details Vies * \\
       public void lowstockdetails(GridView grid, string location, string ddsname)
       {
           string commt = "SELECT p.DDS_Name,p.Cell_Id,p.Cartridge_Id,i.Item_Code,i.Item_Name,pm.PackType,UM.Pack_Size,IL.UOM,p.Aval_Quantity,UM.Max_Cartridge_Qty,p.totqty from Cartridge_Loading  as p left join Item_user_Master as UM on UM.ID=p.IUM_ID left join Item_Master as i on i.MasterID=UM.MasterID left join Cartridge_Master as c on c.Cartridge_Id=p.Cartridge_Id left join Pharmacy as ph on ph.PharmacyID = c.PharmacyID left join Item_Location as IL on IL.MasterID=i.MasterID and IL.PharmacyID=ph.PharmacyID left join Packtype_Master as pm on pm.ID=UM.PacktypeID where p.DDS_Name='" + ddsname + "' and p.Inventory_Status='2' and p.Activation_Status='5' and ph.Location_Name='" + location + "' and p.totqty <=UM.Max_Alert_Qty_DDS";
           con.View(commt, grid);
       }


       // * Auto Alert Screen Pending To Pack Alert * \\

       public int pendingtopack(GridView grid,string location)
       {
           SqlConnection conn = con.getstring();
           conn.Close();
           SqlDataAdapter da = new SqlDataAdapter("DDS_Alert_Pending_To_Pack", conn);
           da.SelectCommand.CommandType = CommandType.StoredProcedure;
           da.SelectCommand.Parameters.Add("@Location ", SqlDbType.VarChar, 50).Value = location.ToString();
           DataSet DSet = new DataSet();
           da.SelectCommand.CommandType = CommandType.StoredProcedure;
           SqlParameter ret = new SqlParameter();
           ret.Direction = ParameterDirection.ReturnValue;
           da.SelectCommand.Parameters.Add(ret);
           conn.Open();
           da.Fill(DSet);
           grid.DataSource = DSet;
           grid.DataBind();
           conn.Close();
           int retvalue = (int)ret.Value;
           return retvalue;
       }       

     

       #endregion


       #region QUEUE STATUS ENQUIRY



       // * QUEUE STATUS ENQUIRY * \\

       // * Queue number, date Range and Status ALL First Grid Search * \\
       public void queuenum_All_fstgrid(GridView grid, string queueno, string datefrom, string dateto, string location)
       {
           datefromfun(datefrom);
           datetofun(dateto);
           string commt = "select distinct iq.Queue_No,iq.PatientID,pid.Patient_Name_FN1 as Patientname,'' as status,CONVERT(varchar,iq.Transaction_Date_Time,103) as Transactiondate,CONVERT(varchar,iq.Transaction_Date_Time,114) as ordertime,'' as lastupdatetime,iq.ProcessTime,CONVERT(varchar,iq.ProcessTime,114) as Process_Time from HL7_In_Queue as iq left join HL7_PID as pid on pid.PatientID=iq.PatientID left join Drug_Order as do on do.Queue_No=iq.Queue_No and do.PatientID=iq.PatientID and iq.Pharmacy_Code=do.Pharmacy_Code and do.Transaction_Date=iq.Transaction_Date_Time left join Pharmacy as p on p.Location_code=iq.Pharmacy_Code and p.Location_code=do.Pharmacy_Code where iq.Queue_No='" + queueno + "' and convert(date,iq.Transaction_Date_Time) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and p.Location_Name='" + location + "' order by ordertime desc";
           con.View(commt, grid);
       }

        // * Queue number, date Range  and Status Assembled First Grid Search * \\
       public void queuenum_Assembled_fstgrid(GridView grid, string queueno, string datefrom, string dateto,string location)
       {
           datefromfun(datefrom);
           datetofun(dateto);
           string commt = "select distinct iq.Queue_No,iq.PatientID,pid.Patient_Name_FN1 as Patientname,'Assembled' as status,CONVERT(varchar,iq.Transaction_Date_Time,103) as Transactiondate ,CONVERT(varchar,iq.Transaction_Date_Time,114) as ordertime,'' as lastupdatetime,iq.ProcessTime,CONVERT(varchar,iq.ProcessTime,114) as Process_Time from HL7_In_Queue as iq left join HL7_PID as pid on pid.PatientID=iq.PatientID left join Assembly as a on a.Queue_No=iq.Queue_No and a.PatientID=iq.PatientID left join Pharmacy as p on p.Location_code=iq.Pharmacy_Code and p.PharmacyID=a.PharmacyID where iq.Queue_No='" + queueno + "' and a.Cont_Assembly_Status='ASSEMBLED' and convert(date,iq.Transaction_Date_Time) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and convert(date,a.Transaction_Date_Time) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and p.Location_Name='" + location + "' order by ordertime desc";
           con.View(commt, grid);
       }

       // * Queue number, date Range and Status WIP First Grid Search * \\
       public void queuenum_WIP_fstgrid(GridView grid, string queueno, string datefrom, string dateto,string location)
       {
           datefromfun(datefrom);
           datetofun(dateto);
           string commt = "select distinct iq.Queue_No,iq.PatientID,pid.Patient_Name_FN1 as Patientname,'Work In Progress' as status,CONVERT(varchar,iq.Transaction_Date_Time,103) as Transactiondate ,CONVERT(varchar,iq.Transaction_Date_Time,114) as ordertime,'' as lastupdatetime,iq.ProcessTime,CONVERT(varchar,iq.ProcessTime,114) as Process_Time from HL7_In_Queue as iq left join HL7_PID as pid on pid.PatientID=iq.PatientID left join DDS_Pick as dp on dp.Queue_No=iq.Queue_No and dp.PatientID=iq.PatientID and CONVERT(varchar,dp.Queue_Date,101)=CONVERT(varchar,iq.Transaction_Date_Time,101)  left join Pharmacy as p on p.Location_code=iq.Pharmacy_Code left join Drug_Pick as drp on drp.DDS_PickID=dp.DDS_PickId  where iq.Queue_No='" + queueno + "' and drp.Flag='NO' and convert(date,iq.Transaction_Date_Time) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and convert(Date,dp.Queue_Date) between convert(Date,'" + QDatefrom + "') and  convert(Date,'" + Qdateto + "') and p.Location_Name='" + location + "' order by ordertime desc";
           con.View(commt, grid);
       }

       // * Queue Number ,date Range  and Status Pending Orders First Grid Search * \\
       public void queuenum_Pen_order_fstgrid(GridView grid, string queueno, string datefrom, string dateto,string location)
       {
           datefromfun(datefrom);
           datetofun(dateto);
           string commt = "select distinct iq.Queue_No,iq.PatientID,pid.Patient_Name_FN1 as Patientname,'Pending DDS/BDS' as status,CONVERT(varchar,iq.Transaction_Date_Time,103) as Transactiondate,CONVERT(varchar,iq.Transaction_Date_Time,114) as ordertime,'' as lastupdatetime,iq.ProcessTime,CONVERT(varchar,iq.ProcessTime,114) as Process_Time from HL7_In_Queue as iq left join HL7_PID as pid on pid.PatientID=iq.PatientID left join Drug_Order as do on do.Queue_No=iq.Queue_No and do.PatientID=iq.PatientID and iq.Pharmacy_Code=do.Pharmacy_Code  and iq.Transaction_Date_Time=do.Transaction_Date left join Pharmacy as p on p.Location_code=iq.Pharmacy_Code and p.Location_code=do.Pharmacy_Code where iq.Queue_No='" + queueno + "' and do.Allocation_Status='NEW' and do.Pre_Allocation<>'NO DDS' and convert(date,iq.Transaction_Date_Time) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and p.Location_Name='" + location + "' order by ordertime desc";
           con.View(commt, grid);
       }

       // * Queue Number ,date range and status Jump Queue *\\
       public void queuenum_Jump(GridView grid, string queueno, string datefrom, string dateto, string location)
       {
           datefromfun(datefrom);
           datetofun(dateto);
           string commt = "select distinct do.Queue_No,do.PatientID,pid.Patient_Name_FN1 as Patientname,'Jump Queue Orders' as status,CONVERT(varchar,do.Transaction_Date,103) as Transactiondate,CONVERT(varchar,do.Transaction_Date,114) as ordertime,do.Processing_date,CONVERT(varchar,do.Processing_date,114) as Process_Time from Drug_Order as do left join HL7_PID as pid on pid.PatientID=do.PatientID left join HL7_Jump_MSH as j on j.QueueNo=do.Queue_No and j.PatientID=do.PatientID left join Pharmacy as p on p.Location_code=do.Pharmacy_Code and p.Location_code=j.Pharmacy_Store where do.Queue_No='" + queueno + "' and convert(Date,do.Transaction_Date) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and p.Location_Name='" + location + "' and  j.Jump_QueueDt>do.Transaction_Date order by ordertime desc";
           con.View(commt, grid);
       }

       // * Queue Number ,date range and status Trigger Orders *\\
       public void queuenum_trigger(GridView grid, string queueno, string datefrom, string dateto, string location)
       {
           datefromfun(datefrom);
           datetofun(dateto);
           string commt = "select distinct do.Queue_No,do.PatientID,pid.Patient_Name_FN1 as Patientname,'Triger Orders' as status,CONVERT(varchar,do.Transaction_Date,103) as Transactiondate,CONVERT(varchar,do.Transaction_Date,114) as ordertime,do.Processing_date,CONVERT(varchar,do.Processing_date,114) as Process_Time from Drug_Order as do left join HL7_PID as pid on pid.PatientID=do.PatientID left join HL7_Trigger_MSH  as t on t.QueueNo=do.Queue_No and  t.Fastrak_JobID=do.JobID  left join Pharmacy as p on p.Location_code=do.Pharmacy_Code and p.Location_code=t.Pharmacy_Store where do.Queue_No='" + queueno + "' and convert(Date,do.Transaction_Date) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and p.Location_Name='" + location + "' order by ordertime desc";
           con.View(commt, grid);
       }


       // * PatientID, date Range and Status ALL First Grid Search * \\
       public void patientid_All_fstgrid(GridView grid, string Patientid, string datefrom, string dateto, string location)
       {
           datefromfun(datefrom);
           datetofun(dateto);
           string commt = "select distinct iq.Queue_No,iq.PatientID,pid.Patient_Name_FN1 as Patientname,'' as status,CONVERT(varchar,iq.Transaction_Date_Time,103) as Transactiondate,CONVERT(varchar,iq.Transaction_Date_Time,114) as ordertime,'' as lastupdatetime,iq.ProcessTime,CONVERT(varchar,iq.ProcessTime,114) as Process_Time from HL7_In_Queue as iq left join HL7_PID as pid on pid.PatientID=iq.PatientID left join Drug_Order as do on do.Queue_No=iq.Queue_No and do.PatientID=iq.PatientID and iq.Pharmacy_Code=do.Pharmacy_Code and do.Transaction_Date=iq.Transaction_Date_Time left join Pharmacy as p on p.Location_code=iq.Pharmacy_Code and p.Location_code=do.Pharmacy_Code where iq.PatientID='" + Patientid + "' and convert(date,iq.Transaction_Date_Time) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and p.Location_Name='" + location + "' order by ordertime desc";
           con.View(commt, grid);
       }
   
       // * PatientID,date Range and Status Assembled First Grid Search * \\
       public void patientid_Assembled_fstgrid(GridView grid, string Patientid, string datefrom, string dateto,string location)
       {
           datefromfun(datefrom);
           datetofun(dateto);
           string commt = "select distinct iq.Queue_No,iq.PatientID,pid.Patient_Name_FN1 as Patientname,'Assembled' as status,CONVERT(varchar,iq.Transaction_Date_Time,103) as Transactiondate ,CONVERT(varchar,iq.Transaction_Date_Time,114) as ordertime,'' as lastupdatetime,iq.ProcessTime,CONVERT(varchar,iq.ProcessTime,114) as Process_Time from HL7_In_Queue as iq left join HL7_PID as pid on pid.PatientID=iq.PatientID left join Assembly as a on a.Queue_No=iq.Queue_No and a.PatientID=iq.PatientID left join Pharmacy as p on p.Location_code=iq.Pharmacy_Code and p.PharmacyID=a.PharmacyID where iq.PatientID='" + Patientid + "' and a.Cont_Assembly_Status='ASSEMBLED' and convert(date,iq.Transaction_Date_Time) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and convert(date,a.Transaction_Date_Time) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "')  and p.Location_Name='" + location + "' order by ordertime desc";
           con.View(commt, grid);
       }

       // * PatientID,date Range and Status WIP First Grid Search * \\
       public void patientid_WIP_fstgrid(GridView grid, string Patientid, string datefrom, string dateto,string location)
       {
           datefromfun(datefrom);
           datetofun(dateto);
           string commt = "select distinct iq.Queue_No,iq.PatientID,pid.Patient_Name_FN1 as Patientname,'Work In Progress' as status,CONVERT(varchar,iq.Transaction_Date_Time,103) as Transactiondate ,CONVERT(varchar,iq.Transaction_Date_Time,114) as ordertime,'' as lastupdatetime,iq.ProcessTime,CONVERT(varchar,iq.ProcessTime,114) as Process_Time from HL7_In_Queue as iq left join HL7_PID as pid on pid.PatientID=iq.PatientID left join DDS_Pick as dp on dp.Queue_No=iq.Queue_No and dp.PatientID=iq.PatientID and CONVERT(varchar,dp.Queue_Date,101)=CONVERT(varchar,iq.Transaction_Date_Time,101)  left join Pharmacy as p on p.Location_code=iq.Pharmacy_Code left join Drug_Pick as drp on drp.DDS_PickID=dp.DDS_PickId   where iq.PatientID='" + Patientid + "' and drp.Flag='NO' and convert(date,iq.Transaction_Date_Time) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and convert(Date,dp.Queue_Date) between convert(Date,'" + QDatefrom + "') and  convert(Date,'" + Qdateto + "') and p.Location_Name='" + location + "' order by ordertime desc";
           con.View(commt, grid);
       }

       // * PatientID ,date Range  and Status Pending Orders First Grid Search * \\
       public void patientid_Pen_order_fstgrid(GridView grid, string Patientid, string datefrom, string dateto, string location)
       {
           datefromfun(datefrom);
           datetofun(dateto);
           string commt = "select distinct iq.Queue_No,iq.PatientID,pid.Patient_Name_FN1 as Patientname,'Pending DDS/BDS' as status,CONVERT(varchar,iq.Transaction_Date_Time,103) as Transactiondate,CONVERT(varchar,iq.Transaction_Date_Time,114) as ordertime,'' as lastupdatetime,iq.ProcessTime,CONVERT(varchar,iq.ProcessTime,114) as Process_Time from HL7_In_Queue as iq left join HL7_PID as pid on pid.PatientID=iq.PatientID left join Drug_Order as do on do.Queue_No=iq.Queue_No and do.PatientID=iq.PatientID and iq.Pharmacy_Code=do.Pharmacy_Code  and iq.Transaction_Date_Time=do.Transaction_Date left join Pharmacy as p on p.Location_code=iq.Pharmacy_Code and p.Location_code=do.Pharmacy_Code where iq.PatientID='" + Patientid + "' and do.Allocation_Status='NEW' and do.Pre_Allocation<>'NO DDS' and convert(date,iq.Transaction_Date_Time) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and p.Location_Name='" + location + "' order by ordertime desc";
           con.View(commt, grid);
       }

       // * PatientID ,date Range  and Status Jump Queue First Grid Search * \\
       public void patientid_jumpqueue(GridView grid, string Patientid, string datefrom, string dateto, string location)
       {
           datefromfun(datefrom);
           datetofun(dateto);
           string commt = "select distinct do.Queue_No,do.PatientID,pid.Patient_Name_FN1 as Patientname,'Jump Queue Orders' as status,CONVERT(varchar,do.Transaction_Date,103) as Transactiondate,CONVERT(varchar,do.Transaction_Date,114) as ordertime,do.Processing_date,CONVERT(varchar,do.Processing_date,114) as Process_Time from Drug_Order as do left join HL7_PID as pid on pid.PatientID=do.PatientID left join HL7_Jump_MSH as j on j.QueueNo=do.Queue_No and j.PatientID=do.PatientID  left join Pharmacy as p on p.Location_code=do.Pharmacy_Code and p.Location_code=j.Pharmacy_Store where do.PatientID='" + Patientid + "' and convert(date,do.Transaction_Date) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and p.Location_Name='" + location + "' and j.Jump_QueueDt>do.Transaction_Date order by ordertime desc";
           con.View(commt, grid);
       }

       // * PatientID ,date Range  and Status Trigger Orders First Grid Search * \\
       public void patientid_trigger(GridView grid, string Patientid, string datefrom, string dateto, string location)
       {
           datefromfun(datefrom);
           datetofun(dateto);
           string commt = "select distinct do.Queue_No,do.PatientID,pid.Patient_Name_FN1 as Patientname,'Triger Orders' as status,CONVERT(varchar,do.Transaction_Date,103) as Transactiondate,CONVERT(varchar,do.Transaction_Date,114) as ordertime,do.Processing_date,CONVERT(varchar,do.Processing_date,114) as Process_Time from Drug_Order as do left join HL7_PID as pid on pid.PatientID=do.PatientID left join HL7_Trigger_MSH  as t on t.QueueNo=do.Queue_No and  t.Fastrak_JobID=do.JobID  left join Pharmacy as p on p.Location_code=do.Pharmacy_Code and p.Location_code=t.Pharmacy_Store where do.PatientID ='" + Patientid + "' and convert(date,do.Transaction_Date) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and p.Location_Name='" + location + "' order by ordertime desc";
           con.View(commt, grid);
       }

       // * Patient Name,date and Ststua ALL First Grid Search * \\
       public void patientname_ALL_fstgrid(GridView grid, string Patientname, string datefrom, string dateto, string location)
       {
           datefromfun(datefrom);
           datetofun(dateto);
           string commt = "select distinct iq.Queue_No,iq.PatientID,pid.Patient_Name_FN1 as Patientname,'' as status,CONVERT(varchar,iq.Transaction_Date_Time,103) as Transactiondate,CONVERT(varchar,iq.Transaction_Date_Time,114) as ordertime,'' as lastupdatetime,iq.ProcessTime,CONVERT(varchar,iq.ProcessTime,114) as Process_Time from HL7_In_Queue as iq left join HL7_PID as pid on pid.PatientID=iq.PatientID left join Drug_Order as do on do.Queue_No=iq.Queue_No and do.PatientID=iq.PatientID and iq.Pharmacy_Code=do.Pharmacy_Code and do.Transaction_Date=iq.Transaction_Date_Time left join Pharmacy as p on p.Location_code=iq.Pharmacy_Code and p.Location_code=do.Pharmacy_Code where pid.Patient_Name_FN1='" + Patientname + "' and convert(date,iq.Transaction_Date_Time) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and p.Location_Name='" + location + "' order by ordertime desc";
           con.View(commt, grid);
       }
       


       // * Patient Name,date and Ststua Pending Orders First Grid Search * \\
       public void patientname_Pen_Order_fstgrid(GridView grid, string Patientname, string datefrom, string dateto,string location)
       {
           datefromfun(datefrom);
           datetofun(dateto);
           string commt = "select distinct iq.Queue_No,iq.PatientID,pid.Patient_Name_FN1 as Patientname,'Pending DDS/BDS' as status,CONVERT(varchar,iq.Transaction_Date_Time,103) as Transactiondate,CONVERT(varchar,iq.Transaction_Date_Time,114) as ordertime,'' as lastupdatetime,iq.ProcessTime,CONVERT(varchar,iq.ProcessTime,114) as Process_Time from HL7_In_Queue as iq left join HL7_PID as pid on pid.PatientID=iq.PatientID left join Drug_Order as do on do.Queue_No=iq.Queue_No and do.PatientID=iq.PatientID and iq.Pharmacy_Code=do.Pharmacy_Code  and iq.Transaction_Date_Time=do.Transaction_Date left join Pharmacy as p on p.Location_code=iq.Pharmacy_Code and p.Location_code=do.Pharmacy_Code where pid.Patient_Name_FN1='" + Patientname + "' and do.Allocation_Status='NEW' and do.Pre_Allocation<>'NO DDS' and convert(date,iq.Transaction_Date_Time) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and p.Location_Name='" + location + "' order by ordertime desc";
           con.View(commt, grid);
       }
       

       // * Patient Name,date Range and status Assembled First Grid Search * \\
       public void patientname_Assembled_fstgrid(GridView grid, string Patientname, string datefrom, string dateto,string location)
       {
           datefromfun(datefrom);
           datetofun(dateto);
           string commt = "select distinct iq.Queue_No,iq.PatientID,pid.Patient_Name_FN1 as Patientname,'Assembled' as status,CONVERT(varchar,iq.Transaction_Date_Time,103) as Transactiondate ,CONVERT(varchar,iq.Transaction_Date_Time,114) as ordertime,'' as lastupdatetime,iq.ProcessTime,CONVERT(varchar,iq.ProcessTime,114) as Process_Time from HL7_In_Queue as iq left join HL7_PID as pid on pid.PatientID=iq.PatientID left join Assembly as a on a.Queue_No=iq.Queue_No and a.PatientID=iq.PatientID left join Pharmacy as p on p.Location_code=iq.Pharmacy_Code and p.PharmacyID=a.PharmacyID where pid.Patient_Name_FN1='" + Patientname + "' and a.Cont_Assembly_Status='ASSEMBLED' and convert(date,iq.Transaction_Date_Time) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and convert(date,a.Transaction_Date_Time) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and p.Location_Name='" + location + "' order by ordertime desc";
           con.View(commt, grid);
       }

       // * Patient Name,date Range and Status WIP First Grid Search * \\
       public void patientname_WIP_fstgrid(GridView grid, string Patientname, string datefrom, string dateto,string location)
       {
           datefromfun(datefrom);
           datetofun(dateto);
           //string commt = "select distinct iq.Queue_No,iq.PatientID,pid.Patient_Name_FN1 as Patientname,'Work In Progress' as status,CONVERT(varchar,iq.Transaction_Date_Time,103) as Transactiondate ,CONVERT(varchar,iq.Transaction_Date_Time,114) as ordertime,'' as lastupdatetime from HL7_In_Queue as iq left join HL7_PID as pid on pid.PatientID=iq.PatientID left join DDS_Pick as dp on dp.Queue_No=iq.Queue_No and dp.PatientID=iq.PatientID and CONVERT(varchar,dp.Queue_Date,101)=CONVERT(varchar,iq.Transaction_Date_Time,101)  left join Pharmacy as p on p.Location_code=iq.Pharmacy_Code where pid.Patient_Name_FN1='" + Patientname + "' and dp.Flag='NO' and iq.Transaction_Date_Time between '" + QDatefrom + "' and DATEADD(DD,1,'" + Qdateto + "') and dp.Queue_Date >= '" + QDatefrom + "' and  dp.Queue_Date < DATEADD(DD,1,'" + Qdateto + "') and p.Location_Name='" + location + "'";
           string commt = "select distinct iq.Queue_No,iq.PatientID,pid.Patient_Name_FN1 as Patientname,'Work In Progress' as status,CONVERT(varchar,iq.Transaction_Date_Time,103) as Transactiondate ,CONVERT(varchar,iq.Transaction_Date_Time,114) as ordertime,'' as lastupdatetime,iq.ProcessTime,CONVERT(varchar,iq.ProcessTime,114) as Process_Time from HL7_In_Queue as iq left join HL7_PID as pid on pid.PatientID=iq.PatientID left join DDS_Pick as dp on dp.Queue_No=iq.Queue_No and dp.PatientID=iq.PatientID and CONVERT(varchar,dp.Queue_Date,101)=CONVERT(varchar,iq.Transaction_Date_Time,101)  left join Pharmacy as p on p.Location_code=iq.Pharmacy_Code left join Drug_Pick as drp on drp.DDS_PickID=dp.DDS_PickId where pid.Patient_Name_FN1='" + Patientname + "' and drp.Flag='NO' and convert(date,iq.Transaction_Date_Time) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and convert(date,dp.Queue_Date) between convert(Date,'" + QDatefrom + "') and  convert(Date,'" + Qdateto + "') and p.Location_Name='" + location + "' order by ordertime desc";
           con.View(commt, grid);
       }

       // * Patient Name,date Range and Status Jumpqueue Grid Search * \\
       public void patientname_jumpqueue(GridView grid, string Patientname, string datefrom, string dateto, string location)
       {
           datefromfun(datefrom);
           datetofun(dateto);
           string commt = "select distinct do.Queue_No,do.PatientID,pid.Patient_Name_FN1 as Patientname,'' as status,CONVERT(varchar,do.Transaction_Date,103) as Transactiondate,CONVERT(varchar,do.Transaction_Date,114) as ordertime,do.Processing_date,CONVERT(varchar,do.Processing_date,114) as Process_Time from Drug_Order as do left join HL7_PID as pid on pid.PatientID=do.PatientID left join HL7_Jump_MSH as j on j.QueueNo=do.Queue_No and j.PatientID=do.PatientID left join Pharmacy as p on p.Location_code=do.Pharmacy_Code and p.Location_code=j.Pharmacy_Store where pid.Patient_Name_FN1='" + Patientname + "' and convert(date,do.Transaction_Date) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and p.Location_Name='" + location + "' and j.Jump_QueueDt>do.Transaction_Date order by ordertime desc";
           con.View(commt, grid);
       }

       // * Patient Name,date Range and Status Trigger Grid Search * \\
       public void patientname_trigger(GridView grid, string Patientname, string datefrom, string dateto, string location)
       {
           datefromfun(datefrom);
           datetofun(dateto);
           string commt = "select distinct do.Queue_No,do.PatientID,pid.Patient_Name_FN1 as Patientname,'Triger Orders' as status,CONVERT(varchar,do.Transaction_Date,103) as Transactiondate,CONVERT(varchar,do.Transaction_Date,114) as ordertime,do.Processing_date,CONVERT(varchar,do.Processing_date,114) as Process_Time from Drug_Order as do left join HL7_PID as pid on pid.PatientID=do.PatientID left join HL7_Trigger_MSH  as t on t.QueueNo=do.Queue_No and  t.Fastrak_JobID=do.JobID left join Pharmacy as p on p.Location_code=do.Pharmacy_Code and p.Location_code=t.Pharmacy_Store where pid.Patient_Name_FN1 ='" + Patientname + "' and convert(date,do.Transaction_Date) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and p.Location_Name='" + location + "' order by ordertime desc";
           con.View(commt, grid);
       }

       // * Single date Status ALL First Grid Search * \\
       public void Sinleday_ALL_fstgrid(GridView grid, string datefrom, string location,string ord)
       {
           datefromfun(datefrom);
           string commt = "select distinct iq.Queue_No,iq.PatientID,pid.Patient_Name_FN1 as Patientname,'' as status,CONVERT(varchar,iq.Transaction_Date_Time,103) as Transactiondate,CONVERT(varchar,iq.Transaction_Date_Time,114) as ordertime,'' as lastupdatetime,iq.ProcessTime,CONVERT(varchar,iq.ProcessTime,114) as Process_Time from HL7_In_Queue as iq left join HL7_PID as pid on pid.PatientID=iq.PatientID left join Drug_Order as do on do.Queue_No=iq.Queue_No and do.PatientID=iq.PatientID and iq.Pharmacy_Code=do.Pharmacy_Code and do.Transaction_Date=iq.Transaction_Date_Time left join Pharmacy as p on p.Location_code=iq.Pharmacy_Code and p.Location_code=do.Pharmacy_Code where convert(date,iq.Transaction_Date_Time) = convert(Date,'" + QDatefrom + "')  and p.Location_Name='" + location + "' and do.Pre_Allocation<>'Cancel' union select distinct iq.Queue_No,iq.PatientID,pid.Patient_Name_FN1 as Patientname,'' as status,CONVERT(varchar,iq.Transaction_Date_Time,103) as Transactiondate,CONVERT(varchar,iq.Transaction_Date_Time,114) as ordertime,'' as lastupdatetime,iq.ProcessTime,CONVERT(varchar,iq.ProcessTime,114) as Process_Time from HL7_In_Queue as iq left join HL7_PID as pid on pid.PatientID=iq.PatientID left join Drug_Order as do on do.Queue_No=iq.Queue_No and do.PatientID=iq.PatientID and iq.Pharmacy_Code=do.Pharmacy_Code and do.Transaction_Date=iq.Transaction_Date_Time left join Pharmacy as p on p.Location_code=iq.Pharmacy_Code and p.Location_code=do.Pharmacy_Code where convert(date,iq.Transaction_Date_Time) = convert(Date,'" + QDatefrom + "')  and p.Location_Name='" + location + "' and do.Amnd_Status='T'" + ord;           
           con.View(commt, grid);
       }

       // * Single date Status Pending Orders First Grid Search * \\
       public void Sinleday_Pen_Order_fstgrid(GridView grid, string datefrom, string location)
       {
           datefromfun(datefrom);
           string commt = "select distinct iq.Queue_No,iq.PatientID,pid.Patient_Name_FN1 as Patientname,'Pending DDS/BDS' as status,CONVERT(varchar,iq.Transaction_Date_Time,103) as Transactiondate,CONVERT(varchar,iq.Transaction_Date_Time,114) as ordertime,'' as lastupdatetime,iq.ProcessTime,CONVERT(varchar,iq.ProcessTime,114) as Process_Time from HL7_In_Queue as iq left join HL7_PID as pid on pid.PatientID=iq.PatientID left join Drug_Order as do on do.Queue_No=iq.Queue_No and do.PatientID=iq.PatientID  and iq.Transaction_Date_Time=do.Transaction_Date and iq.Pharmacy_Code=do.Pharmacy_Code left join Pharmacy as p on p.Location_code=iq.Pharmacy_Code and p.Location_code=do.Pharmacy_Code where do.Allocation_Status='NEW' and do.Pre_Allocation<>'NO DDS' and convert(date,iq.Transaction_Date_Time) = convert(Date,'" + QDatefrom + "')  and p.Location_Name='" + location + "' order by ordertime desc";
           con.View(commt, grid);
       }

   
       // * Single date Status Assembled First Grid Search * \\
       public void Sinleday_Assembled_fstgrid(GridView grid, string datefrom,string location)
       {
           datefromfun(datefrom);
           string commt = "select distinct iq.Queue_No,iq.PatientID,pid.Patient_Name_FN1 as Patientname,'Assembly Completed' as status,CONVERT(varchar,iq.Transaction_Date_Time,103) as Transactiondate ,CONVERT(varchar,iq.Transaction_Date_Time,114) as ordertime,'' as lastupdatetime,iq.ProcessTime,CONVERT(varchar,iq.ProcessTime,114) as Process_Time from HL7_In_Queue as iq left join HL7_PID as pid on pid.PatientID=iq.PatientID left join Assembly as a on a.Queue_No=iq.Queue_No and a.PatientID=iq.PatientID left join Pharmacy as p on p.Location_code=iq.Pharmacy_Code and p.PharmacyID=a.PharmacyID where a.Cont_Assembly_Status='ASSEMBLED' and convert(date,iq.Transaction_Date_Time) = convert(Date,'" + QDatefrom + "')  and  convert(date,a.Transaction_Date_Time) = convert(Date,'" + QDatefrom + "')  and p.Location_Name='" + location + "' order by ordertime desc";
           con.View(commt, grid);
       }

       // * Single date Status WIP First Grid Search * \\
       public void Sinleday_WIP_fstgrid(GridView grid, string datefrom,string location)
       {
           datefromfun(datefrom);
           string commt = "select distinct iq.Queue_No,iq.PatientID,pid.Patient_Name_FN1 as Patientname,'Work In Progress' as status,CONVERT(varchar,iq.Transaction_Date_Time,103) as Transactiondate ,CONVERT(varchar,iq.Transaction_Date_Time,114) as ordertime,'' as lastupdatetime,iq.ProcessTime,CONVERT(varchar,iq.ProcessTime,114) as Process_Time from HL7_In_Queue as iq left join HL7_PID as pid on pid.PatientID=iq.PatientID left join DDS_Pick as dp on dp.Queue_No=iq.Queue_No and dp.PatientID=iq.PatientID and CONVERT(varchar,dp.Queue_Date,101)=CONVERT(varchar,iq.Transaction_Date_Time,101)  left join Pharmacy as p on p.Location_code=iq.Pharmacy_Code left join Drug_Pick as drp on drp.DDS_PickID=dp.DDS_PickId  where drp.Flag='NO' and convert(date,iq.Transaction_Date_Time) = convert(Date,'" + QDatefrom + "')  and convert(Date,dp.Queue_Date) = convert(Date,'" + QDatefrom + "')  and p.Location_Name='" + location + "' order by ordertime desc";
           con.View(commt, grid);
       }

       // * Single date Status Jump queue First Grid Search * \\
       public void Sinleday_jumpqueue(GridView grid, string datefrom, string location)
       {
           datefromfun(datefrom);
           string commt = "select distinct do.Queue_No,do.PatientID,pid.Patient_Name_FN1 as Patientname,'Jump Queue Orders' as status,CONVERT(varchar,do.Transaction_Date,103) as Transactiondate,CONVERT(varchar,do.Transaction_Date,114) as ordertime,do.Processing_date,CONVERT(varchar,do.Processing_date,114) as Process_Time from Drug_Order as do left join HL7_PID as pid on pid.PatientID=do.PatientID left join HL7_Jump_MSH as j on j.QueueNo=do.Queue_No and j.PatientID=do.PatientID left join Pharmacy as p on p.Location_code=do.Pharmacy_Code and p.Location_code=j.Pharmacy_Store where  convert(Date,do.Transaction_Date) = convert(Date,'" + QDatefrom + "')  and p.Location_Name='" + location + "' and j.Jump_QueueDt>do.Transaction_Date order by ordertime desc";
           con.View(commt, grid);
       }

       // * Single date Status Trigger Orders First Grid Search * \\
       public void Sinleday_trigger(GridView grid, string datefrom, string location)
       {
           datefromfun(datefrom);
           string commt = "select distinct do.Queue_No,do.PatientID,pid.Patient_Name_FN1 as Patientname,'Triger Orders' as status,CONVERT(varchar,do.Transaction_Date,103) as Transactiondate,CONVERT(varchar,do.Transaction_Date,114) as ordertime,do.Processing_date,CONVERT(varchar,do.Processing_date,114) as Process_Time from Drug_Order as do left join HL7_PID as pid on pid.PatientID=do.PatientID left join HL7_Trigger_MSH  as t on t.QueueNo=do.Queue_No and t.Fastrak_JobID=do.JobID  left join Pharmacy as p on p.Location_code=do.Pharmacy_Code and p.Location_code=t.Pharmacy_Store where  convert(Date,do.Transaction_Date) = convert(Date,'" + QDatefrom + "') and p.Location_Name='" + location + "' order by ordertime desc";
           con.View(commt, grid);
       }



       // * Queue Status Detailed Grid Display For Status Assembled * \\
       public void Quedetailedassembled(GridView grid ,string queueno, string Patientid, string orderdt,string location)
       {
           datefromfun(orderdt);

           string commt = "select a.Item_Code,a.Item_Name,a.Order_Qty,dp.UOM,a.Indicator as Ind,case dp.DDS_Name when 'Manual' then '' else dp.DDS_Name end as DDS_Name,case a.cell_location when '0' then '' else a.cell_location end as cell_location,case a.cartridge_no when '0' then '' else a.cartridge_no end  as cartridge_no,case a.Cont_Assembly_Status when 'ASSEMBLED' then 'Assembled' else '' end as Status,case a.Intervention when 'AVAILABLE' then '' when 'EMPTY BAG' then 'Empty Bag' when 'MANUAL PK' then 'Manual' else a.Intervention end as Intervention ,CONVERT(varchar,do.Transaction_Date,108) as OrderTime,CONVERT(varchar,do.Opas_Received_Date,108) as Opasreceivedtime,CONVERT(varchar,do.Processing_date,108) as OpasProcessingtime  ,CONVERT(varchar,a.Packed_Date_Time,108) as Packedtime,CONVERT(varchar,a.Cont_Assembled_Date_Time,108) as Assembledtime,case a.Intervention when 'EMPTY BAG' then '0' when 'Order Amended, remove' then(case a.Cartridge_No when '' then (case dp.DDS_Name when 'Manual' then a.Total_Qty else '0' end ) else  a.Total_Qty end ) when 'Order Amended, replace' then(case a.Cartridge_No when '' then (case dp.DDS_Name when 'Manual' then a.Total_Qty else '0' end ) else  a.Total_Qty end )when 'Order Cancelled, remove' then(case a.Cartridge_No when '' then (case dp.DDS_Name when 'Manual' then a.Total_Qty else '0' end ) else  a.Total_Qty end )else  a.Total_Qty end as pickedqty,a.RFID_No,a.Container_No,dp.Packtype,dp.PackSize,isnull(do.ReProcessed,'') as ReProcessed,ISNULL(CONVERT(varchar,dp.MC_Order_Received_Dt,108),'') as MC_Order_Received_Dt from Assembly as a left join Drug_Pick as dp on dp.Drug_pickID=a.Drug_pickid left join Drug_Order as do on do.OrderId=dp.OrderID left join Pharmacy as p on p.PharmacyID=a.PharmacyID and p.Location_code=do.Pharmacy_Code  where do.Queue_No='" + queueno + "' and do.PatientID='" + Patientid + "' and a.Cont_Assembly_Status='ASSEMBLED' and CONVERT(date,do.Transaction_Date)= convert(Date,'" + QDatefrom + "') and p.Location_Name='" + location + "' order by a.Cont_Assembled_Date_Time desc ";
           con.View(commt, grid);
       }   

       // * Queue Status Detailed Grid Display For Status WIP * \\
       public void QuedetailWIP(GridView grid, string queueno, string patientid, string orderdt,string location)
       {
           datefromfun(orderdt);

           string commt = "select dp.Drug_pickID,dp.Item_Code,dp.Item_Name,dp.Order_Qty,dp.UOM,dp.Indicator as Ind,case dp.DDS_Name when 'Manual' then '' else dp.DDS_Name end as DDS_Name,case dp.Cell_Location1 when '0' then '' else dp.Cell_Location1 end as cell_location,case dp.Cartridge_No1 when '0' then '' else dp.Cartridge_No1 end  as cartridge_no,'Work In Progress' Status,case dp.Flag when 'NO' then (case dp.Intervention when 'AVAILABLE' then '' when 'EMPTY BAG' then 'Empty Bag' when 'MANUAL PK' then 'Manual' else dp.Intervention end ) else '' end as Intervention,CONVERT(varchar,do.Transaction_Date,108) as OrderTime,CONVERT(varchar,do.Opas_Received_Date,108) as Opasreceivedtime,CONVERT(varchar,do.Processing_date,108) as OpasProcessingtime ,'' as Packedtime,'' as Assembledtime, '' as pickedqty,'' as RFID_No,dp.Container_No,dp.Packtype,dp.PackSize, isnull(do.ReProcessed,'') as ReProcessed,ISNULL(CONVERT(varchar,dp.MC_Order_Received_Dt,108),'') as MC_Order_Received_Dt from Drug_Pick as dp left join Drug_Order as do on do.OrderId=dp.OrderID left join Pharmacy as p on p.Location_code=do.Pharmacy_Code  where do.Queue_No='" + queueno + "' and do.PatientID='" + patientid + "' and dp.Status <>'Finish' and dp.Flag='NO' and CONVERT(date,do.Transaction_Date)= convert(Date,'" + QDatefrom + "') and p.Location_Name='" + location + "' order by do.Opas_Received_Date desc  ";
           con.View(commt, grid);
       }

       //* Queue Status Detailed Grid Display For Status Pending orders * \\
       public void Quedetailpenorder(GridView grid, string queueno, string patientid, string orderdt, string location)
       {
           datefromfun(orderdt);
           string commt = "select ob.Item_Code,ob.Drug_Name as Item_Name,ob.Sales_Quantity as Order_Qty,ob.Sales_UOM as UOM,ob.Machine_Packed_Indicator as Ind,case dp.DDS_Name when 'Manual' then '' else dp.DDS_Name end as DDS_Name,case dp.Cell_Location1 when '0' then '' else dp.Cell_Location1 end as cell_location,case dp.Cartridge_No1 when '0' then '' else dp.Cartridge_No1 end  as cartridge_no,case dp.Flag when 'NO' then 'Work In Progress'When 'YES' then 'Pending DDS/BDS'when 'OK' then '' else 'Pre Allocation Pending' end as Status,case do.Pre_Allocation  when 'NO DDS' then (case do.Flag  when 'YES' then 'NO DDS' when 'OK' then 'Order Deleted' else do.Flag end) when 'NO' then(case dp.Flag when 'YES' then (case dp.Intervention when 'AVAILABLE' then '' when 'EMPTY BAG' then 'Empty Bag' else dp.Intervention end ) when 'NO' then (case dp.Intervention when 'AVAILABLE' then '' when 'EMPTY BAG' then 'Empty Bag' when 'MANUAL PK' then 'Manual' else dp.Intervention end )else  do.Pre_Allocation end) else '' end as Intervention,CONVERT(varchar,do.Transaction_Date,108) as OrderTime,CONVERT(varchar,do.Opas_Received_Date,108) as Opasreceivedtime,CONVERT(varchar,do.Processing_date,108) as OpasProcessingtime  ,'' as Packedtime,'' as Assembledtime,'' as pickedqty,'' as RFID_No,'' as Container_No,dp.Packtype,dp.PackSize,isnull(do.ReProcessed,'') as ReProcessed,ISNULL(CONVERT(varchar,dp.MC_Order_Received_Dt,108),'') as MC_Order_Received_Dt from Drug_Order as do left join HL7_OBX as ob on ob.Filler_Order=do.Filler_Order_Number and ob.Msg_Control_ID=do.MSG_Control_ID left join Drug_Pick as dp  on do.OrderId=dp.OrderID left join Pharmacy as p on p.Location_code=do.Pharmacy_Code where do.Queue_No='" + queueno + "' and do.PatientID='" + patientid + "' and CONVERT(date,do.Transaction_Date)= convert(Date,'" + QDatefrom + "') and do.Pre_Allocation<>'Cancel' and ob.Order_Control<>'CA' and do.Allocation_Status='NEW' and p.Location_Name='" + location + "' and do.Pre_Allocation<>'NO DDS' order by do.Opas_Received_Date desc ";
          
           con.View(commt, grid);
       }

       // * Queue Status Detailed Grid Display For Status All * \\
       public void Quedetailall(GridView grid, string queueno, string patientid, string orderdt, string location)
       {
           datefromfun(orderdt);

           string commt = "select do.Opas_Received_Date,ob.Item_Code,ob.Drug_Name as Item_Name,CONVERT(varchar,ob.Sales_Quantity) as Order_Qty,ob.Sales_UOM as UOM,ob.Machine_Packed_Indicator as Ind,case dp.DDS_Name when 'Manual' then '' else dp.DDS_Name end as DDS_Name,case dp.Cell_Location1 when '0' then '' else dp.Cell_Location1 end as cell_location,case dp.Cartridge_No1 when '0' then '' else dp.Cartridge_No1 end  as cartridge_no, case do.Pre_Allocation  when 'NO DDS' then (case ob.Machine_Packed_Indicator when 'F' then 'Manual' when 'T' then 'Diverted' else 'Diverted' end ) when 'NO' then (case dp.Flag when 'NO' then 'Work In Progress' When 'YES' then 'Pending DDS/BDS' when 'OK' then(case a.Cont_Assembly_Status when 'Finish' then 'Pending Assembly'  when 'ASSEMBLED' then 'Assembled' else a.Cont_Assembly_Status end)   else 'Pre Allocation Pending' end) else 'Pre Allocation Pending' end as Status , case do.Pre_Allocation  when 'NO DDS' then (case do.Flag  when 'YES' then  (case do.Invalid_Status when 'Invalid Queue Number' then 'Invalid Queue Number'  else 'NO DDS' end ) when 'OK' then (case do.Invalid_Status when 'Not Packed' then 'Not Packed' else do.Invalid_Status end ) else do.Flag end)  when 'NO' then( case dp.Flag when 'ok' then(case a.Intervention when 'AVAILABLE' then '' when 'EMPTY BAG' then 'Empty Bag' when 'MANUAL PK' then 'Manual' else a.Intervention end)  when 'YES' then (case dp.Intervention  when 'AVAILABLE' then ''  when 'EMPTY BAG' then 'Empty Bag' else dp.Intervention end )  when 'NO' then (case dp.Intervention  when 'AVAILABLE' then ''  when 'EMPTY BAG' then 'Empty Bag' else dp.Intervention end ) else  do.Pre_Allocation end) else '' end as Intervention,CONVERT(varchar,do.Transaction_Date,108) as OrderTime,CONVERT(varchar,do.Opas_Received_Date,108) as Opasreceivedtime,CONVERT(varchar,do.Processing_date,108) as OpasProcessingtime ,CONVERT(varchar,a.Packed_Date_Time,108) as Packedtime,CONVERT(varchar,a.Cont_Assembled_Date_Time,108) as Assembledtime,case a.Intervention when 'EMPTY BAG' then '0' when 'Order Amended, remove' then(case a.Cartridge_No when '' then (case dp.DDS_Name when 'Manual' then a.Total_Qty else '0' end ) else  a.Total_Qty end ) when 'Order Amended, replace' then(case a.Cartridge_No when '' then (case dp.DDS_Name when 'Manual' then a.Total_Qty else '0' end ) else  a.Total_Qty end )when 'Order Cancelled, remove' then(case a.Cartridge_No when '' then (case dp.DDS_Name when 'Manual' then a.Total_Qty else '0' end ) else  a.Total_Qty end )else  a.Total_Qty end as pickedqty,a.RFID_No,dp.Container_No,dp.Packtype,dp.PackSize,isnull(do.ReProcessed,'') as ReProcessed,ISNULL(CONVERT(varchar,dp.MC_Order_Received_Dt,108),'') as MC_Order_Received_Dt from Drug_Order as do left join HL7_OBX as ob on ob.Filler_Order=do.Filler_Order_Number and ob.Msg_Control_ID=do.MSG_Control_ID left join Drug_Pick as dp  on do.OrderId=dp.OrderID left join Assembly as a on a.Drug_pickid = dp.Drug_pickID left join Pharmacy as p on p.Location_code=do.Pharmacy_Code where do.Queue_No='" + queueno + "' and do.PatientID='" + patientid + "' and CONVERT(date,do.Transaction_Date)= convert(Date,'" + QDatefrom + "') and do.Pre_Allocation<>'Cancel' and ob.Order_Control<>'CA' and p.Location_Name='" + location + "' and do.Allocation_Status<>'Finish' and dp.Status<>'Deleted'  " +
              "union select do.Opas_Received_Date,ob.Item_Code,ob.Drug_Name as Item_Name,CONVERT(varchar,ob.Sales_Quantity) as Order_Qty,ob.Sales_UOM as UOM,ob.Machine_Packed_Indicator as Ind,case dp.DDS_Name when 'Manual' then '' else dp.DDS_Name end as DDS_Name,case dp.Cell_Location1 when '0' then '' else dp.Cell_Location1 end as cell_location,case dp.Cartridge_No1 when '0' then '' else dp.Cartridge_No1 end  as cartridge_no, case do.Pre_Allocation  when 'NO DDS' then (case ob.Machine_Packed_Indicator when 'F' then 'Manual' when 'T' then 'Diverted' else 'Diverted' end ) when 'NO' then (case dp.Flag when 'NO' then 'Work In Progress' When 'YES' then 'Pending DDS/BDS' when 'OK' then(case a.Cont_Assembly_Status when 'Finish' then 'Pending Assembly'  when 'ASSEMBLED' then 'Assembled' else a.Cont_Assembly_Status end)   else 'Pre Allocation Pending' end) else 'Pre Allocation Pending' end as Status , case do.Pre_Allocation  when 'NO DDS' then (case do.Flag  when 'YES' then  (case do.Invalid_Status when 'Invalid Queue Number' then 'Invalid Queue Number'  else 'NO DDS' end ) when 'OK' then (case do.Invalid_Status when 'Not Packed' then 'Not Packed' else do.Invalid_Status end ) else do.Flag end)  when 'NO' then( case dp.Flag when 'ok' then(case a.Intervention when 'AVAILABLE' then '' when 'EMPTY BAG' then 'Empty Bag' when 'MANUAL PK' then 'Manual' else a.Intervention end)  when 'YES' then (case dp.Intervention  when 'AVAILABLE' then ''  when 'EMPTY BAG' then 'Empty Bag' else dp.Intervention end )  when 'NO' then (case dp.Intervention  when 'AVAILABLE' then ''  when 'EMPTY BAG' then 'Empty Bag' else dp.Intervention end ) else  do.Pre_Allocation end) else '' end as Intervention,CONVERT(varchar,do.Transaction_Date,108) as OrderTime,CONVERT(varchar,do.Opas_Received_Date,108) as Opasreceivedtime,CONVERT(varchar,do.Processing_date,108) as OpasProcessingtime ,CONVERT(varchar,a.Packed_Date_Time,108) as Packedtime,CONVERT(varchar,a.Cont_Assembled_Date_Time,108) as Assembledtime,case a.Intervention when 'EMPTY BAG' then '0' when 'Order Amended, remove' then(case a.Cartridge_No when '' then (case dp.DDS_Name when 'Manual' then a.Total_Qty else '0' end ) else  a.Total_Qty end ) when 'Order Amended, replace' then(case a.Cartridge_No when '' then (case dp.DDS_Name when 'Manual' then a.Total_Qty else '0' end ) else  a.Total_Qty end )when 'Order Cancelled, remove' then(case a.Cartridge_No when '' then (case dp.DDS_Name when 'Manual' then a.Total_Qty else '0' end ) else  a.Total_Qty end )else  a.Total_Qty end as pickedqty,a.RFID_No,dp.Container_No,dp.Packtype,dp.PackSize,isnull(do.ReProcessed,'') as ReProcessed,ISNULL(CONVERT(varchar,dp.MC_Order_Received_Dt,108),'') as MC_Order_Received_Dt from Drug_Order as do left join HL7_OBX as ob on ob.Filler_Order=do.Filler_Order_Number and ob.Msg_Control_ID=do.MSG_Control_ID left join Drug_Pick as dp  on do.OrderId=dp.OrderID left join Assembly as a on a.Drug_pickid = dp.Drug_pickID left join Pharmacy as p on p.Location_code=do.Pharmacy_Code where do.Queue_No='" + queueno + "' and do.PatientID='" + patientid + "' and CONVERT(date,do.Transaction_Date)= convert(Date,'" + QDatefrom + "') and do.Pre_Allocation='NO DDS' and ob.Order_Control<>'CA' and p.Location_Name='" + location + "' and do.Allocation_Status='NEW'  " +
               "union all select do.Opas_Received_Date,a.Item_Code,a.Item_Name,CONVERT(varchar,a.Order_Qty),dp.UOM,a.Indicator as Ind ,case a.DDS_Name when 'Manual' then '' else a.DDS_Name end as DDS_Name,case a.cell_location when '0' then '' else a.cell_location end as cell_location,case a.cartridge_no when '0' then '' else a.cartridge_no end  as cartridge_no,case a.Cont_Assembly_Status when 'Finish' then 'Pending Assembly'   when 'ASSEMBLED' then 'Assembled' else a.Cont_Assembly_Status end as Status,case a.Intervention when 'AVAILABLE' then '' when 'EMPTY BAG' then 'Empty Bag' when 'MANUAL PK' then 'Manual' else a.Intervention end as Intervention,CONVERT(varchar,do.Transaction_Date,108) as OrderTime,CONVERT(varchar,do.Opas_Received_Date,108) as Opasreceivedtime,CONVERT(varchar,do.Processing_date,108) as OpasProcessingtime ,CONVERT(varchar,a.Packed_Date_Time,108) as Packedtime,CONVERT(varchar,a.Cont_Assembled_Date_Time,108) as Assembledtime,case a.Intervention when 'EMPTY BAG' then '0' when 'Order Amended, remove' then(case a.Cartridge_No when '' then (case dp.DDS_Name when 'Manual' then a.Total_Qty else '0' end ) else  a.Total_Qty end ) when 'Order Amended, replace' then(case a.Cartridge_No when '' then (case dp.DDS_Name when 'Manual' then a.Total_Qty else '0' end ) else  a.Total_Qty end )when 'Order Cancelled, remove' then(case a.Cartridge_No when '' then (case dp.DDS_Name when 'Manual' then a.Total_Qty else '0' end ) else  a.Total_Qty end )else  a.Total_Qty end as pickedqty,a.RFID_No,a.Container_No,dp.Packtype,dp.PackSize,isnull(do.ReProcessed,'') as ReProcessed,ISNULL(CONVERT(varchar,dp.MC_Order_Received_Dt,108),'') as MC_Order_Received_Dt from Assembly as a left join Drug_Pick as dp on dp.Drug_pickID=a.Drug_pickid left join Drug_Order as do on do.OrderId=dp.OrderID left join Pharmacy as p on p.PharmacyID =a.PharmacyID where a.Queue_No='" + queueno + "' and a.PatientID='" + patientid + "' and p.Location_Name='" + location + "' and CONVERT(date,do.Transaction_Date)= convert(Date,'" + QDatefrom + "') and do.Allocation_Status='Finish' order by do.Opas_Received_Date desc";
           
          con.View(commt, grid);
       }



       // * No DDS Grid DIsplay * \\
       public void Noddsgrid(GridView grid, string location,string datefrom,string dateto,string ord)
       {
           datefromfun(datefrom);
           datetofun(dateto);
           string commt = "Select distinct do.Queue_No,do.PatientID,CONVERT(varchar,do.Transaction_Date,114) as ordertime,case do.Flag when 'OK' then (case do.Pre_Allocation when 'NO DDS' then 'Diverted' else ''  end) end as Pre_Allocation   from Drug_Order as do left join Pharmacy as p on p.Location_code=do.Pharmacy_Code left join HL7_OBX as ob on ob.Msg_Control_ID=do.MSG_Control_ID and do.Filler_Order_Number=ob.Filler_Order where do.Pre_Allocation='NO DDS' and p.Location_Name='" + location + "' and do.OrderDate =convert(Date,'" + QDatefrom + "')  and ob.Order_Control<>'CA'" + ord;
           con.View(commt, grid);
       }

       // * No DDS Grid DIsplay  with Patient ID* \\
       public void Noddsgridpatientid(GridView grid, string location, string datefrom, string dateto,string patientid)
       {
           datefromfun(datefrom);
           datetofun(dateto);
           string commt = "Select distinct do.Queue_No,do.PatientID,CONVERT(varchar,do.Transaction_Date,114) as ordertime,case do.Flag when 'OK' then (case do.Pre_Allocation when 'NO DDS' then 'Diverted' else ''  end) end as Pre_Allocation from Drug_Order as do left join Pharmacy as p on p.Location_code=do.Pharmacy_Code left join HL7_OBX as ob on ob.Msg_Control_ID=do.MSG_Control_ID and do.Filler_Order_Number=ob.Filler_Order where do.Pre_Allocation='NO DDS' and p.Location_Name='" + location + "' and CONVERT(date,do.OrderDate) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and do.PatientID='" + patientid + "'and ob.Order_Control<>'CA' order by ordertime desc ";
           con.View(commt, grid);
       }

       // * No DDS Grid DIsplay  with Queue Number* \\
       public void Noddsgridqueuno(GridView grid, string location, string datefrom, string dateto, string queueno)
       {
           datefromfun(datefrom);
           datetofun(dateto);
           string commt = "Select distinct do.Queue_No,do.PatientID,CONVERT(varchar,do.Transaction_Date,114) as ordertime,case do.Flag when 'OK' then (case do.Pre_Allocation when 'NO DDS' then 'Diverted' else ''  end) end as Pre_Allocation from Drug_Order as do left join Pharmacy as p on p.Location_code=do.Pharmacy_Code left join HL7_OBX as ob on ob.Msg_Control_ID=do.MSG_Control_ID and do.Filler_Order_Number=ob.Filler_Order where do.Pre_Allocation='NO DDS' and p.Location_Name='" + location + "' and CONVERT(Date,do.OrderDate) between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and do.Queue_No='" + queueno + "'and ob.Order_Control<>'CA' order by ordertime desc ";
           con.View(commt, grid);
       }

       // * No DDS Grid DIsplay  with Patient Name* \\
       public void Noddsgridpatname(GridView grid, string location, string datefrom, string dateto, string patname)
       {
           datefromfun(datefrom);
           datetofun(dateto);
           string commt = "Select distinct do.Queue_No,do.PatientID,CONVERT(varchar,do.Transaction_Date,114) as ordertime,case do.Flag when 'OK' then (case do.Pre_Allocation when 'NO DDS' then 'Diverted' else ''  end) end as Pre_Allocation  from Drug_Order as do left join Pharmacy as p on p.Location_code=do.Pharmacy_Code left join HL7_OBX as ob on ob.Msg_Control_ID=do.MSG_Control_ID and do.Filler_Order_Number=ob.Filler_Order left join HL7_PID as pid on pid.PatientID=do.PatientID  where do.Pre_Allocation='NO DDS' and p.Location_Name='" + location + "' and do.OrderDate between convert(Date,'" + QDatefrom + "') and convert(Date,'" + Qdateto + "') and  pid.Patient_Name_FN1='" + patname + "' and ob.Order_Control<>'CA' order by ordertime desc";
           con.View(commt, grid);
       }

       // * Invalid Order Close * \\

       

       // * No DDS Detail Grid Display * \\
       public void Noddsgriddetail(GridView grid, string location,string datefrom,string queueno,string patientid)
       {
           datefromfun(datefrom);
           string commt = "Select do.Queue_No,do.PatientID,ob.Item_Code,ob.Drug_Name,ob.Sales_Quantity,ob.Machine_Packed_Indicator as Ind,case do.Flag when 'OK' then (case do.Invalid_Status when 'Not Packed' then 'Not Packed' else do.Invalid_Status end ) else '' end as Pre_Allocation  from Drug_Order as do left join Pharmacy as p on p.Location_code=do.Pharmacy_Code left join HL7_OBX as ob on ob.Msg_Control_ID=do.MSG_Control_ID and do.Filler_Order_Number=ob.Filler_Order where do.Pre_Allocation='NO DDS' and p.Location_Name='" + location + "' and do.OrderDate=convert(Date,'" + QDatefrom + "') and do.Queue_No='" + queueno + "' and do.PatientID='" + patientid + "'  and ob.Order_Control<>'CA' order by do.Opas_Received_Date asc";          
           con.View(commt, grid);
       }     


        // * PRINT PRESCRIPTION DRUG LABEL * \\


       //* Queue Numbe Range Status all * \\
       public void ppdlall(GridView grid, string location, string queuenofrom, string queuenoto, string DDSname)
       {           
           string commt = "select do.OrderId,dp.DDS_Name,do.Queue_No,ob.Drug_Name as Item_Name, case dp.Flag when 'NO' then 'Work In Progress' When 'YES' then 'Pending DDS/BDS' when 'OK' then(case a.Cont_Assembly_Status when 'Finish' then 'Pending Assembly'  when 'ASSEMBLED' then 'Assembled' else a.Cont_Assembly_Status end)  else 'Pre Allocation Pending' end as Status from Drug_Order as do left join HL7_OBX as ob on ob.Filler_Order=do.Filler_Order_Number and ob.Msg_Control_ID=do.MSG_Control_ID left join Drug_Pick as dp  on do.OrderId=dp.OrderID left join Assembly as a on a.Drug_pickid = dp.Drug_pickID left join Pharmacy as p on p.Location_code=do.Pharmacy_Code where do.Queue_No between CONVERT(int,'" + queuenofrom + "')  and CONVERT(int,'" + queuenoto + "') and do.OrderDate=CONVERT(varchar,getdate(),101) and do.Pre_Allocation<>'Cancel' and ob.Order_Control<>'CA' and ob.Machine_Packed_Indicator='T' and p.Location_Name='" + location + "' and do.Allocation_Status<>'Finish' and dp.DDS_Name like '" + DDSname + "%' and do.Invalid_Status is null union all select do.OrderId,dp.DDS_Name,do.Queue_No,a.Item_Name,case a.Cont_Assembly_Status when 'Finish' then 'Pending Assembly' when 'ASSEMBLED' then 'Assembled' else a.Cont_Assembly_Status end as Status from Assembly as a left join Drug_Pick as dp on dp.Drug_pickID=a.Drug_pickid left join Drug_Order as do on do.OrderId=dp.OrderID left join Pharmacy as p on p.PharmacyID =a.PharmacyID where a.Queue_No between CONVERT(int,'" + queuenofrom + "')  and CONVERT(int,'" + queuenoto + "') and p.Location_Name='" + location + "' and do.OrderDate=CONVERT(varchar,getdate(),101) and a.Indicator='T' and a.DDS_Name like '" + DDSname + "%' and do.Invalid_Status is null union all select do.OrderId,'NO DDS' as DDS_Name,do.Queue_No,ob.Drug_Name as Item_Name,'NO DDS' as Status from Drug_Order as do left join HL7_OBX as ob on ob.Filler_Order=do.Filler_Order_Number and ob.Msg_Control_ID=do.MSG_Control_ID left join Pharmacy as p on p.Location_code=do.Pharmacy_Code where do.Queue_No between CONVERT(int,'" + queuenofrom + "')  and CONVERT(int,'" + queuenoto + "') and do.OrderDate=CONVERT(varchar,getdate(),101) and do.Pre_Allocation='NO DDS' and do.Flag='YES'  and p.Location_Name='" + location + "' and ob.Machine_Packed_Indicator='T' and do.Invalid_Status is null";
           con.View(commt, grid);
       }

       // * Queue Numbe Range pending DDS * \\
       public void printpendingrange(GridView grid, string location, string queuenofrom, string queuenoto, string DDSname)
       {
           string commt = "select do.OrderId,dp.DDS_Name,do.Queue_No,ob.Drug_Name as Item_Name, 'Pending DDS/BDS' as status from Drug_Order as do left join HL7_OBX as ob on ob.Filler_Order=do.Filler_Order_Number and ob.Msg_Control_ID=do.MSG_Control_ID left join Drug_Pick as dp  on do.OrderId=dp.OrderID left join Assembly as a on a.Drug_pickid = dp.Drug_pickID left join Pharmacy as p on p.Location_code=do.Pharmacy_Code where do.Queue_No between CONVERT(int,'" + queuenofrom + "')  and CONVERT(int,'" + queuenoto + "') and do.OrderDate=CONVERT(varchar,getdate(),101) and dp.Status<>'Finish' and dp.Flag='YES' and do.Pre_Allocation<>'Cancel' and ob.Order_Control<>'CA' and ob.Machine_Packed_Indicator='T' and p.Location_Name='" + location + "' and dp.DDS_Name like '" + DDSname + "%' and do.Invalid_Status is null";
           con.View(commt, grid);
       }

       // * Queue Numbe Range WIP In DDS * \\
       public void printwiprange(GridView grid, string location, string queuenofrom, string queuenoto, string DDSname)
       {
           string commt = "select do.OrderId,dp.DDS_Name,do.Queue_No,ob.Drug_Name as Item_Name,'DDS Work in Progress' as status from Drug_Order as do left join HL7_OBX as ob on ob.Filler_Order=do.Filler_Order_Number and ob.Msg_Control_ID=do.MSG_Control_ID left join Drug_Pick as dp  on do.OrderId=dp.OrderID left join Assembly as a on a.Drug_pickid = dp.Drug_pickID left join Pharmacy as p on p.Location_code=do.Pharmacy_Code where do.Queue_No between CONVERT(int,'" + queuenofrom + "')  and CONVERT(int,'" + queuenoto + "') and do.OrderDate=CONVERT(varchar,getdate(),101) and dp.Status <>'Finish' and dp.Flag='NO' and do.Pre_Allocation<>'Cancel' and ob.Order_Control<>'CA' and ob.Machine_Packed_Indicator='T' and p.Location_Name='" + location + "' and dp.DDS_Name like '" + DDSname + "%' and do.Invalid_Status is null";
           con.View(commt, grid);
       }

       // * Queue Numbe Range pending Assembly * \\
       public void printpenassemblyrange(GridView grid, string location, string queuenofrom, string queuenoto, string DDSname)
       {
           string commt = "select do.OrderId,dp.DDS_Name,do.Queue_No,ob.Drug_Name as Item_Name,'Pending Assembly' as status from Drug_Order as do left join HL7_OBX as ob on ob.Filler_Order=do.Filler_Order_Number and ob.Msg_Control_ID=do.MSG_Control_ID left join Drug_Pick as dp  on do.OrderId=dp.OrderID left join Assembly as a on a.Drug_pickid = dp.Drug_pickID left join Pharmacy as p on p.Location_code=do.Pharmacy_Code where do.Queue_No between CONVERT(int,'" + queuenofrom + "')  and CONVERT(int,'" + queuenoto + "') and do.OrderDate=CONVERT(varchar,getdate(),101) and dp.Status='Finish' and a.Cont_Assembly_Status='Finish'  and do.Pre_Allocation<>'Cancel' and ob.Order_Control<>'CA' and ob.Machine_Packed_Indicator='T' and p.Location_Name='" + location + "' and dp.DDS_Name like '" + DDSname + "%' and do.Invalid_Status is null";
           con.View(commt, grid);
       }

       // * Queue Numbe Range Assembled * \\
       public void printassembledrange(GridView grid, string location, string queuenofrom, string queuenoto,string DDSname)
       {
           string commt = "select do.OrderId,dp.DDS_Name,do.Queue_No,ob.Drug_Name as Item_Name,'Assembled' as status from Drug_Order as do left join HL7_OBX as ob on ob.Filler_Order=do.Filler_Order_Number and ob.Msg_Control_ID=do.MSG_Control_ID left join Drug_Pick as dp  on do.OrderId=dp.OrderID left join Assembly as a on a.Drug_pickid = dp.Drug_pickID left join Pharmacy as p on p.Location_code=do.Pharmacy_Code where do.Queue_No between CONVERT(int,'" + queuenofrom + "')  and CONVERT(int,'" + queuenoto + "') and do.OrderDate=CONVERT(varchar,getdate(),101) and dp.Status='Finish' and a.Cont_Assembly_Status='ASSEMBLED'  and do.Pre_Allocation<>'Cancel' and ob.Order_Control<>'CA' and ob.Machine_Packed_Indicator='T' and p.Location_Name='" + location + "' and dp.DDS_Name like '" + DDSname + "%' and do.Invalid_Status is null";
           con.View(commt, grid);
       }

       // * Queue Number Range Invalid orders

       public void printinvalidorder(GridView grid, string location, string queuenofrom, string queuenoto, string DDSname)
       {
           string commt = "select do.OrderId,'NO DDS' as DDS_Name,do.Queue_No,ob.Drug_Name as Item_Name,'NO DDS' as Status from Drug_Order as do left join HL7_OBX as ob on ob.Filler_Order=do.Filler_Order_Number and ob.Msg_Control_ID=do.MSG_Control_ID left join Pharmacy as p on p.Location_code=do.Pharmacy_Code where do.Queue_No between CONVERT(int,'" + queuenofrom + "')  and CONVERT(int,'" + queuenoto + "') and do.OrderDate=CONVERT(varchar,getdate(),101) and do.Pre_Allocation='NO DDS' and do.Flag='YES'  and p.Location_Name='" + location + "' and ob.Machine_Packed_Indicator='T' and do.Invalid_Status is null";
           con.View(commt, grid);
       }

       #endregion

    }
}
