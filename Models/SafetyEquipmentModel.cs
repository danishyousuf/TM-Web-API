namespace TMCC.Models
{
    public class SafetyEquipmentModel
    {
        public Guid AssetId { get; set; }
        public string AssetName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string? AssignedTo { get; set; }
        public string? Status { get; set; }

        public string? ProductType { get; set; }
        public string? CompanyName { get; set; }
        public int? Quantity { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? ReleaseDate { get; set; }
        public string? AssignDate { get; set; }
    }
}
