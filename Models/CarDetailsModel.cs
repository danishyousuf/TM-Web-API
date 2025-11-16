namespace TMCC.Models
{
    public class CarDetailsModel
    {
        public Guid AssetId { get; set; }
        public string AssetName { get; set; }
        public string Status { get; set; }
        public string AssignedTo { get; set; }
        public string PurchaseDate { get; set; }
        public string Brand { get; set; }
        public string CarType { get; set; }
        public string RegistrationNumber { get; set; }
        public string RcNumber { get; set; }
        public int? KilometerReading { get; set; }
        public string FuelType { get; set; }
        public DateTime? InsuranceExpiry { get; set; }
        public DateTime? PollutionExpiry { get; set; }
        public DateTime? LastServiceDate { get; set; }
        public DateTime? NextServiceDate { get; set; }
    }



}
