function checkForm() {
	//Get param
	var txtFirstName = document.getElementById("txtFirstName").value;
	var txtLastName = document.getElementById("txtLastName").value;
	var dobday = document.getElementById("dobday").value;
	var dobmonth = document.getElementById("dobmonth").value;
	var dobyear = document.getElementById("dobyear").value;
	var sex = document.getElementById("sex").value;
	var email = document.getElementById("email").value;
	var phone = document.getElementById("phone").value;
	var letters = /^[A-Za-z]+$/;
	var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
	var numbers = /^[0-9]+$/;
	// Check handle
	if(txtFirstName.match(letters)) {
		if(txtLastName.match(letters)) {
			if(dobday == "" || dobmonth == "" || dobyear == "") {
				 alert("*Please choose date of birth*");
				return false;
			} else {
				if(sex == "") {
					 alert("*Please choose gender*");
					return false;
				} else {
					if(email.match(mailformat)) {
						if(phone.match(numbers)) {
							if (phone.length < 9 || phone.length > 12) {
								 alert("*Phone is only 9-12 number*");
								return false;
							} else {
								return true;
							}
						} else {
							 alert("*Phone is only number*");
							return false;
						}
					} else {
						 alert("*Please input correct email*");
						return false;
					}
				}
			}
		} else {
			 alert("*Please enter correct last name*");
			return false;
		}
	} else {
		 alert("*Please enter correct first name*");
		return false;
	}
}