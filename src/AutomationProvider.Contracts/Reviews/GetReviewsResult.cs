using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Contracts.Reviews
{
    public record GetReviewsResult(
        Guid ReviewId,
        Guid ProductId,
        Guid? CustomerId,
        List<ReviewResult> SubComments,
        string CustomerName,
        string Comment,
        int? Rating);
}
