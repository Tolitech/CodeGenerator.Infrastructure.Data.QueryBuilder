using System;
using Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.Tests.Domain.ValueObjects;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.Tests.Domain.Entities
{
    public class VipPersonEntity : PersonEntity
    {
        public PhoneNumberValueObject? PhoneNumber { get; set; }
    }
}
