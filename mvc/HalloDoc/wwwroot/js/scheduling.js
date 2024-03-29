window.onload = () => {
    getCurrentDate();
}

var regionid = 0;
var scheduling = 1;
var today = new Date();


function Scheduling(temp)
{
   /* window.sessionStorage.setitem("scheduling", temp);*/
    /* var regionid = window.sessionStorage.getitem("region");*/
    scheduling = temp;

    if (temp == 3)
    {
        getcurrentMonth();
    }

    if (temp == 1) {
        getCurrentDate();
    }


    $.ajax({

        url: '/AdminSite/SchedulingFilter',
        type: 'GET',
        data: {
            Scheduling: scheduling,
            RegionId: regionid
        },

        success: function (res)
        {
            $('#scheduling').html(res);
        }
    });
}


function SchedulingByRegion(region)
{

    regionid = region;
    Scheduling(scheduling);
}


function getCurrentDate()
{
    
    var weekday = new Intl.DateTimeFormat('en', { weekday: 'long' }).format(today);
    let year = new Intl.DateTimeFormat('en', { year: 'numeric' }).format(today);
    let month = new Intl.DateTimeFormat('en', { month: 'short' }).format(today);
    let day = new Intl.DateTimeFormat('en', { day: '2-digit' }).format(today);
    DateForDay = weekday + ", " + month + ' ' + day + ', ' + year;
    document.getElementById("date").innerHTML = DateForDay;
}


function Next()
{
    today.setDate(today.getDate() + 1);

    if (scheduling == 1)
    {
        getCurrentDate();
    }
    if (scheduling == 3)
    {
        getcurrentMonth();
    }
}

function Previous() {
    today.setDate(today.getDate() - 1);

    if (scheduling == 1)
    {
        getCurrentDate();
    }
    if (scheduling == 3)
    {

        getcurrentMonth();
    }
}


function getcurrentMonth()
{
    
    let year = new Intl.DateTimeFormat('en', { year: 'numeric' }).format(today);
    let month = new Intl.DateTimeFormat('en', { month: 'short' }).format(today);
    DateForMonth = month + ', ' + year;
    document.getElementById("date").innerHTML = DateForMonth;
}

            