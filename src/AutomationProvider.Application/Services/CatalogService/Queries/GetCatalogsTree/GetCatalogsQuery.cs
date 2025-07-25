﻿using AutomationProvider.Domain.CatalogAggregate;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Application.Services.CatalogService.Queries.GetSubCatalogs
{
    public record GetCatalogsQuery() : IRequest<ErrorOr<IEnumerable<Catalog>?>>;
}
