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

public partial class Calander : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string scriptStr = "javascript:return popUpCalendar(this," + getClientID() + @", 'dd/mm/yyyy', '__doPostBack(\'" + getClientID() + @"\')')";
        imgCalendar.Attributes.Add("onclick", scriptStr);
    }
    public string getClientID()
    {
        return txt_Date.ClientID;
    }

    // This propery sets/gets the calendar date
    public string CalendarDate
    {
        get
        {
            return txt_Date.Text;
        }
        set
        {
            txt_Date.Text = value;
        }
    }
    // This Property sets or gets the the label for 
  
    //public string Text
    //{
    //    get
    //    {
    //        return lbl_Date.Text;
    //    }
    //    set
    //    {
    //        lbl_Date.Text = value;
    //    }
    //}

}

