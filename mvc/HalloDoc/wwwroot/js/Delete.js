function DeleteFile(id) {
    
    console.log(id)
    $.ajax({
        url: '/AdminSite/Delete',
        type: 'GET',
        contentType: 'application/json',
    });
}
