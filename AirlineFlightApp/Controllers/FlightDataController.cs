using AirlineFlightApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace AirlineFlightApp.Controllers
{
    public class FlightDataController : ApiController
    {
        //utilizing the database connection
        private ApplicationDbContext db = new ApplicationDbContext();

        //GET: 
        //output a list of Flights in the system


        /// <summary>
        /// This is a GET method that will return a list of all the flights in the system. This method presents a foreach that will be responsible 
        /// for setting each of the FlightDto objects with the corresponding information. To do this, the information must first be collected, which will be a list of Flight datatypes. 
        /// Once this is done, the foreach will be run to have each of the data types as FlightDto. Additionally, this table has 2 Foreign Key that will be accessed through the class Airline
        /// for exaxmple to get the Airline name (AirlineName = f.Airline.AirlineName) and Airplane Model (AirplaneModel = f.Airplane.AirplaneModel)
        /// </summary>
        /// <example>
        /// Using browser => GET: api/FlightData/ListFlights
        /// 
        /// Using curl comands in the terminal => curl https://localhost:44379/api/FlightData/ListFlights
        /// </example>
        /// <returns>
        /// A list of Flights in the system
        /// GET: api/FlightData/ListFlights =>
        ///[{"FlightId":18,"FlightNumber":"AC 8606","From":"Montreal","To":"Boston","DepartureAirport":"Montréal-Pierre Elliott Trudeau International Airport (YUL)","DestinationAirport":"Boston Logan International Airport (BOS)","DepartureTime":"2024-02-06T13:10:00","ArrivalTime":"2024-02-06T14:38:00","TicketPrice":588.00,"TimeZoneFrom":"Eastern Standard Time","TimeZoneTo":"Eastern Standard Time","AirlineId":0,"AirlineName":"Air Canada","AirplaneId":0,"AirplaneModel":"Embraer E175SU\r\n"},
        /// {"FlightId":22,"FlightNumber":"AC 8607","From":"Boston","To":"Montreal","DepartureAirport":"Boston Logan International Airport (BOS)","DestinationAirport":"Montréal-Pierre Elliott Trudeau International Airport (YUL)","DepartureTime":"2024-02-06T15:40:00","ArrivalTime":"2024-02-06T17:08:00","TicketPrice":661.00,"TimeZoneFrom":"Eastern Standard Time","TimeZoneTo":"Eastern Standard Time","AirlineId":0,"AirlineName":"Air Canada","AirplaneId":0,"AirplaneModel":"Embraer E175SU\r\n"}]
        ///  
        /// curl https://localhost:44379/api/FlightData/ListFlights
        /// [{"FlightId":18,"FlightNumber":"AC 8606","From":"Montreal","To":"Boston","DepartureAirport":"Montréal-Pierre Elliott Trudeau International Airport (YUL)","DestinationAirport":"Boston Logan International Airport (BOS)","DepartureTime":"2024-02-06T13:10:00","ArrivalTime":"2024-02-06T14:38:00","TicketPrice":588.00,"TimeZoneFrom":"Eastern Standard Time","TimeZoneTo":"Eastern Standard Time","AirlineId":0,"AirlineName":"Air Canada","AirplaneId":0,"AirplaneModel":"Embraer E175SU\r\n"},
        /// {"FlightId":22,"FlightNumber":"AC 8607","From":"Boston","To":"Montreal","DepartureAirport":"Boston Logan International Airport (BOS)","DestinationAirport":"Montréal-Pierre Elliott Trudeau International Airport (YUL)","DepartureTime":"2024-02-06T15:40:00","ArrivalTime":"2024-02-06T17:08:00","TicketPrice":661.00,"TimeZoneFrom":"Eastern Standard Time","TimeZoneTo":"Eastern Standard Time","AirlineId":0,"AirlineName":"Air Canada","AirplaneId":0,"AirplaneModel":"Embraer E175SU\r\n"}]
        /// </returns>
        [HttpGet]
        public IEnumerable<FlightDto> ListFlights()
        {
            //sending a query to the database
            List<Flight> Flights = db.Flights.ToList();
            List<FlightDto> FlightsDtos = new List<FlightDto>();

            Flights.ForEach(f => FlightsDtos.Add(new FlightDto()
            {
                FlightId = f.FlightId,
                FlightNumber = f.FlightNumber,
                From = f.From,
                To = f.To,
                DepartureAirport = f.DepartureAirport,
                DestinationAirport = f.DestinationAirport,
                DepartureTime = f.DepartureTime,
                ArrivalTime = f.ArrivalTime,
                TicketPrice = f.TicketPrice,
                TimeZoneFrom = f.TimeZoneFrom,
                TimeZoneTo = f.TimeZoneTo,
                AirlineId = f.AirlineId,
                AirlineName = f.Airline.AirlineName,
                AirplaneId = f.AirplaneId,
                AirplaneModel = f.Airplane.AirplaneModel
            }));

            return FlightsDtos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <example>
        /// curl https://localhost:44379/api/FlightData/ListFlightsForAirline/26
        /// </example>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<FlightDto> ListFlightsForAirline(int id)
        {
            //sending a query to the database
            List<Flight> Flights = db.Flights.Where(f=>f.AirlineId==id).ToList();
            List<FlightDto> FlightsDtos = new List<FlightDto>();

            Flights.ForEach(f => FlightsDtos.Add(new FlightDto()
            {
                FlightId = f.FlightId,
                FlightNumber = f.FlightNumber,
                From = f.From,
                To = f.To,
                DepartureAirport = f.DepartureAirport,
                DestinationAirport = f.DestinationAirport,
                DepartureTime = f.DepartureTime,
                ArrivalTime = f.ArrivalTime,
                TicketPrice = f.TicketPrice,
                TimeZoneFrom = f.TimeZoneFrom,
                TimeZoneTo = f.TimeZoneTo,
                AirlineId = f.Airline.AirlineId,
                AirlineName = f.Airline.AirlineName,
                AirplaneId = f.Airplane.AirplaneId,
                AirplaneModel = f.Airplane.AirplaneModel
            }));

            return FlightsDtos;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <example>
        /// curl https://localhost:44379/api/FlightData/ListFlightsForAirplane/31
        /// </example>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<FlightDto> ListFlightsForAirplane(int id)
        {
            //sending a query to the database
            List<Flight> Flights = db.Flights.Where(f => f.AirplaneId == id).ToList();
            List<FlightDto> FlightsDtos = new List<FlightDto>();

            Flights.ForEach(f => FlightsDtos.Add(new FlightDto()
            {
                FlightId = f.FlightId,
                FlightNumber = f.FlightNumber,
                From = f.From,
                To = f.To,
                DepartureAirport = f.DepartureAirport,
                DestinationAirport = f.DestinationAirport,
                DepartureTime = f.DepartureTime,
                ArrivalTime = f.ArrivalTime,
                TicketPrice = f.TicketPrice,
                TimeZoneFrom = f.TimeZoneFrom,
                TimeZoneTo = f.TimeZoneTo,
                AirlineId = f.Airline.AirlineId,
                AirlineName = f.Airline.AirlineName,
                AirplaneId = f.Airplane.AirplaneId,
                AirplaneModel = f.Airplane.AirplaneModel
            }));

            return FlightsDtos;
        }


        /// <summary>
        /// This GET method returns an individual flight from the database by specifying the primary key FlightId
        /// Additionally, this method will allow you to collect the information on the Foreign Key IDs as well as the airline and the plane model.
        /// since these will be requested in Details View as well as in Edit View
        /// </summary>
        /// <example>
        /// Using browser => GET: api/FlightData/FindFlight/25
        /// 
        /// Using curl comands in the terminal => curl https://localhost:44379/api/FlightData/FindFlight/25
        /// </example>
        /// <param name="id">This is the FlightId parameter you want to search for.</param>
        /// <returns>
        /// A FlightDto object which represent the flight with the id given 
        /// 
        /// curl https://localhost:44379/api/FlightData/FindFlight/25
        /// {"FlightId":25,"FlightNumber":"AA 33","From":"New York","To":"Los Angeles","DepartureAirport":"John F. Kennedy International Airport (JFK)","DestinationAirport":"Los Angeles International Airport (LAX)","DepartureTime":"2024-02-06T06:00:00","ArrivalTime":"2024-02-06T09:23:00","TicketPrice":583.00,"TimeZoneFrom":"Eastern Standard Time","TimeZoneTo":"Pacific Standard Time","AirlineId":26,"AirlineName":"American\r\n","AirplaneId":28,"AirplaneModel":"Airbus A321-231\r\n"}
        /// 
        /// curl https://localhost:44379/api/FlightData/FindFlight/37
        /// {"FlightId":37,"FlightNumber":"AC 171","From":"Toronto","To":"Edmonton","DepartureAirport":"Toronto Pearson International Airport (YYZ)","DestinationAirport":"Edmonton International Airport (YEG)","DepartureTime":"2024-02-06T10:00:00","ArrivalTime":"2024-02-06T12:18:00","TicketPrice":850.00,"TimeZoneFrom":"Eastern Standard Time","TimeZoneTo":"Mountain Standard Time","AirlineId":28,"AirlineName":"Air Canada","AirplaneId":35,"AirplaneModel":"Airbus A220-300\r\n"}
        /// 
        /// </returns>
        [ResponseType(typeof(Flight))]
        [HttpGet]
        public IHttpActionResult FindFlight(int id)
        {
            Flight Flight = db.Flights.Find(id);

            if (Flight == null)
            {
                return NotFound();
            }

            FlightDto FlightDto = new FlightDto()
            {
                FlightId = Flight.FlightId,
                FlightNumber = Flight.FlightNumber,
                From = Flight.From,
                To = Flight.To,
                DepartureAirport = Flight.DepartureAirport,
                DestinationAirport = Flight.DestinationAirport,
                DepartureTime = Flight.DepartureTime,
                ArrivalTime = Flight.ArrivalTime,
                TicketPrice = Flight.TicketPrice,
                TimeZoneFrom = Flight.TimeZoneFrom,
                TimeZoneTo = Flight.TimeZoneTo,
                AirlineName = Flight.Airline.AirlineName,
                AirplaneModel = Flight.Airplane.AirplaneModel,
                AirlineId = Flight.Airline.AirlineId,
                AirplaneId = Flight.Airplane.AirplaneId
            };


            return Ok(FlightDto);
        }


        /// <summary>
        /// This method is responsible for deleting a flight record in the Flights table, the deleted record will be according to the value indicated in the id parameter
        /// </summary>
        /// <example>
        /// Because this method is POST, you must use the curl command in the terminal to delete the record
        /// 
        /// POST: api/FlightData/DeleteFlight/39
        /// curl -d "" https://localhost:44379/api/FlightData/DeleteFlight/39
        /// </example>
        /// <param name="id">This will indicate the FlightId value that you want to eliminate</param>
        /// <returns>
        /// curl -d "" https://localhost:44379/api/FlightData/DeleteFlight/39
        /// This method will not return anything on the console, it will delete a flight from the DB, in this case the record with the FlightId = 39 will be deleated
        /// </returns>
        [ResponseType(typeof(Flight))]
        [HttpPost]
        public IHttpActionResult DeleteFlight(int id)
        {
            Flight Flight = db.Flights.Find(id);
            if (Flight == null)
            {
                return NotFound();
            }

            db.Flights.Remove(Flight);
            db.SaveChanges();

            return Ok();
        }


        /// <summary>
        /// This method is responsible for receiving a new Flight record. For this, the Flight type object must be sent. In order to be able to test this method, 
        /// you must open the folder where the flight.json is located
        /// </summary>
        /// <example>
        /// curl -d @flight.json -H "Content-type: application/json" https://localhost:44379/api/FlightData/AddFlight
        /// The flight.json file contains the flight information that will be added to the DB.
        /// </example>
        /// <param name="Flight">This is the json object that contains the information of the flight to add.</param>
        /// <returns>
        /// {
        ///     "FlightId": 46,
        ///     "FlightNumber": "AC 426",
        ///     "From": "Toronto",
        ///     "To": "Montreal",
        ///     "DepartureAirport": "Toronto Pearson International Airport (YYZ)",
        ///     "DestinationAirport": "Montréal-Pierre Elliott Trudeau International Airport (YUL)",
        ///     "DepartureTime": "2/6/2024 8:30:00 AM",
        ///     "ArrivalTime": "2/6/2024 9:50:00 PM",
        ///     "TicketPrice": 965.00,
        ///     "TimeZoneFrom": "Eastern Standard Time",
        ///     "TimeZoneTo": "Eastern Standard Time",
        ///     "AirlineId": 28,
        ///     "AirplaneId": 35
        /// }
        /// </returns>
        [ResponseType(typeof(Flight))]
        [HttpPost]
        public IHttpActionResult AddFlight(Flight Flight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Flights.Add(Flight);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Flight.FlightId }, Flight);
        }

 

        /// <summary>
        /// This POST method will be in charge of updating the record according to the id parameter that is provided, for this both the value that is delivered by the url (id) 
        /// and the value that the FlightId (within the object received in FLight) have to match. otherwise the record is not updated
        /// additionally, because this method is post to check its operation you must use the curl command in the terminal. for this open the folder where the flight.json is located on the terminal screen
        /// 
        /// once you have the folder open you can use this command curl -d @flight.json -H "Content-type: application/json" https://localhost:44379/api/FlightData/UpdateFlight/39
        /// </summary>
        /// <example>
        /// curl -d @flight.json -H "Content-type: application/json" https://localhost:44379/api/FlightData/UpdateFlight/39
        /// </example>
        /// <param name="id">The value of the FlightId to update</param>
        /// <param name="Flight">The Flight object, this parameter holds the new data, with this data the method will update the specified Flight</param>
        /// <returns>
        /// This method will not return anything on the console, but the method will return whether the update was successful or not, in case it was not successful because there was no id match (id != Flight.FlightId)
        /// The values of the Flight object and the id sent in the url are printed in the console. If the method runs, without problem, the record will be updated.
        /// </returns>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateFlight(int id, Flight Flight)
        {
            Debug.WriteLine("Update Flight method starts here");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != Flight.FlightId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter " + id);
                Debug.WriteLine("POST parameter " + Flight.FlightId);
                Debug.WriteLine("POST parameter " + Flight.FlightNumber);
                Debug.WriteLine("POST parameter " + Flight.From);
                Debug.WriteLine("POST parameter " + Flight.To);
                Debug.WriteLine("POST parameter " + Flight.DepartureAirport);
                Debug.WriteLine("POST parameter " + Flight.DestinationAirport);
                Debug.WriteLine("POST parameter " + Flight.DepartureTime);
                Debug.WriteLine("POST parameter " + Flight.ArrivalTime);
                Debug.WriteLine("POST parameter " + Flight.TicketPrice);
                Debug.WriteLine("POST parameter " + Flight.TimeZoneFrom);
                Debug.WriteLine("POST parameter " + Flight.TimeZoneTo);
                Debug.WriteLine("POST parameter " + Flight.AirlineId);
                Debug.WriteLine("POST parameter " + Flight.AirplaneId);
                return BadRequest();
            }

            db.Entry(Flight).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightExists(id))
                {
                    Debug.WriteLine("Flight not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        /// <summary>
        /// This method is responsible for returning a bool indicating if the flight exists
        /// </summary>
        /// <param name="id">The value of the FlightId to update</param>
        /// <returns>A bool indicating if the record exists</returns>
        private bool FlightExists(int id)
        {
            return db.Flights.Count(e => e.FlightId == id) > 0;
        }
    }
}
