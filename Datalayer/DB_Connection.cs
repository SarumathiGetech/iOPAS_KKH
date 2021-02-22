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
   public class DB_Connection
    {
        SqlCommand cmd = new SqlCommand();
        LogWriter log = new LogWriter();
        
      public SqlConnection getstring()
      {
          string constr = (string)ConfigurationManager.ConnectionStrings["OpasConnection"].ConnectionString;
          SqlConnection SQLCon = new SqlConnection(constr);
          return SQLCon;
      }

      public void SetDataBase(string Commt)
      {
          try
          {
              using (SqlConnection con = getstring())
              {
                  //SqlConnection con = getstring();
                  // con.Close();
                  cmd.Parameters.Clear();
                  cmd = new SqlCommand(Commt, con);
                  con.Open();
                  cmd.ExecuteNonQuery();
                  con.Close();
              }
          }
          catch (Exception ex)
          {
              log.logwriter("iOPAS", Commt + "  : " + ex.Message);
          }
      }

        //public SqlDataReader GetDataBase(string Commt)
        //{
        //    SqlDataReader Reader= null;
        //    try
        //    {
        //        SqlConnection con = getstring();
        //        con.Close();
        //        cmd.Parameters.Clear();
        //        cmd = new SqlCommand(Commt, con);
        //        con.Open();
        //        Reader = cmd.ExecuteReader();
        //    }
        //    catch (Exception ex)
        //    {
        //        log.logwriter("iOPAS", Commt + "  : " + ex.Message);
        //    }
        //    return Reader;             
        //}

        public DataSet getdataset(string getdata)
        { DataSet ds = new DataSet();
           
            try
            {
                using (SqlConnection con = getstring())
                {
                    //SqlConnection con = getstring();
                    //con.Close();
                    SqlDataAdapter da = new SqlDataAdapter(getdata, con);
                    con.Open();
                    da.Fill(ds, "getdata");
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                log.logwriter("iOPAS", ex.Message);
            }
            return ds;
        }

        public void View(string Commt, GridView Grid)
        {
            try
            {
                using (SqlConnection con = getstring())
                {
                   // SqlConnection con = getstring();
                    //con.Close();
                    SqlDataAdapter Adapt = new SqlDataAdapter(Commt, con);
                    DataSet DSet = new DataSet();
                    con.Open();
                    Adapt.Fill(DSet);
                    Grid.DataSource = DSet;
                    Grid.DataBind();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                log.logwriter("iOPAS", Commt + "  : " + ex.Message);
            }
        }       
    }
}
