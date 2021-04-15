using AppGmz.Core;
using AppGmz.Models.DtoModels.VacanciesDTO;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppGmz.CQRS.Queries.VacanciesQueries.GetDetailVacancies
{
    public class GetDetailVacanciesHandler : IRequestHandler<GetDetailVacancies, DetailVacanciesDto>
    {
        private readonly IVacanciesRepository _repository;
        private readonly ILogger<GetDetailVacanciesHandler> _logger;
        private readonly IMapper _mapper;

        public GetDetailVacanciesHandler(IVacanciesRepository repository, ILogger<GetDetailVacanciesHandler> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<DetailVacanciesDto> Handle(GetDetailVacancies request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.FindById(request.Id);
                if (result != null)
                {
                    var response = _mapper.Map<DetailVacanciesDto>(result);
                    return response;
                }
                return new DetailVacanciesDto();
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(GetDetailVacanciesHandler.Handle), e);
                return new DetailVacanciesDto();
            }
        }
    }
}