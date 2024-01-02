$(function () {

    $('#tbA1Diario').bootstrapTable({
        onPostBody: function (cardView) {
            $(".caret").remove();
        }
    });
    $('#tableGroupByLoads').bootstrapTable({
        onPostBody: function (cardView) {
            $(".caret").remove();
        }
    });
    $('#tableGroupByWeeks').bootstrapTable({
        onPostBody: function (cardView) {
            $(".caret").remove();
        }
    });
    $("#tbA1Diario").show();

    createDatePicker();
    listeners();
    createSelect();

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

function listeners() {
    $("#btnSearchM").click(function () {
        showLoading();
        var fieldsArray = ["FechaInicial", "FechaFinal"];

        var selectArray = ["ClaHorno"];

        var save = validate_fields(fieldsArray, selectArray);
        if (save == false) {
            toast_closable_danger("Por favor complete todos los filtros");
            hideLoading();
            return;
        }


        searchA1();
        searchGroupByLoads();
        searchGroupByWeeks();
    })    
}

function searchA1() {
    showLoading();
    var $table = $('#tbA1Diario');

    var fechaIni = cleanDate($("#FechaInicial").val());
    var fechaFin = cleanDate($("#FechaFinal").val());


    $table.bootstrapTable('destroy')
    $table.hide();
    $.ajax({
        type: 'POST',
        url: prefix + "/DensidadVolumen/getA1Diario?FechaInicial=" + fechaIni + "&FechaFinal=" + fechaFin + "&ClaHorno=" + $("#ClaHorno").val(),
        dataType: 'html',
        data: {},
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        }
    }).done(function (data) {
        if (data.indexOf('2#emt5y') < 0) {
            data = $.parseJSON(data);
            $table.bootstrapTable({
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
            $table.show();
        } else {
            $table.bootstrapTable({
                onPostBody: function (cardView) {
                    $(".caret").remove();
                }
            });
            $table.show();
            toast_closable_danger(data.replace("2#emt5y", ""));
            hideLoading();
        }
    });
}

function searchGroupByLoads() {
    showLoading();
    var fechaIni = cleanDate($("#FechaInicial").val());
    var fechaFin = cleanDate($("#FechaFinal").val());

    var $table = $('#tableGroupByLoads');
    $table.bootstrapTable('destroy')
    $table.hide();

    var $columns = [
        {
            width: 1,
            field: 'ck',
            formatter: 'hideCkFormatter'
        },
        {
            field: 'Ncargas',
            title: 'No. Cargas',
            formatter: "NCargasFormatter"
        },
        {
            field: 'GrupoCH',
            title: 'Grupo CH',
            align: 'center',
            formatter: 'GrupoFormatter'
        },
        {
            field: '#Coladas',
            title: '#Coladas',
            align: 'center'
        },
        {
            field: 'Ton/hr',
            title: 'Ton/hr',
            align: 'center',
            formatter: "decimalFormatter"
        },
        {
            field: '%PlacaEstructura',
            title: '%Placa Estructura',
            align: 'center',
            formatter: "decimalFormatter"
        },
        {
            field: '%RetornoIndustrial',
            title: '%Retorno Industrial',
            align: 'center',
            formatter: "decimalFormatter"
        },
        {
            field: '%Pesado',
            title: '%Pesado',
            align: 'center',
            formatter: "decimalFormatter"
        },
        {
            field: '%PacaPrimera',
            title: '%Paca Primera',
            align: 'center',
            formatter: "decimalFormatter"
        },
        {
            field: '%PacaSegunda',
            title: '%Paca Segunda',
            align: 'center',
            formatter: "decimalFormatter"
        },
        {
            field: '%Bote',
            title: '%Bote',
            align: 'center',
            formatter: "decimalFormatter"
        },
        {
            field: '%Triturado',
            title: '%Triturado',
            align: 'center',
            formatter: "decimalFormatter"
        },
        {
            field: '%Chorreadura',
            title: '%Chorreadura',
            align: 'center',
            formatter: "decimalFormatter"
        },
        {
            field: '%Rebaba',
            title: '%Rebaba',
            align: 'center',
            formatter: "decimalFormatter"
        },
        {
            field: '%RetornoLaminacion',
            title: '%Retorno Laminacion',
            align: 'center',
            formatter: "decimalFormatter"
        },
        {
            field: '%RetornoAceria',
            title: '%Retorno Aceria',
            align: 'center',
            formatter: "decimalFormatter"
        },
        {
            field: 'Densidad',
            title: 'Densidad',
            align: 'center',
            formatter: "fourDecimalFormatter"
        },
        {
            field: 'Volumen',
            title: 'Volumen',
            align: 'center',
            formatter: "fourDecimalFormatter"
        }

    ];
    //$.ajax({
    //    type: 'POST',
    //    url: prefix + "/DensidadVolumen/getAgrupadoCargas?FechaInicial=" + fechaIni + "&FechaFinal=" + fechaFin + "&ClaHorno=" + $("#ClaHorno").val(),
    //    dataType: 'html',
    //    data: {},
    //    error: function (XMLHttpRequest, textStatus, errorThrown) {
    //        toast_closable_danger(errorThrown);
    //        hideLoading();
    //    }
    //}).done(function (data) {
    //    if (data.indexOf('2#emt5y') < 0) {
    //        console.log(data);
    //        debugger

    $table.bootstrapTable({
        //dataField: data,
        url: prefix + "/DensidadVolumen/getAgrupadoCargas?FechaInicial=" + fechaIni + "&FechaFinal=" + fechaFin + "&ClaHorno=" + $("#ClaHorno").val(),
        showColumns: true,
        treeShowField: 'Ncargas',
        idField: 'id',
        parentIdField: 'idPadre',
        columns: $columns,
        rowStyle: function rowStyle(row, index) {
            switch (row.Nivel) {
                case 7:
                    return { css: { background: '#fcfcfc' } }
                    break;
                case 6:
                    return { css: { background: '#ffffff' } }
                    break;
                case 5:
                    return { css: { background: '#fafafa' } }
                    break;
                case 4:
                    return { css: { background: '#f7f7f7f' } }
                    break;
                case 3:
                    return { css: { background: '#f5f5f5' } }
                    break;
                case 2:
                    return { css: { background: '#f2f2f2' } }
                    break;
                case 1:
                    return { css: { background: '#e0e0e0' } }
                    break;
                default: return { css: { color: 'black' } }
            }
        },
        onPostBody: function (cardView) {

            $(".caret").remove();
            hideLoading();
            var columns = $table.bootstrapTable('getOptions').columns

            if (columns && columns[0][1].visible) {
                $table.treegrid({
                    treeColumn: 1,
                    onChange: function () {
                        $table.bootstrapTable('resetView')
                    }
                })
            }
        }
    });
    $table.show();
    //    } else {
    //        $table.bootstrapTable({
    //            onPostBody: function (cardView) {
    //                $(".caret").remove();
    //            }
    //        });
    //        $table.show();
    //        toast_closable_danger(data.replace("2#emt5y", ""));
    //        hideLoading();
    //    }
    //});
}

function searchGroupByWeeks() {
    showLoading();
    var fechaIni = cleanDate($("#FechaInicial").val());
    var fechaFin = cleanDate($("#FechaFinal").val());

    var $table = $('#tableGroupByWeeks');
    $table.bootstrapTable('destroy')
    $table.hide();

    var $columns = [
        {
            width: 1,
            field: 'ck',
            formatter: 'hideCkFormatter'
        },
        {
            field: 'NoCargas',
            title: 'No. Cargas',
            formatter: "NCargasFormatter"
        },
        {
            field: 'Anio',
            title: 'Año',
            align: 'center',
            formatter: 'cleanFormatter'
        },
        {
            field: 'Mes',
            title: 'Mes',
            align: 'center',
            formatter: 'cleanFormatter'
        },
        {
            field: 'Semana',
            title: 'Semana',
            align: 'center',
            formatter: 'cleanFormatter'
        },
        {
            field: 'FechaAdministrativa',
            title: 'Fecha Adm',
            align: 'center',
            formatter: 'dateFormatter'
        },
        {
            field: 'NoColadas',
            title: '#Coladas',
            align: 'center'
        },
        {
            field: 'TonHr',
            title: 'Ton/hr',
            align: 'center',
            formatter: ""
        },
        {
            field: 'PorcPlacaEstr',
            title: '%Placa Estructura',
            align: 'center',
            formatter: ""
        },
        {
            field: 'PorcRetIndustrial',
            title: '%Retorno Industrial',
            align: 'center',
            formatter: ""
        },
        {
            field: 'PorcPesado',
            title: '%Pesado',
            align: 'center',
            formatter: ""
        },
        {
            field: 'PorcPacaPrimera',
            title: '%Paca Primera',
            align: 'center',
            formatter: ""
        },
        {
            field: 'PorcPacaSegunda',
            title: '%Paca Segunda',
            align: 'center',
            formatter: ""
        },
        {
            field: 'PorcBote',
            title: '%Bote',
            align: 'center',
            formatter: ""
        },
        {
            field: 'PorcTriturado',
            title: '%Triturado',
            align: 'center',
            formatter: ""
        },
        {
            field: 'PorcChorreadura',
            title: '%Chorreadura',
            align: 'center',
            formatter: ""
        },
        {
            field: 'PorcRebaba',
            title: '%Rebaba',
            align: 'center',
            formatter: ""
        },
        {
            field: 'PorcRetLaminacion',
            title: '%Retorno Laminacion',
            align: 'center',
            formatter: ""
        },
        {
            field: 'PorcRetAceria',
            title: '%Retorno Aceria',
            align: 'center',
            formatter: ""
        },
        {
            field: 'Densidad',
            title: 'Densidad',
            align: 'center',
            formatter: "sixDecimalFormatter"
        },
        {
            field: 'Volumen',
            title: 'Volumen',
            align: 'center',
            formatter: "sixDecimalFormatter"
        }

    ];
    //$.ajax({
    //    type: 'POST',
    //    url: prefix + "/DensidadVolumen/getAgrupadoSemanas?FechaInicial=" + fechaIni + "&FechaFinal=" + fechaFin + "&ClaHorno=" + $("#ClaHorno").val(),
    //    dataType: 'html',
    //    data: {},
    //    error: function (XMLHttpRequest, textStatus, errorThrown) {
    //        toast_closable_danger(errorThrown);
    //        hideLoading();
    //    }
    //}).done(function (data) {
    //    if (data.indexOf('2#emt5y') < 0) {
    //        console.log(data);
    //        // aquí la tabla
    //        debugger
    //    } else {
    //        $table.bootstrapTable({
    //            onPostBody: function (cardView) {
    //                $(".caret").remove();
    //            }
    //        });
    //        $table.show();
    //        toast_closable_danger(data.replace("2#emt5y", ""));
    //        hideLoading();
    //    }
    //});

    $table.bootstrapTable({
        //dataField: data,
        url: prefix + "/DensidadVolumen/getAgrupadoSemanas?FechaInicial=" + fechaIni + "&FechaFinal=" + fechaFin + "&ClaHorno=" + $("#ClaHorno").val(),
        //url: 'http://localhost:51417/Scripts/lib/bootstrap-table/json/treegrid.json',
        showColumns: true,
        treeShowField: 'NoCargas',
        idField: 'id',
        parentIdField: 'idPadre',
        columns: $columns,
        rowStyle: function rowStyle(row, index) {
            switch (row.Nivel) {
                case 7:
                    return { css: { background: '#fcfcfc' } }
                    break;
                case 6:
                    return { css: { background: '#ffffff' } }
                    break;
                case 5:
                    return { css: { background: '#fafafa' } }
                    break;
                case 4:
                    return { css: { background: '#f7f7f7f' } }
                    break;
                case 3:
                    return { css: { background: '#f5f5f5' } }
                    break;
                case 2:
                    return { css: { background: '#f2f2f2' } }
                    break;
                case 1:
                    return { css: { background: '#e0e0e0' } }
                    break;
                default: return {css: {color: 'black'}}
            }
        },
        onPostBody: function (cardView) {

            $(".caret").remove();
            hideLoading();
            var columns = $table.bootstrapTable('getOptions').columns

            if (columns && columns[0][1].visible) {
                $table.treegrid({
                    treeColumn: 1,
                    initialState: 'collapsed',
                    onChange: function () {
                        $table.bootstrapTable('resetView')
                    }
                })
            }
        }
    });
    $table.show();
    
}

function cleanDate(date) {
    var arrDate = date.split("/");
    return arrDate[2] + "" + arrDate[1] + "" + arrDate[0];
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
}

function decimalFormatter(value, row) {
    if (value == null || value == undefined)
        return "";
    else
        return value.toFixed(2);
}

function sixDecimalFormatter(value, row) {
    if (value == null || value == undefined)
        return "";
    else
        return value.toFixed(6);
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
        return value.split("T")[0];
}


function hideCkFormatter() {
    return "";
}

function NCargasFormatter(value, row, position, name) {
    if (name == "Ncargas") {
        if (value == null || row.GrupoCH != null)
            return "";
        else
            return value;
    }

    if (name == "NoCargas") {
        if (value == null)
            return "";
        else {
            if (value == 5)
                return "Total";
            else
                return value;
        }
    }

    return value;
}

function GrupoFormatter(value, row) {
    if (value == null)
        return "";

    return value;
}

function cleanFormatter(value, row) {
    if (value == null)
        return "";

    return value;
}
