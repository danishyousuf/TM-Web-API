using Dapper;
using System.Data;
using TMCC.Db_Helper;
using TMCC.Models.DTO;
using TMCC.Repository.IRepository;

namespace TMCC.Repository
{
    public class EmployeePaymentHistoryRepository : IEmployeePaymentHistoryRepository
    {
        private readonly DapperHelper _dapperHelper;

        public EmployeePaymentHistoryRepository(DapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }

        public async Task<IEnumerable<EmployeePaymentHistoryDto>> GetEmployeePaymentsAsync(Guid empId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_emp_id", empId);

            var data = await _dapperHelper.QueryAsync<EmployeePaymentHistoryDbDto>(
                "sp_GetEmployeePaymentHistory",
                parameters,
                CommandType.StoredProcedure
            );

            return data.Select(x => new EmployeePaymentHistoryDto
            {
                PaymentId = x.payment_id,
                EmpId = x.emp_id,
                FullName = x.full_name,
                Email = x.email,
                PaymentDate = x.payment_date,
                Amount = x.amount,
                PaymentMode = x.payment_method,
                Remarks = x.remarks,
                CreatedBy = x.created_by,
                CreatedAt = x.created_at
            });
        }


        public async Task<int> AddEmployeePaymentAsync(EmployeePaymentHistoryDto payment)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_emp_id", payment.EmpId);
            parameters.Add("p_amount", payment.Amount);
            parameters.Add("p_payment_date", payment.PaymentDate);
            parameters.Add("p_payment_method", payment.PaymentMode);
            parameters.Add("p_remarks", payment.Remarks);
            parameters.Add("p_created_by", payment.CreatedBy);
            return await _dapperHelper.ExecuteNonQueryAsync("sp_AddEmployeePayment", parameters);
        }

        public async Task<int> UpdateEmployeePaymentAsync(EmployeePaymentHistoryDto payment)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_payment_id", payment.PaymentId);
            parameters.Add("p_amount", payment.Amount);
            parameters.Add("p_payment_date", payment.PaymentDate);
            parameters.Add("p_payment_mode", payment.PaymentMode);
            parameters.Add("p_remarks", payment.Remarks);

            return await _dapperHelper.ExecuteNonQueryAsync("sp_UpdateEmployeePayment", parameters);
        }

        public async Task<int> DeleteEmployeePaymentAsync(Guid paymentId, string deletedBy)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_payment_id", paymentId);
            parameters.Add("p_deleted_by", deletedBy);

            return await _dapperHelper.ExecuteNonQueryAsync(
                "sp_DeleteEmployeePaymentById",
                parameters
            );
        }
        public async Task<int> DeleteAllPaymentsByEmployeeAsync(Guid empId, string deletedBy)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_emp_id", empId);
            parameters.Add("p_deleted_by", deletedBy);

            return await _dapperHelper.ExecuteNonQueryAsync(
                "sp_DeleteAllEmployeePayments",
                parameters
            );
        }

        public async Task<IEnumerable<EmployeePaymentHistoryDto>> GetLatestEmployeePaymentsAsync()
        {
            return await _dapperHelper.QueryAsync<EmployeePaymentHistoryDto>(
                "sp_GetLatestEmployeePayments",
                null,
                System.Data.CommandType.StoredProcedure
            );
        }

    }
}
