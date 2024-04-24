
function SearchFilter(temp) {
    $.ajax({
        url: '/DashBoard/ProjectDashBoardFilter',
        type: 'GET',
        data: {
            srchStr: temp.trim()
        },
        success: function (res) {
            $('#list').html(res);
            
        }
    });
}


function deleteRecord(temp)
{
    $.ajax({
        url: '/DashBoard/Delete',
        type: 'GET',
        data: {
            Id : temp
        },
        success: function (res)
        {
            window.location.reload();
        }
    });
}

function getRecord(temp) {
    $.ajax({
        url: '/DashBoard/GetFormData',
        type: 'GET',
        data: {
            Pid: temp
        },
        success: function (res) {
            $('#task-form').html(res);
            $('#task').modal("show");
        }
    });
}

function clearRecord() {


    $.ajax({
        url: '/DashBoard/EmptyFormData',
        type: 'GET',

        success: function (res) {
            $('#task-form').html(res);
            $('#task').modal("show");
        }
    });
}


