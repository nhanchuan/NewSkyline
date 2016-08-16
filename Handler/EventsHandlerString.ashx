<%@ WebHandler Language="C#" Class="EventsHandlerString" %>

using System;
using System.Web;

public class EventsHandlerString : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

            string jsonStr = "[{";

        jsonStr += "title: 'SFDHGHFGJ,";
        jsonStr += "start: new Date(y, m, 1),";
        jsonStr += "backgroundColor: Metronic.getBrandColor('yellow')";

        jsonStr += "}]";

    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}