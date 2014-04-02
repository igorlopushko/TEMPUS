// Create Project Carusel
$(document).ready(function (ev) {
    $('#create-carusel').on('slide.bs.carousel', function (evt) {
        $('#create-carusel .controls li.active').removeClass('active');
        $('#create-carusel .controls li:eq(' + $(evt.relatedTarget).index() + ')').addClass('active');
    })
});