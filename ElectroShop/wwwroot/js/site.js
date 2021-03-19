// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Goal is to connect a rating system(text and stars to each product item) only one rating is allowed per user.
// We are using javaScript to rout and display the data.



$('.rate').each((i, rate) => {
    let startInput = $(rate).find(".average-rate").val();
    $(rate).find('#star' + startInput).prop("checked", true);
});

