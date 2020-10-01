// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

var url = "https://localhost:5001/";
var person;
var starship;
var parkinglotID;


$(document).ready(function () {
  getFreeParkinglots();
  $("#postInfo").click(async function () {
    var inputName = $("#visitorName").val();
    validateAndPostPerson(inputName);
  });
});



function getFreeParkinglots() {
  $.ajax(url + "parkinglot", {
    data: JSON.stringify({
    }),
    method: "GET",
    contentType: "application/json",
    success: function (results) {
      appendLots(results);
    },
    error: function (jqXHR) {
      document.getElementById("visitorInformation").innerHTML = "Sorry It's Full!";
      alert("Error: " + jqXHR.responseText);
    }
  });
}
function appendLots(parkingLots) {
  parkingLots.forEach(lot => {
    $('#lots').append("<li class='lot' id='" + lot.parkinglotID + "' onClick='parkTheStarship(this.id)'>" + lot.parkinglotID + "</li>");
    $('#lots').append("<div class='lotInfo' id='" + lot.parkinglotID + "'>Length: " + lot.length + " | Cost: " + lot.cost + "</li>");
  });
}

function parkTheStarship(choosenParkinglotID) {
  parkinglotID = choosenParkinglotID
  alert("You have choosen to park on parkinglot with ID: " + parkinglotID);
}



function validateAndPostPerson(inputName) {
  $.ajax(url + "person", {
    data: JSON.stringify({
      Name: inputName
    }),
    method: "POST",
    contentType: "application/json",
    success: function (results) {
      person = results;
      alert("Welcome to the SpacePark, dear " + results.name + ".");
      addStarshipToParkinglot(parkinglotID, person.starship);
    },
    error: function (jqXHR) { alert("Error: " + jqXHR.responseText); }
  });
}

function addStarshipToParkinglot(parkinglotID, starshipToPark) {
  $.ajax(url + "parkinglot/" + parkinglotID, {
    data: JSON.stringify({
      Starship: starshipToPark,
      IsOccupied: true
    }),
    method: "PUT",
    contentType: "application/json",
    success: function () {
      alert("Your beautiful " + starshipToPark.name + " is now parked on the parkinglot with ID: " + parkinglotID + ".");
      document.location.reload();
    },
    error: function (jqXHR) { alert("Error: " + jqXHR.responseText); }
  });
}


$(document).ready(function () {
  $("#deleteInfo").click(async function () {
    var inputName = $("#leavingVisitorName").val();
    ValidateLeavingPerson(inputName)
  });
});

function ValidateLeavingPerson(inputName) {
  $.ajax(url + "person/?name=" + inputName, {
    data: JSON.stringify({
      Name: inputName
    }),
    method: "GET",
    contentType: "application/json",
    success: function (result) {
      person = result[0];
      alert("Found " + inputName + " in the parking.");
      ValidateLeavingStarship(person.starshipID);
    },
    error: function (jqXHR) { alert("Error: " + jqXHR.responseText); }
  });
}

function ValidateLeavingStarship(starshipID) {
  $.ajax({
    url: url + "starship/" + starshipID,
    type: 'GET',
    success: function (result) {
      starship = result;
      alert("Found " + starship.name + " in the parkinglot with ID: " + starship.parkinglotID + ".");
      LeaveParkinglot(starship);
    },
    error: function (jqXHR) { alert("Error: " + jqXHR.responseText); }
  });
}

function LeaveParkinglot(starship) {
  $.ajax(url + "parkinglot/" + starship.parkinglotID, {
    data: JSON.stringify({
      IsOccupied: false
    }),
    method: "PUT",
    contentType: "application/json",
    success: function () {
      alert("We have now placed the ship outside the parking.");
      DeleteLeavingStarship(starship);
    },
    error: function (jqXHR) { alert("Error: " + jqXHR.responseText); }
  });
}

function DeleteLeavingStarship(starship) {
  $.ajax(url + "starship/" + starship.starshipID, {
    method: "DELETE",
    contentType: "application/json",
    success: function () {
      alert("You have payed the parking fee of 500 space credits. Goodbye and safe travels.");
      document.location.reload();
    },
    error: function (jqXHR) { alert("Error: " + jqXHR.responseText); }
  });
}
