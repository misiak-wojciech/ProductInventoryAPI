### ProductInventoryAPI
📦 ProductInventoryAPI is a REST API for managing products and categories in an inventory system. <br />
This project is built with ASP.NET Core 8, using Entity Framework Core and SQL Server Express. <br />

### Technologies used <br />
✅ ASP.NET Core 8.0 – Backend API <br />
✅ Entity Framework Core – ORM for database handling <br />
✅ SQL Server Express – Relational database <br />
✅ Swagger – API documentation <br />

### Other technical details    <br />
✅ Sorting & Pagination  <br />
✅ Repository Pattern   <br />
✅ DTOs  <br /> 
✅ Database Seeding  <br />

## Setup

Follow these steps to set up and run the project locally:

### 1. Clone the repository

```bash
git clone https://github.com/misiak-wojciech/ProductInventoryAPI.git  
```
### 2. Restore dependencies
Restore the project dependencies using the following command:
```
dotnet restore
```
### 3. Add migrations
Before updating the database, we need to add migrations based on the current model:
```
dotnet ef migrations add InitialCreate
```
This will generate the migration files required to set up the database schema.

### 4. Update the database
Once the migration is added, update the database with:
```
dotnet ef database update
```
This command will apply all the migrations and set up the database for the application.

### 5. Run the application
Now, you are ready to run the application:
```
dotnet run
```
This will start the application, and you can access it in your browser at localhost.

## Database Configuration
If you are using SQL Server Express, make sure to have a connection string in the appsettings.json file. Since you mentioned that appsettings.json is not included in the repository, you will need to manually configure it. 

## Available API endpoints <br />

📌 /api/Product – Product management <br />

GET	/api/Product	Get all products (with pagination & sorting) <br />
GET	/api/Product/{id}	Get product by ID <br /> 
GET	/api/Product/category/{id}	Get products by category ID <br />
GET	/api/Product/available	Get only available products <br />
GET	/api/Product/search?query=	Search products by name <br />
POST	/api/Product	Create a new product <br />
PUT	/api/Product/{id}	Update an existing product <br />
DELETE	/api/Product/{id}	Delete a product <br />

📌 /api/Category – Category management  <br />

GET	/api/Category	Get all categories (with pagination & sorting) <br />
GET	/api/Category/{id}	Get category by ID <br />
GET	/api/Category/with-product-count	Get categories with product count <br />
POST	/api/Category	Create a new category <br />
PUT	/api/Category/{id}	Update an existing category <br />
DELETE	/api/Category/{id}	Delete a category <br />
