// Create Project Carusel
$(document).ready(function (ev) {
    $('#create-carusel').on('slide.bs.carousel', function (evt) {
        $('#create-carusel .controls li.active').removeClass('active');
        $('#create-carusel .controls li:eq(' + $(evt.relatedTarget).index() + ')').addClass('active');
    })
});

// test function for highlighting menu section of current page
$(document).ready(function (ev) {
    var urlPieces = document.URL.split('http://').join('').split('/');
    if (urlPieces[1] == '') {
        $("#menu-list li").first().addClass("active");
    } else {
        $("#menu-list li")
          .filter(function (index) {
              return $(this).text().trim() == urlPieces[1];
          }).addClass("active");
    }
});