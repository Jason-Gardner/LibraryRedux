// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function displayUser() {
    var user = document.getElementById("user").value;
    if (user != null) {
        var info = document.getElementById("userName").innerHTML;
        info = user;
    }
}
