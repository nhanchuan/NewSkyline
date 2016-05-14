﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CalendarEVENT.aspx.cs" Inherits="CalendarEvent_CalendarEVENT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Calendar</title>
    <meta http-equiv="Content-Type" content="text/html" charset="utf-8"/>
    <link href="../css/cupertino/jquery-ui-1.7.3.custom.css" rel="stylesheet" />
    <link href="../fullcalendar/fullcalendar.css" rel="stylesheet" />
    <link href='https://fonts.googleapis.com/css?family=Roboto|Source+Sans+Pro|Open+Sans+Condensed:300&subset=latin,vietnamese,latin-ext' rel='stylesheet' type='text/css' />
    <script src="../jquery/jquery-1.3.2.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="../jquery/jquery-ui-1.7.3.custom.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="../jquery/jquery.qtip-1.0.0-rc3.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="../fullcalendar/fullcalendar.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="scripts/calendarscript.js" type="text/javascript" charset="utf-8"></script>
    <%--<script src="../scripts/calendarEventscript.js" type="text/javascript" charset="utf-8"></script>--%>
    <script src="../jquery/jquery-ui-timepicker-addon-0.6.2.min.js" type="text/javascript"></script>
    <style type='text/css'>
        body
        {
            margin-top: 40px;
            text-align: center;
            font-size: 14px;
            font-family: 'Open Sans Condensed', sans-serif;
        }
        #calendar
        {
            width: 900px;
            margin: 0 auto;
        }
        /* css for timepicker */
        .ui-timepicker-div dl
        {
            text-align: left;
        }
        .ui-timepicker-div dl dt
        {
            height: 25px;
        }
        .ui-timepicker-div dl dd
        {
            margin: -25px 0 10px 65px;
        }
        .style1
        {
            width: 100%;
        }
        
        /* table fields alignment*/
        .alignRight
        {
        	text-align:right;
        	padding-right:10px;
        	padding-bottom:10px;
        }
        .alignLeft
        {
        	text-align:left;
        	padding-bottom:10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <div id="calendar">
        </div>
        <div id="updatedialog" style="font: 70%; margin: 50px; "
            title="Update or Delete Event">
            <table cellpadding="0" class="style1">
                <tr>
                    <td class="alignRight">name:</td>
                    <td class="alignLeft">
                        <input id="eventName" type="text" /><br />
                    </td>
                </tr>
                <tr>
                    <td class="alignRight">description:</td>
                    <td class="alignLeft">
                        <%--<textarea id="eventDesc" rows="3"></textarea>--%>
                        <asp:TextBox ID="eventDesc" TextMode="MultiLine" Rows="3" Text="Khách hàng" runat="server"></asp:TextBox>
                        
                    </td>
                </tr>
                <tr>
                    <td class="alignRight">start:</td>
                    <td class="alignLeft">
                        <span id="eventStart"></span></td>
                </tr>
                <tr>
                    <td class="alignRight">end: </td>
                    <td class="alignLeft">
                        <span id="eventEnd"></span>
                        <input type="hidden" id="eventId" /></td>
                </tr>
            </table>
        </div>
        <div id="addDialog" style="font: 70%; margin: 50px; " title="Add Event">
            <table cellpadding="0" class="style1">
                <tr>
                    <td class="alignRight">name:</td>
                    <td class="alignLeft">
                        <input id="addEventName" type="text" size="50" /><br />
                    </td>
                </tr>
                <tr>
                    <td class="alignRight">description:</td>
                    <td class="alignLeft">
                        <textarea id="addEventDesc" rows="3"></textarea>
                        <%--<asp:TextBox ID="addEventDesc" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>--%>
                    </td>
                </tr>
                <tr>
                    <td class="alignRight">start:</td>
                    <td class="alignLeft">
                        <span id="addEventStartDate"></span></td>
                </tr>
                <tr>
                    <td class="alignRight">end:</td>
                    <td class="alignLeft">
                        <span id="addEventEndDate"></span></td>
                </tr>
            </table>
        </div>
        <div runat="server" id="jsonDiv" />
        <input type="hidden" id="hdClient" runat="server" />
    </form>
</body>
</html>
