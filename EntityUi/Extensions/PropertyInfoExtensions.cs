using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace EntityUi.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static bool AttributeExists<T>(this PropertyInfo propertyInfo) where T : class
        {
            var attribute = propertyInfo.GetCustomAttributes(typeof(T), false)
                                        .FirstOrDefault() as T;
            if (attribute == null)
            {
                return false;
            }
            return true;
        }

        public static bool AttributeExists<T>(this Type type) where T : class
        {
            var attribute = type.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
            if (attribute == null)
            {
                return false;
            }
            return true;
        }

        public static T GetAttribute<T>(this Type type) where T : class
        {
            return type.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
        }

        public static T GetAttribute<T>(this PropertyInfo propertyInfo) where T : class
        {
            return propertyInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
        }        
		
        public static string LabelFromType(Type @type)
        {
            var att = GetAttribute<DisplayNameAttribute>(@type);
            return att != null ? att.DisplayName 
                       : @type.Name.ToSeparatedWords();
        }
		
        public static string GetLabel(this Object model)
        {
            return LabelFromType(model.GetType());
        }

        public static string GetLabel(this IEnumerable model)
        {
            var elementType = model.GetType().GetElementType();
            if (elementType == null)
            {
                elementType = model.GetType().GetGenericArguments()[0];
            }
            return LabelFromType(elementType);
        }

        public static string GetHeader(this Object model)
        {
            return GetLabel(model).Replace("Model", "");
        }

        public static string GetHeader(this IEnumerable model)
        {
            return GetLabel(model).Replace("Model", "");
        }

        public static string GetControllerName(this IEnumerable model)
        {
            return GetLabel(model).Replace(" ","").Replace("Model", "").Replace("[]", "");            
        }

        public static int GetCount(this IEnumerable model)
        {
            
            int count = 0;
            var enumerator = model.GetEnumerator();
            
            while (enumerator.MoveNext())
                count++;
            
            return count;
        }
    }
}