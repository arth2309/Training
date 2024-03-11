

function loadRegion() {
    $.ajax({
        url: '/AdminSite/GetRegions',
        type: 'GET',
        success: function (res) {
            var listofregion = JSON.parse(res);
            console.log(listofregion);
            var regionList = document.getElementById("regionList");
            while (regionList.options.length > 0) {
                regionList.remove(0);
            }
   
            regionList.appendChild(new Option("Select Region", "0"));
            listofregion.forEach(function(region) {
                regionList.appendChild(new Option(region.Name, region.RegionId));
            });
            console.log(regionList)
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
            RegionId  : RegionId
        },
        success: function (res) {
            var listOfPhysicians = JSON.parse(res);
            console.log(listOfPhysicians);
            var physicianList = document.getElementById("PhysicianList");
            while (physicianList.options.length > 0) {
                physicianList.remove(0);
            }
            physicianList.appendChild(new Option("Select Physician", "0"));
            listOfPhysicians.forEach(function (physician) {
                physicianList.appendChild(new Option(physician.FirstName, physician.PhysicianId));
            });
        },
        error: function (err) {
            console.error(err);
        }
    });
}

function getRequestID(Requestid)
{
    loadRegion();
    var requestid = document.getElementById("requestid");
    requestid.value = Requestid;
}

function loadRegion1() {
    $.ajax({
        url: '/AdminSite/GetRegions',
        type: 'GET',
        success: function (res) {
            var listofregion = JSON.parse(res);
            console.log(listofregion);
            var regionList = document.getElementById("regionList1");
            while (regionList.options.length > 0) {
                regionList.remove(0);
            }

            regionList.appendChild(new Option("Select Region", "0"));
            listofregion.forEach(function (region) {
                regionList.appendChild(new Option(region.Name, region.RegionId));
            });
            console.log(regionList)
        },
        error: function (err) {
            console.error(err);
        }
    });
}

function loadPhysiciansByRegion1(RegionId) {
    $.ajax({
        url: '/AdminSite/GetPhysiciansByRegion',
        type: 'GET',
        data: {
            RegionId: RegionId
        },
        success: function (res) {
            var listOfPhysicians = JSON.parse(res);
            console.log(listOfPhysicians);
            var physicianList = document.getElementById("PhysicianList1");
            while (physicianList.options.length > 0) {
                physicianList.remove(0);
            }
            physicianList.appendChild(new Option("Select Physician", "0"));
            listOfPhysicians.forEach(function (physician) {
                physicianList.appendChild(new Option(physician.FirstName, physician.PhysicianId));
            });
        },
        error: function (err) {
            console.error(err);
        }
    });
}

function getRequestID1(Requestid) {
    loadRegion1();
    var requestid = document.getElementById("requestid1");
    requestid.value = Requestid;
}


function getRequestIdForClearCase(Requestid) {
    var requestid = document.getElementById("reqforclearcase");
    requestid.value = Requestid;
}
function clearCase(RequestId)
{
    $.ajax({
        url: '/AdminSite/ClearCase',
        type: 'Get',

        data: {
            RequestId: RequestId
        },
        success: function (res) {
            window.location.href = res.redirect;
        }


    });
}

