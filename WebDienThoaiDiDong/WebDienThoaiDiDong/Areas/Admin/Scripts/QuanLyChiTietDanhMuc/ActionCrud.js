﻿
function Edit(id, page) {
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
    if ($("#form_edit").valid()) {
    $.ajax({
        type: 'post',
        url: actionUrl.urlSave,
        data: $("#form_edit").serialize(),
        success: function (response) {
            if (response == 'True') {
                $.ajax({
                    type: 'post',
                    url: actionUrl.urlDanhsachchitietdanhmuctable,
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
}
function dong() {
    $('#div_edit').html("");
}
function Delete(id, page) {
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
                        url: actionUrl.urlDanhsachchitietdanhmuctable,
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