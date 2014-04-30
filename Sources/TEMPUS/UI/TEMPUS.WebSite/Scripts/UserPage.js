var colors = {
    'red': '#FF1B0C',
    'orange': '#E8710B',
    'yellow': '#FFC601',
    'chartreuse': '#BFE80B',
    'green': '#04FF22'
};

function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

//draw chart
$(document).ready(function () {
    //wait for 500ms before trigging the redrawing of the chart
    $(window).resize(function () {
        if (this.resizeTO) clearTimeout(this.resizeTO);
        this.resizeTO = setTimeout(function () {
            $(this).trigger('resizeEnd');
        }, 500);

    });
    var width = $(window).width();
    //redraw graph when window resize is completed  
    $(window).on('resizeEnd', function () {
        if ($(window).width() == width) return;
        width = $(window).width();
        respondCanvas();
        redraw();
    });
    function respondCanvas() {
        $('#respondCanvas').attr('width', $('#respondCanvas').parent().width());
    }
    function redraw() {
        var ctx = $("#respondCanvas").get(0).getContext("2d");
        var data = {
            labels: ["07.04.14", "08.04.14", "09.04.14", "10.04.14", "11.04.14", "14.04.14", "15.04.14"],
            datasets: []
        }
        for (i = 0; i < 1; i++) {
            var pushData = {
                fillColor: "rgba(4," + i * 30 + ",34, 0.4)",
                strokeColor: "rgba(4," + i * 30 + ",34, 0.4)",
                pointColor: "rgba(4," + i * 30 + ",34, 0.4)",
                pointStrokeColor: "rgba(0,0,0, 0.4)",
                data: []
            };
            for (j = 0; j < 7; j++) {
                pushData.data.push(getRandomInt(1,4));
            }
            data.datasets.push(pushData);
        }
        var myNewChart = new Chart(ctx).Line(data, { scaleOverride: true, scaleStepWidth: 1, scaleSteps: 4, datasetFill: false });
    }
    respondCanvas();
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
            color = colors.chartreuse;
            break;
        case 4:
            color = colors.green;
            break;
    }
    $("#avatar").css('background-color', color);
    $("#mood-img").attr("src", img);
}