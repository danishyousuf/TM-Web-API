using TMCC.Models;

namespace TMCC.Services.IServices
{
    public interface IAssetService
    {
        // Laptop
        Task<string> AddLaptopAsync(LaptopModel model);
        Task<bool> EditLaptopAsync(LaptopModel model);
        Task<IEnumerable<LaptopModel>> GetLaptopsAsync();
        Task<bool> DeleteLaptopAsync(string assetId);

        // Car
        Task<string> AddCarAsync(CarModel model);
        Task<bool> EditCarAsync(CarModel model);
        Task<IEnumerable<CarDetailsModel>> GetCarsAsync();
        Task<bool> DeleteCarAsync(string assetId);

        // Computer
        Task<string> AddComputerAsync(ComputerModel model);
        Task<bool> EditComputerAsync(ComputerUpdateModel model);
        Task<IEnumerable<ComputerModel>> GetComputersAsync();
        Task<bool> DeleteComputerAsync(string assetId);

        // Heavy Equipment
        Task<string> AddHeavyEquipmentAsync(HeavyEquipmentModel model);
        Task<bool> EditHeavyEquipmentAsync(HeavyEquipmentModel model);
        Task<IEnumerable<HeavyEquipmentModel>> GetHeavyEquipmentsAsync();
        Task<bool> DeleteHeavyEquipmentAsync(string assetId);

        // Safety Equipment
        Task<string> AddSafetyEquipmentAsync(SafetyEquipmentModel model);
        Task<bool> EditSafetyEquipmentAsync(SafetyEquipmentModel model);
        Task<IEnumerable<SafetyEquipmentModel>> GetSafetyEquipmentsAsync();
        Task<bool> DeleteSafetyEquipmentAsync(string assetId);

        // Documents
        Task<bool> AddAssetDocumentAsync(AssetDocumentModel model);
        Task<IEnumerable<AssetDocumentModel>> GetAssetDocumentsAsync(string assetId);
        Task<bool> DeleteAssetDocumentAsync(string documentId);

        // assign and release assets
        Task<bool> AssignAssetAsync(string assetId, string assetType, string assignedTo, DateTime assignedDate);
        Task<bool> ReleaseAssetAsync(string assetId, string assetType, DateTime releasedDate);

    }
}
