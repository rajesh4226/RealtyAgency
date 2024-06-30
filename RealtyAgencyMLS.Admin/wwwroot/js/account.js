$(document).ready(function () {

    //Login Register Modal
    $("#btnAccountLogin").on("click", function (e) {
        let email = $("#Email").val();
        let password = $("#Password").val();
          
        $.ajax({
            url: `${baseUri}/Account/Login`,
            type: "POST",
            data: ({ Email: email, Password: password, RememberMe: $("#RememberMe").is(':checked') }),
            success: function (response) {
                if (response.isSuccess == true)
                    window.location.href = `${baseUri}/Account/Logout/`;
                else {
                    $("#login-error").text(response.message);
                }
            },
            error: function () {
                alert("An error has occured!");
            }
        });
    });
});

 


         
 

