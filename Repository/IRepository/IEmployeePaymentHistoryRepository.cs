using TMCC.Models.DTO;

namespace TMCC.Repository.IRepository
{
    public interface IEmployeePaymentHistoryRepository
    {
        Task<IEnumerable<EmployeePaymentHistoryDto>> GetEmployeePaymentsAsync(Guid empId);
        Task<int> AddEmployeePaymentAsync(EmployeePaymentHistoryDto payment);
        Task<int> UpdateEmployeePaymentAsync(EmployeePaymentHistoryDto payment);
        Task<int> DeleteEmployeePaymentAsync(Guid paymentId, string deletedBy);
        Task<int> DeleteAllPaymentsByEmployeeAsync(Guid empId, string deletedBy);
        Task<IEnumerable<EmployeePaymentHistoryDto>> GetLatestEmployeePaymentsAsync();

    }
}
