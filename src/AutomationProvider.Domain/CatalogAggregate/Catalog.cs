using AutomationProvider.Domain.CatalogAggregate.ValueObjects;
using AutomationProvider.Domain.Common.Enums;
using AutomationProvider.Domain.Common.Models;
using System.Linq;

namespace AutomationProvider.Domain.CatalogAggregate
{
    public sealed class Catalog : AggregateRoot<Guid>
    {
        private readonly List<Catalog> _subCatalogs = new List<Catalog>();
        private readonly List<Guid> _products = new List<Guid>();
        private readonly List<Attributes> _availableProductAttributes = new List<Attributes>();
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Catalog? Parent { get; private set; }
        public IReadOnlyList<Catalog> SubCatalogs => _subCatalogs.AsReadOnly();
        public IReadOnlyList<Guid> Products => _products.AsReadOnly();
        public IReadOnlyList<Attributes> AvailableProductAttributes => _availableProductAttributes.AsReadOnly();
        private Catalog(Guid id, string name, string description, Catalog? parent, List<Attributes> attributes) : base(id)
        {
            Name = name;
            Description = description;
            Parent = parent;
            _availableProductAttributes = attributes;
        }
        public static Catalog Create(string name, string description, Catalog? parent, List<Attributes> attributes)
        {
            return new Catalog(Guid.NewGuid(), name, description, parent, attributes);
        }

        public void AddAttributes(Attributes attributes)
        {
            _availableProductAttributes.Add(attributes);
        }

        public void SetParent(Catalog catalog)
        {
            if (CheckIfCatalogIsChildren(this, catalog.Id))
                Parent = catalog;
        }

        private bool CheckIfCatalogIsChildren(Catalog parent, Guid rootId)
        {
            if (parent.SubCatalogs.Any(c => c.Id == rootId))
            {
                return true;
            }
            else
            {
                foreach (var subCatalog in parent.SubCatalogs)
                {
                    if (CheckIfCatalogIsChildren(subCatalog, rootId))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public static void DeleteCatalog(Catalog catalog)
        {
            if (catalog.SubCatalogs.Count() > 0)
            {
                foreach(var subCatalog in catalog.SubCatalogs)
                {
                    subCatalog.Parent = catalog.Parent;
                }
            }
        }

        public void SetName(string name)
        {
            if (name.Length > 0 && name.Length < 50)
                Name = name;
        }

        public void SetDescription(string description)
        {
            if (description.Length > 0 && description.Length < 300)
                Description = description;
        }

        public void AddSubCatalog(Catalog subCatalog)
        {
            _subCatalogs.Add(subCatalog);
            subCatalog.SetParent(this);
        }

#pragma warning disable CS8618
#pragma warning disable CS0628
        protected Catalog()
        {
        }
#pragma warning restore CS0628
#pragma warning restore CS8618
    }
}
