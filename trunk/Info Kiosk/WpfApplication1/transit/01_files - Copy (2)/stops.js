function showStops(routeNum, map) {
    var stops = getStops(routeNum);
    var marker;

    for (i in stops) {
        marker = new google.maps.Marker({
            position: new google.maps.LatLng(stops[i]["Lat"], stops[i]["Lng"]),
            map: map,
            title: stops[i]["Title"],
            icon: stops[i]["Icon"],
            //animation: google.maps.Animation.DROP
            animation: map.animation,
            zIndex: 2
        });
        marker.setMap(map);
    }
};

function getStops(route) {
    var icon = [BUS_SITE_ROOT + "images/15x15Square.png", BUS_SITE_ROOT + "images/15x15Square1.png", BUS_SITE_ROOT + "images/15x15Square2.png", BUS_SITE_ROOT + "images/15x15Square3.png", BUS_SITE_ROOT + "images/15x15Square4.png", BUS_SITE_ROOT + "images/15x15Square5.png", BUS_SITE_ROOT + "images/15x15Square6.png", BUS_SITE_ROOT + "images/15x15Square7.png", BUS_SITE_ROOT + "images/15x15Square8.png", BUS_SITE_ROOT + "images/15x15Square9.png"];

    switch (route) {
        // Bonfire     
        case "1":
        case "01":
            icon = [BUS_SITE_ROOT + "images/01Bonfire15x15Dot.png", BUS_SITE_ROOT + "images/01Bonfire15x15Square1.png", BUS_SITE_ROOT + "images/01Bonfire15x15Square2.png", BUS_SITE_ROOT + "images/01Bonfire15x15Square3.png", BUS_SITE_ROOT + "images/01Bonfire15x15Square4.png", BUS_SITE_ROOT + "images/01Bonfire15x15Square5.png", BUS_SITE_ROOT + "images/01Bonfire15x15Square6.png", BUS_SITE_ROOT + "images/01Bonfire15x15Square7.png", BUS_SITE_ROOT + "images/01Bonfire15x15Square8.png", BUS_SITE_ROOT + "images/01Bonfire15x15Square9.png"];
            return [ { Lat: 30.61290893631630, Lng: -96.33947514530870, Title: "Rudder", Icon: icon[1] }
                , { Lat: 30.60245100274390, Lng: -96.34661116281820, Title: "Penberthy", Icon: icon[0] }
                , { Lat: 30.60567770038110, Lng: -96.34733670039990, Title: "Reed Arena", Icon: icon[2] }
                , { Lat: 30.60681763928040, Lng: -96.34714358133940, Title: "Lot 100a/b", Icon: icon[0] }
                , { Lat: 30.60977477489560, Lng: -96.34669565240720, Title: "Kleberg", Icon: icon[0] }
                , { Lat: 30.61213109565560, Lng: -96.34953074750490, Title: "Wehner N", Icon: icon[3] }
                , { Lat: 30.61642397143980, Lng: -96.34357356092770, Title: "Fish Pond", Icon: icon[4] }
                , { Lat: 30.61772484288960, Lng: -96.34118237144840, Title: "Ross and Ireland", Icon: icon[0] }
                , { Lat: 30.61894927137790, Lng: -96.33940272566100, Title: "Ross and Spence", Icon: icon[0] }
                , { Lat: 30.61983440040560, Lng: -96.33806162107360, Title: "Ross and Bizzell", Icon: icon[0] }
                , { Lat: 30.61643335917190, Lng: -96.33494221180330, Title: "Commons", Icon: icon[5] }
                , { Lat: 30.61415214026880, Lng: -96.33289568620300, Title: "Southside Garage", Icon: icon[0] }
                , { Lat: 30.61174083422070, Lng: -96.33671649317240, Title: "Coke Street", Icon: icon[0] }];
            break;

        // Yell Practice     
        case "3":
        case "03":
            icon = [BUS_SITE_ROOT + "images/03YellPractice15x15Dot.png", BUS_SITE_ROOT + "images/03YellPractice15x15Square1.png", BUS_SITE_ROOT + "images/03YellPractice15x15Square2.png", BUS_SITE_ROOT + "images/03YellPractice15x15Square3.png", BUS_SITE_ROOT + "images/03YellPractice15x15Square4.png", BUS_SITE_ROOT + "images/03YellPractice15x15Square5.png", BUS_SITE_ROOT + "images/03YellPractice15x15Square6.png", BUS_SITE_ROOT + "images/03YellPractice15x15Square7.png", BUS_SITE_ROOT + "images/03YellPractice15x15Square8.png", BUS_SITE_ROOT + "images/03YellPractice15x15Square9.png"];
            return [ { Lat: 30.61395633899910, Lng: -96.34165712247240, Title: "MSC", Icon: icon[1] }
                , { Lat: 30.61620671249670, Lng: -96.34339385291300, Title: "Fish Pond", Icon: icon[0] }
                , { Lat: 30.61772484288960, Lng: -96.34118237144840, Title: "Ross and Ireland", Icon: icon[0] }
                , { Lat: 30.61894927137790, Lng: -96.33940272566100, Title: "Ross and Spence", Icon: icon[0] }
                , { Lat: 30.61983440040560, Lng: -96.33806162107360, Title: "Ross and Bizzell", Icon: icon[0] }
                , { Lat: 30.62122244365350, Lng: -96.33838616838370, Title: "Wisenbaker", Icon: icon[2] }
                , { Lat: 30.61202112507940, Lng: -96.34874888353040, Title: "Wehner S", Icon: icon[3] }];
            break;

        // Gig 'Em East      
        case "4E":
        case "04E":
            icon = [BUS_SITE_ROOT + "images/04GigEm15x15Dot.png", BUS_SITE_ROOT + "images/04GigEm15x15Square1.png", BUS_SITE_ROOT + "images/04GigEm15x15Square2.png", BUS_SITE_ROOT + "images/04GigEm15x15Square3.png", BUS_SITE_ROOT + "images/04GigEm15x15Square4.png", BUS_SITE_ROOT + "images/04GigEm15x15Square5.png", BUS_SITE_ROOT + "images/04GigEm15x15Square6.png", BUS_SITE_ROOT + "images/04GigEm15x15Square7.png", BUS_SITE_ROOT + "images/04GigEm15x15Square8.png", BUS_SITE_ROOT + "images/04GigEm15x15Square9.png"];
            return [ { Lat: 30.61643738248570, Lng: -96.34360977075160, Title: "Fish Pond", Icon: icon[1] }
                , { Lat: 30.61772484288960, Lng: -96.34118237144840, Title: "Ross and Ireland", Icon: icon[0] }
                , { Lat: 30.61894927137790, Lng: -96.33940272566100, Title: "Ross and Spence", Icon: icon[0] }
                , { Lat: 30.61983440040560, Lng: -96.33806162107360, Title: "Ross and Bizzell", Icon: icon[0] }
                , { Lat: 30.62122244365350, Lng: -96.33838616838370, Title: "Wisenbaker", Icon: icon[0] }
                , { Lat: 30.62678534548200, Lng: -96.34313636083220, Title: "Hensel @ Ball", Icon: icon[0] }
                , { Lat: 30.62899414473740, Lng: -96.34180464397700, Title: "Hensel @ W-X Row", Icon: icon[0] }
                , { Lat: 30.62872726492450, Lng: -96.33913852805720, Title: "Hensel @ Moore", Icon: icon[0] }
                , { Lat: 30.62748540207660, Lng: -96.34074785356210, Title: "Community Center", Icon: icon[2] }
                , { Lat: 30.62525112183400, Lng: -96.34084977751080, Title: "Moore @ Office", Icon: icon[0] }
                , { Lat: 30.62480453400640, Lng: -96.34029456021160, Title: "Moore @ Haltom", Icon: icon[0] }
                , { Lat: 30.62417689705950, Lng: -96.33948453304080, Title: "Moore @ Front", Icon: icon[0] }
                , { Lat: 30.62086705093780, Lng: -96.33839153280210, Title: "WERC", Icon: icon[3] }
                , { Lat: 30.61905655974490, Lng: -96.33931823607200, Title: "Ross and Spence", Icon: icon[0] }
                , { Lat: 30.61782408462910, Lng: -96.34111665732360, Title: "Ross and Ireland", Icon: icon[0] }];
            break;

        // Gig 'Em West      
        case "4W":
        case "04W":
            icon = [BUS_SITE_ROOT + "images/04WGigEm15x15Dot.png", BUS_SITE_ROOT + "images/04WGigEm15x15Square1.png", BUS_SITE_ROOT + "images/04WGigEm15x15Square2.png", BUS_SITE_ROOT + "images/04WGigEm15x15Square3.png", BUS_SITE_ROOT + "images/04WGigEm15x15Square4.png", BUS_SITE_ROOT + "images/04WGigEm15x15Square5.png", BUS_SITE_ROOT + "images/04WGigEm15x15Square6.png", BUS_SITE_ROOT + "images/04WGigEm15x15Square7.png", BUS_SITE_ROOT + "images/04WGigEm15x15Square8.png", BUS_SITE_ROOT + "images/04WGigEm15x15Square9.png"];
            return [ { Lat: 30.61643738248570, Lng: -96.34360977075160, Title: "Fish Pond", Icon: icon[1] }
                , { Lat: 30.60929197724410, Lng: -96.35353662690740, Title: "Rural Public Health", Icon: icon[2] }
                , { Lat: 30.60586143170960, Lng: -96.36762090728410, Title: "Energy Systems Lab", Icon: icon[3] }
                , { Lat: 30.59777725325690, Lng: -96.39537506672000, Title: "Health Science Center", Icon: icon[4] }
                , { Lat: 30.60929197724410, Lng: -96.35353662690740, Title: "Rural Public Health", Icon: icon[5] }];
            break;

        // Gig 'Em Night   
        case "N04":
        case "N4":
        case "04N":
        case "4N":
            icon = [BUS_SITE_ROOT + "images/04GigEm15x15Dot.png", BUS_SITE_ROOT + "images/04GigEm15x15Square1.png", BUS_SITE_ROOT + "images/04GigEm15x15Square2.png", BUS_SITE_ROOT + "images/04GigEm15x15Square3.png", BUS_SITE_ROOT + "images/04GigEm15x15Square4.png", BUS_SITE_ROOT + "images/04GigEm15x15Square5.png", BUS_SITE_ROOT + "images/04GigEm15x15Square6.png", BUS_SITE_ROOT + "images/04GigEm15x15Square7.png", BUS_SITE_ROOT + "images/04GigEm15x15Square8.png", BUS_SITE_ROOT + "images/04GigEm15x15Square9.png"];
            return [ { Lat: 30.61643738248570, Lng: -96.34360977075160, Title: "Fish Pond", Icon: icon[1] }
                , { Lat: 30.61772484288960, Lng: -96.34118237144840, Title: "Ross and Ireland", Icon: icon[0] }
                , { Lat: 30.61894927137790, Lng: -96.33940272566100, Title: "Ross and Spence", Icon: icon[0] }
                , { Lat: 30.61983440040560, Lng: -96.33806162107360, Title: "Ross and Bizzell", Icon: icon[0] }
                , { Lat: 30.62122244365350, Lng: -96.33838616838370, Title: "Wisenbaker", Icon: icon[0] }
                , { Lat: 30.62678534548200, Lng: -96.34313636083220, Title: "Hensel @ Ball", Icon: icon[0] }
                , { Lat: 30.62899414473740, Lng: -96.34180464397700, Title: "Hensel @ W-X Row", Icon: icon[0] }
                , { Lat: 30.62872726492450, Lng: -96.33913852805720, Title: "Hensel @ Moore", Icon: icon[0] }
                , { Lat: 30.62748540207660, Lng: -96.34074785356210, Title: "Community Center", Icon: icon[2] }
                , { Lat: 30.62525112183400, Lng: -96.34084977751080, Title: "Moore @ Office", Icon: icon[0] }
                , { Lat: 30.62480453400640, Lng: -96.34029456021160, Title: "Moore @ Haltom", Icon: icon[0] }
                , { Lat: 30.62417689705950, Lng: -96.33948453304080, Title: "Moore @ Front", Icon: icon[0] }
                , { Lat: 30.62086705093780, Lng: -96.33839153280210, Title: "WERC", Icon: icon[3] }
                , { Lat: 30.61905655974490, Lng: -96.33931823607200, Title: "Ross and Spence", Icon: icon[0] }
                , { Lat: 30.61782408462910, Lng: -96.34111665732360, Title: "Ross and Ireland", Icon: icon[0] }
                , { Lat: 30.61643738248570, Lng: -96.34360977075160, Title: "Fish Pond", Icon: icon[4] }
                , { Lat: 30.60929197724410, Lng: -96.35353662690740, Title: "Rural Public Health", Icon: icon[5] }];
            break;

        // Bush School     
        case "5":
        case "05":
            icon = [BUS_SITE_ROOT + "images/05BushSchool15x15Dot.png", BUS_SITE_ROOT + "images/05BushSchool15x15Square1.png", BUS_SITE_ROOT + "images/05BushSchool15x15Square2.png", BUS_SITE_ROOT + "images/05BushSchool15x15Square3.png", BUS_SITE_ROOT + "images/05BushSchool15x15Square4.png", BUS_SITE_ROOT + "images/05BushSchool15x15Square5.png", BUS_SITE_ROOT + "images/05BushSchool15x15Square6.png", BUS_SITE_ROOT + "images/05BushSchool15x15Square7.png", BUS_SITE_ROOT + "images/05BushSchool15x15Square8.png", BUS_SITE_ROOT + "images/05BushSchool15x15Square9.png"];
            return [ { Lat: 30.61373908005590, Lng: -96.34143047579710, Title: "MSC", Icon: icon[1] }
                , { Lat: 30.61581242774800, Lng: -96.34320073385240, Title: "Fish Pond", Icon: icon[0] }
                , { Lat: 30.60979757367360, Lng: -96.34693571012830, Title: "Kleberg", Icon: icon[0] }
                , { Lat: 30.60676801841070, Lng: -96.34719722552280, Title: "Lot 100a/b", Icon: icon[0] }
                , { Lat: 30.60573536787840, Lng: -96.34743594213940, Title: "Reed Arena", Icon: icon[2] }
                , { Lat: 30.60527000458660, Lng: -96.35055401030500, Title: "Ag Building", Icon: icon[0] }
                , { Lat: 30.60217071188520, Lng: -96.35311686117150, Title: "TTI", Icon: icon[0] }
                , { Lat: 30.59918675417820, Lng: -96.35277353839720, Title: "Lot 111", Icon: icon[0] }
                , { Lat: 30.60435000683960, Lng: -96.35855101695960, Title: "Lot 108/UPD", Icon: icon[0] }
                , { Lat: 30.60120377547760, Lng: -96.35825865615960, Title: "Centeq/HP", Icon: icon[0] }
                , { Lat: 30.59811789382210, Lng: -96.35519959659570, Title: "Lot 41/43", Icon: icon[0] }
                , { Lat: 30.59915993208640, Lng: -96.35273598746870, Title: "Lot 111", Icon: icon[3] }
                , { Lat: 30.60183007131990, Lng: -96.35284461694030, Title: "TTI", Icon: icon[0] }
                , { Lat: 30.60566428933530, Lng: -96.34988077580220, Title: "Ag Building", Icon: icon[0] }
                , { Lat: 30.60567770038110, Lng: -96.34733670039990, Title: "Reed Arena", Icon: icon[4] }
                , { Lat: 30.60681763928040, Lng: -96.34714358133940, Title: "Lot 100a/b", Icon: icon[0] }
                , { Lat: 30.60977477489560, Lng: -96.34669565240720, Title: "Kleberg", Icon: icon[0] }];
            break;

        // 12th Man     
        case "6":
        case "06":
            icon = [BUS_SITE_ROOT + "images/0612thMan15x15Dot.png", BUS_SITE_ROOT + "images/0612thMan15x15Square1.png", BUS_SITE_ROOT + "images/0612thMan15x15Square2.png", BUS_SITE_ROOT + "images/0612thMan15x15Square3.png", BUS_SITE_ROOT + "images/0612thMan15x15Square4.png", BUS_SITE_ROOT + "images/0612thMan15x15Square5.png", BUS_SITE_ROOT + "images/0612thMan15x15Square6.png", BUS_SITE_ROOT + "images/0612thMan15x15Square7.png", BUS_SITE_ROOT + "images/0612thMan15x15Square8.png", BUS_SITE_ROOT + "images/0612thMan15x15Square9.png"];
            return [ { Lat: 30.61331797321550, Lng: -96.34156860956960, Title: "MSC", Icon: icon[1] }
                , { Lat: 30.61576012466910, Lng: -96.34314843077350, Title: "Fish Pond", Icon: icon[0] }
                , { Lat: 30.61213109565560, Lng: -96.34953074750490, Title: "Wehner N", Icon: icon[2] }
                , { Lat: 30.61434257712020, Lng: -96.35135464974370, Title: "Vet School", Icon: icon[0] }
                , { Lat: 30.61582583879380, Lng: -96.35268234328520, Title: "Lot 71", Icon: icon[0] }
                , { Lat: 30.61781603800150, Lng: -96.35449819889650, Title: "Transit", Icon: icon[0] }
                , { Lat: 30.61981294273220, Lng: -96.35634355880880, Title: "Physical Plant", Icon: icon[0] }
                , { Lat: 30.62207136285730, Lng: -96.35819160093020, Title: "GSC", Icon: icon[3] }
                , { Lat: 30.61978612064040, Lng: -96.35637172200510, Title: "Physical Plant", Icon: icon[0] }
                , { Lat: 30.61779592143270, Lng: -96.35453306761580, Title: "Transit", Icon: icon[0] }
                , { Lat: 30.61580572222500, Lng: -96.35272123531820, Title: "Lot 71", Icon: icon[0] }
                , { Lat: 30.61436269368900, Lng: -96.35142572828680, Title: "Vet School", Icon: icon[0] }
                , { Lat: 30.61195138764090, Lng: -96.34883337311940, Title: "Wehner", Icon: icon[4] }];
            break;

        // Wehner Express     
        case "7":
        case "07":
            icon = [BUS_SITE_ROOT + "images/07WehnerExpress15x15Dot.png", BUS_SITE_ROOT + "images/07WehnerExpress15x15Square1.png", BUS_SITE_ROOT + "images/07WehnerExpress15x15Square2.png", BUS_SITE_ROOT + "images/07WehnerExpress15x15Square3.png", BUS_SITE_ROOT + "images/07WehnerExpress15x15Square4.png", BUS_SITE_ROOT + "images/07WehnerExpress15x15Square5.png", BUS_SITE_ROOT + "images/07WehnerExpress15x15Square6.png", BUS_SITE_ROOT + "images/07WehnerExpress15x15Square7.png", BUS_SITE_ROOT + "images/07WehnerExpress15x15Square8.png", BUS_SITE_ROOT + "images/07WehnerExpress15x15Square9.png"];
            return [ { Lat: 30.61319325048880, Lng: -96.34173356543380, Title: "MSC", Icon: icon[1] }
                , { Lat: 30.61620671249670, Lng: -96.34339385291300, Title: "Fish Pond", Icon: icon[0] }
                , { Lat: 30.61187762688860, Lng: -96.34892456823140, Title: "Wehner S", Icon: icon[2] }];
            break;

        // Howdy     
        case "8":
        case "08":
            icon = [BUS_SITE_ROOT + "images/08Howdy15x15Dot.png", BUS_SITE_ROOT + "images/08Howdy15x15Square1.png", BUS_SITE_ROOT + "images/08Howdy15x15Square2.png", BUS_SITE_ROOT + "images/08Howdy15x15Square3.png", BUS_SITE_ROOT + "images/08Howdy15x15Square4.png", BUS_SITE_ROOT + "images/08Howdy15x15Square5.png", BUS_SITE_ROOT + "images/08Howdy15x15Square6.png", BUS_SITE_ROOT + "images/08Howdy15x15Square7.png", BUS_SITE_ROOT + "images/08Howdy15x15Square8.png", BUS_SITE_ROOT + "images/08Howdy15x15Square9.png"];
            return [ { Lat: 30.61301086026500, Lng: -96.34199508082840, Title: "MSC", Icon: icon[1] }
                , { Lat: 30.61570379827640, Lng: -96.34310149211300, Title: "Fish Pond", Icon: icon[0] }
                , { Lat: 30.60979757367360, Lng: -96.34693571012830, Title: "Kleberg", Icon: icon[2] }
                , { Lat: 30.60798574137600, Lng: -96.34585612093540, Title: "West Campus Garage", Icon: icon[0] }
                , { Lat: 30.60676399509690, Lng: -96.34474166302330, Title: "Reed Arena 2", Icon: icon[0] }
                , { Lat: 30.60466516641770, Lng: -96.34193875443570, Title: "Olsen Field", Icon: icon[3] }
                , { Lat: 30.60700673502720, Lng: -96.34465851453890, Title: "Student Rec Center", Icon: icon[0] }
                , { Lat: 30.60807157206960, Lng: -96.34562008652810, Title: "West Campus Garage", Icon: icon[0] }
                , { Lat: 30.60977477489560, Lng: -96.34669565240720, Title: "Kleberg", Icon: icon[4] }];
            break;

        // Reveille     
        case "12":
            icon = [BUS_SITE_ROOT + "images/12Reveille15x15Dot.png", BUS_SITE_ROOT + "images/12Reveille15x15Square1.png", BUS_SITE_ROOT + "images/12Reveille15x15Square2.png", BUS_SITE_ROOT + "images/12Reveille15x15Square3.png", BUS_SITE_ROOT + "images/12Reveille15x15Square4.png", BUS_SITE_ROOT + "images/12Reveille15x15Square5.png", BUS_SITE_ROOT + "images/12Reveille15x15Square6.png", BUS_SITE_ROOT + "images/12Reveille15x15Square7.png", BUS_SITE_ROOT + "images/12Reveille15x15Square8.png", BUS_SITE_ROOT + "images/12Reveille15x15Square9.png"];
            return [ { Lat: 30.61286602096950, Lng: -96.34220161093480, Title: "MSC", Icon: icon[1] }
                , { Lat: 30.62567356977900, Lng: -96.32950269159690, Title: "Foster", Icon: icon[0] }
                , { Lat: 30.62734592719950, Lng: -96.32767342493970, Title: "University Square", Icon: icon[0] }
                , { Lat: 30.63010860264950, Lng: -96.32465728072270, Title: "Tarrow", Icon: icon[0] }
                , { Lat: 30.63296783762980, Lng: -96.32148556837350, Title: "Munson", Icon: icon[2] }
                , { Lat: 30.63754100427280, Lng: -96.31972872136410, Title: "April Bloom", Icon: icon[0] }
                , { Lat: 30.63820082772980, Lng: -96.32270060912970, Title: "Summer Court", Icon: icon[0] }
                , { Lat: 30.63773278222880, Lng: -96.32777400778380, Title: "Laurel Ridge", Icon: icon[0] }
                , { Lat: 30.64103189951370, Lng: -96.32917814428680, Title: "Royal Oaks Garden", Icon: icon[0] }
                , { Lat: 30.64666051546700, Lng: -96.33348979553520, Title: "Carter Creek Shopping Center", Icon: icon[3] }
                , { Lat: 30.65627891756770, Lng: -96.34340726395890, Title: "Broadmoor St.", Icon: icon[0] }
                , { Lat: 30.66185925375580, Lng: -96.35508828491500, Title: "Hollow Hill", Icon: icon[0] }
                , { Lat: 30.66325000000000, Lng: -96.35185278000000, Title: "Blinn College - Bldg E", Icon: icon[4] }
                , { Lat: 30.65602678990520, Lng: -96.34325437803590, Title: "Broadmoor St.", Icon: icon[0] }
                , { Lat: 30.64662967006150, Lng: -96.33365609250400, Title: "Willow Oaks", Icon: icon[5] }
                , { Lat: 30.63769254909110, Lng: -96.32783972190860, Title: "Laurel Ridge", Icon: icon[0] }
                , { Lat: 30.63816193569670, Lng: -96.32267915145630, Title: "Summer Court", Icon: icon[0] }
                , { Lat: 30.63751552328560, Lng: -96.31977029560630, Title: "April Bloom", Icon: icon[0] }
                , { Lat: 30.63293296891050, Lng: -96.32158883342680, Title: "Munson", Icon: icon[6] }
                , { Lat: 30.63014078915960, Lng: -96.32469080833740, Title: "Tarrow", Icon: icon[0] }
                , { Lat: 30.62737543150040, Lng: -96.32770427034520, Title: "University Square", Icon: icon[0] }
                , { Lat: 30.62569368634780, Lng: -96.32954560694370, Title: "Foster", Icon: icon[0] }];
            break;

        // Old Army     
        case "15":
            icon = [BUS_SITE_ROOT + "images/15OldArmy15x15Dot.png", BUS_SITE_ROOT + "images/15OldArmy15x15Square1.png", BUS_SITE_ROOT + "images/15OldArmy15x15Square2.png", BUS_SITE_ROOT + "images/15OldArmy15x15Square3.png", BUS_SITE_ROOT + "images/15OldArmy15x15Square4.png", BUS_SITE_ROOT + "images/15OldArmy15x15Square5.png", BUS_SITE_ROOT + "images/15OldArmy15x15Square6.png", BUS_SITE_ROOT + "images/15OldArmy15x15Square7.png", BUS_SITE_ROOT + "images/15OldArmy15x15Square8.png", BUS_SITE_ROOT + "images/15OldArmy15x15Square9.png"];
            return [{ Lat: 30.61271179394200, Lng: -96.34248726621200, Title: "MSC", Icon: icon[1] }
                , { Lat: 30.61620671249670, Lng: -96.34339385291300, Title: "Fish Pond", Icon: icon[0] }
                , { Lat: 30.61952996966420, Lng: -96.34675063769520, Title: "College Main Parking Garage", Icon: icon[0] }
                , { Lat: 30.62194932233980, Lng: -96.34926923211030, Title: "Spruce St.", Icon: icon[0] }
                , { Lat: 30.62488097696790, Lng: -96.35248520091090, Title: "Aggie Station", Icon: icon[2] }
                , { Lat: 30.62609333551490, Lng: -96.35584064458850, Title: "Village on the Creek", Icon: icon[0] }
                , { Lat: 30.62703210872600, Lng: -96.35845043411560, Title: "The Islands", Icon: icon[0] }
                , { Lat: 30.62840674092810, Lng: -96.35973253010110, Title: "Reveille Ranch", Icon: icon[3] }
                , { Lat: 30.63087571447350, Lng: -96.36198692691250, Title: "Saddlewood", Icon: icon[0] }
                , { Lat: 30.63293699222430, Lng: -96.36386983775320, Title: "Wellborn @ Tee", Icon: icon[0] }
                , { Lat: 30.63799966204160, Lng: -96.36171602378580, Title: "Villa Maria @ Green St. ", Icon: icon[0] }
                , { Lat: 30.63457500000000, Lng: -96.35462500000000, Title: "Blinn Athletics", Icon: icon[0] }
                , { Lat: 30.63109431452120, Lng: -96.35456257191670, Title: "Academic Village", Icon: icon[0] }
                , { Lat: 30.62483269720270, Lng: -96.35254689172190, Title: "Aggie Station", Icon: icon[4] }
                , { Lat: 30.62192250024810, Lng: -96.34930275972500, Title: "Spruce St.", Icon: icon[0] }
                , { Lat: 30.61950448867700, Lng: -96.34678818862370, Title: "College Main Parking Garage", Icon: icon[0] }
                , { Lat: 30.61623353458840, Lng: -96.34345956703780, Title: "Fish Pond", Icon: icon[0]}];
            break;

        // Old Army North  
        case "15N":
            icon = [BUS_SITE_ROOT + "images/15NOldArmy15x15Dot.png"
                , BUS_SITE_ROOT + "images/15NOldArmy15x15Square1.png"
                , BUS_SITE_ROOT + "images/15NOldArmy15x15Square2.png"
                , BUS_SITE_ROOT + "images/15NOldArmy15x15Square3.png"
                , BUS_SITE_ROOT + "images/15NOldArmy15x15Square4.png"
                , BUS_SITE_ROOT + "images/15NOldArmy15x15Square5.png"
                , BUS_SITE_ROOT + "images/15NOldArmy15x15Square6.png"
                , BUS_SITE_ROOT + "images/15NOldArmy15x15Square7.png"
                , BUS_SITE_ROOT + "images/15NOldArmy15x15Square8.png"
                , BUS_SITE_ROOT + "images/15NOldArmy15x15Square9.png"];
            return [ { Lat: 30.61271179394200, Lng: -96.34248726621200, Title: "MSC", Icon: icon[1] }
                , { Lat: 30.62703210872600, Lng: -96.35845043411560, Title: "The Islands", Icon: icon[0] }
                , { Lat: 30.62840674092810, Lng: -96.35973253010110, Title: "Reveille Ranch", Icon: icon[2] }
                , { Lat: 30.63087571447350, Lng: -96.36198692691250, Title: "Saddlewood", Icon: icon[0] }
                , { Lat: 30.63293699222430, Lng: -96.36386983775320, Title: "Wellborn @ Tee", Icon: icon[0] }
                , { Lat: 30.63799966204160, Lng: -96.36171602378580, Title: "Villa Maria @ Green St. ", Icon: icon[0] }
                , { Lat: 30.63457500000000, Lng: -96.35462500000000, Title: "Blinn Athletics", Icon: icon[0] }
                , { Lat: 30.63109431452120, Lng: -96.35456257191670, Title: "Academic Village", Icon: icon[3] }
                , { Lat: 30.62609333551490, Lng: -96.35584064458850, Title: "Village on the Creek", Icon: icon[0] }];
            break;

        // Old Army South  
        case "15S":
            icon = [BUS_SITE_ROOT + "images/15OldArmy15x15Dot.png", BUS_SITE_ROOT + "images/15OldArmy15x15Square1.png", BUS_SITE_ROOT + "images/15OldArmy15x15Square2.png", BUS_SITE_ROOT + "images/15OldArmy15x15Square3.png", BUS_SITE_ROOT + "images/15OldArmy15x15Square4.png", BUS_SITE_ROOT + "images/15OldArmy15x15Square5.png", BUS_SITE_ROOT + "images/15OldArmy15x15Square6.png", BUS_SITE_ROOT + "images/15OldArmy15x15Square7.png", BUS_SITE_ROOT + "images/15OldArmy15x15Square8.png", BUS_SITE_ROOT + "images/15OldArmy15x15Square9.png"];
            return [ { Lat: 30.61271179394200, Lng: -96.34248726621200, Title: "MSC", Icon: icon[1] }
                , { Lat: 30.61952996966420, Lng: -96.34675063769520, Title: "College Main Parking Garage", Icon: icon[0] }
                , { Lat: 30.62194932233980, Lng: -96.34926923211030, Title: "Spruce St.", Icon: icon[0] }
                , { Lat: 30.62482330947060, Lng: -96.35252409294390, Title: "Aggie Station", Icon: icon[2] }
                , { Lat: 30.62192250024810, Lng: -96.34930275972500, Title: "Spruce St.", Icon: icon[0] }
                , { Lat: 30.61950448867700, Lng: -96.34678818862370, Title: "College Main Parking Garage", Icon: icon[0] }];
            break;

        // Excel       
        case "22":
            icon = [BUS_SITE_ROOT + "images/22Excel15x15Dot.png", BUS_SITE_ROOT + "images/22Excel15x15Square1.png", BUS_SITE_ROOT + "images/22Excel15x15Square2.png", BUS_SITE_ROOT + "images/22Excel15x15Square3.png", BUS_SITE_ROOT + "images/22Excel15x15Square4.png", BUS_SITE_ROOT + "images/22Excel15x15Square5.png", BUS_SITE_ROOT + "images/22Excel15x15Square6.png", BUS_SITE_ROOT + "images/22Excel15x15Square7.png", BUS_SITE_ROOT + "images/22Excel15x15Square8.png", BUS_SITE_ROOT + "images/22Excel15x15Square9.png"];
            return [ { Lat: 30.61353389105400, Lng: -96.33929275508480, Title: "Trigon", Icon: icon[1] }
                , { Lat: 30.62002222000000, Lng: -96.31725833000000, Title: "Campus View", Icon: icon[2] }
                , { Lat: 30.62149468788470, Lng: -96.31559275481660, Title: "Polo Club", Icon: icon[0] }
                , { Lat: 30.62394890927960, Lng: -96.31281130390240, Title: "Cripple Creek", Icon: icon[0] }
                , { Lat: 30.62360022208690, Lng: -96.31085463230940, Title: "Shivers", Icon: icon[0] }
                , { Lat: 30.62753099963250, Lng: -96.30648531356370, Title: "Plantation Oaks", Icon: icon[3] }
                , { Lat: 30.62648225584520, Lng: -96.30979515968530, Title: "Briarwood", Icon: icon[0] }
                , { Lat: 30.62407229090170, Lng: -96.31282471494820, Title: "Cripple Creek", Icon: icon[0] }
                , { Lat: 30.62155906090490, Lng: -96.31565042231380, Title: "Polo Club", Icon: icon[0] }
                , { Lat: 30.62030646922030, Lng: -96.31709479195440, Title: "The Cambridge", Icon: icon[4] }];
            break;

        // Rudder     
        case "26":
            icon = [BUS_SITE_ROOT + "images/26Rudder15x15Dot.png", BUS_SITE_ROOT + "images/26Rudder15x15Square1.png", BUS_SITE_ROOT + "images/26Rudder15x15Square2.png", BUS_SITE_ROOT + "images/26Rudder15x15Square3.png", BUS_SITE_ROOT + "images/26Rudder15x15Square4.png", BUS_SITE_ROOT + "images/26Rudder15x15Square5.png", BUS_SITE_ROOT + "images/26Rudder15x15Square6.png", BUS_SITE_ROOT + "images/26Rudder15x15Square7.png", BUS_SITE_ROOT + "images/26Rudder15x15Square8.png", BUS_SITE_ROOT + "images/26Rudder15x15Square9.png"];
            return [ { Lat: 30.61436269368900, Lng: -96.33973934291240, Title: "Trigon", Icon: icon[1] }
                , { Lat: 30.61123255558210, Lng: -96.32359914920320, Title: "Scandia", Icon: icon[0] }
                , { Lat: 30.60922894532850, Lng: -96.32085390811290, Title: "Lexington", Icon: icon[0] }
                , { Lat: 30.60711670560340, Lng: -96.31767146692700, Title: "Lemon Tree", Icon: icon[0] }
                , { Lat: 30.60619804896100, Lng: -96.31344028195380, Title: "Brentwood", Icon: icon[2] }
                , { Lat: 30.60565221939400, Lng: -96.31079294149830, Title: "Kroger", Icon: icon[0] }
                , { Lat: 30.60950387176890, Lng: -96.30657114425730, Title: "Southwest Parkway @ Cornell", Icon: icon[0] }
                , { Lat: 30.61214167000000, Lng: -96.30358611000000, Title: "Southwest Parkway @ Dartmouth", Icon: icon[0] }
                , { Lat: 30.61343889000000, Lng: -96.30210556000000, Title: "Hickory Park", Icon: icon[0] }
                , { Lat: 30.61550531479750, Lng: -96.29974760411660, Title: "Eastmark/Windsor Point", Icon: icon[0] }
                , { Lat: 30.61759722000000, Lng: -96.30206389000000, Title: "Trails @ Wolf Pen Creek", Icon: icon[3] }
                , { Lat: 30.61573611000000, Lng: -96.30389167000000, Title: "Carnation", Icon: icon[0] }
                , { Lat: 30.61271581725570, Lng: -96.30549155506440, Title: "Brentwood", Icon: icon[0] }
                , { Lat: 30.61173056000000, Lng: -96.30793611000000, Title: "Brentwood @ Cornell", Icon: icon[0] }
                , { Lat: 30.60802329230450, Lng: -96.31283007936660, Title: "Redstone", Icon: icon[4] }
                , { Lat: 30.60643140115930, Lng: -96.31570674870650, Title: "Denver", Icon: icon[0] }
                , { Lat: 30.60713145775390, Lng: -96.31762586937100, Title: "Lemon Tree", Icon: icon[0] }
                , { Lat: 30.60901436859460, Lng: -96.32052667859360, Title: "Lexington", Icon: icon[0] }
                , { Lat: 30.61160538265740, Lng: -96.32406585359960, Title: "Scandia", Icon: icon[0] }
                , { Lat: 30.61252001598600, Lng: -96.32519104034850, Title: "Taos", Icon: icon[5] }];
            break;

        // Ring Dance     
        case "27":
            icon = [BUS_SITE_ROOT + "images/27RingDance15x15Dot.png", BUS_SITE_ROOT + "images/27RingDance15x15Square1.png", BUS_SITE_ROOT + "images/27RingDance15x15Square2.png", BUS_SITE_ROOT + "images/27RingDance15x15Square3.png", BUS_SITE_ROOT + "images/27RingDance15x15Square4.png", BUS_SITE_ROOT + "images/27RingDance15x15Square5.png", BUS_SITE_ROOT + "images/27RingDance15x15Square6.png", BUS_SITE_ROOT + "images/27RingDance15x15Square7.png", BUS_SITE_ROOT + "images/27RingDance15x15Square8.png", BUS_SITE_ROOT + "images/27RingDance15x15Square9.png"];
            return [ { Lat: 30.61456117716800, Lng: -96.33946039315820, Title: "Trigon", Icon: icon[1] }
                , { Lat: 30.61123255558210, Lng: -96.32359914920320, Title: "Scandia", Icon: icon[0] }
                , { Lat: 30.60922894532850, Lng: -96.32085390811290, Title: "Lexington", Icon: icon[0] }
                , { Lat: 30.61183873485560, Lng: -96.31842919101890, Title: "Enclave", Icon: icon[0] }
                , { Lat: 30.61616111494070, Lng: -96.31207771969300, Title: "Arbors @ Wolf Pen Creek", Icon: icon[0] }
                , { Lat: 30.61778921590980, Lng: -96.31010763705420, Title: "River Oaks Townhomes", Icon: icon[0] }
                , { Lat: 30.61837125530070, Lng: -96.30803428936210, Title: "Lofts @ Wolf Pen Creek", Icon: icon[0] }
                , { Lat: 30.62071148280570, Lng: -96.30452193644770, Title: "Mall Park - n - Ride", Icon: icon[2] }
                , { Lat: 30.61842624058880, Lng: -96.30817376423920, Title: "Lofts @ Wolf Pen Creek", Icon: icon[0] }
                , { Lat: 30.61784420119790, Lng: -96.31020285547990, Title: "River Oaks Townhomes", Icon: icon[0] }
                , { Lat: 30.61567831728920, Lng: -96.31268792228030, Title: "Arbors @ Wolf Pen Creek", Icon: icon[0] }
                , { Lat: 30.61190713118950, Lng: -96.31851099839870, Title: "HEB", Icon: icon[3] }
                , { Lat: 30.61081815426460, Lng: -96.32026516319900, Title: "Village", Icon: icon[0] }
                , { Lat: 30.61160538265740, Lng: -96.32406585359960, Title: "Scandia", Icon: icon[4] }
                , { Lat: 30.61252001598600, Lng: -96.32519104034850, Title: "Taos", Icon: icon[0] }];
            break;

        // Elephant Walk     
        case "31":
            icon = [BUS_SITE_ROOT + "images/31ElephantWalk15x15Dot.png", BUS_SITE_ROOT + "images/31ElephantWalk15x15Square1.png", BUS_SITE_ROOT + "images/31ElephantWalk15x15Square2.png", BUS_SITE_ROOT + "images/31ElephantWalk15x15Square3.png", BUS_SITE_ROOT + "images/31ElephantWalk15x15Square4.png", BUS_SITE_ROOT + "images/31ElephantWalk15x15Square5.png", BUS_SITE_ROOT + "images/31ElephantWalk15x15Square6.png", BUS_SITE_ROOT + "images/31ElephantWalk15x15Square7.png", BUS_SITE_ROOT + "images/31ElephantWalk15x15Square8.png", BUS_SITE_ROOT + "images/31ElephantWalk15x15Square9.png"];
            return [ { Lat: 30.61393756353480, Lng: -96.33965217111420, Title: "Trigon", Icon: icon[1] }
                , { Lat: 30.59784967290460, Lng: -96.33099534100260, Title: "Oney Hervey", Icon: icon[2] }
                , { Lat: 30.59929404254520, Lng: -96.32646911302020, Title: "Willow Rock", Icon: icon[0] }
                , { Lat: 30.59736955746230, Lng: -96.32490404396680, Title: "Renaissance Park", Icon: icon[0] }
                , { Lat: 30.59564444000000, Lng: -96.32272500000000, Title: "Yellow House", Icon: icon[0] }
                , { Lat: 30.59750000000000, Lng: -96.32116389000000, Title: "Colony", Icon: icon[3] }
                , { Lat: 30.59524444000000, Lng: -96.32368333000000, Title: "Renaissance Park", Icon: icon[0] }
                , { Lat: 30.59382904135160, Lng: -96.32622637308990, Title: "Willowick/Madison Point", Icon: icon[0] }
                , { Lat: 30.59331003387630, Lng: -96.32712223095430, Title: "Parkway Circle", Icon: icon[4] }
                , { Lat: 30.59188889000000, Lng: -96.32951111000000, Title: "The Woodlands", Icon: icon[0] }];
            break;

        // Texas Aggies     
        case "33":
            icon = [BUS_SITE_ROOT + "images/33TexasAggies15x15Dot.png", BUS_SITE_ROOT + "images/33TexasAggies15x15Square1.png", BUS_SITE_ROOT + "images/33TexasAggies15x15Square2.png", BUS_SITE_ROOT + "images/33TexasAggies15x15Square3.png", BUS_SITE_ROOT + "images/33TexasAggies15x15Square4.png", BUS_SITE_ROOT + "images/33TexasAggies15x15Square5.png", BUS_SITE_ROOT + "images/33TexasAggies15x15Square6.png", BUS_SITE_ROOT + "images/33TexasAggies15x15Square7.png", BUS_SITE_ROOT + "images/33TexasAggies15x15Square8.png", BUS_SITE_ROOT + "images/33TexasAggies15x15Square9.png"];
            return [ { Lat: 30.61454240170370, Lng: -96.33909024829210, Title: "Trigon", Icon: icon[1] }
                , { Lat: 30.59807766068440, Lng: -96.30687691610320, Title: "Doux Chene/Valley View", Icon: icon[2] }
                , { Lat: 30.59967357514340, Lng: -96.30535476239650, Title: "Brookwood", Icon: icon[0] }
                , { Lat: 30.59599090194650, Lng: -96.30154334315920, Title: "Airline", Icon: icon[0] }
                , { Lat: 30.59515807599770, Lng: -96.30012177229650, Title: "Cambridge Court", Icon: icon[0] }
                , { Lat: 30.59366676769650, Lng: -96.29578866337470, Title: "Peppertree", Icon: icon[3] }
                , { Lat: 30.59102076834560, Lng: -96.29291333513940, Title: "Treehouse", Icon: icon[0] }
                , { Lat: 30.58606806910440, Lng: -96.29224144174110, Title: "Bluestem", Icon: icon[0] }
                , { Lat: 30.58448958900510, Lng: -96.29520394177460, Title: "Adrienne", Icon: icon[0] }
                , { Lat: 30.58299962180850, Lng: -96.29960008261200, Title: "Ponderosa @ Brothers Pond Park", Icon: icon[4] }
                , { Lat: 30.58681908767340, Lng: -96.30531989367720, Title: "Van Horn", Icon: icon[0] }
                , { Lat: 30.59050278000000, Lng: -96.30896667000000, Title: "Rio Grande @ Balcones", Icon: icon[0] }];
            break;

        // Fish Camp     
        case "34":
            icon = [BUS_SITE_ROOT + "images/34FishCamp15x15Dot.png", BUS_SITE_ROOT + "images/34FishCamp15x15Square1.png", BUS_SITE_ROOT + "images/34FishCamp15x15Square2.png", BUS_SITE_ROOT + "images/34FishCamp15x15Square3.png", BUS_SITE_ROOT + "images/34FishCamp15x15Square4.png", BUS_SITE_ROOT + "images/34FishCamp15x15Square5.png", BUS_SITE_ROOT + "images/34FishCamp15x15Square6.png", BUS_SITE_ROOT + "images/34FishCamp15x15Square7.png", BUS_SITE_ROOT + "images/34FishCamp15x15Square8.png", BUS_SITE_ROOT + "images/34FishCamp15x15Square9.png"];
            return [ { Lat: 30.61442974891840, Lng: -96.33898832434350, Title: "Trigon", Icon: icon[1] }
                , { Lat: 30.58746013566610, Lng: -96.31497718781100, Title: "Welsh @ First Baptist", Icon: icon[0] }
                , { Lat: 30.58479167000000, Lng: -96.31726944000000, Title: "Westridge @ Pintail", Icon: icon[0] }
                , { Lat: 30.58277778000000, Lng: -96.31856667000000, Title: "Westridge @ Antelope", Icon: icon[2] }
                , { Lat: 30.58157939205050, Lng: -96.31636523105890, Title: "Navarro @ Antelope", Icon: icon[0] }
                , { Lat: 30.58366749189300, Lng: -96.31510861606050, Title: "Navarro @ Pintail", Icon: icon[0] }
                , { Lat: 30.58369297288020, Lng: -96.31204687428750, Title: "Welsh @ San Benito", Icon: icon[3] }
                , { Lat: 30.58057356360990, Lng: -96.31131329007820, Title: "Deacon @ Aztec", Icon: icon[0] }
                , { Lat: 30.57734686597270, Lng: -96.31399684035760, Title: "Fraternity Row #1", Icon: icon[0] }
                , { Lat: 30.57611841417060, Lng: -96.31447159138150, Title: "Fraternity Row #2", Icon: icon[4] }];
            break;

        // Cotton Bowl     
        case "36":
            icon = [BUS_SITE_ROOT + "images/36CottonBowl15x15Dot.png", BUS_SITE_ROOT + "images/36CottonBowl15x15Square1.png", BUS_SITE_ROOT + "images/36CottonBowl15x15Square2.png", BUS_SITE_ROOT + "images/36CottonBowl15x15Square3.png", BUS_SITE_ROOT + "images/36CottonBowl15x15Square4.png", BUS_SITE_ROOT + "images/36CottonBowl15x15Square5.png", BUS_SITE_ROOT + "images/36CottonBowl15x15Square6.png", BUS_SITE_ROOT + "images/36CottonBowl15x15Square7.png", BUS_SITE_ROOT + "images/36CottonBowl15x15Square8.png", BUS_SITE_ROOT + "images/36CottonBowl15x15Square9.png"];
            return [ { Lat: 30.61381820522660, Lng: -96.33955024716560, Title: "Trigon", Icon: icon[1] }
                , { Lat: 30.60224983705580, Lng: -96.33923642869210, Title: "Treehouse", Icon: icon[2] }
                , { Lat: 30.60059223178580, Lng: -96.33853100767920, Title: "Stadium View", Icon: icon[0] }
                , { Lat: 30.59528011651520, Lng: -96.33632891394670, Title: "The Gateway", Icon: icon[0] }
                , { Lat: 30.59397790396080, Lng: -96.33673660974120, Title: "Meadows Point", Icon: icon[0] }
                , { Lat: 30.59272128896240, Lng: -96.33816756833600, Title: "Gridiron", Icon: icon[0] }
                , { Lat: 30.58976683555640, Lng: -96.33805625665520, Title: "The Zone/Walden Pond", Icon: icon[3] }
                , { Lat: 30.58988887607390, Lng: -96.34654142537960, Title: "The Heights", Icon: icon[0] }
                , { Lat: 30.59196624707970, Lng: -96.34420522118840, Title: "Fox Run", Icon: icon[0] }
                , { Lat: 30.59392291867280, Lng: -96.34204201948890, Title: "The District", Icon: icon[4] }
                , { Lat: 30.59536389000000, Lng: -96.34043611000000, Title: "West Luther @ Jones", Icon: icon[0] }
                , { Lat: 30.59721667153930, Lng: -96.33844249477640, Title: "West Luther @ Marion Pugh", Icon: icon[0] }
                , { Lat: 30.60164097557310, Lng: -96.33893199795080, Title: "Callaway Villas", Icon: icon[5] }];
            break;

        case "MSC":
            return [{ Lat: 30.612458, Lng: -96.341273, Title: "MSC", Icon: "http://m-test.tamu.edu/images/apps/map/default.png"}];
            break;

        default: // no route found, return an empty array?
            return [{ Lat: 30.612458, Lng: -96.341273, Title: "MSC", Icon: "http://m-test.tamu.edu/images/apps/map/default.png"}];
            break;
    };
};
