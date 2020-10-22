$("#Schedules").delegate(".editButton",
    "click",
    function (e) {
        var grid = window.$("#Schedules").data("kendoGrid");
        var rowData = grid.dataItem(window.$(this).closest("tr"));

        fillFields(rowData);

        openModal(rowData.AssignmentId);
    });

function fillFields(rowData) {
    var scheduleId = rowData.ScheduleId;
    //if (scheduleId > 0) {
    //GetDropDownListData("cbxAssignments", rowData.AssignmentId, "Assignment");
    //}

    var assignmentId = checkIfValueExists("cbxAssignments", rowData.AssignmentId)
        ? rowData.AssignmentId
        : "";

    var statusId = checkIfValueExists("cbxStatuses", rowData.StatusId)
        ? rowData.StatusId
        : "";

    window.$("#txtScheduleId").val(scheduleId);
    //window.$("#cbxAssignments").val(assignmentId);
    window.$("#dtpDate").data("kendoDatePicker").value(rowData.Date);
    window.$("#cbxStatuses").val(statusId);
    window.$("#txtNote").val(rowData.Note);
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

$("#btnOpenModal").on("click",
    function () {
        openModal(0);
    });

function openModal(id) {
    GetDropDownListData("cbxAssignments", id, "Assignment");
    window.$("#myModalSchedule").modal();
}

function onDataBound(e) {
    var grid = this;

    var today = new Date();

    var day = today.getDate();
    var month = today.getMonth();
    var year = today.getFullYear();

    today = new Date(year, month, day);

    grid.tbody.find('>tr').each(function () {
        // get the row item
        var dataItem = grid.dataItem(this);

        var dueDate = dataItem.Date;

        day = dueDate.getDate();
        month = dueDate.getMonth();
        year = dueDate.getFullYear();

        dueDate = new Date(year, month, day);

        // check for the condition
        if (today > dueDate) {
            // add the formatting if condition is met
            $(this).addClass('outOfDate');
        } else {
            $(this).removeClass('outOfDate');
        }
    })
}

