using System;
using System.Data.Entity;
using AutoMapper;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.IO;
using EntityUi.Data;

namespace EntityUi.Core
{
    /// <summary>
    /// Base controller that provides default Crud Methods built in
    /// </summary>
    /// <typeparam name="TDomain">Specific Domain Model Type</typeparam>
    /// <typeparam name="TView">Specific View Model type</typeparam>
    /// <typeparam name="TContext">Specific Data Context type</typeparam>
    public abstract class ApiControllerBase<TDomain, TView, TContext> : ApiController, IApiControllerBase<TView> 
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

        [HttpGet]
        public virtual HttpResponseMessage Ping()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual HttpResponseMessage Response(HttpStatusCode code, object result = null)
        {
            return result == null ?
                Request.CreateResponse(code) :
                Request.CreateResponse(code, result);
        }

        public virtual HttpResponseMessage ResponseSuccess(object result = null)
        {
            return Response(HttpStatusCode.OK, result);
        }

        public virtual HttpResponseMessage ResponseError(HttpStatusCode code = HttpStatusCode.BadRequest, string errorMessage = null)
        {
            return Response(code, errorMessage == null ? null : new { errorMessage = new[] { errorMessage } });
        }

        public virtual HttpResponseMessage ResponseError(HttpStatusCode code = HttpStatusCode.BadRequest, string[] errorMessageList = null)
        {
            return Response(code, errorMessageList == null ? null : new { errorMessage = errorMessageList });
        }

        public virtual HttpResponseMessage ResponseFile(HttpStatusCode code, byte[] content, string fileName, string mimeType = null)
        {
            HttpResponseMessage response = Request.CreateResponse(code);
            response.Content = new ByteArrayContent(content);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType ?? "application/octet-stream");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };

            return response;
        }

        public virtual HttpResponseMessage ResponseImage(HttpStatusCode code, byte[] content, string mimeType)
        {
            var result = Request.CreateResponse(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);

            result.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

            return result;
        }

        public virtual HttpResponseMessage ResponseImage(HttpStatusCode code, Stream content, string mimeType)
        {
            var result = Request.CreateResponse(HttpStatusCode.OK);
            result.Content = new StreamContent(content);


            result.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

            return result;
        }

        public virtual HttpResponseMessage Get(int id)
        {
            var item = Repository.Get(id);
            var model = GetMappedModel<TView>(item);
            return ResponseSuccess(model);
        }
    
        [HttpPost]
        public virtual HttpResponseMessage Post(TView model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id > 0)
                    {
                        Repository.Add(GetMappedModel<TDomain>(model));
                        return ResponseSuccess();
                    }
                    else
                    {
                        var item = Repository.Get(model.Id);
                        GetMappedModel(ref model, ref item);
                        Repository.Update(item);

                        return ResponseSuccess("Your changes were saved!");
                    }
                }
                
                return ResponseError(errorMessage: "Failed Validation");
            }
            catch (Exception ex)
            {
                return ResponseError(errorMessage:  "Unexpected error " + ex.Message);
            }
        }
        
        
        [HttpPost]
        public virtual HttpResponseMessage Delete(int id)
        {
            try
            {
                Repository.Delete(Repository.Get(id));

                return ResponseSuccess("Your item was deleted");

            }
            catch (Exception ex)
            {
                return ResponseError(errorMessage: "Unexpected error " + ex.Message);
            }
        }


        
        protected int GetUserId()
        {
            return 1;
        }
        
    }
}
