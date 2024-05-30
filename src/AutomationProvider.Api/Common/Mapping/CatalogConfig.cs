using AutomationProvider.Contracts.Catalog;
using AutomationProvider.Domain.CatalogAggregate;
using AutomationProvider.Domain.CatalogAggregate.ValueObjects;
using Mapster;
using System.ComponentModel;
using System.Linq;

namespace AutomationProvider.Api.Commmon.Mapping
{
    public class CatalogConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Catalog, CatalogResponse>()
                  .Map(dest => dest.Id, src => src.Id)
                  .Map(dest => dest.Name, src => src.Name);

            config.NewConfig<Catalog, CatalogDetailsResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.AvailableProductAttributes, src => src.AvailableProductAttributes
                    .Adapt<List<AttributesResponse>>());

            config.NewConfig<Attributes, AttributesResponse>()
                .Map(dest => dest.NameWithUnits, src => src.Unit != null ? $"{src.Name} [{src.Unit}]" : src.Name)
                .Map(dest => dest.IsRangeAttribute, src => src.IsRangeAttribute)
                .Map(dest => dest.Type, src => src.Type)
                .Map(dest => dest.AvailableValues, src => src.Values.Select(v => v.Value.ToString()).ToList());
        }
    }
}
