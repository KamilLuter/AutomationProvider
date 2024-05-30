using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Infrastructure.Persistance.Configurations.Extensions
{
    public static class ValueConversionExtensions
    {
        public static PropertyBuilder<IReadOnlyList<string>> HasJsonConversionForReadOnlyList(this PropertyBuilder<IReadOnlyList<string>> propertyBuilder)
        {
            ValueConverter<IReadOnlyList<string>, string> converter = new ValueConverter<IReadOnlyList<string>, string>
            (
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject< IReadOnlyList<string>> (v) ?? new List<string>()
            );

            ValueComparer<IReadOnlyList<string>> comparer = new ValueComparer<IReadOnlyList<string>>
            (
                (l, r) => JsonConvert.SerializeObject(l) == JsonConvert.SerializeObject(r),
                v => v == null ? 0 : JsonConvert.SerializeObject(v).GetHashCode(),
                v => JsonConvert.DeserializeObject<IReadOnlyList<string>>(JsonConvert.SerializeObject(v))
            );

            propertyBuilder.HasConversion(converter);
            propertyBuilder.Metadata.SetValueConverter(converter);
            propertyBuilder.Metadata.SetValueComparer(comparer);
            propertyBuilder.HasColumnType("nvarchar(max)");

            return propertyBuilder;
        }
    }
}
