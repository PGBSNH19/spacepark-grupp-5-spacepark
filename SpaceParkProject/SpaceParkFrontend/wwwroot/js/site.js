﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

var url = "https://localhost:5001/";
var person;
var starship;
var parkinglotID = 5;


$(document).ready(function () {
  $("#postInfo").click(async function () {
    var inputName = $("#visitorName").val();
    validateAndPostPerson(inputName);
  });
});

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
  $.ajax(url + "person", {
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
    url: url + "starships/" + starshipID,
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
  $.ajax(url + "starships/" + starship.starshipID, {
    method: "DELETE",
    contentType: "application/json",
    success: function () {
      alert("You have payed the parking fee of 500 space credits. Goodbye and safe travels, " + person.name + ".");
      //DeleteLeavingPerson(person)
    },
    error: function (jqXHR) { alert("Error: " + jqXHR.responseText); }
  });
}

//Don't need this funciton since person gets deleted when starship is deleted 

// function DeleteLeavingPerson(person) {
//   $.ajax(url + "person", {
//     data: JSON.stringify({
//       Name: person.name
//     }),
//     method: "DELETE",
//     contentType: "application/json",
//     success: function () {

//       alert("Goodbye, " + person.name + ". See you soon!");
//     },
//     error: function (jqXHR, textStatus, errorThrown) { alert("Error: " + jqXHR.responseText); }
//   });
// }
