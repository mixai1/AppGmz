using AppGmz.Core;
using AppGmz.Models.DomainModels;
using Microsoft.Extensions.Logging;

namespace AppGmz.DAL.Repository
{
    public class VacanciesRepository : Repository<Vacancies>, IVacanciesRepository
    {
        public VacanciesRepository(AppDbContext appDbContext,
            ILogger<Repository<Vacancies>> logger) : base(appDbContext, logger) { }
    }
}