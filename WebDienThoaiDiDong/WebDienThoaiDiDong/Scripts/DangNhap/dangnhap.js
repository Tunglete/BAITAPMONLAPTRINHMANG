
		var tendangnhap,matkhau,thongbao;
		tendangnhap = document.getElementById("username").value;
		matkhau = document.getElementById("pwd").value;

		function checkInput(){
			if(tendangnhap == null){
				thongbao = "Tên đăng nhập không được để trống";
				document.getElementById("thongbao").innerHTML = thongbao;
			}
			$.ajax({
			    url: urlDangNhap,
                type:'post',
                data: $("frm").serialize(),
                success: function () {

                }
			});
		}

		function validText(value) {
			var chaos = new Array ("'","~","@","#","$","%","^","&","*",";","/","\\","|");
			var sum = chaos.length;

			for (var i in chaos) {
				if (!Array.prototype[i]) {sum += value.lastIndexOf(chaos[i])}
			}

			if (sum) {
				alert("Chứa kí tự đặc biệt: @ # $ % ^ * ~ ");
				return false;
			}
			return true;
		}
