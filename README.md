# Airline and Airplane Management System

This airline and airplane system enables users to manage Airlines, Airplanes, and FLights recorded in the CMS through Create, Read, Update, and Delete (CRUD) operations. The system presents 1-M relationship between entities, such as Airline-Flights and Airplane-Flights.

## Entity Relationship Diagram:
<p align="center">
   <img src="https://github.com/ufosanchez/AirlineFlightApp/assets/125388195/36c54027-8f8e-49f5-9525-8e7de0cdc671" alt="ER Diagram" width="450" style="display:block; margin:auto">
</p>

## Key Features:

1. **Create, Read, Update, Delete (CRUD):**
   - Leverage comprehensive CRUD functionalities for efficient management of flight data.

2. **Entity Relationships (1-M):**
   - Establish a clear 1-M relationship between Airlines and their associated Flights, as well as between Airplanes and their recorded Flights.

3. **Specific Queries:**
   - Augment the CRUD base with specialized queries:
      - View flights for a specific airline.
      - Access flights associated with a particular airplane.
      - Retrieve airplanes operated by a specific airline.

4. **Dynamic Search Bar:**
   - Integrate a dynamic search bar allowing users to filter data effortlessly. Users can:
      - Search for specific Airlines, case insensitive so no problem about capital letters or lower letters.
      - Filter by Airplanes.
      - Look up individual Flights directly.

5. **Time Zone Offset Management:**
   - The system adeptly manages time zone offsets related to server timestamps. This ensures accurate recording of each new flight entry and any updates made.
   - Using the local time zone of each of the destinations, the adjustment was made to a single time zone to obtain the flight time.
