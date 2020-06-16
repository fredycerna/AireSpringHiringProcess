## Test Project
C# / ASP.net (MVC or Linq is ok but if you can avoid that it would be helpful). Create a webform for data input/output 
EmployeeID - Unique ID
EmployeeLastName
EmployeeFirstName
EmployeePhone formatted (XXX) XXX-XXXX
EmployeeZip 
HireDate formatted as MM/DD/YYYY
Display data sorted ascending by date 
Extra points – search screen where you can enter either EmployeeLastName or EmployeePhone and it returns all 6 fields

## Architecture
For the solution create a project of three layers each with its own function, design patterns, and tools:

**Data Access Layer**
To manage data access and persistence because the requirement wanted to see SQL queries, instead of using the entity framework, the micro ORM Dapper was used. In this layer, the design patterns of Repositories and Unit of Work were used, starting with a generic repository and then inheriting the repository for employee management.

**Domain Layer**
Or the business layer where business models and services for employee management were created. In this layer, automapper was used for mapping between DAL entities and the Domain Layer. Dependency Injection was used for each service to inject the Unit Of Work into the Repositories.

**Web Layer.**
Or presentation layer In this layer a .netcore 3.1 website was used using the Telerik Framework for the management of the user interface with a Grid and AJAX requests for the CRUD operations of employees (add, edit, delete and search).

**Other files.**
The script to generate the SQL server database used for the exercise is included.
