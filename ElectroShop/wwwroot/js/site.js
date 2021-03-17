// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Goal is to connect a rating system(text and stars to each product item) only one rating is allowed per user.
// We are using javaScript to rout and display the data.

function StarRatings(id) {

    $.get("RatingsAndReviews/StarRatings", (rout) => {

        let routing = rout;
        console.log(routing);
        $(".rate").html(routing);
    });
}

//    $(function () {
//        $('.rate').rating('readOnly', true);
//        $('#rater').hide();
//        $('#rated').mouseover(function () {
//            $('#rated').hide();
//            $('#rater').show();
//        });

//        $('.auto-submit-star').rating({
//            callback: function (value, link) {
//                $.ajax({
//                    type: "POST",
//                    url: "/Article/Rate",
//                    data: $("#rate").serialize(),
//                    dataType: "text/plain",
//                    success: function (response) {
//                        if (response != 'false') {
//                            var data = eval('(' + response + ')');
//                            alert('Your rating has been recorded');
//                            $('#currentlyrated').html('Currently rated ' + data.AverageRating.toFixed(2) +
//                                ' by ' + data.TotalRaters + ' people');
//                        } else {
//                            alert('You have already rated this article');
//                        }
//                        $('#rater').hide();
//                        $('#rated').show();
//                    },
//                    error: function (response) {
//                        alert('There was an error.');
//                    }
//                });
//            }
//        });
//    });
//}