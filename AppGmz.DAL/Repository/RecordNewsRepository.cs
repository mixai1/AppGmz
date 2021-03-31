using AppGmz.Core;
using AppGmz.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppGmz.DAL.Repository
{
    public class RecordNewsRepository : Repository<RecordNews>, IRecordNewsRepository
    {
        private readonly AppDbContext _appDbContext;

        public RecordNewsRepository(AppDbContext appDbContext, ILogger<RecordNewsRepository> logger) : base(appDbContext, logger)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<RecordNews>> GetSomeRecords(int numbers)
        {
            try
            {
                var result = await _appDbContext.RecordNewses.Take(numbers).ToListAsync();
                return result;

            }
            catch (Exception e)
            {
                Log.Error(nameof(this.GetSomeRecords), e);
                return new EnumerableQuery<RecordNews>(new List<RecordNews>());
            }
        }
    }
}