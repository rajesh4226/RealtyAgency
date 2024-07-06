$(document).ready(function () {
    //homesearch
    $('.search-tab').on('click', function () {
        $(this).closest('ul').find('.search-tab').removeClass('active');
        $(this).addClass('active');
        var searchdata = $(this).data('target');
        $(this).closest('.banner-search').find('.search-bar').addClass('hide');
        $(searchdata).removeClass('hide');
        $(this).closest('.banner-search').find('.search-list-group').addClass('hide');
        $(this).removeClass('hide');
    })

    //Login Register Modal

    $("#btnModalLogin").on("click", function (e) {
        let email = $("#logEmail").val();
        let password = $("#logPassword").val();
          
        $.ajax({
            url: `${baseUri}/Account/Login`,
            type: "POST",
            data: ({ Email: email, Password: password, RememberMe: $("#RememberMe").is(':checked') }),
            success: function (response) {
                if (response.isSuccess == true)
                    location.reload();
                else {
                    $("#login-error").text(response.message);
                }
            },
            error: function () {
                alert("An error has occured!");
            }
        });
    });
    $("#btnModalRegister").on("click", function (e) {
        $.ajax({
            url: `${baseUri}/Account/Register`,
            type: "POST",
            data: ({ FirstName: $("#RegFirstName").val(), LastName: $("#RegLastName").val(), Email: $("#RegEmail").val(), Password: $("#RegPassword").val(), RememberMe: true, }),
            success: function (response) {
                if (response.isSuccess == true)
                    location.reload();
                else {
                    $("#register-error").text(response.message);
                }
            },
            error: function () {
                alert("An error has occured!");
            }
        });
    });

     
    //Button click for offer
    //$(".btn-get-my-offer").click(function () {
    //    window.location.href = `${baseUri}/PreQualify/Index/?propertyAddress=` + $("#autocomplete").val();
    //});
});

function validateEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}
 


         
 

