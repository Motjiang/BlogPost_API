# Article Website API

Welcome to the **Article Website API**! This RESTful API is built using **ASP.NET Core**, designed to power an article platform for **Tech Developers**. The platform helps improve the **Software Development Life Cycle (SDLC)** and provides tutorials and guidance for solving various development tasks.

## 📖 Overview

The Article Website API supports two types of users:  
- **Administrator**: Manages article approvals and categories.  
- **Read/Writer**: Writes and reads articles, choosing from predefined categories and adding images to complement their content.  

### Core Features:
- **User Roles**:  
  - **Administrator**: Reviews and approves articles to ensure credibility. Adds and manages categories.  
  - **Read/Writer**: Writes articles and selects a category. Can upload an image to enhance articles.  
- **Article Moderation**: Articles require administrator approval before publication to maintain content integrity.  
- **Category Management**: Administrators can add and manage categories, helping organize articles effectively.  
- **Image Upload**: Writers can attach images relevant to their articles.

---

## 🛠️ Technology Stack

The API is developed with the following technologies:
- **C#**
- **ASP.NET Core 6**
- **Entity Framework Core**
- **SQL Server** (Database)
- **JWT Authentication** (Planned)
- **Docker** (Planned)

Frontend integration is powered by:
- **Angular 16**
- **TypeScript**
- **Bootstrap 5**
- **HTML5**
- **CSS**

---


🔑 Authentication (Planned)
 - The API will include JWT Authentication to secure endpoints. Once implemented, users will need to obtain a token by logging in and include it in the Authorization header for protected routes.

📦 Docker (Planned)
- Docker integration will be added for easier deployment. Stay tuned for updates on this feature.

