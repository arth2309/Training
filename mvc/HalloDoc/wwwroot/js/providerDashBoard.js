
window.onload = () => {
    window.sessionStorage.clear();
}
function GetStatus(temp, currentPage, type) {
    var statusId = temp;
    window.sessionStorage.setItem("status", statusId);
    window.sessionStorage.setItem("currentpage", currentPage);
    var typeid = window.sessionStorage.getItem("typeid");
    var name = window.sessionStorage.getItem("name");
    
    $.ajax({
        url: '/Provider/CheckStatus',
        type: 'Get',
        data: {
            statusI: statusId,
            currentPage: currentPage,
            typeid: typeid,
            name: name
        },

        success: function (response)
        {
            if (response && statusId == 1) {
               
                $('#statusname').text("(new)");

            }
            else if (response && statusId == 2) {
              
                $('#statusname').text("(pending)");
            }
            else if (response && statusId == 3) {
               
                $('#statusname').text("(active)");
            }
            else
            {
              
                $('#statusname').text("(conclude)");
            }

            $('#states').html(response);
            
            
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


function changestring(name)
{
    window.sessionStorage.setItem("name", name);
    var status = window.sessionStorage.getItem("status");
    GetStatus(status, 1, 1);
}

function cleartoggle()
{
    window.sessionStorage.setItem("typeid", 0);
    window.sessionStorage.setItem("name",'');
}

