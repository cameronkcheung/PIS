﻿namespace PIS.Domain.Exceptions
{
    public class ProductAlreadyExistsException : Exception
    {
        public ProductAlreadyExistsException(string message)
            : base(message)
        {
        }
    }
}
