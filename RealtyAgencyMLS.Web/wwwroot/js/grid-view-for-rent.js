function GetPropertyData(isPagination) {
    let url = window.location.href;
    let urlparts = url.split('?');
    $.ajax({
        url: `${baseUri}/ForRent/GetPropertyListAjaxGridView/?IsPagination=` + isPagination + "&" + urlparts[1],
        type: "get",
        success: function (response) {
            $('#dvPropertyList').html('');
            $('#dvPropertyList').html(response);
            if (isPagination) {
                $("html, body").animate({ scrollTop: 0 }, 1000);
            }
        },
        error: function () {
            swal("Server Error", "An error has occured!", "error");
        }
    });
}