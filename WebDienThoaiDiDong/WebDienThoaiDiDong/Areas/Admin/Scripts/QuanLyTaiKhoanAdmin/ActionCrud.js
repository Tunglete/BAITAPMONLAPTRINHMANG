﻿function Edit(id, page) {
    $.ajax({
        type: 'get',
        data: {
            id: id,
            page: page
        },
        url: actionUrl.urlEdit,
        success: function (data) {
            $("#div_edit").html(data);
        }
    });
}
function luu(page) {
    $.ajax({
        type: 'post',
        url: urlSave,
        data: $("#form_edit").serialize(),
        success: function (response) {
            if (response == 'True') {
                $.ajax({
                    type: 'post',
                    url: urladmintable,
                    data: {
                        page: page
                    },
                    success: function (data) {
                        $("#table").html("");
                        $("#table").html(data);
                    }
                });
            }

        }
    });
}
function dong() {
    $('#div_edit').html("");
}
function Delete(id, page) {
    debugger;
    var r = confirm("Bạn có chắc chắn muốn xóa");
    if (r == true) {
        $.ajax({
            type: 'get',
            data: {
                id: id
            },
            url: actionUrl.urlDelete,
            success: function (response) {
                if (response == 'True') {
                    $.ajax({
                        type: 'post',
                        url: actionUrl.urladmintable,
                        data: {
                            page: page
                        },
                        success: function (data) {
                            $("#table").html("");
                            $("#table").html(data);
                        }
                    });
                }
            }
        });
    } else {
        return false;
    }

}