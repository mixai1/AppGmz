using AppGmz.Models.DtoModels.VacanciesDTO;
using MediatR;
using System.Collections.Generic;

namespace AppGmz.CQRS.Queries.VacanciesQueries.GetAllVacancies
{
    public class GetAllVacancies : IRequest<IEnumerable<ShowVacanciesDto>>
    {

    }
}