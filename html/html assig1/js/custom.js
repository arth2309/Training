function sidebar()
{
    if(document.getElementById('sidebarID').classList.contains('d-none'))
    {
        document.getElementById('sidebarID').classList.remove('d-none');
        document.getElementById('sidebarID').classList.add('d-lg-flex');
    }
    else{
        document.getElementById('sidebarID').classList.remove('d-lg-flex');
        document.getElementById('sidebarID').classList.add('d-none');
    }
    }
