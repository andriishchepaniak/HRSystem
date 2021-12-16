$( document ).ready(function() {
    $('#auth-form').submit(function(e){
        e.preventDefault();
        let email = $(this).find('#email').val().trim();
        let password = $(this).find('#password').val().trim();
        if (email === "" || password === ""){
            toastr.error("All fields are required");
            return;
        }
        
        $.ajax({
            url: window.location.origin +'/api/Auth/login',
            type: 'POST',
                     
            data: JSON.stringify ({email: email, password: password}), 
            dataType: "json",
            contentType: "application/json; charset=utf-8",  
            success: function(token) {
                window.localStorage.setItem('access_token', token['access_token']);
                window.location.href = window.location.origin + '/MainPage';
            },
            error: function(error){
                toastr.error(error.responseText);
            }            
        });
    });
});
