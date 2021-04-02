using System.Collections.Generic;
using AppGmz.Models.DtoModels;
using MediatR;

namespace AppGmz.CQRS.Queries.RecordNewsQueries.GetAllRecordNews
{
    public class GetAllRecordNews : IRequest<IEnumerable<FoundRecordNewsDto>>
    {

    }
}