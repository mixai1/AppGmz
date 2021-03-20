using System;
using System.Threading;
using System.Threading.Tasks;
using AppGmz.Core;
using AppGmz.Models.DomainModels;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppGmz.CQRS.Commands.RecordNewsCommand.Create
{
    public class CreateRecordNewsHandler : IRequestHandler<CreateRecordNews, bool>
    {
        private readonly IRecordNewsRepository _repository;
        private readonly ILogger<CreateRecordNewsHandler> _logger;
        private readonly IMapper _mapper;

        public CreateRecordNewsHandler(IRecordNewsRepository repository, IMapper mapper, ILogger<CreateRecordNewsHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(CreateRecordNews request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _mapper.Map<RecordNews>(request.NewsRecordDto);
                var response = await _repository.CreateAsync(result);
                await _repository.SaveAsync(cancellationToken);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(CreateRecordNewsHandler.Handle), e);
                return false;
            }
        }
    }
}