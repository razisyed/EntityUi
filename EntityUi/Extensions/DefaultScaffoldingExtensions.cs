using EntityUi.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EntityUi.Extensions
{
    public static class DefaultScaffoldingExtensions
    {
        public static string GetControllerName(this Type controllerType)
        {
            return controllerType.Name.Replace("Controller", String.Empty);
        }

        public static string GetActionName(this LambdaExpression actionExpression)
        {
            return ((MethodCallExpression)actionExpression.Body).Method.Name;
        }

        public static PropertyInfo[] VisibleProperties(this IEnumerable model)
        {
            var elementType = model.GetType().GetElementType();
            if (elementType == null)
            {
                elementType = model.GetType().GetGenericArguments()[0];
            }
            return elementType.GetProperties().Where(info => info.Name != elementType.IdentifierPropertyName() && RenderProperty(info)).OrderedByDisplayAttr().ToArray();
        }

        public static PropertyInfo[] VisibleProperties(this Object model)
        {
            return model.GetType().GetProperties().Where(info => info.Name != model.IdentifierPropertyName() && RenderProperty(info)).OrderedByDisplayAttr().ToArray();
        }

        public static PropertyInfo[] HiddenProperties(this Object model)
        {
            return model.GetType().GetProperties().Where(info => info.Name != model.IdentifierPropertyName() && info.IsHidden()).OrderedByDisplayAttr().ToArray();
        }

        public static bool IsGrid(this PropertyInfo property)
        {
            return String.CompareOrdinal(ModelMetadataProviders.Current.GetMetadataForProperty(null, property.DeclaringType,property.Name).TemplateHint, "Grid") == 0;
        }

        public static bool IsHidden(this PropertyInfo property)
        {
            return property.AttributeExists<HiddenInputAttribute>();            
        }

        public static bool IsHiddenInGrid(this PropertyInfo property)
        {
            return property.AttributeExists<HideInGrid>();
        }

        public static bool RenderProperty(this PropertyInfo property)
        {
            
            var action = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString().ToLower();
            var isGrid = IsGrid(property);

            return (isGrid && action == "details") || (!isGrid && !IsHidden(property));

        }

        // Support for Order property in DisplayAttribute ([Display(..., Order = n)])
        public static IOrderedEnumerable<PropertyInfo> OrderedByDisplayAttr(this IEnumerable<PropertyInfo> collection)
        {
            return collection.OrderBy(col =>
                                          {
                                              var attr = col.GetAttribute<DisplayAttribute>();
                                              return (attr != null ? attr.GetOrder() : null) ?? 0;
                                          });
        }

        public static RouteValueDictionary GetIdValue(this object model)
        {
            var v = new RouteValueDictionary();
            v.Add(model.IdentifierPropertyName(), model.GetId());
            return v;
        }

        public static object GetId(this object model)
        {
            return model.GetType().GetProperty(model.IdentifierPropertyName()).GetValue(model,new object[0]);
        }


        public static string IdentifierPropertyName(this Object model)
        {
            return IdentifierPropertyName(model.GetType());
        }

        public static string IdentifierPropertyName(this Type type)
        {
            if (type.GetProperties().Any(info => info.AttributeExists<KeyAttribute>()))
            {
                return
                    type.GetProperties().First(
                        info => info.AttributeExists<KeyAttribute>())
                        .Name;
            }
            if (type.GetProperties().Any(p => p.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase)))
            {
                return
                    type.GetProperties().First(
                        p => p.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase)).Name;
            }
            if (type.GetProperties().Any(p => p.Name.Equals(type.Name + "id", StringComparison.CurrentCultureIgnoreCase)))
            {
                return
                    type.GetProperties().First(
                        p => p.Name.Equals(type.Name + "id", StringComparison.CurrentCultureIgnoreCase)).Name;
            }
            return "";
        }

        public static string GetLabel(this PropertyInfo propertyInfo)
        {
            if (IsGrid(propertyInfo)) return ""; // No label needed for grids

            var meta = ModelMetadataProviders.Current.GetMetadataForProperty(null, propertyInfo.DeclaringType, propertyInfo.Name);
            return meta.GetDisplayName();
        }

        public static string ToSeparatedWords(this string value)
        {
            return Regex.Replace(value, "([A-Z][a-z])", " $1").Trim();
        }

    }
}