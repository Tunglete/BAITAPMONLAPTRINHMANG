var checksearch = 0;

$(document).ready(function () {
    $.ajax({
        url: actionUrl.urlChitietgiaodichtable,
        mtype: "POST",
        datatype: "json",
        data: {
            page: 1
        },
        success: function (data) {
            $("#ctgd-table").html(data);
        }
    });
});
function previous(page) {
    if (checksearch == 0) {
        $.ajax({
            url: actionUrl.urlChitietgiaodichtable,
            type: 'get',
        data: {
            page: page - 1
        },
        success: function (response) {
            $("#ctgd-table").html(response)
        }

    });
    } else {
        $.ajax({
            url: actionUrl.urlSearch,
        type: 'get',
        data: {
        page: page - 1
        },
        success: function (response) {
        $("#ctgd-table").html(response)
        }

        })
    }


}
function next(page) {
    if (checksearch == 0) {
        $.ajax({
            url: actionUrl.urlChitietgiaodichtable,
            type: 'get',
        data: {
            page: page + 1
        },
        success: function (response) {
            $("#ctgd-table").html(response)
        }

    });
    }
    else {
        $.ajax({
         url: actionUrl.urlSearch,
        type: 'get',
        data: {
            page: page + 1
        },
        success: function (response) {
            $("#ctgd-table").html(response)
        }

        });
    }


}

    
// function tìm kiếm
function SearchName() {
    var codesearch = $("input#search").val().trim();
    checksearch = 1;

    $.ajax({
        url: actionUrl.urlSearch,
        mtype: "POST",
        datatype: "json",
        data: {
            codesearch: codesearch,
            page: 1
        },
        success: function (data) {
            $("#ctgd-table").html("");
            $("#ctgd-table").html(data);
        }
    });
}