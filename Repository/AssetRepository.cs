using Dapper;
using System.Data;
using TMCC.Db_Helper;
using TMCC.Models;
using TMCC.Repository.IRepository;


namespace TMCC.Repository
{
    public class AssetRepository : IAssetRepository
    {
        private readonly DapperHelper _dapperHelper;

        public AssetRepository(DapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }


        // ==================== LAPTOP ====================
        public async Task<string> AddLaptopAsync(LaptopModel model)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_type", model.Type);
            parameters.Add("p_status", model.Status);
            parameters.Add("p_assigned_to", model.AssignedTo);
            parameters.Add("p_asset_name", model.AssetName);
            parameters.Add("p_company_name", model.CompanyName);
            parameters.Add("p_model_number", model.ModelNumber);
            parameters.Add("p_serial_number", model.SerialNumber);
            parameters.Add("p_operating_system", model.OperatingSystem);
            parameters.Add("p_ram", model.Ram);
            parameters.Add("p_storage", model.Storage);
            parameters.Add("p_warranty_expiry", model.WarrantyExpiry);
            parameters.Add("p_purchase_date", model.PurchaseDate);
            parameters.Add("p_has_external_monitor", model.HasExternalMonitor);
            parameters.Add("p_has_headset", model.HasHeadset);

            await _dapperHelper.ExecuteAsync(
                "sp_AddLaptop",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return "Laptop added successfully";
        }


        public async Task<bool> EditLaptopAsync(LaptopModel model)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_asset_id", model.AssetId);
            parameters.Add("p_asset_name", model.AssetName);
            parameters.Add("p_company_name", model.CompanyName);
            parameters.Add("p_model_number", model.ModelNumber);
            parameters.Add("p_serial_number", model.SerialNumber);
            parameters.Add("p_operating_system", model.OperatingSystem);
            parameters.Add("p_ram", model.Ram);
            parameters.Add("p_storage", model.Storage);
            parameters.Add("p_warranty_expiry", model.WarrantyExpiry);
            parameters.Add("p_purchase_date", model.PurchaseDate);
            parameters.Add("p_assigned_to", model.AssignedTo);
            parameters.Add("p_has_external_monitor", model.HasExternalMonitor);
            parameters.Add("p_has_headset", model.HasHeadset);

            var rows = await _dapperHelper.ExecuteAsync(
                "sp_UpdateLaptop",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return rows > 0;
        }



