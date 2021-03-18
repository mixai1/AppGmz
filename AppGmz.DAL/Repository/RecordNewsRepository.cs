using AppGmz.Core;
using AppGmz.Models.DomainModels;
using Microsoft.Extensions.Logging;

namespace AppGmz.DAL.Repository
{
    public class RecordNewsRepository : Repository<RecordNews>, IRecordNewsRepository
    {
        private readonly AppDbContext _appDbContext;

        public RecordNewsRepository(AppDbContext appDbContext, ILogger<RecordNewsRepository> logger) : base(appDbContext, logger)
        {
            _appDbContext = appDbContext;
        }

    }
}