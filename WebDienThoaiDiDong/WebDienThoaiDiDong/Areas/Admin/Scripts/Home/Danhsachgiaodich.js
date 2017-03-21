$(document).ready(function () {
    $.ajax({
        url: urlDanhsachgiaodich,
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
    $.ajax({
        url: urlDanhsachgiaodich,
        type: 'get',
    data: {
        page: page-1
    },
    success: function(response){
        $("#dsgd-table").html(response)
    }

});

}
function next(page) {
    $.ajax({
        url: urlDanhsachgiaodich,
        type: 'get',
    data: {
        page: page+1
    },
    success: function(response){
        $("#dsgd-table").html(response)
    }

});

}