using System;
using CSharpGPIO.Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CSharpGPIO.WebApi.Models
{
    public class GPIOModel
    {
        public string Number { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public GPIODirection Direction {get; set;}

        public GPIOModel()
        {

        }

        public GPIOModel(GPIO gpio)
        {
            this.Number = gpio.Number;
            this.Direction = gpio.Direction;
        }
    }
}