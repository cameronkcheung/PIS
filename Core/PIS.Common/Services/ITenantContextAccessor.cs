using PIS.Common.Context;

namespace PIS.Common.Services
{
    public interface ITenantContextAccessor
    {
        TenantContext TenantContext { get; set; }
    }
}
