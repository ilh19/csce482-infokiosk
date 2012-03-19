function getRouteColor(route, colorType) {

    // colors array
    var RouteColors = { "01": { "full": "rgb(98, 64, 153)", "30Percent": "rgb(208, 198, 225)", "compliment": "rgb(255, 255, 255)" }
        , "03": { "full": "rgb(1, 1, 1)", "30Percent": "rgb(180, 181, 181)", "compliment": "rgb(255, 255, 255)" }
        , "04E": { "full": "rgb(236, 39, 39)", "30Percent": "rgb(25, 190, 191)", "compliment": "rgb(255, 255, 255)" }
        , "04N": { "full": "rgb(236, 39, 39)", "30Percent": "rgb(25, 190, 191)", "compliment": "rgb(255, 255, 255)" }
        , "04W": { "full": "rgb(234, 116, 36)", "30Percent": "rgb(250, 223, 205)", "compliment": "rgb(255, 255, 255)" }
        , "05": { "full": "rgb(94, 155, 211)", "30Percent": "rgb(207, 255, 242)", "compliment": "rgb(255, 255, 255)" }
        , "06": { "full": "rgb(244, 191, 38)", "30Percent": "rgb(252, 236, 190)", "compliment": "rgb(0, 0, 0)" }
        , "07": { "full": "rgb(20, 178, 75)", "30Percent": "rgb(190, 232, 201)", "compliment": "rgb(255, 255, 255)" }
        , "08": { "full": "rgb(233, 22, 139)", "30Percent": "rgb(249, 183, 221)", "compliment": "rgb(255, 255, 255)" }
        , "12": { "full": "rgb(0, 84, 166)", "30Percent": "rgb(179, 204, 228)", "compliment": "rgb(255, 255, 255)" }
        , "15": { "full": "rgb(40, 144, 58)", "30Percent": "rgb(191, 222, 197)", "compliment": "rgb(255, 255, 255)" }
        , "15N": { "full": "#5A3B00", "30Percent": "#C5B196", "compliment": "rgb(255, 255, 255)" }
        , "15S": { "full": "rgb(40, 144, 58)", "30Percent": "rgb(191, 222, 197)", "compliment": "rgb(255, 255, 255)" }
        , "22": { "full": "rgb(189, 26, 141)", "30Percent": "rgb(235, 187, 221)", "compliment": "rgb(255, 255, 255)" }
        , "26": { "full": "rgb(0, 111, 59)", "30Percent": "rgb(179, 212, 197)", "compliment": "rgb(255, 255, 255)" }
        , "27": { "full": "rgb(0, 174, 239)", "30Percent": "rgb(179, 231, 250)", "compliment": "rgb(255, 255, 255)" }
        , "31": { "full": "rgb(102, 45, 145)", "30Percent": "rgb(209, 192, 222)", "compliment": "rgb(255, 255, 255)" }
        , "33": { "full": "rgb(151, 27, 30)", "30Percent": "rgb(224, 187, 188)", "compliment": "rgb(255, 255, 255)" }
        , "34": { "full": "rgb(247, 147, 30)", "30Percent": "rgb(253, 223, 188)", "compliment": "rgb(0, 0, 0)" }
        , "36": { "full": "rgb(150, 115, 72)", "30Percent": "rgb(224, 213, 200)", "compliment": "rgb(255, 255, 255)" }
        , "default": { "full": "rgb(80, 0, 0)", "30Percent": "rgb(131, 107, 107)", "compliment": "rgb(255, 255, 255)" }
    };

    if (RouteColors[route][colorType] !== undefined) {
        // the requested route/colorType is in the array, return it!
        return RouteColors[route][colorType];
    }
    else if (RouteColors[route]["full"] !== undefined) {
        // if the colorType was not specified, return the full color for the route
        return RouteColors[route]["full"];
    }
    else if (RouteColors["default"][colorType] !== undefined) {
        // if the route was not found, return the default colorType
        return RouteColors["default"][colorType];
    }
    else {
        // could not find the requested color, nor anything close! Return default/full color
        return RouteColors["default"]["full"];
    }
};