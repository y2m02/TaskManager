function clearErrorMessage(parameters) {
    for (var i = 0; i < parameters.length; i++) {
        window.$("#" + parameters[i].key).css("borderColor", "");
        window.$("#" + parameters[i].value).html("");
    }
}

function removeErrorMessage(id, messageId) {
    window.$("#" + id).css("borderColor", "");
    window.$("#" + messageId).html("");
}

function buildError(field, label) {
    var fieldId = "#" + field;

    if (window.$(fieldId).val() === "") {
        window.$(fieldId).css("border-color", "Red");
        window.$("#" + label).html("Campo requerido");
        return false;
    }

    window.$(fieldId).css("border-color", "");

    return true;
}

function onClick(img) {
    var src = img.currentSrc;

    window.$("#imageId").attr("src", src);
    window.$("#myModalImage").modal();
}

$(function() {
    window.$("input").attr("autocomplete", "off");
});


function GetDropDownListData(elementId, id, controllerName) {
    window.$.ajax({
        url: "/" + controllerName + "/GetAllForDropDownList",
        data: { id: id },
        type: "GET",
        content: "application/json;charset=utf-8",
        dataType: "json",
        success: function(result) {
            fillDropDownList(elementId, result);
            
            if (id != null) {
                window.$("#" + elementId).val(id);
            }
        },
        error: function(errorMessage) {
            alert(errorMessage.responseText);
        }
    });
}

function fillDropDownList(elementId, result) {
    window.$("#" + elementId).children("option:not(:first)").remove();

    window.$.each(result,
        function(key, data) {
            var option = new Option();

            window.$(option).val(data.id);
            window.$(option).html(data.description);
            window.$("#" + elementId).append(option);
        });

    window.$("#" + elementId).val(result[result.length - 1].id);
}

function checkIfValueExists(elementId, id) {
    var options = window.$("#" + elementId + " option");

    for (var i = 0; i < options.length; i++) {
        if (options[i].value === id.toString()) {
            return true;
        }
    }
    return false;
}

function appendNewOption(elementId, id, description) {
    var option = new Option();

    window.$(option).val(id);
    window.$(option).html(description);
    window.$("#" + elementId).append(option);
}

function redirectToIndex(e, controller) {
    if ((e.type == "create" || e.type == "update" || e.type == "destroy") && !e.response.modelState) {
        //var data = e.response.Data[0];
        //window.location.href = "@(Url.Action("Index", "Size"))";
        window.location.href = "/" + controller + "/Index";
    }
}

$(function () {
    window.$('.numericField').bind('paste', function (e) {
        e.preventDefault(); 
    });

    //$('.numericField').bind('copy paste cut', function (e) {
    //    e.preventDefault(); 
    //});

    window.$("input[class*='numericField']").keydown(function (event) {

        if (event.shiftKey == true) {
            event.preventDefault();
        }

        if (!((event.keyCode >= 48 && event.keyCode <= 57) ||
            (event.keyCode >= 96 && event.keyCode <= 105) ||
            event.keyCode == 8 ||
            event.keyCode == 9 ||
            event.keyCode == 37 ||
            event.keyCode == 39 ||
            event.keyCode == 46 ||
            event.keyCode == 190)) {

            event.preventDefault();
        }

        if (window.$(this).val().indexOf(".") !== -1 && event.keyCode == 190)
            event.preventDefault();
        //if a decimal has been added, disable the "."-button
    });

    window.$(".numericField").focus(function () {
        var $this = window.$(this);
        $this.select();

        // Work around Chrome's little problem
        $this.mouseup(function () {
            // Prevent further mouseup intervention
            $this.unbind("mouseup");
            return false;
        });
    });
});

function convertToNumericFormat(quantity) {
    var fixedQuantity = quantity.toFixed(2);
    var length = fixedQuantity.length;
    var digits = 6;

    if (length > digits) {
        var position = length - digits;

        fixedQuantity = [fixedQuantity.slice(0, position), ",", fixedQuantity.slice(position)].join("");
    }

    return fixedQuantity;
}