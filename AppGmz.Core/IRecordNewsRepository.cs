using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppGmz.Models.DomainModels;

namespace AppGmz.Core
{
    public interface IRecordNewsRepository : IRepository<RecordNews>
    {
        // this method is needed to return a certain
        // (number of records in method arguments) number of records
        Task<IEnumerable<RecordNews>> GetSomeRecords(int numbers);
    }
}