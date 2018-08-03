using BeerCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BeerCollection.Controllers
{
    public class BeerController : ApiController
    {
        public BeerController()
        {
        }
        [HttpGet]
        public IHttpActionResult GetAllBeers(string name = null)
        {
            var result = GetJsonData.GetAllData(name);

            if (result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }
        [HttpPost]
        public IHttpActionResult PostNewBeer(BeerData beer)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            var status = GetJsonData.PostNewBeer(beer);
            if (status)
               return Ok();
            else
               return BadRequest("Invalid data.");
        }
        [HttpPut]
        public IHttpActionResult UpdateBeer(BeerData beer)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            var status = GetJsonData.UpdateBeerRating(beer);
            if (status)
                return Ok();
            else
                return BadRequest("Invalid data.");
        }
        [HttpDelete]
        public IHttpActionResult Delete(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Invalid data.");

            var status = GetJsonData.DeleteBeer(name);
            if (status)
                return Ok();
            else
                return BadRequest("Invalid data.");
        }
    }
}
