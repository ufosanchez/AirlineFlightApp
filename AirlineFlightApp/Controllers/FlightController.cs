using AirlineFlightApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AirlineFlightApp.Controllers
{
    public class FlightController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static FlightController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44379/api/FlightData/");
        }

        /// <summary>
        /// GET: Flight/List
        /// This GET method is responsible for making the call to the flight API, in which it will collect the list of flights and provide the collected information to the View.
        /// This code is responsible for utilizing the client.BaseAddress and calling the ListFlights method
        /// Go to  -> /Views/Flight/List.cshtml
        /// </summary>
        /// <returns>
        /// Returns the List View, which will display a list of the flights in the system. Each of the flights in the database will be of the datatype FlightDto.
        /// </returns>
        public ActionResult List()
        {
            //communicate with the flight data api to retrieve a list of flights
            //curl https://localhost:44379/api/FlightData/ListFlights

            string url = "ListFlights";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<FlightDto> flights = response.Content.ReadAsAsync<IEnumerable<FlightDto>>().Result;

            return View(flights);
        }

        /// <summary>
        /// GET: Flight/Details/{id}
        /// This GET method will be responsible for calling the FindFlight method from the flight API and returning the view with the API response.
        /// Go to  -> /Views/Flight/Details.cshtml
        /// </summary>
        /// <param name="id">This is an int datatype parameter of the flight you want to find.</param>
        /// <returns>
        /// Return the Details View of the flight found by the ID given in the URL. This flight will be of the datatype FlightDto
        /// </returns>
        public ActionResult Details(int id)
        {
            //communicate with the flight data api to retrieve one flight
            //curl https://localhost:44379/api/FlightData/FindFlight/{id}

            string url = "FindFlight/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            FlightDto selectedflight = response.Content.ReadAsAsync<FlightDto>().Result;
            return View(selectedflight);
        }

        /// <summary>
        /// GET: Flight/New
        /// GET method to add a new flight to the system, responsible for providing the view of the form for inserting a new flight.
        /// Go to  -> /Views/Flight/New.cshtml
        /// </summary>
        /// <returns>
        /// Returns the view of the form so that the user can insert a new flight.
        /// </returns>
        public ActionResult New()
        {
            return View();
        }

        /// <summary>
        /// POST: Flight/Create
        /// This POST method will be in charge of receiving the information sent by the new form, once the information is received 
        /// the method will be in charge of processing the conversion of the Flight object to json in order to be sent in the body of the HTTP REQUEST
        /// Additionally, it is indicated that its content is of type json in the reques header. Once this is done, 
        /// A POST request will be sent to the specified Uri as an asynchronous operation. If its IsSuccessStatusCode is true, 
        /// it will redirect the user to the list Flights page, otherwise it will indicate to the user that there is an error.
        /// Go to (if new flight success) -> /Views/Flight/List.cshtml
        /// Go to (if not success) -> /Views/Flight/Error.cshtml
        /// </summary>
        /// <param name="flight">This parameter represents the object received by the form for creating a new flight.</param>
        /// <returns>
        /// Returns the user to either the List View or the Error View, depending on the response StatusCode
        /// </returns>
        [HttpPost]
        public ActionResult Create(Flight flight)
        {
            //objective: add a new flight into the system using the API
            //curl -d @flight.json -H "Content-type: application/json" https://localhost:44379/api/FlightData/AddFlight
            string url = "AddFlight";

            string jsonpayload = jss.Serialize(flight);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        /// <summary>
        /// GET: Flight/Edit/{id}
        /// This GET method is in charge of collecting and sending the informaction to the View which will have a form with the flight information that is requested by its id, 
        /// for this the api/FlightData/FindFlight/{id} is used. Once the call to the API is made, the information collected of the datatype FlightDto will be sent to the view. 
        /// In this way the form will be populated with the information of the flight
        /// Go to  -> /Views/Flight/Edit.cshtml
        /// </summary>
        /// <param name="id">This is an int datatype parameter of the FlightId value that will be displayed in the form in order to make an update</param>
        /// <returns>
        /// Returns the view with the form filled with the information of the flight to update
        /// </returns>
        public ActionResult Edit(int id)
        {
            string url = "FindFlight/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            FlightDto selectedflight = response.Content.ReadAsAsync<FlightDto>().Result;
            return View(selectedflight);
        }

        /// <summary>
        /// POST: Flight/Update/{id}
        /// This POST method is responsible for making the call to the UpdateFlight method of the flight api. The information collected by the form will be sent in the body of the request
        /// Go to (if success) -> /Views/Flight/Details/{id}.cshtml
        /// Go to (if not success) -> /Views/Flight/Error.cshtml
        /// </summary>
        /// <param name="id">This is the parameter provided by the url that identifies the FlightId that is going to be updated</param>
        /// <param name="flight">The flight object, this parameter holds the new data, this new data will be sent as a body to the UpdateFlight method of the Flight API</param>
        /// <returns>
        /// If the update is satisfactory the user will be redirected to the flight list, otherwise it will be sent to the error page
        /// </returns>
        [HttpPost]
        public ActionResult Update(int id, Flight flight)
        {
            //serialize into JSON
            //Send the request to the API
            string url = "UpdateFlight/" + id;

            string jsonpayload = jss.Serialize(flight);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            Debug.WriteLine("code --> " + (int)(response.StatusCode));
            Debug.WriteLine("status --> " + response.IsSuccessStatusCode);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details/" + id);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        /// <summary>
        /// GET: Flight/DeleteConfirm/{id}
        /// This is a GET method that is responsible for finding the information of the flight to delete, this is done through its flight id which is provided by the id of the url
        /// Go to  -> /Views/Flight/DeleteConfirm.cshtml
        /// </summary>
        /// <param name="id">This is an int datatype parameter of the FlightId that will be displayed in DeleteConfirm View in order to delete the record</param>
        /// <returns>
        /// Returns a view that provides information about the flight to delete, this is through the selectedflight that was found by the supplied id
        /// </returns>
        public ActionResult DeleteConfirm(int id)
        {
            string url = "FindFlight/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            FlightDto selectedflight = response.Content.ReadAsAsync<FlightDto>().Result;
            return View(selectedflight);
        }

        /// <summary>
        /// POST: Flight/Delete/{id}
        /// This POST method is responsible for making the request to api/FlightData/DeleteFlight to be able to delete the indicated flight from the database. 
        /// If the IsSuccessStatusCode is true it will send the user to the list of flights, otherwise it will send the user to the Error page
        /// Go to (if success) -> /Views/Flight/List.cshtml
        /// Go to (if not success) -> /Views/Flight/Error.cshtml
        /// </summary>
        /// <param name="id">This id indicates the FlightId that will be used to determine the flight that will be deleted</param>
        /// <returns>
        /// If the deletion is completed and no error occurs the user will be directed to the list of flights which will not show the recently deleted flight. 
        /// If the IsSuccessStatusCode is false, this will indicate that the record was not deleted and the user will be directed to the View Error 
        /// </returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "DeleteFlight/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        /// <summary>
        /// This Get method is responsible for returning the view when an error occurs, such as not finding the ID in the system or some of the operations that did not work
        /// Go to -> /Views/Flight/Error.cshtml
        /// </summary>
        /// <returns>
        /// Return the Error View
        /// </returns>
        public ActionResult Error()
        {
            return View();
        }
    }
}