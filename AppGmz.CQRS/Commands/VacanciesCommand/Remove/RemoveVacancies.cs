using MediatR;
using System;

namespace AppGmz.CQRS.Commands.VacanciesCommand.Remove
{
    public class RemoveVacancies : IRequest<bool>
    {
        public Guid Id { get; }

        public RemoveVacancies(Guid id)
        {
            Id = id;
        }
    }
}