window.onload = () => {
    getcurrentWeek();
    getCurrentDate();
    
}

var regionid = 0;
var scheduling = 1;
var today = new Date();
var Week = new Date();
var Month = new Date();







async function Scheduling(temp)
{
   /* window.sessionStorage.setitem("scheduling", temp);*/
    /* var regionid = window.sessionStorage.getitem("region");*/
    scheduling = temp;

     today = new Date();
     Week = new Date();
    Month = new Date();
    await $.ajax({

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

    if (temp == 3)
    {
        getcurrentMonth();
    }

    if (temp == 1) {
        getCurrentDate();
    }
    if (temp == 2)
    {
        getcurrentWeek();
    }

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

function getcurrentWeek()
{
    console.log(Intl.DateTimeFormat('en', { day: '2-digit' }).format(Week));
    var  weekstart = Week.setDate(Week.getDate() - Week.getDay());
    let monthstart = new Intl.DateTimeFormat('en', { month: 'short' }).format(weekstart);
    let daystart = new Intl.DateTimeFormat('en', { day: '2-digit' }).format(weekstart);
    var weekend = new Date()
    var weekend = Week.setDate((Week.getDate() - Week.getDay())+6);
    let monthend = new Intl.DateTimeFormat('en', { month: 'short' }).format(weekend);
    let dayend = new Intl.DateTimeFormat('en', { day: '2-digit' }).format(weekend);
    DateForDay = monthstart + ' ' + daystart + '-' + monthend + ' ' + dayend;

    $('#sun').html(daystart);


    var mon = Week.setDate((Week.getDate() - Week.getDay()) + 1);

    let monday = new Intl.DateTimeFormat('en', { day: '2-digit' }).format(mon);
    $('#mon').html(monday);

    var tue = Week.setDate((Week.getDate() - Week.getDay()) + 2);
    let tuesday = new Intl.DateTimeFormat('en', { day: '2-digit' }).format(tue);
    $('#tue').html(tuesday);

    var wed = Week.setDate((Week.getDate() - Week.getDay()) + 3);
    let wednseday = new Intl.DateTimeFormat('en', { day: '2-digit' }).format(wed);
    $('#wed').html(wednseday);


    var thu = Week.setDate((Week.getDate() - Week.getDay()) + 4);
    let thursday = new Intl.DateTimeFormat('en', { day: '2-digit' }).format(thu);
    $('#thu').html(thursday);

    var fri = Week.setDate((Week.getDate() - Week.getDay()) + 5);
    let friday = new Intl.DateTimeFormat('en', { day: '2-digit' }).format(fri);
    $('#fri').html(friday);

    $('#sat').html(dayend);

    document.getElementById("date").innerHTML = DateForDay;

   

   

}


function Next()
{

    if (scheduling == 1)
    {

        today.setDate(today.getDate() + 1);
        getCurrentDate();
    }
    if (scheduling == 2)
    {
        Week.setDate(Week.getDate() + 7)
        getcurrentWeek();
        
    }
    if (scheduling == 3)
    {
        Month.setDate(1);
        Month.setMonth(Month.getMonth() + 1);
        getcurrentMonth();
    }
}

function Previous() {
   

    if (scheduling == 1)
    {
        today.setDate(today.getDate() - 1);
        getCurrentDate();
    }
    if (scheduling == 2) {
        Week.setDate(Week.getDate() - 7)
        getcurrentWeek();
    }
    if (scheduling == 3)
    {
        Month.setDate(1);
        Month.setMonth(Month.getMonth() - 1);
        getcurrentMonth();
    }
}




function getcurrentMonth()
{
    

    let year = new Intl.DateTimeFormat('en', { year: 'numeric' }).format(Month);
    let month = new Intl.DateTimeFormat('en', { month: 'short' }).format(Month);
    month = month;
    DateForMonth = month + ', ' + year;
    document.getElementById("date").innerHTML = DateForMonth;
}

            