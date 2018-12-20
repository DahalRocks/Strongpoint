using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Strongpoint.Models.Repository
{
    public class LeverendørRepository:ILeverendørRepository
    {
        
        private readonly IConfiguration _config;
        public LeverendørRepository(IConfiguration config)
        {
            _config = config;
        }
        public IDbConnection ConnectionToNorge
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("ConnectionToNorge"));
            }
        }
        public async Task<object> GetLeverendør()
        {
            using (IDbConnection conn = ConnectionToNorge)
            {
                    var result = await conn.QueryAsync<Leverendør>(
                    "GetLeverendør_Norge",
                    commandType: CommandType.StoredProcedure
                    );
                return result;
            }
        }
    }
}
