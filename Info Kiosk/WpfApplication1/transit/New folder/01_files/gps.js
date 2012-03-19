function showBusPosition(map, route) {
    // Finds and displays bus markers for the requested route(s)
    
    var ajaxOptions = {};

    ajaxOptions.type = "get";
    ajaxOptions.url = BUS_SITE_ROOT + "busPosition.aspx";
    ajaxOptions.data = { route: route };
    ajaxOptions.cache = false;
    ajaxOptions.success = function (data, status, response) {
        //alert("Data: " + data);
        var node;
        var routeNumber;
        var lat;
        var lon;
        var dir;
        var busNumber;
        var busDirection;
        var busSpriteOffset = 0;
        var busImage = new google.maps.MarkerImage(BUS_SITE_ROOT + "images/busSilhouette15.png"
            , new google.maps.Size(15, 17)
            , new google.maps.Point(0, 0)
            , new google.maps.Point(7.5, 8.5)
            );

        //        alert("parsing data:\n" + data);
        var parsedData = jQuery.parseXML(data);
        //        alert("parsedData:\n" + parsedData);

        var xml = $(parsedData);
        //        alert("xml:\n " + xml.text());

        var buses = xml.find("data").children();
        //alert("buses " + buses.length + " : " + buses.text());

        buses.each(function () {
            node = $(this);
            routeNumber = node.find("route_id").text();
            lat = node.find("lat").text();
            lon = node.find("lon").text();
            dir = node.find("direction").text();
            busNumber = node.find("name").text();

            //            busDirection = new google.maps.MarkerImage(getBusDirectionImage(dir)
            //            , new google.maps.Size(40, 40)  //size
            //            , new google.maps.Point(0, 0)   //origin
            //            , new google.maps.Point(20, 20) //anchor
            //            );

            busSpriteOffset = getBusSpriteOffset(dir);

            var marker = new google.maps.Marker({
                map: map
                , position: new google.maps.LatLng(lat, lon)
                , direction: dir
                //, title: "Bus: " + busNumber + "\nRoute: " + routeNumber + "\nLat: " + lat + "\nLon: " + lon + "\nDir: " + dir
                , title: "Route: " + routeNumber
                //, icon: "http://m-test.tamu.edu/images/apps/map/bus.png"
                //, icon: busImage
                , icon: new google.maps.MarkerImage(BUS_SITE_ROOT + "images/busDirSprite.png", new google.maps.Size(36, 36), new google.maps.Point(busSpriteOffset, 0), new google.maps.Point(18, 18))
                //, shadow: busDirection
                , animation: map.animation
                , zIndex: 3
            });

            //            google.maps.event.addListener(marker, 'shadow_changed', function () { alert("shadow changed\n" + $(this).find("img")); $(this).find("img").rotate(90); });

            //            marker.setShadow("Images/busDirectionArrow.png");

        });
    };
    ajaxOptions.complete = function () {
        //        alert("complete");
    };
    ajaxOptions.error = function (xhr, textStatus, errorThrown) {
        //        alert("An error occurred while reading the bus position feed.  Unable to place buses on the map.\nstatus: " + status + "\nerror text: " + errorThrown);
    };

    jQuery.ajax(ajaxOptions);
    //    jQuery.ajax({
    //        type: "get",
    //        url: 'BusPosition.aspx',
    //        data: { route: route },
    //        cache: false,
    //        success: function (data, status, response) {
    //            //alert("Data: " + data);

    //            var parsedData = jQuery.parseXML(data);
    //            //alert("parsedData: " + parsedData);

    //            var xml = $(parsedData);
    //            //alert("xml: " + xml.text());

    //            var buses = xml.find("data").children();
    //            //alert("buses " + buses.length + " : " + buses.text());

    //            buses.each(function () {
    //                var node = $(this);
    //                var routeNumber = node.find("route_id").text();
    //                var lat = node.find("lat").text();
    //                var lon = node.find("lon").text();

    //                //alert("Route " + route + "\nLat: " + lat + "\nLon: " + lon);
    //                new google.maps.Marker({
    //                    map: map
    //                            , position: new google.maps.LatLng(lat, lon)
    //                            , title: routeNumber
    //                            , icon: "http://m-test.tamu.edu/images/apps/map/bus.png"
    //                });
    //            });
    //        },
    //        complete: function () {
    //            //alert("complete");
    //        },
    //        error: function (xhr, textStatus, errorThrown) {
    //            alert("error occurred\nstatus: " + status + "\nerror text: " + errorThrown);
    //        }
    //    });
}

function getBusDirectionImage(degrees) {
    if (22.5 < degrees && degrees <= 67.5) {
        return BUS_SITE_ROOT + "images/busDirectionArrow-NE.png";
    }
    else if (67.5 < degrees && degrees <= 112.5) {
        return BUS_SITE_ROOT + "images/busDirectionArrow-E.png";
    }
    else if (112.5 < degrees && degrees <= 157.5) {
        return BUS_SITE_ROOT + "images/busDirectionArrow-SE.png";
    }
    else if (157.5 < degrees && degrees <= 202.5) {
        return BUS_SITE_ROOT + "images/busDirectionArrow-S.png";
    }
    else if (202.5 < degrees && degrees <= 247.5) {
        return BUS_SITE_ROOT + "images/busDirectionArrow-SW.png";
    }
    else if (247.5 < degrees && degrees <= 292.5) {
        return BUS_SITE_ROOT + "images/busDirectionArrow-W.png";
    }
    else if (292.5 < degrees && degrees <= 337.5) {
        return BUS_SITE_ROOT + "images/busDirectionArrow-NW.png";
    }
    else {
        return BUS_SITE_ROOT + "images/busDirectionArrow-N.png";
    }
}

function getBusSpriteOffset(degrees) {
    var imageSize = 36;
    if (22.5 < degrees && degrees <= 67.5) {
        // North-East
        return (imageSize * 1);
    }
    else if (67.5 < degrees && degrees <= 112.5) {
        // East
        return (imageSize * 2);
    }
    else if (112.5 < degrees && degrees <= 157.5) {
        // South-East
        return (imageSize * 3);
    }
    else if (157.5 < degrees && degrees <= 202.5) {
        // South
        return (imageSize * 4);
    }
    else if (202.5 < degrees && degrees <= 247.5) {
        // South-West
        return (imageSize * 5);
    }
    else if (247.5 < degrees && degrees <= 292.5) {
        // West
        return (imageSize * 6);
    }
    else if (292.5 < degrees && degrees <= 337.5) {
        // North-West
        return (imageSize * 7);
    }
    else {
        // North
        return (imageSize * 0);
    }
}