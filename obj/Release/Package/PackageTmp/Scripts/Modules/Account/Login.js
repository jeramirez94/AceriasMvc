$(function () {
    $("#frmLogin").submit(function () {
        var form = $("#frmLogin");
        $("#auxValidate").remove();
        $("#message").hide();
        if (form[0].checkValidity() === false) {
            hideLoading();
            event.preventDefault();
            event.stopPropagation();
            form.addClass('was-validated');
        }
        else
        {
            showLoading();
            form.addClass('was-validated');
            $("#divMessage").hide();
            $.ajax({
                type: 'POST',
                url: "/Account/getLogin",
                dataType: 'json',
                data: '{"user":"' + $("#user").val() + '", "password":"' + $("#password").val() + '"}',
                contentType: 'application/json; charset=utf-8',
                error: function (XMLHttpRequest, textStatus, errorThown) { }
            }).done(function (data) {
                hideLoading();
                if (data == 0) {
                    $("#message").html("Usuario y/o contraseña incorrectos");
                    //sweet_alert_error("No puede continuar", "Para esta funcionalidad debe seleccionar una máquina en el panel izquierdo", "La consulta no puede ser a nivel área");
                    $("#message").show();
                    event.preventDefault();
                    event.stopPropagation();
                    return false;
                }
                else if(data == -1)
                {
                    $("#message").html("No tienes permisos para esta ubicación");
                    //sweet_alert_error("No puede continuar", "Para esta funcionalidad debe seleccionar una máquina en el panel izquierdo", "La consulta no puede ser a nivel área");
                    $("#message").show();
                    event.preventDefault();
                    event.stopPropagation();
                    return false;
                }
                else {
                    location.href = "/Home/Index";
                }
            })

            event.preventDefault();
            event.stopPropagation();
            return false;
        }
    });
})