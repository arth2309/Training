window.onload = () => {

    getCurrentDate();
    loadRegionForShift();
    $('#isRepeat').hide();
}

var regionid = 0;
var scheduling = 1;
var today = new Date();
var Week = new Date();
var Month = new Date();
const weekDays = ["sun", "mon", "tue", "wed", "thu", "fri", "sat"];


async  function MainScheduling(temp)
{

    today = new Date();
    Week = new Date();
    Month = new Date();

    $('.sc').removeClass("btn-info btn-outline-info text-white");

    if (temp == 1)
    {
        $('.day').addClass("btn-info text-white");
        $('.week').addClass("btn-outline-info");
        $('.month').addClass("btn-outline-info");
    }
    if (temp == 2)
    {
        $('.week').addClass("btn-info text-white");
        $('.day').addClass("btn-outline-info");
        $('.month').addClass("btn-outline-info");
    }
    if (temp == 3)
    {
        $('.month').addClass("btn-info text-white");
        $('.week').addClass("btn-outline-info");
        $('.day').addClass("btn-outline-info");
    }

    await Scheduling(temp);

    if (temp == 3) {
        getcurrentMonth();
    }

    if (temp == 1) {
        getCurrentDate();
    }
    if (temp == 2) {
        getcurrentWeek();
    }
}




async function Scheduling(temp)
{
   /* window.sessionStorage.setitem("scheduling", temp);*/
    /* var regionid = window.sessionStorage.getitem("region");*/
    scheduling = temp;
    var reqDate;
    if (scheduling == 1)
    {
        reqDate = today;
    }
    else if (scheduling == 2)
    {
        reqDate = Week;
    }
    else
    {
        reqDate = Month;
    }

    var weekday = new Intl.DateTimeFormat('en', { weekday: 'long' }).format(reqDate);
    let year = new Intl.DateTimeFormat('en', { year: 'numeric' }).format(reqDate);
    let month = new Intl.DateTimeFormat('en', { month: 'short' }).format(reqDate);
    let day = new Intl.DateTimeFormat('en', { day: '2-digit' }).format(reqDate);
    DateForDay = weekday + ", " + month + ' ' + day + ', ' + year;



    await $.ajax({

        url: '/AdminSite/SchedulingFilter',
        type: 'GET',
        data: {
            Scheduling: scheduling,
            RegionId: regionid,
            ReqDate: DateForDay
        },

        success: function (res)
        {
            $('#scheduling').html(res);
        }
    });

    

}


async function SchedulingByRegion(region)
{
  
    regionid = region;
    await Scheduling(scheduling);
    if (scheduling == 3) {
        getcurrentMonth();
    }

    if (scheduling == 1) {
        getCurrentDate();
    }
    if (scheduling == 2) {
        getcurrentWeek();
    }
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

    var sun = Week.setDate((Week.getDate() - Week.getDay()));
    let sunday = new Intl.DateTimeFormat('en', { day: 'numeric' }).format(sun);
    $('#sun').html(sunday);


    var mon = Week.setDate((Week.getDate() - Week.getDay()) + 1);
    let monday = new Intl.DateTimeFormat('en', { day: 'numeric' }).format(mon);
    $('#mon').html(monday);

    var tue = Week.setDate((Week.getDate() - Week.getDay()) + 2);
    let tuesday = new Intl.DateTimeFormat('en', { day: 'numeric' }).format(tue);
    $('#tue').html(tuesday);

    var wed = Week.setDate((Week.getDate() - Week.getDay()) + 3);
    let wednseday = new Intl.DateTimeFormat('en', { day: 'numeric' }).format(wed);
    $('#wed').html(wednseday);


    var thu = Week.setDate((Week.getDate() - Week.getDay()) + 4);
    let thursday = new Intl.DateTimeFormat('en', { day: 'numeric' }).format(thu);
    $('#thu').html(thursday);

    var fri = Week.setDate((Week.getDate() - Week.getDay()) + 5);
    let friday = new Intl.DateTimeFormat('en', { day: 'numeric' }).format(fri);
    $('#fri').html(friday);

    var sat = Week.setDate((Week.getDate() - Week.getDay()) + 6);
    let saturday = new Intl.DateTimeFormat('en', { day: 'numeric' }).format(sat);
    $('#sat').html(saturday);

    document.getElementById("date").innerHTML = DateForDay;

   

   

}


