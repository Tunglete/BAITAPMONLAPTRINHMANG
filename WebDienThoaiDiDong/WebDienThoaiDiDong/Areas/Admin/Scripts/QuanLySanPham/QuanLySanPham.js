var checksearch = 0;
var codesearch = "";
$(document).ready(function () {

    $.ajax({
        url: formUrl.urlDanhsachsanphamtable,
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
    if (checksearch == 0) {
        $.ajax({
            url: formUrl.urlDanhsachsanphamtable,
            type: 'get',
            data: {
                page: page - 1
            },
            success: function (response) {
                $("#table").html(response)
            }

        });
    } else {
        $.ajax({
            url: formUrl.urlSearch,
            type: 'get',
            data: {
                codesearch: codesearch,
                page: page - 1
            },
            success: function (response) {
                $("#table").html(response)
            }

        })
    }


}
function next(page) {
    if (checksearch == 0) {
        $.ajax({
            url: formUrl.urlDanhsachsanphamtable,
            type: 'get',
            data: {
                page: page + 1
            },
            success: function (response) {
                $("#table").html(response)
            }

        });
    }
    else {
        $.ajax({
            url: formUrl.urlSearch,
            type: 'get',
            data: {
                codesearch: codesearch,
                page: page + 1
            },
            success: function (response) {
                $("#table").html(response)
            }

        });
    }
}
function Chitiet(id) {
    $.ajax({
        url: actionUrl.urlDetail,
        type: 'post',
        data: {
            id: id
        },
        success: function (data) {
            $("#modalcontent").html(data)
        }
    });
}


