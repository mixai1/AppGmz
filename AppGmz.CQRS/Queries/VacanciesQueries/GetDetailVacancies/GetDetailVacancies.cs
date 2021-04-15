using System;
using AppGmz.Models.DtoModels.VacanciesDTO;
using MediatR;

namespace AppGmz.CQRS.Queries.VacanciesQueries.GetDetailVacancies
{
    public class GetDetailVacancies : IRequest<DetailVacanciesDto>
    {
        public Guid Id { get; }
        public GetDetailVacancies(Guid id)
        {
            Id = id;
        }
    }
}