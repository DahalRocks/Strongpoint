using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace Strongpoint.Models
{
    public class ReportRepository : IReportRepository
    {
        private readonly IConfiguration _config;
        public ReportRepository(IConfiguration config)
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

        public IDbConnection ConnectionToSverige
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("ConnectionToSverige"));
            }
        }

        public async Task<object> GetReport()
        {
            using (IDbConnection conn = ConnectionToNorge)
                using(IDbConnection connSv=ConnectionToSverige)
            {

                var result = await conn.QueryAsync<Faktura, Leverendør, Faktura>(
                    "FakturaRecord_SearchOptions_FakNum_LevId_Dato",
                    (f, l) =>
                    {
                        f.Leverendør = l;
                        return f;
                    },
                    commandType: CommandType.StoredProcedure
                    );
                var resultSv= await connSv.QueryAsync<Faktura, Leverendør, Faktura>(
                    "FakturaRecord_SearchOptions_FakNum_LevId_Dato",
                    (f, l) =>
                    {
                        f.Leverendør = l;
                        return f;
                    },
                    commandType: CommandType.StoredProcedure
                    );
                var finalResult = result.Concat(resultSv);
                return finalResult;
            }
        }

        public async Task<object> GetReportBySearch(Faktura faktura)
        {
            var pageSize = 5;
            var pageNumber = 1;
            using (IDbConnection conn = ConnectionToNorge)
            using (IDbConnection connSv = ConnectionToSverige)
            {
                var result = await conn.QueryAsync<Faktura, Leverendør, Faktura>(
                    "FakturaRecord_SearchOptions_FakNum_LevId_Dato",
                    (f, l) =>
                    {
                        f.Leverendør = l;
                        return f;
                    }, new { PageNumber = pageNumber, PageSize = pageSize, Nummer = faktura.Faktura_Nummer, LeverendørId=faktura.Leverendør_Id,FraDato=faktura.FraDato,TillDato=faktura.TillDato},
                    commandType: CommandType.StoredProcedure
                    
                    );
                var resultSv = await connSv.QueryAsync<Faktura, Leverendør, Faktura>(
                    "FakturaRecord_SearchOptions_FakNum_LevId_Dato",
                    (f, l) =>
                    {
                        f.Leverendør = l;
                        return f;
                    }, new { PageNumber = pageNumber, PageSize = pageSize, Nummer = faktura.Faktura_Nummer, LeverendørId = faktura.Leverendør_Id, FraDato = faktura.FraDato, TillDato = faktura.TillDato },
                    commandType: CommandType.StoredProcedure
                    );
                var finalResult = result.Concat(resultSv);
                return finalResult;
            }

        }
    }
}
