using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;

namespace Datalayer
{
  public class UOM
    {
        DB_Connection con = new DB_Connection();
        SqlCommand cmd = new SqlCommand();

        // * UOM Mapping Insert* \\
        public int Uom_Mapping_Insert(string Location, string ItemCode,int CurrentQty, string CurrentUom, int ConversionQty,string ConversionUom,string ActInc, string Createdby)
        {
            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "UOM_Mapping_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PharmacyLocation", SqlDbType.VarChar, 50).Value = Location.ToString();
            cmd.Parameters.Add("@ItemCode", SqlDbType.VarChar, 20).Value = ItemCode.ToString();
            cmd.Parameters.Add("@CurrentQty", SqlDbType.Int).Value = CurrentQty;
            cmd.Parameters.Add("@CurrentUom", SqlDbType.VarChar, 10).Value = CurrentUom.ToString();
            cmd.Parameters.Add("@ConversionQty", SqlDbType.Int).Value = ConversionQty;
            cmd.Parameters.Add("@ConversionUom", SqlDbType.VarChar, 10).Value = ConversionUom.ToString();
            cmd.Parameters.Add("@Active", SqlDbType.VarChar, 10).Value = ActInc.ToString();
            cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 20).Value = Createdby.ToString();
            SqlParameter par = new SqlParameter();
            par.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(par);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            int retvalue = (int)par.Value;
            return retvalue;
        }


        // * UOM Mapping Update * \\
        public int Uom_Mapping_Update(Int64 MapID,string Location, string ItemCode, int CurrentQty, string CurrentUom, int ConversionQty, string ConversionUom, string ActInc, string Createdby)
        {
            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "UOM_Mapping_Update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MapID", SqlDbType.BigInt).Value = MapID;
            cmd.Parameters.Add("@PharmacyLocation", SqlDbType.VarChar, 50).Value = Location.ToString();
            cmd.Parameters.Add("@ItemCode", SqlDbType.VarChar, 20).Value = ItemCode.ToString();
            cmd.Parameters.Add("@CurrentQty", SqlDbType.Int).Value = CurrentQty;
            cmd.Parameters.Add("@CurrentUom", SqlDbType.VarChar, 10).Value = CurrentUom.ToString();
            cmd.Parameters.Add("@ConversionQty", SqlDbType.Int).Value = ConversionQty;
            cmd.Parameters.Add("@ConversionUom", SqlDbType.VarChar, 10).Value = ConversionUom.ToString();
            cmd.Parameters.Add("@Active", SqlDbType.VarChar, 10).Value = ActInc.ToString();
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 20).Value = Createdby.ToString();
            SqlParameter par = new SqlParameter();
            par.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(par);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            int retvalue = (int)par.Value;
            return retvalue;
        }


        public void Uomgridall(GridView grid, string ItemCode)
        {
            string Commt = "select um.MapID ,p.Location_Name,il.UOM as Current_Uom,um.Current_Qty,um.Conversion_Qty,um.Conversion_Uom,um.Act_Inc_Status as Status,um.Created_By,um.Created_Date,um.Updated_By,um.Updated_Date  from Uom_Mapping as um left join Item_Master as im on im.MasterID=um.MasterID left join Item_Location as il on il.ID=um.UomID left join Pharmacy as p on p.PharmacyID=um.PharmacyID where im.Item_Code='" + ItemCode + "'";
            con.View(Commt, grid);
        }
    }
}
