# Gift Of The Givers – Disaster Relief Management System


This is an ASP.NET Core MVC application built to support Gift Of The Givers, enabling the organization to efficiently manage donations, volunteers, and incidents in disaster relief efforts.

The system allows community members to donate items, register as volunteers, and enables administrators to track incidents and manage contributions.

🚀 Features

User Authentication

Built on ASP.NET Core Identity.

Donors and volunteers can register and log in.

Admins can view all records, while users see their own data.

Donations

Donors can submit donations (type, quantity, date).

Thank-you confirmation page after submitting.

Admins can see all donations; users can see their own.

Volunteers

Volunteers can register with their name, skills, email, and availability.

Volunteer profiles include detailed information.

Admins can view all registered volunteers.

Incidents (Emergency Tracking)

Capture and display incidents that require relief.

Volunteers and donors can align efforts with current needs.


📂 Project Structure
GiftOfTheGivers_ST10239864/
│── Controllers/         # MVC Controllers (Donation, Volunteer, Incident, Home)
│── Models/              # Data models (Donation, Volunteer, Incident, ApplicationUser)
│── Services/            # Business logic & database services
│── Data/                # AppDbContext and EF Core setup
│── Views/               # Razor views (UI pages)
│   ├── Donation/
│   ├── Volunteer/
│   ├── Incident/
│   └── Shared/
│── wwwroot/             # Static files (CSS, JS, images)
│── Program.cs           # App startup
│── appsettings.json     # Configurations (DB connection string, Identity)
│── README.md            # Documentation (this file)

⚙️ Installation & Setup
1. Clone the repository
git clone https://github.com/your-username/gift-of-the-givers.git
cd gift-of-the-givers

2. Configure the database

Update your SQL Server connection string in appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GiftOfTheGiversDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}

3. Run Entity Framework migrations
dotnet ef database update

4. Build and run
dotnet run


Then open https://localhost:7095
 in your browser.
=======
📂 Project Structure GiftOfTheGivers_ST10239864/ │── Controllers/ # MVC Controllers (Donation, Volunteer, Incident, Home) │── Models/ # Data models (Donation, Volunteer, Incident, ApplicationUser) │── Services/ # Business logic & database services │── Data/ # AppDbContext and EF Core setup │── Views/ # Razor views (UI pages) │ ├── Donation/ │ ├── Volunteer/ │ ├── Incident/ │ └── Shared/ │── wwwroot/ # Static files (CSS, JS, images) │── Program.cs # App startup │── appsettings.json # Configurations (DB connection string, Identity) │── README.md # Documentation (this file)

⚙️ Installation & Setup

Clone the repository git clone https://github.com/your-username/gift-of-the-givers.git cd gift-of-the-givers

Configure the database

Update your SQL Server connection string in appsettings.json:

"ConnectionStrings": { "DefaultConnection": "Server=(localdb)\mssqllocaldb;Database=GiftOfTheGiversDB;Trusted_Connection=True;MultipleActiveResultSets=true" }

Run Entity Framework migrations dotnet ef database update

Build and run dotnet run

Then open https://localhost:7095 in your browser.


🛠️ Technologies Used

ASP.NET Core MVC (7.0+) – Web framework

Entity Framework Core – Database ORM

SQL Server LocalDB – Development database

ASP.NET Identity – Authentication & authorization

Bootstrap 5 – Frontend styling

📸 Screenshots

(Add screenshots of your Donations, Volunteers, and Incidents pages here.)

📌 Future Improvements

Add role-based dashboards for Admin, Donor, and Volunteer.

Integrate email/SMS notifications for donations & incidents.

Add reporting & analytics for relief efforts.

👨‍💻 Author


Katlego Sebona
Disaster Relief Management System for Gift Of The Givers

