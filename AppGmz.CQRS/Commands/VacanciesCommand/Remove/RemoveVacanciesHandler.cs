using System;
using System.Threading;
using System.Threading.Tasks;
using AppGmz.Core;
using AppGmz.CQRS.Commands.VacanciesCommand.Create;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppGmz.CQRS.Commands.VacanciesCommand.Remove
{
    public class RemoveVacanciesHandler : IRequestHandler<RemoveVacancies, bool>
    {
        private readonly IVacanciesRepository _repository;
        private readonly ILogger<RemoveVacanciesHandler> _logger;
        private readonly IMapper _mapper;

        public RemoveVacanciesHandler(IMapper mapper, ILogger<RemoveVacanciesHandler> logger, IVacanciesRepository repository)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(RemoveVacancies request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _repository.RemoveById(request.Id);
                await _repository.SaveAsync(cancellationToken);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(RemoveVacanciesHandler.Handle), e);
                return false;
            }
        }
    }
}