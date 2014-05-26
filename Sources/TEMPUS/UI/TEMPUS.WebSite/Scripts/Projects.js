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

var index = 0;

function addUser(button) {
    var add = true;
    var userID = $(button).parent().find("[name='person.UserId']").val();
    $('#resultTable tr td').children('input').each(function () {
        if ($(this).val() == userID) {
            add = false;
            return false;
        }
    });
    if (add) {
        var name = $(button).parent().parent().find("[data-name='person.Name']").html();
        var nameCellTemplate = "<td><input type='hidden' value='" + userID + "' name='CreateProjectTeamViewModel.ProjectTeamMemberViewModel[" + index + "].UserId' />" + name + "</td>";
        var fteCellTemplate = "<td><div class='col-md-3 col-sm-3'><input name='CreateProjectTeamViewModel.ProjectTeamMemberViewModel[" + index + "].FTE' type='text' class='form-control'></div></td>";
        var deleteTemplate = "<td><button type='button' onclick='deleteUser(\"" + userID + "\")' class='btn btn-danger btn-sm glyphicon glyphicon-remove' /></td>";
        $('#resultTable tr:last').after('<tr>' + nameCellTemplate + fteCellTemplate + deleteTemplate + '</tr>');
        index++;
    }
    if (index > 0) {
        $('#emptyMessage').hide();
        $('#resultTable').show();
    }
}

function deleteUser(userId) {
    $('#resultTable tr td').children('input').each(function () {
        if ($(this).val() == userId) {
            $(this).parent().parent().remove();
            index--;
            return false;
        }
    });

    var trId = -1;
    $('#resultTable tr').each(function () {
        $(this).find('td input').each(function () {
            var str = $(this).attr('name');
            var res = str.replace(/\[\d\]/i, "[" + trId + "]");
            $(this).attr('name', res);
        });
        trId++;
    });

    if (index <= 0) {
        $('#emptyMessage').show();
        $('#resultTable').hide();
    }
}

function validateDigit(input) {
    text = $(input).val();
    return (text.match(/^\d[\.,]{1}\d+/) != null);
}

