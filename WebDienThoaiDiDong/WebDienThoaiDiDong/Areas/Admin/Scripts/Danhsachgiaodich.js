
function chitiet(id, namecus) {
    $.ajax({
        type: 'post',
        data: {
            id: id,
            namecus:namecus
        },
        url: formUrl.urlDetail,
        success: function (data) {
            $("#modalcontent").html(data);
        }
    });

   
}