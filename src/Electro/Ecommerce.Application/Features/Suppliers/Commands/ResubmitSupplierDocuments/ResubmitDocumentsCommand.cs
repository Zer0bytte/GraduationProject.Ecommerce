using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Suppliers.Commands.ResubmitSupplierDocuments;
public class ResubmitDocumentsCommand : IRequest<ResubmitDocumentsResult>
{
    public IFormFile IdFront { get; set; }
    public IFormFile IdBack { get; set; }
    public IFormFile TaxCard { get; set; }

}