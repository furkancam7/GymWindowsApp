# Gym Management System

A Windows Forms Application developed using **C#** and **LiveCharts** for managing gym operations. The system connects to a database to manage members, classes, equipment, staff, and other essential functionalities. The application includes dynamic dashboards to visualize data such as member counts, class details, and equipment availability.

## Features

- **Member Management**:
  - Add, edit, and view gym members.
  - Visualize member statistics dynamically on the dashboard.

- **Class Management**:
  - View classes grouped by name.
  - Dynamic chart visualization of available classes.

- **Equipment Management**:
  - Track and visualize equipment grouped by name.
  - Add and manage gym equipment dynamically.

- **Staff Management**:
  - Manage gym staff details.
  - Visualize staff statistics on the dashboard.

- **Dynamic Dashboard**:
  - Real-time data visualization using **LiveCharts**.
  - Displays:
    - Total members, staff, classes, and equipment.
    - Classes and equipment grouped by name with bar charts.

- **Navigation**:
  - A "Back" button to navigate to the main menu (`MainForm`).

## Technologies Used

- **C#** (Windows Forms)
- **LiveCharts** (Data visualization)
- **Entity Framework** (Database interaction)
- **SQL Server** (Database)

## Project Structure

The main components of the project:

### `Admin.cs`
Manages admin accounts and their privileges.

### `Member.cs`
Handles member information including:
- Name
- Address
- Date of Birth
- Gender
- Membership details

### `Classes.cs`
Represents gym classes with:
- Class name
- Schedule
- Description
- Capacity

### `Equipment.cs`
Tracks gym equipment:
- Equipment name
- Quantity
- Maintenance logs

### `Staff.cs`
Manages staff information:
- Name
- Role
- Assigned tasks

### `Maintenance_Logs.cs`
Logs maintenance activities for equipment.

### `HomeForm.cs`
Displays the dynamic dashboard with:
- Real-time data visualization of members, classes, equipment, and staff.
- A "Back" button for navigation to the main menu.

### `MainForm.cs`
Acts as the main menu for navigating to different modules.

## Setup and Installation

### Prerequisites
- **Visual Studio** (with C# development tools)
- **SQL Server** or a compatible database system
- **NuGet Packages**:
  - `LiveCharts.WinForms`
  - `EntityFramework`

### Steps to Run the Application
1. Clone the repository or download the project files.
2. Open the project in **Visual Studio**.
3. Restore NuGet packages (`Tools > NuGet Package Manager > Manage NuGet Packages for Solution`).
4. Update the database connection string in the `App.config` file.
5. Build the project (`Build > Build Solution`).
6. Run the project (`Start` or `Ctrl+F5`).

## Usage

1. **Login/Registration**:
   - Navigate to the `LoginForm` or `RegistrationForm` to register a new user or log in.
2. **Main Menu**:
   - Use the main menu to access members, classes, equipment, and staff management.
3. **Dynamic Dashboard**:
   - View real-time statistics and data visualizations for all gym entities.
4. **Navigation**:
   - Use the "Back" button to navigate between forms.

## Contributing

Contributions are welcome! To contribute:
1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Commit your changes (`git commit -m "Add feature"`).
4. Push to the branch (`git push origin feature-branch`).
5. Open a pull request.

## License

This project is licensed under the MIT License. See the `LICENSE` file for more details.

## Contact



---

