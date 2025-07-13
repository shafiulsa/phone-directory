
# Phone Directory Backend

## Overview

The **Phone Directory Backend** is a .NET Core 8 Web API for managing contact information with CRUD operations, bulk upload, bulk delete, and bulk disable functionalities. It uses an Azure SQL Database for storage, JWT-based authentication with Admin and Client roles, and supports `.txt` and `.xlsx` file uploads for bulk operations. The API integrates with an Angular 18 frontend.

## Features

-   **Contact Management**:
    -   Create, read, update, delete, and disable contacts.
-   **Bulk Operations**:
    -   Upload up to 200 contacts via `.txt` or `.xlsx`.
    -   Delete up to 150 contacts by ID.
    -   Disable up to 100 contacts by ID.
-   **Security**:
    -   JWT authentication with role-based access (Admin-only for modifications).
-   **File Processing**:
    -   Parses `.txt` (comma-separated) and `.xlsx` files with validation.
-   **Background Processing**:
    -   Handles bulk operations asynchronously via `BulkOperationService`.
-   **Logging**:
    -   Comprehensive logging for debugging and error tracking.

## Project Structure

```
PhoneDirectory/
├── PhoneDirectory.Api/
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── BackgroundServices/
│   │   └── BulkOperationService.cs
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   └── ContactsController.cs
│   ├── Models/
│   │   ├── ContactDto.cs
│   │   └── LoginDto.cs
│   ├── Services/
│   │   ├── AuthService.cs
│   │   └── ContactService.cs
│   ├── Program.cs
│   └── PhoneDirectory.Api.csproj
├── PhoneDirectory.Domain/
│   ├── Entities/
│   │   ├── Contact.cs
│   │   └── User.cs
│   ├── Interfaces/
│   │   ├── IContactRepository.cs
│   │   └── IContactService.cs
│   └── PhoneDirectory.Domain.csproj
├── PhoneDirectory.Infrastructure/
│   ├── Data/
│   │   ├── DesignTimeDbContextFactory.cs
│   │   └── PhoneDirectoryContext.cs
│   ├── FileHandlers/
│   │   ├── ExcelFileHandler.cs
│   │   └── TextFileHandler.cs
│   ├── Migrations/
│   │   ├── 20250711155940_InitialCreate.cs
│   │   ├── ... (other migration files)
│   ├── Repositories/
│   │   └── ContactRepository.cs
│   └── PhoneDirectory.Infrastructure.csproj
└── PhoneDirectory.sln

```

## Prerequisites

-   **.NET SDK**: 8.0 or later
-   **Azure SQL Database**: Configured with `Contacts` table
-   **Node.js/npm**: For Angular frontend (optional)
-   **IDE**: Visual Studio, VS Code, or Rider
-   **Dependencies**:
    -   `Microsoft.EntityFrameworkCore.SqlServer`
    -   `Microsoft.AspNetCore.Authentication.JwtBearer`
    -   `EPPlus`
    -   `Microsoft.Extensions.Logging.Console`

## Setup

### 1. Clone Repository

```bash
git clone <repository-url>
cd PhoneDirectory

```

### 2. Configure Database

Update `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tcp:<your-server>.database.windows.net,1433;Initial Catalog=PhoneDirectory;..."
  },
  "Jwt": {
    "Issuer": "your-issuer",
    "Audience": "your-audience",
    "Key": "your-secure-key-32-chars-long"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}

```

Create `Contacts` table:

```sql
CREATE TABLE Contacts (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PhoneNumber NVARCHAR(20) NOT NULL UNIQUE,
    Balance DECIMAL(18,2) NOT NULL,
    Address NVARCHAR(200),
    [Group] NVARCHAR(50),
    Status BIT NOT NULL
);

```

### 3. Install Dependencies

```bash
cd PhoneDirectory.Api
dotnet restore

```

### 4. Apply Migrations

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update

```

### 5. Run the API

```bash
dotnet run

```

API runs at `http://localhost:5000/api`.

### 6. Enable CORS (Optional)

For Angular frontend (`http://localhost:4200`), add to `Program.cs`:

```csharp
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
app.UseCors("AllowAll");

```



