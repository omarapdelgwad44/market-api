
# **Market API**

## **Introduction**
The Market API provides essential features for managing product inventories, orders, and sales. It's a backend solution designed to streamline e-commerce workflows, track inventory levels, and handle customer transactions efficiently.

---

## **Features**
- **Product Management**: Add, update, delete, and retrieve product details.
- **Order Handling**: Process customer orders and maintain transaction records.
- **Inventory Tracking**: Automatically adjust inventory levels based on sales.
- **Secure Authentication**: Role-based access control using JWT.
- **Search and Filter**: Advanced filtering options for quick data retrieval.

---

## **Technologies Used**
- **Framework**: ASP.NET Core (C#)
- **Database**: SQL Server
- **ORM**: Entity Framework Core (EF Core)
- **Authentication**: JWT (JSON Web Token)
- **Testing**: Postman or Swagger for endpoint validation

---

## **Setup Instructions**
### **Prerequisites**
- .NET SDK (6.0 or later)
- SQL Server installed and running
- Postman (optional) for testing

### **Steps**
1. Clone the repository:
   ```bash
   git clone https://github.com/omarapdelgwad44/market-api.git
   ```
2. Navigate to the project folder:
   ```bash
   cd market-api
   ```
3. Restore dependencies:
   ```bash
   dotnet restore
   ```
4. Configure the database connection:
   - Open `appsettings.json`.
   - Update the connection string in `"ConnectionStrings": { "DefaultConnection": "<your_connection_string>" }`.

5. Run migrations to initialize the database:
   ```bash
   dotnet ef database update
   ```
6. Start the application:
   ```bash
   dotnet run
   ```
   The API will be available at `https://localhost:5001` or `http://localhost:5000`.

---

## **API Documentation**
### **Base URL**
`https://<your-api-domain>/api`

### **Endpoints**

#### **Products**
| Endpoint            | Method | Description            | Auth Required |
|---------------------|--------|------------------------|---------------|
| `/products`         | GET    | Retrieve all products  | No            |
| `/products/{id}`    | GET    | Get a product by ID    | No            |
| `/products`         | POST   | Add a new product      | Yes           |
| `/products/{id}`    | PUT    | Update a product by ID | Yes           |
| `/products/{id}`    | DELETE | Delete a product by ID | Yes           |

#### **Orders**
| Endpoint            | Method | Description            | Auth Required |
|---------------------|--------|------------------------|---------------|
| `/orders`           | GET    | Retrieve all orders    | Yes           |
| `/orders/{id}`      | GET    | Get an order by ID     | Yes           |
| `/orders`           | POST   | Create a new order     | Yes           |

#### **Authentication**
| Endpoint            | Method | Description               |
|---------------------|--------|---------------------------|
| `/auth/register`    | POST   | Register a new user       |
| `/auth/login`       | POST   | Log in and get a JWT token|

### **Example Request**
#### **Retrieve Products**
```http
GET /api/products
Authorization: Bearer <your-token>
```
#### **Sample Response**
```json
[
  {
    "id": 1,
    "name": "Product A",
    "price": 100.00,
    "stock": 50
  },
  {
    "id": 2,
    "name": "Product B",
    "price": 200.00,
    "stock": 30
  }
]
```

---

## **Usage**
1. Use the included [Postman collection](#) for pre-configured requests.
2. Integrate the API into your project by following the provided examples.

---

## **Contributing**
- Fork the repository.
- Create a feature branch: `git checkout -b feature-name`.
- Submit a pull request for review.

---

## **License**
This project is licensed under the MIT License. See the `LICENSE` file for details.
