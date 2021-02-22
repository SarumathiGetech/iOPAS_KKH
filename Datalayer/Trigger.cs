using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Datalayer
{
     
  public class Trigger
    {
        DB_Connection con = new DB_Connection();
        SqlCommand cmd = new SqlCommand();


      // * Trigger Master Insert Procedure * \\
        public int Triggerinsert(string location, string mode,string createdby)
        {
            SqlConnection conn = con.getstring();
            conn.Close();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "RFID_Master_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PharmacyLocation", SqlDbType.VarChar, 50).Value = location.ToString();
            cmd.Parameters.Add("@Mode", SqlDbType.VarChar, 10).Value = mode.ToString();
            cmd.Parameters.Add("@Createdby", SqlDbType.VarChar, 10).Value = createdby.ToString();
            SqlParameter par = new SqlParameter();
            par.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(par);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            int retvalue = (int)par.Value;
            return retvalue;
        }

      // * Trigger Master Grid Display * \\

      public void griddisp(GridView grid)
      {
          string commt = "select t.Mode,p.Location_Name,t.Createdby,convert(varchar(20),t.Createddate,113) as Createddate,t.Updatedby,convert(varchar(20),t.Updateddate,113) as Updateddate from Trigger_Master as t left join Pharmacy as p on p.PharmacyID=t.PharmacyID order by t.Updateddate desc";
          con.View(commt, grid);
      }

      // * Trigger Master reader * \\
      //public SqlDataReader trigerreader(string location)
      //{
      //    string cmd = "select  t.Mode from Trigger_Master as t left join Pharmacy as p on p.PharmacyID=t.PharmacyID where p.Location_Name='" + location + "'";
      //    SqlDataReader dr = con.GetDataBase(cmd);
      //    return dr;
      //}


    }
}
