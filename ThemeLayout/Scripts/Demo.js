

var source, destination;
var directionsDisplay;
var directionsService;
var map;
var uluru;
function initMap() {

  directionsService = new google.maps.DirectionsService();

  // initialise the location of the map on Chichester in England (ref lat and lng)
  uluru = { lat: 23.777176, lng: 90.399452 };
  map = new google.maps.Map(document.getElementById('dvMap'), {
    center: uluru,
    zoom: 13,
    mapTypeId: 'roadmap'
  });

  var el = $("#travelfrom");

  google.maps.event.addDomListener(window, 'load', function () {
    //new google.maps.places.SearchBox(el);
    //new google.maps.places.SearchBox(document.getElementById('travelto'));
    directionsDisplay = new google.maps.DirectionsRenderer({ 'draggable': true });
  });

  var marker = new google.maps.Marker({
    position: uluru,
    map: map
  });

  //directionsDisplay.setMap(map);



}

function GetRoute() {
  //var source = document.getElementById("travelfrom").value;
  //var destination = document.getElementById("travelto").value;

  var source = $("#travelfrom").val();
  var destination = $("#travelto").val();

  var request = {
    origin: source,
    destination: destination,
    travelMode: google.maps.TravelMode.DRIVING
  };

  directionsService.route(request, function (response, status) {
    if (status == google.maps.DirectionsStatus.OK) {
      directionsDisplay.setDirections(response);
    }
  });

  //*********DISTANCE AND DURATION**********************//
  var service = new google.maps.DistanceMatrixService();
  service.getDistanceMatrix({
    origins: [source],
    destinations: [destination],
    travelMode: google.maps.TravelMode.DRIVING,
    unitSystem: google.maps.UnitSystem.METRIC,
    avoidHighways: false,
    avoidTolls: false
  }, function (response, status) {

    if (status == google.maps.DistanceMatrixStatus.OK && response.rows[0].elements[0].status != "ZERO_RESULTS") {
      var distance = response.rows[0].elements[0].distance.text;
      var duration = response.rows[0].elements[0].duration.value;
      var dvDistance = document.getElementById("dvDistance");
      duration = parseFloat(duration / 60).toFixed(2);
      dvDistance.innerHTML = "";
      dvDistance.innerHTML += "Distance: " + distance + "<br />";
      dvDistance.innerHTML += "Time:" + duration + " min";

    } else {
      alert("Unable to find the distance via road.");
    }
  });

}

