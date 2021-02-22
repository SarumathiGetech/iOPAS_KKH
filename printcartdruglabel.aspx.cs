using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using Datalayer;
public partial class printcartdruglabel : System.Web.UI.Page
{
    DB_Connection DBCon = new DB_Connection();
    SqlCommand cmd = new SqlCommand();
    smsclass sms = new smsclass();
    Print pr = new Print();
    string sessionuserid="", location = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sessionuserid = Session["Userid"].ToString();
            location = Session["location"].ToString();
            if (location != "")
            {
                if (!IsPostBack)
                {
                    btnsearch.Attributes.Add("onclick", "itemsearch() ;return false;");
                    defaultprinter();
                }
            }
            else if (location == "" || location == null)
            {
                pp.Visible = false;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('This workstation is not authorised to do label printing');</script>", false);
                return;
            }

            txtitemcode.Attributes.Add("onKeyPress", "doClick2(event)");
            txtdrugcode.Attributes.Add("onKeyPress", "doClick2(event)");
            txtitemname.Attributes.Add("onKeyPress", "doClick2(event)");
            txtbrand.Attributes.Add("onKeyPress", "doClick2(event)");
            txtmfrcode.Attributes.Add("onKeyPress", "doClick2(event)");  
        }
        catch (NullReferenceException)
        {
            //Response.Redirect("opas.html?Session=End");
            Response.Redirect("iopas.html");
        }

       
        
    }

    protected void btnok_Click(object sender, EventArgs e)
    {
        codereader();    
        
    }

    // * Itemcode,Name Drugcode Reader * \\
    public void codereader()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select i.item_code,i.item_name,i.drug_code,pm.PackType,iu.Pack_Size,IL.UOM,b.brandname from Item_Master as i left join Item_user_Master as iu on iu.MasterID=i.MasterID left join Item_Location as IL on IL.MasterID=i.MasterID left join Brand_Master as b on b.BrandID=iu.Brandid left join Packtype_Master as pm on pm.ID=iu.PacktypeID where iu.ID='" + Convert.ToInt32(searchvalue.Text) + "'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtitemcode.Text = dr[0].ToString();
                        txtitemname.Text = dr[1].ToString();
                        txtdrugcode.Text = dr[2].ToString();
                        txtpacktype.Text = dr[3].ToString();
                        txtpacksize.Text = dr[4].ToString();
                        txtuom.Text = dr[5].ToString();
                        txtbrand.Text = dr[6].ToString();
          
                    }
                }
            }
        }
    }
    //protected void btnclear_Click(object sender, EventArgs e)
    protected void btnclear_Click(object sender, ImageClickEventArgs e)
    {
        Clear();
    }

    // * text box Clear * \\
    public void Clear()
    {
        txtitemcode.Text = "";
        txtitemname.Text = "";
        txtdrugcode.Text = "";
        txtbrand.Text = "";
        txtmfrcode.Text = "";
        txtpacksize.Text = "";
        txtpacktype.Text = "";
        txtuom.Text = "";
    }
    //protected void btnsave_Click(object sender, EventArgs e)
    protected void btnsave_Click(object sender, ImageClickEventArgs e)
    {
        string barcode = "";
        if (searchvalue.Text.Length == 1)
        {
            barcode="000000000-"+searchvalue.Text.Trim();
        }
        else if (searchvalue.Text.Length == 2)
        {
            barcode = "00000000-" + searchvalue.Text.Trim();
        }
        else if (searchvalue.Text.Length == 3)
        {
            barcode = "0000000-" + searchvalue.Text.Trim();
        }
        else if (searchvalue.Text.Length == 4)
        {
            barcode = "000000-" + searchvalue.Text.Trim();
        }
        else if (searchvalue.Text.Length == 5)
        {
            barcode = "00000-" + searchvalue.Text.Trim();
        }
        else if (searchvalue.Text.Length == 6)
        {
            barcode = "0000-" + searchvalue.Text.Trim();
        }
        pr.PrintCartridgeDrugLabel(txtitemcode.Text.Trim(), txtitemname.Text.Trim(), txtbrand.Text.Trim(), txtpacktype.Text.Trim(), txtpacksize.Text.Trim(), txtuom.Text.Trim(), barcode, ddlprintername.SelectedValue.ToString());
       
        transactioninsert();
    }

    // Default Printer Reader \\
    public void defaultprinter()
    {
        using (SqlConnection con = DBCon.getstring())
        {
            string Commt = "select p.Printer_Name,f.Default_Printer from Printer as p left join Printer_Function as f on f.PrinterID=p.PrinterID left join Pharmacy as ph on ph.PharmacyID=p.PharmacyID where f.Label_Name='CL'and ph.Location_Name='" + location + "' and p.Status='Active'";
            cmd = new SqlCommand(Commt, con);

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr[1].ToString() == "Yes")
                        {
                            ddlprintername.Items.Insert(0, new ListItem(dr[0].ToString()));
                        }
                        else if (dr[1].ToString() == "No")
                        {
                            ddlprintername.Items.Add(dr[0].ToString());
                        }
                    }
                }
            }
        }
    }
    // * Prescription Drug Label Transaction Insert * \\
    public void transactioninsert()
    {
        sms.PrescriptionDLtrans(sessionuserid, location, "Print Cartridge drug Label");
    }

    
}
