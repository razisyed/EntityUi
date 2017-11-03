using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using AutoMapper;
using EntityUi.Extensions;
using EntityUi.Helpers;
using EntityUi.Data;

namespace EntityUi.Core
{
    /// <summary>
    /// Base controller that provides default Crud Methods built in
    /// </summary>
    /// <typeparam name="TDomain">Specific Domain Model Type</typeparam>
    /// <typeparam name="TView">Specific View Model type</typeparam>
    /// <typeparam name="TContext">Specific Data Context type</typeparam>
    public abstract class ControllerBase<TDomain, TView, TContext> : Controller, IControllerBase<TView> 
        where TDomain : IDataModelBase where TView : IViewModelBase, new() where TContext: DbContext
    {
        protected IRepositoryBase<TDomain, TContext> Repository { get; set; }

        private T[] GetMappedModel<T>(TDomain[] items){
            try
            {
                return Mapper.Map<T[]>(items);
            }
            catch
            {
                Mapper.CreateMap<T, TDomain>().ReverseMap();

                try
                {
                    return Mapper.Map<T[]>(items);
                }
                catch
                {
                    return Mapper.DynamicMap<T[]>(items);
                }

            }
        }

        private T GetMappedModel<T>(TView model)
        {
            model = SetupModel(model);
            try
            {
                return Mapper.Map<T>(model);
            }
            catch
            {
                Mapper.CreateMap<TView, T>().ReverseMap();

                try
                {
                    return Mapper.Map<T>(model);
                }
                catch
                {
                    return Mapper.DynamicMap<T>(model);
                }

            }
        }

        private T GetMappedModel<T>(TDomain model)
        {
            try
            {
                return Mapper.Map<T>(model);
            }
            catch
            {
                Mapper.CreateMap<T, TDomain>().ReverseMap();

                try
                {
                    return Mapper.Map<T>(model);
                }
                catch
                {
                    return Mapper.DynamicMap<T>(model);
                }

            }
        }


        private void GetMappedModel(ref TView model, ref TDomain item)
        {
            model = SetupModel(model);
            try
            {
                Mapper.Map(model, item);
            }
            catch
            {
                Mapper.CreateMap<TView, TDomain>().ReverseMap();

                try
                {
                    Mapper.Map(model, item);
                }
                catch
                {
                    Mapper.Map(model, item);
                }

            }
        }

        public virtual ActionResult Index(bool useAjax=false)
        {
            var items = Repository.List().ToArray();
            var model = GetMappedModel<TView>(items);

            if (useAjax)            
                return View("IndexAjax",model);            
            else
                return View(model);
        }
        
        public virtual TView SetupModel(TView model)
        {
            try
            {
                if (ViewBag.ParentId != null) model.ParentId = (int)ViewBag.ParentId;
            }
            catch (Exception ex)
            {

            }
            return model;
        }

        [HttpPost]
        public virtual JsonResult CreateAjax(TView model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Repository.Add(GetMappedModel<TDomain>(model));
                    return Json(new { success = "Your changes were saved!" });
                }

                Error("There were some errors in your form.");
                return Json(new { error = "There were some errors in your form." });
            }
            catch (Exception ex)
            {
                return Json(new { error = "Unexpected error " + ex.Message });
            }
        }

        public virtual ActionResult CreateAjax()
        {
            var model = new TView();
            return PartialView("_Create", model);
        }


        public virtual ActionResult EditAjax(int id)
        {
            var item = Repository.Get(id);
            var model = GetMappedModel<TView>(item);
            return PartialView("_Create", model);
        }

        [HttpPost]
        public virtual JsonResult EditAjax(TView model, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var item = Repository.Get(id);
                    GetMappedModel(ref model, ref item);
                    item.Id = id;
                    Repository.Update(item);

                    return Json(new { success = "Your changes were saved!" });
                }

                Error("There were some errors in your form.");
                return Json(new { error = "There were some errors in your form." });
            }
            catch (Exception ex)
            {
                return Json(new { error = "Unexpected error " + ex.Message });
            }
        }


        
        [HttpPost]
        public virtual JsonResult DeleteAjax(int id)
        {
            try
            {
                Repository.Delete(Repository.Get(id));

                return Json(new { success = "Your item was deleted" });

            }
            catch (Exception ex)
            {
                return Json(new { error = "Unexpected error " + ex.Message });
            }
        }

       
        [HttpPost]
        public virtual ActionResult Create(TView model)
        {
            if (ModelState.IsValid)
            {
                Repository.Add(GetMappedModel<TDomain>(model));
                Success("Your changes were saved!");
                return RedirectToAction("Index");
            }

            Error("There were some errors in your form.");
            return View(model);
        }

        public virtual ActionResult Create()
        {
            var model = new TView();
            return View(model);
        }


        public virtual ActionResult Edit(int id)
        {
            var item = Repository.Get(id);
            var model = GetMappedModel<TView>(item);
            return View("Create", model);
        }

        [HttpPost]
        public virtual ActionResult Edit(TView model, int id)
        {
            if (ModelState.IsValid)
            {

                var item = Repository.Get(id);
                GetMappedModel(ref model, ref item);
                item.Id = id;
                Repository.Update(item);

                Success("The item was updated!");
                return RedirectToAction("index");
            }
            return View("Create", model);
        }

        public virtual ActionResult Details(int id)
        {
            ViewBag.ParentId = id;

            var item = Repository.Get(id);
            var model = GetMappedModel<TView>(item);            
            return View(model);
        }

        public virtual ActionResult Delete(int id)
        {
            Repository.Delete(Repository.Get(id));
            Information("Your item was deleted");

            return RedirectToAction("index");
        }

        
        protected int GetUserId()
        {
            return 1;
        }
        
        public void Attention(string message)
        {
            TempData.Add(Alerts.ATTENTION, message);
        }

        public void Success(string message)
        {
            TempData.Add(Alerts.SUCCESS, message);
        }

        public void Information(string message)
        {
            TempData.Add(Alerts.INFORMATION, message);
        }

        public void Error(string message)
        {
            TempData.Add(Alerts.ERROR, message);
        }
    }
}
