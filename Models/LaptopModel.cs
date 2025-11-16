namespace TMCC.Models
{
    public class LaptopModel
    {
        public Guid AssetId { get; set; }
        public string? Type { get; set; }
        public string Status { get; set; }
        public string? AssignedTo { get; set; }
        public string AssetName { get; set; }
        public string CompanyName { get; set; }
        public string? ModelNumber { get; set; }
        public string? SerialNumber { get; set; }
        public string? OperatingSystem { get; set; }
        public string? Ram { get; set; }
        public string? Storage { get; set; }
        public DateTime? WarrantyExpiry { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public bool? HasExternalMonitor { get; set; }
        public bool? HasHeadset { get; set; }
        public string? ReleaseDate { get; set; }
        public string? AssignDate { get; set; }
    }

}
