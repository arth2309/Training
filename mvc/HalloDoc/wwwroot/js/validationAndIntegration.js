function transferRequest() {
    var data = JSON.stringify({
        RequestId: parseInt($("#requestidfortransfer").val()),
        Description: $("#notesForTransfer").val(),
        RegionId: parseInt($("#regionList1").val()),
        PhysicianId: parseInt($("#PhysicianList1").val())
    });

    console.log(data);
    if (validateFormInput()) {
        $.ajax({
            url: "/AdminSite/TransferCase",
            type: "GET",
            data: {
                data: data
            },
            success: function (res) {
                window.location.reload();
            },

            error: function (err) {
                console.error(err);
            }
        });
    } else {
        return;
    }
}

function removeValidation()
{
    $("#errorRegionForTransfer").text("");
    $("#errorPhysicianForTransfer").text("");
    $("#errorNotesForTransfer").text("");

}

function validateFormInput() {
    if ($("#regionList1").val() == "" || $("#PhysicianList1").val() == "" || $("#notesForTransfer").val() == null || $("#notesForTransfer").val() == "") {
        if ($("#regionList1").val() == "") {
            $("#errorRegionForTransfer").text("Please select region.");
            
        } else {
            $("#errorRegionForTransfer").text("");
            
        }
        if ($("#PhysicianList1").val() == "") {
            $("#errorPhysicianForTransfer").text("Please select physician.");
            
        } else {
            $("#errorPhysicianForTransfer").text("");
           
        }
        if ($("#notesForTransfer").val() == null || $("#notesForTransfer").val() == "") {
            $("#errorNotesForTransfer").text("Please enter description.");
           
        } else {
            $("#errorNotesForTransfer").text("");
            
        }
        return false;
    } else {
        $("#errorRegionForTransfer").text("");
       
        $("#errorPhysicianForTransfer").text("");
       
        $("#errorNotesForTransfer").text("");
       
        return true;
    }
}


function assignRequest() {
    var data = JSON.stringify({
        RequestId: parseInt($("#requestidforAssign").val()),
        Description: $("#notesForAssign").val(),
        RegionId: parseInt($("#regionList").val()),
        PhysicianId: parseInt($("#PhysicianList").val())
    });

    console.log(data);
    if (validateFormInputA()) {
        $.ajax({
            url: "/AdminSite/AssignCase",
            type: "GET",
            data: {
                data: data
            },
            success: function (res) {
                window.location.reload();
            },

            error: function (err) {
                console.error(err);
            }
        });
    } else {
        return;
    }
}

function removeValidationA() {
    $("#errorRegionForAssign").text("");
    $("#errorPhysicianForAssign").text("");
    $("#errorNotesForAssign").text("");
}

function validateFormInputA() {
    if ($("#regionList").val() == "" || $("#PhysicianList").val() == "" || $("#notesForAssign").val() == null || $("#notesForAssign").val() == "") {
        if ($("#regionList").val() == "") {
            $("#errorRegionForAssign").text("Please select region.");

        } else {
            $("#errorRegionForAssign").text("");

        }
        if ($("#PhysicianList").val() == "") {
            $("#errorPhysicianForAssign").text("Please select physician.");

        } else {
            $("#errorPhysicianForAssign").text("");

        }
        if ($("#notesForAssign").val() == null || $("#notesForAssign").val() == "") {
            $("#errorNotesForAssign").text("Please enter description.");

        } else {
            $("#errorNotesForAssign").text("");

        }
        return false;
    } else {
        $("#errorRegionForAssign").text("");

        $("#errorPhysicianForAssign").text("");

        $("#errorNotesForAssign").text("");

        return true;
    }
}

function GetRequestIdBlock(requestId) {

    $.ajax({
        url: '/AdminSite/LoadBlockCaseData',
        type: 'GET',
        data: {
            Requestid: requestId
        },
        success: function (res) {
            var datalist = JSON.parse(res);
            console.log(datalist);
            var name = "Patient: "+ datalist.FirstName + "," + datalist.LastName ;
            $('#blockCasePatientName').html(name);
            document.getElementById("RequestIdBlock").value = requestId;
            console.log(document.getElementById("RequestIdBlock").value);
        }
    });
}

