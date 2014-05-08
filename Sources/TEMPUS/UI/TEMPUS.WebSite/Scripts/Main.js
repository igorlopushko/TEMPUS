// Create Project Carusel
$(document).ready(function (ev) {
    $('#create-carusel').on('slide.bs.carousel', function (evt) {
        $('#create-carusel .controls li.active').removeClass('active');
        $('#create-carusel .controls li:eq(' + $(evt.relatedTarget).index() + ')').addClass('active');
    });
});

jQuery(function ($) {
    $('[role="datepicker"]').datetimepicker({
        pickTime: false
    });
});

/* Noscroll for collapsing navbar */
$(document).ready(function (ev) {
    $('.navbar-collapse').on('shown.bs.collapse', function () {
        var navHeight = $(".navbar-header").height();
        var windowHeight = $(window).height();
        $(".navbar-collapse").css("max-height", (windowHeight - navHeight) + "px");
    });
});