## API Endpoints
### Main
- <img width="1730" height="887" alt="1main1" src="https://github.com/user-attachments/assets/cfc44328-7c00-4863-8c4c-a1532dc9d70e" />

### Authentication

#### POST /api/auth/logi

-   **Description**: Authenticates a user and returns a JWT token.
<img width="1775" height="739" alt="1auth1" src="https://github.com/user-attachments/assets/0650d1df-f6b9-4dd8-aba3-685f0d685cf1" />
<img width="1707" height="638" alt="1auth2" src="https://github.com/user-attachments/assets/6f59bd36-f8f9-4e46-872c-0bec4dc70533" />
<img width="1608" height="700" alt="1auth-complit" src="https://github.com/user-attachments/assets/9a82e0fe-4946-47b0-b1ca-a87f3a989cbf" />

    

### Contacts

#### POST /api/contacts (Admin)

-   **Description**: Creates a contact.
##### Unauthorized:
- <img width="1730" height="710" alt="1contact-un-post1" src="https://github.com/user-attachments/assets/c6ed311a-13d0-4431-92fd-59bcbdeb4e6d" />
- <img width="1636" height="714" alt="1contact-un-post2" src="https://github.com/user-attachments/assets/ac036898-4379-4c64-ad30-bfa2d6e98129" />

#####  Authorized:

- <img width="1594" height="666" alt="1contact-au-post1" src="https://github.com/user-attachments/assets/6bde9fd0-bf59-490c-ba3c-2f71367b9c33" />
- <img width="1556" height="488" alt="1contact-au-post2" src="https://github.com/user-attachments/assets/ed937f32-04e5-4d03-a501-a407ba5e1630" />




#### GET /api/contacts

-   **Description**: Retrieves all contacts.
##### Unauthorized:
- <img width="1725" height="860" alt="1contacts-un1" src="https://github.com/user-attachments/assets/9cf5ab18-37f4-4487-aafd-a3083ba2eb4c" />
- <img width="1657" height="754" alt="1contacts-un2" src="https://github.com/user-attachments/assets/e81f2b76-8339-44a5-a00e-e8dff9b11ca8" />

-

#### GET /api/contacts/{id}

-   **Description**: Retrieves a contact by ID.
- <img width="1583" height="865" alt="1contacts-id-un1" src="https://github.com/user-attachments/assets/a69be60d-590e-4c85-ace9-8aa3ea15b8e7" />


#### PUT /api/contacts/{id} (Admin)

-   **Description**: Updates a contact.
##### Unauthorized:
- <img width="1633" height="765" alt="1contacts-id-put-un1" src="https://github.com/user-attachments/assets/346ea6a9-1958-443c-8af4-52e3cfb327c9" />
- <img width="1522" height="358" alt="1contacts-id-put-un2" src="https://github.com/user-attachments/assets/ff070a4b-a068-40c2-a910-871e3eff36bb" />
#####  Authorized:
- <img width="1613" height="744" alt="1contacts-id-put-au1" src="https://github.com/user-attachments/assets/e6bf8abb-d654-4f9a-891b-edaf232f2938" />
- <img width="1571" height="611" alt="1contacts-id-put-au2" src="https://github.com/user-attachments/assets/fcc367fd-98ab-4c75-8197-617418c16f39" />

 

#### DELETE /api/contacts/{id} (Admin)

-   **Description**: Deletes a contact.
##### Unauthorized:
   - <img width="1509" height="846" alt="1contacts-id-del-un1" src="https://github.com/user-attachments/assets/4e007205-8246-4cf4-a72e-9d3cddf576a2" />
   #####  Authorized:
- <img width="1540" height="824" alt="1contacts-id-del-au1" src="https://github.com/user-attachments/assets/be6c5985-8a05-4108-9b30-5c6e40d6e4c2" />


#### PUT /api/contacts/{id}/disable (Admin)

-   **Description**: Disables a contact.
##### Unauthorized:
- <img width="1608" height="831" alt="1contacts-id-disable-un1" src="https://github.com/user-attachments/assets/6814fd87-d645-41b7-b1c6-8c7a04c40ed2" />
#####  Authorized:
- <img width="1494" height="781" alt="1contacts-id-disable-au1" src="https://github.com/user-attachments/assets/86d1fe06-4e74-4a60-9c5e-07b804cdda04" />



