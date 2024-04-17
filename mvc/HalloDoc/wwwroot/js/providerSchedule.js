window.onload = () =>
{

    getcurrentMonth();
    $('#isRepeat').hide();
}


var Month = new Date();
const weekDays = ["sun", "mon", "tue", "wed", "thu", "fri", "sat"];
const weekDays1 = ["sun1", "mon1", "tue1", "wed1", "thu1", "fri1", "sat1"];








async function Scheduling()
{
    
    var startDate = Month.setDate(1);
       Month.setMonth(Month.getMonth() + 1);
      var endDate = Month.setDate(Month.getDate() - 1);


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

        url: '/Provider/ProviderScheduling',
        type: 'GET',
        data: {
            
            StartDay: startDay,
            EndDay: endDay
        },

        success: function (res) {
            $('#scheduling').html(res);
        }
    });



}







async function Next()
{


        Month.setDate(1);
        Month.setMonth(Month.getMonth() + 1);
        await Scheduling(scheduling);
        await getcurrentMonth();
    
}

async function Previous()
{

        Month.setDate(1);
        Month.setMonth(Month.getMonth() - 1);
        await Scheduling(scheduling);
        getcurrentMonth();
    
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



    if (currentMonth == 2) {
        if (leapyear(year)) {
            numberOfDays = 29;
        }
        else {
            numberOfDays = 28;
        }
    }
    else if (currentMonth == 4 || currentMonth == 6 || currentMonth == 9 || currentMonth == 11) {
        numberOfDays = 30;
    }
    else {
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











function stopModal() {
    $('#createShift').modal('toggle');
}



function Repeat(doc) {
    if ($(doc).is(':checked')) {
        $('#isRepeat').show();
    }
    else {
        $('#isRepeat').hide();

    }
}



