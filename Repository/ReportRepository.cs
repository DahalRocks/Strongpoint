using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using DomainModel;
using Interfaces;
using System.Collections.Generic;

namespace Repository
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

        public async Task<Tuple<IEnumerable<IInvoice>, int>> GetReport(IInvoice faktura)
        {
            using (IDbConnection conn = ConnectionToNorge)
            using (IDbConnection connSv = ConnectionToSverige)
            {
                var searchOption = new DynamicParameters();
                    searchOption.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    searchOption.Add("@PageNumber", faktura.CurrentPage, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    searchOption.Add("@PageSize", faktura.PageSize, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    searchOption.Add("@Nummer", faktura.Faktura_Nummer, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    searchOption.Add("@LeverendørId", faktura.Leverendør_Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    searchOption.Add("@FraDato", faktura.FraDato, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                    searchOption.Add("@TillDato", faktura.TillDato, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                var result = await conn.QueryAsync<Invoice, Supplier, Invoice>(
                    "FakturaRecord_SearchOptions_FakNum_LevId_Dato",
                    (f, l) =>
                    {
                        f.Leverendør = l;
                        return f;
                    }, searchOption,
                    commandType: CommandType.StoredProcedure
                    );
                var totalRecordInNorge = searchOption.Get<int>("@TotalRows");
                var resultSv = await connSv.QueryAsync<Invoice, Supplier, Invoice>(
                    "FakturaRecord_SearchOptions_FakNum_LevId_Dato",
                    (f, l) =>
                    {
                        f.Leverendør = l;
                        return f;
                    }, searchOption,
                    commandType: CommandType.StoredProcedure
                    );
                var totalRecordInSverige = searchOption.Get<int>("@TotalRows");
                var totalRecords = totalRecordInNorge + totalRecordInSverige;
                var finalResult = result.Concat(resultSv);
                IEnumerable<IInvoice> invoices = finalResult.ToList<IInvoice>();
                return new Tuple<IEnumerable<IInvoice>, int>(invoices, totalRecords);
            }
        }
    }
}
