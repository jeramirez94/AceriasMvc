$(function () {

    $('#tbTiempoDisponible').bootstrapTable({
        onPostBody: function (cardView) {
            $(".caret").remove();
        }
    });
    $("#tbTiempoDisponible").show();

    listeners();
    loadTiempoDisponible();
})

function listeners() {

}

function loadTiempoDisponible() {
    showLoading();
    var $table = $('#tbTiempoDisponible');

    $table.bootstrapTable('destroy')
    $table.hide();
    $.ajax({
        type: 'POST',
        url: prefix + "/TiempoDisponibleUCI/getTiempoDisponible",
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
                    $("table thead tr th").css("background", tbbgcolor); //attr("bgcolor", tbbgcolor);
                    $("table thead tr th .th-inner").css("color", tbfont);
                    hideLoading();
                },
                onRefresh: function (params) {
                    hideLoading();
                },
                onLoad: function (params) {
                    hideLoading();
                },
                onEditableSave: function (field, row, oldValue, $el) {
                    alert("abc" + $el);
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

function decimalFormatter(value, row) {
    if (value == null || value == undefined)
        return "";
    else
        return value.toFixed(2);
}