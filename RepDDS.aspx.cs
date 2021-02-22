using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Microsoft.Reporting.WebForms;
using Datalayer;

public partial class RepDDS : System.Web.UI.Page
{
    private void Page_Init(System.Object sender, System.EventArgs e)
    {
        //Repdds.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        //Repdds.ShowCredentialPrompts = true;
        //Repdds.ServerReport.ReportServerCredentials = new ReportServerCredentials();
        //Repdds.ServerReport.ReportServerUrl = new System.Uri("http://adgetech/ReportServer");
        //Repdds.ServerReport.ReportPath = "/OPAS Report One/DDSReport";
        //Repdds.ServerReport.Refresh();
    } 

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}