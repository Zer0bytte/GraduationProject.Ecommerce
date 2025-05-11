using Ecommerce.Application.Common.Persistance.Cursor;
using Ecommerce.Domain.Entities;
using Google.Apis.Auth;
using MassTransit.Internals;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using TagLib.Ape;

namespace Ecommerce.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<GetAllProductsQuery, CursorResult<GetAllProductsResult>>
{
    public async Task<CursorResult<GetAllProductsResult>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
    {
        HttpRequest? httpRequest = httpContextAccessor.HttpContext?.Request;
        string imageUrl = httpRequest?.Scheme + "://" + httpRequest?.Host + "/media/";



        //var productssss = await context.Products
        //.Include(p => p.Images)
        //.Include(p => p.Options)
        //.ToListAsync(cancellationToken);

        //int number = productssss.Count + 1;
        //context.Products.RemoveRange(productssss);
        //foreach (var original in productssss)
        //{
        //    var duplicate = new Product
        //    {
        //        Id = Guid.Parse(NewId.Next().ToString()),
        //        Title = original.Title,
        //        Price = original.Price,
        //        Discount = original.Discount,
        //        Stock = original.Stock,
        //        SKU = $"{original.SKU}",
        //        Tags = original.Tags,
        //        Brand = original.Brand,
        //        Description = original.Description,
        //        CategoryId = original.CategoryId,
        //        SupplierId = original.SupplierId,
        //    };
        //    duplicate.Images = original.Images.Select(img => new ProductImage
        //    {
        //        Id = Guid.NewGuid(),
        //        NameOnServer = img.NameOnServer,
        //        ProductId = duplicate.Id
        //    }).ToList();
        //    duplicate.Options = original.Options.Select(opt => new ProductOption
        //    {
        //        Id = Guid.NewGuid(),
        //        OptionGroupName = opt.OptionGroupName,
        //        OptionName = opt.OptionName,
        //        ProductId = duplicate.Id
        //    }).ToList();
        //    number++;
        //    context.Products.Add(duplicate);
        //}

        //await context.SaveChangesAsync(cancellationToken);


        IQueryable<Product> baseQuery = context.Products.AsQueryable();
        if (!string.IsNullOrWhiteSpace(query.SearchQuery))
            baseQuery = baseQuery.Where(prd => prd.Title.Contains(query.SearchQuery) || prd.Tags.Contains(query.SearchQuery));

        if (query.HasDiscount.HasValue && query.HasDiscount.Value)
        {
            baseQuery = baseQuery.Where(prd => prd.Discount > 0);
        }

        if (query.MinimumPrice.HasValue)
        {
            baseQuery = baseQuery.Where(prd =>
                prd.Price * (1 - prd.Discount / 100m) >= query.MinimumPrice.Value);
        }

        if (query.MaximumPrice.HasValue)
        {
            baseQuery = baseQuery.Where(prd =>
                prd.Price * (1 - prd.Discount / 100m) <= query.MaximumPrice.Value);
        }

        if (query.CategoryId.HasValue && query.CategoryId != Guid.Empty)
        {
            baseQuery = baseQuery.Where(p => p.CategoryId == query.CategoryId);
        }

        if (!string.IsNullOrWhiteSpace(query.OptionGroupName) && !string.IsNullOrWhiteSpace(query.OptionValue))
        {
            baseQuery = baseQuery.Where(product => product.Options.Any(option => option.OptionGroupName == query.OptionGroupName && option.OptionName == query.OptionValue));
        }
        if (!string.IsNullOrWhiteSpace(query.Cursor))
        {
            var decodedCursor = Cursor.Decode(query.Cursor);
            if (decodedCursor is not null)
            {
                baseQuery = baseQuery.Where(x => x.CreatedOn < decodedCursor.Date || x.CreatedOn == decodedCursor.Date && x.Id <= decodedCursor.LastId);
            }



        }
        var products = await baseQuery
            .OrderByDescending(x => x.CreatedOn)
            .ThenByDescending(x => x.Id)
            .Select(p => new GetAllProductsResult
            {
                Id = p.Id,
                SupplierId = p.Supplier.UserId,
                SupplierName = p.Supplier.StoreName,
                Title = p.Title,
                Price = p.Price,
                DiscountedPrice = p.Discount >= 1 ? p.Price * (1 - p.Discount / 100m) : 0,
                DiscountPercentage = p.Discount,
                Description = p.Description,
                Images = p.Images.Select(p => imageUrl + p.NameOnServer).ToArray(),
                Category = p.Category.Name,
                CreatedOn = p.CreatedOn
            }).Take(query.Limit + 1).ToListAsync(cancellationToken);

        var prdFinal = products.Take(query.Limit).ToList();

        var hasMore = products.Count > query.Limit;
        DateTime? nextDate = products.Count > query.Limit ? products[^1].CreatedOn : null;
        Guid? nextId = products.Count > query.Limit ? products[^1].Id : null;

        var result = new CursorResult<GetAllProductsResult>
        {
            Items = prdFinal,
            Cursor = nextDate is not null && nextId is not null ? Cursor.Encode(nextDate.Value, nextId.Value) : null,
            HasMore = hasMore
        };

        return result;


    }


}