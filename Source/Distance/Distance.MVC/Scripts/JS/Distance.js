
var htmlstring = "";

function calculateDistances(ori, des) {

    var service = new google.maps.DistanceMatrixService();
    service.getDistanceMatrix(
      {
          origins: ori,
          destinations: des,
          travelMode: google.maps.TravelMode.DRIVING,
          unitSystem: google.maps.UnitSystem.METRIC,
          avoidHighways: false,
          avoidTolls: false
      }, callback);

    htmlstring = "";

}

function callback(response, status) {

    console.log(response)

    if (status != google.maps.DistanceMatrixStatus.OK) {
        alert('Error was: ' + status);
    } else {
        var origins = response.originAddresses;
        var destinations = response.destinationAddresses;

        for (var i = 0; i < origins.length; i++) {

            var myLatlngO = new google.maps.LatLng(ori[i]);

            console.log(ori[i])
            console.log(myLatlngO)


            var results = response.rows[i].elements;
            htmlstring += '<center><div class="table-responsive"><table border="1" class="table table-bordered table-hover tablesorter text-center table-striped tablesorter-default" style="width: 95%">'
                            + '<tr class="success">'
                             + '<th style="display:none;">' + ori[i] + '</th>'
                            + '<th colspan="2" style="text-align:center">&nbsp;' + checkNameP(orip[i]) + " " + checkNameA(oria[i]) + '</th>'
                            + '<th colspan="3" style="text-align:center">&nbsp;' + origins[i] + '</th>'
                            + '</tr>'
                            + '<tr class="info">'
                           + '<th style="width: 25px; text-align:center">No. </th>'
                           + '<th style="width: 100px; text-align:center">Destination </th>'
                           + '<th style="width: 200px; text-align:center">Destination </th>'
                           + '<th style="width: 75px; text-align:center">Distance (km)</th>'
                           + '<th style="width: 75px; text-align:center">Duration (hr)</th>'
                           + '</tr>';

            for (var j = 0 ; j < destinations.length ; j++) {

                var myLatlngD = new google.maps.LatLng(des[j]);

                console.log(des[j])
                console.log(myLatlngD)

                htmlstring += '<tr>'
                            + '<td style="display:none;">' + des[j] + '</th>'
                            + '<td>' + (j + 1) + '</td>'
                             + '<td>' + checkNameP(desp[j]) + " " + checkNameA(desa[j]) + '  ' + '<button class="btn btn-danger btn-xsm" onclick="Map(' + myLatlngO + ',' + myLatlngD + ')" type="button">Map</button></td>'
                            + '<td>' + destinations[j] + '</td>'
                            + '<td>' + results[j].distance.text + '</td>'
                            + '<td>' + results[j].duration.text + '</td>'
                            + '</tr>';

            }
        }

        htmlstring += '</table></div></center>';

        var outputDiv = document.getElementById('outputDiv');
        outputDiv.innerHTML = htmlstring;
    }
}


    function checkNameP(name) {
        if (name != " - ") {
            return "จ." + name
        } else {
            return ""
        }
    }
    function checkNameA(name){
        if(name!=" - "){
            return "อ." +name
        } else {
            return ""
        }
    }






