using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TennisCourtBookingApp.Provider.IProvider;
using TennisCourtBookingApp.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Reporting.WebForms;
using TennisCourtBookingApp.Repository.ADO;
using System.Data;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Font;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using static iText.Svg.SvgConstants;
//using PdfSharp.Pdf;
//using PdfSharp.Drawing;


namespace TennisCourtBookingApp.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly ICommonProvider _commonProvider;
        private readonly IUserProvider _userProvider;
        private readonly ITennisCourtProvider _tennisCourtProvider;
        private readonly IBookingProvider _bookingProvider;
        private readonly IWebHostEnvironment _webHostEnvironment;
      

        public UserController(ICommonProvider commonProvider, IUserProvider userProvider, ITennisCourtProvider tennisCourtProvider, IBookingProvider bookingProvider, IWebHostEnvironment webHostEnvironment) : base(commonProvider)
        {
            _commonProvider = commonProvider;
            _userProvider = userProvider;
            _tennisCourtProvider = tennisCourtProvider;
            _bookingProvider = bookingProvider;
            _webHostEnvironment = webHostEnvironment;
           
        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult UserDetails(int? userId)
        {
            TennisCourtBookingViewModel viewModel = new TennisCourtBookingViewModel();
            if (HttpContext.Session.GetString("UserSession") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            userId = HttpContext.Session.GetInt32("UserId");

            viewModel.TennisCourtBookingUserModel = _userProvider.GetUserById(userId);
            //viewModel.TennisCourtBookingModels = _bookingProvider.GetBookCourtDetails(userId);
            //viewModel.TennisCourtModels = _tennisCourtProvider.GetTennisCourtList();
            //TempData.Remove("List_items" + userId);

            return View(viewModel);
        }




        [HttpPost]
        public JsonResult GetUserBooking(int status, string searchText)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            var viewModel = _userProvider.GetUserBooking(GetPagingRequestModel(), userId, status, searchText);
            return Json(viewModel);
        }

        [HttpGet]
        public IActionResult UserProfile()
        {
            TennisCourtBookingViewModel viewModel = new TennisCourtBookingViewModel();
            if (HttpContext.Session.GetString("UserSession") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int? userId = HttpContext.Session.GetInt32("UserId");
            viewModel.TennisCourtBookingUserModel = _userProvider.GetUserById(userId);

            return PartialView("_UserProfile", viewModel);
        }


        public IActionResult UserDetailsPDF()
        {
            TennisCourtBookingViewModel viewModel = new TennisCourtBookingViewModel();
            int? userId = HttpContext.Session.GetInt32("UserId");
            viewModel.TennisCourtBookingUserModel = _userProvider.GetUserById(userId);

            viewModel.TennisCourtBookingUserModel.InvoiceAttachmentBase = "" + viewModel.TennisCourtBookingUserModel.InvoiceAttachmentBase;
            return new Rotativa.AspNetCore.ViewAsPdf("_UserProfile", viewModel)
            {
                FileName = "UserProfile_" + Guid.NewGuid().ToString() + ".pdf",
                CustomSwitches = "--print-media-type --viewport-size 1024x768",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.Letter,
                PageMargins = new Rotativa.AspNetCore.Options.Margins(7, 10, 7, 7),
            };
        }

        [HttpGet]
        public IActionResult UserEdit(int userId)
        {
            TennisCourtBookingViewModel model = new TennisCourtBookingViewModel();
            if (HttpContext.Session.GetString("UserSession") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            model.TennisCourtBookingUserModel = _userProvider.GetUserById(userId);
            model.IsEdit = true;
            model.IsUser = true;
            return PartialView("~/Views/Home/_UserPartial.cshtml", model);
            //return PartialView("_UserPartial", model);
        }

        [HttpPost]
        public IActionResult UserEdit(TennisCourtBookingViewModel model)
        {

            var userId = _userProvider.UpdateUser(model.TennisCourtBookingUserModel);
            //return RedirectToAction("UserDetails" , new {userId = userId});
            return Json(userId);
        }



        [HttpGet]
        public IActionResult ListedCourt()
        {
            if (HttpContext.Session.GetString("UserSession") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            TennisCourtBookingViewModel viewModel = new TennisCourtBookingViewModel();

            int? userId = HttpContext.Session.GetInt32("UserId");

            //viewModel.TennisCourtBookingUserModel = _userProvider.GetUserById(userId);
            //viewModel.TennisCourtBookingModels = _bookingProvider.GetBookCourtDetails(userId);
            //viewModel.TennisCourtModels = _tennisCourtProvider.GetTennisCourtList();
            viewModel.TempList = GetTempData(userId);
            //ViewBag.TempList = tempList;
            //TempData.Remove("SlotCheck");
            viewModel.tempDataKey = "SlotCheck" + userId;
            return View(viewModel);
        }

        [HttpPost]
        public JsonResult GetList(string searchText)
        {

            var model = GetPagingRequestModel();
            var viewModel = _tennisCourtProvider.GetList(model, searchText, 0);
            return Json(viewModel);
        }
        [HttpGet]

        public IActionResult BookCourt(int courtId)
        {
            if (HttpContext.Session.GetString("UserSession") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            TennisCourtBookingViewModel viewModel = new TennisCourtBookingViewModel();
            viewModel.TennisCourtModel = _tennisCourtProvider.GetCourtById(courtId);
            return PartialView("_BookingCourtPartial", viewModel);
        }

        [HttpGet]
        public IActionResult EditBooking(int bookingId)
        {
            if (HttpContext.Session.GetString("UserSession") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            TennisCourtBookingViewModel viewModel = new TennisCourtBookingViewModel();
            viewModel.TennisCourtBookingModel = _bookingProvider.GetBookingById(bookingId);
            int? courtId = viewModel.TennisCourtBookingModel.TennisCourtId;
            viewModel.TennisCourtModel = _tennisCourtProvider.GetCourtById(courtId);
            viewModel.IsEdit = true;

            return PartialView("_BookingCourtPartial", viewModel);
        }
        [HttpPost]
        public IActionResult EditBooking(TennisCourtBookingViewModel viewModel)
        {


            int? courtId = viewModel.TennisCourtBookingModel.TennisCourtId;
            int? userId = HttpContext.Session.GetInt32("UserId");
            var message = _bookingProvider.SaveBooking(viewModel.TennisCourtBookingModel, userId, courtId);

            return Json(message);
        }

        [HttpPost]
        public IActionResult DeleteCourtBooking(int bookingId)
        {
            _bookingProvider.DeleteBooking(bookingId);
            return RedirectToAction("UserDetails");
        }

        [HttpGet]

        public JsonResult AddBookingInTempData(TennisCourtBookingViewModel viewModel)
        {
            TennisCourtBookingViewModel model = new TennisCourtBookingViewModel();

            int? userId = HttpContext.Session.GetInt32("UserId");
            int courtId = viewModel.TennisCourtModel?.TennisCourtId ?? 0;
            var result = _bookingProvider.CheckSlotAvailability(viewModel.TennisCourtBookingModel, courtId);
            // Retrieve existing TempData


            List<TennisCourtBookingViewModel> tempList = GetTempData(userId);

            // Initialize the list if TempData is null
            if (tempList.Count == 0)
            {
                tempList = new List<TennisCourtBookingViewModel>();
            }

            // Set the user ID and court ID in the ViewModel
            viewModel.TennisCourtBookingModel.UserId = userId;
            viewModel.TennisCourtBookingModel.TennisCourtId = courtId;
            if (result.Count < result.Capacity)
            {
                foreach (var item in tempList)
                {
                    if (item != null)
                    {
                        var check = item.TennisCourtBookingModel.TennisCourtId == viewModel.TennisCourtModel.TennisCourtId && item.TennisCourtBookingModel.BookingDate == viewModel.TennisCourtBookingModel.BookingDate && item.TennisCourtBookingModel.BookingTime == viewModel.TennisCourtBookingModel.BookingTime;
                        if (check == true)
                        {
                            result.Count++;
                        }
                    }
                }
                if (result.Count < result.Capacity)
                {
                    tempList.Add(viewModel);
                }
                else
                {
                    model.tempDataKey = "SlotCheck" + userId;
                    TempData[model.tempDataKey] = "Slot Not Available!";
                    //model.Message = "false";
                }
                // Add the new booking to the list


                // Set the updated list in TempData
                SetTempData(tempList, userId);
                TempData["SlotCheck"] = "";
            }
            else
            {
                //model.Message = "false";
                model.tempDataKey = "SlotCheck" + userId;
                TempData[model.tempDataKey] = "Slot Not Available!";
            }
            return Json(model);
        }
        public IActionResult DeleteFromTempData(int id, DateTime bookingDate, TimeSpan bookingTime)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            var itemlist = GetTempData(userId);
            TennisCourtBookingViewModel itemToRemove = itemlist.FirstOrDefault(item => item.TennisCourtBookingModel.TennisCourtId == id && item.TennisCourtBookingModel.BookingDate == bookingDate && item.TennisCourtBookingModel.BookingTime == bookingTime);

            itemlist.Remove(itemToRemove);
            SetTempData(itemlist, userId);
            return Json("");
        }

        protected void SetTempData(List<TennisCourtBookingViewModel> bookings, int? id)
        {
            TempData["List_items" + id] = null;
            TempData["List_items" + id] = JsonSerializer.Serialize(bookings);
        }

        protected List<TennisCourtBookingViewModel> GetTempData(int? id)
        {
            if (TempData["List_items" + id] != null)
            {
                string data = TempData["List_items" + id].ToString();
                TempData.Keep();
                return JsonSerializer.Deserialize<List<TennisCourtBookingViewModel>>(data);
            }
            return new List<TennisCourtBookingViewModel>();
        }


        public IActionResult SubmitBookingsToDatabase()
        {
            TennisCourtBookingViewModel model = new TennisCourtBookingViewModel();
            int? userId = HttpContext.Session.GetInt32("UserId");
            // Retrieve all bookings from TempData
            List<TennisCourtBookingViewModel> bookings = GetTempData(userId);

            // Loop through the bookings and submit them to the database
            foreach (var booking in bookings)
            {
                //int? userId = HttpContext.Session.GetInt32("UserId");
                int? courtId = booking.TennisCourtModel?.TennisCourtId ?? 0;

                // The actual database save logic is separated into the _bookingProvider
                _bookingProvider.SaveBooking(booking.TennisCourtBookingModel, userId, courtId);
            }

            // Clear TempData after submitting to the database
            TempData.Remove("List_items" + userId);
            //TempData.Remove(model.tempDataKey);

            return Json(userId);
        }
        [HttpPost]
        public IActionResult EditBookingStatus(int status, int bookingId)
        {
            var msg = _bookingProvider.EditBookStatus(status, bookingId);
            return Json(msg);
        }
        public JsonResult DownloadTicket(int Id)
        {
            DownloadReportModel model = new DownloadReportModel();
            string fullPath = "";
            string filename = "";
            string reportName = "";
            DataTable dt = new DataTable();
             dt =_bookingProvider.GetBookingDetailsById(Id);
           
            reportName = Path.Combine(_webHostEnvironment.WebRootPath, "/ExtraFolder/Reports/Report1.rdlc");
            string _Guid = Guid.NewGuid().ToString();
            filename = "Download_Ticket" + "_" + _Guid + ".pdf";
            string FilePath = _webHostEnvironment.WebRootPath + "/ExtraFolder/Downloads";
            string realPath = _webHostEnvironment.WebRootPath + "\\" + reportName;
            using var fs = new FileStream(realPath, FileMode.Open);
            LocalReport report = new LocalReport();
            report.LoadReportDefinition(fs);
            report.DataSources.Add(new ReportDataSource("DataSet1"));

            byte[] bytes = report.Render("PDF");
            fullPath = FilePath + "\\" + filename;
            System.IO.File.WriteAllBytes(fullPath, bytes);
            
            return Json(Url.Content("~/ExtraFolder/Downloads/" + filename));
        }
        //public MemoryStream CreatePdfTemplate()
        //{
        //    var stream = new MemoryStream();

        //    var pdf = new PdfDocument(new PdfWriter(stream));
        //    var document = new Document(pdf);

        //    // Add content to the PDF document
        //    //document.Add(new Paragraph("EMPLOYEE EXIT INTERVIEW\n"));
        //    //document.Add(new Paragraph("PURPOSE\n"));
        //    //document.Add(new Paragraph("The intent of this Exit Interview is to ensure that any employee is informed of his/her rights, benefits, and the records are collected and maintained regarding the termination of employment.\n"));

        //    //document.Add(new Paragraph("POLICY\n"));
        //    //document.Add(new Paragraph("It is the policy of Acme Global Company to ensure that any employee whose employment is being terminated, whether voluntary or involuntarily, receives an exit interview. The exit interview shall be conducted by Sharon Williams and/ or Natalia Winfree. The objectives of the exit interview are as follows:\n"));
        //    //document.Add(new List()
        //    //    .Add(new ListItem("To determine and discuss the employee’s reason for resignation, if applicable."))
        //    //    .Add(new ListItem("To discover and discuss any misunderstandings the employee may have had about his/her job or with his/her manager."))
        //    //    .Add(new ListItem("To maintain good will and teamwork amongst current and future employees."))
        //    //    .Add(new ListItem("To review administrative details with the employee such as benefit continuation rights and conversion privileges, if any, final pay, re-employment policy, and employment compensation."))
        //    //    .Add(new ListItem("To arrange for the return of any company property to the operations team."))
        //    //);

        //    //document.Add(new Paragraph("PROCEDURE\n"));
        //    //document.Add(new Paragraph("Upon an employee’s announcement of his/her intent to resign, the project director or manager shall schedule an exit interview for the employee with Sharon Williams or Natalia Winfree as soon as possible.\n"));
        //    //document.Add(new Paragraph("In the event that a decision has been made to terminate an employee, the employee shall meet with Sharon Williams or Natalia Winfree for an exit interview as soon as possible, or as deemed appropriate.\n"));
        //    //document.Add(new Paragraph("Throughout the duration of the exit interview, Sharon Williams or Natalia Winfree shall seek to meet all objectives listed within the exit interview policy.\n"));
        //    //document.Add(new Paragraph("The departing employee shall complete the following exit interview form as thoroughly as possible.\n"));
        //    //document.Add(new Paragraph("Any information obtained during the exit interview may be disclosed to and/or discussed with the employee manager, the project Director and Partners, as deemed necessary, in order to investigate any allegations made or to inform them of any emerging problems.\n"));

        //    //document.Add(new Paragraph("REMINDERS\n"));
        //    //document.Add(new Paragraph("Please remember that your work with Acme Global Company was completed under a non-disclosure agreement. We highly value client confidentiality and all terms of the agreement. Feel free to request a copy for your reference if you do not already have one.\n"));
        //    //document.Add(new Paragraph("All Acme Global Company equipment must be returned to the main office in order to receive final payment.\n"));

        //    //// Add form fields
        //    //document.Add(new Paragraph("Employee Name"));
        //    //document.Add(new Paragraph("First Name"));
        //    //document.Add(new Paragraph("Last Name"));
        //    //document.Add(new Paragraph("Job Title"));

        //    //document.Add(new Paragraph("Start Date"));
        //    // Add more form fields as needed
        //    document.Add(new Paragraph("Hello World!"));

        //    stream.Position = 0; // Reset the stream position to the beginning
        //    document.Close();
        //    return stream;
        //}
       
        private void pdfgenerator()
        {
            string path = "E:/Report/first1.pdf";
            using(PdfWriter writer=new PdfWriter(path))
            using (PdfDocument pdfDoc =new PdfDocument(writer))
                using(Document document= new Document(pdfDoc))
            {
                Paragraph heading= new Paragraph("hello world").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFontSize(50);
            document.Add(heading);
                string intro="Hello my name is amit soni. I'm using iText 7 core.IT is an open source pdf library for creaing pdf. It is very easy and understandable.";
                Paragraph content = new Paragraph(intro);
                document.Add(content);
                Paragraph secondHeading = new Paragraph("what i am do right now.");
                document.Add(secondHeading);
                document.Add(new Paragraph("\nEmployee Name\n"));
                document.Add(new Paragraph("First Name: ").Add(new Text("__________________________")));
                document.Close();
            }
        }
        private MemoryStream CreatePdfTemplate()
        {
            using (var stream = new MemoryStream())
            {

                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                // Add content to the PDF document
                document.Add(new Paragraph("EMPLOYEE EXIT INTERVIEW\n"));
                document.Add(new Paragraph("PURPOSE\n"));
                document.Add(new Paragraph("The intent of this Exit Interview is to ensure that any employee is informed of his/her rights, benefits, and the records are collected and maintained regarding the termination of employment.\n"));

                // Add more content as needed

                // Close the document

                stream.Position = 0; // Reset the stream position to the beginning
                //document.Close();
                return stream;
            }
        }
      
      










        public IActionResult Download()
        {
            string documentPath = Path.Combine(_webHostEnvironment.WebRootPath, "ExtraFolder", "Reports");
            string newDocFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "ExtraFolder", "Downloads");
            string docFilePath = Path.Combine(documentPath, "first1.pdf");
            if (!System.IO.Directory.Exists(newDocFilePath))
                Directory.CreateDirectory(newDocFilePath);
            string fileName = "generatepdf" + ".pdf";
            newDocFilePath = Path.Combine(newDocFilePath, fileName);
           
            if (System.IO.File.Exists(newDocFilePath))
                System.IO.File.Delete(newDocFilePath);
            PdfReader pdfReader = new PdfReader(docFilePath);
            PdfWriter pdfWriter = new PdfWriter(newDocFilePath);
            PdfDocument pdfDocument = new PdfDocument(pdfReader, pdfWriter);
            PdfAcroForm pdfFormFields = PdfAcroForm.GetAcroForm(pdfDocument, true);
            pdfFormFields.GetField("First Name").SetValue("Amit");
            pdfReader.Close();
            return Json(fileName);
        }

        public IActionResult DownloadPdf(string documentName)
        {
          
            string downloadPath = Path.Combine(_webHostEnvironment.WebRootPath, "ExtraFiles", "Downloads", "ContractDocument");
            string newdocumentsFile = Path.Combine(downloadPath, documentName);
            byte[] bytes = System.IO.File.ReadAllBytes(newdocumentsFile);
            return File(bytes, "application/pdf", documentName);
        }
    }
}
