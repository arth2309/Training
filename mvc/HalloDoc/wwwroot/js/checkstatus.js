function GetStatus(temp,currentPage,type) {
    var statusId = temp;
    window.sessionStorage.setItem("status", statusId);
    var typeid = window.sessionStorage.getItem("typeid");
    var regionid = window.sessionStorage.getItem("regionid");

    $.ajax({
        url: '/AdminSite/CheckStatus',
        type: 'Get',
        data: {
            statusI: statusId,
            currentPage: currentPage,
            typeid: typeid,
            regionid: regionid
        },

        success: function (response) {
            if (response && statusId == 1) {
                $('#newstate').html(response);
            }
            else if (response && statusId == 2) {
                $('#pendingstate').html(response);
            }
            else if (response && statusId == 3) {
                $('#activestate1').html(response);
            }
            else if (response && statusId == 4) {
                $('#concludestate').html(response);
            }
            else if (response && statusId == 5) {
                $('#toclosestate').html(response);
            }
            else if (response && statusId == 6) {
                $('#unpaidstate').html(response);
            }
            else {
                $('#newstate').html(response);

                 }
        },
        error: function (err) {
            console.error(err);
            if (statusId == 1)
            {
                
                $('#nonewtabledata').html('<p>no data available</p>');
            }
        }

    });
    

}


function changetype(typeid)
{
    window.sessionStorage.setItem("typeid", typeid);
    var status = window.sessionStorage.getItem("status");
    GetStatus(status, 1, typeid);
    
}

function changeregion(regionid)
{
    window.sessionStorage.setItem("regionid", regionid);
    var status = window.sessionStorage.getItem("status");
    GetStatus(status, 1, 1);

}

function cleartoggle()
{
    window.sessionStorage.setItem("typeid", 0);
    window.sessionStorage.setItem("regionid", 0);
}



