using AppGmz.CQRS.Commands.VacanciesCommand.Create;
using AppGmz.CQRS.Commands.VacanciesCommand.Remove;
using AppGmz.CQRS.Queries.VacanciesQueries.GetAllVacancies;
using AppGmz.CQRS.Queries.VacanciesQueries.GetDetailVacancies;
using AppGmz.Models.DtoModels.VacanciesDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;

namespace AppGmzAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacanciesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<VacanciesController> _logger;

        public VacanciesController(IMediator mediator, ILogger<VacanciesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Route("vacancies")]
        [HttpGet]
        public async Task<IActionResult> GetAllVacancies()
        {
            try
            {
                _logger.LogInformation(nameof(VacanciesController.GetAllVacancies));
                var result = await _mediator.Send(new GetAllVacancies());
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(VacanciesController.GetAllVacancies), e);
                Log.Error(nameof(VacanciesController.GetAllVacancies), e);
                return BadRequest("Error");
            }
        }

        [Route("vacancies/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetDetailVacancies(Guid id)
        {
            try
            {
                _logger.LogInformation(nameof(VacanciesController.GetDetailVacancies));
                var result = await _mediator.Send(new GetDetailVacancies(id));
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Route("vacancies")]
        [HttpPost]
        public async Task<IActionResult> CreateVacancies(CreateVacanciesDto createVacanciesDto)
        {
            try
            {
                if (TryValidateModel(createVacanciesDto))
                {
                    Log.Information(nameof(VacanciesController.CreateVacancies));
                    _logger.LogInformation(nameof(VacanciesController.CreateVacancies));
                    var result = await _mediator.Send(new CreateVacancies(createVacanciesDto));
                    if (result)
                    { 
                        return Ok();
                    }

                    return BadRequest("Error");
                }

                Log.Error(nameof(VacanciesController.CreateVacancies));
                _logger.LogError(nameof(VacanciesController.CreateVacancies));
                return BadRequest("Error");
            }
            catch (Exception e)
            {
                Log.Error(nameof(VacanciesController.CreateVacancies), e);
                _logger.LogError(nameof(VacanciesController.CreateVacancies), e);
                return BadRequest("Error");
            }
        }

        [Route("vacancies/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteVacancies(Guid id)
        {
            try
            {
                Log.Information(nameof(VacanciesController.CreateVacancies));
                _logger.LogInformation(nameof(VacanciesController.CreateVacancies));
                var result = await _mediator.Send(new RemoveVacancies(id));
                if (result)
                {
                    return Ok();
                }
                return BadRequest("Error");
            }
            catch (Exception e)
            {
                Log.Error(nameof(VacanciesController.CreateVacancies), e);
                _logger.LogError(nameof(VacanciesController.CreateVacancies), e);
                return BadRequest("Error");
            }
        }
    }
}