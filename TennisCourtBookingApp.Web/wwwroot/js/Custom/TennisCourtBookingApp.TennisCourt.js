
TennisCourtBookingApp.TennisCourt = new function () {

    this.GetBookingListUser = function (status) {
       
        var dataTable = $('#AllUserBookings').DataTable();
        var searchText = $('#BookingExtraSearch').val() ?? null;
        var status = $('#Status').val() ?? status;
        // Destroy the DataTable
        dataTable.destroy();

        // Now, you can reinitialize the DataTable with your desired options
        $('#AllUserBookings').dataTable({


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
                url: "/TennisCourt/UserBookingDetails",

                //dataType: 'Json'
                data: function (dtParms) {

                  
                    //dtParms.search.value = $("#" + Demo.Common.Option.SearchId).val();
                    dtParms.status = status;
                    dtParms.searchText = searchText;


                    return dtParms;
                },
            },

            columns: [

                {
                    "data": "userName", "name": "UserName", "autoWidth": true,
                    "render": function (data) {
                        // Apply inline CSS to the cell
                        return '<div style="color: Black; font-weight: bold;">' + data + '</div>';
                    }
                },

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
                    "data": "bookingDate",
                    "name": "BookingDate",
                    "width": "200px",
                    "render": function (data) {
                        // Check if data is not null before formatting
                        if (data) {
                            var formattedDate = new Date(data).toLocaleDateString("en-US", { month: '2-digit', day: '2-digit', year: 'numeric' });

                            // Apply inline CSS to the cell
                            return '<div style="color: Black; font-weight: bold;">' + formattedDate + '</div>';
                        } else {
                            return '';  // Handle null or undefined data if needed
                        }
                    }
                },

                {
                    "data": "bookingTime", "name": "BookingTime", "autoWidth": true,
                    "render": function (data) {
                        var formattedTime = new Date('1970-01-01T' + data).toLocaleTimeString("en-US", { hour: '2-digit', minute: '2-digit', hour12: true });
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
                        debugger;
                        if (row.status == 2) {
                            //    //let btnConfirm = '<button title="Book Slot" class="btn btn-success btn-sm mt-1 mr-3" style=" margin-right: 2px;" onclick="TennisCourtBookingApp.Booking.EditBookingStatus(\'' + 'Confirm' + data + '\')">Confirm</button>';
                            let btnConfirm = '<button title="Confirm" class="btn btn-success btn-sm mt-1 mr-3" style="margin-right: 2px;" onclick="TennisCourtBookingApp.Booking.EditBookingStatus(\'' + 4 + '\', \'' + data + '\')"><i class="fa-solid fa-square-check"></i> Confirm</button>';
                            //    //let btnReject = '<button title="Book Slot" class="btn btn-success btn-sm mt-1 mr-3" style=" margin-right: 2px;" onclick="TennisCourtBookingApp.Booking.EditBookingStatus(\'' + 'Reject' + data + '\')">Reject</button>';
                            let btnReject = '<button title="Reject" class="btn btn-danger btn-sm mt-1 mr-3" style="margin-right: 2px;" onclick="TennisCourtBookingApp.Booking.EditBookingStatus(\'' + 3 + '\', \'' + data + '\')"><i class="fa-solid fa-xmark"></i> Reject</button>';



                            //return btnConfirm + btnReject;
                            return btnConfirm + btnReject;
                        }
                        else {

                            return null;
                        }
                    }
                }
            ],
            order: [0, "ASC"],
        });

    }

    this.TennisCourtOption = {
        Table: null,
        TableId: "",
        RoleId: 0

    }
    this.GetCourtList = function (options) {
        TennisCourtBookingApp.TennisCourt.TennisCourtOption = $.extend({}, TennisCourtBookingApp.TennisCourt.TennisCourtOption, options);
        var dataTable = $('#CourtList').DataTable();
        var searchText = $("#CourtExtraSearch").val() ?? null;
        //var length = $('#Court_length').val();
        //dataTable.page.len(length).draw();
        // Destroy the DataTable
        dataTable.destroy();
        TennisCourtBookingApp.TennisCourt.TennisCourtOption.Table = $('#CourtList').DataTable({
            searching: false,
            paging: true,
            serverSide: true,
            processing: true,
            bLengthChange: false,
            async: true,
            //dom: '<"top"flp>rt<"row btmpage"<"col-2 lgndrp"><"col-7"i><"col-3"p>>',
            pageLength: 10,
            ajax: {

                type: "Post",
                url: "/TennisCourt/GetList",

                //dataType: 'Json'
                data: function (dtParms) {
                    //dtParms.search.value = $("#" + Demo.Common.Option.SearchId).val();
                    dtParms.searchText = searchText;
                    //dtParms.length = parseInt(length,10);
                    //existingElement.id += length;
                    return dtParms;
                },

            },
            //drawCallback: function (settings) {
            //    var api = this.api();
            //    var pageInfo = api.page.info();

            //    // Update display of total and filtered records
            //    $('#totalRecords').text(pageInfo.recordsTotal);
            //    $('#filteredRecords').text(pageInfo.recordsDisplay);
            //},

            columns: [

                {
                    "data": "tennisCourtName", "name": "TennisCourtName", "width": "300px",
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
                    "data": "tennisCourtCapacity", "name": "TennisCourtCapacity", "width": "200px",
                    "render": function (data) {
                        // Apply inline CSS to the cell
                        return '<div style="color: Black; font-weight: bold;">' + data + '</div>';
                    }
                },
                {
                    "data": 'tennisCourtId', "className": "text-center", "width": "400px", orderable: false,
                    "render": function (data, type, row) {

                        let btnDelete = '<button title="Delete" class="btn btn-danger btn-sm mt-1 mr-3" style=" margin-right: 2px;" onclick="TennisCourtBookingApp.TennisCourt.CourtDeleteGet(\'' + data + '\')"><i class="fa-solid fa-trash"></i></button>';

                        let btnEdit = '<button title="Edit" class="btn btn-primary btn-sm  mt-1 mr-3" style="  margin-right: 2px;" onclick="TennisCourtBookingApp.TennisCourt.CourtEditGet(\'' + data + '\')"><i class="fa-solid fa-pen-to-square"></i></button>';

                        //let btnShowCourse = '<button title="ShowCourse" class="btn btn-primary btn-sm  mt-1 mr-3" style="  margin-right: 2px;" onclick="ShowCourses(\'' + data + '\')">Show Course</button>';
                        return btnEdit + btnDelete;
                    }
                }
            ]

        });
        //$('#Court_length').on('change', function () {
        //    customLength = parseInt($(this).val(), 10);
        //    dataTable.page.len(customLength).draw(); // Update DataTables length
        //});
        $('#CourtList_filter ').css({
            'float': 'left',
            'margin-left': '10px',
            'padding-right': '20px'
        });

        $('#CourtList_length').css({
            'float': 'right'
        });
    }
    this.Search = function () {
        TennisCourtBookingApp.TennisCourt.TennisCourtOption.Table.reload();
    }
    this.AddCourt = function () {

        $(".preloader").show();
        $.ajax({
            type: "Get",
            url: UrlContent("TennisCourt/AddCourt"),

            success: function (response) {

                $("#modalContent").html(response);

                $("#modalShow").modal('show');
                $.validator.unobtrusive.parse($("#TennisCourt"));
                $(".preloader").hide();

            },
            error: function () {
                // Handle error if needed
                $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
            }
        });
    }



    this.AddCourtPost = function () {
        if ($("#TennisCourt").valid()) {
            $(".preloader").show();
            var formdata = $("#TennisCourt").serialize();
            $.ajax({
                type: "Post",
                url: UrlContent("TennisCourt/AddCourt"),
                data: formdata,
                dataType: "text",
                success: function (response) {


                    Swal.fire({
                        title: "Tennis Court Added Successfully",
                        icon: "Success"
                    }).then((result) => {
                        if (result.isConfirmed) {

                        }
                        var url = 'https://localhost:7235/TennisCourt';
                        window.location.href = url;
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




    this.CourtEditGet = function (courtId) {

        $(".preloader").show();
        $.ajax({
            type: "Get",
            url: UrlContent("TennisCourt/TennisCourtEdit"),
            data: {
                courtId: courtId
            },
            success: function (response) {

                $("#modalContent").html(response);
                $("#modalShow").modal('show');
                $.validator.unobtrusive.parse($("#TennisCourt"));
                $(".preloader").hide();

            },
            error: function () {
                // Handle error if needed
                $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
            }
        });

    }



    this.CourtEditPost = function () {
        if ($("#TennisCourt").valid()) {
            $(".preloader").show();

            var formdata = $("#TennisCourt").serialize();
            $.ajax({
                type: "Post",
                url: UrlContent("TennisCourt/TennisCourtEdit"),
                data: formdata,
                dataType: "text",
                success: function (response) {


                    Swal.fire({
                        title: "Tennis Court Update Successfully",
                        icon: "Success"
                    }).then((result) => {
                        if (result.isConfirmed) {

                        }
                        var url = 'https://localhost:7235/TennisCourt';
                        window.location.href = url;
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




    this.CourtDeleteGet = function (courtId) {
        $(".preloader").show();
        $.ajax({
            type: "Get",
            url: UrlContent("TennisCourt/TennisCourtDelete"),
            data: {
                courtId: courtId
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

    this.CourtDeletePost = function () {
        debugger;
        $(".preloader").show();
        var formdata = $("#TennisCourt").serialize();
        $.ajax({
            type: "Post",
            url: UrlContent("TennisCourt/TennisCourtDelete"),
            data: formdata,
            dataType: "text",
            success: function (response) {

                Swal.fire({
                    title: "Tennis Court Delete Successfully",
                    icon: "error"
                }).then((result) => {
                    if (result.isConfirmed) {

                    }
                    var url = 'https://localhost:7235/TennisCourt';
                    window.location.href = url;
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

    this.PreviousBookings = function () {
       
        $(".preloader").show();
        $.ajax({
            type: "Get",
            url: UrlContent("TennisCourt/PreviousBookings"),

            success: function (response) {
                window.location.href = "https://localhost:7235/TennisCourt/PreviousBookings";
            },
            error: function () {
                // Handle error if needed
                $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
            }
        });
    }


    this.BulkBookingDetailDownload = function () {

        $(".preloader").show();

        $.ajax({
            type: "POST",
            url: UrlContent("TennisCourt/BulkBookingDownload"),
            success: function (result) {
                $("#progress").hide();
                console.log(result)
                debugger;
                if (result.result) {
                    debugger;
                    window.location = result.message;

                }
/*                window.location.href = "https://localhost:7235/TennisCourt/PreviousBookings";*/
            },
            dataType: "json",
            traditional: true
        });


    }

   


    //this.ExtraSearch = function () {
    //    debugger;
    //    $(".preloader").show();
    //    var searchText = $("#CourtExtraSearch").val();
    //    $.ajax({
    //        type: "Post",
    //        url: UrlContent("TennisCourt/ExtraSearch"),
    //        data: {
    //            searchText: searchText
    //        },
    //        dataType: "text",
    //        success: function (response) {

    //            TennisCourtBookingApp.TennisCourt.GetCourtList(response);
    //            //$("#modalContent").html(response);

    //            //$("#modalShow").modal('show');
    //            //$.validator.unobtrusive.parse($("#TennisCourt"));
    //            $(".preloader").hide();

    //        },
    //        error: function () {
    //            // Handle error if needed
    //            $(".preloader").hide(); // Ensure the preloader is hidden in case of an error
    //        }
    //    });
    //}

}


