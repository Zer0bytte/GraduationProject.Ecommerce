using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Suppliers.Queries.GetCurrentSupplier;
public class GetCurrentSupplierResult
{
    public Guid Id { get; set; }
    public string BusinessName { get; set; } = default!;
    public string StoreName { get; set; } = default!;
    public VerificationStatus VerificationStatus { get; set; }
}