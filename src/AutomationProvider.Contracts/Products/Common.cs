using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Contracts.Products
{
    public record ProductAttributeContract(string Name, string Value);
    public record MoneyResponseContract(decimal Value, string Currency);
}
