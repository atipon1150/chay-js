Myapi/                     # Backend project
├── Controllers/
│   └── DepartmentsController.cs   # CRUD API สำหรับ Department
├── Data/
│   └── AppDbContext.cs            # DbContext สำหรับ EF Core
├── Models/
│   ├── Department.cs              # Entity สำหรับ Department
│   └── User.cs                    # Entity สำหรับ User
├── Properties/
│   └── launchSettings.json        # การตั้งค่า launch ของโปรเจกต์
├── appsettings.json               # การตั้งค่า connection string + Logging
├── Program.cs                     # Entry point ของแอป
└── Myapi.csproj                   # Project file ของ .NET
