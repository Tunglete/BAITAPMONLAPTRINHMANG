var checksearch = 0;
var codesearch = "";

$(document).ready(function () {
    
    $.ajax({
        url: formUrl.urlDanhsachgiaodichtable,
        mtype: "POST",
        datatype: "json",
        data:{
            page: 1
        },
        success: function (data) {
            $("#dsgd-table").html(data);
        }
    });
});
function previous(page) {
    if (checksearch == 0) {
        $.ajax({
            url: formUrl.urlDanhsachgiaodichtable,
            type: 'get',
        data: {
            page: page-1
        },
        success: function(response){
            $("#dsgd-table").html(response)
        }

    });
} else {
            $.ajax({
                url: formUrl.urlSearch,
    type: 'get',
data: {
    codesearch: codesearch,
    page: page-1
},
success: function(response){
    $("#dsgd-table").html(response)
}

})
}


}
function next(page) {
    if (checksearch == 0) {
        $.ajax({
            url: formUrl.urlDanhsachgiaodichtable,
            type: 'get',
        data: {
            page: page+1
        },
        success: function(response){
            $("#dsgd-table").html(response)
        }

    });
}
else {
            $.ajax({
                url: formUrl.urlSearch,
    type: 'get',
data: {
    codesearch: codesearch,
    page: page+1
},
success: function(response){
    $("#dsgd-table").html(response)
}

});
}


}

   
function Delete(id, page) {
    var r = confirm("Bạn có chắc chắn muốn xóa");
    if (r == true) {
        $.ajax({
            type: 'post',
            data: {
                id: id
            },
            url: formUrl.urlDelete,
            success: function (response) {
                if (response == 'True') {
                    $.ajax({
                        url: formUrl.urlDanhsachgiaodichtable,
                        mtype: "POST",
                        datatype: "json",
                        data: {
                            page: page
                        },
                        success: function (data) {
                            $("#dsgd-table").html(data);
                        }
                    });
                }
            }
        });
    } else {
        return false;
    }

}
//function button tìm kiếm theo trạng thái đơn hàng
$(function () {

    $("#ttdh li a").click(function () {

        $("#btn-ttdh").text($(this).text());
        checksearch = 1;
        codesearch = $(this).text();
        $.ajax({
            url: formUrl.urlSearch,
            mtype: "POST",
            datatype: "json",
            data: {
                codesearch: codesearch,
                page:1

            },
            success: function (data) {
                $("#dsgd-table").html("");
                $("#dsgd-table").html(data);
            }
        });
    });

});
//function button tìm kiếm theo tổng giá trị đơn hàng
$(function () {

    $("#tgtdh li a").click(function () {
        checksearch = 1;
        $("#btn-tgtdh").text($(this).text());
        codesearch = $(this).text();
        $.ajax({
            url: formUrl.urlSearch,
            mtype: "POST",
            datatype: "json",
            data: {
                codesearch: codesearch,
                page: 1
            },
            success: function (data) {
                $("#dsgd-table").html("");
                $("#dsgd-table").html(data);
            }
        });
    });

});
//function button đặt tìm kiếm về mặc định
$(function () {

    $("#macdinh").click(function () {
        checksearch = 0;
        $("#btn-ttdh").text("Trạng thái đơn hàng");
        $("#btn-ttdh").val("");
        $("#btn-tgtdh").text("Tổng giá trị đơn hàng");
        $("#btn-tgtdh").val("");
        $.ajax({
            url: formUrl.urlSearch,
            mtype: "POST",
            datatype: "json",
            data: {
                codesearch: "",
                page: 1
            },
            success: function (data) {
                $("#dsgd-table").html("");
                $("#dsgd-table").html(data);
            }
        });
    });

});
