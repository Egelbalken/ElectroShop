// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Goal is to connect a rating system(text and stars to each product item) only one rating is allowed per user.
// We are using javaScript to rout and display the data.



$('.rate').each((i, rate) => {
    let startInput = $(rate).find(".average-rate").val();
    $(rate).find('#star' + startInput).prop("checked", true);
});

$(function () {
    $(".item").first().addClass("active");
    $("ol.carousel-indicators").find("li").eq(0).addClass("active");
})

/* When the user clicks on the button, 
toggle between hiding and showing the dropdown content */
function myFunction() {
    document.getElementById("myDropdown").classList.toggle("show");
}

// Close the dropdown if the user clicks outside of it
window.onclick = function (event) {
    if (!event.target.matches('.dropbtn')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}