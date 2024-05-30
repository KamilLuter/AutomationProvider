using AutomationProvider.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Domain.UserAggregate.ValueObjects
{
    public sealed class UserId : ValueObjectId<UserId>
    {
        private UserId(Guid value) : base(value)
        {
        }
        public static UserId CreateUnique()
        {
            return new UserId(Guid.NewGuid());
        }
    }
}
