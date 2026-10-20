# TeacherTracker

**A school employee attendance and working hours tracking system built with ASP.NET Core, .NET MAUI, and Vue/Nuxt.**

> **Project status:** In development

## Overview

TeacherTracker is a digital system for managing employee attendance and working time records in schools. It aims to replace paper-based records with a centralized, transparent, and auditable solution.

The system is designed for both teaching and administrative/technical staff. It consists of a mobile application for employees, a web-based administration panel, and a REST API backed by a relational database.

Unlike a simple check-in/check-out application, TeacherTracker is intended to support daily attendance forms, work categories, monthly summaries, administrative corrections, and monthly record approval and locking.

This project is being developed for educational and portfolio purposes, with a focus on maintainable backend architecture, relational database design, and practical software engineering principles.

## Planned Features

### Employee Mobile Application

- **Daily records** — record daily attendance and relevant work activities.
- **Attendance history** — view personal daily records and monthly summaries.
- **Work categories** — select applicable attendance codes and work categories.
- **Personal access** — access only the employee's own records.

### School Administration Panel

- **Employee management** — manage employee profiles and employment records.
- **Daily overview** — view attendance records for individual employees or all employees on a selected date.
- **Bulk entry** — enter or review daily records for multiple employees.
- **Record corrections** — correct existing records with a documented reason and audit trail.
- **Monthly review** — review employee records and monthly summaries.
- **Monthly locking** — lock reviewed monthly records to preserve an approved version.
- **Reports and exports** — prepare monthly reports, with Excel and PDF export planned.

### Backend and Data Integrity

- **REST API** — expose application functionality to the mobile app and administration panel.
- **Authentication and authorization** — authenticate users and enforce role- and school-based access.
- **Validation** — prevent invalid dates, duplicate records, and inconsistent data.
- **Audit trail** — record important administrative changes.
- **Monthly snapshots** — preserve the contents of locked monthly records for reliable future reporting.
- **Error handling and logging** — provide consistent error responses and application diagnostics.

> The exact attendance codes, work categories, and calculation rules must be aligned with the applicable school forms and requirements before the final working-hours calculation is implemented.

## Tech Stack

### Backend

- **C# / .NET 9**
- **ASP.NET Core Web API**
- **Entity Framework Core** — ORM and Code First migrations
- **PostgreSQL** — relational database
- **Npgsql** — PostgreSQL provider for EF Core
- **ASP.NET Core Identity** — user and account management
- **JWT authentication** — planned API authentication mechanism
- **FluentValidation or built-in validation** — request validation
- **OpenAPI** — API documentation
- **Serilog** — planned structured logging

### Frontend

- **.NET MAUI** — planned employee mobile application
- **Vue.js / Nuxt.js** — planned school administration panel

### Testing and DevOps

- **xUnit** — planned unit testing
- **Integration and API testing** — planned automated tests
- **Docker** — planned containerization
- **GitHub Actions** — planned CI/CD workflows

> The technology stack describes the intended architecture. Individual technologies and features are being introduced incrementally and should not be considered implemented until they are available in the codebase.

## Architecture

TeacherTracker follows a client-server architecture.

The mobile application and web administration panel communicate with the ASP.NET Core Web API. The API handles authentication, authorization, business rules, validation, and persistence through Entity Framework Core. PostgreSQL stores application data.

The backend is designed to support multiple schools, with access to employee and attendance data restricted according to the authenticated user's permissions.

### High-Level Architecture

```text
+----------------------+      +------------------------+
|  Employee Mobile App |      |  School Admin Web App  |
|      .NET MAUI       |      |      Vue / Nuxt        |
+----------+-----------+      +------------+-----------+
           |                               |
           +---------------+---------------+
                           |
                     REST API
                  ASP.NET Core
                           |
                +----------+-----------+
                |                      |
        Authentication &        Business Logic
         Authorization          and Validation
                |                      |
                +----------+-----------+
                           |
                  Entity Framework Core
                           |
                      PostgreSQL
```

## User Roles

| Role | Responsibilities |
|---|---|
| `SUPER_ADMIN` | Manage the overall platform and school-level administration. |
| `SCHOOL_ADMIN` | Manage employees and attendance records within authorized schools, review corrections, and lock monthly records. |
| `EMPLOYEE` | Record daily work information and view personal attendance history and monthly reports. |

The exact permissions will be enforced by the backend. A user must not gain access to another school's data merely by changing an identifier in an API request.

## Data Model

The planned database is centered around the following entities:

- **Schools** — schools registered in the system.
- **Users** — authenticated accounts managed through ASP.NET Core Identity.
- **SchoolMemberships** — administrator membership and permissions for individual schools.
- **Employees** — employee profiles linked to user accounts when applicable.
- **EmploymentRecords** — employment history, school assignments, and employment-related attributes.
- **AttendanceCodes** — codes used in daily attendance forms.
- **WorkCategories** — categories used to classify work and produce reports.
- **MonthlyRecords** — monthly records associated with an employee and a school.
- **DailyRecords** — employee records for individual dates.
- **DailyAttendanceSlots** — individual entries in the daily attendance form.
- **DailyWorkEntries** — manually entered working hours by category.
- **AuditLogs** — history of important changes.
- **MonthlyRecordVersions** — snapshots of locked monthly records.

The schema will evolve as the actual school forms and attendance calculation rules are verified.

## Key Business Rules

The initial version is planned around the following rules:

- Each monthly record belongs to one employee, one school, and one calendar month.
- Each daily record belongs to a monthly record and a specific date.
- Monthly summaries are derived from daily data rather than maintained as unrelated duplicate totals.
- Employees can access their own records.
- School administrators can access records only within their authorized schools.
- Administrative corrections are documented.
- Monthly records have two initial states: `Open` and `Locked`.
- Locked records are preserved through versioned snapshots.
- Attendance codes and work categories must be mapped to the actual reporting rules before calculations are finalized.

## Project Goals

TeacherTracker is intended to demonstrate practical software engineering skills, including:

- Designing and implementing RESTful APIs.
- Developing maintainable backend applications with C# and ASP.NET Core.
- Designing relational databases and managing schema changes with EF Core migrations.
- Implementing authentication and role- and school-based authorization.
- Applying validation and business rules consistently.
- Writing unit and integration tests.
- Implementing auditing and reliable record management.
- Separating backend responsibilities from client applications.
- Automating builds and tests through CI/CD.


## Current Development Status

The project is in its initial backend and database development phase.

The solution, ASP.NET Core Web API project, PostgreSQL connection, EF Core setup, initial entities, and initial migrations have been created. 

Authentication, the complete attendance data model, business endpoints, automated tests, and frontend applications are not yet complete.

## License

All rights reserved. See the repository's licensing notice for details.

---

**Disclaimer:** This is an independent educational and portfolio project.
