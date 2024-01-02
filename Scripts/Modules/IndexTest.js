

$(function () {
    loadHeader();
    loadDemorasAcumuladas();
    loadDemorasTiempo();
    loadDemorasFrecuencia();
    loadDemorasAuxiliares();
})


function loadHeader() {
    $('#tbHeader').bootstrapTable('destroy')
    $("#tbHeader").hide();
    
    $.ajax({
        type: 'POST',
        url: "/Home/getTableHeader",
        async: false,
        data: {},
        dataType: 'html',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            toast_closable_danger("Error al consultar datos");
            hideLoading();
        }
    }).done(function (data) {
        if (data.indexOf('2#emt5y') < 0) {
            data = $.parseJSON(data);
            $('#tbHeader').bootstrapTable({
                data: data,
                onClickRow: function (row, $element, field) {
                },
                onDblClickCell: function (field, value, row, $element) {
                },
                onClickCell: function (field, value, tableRow, $element) {
                },
                onPostBody: function (cardView) {
                    $(".caret").remove();
                    //$(".fixed-table-loading").remove();
                    //$(".page-link").attr("color", tbbgcolor);
                    //$("#tbHeader thead tr").attr("bgcolor", tbbgcolor);
                    //$("table thead tr th .th-inner").css("color", tbfont);
                    hideLoading();
                },
                onRefresh: function (params) {
                    hideLoading();
                }
                ,
                onLoad: function (params) {
                    hideLoading();
                }
            });
            $("#tbHeader").show();
        } else {
            $('#tbHeader').bootstrapTable({
                onPostBody: function (cardView) {
                    $(".fixed-table-loading").remove();
                    $("table thead tr").attr("bgcolor", tbbgcolor);
                    $("table thead tr th .th-inner").css("color", tbfont);
                }
            });
            $("#tbHeader").show();
            hideLoading();
        }
    })
}

function loadDemorasAcumuladas() {
    $('#tbDemorasAcumMes').bootstrapTable('destroy')
    $("#tbDemorasAcumMes").hide();

    $.ajax({
        type: 'POST',
        url: "/Home/getDemorasAcumuladas",
        async: false,
        data: {},
        dataType: 'html',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            toast_closable_danger("Error al consultar datos");
            hideLoading();
        }
    }).done(function (data) {
        if (data.indexOf('2#emt5y') < 0) {
            data = $.parseJSON(data);
            $('#tbDemorasAcumMes').bootstrapTable({
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
                }
                ,
                onLoad: function (params) {
                    hideLoading();
                }
            });
            $("#tbDemorasAcumMes").show();
        } else {
            $('#tbDemorasAcumMes').bootstrapTable({
                onPostBody: function (cardView) {
                    $(".fixed-table-loading").remove();
                    $("table thead tr").attr("bgcolor", tbbgcolor);
                    $("table thead tr th .th-inner").css("color", tbfont);
                }
            });
            $("#tbDemorasAcumMes").show();
            hideLoading();
        }
    })
}

function loadDemorasTiempo() {
    $('#tbDemorasTiempo').bootstrapTable('destroy')
    $("#tbDemorasTiempo").hide();

    $.ajax({
        type: 'POST',
        url: "/Home/getDemorasTiempo",
        async: false,
        data: {},
        dataType: 'html',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            toast_closable_danger("Error al consultar datos");
            hideLoading();
        }
    }).done(function (data) {
        if (data.indexOf('2#emt5y') < 0) {
            data = $.parseJSON(data);
            $('#tbDemorasTiempo').bootstrapTable({
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
                }
                ,
                onLoad: function (params) {
                    hideLoading();
                }
            });
            $("#tbDemorasTiempo").show();
        } else {
            $('#tbDemorasTiempo').bootstrapTable({
                onPostBody: function (cardView) {
                    $(".fixed-table-loading").remove();
                    $("table thead tr").attr("bgcolor", tbbgcolor);
                    $("table thead tr th .th-inner").css("color", tbfont);
                }
            });
            $("#tbDemorasTiempo").show();
            hideLoading();
        }
    })
}

function loadDemorasFrecuencia() {
    $('#tbDemorasFrecuencia').bootstrapTable('destroy')
    $("#tbDemorasFrecuencia").hide();

    $.ajax({
        type: 'POST',
        url: "/Home/getDemorasFrecuencia",
        async: false,
        data: {},
        dataType: 'html',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            toast_closable_danger("Error al consultar datos");
            hideLoading();
        }
    }).done(function (data) {
        if (data.indexOf('2#emt5y') < 0) {
            data = $.parseJSON(data);
            $('#tbDemorasFrecuencia').bootstrapTable({
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
                }
                ,
                onLoad: function (params) {
                    hideLoading();
                }
            });
            $("#tbDemorasFrecuencia").show();
        } else {
            $('#tbDemorasFrecuencia').bootstrapTable({
                onPostBody: function (cardView) {
                    $(".fixed-table-loading").remove();
                    $("table thead tr").attr("bgcolor", tbbgcolor);
                    $("table thead tr th .th-inner").css("color", tbfont);
                }
            });
            $("#tbDemorasFrecuencia").show();
            hideLoading();
        }
    })
}

function loadDemorasAuxiliares() {
    $('#tbDemorasAuxiliares').bootstrapTable('destroy')
    $("#tbDemorasAuxiliares").hide();

    $.ajax({
        type: 'POST',
        url: "/Home/getDemorasAuxiliares",
        async: false,
        data: {},
        dataType: 'html',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            toast_closable_danger("Error al consultar datos");
            hideLoading();
        }
    }).done(function (data) {
        if (data.indexOf('2#emt5y') < 0) {
            data = $.parseJSON(data);
            $('#tbDemorasAuxiliares').bootstrapTable({
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
                }
                ,
                onLoad: function (params) {
                    hideLoading();
                }
            });
            $("#tbDemorasAuxiliares").show();
        } else {
            $('#tbDemorasAuxiliares').bootstrapTable({
                onPostBody: function (cardView) {
                    $(".fixed-table-loading").remove();
                    $("table thead tr").attr("bgcolor", tbbgcolor);
                    $("table thead tr th .th-inner").css("color", tbfont);
                }
            });
            $("#tbDemorasAuxiliares").show();
            hideLoading();
        }
    })
}