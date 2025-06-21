namespace Ecommerce.Application.Features.Suppliers.Commands.ResubmitSupplierDocuments;
public class ResubmitDocumentsCommandHandler(IApplicationDbContext context, ICurrentUser currentUser, DirectoryConfiguration directoryConfiguration) : IRequestHandler<ResubmitDocumentsCommand, ResubmitDocumentsResult>
{
    public async Task<ResubmitDocumentsResult> Handle(ResubmitDocumentsCommand command, CancellationToken cancellationToken)
    {
        SupplierProfile? supplier = await context.SupplierProfiles.Where(s => s.Id == currentUser.SupplierId).FirstOrDefaultAsync(cancellationToken);
        if (supplier is null) throw new ApplicationException("التاجر غير موجود");

        if (supplier.VerificationStatus == VerificationStatus.Rejected)
        {
            byte[] Front = await ConvertToBytesAsync(command.IdFront);
            byte[] Back = await ConvertToBytesAsync(command.IdBack);
            byte[] Tax = await ConvertToBytesAsync(command.TaxCard);

            supplier.NationalIdFrontNameOnServer = Front;
            supplier.NationalIdBackNameOnServer = Back;
            supplier.TaxCardNameOnServer = Tax;
        }

        await context.SaveChangesAsync(cancellationToken);

        return new ResubmitDocumentsResult
        {
            IsSuccess = true
        };
    }

    private async Task<string> SaveFileAsync(IFormFile file)
    {
        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        string filePath = Path.Combine(directoryConfiguration.MediaDirectory, fileName);

        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return fileName;
    }

    private async Task<byte[]> ConvertToBytesAsync(IFormFile file)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        return ms.ToArray();
    }
}