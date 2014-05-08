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
                }
            },
            padding: {right:15}
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