
(function ($) {
    $.fn.star_check = function () {
        this.addClass('fa-star').removeClass('fa-star-o');
    };

    $.fn.star_uncheck = function () {
        this.addClass('fa-star-o').removeClass('fa-star');
    };
})(jQuery);

$(function () {

    $(".rating input").hide();
    $('#review-form').hide();

    // uncheck all
    $(".rating label i").star_uncheck();

    $(".rating label i").hover(
        function () {
            $(this).star_check();
            $(this).parent().prevAll("label").find("i").star_check();
        },
        function() {
            $(this).not(".selected").star_uncheck();
            $(this).parent().prevAll("label").find("i:not(.selected)").star_uncheck();
        }
    );

    $(".rating label i").click(function () {
        $('#review-form').slideDown();
        $(this).addClass('selected');
        $(this).parent().prevAll("label").find("i").addClass('selected');
        $(this).parent().nextAll("label").find("i").removeClass('selected').star_uncheck();
    });
});



