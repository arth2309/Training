function Region() {
    var id = ".region-" + $('#selectRegion').val();
    console.log(id);
    $('.physicianOptions').css("display", "none");
    $(id).css("display", "block");
}