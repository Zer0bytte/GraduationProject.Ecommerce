using Ecommerce.Application.Features.Authentication.Commands.RegisterUser;

namespace Ecommerce.Application.Features.Authentication.Commands.RegisterSupplier;

public class RegisterSupplierCommandHandler(IApplicationDbContext context,
    UserManager<AppUser> userManager,
    DirectoryConfiguration directoryConfiguration, IBus bus)
    : IRequestHandler<RegisterSupplierCommand, RegisterSupplierResult>
{
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
    public async Task<RegisterSupplierResult> Handle(RegisterSupplierCommand command, CancellationToken cancellationToken)
    {
        RegisterUserCommandHandler registerCommand = new RegisterUserCommandHandler(userManager, bus, true);

        RegisterUserResult registerResult = await registerCommand.Handle(
            new RegisterUserCommand
            {
                Email = command.Email,
                FullName = command.FullName,
                Password = command.Password,
                PhoneNumber = command.PhoneNumber
            },
            cancellationToken
        );

        if (registerResult.UserId != Guid.Empty)
        {
            byte[] nameFront = await ConvertToBytesAsync(command.NationalIdFront);
            byte[] nameBack = await ConvertToBytesAsync(command.NationalIdBack);
            byte[] nameTax = await ConvertToBytesAsync(command.TaxCard);


            SupplierProfile supplierProfile = new SupplierProfile
            {
                Id = Guid.NewGuid(),
                UserId = registerResult.UserId,
                BusinessName = command.BusinessName,
                NationalId = command.NationalId,
                TaxNumber = command.TaxNumber,
                StoreName = command.StoreName,
                Governorate = command.Governorate,
                NationalIdFrontNameOnServer = nameFront,
                NationalIdBackNameOnServer = nameBack,
                TaxCardNameOnServer = nameTax
            };

            AppUser? user = await userManager.FindByIdAsync(registerResult.UserId.ToString());
            user!.SupplierProfileId = supplierProfile.Id;
            user.IsSeller = true;
            context.SupplierProfiles.Add(supplierProfile);
            await context.SaveChangesAsync(cancellationToken);

            return new RegisterSupplierResult
            {
                UserName = registerResult.UserName,
                UserId = registerResult.UserId
            };
        }


        return new RegisterSupplierResult
        {
            Errors = registerResult.Errors,
        };


    }
}
