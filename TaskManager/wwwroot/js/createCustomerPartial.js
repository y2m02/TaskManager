function isCustomerValid() {
    return buildError("txtFullName", "lblFullNameError");
}

function createCustomer() {
    document.body.style.cursor = 'wait';
    if (!isCustomerValid()) {
        document.body.style.cursor = 'default';
        return false;
    }
    var customer = {
        "FullName": window.$("#txtFullName").val()
    }

    window.$.ajax({
        url: "/Customer/AjaxCreate",
        data: customer,
        type: "POST",
        content: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            document.body.style.cursor = 'default';
            GetDropDownListData("cbxCustomers", result.Response.CustomerId, "Customer");

            window.$(function () {
                window.$('#myModalCustomer').modal('toggle');
            });
        },
        error: function (errorMessage) {
            document.body.style.cursor = 'default';
            alert(errorMessage.responseText);
        }
    });

    return true;
}

$("#myModalCustomer").on("hidden.bs.modal",
    function () {
        window.$("#txtFullName").val("");

        clearErrorMessage([
            {
                'key': "txtFullName",
                'value': "lblFullNameError"
            },
        ]);
    });