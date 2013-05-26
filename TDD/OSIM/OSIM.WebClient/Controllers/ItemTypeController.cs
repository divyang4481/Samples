using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSIM.Core.Entities;
using OSIM.Core.Persistence;

namespace OSIM.WebClient.Controllers
{
    public class ItemTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemTypeController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var model = _unitOfWork.ItemTypes.Get();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ItemType itemType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.ItemTypes.Insert(itemType);
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View("Create", itemType);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var itemType = _unitOfWork.ItemTypes.GetById(id);
            return View("Edit", itemType);
        }

        [HttpPost]
        public ActionResult Edit(ItemType itemType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.ItemTypes.Update(itemType);
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View("Edit", itemType);
        }
    }
}
