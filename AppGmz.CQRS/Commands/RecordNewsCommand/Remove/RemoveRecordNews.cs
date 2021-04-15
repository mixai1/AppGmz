using System;
using AppGmz.Models.DtoModels;
using MediatR;

namespace AppGmz.CQRS.Commands.RecordNewsCommand.Remove
{
    public class RemoveRecordNews : IRequest<bool>
    {
        public Guid Id { get; }

        public RemoveRecordNews(Guid id)
        {
            Id = id;
        }
    }
}