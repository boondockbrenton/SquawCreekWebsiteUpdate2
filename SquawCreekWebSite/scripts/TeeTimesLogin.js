var uri = '../api/users';

$(document).ready(function () {
	$('#signIn').submit(function (ev) {
		ev.preventDefault(); // to stop the form from submitting

		$.ajax({
			type: 'POST',
			url: uri,
			data: '{action:"checkUserPass", user:"' + $('#signIn').find('input[name="username"]').val() + '", password:"' + $('#signIn').find('input[name="password"]').val() + '" }', // or JSON.stringify ({name: 'jonas'}),
			success: function (result) {
				if (result !== "")
					window.location.href = result;
				else
					$("#loginFailed").removeClass("hide");
			},
			contentType: "application/json",
			dataType: 'json'
		});
	});
});