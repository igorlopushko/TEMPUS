var colors = {
    'red': '#FF1B0C',
    'orange': '#E8710B',
    'yellow': '#FFC601',
    'chartreuse': '#BFE80B',
    'green': '#04FF22'
};

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
        default:
            return;
    }
    $("#avatar").css('background-color', color);
    $("#mood-img").attr("src", img);
    $(".mood-related").removeClass("mood-related");
}
var setted = false;

function setMoodHandler(ajaxUrl, id) {
    $(".mood-set").removeClass("mood-set");
    $(".ul-chooser img").click(function (event) {
        if (!setted) {
            setted = true;
            $(this).css("opacity", "1.0");
            $(".ul-chooser img").click(function () { });
            var rate = $(this).data("mood");
            $.post(ajaxUrl, { UserId: id, Rate: rate }).done(function (data) {
                $("div.panel-heading").text("Thanks!");
            });
            setMood(rate);
        }
    });
}