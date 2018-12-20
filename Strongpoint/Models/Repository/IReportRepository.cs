using System;
using System.Threading.Tasks;

namespace Strongpoint.Models
{
    public interface IReportRepository
    {
        Task<Tuple<object,int>> GetReport(Faktura faktura);
    }
}
