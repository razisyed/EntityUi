using System;
namespace EntityUi.Core
{
    public interface IControllerBase<TView>
     where TView : IViewModelBase, new()
    {
        void Attention(string message);
        System.Web.Mvc.ActionResult Create();
        System.Web.Mvc.ActionResult Create(TView model);
        System.Web.Mvc.ActionResult CreateAjax();
        System.Web.Mvc.JsonResult CreateAjax(TView model);
        System.Web.Mvc.ActionResult Delete(int id);
        System.Web.Mvc.JsonResult DeleteAjax(int id);
        System.Web.Mvc.ActionResult Details(int id);
        System.Web.Mvc.ActionResult Edit(int id);
        System.Web.Mvc.ActionResult Edit(TView model, int id);
        System.Web.Mvc.ActionResult EditAjax(int id);
        System.Web.Mvc.JsonResult EditAjax(TView model, int id);
        void Error(string message);
        System.Web.Mvc.ActionResult Index(bool useAjax = false);
        void Information(string message);
        void Success(string message);
    }
}
