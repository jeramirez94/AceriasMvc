$(function () {

    $('#tbObjetivos').bootstrapTable({
        onPostBody: function (cardView) {
            $(".caret").remove();
        }
    });
    
    $("#tbA1Diario").show();


    loadTableObjetives();
    //listeners();
    //createSelect();
})

function loadTableObjetives() {
    showLoading();
    var $table = $('#tbObjetivos');

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