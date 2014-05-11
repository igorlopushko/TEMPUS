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
                        format: '%d-%b',
                    }
                },
                y: {
                    tick: {
                        format: formatY
                    }
                }
            },
            padding: {
                left: 55, right: 15
            },
            //hack for mobile devices
            legend: {
                item: {
                    onmouseover: function (id) {
                        chart.revert();
                    },
                    onclick: function (id) {
                        chart.toggle(id);
                        chart.revert(id);
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

function formatY(data) {
    switch (data) {
        case 1:
            data = "very sad"
            break
        case 2:
            data = "not ok"
            break
        case 3:
            data = "good"
            break
        case 4:
            data = "awesome"
            break
        default:
            data = ""
    }
    return data;
}