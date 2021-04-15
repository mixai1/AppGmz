using AppGmz.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppGmz.CQRS.Commands.RecordNewsCommand.Remove
{
    public class RemoveRecordNewsHandler : IRequestHandler<RemoveRecordNews, bool>
    {
        private readonly IRecordNewsRepository _repository;
        private readonly ILogger<RemoveRecordNewsHandler> _logger;

        public RemoveRecordNewsHandler(IRecordNewsRepository repository, ILogger<RemoveRecordNewsHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<bool> Handle(RemoveRecordNews request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.RemoveById(request.Id);
                await _repository.SaveAsync(cancellationToken);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(RemoveRecordNewsHandler.Handle), e);
                return false;
            }
        }
    }
}