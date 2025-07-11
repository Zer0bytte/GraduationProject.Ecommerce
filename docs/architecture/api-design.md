# API Design

## Overview
The Electro API is built using ASP.NET Core Web API following REST principles. The API implements CQRS pattern for better separation of read and write operations, and uses JWT authentication for security.

## Base URL
```
https://api.electro.com/v1
```

## Authentication
All API endpoints (except public ones) require JWT authentication.
```
Authorization: Bearer {token}
```

## API Endpoints

### Authentication
```
POST /api/auth/register
POST /api/auth/login
POST /api/auth/refresh-token
POST /api/auth/forgot-password
POST /api/auth/reset-password
```

### Users
```
GET    /api/users/me
PUT    /api/users/me
GET    /api/users/{id}
GET    /api/users
PUT    /api/users/{id}
DELETE /api/users/{id}
```

### Addresses
```
GET    /api/addresses
POST   /api/addresses
PUT    /api/addresses/{id}
DELETE /api/addresses/{id}
```

### Products
```
GET    /api/products
GET    /api/products/{id}
POST   /api/products
PUT    /api/products/{id}
DELETE /api/products/{id}
GET    /api/products/category/{categoryId}
GET    /api/products/search
```

### Product Images
```
GET    /api/products/{productId}/images
POST   /api/products/{productId}/images
DELETE /api/products/{productId}/images/{imageId}
```

### Product Options
```
GET    /api/products/{productId}/options
POST   /api/products/{productId}/options
PUT    /api/products/{productId}/options/{optionId}
DELETE /api/products/{productId}/options/{optionId}
```

### Product Reviews
```
GET    /api/products/{productId}/reviews
POST   /api/products/{productId}/reviews
PUT    /api/products/{productId}/reviews/{reviewId}
DELETE /api/products/{productId}/reviews/{reviewId}
```

### Categories
```
GET    /api/categories
GET    /api/categories/{id}
POST   /api/categories
PUT    /api/categories/{id}
DELETE /api/categories/{id}
```

### Orders
```
GET    /api/orders
GET    /api/orders/{id}
POST   /api/orders
PUT    /api/orders/{id}/status
GET    /api/orders/user/{userId}
```

### Order Items
```
GET    /api/orders/{orderId}/items
POST   /api/orders/{orderId}/items
PUT    /api/orders/{orderId}/items/{itemId}
DELETE /api/orders/{orderId}/items/{itemId}
```

### Supplier Profiles
```
GET    /api/suppliers
GET    /api/suppliers/{id}
POST   /api/suppliers
PUT    /api/suppliers/{id}
DELETE /api/suppliers/{id}
GET    /api/suppliers/{id}/products
GET    /api/suppliers/{id}/transactions
```

### Supplier Balance Transactions
```
GET    /api/suppliers/{supplierId}/transactions
POST   /api/suppliers/{supplierId}/transactions
```

### Coupon Codes
```
GET    /api/coupons
GET    /api/coupons/{code}
POST   /api/coupons
PUT    /api/coupons/{id}
DELETE /api/coupons/{id}
```

### Conversations
```
GET    /api/conversations
GET    /api/conversations/{id}
POST   /api/conversations
```

### Messages
```
GET    /api/conversations/{conversationId}/messages
POST   /api/conversations/{conversationId}/messages
PUT    /api/conversations/{conversationId}/messages/{messageId}/read
```

## Request/Response Format

### Request Headers
```
Content-Type: application/json
Accept: application/json
Authorization: Bearer {token}
```

### Response Format
```json
{
    "success": true,
    "data": {
        // Response data
    },
    "message": "Operation successful",
    "errors": null
}
```

### Error Response Format
```json
{
    "success": false,
    "data": null,
    "message": "Error message",
    "errors": [
        {
            "field": "fieldName",
            "message": "Error message"
        }
    ]
}
```

## Pagination
All list endpoints support pagination using the following query parameters:
```
?page=1&pageSize=10&sortBy=createdAt&sortOrder=desc
```

## Filtering
Most list endpoints support filtering using query parameters:
```
?categoryId=123&minPrice=10&maxPrice=100&inStock=true
```

## Rate Limiting
- 100 requests per minute for authenticated users
- 20 requests per minute for unauthenticated users

## Caching
- GET requests are cached using Redis
- Cache duration: 5 minutes for product data
- Cache duration: 1 hour for category data

## Versioning
- URL-based versioning (/v1/)
- Version header support (X-API-Version)

## Security
- JWT authentication
- HTTPS only
- CORS enabled for specific origins
- Rate limiting
- Input validation
- SQL injection prevention
- XSS protection

## Error Codes
- 200: Success
- 201: Created
- 400: Bad Request
- 401: Unauthorized
- 403: Forbidden
- 404: Not Found
- 422: Validation Error
- 429: Too Many Requests
- 500: Internal Server Error

## Webhooks
```
POST /api/webhooks/order-created
POST /api/webhooks/payment-received
POST /api/webhooks/shipment-updated
```

## Documentation
- Swagger/OpenAPI documentation available at `/swagger`
- API documentation available at `/docs` 