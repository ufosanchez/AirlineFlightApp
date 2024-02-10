﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineFlightApp.Models
{
    public class Flight
    {
        //the following fields define a Flight
        [Key]
        public int FlightId { get; set; }

        public string FlightNumber { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string DepartureAirport { get; set; }

        public string DestinationAirport { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public Decimal TicketPrice { get; set; }

        public string TimeZoneFrom { get; set; }

        public string TimeZoneTo { get; set; }

        //Foreign key that refers the Airline entity
        //A Flight belongs to one Airline Company
        //An Airline Company can have many Flights
        [ForeignKey("Airline")]
        public int AirlineId { get; set; }
        public virtual Airline Airline { get; set; }

        //Foreign key that refers the Airplane entity
        //A Flight belongs to one Airplane
        //An Airplane can have many Flights
        [ForeignKey("Airplane")]
        public int AirplaneId { get; set; }
        public virtual Airplane Airplane { get; set; }
    }

    public class FlightDto
    {
        public int FlightId { get; set; }

        public string FlightNumber { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string DepartureAirport { get; set; }

        public string DestinationAirport { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public Decimal TicketPrice { get; set; }

        public string TimeZoneFrom { get; set; }

        public string TimeZoneTo { get; set; }

        public int AirlineId { get; set; }
        public string AirlineName { get; set; }

        public int AirplaneId { get; set; }
        public string AirplaneModel { get; set; }
    }
}