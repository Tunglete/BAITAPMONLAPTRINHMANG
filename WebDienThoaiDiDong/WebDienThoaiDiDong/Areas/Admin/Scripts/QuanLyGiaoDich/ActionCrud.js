function chitiet(id, namecus) {
    $.ajax({
        type: 'post',
        data: {
            id: id,
            namecus: namecus
        },
        url: actionUrl.urlDetail,
        success: function (data) {
            $("#modalcontent").html(data);
        }
    });
}
function Edit(id, namecus,page) {
    
    $.ajax({
        type: 'get',
        data: {
            id: id,
            namecus: namecus,
            page: page
        },
        url: actionUrl.urlEdit,
        success: function (data) {
            $("#div_edit").html(data);
            $("#TrangThaiDonHang").select2();
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
                    url: urlDsgdtable,
                    data: {
                        page:page
                    },
                    success: function (data) {
                        $("#dsgd-table").html("");
                        $("#dsgd-table").html(data);
                        $("#TrangThaiDonHang").select2();
                    }
                    });
                $("#TrangThaiDonHang").select2();
            }
           
        }
    });
}
function dong() {
    $('#div_edit').html("");
}

