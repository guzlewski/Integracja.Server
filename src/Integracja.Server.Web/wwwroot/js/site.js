$('#sidebarCollapse').on('click touchstart touchend', function () {
    $('#sidebar, .content').toggleClass('active');
});

// zabrane z https://stackoverflow.com/questions/12744145/how-to-remember-scroll-position-of-page/12744617
// i przeniesione na https://github.com/js-cookie/js-cookie
$(document).ready(function() {

    // If cookie is set, scroll to the position saved in the cookie.
    if ( Cookies.get("scroll") !== null ) {
        $(document).scrollTop( Cookies.get("scroll") );
    }

    // When a button is clicked...
    $('.btn').on("click", function() {

        // Set a cookie that holds the scroll position.
        Cookies.set("scroll", $(document).scrollTop() );
    });
});