#### POST /api/contacts/bulk-upload (Admin)

-   **Description**: Uploads up to 200 contacts from `.txt` or `.xlsx`.
##### Unauthorized:
- <img width="1674" height="871" alt="1contact-bulk-upload-ua1" src="https://github.com/user-attachments/assets/0a6ce0aa-0fd8-4fe5-a5fa-47b0bf6fc160" />

#####  Authorized:
- <img width="1628" height="927" alt="1contact-bulk-upload-au1" src="https://github.com/user-attachments/assets/cf068bb4-f1cc-4af4-bd7f-9415dfcb5e2a" />


#### POST /api/contacts/bulk-delete (Admin)
##### Unauthorized:
- <img width="1659" height="644" alt="1contact-bulk-delete-ua1" src="https://github.com/user-attachments/assets/a5eccc8a-4291-4347-a131-02f02e351b0b" />
- <img width="1544" height="631" alt="1contact-bulk-delete-ua2" src="https://github.com/user-attachments/assets/0f990568-68e7-4232-b6a5-e39dbcf9eb8c" />
#####  Authorized:
- <img width="1545" height="653" alt="1contact-bulk-delete-au1" src="https://github.com/user-attachments/assets/3643ab53-c6ef-4559-bbb5-791334f128f4" />
- <img width="1555" height="454" alt="1contact-bulk-delete-au2" src="https://github.com/user-attachments/assets/2d92ce7a-2124-472e-9bb8-38425f42a181" />


#### POST /api/contacts/bulk-disable (Admin)

-   **Description**: Disables up to 100 contacts.
##### Unauthorized:
- <img width="1623" height="611" alt="1contact-bulk-disable-ua1" src="https://github.com/user-attachments/assets/cd1b68c1-4804-4566-96a5-e2969abbf034" />
- <img width="1628" height="397" alt="1contact-bulk-disable-ua2" src="https://github.com/user-attachments/assets/51d58e26-1bb9-4353-b001-ea9c8889de68" />

#####  Authorized:
- <img width="1582" height="647" alt="1contact-bulk-disable-au1" src="https://github.com/user-attachments/assets/c2c58e7b-f3f1-47ef-a36c-dc6292bc58c2" />
- <img width="1540" height="495" alt="1contact-bulk-disable-au2" src="https://github.com/user-attachments/assets/e3ab86a6-821c-4a29-8683-fa8564273a41" />


## File Formats

### .txt (Comma-Separated)

```text
John Doe,john.doe@example.com,1234567890,100.50,123 Main St,Friends,true
Jane Smith,jane.smith@example.com,2345678901,200.75,456 Oak Ave,Work,true

```


## Testing

1.  **Run API**:
    
    ```bash
    dotnet run
    
    ```
    
2.  **Test Login**:
    
    ```bash
    curl -X POST http://localhost:5000/api/auth/login -H "Content-Type: application/json" -d '{"username":"admin","password":"admin123"}'
    
    ```
    
3.  **Test Bulk Upload**:
    
    ```bash
    curl -X POST http://localhost:5000/api/contacts/bulk-upload -H "Authorization: Bearer <token>" -F "file=@contacts_200.txt"
    
    ```
    
4.  **Verify Database**:
    
    ```sql
    SELECT COUNT(*) FROM Contacts;
    
    ```
    

## Troubleshooting

-   **Database Errors**: Check connection string and Azure SQL firewall.
-   **Auth Issues**: Verify JWT `role` claim and key length.
-   **Bulk Upload**: Ensure correct file format; check logs.
-   **CORS**: Enable `AllowAll` policy for frontend.

## Dependencies

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.0" />
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.0" />
<PackageReference Include="EPPlus" Version="6.2.10" />
<PackageReference Include="Microsoft.Extensions.Logging.Console" Version="8.0.0" />

```

## Contributing

1.  Fork the repo.
2.  Create a branch: `git checkout -b feature/your-feature`.
3.  Commit: `git commit -m "Add feature"`.
4.  Push: `git push origin feature/your-feature`.
5.  Submit a pull request.

