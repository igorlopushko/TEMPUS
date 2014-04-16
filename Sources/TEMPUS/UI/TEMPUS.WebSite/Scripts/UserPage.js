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
        var data = [
        {
            value: 2,
            color: colors.red
        },
        {
            value: 1,
            color: colors.orange
        },
        {
            value: 4,
            color: colors.yellow
        },
        {
            value: 8,
            color: colors.chartreuse
        },
        {
            value: 3,
            color: colors.green
        }

        ]
        var myNewChart = new Chart(ctx).Doughnut(data);
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