function blockRequest() {
    var data = JSON.stringify({
        requestId: parseInt($("#RequestIdBlock").val()),
        blockNotes: $("#ReasonForBlock").val(),
    });
    if (ValidateFormBlock()) {
        $.ajax({
            url: "/AdminSite/BlockCase",
            type: "GET",
            data: {
                data: data
            },
            success: function (res) {
                window.location.reload();

            },
            error: function (err) {
                console.error(err);
            },

        });
    } else {
        return;
    }

}

function ValidateFormBlock() {
    if ($("#ReasonForBlock").val() == null || $("#ReasonForBlock").val() == "") {
        $("#errorForReasonBlock").text("Please enter Reason for block.");
        return false;
    } else {
        $("#errorForReasonBlock").text("");
        return true;
    }
}

function removeValidationBlock() {
    $("#errorForReasonBlock").text("");
}


function getRequestIdForCancelCase(requestID) {
    $.ajax({
        url: '/AdminSite/LoadBlockCaseData',
        type: 'GET',
        data: {
            Requestid: requestID
        },
        success: function (res) {
            var datalist = JSON.parse(res);
            console.log(datalist);
            var name = "Patient: " + datalist.FirstName + "," + datalist.LastName;
            $('#CancelCasePatientName').html(name);
            document.getElementById("RequestIdCancel").value = requestID;
            console.log(document.getElementById("RequestIdCancel").value);
        }
    });
}

function CancelCaseConfirm() {
    console.log("CancelCase");
    var data = JSON.stringify({
        requestId: parseInt($("#RequestIdCancel").val()),
        Notes: $("#descriptionCancel").val(),
        CaseTag: $("#ReasonForCancellation").val()
    });
    if (ValidateCancelCase()) {
        console.log("Valid");
        $.ajax({
            url: "/AdminSite/CancelCase",
            type: "GET",
            data: {
                data: data
            },
            success: function (res) {
                if (res) {
                    console.log(res);
                    window.location.reload();

                }
            },
            error: function (err) {
                console.error(err);
            },
        });
    } else {
        console.log("InValid");
        return;
    }
}

function ValidateCancelCase() {
    console.log($("#ReasonForCancellation").val());
    if ($("#ReasonForCancellation").val() == 0 || $("#ReasonForCancellation").val() == '' || $("#ReasonForCancellation").val() == "") {
        console.log($("#errorForReason1"));
        $("#errorForReason1").text("Please select valid Reason.");
        return false;
    } else {
        $("#errorForReason1").text("");
        return true;
    }
}

function removeValidationCancel() {
    $("#errorForReason1").text("");

}



function SendAgreement() {
    var data = JSON.stringify({
        Requestid: parseInt($("#requestIdForAgreement").val()),
        Mobile: parseInt($("#phoneNumberForAgreement").val()),
        Email: $("#emailForAgreement").val()
    });

    if (ValidateSendAgreement()) {
        console.log("Valid")
        $.ajax({
            url: "/AdminSite/SendAgreement",
            type: "GET",
            data: {
                data: data
            },
            success: function (res) {
                window.location.reload();
            },
            error: function (err) {
                console.error(err);
            },
        });
    } else {
        console.log("Invalid")
        return;
    }
}

function ValidateSendAgreement() {
    if ($("#phoneNumberForAgreement").val() == "" || $("#emailForAgreement").val() == "" || !validateEmail($("#emailForAgreement").val())) {
        if ($("#phoneNumberForAgreement").val() == "") {
            $("#errorForNumberForAgreement").text("Please enter Phone Number.");

        } else {
            $("#errorForNumberForAgreement").text("");

        }

        if (validateEmail($("#emailForAgreement").val())) {
            $("#errorForEmailForAgreement").text("");
        } else {
            $("#errorForEmailForAgreement").text("Invalid Email");
        }

        if ($("#emailForAgreement").val() == "") {
            $("#errorForEmailForAgreement").text("Please enter Email Number.");
        } else {
            $("#errorForEmailForAgreement").text("");
        }
        return false;
    } else {
        $("#errorForNumberForAgreement").text("");
        $("#errorForEmailForAgreement").text("");
        return true;
    }
}

function removeValidationForAgreement() {
    $("#errorForNumberForAgreement").text("");
    $("#errorForEmailForAgreement").text("");
}

function validateEmail(email) {
    return email.match(
        /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    );
};