// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

var url = "http://localhost:5001/";

$(document).ready(function () {
  $("#postInfo").click(async function () {
    var inputName = $("#visitorName").val();
    $.ajax(url + "person", {
      data: JSON.stringify({
        Name: inputName
      }),
      method: "POST",
      contentType: "application/json",
      success: function (results) {
        alert("Welcome to the SpacePark, my dear " + results.name + ".");
        showBooking(results);

      }                 
    });
  });
});

$(document).ready(function () {
  $("#deleteInfo").click(async function () {
    var inputName = $("#leavingVisitorName").val();
    $.ajax(url + "person", {
      data: JSON.stringify({
        Name: inputName,
      }),
      method: "GET",
      contentType: "application/json",
      success: function (results) {
        results.forEach((element) => {
          if (element.name == inputName) {
            deletePerson(element, element.personID);
          }
        });
      },
    });
  });
});

function deletePerson(person, personID) {
  $.ajax(url + "person/" + personID, {
    method: "DELETE",
    contentType: "application/json",
    success: function (results) {
      alert("Thank you, come again!" + person);

      $.ajax(url + "parkinglot/" + 4, {
        data: JSON.stringify({
        IsOccupied: false,
        starship: person.starship
        }),
        method: "PUT",
        contentType: "application/json",
        success: function (results) {
        alert("check mark" + person);  
       
       $.ajax(url + "starships/" + person.starshipID, {
        method: "DELETE",
        contentType: "application/json",
        success: function (result) {
       alert("You" + person.name + " have left with your starship with ID " + person.starshipID + ".");  
       
       
  }});
       
  }});
}});
}

function updatePerson(person, starship){
  $.ajax(url + "person/" + person.personID, 
  {
    data: JSON.stringify({
    starshipID: starship.starshipID
    }),
    method: "PUT",
    contentType: "application/json",
    success: function (results) {
    alert("check mark " + starship.starshipID + " " + person);  
  }
});

}

function showBooking(person){

  $.ajax(url + "starships", {
      data: JSON.stringify({
        starshipID: person.starship.starshipID,
        Name: person.starship.name,   
        Length: person.starship.length,     
      }),
      method: "POST",
      contentType: "application/json",
      success: function (result){
        alert("you're in " + result.starshipID + " " + result.name);
        updatePerson(person, result);
        
        $.ajax(url + "parkinglot/" + 4, {
        data: JSON.stringify({
        IsOccupied: true,
        starship: result
        }),
        method: "PUT",
        contentType: "application/json",
        success: function (results) {
       alert("You have the parkinglot with id " + results.ParkinglotID + ".");
      
      
    },

  });
  }
  });

  

}