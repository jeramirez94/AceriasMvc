$(function () {
    
    $('#tbColadaPorRango').bootstrapTable({
        onPostBody: function (cardView) {
            $(".caret").remove();
            hideLoading();
        }
    });
    $("#tbColadaPorRango").show();

    createDatePicker();
    listeners();
    createSelect();

    changeFilter($("input[name='optionsRadios']:checked").val());

    $("#ColadaInicial").ForceNumericOnly();
    $("#ColadaFinal").ForceNumericOnly();
})

function createDatePicker() {
    var formatDate = "DD/MM/YYYY"
    moment.locale("es-MX");
    $('.calendar').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        locale: {
            format: formatDate
        },
        timePicker: false,
        autoApply: true,
        applyClass: "btn-primary",
        "opens": "right",
        "drops": "down"
    },
    function (start, end, label) {
    });
}

function changeFilter($opc) {
    // 1. fechas 0.coladas    
    if ($opc == 1) {
        $(".divFechas").show();
        $(".divColadas").hide();
        
    }

    if ($opc == 0) {
        $(".divFechas").hide();
        $(".divColadas").show();
    }

}

function listeners() {
    changeFilter($("input[name='optionsRadios']:checked").val());

    $("input[name='optionsRadios']").change(function () {
        changeFilter($(this).val());
    });

    $("#btnSearchM").click(function () {
        showLoading();

        if ($("input[name='optionsRadios']:checked").val() == 1) {
            var fieldsArray = ["FechaInicial", "FechaFinal"];
            $("#ColadaInicial").val(0);
            $("#ColadaFinal").val(0);
        }
        else
            var fieldsArray = ["ColadaInicial", "ColadaFinal"];

        var selectArray = ["ClaHorno", "ClaTurno"];

        var save = validate_fields(fieldsArray, selectArray);
        if (save == false) {
            toast_closable_danger("Por favor complete todos los filtros");
            hideLoading();
            return;
        }

        serchColadasPorRango();
    })

    $("#btnSearchBitacora").click(function () {
        serchBitacora();
    })

    $("#btnLog").click(function () {
        $("#mdlBitacora").modal("show");
        serchBitacora();
    });

    $("#btnCatalogo").click(function () {
        $("#mdlCatalogo").modal("show");
        serchCatalogo();
    });

    $("#btnCaracterizar").click(function () {
        $("#lblColadas").text("");

        var coladas = $("#frmColadasPorRango").serialize();
        if (coladas == "" || coladas == null)
            return;

        coladas = coladas.replace(/\id=/g, '');
        coladas = coladas.replace(/\&/g, ',');
        $("#lblColadas").text("Coladas utilizadas: " + coladas);
        getCaracterizacion(coladas, 1);
    });

    $("#btnApprove").click(function () {
        var coladas = $("#frmColadasPorRango").serialize();
        if (coladas == "" || coladas == null)
            return;

        coladas = coladas.replace(/\id=/g, '');
        coladas = coladas.replace(/\&/g, ',');
        getCaracterizacion(coladas, 0);
    });
}

function getCaracterizacion($coladas, $soloConsultar) {
    showLoading();

    if ($soloConsultar == 1) {
        $('#tbCaracterizacion').bootstrapTable('destroy')
        $("#tbCaracterizacion").hide();
    }

    $.ajax({
        type: 'POST',
        url: prefix + "/CaracterizacionDeChatarra/getCaracterizacion?ClaHorno=" + $("#ClaHorno").val() + "&Coladas=" + $coladas + "&SoloConsultar=" + $soloConsultar,
        dataType: 'html',
        data: {},
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        }
    }).done(function (data) {
        if (data.indexOf('2#emt5y') < 0) {
            if ($soloConsultar == 1) {
                data = $.parseJSON(data);
                $('#tbCaracterizacion').bootstrapTable({
                    data: data,
                    onPostBody: function (cardView) {
                        $(".caret").remove();
                        hideLoading();
                    },
                    onLoad: function (params) {
                        hideLoading();
                    }
                });
                $("#mdlCaracterizacion").modal("show");
                $("#tbCaracterizacion").show();
            } else {
                hideLoading();
                toast_closable_success("Caracterización completa");
                $("#mdlCaracterizacion").modal("hide");
            }
        } else {
            if ($soloConsultar == 1) {
                toast_closable_danger(data.replace("2#emt5y",""));
                $('#tbCaracterizacion').bootstrapTable({
                    onPostBody: function (cardView) {
                        $(".caret").remove();
                    }
                });
                $("#mdlCaracterizacion").modal("show");
                $("#tbCaracterizacion").show();
                hideLoading();
            } else {
                hideLoading();
                toast_closable_danger(data.replace("2#emt5y", ""));
            }
        }
    });
}

function serchColadasPorRango() {
    showLoading();
    
    var arrDate = $("#FechaInicial").val().split("/");
    var fechaIni = arrDate[2]+""+arrDate[1]+""+arrDate[0];
    arrDate = $("#FechaFinal").val().split("/");
    var fechaFin = arrDate[2]+""+arrDate[1]+""+arrDate[0];


    $('#tbColadaPorRango').bootstrapTable('destroy')
    $("#tbColadaPorRango").hide();
    $.ajax({
        type: 'POST',
        url: prefix + "/CaracterizacionDeChatarra/getColadasPorRango?FechaInicial=" + fechaIni + "&FechaFinal=" + fechaFin + "&ClaHorno=" + $("#ClaHorno").val() + "&ClaTurno=" + $("#ClaTurno").val()
                    + "&TipoFiltro=" + $("input[name='optionsRadios']:checked").val() + "&ColadaInicial=" + $("#ColadaInicial").val() + "&ColadaFinal=" + $("#ColadaFinal").val(),
        dataType: 'html',
        data: {},
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        }
    }).done(function (data) {
        if (data.indexOf('2#emt5y') < 0) {
            data = $.parseJSON(data);
            $('#tbColadaPorRango').bootstrapTable({
                data: data,
                onClickRow: function (row, $element, field) {
                    
                },
                onDblClickCell: function (field, value, row, $element) {
                },
                onClickCell: function (field, value, tableRow, $element) {
                },
                onPostBody: function (cardView) {
                    $(".caret").remove();
                    hideLoading();
                },
                onRefresh: function (params) {
                    hideLoading();
                },
                onLoad: function (params) {
                    hideLoading();
                }
            });
            $("#tbColadaPorRango").show();
        } else {
            $('#tbColadaPorRango').bootstrapTable({
                onPostBody: function (cardView) {
                    $(".caret").remove();
                }
            });
            $("#tbColadaPorRango").show();
            hideLoading();
        }
    });
}

