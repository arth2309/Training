function GetStatus(temp,currentPage,type) {
    var statusId = temp;
    window.sessionStorage.setItem("status", statusId);
    window.sessionStorage.setItem("currentpage", currentPage);
    var typeid = window.sessionStorage.getItem("typeid");
    var regionid = window.sessionStorage.getItem("regionid");
    var name = window.sessionStorage.getItem("name");
    
    $.ajax({
        url: '/AdminSite/CheckStatus',
        type: 'Get',
        data: {
            statusI: statusId,
            currentPage: currentPage,
            typeid: typeid,
            regionid: regionid,
            name: name
        },

        success: function (response) {
            if (response && statusId == 1) {
                $('#newstate').html(response);
                $('#statusname').text("(new)");

            }
            else if (response && statusId == 2) {
                $('#pendingstate').html(response);
                $('#statusname').text("(pending)");
            }
            else if (response && statusId == 3) {
                $('#activestate1').html(response);
                $('#statusname').text("(active)");
            }
            else if (response && statusId == 4) {
                $('#concludestate').html(response);
                $('#statusname').text("(conclude)");
            }
            else if (response && statusId == 5) {
                $('#toclosestate').html(response);
                $('#statusname').text("(toclose)");
            }
            else if (response && statusId == 6) {
                $('#unpaidstate').html(response);
                $('#statusname').text("(unpaid)");
            }
            else {
                $('#newstate').html(response);

            }



        },
        error: function (err) {
            console.error(err);
           
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

function changestring(name)
{
    window.sessionStorage.setItem("name", name);
    var status = window.sessionStorage.getItem("status");
    GetStatus(status, 1, 1);
}

function cleartoggle() {
    window.sessionStorage.setItem("typeid", 0);
    window.sessionStorage.setItem("regionid", 0);
    window.sessionStorage.setItem("name",'');
}



