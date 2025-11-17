# 🚀 TrackIt – Hybrid Application Tracking System

**A .NET Core 8 Web API built as part of the Junior Backend Engineer (.NET) – PoC assignment for 6S Consulting.**

This system implements **Hybrid Application Tracking** with **Applicant**, **Admin**, and **Bot Mimic** roles, supporting both manual and automated tracking with **full traceability**.

---

## 📌 Overview (Matches the PDF)

**TrackIt** is an Application Tracking System where:

- **Technical role applications** → Updated automatically via **Bot Mimic**
- **Non-technical role applications** → Updated manually by **Admin**
- **Applicants** → Submit and monitor their own applications
- **Full Traceability** → Every action logged with timestamp, role, comment, status transitions

### The system includes three roles:

| Role | Purpose |
|------|---------|
| **Applicant** | Submit & track own applications |
| **Admin** | Create job roles, manually update non-technical applications |
| **Bot Mimic** | Automates updates for technical applications |

---

## 🛠 Tech Stack (as per PDF)

- ✅ **.NET Core / ASP.NET Core Web API**
- ✅ **Entity Framework Core**
- ✅ **SQL Server**
- ✅ **JWT Authentication**
- ✅ **Swagger UI** (used as the "View" in MVC per PDF)
- ✅ **Clean & Modular Code Structure**

---

## 📁 Project Structure (As Required by PDF)

```
TrackIt-ApplicationTracker/
│
├── Controllers/
│   ├── AuthController.cs
│   ├── ApplicantController.cs
│   ├── AdminController.cs
│   ├── BotController.cs
│   └── DashboardController.cs      # Dashboard Insights (PDF Requirement)
│
├── Models/
├── DTOs/
├── Data/
│   └── ApplicationDbContext.cs
│
├── Program.cs
└── README.md
```

---

## 🔐 Role-Based Authentication (PDF Requirement)

The system uses **JWT authentication** with three roles:

- **Applicant**
- **Admin**
- **BotMimic**

Each role has access only to its allowed endpoints.

---

## 📡 API Endpoints (Exactly as Required in PDF)

### 🔐 Auth

| Method | Route | Description |
|--------|-------|-------------|
| `POST` | `/auth/register` | Register a new user (Admin/Applicant/BotMimic) |
| `POST` | `/auth/login` | Login & receive JWT |

---

### 👤 Applicant Endpoints

> **From PDF** → *Applicant – Can create and track own applications*

| Method | Route | Description |
|--------|-------|-------------|
| `POST` | `/applicant/apply` | Create new application |
| `GET` | `/applicant/my-applications` | View own applications |
| `GET` | `/applicant/application/{id}` | View a specific application |
| `GET` | `/applicant/application/{id}/logs` | View full traceability logs |

#### **Applicant Dashboard (PDF Requirement: Dashboard endpoint for insights)**

| Method | Route | Description |
|--------|-------|-------------|
| `GET` | `/applicant/dashboard` | Shows summary of applicant activity |

---

### 🛠 Admin Endpoints

> **From PDF** → *Admin creates job roles, manages non-technical applications*

| Method | Route | Description |
|--------|-------|-------------|
| `POST` | `/admin/create-jobrole` | Create new job role (technical/non-technical) |
| `GET` | `/admin/applications` | View all applications |
| `PUT` | `/admin/application/{id}/status` | Update non-technical status only |

#### **Admin Dashboard**

| Method | Route | Description |
|--------|-------|-------------|
| `GET` | `/admin/dashboard` | Shows system-wide insights |

#### 📌 **If Admin tries to update a technical application →**
System blocks it and returns a message as required:

```
Admin cannot update TECHNICAL applications. Bot will handle these.
```

---

### 🤖 Bot Mimic Endpoints

> **From PDF** → *Bot Mimic simulates automated tracking for technical roles*

| Method | Route | Description |
|--------|-------|-------------|
| `POST` | `/bot/run` | Automatically updates technical role applications |

#### **Bot Dashboard**

| Method | Route | Description |
|--------|-------|-------------|
| `GET` | `/bot/dashboard` | Shows pending & updated technical applications |

#### Bot performs:
```
Applied → Reviewed → Interview → Offer → Hired
```

