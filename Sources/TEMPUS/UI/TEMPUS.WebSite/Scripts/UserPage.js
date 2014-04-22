var colors = {
    'red': '#FF1B0C',
    'orange': '#E8710B',
    'yellow': '#FFC601',
    'chartreuse': '#BFE80B',
    'green': '#04FF22'
};

//rubber canvas
$(document).ready(function () {
    var c = $('#respondCanvas');
    var ct = c.get(0).getContext('2d');
    var container = $(c).parent();
    $(window).resize(respondCanvas);
    function respondCanvas() {
        c.attr('width', $(container).width());
    }
    respondCanvas();
});

//draw chart
$(document).ready(function () {
    $(window).resize(redraw);
    function redraw() {
        var ctx = $("#respondCanvas").get(0).getContext("2d");
        var data = {
            labels: ["07.04.14", "08.04.14", "09.04.14", "10.04.14", "11.04.14", "14.04.14", "15.04.14"],
            datasets: [
                {
                    fillColor: "rgba(4,200,34, 0.4)",
                    strokeColor: "rgba(4,200,34, 1)",
                    pointColor: "rgba(4,200,34, 1)",
                    pointStrokeColor: "rgba(0,0,0, 0.4)",
                    data: [3, 2, 1, 4, 5, 4, 4]
                }
            ]
        }
        var myNewChart = new Chart(ctx).Line(data, { scaleOverride: true, scaleStepWidth: 1, scaleSteps: 5 });
    }
    redraw();
});

//color and image setting for mood
function setMood(mood) {
    var img = "/Content/Images/" + mood + "_mood.png";
    var color;
    switch (mood) {
        case 1:
            color = colors.red;
            break;
        case 2:
            color = colors.orange;
            break;
        case 3:
            color = colors.yellow;
            break;
        case 4:
            color = colors.chartreuse;
            break;
        case 5:
            color = colors.green;
            break;
    }
    var current_attr = $("#mood").css('background-color');
    $("#mood").css('background-image', 'none');
    $("#mood").css('background-color', color);
    current_attr = $("#mood-img").attr("src");
    $("#mood-img").attr("src", img);
}