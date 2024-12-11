# CS330-Fall2024-FinalProject

## Team
Anastasia Spencer, Brooke Boskus, Collin Price, Owen McMenaman, Jeongbin Son

# Ski Team Management Website

## Overview
The Ski Team Management System is designed to facilitate communication and team management for snow ski athletes, managers, and coaches. The application enables efficient team management and provides information access through a tiered role-based structure. Built using C# .NET MVC and Entity Framework, it employs a multi-tiered architecture for robust and scalable development.

### Features
- **Multi-Tiered Architecture**: Includes domain, infrastructure, and application layers.
- **Identity Roles**: Differentiated access and features for Athletes, Managers, and Coaches.
- **Secure Application**:
  - SRI hashes on external resources.
  - Authorization applied to controllers and actions.
  - Secrets managed via Secrets Manager during development and through Azure environment variables post-deployment.
- **Integrated APIs**:
  - OpenWeather API: Displays weather for the top three ski slopes on the homepage.
  - OpenAI Chatbot: Assists users with common queries.
- **Role-Based Access Control**:
  - **Logged-out Users**: Access roster, schedule, and home pages.
  - **Athletes**: Additional access to the stats page.
  - **Managers**: All athlete views plus the ability to edit events.
  - **Coaches**: Admin-level access to assign roles and edit all user profiles.
- **Reports**:
  - Schedule Report: Events, practices, and games.
  - Stats Report: Highlights top stats for the team.
- **Email Confirmation**: Mandatory email confirmation for new user registration.

## Project Architecture
This application follows a repository pattern for managing identity roles and a multi-tiered architecture for the rest of the functionality and adheres to best practices in secure, scalable development. Key architectural highlights include:
- **Domain Layer**: Encapsulates core business logic and domain entities.
- **Infrastructure Layer**: Handles database operations and external integrations.
- **Application Layer**: Manages the application’s interface with the domain and infrastructure.

## Frontend
- **CSS Framework**: Combination of Bootstrap CSS and custom CSS.
- **Bootstrap Theme**: [Squadfree Bootstrap Template](https://bootstrapmade.com/squadfree-free-bootstrap-template-creative/)
  - Found in `/wwwroot/assets/...`
  - Used for:
    - Hero/Primary Page: Home
    - Secondary Pages: Schedule, Roster, Stats
    - Tertiary Pages: Register, Login

## Identity Roles and Access
1. **Coach Role**:
   - Admin-level access.
   - Assign roles to users.
   - Edit all user profiles.
   - Demo Account:
     - Email: `akspencer1@crimson.ua.edu`
     - Password: `Stasia716!`
2. **Manager Role**:
   - Access all athlete views.
   - Ability to edit events.
   - Demo Account:
     - Email: `json10@crimson.ua.edu`
     - Password: `IH3727jy0419!!`
3. **Athlete Role**:
   - Access roster, schedule, stats pages, and home.
   - Demo Account:
     - Email: `Beboskus@crimson.ua.edu`
     - Password: `Test123!`

## APIs and Integrations
- **OpenWeather API**: Fetches and displays weather information for top three ski slopes on the homepage.
- **OpenAI Chatbot**: Provides interactive assistance to users.

## Security Features
- **SRI Hashes**: Protect external resource integrity.
- **Authorization**: Applied at the controller and action levels.
- **Secrets Management**:
  - During development: Secrets Manager.
  - After deployment: Environment variables using Azure App Configuration.
- **Email Confirmation**: Ensures new accounts are verified before login.

---



	

