var currentPage = "1"
var fdate = new Date(0001, 01, 01);
var tdate = new Date(0001, 01, 01);
var patientname = '';
var providername
var mobile = '';
var type = 0;
var email = '';



function searchButton() {
    currentPage = 1;
    fdate = $('#fromdate').val();
    tdate = $('#todate').val();
    patientname = $('#patientname').val();
    providername = $('#providername').val();
    email = $('#email').val();
    mobile = $('#mobile').val();
    type = $('#requestType').val();
    getFilter();
}


function getFilter() {
    $.ajax({
        url: '/AdminSite/SearchRecordFilter',
        type: 'GET',
        data:
        {
            PatientName: patientname,
            ProviderName: providername,
            TypeId: type,
            Email: email,
            Mobile: mobile,
            FDate: fdate,
            TDate: tdate,
            CurrentPage: currentPage
        },

        success: function (res) {
            $('#recordlist').html(res);
        }
    });
}

function pageNumber(temp)
{
    currentPage = temp;
    getFilter();
}


function ExportRecordData() {
    
    $.ajax({
        url: '/AdminSite/ExportSearchRecord',
        type: 'POST',
        xhrFields: {
            responseType: 'arraybuffer'
        },
        data: {
            PatientName: patientname,
            ProviderName: providername,
            TypeId: type,
            Email: email,
            Mobile: mobile,
            FDate: fdate,
            TDate: tdate
        },
        success: function (res) {
            console.log(res);
            const a = document.createElement('a');
            var unit8array = new Uint8Array(res);
            a.href = window.URL.createObjectURL(new Blob([unit8array],
                { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' }));
            a.download = 'Record.xlsx';
            a.click();
        },

    });
}