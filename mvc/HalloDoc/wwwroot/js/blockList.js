
var currentPage = 1;
var email = '';
var date = new Date(0001, 01, 01);
var patientname = '';
var mobile = '';


function searchButton()
    {
    email = $('#email').val();
    date = $('#date').val();
    mobile = $('#mobile').val();
    patientname = $('#name').val();
    blockHistoryFilter();

    }

function blockHistoryFilter()
{

    $.ajax({
        url: '/AdminSite/BlockedHistoryFilter',
        type: 'GET',
        data: {
            Name: patientname,
            Email: email,
            Date: date,
            Mobile: mobile,
            CurrentPage: currentPage
        },
        success: function (res)
        {
            $('#blockedhistory').html(res);
        }

    });
}


function pageNumberForBlockList(temp)
{
    currentPage = temp;
    blockHistoryFilter();
}
