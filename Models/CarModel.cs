using System;
using System.ComponentModel.DataAnnotations;

namespace TMCC.Models
{
    public class CarModel
    {
        public Guid AssetId { get; set; }

        public string Type { get; set; } = "Car";  // Default value (optional)
        public string Status { get; set; } = "Available";

        public string? AssignedTo { get; set; }  // Optional

        [Required(ErrorMessage = "Asset Name is required.")]
        public string AssetName { get; set; }

        [Required(ErrorMessage = "Brand is required.")]
        public string Brand { get; set; }      

        [Required(ErrorMessage = "Registration Number is required.")]
        public string RegistrationNumber { get; set; }

        [Required(ErrorMessage = "RC Number is required.")]
        public string RcNumber { get; set; }

        [Required(ErrorMessage = "Fuel Type is required.")]
        public string FuelType { get; set; }
        public string? CarType { get; set; }

        public int? KilometerReading { get; set; }  // Optional
        public DateTime PurchaseDate { get; set; }
        public DateTime? InsuranceExpiry { get; set; }
        public DateTime? PollutionExpiry { get; set; }
        public DateTime? LastServiceDate { get; set; }
        public DateTime? NextServiceDate { get; set; }
        public string? ReleaseDate { get; set; }
        public string? AssignDate { get; set; }
    }
}
