using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSharpGPIO.Library;
using CSharpGPIO.WebApi.Models;

namespace CSharpGPIO.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class GPIOController : Controller
    {
        private static IDictionary<string, GPIO> GPIOPool;

        // GET api/GPIO
        [HttpGet]
        public IEnumerable<GPIOModel> Get()
        {
            var result = new List<GPIOModel>();
            if (GPIOPool != null)
            {
                foreach(var gpio in GPIOPool.Values)
                {
                    var gpioModel = new GPIOModel(gpio);
                    result.Add(gpioModel);
                }
            }

            return result;
        }

        // GET api/GPIO/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            if (GPIOPool != null && GPIOPool.ContainsKey(id)) {
                return GPIOPool[id].GetValue() ? "1" : "0";
            }
            return null;
        }

        // POST api/GPIO
        [HttpPost]
        public void Post([FromBody]GPIOModel model)
        {
            if (GPIOPool == null) {
                GPIOPool = new Dictionary<string, GPIO>();
            }

            if (!GPIOPool.ContainsKey(model.Number)) {
                GPIOPool[model.Number] = new GPIO(model.Number, model.Direction);
            }
        }

        // PUT api/GPIO/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]string value)
        {
            if (GPIOPool != null && GPIOPool.ContainsKey(id)) {
                var gpio = GPIOPool[id];
                bool boolValue = !string.IsNullOrWhiteSpace(value) && value != "0";
                gpio.SetValue(boolValue);
            }
        }

        // DELETE api/GPIO/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            if (GPIOPool != null && GPIOPool.ContainsKey(id)) {
                GPIOPool.Remove(id);
            }
        }
    }
}
