<%@ WebHandler Language="C#" Class="Pre_Loaded_Cartridge" %>

using System;
using System.Web;
using System.Data;

public class Pre_Loaded_Cartridge : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string location = context.Request.Params.Get("location");
        string Itemcode = context.Request.Params.Get("Itemcode");
        string Drugcode = context.Request.Params.Get("Drugcode");
        string ItemName = context.Request.Params.Get("itemname");
        string BrandName = context.Request.Params.Get("Brandname");
        string MFRCode = context.Request.Params.Get("Barcode");
        string CartridgeNo = context.Request.Params.Get("Cartridgeno");
        string statu = context.Request.Params.Get("status"); 
        context.Response.ContentType = "text/xml";
        context.Response.AddHeader("Content-Disposition", "attachment; filename=PreLoaded-report" + ".xls");
        //context.Response.AddHeader("Content-Disposition", "attachment; filename=PreLoaded-Cartridge-report"+ ".xml");
        context.Response.Write("<?xml version=\"1.0\"?>\x0d\x0a");
        context.Response.Write("<?mso-application progid=\"Excel.Sheet\"?>\x0d\x0a");
        context.Response.Write("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\x0d\x0a");
        context.Response.Write(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"\x0d\x0a");
        context.Response.Write(" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"\x0d\x0a");
        context.Response.Write(" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"\x0d\x0a");
        context.Response.Write(" xmlns:html=\"http://www.w3.org/TR/REC-html40\">\x0d\x0a");
        context.Response.Write(" <Worksheet ss:Name=\"Cartridge Status " + " for " + "\">\x0d\x0a");
        context.Response.Write("  <Table>\x0d\x0a");
        context.Response.Write("   <Column ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>\x0d\x0a");
        context.Response.Write("   <Column ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>\x0d\x0a");       

        string[] seq = new string[]
        {   
            "Cartridge No","Item Code", "Item Name", 
            "Brand Name", "Pack Type", "Pack Size", "UOM", "Status",
            "PreLoaded By", "PreLoaded Date Time","First Verified by","First Verified Date Time",
            "Second Verified by","Second Verified Date Time"
        };

        AppDataTableAdapters.Export_Excel_Preloaded_Cartridge_StatusTableAdapter pre = new AppDataTableAdapters.Export_Excel_Preloaded_Cartridge_StatusTableAdapter();
        System.Data.DataTable dt = pre.GetData(location, Itemcode, Drugcode, ItemName, BrandName, MFRCode, CartridgeNo, statu);

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
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}