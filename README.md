# AlarmApp

## Project Overview
AlarmApp is a web-based application developed using .NET MVC. It allows users to manage and set alarms with a user-friendly interface. The project follows the MVC architecture and interacts with a PostgreSQL database for storing alarm records.

## Tech Stack
- **Framework:** .NET MVC  
- **Programming Languages:** C#, HTML, CSS, JavaScript  
- **Database:** PostgreSQL  
- **ORM:** Dapper  
- **UI Libraries:** DevExtreme (for grids and charts)  
- **Templating Engine:** Razor Syntax  

![Project Overview](preview.gif)

## Features
- User authentication (Login system)
- Alarm monitoring (sort & filter)  
- Data visualization using charts
- Responsive UI using DevExtreme components
- PostgreSQL integration for database management

# Security Features
## Authentication & Authorization:
### Cookie-based Authentication
- Implements `AddAuthentication` and `AddCookie` for secure, persistent user sessions.
- Configured with:
  - `HttpOnly` to prevent XSS (Cross-Site Scripting) attacks.
  - `SecurePolicy.Always` to enforce HTTPS-only cookies.
  - `SameSite.Strict` to mitigate CSRF (Cross-Site Request Forgery) risks.

### User Authentication with Secure Password Storage
- Uses **BCrypt** for password hashing and salting to enhance security.
- Ensures proper password verification with `BCrypt.Verify()`, preventing credential misuse.

## Session Management & Expiry
- Authentication sessions automatically **expire after 30 minutes** (`ExpireTimeSpan = TimeSpan.FromMinutes(30);`).
- **Persistent login sessions** controlled via `AuthenticationProperties { IsPersistent = true }`.
- Implements **proper logout mechanism** using `SignOutAsync()`.
- **Prevents session fixation attacks** by forcing a session reset after login:
  ```csharp
  await HttpContext.SignOutAsync();
  await HttpContext.SignInAsync(principal, authProperties);

## Brute-Force Protection
### Login Rate Limiting & Account Lockout Mechanism
- Tracks failed login attempts using `_failedLogins` dictionary.
- **Accounts are locked for 5 minutes after 3 failed attempts** to prevent brute-force attacks.
- Uses `RegisterFailedAttempt(email)` to log failures and `resetLockOut(email)` upon successful login.

## Database Security & Secure Coding Practices
### Secure SQL Queries Using Dapper & Parameterized Queries
- Prevents SQL Injection by using parameterized queries:
  ```csharp
  QueryFirstOrDefaultAsync<User>("SELECT * FROM users WHERE email = @Email", new { Email = email });

## Role-Based Access Control (RBAC) & Authorization
- Restricts access to AlarmController using [Authorize], ensuring only authenticated users can retrieve alarm data.

## Error Handling & Secure Responses
- Implements try-catch blocks to prevent sensitive error leaks.
- Returns user-friendly error messages instead of exposing stack traces or database details.

## Secure Deployment Practices
### HTTPS Enforcement
- Forces all requests to use HTTPS:
  ```csharp
  app.UseHttpsRedirection();
- Prevents MITM (Man-in-the-Middle) attacks by ensuring secure connections.

### HTTP Strict Transport Security (HSTS)
- Enabled in production mode to enforce HTTPS:

   ```csharp
    app.UseHsts();
- Protects against protocol downgrade attacks and improves transport security.
