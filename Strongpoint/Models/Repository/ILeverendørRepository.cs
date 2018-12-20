using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strongpoint.Models.Repository
{
    public interface ILeverendørRepository
    {
        Task<object> GetLeverendør();
    }
}
