# 🚀 TrackIt - Hybrid Application Tracking System

<div align="center">

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=JSON%20web%20tokens&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)

**A modern .NET 8 Web API for intelligent job application tracking with automated workflows, role-based access control, and real-time dashboard analytics.**

[Features](#-key-features) • [Quick Start](#-quick-start) • [API Documentation](#-api-endpoints) • [Deployment](#-deployment)

</div>

---

## 📋 Overview

**TrackIt** is a production-ready Application Tracking System (ATS) designed to handle both manual and automated job application workflows. Built with **clean architecture principles**, it provides complete transparency through detailed activity logging, **real-time dashboard analytics**, and supports three distinct user roles with **JWT authentication**.

### 🎯 Why TrackIt?

- ✅ **Hybrid Workflow**: Manual updates for non-technical roles, automated bot handling for technical positions
- ✅ **Real-Time Dashboards**: Role-specific analytics with charts, stats, and insights
- ✅ **Full Audit Trail**: Every status change is logged with timestamps, role attribution, and comments
- ✅ **Role-Based Security**: JWT authentication with granular access control
- ✅ **Production Ready**: Built on .NET 8 with Entity Framework Core and SQL Server
- ✅ **Automated Scheduling**: Background service for continuous bot processing
- ✅ **Developer Friendly**: Comprehensive Swagger UI documentation

---

## ⚡ Key Features

### 🔐 Role-Based Authentication
- **Three Distinct Roles**: Applicant, Admin, Bot Mimic
- **JWT Token Security**: Industry-standard authentication
- **Endpoint Protection**: Role-specific access control

### 📊 Dashboard Analytics (NEW!)
Each role has a dedicated dashboard endpoint providing real-time insights:

| Role | Dashboard Metrics |
|------|-------------------|
| **Applicant** | Total applications • Status breakdown • Recent activity • Technical vs Non-technical splits |
| **Admin** | All applications • Role distribution • Status analytics • Recent updates • Top applied roles |
| **Bot Mimic** | Processed applications • Pending queue • Last run timestamp • Automation statistics |

### 📈 Application Management
| Role | Capabilities |
|------|-------------|
| **Applicant** | Create applications • View personal history • Track status changes • Dashboard insights |
| **Admin** | Manage job roles • Update non-technical applications • View all submissions • System-wide analytics |
| **Bot Mimic** | Auto-process technical applications • Update status progressively • Generate audit logs • View automation stats |

### 🤖 Intelligent Automation
Automated workflow for technical roles with **scheduled background processing**:
```
Applied → Reviewed → Interview → Offer → Hired
```
- Auto-generated timestamps
- Smart status progression
- Detailed activity comments
- Runs every 30 minutes (configurable)
- On-demand manual trigger available

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
| **Background Services** | Scheduled automation |
| **Swagger/OpenAPI** | API documentation |

---

## 🏗️ Architecture

```
TrackIt-ApplicationTracker/
│
├── 📂 Controllers/
│   ├── AuthController.cs          # Authentication & registration
│   ├── ApplicantController.cs     # Applicant operations + Dashboard
│   ├── AdminController.cs         # Admin management + Dashboard
│   ├── BotController.cs           # Automation endpoints + Dashboard
│   └── DashboardController.cs     # Centralized dashboard logic
│
├── 📂 Services/
│   ├── BotSchedulerService.cs     # Background automation scheduler
│   ├── DashboardService.cs        # Dashboard data aggregation
│   └── ApplicationService.cs      # Business logic
│
├── 📂 Data/
│   └── ApplicationDbContext.cs    # EF Core context
│
├── 📂 DTOs/
│   ├── DashboardDTO.cs            # Dashboard response models
│   └── [Other Data Transfer Objects]
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
    "Key": "YOUR_SECURE_SECRET_KEY_HERE",
    "Issuer": "TrackItAPI",
    "Audience": "TrackItClient"
  },
  "BotScheduler": {
    "IntervalMinutes": 30
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

5️⃣ **Run database migrations**
```bash
dotnet ef database update
```

6️⃣ **Run the application**
```bash
dotnet run
```

🎉 **Success!** Navigate to: **[http://localhost:5010/swagger](http://localhost:5010/swagger)**

---

## 🌐 Deployment

### Live API Endpoint
🔗 **Production URL**: `https://your-app.azurewebsites.net/swagger`

### Deployment Options

#### Option 1: Azure App Service (Recommended)
```bash
# Install Azure CLI
az login
az webapp up --name trackit-api --resource-group TrackItRG --runtime "DOTNET:8.0"
```

#### Option 2: Railway
```bash
# Connect GitHub repo to Railway
# Add environment variables in Railway dashboard
# Automatic deployment on push
```

#### Option 3: Docker
```bash
# Build Docker image
docker build -t trackit-api .

# Run container
docker run -p 5010:8080 trackit-api
```

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
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "role": "Admin",
  "username": "admin1"
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
| `GET` | `/applicant/dashboard` | **📊 View personal dashboard with analytics** |
| `POST` | `/applicant/apply` | Submit job application |
| `GET` | `/applicant/my-applications` | View all your applications |
| `GET` | `/applicant/application/{id}` | View specific application |
| `GET` | `/applicant/application/{id}/logs` | View complete activity log |

#### **📊 Dashboard Response**:
```json
GET /applicant/dashboard

{
  "totalApplications": 5,
  "applicationsByStatus": {
    "Applied": 2,
    "Reviewed": 1,
    "Interview": 1,
    "Offer": 1,
    "Hired": 0
  },
  "technicalRolesCount": 3,
  "nonTechnicalRolesCount": 2,
  "recentApplications": [
    {
      "id": 1,
      "jobTitle": "Backend Engineer",
      "department": "Engineering",
      "status": "Interview",
      "appliedDate": "2025-11-10T08:30:00Z",
      "roleType": "Technical"
    }
  ],
  "lastUpdated": "2025-11-17T10:00:00Z"
}
```

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
| `GET` | `/admin/dashboard` | **📊 View system-wide dashboard with metrics** |
| `POST` | `/admin/create-jobrole` | Create new job role |
| `GET` | `/admin/jobroles` | View all job roles |
| `GET` | `/admin/applications` | View all applications |
| `GET` | `/admin/applications/technical` | View technical applications only |
| `GET` | `/admin/applications/non-technical` | View non-technical applications only |
| `PUT` | `/admin/application/{id}/status` | Update non-technical application status |
| `DELETE` | `/admin/jobrole/{id}` | Delete job role |

#### **📊 Dashboard Response**:
```json
GET /admin/dashboard

{
  "totalApplications": 50,
  "totalJobRoles": 10,
  "totalApplicants": 25,
  "applicationsByRoleType": {
    "Technical": 30,
    "NonTechnical": 20
  },
  "applicationsByStatus": {
    "Applied": 15,
    "Reviewed": 12,
    "Interview": 10,
    "Offer": 8,
    "Hired": 5
  },
  "topAppliedRoles": [
    {
      "roleTitle": "Backend Engineer",
      "applicationCount": 12,
      "roleType": "Technical"
    },
    {
      "roleTitle": "HR Executive",
      "applicationCount": 8,
      "roleType": "NonTechnical"
    }
  ],
  "recentActivities": [
    {
      "applicationId": 23,
      "jobTitle": "Frontend Developer",
      "applicantName": "applicant1",
      "action": "Status Updated",
      "oldStatus": "Applied",
      "newStatus": "Reviewed",
      "updatedBy": "Admin",
      "timestamp": "2025-11-17T09:45:00Z"
    }
  ],
  "lastUpdated": "2025-11-17T10:00:00Z"
}
```

**Example - Create Job Role**:
```json
POST /admin/create-jobrole
{
  "title": "Backend Engineer",
  "department": "Engineering",
  "roleType": "Technical",
  "description": "Develop and maintain backend services"
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
| `GET` | `/bot/dashboard` | **📊 View automation statistics** |
| `POST` | `/bot/run` | Execute automated workflow for all pending technical applications |
| `GET` | `/bot/pending-applications` | View technical applications pending automation |
| `GET` | `/bot/scheduler-status` | Check background scheduler status |

#### **📊 Dashboard Response**:
```json
GET /bot/dashboard

{
  "totalProcessedApplications": 30,
  "pendingTechnicalApplications": 5,
  "lastRunTimestamp": "2025-11-17T10:00:00Z",
  "nextScheduledRun": "2025-11-17T10:30:00Z",
  "processingStats": {
    "Applied": 5,
    "Reviewed": 10,
    "Interview": 8,
    "Offer": 5,
    "Hired": 2
  },
  "automationHistory": [
    {
      "runTimestamp": "2025-11-17T10:00:00Z",
      "applicationsProcessed": 3,
      "status": "Completed"
    }
  ],
  "schedulerEnabled": true,
  "intervalMinutes": 30
}
```

**Response Example**:
```json
POST /bot/run

{
  "success": true,
  "message": "Bot automation completed successfully",
  "applicationsProcessed": 3,
  "timestamp": "2025-11-17T10:00:00Z",
  "updates": [
    {
      "applicationId": 12,
      "jobTitle": "Backend Engineer",
      "oldStatus": "Applied",
      "newStatus": "Reviewed",
      "comment": "Bot: Initial screening completed"
    }
  ]
}
```

---

## 🧪 Testing Workflow

Follow this sequence to test all features:

### ✅ Phase 1: Setup Users
```bash
1. POST /auth/register → Register Admin (admin1)
2. POST /auth/register → Register Applicant (applicant1)
3. POST /auth/register → Register Bot Mimic (bot1)
4. POST /auth/login → Login each user and save tokens
```

### ✅ Phase 2: Create Job Roles (Admin)
```bash
Login as Admin → POST /admin/create-jobrole
  - "Backend Engineer" (Technical)
  - "Frontend Developer" (Technical)
  - "HR Executive" (Non-Technical)
  - "Marketing Manager" (Non-Technical)
```

### ✅ Phase 3: Submit Applications (Applicant)
```bash
Login as Applicant → POST /applicant/apply
  - Apply to Backend Engineer
  - Apply to HR Executive
  - Apply to Frontend Developer
```

### ✅ Phase 4: View Applicant Dashboard
```bash
GET /applicant/dashboard
  - Verify total applications count
  - Check status breakdown
  - View recent applications
```

### ✅ Phase 5: Admin Processing
```bash
Login as Admin:
  - GET /admin/dashboard → View system metrics
  - PUT /admin/application/{id}/status → Update HR Executive application
  - Try to update Backend Engineer → See bot handling message
```

### ✅ Phase 6: Bot Automation
```bash
Login as Bot Mimic:
  - GET /bot/dashboard → View automation stats
  - GET /bot/pending-applications → See technical applications
  - POST /bot/run → Execute automation
  - GET /bot/dashboard → Verify updated stats
```

### ✅ Phase 7: Verify Logs
```bash
Login as Applicant:
  - GET /applicant/application/{id}/logs → View full audit trail
  - Verify bot comments and timestamps
```

### ✅ Phase 8: Monitor Background Scheduler
```bash
Wait 30 minutes → Check /bot/dashboard
  - Verify lastRunTimestamp updated
  - Check applicationsProcessed count increased
```

---

## 📊 Database Schema

### Core Entities

#### 👤 Users
- `Id` (Primary Key)
- `Username` (Unique)
- `PasswordHash`
- `Role` (Admin/Applicant/BotMimic)
- `CreatedAt`

#### 💼 JobRoles
- `Id` (Primary Key)
- `Title`
- `Department`
- `RoleType` (Technical/Non-Technical)
- `Description`
- `CreatedAt`
- `IsActive`

#### 📄 Applications
- `Id` (Primary Key)
- `UserId` (Foreign Key)
- `JobRoleId` (Foreign Key)
- `Status` (Applied/Reviewed/Interview/Offer/Hired/Rejected)
- `AppliedDate`
- `LastUpdatedDate`

#### 📝 ApplicationLogs
- `Id` (Primary Key)
- `ApplicationId` (Foreign Key)
- `OldStatus`
- `NewStatus`
- `UpdatedBy` (Admin/BotMimic/System)
- `UpdatedAt`
- `Comments`

---

## 🎨 Status Flow

### Technical Roles (Automated)
```
📝 Applied
    ↓ (Bot - 30 min interval or manual trigger)
👀 Reviewed
    ↓ (Bot - 30 min interval)
💬 Interview
    ↓ (Bot - 30 min interval)
💰 Offer
    ↓ (Bot - 30 min interval)
✅ Hired
```

### Non-Technical Roles (Manual)
```
📝 Applied
    ↓ (Admin manual update)
👀 Reviewed
    ↓ (Admin manual update)
💬 Interview
    ↓ (Admin manual update)
💰 Offer
    ↓ (Admin manual update)
✅ Hired / ❌ Rejected
```

---

## 🔒 Security Features

- ✅ **JWT Bearer Authentication**: Industry-standard token-based auth
- ✅ **Password Hashing**: Secure credential storage with BCrypt
- ✅ **Role-Based Authorization**: Granular access control per endpoint
- ✅ **Endpoint Protection**: All routes secured by role validation
- ✅ **Audit Logging**: Complete activity tracking with actor identification
- ✅ **CORS Configuration**: Controlled cross-origin requests
- ✅ **Input Validation**: DTO validation with data annotations

---

## ⚙️ Configuration

### Background Scheduler Settings

Edit `appsettings.json`:
```json
{
  "BotScheduler": {
    "IntervalMinutes": 30,
    "Enabled": true,
    "RunOnStartup": false
  }
}
```

### JWT Settings
```json
{
  "Jwt": {
    "Key": "your-secret-key-min-32-characters",
    "Issuer": "TrackItAPI",
    "Audience": "TrackItClient",
    "ExpiryMinutes": 1440
  }
}
```

---

## 📈 Sample Credentials

Use these credentials for testing:

| Role | Username | Password | Purpose |
|------|----------|----------|---------|
| **Admin** | admin1 | Admin@123 | Manage job roles & non-technical applications |
| **Applicant** | applicant1 | User@123 | Submit and track applications |
| **Bot Mimic** | bot1 | Bot@123 | Execute automated workflows |

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
- Inspired by modern ATS systems

---

## 📞 Support

For issues or questions:
- 📧 Open an issue on GitHub
- 💬 Check existing documentation
- 🔍 Review Swagger UI for API details

---

<div align="center">

**⭐ If you found this project helpful, please give it a star! ⭐**

Made with ❤️ using .NET 8 | Fully Assignment Compliant ✅

**Submission Ready** • Dashboard Analytics ✅ • Background Automation ✅ • Complete Audit Trail ✅

</div>