function validateStep1() {
    projectName = $("#projectName");
    projectManager = $("#projectManager");
    projectOwner = $("#projectOwner");
    department = $("#department");
    time = $("#time");
    cost = $("#cost");
    quality = $("#quality");
    orderer = $("#orderer");
    organisation = $("#organisation");
    start = $("#start");
    due = $("#due");
    description = $("#description");

    var hasErrors = false;

    if (isEmpty(projectName.val())) {
        projectName.parent().parent().addClass("has-error");
        $("#projectNameError").text("Project Name is required");
        hasErrors = true;
    } else {
        projectName.parent().parent().removeClass("has-error");
        $("#projectNameError").text("");
    }

    if (isEmpty(projectManager.val())) {
        projectManager.parent().parent().addClass("has-error");
        $("#projectManagerError").text("Project Manager is required");
        hasErrors = true;
    } else {
        projectManager.parent().parent().removeClass("has-error");
        $("#projectManagerError").text("");
    }

    if (isEmpty(projectOwner.val())) {
        projectOwner.parent().parent().addClass("has-error");
        $("#projectOwnerError").text("Project Owner is required");
        hasErrors = true;
    } else {
        projectOwner.parent().parent().removeClass("has-error");
        $("#projectOwnerError").text("");
    }

    if (isEmpty(department.val())) {
        department.parent().parent().addClass("has-error");
        $("#departmentError").text("Department is required");
        hasErrors = true;
    } else {
        department.parent().parent().removeClass("has-error");
        $("#departmentError").text("");
    }

    if (isEmpty(time.val())) {
        time.parent().parent().addClass("has-error");
        $("#timeError").text("Time is required");
        hasErrors = true;
    } if(!validateDigit(time)) {
        time.parent().parent().addClass("has-error");
        $("#timeError").text("Time must be between 0.1 - 0.9");
        hasErrors = true;
    } else {
        time.parent().parent().removeClass("has-error");
        $("#timeError").text("");
    }

    if (isEmpty(cost.val())) {
        cost.parent().parent().addClass("has-error");
        $("#costError").text("Cost is required");
        hasErrors = true;
    } if(!validateDigit(cost)) {
        cost.parent().parent().addClass("has-error");
        $("#costError").text("Cost must be between 0.1 - 0.9");
        hasErrors = true;
    } else {
        cost.parent().parent().removeClass("has-error");
        $("#costError").text("");
    }

    if (isEmpty(quality.val())) {
        quality.parent().parent().addClass("has-error");
        $("#qualityError").text("Quality is required");
        hasErrors = true;
    } if(!validateDigit(quality)) {
        quality.parent().parent().addClass("has-error");
        $("#qualityError").text("Quality must be between 0.1 - 0.9");
        hasErrors = true;
    } else {
        quality.parent().parent().removeClass("has-error");
        $("#qualityError").text("");
    }

    if (isEmpty(orderer.val())) {
        orderer.parent().parent().addClass("has-error");
        $("#ordererError").text("Orderer is required");
        hasErrors = true;
    } else {
        orderer.parent().parent().removeClass("has-error");
        $("#ordererError").text("");
    }

    if (isEmpty(organisation.val())) {
        organisation.parent().parent().addClass("has-error");
        $("#organisationError").text("Organisation is required");
        hasErrors = true;
    } else {
        organisation.parent().parent().removeClass("has-error");
        $("#organisationError").text("");
    }

    if (isEmpty(start.val())) {
        start.parent().parent().parent().addClass("has-error");
        $("#startError").text("Start Date is required");
        hasErrors = true;
    } else if ($(start).val().match(/\d{2}\/\d{2}\/\d{4}/) == null) {
        start.parent().parent().parent().addClass("has-error");
        $("#startError").text("Start Date must be in dd/MM/yyyy format");
        hasErrors = true;
    } else {
        start.parent().parent().parent().removeClass("has-error");
        $("#startError").text("");
    }

    if (isEmpty(due.val())) {
        due.parent().parent().parent().addClass("has-error");
        $("#dueError").text("Due Date is required");
        hasErrors = true;
    } else if ($(due).val().match(/\d{2}\/\d{2}\/\d{4}/) == null) {
        due.parent().parent().parent().addClass("has-error");
        $("#dueError").text("Due Date must be in dd/MM/yyyy format");
        hasErrors = true;
    } else {
        due.parent().parent().parent().removeClass("has-error");
        $("#dueError").text("");
    }

    if (isEmpty(description.val())) {
        description.parent().parent().addClass("has-error");
        $("#descriptionError").text("Description is required");
        hasErrors = true;
    } else {
        description.parent().parent().removeClass("has-error");
        $("#descriptionError").text("");
    }

    if (hasErrors == false) {
        timeValue = $(time).val();
        costValue = $(cost).val();
        qualityValue = $(quality).val();
        if (timeValue + costValue + qualityValue != 1) {
            time.parent().parent().parent().addClass("has-error");
            cost.parent().parent().parent().addClass("has-error");
            quality.parent().parent().parent().addClass("has-error");
            $("#timeError").text("The sum of Time, Cost and Quality must be 1.");
            hasErrors = true;
        } else {
            time.parent().parent().parent().removeClass("has-error");
            cost.parent().parent().parent().removeClass("has-error");
            quality.parent().parent().parent().removeClass("has-error");
            $("#timeError").text("");
            hasErrors = false;
        }
    }

    if (hasErrors == false) {
        $('#create-carusel').carousel('next');
    }
}

function validateStep2() {
    hasErrors == false;
    if ($('#resultTable tr').length == 1) {
        hasErrors == true;
        $("#errorMessage").text("Add at least one user to team.");
    }



    if (hasErrors == false) {
        $('#create-carusel').carousel('next');
    }
}

function isEmpty(str) {
    return (!str || !str.trim() || 0 === str.length);
}