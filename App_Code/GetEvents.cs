using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using DAL;
using BLL;

/// <summary>
/// Summary description for GetEvents
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class GetEvents : System.Web.Services.WebService
{
    CalendarEventBLL calendarevent;
    public GetEvents()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [Serializable()]
    public class clsevents
    {
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public clsevents(string Title, DateTime Start, DateTime End)
        {
            title = Title;
            start = Start;
            end = End;
        }
    }

    [WebMethod]
    List<clsevents> getListOfEvents()
    {
        calendarevent = new CalendarEventBLL();
        List<clsevents> lst = new List<clsevents>();

        DataTable tb = calendarevent.getEventsByUserID(Session.GetCurrentUser().UserID);
        foreach (DataRow r in tb.Rows)
        {
            lst.Add(new clsevents (
               (string)r["title"],
               (DateTime)r["event_start"],
               (DateTime)r["event_end"]
                ));

        }
        return lst;
    }
}
