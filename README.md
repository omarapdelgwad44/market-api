
# **Market API**

## **Introduction**
The Market API is a robust backend solution for managing product inventories, customer orders, and sales processes. Designed for e-commerce platforms, it helps streamline workflows, track inventory, and handle transactions efficiently.

---

## **Features**
- **Product Management**: Create, update, delete, and retrieve product details.
- **Order Handling**: Process customer orders with detailed transaction records.
- **Inventory Tracking**: Automatically adjust stock levels based on sales.
- **Secure Authentication**: Role-based access control with JWT authentication.
- **Search and Filter**: Powerful filtering options for efficient data retrieval.

---

## **Technologies Used**
- **Framework**: ASP.NET Core (C#)
- **Database**: SQL Server
- **ORM**: Entity Framework Core (EF Core)
- **Authentication**: JWT (JSON Web Token)
- **Testing Tools**: Postman and Swagger for endpoint validation

---

## **Setup Instructions**
### **Prerequisites**
- .NET SDK (6.0 or later)
- SQL Server installed and running
- Postman (optional) for API testing

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
   - Update the connection string under `"ConnectionStrings": { "DefaultConnection": "<your_connection_string>" }`.

5. Run migrations to initialize the database:
   ```bash
   dotnet ef database update
   ```
6. Start the application:
   ```bash
   dotnet run
   ```
   The API will be available at:
   - `https://localhost:5001` (HTTPS)
   - `http://localhost:5000` (HTTP)

---

## **API Documentation**
### **Base URL**
`https://<your-api-domain>/api`

### **Endpoints**

#### **Products**
| Endpoint            | Method | Description             | Auth Required |
|---------------------|--------|-------------------------|---------------|
| `/products`         | GET    | Retrieve all products   | No            |
| `/products/{id}`    | GET    | Retrieve a product by ID| No            |
| `/products`         | POST   | Add a new product       | Yes           |
| `/products/{id}`    | PUT    | Update a product by ID  | Yes           |
| `/products/{id}`    | DELETE | Delete a product by ID  | Yes           |

#### **Orders**
| Endpoint            | Method | Description             | Auth Required |
|---------------------|--------|-------------------------|---------------|
| `/orders`           | GET    | Retrieve all orders     | Yes           |
| `/orders/{id}`      | GET    | Retrieve an order by ID | Yes           |
| `/orders`           | POST   | Create a new order      | Yes           |

#### **Authentication**
| Endpoint            | Method | Description             |
|---------------------|--------|-------------------------|
| `/auth/register`    | POST   | Register a new user     |
| `/auth/login`       | POST   | Log in and get a JWT token|

---

### **Example Requests**
#### **Retrieve All Products**
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

#### **Create a Product**
```http
POST /api/products
Authorization: Bearer <your-token>
Content-Type: application/json
```

**Request Body:**
```json
{
  "name": "New Product",
  "price": 150.00,
  "stock": 20
}
```

**Response:**
```json
{
  "id": 3,
  "name": "New Product",
  "price": 150.00,
  "stock": 20
}
```

---

## **Error Handling**
| Status Code | Description                |
|-------------|----------------------------|
| 400         | Bad Request                |
| 401         | Unauthorized (invalid JWT) |
| 404         | Resource Not Found         |
| 500         | Internal Server Error      |

---

## **Testing**
1. Import the [Postman collection](#) (link to be provided) for pre-configured API requests.
2. Alternatively, use the built-in Swagger documentation available at `/swagger` once the application is running.

---

## **Deployment**
### **Docker (Optional)**
You can containerize the application for deployment. Here’s an example `Dockerfile`:
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY . .
RUN dotnet publish -c Release -o out
EXPOSE 5000
ENTRYPOINT ["dotnet", "out/MarketAPI.dll"]
```

Run the following commands to build and run the container:
```bash
docker build -t market-api .
docker run -p 5000:5000 market-api
```

### **Live Demo**
Consider hosting the API on [Render](https://render.com/), [Heroku](https://www.heroku.com/), or [Azure](https://azure.microsoft.com/).

---

## **Architecture Overview**
![Architecture Diagram](#) *(Replace with an actual image link)*  
- **Client**: Communicates with the API via HTTP requests.
- **Server**: ASP.NET Core handles logic and data processing.
- **Database**: SQL Server stores data.

---

## **Contributing**
- Fork the repository.
- Create a feature branch: `git checkout -b feature-name`.
- Submit a pull request for review.

---

## **License**
This project is licensed under the MIT License. See the `LICENSE` file for details.

---

## **Badges**
![Build](https://img.shields.io/badge/build-passing-brightgreen)  
![License](https://img.shields.io/badge/license-MIT-blue)
