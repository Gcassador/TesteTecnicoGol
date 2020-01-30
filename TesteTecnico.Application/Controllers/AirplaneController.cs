using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TesteTecnico.Application.ViewModels;
using TesteTecnico.Domain.Entities;
using TesteTecnico.Domain.Interfaces;
using TesteTecnico.Service.Validators;

namespace TesteTecnico.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirplaneController : ControllerBase
    {

        private readonly IService<Airplane> _service;

        public AirplaneController(
            IService<Airplane> service
            )
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<AirplaneViewModel> Get()
        {
            return _service.Get().Select(x => new
             AirplaneViewModel
            {
                Id = x.Id,
                Model = x.Model,
                NumberOfPassenger = x.NumberOfPassengers,
                CreatedDate = x.CreatedDate
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody] AirplaneViewModel airplaneView)
        {
            try
            {
                Airplane airplane = new Airplane(airplaneView.Model, airplaneView.NumberOfPassenger, airplaneView.CreatedDate, null);

                _service.Post<AirplaneValidator>(airplane);

                return new ObjectResult(airplane.Id);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] AirplaneViewModel airplaneView)
        {
            try
            {
                Airplane airplane = new Airplane(airplaneView.Model, airplaneView.NumberOfPassenger, airplaneView.CreatedDate, airplaneView.Id);

                _service.Put<AirplaneValidator>(airplane);

                return new ObjectResult(airplane);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            try
            {
                _service.Delete(Id);

                return new NoContentResult();
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}