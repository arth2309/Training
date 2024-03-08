window.onload = () => {
    loadRegion();
}


function loadRegion() {
    $.ajax({
        url: '/AdminSite/GetRegions',
        type: 'GET',
        success: function (res) {
            var listofregion = JSON.parse(res);
            console.log(listofregion);
            var regionList = document.getElementsByClassName("regionList");
   
            regionList.appendChild(new Option("Select Region", "0"));
            listofregion.forEach(function(region) {
                regionList.appendChild(new Option(region.Name, region.RegionId));
            });
            console.log(regionList)
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
            faxNumber.value = "";
        },
        error: function (err) {
            console.error(err);
        }
    });
}