async function Next()
{
   
    if (scheduling == 1)
    {
        
        today.setDate(today.getDate() + 1);
        await Scheduling(scheduling);
        await getCurrentDate();
    }
    if (scheduling == 2)
    {
        Week.setDate(Week.getDate() + 7);
        await Scheduling(scheduling);
        await getcurrentWeek();
        
    }
    if (scheduling == 3)
    {

        //for (i = 0; i < 6; i++)
        //{
        //    for (j = 0; j < 7; j++)
        //    {
        //        var y = weekDays[j] + "-" + i;
        //        $(`#${y}`).text("");
        //        $(`#${y}`).addClass("bg-white");
        //    }
        //}
        

        Month.setDate(1);
        Month.setMonth(Month.getMonth() + 1);
        await Scheduling(scheduling);
        await getcurrentMonth();
    }
}

async function Previous() {
   
    
    if (scheduling == 1)
    {
        today.setDate(today.getDate() - 1);
        await Scheduling(scheduling);
        getCurrentDate();
    }
    if (scheduling == 2) {
        Week.setDate(Week.getDate() - 7)
        await Scheduling(scheduling);
        getcurrentWeek();
    }
    if (scheduling == 3)
    {
        //for (i = 0; i < 6; i++) {
        //    for (j = 0; j < 7; j++) {
        //        var y = weekDays[j] + "-" + i;
        //        $(`#${y}`).text("");
        //        $(`#${y}`).addClass("bg-white");
        //    }
        //}

        Month.setDate(1);
        Month.setMonth(Month.getMonth() - 1);
        await Scheduling(scheduling);
        getcurrentMonth();
    }
}




function leapyear(year) {
    return (year % 100 === 0) ? (year % 400 === 0) : (year % 4 === 0);
}
function getcurrentMonth() {


    let year = new Intl.DateTimeFormat('en', { year: 'numeric' }).format(Month);
    let month = new Intl.DateTimeFormat('en', { month: 'short' }).format(Month);
    month = month;
    DateForMonth = month + ', ' + year;
    document.getElementById("date").innerHTML = DateForMonth;


    Month.setDate(1);
    var currentMonth = new Intl.DateTimeFormat('en', { month: 'numeric' }).format(Month);
    

    var numberOfDays;

    

    if (currentMonth == 2)
    {
        if (leapyear(year))
        {
            numberOfDays = 29;
        }
        else
        {
            numberOfDays = 28;
        }
    }
    else if (currentMonth == 4 || currentMonth == 6 || currentMonth == 9 || currentMonth == 11) {
        numberOfDays = 30;
    }
    else
    {
        numberOfDays = 31;
    }
    console.log(numberOfDays);

    var temp = 0;
    for (i = 0; i < numberOfDays; i++) {
        var x = Month.getDay();
        if (x == 0 && i != 0) {
            temp++;
        }
        var y = weekDays[x] + "-" + temp;
        console.log(y);
        $(`#${y}`).text(i + 1);
         $(`#${y}`).removeClass("bg-white");
        Month.setDate(Month.getDate() + 1);
    }
    if (temp != 4) {
        $(`.row-4`).css("display", "none");
    }
    if (temp != 5) {
        $(`.row-5`).css("display", "none");
    }
    Month.setDate(Month.getDate() - 1);
}

function loadRegionForShift() {
    $.ajax({
        url: '/AdminSite/GetRegions',
        type: 'GET',
        success: function (res) {
            var listofregion = JSON.parse(res);
            console.log(listofregion);
            var regionList = document.getElementById("selectRegion");
            while (regionList.options.length > 0) {
                regionList.remove(0);
            }

            regionList.appendChild(new Option("Region",""));
            listofregion.forEach(function (region) {
                regionList.appendChild(new Option(region.Name, region.RegionId));
            });
            console.log(regionList);

            //var physicianList = document.getElementById("PhysicianList");
            //while (physicianList.options.length > 0) {
            //    physicianList.remove(0);
            //}
            //physicianList.appendChild(new Option("Select Physician", ""));

            //var description = document.getElementById("notesForAssign");
            //description.value = '';

        },
        error: function (err) {
            console.error(err);
        }
    });
}


function loadPhysiciansByRegion(RegionId) {
    $.ajax({
        url: '/AdminSite/GetPhysiciansByRegion',
        type: 'GET',
        data: {
            RegionId: RegionId
        },
        success: function (res) {
            var listOfPhysicians = JSON.parse(res);
            console.log(listOfPhysicians);
            var physicianList = document.getElementById("selectPhysician");
            while (physicianList.options.length > 0) {
                physicianList.remove(0);
            }
            physicianList.appendChild(new Option("Physician", ""));
            listOfPhysicians.forEach(function (physician) {
                physicianList.appendChild(new Option(physician.FirstName, physician.PhysicianId));
            });
        },
        error: function (err) {
            console.error(err);
        }
    });
}

function stopModal()
{
    $('#createShift').modal('toggle');
}



function Repeat(doc)
{
    if ($(doc).is(':checked'))
    {
        $('#isRepeat').show();
    }
    else
    {
        $('#isRepeat').hide();
       
    }
}

            