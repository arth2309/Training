

function adminResetPassword(id,password,adminid)
{
    $.ajax({

        url: '/AdminSite/AdminResetPassword',
        type: 'GET',
        data: {
            id: id,
            adminid: adminid,
            password: password
        },
        success: function (res) {
            window.location.href = res.redirect;
        }

    });
}


function adminInformation(id,adminid, firstname, lastname, email, mobile)
{
    $.ajax({

        url: '/AdminSite/AdminInformation',
        type: 'GET',
        data: {
            id: id,
            adminid: adminid,
            firstname: firstname,
            lastname: lastname,
            email: email,
            mobile: mobile
        },
        success: function (res) {
            window.location.href = res.redirect;
        }

    });
}



function adminBillingInformation(adminid, ad1, ad2, city, state, zip, altphone)
{
    $.ajax({
        url: '/AdminSite/AdminBillingInformation',
        type: 'GET',
        data: {
            adminid: adminid,
            ad1: ad1,
            ad2: ad2,
            city: city,
            state: state,
            zip: zip,
            altphone: altphone
        },
        success: function (res) {
            window.location.href = res.redirect;
        }
    });
}