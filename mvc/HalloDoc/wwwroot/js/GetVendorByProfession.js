
function GetVendor(id) {

    console.log(id)
    $.ajax({
        url: '/AdminSite/GetVendor',
        type: 'GET',
        contentType: 'application/IAction',
        data: { id: id },

       
        }
        
    });
}