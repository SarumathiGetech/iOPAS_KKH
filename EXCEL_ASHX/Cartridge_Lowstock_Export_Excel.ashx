<%@ WebHandler Language="C#" Class="Cartridge_Lowstock_Export_Excel" %>

using System;
using System.Web;


public class Cartridge_Lowstock_Export_Excel : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        string location = context.Request.Params.Get("location");
        string DDSName = context.Request.Params.Get("DDS");
        
        context.Response.ContentType = "text/xml";
        context.Response.AddHeader("Content-Disposition", "attachment; filename="+DDSName+"-" + "Cartridge-Lowstock" + ".xls");    
        context.Response.Write("<?xml version=\"1.0\"?>\x0d\x0a");
        context.Response.Write("<?mso-application progid=\"Excel.Sheet\"?>\x0d\x0a");
        context.Response.Write("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\x0d\x0a");
        context.Response.Write("xmlns:o=\"urn:schemas-microsoft-com:office:office\"\x0d\x0a");
        context.Response.Write("xmlns:x=\"urn:schemas-microsoft-com:office:excel\"\x0d\x0a");
        context.Response.Write("xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"\x0d\x0a");
        context.Response.Write("xmlns:html=\"http://www.w3.org/TR/REC-html40\">\x0d\x0a");
        context.Response.Write("<Worksheet ss:Name=\"Cartridge Low Stock " + " for " + "\">\x0d\x0a");
        context.Response.Write("<Table>\x0d\x0a");
        context.Response.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>\x0d\x0a");
        context.Response.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>\x0d\x0a");

        string[] seq = new string[]
        {   
            "Item_Code","Item_Name", "DDS_Name", 
            "Cell_No", "Cartridge_Quantity", "Cartridge_Max_Qty", "DDS_Quantity", "Pack_Type",
            "Pack_Size", "UOM"
        };


        AppDataTableAdapters.LowstockTableAdapterTableAdapter low = new AppDataTableAdapters.LowstockTableAdapterTableAdapter();
            
            System.Data.DataTable dt = low.GetData(DDSName, location);
       

        context.Response.Write("<Row>\x0d\x0a");
        foreach (string cn in seq)
        {
            context.Response.Write("<Cell><Data ss:Type=\"String\">");
            context.Response.Write(cn);
            context.Response.Write("</Data></Cell>\x0d\x0a");
        }
        context.Response.Write("</Row>\x0d\x0a");

        foreach (System.Data.DataRow rw in dt.Rows)
        {
            context.Response.Write("<Row>\x0d\x0a");
            foreach (string cn in seq)
            {
                context.Response.Write("<Cell><Data ss:Type=\"String\">");
                context.Response.Write(rw[cn]);
                context.Response.Write("</Data></Cell>\x0d\x0a");
            }
            context.Response.Write("</Row>\x0d\x0a");
        }
        context.Response.Write("</Table>\x0d\x0a");
        context.Response.Write("</Worksheet>\x0d\x0a");
        context.Response.Write("</Workbook>\x0d\x0a");
      
      
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
}