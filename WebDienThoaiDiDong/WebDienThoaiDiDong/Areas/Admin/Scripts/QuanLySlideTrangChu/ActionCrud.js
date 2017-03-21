
function luu(page) {
    $.ajax({
        type: 'post',
        url:actionUrl.urlSave,
        data: $("#upload").serialize(),
        success: function (response) {
            if (response == 'True') {
                $.ajax({
                    type: 'post',
                    url: actionUrl.urlDanhsachslideranhsanpham,
                    data: {
                        page: 1
                    },
                    success: function (data) {
                        $("#table").html("");
                        $("#table").html(data);
                        dong();
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
    var r = confirm("Bạn có chắc chắn muốn xóa");
    if (r == true) {
        $.ajax({
            type: 'post',
            data: {
                id: id
            },
            url: actionUrl.urlDelete,
            success: function (response) {
                if (response == 'True') {
                    $.ajax({
                        url: actionUrl.urlDanhsachslideranhsanpham,
                        mtype: "POST",
                        datatype: "json",
                        data: {
                            page: page
                        },
                        success: function (data) {
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