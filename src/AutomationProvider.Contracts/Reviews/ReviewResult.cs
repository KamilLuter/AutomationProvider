using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Contracts.Reviews
{
    public record ReviewResult(
        Guid ReviewId,
        Guid ProductId,
        Guid? ParentReviewId,
        Guid? CustomerId,
        string CustomerName,
        string Comment,
        int? Rating);
}
