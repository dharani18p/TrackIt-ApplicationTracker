# 🚀 TrackIt - Hybrid Application Tracking System

<div align="center">

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=JSON%20web%20tokens&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)

**A modern .NET 8 Web API for intelligent job application tracking with automated workflows, role-based access control, and complete audit trails.**

[Features](#-key-features) • [Quick Start](#-quick-start) • [API Documentation](#-api-endpoints) • [Architecture](#-architecture)

</div>

---

## 📋 Overview

**TrackIt** is a production-ready Application Tracking System (ATS) designed to handle both manual and automated job application workflows. Built with **clean architecture principles**, it provides complete transparency through detailed activity logging and supports three distinct user roles with **JWT authentication**.

### 🎯 Why TrackIt?

- ✅ **Hybrid Workflow**: Manual updates for non-technical roles, automated bot handling for technical positions
- ✅ **Full Audit Trail**: Every status change is logged with timestamps, role attribution, and comments
- ✅ **Role-Based Security**: JWT authentication with granular access control
- ✅ **Production Ready**: Built on .NET 8 with Entity Framework Core and SQL Server
- ✅ **Developer Friendly**: Comprehensive Swagger UI documentation

---

## ⚡ Key Features

### 🔐 Role-Based Authentication
- **Three Distinct Roles**: Applicant, Admin, Bot Mimic
- **JWT Token Security**: Industry-standard authentication
- **Endpoint Protection**: Role-specific access control

### 📊 Application Management
| Role | Capabilities |
|------|-------------|
| **Applicant** | Create applications • View personal history • Track status changes |
| **Admin** | Manage job roles • Update non-technical applications • View all submissions |
| **Bot Mimic** | Auto-process technical applications • Update status progressively • Generate audit logs |

### 🤖 Intelligent Automation
Automated workflow for technical roles:
```
Applied → Reviewed → Interview → Offer → Hired
```
- Auto-generated timestamps
- Smart status progression
- Detailed activity comments

### 📝 Complete Activity Logging
Every update captures:
- ⏰ **Timestamp**: Exact date and time
- 🔄 **Status Transition**: Old → New status
- 👤 **Actor**: Which role made the change
- 💬 **Comments**: Contextual information

---

## 🛠️ Tech Stack

| Technology | Purpose |
|------------|---------|
| **[.NET 8](https://dotnet.microsoft.com/)** | Modern web framework |
| **ASP.NET Core Web API** | RESTful API development |
| **Entity Framework Core** | ORM and data access |
| **SQL Server** | Relational database |
| **JWT Bearer** | Secure authentication |
| **Swagger/OpenAPI** | API documentation |

---

## 🏗️ Architecture

```
TrackIt-ApplicationTracker/
│
├── 📂 Controllers/
│   ├── AuthController.cs          # Authentication & registration
│   ├── ApplicantController.cs     # Applicant operations
│   ├── AdminController.cs         # Admin management
│   └── BotController.cs           # Automation endpoints
│
├── 📂 Data/
│   └── ApplicationDbContext.cs    # EF Core context
│
├── 📂 DTOs/
│   └── [Data Transfer Objects]
│
├── 📂 Models/
│   ├── User.cs                    # User entity
│   ├── JobRole.cs                 # Job role entity
│   ├── Application.cs             # Application entity
│   └── ApplicationLog.cs          # Audit log entity
│
├── Program.cs                     # Application entry point
├── appsettings.json              # Configuration
└── README.md
```

---

## 🚀 Quick Start

### Prerequisites

Ensure you have the following installed:

- ✅ [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- ✅ [SQL Server](https://www.microsoft.com/sql-server) (Express or higher)
- ✅ [EF Core Tools](https://docs.microsoft.com/ef/core/cli/dotnet) (optional)

```bash
# Install EF Core tools globally (optional)
dotnet tool install --global dotnet-ef
```

### Installation

1️⃣ **Clone the repository**
```bash
git clone https://github.com/dharani18p/TrackIt-ApplicationTracker.git
cd TrackIt-ApplicationTracker
```

2️⃣ **Configure database connection**

Edit `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ATSDB;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Jwt": {
    "Key": "YOUR_SECURE_SECRET_KEY_HERE"
  }
}
```

> 💡 **Generate a secure key**: Use `[guid]::NewGuid().ToString()` in PowerShell

3️⃣ **Restore dependencies**
```bash
dotnet restore
```

4️⃣ **Build the project**
```bash
dotnet build
```

5️⃣ **Run the application**
```bash
dotnet run
```

🎉 **Success!** Navigate to: **[http://localhost:5010/swagger](http://localhost:5010/swagger)**

---

## 🔑 Authentication Setup

### Step 1: Register Users

**Endpoint**: `POST /auth/register`

Create users for each role:

| Role | Username | Password | Body Example |
|------|----------|----------|--------------|
| **Admin** | admin1 | Admin@123 | `{"username": "admin1", "password": "Admin@123", "role": "Admin"}` |
| **Applicant** | applicant1 | User@123 | `{"username": "applicant1", "password": "User@123", "role": "Applicant"}` |
| **Bot Mimic** | bot1 | Bot@123 | `{"username": "bot1", "password": "Bot@123", "role": "BotMimic"}` |

### Step 2: Login & Get Token

**Endpoint**: `POST /auth/login`

```json
{
  "username": "admin1",
  "password": "Admin@123"
}
```

**Response**:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

### Step 3: Authorize in Swagger

1. Click **🔓 Authorize** button
2. Enter: `Bearer YOUR_TOKEN_HERE`
3. Click **Authorize**

---

## 📡 API Endpoints

### 🔐 Authentication

| Method | Endpoint | Description | Access |
|--------|----------|-------------|--------|
| `POST` | `/auth/register` | Register new user | Public |
| `POST` | `/auth/login` | Login and receive JWT | Public |

---

### 👤 Applicant Endpoints

> 🔒 **Requires**: Applicant role JWT token

| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/applicant/apply` | Submit job application |
| `GET` | `/applicant/my-applications` | View all your applications |
| `GET` | `/applicant/application/{id}` | View specific application |
| `GET` | `/applicant/application/{id}/logs` | View complete activity log |

**Example - Apply for Job**:
```json
POST /applicant/apply
{
  "jobRoleId": 1
}
```

---

### 🛠️ Admin Endpoints

> 🔒 **Requires**: Admin role JWT token

| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/admin/create-jobrole` | Create new job role |
| `GET` | `/admin/applications` | View all applications |
| `PUT` | `/admin/application/{id}/status` | Update non-technical application status |

**Example - Create Job Role**:
```json
POST /admin/create-jobrole
{
  "title": "Backend Engineer",
  "department": "Engineering",
  "roleType": "Technical"
}
```

**Example - Update Status**:
```json
PUT /admin/application/{id}/status
{
  "newStatus": "Reviewed",
  "comment": "Candidate profile looks promising"
}
```

> ⚠️ **Important**: Admins **cannot** update technical role applications. Bot handles those automatically.

---

### 🤖 Bot Mimic Endpoints

> 🔒 **Requires**: BotMimic role JWT token

| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/bot/run` | Execute automated workflow for all technical applications |

**Response Example**:
```json
{
  "message": "Bot automation completed. Updated 3 applications.",
  "updatedCount": 3
}
```

---

## 🧪 Testing Workflow

Follow this sequence to test all features:

### ✅ Phase 1: Setup Users
```bash
1. Register Admin (admin1)
2. Register Applicant (applicant1)
3. Register Bot Mimic (bot1)
```

### ✅ Phase 2: Create Job Roles
```bash
Login as Admin → Create roles:
  - "Backend Engineer" (Technical)
  - "HR Executive" (Non-Technical)
```

### ✅ Phase 3: Submit Applications
```bash
Login as Applicant → Apply to both roles
```

### ✅ Phase 4: Admin Processing
```bash
Login as Admin → Update non-technical application
Try to update technical → See bot handling message
```

### ✅ Phase 5: Bot Automation
```bash
Login as Bot Mimic → Run /bot/run
Check application logs → Verify automatic updates
```

### ✅ Phase 6: Verify Logs
```bash
Login as Applicant → View logs for full audit trail
```

---

## 📊 Database Schema

### Core Entities

#### 👤 Users
- `Id` (Primary Key)
- `Username` (Unique)
- `PasswordHash`
- `Role` (Admin/Applicant/BotMimic)

#### 💼 JobRoles
- `Id` (Primary Key)
- `Title`
- `Department`
- `RoleType` (Technical/Non-Technical)

#### 📄 Applications
- `Id` (Primary Key)
- `UserId` (Foreign Key)
- `JobRoleId` (Foreign Key)
- `Status`
- `AppliedDate`

#### 📝 ApplicationLogs
- `Id` (Primary Key)
- `ApplicationId` (Foreign Key)
- `OldStatus`
- `NewStatus`
- `UpdatedBy`
- `UpdatedAt`
- `Comments`

---

## 🎨 Status Flow

### Technical Roles (Automated)
```
📝 Applied
    ↓
👀 Reviewed (Bot)
    ↓
💬 Interview (Bot)
    ↓
💰 Offer (Bot)
    ↓
✅ Hired (Bot)
```

### Non-Technical Roles (Manual)
```
📝 Applied
    ↓
👀 Reviewed (Admin)
    ↓
💬 Interview (Admin)
    ↓
💰 Offer (Admin)
    ↓
✅ Hired (Admin)
```

---

## 🔒 Security Features

- ✅ **JWT Bearer Authentication**: Industry-standard token-based auth
- ✅ **Password Hashing**: Secure credential storage
- ✅ **Role-Based Authorization**: Granular access control
- ✅ **Endpoint Protection**: All routes secured by role
- ✅ **Audit Logging**: Complete activity tracking

---

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 👨‍💻 Author

**Dharani**

- GitHub: [@dharani18p](https://github.com/dharani18p)
- Project Link: [TrackIt-ApplicationTracker](https://github.com/dharani18p/TrackIt-ApplicationTracker)

---

## 🙏 Acknowledgments

- Built with [.NET 8](https://dotnet.microsoft.com/)
- Powered by [Entity Framework Core](https://docs.microsoft.com/ef/core/)
- Documented with [Swagger/OpenAPI](https://swagger.io/)

---

<div align="center">

**⭐ If you found this project helpful, please give it a star! ⭐**

Made with ❤️ using .NET 8

</div>
