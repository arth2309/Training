function DeleteFile(id) {
    
    console.log(id)
    $.ajax({
        url: '/AdminSite/Delete',
        type: 'GET',
        contentType: 'application/json',
        data: { id: id },
        success: function (response) {
            window.location.href = response.redirect;
        }
    });
}
