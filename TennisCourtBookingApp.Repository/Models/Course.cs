using System;
using System.Collections.Generic;

namespace TennisCourtBookingApp.Repository.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseStudents = new HashSet<CourseStudent>();
        }

        public int CourseId { get; set; }
        public string? CourseName { get; set; }

        public virtual ICollection<CourseStudent> CourseStudents { get; set; }
    }
}
