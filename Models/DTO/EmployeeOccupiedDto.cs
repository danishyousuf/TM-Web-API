namespace TMCC.Models.DTO
{
    public class EmployeeOccupiedDto
    {
        // Employee fields
        public Guid EmpId { get; set; }
        public string EmpCode { get; set; }
        public string FullName { get; set; }
        public string Profession { get; set; }
        public string Nationality { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool IsBusy { get; set; }

        // Client fields
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientMobile { get; set; }
        public DateTime? BookedDate { get; set; }

        // 🔥 NEW fields from stored procedure
        public string OccupiedFromDate { get; set; }   // "15-11-2025"
        public int OccupiedDays { get; set; }          // 30
        public string OccupiedFrom { get; set; }       // "15-11-2025 (30 days)"
    }
}
