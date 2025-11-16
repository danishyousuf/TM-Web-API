using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TMCC.Db_Helper;
using TMCC.Models;
using TMCC.Services.IServices;

namespace TMCC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _service;

        public AssetController(IAssetService service)
        {
            _service = service;
        }

        // ========== LAPTOP ==========
        [Authorize]
        [HttpGet("get-laptops")]
        public async Task<IActionResult> GetLaptops() => Ok(await _service.GetLaptopsAsync());

        [Authorize]
        [HttpPost("add-laptop")]
        public async Task<IActionResult> AddLaptop([FromBody] LaptopModel model)
        {
            var result = await _service.AddLaptopAsync(model);
            return Ok(new { Message = result });
        }

        [Authorize]
        [HttpPut("edit-laptop")]
        public async Task<IActionResult> EditLaptop([FromBody] LaptopModel model) =>
            Ok(await _service.EditLaptopAsync(model));

        [HttpDelete("delete-laptop/{assetId}")]
        public async Task<IActionResult> DeleteLaptop(string assetId) =>
            Ok(await _service.DeleteLaptopAsync(assetId));

        // ========== CAR ==========
        [Authorize]
        [HttpGet("get-cars")]
        public async Task<IActionResult> GetCars() => Ok(await _service.GetCarsAsync());
        [Authorize]
        [HttpPost("add-car")]
        public async Task<IActionResult> AddCar([FromBody] CarModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.AddCarAsync(model);
                return Ok(new { message = result });  
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        [Authorize]
        [HttpPut("edit-car")]
        public async Task<IActionResult> EditCar([FromBody] CarModel model) =>
            Ok(await _service.EditCarAsync(model));
        [Authorize]
        [HttpDelete("delete-car/{assetId}")]
        public async Task<IActionResult> DeleteCar(string assetId) =>
            Ok(await _service.DeleteCarAsync(assetId));

        // ========== COMPUTER ==========
        [Authorize]
        [HttpGet("get-computers")]
        public async Task<IActionResult> GetComputers() => Ok(await _service.GetComputersAsync());

        [Authorize]
        [HttpPost("add-computer")]
        public async Task<IActionResult> AddComputer([FromBody] ComputerModel model) =>
            Ok(new { AssetId = await _service.AddComputerAsync(model) });

        [Authorize]
        [HttpPut("edit-computer")]
        public async Task<IActionResult> EditComputer([FromBody] ComputerUpdateModel updateModel) =>
            Ok(await _service.EditComputerAsync(updateModel));

        [Authorize]
        [HttpDelete("delete-computer/{assetId}")]
        public async Task<IActionResult> DeleteComputer(string assetId) =>
            Ok(await _service.DeleteComputerAsync(assetId));

        // ========== HEAVY EQUIPMENT ==========
        [Authorize]
        [HttpGet("get-heavy-equipments")]
        public async Task<IActionResult> GetHeavyEquipments() => Ok(await _service.GetHeavyEquipmentsAsync());

        [Authorize]
        [HttpPost("add-heavy-equipment")]
        public async Task<IActionResult> AddHeavyEquipment([FromBody] HeavyEquipmentModel model) =>
            Ok(new { AssetId = await _service.AddHeavyEquipmentAsync(model) });

        [Authorize]
        [HttpPut("edit-heavy-equipment")]
        public async Task<IActionResult> EditHeavyEquipment([FromBody] HeavyEquipmentModel model) =>
            Ok(await _service.EditHeavyEquipmentAsync(model));

        [Authorize]
        [HttpDelete("delete-heavy-equipment/{assetId}")]
        public async Task<IActionResult> DeleteHeavyEquipment(string assetId) =>
            Ok(await _service.DeleteHeavyEquipmentAsync(assetId));

        // ========== SAFETY EQUIPMENT ==========
        [Authorize]
        [HttpGet("get-safety-equipments")]
        public async Task<IActionResult> GetSafetyEquipments() => Ok(await _service.GetSafetyEquipmentsAsync());

        [Authorize]
        [HttpPost("add-safety-equipment")]
        public async Task<IActionResult> AddSafetyEquipment([FromBody] SafetyEquipmentModel model) =>
            Ok(new { AssetId = await _service.AddSafetyEquipmentAsync(model) });

        [Authorize]
        [HttpPut("edit-safety-equipment")]
        public async Task<IActionResult> EditSafetyEquipment([FromBody] SafetyEquipmentModel model) =>
            Ok(await _service.EditSafetyEquipmentAsync(model));

        [Authorize]
        [HttpDelete("delete-safety-equipment/{assetId}")]
        public async Task<IActionResult> DeleteSafetyEquipment(string assetId) =>
            Ok(await _service.DeleteSafetyEquipmentAsync(assetId));

        // ========== DOCUMENT ==========
        [Authorize]
        [HttpGet("get-documents/{assetId}")]
        public async Task<IActionResult> GetAssetDocuments(string assetId)
        {
            try
            {
                var docs = await _service.GetAssetDocumentsAsync(assetId);

              
                return Ok(docs);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, new { message = "Failed to fetch documents" });
            }
        }

        [Authorize]
        [HttpPost("add-document")]
        public async Task<IActionResult> AddDocument([FromForm] IFormFile file, [FromForm] string documentName,
                                                 [FromForm] Guid assetId, [FromForm] string assetType,
                                                 [FromForm] DateTime? expiryDate, [FromForm] string? uploadedBy = null)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { error = "File is required." });

            // ✅ Read file into memory
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }

            // ✅ Build the model object
            var model = new AssetDocumentModel
            {
                DocumentId = Guid.NewGuid(),
                AssetId = assetId,
                DocumentName = documentName,
                FileName = Path.GetFileName(file.FileName),
                FileExtension = Path.GetExtension(file.FileName),
                FileContent = fileBytes,
                FileSize = file.Length,
                ExpiryDate = expiryDate,
                UploadedBy = uploadedBy ?? "System",
                UploadedAt = DateTime.UtcNow,
                AssetType = assetType
            };

            var result = await _service.AddAssetDocumentAsync(model);

            return result
                ? Ok(new { message = "Document uploaded successfully." })
                : StatusCode(500, new { error = "Failed to upload document." });
        }


        [Authorize]
        [HttpDelete("delete-document/{documentId}")]
        public async Task<IActionResult> DeleteDocument(string documentId)
        {
            var result = await _service.DeleteAssetDocumentAsync(documentId);
            return result
                ? Ok(new { message = "Document deleted successfully." })
                : NotFound(new { error = "Document not found." });
        }
        // ========== ASSIGN ASSET ==========
        [Authorize]
        [HttpPost("assign-asset")]
        public async Task<IActionResult> AssignAsset([FromBody] AssignAssetModel model)
        {
            try
            {
                var success = await _service.AssignAssetAsync(
                    model.AssetId,
                    model.AssetType,
                    model.AssignedTo,
                    model.AssignedDate
                );

                if (success)
                    return Ok(new { message = "Asset assigned successfully." });
                else
                    return StatusCode(500, new { error = "Failed to assign asset (no rows affected)." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Exception occurred: {ex.Message}" });
            }
        }

        // ========== RELEASE ASSET ==========
        [Authorize]
        [HttpPost("release-asset")]
        public async Task<IActionResult> ReleaseAsset([FromBody] ReleaseAssetModel model)
        {
            var success = await _service.ReleaseAssetAsync(model.AssetId, model.AssetType, model.ReleasedDate);
            return success
                ? Ok(new { message = "Asset released successfully." })
                : StatusCode(500, new { error = "Failed to release asset." });
        }



    }
}
