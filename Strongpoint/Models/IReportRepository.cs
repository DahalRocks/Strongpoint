using Strongpoint.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strongpoint.Models
{
    public interface IReportRepository
    {
        Tuple<object,int> GetReport(Faktura faktura);
        /*Task<Tuple<object, int>> GetReport();*/
        Tuple<object, int> GetReportBySearch(Faktura faktura);
        
    }
}
