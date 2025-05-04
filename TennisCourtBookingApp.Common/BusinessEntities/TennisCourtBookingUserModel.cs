using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCourtBookingApp.Common.BusinessEntities
{
    public  class TennisCourtBookingUserModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please Enter Your Name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name should be minimum of 3 character")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid Email")]
        public string? Email { get; set; }
        [Required]
        public string? Address { get; set; }

        //[Required]
        //[Range(10, 10, ErrorMessage = "Enter Right Phone NO.")]
        public long? PhoneNo { get; set; }
        [Required]
        public string? Password { get; set; }
        public int? RoleId { get; set; }
        public string? Image { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? IsActive { get; set; }
        public string? InvoiceAttachmentBase {  get; set; }
        public virtual TennisCourtBookingRoleModel? Role { get; set; }
        public string InvoiceAttachment { get; set; }
    }
}
