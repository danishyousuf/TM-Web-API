using System;

namespace TMCC.Models
{
    public class ComputerUpdateModel
    {
        public Guid ComputerId { get; set; }          
        public string CompanyName { get; set; }      
        public string? OperatingSystem { get; set; }    
        public DateTime? WarrantyExpiry { get; set; }  
        public Boolean? CpuGiven { get; set; }          
        public Boolean? MonitorGiven { get; set; }   
        public Boolean? UpsGiven { get; set; }     
        public Boolean? HeadsetGiven { get; set; }       
        public string? CpuSerial { get; set; }          
        public string? MonitorSerial { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string? AssetName { get; set; }
    }
}
