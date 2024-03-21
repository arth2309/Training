

function loadRegion() {
    $.ajax({
        url: '/AdminSite/GetRegions',
        type: 'GET',
        success: function (res) {
            var listofregion = JSON.parse(res);
            console.log(listofregion);
            var regionList = document.getElementById("regionList");
            while (regionList.options.length > 0) {
                regionList.remove(0);
            }
   
            regionList.appendChild(new Option("Select Region",""));
            listofregion.forEach(function(region) {
                regionList.appendChild(new Option(region.Name, region.RegionId));
            });
            console.log(regionList)

            var physicianList = document.getElementById("PhysicianList");
            while (physicianList.options.length > 0) {
                physicianList.remove(0);
            }
            physicianList.appendChild(new Option("Select Physician", ""));

            var description = document.getElementById("notesForAssign");
            description.value = '';

        },
        error: function (err) {
            console.error(err);
        }
    });
}

function loadPhysiciansByRegion(RegionId) {
    $.ajax({
        url: '/AdminSite/GetPhysiciansByRegion',
        type: 'GET',
        data: {
            RegionId  : RegionId
        },
        success: function (res) {
            var listOfPhysicians = JSON.parse(res);
            console.log(listOfPhysicians);
            var physicianList = document.getElementById("PhysicianList");
            while (physicianList.options.length > 0) {
                physicianList.remove(0);
            }
            physicianList.appendChild(new Option("Select Physician", ""));
            listOfPhysicians.forEach(function (physician) {
                physicianList.appendChild(new Option(physician.FirstName, physician.PhysicianId));
            });
        },
        error: function (err) {
            console.error(err);
        }
    });
}

function getRequestID(Requestid)
{
    loadRegion();
    var requestid = document.getElementById("requestidforAssign");
    requestid.value = Requestid;
}

function loadRegion1() {
    $.ajax({
        url: '/AdminSite/GetRegions',
        type: 'GET',
        success: function (res) {
            var listofregion = JSON.parse(res);
            console.log(listofregion);
            var regionList = document.getElementById("regionList1");
            while (regionList.options.length > 0) {
                regionList.remove(0);
            }

            regionList.appendChild(new Option("Select Region", ""));
            listofregion.forEach(function (region) {
                regionList.appendChild(new Option(region.Name, region.RegionId));
            });
            console.log(regionList)
        },
        error: function (err) {
            console.error(err);
        }
    });
}

function loadPhysiciansByRegion1(RegionId) {
    $.ajax({
        url: '/AdminSite/GetPhysiciansByRegion',
        type: 'GET',
        data: {
            RegionId: RegionId
        },
        success: function (res) {
            var listOfPhysicians = JSON.parse(res);
            console.log(listOfPhysicians);
            var physicianList = document.getElementById("PhysicianList1");
            while (physicianList.options.length > 0) {
                physicianList.remove(0);
            }
            physicianList.appendChild(new Option("Select Physician", ""));
            listOfPhysicians.forEach(function (physician) {
                physicianList.appendChild(new Option(physician.FirstName, physician.PhysicianId));
            });
        },
        error: function (err) {
            console.error(err);
        }
    });
}

function getRequestID1(Requestid) {
    loadRegion1();
    var requestid = document.getElementById("requestidfortransfer");
    requestid.value = Requestid;
    console.log(requestid.value);
}


function getRequestIdForClearCase(Requestid) {
    var requestid = document.getElementById("reqforclearcase");
    requestid.value = Requestid;
}
function clearCase(RequestId)
{
    $.ajax({
        url: '/AdminSite/ClearCase',
        type: 'Get',

        data: {
            RequestId: RequestId
        },
        success: function (res) {
            window.location.href = res.redirect;
        }


    });
}

function loadSendAgreeMentData(requestid)
{
    $.ajax({
        url: '/AdminSite/LoadAgreementData',
        type: 'GET',

        data: {
            Requestid: requestid
        },
        success: function (res)
        {
            var dataList = JSON.parse(res);
            console.log(dataList);
            var requestTypeId = document.getElementById("requestTypeIdForAgreement");
            var email = document.getElementById("emailForAgreement");
            var phoneNumber = document.getElementById("phoneNumberForAgreement");
            var request = document.getElementById("requestIdForAgreement");
           


            requestTypeId.value = dataList.RequestTypeId;
            email.value = dataList.Email;
            phoneNumber.value = dataList.PhoneNumber;
            request.value = dataList.RequestId;

           

            document.getElementById("patientagreement").classList.remove("d-flex");
            document.getElementById("patientagreement").classList.add("d-none");
            document.getElementById("familyagreement").classList.remove("d-flex");
            document.getElementById("familyagreement").classList.add("d-none");
            document.getElementById("conciergeagreement").classList.remove("d-flex");
            document.getElementById("conciergeagreement").classList.add("d-none");
            document.getElementById("businessagreement").classList.remove("d-flex");
            document.getElementById("businessagreement").classList.add("d-none");

           
  
            if (requestTypeId.value == 1) {
                document.getElementById("patientagreement").classList.add("d-flex");
                document.getElementById("patientagreement").classList.remove("d-none");

               
            }
            else if (requestTypeId.value == 2) {
                document.getElementById("familyagreement").classList.add("d-flex");
                document.getElementById("familyagreement").classList.remove("d-none");
               
            }
            else if (requestTypeId.value == 3) {
                document.getElementById("conciergeagreement").classList.add("d-flex");
                document.getElementById("conciergeagreement").classList.remove("d-none");

                
            }
            else
            {
                
                document.getElementById("businessagreement").classList.add("d-flex");
                document.getElementById("businessagreement").classList.remove("d-none");
               
            }
            },

        error: function (err) {
            console.error(err);
        }

    });
}

function loadViewAgreementData(requestid)
{
    $.ajax({
        url: '/AdminSite/LoadViewAgeement',
        type: 'GET',
        data: {
        Requestid: requestid
        },
        success: function (res) {
            var datalist = JSON.parse(res);
            console.log(datalist);
            var name = datalist.FirstName + " " + datalist.LastName + ",";
            $('#getnameforcancelagreement').html(name);
        }
    });
}


function cancelViewAgreementData(requestid, description)
{
    $.ajax({
        url: '/AdminSite/CancelViewAgeement',
        type: 'GET',
        data: {
            Requestid: requestid,
            Description: description
        },

        success: function (res) {
            window.location.href = res.redirect;
        }
    });
}


function AcceptViewAgreementData(requestid) {
    $.ajax({
        url: '/AdminSite/AcceptViewAgreement',
        type: 'GET',
        data: {
            Requestid: requestid
        },

        success: function (res) {
            window.location.href = res.redirect;
        }
    });
}

