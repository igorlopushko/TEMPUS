/**
 * Created by zWeRz on 06.05.14.
 */

function c3DrawMoodChart(url) {
    d3.json(url, function (error, json) {
        var input = {
            data: {
                xs: {},
                columns: [],
                type: 'spline'
            },
            axis: {
                x: {
                    type: 'timeseries',
                    tick: {
                        format: '%d-%b'
                    }
                }
            }
        };
        json.forEach(function (obj) {
            input.data.xs[obj.name] = "x" + obj.name;
            var dates = [("x" + obj.name)];
            obj.data.forEach(function (el) {
                dates.push(el.date);
            });
            input.data.columns.push(dates);
        });
        json.forEach(function (obj) {
            var datas = [obj.name];
            obj.data.forEach(function (el) {
                datas.push(+el.mood);
            });
            input.data.columns.push(datas);
        });
        var chart = c3.generate(input);
        json.forEach(function (obj, index) {
            if (index === 0) return;
            chart.toggle(obj.name);
        });
    });
}

function drawMoodChart(elementId, url) {
    $("#" + elementId).empty();
    $("#" + elementId).append("<div class='row'></div>");
    d3.json(url, function (error, data) {
        data.forEach(function (obj) {

            $("#" + elementId + ">.row")
                .append("<div class='col-sm-6 col-md-6 mood-box'>" +
                    "<div class='thumbnail'>" +
                    "<div class='row'>" +
                    "<div class='col-xs-2 col-sm-3 col-md-3 img-place text-center'></div>" +
                    "<div class='col-xs-10 col-sm-9 col-md-9 chart-place'></div>" +
                    "</div>" +
                    "</div>" +
                    "</div>");
            var element = $("#" + elementId + ">.row>div").last();
            var imgPlace = element.find('.img-place');
            imgPlace.append("<p>" + obj.name + "</p>");
            imgPlace.append("<img src='user.png' class='img-responsive'>");
            var container = element.find('.chart-place');
            container.css("height", element.height());
            var margin = {top: 30, right: 30, bottom: 45, left: 20};
            var width = container.width() - margin.left - margin.right;
            var height = container.height() - margin.top - margin.bottom;

            var imgWidth = 100;

            var parseDate = d3.time.format("%Y-%m-%d").parse;

            var x = d3.time.scale()
                .range([0, width]);

            var y = d3.scale.linear()
                .range([height, 0]);

            var xAxis = d3.svg.axis()
                .scale(x)
                .orient("bottom")
                .ticks(d3.time.days, 1)
                .tickFormat(d3.time.format('%d %b'));

            var yAxis = d3.svg.axis()
                .scale(y)
                .orient("left")
                .ticks(4)
                .tickFormat(d3.format("d"));

            var line = d3.svg.line()
                .interpolate("cardinal")
                .x(function (d) {
                    return x(d.date);
                })
                .y(function (d) {
                    return y(d.mood);
                });
            var svg = d3.selectAll(container.toArray()).append("svg")
                .attr("width", width + margin.left + margin.right)
                .attr("height", height + margin.top + margin.bottom);
            var inner = svg
                .append("g")
                .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

            obj.data.forEach(function (d) {
                d.date = parseDate(d.date);
                d.mood = +d.mood;
            });

            x.domain(d3.extent(obj.data, function (d) {
                return d.date;
            }));
            y.domain([1, 4]);

            inner.append("g")
                .attr("class", "x axis")
                .attr("transform", "translate(0," + height + ")")
                .call(xAxis)
                .selectAll("text")
                .style("text-anchor", "end")
                .attr("dx", "-.8em")
                .attr("dy", ".15em")
                .attr("transform", function (d) {
                    return "rotate(-45) translate(15,15)"
                });
            ;

            inner.append("g")
                .attr("class", "y axis")
                .call(yAxis)
                .append("text")
                .attr("y", -5)
                .text("Mood");

            inner.append("path")
                .datum(obj.data)
                .attr("class", "line")
                .attr("d", line);

            inner.append("g")
                .selectAll('.data-point')
                .data(obj.data)
                .enter()
                .append('circle')
                .attr('class', 'data-point')
                .style('opacity', 1)
                .attr('cx', function (d) {
                    return x(d.date)
                })
                .attr('cy', function (d) {
                    return y(d.mood)
                })
                .attr('r', 4);

            $('svg circle').tipsy({
                gravity: 'w',
                html: true,
                title: function () {
                    var d = this.__data__;
                    var format = d3.time.format("%d %b");
                    return '<h4>Date: ' + format(d.date) + '<br>Mood: ' + d.mood + "</h4>";
                }
            });
        });
    });

}
