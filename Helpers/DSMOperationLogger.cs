using System;
using System.Threading.Tasks;
using DSM.UI.Api.Models.LogModels;

namespace DSM.UI.Api.Helpers
{
    public interface IDSMOperationLogger
    {
        Task LogOperationToDbAsync(OperationLog operationLog);
    }
    public class DSMOperationLogger : IDSMOperationLogger
    {
        private readonly DSMStorageDataContext _context;

        public DSMOperationLogger(DSMStorageDataContext context)
        {
            _context = context; 
        }

        public async Task LogOperationToDbAsync(OperationLog operationLog)
        {
            operationLog.LogDate = DateTime.Now;
            await _context.OperationLogs.AddAsync(operationLog);
            await _context.SaveChangesAsync();
        }
    }
}