using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Domain.Common.Models
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
        where TId : notnull
    {
        public TId Id { get; protected set; }
        protected Entity(TId id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            return obj is Entity<TId> entity && Equals(entity.Id);
        }

        public bool Equals(Entity<TId>? other)
        {
            return Equals((object?)other);
        }

        public static bool operator ==(Entity<TId> one, Entity<TId> two)
        {
            return Equals(one, two);
        }

        public static bool operator !=(Entity<TId> one, Entity<TId> two)
        {
            return !Equals(one, two);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #pragma warning disable CS8618
        protected Entity()
        {
        }
        #pragma warning restore CS8618
    }
}
