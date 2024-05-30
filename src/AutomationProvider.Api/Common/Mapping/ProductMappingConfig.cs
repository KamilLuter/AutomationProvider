using AutomationProvider.Application.Services.ProductService.Commands.Common;
using AutomationProvider.Application.Services.ProductService.Commands.CreateProductCommand;
using AutomationProvider.Contracts.Products;
using AutomationProvider.Domain.Common.Enums;
using AutomationProvider.Domain.Common.ValueObjects;
using AutomationProvider.Domain.Product;
using Mapster;
using MediatR;
using Newtonsoft.Json.Linq;
using System.Data;

namespace AutomationProvider.Api.Commmon.Mapping
{
    public class ProductMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ProductResult, GetProductResponse>()
                .Map(dest => dest.Name, src => src.Product.Name)
                .Map(dest => dest.Price, src => new MoneyResponseContract(src.Product.Price.Value, src.Product.Price.Currency.ToString()))
                .Map(dest => dest.Id, src => src.Product.Id)
                .Map(dest => dest.Description, src => src.Product.Description)
                .Map(dest => dest.CatalogName, src => src.CategoryName)
                .Map(dest => dest.CatalogId, src => src.Product.CatalogId)
                .Map(dest => dest.ProductAttributes, src => GetModifiedProductAttributes(src.Product.ProductAttributes));

            config.NewConfig<(CreateProductRequest req, Guid reqId), CreateProductCommand>()
                .Map(dest => dest.requestId, src => src.reqId)
                .Map(dest => dest.Name, src => src.req.Name)
                .Map(dest => dest.Description, src => src.req.Description)
                .Map(dest => dest.CategoryId, src => src.req.CatalogId)
                .Map(dest => dest.ProductAttributes, src => GetProductAttributesEnumStrings(src.req.ProductAttributes))
                .Map(dest => dest.Price, src => src.req.Price).MapToConstructor(true);

            config.NewConfig<MoneyResponseContract, Money>()
                .ConstructUsing(src => Money.Create(src.Value, Enum.Parse<Currency>(src.Currency)).Value);
        }
        private Dictionary<string, object> GetModifiedProductAttributes(IReadOnlyDictionary<string, object>? productAttributes)
        {
            var modifiedAttributes = new Dictionary<string, object>();

            if (productAttributes is null) 
            {
                return modifiedAttributes;
            }

            foreach (var kvp in productAttributes)
            {
                var modifiedKey = kvp.Key.Replace('_', ' '); 
                modifiedAttributes.Add(modifiedKey, kvp.Value);
            }

            return modifiedAttributes;
        }

        private Dictionary<string, object> GetProductAttributesEnumStrings(IReadOnlyDictionary<string, object> productAttributes)
        {
            var modifiedAttributes = new Dictionary<string, object>();
            foreach (var kvp in productAttributes)
            {
                var modifiedKey = kvp.Key.Replace(' ', '_');
                modifiedAttributes.Add(modifiedKey, kvp.Value);
            }
            return modifiedAttributes;
        }
    }
}