using AppGmz.Models.DtoModels;
using MediatR;
using System.Collections.Generic;

namespace AppGmz.CQRS.Queries.RecordNewsQueries.GetSomeRecordNews
{
    public class GetSomeRecordNews : IRequest<IEnumerable<FoundRecordNewsDto>>
    {
        public int Number { get; }

        public GetSomeRecordNews(int number)
        {
            Number = number;
        }

    }
}