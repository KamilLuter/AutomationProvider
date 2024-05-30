using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Domain.Common.Models
{
    public abstract class ValueObjectId<T> : ValueObject where T : ValueObjectId<T>
    {
        public Guid Value { get; private set; }

        protected ValueObjectId(Guid value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

    }
}
