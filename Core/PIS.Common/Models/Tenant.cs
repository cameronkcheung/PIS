namespace PIS.Common.Models
{
    public record Tenant
    {
        public string Name { get; set; }
        public List<Setting> Settings { get; set; }
    }
}
