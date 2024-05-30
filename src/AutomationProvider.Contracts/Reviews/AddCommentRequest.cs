using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Contracts.Reviews
{
    public record AddCommentRequest(
        Guid ProductId,
        Guid? ParentReviewId,
        string CustomerName,
        string Comment,
        int? Rating);
}
