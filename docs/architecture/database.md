# Database Design

## Overview
The Electro platform uses a relational database (SQL Server) with Entity Framework Core as the ORM. The database design follows Clean Architecture principles and is optimized for electronic components e-commerce operations.

## Core Entities

### 1. AppUser
```sql
AppUser
- Id (PK)
- UserName
- Email
- PasswordHash
- FirstName
- LastName
- PhoneNumber
- CreatedAt
- UpdatedAt
- IsActive
```

### 2. Address
```sql
Address
- Id (PK)
- UserId (FK)
- Street
- City
- State
- Country
- ZipCode
- IsDefault
- CreatedAt
- UpdatedAt
```

### 3. Product
```sql
Product
- Id (PK)
- Name
- Description
- Price
- StockQuantity
- CategoryId (FK)
- SupplierId (FK)
- CreatedAt
- UpdatedAt
- IsActive
```

### 4. Category
```sql
Category
- Id (PK)
- Name
- Description
- ParentCategoryId (FK, self-referencing)
- CreatedAt
- UpdatedAt
```

### 5. ProductImage
```sql
ProductImage
- Id (PK)
- ProductId (FK)
- ImageUrl
- IsMain
```

### 6. ProductOption
```sql
ProductOption
- Id (PK)
- ProductId (FK)
- Name
- Value
```

### 7. ProductReview
```sql
ProductReview
- Id (PK)
- ProductId (FK)
- UserId (FK)
- Rating
- Comment
- CreatedAt
```

### 8. Order
```sql
Order
- Id (PK)
- UserId (FK)
- OrderDate
- Status
- TotalAmount
- ShippingAddress
- BillingAddress
- PaymentStatus
- CreatedAt
- UpdatedAt
```

### 9. OrderItem
```sql
OrderItem
- Id (PK)
- OrderId (FK)
- ProductId (FK)
- Quantity
- UnitPrice
- CreatedAt
- UpdatedAt
```

### 10. SupplierProfile
```sql
SupplierProfile
- Id (PK)
- UserId (FK)
- CompanyName
- Description
- Website
- ContactInfo
- Balance
- CreatedAt
- UpdatedAt
```

### 11. SupplierBalanceTransaction
```sql
SupplierBalanceTransaction
- Id (PK)
- SupplierId (FK)
- Amount
- Type
- Description
- CreatedAt
```

### 12. CouponCode
```sql
CouponCode
- Id (PK)
- Code
- DiscountPercentage
- ValidFrom
- ValidTo
- IsActive
- CreatedAt
- UpdatedAt
```

### 13. Conversation
```sql
Conversation
- Id (PK)
- UserId (FK)
- SupplierId (FK)
- CreatedAt
- UpdatedAt
```

### 14. Message
```sql
Message
- Id (PK)
- ConversationId (FK)
- SenderId (FK)
- Content
- CreatedAt
- IsRead
```

### 15. RefreshToken
```sql
RefreshToken
- Id (PK)
- UserId (FK)
- Token
- ExpiryDate
```

### 16. TransactionReference
```sql
TransactionReference
- Id (PK)
- OrderId (FK)
- ReferenceNumber
- PaymentProvider
- Status
- CreatedAt
- UpdatedAt
```

## Relationships

1. **User-Address Relationship**
   - One-to-Many: One User can have many Addresses
   - Foreign Key: Address.UserId references AppUser.Id

2. **Product-Category Relationship**
   - Many-to-One: Many Products can belong to one Category
   - Foreign Key: Product.CategoryId references Category.Id

3. **Category-ParentCategory Relationship**
   - Self-referencing: Categories can have parent categories
   - Foreign Key: Category.ParentCategoryId references Category.Id

4. **Product-Supplier Relationship**
   - Many-to-One: Many Products can belong to one Supplier
   - Foreign Key: Product.SupplierId references SupplierProfile.Id

5. **Product-Image Relationship**
   - One-to-Many: One Product can have many Images
   - Foreign Key: ProductImage.ProductId references Product.Id

6. **Product-Option Relationship**
   - One-to-Many: One Product can have many Options
   - Foreign Key: ProductOption.ProductId references Product.Id

7. **Product-Review Relationship**
   - One-to-Many: One Product can have many Reviews
   - Foreign Key: ProductReview.ProductId references Product.Id

8. **Order-User Relationship**
   - Many-to-One: One User can have many Orders
   - Foreign Key: Order.UserId references AppUser.Id

9. **Order-OrderItem Relationship**
   - One-to-Many: One Order can have many OrderItems
   - Foreign Key: OrderItem.OrderId references Order.Id

10. **Supplier-Transaction Relationship**
    - One-to-Many: One Supplier can have many Transactions
    - Foreign Key: SupplierBalanceTransaction.SupplierId references SupplierProfile.Id

11. **Conversation-User Relationship**
    - Many-to-One: One User can have many Conversations
    - Foreign Key: Conversation.UserId references AppUser.Id

12. **Message-Conversation Relationship**
    - One-to-Many: One Conversation can have many Messages
    - Foreign Key: Message.ConversationId references Conversation.Id

## Indexes

1. **Product Indexes**
   - CategoryId
   - SupplierId
   - Name (Full-text search)

2. **Order Indexes**
   - UserId
   - OrderDate
   - Status

3. **User Indexes**
   - Email (Unique)
   - UserName (Unique)

4. **Category Indexes**
   - ParentCategoryId
   - Name

## Constraints

1. **Product Constraints**
   - Price must be positive
   - StockQuantity must be non-negative

2. **Order Constraints**
   - TotalAmount must be positive
   - Status must be valid enum value

3. **User Constraints**
   - Email must be unique
   - UserName must be unique
   - PasswordHash must not be null

## Data Types

1. **Common Types**
   - Id: GUID
   - CreatedAt: DateTime
   - UpdatedAt: DateTime
   - IsActive: Boolean

2. **Product Types**
   - Price: Decimal(18,2)
   - StockQuantity: Integer

3. **User Types**
   - Email: NVARCHAR(255)
   - PasswordHash: NVARCHAR(MAX)
   - PhoneNumber: NVARCHAR(20)

## Caching Strategy

1. **Redis Caching**
   - Product catalog
   - Category hierarchy
   - User sessions

2. **Cache Keys**
   - Products: "product:{id}"
   - Categories: "category:{id}"
   - User sessions: "session:{userId}"

## Data Migration

1. **Migration Strategy**
   - Code-first migrations
   - Version control for schema changes
   - Data seeding for initial setup

2. **Backup Strategy**
   - Daily full backups
   - Transaction log backups
   - Point-in-time recovery 