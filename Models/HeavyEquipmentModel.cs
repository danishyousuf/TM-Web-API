namespace TMCC.Models
{
    public class HeavyEquipmentModel
    {
        public Guid AssetId { get; set; }
        public string AssetName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string? AssignedTo { get; set; }
        public string? Status { get; set; }
        public string? ReleaseDate { get; set; }
        public string? AssignDate { get; set; }
        public string EquipmentType { get; set; }
        public string Brand { get; set; }
        public string? SerialNumber { get; set; }
        public int? UsageHours { get; set; }
        public string? OperatorAssigned { get; set; }
        public DateTime? LastServiceDate { get; set; }
        public DateTime? NextServiceDate { get; set; }
        public string? MaintenanceNotes { get; set; }
    }
}
