TennisCourtBookingApp.Common = new function () {

   
    this.SignUpGet = function () {

        $(".preloader").show();
        $.ajax({
            type: "Get",
            url: UrlContent("Home/UserSignUp"),

            success: function (response) {

                $("#modalContent").html(response);
                $.validator.unobtrusive.parse($("#UserDetails"));
                $("#modalShow").modal('show');
                $(".preloader").hide();

            },
            error: function () {
                // Handle error if needed
                $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
            }
        });
    }

    //this.SignUpPosttt=function() {
    //    debugger;
    //    if ($("#UserDetails").valid()) {
    //        $(".preloader").show();
    //        var fileUpload = $("#SaveImage").get(0);
    //        var files = fileUpload.files;
    //        var formdata = new FormData();
    //        for (var i = 0; i < files.length; i++) {
    //            formdata.append("Image", files[i]);
    //        }
    //        var formdata = $("#UserDetails").serialize();
    //        $.ajax({
    //            type: "Post",
    //            url: UrlContent("Home/UserSignUp"),
    //            data: formdata,
    //            contentType: false,
    //            processData: false,
    //            //dataType: "text",
    //            success: function (response) {
    //                debugger;
    //                Swal.fire({
    //                    title: "Registration  Successfully",
    //                    icon: "Success"
    //                }).then((result) => {
    //                    if (result.isConfirmed) {

    //                    }
    //                    //var url = 'https://localhost:7235/';
    //                    //window.location.href = url;
    //                    //LoginGet();
    //                    //location.replace(url);
    //                    //window.location.reload();
    //                });
    //                //$("#modalContent").html(response);
    //                //$("#modalShow").modal('show');

    //                $(".preloader").hide();

    //            },
    //            error: function () {
    //                // Handle error if needed
    //                $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
    //            }
    //        });
    //    }
    //}




    //this.SignUpPosttttt=function () {

    //    if ($("#UserDetails").valid()) {
    //        $(".preloader").show();

    //        var formdata = new FormData();

    //        // Collect image files
    //        var fileUpload = $("#SaveImage").get(0);
    //        var files = fileUpload.files;
    //        for (var i = 0; i < files.length; i++) {
    //            formdata.append("Images", files[i]);
    //        }

    //        // Collect other form fields
    //        //formdata.append("Username", $("#Username").val());
    //        //formdata.append("Email", $("#Email").val());
    //        //formdata.append("Username", $("#UserAddress").val());
    //        //formdata.append("Username", $("#UserPhoneNo").val());
    //        //formdata.append("Username", $("#UserPassword").val());
    //        // Add more fields as needed

    //        $.ajax({
    //            type: "POST",
    //            url: UrlContent("Home/SaveProfileImage"),
    //            data: formdata,
    //            contentType: false,
    //            processData: false,
    //            //dataType: "json", // Adjust based on expected response type
    //            success: function (response) {

    //                Swal.fire({
    //                    title: "Registration Successfully",
    //                    icon: "success"
    //                }).then((result) => {
    //                    if (result.isConfirmed) {
    //                        // Handle successful registration, e.g., redirect to login page
    //                        window.location.href = 'https://localhost:7235/';
    //                    }
    //                });

    //                $(".preloader").hide();
    //            },
    //            error: function () {
    //                // Handle error if needed
    //                $(".preloader").hide();
    //            }
    //        });
    //    }
    //}



    this.LoginGet=function () {
        debugger;
        $(".preloader").show();
      
        $.ajax({
            type: "Get",
            url: UrlContent("Home/Login"),

            success: function (response) {

                $("#modalContent").html(response);
                //$.validator.unobtrusive.parse($("#UserDetails"));
                $("#modalShow").modal('show');
                $(".preloader").hide();

            },
            error: function () {
                // Handle error if needed
                $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
            }
        });
    }
    this.encryptPwdWithoutUsername = function () {
        debugger;
        if (document.getElementById("UserPassword") != null) {
            var txtPassword = document.getElementById("UserPassword").value.trim();
            if (txtPassword === "") {
                return false;
            }
            else {
                var key = CryptoJS.enc.Utf8.parse('8080808080808080');
                var iv = CryptoJS.enc.Utf8.parse('8080808080808080');
                var encryptedpassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(txtPassword), key,
                    {
                        keySize: 128 / 8,
                        iv: iv,
                        mode: CryptoJS.mode.CBC,
                        padding: CryptoJS.pad.Pkcs7
                    });
                document.getElementById("UserPassword").value = encryptedpassword;
                //let confPass = document.getElementById("ConfirmPassword");
                //if (confPass != null && typeof confPass != "undefined") {
                //    confPass.value = encryptedpassword;
                //}
                return true;
            }
        }
    }
    this.LoginPost = function () {
        TennisCourtBookingApp.Common.encryptPwdWithoutUsername();
        //if ($("#UserDetails").valid()) {
        $(".preloader").show();
        var formdata = $("#UserLogin").serialize();
        $.ajax({
            type: "Post",
            url: UrlContent("Home/Login"),
            data: formdata,
            dataType: "text",
            success: function (response) {
                var name = response;
                debugger;
                console.log(name);
                if (response=="1") {
                    var url = 'https://localhost:7235/TennisCourt'
                    window.location.href = url;
                }
                else if (response=="2") {

                    var url = 'https://localhost:7235/User/UserDetails?userId=response.userId'
                    window.location.href = url;
                }
                else {
                    TennisCourtBookingApp.Common.LoginGet();
                }
                $(".preloader").hide();

            },
            error: function () {
                // Handle error if needed
                $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
            }
        });
        //}
    }
    this.CaptchaCheck = function () {
        debugger;
        $(".preloader").show();
        var formdata = $("#UserLogin").serialize();
        $.ajax({
            type: "Post",
            url: UrlContent("Home/CaptchaCheck"),
            data: formdata,
            dataType: "text",
            success: function (response) {
                var name = response;
                debugger;
                console.log(name);
                if (response == "1") {
                    TennisCourtBookingApp.Common.LoginPost();
                }
                else if (response == "2") {
                    TennisCourtBookingApp.Common.LoginGet();
                   
                }
               
                $(".preloader").hide();

            },
            error: function () {
                // Handle error if needed
                $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
            }
        });
        //}
    }
    this.SignUpPost = function () {
        debugger;
        if ($("#UserDetails").valid()) {
            TennisCourtBookingApp.Common.encryptPwdWithoutUsername();
            var fileUpload = $("#SaveImage").get(0);
            var files = fileUpload.files;
            var formdata = new FormData();
            for (var i = 0; i < files.length; i++) {
                formdata.append("Image", files[i]);
            }
            var form_data = $('#UserDetails').serializeArray();
            $.each(form_data, function (key, input) {
                formdata.append(input.name, input.value);
            });
            console.log(formdata)
            $.ajax({
                type: "Post",
                url: UrlContent("Home/SignUp/"),
                data: formdata,
                contentType: false,
                processData: false,
                dataType: 'json',
                success: function (response) {
                    debugger;
                    Swal.fire({
                        title: "Registration  Successfully",
                        icon: "Success"
                    }).then((result) => {
                        if (result.isConfirmed) {

                        }
                        TennisCourtBookingApp.Common.LoginGet();
                        //var url = 'https://localhost:7235/Home/Login';
                        //window.location.href = url;
                        //location.replace(url);
                        //window.location.reload();
                    });
                    //$("#modalContent").html(response);
                    //$("#modalShow").modal('show');
                    ////var url = 'https://localhost:7235/Home/Login'
                    ////window.location.href = url;
                    //window.location.reload();
                    $(".preloader").hide();

                },
                error: function () {
                    // Handle error if needed
                    $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
                }
            });
        }
    }

    this.previewImage=function (input) {
        var file = input.files[0];
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imagePreview').attr('src', e.target.result).show();
        };

        reader.readAsDataURL(file);
    }

    //this.triggerFileInput=function () {
    //    $('#SaveImage').click();
    //}

    this.ResetPasswordGet = function () {

        $(".preloader").show();
        $.ajax({
            type: "Get",
            url: UrlContent("Home/ResetPassword"),

            success: function (response) {

                $("#modalContent").html(response);
                //$.validator.unobtrusive.parse($("#UserDetails"));
                $("#modalShow").modal('show');
                $(".preloader").hide();

            },
            error: function () {
                // Handle error if needed
                $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
            }
        });
    }

    this.SubmitEmail = function () {
        debugger;
        //TennisCourtBookingApp.Common.encryptPwdWithoutUsername();
        //if ($("#UserDetails").valid()) {
        //$("#smtButton").show();
        //$(".preloader").hide();

        //TennisCourtBookingApp.Common.encryptPwdWithoutUsername();
        //if ($("#UserDetails").valid()) {
        // Show the preloader
        $("#smtButton").hide();
        $(".preloader").show();
  
        var email = $("#UserEmail").val();
        $.ajax({
            type: "Post",
            url: UrlContent("Home/ResetPassword"),
            data: { email: email },
            dataType: "text",
            success: function (response) {

                Swal.fire({
                    title: response,
                    icon: "Success"
                }).then((result) => {
                    if (result.isConfirmed) {

                    }
                    var url = 'https://localhost:7235/Home/Index';
                    window.location.href = url;
                    //location.replace(url);
                    //window.location.reload();
                });
               
                $(".preloader").hide();

            },
            error: function () {
                // Handle error if needed
                $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
            }
        });
        //}
    }
    this.ChangePasswordPost = function () {
        debugger;
       
        //if ($("#UserDetails").valid()) {
        $(".preloader").show();
        var formdata = $("#ChangePassword").serialize();
        $.ajax({
            type: "Post",
            url: UrlContent("Home/ChangePassword"),
            data: formdata,
            dataType: "text",
            success: function (response) {
                debugger;
                //response = response.trim();
                console.log(response);
                if (response == "true") { 
                Swal.fire({
                    title: "Password Change  Successfully",
                    icon: "Success"
                }).then((result) => {
                    if (result.isConfirmed) {

                    }
                    var url = 'https://localhost:7235/Home/Index';
                    window.location.href = url;
                    //location.replace(url);
                    //window.location.reload();
                });
                }
               else if (response == "false") {
                    Swal.fire({
                        title: "Passoword Not Change",
                        icon: "Error"
                    }).then((result) => {
                        if (result.isConfirmed) {

                        }
                        var url = 'https://localhost:7235/Home/Index';
                        window.location.href = url;
                        //location.replace(url);
                        //window.location.reload();
                    });
                }
                //$("#modalContent").html(response);
                //$("#modalShow").modal('show');
                //if (response.userId == null) {
                //    var url = 'https://localhost:7235/TennisCourt'
                //    window.location.href = url;
                //}
                //else {


                //    var url = 'https://localhost:7235/User/UserDetails?userId=response.userId'
                //    window.location.href = url;
                //}
                $(".preloader").hide();

            },
            error: function () {
                // Handle error if needed
                $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
            }
        });
        //}
       
    }
    this.RefreshCaptcha = function () {
        $.ajax({
            url: UrlContent("Home/RefreshCaptcha"),
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                $(".imgCaptcha").attr("src", data);
            }
        });
    }

    this.isNumber = function (evt) {
        debugger;
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if ((charCode > 31 && charCode < 48) || charCode > 57) {
            return false;
        }
        return true;
    }

}


