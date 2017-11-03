using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EntityUi.Extensions;
using System.Collections.Concurrent;
using AutoMapper;

namespace EntityUi.Helpers
{
    public class Helper
    {
        public static T EnumParse<T>(string s)
        {
            return (T) Enum.Parse(typeof (T), s.Replace(" ", ""));
        }

        public static IEnumerable<SelectListItem> EnumToList<T>(bool useIntValues=false)
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(v => new SelectListItem
            {
                Text = v.ToString().ToSeparatedWords(),
                Value = useIntValues ? (Convert.ToInt32(v)).ToString() : v.ToString()
            });
        }

        public static string AddKnockoutBinding(string html, string property, string databindtext, bool addvalue=true)
        {
            var hasEndTag = html.Contains("/>");
            var hasDataBind = html.Contains("data-bind");
            var hasValue = html.Contains("value:");
            var tagEndIndex =  hasDataBind ? html.IndexOf("data-bind=\"") : hasEndTag ?  html.IndexOf("/>") : html.IndexOf(">");

            if (tagEndIndex > 0)
            {
                html = html.Insert(tagEndIndex,
                                   string.Format(" {0}{1}{2}{3} ", 
                                                 (hasDataBind ? "" : "data-bind=\""),
                                                 (hasValue || !addvalue ? "" : (html.Contains("type=\"checkbox\"") ? "checked:" : "value: ") + property), 
                                                 ((!hasValue || hasDataBind) && !String.IsNullOrWhiteSpace(databindtext) ? ", ":"") + databindtext,
                                                 (hasDataBind ? "" : "\"")));                   
                    
            }

            return html;
        }

        public static bool IsNumeric(string value)
        {
            int n;
            return int.TryParse(value, out n);
        }
    }

    //public static class MapHelper
    //{
    //    private static readonly ConcurrentDictionary<string, object> Mappings = new ConcurrentDictionary<string, object>();

    //    public static Mapper<TFrom, TTo> GetMapper<TFrom, TTo>()
    //    {
    //        var key = typeof(TFrom).FullName + typeof(TTo).FullName;

    //        var mapper =
    //            (ObjectsMapper<TFrom, TTo>)
    //                Mappings.GetOrAdd(key, k => ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>(
    //                    new DefaultMapConfig()
    //                        .ConvertUsing<decimal?, decimal>(v => v.GetValueOrDefault())));

    //        return mapper;
    //    }

    //    public static TTo Map<TFrom, TTo>(TFrom obj)
    //        where TFrom : class
    //        where TTo : class
    //    {
    //        if (obj == null) return default(TTo);

    //        var mapper = GetMapper<TFrom, TTo>();

    //        return mapper.Map(obj);
    //    }

    //    public static List<TTo> MapManyToList<TFrom, TTo>(IEnumerable<TFrom> list, Action<TTo> additionalWork = null)
    //        where TFrom : class
    //        where TTo : class
    //    {
    //        if (list == null) return new List<TTo>();
    //        var mapper = GetMapper<TFrom, TTo>();

    //        if (additionalWork != null)
    //        {
    //            return list.Select(x =>
    //            {
    //                var y = mapper.Map(x);
    //                additionalWork(y);
    //                return y;
    //            }).ToList();
    //        }
    //        return list.Select(mapper.Map).ToList();
    //    }

    //    public static TTo[] MapManyToArray<TFrom, TTo>(IEnumerable<TFrom> list, Action<TTo> additionalWork = null)
    //        where TFrom : class
    //        where TTo : class
    //    {
    //        if (list == null) return new TTo[0];
    //        var mapper = GetMapper<TFrom, TTo>();

    //        if (additionalWork != null)
    //        {
    //            return list.Select(x =>
    //            {
    //                var y = mapper.Map(x);
    //                additionalWork(y);
    //                return y;
    //            }).ToArray();
    //        }

    //        return list.Select(mapper.Map).ToArray();
    //    }
    //}
}
