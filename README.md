# TeacherTracker
A teacher work attendance and working hours tracking system built with ASP.NET Core, .NET MAUI, and Vue/Nuxt


# Teacher Work Evidence System

A digital system for managing teacher attendance and working hours in schools, designed to replace traditional paper-based records with a centralized, transparent, and reliable solution.

The project consists of a mobile application for teachers and a web administration panel, supported by a REST API built with ASP.NET Core.

> **Project status:** In development

## Overview

The Teacher Work Evidence System aims to simplify daily attendance tracking, reduce administrative workload, and provide school administrators with a clear overview of teacher working hours.

Teachers can record their arrivals and departures, review their attendance history, and access their records. School administrators can manage teachers, review attendance data, make documented corrections, and generate reports.

## Planned Features

- **Attendance tracking** — daily check-in and check-out.
- **Attendance history** — view individual records and monthly summaries.
- **School management** — manage schools, teachers, and user roles.
- **Administrative dashboard** — monitor attendance and working hours.
- **Reports and exports** — generate reports and export data to Excel or PDF.
- **Record corrections** — manually correct attendance records with an audit trail.
- **Access control** — role-based permissions for teachers and administrators.
- **Validation and error handling** — prevent duplicate or invalid attendance entries.

## Tech Stack

### Backend

- C#
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- ASP.NET Core Identity
- JWT Authentication
- FluentValidation
- Swagger / OpenAPI
- Serilog

### Frontend

- .NET MAUI — teacher mobile application
- Vue.js / Nuxt.js — administration web panel

### Testing & DevOps

- xUnit
- Integration and API testing
- Docker
- GitHub Actions

*Technologies and features listed above represent the planned architecture and may change during development.*

## Architecture

The application follows a client-server architecture, with the mobile application and web panel communicating with the backend through a REST API.

## User Roles

| Role           | Responsibilities                            |
| -------------- | ------------------------------------------- |
| `SUPER_ADMIN`  | Manage the overall system and schools       |
| `SCHOOL_ADMIN` | Manage school staff and attendance records  |
| `TEACHER`      | Record attendance and view personal history |

## Project Goals

This project is intended to demonstrate practical software engineering skills, including:

- Designing and developing RESTful APIs.
- Building maintainable backend applications with C# and ASP.NET Core.
- Implementing authentication and authorization.
- Working with relational databases and Entity Framework Core.
- Writing unit and integration tests.
- Applying clean architecture principles and separation of concerns.
- Automating builds and tests using CI/CD pipelines.

## Development Roadmap

- [ ] **Define the MVP** - Document requirements, user roles, attendance rules, and the scope of the first release.
- [ ] **Set Up GitHub and .NET** - Create the repository, initialize the .NET solution, and set up the first ASP.NET Core Web API project.
- [ ] **Set Up the API and Database** - Connect PostgreSQL, configure Entity Framework Core, and create the initial database migration.
- [ ] **Implement the Attendance Data Model** - Create the `School`, `User`, and `AttendanceRecord` entities and define their relationships.
- [ ] **Implement Check-In and Check-Out** - Develop the initial API endpoints and validate business rules.
- [ ] **Document and Review** - Update the README, review the project structure, and define the next sprint.

## License

All rights reserved. See the repository's licensing notice for details.

---

**Note:** This project is intended for educational and portfolio purposes. It is not affiliated with or endorsed by CARNET or any educational institution.
