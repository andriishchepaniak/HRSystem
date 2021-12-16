$( document ).ready(function() {
    $.ajax({
        url: window.location.origin +'/api/Employees/GetAll',
        type: 'GET',
        contentType: "application/json; charset=utf-8",  
        headers: {"Authorization": localStorage.getItem('access_token')},
        success: function(employees) {
            createCardList(employees)
        },
        error: function(res){
            toastr.error(res.responseText);
        }            
    });
});

function createCardList(employees){
    container = $('.row')
    $.each(employees, function(index, employee){
        html = `<div id="card-wrapper" class='col'>
                    <div class="card" >
                        <img class="card-img-top" src="/images/blank-profile-picture-973460_1280.png" alt="Card image cap">
                        <div class="card-body">
                            <h5 class="card-title">${employee['firstName']} ${employee['lastName']}</h5>
                        </div>
                        <ul class="list-group list-group-flush">
                            <li class="list-group-item">${employee['education']['university']}</li>
                            <li class="list-group-item">${employee['email']}</li>
                        </ul>
                        <div class="card-body">
                            <a href="#" class="card-link">View Profile</a>
                        </div>
                    </div>
                </div>`
        container.append(html);
    });
}
    