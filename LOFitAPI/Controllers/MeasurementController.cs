﻿using LOFitAPI.DbControllers;
using LOFitAPI.DbControllers.Accounts;
using LOFitAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LOFitAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MeasurementController : ControllerBase
    {
        [HttpGet]
        [Route("{dateString}")]
        public ActionResult<PomiarModel> Get(string dateString)
        {
            DateTime date = DateTime.Parse(dateString);

            int? idUsera = KontoDbController.ReturnUserId(User?.Identity?.Name);

            if (idUsera == null)
                return Unauthorized();

            PomiarModel measurement = PomiarDbController.Get(date, (int)idUsera);

            return Ok(measurement);
        }
        [HttpPost]
        public ActionResult<string> Add(PomiarModel measurement)
        {
            int? idUsera = KontoDbController.ReturnUserId(User.Identity.Name);

            if (idUsera == null)
                return Unauthorized();

            measurement.Id_usera = (int)idUsera;

            string answer = PomiarDbController.Add(measurement);

            return Ok(answer);
        }

        [HttpPut]
        public ActionResult<string> Update(PomiarModel measurement)
        {
            int? idUsera = KontoDbController.ReturnUserId(User.Identity.Name);

            if (idUsera == null)
                return Unauthorized();

            measurement.Id_usera = (int)idUsera;

            string answer = PomiarDbController.Update(measurement);

            return Ok(answer);
        }
    }

}
