function redrawCharts() {
    redrawStatusChart();
    redrawBudgetChart();
}

function redrawDetailsCharts() {
    redrawRiskChart();
    redrawTimeChart();
}

function redrawTimeChart() {
    var data = google.visualization.arrayToDataTable([
          ['Person', 'Planned Time', 'Used Time', 'Trend'],
          ['Person1', 16, 20, 18],
          ['Person2', 13, 11, 12],
          ['Person3', 15, 14, 14.5],
          ['Person4', 14, 18, 16],
          ['Person5', 11, 16, 13]
    ]);

    var options = {
        vAxis: { title: "Hours" },
        seriesType: "bars",
        series: {
            0: { color: '#E0E0E0' },
            1: { color: '#C0C0C0' },
            2: { type: "line", color: '#3399FF' }
        }
    };

    var chart = new google.visualization.ComboChart(document.getElementById('timeChart'));
    chart.draw(data, options);
}

function redrawBudgetChart() {
    var data = google.visualization.arrayToDataTable([
	    ['Project', 'Budget', { role: 'style' }],
        ['Project1', 1800, '#3399FF'],          
	    ['Project2', 2100, '#3399FF'],       
	    ['Project3', 1500, '#3399FF']
    ]);

    var options = {
    };

    var chart = new google.visualization.ColumnChart(document.getElementById('budgetChart'));
    chart.draw(data, options);
}

function redrawStatusChart() {
    var data = google.visualization.arrayToDataTable([
      ['Status', 'Count'],
      ['Red', 11],
      ['Yellow', 5],
      ['Green', 7]
    ]);

    var options = {
        pieSliceTextStyle: { color: 'black' },
        slices: {
            0: { color: '#FF0033' },
            1: { color: '#FFFF66' },
            2: { color: '#00FF99' }
        }
    };

    var chart = new google.visualization.PieChart(document.getElementById('statusChart'));
    chart.draw(data, options);
}

function redrawRiskChart() {
    var data = google.visualization.arrayToDataTable([
      ['Status', 'Count'],
      ['Trouble tasks', 3],
      ['Task at risk', 5],
      ['Health tasks', 7]
    ]);

    var options = {
        pieSliceTextStyle: { color: 'black' },
        slices: {
            0: { color: '#FF0033' },
            1: { color: '#FFFF66' },
            2: { color: '#00FF99' }
        }
    };

    var chart = new google.visualization.PieChart(document.getElementById('riskChart'));
    chart.draw(data, options);
}