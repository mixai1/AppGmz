using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AppGmz.Core;
using AppGmz.Models.DtoModels;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppGmz.CQRS.Queries.RecordNewsQueries.GetAllRecordNews
{
    public class GetAllRecordNewsHandler : IRequestHandler<GetAllRecordNews, IEnumerable<FoundRecordNewsDto>>
    {
        private readonly IRecordNewsRepository _repository;
        private readonly ILogger<GetAllRecordNewsHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllRecordNewsHandler(IRecordNewsRepository repository, ILogger<GetAllRecordNewsHandler> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FoundRecordNewsDto>> Handle(GetAllRecordNews request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.GetAllAsync();
                if (result != null)
                {
                    var response = _mapper.Map<IEnumerable<FoundRecordNewsDto>>(result);
                    return response;
                }
                return new List<FoundRecordNewsDto>();
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(GetAllRecordNewsHandler.Handle), e);
                return new List<FoundRecordNewsDto>();
            }
        }
    }
}
