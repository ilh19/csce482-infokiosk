var BUS_SITE_ROOT = "/BusRoutes/";

function removeJavaScriptWarning() {
    $('#NeedJavaScript').hide();
};

function addNumberedSquares(route) {
    // Adds the numbered route stop square in front of the stop name
    var stops = getStops(route);

    var headers = $('table.timetable th');
    var txt = "headers: " + headers + "\n";

    // TODO: think of a way to dynamically write a number over an empty square marker in the table AND on the Google Map
    headers.each(function (index) {
        txt += index + ": " + $(this).text() + "\n"

        // Loop through stops array and assign timestop images to matching column header
        var title = "";
        for (ary in stops) {
            title = stops[ary]["Title"];
            txt += "title: " + title;

            if ($(this).text().indexOf(title) != -1) {
                txt += "match! ";
                $('<img src="' + stops[ary]["Icon"] + '" /><br /> ').prependTo($(this));
            }
            txt += "found: " + $(this).find(title);
            txt += "\n";
        }
    });
    //alert(txt);
}

function timetableColoring(option, route) {
    // timetable coloring options.  Once a final coloring has been decided, some values can be moved to the style sheet.
    switch (option) {
        case 1: // column headings use route color; alternating columns use route color; alternating rows use tan
            $('table.timetable td.colEven')
                        .css('background-color', getRouteColor(route, "full"))
                        .css('color', 'White');
            $('table.timetable thead, table.timetable th').css('background-color', getRouteColor(route, "full"));
            $('table.timetable tr:even td')
                        .css('background-color', '#D3Bf96')
                        .css('color', 'Black');
            break;

        case 2: // column headings use route color; alternating columns use route color
            $('table.timetable tr:even td').css('background-color', 'transparent'); // undo stylesheet row coloring
            $('table.timetable td.colEven')
                        .css('background-color', getRouteColor(route, "full"))
                        .css('color', 'White');
            $('table.timetable thead, table.timetable th').css('background-color', getRouteColor(route, "full"));
            break;

        case 3:
            // time table row background color is tan
            $('table.timetable th.colEven, table.timetable th.colOdd').css('background-color', getRouteColor(route, "full"));
            $('table.timetable tr.oddRow').css('background-color', '#D3Bf96');

            // time table header background color uses route color
            $('table.timetable th.colEven, table.timetable th.colOdd')
                .css('color', getRouteColor(route, "compliment"))
                .css('background-color', getRouteColor(route, "full"));
            break;

        case 4: // column headings use route color; alternating columns use tan
            $('table.timetable tr:even td').css('background-color', 'transparent'); // undo stylesheet row coloring
            $('table.timetable thead, table.timetable th').css('background-color', getRouteColor(route, "full"));
            $('table.timetable td.colEven').css('background-color', '#D3Bf96');
            break;
    }
}

function leaveTimeColoring() {
    var leaveTimeCells = $("table.timetable td");
    leaveTimeCells.each(function (i, el) {
        var now = new Date();
        var leaveDateTime = new Date();
        var leaveTime;
        var hour;
        var minute;
        leaveTime = $(this).text();

        hour = getHour(leaveTime);
        minute = getMinute(leaveTime);

        if (is12Hour(leaveTime)) {
            // leaveTime is 12 hour format

            if (leaveTime.search(/PM/i) != -1) {

                // add 12 hours to all times EXCEPT 12pm.
                if (hour != 12) {
                    leaveDateTime.setHours(hour + 12, minute);
                }
                else {
                    leaveDateTime.setHours(hour, minute);
                }
            }
            else {
                // leave time is AM, leave hour unchanged
                leaveDateTime.setHours(hour, minute);

                // leave times between midnight and 5am should be set to the next day
                if (hour == 12 || hour < 5) {
                    leaveDateTime.setDate(leaveDateTime.getDate() + 1);
                }
            }
        }
        else {
            // leaveTime is 24 hour format

            // set the time
            leaveDateTime.setHours(hour, minute);

            // leave times between midnight and 5 am should be set to the next day
            if (hour < 5) {
                leaveDateTime.setDate(leaveDateTime.getDate() + 1);
            }
        }

        if (leaveDateTime > now) {
            $(this).addClass("FutureLeaveTime");
        }
        else {
            $(this).addClass("PastLeaveTime");
        }
    });

    function getHour(strTime) {
        var numIndex = strTime.indexOf(":");

        if (numIndex == -1) {
            return NaN;
        }

        var strHour = strTime.substr(numIndex - 2, 2);
        var numHour = new Number(strHour);
        return numHour;
    };

    function getMinute(strTime) {
        var numIndex = strTime.indexOf(":");

        if (numIndex == -1) {
            return NaN;
        }

        var strMin = strTime.substr(numIndex + 1, 2);
        var numMin = new Number(strMin)
        return numMin;
    }

    function is12Hour(strTime) {
        var numAmPm = strTime.search(/AM|PM/i);
        return (numAmPm != -1);
    }

    // identify rows that have all leave times past
    var leaveTimeRows = $("table.timetable tbody tr");
    leaveTimeRows.each(function (i, el) {
        var leaveTimeCells = $(this).children("td");

        $(this).addClass("PastLeaveTime");

        leaveTimeCells.each(function () {
            if (!$(this).hasClass("PastLeaveTime")) {
                $(this).parent().removeClass("PastLeaveTime");
                return false;  // exit the loop, an exeption has been found
            }
        });
    });
};

function syncLeaveTimeCheckBox(objTimeTable) {
    var objTimeTable = $(objTimeTable);
    var hideTimesCheckBox = objTimeTable.find("input.leaveTimesCheckBox")
    var leaveTimes = objTimeTable.find("tr.PastLeaveTime");

    if (hideTimesCheckBox.is(":checked")) {
        leaveTimes.hide();
    }
    else {
        leaveTimes.show();
    }
}

var gAutoRefresh;
function toggleAutoRefresh(autoRefresh) {
    if (autoRefresh) {
        gAutoRefresh = setTimeout("$('form').submit()", 60000);
    }
    else if (gAutoRefresh) {
        clear(gAutoRefresh);
    }
}