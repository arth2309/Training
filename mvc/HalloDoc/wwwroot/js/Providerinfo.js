


function getEmail(id) {

    console.log(id)
    $.ajax({
        url: '/AdminSite/GetEmailForMessage',
        type: 'GET',
        data: { id: id },
        success: function (res) {
            var datalist = JSON.parse(res);
            console.log(datalist);
            var emailforcontact = document.getElementById("emailForContact");
            emailforcontact.value = datalist.Email;
            console.log(emailforcontact.value);
           
        }
    });
}

function SendEmail(email,description) {

    
    $.ajax({
        url: '/AdminSite/SendEmailForMessage',
        type: 'GET',
        data: {
            email: email,
            description: description
        },
        success: function (res) {
            window.location.href = res.redirect;
        }
    });
}


