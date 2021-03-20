using AppGmz.Models.DtoModels;
using MediatR;

namespace AppGmz.CQRS.Commands.RecordNewsCommand.Create
{
    public class CreateRecordNews : IRequest<bool>
    {
        public CreateRecordNewsDto NewsRecordDto { get; }

        public CreateRecordNews(CreateRecordNewsDto newsRecordDto)
        {
            NewsRecordDto = newsRecordDto;
        }
    }
}