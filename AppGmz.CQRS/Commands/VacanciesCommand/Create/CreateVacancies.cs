using AppGmz.Models.DtoModels.VacanciesDTO;
using MediatR;

namespace AppGmz.CQRS.Commands.VacanciesCommand.Create
{
    public class CreateVacancies : IRequest<bool>
    {
        public CreateVacanciesDto CreateVacanciesDto { get; }
        public CreateVacancies(CreateVacanciesDto createVacanciesDto)
        {
            CreateVacanciesDto = createVacanciesDto;
        }

    }
}