#### Bot adds:
- ✅ Comments
- ✅ UpdatedByRole = "BotMimic"
- ✅ Timestamp logs

---

## 📝 Full Traceability (PDF Requirement)

Every application update — **Admin** or **Bot** — creates a log entry with:

- **Old Status**
- **New Status**
- **UpdatedByRole**
- **Timestamp**
- **Optional comment**

This ensures **complete audit trail transparency**, exactly as required.

---

## ⚙️ Setup Instructions

### 1️⃣ Clone Repo
```bash
git clone https://github.com/dharani18p/TrackIt-ApplicationTracker.git
cd TrackIt-ApplicationTracker
```

### 2️⃣ Update `appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ATSDB;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Jwt": {
    "Key": "YOUR_SECRET_KEY_HERE"
  }
}
```

**Generate secret key:**
```powershell
[guid]::NewGuid().ToString()
```

### 3️⃣ Run Migrations (if added)
```bash
dotnet ef database update
```

### 4️⃣ Run API
```bash
dotnet run
```

**Access Swagger:**
```
http://localhost:5010/swagger
```

---

## 🔑 Sample Accounts for Testing (PDF Requirement)

Use these during submission:

| Role | Username | Password |
|------|----------|----------|
| **Admin** | admin1 | Admin@123 |
| **Applicant** | applicant1 | User@123 |
| **Bot Mimic** | bot1 | Bot@123 |

---

## 🧪 Testing Workflow (Matches PDF)

### 1. Register 3 users
- Admin / Applicant / Bot Mimic

### 2. Admin
- Create job roles (technical + non-technical)

### 3. Applicant
- Apply for both roles

### 4. Admin
- Update non-technical application
- Try updating technical → **should block**

### 5. Bot Mimic
- Run `/bot/run`
- Check application status moves forward
- Logs generated

### 6. Dashboards
- Check insights for all roles

---

## 📊 Dashboard Endpoints (PDF Requirement)

### **Applicant Dashboard** - `GET /applicant/dashboard`
```json
{
  "totalApplications": 5,
  "applicationsByStatus": {
    "Applied": 2,
    "Reviewed": 1,
    "Interview": 1,
    "Offer": 1
  },
  "technicalRolesCount": 3,
  "nonTechnicalRolesCount": 2,
  "recentApplications": [...]
}
```

### **Admin Dashboard** - `GET /admin/dashboard`
```json
{
  "totalApplications": 50,
  "totalJobRoles": 10,
  "applicationsByRoleType": {
    "Technical": 30,
    "NonTechnical": 20
  },
  "applicationsByStatus": {...},
  "topAppliedRoles": [...],
  "recentActivities": [...]
}
```

### **Bot Dashboard** - `GET /bot/dashboard`
```json
{
  "totalProcessedApplications": 30,
  "pendingTechnicalApplications": 5,
  "lastRunTimestamp": "2025-11-17T10:00:00Z",
  "processingStats": {...},
  "automationHistory": [...]
}
```

---

## 🎯 Assignment Compliance Checklist

✅ **Framework**: .NET Core / ASP.NET Web API  
✅ **Database**: SQL Server with Entity Framework Core  
✅ **UI**: Swagger as the V in MVC  
✅ **Code Structure**: Modular, clean architecture  
✅ **Documentation**: README.md + Swagger UI  
✅ **Role-Based Authentication**: JWT with 3 roles  
✅ **Dashboard Endpoints**: All roles have dashboards  
✅ **Application Creation & Tracking**: Fully implemented  
✅ **Bot Mimic Automation**: Technical roles auto-updated  
✅ **Admin Manual Updates**: Non-technical roles only  
✅ **Full Traceability**: Complete audit logs  
✅ **Sample Credentials**: Provided for all roles  

---

## 🚀 Deployment

**Live API Endpoint**: `https://your-deployed-url/swagger`

*(Add your deployment URL after deploying to Azure/Railway/Render)*

---

## 👨‍💻 Author

**Dharani**

- GitHub: [@dharani18p](https://github.com/dharani18p)
- Project Link: [TrackIt-ApplicationTracker](https://github.com/dharani18p/TrackIt-ApplicationTracker)

---

<div align="center">

**Assignment Submission for 6S Consulting - Junior Backend Engineer (.NET) PoC**

Made with ❤️ using .NET Core 8

</div>
