$("#Assignments").delegate(".editButton",
    "click",
    function(e) {
        var grid = window.$("#Assignments").data("kendoGrid");
        var rowData = grid.dataItem(window.$(this).closest("tr"));

        //GetDropDownListData("cbxCategories", rowData.CategoryId, "Category");
        fillFields(rowData);

        window.$("#myModalProduct").modal();
    });

function fillFields(rowData) {
    var storeId = checkIfValueExists("cbxStores", rowData.StoreId)
        ? rowData.StoreId
        : "";

    window.$("#cbxStores").val(storeId);
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

function validate() {
    document.body.style.cursor = "wait";

    var storeIdIsValid = buildError("cbxStores", "lblStoresError");
    var descriptionIsValid = buildError("txtDescription", "lblDescriptionError");

    document.body.style.cursor = "default";

    return storeIdIsValid && descriptionIsValid;
}