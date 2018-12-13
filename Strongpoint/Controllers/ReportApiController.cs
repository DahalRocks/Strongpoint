using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Strongpoint.Models;
using Strongpoint.Models.ViewModel;

namespace Strongpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportApiController : ControllerBase
    {
        private readonly SQLDBNORGEContext _dbNorge;
        private readonly SQLDBSVERIGEContext _dbSverige;
        [BindProperty]
        public ReportViewModel ReportVM { get; set; }
        public ReportApiController(SQLDBNORGEContext dbNorge,SQLDBSVERIGEContext dbSverige)
        {
            _dbNorge = dbNorge;
            _dbSverige = dbSverige;
            ReportVM = new ReportViewModel()
            {
                Faktura = new Models.Faktura(),
                Leverendør = new Models.Leverendør()
            };
        }
        // GET: api/ReportApi
        [HttpGet]
       
        public object GetFaktura()
        {

            


            //List<ActivityLog> list = db.ActivityLog.Where(c => devicename == "" || c.Devices.devName.ToLower().Contains(devicename.ToLower()))
            //                                   .Where(c => user == "" || c.Users.uName.ToLower().Contains(user.ToLower()))
            //                                   .Where(c => alarm == "" || c.AlarmCodes.aName.ToLower().Contains(alarm.ToLower()))
            //                                   .OrderBy(c => c.dateTime).Skip(skip).Take(pageSize).ToList();
            var fakturaFraNorge = (from f in _dbNorge.Faktura
                           from l in _dbNorge.Leverendør.Where(x => x.Id == f.Leverendør_Id)
                           select new
                           {
                               fakturaNumber = f.Faktura_Nummer,
                               leverendørNavn = l.Navn,
                               attesteradAv=l.Attesterad_Av,
                               komment=l.Eventuella_Kommentarer
                           }).ToList();
            var fakturaFraSverige = (from f in _dbSverige.Faktura
                           from l in _dbSverige.Leverendør.Where(x => x.Id == f.Leverendør_Id)
                           select new
                           {
                               fakturaNumber = f.Faktura_Nummer,
                               leverendørNavn = l.Navn,
                               attesteradAv = l.Attesterad_Av,
                               komment = l.Eventuella_Kommentarer
                           }).ToList();
            return fakturaFraNorge;
            //var finalResult = from n in fakturaFraNorge
            //                  join s in fakturaFraSverige on 

            //var fakturaFraNorge = (from f in _dbNorge.Faktura
            //               from l in _dbNorge.Leverendør.Where(x => (x.Id == f.LeverendørId) && (f.FakturaNummer == 1004))
            //               select new
            //               {
            //                   fakturaNumber = f.FakturaNummer,
            //                   leverendørNavn = l.Navn
            //               }).ToList();
            //var fakturaFraNorge = (from f in _dbNorge.Faktura
            //               from l in _dbNorge.Leverendør.Where(x => (x.Id == f.LeverendørId) && (x.Id == 3))
            //               select new
            //               {
            //                   fakturaNumber = f.FakturaNummer,
            //                   leverendørNavn = l.Navn
            //               }).ToList();


        }
        //POST:get report using search options
        [HttpPost]
        [Route("{usingsearch}")]
        public object GetReportUsingSearch([FromBody]Faktura faktura)
        {
            var selectedInfo = (from f in _dbNorge.Faktura
                           from l in _dbNorge.Leverendør.Where(x => x.Id.Equals(f.Leverendør_Id))
                           select new
                           {
                               fakturaNumber = f.Faktura_Nummer,
                               leverendørNavn = l.Navn,
                               leverendørId = l.Id,
                               datoIntervall = f.Datum_Intervall,
                               attesteradAv = l.Attesterad_Av,
                               komment = l.Eventuella_Kommentarer

                           }).ToList();
            var filteredResult = new Object();
            if (faktura.Leverendør_Id != null && faktura.Faktura_Nummer != null && faktura.FraDato != null && faktura.TillDato != null)
            {
                filteredResult = selectedInfo.Where(f => f.fakturaNumber == ' ' || f.fakturaNumber == faktura.Faktura_Nummer)
                .Where(f => f.leverendørId == ' ' || f.leverendørId == faktura.Leverendør_Id)
                .Where(f => f.datoIntervall>faktura.FraDato && f.datoIntervall<faktura.TillDato).ToList();
            }
            else if (faktura.Faktura_Nummer != null && faktura.Leverendør_Id != null)
            {
                filteredResult = selectedInfo.Where(f => f.fakturaNumber == ' ' || f.fakturaNumber == faktura.Faktura_Nummer)
                .Where(f => f.leverendørId == ' ' || f.leverendørId == faktura.Leverendør_Id).ToList();
            }
            else if (faktura.Faktura_Nummer != null && faktura.FraDato != null && faktura.TillDato != null)
            {
                filteredResult = selectedInfo.Where(f => f.datoIntervall >= faktura.FraDato && f.datoIntervall <= faktura.TillDato)
                .Where(f => f.fakturaNumber == ' ' || f.fakturaNumber == faktura.Faktura_Nummer).ToList();
            }
            else if (faktura.Leverendør_Id != null && faktura.FraDato != null && faktura.TillDato != null)
            {
                filteredResult = selectedInfo.Where(f => f.datoIntervall >= faktura.FraDato && f.datoIntervall <= faktura.TillDato)
                .Where(f => f.leverendørId == ' ' || f.leverendørId == faktura.Leverendør_Id).ToList();
            }
            else if (faktura.Faktura_Nummer != null)
            {
                filteredResult = selectedInfo.Where(f => f.fakturaNumber == ' ' || f.fakturaNumber == faktura.Faktura_Nummer)
                .ToList();
            }
            else if (faktura.Leverendør_Id != null)
            {
                filteredResult = selectedInfo.Where(f => f.leverendørId == ' ' || f.leverendørId == faktura.Leverendør_Id)
                .ToList();
            }
            else if(faktura.FraDato != null && faktura.TillDato != null)
            {
                //check if fradato is greater than tilldato


                filteredResult = selectedInfo.Where(f => f.datoIntervall>=faktura.FraDato && f.datoIntervall<=faktura.TillDato)
                .ToList();
            }
            else
            {
                return selectedInfo;
            }

            return filteredResult;
            
        }

    }
}