        public async Task<IEnumerable<LaptopModel>> GetLaptopsAsync()
        {
            
            return await _dapperHelper.QueryAsync<LaptopModel>("sp_GetLaptops", commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<bool> DeleteLaptopAsync(string assetId)
        {
            
            var rows = await _dapperHelper.ExecuteAsync("sp_DeleteLaptop", new { p_asset_id = assetId }, commandType: System.Data.CommandType.StoredProcedure);
            return rows > 0;
        }

        // ==================== CAR ====================
        public async Task<string> AddCarAsync(CarModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_type", model.Type);
            parameters.Add("p_status", model.Status);
            parameters.Add("p_asset_name", model.AssetName);
            parameters.Add("p_brand", model.Brand);
            parameters.Add("p_car_type", model.CarType);
            parameters.Add("p_registration_number", model.RegistrationNumber);
            parameters.Add("p_rc_number", model.RcNumber);
            parameters.Add("p_kilometer_reading", model.KilometerReading);
            parameters.Add("p_fuel_type", model.FuelType);
            parameters.Add("p_purchase_date", model.PurchaseDate);
            parameters.Add("p_insurance_expiry", model.InsuranceExpiry);
            parameters.Add("p_pollution_expiry", model.PollutionExpiry);
            parameters.Add("p_last_service_date", model.LastServiceDate);
            parameters.Add("p_next_service_date", model.NextServiceDate);

            await _dapperHelper.ExecuteAsync(
                "sp_AddCar", parameters, commandType: CommandType.StoredProcedure);

            return "Car added successfully";
        }


        public async Task<bool> EditCarAsync(CarModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_asset_id", model.AssetId);
            parameters.Add("p_type", model.Type);
            parameters.Add("p_status", model.Status);
            parameters.Add("p_assigned_to", model.AssignedTo);
            parameters.Add("p_asset_name", model.AssetName);
            parameters.Add("p_brand", model.Brand);
            parameters.Add("p_car_type", model.CarType);
            parameters.Add("p_registration_number", model.RegistrationNumber);
            parameters.Add("p_rc_number", model.RcNumber);
            parameters.Add("p_kilometer_reading", model.KilometerReading);
            parameters.Add("p_fuel_type", model.FuelType);
            parameters.Add("p_purchase_date", model.PurchaseDate);
            parameters.Add("p_insurance_expiry", model.InsuranceExpiry);
            parameters.Add("p_pollution_expiry", model.PollutionExpiry);
            parameters.Add("p_last_service_date", model.LastServiceDate);
            parameters.Add("p_next_service_date", model.NextServiceDate);

            var rows = await _dapperHelper.ExecuteAsync(
                "sp_UpdateCar",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return rows > 0;
        }


        public async Task<IEnumerable<CarDetailsModel>> GetCarsAsync()
        {
            
            return await _dapperHelper.QueryAsync<CarDetailsModel>("sp_GetCars", commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<bool> DeleteCarAsync(string assetId)
        {
            var rows = await _dapperHelper.ExecuteAsync(
                "sp_DeleteCar",
                new { p_asset_id = assetId }, 
                commandType: System.Data.CommandType.StoredProcedure
            );

            return rows > 0;
        }

        // ==================== COMPUTER ====================
        public async Task<string> AddComputerAsync(ComputerModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_status", model.Status);
            parameters.Add("p_assigned_to", model.AssignedTo);
            parameters.Add("p_asset_name", model.AssetName);
            parameters.Add("p_company_name", model.CompanyName);
            parameters.Add("p_operating_system", model.OperatingSystem);
            parameters.Add("p_warranty_expiry", model.WarrantyExpiry);
            parameters.Add("p_cpu_given", model.CpuGiven);
            parameters.Add("p_monitor_given", model.MonitorGiven);
            parameters.Add("p_ups_given", model.UpsGiven);
            parameters.Add("p_headset_given", model.HeadsetGiven);
            parameters.Add("p_cpu_serial", model.CpuSerial);
            parameters.Add("p_monitor_serial", model.MonitorSerial);
            parameters.Add("p_purchase_date", model.PurchaseDate);

            return await _dapperHelper.QueryFirstOrDefaultAsync<string>(
                "sp_AddComputer",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> EditComputerAsync(ComputerUpdateModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_computer_id", model.ComputerId);
            parameters.Add("p_company_name", model.CompanyName);
            parameters.Add("p_asset_name", model.AssetName);
            parameters.Add("p_operating_system", model.OperatingSystem);
            parameters.Add("p_warranty_expiry", model.WarrantyExpiry);
            parameters.Add("p_cpu_given", model.CpuGiven);
            parameters.Add("p_monitor_given", model.MonitorGiven);
            parameters.Add("p_ups_given", model.UpsGiven);
            parameters.Add("p_headset_given", model.HeadsetGiven);
            parameters.Add("p_cpu_serial", model.CpuSerial);
            parameters.Add("p_monitor_serial", model.MonitorSerial);
            parameters.Add("p_purchase_date", model.PurchaseDate);

            var rows = await _dapperHelper.ExecuteAsync(
                "sp_UpdateComputer",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return rows > 0;
        }


        public async Task<IEnumerable<ComputerModel>> GetComputersAsync()
        {
            return await _dapperHelper.QueryAsync<ComputerModel>(
                "sp_GetComputers",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> DeleteComputerAsync(string computerId)
        {
            var rows = await _dapperHelper.ExecuteAsync(
                "sp_DeleteComputer",
                new { p_asset_id = computerId },
                commandType: CommandType.StoredProcedure
            );

            return rows > 0;
        }


        // ==================== HEAVY EQUIPMENT ====================
        public async Task<string> AddHeavyEquipmentAsync(HeavyEquipmentModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_asset_id", model.AssetId);
            parameters.Add("p_type", "HeavyEquipment"); 
            parameters.Add("p_status", model.Status);
            parameters.Add("p_assigned_to", model.AssignedTo);
            parameters.Add("p_asset_name", model.AssetName);
            parameters.Add("p_equipment_type", model.EquipmentType);
            parameters.Add("p_brand", model.Brand);
            parameters.Add("p_serial_number", model.SerialNumber);
            parameters.Add("p_usage_hours", model.UsageHours);
            parameters.Add("p_operator_assigned", model.OperatorAssigned);
            parameters.Add("p_last_service_date", model.LastServiceDate);
            parameters.Add("p_next_service_date", model.NextServiceDate);
            parameters.Add("p_maintenance_notes", model.MaintenanceNotes);
            parameters.Add("p_purchase_date", model.PurchaseDate);

            return await _dapperHelper.QueryFirstOrDefaultAsync<string>(
                "sp_AddHeavyEquipment",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }


        public async Task<bool> EditHeavyEquipmentAsync(HeavyEquipmentModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_asset_id", model.AssetId);
            parameters.Add("p_asset_name", model.AssetName);
            parameters.Add("p_purchase_date", model.PurchaseDate);
            parameters.Add("p_equipment_type", model.EquipmentType);
            parameters.Add("p_brand", model.Brand);
            parameters.Add("p_serial_number", model.SerialNumber);
            parameters.Add("p_usage_hours", model.UsageHours);
            parameters.Add("p_operator_assigned", model.OperatorAssigned);
            parameters.Add("p_last_service_date", model.LastServiceDate);
            parameters.Add("p_next_service_date", model.NextServiceDate);
            parameters.Add("p_maintenance_notes", model.MaintenanceNotes);

            var rows = await _dapperHelper.ExecuteAsync(
                "sp_UpdateHeavyEquipment",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return rows > 0;
        }

        public async Task<IEnumerable<HeavyEquipmentModel>> GetHeavyEquipmentsAsync()
        {
            
            return await _dapperHelper.QueryAsync<HeavyEquipmentModel>("sp_GetHeavyEquipments", commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<bool> DeleteHeavyEquipmentAsync(string assetId)
        {
            
            var rows = await _dapperHelper.ExecuteAsync("sp_DeleteHeavyEquipment", new { p_asset_id = assetId }, commandType: System.Data.CommandType.StoredProcedure);
            return rows > 0;
        }

        // ==================== SAFETY EQUIPMENT ====================
        public async Task<string> AddSafetyEquipmentAsync(SafetyEquipmentModel model)
        {
            // Adds new safety equipment and asset entry together
            return await _dapperHelper.QueryFirstOrDefaultAsync<string>(
                "sp_AddSafetyEquipment",
                new
                {
                    p_asset_name = model.AssetName,
                    p_type = "SafetyEquipment",
                    p_purchase_date = model.PurchaseDate,
                    p_assigned_to = model.AssignedTo,
                    p_status = model.Status,
                    p_product_type = model.ProductType,
                    p_company_name = model.CompanyName,
                    p_quantity = model.Quantity,
                    p_expiry_date = model.ExpiryDate
                },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }

        public async Task<bool> EditSafetyEquipmentAsync(SafetyEquipmentModel model)
        {
            // Updates both asset and safety equipment data
            var rows = await _dapperHelper.ExecuteAsync(
                "sp_UpdateSafetyEquipment",
                new
                {
                    p_asset_id = model.AssetId,
                    p_asset_name = model.AssetName,
                    p_purchase_date = model.PurchaseDate,
                    p_assigned_to = model.AssignedTo,
                    p_status = model.Status,
                    p_product_type = model.ProductType,
                    p_company_name = model.CompanyName,
                    p_quantity = model.Quantity,
                    p_expiry_date = model.ExpiryDate
                },
                commandType: System.Data.CommandType.StoredProcedure
            );

            return rows > 0;
        }

        public async Task<IEnumerable<SafetyEquipmentModel>> GetSafetyEquipmentsAsync()
        {
            // Fetches all active safety equipments with asset info
            return await _dapperHelper.QueryAsync<SafetyEquipmentModel>(
                "sp_GetSafetyEquipments",
                commandType: System.Data.CommandType.StoredProcedure
            );
        }


        public async Task<bool> DeleteSafetyEquipmentAsync(string assetId)
        {
            
            var rows = await _dapperHelper.ExecuteAsync("sp_DeleteSafetyEquipment", new { p_asset_id = assetId }, commandType: System.Data.CommandType.StoredProcedure);
            return rows > 0;
        }

        // ==================== DOCUMENT ====================
        public async Task<bool> AddAssetDocumentAsync(AssetDocumentModel model)
        {
            var parameters = new
            {
                p_document_id = Guid.NewGuid(),
                p_asset_id = model.AssetId,
                p_document_name = model.DocumentName,
                p_file_name = model.FileName,
                p_file_extension = model.FileExtension,
                p_file_content = model.FileContent,
                p_file_size = model.FileSize,
                p_expiry_date = model.ExpiryDate,
                p_uploaded_by = model.UploadedBy,
                p_asset_type = model.AssetType
            };

            var result = await _dapperHelper.ExecuteAsync(
                "sp_AddAssetDocument",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result > 0;
        }
        public async Task<IEnumerable<AssetDocumentModel>> GetAssetDocumentsAsync(string assetId)
        {
            return await _dapperHelper.QueryAsync<AssetDocumentModel>(
                "sp_GetAssetDocuments",
                new { p_asset_id = assetId },
                commandType: CommandType.StoredProcedure
            );
        }


        public async Task<bool> DeleteAssetDocumentAsync(string documentId)
        {
            var rows = await _dapperHelper.ExecuteAsync(
                "sp_DeleteAssetDocument",
                new { p_document_id = documentId },
                commandType: System.Data.CommandType.StoredProcedure
            );

            return rows > 0;
        }

        // assign and release assets
        public async Task<bool> AssignAssetAsync(string assetId, string assetType, string assignedTo, DateTime assignedDate)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_asset_id", assetId);
                parameters.Add("p_asset_type", assetType);
                parameters.Add("p_assigned_to", assignedTo);
                parameters.Add("p_assigned_date", assignedDate);

                var result = await _dapperHelper.QueryFirstOrDefaultAsync<int>(
                    "sp_AssignAssetToEmployee",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AssignAssetAsync ERROR] {ex.Message}");
                throw;
            }
        }


        public async Task<bool> ReleaseAssetAsync(string assetId, string assetType, DateTime releasedDate)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_asset_id", assetId);
            parameters.Add("p_asset_type", assetType);
            parameters.Add("p_released_date", releasedDate);

            var rows = await _dapperHelper.QueryFirstOrDefaultAsync<int>(
                "sp_ReleaseAssetFromEmployee",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return rows > 0;
        }


    }
}
