

//function ChangedStatus(selectStatus) {
//    debugger;
//    // Access the selected value using selectElement.value
//    var selectedValue = selectStatus.value;
//    TennisCourtBookingApp.User.GetUserBooking(selectedValue);
//    //console.log("Selected value: " + selectedValue);
//    // Perform any additional actions based on the selected value

//}

TennisCourtBookingApp.User = new function () {


    $(document).ready(function () {
        TennisCourtBookingApp.User.GetCourtListUser();
        TennisCourtBookingApp.User.GetUserBooking(1);
    });
    this.ChangedStatus=function(selectStatus) {
        debugger;
        // Access the selected value using selectElement.value
        var selectedValue = selectStatus.value;
        TennisCourtBookingApp.User.GetUserBooking(selectedValue);
        //console.log("Selected value: " + selectedValue);
        // Perform any additional actions based on the selected value

    }
    this.UserProfile = function () {
        $(".preloader").show();
        $.ajax({
            type: "Get",
            url: UrlContent("User/UserProfile"),
            success: function (response) {

                $("#modalContent").html(response);
                $("#modalShow").modal('show');
                //$.validator.unobtrusive.parse($("#UserDetails"));
                $(".preloader").hide();

            },
            error: function () {
                // Handle error if needed
                $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
            }
        });

    }
    this.UserEditGet = function (userId) {

        $(".preloader").show();
        $.ajax({
            type: "Get",
            url: UrlContent("User/UserEdit"),
            data: {
                userId: userId
            },
            success: function (response) {

                $("#modalContent").html(response);
                $("#modalShow").modal('show');
                $.validator.unobtrusive.parse($("#UserDetails"));
                $(".preloader").hide();

            },
            error: function () {
                // Handle error if needed
                $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
            }
        });

    }


    this.UserEditPost = function () {
        debugger;
        if ($("#UserDetails").valid()) {
            $(".preloader").show();
            var formdata = $("#UserDetails").serialize();
            $.ajax({
                type: "Post",
                url: UrlContent("User/UserEdit"),
                data: formdata,
                dataType: "text",
                success: function (response) {


                    Swal.fire({
                        title: "Update Details Successfully",
                        icon: "Success"
                    }).then((result) => {
                        if (result.isConfirmed) {

                        }
                        //var url = 'https://localhost:7235/User/UserDetails?userId=response.userId';
                        //window.location.href = url;
                        TennisCourtBookingApp.User.UserProfile();
                    });

                    //$("#modalContent").html(response);
                    //$("#modalShow").modal('show');
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

    this.GetCourtListUser = function () {
       

        var dataTable = $('#CourtListUser').DataTable();
        var searchText = $('#UserCourtExtraSearch').val() ?? null;
        // Destroy the DataTable
        dataTable.destroy();

        // Now, you can reinitialize the DataTable with your desired options
        $('#CourtListUser').dataTable({


            searching: false,
            paging: true,
            serverSide: true,
            processing: true,
            bLengthChange: false,
            async: true,
            lengthMenu: [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "All"]],

            //dom: '<"top"flp>rt<"row btmpage"<"col-2 lgndrp"><"col-7"i><"col-3"p>>',
            pageLength: 10,
            ajax: {

                type: "Post",
                url: "/User/GetList",

                //dataType: 'Json'
                data: function (dtParms) {

                    debugger;
                    //dtParms.search.value = $("#" + Demo.Common.Option.SearchId).val();
                    dtParms.searchText = searchText;
                    return dtParms;
                },
            },

            columns: [

                {
                    "data": "tennisCourtName", "name": "TennisCourtName", "width": "400px",
                    "render": function (data) {
                        // Apply inline CSS to the cell
                        return '<div style="color: Black; font-weight: bold;">' + data + '</div>';
                    }

                },

                {
                    "data": "tennisCourtAddress", "name": "TennisCourtAddress", "width": "200px",
                    "render": function (data) {
                        // Apply inline CSS to the cell
                        return '<div style="color: Black; font-weight: bold;">' + data + '</div>';
                    }
                },

                {
                    "data": "tennisCourtCapacity", "name": "TennisCourtCapacity", "width": "100px",
                    "render": function (data) {
                        // Apply inline CSS to the cell
                        return '<div style="color: Black; font-weight: bold;">' + data + '</div>';
                    }
                },



                {
                    "data": 'tennisCourtId', "className": "text-center", "width": "200px", orderable: false,
                    "render": function (data, type, row) {
                        debugger;
                        let btnBooking = '<button title="Book Slot" class="btn btn-success btn-sm mt-1 mr-3" style=" margin-right: 2px;" onclick="TennisCourtBookingApp.Booking.CourtBookingGet(\'' + data + '\')">Book Slot</button>';




                        return btnBooking;
                    }
                }
            ],
            order: [0, "ASC"]
        });
    }

    this.triggerFileInput = function () {
        $('#UpdateImage').click();
    }
    this.UpdateUserImage = function () {

        var fileUpload = $("#UpdateImage").get(0);
        var files = fileUpload.files;
        var formdata = new FormData();
        for (var i = 0; i < files.length; i++) {
            formdata.append("Image", files[i]);
        }
        $.ajax({
            type: "Post",
            url: UrlContent("Home/SignUp/"),
            data: formdata,
            contentType: false,
            processData: false,
            dataType: 'json',
            success: function (response) {
                //window.location.reload();
                TennisCourtBookingApp.User.UserProfile();
                $(".preloader").hide();

            },
            error: function () {
                // Handle error if needed
                $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
            }
        });

    }


    this.GetUserBooking = function (status) {
        
        var dataTable = $('#UserBookings').DataTable();
        var searchText = $('#UserBookingExtraSearch').val();
        var status = $('#Status').val() ?? status;
        // Destroy the DataTable
        dataTable.destroy();

        // Now, you can reinitialize the DataTable with your desired options
        $('#UserBookings').DataTable({


            searching: false,
            paging: true,
            serverSide: true,
            processing: true,
            bLengthChange: false,
            async: true,
            lengthMenu: [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "All"]],


            //dom: '<"top"flp>rt<"row btmpage"<"col-2 lgndrp"><"col-7"i><"col-3"p>>',
            pageLength: 10,
            ajax: {

                type: "Post",
                url: "/User/GetUserBooking",

                //dataType: 'Json'
                data: function (dtParms) {

                  
                    //dtParms.search.value = $("#" + Demo.Common.Option.SearchId).val(); 
                    dtParms.searchText = searchText;
                    dtParms.status = status;
                    return dtParms;
                },
            },

            columns: [

                {
                    "data": "tennisCourtName", "name": "TennisCourtName", "autoWidth": true,
                    "render": function (data) {
                        // Apply inline CSS to the cell
                        return '<div style="color: Black; font-weight: bold;">' + data + '</div>';
                    }

                },

                {
                    "data": "tennisCourtAddress", "name": "TennisCourtAddress", "autoWidth": true,
                    "render": function (data) {
                        // Apply inline CSS to the cell
                        return '<div style="color: Black; font-weight: bold;">' + data + '</div>';
                    }
                },

                {
                    "data": "bookingDate", "name": "BookingDate", "autoWidth": true,
                    "render": function (data) {
                        // Apply inline CSS to the cell
                        if (data) {
                            var formattedDate = new Date(data).toLocaleDateString("en-US", { month: '2-digit', day: '2-digit', year: 'numeric' });

                            // Apply inline CSS to the cell
                            return '<div style="color: Black; font-weight: bold;">' + formattedDate + '</div>';
                        }
                    }
                },
                {
                    "data": "bookingTime", "name": "BookingTime", "autoWidth": true,
                    "render": function (data) {
                        var formattedTime = new Date('1970-01-01T' + data).toLocaleTimeString("en-US", { hour: '2-digit', minute: '2-digit', hour12: true });
                        //var time = DateTime.Today.Add(Data).ToString("hh:mm tt");
                        // Apply inline CSS to the cell
                        return '<div style="color: Black; font-weight: bold;">' + formattedTime + '</div>';
                    }
                },
                {
                    "data": "status", "name": "Status", "autoWidth": true,
                    //"render": function (data) {
                    //    // Apply inline CSS to the cell
                    //    return '<div style="color: Black; font-weight: bold;">' + data + '</div>';
                    //}
                    "render": function (data) {
                  
                        // Conditionally apply classes based on the value of isActive
                        if (data === 2) {
                            return '<span class="badge badge-warning">Pending</span>';
                        } else if (data === 3) {
                            return '<span class="badge badge-danger">Rejected</span>';
                        }
                        else if (data === 4) {
                            return '<span class="badge badge-success">Confirm</span>';
                        }
                    }
                },



                {
                    "data": 'bookingId', "className": "text-center", "width": "300px", orderable: false,
                    "render": function (data, type, row) {

                        if (row.status == 2)
                        {
                            //    //let btnConfirm = '<button title="Book Slot" class="btn btn-success btn-sm mt-1 mr-3" style=" margin-right: 2px;" onclick="TennisCourtBookingApp.Booking.EditBookingStatus(\'' + 'Confirm' + data + '\')">Confirm</button>';
                            let btnEdit = '<button title="Edit" class="btn btn-success btn-sm mt-1 mr-3" style="margin-right: 2px;" onclick="TennisCourtBookingApp.Booking.EditCourtBooking( \'' + data + '\')"><i class="fa-solid fa-pen-to-square"></i></button>';
                            //    //let btnReject = '<button title="Book Slot" class="btn btn-success btn-sm mt-1 mr-3" style=" margin-right: 2px;" onclick="TennisCourtBookingApp.Booking.EditBookingStatus(\'' + 'Reject' + data + '\')">Reject</button>';
                            let btnCancel = '<button title="Cancel" class="btn btn-danger btn-sm mt-1 mr-3" style="margin-right: 2px;" onclick="TennisCourtBookingApp.Booking.DeleteCourtBookingPost( \'' + data + '\')"><i class="fa-solid fa-xmark"></i></button>';



                            //return btnConfirm + btnReject;
                            return btnEdit + btnCancel;
                        }
                        else if(row.status ==4)
                        {
                            
                            let btnCancel = '<button title="Cancel" class="btn btn-danger btn-sm mt-1 mr-3" style="margin-right: 2px;" onclick="TennisCourtBookingApp.Booking.DeleteCourtBookingPost( \'' + data + '\')"><i class="fa-solid fa-xmark"></i></button>';
                            return btnCancel;

                        }
                        else if (row.status==3)
                        {
                            return null;
                        }
                    }
                },
                {
                    "data": 'bookingId', "className": "text-center", "width": "300px", orderable: false,
                    "render": function (data, type, row) {
                        let btnDownload = '<button title="Download" class="btn btn-success btn-sm mt-1 mr-3" style="margin-right: 2px;" onclick="TennisCourtBookingApp.User.DownloadTicket( \'' + data + '\')"><i class="fa-solid fa-pen-to-square"></i></button>';
                        return btnDownload;
                    }
                }
            ],
            order: [0, "ASC"],        
            
        });       

    }
    //this.DownloadTicket = function (id) {
    //    debugger;
    //    $(".preloader").show();
    //    $.ajax({
    //        type: "POST",
    //        url: UrlContent("User/DownloadTicket"),
    //        data: { Id: id },
    //        success: function (result) {
              
               
    //            window.open(result, "_blank");
    //            $(".preloader").hide();
                
    //        },
    //        error: function (textStatus, errorThrown) {
    //        }
    //    })
    //}

    this.DownloadTicket = function (id) {
       
        $(".loading-screen").show();
        $.ajax({
            url: UrlContent("User/Download"),
            success: function (response) {
                debugger;
                
                window.location.href = UrlContent("User/DownloadPdf?documentName=" + response);
                
            }
        });
    }


    this.DownloadReportPdf = function (id) {
        $("#progress").show();
        setTimeout(function () {
            $("#progress").hide();
        }, 5000);
        window.location.href = UrlContent("User/UserDetailsPDF/");
        /*$.ajax({
            url: UrlContent("Maintenance/GeneratePDF/" + id),
            method: "GET",
            success: function (data) {
                debugger;
                $("#progress").hide();
            },
            error: function () {
                $("#progress").hide();
                console.error("Error during PDF generation");
            }
        });*/
    }

}