using System;

namespace WindowsFormsApp1.Models
{
    [Serializable]
    public class UserInfo
    {
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