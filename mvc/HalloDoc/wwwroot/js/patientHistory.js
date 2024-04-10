var currentPage = "1"
var fname = '';
var lname = '';
var mobile = '';
var email = '';




function searchButton() {
    fname = $('#fname').val();
    lname = $('#lname').val();
    mobile = $('#mobile').val();
    email = $('#email').val();
    getFilter();
}


function getFilter() {
    $.ajax({
        url: '/AdminSite/PatientHistoryFilter',
        type: 'GET',
        data:
        {
            FirstName: fname,
            LastName: lname,
            Email: email,
            Mobile: mobile,
            CurrentPage: currentPage

        },

        success: function (res) {
            $('#patienthistory').html(res);
        }
    });
}

function pageNumber(temp) {
    currentPage = temp;
    getFilter();
}
