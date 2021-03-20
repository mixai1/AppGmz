using AppGmz.Models.DtoModels;
using MediatR;

namespace AppGmz.CQRS.Commands.RecordNewsCommand.Remove
{
    public class RemoveRecordNews : IRequest<bool>
    {
        public RemoveRecordNewsDto RemoveNewsRecordDto { get; }

        public RemoveRecordNews(RemoveRecordNewsDto removeNewsRecordDto)
        {
            RemoveNewsRecordDto = removeNewsRecordDto;
        }
    }
}