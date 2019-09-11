using System;

namespace WindowsFormsApp1.Models
{
    public class UserInfo
    {
        public UserInfo(Guid? userId = null)
        {
            Id = userId ?? Guid.NewGuid();
        }

        public Guid Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FacultyName { get; set; }
        public string Email { get; set; }
        public string GroupNumber { get; set; }
        public string Phone { get; set; }

        public int DobDay { get; set; }
        public int DobMonth { get; set; }
        public int DobYear { get; set; }
    }
}