using AppGmz.CQRS.Commands.RecordNewsCommand.Create;
using AppGmz.CQRS.Commands.RecordNewsCommand.Remove;
using AppGmz.CQRS.Queries.RecordNewsQueries.GetAllRecordNews;
using AppGmz.Models.DtoModels;
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
    public class RecordNewsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RecordNewsController> _logger;

        public RecordNewsController(IMediator mediator, ILogger<RecordNewsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Route("getAll")]
        [HttpGet]
        //GET : api/RecordNews/getAll
        public async Task<IActionResult> GetAllNewsRecord()
        {
            try
            {
                _logger.LogInformation(nameof(RecordNewsController.GetAllNewsRecord));
                var result = await _mediator.Send(new GetAllRecordNews());
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(RecordNewsController), e);
                Log.Error(nameof(RecordNewsController), e);
                return BadRequest("Error");
            }
        }

        [Route("create")]
        [HttpPost]
        //POST : /api/RecordNews/create
        public async Task<IActionResult> CreateNewsRecord(CreateRecordNewsDto newsRecordDto)
        {
            try
            {
                _logger.LogInformation(nameof(RecordNewsController.CreateNewsRecord));
                Log.Information(nameof(RecordNewsController.CreateNewsRecord));
                if (TryValidateModel(newsRecordDto))
                {
                    var result = await _mediator.Send(new CreateRecordNews(newsRecordDto));
                    if (result)
                    {
                        return Ok();
                    }
                    return BadRequest();
                }
                Log.Error(nameof(RecordNewsController.CreateNewsRecord));
                _logger.LogError(nameof(RecordNewsController.CreateNewsRecord));
                return BadRequest("Error");
            }
            catch (Exception e)
            {
                Log.Error(nameof(RecordNewsController.CreateNewsRecord), e);
                _logger.LogError(nameof(RecordNewsController.CreateNewsRecord), e);
                return BadRequest("Error");
            }
        }

        [Route("remove")]
        [HttpPost]
        //POST : /api/RecordNews/remove
        public async Task<IActionResult> RemoveNewsRecord(RemoveRecordNewsDto removeNewsRecordDto)
        {
            try
            {
                Log.Information(nameof(RecordNewsController.RemoveNewsRecord));
                _logger.LogInformation(nameof(RecordNewsController.RemoveNewsRecord));
                if (TryValidateModel(removeNewsRecordDto))
                {
                    var result = await _mediator.Send(new RemoveRecordNews(removeNewsRecordDto));
                    if (result)
                    {
                        return Ok();
                    }
                    return BadRequest();
                }

                Log.Error(nameof(RecordNewsController.RemoveNewsRecord));
                _logger.LogError(nameof(RecordNewsController.RemoveNewsRecord));
                return BadRequest("Error");
            }
            catch (Exception e)
            {
                Log.Error(nameof(RecordNewsController.RemoveNewsRecord), e);
                _logger.LogError(nameof(RecordNewsController.RemoveNewsRecord), e);
                return BadRequest("Error");
            }
        }
    }
}