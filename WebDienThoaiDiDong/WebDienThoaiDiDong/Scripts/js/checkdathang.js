
		$(document).ready(function(){
		    $('input[name="radio2"]').on('click', function() {
		        if ($(this).val() === '0') {
		            $('#city').removeProp("disabled");
		            $('#address').removeProp("disabled");
		        }
		        else {
		            $('#city').prop("disabled", "disabled");
		            $('#address').prop("disabled", "disabled");
		        }
		    });
		    $('#form_register').validate({
		        rules:{
		            name: "required",
		            phone: {
		                required:true,
		                minlength:10,
		                number:true,
		            },
		            radio2: {required:true},
		            city: {required:true},
		            address: "required",
		        },
		        messages:{
		            name: "Vui lòng nhập họ tên",
		            phone:{
		                required: "Vui lòng nhập số điện thoại",
		                minlength: "Số máy quý khách vừa nhập là số không có thực",
		                number: "Số máy quý khách vừa nhập là số không có thực",
		            },
		            radio: "Vui lòng chọn",
		            radio2: "Vui lòng chọn",
		            city: "Vui lòng chọn",
		            address:"Vui lòng nhập địa chỉ",

		        }
		    });
		});