﻿$("#Schedules").delegate(".editButton",
    "click",
    function(e) {
        var grid = window.$("#Schedules").data("kendoGrid");
        var rowData = grid.dataItem(window.$(this).closest("tr"));

        fillFields(rowData);

        window.$("#myModalSchedule").modal();
    });

function fillFields(rowData) {
    var assignmentId = checkIfValueExists("cbxAssignments", rowData.AssignmentId)
        ? rowData.AssignmentId
        : "";

    var statusId = checkIfValueExists("cbxStatuses", rowData.StatusId)
        ? rowData.StatusId
        : "";

    window.$("#cbxAssignments").val(assignmentId);
    window.$("#dtpDate").data("kendoDatePicker").value(rowData.Date);
    window.$("#cbxStatuses").val(statusId);
    window.$("#txtNote").val(rowData.Note);
}

$("#myModalSchedule").on("hidden.bs.modal",
    function() {
        window.$("#cbxAssignments").val("");
        window.$("#dtpDate").data("kendoDatePicker").value("");
        window.$("#cbxStatuses").val("");
        window.$("#txtNote").val("");

        clearErrorMessage([
            {
                'key': "cbxAssignments",
                'value': "lblAssingmentsError"
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

function isValid() {
    document.body.style.cursor = "wait";

    var assignmentIdIsValid = buildError("cbxAssignments", "lblAssignmentsError");
    var dateIsValid = buildError("dtpDate", "lblDateError");
    var statusIdIsValid = buildError("cbxStatuses", "lblStatusesError");

    document.body.style.cursor = "default";

    return assignmentIdIsValid &&
        dateIsValid &&
        statusIdIsValid;
}

function createSchedule() {
    document.body.style.cursor = 'wait';

    if (!isValid()) {
        document.body.style.cursor = 'default';
        return false;
    }

    var scheduleId = window.$("#txtScheduleId").val();
    var assignmentId = parseInt(window.$("#cbxAssignments").val());
    var date = window.$("#dtpDate").data("kendoDatePicker").value().toDateString();
    var statusId = parseInt(window.$("#cbxStatuses").val());
    var note = window.$("#txtNote").val();

    var schedule = {
        "ScheduleId": scheduleId == '' ? 0 : parseInt(scheduleId),
        "AssignmentId": assignmentId,
        "Date": date,
        "StatusId": statusId,
        "Note": note
    }

    window.$.ajax({
        url: "Schedule/Upsert",
        data: { request: schedule },
        type: "POST",
        content: "application/json;",
        dataType: "json",
        success: function (result) {
            $('#myModalSchedule').modal('toggle');
            RefreshGrid('Schedules');
            document.body.style.cursor = 'default';
        },
        error: function (errorMessage) {
            document.body.style.cursor = 'default';
            alert(errorMessage.responseText);
        }
    });

    return true;
}