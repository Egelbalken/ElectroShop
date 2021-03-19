// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Goal is to connect a rating system(text and stars to each product item) only one rating is allowed per user.
// We are using javaScript to rout and display the data.


//let starInput = 4;

//$('#star-rate').ready(function () {
//    let starInput = $('.rate > .average-rate').val();
//    let productId = $('.rate > .product-id').val();

//    console.log("Product ID:", productId);
//    console.log("Average Rating:", starInput);

//    $('#star' + starInput).prop('checked', true);
//});

$('.rate').each((i, rate) => {
    let startInput = $(rate).find(".average-rate").val();
    $(rate).find('#star' + startInput).prop("checked", true);
});
