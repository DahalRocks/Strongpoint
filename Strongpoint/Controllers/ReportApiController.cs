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
        }
    }
}