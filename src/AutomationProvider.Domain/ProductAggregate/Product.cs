using AutomationProvider.Domain.Common.Enums;
using AutomationProvider.Domain.Common.Models;
using AutomationProvider.Domain.Common.ValueObjects;

namespace AutomationProvider.Domain.Product
{
    public sealed class Product : AggregateRoot<Guid>
    {
        private readonly Dictionary<string, object>? _productAttributes = new Dictionary<string, object>();
        private readonly List<Guid>? _productReviews = new List<Guid>();
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Money Price { get; private set; }
        public Guid CatalogId { get; private set; }
        public decimal AvgRating { get; private set; }
        public string Manufacturer { get; private set; }
        public IReadOnlyDictionary<string, object>? ProductAttributes => _productAttributes?.AsReadOnly();
        public IReadOnlyList<Guid>? ProductReviews => _productReviews?.AsReadOnly();
        private Product(Guid productId
                        , string name
                        , string description
                        , string manufacturer
                        , Money price
                        , Guid catalogId
                        , Dictionary<string, object> productAttributes
                        , List<Guid>? productReviews
                        )
            : base(productId)
        {
            Id = productId;
            Name = name;
            Description = description;
            CatalogId = catalogId;
            Price = price;
            _productAttributes = productAttributes;
            _productReviews = productReviews;
            Manufacturer = manufacturer;
        }
        public static Product Create(string name
                                    , string description
                                    , string manufacturer
                                    , Guid catalogId
                                    , Money price
                                    , Dictionary<string, object> productAttributes
                                    , List<Guid>? productReviews = null)
        {
            return new Product(
                        Guid.NewGuid(), 
                        name, 
                        description,
                        manufacturer,
                        price, 
                        catalogId, 
                        productAttributes,
                        productReviews);
        }
        #pragma warning disable CS8618
        protected Product()
        {
        }
        #pragma warning restore CS8618
}
}
