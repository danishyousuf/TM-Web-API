using System;

namespace TMCC.Models.DTO
{
    public class EmployeeAvailableDto
    {
        public Guid EmpId { get; set; }
        public string EmpCode { get; set; }
        public string FullName { get; set; }
        public string Profession { get; set; }
        public string Nationality { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool IsBusy { get; set; }

        public string LastWorkingDate { get; set; }
        public int? FreeDays { get; set; }
        public string FreeFrom { get; set; }
    }
}
