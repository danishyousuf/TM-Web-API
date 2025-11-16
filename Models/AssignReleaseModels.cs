namespace TMCC.Models
{
    public class AssignAssetModel
    {
        public string AssetId { get; set; } = string.Empty;
        public string AssetType { get; set; } = string.Empty;
        public string AssignedTo { get; set; } = string.Empty;
        public DateTime AssignedDate { get; set; }

    }

    public class ReleaseAssetModel
    {
        public string AssetId { get; set; } = string.Empty;
        public string AssetType { get; set; } = string.Empty;
        public DateTime ReleasedDate { get; set; }
    }
}
