using PIS.Common.Context;

namespace PIS.Common.Services
{
    public class TenantContextAccessor : ITenantContextAccessor
    {
        public TenantContext TenantContext { get; set; }

    }
}
