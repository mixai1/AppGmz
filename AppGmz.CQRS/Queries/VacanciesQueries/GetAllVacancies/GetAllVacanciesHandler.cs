using AppGmz.Core;
using AppGmz.Models.DtoModels.VacanciesDTO;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AppGmz.CQRS.Queries.VacanciesQueries.GetAllVacancies
{
    public class GetAllVacanciesHandler : IRequestHandler<GetAllVacancies, IEnumerable<ShowVacanciesDto>>
    {
        private readonly IVacanciesRepository _repository;
        private readonly ILogger<GetAllVacanciesHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllVacanciesHandler(IVacanciesRepository repository, ILogger<GetAllVacanciesHandler> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShowVacanciesDto>> Handle(GetAllVacancies request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _repository.GetAllAsync();
                if (response != null)
                {
                    var result = _mapper.Map<IEnumerable<ShowVacanciesDto>>(response);
                    return result;
                }
                return new List<ShowVacanciesDto>();
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(GetAllVacanciesHandler.Handle), e);
                return new List<ShowVacanciesDto>();
            }
        }
    }
}