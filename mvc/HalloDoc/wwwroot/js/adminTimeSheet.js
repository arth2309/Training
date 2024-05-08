var Id = 1;
var tdate = new Date();
tdate.setDate(1);
tdate.setMonth(-3);
var sdate = tdate;



function getTimeSheet()
{
    $.ajax({

        url: '/AdminSite/GetProviderTimeSheet',
        type: 'GET',
        data: {
            PhysicianId: Id,
            StartDate: sdate
        },

        success: function (res)
        {
            $('#sheet').html(res);
        }
    });
}



function changePhysician(temp)
{
    Id = temp;
    getTimeSheet();
}

function changeDate(temp)
{
    sdate = temp;
    getTimeSheet();
}


function biWeeklySheet() {
    $.ajax({
        url: '/AdminSite/WeeklySheet',
        type: 'GET',
        data: {
            id: Id,
            StartDate: sdate
        },
        success: function (res) {
            window.location.href = res.redirect;

        }
    });
}