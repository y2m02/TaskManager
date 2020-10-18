$("#Products").delegate(".editButton",
    "click",
    function(e) {
        var grid = window.$("#Products").data("kendoGrid");
        var rowData = grid.dataItem(window.$(this).closest("tr"));

        //GetDropDownListData("cbxCategories", rowData.CategoryId, "Category");
        fillFields(rowData);

        window.$("#myModalProduct").modal();
    });

function fillFields(rowData) {
    var image = rowData.Image;
    if (image != null) {
        var fileName = image.split("W&Y")[1];
        window.$(".custom-file-input").siblings(".custom-file-label").addClass("selected").html(fileName);
    }

    var baleId = checkIfValueExists("cbxBales", rowData.BaleId)
        ? rowData.BaleId
        : "";

    var categoryId = checkIfValueExists("cbxCategories", rowData.CategoryId)
        ? rowData.CategoryId
        : "";

    var sizeId = checkIfValueExists("cbxSizes", rowData.SizeId)
        ? rowData.SizeId
        : "";

    window.$("#cbxBales").val(baleId);
    window.$("#cbxCategories").val(categoryId);
    window.$("#txtProductCode").val(rowData.ProductCode);
    window.$("#txtDescription").val(rowData.Description);
    window.$("#txtPrice").val(rowData.Price);
    window.$("#cbxSizes").val(sizeId);
    //window.$("#txtComments").val(rowData.Comments);
}

$("#myModalProduct").on("hidden.bs.modal",
    function() {
        window.$(".custom-file-input").siblings(".custom-file-label").addClass("selected").html("Elija una imagen");
        window.$("#cbxBales").val("");
        window.$("#cbxCategories").val("");
        window.$("#txtProductCode").val("");
        window.$("#txtDescription").val("");
        window.$("#txtPrice").val("");
        window.$("#cbxSizes").val("");
        //window.$("#txtComments").val("");

        clearErrorMessage([
            {
                'key': "txtDescription",
                'value': "lblDescriptionError"
            },
            {
                'key': "txtPrice",
                'value': "lblPriceError"
            },
            {
                'key': "cbxCategories",
                'value': "lblCategoriesError"
            },
            {
                'key': "cbxBales",
                'value': "lblBalesError"
            },
        ]);
    });

$(".custom-file-input").on("change",
    function() {
        var fileName = window.$(this).val().split("\\").pop();
        window.$(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    });

function validate() {
    document.body.style.cursor = 'wait';

    var baleIdIsValid = buildError("cbxBales", "lblBalesError");
    var categoryIdIsValid = buildError("cbxCategories", "lblCategoriesError");
    var descriptionIsValid = buildError("txtDescription", "lblDescriptionError");
    var priceIsValid = buildError("txtPrice", "lblPriceError");

    document.body.style.cursor = 'default';

    return baleIdIsValid
        && categoryIdIsValid
        && descriptionIsValid
        && priceIsValid;
}