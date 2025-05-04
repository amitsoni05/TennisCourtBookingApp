TennisCourtBookingApp.Booking = new function () {
    this.CourtBookingGet=function (courtId) {
        
        $(".preloader").show();
        $.ajax({
            type: "Get",
            url: UrlContent("User/BookCourt"),
            data: {
                courtId: courtId
            },

            success: function (response) {
                
                $("#modalContent").html(response);
                $.validator.unobtrusive.parse($("#BookTennisCourt"));
                $("#modalShow").modal('show');
                $(".preloader").hide();

            },
            error: function () {
                // Handle error if needed
                $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
            }
        });
    }

   

   


    this.addItemToTempData = function () {
      
        if ($("#BookTennisCourt").valid()) {
            var item = $("#BookTennisCourt").serialize();
            $.ajax({
                type: "Get",
                url: UrlContent("User/AddBookingInTempData"),
                data: item,
                dataType: "text",
                success: function (data) {
                    debugger;
                    if (data.Message === "false") { 
                    Swal.fire({
                        title: "Slot Not Available",
                        icon: "error"
                    }).then((result) => {
                        if (result.isConfirmed) {

                        }
                       
                    });
                        
                    }
                    console.log(data.Message)
                        window.location.reload();
                    
                },
                error: function () {
                    debugger;
                    // Handle error if needed
                }
            });
        }
    }
      this.DeleteFromTemp= function (id, bookingDate, bookingTime) {
       
        $(".preloader").show();
        //var formdata = $("#BookTennisCourt").serialize();
        $.ajax({
            type: "Post",
            url: UrlContent("User/DeleteFromTempData"),
            data: {
                id: id, bookingDate: bookingDate, bookingTime: bookingTime
            },
            dataType: "text",
            success: function (response) {

               
                //$("#modalContent").html(response);
                //$("#modalShow").modal('show');
                //window.location.href = 'UserDetails';
                window.location.reload();
                $(".preloader").hide();

            },
            error: function () {
                // Handle error if needed
                $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
            }
        });
      }

    this.BookCourtPost = function () {
      
        $(".preloader").show();
        //var formdata = $("#BookTennisCourt").serialize();
        $.ajax({
            type: "Post",
            url: UrlContent("User/SubmitBookingsToDatabase"),
            //data: formdata,
            dataType: "text",
            success: function (response) {

            
                Swal.fire({
                    title: "Booking Successfully",
                    icon: "Success"
                }).then((result) => {
                    if (result.isConfirmed) {

                    }
                    var url = 'https://localhost:7235/User/UserDetails?userId=response.userId';
                    window.location.href = url;
                });

                //$("#modalContent").html(response);
                //$("#modalShow").modal('show');
                //window.location.href = 'UserDetails';
                //window.location.reload();
                $(".preloader").hide();

            },
            error: function () {
                // Handle error if needed
                $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
            }
        });
    }

    this.EditCourtBooking = function (bookingId) {
       
        $(".preloader").show();
        $.ajax({
            type: "Get",
            url: UrlContent("User/EditBooking"),
            data: {
                bookingId: bookingId
            },

            success: function (response) {
            
                $("#modalContent").html(response);

                $("#modalShow").modal('show');
                $(".preloader").hide();

            },
            error: function () {
                // Handle error if needed
                $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
            }
        });
    }

    this.EditBookingPost = function () {
        debugger;
        $(".preloader").show();
        var formdata = $("#BookTennisCourt").serialize();
        $.ajax({
            type: "Post",
            url: UrlContent("User/EditBooking"),
            data: formdata,
            dataType: "text",
            success: function (response) {
                debugger;
                console.log(response);
                if (response === "Success") {
                    Swal.fire({
                        title: response,
                        icon: "Success"
                    }).then((result) => {
                        if (result.isConfirmed) {
                          
                        }
                       
                    });
                    var url = 'https://localhost:7235/User/UserDetails';
                    window.location.href = url;
                }
                else {
                    Swal.fire({
                        title: response,
                        icon: "error"
                    }).then((result) => {
                        if (result.isConfirmed) {

                        }
                        var url = 'https://localhost:7235/User/UserDetails';
                        window.location.href = url;
                    });
                }
                //$("#modalContent").html(response);
                //$("#modalShow").modal('show');
                //window.location.href = 'UserDetails';
                //window.location.reload();
                $(".preloader").hide();

            },
            error: function () {
                // Handle error if needed
                $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
            }
        });
       }


    this.DeleteCourtBookingPost = function (bookingId) {
        debugger;
        $(".preloader").show();
        //var formdata = $("#TennisCourt").serialize();
        $.ajax({
            type: "Post",
            url: UrlContent("User/DeleteCourtBooking"),
            data: {
                bookingId: bookingId
            },
            dataType: "text",
            success: function (response) {
                Swal.fire({
                    title: "Booking Cancel Successfully",
                    icon: "errorr"
                }).then((result) => {
                    if (result.isConfirmed) {

                    }
                    var url = 'https://localhost:7235/User/UserDetails';
                    window.location.href = url;
                });
                //$("#modalContent").html(response);
                //$("#modalShow").modal('show');
                
                $(".preloader").hide();

            },
            error: function () {
                // Handle error if needed
                $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
            }
        });
    }


    this.EditBookingStatus = function (Status, BookingId) {
        
        $(".preloader").show();
        $.ajax({
            type: "Post",
            url: UrlContent("User/EditBookingStatus"),
            data: {
                Status: Status,
                BookingId: BookingId,
            },
            success: function (response) {
                debugger;
                if (response.confirmation == 3) {
                    Swal.fire({
                        title: "Booking Reject Successfully",
                        icon: "error"
                    }).then((result) => {
                        if (result.isConfirmed) {

                        }
                        window.location.href = 'BookingDetails';
                    });
                }
                else {
                    Swal.fire({
                        title: "Booking Confirm Successfully",
                        icon: "error"
                    }).then((result) => {
                        if (result.isConfirmed) {

                        }
                        window.location.href = 'BookingDetails';
                    });
                }
            
        
                //$("#modalContent").html(response);
                //$("#modalShow").modal('show');
               
                //Window.location.reload();
                $(".preloader").hide();

            },
            error: function () {
                // Handle error if needed
                $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
            }
        });

    }



}

