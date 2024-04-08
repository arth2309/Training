
var vendorname = '';
var currentpage = 1;
var professionid = 0;


function getVendorData()
{
    $.ajax({
        url: '/AdminSite/ProfessionMenuFilter',
        type: 'GET',
        data: {
            ProfessionId: professionid,
            Name: vendorname,
            CurrentPage: currentpage
        },

        success: function (res)
        {
            $('#vendorlist').html(res);
        }
    });
    
}


function searchVendor(temp)
{
    vendorname = temp.trim();
    getVendorData();
}

function pageNumberForVendorList(temp)
{
    currentpage = temp;
    getVendorData();
}

function searchProfession(temp)
{
    professionid = temp;
    getVendorData();
}

function deleteVendor(temp)
{
    $.ajax({
        url: '/AdminSite/DeleteVendor',
        type: 'GET',
        data: {
            VendorId: temp
        },
        success: function (res)
        {
            window.location.reload();
        }

    });
}