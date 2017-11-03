﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using EntityUi.Extensions;
using EntityUi.Helpers;

namespace EntityUi.Core
{
   
    /// <summary>
    /// Base Repository that works with Base Domain and provides default CRUD methods using Entity Framework
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public abstract class RepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity, TContext> where TEntity : DomainModelBase where TContext: DbContext
    {
        /// <summary>
        /// Method to get Account ID, tries to use common methods, otherwise returns 0 
        /// </summary>
        /// <returns></returns>
        protected virtual int AccountId()
        {
            try
            {
                // Try to get it from Session
                if (HttpContext.Current.Session["AccountId"] != null && Helper.IsNumeric(HttpContext.Current.Session["AccountId"].ToString()))
                {
                    return Convert.ToInt32(HttpContext.Current.Session["AccountId"]);
                }

                // Next try getting Account Id from Claims
                if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var claim = ((ClaimsIdentity)HttpContext.Current.User.Identity);
                    var accountClaim = claim.Claims.FirstOrDefault(x => x.Type == "AccountId");

                    return accountClaim != null && Helper.IsNumeric(accountClaim.Value) ? Convert.ToInt32(accountClaim.Value) : 0;
                }

            }
            catch (Exception ex)
            {

            }

            return 0;
        }

        /// <summary>
        /// Method to get User ID, tries to use common methods, otherwise returns 0 
        /// </summary>
        /// <returns></returns>
        protected virtual int UserId()
        {
            try
            {
                // First try to get it from Session
                if (HttpContext.Current.Session["UserId"] != null && Helper.IsNumeric(HttpContext.Current.Session["UserId"].ToString()))
                {
                    return Convert.ToInt32(HttpContext.Current.Session["UserId"]);
                }

                // Next try getting User Id from Claims
                if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var claim = ((ClaimsIdentity)HttpContext.Current.User.Identity);
                    var accountClaim = claim.Claims.FirstOrDefault(x => x.Type == "UserId");

                    return accountClaim != null && Helper.IsNumeric(accountClaim.Value) ? Convert.ToInt32(accountClaim.Value) : 0;
                }
            }
            catch (Exception ex)
            {

            }

            return 0;
        }

        /// <summary>
        /// Method to get User ID, tries to use common methods, otherwise returns 0 
        /// </summary>
        /// <returns></returns>
        protected virtual string UserName()
        {
            // First try getting User Id from Claims
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return HttpContext.Current.User.Identity.Name;
            }

            // Next try to get it from Session
            if (HttpContext.Current.Session["UserName"] != null)
            {
                return HttpContext.Current.Session["UserName"] as string;
            }

            return "";
        }


        public virtual void Update(TEntity entity)
        {
            using (var context = GetContext())
            {
                entity.ModifiedBy = UserName();
                entity.Modified = DateTime.Now;

                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public virtual void Add(TEntity entity)
        {
            using (var context = GetContext())
            {
                if (entity.AccountId==0) entity.AccountId = AccountId();
                entity.CreatedBy = UserName();
                entity.Created = DateTime.Now;
                entity.Modified = DateTime.Now;

                context.Entry(entity).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public virtual void Delete(TEntity entity)
        {
            using (var context = GetContext())
            {
                context.Entry(entity).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public virtual TEntity Get(int id) 
        {
            using (var context = GetContext())
            {
                return context.Set<TEntity>().FirstOrDefault(x => x.Id == id);
            }
        }

        public virtual List<TEntity> List()
        {
            using (var context = GetContext())
            {
                var accountId = AccountId();
                return context.Set<TEntity>().Where(x => x.AccountId == accountId || accountId == 0).ToList();
            }
        }

        protected abstract TContext GetContext();
        
    }
}
