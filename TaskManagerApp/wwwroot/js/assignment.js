$("#Assignments").delegate(".editButton",
    "click",
    function(e) {
        e.preventDefault();

        var grid = window.$("#Assignments").data("kendoGrid");
        var rowData = grid.dataItem(window.$(this).closest("tr"));

        fillFields(rowData);

        window.$("#myModalAssignment").modal();
    });

function fillFields(rowData) {
    window.$("#cbxStores").val(rowData.StoreId);
    window.$("#txtAssignmentId").val(rowData.AssignmentId);
    window.$("#txtDescription").val(rowData.Description);
}

$("#myModalAssignment").on("hidden.bs.modal",
    function() {
        window.$("#cbxStores").val("");
        window.$("#txtAssignmentId").val("");
        window.$("#txtDescription").val("");

        clearErrorMessage([
            {
                'key': "txtDescription",
                'value': "lblDescriptionError"
            },
            {
                'key': "cbxStores",
                'value': "lblStoresError"
            },
        ]);
    });

function isValid() {
    document.body.style.cursor = "wait";

    var storeIdIsValid = buildError("cbxStores", "lblStoresError");
    var descriptionIsValid = buildError("txtDescription", "lblDescriptionError");

    document.body.style.cursor = "default";

    return storeIdIsValid && descriptionIsValid;
}

function createAssignment() {
    document.body.style.cursor = 'wait';

    if (!isValid()) {
        document.body.style.cursor = 'default';
        return false;
    }

    var assignmentId = window.$("#txtAssignmentId").val();
    var storeId = parseInt(window.$("#cbxStores").val());
    var description = window.$("#txtDescription").val();

    var assignment = {
        "AssignmentId": assignmentId == '' ? 0 : parseInt(assignmentId),
        "StoreId": storeId,
        "Description": description
    }

    window.$.ajax({
        url: "Assignment/Upsert",
        data: { request: assignment },
        type: "POST",
        content: "application/json;",
        dataType: "json",
        success: function (result) {
            window.$('#myModalAssignment').modal('toggle');
            RefreshGrid('Assignments');
            document.body.style.cursor = 'default';
        },
        error: function (errorMessage) {
            document.body.style.cursor = 'default';
            alert(errorMessage.responseText);
        }
    });

    return true;
}

window.$("#cbxStores").on("change",
    function() {
        removeErrorMessage('cbxStores','lblStoresError');
    });

window.$("#txtDescription").on("input",
    function() {
        removeErrorMessage('txtDescription','lblDescriptionError');
    });
