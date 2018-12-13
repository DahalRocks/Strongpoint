using Strongpoint.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strongpoint.Models
{
    public interface IReportRepository
    {
        Task<object> GetReport();
        Task<object> GetReportBySearch(Faktura faktura);
        
    }
}
