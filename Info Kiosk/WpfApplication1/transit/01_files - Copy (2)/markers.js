function getDot(route) {
    switch (route) {
        case 1: // Bonfire
        case "1":
        case "01":
            return BUS_SITE_ROOT + "images/01Bonfire15x15Dot.png";
            break;

        case 3: // Yell Practice
        case "3":
        case "03":
            return BUS_SITE_ROOT + "images/03YellPractice15x15Dot.png";
            break;

        case 4: // Gig 'Em
        case "4":
        case "04":
            return BUS_SITE_ROOT + "images/04GigEm15x15Dot.png";
            break;

        case 5: // Bush School
        case "5":
        case "05":
            return BUS_SITE_ROOT + "images/05BushSchool15x15Dot.png";
            break;

        case 6: // 12th Man
        case "6":
        case "06":
            return BUS_SITE_ROOT + "images/0612thMan15x15Dot.png";
            break;

        case 7: // Wehner Express
        case "7":
        case "07":
            return BUS_SITE_ROOT + "images/07WehnerExpress15x15Dot.png";
            break;

        case 8: // Howdy
        case "8":
        case "08":
            return BUS_SITE_ROOT + "images/08Howdy15x15Dot.png";
            break;

        case 12: // Reveille
        case "12":
            return BUS_SITE_ROOT + "images/12Reveille15x15Dot.png";
            break;

        case "15": // Old Army
        case "15S":
            return BUS_SITE_ROOT + "images/15OldArmy15x15Dot.png";
            break;

        case "15N": // Old Army North
            return BUS_SITE_ROOT + "images/15NOldArmy15x15Dot.png";
            break;

        case 22: // Excel
        case "22":
            return BUS_SITE_ROOT + "images/22Excel15x15Dot.png";
            break;

        case 26: // Rudder
        case "26":
        case "26 Ex":
            return BUS_SITE_ROOT + "images/26Rudder15x15Dot.png";
            break;

        case 27: // Ring Dance
        case "27":
            return BUS_SITE_ROOT + "images/27RingDance15x15Dot.png";
            break;

        case 31: // Elephant Walk
        case "31":
        case "31 Ex":
            return BUS_SITE_ROOT + "images/31ElephantWalk15x15Dot.png";
            break;

        case 33: // Texas Aggies
        case "33":
            return BUS_SITE_ROOT + "images/33TexasAggies15x15Dot.png";
            break;

        case 34: // Fish Camp
        case "34":
            return BUS_SITE_ROOT + "images/34FishCamp15x15Dot.png";
            break;

        case 36: // Cotton Bowl
        case "36":
            return BUS_SITE_ROOT + "images/36CottonBowl15x15Dot.png";
            break;

        default: // no route match, use a marron dot
            return BUS_SITE_ROOT + "images/Maroon15x15Dot.png";
            break;
    };

    return routeDot[route];
}

function getSquare(route) {
    switch (route) {
        case 1: // Bonfire
        case "1":
        case "01":
            return BUS_SITE_ROOT + "images/01Bonfire15x15Square.png";
            break;

        case 3: // Yell Practice
        case "3":
        case "03":
            return BUS_SITE_ROOT + "images/03YellPractice15x15Square.png";
            break;

        case 4: // Gig 'Em
        case "4":
        case "04":
            return BUS_SITE_ROOT + "images/04GigEm15x15Square.png";
            break;

        case 5: // Bush School
        case "5":
        case "05":
            return BUS_SITE_ROOT + "images/05BushSchool15x15Square.png";
            break;

        case 6: // 12th Man
        case "6":
        case "06":
            return BUS_SITE_ROOT + "images/0612thMan15x15Square.png";
            break;

        case 7: // Wehner Express
        case "7":
        case "07":
            return BUS_SITE_ROOT + "images/07WehnerExpress15x15Square.png";
            break;

        case 8: // Howdy
        case "8":
        case "08":
            return BUS_SITE_ROOT + "images/08Howdy15x15Square.png";
            break;

        case 12: // Reveille
        case "12":
            return BUS_SITE_ROOT + "images/12Reveille15x15Square.png";
            break;

        case "15N": // Old Army North
            return BUS_SITE_ROOT + "images/15NOldArmy15x15Square.png";
            break;

        case "15": // Old Army South
        case "15S":
            return BUS_SITE_ROOT + "images/15OldArmy15x15Square.png";
            break;

        case 22: // Excel
        case "22":
            return BUS_SITE_ROOT + "images/22Excel15x15Square.png";
            break;

        case 26: // Rudder
        case "26":
        case "26 Ex":
            return BUS_SITE_ROOT + "images/26Rudder15x15Square.png";
            break;

        case 27: // Ring Dance
        case "27":
            return BUS_SITE_ROOT + "images/27RingDance15x15Square.png";
            break;

        case 31: // Elephant Walk
        case "31":
        case "31 Ex":
            return BUS_SITE_ROOT + "images/31ElephantWalk15x15Square.png";
            break;

        case 33: // Texas Aggies
        case "33":
            return BUS_SITE_ROOT + "images/33TexasAggies15x15Square.png";
            break;

        case 34: // Fish Camp
        case "34":
            return BUS_SITE_ROOT + "images/34FishCamp15x15Square.png";
            break;

        case 36: // Cotton Bowl
        case "36":
            return BUS_SITE_ROOT + "images/36CottonBowl15x15Square.png";
            break;

        default: // no route match, use a marron dot
            return BUS_SITE_ROOT + "images/Maroon15x15Square.png";
            break;
    };

    return routeDot[route];
}