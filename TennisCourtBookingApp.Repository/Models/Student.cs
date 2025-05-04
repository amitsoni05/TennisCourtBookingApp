using System;
using System.Collections.Generic;

namespace TennisCourtBookingApp.Repository.Models
{
    public partial class Student
    {
        public Student()
        {
            CourseStudents = new HashSet<CourseStudent>();
            UserMasters = new HashSet<UserMaster>();
        }

        public int StudentId { get; set; }
        public string? StuFname { get; set; }
        public string? StuLname { get; set; }
        public string? StuEmail { get; set; }
        public string? StuPassword { get; set; }
        public bool? IsActive { get; set; }
        public string? Role { get; set; }
        public string? StuCity { get; set; }
        public string? StuState { get; set; }
        public byte[]? StuImage { get; set; }

        public virtual ICollection<CourseStudent> CourseStudents { get; set; }
        public virtual ICollection<UserMaster> UserMasters { get; set; }
    }
}
