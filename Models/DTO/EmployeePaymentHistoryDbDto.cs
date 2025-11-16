namespace TMCC.Models.DTO
{
    public class EmployeePaymentHistoryDbDto
    {
        public Guid payment_id { get; set; }
        public Guid emp_id { get; set; }
        public string full_name { get; set; }
        public string email { get; set; }

        public DateTime payment_date { get; set; }
        public decimal amount { get; set; }
        public string payment_method { get; set; }
        public string remarks { get; set; }
        public string created_by { get; set; }
        public DateTime created_at { get; set; }
    }
}
