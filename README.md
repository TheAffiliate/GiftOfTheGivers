# 🌍 Gift Of The Givers – Disaster Relief Management System

This is an ASP.NET Core MVC application built to support Gift Of The Givers, enabling the organization to efficiently manage donations, volunteers, and incidents in disaster relief efforts.

The system allows community members to contribute directly, while administrators review and approve requests to ensure effective coordination.

#🚀 Features
🔐 User Authentication & Roles

Built on ASP.NET Core Identity.

Users can register & log in with their email.

Admins register like normal users but can access a dedicated Admin Panel via a navigation link.

Role system planned: Admin, User (Donor/Volunteer).

🎁 Donations

Donors can submit donations (type, quantity, description, date).

Each donation is linked to the submitting UserId.

Admin can approve/reject donations with feedback messages.

Users get notified in their Profile Inbox when their donation is approved/rejected.

🙋 Volunteers

Volunteers can register with skills, email, availability, and location.

Each volunteer record is linked to a UserId.

Admin can approve/reject volunteer registrations with reasons.

Notifications update the volunteer’s Inbox when decisions are made.

🚨 Incidents (Emergency Tracking)

Users can report disaster incidents requiring relief.

Each report is linked to a UserId.

Admins can approve/reject incident reports.

Users get inbox notifications about the status of their report.

📬 Notifications / Inbox

All users have an Inbox on their Profile page.

Displays updates on donations, incidents, and volunteer registrations.

Admin feedback (Approved/Rejected + message) is stored in the database.

#📂 Project Structure
GiftOfTheGivers_ST10239864/
│── Controllers/        # MVC Controllers (Donation, Volunteer, Incident, Admin, Profile, Account, Home)
│── Models/             # Data models (Donation, Volunteer, Incident, Notification, ApplicationUser)
│── ViewModels/         # View models (RegisterViewModel, LoginViewModel, UserProfileViewModel, etc.)
│── Services/           # Business logic & database services (DonationService, VolunteerService, IncidentService)
│── Data/               # AppDbContext and EF Core setup
│── Views/              # Razor views (UI pages)
│    ├── Account/       # Register, Login
│    ├── Donation/
│    ├── Volunteer/
│    ├── Incident/
│    ├── Profile/
│    ├── Admin/
│    └── Shared/
│── wwwroot/            # Static files (CSS, JS, images, Bootstrap)
│── Program.cs          # App startup
│── appsettings.json    # Configurations (DB connection string, Identity)
│── README.md           # Documentation (this file)

# ⚙️ Installation & Setup

Clone the repository

git clone https://github.com/your-username/gift-of-the-givers.git
cd gift-of-the-givers


Configure the database
Update your SQL Server connection string in appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GiftOfTheGiversDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}


Apply migrations and build database

dotnet ef database update


⚠️ If schema errors occur (e.g. missing UserId), reset the database:

dotnet ef database drop
dotnet ef database update


Run the application

dotnet run


Open https://localhost:7095
 in your browser.

# 🛠️ Technologies Used

ASP.NET Core MVC (7.0+) – Web framework

Entity Framework Core – Database ORM

SQL Server LocalDB – Development database

ASP.NET Core Identity – Authentication & authorization

Bootstrap 5 – Frontend styling

# 📌 Future Improvements

✅ Role-based dashboards for Admin, Donor, and Volunteer.

✅ Auto-create a default Admin user when the database is reset.

📧 Integrate email/SMS notifications for approvals.

📊 Add reporting & analytics for relief efforts.

🌍 Deploy to Azure App Service with Azure SQL.

👨‍💻 Author

Katlego Sebona / ST10239864
Disaster Relief Management System for Gift Of The Givers