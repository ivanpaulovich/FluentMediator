using System.ComponentModel.DataAnnotations;
using Core.UseCases;
using FluentMediator;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Register
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public IActionResult Post([FromBody][Required] RegisterRequest request)
        {
            var registerCommand = new RegisterCommand(
                request.Name,
                request.SSN,
                request.Balance
            );

            var response = _mediator.Send<RegisterResponse>(registerCommand);

            return Ok(response);
        }
    }
}