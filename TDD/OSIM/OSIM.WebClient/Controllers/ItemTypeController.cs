using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSIM.Core.Persistence;

namespace OSIM.WebClient.Controllers
{
    public class ItemTypeController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public ItemTypeController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            ViewData.Model = _unitOfWork.ItemTypes.FindAll();
            return View();
        }

    }
}
