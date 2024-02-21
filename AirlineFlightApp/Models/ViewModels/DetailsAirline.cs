﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineFlightApp.Models.ViewModels
{
    public class DetailsAirline
    {

        public AirlineDto SelectedAirline { get; set; }
        public IEnumerable<FlightDto> RelatedFlights { get; set; }

    }
}