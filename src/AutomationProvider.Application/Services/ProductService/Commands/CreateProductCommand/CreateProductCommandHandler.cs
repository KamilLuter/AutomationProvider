using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Application.Services.ProductService.Commands.Common;
using AutomationProvider.Domain.Common.Errors;
using AutomationProvider.Domain.Product;
using ErrorOr;
using MediatR;

namespace AutomationProvider.Application.Services.ProductService.Commands.CreateProductCommand
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<ProductResult>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICatalogRepository _catalogRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IProductRepository productRepository,ICatalogRepository catalogRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _catalogRepository = catalogRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<ProductResult>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var catalog = await _catalogRepository.GetCatalogByIdAsync(request.CategoryId, cancellationToken);

            if (catalog is null)
            {
                return Errors.Catalog.NotFound;
            }


            foreach (var attribute in request.ProductAttributes)
            {
                var key = attribute.Key;

                var att = catalog.AvailableProductAttributes.FirstOrDefault(attr => attr.Type.ToString().Equals(key, StringComparison.OrdinalIgnoreCase));

                if (att is null)
                {
                    return Errors.Product.WrongAttributeName;
                }
                  

                if (att.IsRangeAttribute)
                {
                    if (!double.TryParse(attribute.Value.ToString().Replace('.', ','), out var _))
                    {
                        return Errors.Product.WrongNumericAttributeValue;
                    }
                }
                else
                {
                    if (!att.Values.Any(v => v.Value.Equals(attribute.Value.ToString())))
                    {
                        return Errors.Product.WrongEnumAttributeValue;
                    }
                }
            }

            var product = Product.Create(
                request.Name,
                request.Description,
                request.Manufacturer,
                request.CategoryId,
                request.Price,
                request.ProductAttributes
                );

            var productResult = await _productRepository.AddProductAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (productResult is null)
            { 
                return Errors.Product.CreateError; 
            }

            ProductResult result = new ProductResult(productResult, catalog.Name);

            return result;
        }
    }
}
