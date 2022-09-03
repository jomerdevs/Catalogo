
$("#signup").click(function () {
    $("#first").fadeOut("fast", function () {
        $("#second").fadeIn("fast");
    });
});

$("#signin").click(function () {
    $("#second").fadeOut("fast", function () {
        $("#first").fadeIn("fast");
    });
});



$(function () {
    $("form[name='login']").validate({
        rules: {

            Email: {
                required: true,
                email: true
            },
            Password: {
                required: true,

            }
        },
        messages: {
            Email: "Please enter a valid email address",

            Password: {
                required: "Please enter password",

            }

        },
        submitHandler: function (form) {
            form.submit();
        }
    });
});



$(function () {

    $("form[name='registration']").validate({
        rules: {
            FirstName: "required",
            Email: {
                required: true,
                email: true
            },
            Phone: "required",
            Password: {
                required: true,
            },
            ConfirmPassword: {
                required: true,
            }
        },

        messages: {
            FirstName: "Please enter your firstname",
            Password: {
                required: "Please provide a password",
            },
            ConfirmPassword: {
                required: "Passwords must be the same",
            },
            Email: "Please enter a valid email address",        
            Phone: "Please enter a phone number"
        },
        

        submitHandler: function (form) {
            form.submit();
        }
    });
});