function serchBitacora() {
    showLoading();
    $('#tbBitacora').bootstrapTable('destroy');
    
    $.ajax({
        type: 'POST',
        url: prefix + "/LogEventos/getLogEventos?IdModulo=1&ClaveInt=null&ClaveString=" + $("#fChatarra").val(),
        dataType: 'html',
        data: {},
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        }
    }).done(function (data) {
        debugger
        if (data.indexOf('2#emt5y') < 0) {
            data = $.parseJSON(data);
            $('#tbBitacora').bootstrapTable({
                data: data,
                onClickRow: function (row, $element, field) {

                },
                onDblClickCell: function (field, value, row, $element) {
                },
                onClickCell: function (field, value, tableRow, $element) {
                },
                onPostBody: function (cardView) {
                    $(".caret").remove();
                    hideLoading();
                },
                onRefresh: function (params) {
                    hideLoading();
                },
                onLoad: function (params) {
                    hideLoading();
                }
            });
            $("#tbBitacora").show();
            hideLoading();
        } else {
            $('#tbBitacora').bootstrapTable({
                onPostBody: function (cardView) {
                    $(".caret").remove();
                }
            });
            $("#tbBitacora").show();
            hideLoading();
        }
    });
}

function serchCatalogo() {
    showLoading();
    $('#tbCatalogo').bootstrapTable('destroy');

    $.ajax({
        type: 'POST',
        url: prefix + "/CaracterizacionDeChatarra/getCatalogoChatarra",
        dataType: 'html',
        data: {},
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        }
    }).done(function (data) {
        debugger
        if (data.indexOf('2#emt5y') < 0) {
            data = $.parseJSON(data);
            $('#tbCatalogo').bootstrapTable({
                data: data,
                onClickRow: function (row, $element, field) {

                },
                onDblClickCell: function (field, value, row, $element) {
                },
                onClickCell: function (field, value, tableRow, $element) {
                },
                onPostBody: function (cardView) {
                    $(".caret").remove();
                    hideLoading();
                },
                onRefresh: function (params) {
                    hideLoading();
                },
                onLoad: function (params) {
                    hideLoading();
                }
            });
            $("#tbCatalogo").show();
            hideLoading();
        } else {
            $('#tbCatalogo').bootstrapTable({
                onPostBody: function (cardView) {
                    $(".caret").remove();
                }
            });
            $("#tbCatalogo").show();
            hideLoading();
        }
    });
}

function createSelect() {

    $.fn.select2.defaults.set('language', "es-MX");
    $('#ClaHorno').select2({
        placeholder: "-Seleccionar-",
        allowClear: true,
        minimumResultsForSearch: Infinity,
        //maximumSelectionLength: 5,
        tags: [],
        ajax: {
            url: "/Horno/getHornoFusion",
            dataType: 'json',
            type: "POST",
            quietMillis: 50,
            data: function (term) {
                return {
                    Valor: term, 
                    Tipo: 1,
                    IncluirTodosSN: 1
                }
            },
            processResults: function (data) {
                if (data != "2#emt5y") {
                    dataCmb = [];
                    for (var i = 0; i < data.length; i++) {
                        dataCmb.push({ id: data[i].ClaHornoFusion, text: data[i].NomHornoFusion });
                    }
                    return {
                        results: dataCmb,
                    };
                } else {
                    return "";
                }
            }
        }
    })


    $.fn.select2.defaults.set('language', "es-MX");
    $('#ClaTurno').select2({
        placeholder: "-Seleccionar-",
        allowClear: true,
        minimumResultsForSearch: Infinity,
        //maximumSelectionLength: 5,
        tags: [],
        ajax: {
            url: "/Turno/getTurno",
            dataType: 'json',
            type: "POST",
            quietMillis: 50,
            data: function (term) {
                return {
                    wtk0: -1,
                    IncluirTodosSN: 1
                }
            },
            processResults: function (data) {
                if (data != "2#emt5y") {
                    dataCmb = [];
                    for (var i = 0; i < data.length; i++) {
                        dataCmb.push({ id: data[i].ClaTurno, text: data[i].NomTurno });
                    }
                    return {
                        results: dataCmb,
                    };
                } else {
                    return "";
                }
            }
        }
    })
}

function decimalFormatter(value, row) {
    if (value == null || value == undefined)
        return "";
    else
        return value.toFixed(2);
}

function fourDecimalFormatter(value, row) {
    if (value == null || value == undefined)
        return "";
    else
        return value.toFixed(4);
}

function dateFormatter(value, row) {
    if (value == null || value == undefined)
        return "";
    else
        return value.replace("T"," ");
}

function activeFormatter(value, row) {
    if (value == null || value == undefined)
        return "";
    else
        return value == 1 ? "Sí" : "No";

}