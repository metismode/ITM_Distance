


var origin1 = "55.930, -3.118";
var origin2 = 'Greenwich, England';
var destinationA = 'Stockholm, Sweden';
var destinationB = "50.087, 14.421";

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
}

function callback(response, status) {

    console.log(response)

    if (status != google.maps.DistanceMatrixStatus.OK) {
        alert('Error was: ' + status);
    } else {
        var origins = response.originAddresses;
        var destinations = response.destinationAddresses;
        var outputDiv = document.getElementById('outputDiv');
       
        for (var i = 0; i < origins.length; i++) {
            var results = response.rows[i].elements;
           
            for (var j = 0; j < results.length; j++) {
               
                outputDiv.innerHTML += origins[i] + ' to ' + destinations[j]
                    + ': ' + results[j].distance.text + ' in '
                    + results[j].duration.text + '<br>';
            }
        }
    }
}





