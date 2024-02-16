function checkemail() {
    var emailid = $(".email1").val();
    console.log(emailid)
    $.ajax({
        url: '/Patient/CheckEmailExists',
        type: 'GET',
        contentType: 'application/json',
        data: { email: emailid },
        success: function (response) {
            console.log(response)
            if (response) {
                $('#input-password').hide();
            } else {
                $('#input-password').css("display", "flex");
            }
        }

    });
}
