🛒 ProductInventoryAPI <br />
📦 ProductInventoryAPI is a REST API for managing products and categories in an inventory system. <br />
This project is built with ASP.NET Core 8, using Entity Framework Core and SQL Server Express. <br />

🚀 Technologies used <br />
✅ ASP.NET Core 8.0 – Backend API <br />
✅ Entity Framework Core – ORM for database handling <br />
✅ SQL Server Express – Relational database <br />
✅ Swagger – API documentation <br />

🛠 Other technical details    <br />
✅ Sorting & Pagination  <br />
✅ Repository Pattern   <br />
✅ DTOs  <br /> 
✅ Database Seeding  <br />

📑 How to run the project?<br />
1️⃣ Clone the repository:<br />
git clone (link) <br />
cd ProductInventoryAPI<br />

2️⃣ Install dependencies: <br />
dotnet restore <br />

3️⃣ Update the database: <br />
dotnet ef database update <br />

4️⃣ Run the application: <br />
dotnet run --project ProductInventoryAPI.API <br />

5️⃣ Open Swagger:
https://localhost:7133/swagger <br />

Available API endpoints <br />

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
