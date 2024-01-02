var prefix = "",
    tbbgcolor = "#2b2b2b",
    tbfont = "#FFFFFF";

if (window.location.href.indexOf("localhost") >= 0)
    prefix = "";
else
    prefix = "";

function getSelect2Html(idCombo, claEntity, claDependency1, claDependency2, claDependency3, claDependency4) {
    claDependency1 = (claDependency1 == null) ? 0 : claDependency1;
    claDependency2 = (claDependency2 == null) ? 0 : claDependency2;
    claDependency3 = (claDependency3 == null) ? 0 : claDependency3;
    claDependency4 = (claDependency4 == null) ? 0 : claDependency4;
    isTokenAlive();
    $.ajax({

        type: 'POST',
        url: "/General/getCombo?entity=" + claEntity + "&dependency1=" + claDependency1
                                                     + "&dependency2=" + claDependency2
                                                     + "&dependency3=" + claDependency3
                                                     + "&dependency4=" + claDependency4,
        dataType: 'html',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //toast_closable_danger(errorThrown);
        }
    }).done(function (response) {
        $('#' + idCombo)
                        .find('option')
                        .remove()
                        .end();
        var html = "<option value='-1' selected='selected'>  - Selecciona - </option>";
        if (response.indexOf('vacío') < 0) {
            var jsResponse = $.parseJSON(response);
            for (var i = 0; i < jsResponse.length; i++) {
                html += "<option value='" + jsResponse[i].Value + "'>" + jsResponse[i].Text + "</option>";
            }

        }
        $("#" + idCombo).append(html);
        //ConvertToSelect2(idCombo, html);
    });
}

function headerStyle(column) {
    return {
        css: {
            background: '#2b2b2b',
            color: "#FFFFFF"
        }
    }
}

function getSelectToHtml(idCombo, claEntity, claDependency1, claDependency2, claDependency3, claDependency4, defaultValue) {
    claDependency1 = (claDependency1 == null) ? 0 : claDependency1;
    claDependency2 = (claDependency2 == null) ? 0 : claDependency2;
    claDependency3 = (claDependency3 == null) ? 0 : claDependency3;
    claDependency4 = (claDependency4 == null) ? 0 : claDependency4;
    var $isDefault = "";
    isTokenAlive();
    $.ajax({
        type: 'POST',
        url: "/General/getCombo?entity=" + claEntity + "&dependency1=" + claDependency1
                                                     + "&dependency2=" + claDependency2
                                                     + "&dependency3=" + claDependency3
                                                     + "&dependency4=" + claDependency4,
        dataType: 'html',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //toast_closable_danger(errorThrown);
        }
    }).done(function (response) {
        $('#' + idCombo)
                        .find('option')
                        .remove()
                        .end();
        var html = "";
        if (response.indexOf('vacío') < 0) {
            var jsResponse = $.parseJSON(response);
            for (var i = 0; i < jsResponse.length; i++) {
                debugger
                if (jsResponse[i].Value == defaultValue)
                    $isDefault = "selected = 'selected'";
                html += "<option value='" + jsResponse[i].Value + "' " + $isDefault + " >" + jsResponse[i].Text + "</option>";
                $isDefault = "";
            }

        }
        $("#" + idCombo).append(html);
        //ConvertToSelect2(idCombo, html);
    });
}

function cleanCombo(idCombo, change) {
    $('#' + idCombo)
                        .find('option')
                        .remove()
                        .end();
    var html = "<option value='-1'>  - Select - </option>";
    $("#" + idCombo).append(html);
    if (change == 1)
        $("#" + idCombo).val(-1).change();
}

function validate_fields(fieldsArray, selectArray) {
    $(".required_value").css("border-color", "gray");
    $(".required_value").css("border-width", "1");

    var complete = true;
    if (fieldsArray != null)
        for (var i = 0; i < fieldsArray.length; i++) {

            if ($("#" + fieldsArray[i] + "").val() == "") {
                $("#" + fieldsArray[i] + "").css("border-color", "red");
                $("#" + fieldsArray[i] + "").css("border-width", "2");
                complete = false;
            }
        }
    if (selectArray != null)
        for (var i = 0; i < selectArray.length; i++) {
            if ($("#" + selectArray[i] + "").val() == "-2" || $("#" + selectArray[i] + "").val() == null) {
                $("#" + selectArray[i] + "").css("border-color", "red");
                $("#" + selectArray[i] + "").css("border-width", "2");
                complete = false;
            }
        }

    return complete;
}

function showLoading() {
    //$("body").append("<div class='loadingModal'></div>");
    $("body").append('<div class="loaderContainer"><span class="loadingDeacero" style="color:white"></span></div>');
    $(".loaderContainer").show();
}

function hideLoading() {
    //$(".loadingModal").hide();
    //$(".loadingModal").remove();
    $(".loaderContainer").hide();
    $(".loaderContainer").remove();
}

function toast_closable_danger($message) {
    new Toast({
        message: $message,
        type: 'danger'
    });
    window.setTimeout(function () {
        $("#btnCloseToast").click();
    }, 3000);
}

function toast_not_closable_danger($message) {
    new Toast({
        message: $message,
        type: 'danger'
    });
}

function toast_not_closable_success($message) {
    new Toast({
        message: $message,
        type: 'success'
    });
}

function toast_closable_success($message) {
    new Toast({
        message: $message,
        type: 'success'
    });
    window.setTimeout(function () {
        $("#btnCloseToast").click();
    }, 3000);
}

function sweet_alert_success($title, $text) {
    Swal.fire({
        icon: 'success',
        title: $title,
        text: $text
    })
}

function sweet_alert_success($title, $text, $footer) {
    Swal.fire({
        icon: 'success',
        title: $title,
        text: $text,
        footer: $footer
    })
}

function sweet_alert_error($title, $text) {
    Swal.fire({
        icon: 'error',
        title: $title,
        text: $text
    })
}

function sweet_alert_error($title, $text, $footer) {
    Swal.fire({
        icon: 'error',
        title: $title,
        text: $text,
        footer: $footer
    })
}

function selectedRow(TableRow) {
    $('.c_row').css("background-color", "white");
    TableRow.style.backgroundColor = "#e5edf9";
}

jQuery.fn.ForceNumericOnly =
	function () {
	    return this.each(function () {
	        $(this).keydown(function (e) {
	            var key = e.charCode || e.keyCode || 0;
	            // allow backspace, tab, delete, enter, arrows, numbers and keypad numbers ONLY
	            // home, end, period, and numpad decimal
	            return (
					key == 8 ||
					key == 9 ||
					key == 13 ||
					key == 46 ||
					key == 110 ||
					key == 190 ||
					(key >= 35 && key <= 40) ||
					(key >= 48 && key <= 57) ||
					(key >= 96 && key <= 105));
	        });
	    });
	};

function dateFormatter(value, row) {
    if (value == null) {
        return "";
    }
    return value = value.split("T")[0];
}