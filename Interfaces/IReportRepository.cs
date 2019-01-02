using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IReportRepository
    {
        Task<Tuple<IEnumerable<IInvoice>, int>> GetReport(IInvoice faktura);
    }
}
