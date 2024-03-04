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
                $('#tableID').DataTable();
                $('#customSearch').on('keyup', function () {
                    var searchTerm = $(this).val().toLowerCase();
                    console.log(searchTerm)
                    $('#tableID').DataTable().search(searchTerm).draw();
                });
            }
            else if (response && statusId == 2) {
                $('#pendingstate').html(response);
                $('#tableID1').DataTable();
                $('#customSearch').on('keyup', function () {
                    var searchTerm = $(this).val().toLowerCase();
                    console.log(searchTerm)
                    $('#tableID').DataTable().search(searchTerm).draw();
                });
            }
            else if (response && statusId == 3) {
                $('#activestate1').html(response);
                $('#tableID2').DataTable();
                $('#customSearch2').on('keyup', function () {
                    var searchTerm = $(this).val().toLowerCase();
                    console.log(searchTerm)
                    $('#tableID2').DataTable().search(searchTerm).draw();
                });
            }
            else if (response && statusId == 4) {
                $('#concludestate').html(response);
                $('#tableID3').DataTable();
                $('#customSearch3').on('keyup', function () {
                    var searchTerm = $(this).val().toLowerCase();
                    console.log(searchTerm)
                    $('#tableID3').DataTable().search(searchTerm).draw();
                });
            }
            else if (response && statusId == 5) {
                $('#toclosestate').html(response);
                $('#tableID4').DataTable();
                $('#customSearch4').on('keyup', function () {
                    var searchTerm = $(this).val().toLowerCase();
                    console.log(searchTerm)
                    $('#tableID4').DataTable().search(searchTerm).draw();
                });
            }
            else if (response && statusId == 6) {
                $('#unpaidstate').html(response);
                $('#tableID5').DataTable();
                $('#customSearch5').on('keyup', function () {
                    var searchTerm = $(this).val().toLowerCase();
                    console.log(searchTerm)
                    $('#tableID5').DataTable().search(searchTerm).draw();
                });
            }
            else
            {
                $('#newstate').html(response);

            }
        }

    }
    );
}

