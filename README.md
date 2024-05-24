# AgriEnergyConnect Database Prototype

#### Sample data for testing
Employee Account ID and Passwords:

EMP001 password123

EMP002 securepass

EMP003 letmein

EMP004 password4

EMP005 password5


#### Farmer Account ID and Passwords:
FARM001 farmerpass

FARM002 garden123

FARM003 greenfarm

FARM004 12345a

FARM005 vgyferbhd


# Using the App
1. Clone the repository or download the code files.
  - GitHub Link: https://github.com/ST10029788/ST10029788_Agri_Energy_Connect_PROG7311_POE.git
2. Open the solution in your preferred IDE.
   - Visual Studio 2022 Download Link: https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Community&channel=Release&version=VS2022&source=VSLandingPage&cid=2030&passive=false
4. Build the solution to restore dependencies and compile the code.
5. Run the application.
6. Once opened in your preferrered browser, the Home Screen will be displayed.
7. Click on your preffered role from the top ribbon and log in to access functionality.

## Database

1. Created the relational Agri-Energy Connect database using Azure and Azure Data Studio.
2. Populated with sample data as seen from table data screenshots and SQL script SQL_Agri_Energy.sql.
3. It functions without adding the dependency, but to view the database for testing you can add as a dependency on Visual Studio:

   -Open Server Explorer and click Connect to database
   
   -Server Name: agri-energy-connect-server.database.windows.net
   
   -Use SQL authentication
   
   -Username: aariya
   
   -Password: B1ngus@rc
   
   -Database name is AgriEnergyConnect
   
   -Test connection and Save by pressing Ok
   
   


## User Role Definition and Authentication System

### Roles:
- Farmer
- Employee

### Functionality:
- Login functionality with role-based access.
- Farmers can:
  - Add products.
  - View their products.
  - Update their password
- Employees can:
  - Add new farmer profiles.
  - View products from a specific farmer.
  - Filter products for searching.

## Functional Features

### Farmers:
- Add product details such as name, product type, price, quantity and production date.

### Employees:
- Add farmer profiles.
- View and filter products based on date range and product type.
- View all farmers products
- Update details of products

## UI Design and Usability

1. Clear data presentation for ease of understanding.

## Data Accuracy and Validation

1. Validation checks ensure data integrity.
2. Hashed passwords


## Documentation

1. Detailed instructions for setting up the development environment.
2. Steps to build and run the prototype.
3. Explanation of system functionality and user roles.
4. Suitable for both technical and non-technical users.


