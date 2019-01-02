using Dapper;
using DomainModel;
using Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Repository
{
    public class SupplierRepository : ISupplierRepository
    {

        private readonly IConfiguration _config;
        public SupplierRepository(IConfiguration config)
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
        public async Task<IEnumerable<ISupplier>> GetSupplier()
        {
            using (IDbConnection conn = ConnectionToNorge)
            {
                IEnumerable<ISupplier> lstSupplier = await conn.QueryAsync<Supplier>(
                "GetLeverendør_Norge",
                commandType: CommandType.StoredProcedure
                );
                return lstSupplier;
            }
        }
    }
}
