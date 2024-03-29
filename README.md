# Airline and Airplane Management System

This airline and airplane system enables users to manage Airlines, Airplanes, and FLights recorded in the CMS through Create, Read, Update, and Delete (CRUD) operations. The system presents 1-M relationship between entities, such as Airline-Flights and Airplane-Flights.

## Entity Relationship Diagram:
<p align="center">
   <img src="https://github.com/ufosanchez/AirlineFlightApp/assets/125388195/36c54027-8f8e-49f5-9525-8e7de0cdc671" alt="ER Diagram" width="450" style="display:block; margin:auto">
</p>

## Key Features:

1. **Create, Read, Update, Delete (CRUD):**
   - Leverage comprehensive CRUD functionalities for efficient management of Airlines, Airplanes, and Flight data.

2. **Entity Relationships (1-M):**
   - Establish a clear 1-M relationship between Airlines and their associated Flights, as well as between Airplanes and their recorded Flights.

3. **Specific Queries:**
   - Augment the CRUD base with specialized queries:
      - View flights for a specific airline.
      - Access flights associated with a particular airplane.
      - Retrieve airplanes operated by a specific airline.

4. **Dynamic Search Bar:**
   - Integrate a dynamic search bar allowing users to filter data effortlessly. Users can:
      - Search for specific Airlines, case insensitive so no problem with capital letters or lower letters.
      - Filter by Airplanes.
      - Look up individual Flights directly.

5. **Time Zone Offset Management:**
   - The system adeptly manages time zone offsets related to server timestamps. This ensures accurate recording of each new flight entry and any updates made.
   - Using the local time zone of each of the destinations, the adjustment was made to a single time zone to obtain the flight time.

## Extra Features:
   - Search by Airlines (Filter option - case insensitive).
   - Search by Airplanes (Filter option - case insensitive).
   - Search by Flights (Filter option - case insensitive).
   - **TimeZoneInfo.Local** for handling Time Zone offset of the computer for **Add Flight** and **Update Flight**
   - **ConvertTimeBySystemTimeZoneId** for handling Time Zone Difference by using, so I can get the correct **Duration Flight**

# Pages and methods used:
### AirlineDataController
   - List of Airlines or for particular Airlines (FIlter) `HttpGet api/AirlineData/ListAirlines/{AirlineSearch?}`
   - Find a particular Airline `HttpGet api/AirlineData/FindAirline/{id}`
   - Delete an Airline `HttpPost api/AirlineData/DeleteAirline/{id}`
   - Add an Airline `HttpPost api/AirlineData/AddAirline`
   - Update an Airline `HttpPost api/AirlineData/UpdateAirline/{id}`

### AirplaneDataController
   - List of Airplanes or for particular Airplane (Filter) `api/AirplaneData/ListAirplanes/{AirplaneSearch?}`
   - Find a particular Airplane `api/AirplaneData/FindAirplane/{id}`
   - Delete an Airplane `HttpPost api/AirplaneData/DeleteAirplane/{id}`
   - Add an Airplane `HttpPost api/AirplaneData/AddAirplane`
   - Update an Airline `HttpPost api/AirplaneData/UpdateAirplane/{id}`

### FlightDataController
   - List of Flights or for particular Flights (Filter) `HttpGet api/FlightData/ListFlights/{FlightSearch?}`
   - List for Flights for a particular Airline `HttpGet api/FlightData/ListFlightsForAirline/{id}`
   - List for Flights for a particular Airplane `HttpGet api/FlightData/ListFlightsForAirplane/{id}`
   - List for related Planes (belongs) to an Airline `HttpGet api/FlightData/ListPlanesForAirline/{id}`
   - Find a particular Flight `HttpGet api/FlightData/FindFlight/{id}`
   - Delete a Flight `HttpPost api/FlightData/DeleteFlight/{id}`
   - Add a Flight `HttpPost api/FlightData/AddFlight`
   - Update a Flight `HttpPost api/FlightData/UpdateFlight/{id}`

## Technologies used for the project:
   - ASP.NET Entity Framework
   - Bootstrap CSS Framework
   - Vanilla CSS
   - LINQ
   - Web API Controllers
   - MVC Controllers

## Getting Started:
   1. Clone the repository to your local machine.
   2. Open the code with Visual Studio
   3. Run the Project and if you get an Error due to the target framework
      - Change target framework to 4.7.1
      - Change back to 4.7.2
   4. Make sure there is an App_Data folder in the project (Right click solution > View in File Explorer)
      - If there is no folder create a folder called App_Data
   5. Run Update-Database by typing it on Tools > Nuget Package Manager > Package Manage Console
   6. Check that the database is created using (View > SQL Server Object Explorer > MSSQLLocalDb > ..)
   7. Access the Features by the browser or run API commands through CURL in your terminal
