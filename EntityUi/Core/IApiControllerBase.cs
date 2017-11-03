using System;
using System.Net.Http;
namespace EntityUi.Core
{
    public interface IApiControllerBase<TView>
     where TView : IViewModelBase, new()
    {
        HttpResponseMessage Post(TView model);
        HttpResponseMessage Delete(int id);
        HttpResponseMessage Get(int id);
        
    }
}
