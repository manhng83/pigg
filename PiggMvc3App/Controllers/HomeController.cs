using System;
using System.Web.Mvc;
using Pigg.CQRS;
using Pigg.CQRS.Commands;
using Pigg.CQRS.ReadModel;
using PiggMvc3App.Helpers;
using Microsoft.Practices.Unity;
using Pigg.Model;
using Pigg.Contracts.Repositories;

namespace PiggMvc3App.Controllers
{
    [HandleError]
    public class HomeController : MyBaseController
    {
        private readonly IBus _bus;

        public HomeController()
        {
            _bus = ServiceLocator.Bus;
        }

        [Dependency]
        public ICustomPageReader ReadModelFacade { get; set; }

        public ActionResult Index()
        {
            CustomPage cp = ReadModelFacade.GetFrontPage(CurrentLanguage);
            if (cp == null)            
                return View();
            
            if (cp.IsPublished == false && User.IsInRole(Constants.AdminRole) == false)            
                return View();
            
            return View(cp); 
        }

        public ActionResult Details(int id)
        {
            CustomPage p = ReadModelFacade.GetById(id);
            if (p.IsFrontPage)
                return RedirectToAction("Index");
            return View(p);
        }

        [RequireWriter]
        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        [RequireWriter]
        [ValidateInput(false)]
        public ActionResult Create(CustomPage item)
        {
            if (ModelState.IsValid)
            {
                try
                {             
                    var entityId = Guid.NewGuid();
                    _bus.Send(new CreateCustomPage(-1, entityId, item.ParentId, item.LanguageIsoCode, item.Title,
                                                    item.LongTitle, item.Description, item.PageContent, item.Keywords,
                                                    item.IsPublished, item.IsFrontPage, item.ShowInList,
                                                    item.OrderInList, item.IsExternal, item.ExternalUrl));
                    var itemCreated = ReadModelFacade.GetById(entityId);
                    return RedirectToAction("Details", new { id = itemCreated.PageId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(Constants.ServiceErrorPlaceHolder,
                                             ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                    return View(item);
                }
            }
            return View(item);
        }

        [RequireWriter]
        public ActionResult Edit(int id)
        {
            CustomPage item = ReadModelFacade.GetById(id);
            return View(item);
        }

        [HttpPost]
        [RequireWriter]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            CustomPage item = null;
            try
            {
                item = ReadModelFacade.GetById(id);
                TryUpdateModel(item, collection);
                
                _bus.Send(new UpdateCustomPage(id, item.EntityId, item.ParentId, item.LanguageIsoCode, item.Title,
                                                    item.LongTitle, item.Description, item.PageContent, item.Keywords,
                                                    item.IsPublished, item.IsFrontPage, item.ShowInList,
                                                    item.OrderInList, item.IsExternal, item.ExternalUrl));

                return RedirectToAction("Details", new { id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(Constants.ServiceErrorPlaceHolder,
                                         ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                return View(item);
            }
        }

        [RequireWriter]
        public ActionResult Delete(int id)
        {
            CustomPage item = ReadModelFacade.GetById(id);
            return View(item);
        }
        
        [HttpPost]
        [RequireWriter]
        public ActionResult Delete(int id, FormCollection collection)
        {
            CustomPage item = null;
            try
            {
                item = ReadModelFacade.GetById(id);
               
                _bus.Send(new DeleteCustomPage(item.EntityId));

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(Constants.ServiceErrorPlaceHolder,
                                         ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                return View(item);
            }
        }
    }
}
