using AppGmz.Core;
using AppGmz.Models.DtoModels;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AppGmz.CQRS.Queries.RecordNewsQueries.GetSomeRecordNews
{
    public class GetSomeRecordNewsHandler : IRequestHandler<GetSomeRecordNews, IEnumerable<FoundRecordNewsDto>>
    {
        private readonly IRecordNewsRepository _repository;
        private readonly ILogger<GetSomeRecordNewsHandler> _logger;
        private readonly IMapper _mapper;

        public GetSomeRecordNewsHandler(IRecordNewsRepository repository, ILogger<GetSomeRecordNewsHandler> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FoundRecordNewsDto>> Handle(GetSomeRecordNews request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.GetSomeRecords(request.Number);

                if (result != null)
                {
                    var response = _mapper.Map<IEnumerable<FoundRecordNewsDto>>(result);
                    return response;
                }
                return new List<FoundRecordNewsDto>();
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(GetSomeRecordNewsHandler.Handle), e);
                return new List<FoundRecordNewsDto>();
            }
        }
    }
}