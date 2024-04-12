﻿window.onload = () => {

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
const weekDays1 = ["sun1", "mon1", "tue1", "wed1", "thu1", "fri1", "sat1"];



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
    var startDate;
    var endDate;
    if (scheduling == 1)
    {
        startDate = today;
        endDate = today;
    }
    else if (scheduling == 2)
    {
        startDate = Week.setDate(Week.getDate() - Week.getDay());
        endDate = Week.setDate((Week.getDate() - Week.getDay()) + 6);
        
    }
    else
    {
        startDate = Month.setDate(1);
        Month.setMonth(Month.getMonth() + 1);
        endDate = Month.setDate(Month.getDate() - 1);
    }

    var weekday = new Intl.DateTimeFormat('en', { weekday: 'long' }).format(startDate);
    let year = new Intl.DateTimeFormat('en', { year: 'numeric' }).format(startDate);
    let month = new Intl.DateTimeFormat('en', { month: 'short' }).format(startDate);
    let day = new Intl.DateTimeFormat('en', { day: '2-digit' }).format(startDate);
    startDay = weekday + ", " + month + ' ' + day + ', ' + year;

    var weekday1 = new Intl.DateTimeFormat('en', { weekday: 'long' }).format(endDate);
    let year1 = new Intl.DateTimeFormat('en', { year: 'numeric' }).format(endDate);
    let month1 = new Intl.DateTimeFormat('en', { month: 'short' }).format(endDate);
    let day1 = new Intl.DateTimeFormat('en', { day: '2-digit' }).format(endDate);
    endDay = weekday1 + ", " + month1 + ' ' + day1 + ', ' + year1;



    await $.ajax({

        url: '/AdminSite/SchedulingFilter',
        type: 'GET',
        data: {
            Scheduling: scheduling,
            RegionId: regionid,
            StartDay: startDay,
            EndDay: endDay
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
    let mday = new Intl.DateTimeFormat('en', { day: 'numeric' }).format(today);
    DateForDay = weekday + ", " + month + ' ' + day + ', ' + year;
    document.getElementById("date").innerHTML = DateForDay;
    document.getElementById("mMonth").innerHTML = month;
    document.getElementById("mDay").innerHTML = mday;
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
    $('#sun1').html(sunday);


    var mon = Week.setDate((Week.getDate() - Week.getDay()) + 1);
    let monday = new Intl.DateTimeFormat('en', { day: 'numeric' }).format(mon);
    $('#mon').html(monday);
    $('#mon1').html(monday);

    var tue = Week.setDate((Week.getDate() - Week.getDay()) + 2);
    let tuesday = new Intl.DateTimeFormat('en', { day: 'numeric' }).format(tue);
    $('#tue').html(tuesday);
    $('#tue1').html(tuesday);

    var wed = Week.setDate((Week.getDate() - Week.getDay()) + 3);
    let wednseday = new Intl.DateTimeFormat('en', { day: 'numeric' }).format(wed);
    $('#wed').html(wednseday);
    $('#wed1').html(wednseday);

    var thu = Week.setDate((Week.getDate() - Week.getDay()) + 4);
    let thursday = new Intl.DateTimeFormat('en', { day: 'numeric' }).format(thu);
    $('#thu').html(thursday);
    $('#thu1').html(thursday);

    var fri = Week.setDate((Week.getDate() - Week.getDay()) + 5);
    let friday = new Intl.DateTimeFormat('en', { day: 'numeric' }).format(fri);
    $('#fri').html(friday);
    $('#fri1').html(friday);

    var sat = Week.setDate((Week.getDate() - Week.getDay()) + 6);
    let saturday = new Intl.DateTimeFormat('en', { day: 'numeric' }).format(sat);
    $('#sat').html(saturday);
    $('#sat1').html(saturday);

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
        var z = weekDays1[x] + "-" + temp;
        
        console.log(y);
        $(`#${y}`).text(i + 1);
        $(`#${z}`).text(i + 1);
         $(`#${y}`).removeClass("bg-white");
        Month.setDate(Month.getDate() + 1);
    }
    if (temp != 4) {
        $(`.row-4`).css("display", "none");
        $(`.row1-4`).css("display", "none");
    }
    if (temp != 5) {
        $(`.row-5`).css("display", "none");
        $(`.row1-5`).css("display", "none");
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


function viewShift(id)
{
    $.ajax({
        url: '/AdminSite/ViewShift',
        type: 'GET',
        data: {
            ShiftDetailId: id
        },
        success: function (res) {

            var list = JSON.parse(res);
            console.log(list);

            var physicianName = document.getElementById("pname");
            var shiftDate = document.getElementById("shiftdate");
            var startTime = document.getElementById("starttime");
            var endTime = document.getElementById("endtime");
            var sid = document.getElementById("shiftdetailid");

            physicianName.value = list.PhysicianName;
            shiftDate.value = list.ShiftDate;
            startTime.value = list.StartTime;
            endTime.value = list.EndTime;
            sid.value = id;


        },

    });
}

function toggle()
{
    $('.dis').prop('disabled', false);
    $('#edit').addClass("d-none");
    $('#save').removeClass("d-none");
}

function removetoggle()
{
    $('.dis').prop('disabled', true);
    $('#edit').removeClass("d-none");
    $('#save').addClass("d-none");
}

function editViewShift()
{
    var data = JSON.stringify({
        Id : parseInt($('#shiftdetailid').val()),
        ShiftDate: $('#shiftdate').val(),
        StartTime: $('#starttime').val(),
        EndTime: $('#endtime').val()
    });

    $.ajax({

        url: '/AdminSite/EditViewShift',
        type: 'GET',
        data: {
            Data: data
        },
        success: function (res)
        {
            window.location.reload();
        }

    });
}

function deleteViewShift(id)
{
    $.ajax({

        url: '/AdminSite/DeleteViewShift',
        type: 'GET',
        data: {
            Id: id
        },
        success: function (res) {
            window.location.reload();
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


async function ChangeShift(temp)
{
    console.log(today);
    Month.setDate(temp)
    today = Month;

    $('.sc').removeClass("btn-info btn-outline-info text-white");
    $('.day').addClass("btn-info text-white");
    $('.week').addClass("btn-outline-info");
    $('.month').addClass("btn-outline-info");

    await Scheduling(1);

    getCurrentDate();
}
            