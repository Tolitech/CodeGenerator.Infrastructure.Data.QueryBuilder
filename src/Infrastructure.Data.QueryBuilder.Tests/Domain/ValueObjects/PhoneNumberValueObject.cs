using System;
using Tolitech.CodeGenerator.Domain.ValueObjects;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.Tests.Domain.ValueObjects
{
    public  class PhoneNumberValueObject : ValueObject
    {
        public string? AreaCode { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
