<%@ WebHandler Language="C#" Class="Cartridge_Status_Excel" %>

using System;
using System.Web;
using System.Data;
public class Cartridge_Status_Excel : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        string ddsname = context.Request.Params.Get("DDS");
        string location = context.Request.Params.Get("location");
        string ItemName = context.Request.Params.Get("itemname");
        string statu = context.Request.Params.Get("status");
        context.Response.ContentType = "text/xml";
        context.Response.AddHeader("Content-Disposition", "attachment; filename=Cartridge-Status-report-" + ddsname + ".xls");
        //context.Response.AddHeader("Content-Disposition", "attachment; filename=Cartridge-Status-report-" + ddsname + ".xml");
        context.Response.Write("<?xml version=\"1.0\"?>\x0d\x0a");
        context.Response.Write("<?mso-application progid=\"Excel.Sheet\"?>\x0d\x0a");
        context.Response.Write("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\x0d\x0a");
        context.Response.Write(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"\x0d\x0a");
        context.Response.Write(" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"\x0d\x0a");
        context.Response.Write(" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"\x0d\x0a");
        context.Response.Write(" xmlns:html=\"http://www.w3.org/TR/REC-html40\">\x0d\x0a");
        context.Response.Write(" <Worksheet ss:Name=\"Cartridge Status " + " for " + ddsname + "\">\x0d\x0a");
        context.Response.Write("  <Table>\x0d\x0a");
        context.Response.Write("   <Column ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>\x0d\x0a");
        context.Response.Write("   <Column ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>\x0d\x0a");

        string[] seq = new string[]
        {   
            "DDS_Name", "CellNo", "CartridgeNo", "Item_Code", 
            "Item_Name","CurrentQuantity", "PackType", "Pack_Size", "UOM", 
            "Expiry_Date", "Status","Reason"
        };
        //if (statu != "Empty")
        //{           
            AppDataTableAdapters.Export_Excel_Cartridge_StatusTableAdapter ex1 = new AppDataTableAdapters.Export_Excel_Cartridge_StatusTableAdapter();
            System.Data.DataTable dt = ex1.GetData(ddsname, location, ItemName, statu);



            context.Response.Write("   <Row>\x0d\x0a");
            foreach (string cn in seq)
            {
                context.Response.Write("    <Cell><Data ss:Type=\"String\">");
                context.Response.Write(cn);
                context.Response.Write("</Data></Cell>\x0d\x0a");
            }
            context.Response.Write("   </Row>\x0d\x0a");

            foreach (System.Data.DataRow rw in dt.Rows)
            {
                context.Response.Write("   <Row>\x0d\x0a");
                foreach (string cn in seq)
                {
                    context.Response.Write("    <Cell><Data ss:Type=\"String\">");
                    context.Response.Write(rw[cn]);
                    context.Response.Write("</Data></Cell>\x0d\x0a");
                }
                context.Response.Write("   </Row>\x0d\x0a");
            }

            context.Response.Write("  </Table>\x0d\x0a");
            context.Response.Write(" </Worksheet>\x0d\x0a");
            context.Response.Write("</Workbook>\x0d\x0a");

            //context.Response.Redirect("~/Cartridgestatus.aspx");
        //}
        //else
        //{
        //    AppDataTableAdapters.CellTableAdapter cel = new AppDataTableAdapters.CellTableAdapter();
        //    System.Data.DataTable dt = cel.GetData(ddsname);
        //    context.Response.Write("   <Row>\x0d\x0a");
        //    foreach (string cn in seq)
        //    {
        //        context.Response.Write("    <Cell><Data ss:Type=\"String\">");
        //        context.Response.Write(cn);
        //        context.Response.Write("</Data></Cell>\x0d\x0a");
        //    }
        //    context.Response.Write("   </Row>\x0d\x0a");

        //    foreach (System.Data.DataRow rw in dt.Rows)
        //    {
        //        context.Response.Write("   <Row>\x0d\x0a");
        //        foreach (string cn in seq)
        //        {
        //            context.Response.Write("    <Cell><Data ss:Type=\"String\">");
        //            context.Response.Write(rw[cn]);
        //            context.Response.Write("</Data></Cell>\x0d\x0a");
        //        }
        //        context.Response.Write("   </Row>\x0d\x0a");
        //    }

        //    context.Response.Write("  </Table>\x0d\x0a");
        //    context.Response.Write(" </Worksheet>\x0d\x0a");
        //    context.Response.Write("</Workbook>\x0d\x0a");
        //}
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}