var currentPage = "1"
var cdate = new Date(0001, 01, 01);
var sdate = new Date(0001, 01, 01);
var recname = '';
var email = '';
var roleid = 0;



function searchButton()
{
    cdate = $('#cdate').val();
    sdate = $('#sdate').val();
    recname = $('#name').val();
    email = $('#email').val();
    roleid = $('#role').val();
    getFilter();
}


function getFilter()
{
    $.ajax({
        url: '/AdminSite/EmailLogFilter',
        type: 'GET',
        data:
        {
            Name: recname,
            RoleId: roleid,
            Email: email,
            CDate: cdate,
            SDate: sdate,
            CurrentPage: currentPage

        },

        success: function (res)
        {
            $('#loglist').html(res);
        }
    });
}

function pageNumber(temp) {
    currentPage = temp;
    getFilter();
}