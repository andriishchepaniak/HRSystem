$( document ).ready(function() {
    $('#auth-register-form').submit(function(e){
        e.preventDefault();
        let email = $(this).find('#email').val().trim();
        let password = $(this).find('#password').val().trim();
        let repeatPassword = $(this).find('#repeat-password').val().trim();
        let university = $(this).find('#university').val().trim();
        let firstname = $(this).find('#firstname').val().trim();
        let lastname = $(this).find('#lastname').val().trim();
        if (email === "" || password === "" || repeatPassword === "" || university === "" || firstname === "" || lastname === ""){
            toastr.error("All fields are required");
            return;
        }
        if (password !== repeatPassword)
        {
            toastr.error("Please, repeat your password correctly");
            return;
        }
        
        $.ajax({
            url: window.location.origin +'/api/Auth/register',
            type: 'POST',
                     
            data: JSON.stringify ({Email: email, Password: password, University: university, FirstName: firstname, LastName: lastname}), 
            contentType: "application/json; charset=utf-8",  
            success: function(token) {
                window.localStorage.setItem('access_token', token['access_token']);
                window.location.href = window.location.origin + '/MainPage';
            
            },
            error: function(res){
                toastr.error(res.responseText);
            }            
        });
    });
});