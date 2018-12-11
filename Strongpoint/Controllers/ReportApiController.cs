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
        [BindProperty]
        public ReportViewModel ReportVM { get; set; }
        public ReportApiController(SQLDBNORGEContext dbNorge)
        {
            _dbNorge = dbNorge;
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
            var faktura = (from f in _dbNorge.Faktura
                           from l in _dbNorge.Leverendør.Where(x => x.Id == f.LeverendørId)
                           select new
                           {
                               fakturaNumber = f.FakturaNummer,
                               leverendørNavn = l.Navn,
                               attesteradAv=l.AttesteradAv,
                               komment=l.EventuellaKommentarer
                           }).ToList();
            return faktura;

            //var faktura = (from f in _dbNorge.Faktura
            //               from l in _dbNorge.Leverendør.Where(x => (x.Id == f.LeverendørId) && (f.FakturaNummer == 1004))
            //               select new
            //               {
            //                   fakturaNumber = f.FakturaNummer,
            //                   leverendørNavn = l.Navn
            //               }).ToList();
            //var faktura = (from f in _dbNorge.Faktura
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
                           from l in _dbNorge.Leverendør.Where(x => x.Id.Equals(f.LeverendørId))
                           select new
                           {
                               fakturaNumber = f.FakturaNummer,
                               leverendørNavn = l.Navn,
                               leverendørId = l.Id,
                               datoIntervall = f.DatumIntervall,
                               attesteradAv = l.AttesteradAv,
                               komment = l.EventuellaKommentarer

                           }).ToList();
            var filteredResult = new Object();
            if (faktura.LeverendørId != null && faktura.FakturaNummer != null && faktura.FraDato != null && faktura.TillDato != null)
            {
                filteredResult = selectedInfo.Where(f => f.fakturaNumber == ' ' || f.fakturaNumber == faktura.FakturaNummer)
                .Where(f => f.leverendørId == ' ' || f.leverendørId == faktura.LeverendørId)
                .Where(f => f.datoIntervall>faktura.FraDato && f.datoIntervall<faktura.TillDato).ToList();
            }
            else if (faktura.FakturaNummer != null && faktura.LeverendørId != null)
            {
                filteredResult = selectedInfo.Where(f => f.fakturaNumber == ' ' || f.fakturaNumber == faktura.FakturaNummer)
                .Where(f => f.leverendørId == ' ' || f.leverendørId == faktura.LeverendørId).ToList();
            }
            else if (faktura.FakturaNummer != null && faktura.FraDato != null && faktura.TillDato != null)
            {
                filteredResult = selectedInfo.Where(f => f.datoIntervall >= faktura.FraDato && f.datoIntervall <= faktura.TillDato)
                .Where(f => f.fakturaNumber == ' ' || f.fakturaNumber == faktura.FakturaNummer).ToList();
            }
            else if (faktura.LeverendørId != null && faktura.FraDato != null && faktura.TillDato != null)
            {
                filteredResult = selectedInfo.Where(f => f.datoIntervall >= faktura.FraDato && f.datoIntervall <= faktura.TillDato)
                .Where(f => f.leverendørId == ' ' || f.leverendørId == faktura.LeverendørId).ToList();
            }
            else if (faktura.FakturaNummer != null)
            {
                filteredResult = selectedInfo.Where(f => f.fakturaNumber == ' ' || f.fakturaNumber == faktura.FakturaNummer)
                .ToList();
            }
            else if (faktura.LeverendørId != null)
            {
                filteredResult = selectedInfo.Where(f => f.leverendørId == ' ' || f.leverendørId == faktura.LeverendørId)
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