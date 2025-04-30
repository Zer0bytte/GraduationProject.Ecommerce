using Ecommerce.Application.Features.Reviews.Commands.AddProductReview;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Endpoints.Reviews;

public class AddProductReviewEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/reviews/", async ([FromForm] AddProductReviewCommand request, ISender sender) =>
        {
            AddProductReviewResult result = await sender.Send(request);
            return Results.Ok(ApiResponse<AddProductReviewResult>.Success(result, ArabicResponseMessages.Reviews.Created));

        })
            .WithTags("Reviews")
            .WithSummary("Add Product Review")
            .RequireAuthorization("User")
            .DisableAntiforgery();
    }
}
