using AutomationProvider.Domain.Common.Enums;
using AutomationProvider.Domain.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Application.Common.Interfaces.Services
{
    public interface ICurrencyConverter
    {
        Task<Money> Convert(Money source, Currency targetCurrency);
    }
}
