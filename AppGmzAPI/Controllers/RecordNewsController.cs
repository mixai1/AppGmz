﻿using AppGmz.CQRS.Commands.RecordNewsCommand.Create;
using AppGmz.CQRS.Commands.RecordNewsCommand.Remove;
using AppGmz.CQRS.Queries.RecordNewsQueries.FindRecordNews;
using AppGmz.CQRS.Queries.RecordNewsQueries.GetAllRecordNews;
using AppGmz.CQRS.Queries.RecordNewsQueries.GetSomeRecordNews;
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

        [Route("records")]
        [HttpGet]
        //GET : api/RecordNews/records
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

        [Route("records/{numbers}")]
        [HttpGet]
        // GET: api/RecordNews/someRecords/{number}
        public async Task<IActionResult> GetSomeRecords(int numbers)
        {
            try
            {
                Log.Information(nameof(RecordNewsController.GetSomeRecords));
                _logger.LogInformation(nameof(RecordNewsController.GetSomeRecords));
                var result = await _mediator.Send(new GetSomeRecordNews(numbers));
                return Ok(result);
            }
            catch (Exception e)
            {
                Log.Error(nameof(RecordNewsController.FindNewsRecord), e);
                _logger.LogError(nameof(RecordNewsController.FindNewsRecord), e);
                return BadRequest("Error");
            }
        }

        [Route("record/{id}")]
        [HttpGet]
        //Get : /api/RecordNews/findRecord/{id}
        public async Task<IActionResult> FindNewsRecord(Guid id)
        {
            try
            {
                Log.Information(nameof(RecordNewsController.FindNewsRecord));
                _logger.LogInformation(nameof(RecordNewsController.FindNewsRecord));
                var result = await _mediator.Send(new FindRecordNews(id));
                return Ok(result);
            }
            catch (Exception e)
            {
                Log.Error(nameof(RecordNewsController.FindNewsRecord), e);
                _logger.LogError(nameof(RecordNewsController.FindNewsRecord), e);
                return BadRequest("Error");
            }
        }

        [Route("record")]
        [HttpPost]
        //POST : /api/RecordNews/createRecord
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
                    return BadRequest("Error");
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

        [Route("record/{id}")]
        [HttpDelete]
        //Delete : /api/RecordNews/removeRecord/{id}
        public async Task<IActionResult> RemoveNewsRecord(Guid id)
        {
            try
            {
                Log.Information(nameof(RecordNewsController.RemoveNewsRecord));
                _logger.LogInformation(nameof(RecordNewsController.RemoveNewsRecord));

                var result = await _mediator.Send(new RemoveRecordNews(id));
                if (result)
                {
                    return Ok();
                }
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