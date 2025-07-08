using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class HelperPropiedades
    {
        public static object GetPropertyValue(object obj, string propertyName)
        {
            if (propertyName.Contains("."))
            {
                var nameParts = propertyName.Split(new[] { '.' }, 2);
                return GetPropertyValue(GetPropertyValue(obj, nameParts[0]), nameParts[1]);
            }
            else
            {
                var property = TypeDescriptor.GetProperties(obj)[propertyName];
                return property.GetValue(obj);
            }
        }

        public static void SetPropertyValue(object obj, string propertyName, object newValue)
        {
            if (propertyName.Contains("."))
            {
                var nameParts = propertyName.Split(new[] { '.' }, 2);
                SetPropertyValue(GetPropertyValue(obj, nameParts[0]), nameParts[1], newValue);
            }
            else
            {
                var property = TypeDescriptor.GetProperties(obj)[propertyName];
                property.SetValue(obj, newValue);
            }
        }

        public static bool HasProperty(object obj, string propertyName)
        {
            var type = obj.GetType();
            var propertyInfo = type.GetProperty(propertyName);
            return propertyInfo != null;
        }
    }
}
