
var currentPage = 1;
function blockHistoryFilter()
{
    var email = $('#email').val();
    var date = $('#date').val();
    var mobile = $('#mobile').val();
    var name = $('#name').val();

    $.ajax({
        url: '/AdminSite/BlockedHistoryFilter',
        type: 'GET',
        data: {
            Name: name,
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
