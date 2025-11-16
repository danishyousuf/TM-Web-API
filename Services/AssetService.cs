using TMCC.Models;
using TMCC.Repository.IRepository;
using TMCC.Services.IServices;

namespace TMCC.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _repo;
        public AssetService(IAssetRepository repo) => _repo = repo;

        // Laptop
        public Task<string> AddLaptopAsync(LaptopModel model)
        {
            return _repo.AddLaptopAsync(model);
        }

        public Task<bool> EditLaptopAsync(LaptopModel model) => _repo.EditLaptopAsync(model);
        public Task<IEnumerable<LaptopModel>> GetLaptopsAsync() => _repo.GetLaptopsAsync();
        public Task<bool> DeleteLaptopAsync(string assetId) => _repo.DeleteLaptopAsync(assetId);

        // Car
        public Task<string> AddCarAsync(CarModel model) => _repo.AddCarAsync(model);
        public Task<bool> EditCarAsync(CarModel model) => _repo.EditCarAsync(model);
        public Task<IEnumerable<CarDetailsModel>> GetCarsAsync() => _repo.GetCarsAsync();
        public Task<bool> DeleteCarAsync(string assetId) => _repo.DeleteCarAsync(assetId);

        // Computer
        public Task<string> AddComputerAsync(ComputerModel model) => _repo.AddComputerAsync(model);
        public Task<bool> EditComputerAsync(ComputerUpdateModel model) => _repo.EditComputerAsync(model);
        public Task<IEnumerable<ComputerModel>> GetComputersAsync() => _repo.GetComputersAsync();
        public Task<bool> DeleteComputerAsync(string assetId) => _repo.DeleteComputerAsync(assetId);

        // Heavy Equipment
        public Task<string> AddHeavyEquipmentAsync(HeavyEquipmentModel model) => _repo.AddHeavyEquipmentAsync(model);
        public Task<bool> EditHeavyEquipmentAsync(HeavyEquipmentModel model) => _repo.EditHeavyEquipmentAsync(model);
        public Task<IEnumerable<HeavyEquipmentModel>> GetHeavyEquipmentsAsync() => _repo.GetHeavyEquipmentsAsync();
        public Task<bool> DeleteHeavyEquipmentAsync(string assetId) => _repo.DeleteHeavyEquipmentAsync(assetId);

        // Safety Equipment
        public Task<string> AddSafetyEquipmentAsync(SafetyEquipmentModel model) => _repo.AddSafetyEquipmentAsync(model);
        public Task<bool> EditSafetyEquipmentAsync(SafetyEquipmentModel model) => _repo.EditSafetyEquipmentAsync(model);
        public Task<IEnumerable<SafetyEquipmentModel>> GetSafetyEquipmentsAsync() => _repo.GetSafetyEquipmentsAsync();
        public Task<bool> DeleteSafetyEquipmentAsync(string assetId) => _repo.DeleteSafetyEquipmentAsync(assetId);

        // Documents
        public Task<bool> AddAssetDocumentAsync(AssetDocumentModel model) => _repo.AddAssetDocumentAsync(model);
        public Task<IEnumerable<AssetDocumentModel>> GetAssetDocumentsAsync(string assetId) => _repo.GetAssetDocumentsAsync(assetId);
        public Task<bool> DeleteAssetDocumentAsync(string documentId) => _repo.DeleteAssetDocumentAsync(documentId);

        // assign and release assets
        public Task<bool> AssignAssetAsync(string assetId, string assetType, string assignedTo, DateTime assignedDate)
    => _repo.AssignAssetAsync(assetId, assetType, assignedTo, assignedDate);

        public Task<bool> ReleaseAssetAsync(string assetId, string assetType, DateTime releasedDate)
            => _repo.ReleaseAssetAsync(assetId, assetType, releasedDate);

    }
}


