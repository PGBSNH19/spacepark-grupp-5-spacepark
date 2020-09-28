// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

$(document).ready(function () {
  $("#postInfo").click(async function () {
    var inputName = $("#visitorName").val();
    $.ajax("https://localhost:44328/person", {
      data: JSON.stringify({
        Name: inputName,
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
    $.ajax("https://localhost:44328/person", {
      data: JSON.stringify({
        Name: inputName,
      }),
      method: "GET",
      contentType: "application/json",
      success: function (results) {
        results.forEach((element) => {
          if (element.name == inputName) {
            deletePerson(element.personID);
          }
        });
      },
    });
  });
});

function deletePerson(personID) {
  $.ajax("https://localhost:44328/person" + personID, {
    method: "DELETE",
    contentType: "application/json",
    success: function (results) {
      alert("Thank you, come again!");
    },
  });
}

function showBooking(person){

  $.ajax("https://localhost:44328/starships", {
      data: JSON.stringify({
        Name: person.starship.name,   
        Length: person.starship.length     
      }),
      method: "POST",
      contentType: "application/json",
      success: function (result){
        alert("you're in" + result.name);
        
        $.ajax("https://localhost:44328/parkinglot/" + 1, {
        data: JSON.stringify({
        IsOccupied: true,
        StarshipID : result.starshipID,
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