
window.onload = () => {
    document.getElementById("selectRegion").value = 0;
}

var currentPage = 1;
var regionid = 0;
var currentMonth = false;


function getPhysicianList()
{
    $.ajax({
        url: '/AdminSite/GetPhysicianListForShift',
        type: 'GET',
        data: {
            CurrentPage : currentPage,
            RegionId: regionid,
            CurrentMonth: currentMonth
        },
        success: function (res)
        {
            $('#shiftforreview').html(res);
        },
    });
}


function pageNumber(page)
{
    list = [];
    currentPage = page;
    getPhysicianList();
}

function Region(id)
{
    list = [];
    regionid = id;
    currentPage = 1;
    getPhysicianList();
   
}

function ViewCurrentMonth()
{
    list = [];
    if (currentMonth)
    {
        currentMonth = false;
        currentPage = 1;
        getPhysicianList();

        $('#currentMonth').removeClass("btn-outline-success shadow");
        $('#currentMonth').addClass("btn-success");
    }
    else
    {
        currentMonth = true;
        currentPage = 1;
        getPhysicianList();

        $('#currentMonth').removeClass("btn-success");
        $('#currentMonth').addClass("btn-outline-success shadow");
      
    }
}

var list = []
function checkedlist(temp)
{
    if ($(temp).is(':checked')) {
        var checkboxes = document.querySelectorAll('input[name=shift]');
        var checkedBoxes = document.querySelectorAll('input[name=shift]:checked');
        console.log(checkedBoxes);
        if (checkedBoxes.length == checkboxes.length)
        {
            $('#checked').prop('checked', true);
        }
        var checked = ($(temp).val());
        list.push(checked);
    }
    else
    {
        $('#checked').prop('checked', false);
        var index = list.indexOf($(temp).val());
        if (index > -1)
        {
            
            list.splice(index,1);
        }
    }
    console.log(list);
}


function checkedAll(temp)
{
    if ($(temp).is(':checked'))
    {
        $('.shift').prop('checked', true);
        list=[]
        var checked = $('.shift');
        for (i = 0; i < checked.length;i++)
        {
            list.push($(checked[i]).attr("id"));
        }
        console.log(list);
    }
    else
    {
        $('.shift').prop('checked', false);
        list = [];
    }
}


function approveShift()
{
    $.ajax({

        url: '/AdminSite/ApproveShift',
        type: 'POST',
        data: {
            List: list
        },
        success: function (res)
        {
            window.location.reload();
        }

    });
}

function deleteShift() {
    $.ajax({

        url: '/AdminSite/DeleteShift',
        type: 'POST',
        data: {
            List: list
        },
        success: function (res) {
            window.location.reload();
        }

    });
}