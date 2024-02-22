﻿using AirlineFlightApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Diagnostics;
using AirlineFlightApp.Models.ViewModels;

namespace AirlineFlightApp.Controllers
{
    public class AirlineController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static AirlineController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44379/api/");
        }

        /// <summary>
        /// GET: Airline/List
        /// This GET method is responsible for making the call to the airline API, in which it will collect the list of airlines and provide the collected information to the View.
        /// This code is responsible for utilizing the client.BaseAddress and calling the ListAirlines method
        /// Go to  -> /Views/Airline/List.cshtml
        /// </summary>
        /// <returns>
        /// Returns the List View, which will display a list of the airlines in the system. Each of the airlines in the database will be of the datatype AirlineDto.
        /// </returns>
        public ActionResult List(string AirlineSearch = null)
        {
            //communicate with the airline data api to retrieve a list of airlines
            //curl https://localhost:44379/api/AirlineData/ListAirlines

            string url = "AirlineData/ListAirlines/" + AirlineSearch;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine(url);

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<AirlineDto> airlines = response.Content.ReadAsAsync<IEnumerable<AirlineDto>>().Result;
            //Debug.WriteLine("Number of airlines received : ");
            //Debug.WriteLine(airlines.Count());

            return View(airlines);
        }

        /// <summary>
        /// GET: Airline/Details/{id}
        /// This GET method will be responsible for calling the FindAirline method from the airline API and returning the view with the API response.
        /// Go to  -> /Views/Airline/Details.cshtml
        /// </summary>
        /// <param name="id">This is an int datatype parameter of the airline you want to find.</param>
        /// <returns>
        /// Return the Details View of the airline found by the ID given in the URL. This airline will be of the datatype AirlineDto
        /// </returns>
        public ActionResult Details(int id)
        {
            //communicate with the airline data api to retrieve one airline
            //curl https://localhost:44379/api/AirlineData/FindAirline/{id}

            //instance of ViewModel
            DetailsAirline ViewModel = new DetailsAirline();

            string url = "AirlineData/FindAirline/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            AirlineDto SelectedAirline = response.Content.ReadAsAsync<AirlineDto>().Result;
            ViewModel.SelectedAirline = SelectedAirline;
            //Debug.WriteLine("airline received : ");
            //Debug.WriteLine(selectedairline.AirlineName);

            //showcase information about Flights related to this Airline -> ListFlightsForAirline
            //send a request to gather information about Flights related to a particular ID 
            url = "FlightData/ListFlightsForAirline/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<FlightDto> RelatedFlights = response.Content.ReadAsAsync<IEnumerable<FlightDto>>().Result;
            ViewModel.RelatedFlights = RelatedFlights;

            /*showcase information about Airplanes related to this Airline -> ListPlanesForAirline
            send a request to gather information about Flights related to a particular ID 
            The flights will hold information about the AirplaneModel and RegistrationNum
            Even though there will be repeated airplanes, only the first one is selected, this was done by
            
            List<FlightDto> FlightsDtosUnique = FlightsDtos
            .GroupBy(flight => flight.RegistrationNum)
            .Select(group => group.First())
            .ToList();

            This is through LINQ that grouped the airplanes through the RegistrationNum and then only selected the 
            first element of the group, thus allowing not to have duplicate airplanes*/
            url = "FlightData/ListPlanesForAirline/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<FlightDto> RelatedAirplanes = response.Content.ReadAsAsync<IEnumerable<FlightDto>>().Result;
            ViewModel.RelatedAirplanes = RelatedAirplanes;

            return View(ViewModel);
        }

        /// <summary>
        /// GET: Airline/New
        /// GET method to add a new airline to the system, responsible for providing the view of the form for inserting a new airline.
        /// Go to  -> /Views/Airline/New.cshtml
        /// </summary>
        /// <returns>
        /// Returns the view of the form so that the user can insert a new airline.
        /// </returns>
        public ActionResult New()
        {
            return View();
        }


        /// <summary>
        /// POST: Airline/Create
        /// This POST method will be in charge of receiving the information sent by the new form, once the information is received 
        /// the method will be in charge of processing the conversion of the Airline object to json in order to be sent in the body of the HTTP REQUEST
        /// Additionally, it is indicated that its content is of type json in the reques header. Once this is done, 
        /// a POST request will be sent to the specified Uri as an asynchronous operation. If its IsSuccessStatusCode is true, 
        /// it will redirect the user to the list page, otherwise it will indicate to the user that there is an error.
        /// Go to (if success) -> /Views/Airline/List.cshtml
        /// Go to (if not success) -> /Views/Airline/Error.cshtml
        /// </summary>
        /// <param name="airline">This parameter represents the object received by the form for creating a new airline.</param>
        /// <returns>
        /// Returns the user to either the List View or the Error View, depending on the response StatusCode
        /// </returns>
        [HttpPost]
        public ActionResult Create(Airline airline)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(airline.AirlineName);
            //objective: add a new airline into the system using the API
            //curl -d @airline.json -H "Content-type: application/json" https://localhost:44379/api/AirlineData/AddAirline 
            string url = "AirlineData/AddAirline";

            string jsonpayload = jss.Serialize(airline);

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
        /// GET: Airline/Edit/{id}
        /// This GET method is in charge of collecting and sending the informaction to the View which will have a form with the airline information that is requested by its id, 
        /// for this the api/AirlineData/FindAirline/{id} is used. Once the call to the API is made, the information collected of the datatype AirlineDto will be sent to the view. 
        /// In this way the form will be populated with the information of the airline
        /// Go to  -> /Views/Airline/Edit.cshtml
        /// </summary>
        /// <param name="id">This is an int datatype parameter of the AirlineId value providaded by the url that will be displayed in the form in order to make an update</param>
        /// <returns>
        /// Returns the view with the form filled with the information of the airline to update
        /// </returns>
        public ActionResult Edit(int id)
        {
            string url = "AirlineData/FindAirline/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            AirlineDto selectedairline = response.Content.ReadAsAsync<AirlineDto>().Result;

            Debug.WriteLine(selectedairline.AirlineName);
            Debug.WriteLine(selectedairline.FoundingYear);

            Debug.WriteLine("code --> " + (int)(response.StatusCode));

            return View(selectedairline);
        }

        /// <summary>
        /// POST: Airline/Update/{id}
        /// This POST method is responsible for making the call to the UpdateAirline method of the airline api. The information collected by the form will be sent in the body of the request
        /// Go to (if success) -> /Views/Airline/Details/{id}.cshtml
        /// Go to (if not success) -> /Views/Airline/Error.cshtml
        /// </summary>
        /// <param name="id">This is the parameter provided by the url that identifies the AirlineId that is going to be modified</param>
        /// <param name="airline">The airline object, this parameter holds the new data, this new data will be sent as a body to the UpdateAirline method of the Airline API</param>
        /// <returns>
        /// If the update is satisfactory the user will be redirected to the airline list, otherwise it will be sent to the error page
        /// </returns>
        [HttpPost]
        public ActionResult Update(int id, Airline airline)
        {
            //serialize into JSON
            //Send the request to the API
            string url = "AirlineData/UpdateAirline/" + id;

            string jsonpayload = jss.Serialize(airline);
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
        /// GET: Airline/DeleteConfirm/{id}
        /// This is a GET method that is responsible for finding the information of the airline to delete, this is done through its airline id which is provided by the id of the url
        /// Go to  -> /Views/Airline/DeleteConfirm.cshtml
        /// </summary>
        /// <param name="id">This is an int datatype parameter of the airline that will be displayed in DeleteConfirm View in order to delete the record</param>
        /// <returns>
        /// Returns a view that provides information about the airline to delete, this is through the selectedairline that was found by the supplied id
        /// </returns>
        public ActionResult DeleteConfirm(int id)
        {
            string url = "AirlineData/FindAirline/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AirlineDto selectedairline = response.Content.ReadAsAsync<AirlineDto>().Result;
            return View(selectedairline);
        }

        /// <summary>
        /// POST: Airline/Delete/{id}
        /// This POST method is responsible for making the request to api/AirlineData/DeleteAirline to be able to delete the indicated airline from the database. 
        /// If the IsSuccessStatusCode is true it will send the user to the list of airlines, otherwise it will send the user to the Error page
        /// Go to (if success) -> /Views/Airline/List.cshtml
        /// Go to (if not success) -> /Views/Airline/Error.cshtml
        /// </summary>
        /// <param name="id">This id indicates the AirlineId that will be used to determine the airline that will be deleted</param>
        /// <returns>
        /// If the deletion is completed and no error occurs the user will be directed to the list of airlines which will not show the recently deleted airline. 
        /// If the IsSuccessStatusCode is false, this will indicate that the record was not deleted and the user will be directed to the View Error 
        /// </returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "AirlineData/DeleteAirline/" + id;
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
        /// Go to -> /Views/Airline/Error.cshtml
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