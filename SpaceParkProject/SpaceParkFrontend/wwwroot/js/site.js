// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

// //document.getElementById("new-todo").onkeydown = function(pressEnter){
    
//     if(pressEnter.keyCode === 13){
//         let response = document.getElementById("new-todo").value;      
     
//        todos.push(response);
//        localStorage.setItem("theTodos", JSON.stringify(todos));
//       // console.log(localStorage.getItem("theTodos"));
        
       
//        printStuff();
            
//         //resets input after pressing enter so it is empty
//         pressEnter.preventDefault();
//         $(this).val('');
//         return false;
       
        
//     }
// };//

// document.getElementById("input-data").onclick = function(){
   
//     let response = document.getElementById("fname").value; 
//     console.log(response);

// }


// // Attach a submit handler to the form
// $( "#visitorForm" ).submit(function( event ) {
 
//   // Stop form from submitting normally
//   event.preventDefault();
 
//   // Get some values from elements on the page:
//   var $form = $( this ),
//     term = $form.find( "input[name='s']" ).val(),
//     url = $form.attr( "action" );
 
//   // Send the data using post
//   var posting = $.post( url, { s: term } );
 
//   // Put the results in a div
//   posting.done(function( data ) {
//     var content = $( data ).find( "#content" );
//     $( "#result" ).empty().append( content );
//   });
// });


// var inputN = $("#visitorName").val(); 

// $(document).ready(function(){
   
//     $("#postInfo").click(function(){

//         console.log($("#visitorName").val());
//         $.postJSON = function(callback) {
//             return jQuery.ajax({
//             headers: { 
//                 'Accept': 'application/json',
//                 'Content-Type': 'application/json' 
//             },
//             'type': 'POST',
//             'url': 'https://localhost:44328/person',
//             'data': { 'name' : $("#visitorName").val()},
//             'dataType': 'json',
//             'success': callback            
//             });          
//         };           
//     });
//   });

$("#postInfo").click(function(){

    $.ajax({
        url : "https://localhost:44328/person",
        data: JSON.stringify({
            Name : $("#visitorName").val(),
        }),
        type: "POST",
        contentType: "application/json; charset=utf-8"
    }).done(function(response, statusText, xhr) {
        if(xhr.status == 200) {
            console.log(statusText + ". Hello " + response.Name);
            successMessage += "";
            
        }
    }).fail(function(){
       console.log("error");
    });

});