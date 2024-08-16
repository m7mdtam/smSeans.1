using System;
using System.Threading.Tasks;

public interface IAuditLogRepository
{
    Task<AuditLog> GetAuditLogByIdAsync(int id);
    Task<int> CreateAuditLogAsync(AuditLog auditLog);
    Task<bool> UpdateAuditLogAsync(AuditLog auditLog);
    Task <bool>DeleteAuditLogAsync(int id);
}
