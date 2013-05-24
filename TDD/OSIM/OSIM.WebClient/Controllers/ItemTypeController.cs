using System;
using System.Collections.Generic;
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
            ViewData.Model = _unitOfWork.ItemTypes.FindAll();
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ItemType itemType)
        {
            _unitOfWork.ItemTypes.Add(itemType);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var itemType = _unitOfWork.ItemTypes.FindById(id);
            return View(itemType);
        }

        [HttpPost]
        public ActionResult Edit(ItemType itemType)
        {
            var editedItem = _unitOfWork.ItemTypes.FindById(itemType.Id);
            editedItem.Name = itemType.Name;
            _unitOfWork.Commit();

            return RedirectToAction("Index");
        }
    }
}
