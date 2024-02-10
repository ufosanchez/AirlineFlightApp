﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineFlightApp.Models
{
    public class Airplane
    {
        //the following fields define an Airplane
        [Key]
        public int AirplaneId { get; set; }

        public string AirplaneModel { get; set; }

        public string RegistrationNum { get; set; }

        public string ManufacturerName { get; set; }

        public DateTime ManufactureYear { get; set; }

        public int MaxPassenger { get; set; }

        public string EngineModel { get; set; }

        public decimal Speed { get; set; }

        public decimal Range { get; set; }
    }

    public class AirplaneDto
    {
        public int AirplaneId { get; set; }

        public string AirplaneModel { get; set; }

        public string RegistrationNum { get; set; }

        public string ManufacturerName { get; set; }

        public DateTime ManufactureYear { get; set; }

        public int MaxPassenger { get; set; }

        public string EngineModel { get; set; }

        public decimal Speed { get; set; }

        public decimal Range { get; set; }
    }
}