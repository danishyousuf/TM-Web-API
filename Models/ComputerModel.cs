namespace TMCC.Models
{
    public class ComputerModel
    {
        public Guid ComputerId { get; set; }
        public Guid AssetId { get; set; }
        public string CompanyName { get; set; }
        public string? OperatingSystem { get; set; }
        public DateTime? WarrantyExpiry { get; set; }
        public Boolean? CpuGiven { get; set; }
        public Boolean? MonitorGiven { get; set; }
        public Boolean? UpsGiven { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Boolean? HeadsetGiven { get; set; }
        public string? CpuSerial { get; set; }
        public string? MonitorSerial { get; set; }

        // Optionally include some asset details for UI
        public string? AssetName { get; set; }
        public string? AssignedTo { get; set; }
        public string? Status { get; set; }
        public string? ReleaseDate { get; set; }
        public string? AssignDate { get; set; }
    }

}
