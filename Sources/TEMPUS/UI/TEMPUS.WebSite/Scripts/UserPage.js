$(document).ready( function(){
    var c = $('#respondCanvas');
    var ct = c.get(0).getContext('2d');
    var container = $(c).parent();
    $(window).resize( respondCanvas );
    function respondCanvas(){ 
        c.attr('width', $(container).width() );
    }
    respondCanvas();
}); 

function setMood(mood) {
    var img = "/Content/Images/" + mood + "_mood.png";
    var color;
    switch (mood) {
        case 1:
            color = "#FF1B0C";
            break;
        case 2:
            color = "#E8710B";
            break;
        case 3:
            color = "#FFC601";
            break;
        case 4:
            color = "#BFE80B";
            break;
        case 5:
            color = "#04FF22";
            break;
    }
    var current_attr = $("#mood").css('background-color');
    $("#mood").css('background-image', 'none');
    $("#mood").css('background-color', color);
    current_attr = $("#mood-img").attr("src");
    $("#mood-img").attr("src", img);
}