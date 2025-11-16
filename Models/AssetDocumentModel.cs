namespace TMCC.Models
{
    public class AssetDocumentModel
    {
        public Guid DocumentId { get; set; }
        public Guid AssetId { get; set; }
        public string DocumentName { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public byte[]? FileContent { get; set; }
        public long FileSize { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public string AssetType { get; set; }

        // For form-data upload
        public IFormFile File { get; set; }
    }
}
