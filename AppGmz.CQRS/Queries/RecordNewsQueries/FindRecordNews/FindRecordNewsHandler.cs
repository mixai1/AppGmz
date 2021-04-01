using AppGmz.Core;
using AppGmz.Models.DtoModels;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace AppGmz.CQRS.Queries.RecordNewsQueries.FindRecordNews
{
    public class FindRecordNewsHandler : IRequestHandler<FindRecordNews,FullRecordNewsDto>
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

        public async Task<FullRecordNewsDto> Handle(FindRecordNews request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.FindById(request.Id);
                if (result != null)
                {
                    var response = _mapper.Map<FullRecordNewsDto>(result);
                    return response;
                }

                _logger.LogError(nameof(FindRecordNewsHandler.Handle));
                return new FullRecordNewsDto();
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(FindRecordNewsHandler.Handle), e);
                return new FullRecordNewsDto();
            }
        }
    }
}