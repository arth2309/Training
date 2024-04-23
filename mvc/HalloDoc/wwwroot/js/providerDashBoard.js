
window.onload = () => {
    window.sessionStorage.clear();
    window.sessionStorage.setItem("status", 1);
    
}
function GetStatus(temp, currentPage, type) {
    var statusId = temp;
    window.sessionStorage.setItem("status", statusId);
    window.sessionStorage.setItem("currentpage", currentPage);
    var typeid = window.sessionStorage.getItem("typeid");
    var name = window.sessionStorage.getItem("name");
    
    $.ajax({
        url: '/Provider/CheckStatus',
        type: 'Get',
        data: {
            statusI: statusId,
            currentPage: currentPage,
            typeid: typeid,
            name: name
        },

        success: function (response)
        {
            if (response && statusId == 1) {
               
                $('#statusname').text("(new)");

            }
            else if (response && statusId == 2) {
              
                $('#statusname').text("(pending)");
            }
            else if (response && statusId == 3) {
               
                $('#statusname').text("(active)");
            }
            else
            {
              
                $('#statusname').text("(conclude)");
            }

            $('#states').html(response);
            
            
        },
        error: function (err) {
            console.error(err);
           
        }

    });
    

}


function changetype(typeid)
{
    window.sessionStorage.setItem("typeid", typeid);
    var status = window.sessionStorage.getItem("status");
    GetStatus(status, 1, typeid);


    
}


function changestring(name)
{
    window.sessionStorage.setItem("name", name);
    var status = window.sessionStorage.getItem("status");
    GetStatus(status, 1, 1);
}

function cleartoggle()
{
    window.sessionStorage.setItem("typeid", 0);
    window.sessionStorage.setItem("name",'');
}


function sendRequestId(temp)
{
    var requestId = document.getElementById("ride");
    requestId.value = temp;
}

function houseCall(temp) 
{
    $.ajax({
        url: '/Provider/HouseCall',
        type: 'GET',
        data: {
            RequestId: temp
        },
        success: function (res)
        {
            window.location.reload();
        }
    });
}

function sendTransferRequestId(temp) {
    var requestId = document.getElementById("trid1");
    requestId.value = temp;
}

function removeValidationTransfer() {
    $('#tdes').text("");
}

function loadSendAgreeMentData(requestid) {
    $.ajax({
        url: '/AdminSite/LoadAgreementData',
        type: 'GET',

        data: {
            Requestid: requestid
        },
        success: function (res) {
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
            else {

                document.getElementById("businessagreement").classList.add("d-flex");
                document.getElementById("businessagreement").classList.remove("d-none");

            }
        },

        error: function (err) {
            console.error(err);
        }

    });
}


function toConclude(temp)
{
    $.ajax
        ({
            url: '/Provider/ToConclude',
            type: 'GET',
            data: {
                RequestId:temp
            },
            success: function (res)
            {
                window.location.reload();
            }
        });
}

function generatePdf(temp) {



    $.ajax({
        url: "/Provider/downloadEncounterDocument",
        type: 'GET',
        data: {
            requestId: temp
        },

        xhrFields: {
            responseType: 'blob'
        },

        success: function (response) {

            var blob = new Blob([response], { type: "application/pdf" });
            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            link.download = "Encounter.pdf";
            link.click();

            window.location.href = "/Provider/ProviderDashboard";
        },
        error: function (error) {
            console.error("Error generating PDF:", error);
        }
    });

}