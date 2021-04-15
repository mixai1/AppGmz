using System;
using AppGmz.Core;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using AppGmz.Models.DomainModels;

namespace AppGmz.CQRS.Commands.VacanciesCommand.Create
{
    public class CreateVacanciesHandler : IRequestHandler<CreateVacancies, bool>
    {
        private readonly IVacanciesRepository _repository;
        private readonly ILogger<CreateVacanciesHandler> _logger;
        private readonly IMapper _mapper;

        public CreateVacanciesHandler(IMapper mapper, ILogger<CreateVacanciesHandler> logger, IVacanciesRepository repository)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(CreateVacancies request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _mapper.Map<Vacancies>(request.CreateVacanciesDto);
                var response = await _repository.CreateAsync(result);
                await _repository.SaveAsync(cancellationToken);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(CreateVacanciesHandler.Handle), e);
                return false;
            }
        }
    }
}