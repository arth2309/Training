function GetStatus(temp)
{
    var statusId = temp;
    $.ajax({
        url: '/AdminSite/CheckStatus',
        type: 'Get',
        contentType: 'application/IAction',
        data: {statusI: statusId},
        success: function (response) {
            if (response && statusId == 1) {
                $('#newstate').html(response);
            }
            else if (response && statusId == 2) {
                $('#pendingstate').html(response);
            }
            else if (response && statusId == 3) {
                $('#activestate1').html(response);
            }
            else if (response && statusId == 4) {
                $('#concludestate').html(response);
            }
            else if (response && statusId == 5) {
                $('#toclosestate').html(response);
            }
            else if (response && statusId == 6) {
                $('#unpaidstate').html(response);
            }
            else
            {
                $('#newstate').html(response);
            }
        }

    }
    );
}

