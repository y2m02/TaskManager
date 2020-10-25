$("#Schedules").delegate(".editButton",
    "click",
    function (e) {
        var grid = window.$("#Schedules").data("kendoGrid");
        var rowData = grid.dataItem(window.$(this).closest("tr"));

        fillFields(rowData);

        openModal(rowData.AssignmentId);
    });

function fillFields(rowData) {
    window.$("#txtScheduleId").val(rowData.ScheduleId);
    window.$("#dtpDate").data("kendoDatePicker").value(rowData.Date);
    window.$("#cbxStatuses").val(rowData.StatusId);
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
        openModal(0);
    });

function openModal(id) {
    GetDropDownListData("cbxAssignments", id, "Assignment");

    window.$("#myModalSchedule").modal();
}

function onDataBound(e) {
    var grid = this;

    var today = getOnlyDate(new Date());

    grid.tbody.find(">tr").each(function () {
        var dataItem = grid.dataItem(this);

        var dueDate = getOnlyDate(dataItem.Date);

        var status = dataItem.StatusDescription;

        if (today > dueDate && status != "Finalizada") {
            window.$(this).addClass("outOfDate");
        } else {
            window.$(this).removeClass("outOfDate");
        }
    });
}

window.$("#cbxAssignments").on("change",
    function () {
        removeErrorMessage('cbxAssignments', 'lblAssignmentsError');
    });

window.$("#dtpDate").on("change",
    function () {
        removeErrorMessage("dtpDate", "lblDateError");
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