# IronBookStoreAuth
Research &amp; testing different Authentication/Authorization techniques with ASP.NET Core - API

# Business - Iron Book Store

### Books
1. /api/books - GET - Authorize Roles: GeneralManager or Administrator
1. /api/books/{bookId} - GET - Authorize Roles: GeneralManager or Administrator
2. /api/books - POST - Authorize Roles: GeneralManager or Administrator
3. /api/books/{bookId}  - PUT - Authorize Roles: GeneralManager or Administrator
4. /api/books/{bookId}  - DELETE - Authorize Roles: Administrator. In addition, an administrator must have created the book to delete it (implement policy to check it).

### Auth
1. api/auth/login - POST - Anonymous - Return JWT
2. api/auth/register - POST - Anonymous - register new user with Roles: GeneralManager & Administrator

##Users
1. api/users - GET - Authorize Roles: Administrator or SecurityManager - Return all users


# Project: IronBookStoreAuthJWT
## JSON WEB TOKENS - Net Core 3.0

1. Api Project - ASP.NET CORE 3.0
2. Using JWT for Authentication & Authorization | Without Identity
3. Implementing Role-based | Policy-based (simple&complex requirements) Authorization
4. Entity framework core 
5. Repository pattern