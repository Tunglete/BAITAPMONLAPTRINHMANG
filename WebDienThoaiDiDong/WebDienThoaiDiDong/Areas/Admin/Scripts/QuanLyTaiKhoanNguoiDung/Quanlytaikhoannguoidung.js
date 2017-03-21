var checksearch = 0;
var codesearch = "";

$(document).ready(function () {
    $.ajax({
        url: formUrl.urlDanhsachtaikhoantable,
        mtype: "POST",
        datatype: "json",
        data: {
            page: 1
        },
        success: function (data) {
            $("#table").html(data);
        }
    });
});
function previous(page) {
    $.ajax({
        url: formUrl.urlDanhsachtaikhoantable,
        type: 'get',
        data: {
            page: page - 1
        },
        success: function (response) {
            $("#table").html(response)
        }

    });
}
function next(page) {
    $.ajax({
        url: formUrl.urlDanhsachtaikhoantable,
        type: 'get',
        data: {
            page: page + 1
        },
        success: function (response) {
            $("#table").html(response)
        }

    });
}
