window.onload = () => {
    loadProfession();
}


function loadProfession() {
    $.ajax({
        url: '/admin/profession',
        type: 'GET',
        success: function (res) {
            var listOfProfession = JSON.parse(res);
            var professionList = document.getElementById("professionList");
            while (professionList.options.length > 0) {
                professionList.remove(0);
            }
            professionList.appendChild(new Option("Select Profession", "0"));
            listOfProfession.forEach(function (profession) {
                professionList.appendChild(new Option(profession.ProfessionName, profession.HealthProfessionalId));
            });
            console.log(professionList);
        },
        error: function (err) {
            console.error(err);
        }
    });
}

function loadBusinessList(professionTypeId) {
    $.ajax({
        url: '/admin/business',
        type: 'GET',
        data: {
            professionTypeId: professionTypeId
        },
        success: function (res) {
            var listOfBusiness = JSON.parse(res);
            var businessList = document.getElementById("businessList");
            while (businessList.options.length > 0) {
                businessList.remove(0);
            }
            businessList.appendChild(new Option("Select Business", "0"));
            listOfBusiness.forEach(function (business) {
                businessList.appendChild(new Option(business.VendorName, business.VendorId));
            });
            var contact = document.getElementById("vendorMobileNumber");
            var email = document.getElementById("email");
            var faxNumber = document.getElementById("faxnumber");

            contact.value = "";
            email.value = "";
            faxNumber.value ="";
        },
        error: function (err) {
            console.error(err);
        }
    });
}

function loadBusinessData(vendorId) {
    $.ajax({
        url: '/admin/business-data',
        type: 'GET',
        data: {
            vendorId: vendorId
        },
        success: function (res) {
            var businessData = JSON.parse(res);
            console.log(businessData);
            var contact = document.getElementById("vendorMobileNumber");
            var email = document.getElementById("email");
            var faxNumber = document.getElementById("faxnumber");

            contact.value = businessData.PhoneNumber;
            email.value = businessData.Email;
            faxNumber.value = businessData.FaxNumber;
        },
        error: function (err) {
            console.error(err);
        }
    });
}

