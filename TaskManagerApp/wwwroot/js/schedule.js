$("#Schedules").delegate(".editButton",
    "click",
    function (e) {
        document.body.style.cursor = 'wait';
        e.preventDefault();

        var grid = window.$("#Schedules").data("kendoGrid");
        var rowData = grid.dataItem(window.$(this).closest("tr"));

        fillFields(rowData);

        document.body.style.cursor = 'default';
    });

function fillFields(rowData) {
    window.$("#txtScheduleId").val(rowData.ScheduleId);
    window.$("#dtpDate").data("kendoDatePicker").value(rowData.Date);
    window.$("#cbxStatuses").val(rowData.StatusId);
    window.$("#txtNote").val(rowData.Note);

    GetAssignmentDropDownListData(rowData.AssignmentId);
}

$("#myModalSchedule").on("hidden.bs.modal",
    function () {
        window.$("#txtScheduleId").val("");
        window.$("#cbxAssignments").val("");
        window.$("#dtpDate").data("kendoDatePicker").value("");
        window.$("#cbxStatuses").val("");
        window.$("#txtNote").val("");

        clearErrorMessage([
            {
                'key': "cbxAssignments",
                'value': "lblAssignmentsError"
            },
            {
                'key': "dtpDate",
                'value': "lblDateError"
            },
            {
                'key': "cbxStatuses",
                'value': "lblStatusesError"
            },
        ]);
    });

function validateDate() {
    var date = window.$("#dtpDate").val();

    if (date === "") {
        window.$(".k-picker-wrap").css("border-color", "red", "important");
        window.$("#lblDateError").html("Campo requerido");
        return false;
    }

    var formatIsValid = moment(date, 'DD/MM/YYYY', true).isValid();

    if (!formatIsValid) {
        window.$(".k-picker-wrap").css("border-color", "red", "important");
        window.$("#lblDateError").html("Formato inválido. Debe ser 'DD/MM/YYYY'");
        return false;
    }

    return true;
}

function isValid() {
    document.body.style.cursor = "wait";

    var assignmentIdIsValid = buildError("cbxAssignments", "lblAssignmentsError");
    var dateIsValid = validateDate();
    var statusIdIsValid = buildError("cbxStatuses", "lblStatusesError");

    document.body.style.cursor = "default";

    return assignmentIdIsValid &&
        dateIsValid &&
        statusIdIsValid;
}

function createSchedule() {
    document.body.style.cursor = "wait";

    if (!isValid()) {
        document.body.style.cursor = "default";
        return false;
    }

    var scheduleId = window.$("#txtScheduleId").val();
    var assignmentId = parseInt(window.$("#cbxAssignments").val());
    var date = window.$("#dtpDate").data("kendoDatePicker").value().toDateString();
    var statusId = parseInt(window.$("#cbxStatuses").val());
    var note = window.$("#txtNote").val();

    var schedule = {
        "ScheduleId": scheduleId == "" ? 0 : parseInt(scheduleId),
        "AssignmentId": assignmentId,
        "Date": date,
        "StatusId": statusId,
        "Note": note
    };

    window.$.ajax({
        url: "Schedule/Upsert",
        data: { request: schedule },
        type: "POST",
        content: "application/json;",
        dataType: "json",
        success: function (result) {
            window.$("#myModalSchedule").modal("toggle");
            RefreshGrid("Schedules");
            document.body.style.cursor = "default";
        },
        error: function (errorMessage) {
            document.body.style.cursor = "default";
            alert(errorMessage.responseText);
        }
    });

    return true;
}

$("#btnOpenModal").on("click",
    function () {
        document.body.style.cursor = 'wait';

        GetAssignmentDropDownListData(0);

        document.body.style.cursor = 'default';
    });

function openModal() {

    var cbxStatus = window.$("#cbxStatuses");
    var date = window.$("#dtpDate").val();

    if (date == "") {
        cbxStatus.attr("disabled", false);
    } else {
        var today = getOnlyDate(new Date());
        var splitDate = date.split("/");

        var scheduleDate = new Date(splitDate[2], splitDate[1] - 1, splitDate[0]);

        if (scheduleDate > today) {
            cbxStatus.attr("disabled", true);
        } else {
            cbxStatus.attr("disabled", false);
        }
    }

    window.$("#myModalSchedule").modal();
}

function onDataBound(e) {
    var grid = this;

    var today = getOnlyDate(new Date());

    grid.tbody.find(">tr").each(function () {
        var dataItem = grid.dataItem(this);

        var dueDate = getOnlyDate(dataItem.Date);

        var status = dataItem.StatusDescription;

        if (today > dueDate && status != "Finalizada" && status != "Detenida") {
            window.$(this).removeClass("k-alt");
            window.$(this).addClass("outOfDate");
        } else {
            window.$(this).removeClass("outOfDate");
        }
    });

    var groups = grid.dataSource.group();

    if (groups.length) {
        grid.tbody.children(".k-grouping-row").each(function () {
            var row = $(this),
                groupKey = rowGroupKey(row, grid);
            if (collapsed[groupKey]) {
                grid.collapseRow(row);
            }
        });
    }
}

window.$("#cbxAssignments").on("change",
    function () {
        removeErrorMessage('cbxAssignments', 'lblAssignmentsError');
    });

window.$("#dtpDate").on("change",
    function () {
        var cbxStatus = window.$("#cbxStatuses");

        if (validateDate()) {
            removeErrorMessage("dtpDate", "lblDateError");

            var today = getOnlyDate(new Date());

            var date = window.$("#dtpDate").val();
            var splitDate = date.split("/");

            var scheduleDate = new Date(splitDate[2], splitDate[1] - 1, splitDate[0]);

            if (scheduleDate > today) {
                cbxStatus.val("1");
                cbxStatus.attr("disabled", true);
            } else {
                cbxStatus.attr("disabled", false);
            }
        } else {
            cbxStatus.attr("disabled", false);
        }
    });

window.$("#cbxStatuses").on("change",
    function () {
        removeErrorMessage('cbxStatuses', 'lblStatusesError');
    });

function getOnlyDate(date) {
    var day = date.getDate();
    var month = date.getMonth();
    var year = date.getFullYear();

    return new Date(year, month, day);
}

var collapsed = {};

$(function () {
    var grid = $("#Schedules").data("kendoGrid");
    grid.table.on("click", ".k-grouping-row .k-i-collapse, .k-grouping-row .k-i-expand", function (e) {
        var row = $(this).closest("tr"),
            groupKey = rowGroupKey(row, grid);

        if ($(this).hasClass("k-i-collapse")) {
            collapsed[groupKey] = false;
        }
        else {
            collapsed[groupKey] = true;
        }
    });
});

function rowGroupKey(row, grid) {
    var next = row.nextUntil("[data-uid]").next(),
        item = grid.dataItem(next.length ? next : row.next()),
        groupIdx = row.children(".k-group-cell").length,
        groups = grid.dataSource.group(),
        field = grid.dataSource.group()[groupIdx].field,
        groupValue = item[field];
    return "" + groupIdx + groupValue;
}

function GetAssignmentDropDownListData(id) {
    window.$.ajax({
        url: "/Assignment/GetAllForDropDownList",
        data: { id: id },
        type: "GET",
        content: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            fillDropDownList("cbxAssignments", result);

            if (id > 0) {
                window.$("#cbxAssignments").val(id);
            }

            openModal();
        },
        error: function (errorMessage) {
            alert(errorMessage.responseText);
        }
    });
}