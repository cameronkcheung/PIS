namespace PIS.Domain.Exceptions
{
    public class ManufacturerAlreadyExistsException : Exception
    {
        public ManufacturerAlreadyExistsException(string message)
            : base(message)
        {
        }
    }
}
