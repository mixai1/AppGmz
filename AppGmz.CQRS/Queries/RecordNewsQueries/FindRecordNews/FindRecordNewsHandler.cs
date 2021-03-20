using AppGmz.Core;
using AppGmz.Models.DtoModels;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppGmz.CQRS.Queries.RecordNewsQueries.FindRecordNews
{
    public class FindRecordNewsHandler
    {
        private readonly IRecordNewsRepository _repository;
        private readonly ILogger<FindRecordNewsHandler> _logger;
        private readonly IMapper _mapper;

        public FindRecordNewsHandler(IRecordNewsRepository repository, ILogger<FindRecordNewsHandler> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<FoundRecordNewsDTO> Handle(FindRecordNews request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.FindById(request.Id);
                if (result != null)
                {
                    var response = _mapper.Map<FoundRecordNewsDTO>(result);
                    return response;
                }

                _logger.LogError(nameof(FindRecordNewsHandler.Handle));
                return new FoundRecordNewsDTO();
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(FindRecordNewsHandler.Handle), e);
                return new FoundRecordNewsDTO();
            }
        }
    }
}