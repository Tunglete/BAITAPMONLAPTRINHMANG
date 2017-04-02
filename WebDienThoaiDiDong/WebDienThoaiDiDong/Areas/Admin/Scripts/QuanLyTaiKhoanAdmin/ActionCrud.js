function validate(){
    var TenQuanTri = document.getElementById("TenQuanTri").value;
    var TenDangNhap = document.getElementById("TenDangNhap").value;
    var MatKhau = document.getElementById("MatKhau").value;
    var letters = /^[A-Za-z]+$/;
    var numbers = /^[0-9]+$/;
    // Check handle
    if (TenQuanTri.length > 0) {
        if (TenDangNhap.length > 0) {
            $("#error_TenTaiKhoan").text('');
            if (MatKhau.length > 0) {
                $("#error_TenDangNhap").text('');
                if(MatKhau.length < 6){
                    $("#error_MatKhau").text('*Mật khẩu không được nhỏ hơn 6 ký tự*');
                    return false;
                }else{
                    return true;
                }
            } else {
                $("#error_MatKhau").text('*Mật khẩu không được để trống*');
                return false;
            }
        } else {
            $("#error_TenDangNhap").text('*Tên đăng nhập không được để trống*');
            return false;
        }
    } else {
        $("#error_TenTaiKhoan").text('*Tên tài khoản không được để trống*');
        return false;
    }
}

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
    if (validate() == true) {
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
    } else {
        return false;
    }
    
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